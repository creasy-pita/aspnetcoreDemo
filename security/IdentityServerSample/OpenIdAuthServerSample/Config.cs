using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace MyCookieAuthSample
{
    public class Config{

        public static IEnumerable<ApiResource> GetResource()
        {

            return new ApiResource[]{
                new ApiResource("api","myapi")
            };
        }

        public static IEnumerable<Client> GetClient()
        {

            return new Client[]{
                new Client{
                    ClientId="mvc",
                    ClientName="mvc client",
                    ClientUri="http://localhost:5001",
                    LogoUri = "https://cdn.dribbble.com/users/42044/screenshots/3005802/net-core-logo-proposal.jpg",
                    AllowRememberConsent = true,

                    AllowedGrantTypes= GrantTypes.Implicit,
                    ClientSecrets = {new Secret("secret".Sha256())},
                    RequireConsent=true,
                    // where to redirect to after login
                    RedirectUris = { "http://localhost:5001/signin-oidc" },

                    // where to redirect to after logout
                    PostLogoutRedirectUris = { "http://localhost:5001/signout-callback-oidc" },

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "api"
                    }
                },
            };
        } 

        public static IEnumerable<IdentityResource> GetIdentityResource()
        {

            return new List<IdentityResource>{
                new IdentityResources.OpenId(),               
                new IdentityResources.Profile(),               
                new IdentityResources.Email()               
            };
        }         
        public static List<TestUser> GetTestUser()
        {

            return new List<TestUser>{
                new TestUser{
                    SubjectId="1",
                    Username="jesse",
                    Password="123456",
                    Claims = new []
                    {
                        new Claim("name", "Bob"),
                        new Claim("website", "https://bob.com")
                    }
                }
            };
        }   
    }
}