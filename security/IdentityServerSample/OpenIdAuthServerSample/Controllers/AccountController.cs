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
using IdentityServer4.Services;
using System.Text;

namespace MyCookieAuthSample.Controllers
{
    public class AccountController : Controller
    {
        //private TestUserStore _user;
        private SignInManager<ApplicationUser> signInManager;
        private UserManager<ApplicationUser> userManager;
        private IIdentityServerInteractionService interactionService;
        //public AccountController(TestUserStore testUserStore)
        //{
        //    this._user = testUserStore;
        //}
        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IIdentityServerInteractionService interactionService)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.interactionService = interactionService;
        }

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

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel, string returnUrl = null)
        {

            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                
                ApplicationUser user = new ApplicationUser
                {
                    Email = registerViewModel.Email,
                    //PasswordHash = Convert.ToBase64String(Encoding.Unicode.GetBytes(registerViewModel.Password)),
                     //PasswordHash =registerViewModel.Password,
                    UserName = registerViewModel.Email
                };


                //var result = await userManager.CreateAsync(user);
                var result = await userManager.CreateAsync(user,"Password@123");
                if(result.Succeeded)
                {
                    var proper = new AuthenticationProperties{IsPersistent=true, ExpiresUtc= DateTime.UtcNow.AddMinutes(20)};
                    await signInManager.SignInAsync(user, proper);
                    if (interactionService.IsValidReturnUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return Redirect("/");
                    }

                }
                else
                {
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code,  error.Description);
                    }
                }
            }
            return View( registerViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Logout(LoginViewModel loginViewModel, string returnUrl = null)
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }

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
                ApplicationUser user = await userManager.FindByEmailAsync(loginViewModel.Email);

                if (user == null)
                { 
                    ModelState.AddModelError("111","username not exists");
                }
                else
                {
                    bool isSucc = await userManager.CheckPasswordAsync(user, loginViewModel.Password);
                    //bool isSucc = await userManager.CheckPasswordAsync(user, Convert.ToBase64String(Encoding.Unicode.GetBytes(loginViewModel.Password)));
                    
                    if (isSucc)
                    {
                        AuthenticationProperties properties = null;
                        if (loginViewModel.Rememberme)
                        {
                            properties = new AuthenticationProperties{IsPersistent=true, ExpiresUtc= DateTime.UtcNow.AddMinutes(1)};
                        }

                        await signInManager.SignInAsync(user, properties);
                        if(interactionService.IsValidReturnUrl(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }
                        else
                        {
                            return Redirect("/");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "a long password");
                        return View(loginViewModel);
                    }
                }
                return RedirectToReturnlUrl(returnUrl);
            }
            return View(loginViewModel);
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
