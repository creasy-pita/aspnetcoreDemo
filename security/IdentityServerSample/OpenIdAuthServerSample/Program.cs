using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MyCookieAuthSample.Data;

namespace MyCookieAuthSample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args)
                .MigrationDbContext<ApplicationDbContext>((context, services)=>
                    {
                        new ApplicationDbContextSeed().SeedAsync(context, services).Wait();
                    }
                )
                .Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                //.UseUrls("http://localhost:5000")
                .UseStartup<Startup>()
                .Build();
    }
}
