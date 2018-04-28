using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using MyCookieAuthSample.Data;
using MyCookieAuthSample.Models;
using Microsoft.AspNetCore.Identity;
using MyCookieAuthSample.Services;
using IdentityServer4.AspNetIdentity;
using IdentityServer4;
using IdentityServer4.Services;
using System.Reflection;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;

namespace MyCookieAuthSample
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
            //各种服务的注入方式
            /*
             services.AddScoped<ConsentService>();//背后方式
             services.AddDbContext
             */
            string assemblyName = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            string IdentityServer4Connection = Configuration.GetConnectionString("IdentityServer4Connection");

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlite(Configuration.GetConnectionString("DefaultConnection"));
            });
            //IdentityServer4.EntityFramework.DbContexts.PersistedGrantDbContext
            services.AddScoped<ConsentService>();

            services.AddIdentity<ApplicationUser, ApplicationUserRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            //修改Identity配置
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireLowercase = false;//包含小写字母
                options.Password.RequireNonAlphanumeric = false;//包含特殊符号
                options.Password.RequireUppercase = false;//包含大写字母
                options.Password.RequiredLength = 6;//长度12位
            });

            IIdentityServerBuilder identityServerBuilder = services.AddIdentityServer();


            identityServerBuilder.AddDeveloperSigningCredential()
            //.AddInMemoryApiResources(Config.GetResource())
            //.AddInMemoryClients(Config.GetClient())
            //.AddInMemoryIdentityResources(Config.GetIdentityResource())
            //.AddTestUsers(Config.GetTestUser());
            .AddAspNetIdentity<ApplicationUser>()

            .AddConfigurationStore( options=>
                {
                    options.ConfigureDbContext = (builder =>
                     {
                         builder.UseSqlite(IdentityServer4Connection,
                             sqlOptions => sqlOptions.MigrationsAssembly(assemblyName));
                     }
                    );
                }
            )
            .AddOperationalStore(options =>
            {
                options.ConfigureDbContext = (builder =>
                {
                    builder.UseSqlite(IdentityServer4Connection,
                        sqlOptions => sqlOptions.MigrationsAssembly(assemblyName));
                }
                );
            }
            )

            .Services
                .AddScoped<IProfileService, MyProfileService>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            InitIdentityServer4Database(app);

            app.UseIdentityServer();
            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        public void InitIdentityServer4Database(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                ConfigurationDbContext configurationDbContext = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
                PersistedGrantDbContext persistedGrantDbContext = scope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>();
                if (!configurationDbContext.Clients.Any())
                {
                    foreach (var client in Config.GetClient())
                    {
                        configurationDbContext.Clients.Add(client.ToEntity());
                    }
                    configurationDbContext.SaveChanges();
                }
                if (!configurationDbContext.ApiResources.Any())
                {
                    foreach (var client in Config.GetResource())
                    {
                        configurationDbContext.ApiResources.Add(client.ToEntity());
                    }
                    configurationDbContext.SaveChanges();
                }
                if (!configurationDbContext.IdentityResources.Any())
                {
                    foreach (var client in Config.GetIdentityResource())
                    {
                        configurationDbContext.IdentityResources.Add(client.ToEntity());
                    }
                    configurationDbContext.SaveChanges();
                }
            }
        }
    }
}
