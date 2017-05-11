using System.Collections.Generic;

namespace Xemio.Api.Data.Models.Notes
{
    public class FolderDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public SimpleFolderDTO ParentFolder { get; set; }
        public IList<SimpleFolderDTO> ParentFolderHierarchy { get; set; }
        public IList<SimpleFolderDTO> SubFolders { get; set; }
        public IList<SimpleNoteDTO> Notes { get; set; }
    }
}
