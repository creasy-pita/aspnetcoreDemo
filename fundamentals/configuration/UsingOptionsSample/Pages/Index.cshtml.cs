using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using UsingOptionsSample.Models;

namespace UsingOptionsSample.Pages
{
    public class IndexModel : PageModel
    {
        public MyOptions options;
        public MyDelegateConfigOptions delegateConfigOptions;
        public SubOptions subOptions;
        public MyOptions snapshotoptions;
        public MyOptions namedOptions1;
        public MyOptions namedOptions2;

        public IndexModel(IOptions<MyOptions> optionsAccessor
            ,IOptions<MyDelegateConfigOptions> delegateConfigOptionsAccessor
            ,IOptions<SubOptions> subOptionsAccessor
            ,IOptionsSnapshot<MyOptions> namedOptionsAccessor
            , IOptionsSnapshot<MyOptions> snapshotOptionsAccessor
            )
        {
            options = optionsAccessor.Value;
            subOptions = subOptionsAccessor.Value;
            delegateConfigOptions = delegateConfigOptionsAccessor.Value;
            snapshotoptions = snapshotOptionsAccessor.Value;
            namedOptions1 = namedOptionsAccessor.Get("named_options_1");
            namedOptions2 = namedOptionsAccessor.Get("named_options_2");
            options.Option1 = "value_from_json_and modify by PageModel";
        }

        public void OnGet()
        {

        }
    }
}