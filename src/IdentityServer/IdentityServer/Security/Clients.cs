using System.Collections.Generic;
using IdentityServer3.Core;
using IdentityServer3.Core.Models;

namespace IdentityServer.Security
{
    public static class Clients
    {
        public static IEnumerable<Client> Get()
        {
            return new[]
            {
                new Client
                {
                    Enabled = true,
                    ClientName = "client1",
                    ClientId = "client1",
                    Flow = Flows.Hybrid,
                    RequireConsent = false,
                    AccessTokenType = AccessTokenType.Jwt,
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("test".Sha256())
                    },
                    AllowedScopes = new List<string>
                    {
                        Constants.StandardScopes.OpenId,
                        Constants.StandardScopes.Profile,
                        Constants.StandardScopes.Email,
                        Constants.StandardScopes.Roles
                    },
                    RedirectUris = new List<string>
                    {
                        "http://localhost:4567/"
                    },
                    PostLogoutRedirectUris = new List<string>
                    {
                        "http://localhost:4567/"
                    }
                }
            };
        }
    }
}
