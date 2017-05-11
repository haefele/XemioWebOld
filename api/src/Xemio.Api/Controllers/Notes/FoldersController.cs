using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EnsureThat;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Raven.Client;
using Xemio.Api.Data.Models.Notes;
using Xemio.Api.Entities.Notes;
using Xemio.Api.Extensions;
using Xemio.Api.Mapping;

namespace Xemio.Api.Controllers.Notes
{
    [Authorize]
    [Route("notes/folders")]
    public class FoldersController : ControllerBase
    {
        public static class RouteNames
        {
            public const string GetRootFolders = nameof(GetRootFolders);
            public const string GetSubFolders = nameof(GetSubFolders);
            public const string GetFolderById = nameof(GetFolderById);
            public const string CreateFolder = nameof(CreateFolder);
            public const string UpdateFolder = nameof(UpdateFolder);
            public const string DeleteFolder = nameof(DeleteFolder);
        }

        private readonly IAsyncDocumentSession _documentSession;
        private readonly IMapper<Folder, FolderDTO> _folderToFolderDTOMapper;

        public FoldersController(IAsyncDocumentSession documentSession, IMapper<Folder, FolderDTO> folderToFolderDTOMapper)
        {
            EnsureArg.IsNotNull(documentSession, nameof(documentSession));
            EnsureArg.IsNotNull(folderToFolderDTOMapper, nameof(folderToFolderDTOMapper));

            this._documentSession = documentSession;
            this._folderToFolderDTOMapper = folderToFolderDTOMapper;
        }

        [HttpGet(Name = RouteNames.GetRootFolders)]
        public async Task<IActionResult> GetRootFoldersAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var hierarchy = await this._documentSession.LoadOrCreateHierarchyAsync(this.User.Identity.Name, cancellationToken);

            var rootFolderIds = hierarchy.GetRootFolderIds();

            var folders = await this._documentSession.LoadAsync<Folder>(rootFolderIds, cancellationToken);
            var folderDTOs = await this._folderToFolderDTOMapper.MapListAsync(folders, cancellationToken);

            return this.Ok(folderDTOs);
        }

        [HttpGet("{folderId:long}/folders", Name = RouteNames.GetSubFolders)]
        public async Task<IActionResult> GetSubFoldersAsync([Required]long? folderId, CancellationToken cancellationToken = default(CancellationToken))
        {
            var stringFolderId = this._documentSession.ToStringId<Folder>(folderId);

            var hierarchy = await this._documentSession.LoadOrCreateHierarchyAsync(this.User.Identity.Name, cancellationToken);
            
            if (hierarchy.HasFolder(stringFolderId) == false)
                return this.NotFound();

            var subFolderIds = hierarchy.GetSubFolderIds(stringFolderId);
            
            var folders = await this._documentSession.LoadAsync<Folder>(subFolderIds, cancellationToken);
            var folderDTOs = await this._folderToFolderDTOMapper.MapListAsync(folders, cancellationToken);

            return this.Ok(folderDTOs);
        }

        [HttpGet("{folderId:long}", Name = RouteNames.GetFolderById)]
        public async Task<IActionResult> GetFolderByIdAsync([Required]long? folderId, CancellationToken cancellationToken = default(CancellationToken))
        {
            var folder = await this._documentSession.LoadAsync<Folder>(folderId, cancellationToken);

            if (folder == null || folder.UserId != this.User.Identity.Name)
                return this.NotFound();

            var folderDTO = await this._folderToFolderDTOMapper.MapAsync(folder, cancellationToken);

            return this.Ok(folderDTO);
        }

        [HttpPost(Name = RouteNames.CreateFolder)]
        public async Task<IActionResult> CreateFolderAsync([FromBody][Required]CreateFolder data, CancellationToken cancellationToken = default(CancellationToken))
        {
            var hierarchy = await this._documentSession.LoadOrCreateHierarchyAsync(this.User.Identity.Name, cancellationToken);
            
            if (data.ParentFolderId.HasValue)
            {
                string stringParentFolderId = this._documentSession.ToStringId<Folder>(data.ParentFolderId.Value);
                
                if (hierarchy.HasFolder(stringParentFolderId) == false)
                    data.ParentFolderId = null;
            }

            var folder = new Folder
            {
                Name = data.Name,
                UserId = this.User.Identity.Name
            };

            await this._documentSession.StoreAsync(folder, cancellationToken);

            hierarchy.AddNewFolder(folder, this._documentSession.ToStringId<Folder>(data.ParentFolderId));

            if (hierarchy.Validate() == false)
                return this.BadRequest();

            await this._documentSession.SaveChangesAsync(cancellationToken);

            var folderDTO = await this._folderToFolderDTOMapper.MapAsync(folder, cancellationToken);

            return this.CreatedAtRoute(RouteNames.GetFolderById, new { folderId = folderDTO.Id }, folderDTO);
        }

        [HttpPatch("{folderId:long}", Name = RouteNames.UpdateFolder)]
        public async Task<IActionResult> UpdateFolderAsync([Required]long? folderId, [FromBody][Required]UpdateFolder data, CancellationToken cancellationToken = default(CancellationToken))
        {
            var hierarchy = await this._documentSession.LoadOrCreateHierarchyAsync(this.User.Identity.Name, cancellationToken);

            if (hierarchy.HasFolder(this._documentSession.ToStringId<Folder>(folderId)) == false)
                return this.NotFound();

            var folder = await this._documentSession.LoadAsync<Folder>(folderId, cancellationToken);

            if (data.HasName())
            {
                folder.Name = data.Name;
                hierarchy.UpdateFolderName(folder.Id, folder.Name);
            }

            if (data.HasParentFolderId())
            {
                hierarchy.UpdateParentFolderId(folder.Id, this._documentSession.ToStringId<Folder>(data.ParentFolderId));
            }

            if (hierarchy.Validate() == false)
                return this.BadRequest();

            await this._documentSession.SaveChangesAsync(cancellationToken);

            var folderDTO = await this._folderToFolderDTOMapper.MapAsync(folder, cancellationToken);

            return this.Ok(folderDTO);
        }

        [HttpDelete("{folderId:long}", Name = RouteNames.DeleteFolder)]
        public async Task<IActionResult> DeleteFolderAsync([Required]long? folderId, CancellationToken cancellationToken = default(CancellationToken))
        {
            var stringFolderId = this._documentSession.ToStringId<Folder>(folderId);

            var hierarchy = await this._documentSession.LoadOrCreateHierarchyAsync(this.User.Identity.Name, cancellationToken);

            if (hierarchy.HasFolder(stringFolderId) == false)
                return this.NotFound();

            var folderToDelete = hierarchy.GetFolder(stringFolderId);

            var allFolders = folderToDelete.SubFolders.Flatten(f => f.SubFolders).Concat(new[] {folderToDelete}).ToList();
            var allNotes = allFolders.SelectMany(f => f.Notes).ToList();

            foreach (var folder in allFolders)
            {
                this._documentSession.Delete(folder.FolderId);
            }
            foreach (var note in allNotes)
            {
                this._documentSession.Delete(note);
            }

            hierarchy.DeleteFolder(stringFolderId);

            await this._documentSession.SaveChangesAsync(cancellationToken);

            return this.Ok();
        }
    }
}
