using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Security.Claims;

namespace IdentityServer
{
    public class Config
    {        
        public static IEnumerable<Client> clients => new Client[] 
        {
            new Client
            {
                ClientName = "movieClient",
                ClientId = "movieClient",
                Enabled = true,
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },
                AllowedScopes = {"movieAPI"}
            },
            new Client
            {
                ClientId = "movie_mvc_client",
                ClientName = "movie mvc web app",
                AllowedGrantTypes = GrantTypes.Code,
                AllowRememberConsent = false,
                RedirectUris = new List<string>
                {
                    "https://localhost:7093/signin-oidc"
                },
                PostLogoutRedirectUris= new List<string>
                {
                    "https://localhost:7093/signout-callback-oidc"
                },
                ClientSecrets= new List<Secret>
                {
                    new Secret("secret".Sha256())
                },
                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                }
            }
        };
        public static IEnumerable<ApiScope> apiScopes => new ApiScope[] 
        {
            new ApiScope("movieAPI", "Movie API")
        };
        public static IEnumerable<ApiResource> apiResources => new ApiResource[] { };
        public static IEnumerable<IdentityResource> identityResources => new IdentityResource[] 
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
        };
        public static List<TestUser> testUsers => new List<TestUser> 
        {
            new TestUser
            {
                SubjectId = new Guid().ToString(),
                Username = "Test",
                Password = "Test",
                Claims = new List<Claim>
                {
                    new Claim(JwtClaimTypes.GivenName, "Test"),
                    new Claim(JwtClaimTypes.FamilyName, "User")
                }

            }
        };

    }
}
