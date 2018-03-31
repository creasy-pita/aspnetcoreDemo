using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MyCookieAuthSample.Data
{
    public static class WebHostMigrationExtensions
    {
        public static IWebHost MigrationDbContext<TContext>(this IWebHost host, Action<TContext, IServiceProvider> seeder)where TContext:DbContext
        {
            using (var scope = host.Services.CreateScope())
            {//只在本区间内有效
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<TContext>>();
                var context = services.GetService<TContext>();

                try
                {
                    context.Database.Migrate();
                    seeder(context, services);

                    logger.LogInformation($"执行DBContext {typeof(TContext).Name} seed执行成功");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, $"执行DBContext {typeof(TContext).Name} seed方法失败");
                }
            }



            return host;
        }

    }
}
