using System.Collections.Generic;

namespace Xemio.Api.Entities.Notes
{
    public class SimpleFolder
    {
        public SimpleFolder()
        {
            this.SubFolders = new List<SimpleFolder>();
            this.Notes = new List<SimpleNote>();
        }

        public string FolderId { get; set; }
        public string FolderName { get; set; }
        public IList<SimpleFolder> SubFolders { get; set; }
        public IList<SimpleNote> Notes { get; set; }
    }
}