using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MVCRoutes.Controllers
{
    public class HelloController : Controller
    {

        public ActionResult<IEnumerable<string>> Index(string id)
        {
            return new string[] { "value1", "value2",id };
        }
    }
}
