using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Xemio.Api.Setup
{
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