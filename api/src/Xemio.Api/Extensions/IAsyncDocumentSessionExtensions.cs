using Raven.Client;

namespace Xemio.Api.Extensions
{
    // ReSharper disable once InconsistentNaming
    public static class IAsyncDocumentSessionExtensions
    {
        internal static long ToLongId(this IAsyncDocumentSession self, string id)
        {
            return long.Parse(self.Advanced.DocumentStore.Conventions.FindIdValuePartForValueTypeConversion(null, id));
        }
    }
}