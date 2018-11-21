using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThirdPartyAssembly;

namespace DependencyInjectionSample.Services
{
    public class MyServiceDependOnThirdPartyService
    {
        public ThirdPartyService ThirdPartyService { get; }
        public MyServiceDependOnThirdPartyService(ThirdPartyService thirdPartyService)
        {
            ThirdPartyService = thirdPartyService;
        }
    }
}
