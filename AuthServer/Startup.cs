using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AuthServer.ConfigStore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using IdentityServer4.Stores;
using System.IO;
using Microsoft.AspNetCore.Identity;
using AuthServer.Services;

namespace AuthServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContextPool<ConfigurationStoreContext>(o => o.UseSqlServer(connectionString));

            services.AddTransient<IClientStore, ClientStore>();
            services.AddTransient<IResourceStore, ResourceStore>();

            services.AddSingleton<IEmailSender, EmailSender>();

            services.AddIdentity<ApplicationUser, IdentityRole>(o =>
            {
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
            })
                .AddEntityFrameworkStores<ConfigurationStoreContext>()
                .AddDefaultTokenProviders();

            services.AddIdentityServer()
                //.AddSigningCredential(cert)
                .AddDeveloperSigningCredential()
                .AddResourceStore<ResourceStore>()
                .AddClientStore<ClientStore>()
                //.AddProfileService<ProfileService>()

                .AddAspNetIdentity<ApplicationUser>();
            //.AddProfileService<IdentityWithAdditionalClaimsProfileService>();
            services.AddControllers();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            InitializeDatabase(app);
            //app.UseHttpsRedirection();

            app.UseIdentityServer();
            app.UseRouting();

            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapGet("/", async context => { await context.Response.WriteAsync("hello"); });
            //});
        }
        private void InitializeDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var authContext = serviceScope.ServiceProvider.GetRequiredService<ConfigurationStoreContext>();
                var _userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                authContext.Database.Migrate();

                if (!authContext.Users.Any())
                {
                    SeedAuthData.Seed(authContext);
                    var user = new ApplicationUser { UserName = "naveed@email.com", Email = "naveed@email.com" };
                    var result = _userManager.CreateAsync(user, "naveed123").GetAwaiter().GetResult();
                }

            }
        }
    }
}
