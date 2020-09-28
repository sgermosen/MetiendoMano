using IdentityServer4;
using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Auth.Config
{
    public static class IdentityConfig
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("Core.Api", "My Anexgram core API")
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        }

        public static IEnumerable<Client> GetClients(
            IConfiguration configuration
        )
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = configuration.GetValue<string>("Client:ClientId"),
                    ClientName = "Back-office client",
                    AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,

                    // Este valor esta expresado en segundos, lo setamos a 4 horas
                    AccessTokenLifetime = 3600 * 4,

                    RequireConsent = false,

                    ClientSecrets =
                    {
                        new Secret(configuration.GetValue<string>("Client:SecretKey").Sha256())
                    },

                    RedirectUris           = { $"{configuration.GetValue<string>("Client:Url")}signin-oidc" },
                    PostLogoutRedirectUris = { $"{configuration.GetValue<string>("Client:Url")}signout-callback-oidc" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "Core.Api",
                    },

                    AllowOfflineAccess = true,

                    // Envía el token por defecto
                    AlwaysIncludeUserClaimsInIdToken = true
                }
            };
        }
    }
}
