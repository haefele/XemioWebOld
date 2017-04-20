using System.Linq;
using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;
using Xemio.Api.Entities.Notes;

namespace Xemio.Api.Database.Indexes
{
    public class Folders_ForQuery : AbstractIndexCreationTask<Folder>
    {
        public Folders_ForQuery()
        {
            this.Map = folders =>
                from folder in folders
                select new
                {
                    folder.UserId,
                    folder.ParentFolderId
                };
            
            this.Index(f => f.UserId, FieldIndexing.NotAnalyzed);
            this.Index(f => f.ParentFolderId, FieldIndexing.NotAnalyzed);
        }
    }
}