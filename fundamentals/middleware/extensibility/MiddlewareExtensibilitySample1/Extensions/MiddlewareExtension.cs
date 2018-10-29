using Microsoft.AspNetCore.Builder;
using MiddlewareExtensibilitySample1.Middleware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiddlewareExtensibilitySample1.Extensions
{
    public static class MiddlewareExtension
    {
        public static IApplicationBuilder UseMyMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<MyMiddleware>();
        }

        public static IApplicationBuilder UseMyMiddlewareWithPara(this IApplicationBuilder app,int value)
        {
            return app.UseMiddleware<MyMiddlewareWithPara>(new object[] { value});
        }
    }
}
