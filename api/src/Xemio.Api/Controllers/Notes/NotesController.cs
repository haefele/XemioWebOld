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
    [Route("notes")]
    public class NotesController : ControllerBase
    {
        public static class RouteNames
        {
            public const string CreateNote = nameof(CreateNote);
            public const string GetNoteById = nameof(GetNoteById);
            public const string GetNotesFromFolder = nameof(GetNotesFromFolder);
            public const string UpdateNote = nameof(UpdateNote);
            public const string DeleteNote = nameof(DeleteNote);
        }

        private readonly IAsyncDocumentSession _documentSession;
        private readonly IMapper<Note, NoteDTO> _noteToNoteDTOMapper;

        public NotesController(IAsyncDocumentSession documentSession, IMapper<Note, NoteDTO> noteToNoteDTOMapper)
        {
            EnsureArg.IsNotNull(documentSession, nameof(documentSession));
            EnsureArg.IsNotNull(noteToNoteDTOMapper, nameof(noteToNoteDTOMapper));

            this._documentSession = documentSession;
            this._noteToNoteDTOMapper = noteToNoteDTOMapper;
        }

        [HttpGet("{noteId:long}", Name = RouteNames.GetNoteById)]
        public async Task<IActionResult> GetNoteByIdAsync([Required]long? noteId, CancellationToken cancellationToken = default(CancellationToken))
        {
            var note = await this._documentSession.LoadAsync<Note>(noteId, cancellationToken);

            if (note == null || note.UserId != this.User.Identity.Name)
                return this.NotFound();

            var noteDTO = await this._noteToNoteDTOMapper.MapAsync(note, cancellationToken);

            return this.Ok(noteDTO);
        }

        [HttpGet(Name = RouteNames.GetNotesFromFolder)]
        public async Task<IActionResult> GetNotesFromFolderAsync([FromQuery]long? folderId, CancellationToken cancellationToken = default(CancellationToken))
        {
            var stringFolderId = this._documentSession.ToStringId<Folder>(folderId);

            var hierarchy = await this._documentSession.LoadOrCreateHierarchyAsync(this.User.Identity.Name, cancellationToken);

            if (folderId != null && hierarchy.HasFolder(stringFolderId) == false)
                return this.NotFound();

            var noteIds = folderId == null
                ? hierarchy.GetRootNoteIds()
                : hierarchy.GetNoteIds(stringFolderId);

            var notes = await this._documentSession.LoadAsync<Note>(noteIds, cancellationToken);

            var noteDTOs = await this._noteToNoteDTOMapper.MapListAsync(notes, cancellationToken);

            return this.Ok(noteDTOs);
        }

        [HttpPost(Name = RouteNames.CreateNote)]
        public async Task<IActionResult> CreateNoteAsync([FromBody][Required]CreateNote createNote, CancellationToken cancellationToken = default(CancellationToken))
        {
            var hierarchy = await this._documentSession.LoadOrCreateHierarchyAsync(this.User.Identity.Name, cancellationToken);

            if (createNote.FolderId != null)
            {
                var stringFolderId = this._documentSession.ToStringId<Folder>(createNote.FolderId);

                if (hierarchy.HasFolder(stringFolderId) == false)
                    createNote.FolderId = null;
            }
            
            var note = new Note
            {
                Title = createNote.Title,
                Content = createNote.Content,
                UserId = this.User.Identity.Name,
            };

            await this._documentSession.StoreAsync(note, cancellationToken);

            hierarchy.AddNewNote(note, this._documentSession.ToStringId<Folder>(createNote.FolderId));

            if (hierarchy.Validate() == false)
                return this.BadRequest();

            await this._documentSession.SaveChangesAsync(cancellationToken);

            var noteDTO = await this._noteToNoteDTOMapper.MapAsync(note, cancellationToken);

            return this.CreatedAtRoute(RouteNames.GetNoteById, new { noteId = note.Id }, noteDTO);
        }

        [HttpPatch("{noteId:long}", Name = RouteNames.UpdateNote)]
        public async Task<IActionResult> UpdateNoteAsync([Required] long? noteId, [FromBody][Required]UpdateNote data, CancellationToken cancellationToken = default(CancellationToken))
        {
            var hierarchy = await this._documentSession.LoadOrCreateHierarchyAsync(this.User.Identity.Name, cancellationToken);

            if (hierarchy.HasNote(this._documentSession.ToStringId<Note>(noteId)) == false)
                return this.NotFound();

            var note = await this._documentSession.LoadAsync<Note>(noteId, cancellationToken);
            
            if (data.HasTitle())
            {
                note.Title = data.Title;
                hierarchy.UpdateNoteTitle(note.Id, note.Title);
            }

            if (data.HasContent())
            {
                note.Content = data.Content;
            }

            if (data.HasFolderId())
            {
                hierarchy.UpdateFolderId(note.Id, this._documentSession.ToStringId<Folder>(data.FolderId));
            }

            if (hierarchy.Validate() == false)
                return this.BadRequest();

            await this._documentSession.SaveChangesAsync(cancellationToken);

            var noteDTO = await this._noteToNoteDTOMapper.MapAsync(note, cancellationToken);

            return this.Ok(noteDTO);
        }

        [HttpDelete("{noteId:long}", Name = RouteNames.DeleteNote)]
        public async Task<IActionResult> DeleteNoteAsync([Required] long? noteId, CancellationToken cancellationToken = default(CancellationToken))
        {
            var note = await this._documentSession.LoadAsync<Note>(noteId, cancellationToken);

            if (note == null || note.UserId != this.User.Identity.Name)
                return this.NotFound();

            this._documentSession.Delete(note);
            await this._documentSession.SaveChangesAsync(cancellationToken);

            return this.Ok();
        }
    }
}