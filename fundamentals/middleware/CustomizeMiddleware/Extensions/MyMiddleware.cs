using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace CustomizeMiddleware.Extensions
{
public class MyMiddleware
    {
        private readonly RequestDelegate _next;
        private int _value;

        public MyMiddleware(RequestDelegate next,int value)
        {
            _next = next;
            _value = value;
        }
        public async Task Invoke(HttpContext context)
        {
            // Do something with context near the beginning of request processing.
            context.Items.Add("VisitTime", _value++);
            await  _next.Invoke(context);
            Console.WriteLine("handle the "+ _value +" request");
            // Clean up.
        }
    }


    public static class MiddleExtension
    {
        public static IApplicationBuilder UseMyMiddleware(this IApplicationBuilder builder ,int value)
        {
            return builder.UseMiddleware<MyMiddleware>(value);

        }

    }
}
