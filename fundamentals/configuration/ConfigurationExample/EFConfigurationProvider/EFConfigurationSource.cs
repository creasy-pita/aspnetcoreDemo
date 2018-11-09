using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConfigurationExample.EFConfigurationProvider
{
    public class EFConfigurationSource : IConfigurationSource
    {
        private DbContextOptions _dbContextOptions;

        public EFConfigurationSource(DbContextOptions dbContextOptions)
        {
            this._dbContextOptions = dbContextOptions;
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new EFConfigurationProvider(_dbContextOptions);
        }
    }
}
