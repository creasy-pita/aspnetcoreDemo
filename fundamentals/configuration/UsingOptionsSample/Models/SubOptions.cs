using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsingOptionsSample.Models
{
    public class SubOptions
    {
        public SubOptions()
        {
            // Set default value.
            SubOption1 = "value1_from_ctor";
        }

        public string SubOption1 { get; set; }
        public int SubOption2 { get; set; } = 5;
    }
}
