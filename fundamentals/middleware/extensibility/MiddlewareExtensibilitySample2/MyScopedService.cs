﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiddlewareExtensibilitySample2
{
    public class MyScopedService : IMyScopedService
    {
        public int MyProperty { get ; set; }
    }
}
