﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsingOptionsSample.Models
{
    public class MyDelegateConfigOptions
    {
        public MyDelegateConfigOptions()
        {
            // Set default value.
            Option1 = "value1_from_ctor";
        }

        public string Option1 { get; set; }
        public int Option2 { get; set; } = 5;
    }
}
