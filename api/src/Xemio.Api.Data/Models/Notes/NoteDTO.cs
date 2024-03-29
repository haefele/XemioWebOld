﻿namespace Xemio.Api.Data.Models.Notes
{
    public class NoteDTO
    {
        public long Id { get; set; }

        public string Title { get; set; }
        public string Content { get; set; }

        public SimpleFolderDTO Folder { get; set; }
    }
}