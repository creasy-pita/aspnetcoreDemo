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

namespace MyCookieAuthSample.Controllers
{
    public class AccountController : Controller
    {
        private SignInManager<ApplicationUser> signInManager;
        private UserManager<ApplicationUser> userManager;

        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        public IActionResult Register()
        {


            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            var identityUser = new ApplicationUser {
                Email = registerViewModel.Email,
                UserName = registerViewModel.Email,
                NormalizedUserName = registerViewModel.Email
               
            };

            var identityResult = await userManager.CreateAsync(identityUser,registerViewModel.Password);
            if (identityResult.Succeeded)
            {
                return RedirectToAction("Home", "Index");
            }
            return View();
        }

        public IActionResult LogIn()
        {
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

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
