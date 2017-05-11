using System.Threading;
using System.Threading.Tasks;
using Raven.Client;
using Xemio.Api.Entities.Notes;

namespace Xemio.Api.Extensions
{
    // ReSharper disable once InconsistentNaming
    public static class IAsyncDocumentSessionExtensions
    {
        internal static long ToLongId(this IAsyncDocumentSession self, string id)
        {
            return long.Parse(self.Advanced.DocumentStore.Conventions.FindIdValuePartForValueTypeConversion(null, id));
        }

        internal static string ToStringId<T>(this IAsyncDocumentSession self, long id)
        {
            return self.Advanced.DocumentStore.Conventions.FindFullDocumentKeyFromNonStringIdentifier(id, typeof(T), false);
        }

        internal static string ToStringId<T>(this IAsyncDocumentSession self, long? id)
        {
            return id == null
                ? null
                : self.ToStringId<T>(id.Value);
        }

        internal static async Task<FoldersNotesHierarchy> LoadOrCreateHierarchyAsync(this IAsyncDocumentSession self, string userId, CancellationToken cancellationToken = default (CancellationToken))
        {
            var hierarchy = await self.LoadAsync<FoldersNotesHierarchy>(FoldersNotesHierarchy.CreateId(userId), cancellationToken);

            if (hierarchy == null)
            {
                hierarchy = new FoldersNotesHierarchy { UserId = userId };
                await self.StoreAsync(hierarchy, cancellationToken);
            }

            return hierarchy;
        }
    }
}