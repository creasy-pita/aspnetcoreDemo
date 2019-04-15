using ConfigurationExample.EFConfigurationProvider;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.Extensions.Configuration
{
    public static class PropConfigurationExtensions
    {
        public static IConfigurationBuilder AddPropFile(this IConfigurationBuilder builder,string fullPath)
        {
            return builder.Add(new PropConfigurationSource(optionsAction));
        }
    }
}
