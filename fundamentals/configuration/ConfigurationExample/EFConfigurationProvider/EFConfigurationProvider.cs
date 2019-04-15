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
            PropConfigurationContext efContext = new PropConfigurationContext(builder.Options);

            Data = !efContext.Values.Any() ?
                    CreateAndSaveDefaultValues(efContext):
                efContext.Values.ToDictionary(c => c.Id, c => c.Value);
        }
        private static IDictionary<string, string> CreateAndSaveDefaultValues(
            PropConfigurationContext dbContext)
        {
            // Quotes (c)2005 Universal Pictures: Serenity
            // https://www.uphe.com/movies/serenity
            var configValues = new Dictionary<string, string>
                {
                    { "quote1", "I aim to misbehave." },
                    { "quote2", "I swallowed a bug." },
                    { "quote3", "You can't stop the signal, Mal." }
                };

            dbContext.Values.AddRange(configValues
                .Select(kvp => new EFConfigurationValue
                {
                    Id = kvp.Key,
                    Value = kvp.Value
                })
                .ToArray());

            dbContext.SaveChanges();

            return configValues;
        }

    }
}
