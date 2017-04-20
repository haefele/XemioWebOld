﻿using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EnsureThat;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Raven.Client;
using Xemio.Api.Data.Models.Notes;
using Xemio.Api.Database.Indexes;
using Xemio.Api.Entities.Notes;
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
            var folders = await this._documentSession.Query<Folder, Folders_ForQuery>()
                .Where(f => f.UserId == this.User.Identity.Name && f.ParentFolderId == null)
                .ToListAsync(cancellationToken);

            var folderDTOs = await this._folderToFolderDTOMapper.MapListAsync(folders, cancellationToken);

            return this.Ok(folderDTOs);
        }

        [HttpGet("{folderId:long}/folders", Name = RouteNames.GetSubFolders)]
        public async Task<IActionResult> GetSubFoldersAsync([Required]long? folderId, CancellationToken cancellationToken = default(CancellationToken))
        {
            var folder = await this._documentSession.LoadAsync<Folder>(folderId, cancellationToken);

            if (folder == null || folder.UserId != this.User.Identity.Name)
                return this.NotFound();

            var folders = await this._documentSession.Query<Folder, Folders_ForQuery>()
                .Where(f => f.UserId == this.User.Identity.Name && f.ParentFolderId == folder.Id)
                .ToListAsync(cancellationToken);

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
            var parentFolder = data.ParentFolderId != null
                ? await this._documentSession.LoadAsync<Folder>(data.ParentFolderId.Value, cancellationToken)
                : null;

            var folder = new Folder
            {
                Name = data.Name,
                ParentFolderId = parentFolder?.Id,
                UserId = this.User.Identity.Name
            };

            await this._documentSession.StoreAsync(folder, cancellationToken);
            await this._documentSession.SaveChangesAsync(cancellationToken);

            var folderDTO = await this._folderToFolderDTOMapper.MapAsync(folder, cancellationToken);

            return this.CreatedAtRoute(RouteNames.GetFolderById, new { folderId = folderDTO.Id }, folderDTO);
        }

        [HttpPatch("{folderId:long}", Name = RouteNames.UpdateFolder)]
        public async Task<IActionResult> UpdateFolderAsync([Required]long? folderId, [FromBody][Required]UpdateFolder data, CancellationToken cancellationToken = default(CancellationToken))
        {
            var folder = await this._documentSession.LoadAsync<Folder>(folderId, cancellationToken);

            if (folder == null || folder.UserId != this.User.Identity.Name)
                return this.NotFound();

            if (data.HasName())
            {
                folder.Name = data.Name;
            }

            if (data.HasParentFolderId())
            {
                if (data.ParentFolderId == null)
                {
                    folder.ParentFolderId = null;
                }
                else
                {
                    var parentFolder = await this._documentSession.LoadAsync<Folder>(data.ParentFolderId.Value, cancellationToken);

                    if (parentFolder != null && parentFolder.UserId == this.User.Identity.Name)
                        folder.ParentFolderId = parentFolder.Id;
                }
            }

            await this._documentSession.SaveChangesAsync(cancellationToken);

            var folderDTO = await this._folderToFolderDTOMapper.MapAsync(folder, cancellationToken);

            return this.Ok(folderDTO);
        }

        [HttpDelete("{folderId:long}", Name = RouteNames.DeleteFolder)]
        public async Task<IActionResult> DeleteFolderAsync([Required]long? folderId, CancellationToken cancellationToken = default(CancellationToken))
        {
            Folder folder = await this._documentSession.LoadAsync<Folder>(folderId, cancellationToken);

            if (folder == null || folder.UserId != this.User.Identity.Name)
                return this.NotFound();
            
            this._documentSession.Delete(folder);
            await this._documentSession.SaveChangesAsync(cancellationToken);

            return this.Ok();
        }
    }
}
