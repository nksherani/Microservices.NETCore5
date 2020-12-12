using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServer
{

    public class Config
    {


        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>{
                new ApiScope(name: "openid",   displayName: "Read your data."),
                new ApiScope(name: "read",   displayName: "Read your data."),
                new ApiScope(name: "write",  displayName: "Write your data."),
                new ApiScope(name: "delete", displayName: "Delete your data.")
             };
        }
        public static IEnumerable<ApiResource> GetApiResources()
        {
            ApiResource resource = new ApiResource();
            resource.Name = "myresourceapi";
            resource.DisplayName = "myresourceapi";
            resource.Description = "myresourceapi";
            //resource.Properties.Add("aud", "https://localhost:443");
            resource.Scopes = new List<string> { "myresourceapi.openid" };
            return new List<ApiResource> {
                resource
                //new ApiResource("myresourceapi", "My Resource API"){Scopes={ "myresourceapi.openid" }, Name="aud1", UserClaims = { JwtClaimTypes.Name, JwtClaimTypes.Profile} }, 
                //new ApiResource("mysecondapi", "My Second API"){Scopes={ "mysecondapi.read" }}
            };
        }
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>{
            new Client
            {
                
                ClientId = "secret_client_id",
                ClientName = "abc",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = new List<Secret> {new Secret("secret".Sha256())}, // change me!
                AllowedScopes = {"openid","read"},

                //Claims = {new ClientClaim("aud","myresourceapi")}
                
            }
        };
        }

    }
}
