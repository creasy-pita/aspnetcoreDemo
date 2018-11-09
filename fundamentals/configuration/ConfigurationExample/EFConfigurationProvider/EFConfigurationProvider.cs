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
        private Action<DbContextOptionsBuilder> _optionsAction;

        public EFConfigurationProvider(Action<DbContextOptionsBuilder> optionsAction)
        {
            this._optionsAction = optionsAction;
        }
        public override void Load()
        {
            DbContextOptionsBuilder builder = new DbContextOptionsBuilder();
            //通过 委托 _optionsAction 传入 DbContextOptionsBuilder  来配置 DbContextOptions
            //配置好了以后  通过DbContextOptionsBuilder 获取 DbContextOptions
            _optionsAction(builder);
            EFConfigurationContext efContext = new EFConfigurationContext(builder.Options);
            efContext.Values.Add(new EFConfigurationValue {Id = "quote1", Value = "EFConfigurationvalue1"});
            efContext.Values.Add(new EFConfigurationValue {Id = "quote2", Value = "EFConfigurationvalue2"});
            efContext.Values.Add(new EFConfigurationValue {Id = "quote3", Value = "EFConfigurationvalue3"});
            efContext.SaveChanges();
            foreach (var item in efContext.Values) {
                Data.Add(item.Id, item.Value);
            }
        }


    }
}
