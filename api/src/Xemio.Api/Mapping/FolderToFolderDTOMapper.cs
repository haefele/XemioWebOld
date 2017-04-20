﻿using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Raven.Client;
using Raven.Client.Linq;
using Xemio.Api.Data.Models.Notes;
using Xemio.Api.Database.Indexes;
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
            if (input == null)
                return null;

            var counts = await this._documentSession.Query<Folders_ByChildrenCount.Result, Folders_ByChildrenCount>()
                .Where(f => f.FolderId == input.Id)
                .FirstOrDefaultAsync(cancellationToken);

            return this.ToFolderDTO(input, counts);
        }

        public override async Task<IList<FolderDTO>> MapListAsync(IList<Folder> input, CancellationToken cancellationToken = default(CancellationToken))
        {
            var folderIds = input.Where(f => f != null).Select(f => f.Id).Distinct().ToList();

            var counts = await this._documentSession.Query<Folders_ByChildrenCount.Result, Folders_ByChildrenCount>()
                .Where(f => f.FolderId.In(folderIds))
                .ToListAsync(cancellationToken);

            return input
                .Select(f => f == null 
                    ? null 
                    : this.ToFolderDTO(f, counts.FirstOrDefault(d => d.FolderId == f.Id)))
                .ToList();
        }

        private FolderDTO ToFolderDTO(Folder folder, Folders_ByChildrenCount.Result counts)
        {
            return new FolderDTO
            {
                Id = this._documentSession.ToLongId(folder.Id),
                Name = folder.Name,
                ParentFolderId = folder.ParentFolderId == null ? (long?)null : this._documentSession.ToLongId(folder.ParentFolderId),
                NotesCount = counts?.NotesCount ?? 0,
                SubFoldersCount = counts?.SubFolderCount ?? 0,
            };
        }
    }
}
