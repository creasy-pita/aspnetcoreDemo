using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebUserAPI.Data;

namespace WebUserAPI
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
    //        services.AddDbContext<AppUserDbContext>(options =>
    //options.UseMySQL(Configuration.GetConnectionString("DefaultConnection")));
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //InitUserDb(app);

            app.UseMvc();
        }

        private void InitUserDb(IApplicationBuilder app)
        {
            try
            {
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<AppUserDbContext>();
                    if (!context.User.Any())
                    {
                        AppUser appUser = new AppUser { Id = 1,Company="google1", Name="creasypita", Title="111" };
                        context.Add<AppUser>(appUser);
                        context.SaveChanges();
                        Console.WriteLine("初始默认用户成功");
                    }
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("ignoring everything");
            }            

        }
    }
}
