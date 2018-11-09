using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConfigurationExample.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ConfigurationExample.EFConfigurationProvider
{
    public class EFConfigurationProvider:ConfigurationProvider
    {
        private DbContextOptions _dbContextOptions;

        public EFConfigurationProvider(DbContextOptions dbContextOptions)
        {
            this._dbContextOptions = dbContextOptions;
        }
        public override void Load()
        {
            EFConfigurationContext efContext = new EFConfigurationContext(_dbContextOptions);
            efContext.Values.Add(new EFConfigurationValue {Id = "1", Value = "EFConfigurationvalue1"});
            efContext.Values.Add(new EFConfigurationValue {Id = "2", Value = "EFConfigurationvalue2"});
            efContext.Values.Add(new EFConfigurationValue {Id = "3", Value = "EFConfigurationvalue3"});
            foreach (var item in efContext.Values) {
                Data.Add(item.Id, item.Value);
            }
        }


    }
}
