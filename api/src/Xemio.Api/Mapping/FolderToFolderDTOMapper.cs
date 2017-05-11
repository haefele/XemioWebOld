using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Raven.Client;
using Raven.Client.Linq;
using Xemio.Api.Data.Models.Notes;
using Xemio.Api.Entities.Notes;
using Xemio.Api.Extensions;

namespace Xemio.Api.Mapping
{
    public class FolderToFolderDTOMapper : MapperBase<Folder, FolderDTO>
    {
        private readonly IAsyncDocumentSession _documentSession;

        public FolderToFolderDTOMapper(IAsyncDocumentSession documentSession)
        {
            this._documentSession = documentSession;
        }

        public override async Task<FolderDTO> MapAsync(Folder input, CancellationToken cancellationToken = default(CancellationToken))
        {
            var hierarchy = await this._documentSession.LoadOrCreateHierarchyAsync(input.UserId, cancellationToken);
            return this.ToFolderDTO(input, hierarchy);
        }
        
        private FolderDTO ToFolderDTO(Folder folder, FoldersNotesHierarchy hierarchy)
        {
            var simpleFolder = hierarchy.GetFolder(folder.Id);
            var parentFolder = hierarchy.GetParentFolder(folder.Id);
            var parentFolderList = hierarchy.GetParentFolderList(folder.Id);

            return new FolderDTO
            {
                Id = this._documentSession.ToLongId(folder.Id),
                Name = folder.Name,
                ParentFolder = new SimpleFolderDTO
                {
                    Id = this._documentSession.ToLongId(parentFolder.FolderId),
                    Name = parentFolder.FolderName
                },
                ParentFolderHierarchy = parentFolderList.Select(f => new SimpleFolderDTO
                {
                    Id = this._documentSession.ToLongId(f.FolderId),
                    Name = f.FolderName
                }).ToList(),
                SubFolders = simpleFolder.SubFolders.Select(f => new SimpleFolderDTO
                {
                    Id = this._documentSession.ToLongId(f.FolderId),
                    Name = f.FolderName
                }).ToList(),
                Notes = simpleFolder.Notes.Select(f => new SimpleNoteDTO
                {
                    Id = this._documentSession.ToLongId(f.NoteId),
                    Title = f.NoteTitle
                }).ToList(),
            };
        }
    }
}
