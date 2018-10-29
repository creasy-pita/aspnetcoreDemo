using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiddlewareExtensibilitySample1.Middleware
{
    public class MyMiddlewareWithPara
    {
        RequestDelegate _next;
        int _value;

        public MyMiddlewareWithPara(RequestDelegate next,int value)
        {
            _next = next;
            _value = value;
        }

        public Task Invoke(HttpContext context)
        {
            //Console.WriteLine($"invoke count {++_value}");
            context.Response.WriteAsync($"invoke count {++_value}");
            Task t =_next.Invoke(context);
            return t;
        }
    }
}
