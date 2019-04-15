using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DependencyInjectionSample.Interfaces;
using DependencyInjectionSample.Services;
using ThirdPartyAssembly;

namespace DependencyInjectionSample.Pages
{
    #region snippet1
    public class IndexModel : PageModel
    {
        private readonly IMyDependency _myDependency;



        public IndexModel(
            IMyDependency myDependency, 
            OperationService operationService,
            OperationService operationService2,
            IOperationTransient transientOperation,
            IOperationScoped scopedOperation,
            IOperationSingleton singletonOperation,
            IOperationSingletonInstance singletonInstanceOperation,
            IFoo foo,
            IBar bar,
            IThirdPartyService thirdPartyService
            )
        {
            _myDependency = myDependency;
            OperationService = operationService;
            OperationService2 = operationService2;
            TransientOperation = transientOperation;
            ScopedOperation = scopedOperation;
            SingletonOperation = singletonOperation;
            SingletonInstanceOperation = singletonInstanceOperation;
            Foo = foo;
            Bar = bar;
            ThirdPartyService = thirdPartyService;
        }
        public IFoo Foo { get; }
        public IBar Bar { get; }
        public OperationService OperationService { get; }
        public OperationService OperationService2 { get; }
        public IThirdPartyService ThirdPartyService { get; }
        public IOperationTransient TransientOperation { get; }
        public IOperationScoped ScopedOperation { get; }
        public IOperationSingleton SingletonOperation { get; }
        public IOperationSingletonInstance SingletonInstanceOperation { get; }

        public async Task OnGetAsync()
        {


            await _myDependency.WriteMessage(
                "IndexModel.OnGetAsync created this message.");
        }
    }
    #endregion
}
