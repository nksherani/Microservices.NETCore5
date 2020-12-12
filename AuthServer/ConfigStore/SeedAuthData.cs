using IdentityServer4.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServer.ConfigStore
{
    public class SeedAuthData
    {
        public static void Seed(ConfigurationStoreContext context)
        {
            Client client = new Client
            {

                ClientId = "secret_client_id",
                ClientName = "abc",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = new List<Secret> { new Secret("secret".Sha256()) }, // change me!
                AllowedScopes = { "openid", "read", "offline_access" },
                AllowOfflineAccess = true
                //Claims = {new ClientClaim("aud","myresourceapi")}

            };
            client.AllowedGrantTypes.Add(GrantType.ResourceOwnerPassword);
            ClientEntity clientEntity = new ClientEntity() { ClientId = "secret_client_id", ClientData = JsonConvert.SerializeObject(client) };

            ApiResource resource = new ApiResource();
            resource.Name = "myresourceapi";
            resource.DisplayName = "myresourceapi";
            resource.Description = "myresourceapi";
            //resource.Properties.Add("aud", "https://localhost:443");
            resource.Scopes = new List<string> { "myresourceapi.openid", "myresourceapi.read" };

            ApiResourceEntity resourceEntity = new ApiResourceEntity() { ApiResourceName = "myresourceapi", ApiResourceData = JsonConvert.SerializeObject(resource) };

            List<ScopeEntity> scopesList = new List<ScopeEntity>();
            var scope = new ApiScope(name: "openid", displayName: "Read your data.");
            ScopeEntity scopeEntity = new ScopeEntity() { ApiScopeName = "openid", ApiScopeData = JsonConvert.SerializeObject(scope) };
            scopesList.Add(scopeEntity);
            var scope2 = new ApiScope(name: "read", displayName: "Read your data.");
            ScopeEntity scopeEntity2 = new ScopeEntity() { ApiScopeName = "read", ApiScopeData = JsonConvert.SerializeObject(scope2) };
            scopesList.Add(scopeEntity2);
            
            context.Add(clientEntity);

            IdentityResource identityResource = new IdentityResource();
            identityResource.Name = "naveed";
            identityResource.DisplayName = "naveed";
            identityResource.UserClaims = new List<string>() { "role" };

            var identityResourceEntity = new IdentityResourceEntity() { ID = "naveed", IdentityResourceName = "naveed", IdentityResourceData = JsonConvert.SerializeObject(identityResource) };


            context.Add(identityResourceEntity);
            context.Add(resourceEntity);
            context.AddRange(scopesList);
            
            context.SaveChanges();
        }
    }
}
