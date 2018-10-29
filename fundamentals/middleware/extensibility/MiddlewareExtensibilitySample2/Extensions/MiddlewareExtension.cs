using Microsoft.AspNetCore.Builder;
using MiddlewareExtensibilitySample2.Middleware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiddlewareExtensibilitySample2.Extensions
{
    public static class MiddlewareExtension
    {
        public static IApplicationBuilder UseMyMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<PerRequestDIMiddleware1>();
        }

        //public static IApplicationBuilder UseMyMiddleware2(this IApplicationBuilder app, IMyScopedService myScopedService)
        //{
        //    return app.UseMiddleware<PerRequestDIMiddleware2>(new object[] { myScopedService });
        //}

        public static IApplicationBuilder UseMyMiddleware2(this IApplicationBuilder app)
        {
            return app.UseMiddleware<PerRequestDIMiddleware2>();
        }
    }
}
