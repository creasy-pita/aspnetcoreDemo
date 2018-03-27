using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyCookieAuthSample.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using MyCookieAuthSample.ViewModels;
using IdentityServer4.Test;

namespace MyCookieAuthSample.Controllers
{
    public class AccountController : Controller
    {
        private TestUserStore _user;
        // private SignInManager<ApplicationUser> signInManager;
        // private UserManager<ApplicationUser> userManager;
        public AccountController(TestUserStore testUserStore)
        {
            this._user = testUserStore;
        }
        // public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        // {
        //     this.signInManager = signInManager;
        //     this.userManager = userManager;
        // }

        public IActionResult Register(string returnUrl =null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        public IActionResult RedirectToReturnlUrl(string returnUrl)
        {
            if (returnUrl != null && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        // [HttpPost]
        // public async Task<IActionResult> Register(RegisterViewModel registerViewModel, string returnUrl = null)
        // {

        //     ViewData["ReturnUrl"] = returnUrl;
        //     if(ModelState.IsValid)
        //     { 


        //     }
        //     return View();
        // }

        public IActionResult LogIn(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LogIn(LoginViewModel loginViewModel, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var user =  _user.FindByUsername(loginViewModel.Username);
                if (user == null)
                { 
                    ModelState.AddModelError("111","username not exists");
                }
                else
                {
                    bool isSucc = _user.ValidateCredentials(user.Username, user.Password);
                    if(isSucc)
                    {

                        var proper = new AuthenticationProperties{IsPersistent=true, ExpiresUtc= DateTime.UtcNow.AddMinutes(20)};
                         await Microsoft.AspNetCore.Http.AuthenticationManagerExtensions.SignInAsync(
                            HttpContext, user.SubjectId, user.Username, proper
                        );
                    }
                    
                }
                return RedirectToReturnlUrl(returnUrl);
            }
            return View();
        }

        public IActionResult MakeLogin()
        {
            //创建cookie

            var claims = new List<Claim> {
                new Claim(ClaimTypes.Name,"jesse"),
                new Claim(ClaimTypes.Role, "admin")
            };
            var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimIdentity));

            return Ok();
        }

        // public async Task<IActionResult> Logout()
        // {
        //     // await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        //     //await signInManager.SignOutAsync();
        //     return RedirectToAction("Index", "Home");
        // }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
