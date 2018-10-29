using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MVCRoutes
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
            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddMvc();
        }


        #region    router模块脱离 MVC 单独使用  方法
        /*
        /// <summary>
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            var trackPackageRouteHandler = new RouteHandler(context =>
            {
                var routeValues = context.GetRouteData().Values;
                return context.Response.WriteAsync(
                    $"Hello! Route values: {string.Join(", ", routeValues)}");
            });
            app.UseRouter(
                routeBuilder =>
                {
                    routeBuilder.DefaultHandler = trackPackageRouteHandler;
                    routeBuilder.MapGet("hello/{name}", context =>
                    {
                        var name = context.GetRouteValue("name");
                        // The route handler when HTTP GET "hello/<anything>" matches
                        // To match HTTP GET "hello/<anything>/<anything>, 
                        // use routeBuilder.MapGet("hello/{*name}"
                        return context.Response.WriteAsync($"Hi, {name}!");
                    });
                }
                );
        }
        */
        #endregion

        /// <summary>
        /// querystring参数：分别从url slash divide block 斜线分隔块 中 和 querystring中取参数
        ///  url example:http://localhost:5001/home/index1/1?name=creasy&phone=8750
        ///  /home/index1/1 会作为route内容 根据路由模板规则来匹配
        ///  ?name=creasy&phone=8750 部分会作为 querystring
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMvc(routeBuilder =>
            {
                //routeBuilder.MapRoute(name: "aaa", template: "hello/{name}");
                routeBuilder.MapRoute(template: "hello/{name}", handler: context =>
                    {
                        var routeValues = context.GetRouteData().Values;
                        return context.Response.WriteAsync(
                            $"Hello! Route values: {string.Join(", ", routeValues)} ; routeBuilder.Routes.Count: {routeBuilder.Routes.Count}");
                    });
            });
        }

        ///// <summary>
        /////  打破规则的 【* 通配符】  blockbluster
        /////  url example:http://localhost:5001/home/index/aa/112
        ///// </summary>
        ///// <param name="app"></param>
        ///// <param name="env"></param>
        //public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        //{
        //    app.UseMvc(routeBuilder =>
        //    {
        //        routeBuilder.MapRoute(template: "{controller}/{action}/{*name}"
        //            , handler: context =>
        //            {
        //                var routeValues = context.GetRouteData().Values;
        //                return context.Response.WriteAsync(
        //                    $"Hello! Route values: {string.Join(", ", routeValues)}");
        //            });
        //        routeBuilder.MapRoute(name: "aaa", template: "{controller}/{action}/aa/{name}");
        //    });
        //}

        ///// <summary>
        ///// 路由匹配后，不会被其他路由再次匹配的处理，只会成功匹配一次
        ///// </summary>
        ///// <param name="app"></param>
        ///// <param name="env"></param>
        //public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        //{
        //    app.UseMvc(routeBuilder =>
        //    {
        //        routeBuilder.MapGet(template: "hello/{name?}"
        //            , handler: context =>
        //            {
        //                var name = context.GetRouteValue("name");
        //                return context.Response.WriteAsync($"Hi, {name}!");
        //            });

        //        //路由参数 有controller  但通过第二个 Delegate类型的参数直接 定义处理方式，不通过mvc处理的方式
        //        routeBuilder.MapRoute(template: "{controller}/{action}/{name?}"
        //            , handler: context =>
        //            {
        //                var routeValues = context.GetRouteData().Values;
        //                return context.Response.WriteAsync(
        //                    $"Hello! Route values: {string.Join(", ", routeValues)}");
        //            });
        //        //与上面的路由模板规则 相同，但此处通过mvc处理的方式， 路由命中后不会再次命中
        //        routeBuilder.MapRoute(name: "aaa", template: "{controller}/{action}/{name?}"
        //            //, 
        //            //defaults: new { controller="Home"}
        //            );

        //    });
        //}

        ///// <summary>
        ///// 不同的配置路由方式 
        ///// </summary>
        ///// <param name="app"></param>
        ///// <param name="env"></param>
        //public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        //{
        //    app.UseMvc(routeBuilder =>
        //    {
        //        routeBuilder.MapGet(template: "hello/{name?}"
        //            , handler: context =>
        //            {
        //                var name = context.GetRouteValue("name");
        //                return context.Response.WriteAsync($"Hi, {name}!");
        //            });

        //        //路由参数 有controller  但通过第二个 Delegate类型的参数直接 定义处理方式，不通过mvc处理的方式
        //        routeBuilder.MapRoute(template: "{controller}/{action}/{name?}"
        //            , handler: context =>
        //            {
        //                var routeValues = context.GetRouteData().Values;
        //                return context.Response.WriteAsync(
        //                    $"Hello! Route values: {string.Join(", ", routeValues)}");
        //            });
        //        //与上面的路由模板规则 相同，但此处通过mvc处理的方式， 路由命中后不会再次命中
        //        routeBuilder.MapRoute(name: "aaa", template: "{controller}/{action}/{name?}"
        //            //, 
        //            //defaults: new { controller="Home"}
        //            );

        //    });
        //}

        ///// <summary>
        ///// url中的子路径中带多个route参数的情形   不同的配置路由方式 
        ///// </summary>
        ///// <param name="app"></param>
        ///// <param name="env"></param>
        //public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        //{
        //    app.UseMvc(routeBuilder =>
        //    {
        //        routeBuilder.MapGet(template: "hello/{name?}"
        //            , handler: context => 
        //            {
        //                var name = context.GetRouteValue("name");
        //                return context.Response.WriteAsync($"Hi, {name}!");
        //            });
        //        routeBuilder.MapRoute(template: "{controller}/{action}/{name?}"
        //            , handler: context =>
        //            {
        //                var name = context.GetRouteValue("name");
        //                return context.Response.WriteAsync($"Hi controller action:, {name}!");
        //            });
        //        routeBuilder.MapRoute(template: "{controller}/{action}22{id}/{name}/{value}"
        //            , handler: context =>
        //            {
        //                var name = context.GetRouteValue("name");
        //                var value = context.GetRouteValue("value");
        //                var id = context.GetRouteValue("id");
        //                return context.Response.WriteAsync($"Hi controller action22id :, {id}+{name}+{value}!");
        //            });
        //    });

        //}

        ///// <summary>
        ///// * 通配符对后续影响
        ///// </summary>
        ///// <param name="app"></param>
        ///// <param name="env"></param>
        //public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        //{
        //    app.UseMvc(routeBuilder =>
        //    {

        //        routeBuilder.MapRoute(template: "{controller}/{action}/{*name}"
        //            , handler: context =>
        //            {
        //                var name = context.GetRouteValue("name");
        //                return context.Response.WriteAsync($"Hi controller action:, {name}!");
        //            });
        //        routeBuilder.MapRoute(template: "{controller}/{action}/{name}/{id}"
        //            , handler: context =>
        //            {
        //                var name = context.GetRouteValue("name");
        //                var id = context.GetRouteValue("id");
        //                return context.Response.WriteAsync($"Hi controller action22id :, {id}+{name}!");
        //            });
        //    });

        //}

        //public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        //{
        //    app.UseMvc(routes =>
        //    {
        //    routes.MapRoute(
        //        name: "default",
        //        //template: "{controller=Home}/{action=Index}/{id?}"
        //        template: "{controller}/{action}/{id?}",
        //        defaults: new { controller = "Home", action = "Index" }
        //            );
        //    });
        //}
        /*
            // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
            public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            #region
            //var trackPackageRouteHandler = new RouteHandler(context =>
            //{
            //    var routeValues = context.GetRouteData().Values;
            //    return context.Response.WriteAsync(
            //        $"Hello! Route values: {string.Join(", ", routeValues)}");
            //});

            //var routeBuilder = new RouteBuilder(app, trackPackageRouteHandler);

            //routeBuilder.MapRoute(
            //    "Track Package Route",
            //    "package/{operation:regex(^track|create|detonate$)}/{id:int}");

            //routeBuilder.MapGet("hello/{name}", context =>
            //{
            //    var name = context.GetRouteValue("name");
            //    // The route handler when HTTP GET "hello/<anything>" matches
            //    // To match HTTP GET "hello/<anything>/<anything>, 
            //    // use routeBuilder.MapGet("hello/{*name}"
            //    return context.Response.WriteAsync($"Hi, {name}!");
            //});

            //var routes = routeBuilder.Build();
            //app.UseRouter(routes);

            var trackPackageRouteHandler = new RouteHandler(context =>
            {
                var routeValues = context.GetRouteData().Values;
                return context.Response.WriteAsync(
                    $"Hello! Route values: {string.Join(", ", routeValues)}");
            });
            app.UseRouter(
                routeBuilder =>
                {
                    routeBuilder.DefaultHandler = trackPackageRouteHandler;
                    routeBuilder.MapRoute(
                        "Track Package Route",
                        "package/{operation:regex(^track|create|detonate$)}/{id:int}");
                    routeBuilder.MapGet("hello/{name}", context =>
                    {
                        var name = context.GetRouteValue("name");
                        // The route handler when HTTP GET "hello/<anything>" matches
                        // To match HTTP GET "hello/<anything>/<anything>, 
                        // use routeBuilder.MapGet("hello/{*name}"
                        return context.Response.WriteAsync($"Hi, {name}!");
                    });
                }
                );
            #endregion

            app.UseMvc();
        }

        */
    }
}
