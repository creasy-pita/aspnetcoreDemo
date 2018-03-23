using System.Collections;
using System.Collections.Generic;
using IdentityServer4.Models;


namespace IdentityServerCenter
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
                    ClientId="client",
                    AllowedGrantTypes= GrantTypes.ClientCredentials,
                    ClientSecrets = {new Secret("secret".Sha256())},
                    AllowedScopes = {"api"}
                }
            };
        }        
    }
}