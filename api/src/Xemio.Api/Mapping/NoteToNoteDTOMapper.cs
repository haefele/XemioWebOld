using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EnsureThat;
using Raven.Client;
using Xemio.Api.Data.Models.Notes;
using Xemio.Api.Entities.Notes;
using Xemio.Api.Extensions;

namespace Xemio.Api.Mapping
{
    public class NoteToNoteDTOMapper : MapperBase<Note, NoteDTO>
    {
        private readonly IAsyncDocumentSession _documentSession;

        public NoteToNoteDTOMapper(IAsyncDocumentSession documentSession)
        {
            EnsureArg.IsNotNull(documentSession, nameof(documentSession));

            this._documentSession = documentSession;
        }

        public override async Task<NoteDTO> MapAsync(Note input, CancellationToken cancellationToken = new CancellationToken())
        {
            if (input == null)
                return null;

            var hierarchy = await this._documentSession.LoadOrCreateHierarchyAsync(input.UserId, cancellationToken);
            return this.ToNoteDTO(input, hierarchy);
        }

        private NoteDTO ToNoteDTO(Note note, FoldersNotesHierarchy hierarchy)
        {
            if (note == null)
                return null;

            var folder = hierarchy.GetNoteFolder(note.Id);

            return new NoteDTO
            {
                Id = this._documentSession.ToLongId(note.Id),
                Title = note.Title,
                Content = note.Content,
                Folder = folder == null
                    ? null
                    : new SimpleFolderDTO
                    {
                        Id = this._documentSession.ToLongId(folder.FolderId),
                        Name = folder.FolderName
                    }
            };
        }
    }
}