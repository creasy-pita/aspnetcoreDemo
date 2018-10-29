using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiddlewareExtensibilitySample2.Middleware
{
    public class PerRequestDIMiddleware2
    {
        private readonly RequestDelegate _next;
        private IMyScopedService _myScopedService;
        // IMyScopedService is injected into Constructor
        public PerRequestDIMiddleware2(RequestDelegate next, IMyScopedService myScopedService)
        {
            _next = next;
            _myScopedService = myScopedService;
        }

        // IMyScopedService is injected into Invoke
        public async Task Invoke(HttpContext httpContext)
        {
            _myScopedService.MyProperty++;
            await httpContext.Response.WriteAsync($"_myScopedService.MyProperty:{_myScopedService.MyProperty}");
            await _next(httpContext);
        }
    }
}
