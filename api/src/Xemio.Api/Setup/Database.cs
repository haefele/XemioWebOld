using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Raven.Client;
using Raven.Client.Connection.Async;
using Raven.Client.Document;
using Raven.Client.Indexes;
using Raven.Client.Linq;
using Xemio.Api.Entities.Notes;

namespace Xemio.Api.Setup
{
    public static class Database
    {
        public static void AddDatabase(this IServiceCollection self, IConfiguration configuration)
        {
            var documentStore = new DocumentStore();
            documentStore.ParseConnectionString(configuration.GetValue<string>("ConnectionString"));

            documentStore.Initialize();

            CustomizeDocumentStore(documentStore);
            
            self.AddSingleton<IDocumentStore>(documentStore);
            self.AddScoped(sp =>
            {
                var session = sp.GetService<IDocumentStore>().OpenAsyncSession();
                session.Advanced.WaitForIndexesAfterSaveChanges();
                return session;
            });
        }

        private static void CustomizeDocumentStore(DocumentStore documentStore)
        {
            documentStore.Conventions.RegisterAsyncIdConvention<FoldersNotesHierarchy>((databaseName, commands, entity) => 
                Task.FromResult(FoldersNotesHierarchy.CreateId(entity.UserId)));
        }

        public static void MigrateDatabase(this IApplicationBuilder self)
        {
            var documentStore = self.ApplicationServices.GetService<IDocumentStore>();
            IndexCreation.CreateIndexes(typeof(Startup).GetTypeInfo().Assembly, documentStore);
        }
    }
}