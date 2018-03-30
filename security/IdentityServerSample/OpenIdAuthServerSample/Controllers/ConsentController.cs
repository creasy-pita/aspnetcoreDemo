using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Mvc;
using MyCookieAuthSample.Models;
using MyCookieAuthSample.Services;
using MyCookieAuthSample.ViewModels;

namespace MyCookieAuthSample.Controllers
{
    public class ConsentController : Controller
    {
        private readonly ConsentService consentService;


        public ConsentController(ConsentService consentService)
        {
            this.consentService = consentService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string returnUrl)
        {
            //根据returnurl client信息
            var model = await consentService.BuildConsentViewModel(returnUrl);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(InputViewModel viewModel)
        {
            ConsentProcessResult result =await consentService.ProcessResult(viewModel);
            if (result.IsRedirect)
            {
                return Redirect(result.RedirectUrl);
            }
            else
            {
                if (!string.IsNullOrEmpty(result.ValidateError))
                {
                    ModelState.AddModelError("", result.ValidateError);
                }
            }
            return View(result.consentViewModel);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
