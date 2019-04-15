using ConfigurationExample.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConfigurationExample.EFConfigurationProvider
{
    public class PropConfigurationContext:DbContext
    {
        public PropConfigurationContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<EFConfigurationValue> Values { get; set; }
    }
}
