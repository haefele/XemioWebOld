namespace Xemio.Api.Data.Models.Notes
{
    public class CreateFolder
    {
        public string Name { get; set; }
        public long? ParentFolderId { get; set; }
    }
}