using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConfigurationExample.EFConfigurationProvider
{
    public class PropConfigurationSource : IConfigurationSource
    {
        private string _fullPath;

        public PropConfigurationSource(string fullPath)
        {
            this._fullPath = fullPath;
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new PropConfigurationProvider(_fullPath);
        }
    }
}
