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
using MvcCookieAuthSample.Data;
using Microsoft.EntityFrameworkCore;
using MvcCookieAuthSample.Models;
using Microsoft.AspNetCore.Identity;

namespace MvcCookieAuthSample
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
            //使用配置ApplicationDbContext使用sqlserver数据库，并配置数据库连接字符串
            services.AddDbContext<ApplicationDbContext>(options=> {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            //配置Identity
            services.AddIdentity<ApplicationUser, ApplicationUserRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            //修改Identity配置
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireLowercase = true;//包含小写字母
                options.Password.RequireNonAlphanumeric = true;//包含特殊符号
                options.Password.RequireUppercase = true;//包含大写字母
                options.Password.RequiredLength = 12;//长度12位
            });


            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options=>{
                    options.LoginPath="/Account/Login";
                });


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

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
