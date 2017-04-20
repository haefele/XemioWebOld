using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Indexes;

namespace Xemio.Api.Setup
{
    public static class Database
    {
        public static void AddDatabase(this IServiceCollection self, IConfiguration configuration)
        {
            var documentStore = new DocumentStore();
            documentStore.ParseConnectionString(configuration.GetValue<string>("ConnectionString"));

            documentStore.Initialize();

            self.AddSingleton<IDocumentStore>(documentStore);
            self.AddScoped(sp =>
            {
                var session = sp.GetService<IDocumentStore>().OpenAsyncSession();
                session.Advanced.WaitForIndexesAfterSaveChanges();
                return session;
            });
        }

        public static void MigrateDatabase(this IApplicationBuilder self)
        {
            var documentStore = self.ApplicationServices.GetService<IDocumentStore>();
            IndexCreation.CreateIndexes(typeof(Startup).GetTypeInfo().Assembly, documentStore);
        }
    }

    public static class Auth0
    {
        public static void UseAuth0Authentication(this IApplicationBuilder self, IConfiguration configuration)
        {
            var options = new JwtBearerOptions
            { 
                TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = $"https://{configuration.GetValue<string>("Domain")}/",
                    ValidAudience = configuration.GetValue<string>("ClientId"),
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("ClientSecret"))),
                    NameClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"
                }
            };

            self.UseJwtBearerAuthentication(options);
        }
    }
}