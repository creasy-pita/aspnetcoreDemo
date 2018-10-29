using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiddlewareExtensibilitySample2
{
    public interface IMyScopedService
    {
        int MyProperty { set; get; }
    }
}
