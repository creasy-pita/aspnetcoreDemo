using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ConfigurationExample.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ConfigurationExample.EFConfigurationProvider
{
    public class PropConfigurationProvider:ConfigurationProvider
    {
        private string _fullPath;

        public PropConfigurationProvider(string fullPath)
        {
            this._fullPath = fullPath;
        }
        public override void Load()
        {

        }
        private static IDictionary<string, string> CreateAndSaveDefaultValues(
            string fullPath)
        {
            using (var stream = new FileStream(fullPath, FileMode.Open))
            {
                try
                {
                    Load(stream);
                }
                catch (Exception e)
                {
                    bool ignoreException = false;
                    if (Source.OnLoadException != null)
                    {
                        var exceptionContext = new FileLoadExceptionContext
                        {
                            Provider = this,
                            Exception = e
                        };
                        Source.OnLoadException.Invoke(exceptionContext);
                        ignoreException = exceptionContext.Ignore;
                    }
                    if (!ignoreException)
                    {
                        throw e;
                    }
                }
            }

            // Quotes (c)2005 Universal Pictures: Serenity
            // https://www.uphe.com/movies/serenity
            var configValues = new Dictionary<string, string>
                {
                    { "quote1", "I aim to misbehave." },
                    { "quote2", "I swallowed a bug." },
                    { "quote3", "You can't stop the signal, Mal." }
                };

            return configValues;
        }

    }
}
