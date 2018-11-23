using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using DependencyInjectionSample.Interfaces;
using DependencyInjectionSample.Models;
using DependencyInjectionSample.Services;
using ThirdPartyAssembly;
using NetCore.AutoRegisterDi;
using System.Reflection;

namespace DependencyInjectionSample
{
    public class Startup
    {
        #region snippet1
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddScoped<IMyDependency, MyDependency>();
            services.AddTransient<IOperationTransient, Operation>();
            services.AddScoped<IOperationScoped, Operation>();
            services.AddSingleton<IOperationSingleton, Operation>();
            services.AddSingleton<IOperationSingletonInstance>(new Operation(Guid.Empty));

            // OperationService depends on each of the other Operation types.
            //services.AddTransient<OperationService, OperationService>();
            services.AddScoped<OperationService, OperationService>();
            //services.AddSingleton<OperationService, OperationService>();


            #region one class implements multi interface  #e1
            //services.AddSingleton<IFoo, Foo>();
            //services.AddSingleton<IBar,Foo >(); 
            #endregion
            #region one class implements multi interface  #e2
            //Foo foo = new Foo();
            //services.AddSingleton<IFoo>(foo);
            //services.AddSingleton<IBar>(foo);
            #endregion
            #region one class implements multi interface  #e3
            //#e2 缺点 在scoped 场景中不适用
            //services.AddScoped<IFoo>(foo);//scope 是每个生命周期去创建实例的，所以不能传实例

            //services.AddSingleton<Foo>(); 
            //services.AddSingleton<IFoo>( sp=> sp.GetRequiredService<Foo>());
            //services.AddSingleton<IBar>( sp=> sp.GetRequiredService<Foo>());
            #endregion

            #region one class implements multi interface  #e4
            //#e4 在scoped 场景中 不同的接口可以使用相同的实例
            services.AddScoped<Foo>();
            services.AddScoped<IFoo>(sp => sp.GetRequiredService<Foo>());
            services.AddScoped<IBar>(sp => sp.GetRequiredService<Foo>());
            #endregion

            #region ThirdPartyService inject
            //services.AddScoped<ThirdPartyService2, ThirdPartyService2>();
            //services.AddScoped<ThirdPartyService, ThirdPartyService>();
            #endregion
            Type nam =typeof(ThirdPartyService);

            var aAutoScan = Assembly.GetAssembly(typeof(ThirdPartyService));
            services.RegisterAssemblyPublicNonGenericClasses(aAutoScan)
                .Where(
                x => x.Name == nameof(ThirdPartyService)
                )
                .AsPublicImplementedInterfaces(ServiceLifetime.Singleton);
            //services.RegisterAssemblyPublicNonGenericClasses(aAutoScan).AsPublicImplementedInterfaces();

        }
        #endregion

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseMvc();
        }
    }
}
