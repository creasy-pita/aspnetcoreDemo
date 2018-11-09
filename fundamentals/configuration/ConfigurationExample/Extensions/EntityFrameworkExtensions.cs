using ConfigurationExample.EFConfigurationProvider;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.Extensions.Configuration
{
    public static class EntityFrameworkExtensions
    {
        public static IConfigurationBuilder AddEFConfiguration(this IConfigurationBuilder builder,DbContextOptions options)
        {
            return builder.Add(new EFConfigurationSource(options));
        }
    }
}
