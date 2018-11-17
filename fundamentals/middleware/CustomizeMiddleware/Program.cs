using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CustomizeMiddleware
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            IWebHostBuilder webHostBuilder = WebHost.CreateDefaultBuilder(args);
            webHostBuilder = webHostBuilder.UseStartup<Startup>();
            webHostBuilder = webHostBuilder.UseUrls("http://0.0.0.0:5001");
            return webHostBuilder.Build();
        }

        //public static IWebHost BuildWebHost(string[] args) =>
        //    WebHost.CreateDefaultBuilder(args)
        //        .UseStartup<Startup>()
        //        //.UseUrls("http://192.168.174.128:5000")
        //        .UseUrls("http://0.0.0.0:5001")
        //        .Build();
    }
}
