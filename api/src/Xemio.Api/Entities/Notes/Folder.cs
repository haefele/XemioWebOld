namespace Xemio.Api.Entities.Notes
{
    public class Folder : IEntity
    {
        public string Id { get; set; }

        public string UserId { get; set; }
        public string Name { get; set; }
    }
}
