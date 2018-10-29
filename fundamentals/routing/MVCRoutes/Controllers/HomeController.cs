using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;

namespace MVCRoutes.Controllers
{
    [Route("/Index1")]
    public class HomeController : Controller
    {
        private IApplicationBuilder _applicationBuilder;

        public HomeController(IApplicationBuilder applicationBuilder)
        {
            this._applicationBuilder = applicationBuilder;
        }


        [Route("[controller]/[action]/aa/{id}")]
        public ActionResult<IEnumerable<string>> Index(string id)
        {
            var routeValues = ControllerContext.RouteData.Values;
            return new string[] { $"Hello! Route values: {string.Join(", ", routeValues)}"
            };
        }

        public ActionResult<IEnumerable<string>> Index1(string id, string name = "defaultname", string phone = "defaultphone")
        {
            var routeValues = ControllerContext.RouteData.Values;
            return new string[] { $"id:{id}", $"name:{name}", $"phone:{phone}"
                , $"Hello! Route values: {string.Join(", ", routeValues)}"
                ,$"QueryString:{HttpContext.Request.QueryString}"
            };
        }

    }
}
