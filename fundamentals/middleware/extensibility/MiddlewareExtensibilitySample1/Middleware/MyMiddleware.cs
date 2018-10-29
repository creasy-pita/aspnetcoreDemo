using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiddlewareExtensibilitySample1.Middleware
{
    public class MyMiddleware 
    {
        RequestDelegate _next;

        public MyMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext context)
        {
            Console.WriteLine("begin mymiddleware handler logic");
            context.Response.WriteAsync("begin mymiddleware handler logic!");
            Task t =_next.Invoke(context);
            context.Response.WriteAsync("end mymiddleware handler logic!");
            Console.WriteLine("end mymiddleware handler logic");
            return t;
        }
    }
}
