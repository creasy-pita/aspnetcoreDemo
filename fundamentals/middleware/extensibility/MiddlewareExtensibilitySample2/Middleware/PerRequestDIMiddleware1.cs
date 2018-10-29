using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiddlewareExtensibilitySample2.Middleware
{
    public class PerRequestDIMiddleware1
    {
        private readonly RequestDelegate _next;

        public PerRequestDIMiddleware1(RequestDelegate next)
        {
            _next = next;
        }

        // IMyScopedService is injected into Invoke
        public async Task Invoke(HttpContext httpContext, IMyScopedService myScopedService)
        {
            myScopedService.MyProperty++;
            await httpContext.Response.WriteAsync($"myScopedService.MyProperty:{myScopedService.MyProperty}");
            await _next(httpContext);
        }
    }
}
