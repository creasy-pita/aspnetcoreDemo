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
            string assemblyName = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            string IdentityServer4Connection = Configuration.GetConnectionString("IdentityServer4Connection");

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlite(Configuration.GetConnectionString("DefaultConnection"));
            });

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

            services.AddIdentityServer()
            .AddDeveloperSigningCredential()
            .AddInMemoryApiResources(Config.GetResource())
            .AddInMemoryClients(Config.GetClient())
            .AddInMemoryIdentityResources(Config.GetIdentityResource())
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
            app.UseIdentityServer();
            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
