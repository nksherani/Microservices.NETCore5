using IdentityServer4.Models;
using IdentityServer4.Stores;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServer.ConfigStore
{
    public class ResourceStore : IResourceStore
    {
        private readonly ConfigurationStoreContext _context;
        private readonly ILogger _logger;

        public ResourceStore(ConfigurationStoreContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("ResourceStore");
        }

        public Task<ApiResource> FindApiResourceAsync(string name)
        {
            var apiResource = _context.ApiResources.First(t => t.ApiResourceName == name);
            apiResource.MapDataFromEntity();
            return Task.FromResult(apiResource.ApiResource);
        }

        public Task<IEnumerable<ApiResource>> FindApiResourcesByNameAsync(IEnumerable<string> apiResourceNames)
        {
            var apiResourcesEntities = _context.ApiResources.Where(x => apiResourceNames.Contains(x.ApiResourceName)).ToList();
            var apiResources = new List<ApiResource>();

            foreach (var apiResourceEntity in apiResourcesEntities)
            {
                apiResourceEntity.MapDataFromEntity();

                apiResources.Add(apiResourceEntity.ApiResource);
            }

            return Task.FromResult(apiResources.AsEnumerable());
        }

        //public Task<IEnumerable<ApiResource>> FindApiResourcesByScopeAsync(IEnumerable<string> scopeNames)
        //{
        //    if (scopeNames == null) throw new ArgumentNullException(nameof(scopeNames));


        //    var apiResources = new List<ApiResource>();
        //    var apiResourcesEntities = from i in _context.ApiResources
        //                               where scopeNames.Contains(i.ApiResourceName)
        //                               select i;

        //    foreach (var apiResourceEntity in apiResourcesEntities)
        //    {
        //        apiResourceEntity.MapDataFromEntity();

        //        apiResources.Add(apiResourceEntity.ApiResource);
        //    }

        //    return Task.FromResult(apiResources.AsEnumerable());
        //}

        public Task<IEnumerable<ApiResource>> FindApiResourcesByScopeNameAsync(IEnumerable<string> scopeNames)
        {
            if (scopeNames == null) throw new ArgumentNullException(nameof(scopeNames));


            var apiResources = new List<ApiResource>();
            var apiResourcesEntities = new List<ApiResourceEntity>();
            //var apiResourcesEntities = from i in _context.ApiResources
            //                           where scopeNames.Contains(i.ApiResourceName)
            //                           select i;
            var apiResourcesTemp = _context.ApiResources.ToList();
            foreach (var rsrc in apiResourcesTemp)
            {
                var tmp = JsonConvert.DeserializeObject<ApiResource>(rsrc.ApiResourceData);
                int count = tmp.Scopes.Select(x => x.Split('.')[1]).Where(x=>scopeNames.Contains(x)).Count();
                if (count > 0)
                    apiResourcesEntities.Add(rsrc);
            }
                                      
            foreach (var apiResourceEntity in apiResourcesEntities)
            {
                apiResourceEntity.MapDataFromEntity();

                apiResources.Add(apiResourceEntity.ApiResource);
            }

            return Task.FromResult(apiResources.AsEnumerable());
        }

        public Task<IEnumerable<ApiScope>> FindApiScopesByNameAsync(IEnumerable<string> scopeNames)
        {
            var apiScopesEntities = _context.Scopes.Where(x => scopeNames.Contains(x.ApiScopeName)).ToList();
            var apiScopes = new List<ApiScope>();

            foreach (var apiScopeEntity in apiScopesEntities)
            {
                apiScopeEntity.MapDataFromEntity();

                apiScopes.Add(apiScopeEntity.ApiScope);
            }

            return Task.FromResult(apiScopes.AsEnumerable());
        }

        public Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeNameAsync(IEnumerable<string> scopeNames)
        {
            if (scopeNames == null) throw new ArgumentNullException(nameof(scopeNames));

            var identityResources = new List<IdentityResource>();
            var identityResourcesEntities = from i in _context.IdentityResources
                                            where scopeNames.Contains(i.IdentityResourceName)
                                            select i;

            foreach (var identityResourceEntity in identityResourcesEntities)
            {
                identityResourceEntity.MapDataFromEntity();

                identityResources.Add(identityResourceEntity.IdentityResource);
            }

            return Task.FromResult(identityResources.AsEnumerable());
        }

       

        public Task<Resources> GetAllResourcesAsync()
        {
            var apiResourcesEntities = _context.ApiResources.ToList();
            var identityResourcesEntities = _context.IdentityResources.ToList();
            var scopeEntities = _context.Scopes.ToList().Select(x=>new ApiScope(x.ApiScopeName));

            var apiResources = new List<ApiResource>();
            var identityResources = new List<IdentityResource>();

            foreach (var apiResourceEntity in apiResourcesEntities)
            {
                apiResourceEntity.MapDataFromEntity();

                apiResources.Add(apiResourceEntity.ApiResource);
            }

            foreach (var identityResourceEntity in identityResourcesEntities)
            {
                identityResourceEntity.MapDataFromEntity();

                identityResources.Add(identityResourceEntity.IdentityResource);
            }

            var result = new Resources(identityResources, apiResources, scopeEntities);
            return Task.FromResult(result);
        }
    }
}
