using IdentityServer4.Models;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthServer.ConfigStore
{
    public class ScopeEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ID { get; set; }
        public string ApiScopeData { get; set; }

        [Key]
        public string ApiScopeName { get; set; }

        [NotMapped]
        public ApiScope ApiScope { get; set; }

        public void AddDataToEntity()
        {
            ApiScopeData = JsonConvert.SerializeObject(ApiScope);
            ApiScopeName = ApiScope.Name;
        }

        public void MapDataFromEntity()
        {
            ApiScope = JsonConvert.DeserializeObject<ApiScope>(ApiScopeData);
            ApiScopeName = ApiScope.Name;
        }
    }
}