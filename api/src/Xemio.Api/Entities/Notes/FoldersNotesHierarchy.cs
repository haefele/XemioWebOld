using System.Collections.Generic;
using System.Linq;
using Xemio.Api.Extensions;

namespace Xemio.Api.Entities.Notes
{
    public class FoldersNotesHierarchy : IEntity
    {
        public static string CreateId(string userId)
        {
            return $"FoldersNotesHierarchy/{userId}";
        }

        public FoldersNotesHierarchy()
        {
            this.Folders = new List<SimpleFolder>();
        }

        public string Id { get; set; }
        public string UserId { get; set; }
        public IList<SimpleFolder> Folders { get; set; }
        public IList<SimpleNote> Notes { get; set; }

        #region Get
        public bool HasFolder(string folderId)
        {
            return this.GetFolder(folderId) != null;
        }
        public SimpleFolder GetFolder(string folderId)
        {
            return this.Folders.Flatten(f => f.SubFolders).FirstOrDefault(f => f.FolderId == folderId);
        }
        public IList<string> GetRootFolderIds()
        {
            return this.Folders.Select(f => f.FolderId).ToList();
        }
        public IList<string> GetSubFolderIds(string folderId)
        {
            return this.GetFolder(folderId)?.SubFolders.Select(f => f.FolderId).ToList() ?? new List<string>();
        }
        public SimpleFolder GetParentFolder(string folderId)
        {
            var folder = this.GetFolder(folderId);
            return this.Folders.Flatten(f => f.SubFolders).FirstOrDefault(f => f.SubFolders.Contains(folder));
        }
        public IList<SimpleFolder> GetParentFolderList(string folderId)
        {
            var result = new List<SimpleFolder>();

            var currentFolder = this.GetFolder(folderId);
            while (currentFolder != null)
            {
                result.Add(currentFolder);
                currentFolder = this.GetParentFolder(currentFolder.FolderId);
            }

            return result;
        }

        public bool HasNote(string noteId)
        {
            return this.GetNote(noteId) != null;
        }
        public SimpleNote GetNote(string noteId)
        {
            return this.Folders.Flatten(f => f.SubFolders).SelectMany(f => f.Notes).Concat(this.Notes).FirstOrDefault(f => f.NoteId == noteId);
        }
        public IList<string> GetRootNoteIds()
        {
            return this.Notes.Select(f => f.NoteId).ToList();
        }
        public IList<string> GetNoteIds(string folderId)
        {
            return this.GetFolder(folderId)?.Notes.Select(f => f.NoteId).ToList() ?? new List<string>();
        }
        public SimpleFolder GetNoteFolder(string noteId)
        {
            var note = this.GetNote(noteId);
            return this.Folders.Flatten(f => f.SubFolders).FirstOrDefault(f => f.Notes.Contains(note));
        }
        #endregion

        #region Change
        public void AddNewFolder(Folder folder, string parentFolderId)
        {
            var simpleFolder = new SimpleFolder
            {
                FolderId = folder.Id,
                FolderName = folder.Name
            };

            var parentFolder = this.GetFolder(parentFolderId);
            if (parentFolder != null)
            {
                parentFolder.SubFolders.Add(simpleFolder);
            }
            else
            {
                this.Folders.Add(simpleFolder);
            }
        }
        public void UpdateParentFolderId(string folderId, string newParentFolderId)
        {
            var folder = this.GetFolder(folderId);

            var currentParentFolder = this.Folders.Flatten(f => f.SubFolders).FirstOrDefault(f => f.SubFolders.Contains(folder));
            if (currentParentFolder != null)
            {
                currentParentFolder.SubFolders.Remove(folder);
            }
            else
            {
                this.Folders.Remove(folder);
            }

            var newParentFolder = this.Folders.Flatten(f => f.SubFolders).FirstOrDefault(f => f.FolderId == newParentFolderId);
            if (newParentFolder != null)
            {
                newParentFolder.SubFolders.Add(folder);
            }
            else
            {
                this.Folders.Add(folder);
            }
        }
        public void UpdateFolderName(string folderId, string newFolderName)
        {
            var folder = this.GetFolder(folderId);
            folder.FolderName = newFolderName;
        }
        public void DeleteFolder(string folderId)
        {
            var folder = this.GetFolder(folderId);

            var parentFolder = this.Folders.Flatten(f => f.SubFolders).FirstOrDefault(f => f.SubFolders.Contains(folder));
            if (parentFolder != null)
            {
                parentFolder.SubFolders.Remove(folder);
            }
            else
            {
                this.Folders.Remove(folder);
            }
        }

        public void AddNewNote(Note note, string folderId)
        {
            var simpleNote = new SimpleNote
            {
                NoteId = note.Id,
                NoteTitle = note.Title
            };

            var parentFolder = this.GetFolder(folderId);
            if (parentFolder != null)
            {
                parentFolder.Notes.Add(simpleNote);
            }
            else
            {
                this.Notes.Add(simpleNote);
            }
        }
        public void UpdateNoteTitle(string noteId, string newNoteTitle)
        {
            var note = this.GetNote(noteId);
            note.NoteTitle = newNoteTitle;
        }
        public void UpdateFolderId(string noteId, string newFolderId)
        {
            var note = this.GetNote(noteId);

            var currentFolder = this.Folders.Flatten(f => f.SubFolders).FirstOrDefault(f => f.Notes.Contains(note));
            if (currentFolder != null)
            {
                currentFolder.Notes.Remove(note);
            }
            else
            {
                this.Notes.Remove(note);
            }

            var newFolder = this.Folders.Flatten(f => f.SubFolders).FirstOrDefault(f => f.FolderId == newFolderId);
            if (newFolder != null)
            {
                newFolder.Notes.Add(note);
            }
            else
            {
                this.Notes.Add(note);
            }
        }
        #endregion

        #region Validate
        public bool Validate()
        {
            return true;
        }
        #endregion
    }
}