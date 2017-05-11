using System.Collections;

namespace Xemio.Api.Entities.Notes
{
    public class Note : IEntity
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        public string Title { get; set; }
        public string Content { get; set; }
    }
}