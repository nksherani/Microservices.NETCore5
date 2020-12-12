using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServer.ConfigStore
{

    public class ConfigurationStoreContext : IdentityDbContext<ApplicationUser>
    {
        public ConfigurationStoreContext(DbContextOptions<ConfigurationStoreContext> options) : base(options)
        { 
        
        }

        public DbSet<ClientEntity> Clients { get; set; }
        public DbSet<ApiResourceEntity> ApiResources { get; set; }
        public DbSet<IdentityResourceEntity> IdentityResources { get; set; }
        public DbSet<ScopeEntity> Scopes { get; set; }
        //public DbSet<ApplicationUser> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ClientEntity>().HasKey(m => m.ClientId);
            builder.Entity<ApiResourceEntity>().HasKey(m => m.ApiResourceName);
            builder.Entity<IdentityResourceEntity>().HasKey(m => m.IdentityResourceName);
            base.OnModelCreating(builder);
        }
    }
}
