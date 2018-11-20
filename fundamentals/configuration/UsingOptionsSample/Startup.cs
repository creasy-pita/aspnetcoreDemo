using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UsingOptionsSample.Models;

namespace UsingOptionsSample
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddOptions();
            services.Configure<MyOptions>(Configuration);
            #region Example #2
            services.Configure<MyDelegateConfigOptions>(options =>
               {
                   options.Option1 = "value1_from_DelegateConfig";
                   options.Option2 = 1;
               });
            #endregion
            #region Example #3   Suboptions configuration
            services.Configure<SubOptions>(Configuration.GetSection("subsection"));
            #endregion

            #region Example 4
            services.Configure<MyOptions>("named_options_1", Configuration);
            services.Configure<MyOptions>("named_options_2", myoptions => myoptions.Option1 = "value_from_named_options_2"); 
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMvc();

        }
    }
}
