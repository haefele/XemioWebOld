namespace Xemio.Api.Data.Models.Notes
{
    public class CreateNote
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public long? FolderId { get; set; }
    }
}