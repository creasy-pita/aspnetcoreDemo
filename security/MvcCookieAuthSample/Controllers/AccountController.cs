using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MvcCookieAuthSample.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using MvcCookieAuthSample.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MvcCookieAuthSample.Controllers
{

    public class AccountController : Controller
    {
        private UserManager<ApplicationUser> _userManager;//创建用户的
        private SignInManager<ApplicationUser> _signInManager;//用来登录的

        //内部跳转
        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {//如果是本地
                return Redirect(returnUrl);
            }

            return RedirectToAction(nameof(HomeController.Index),"Home");
        }

        //添加验证错误
        private void AddError(IdentityResult result)
        {
            //遍历所有的验证错误
            foreach (var error in result.Errors)
            {
                //返回error到model
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        //依赖注入
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        public IActionResult Register(string returnUrl=null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                ViewData["ReturnUrl"] = returnUrl;
                var identityUser = new ApplicationUser
                {
                    Email = registerViewModel.Email,
                    UserName = registerViewModel.Email,
                    NormalizedUserName = registerViewModel.Email
                };
                var identityResult = await _userManager.CreateAsync(identityUser, registerViewModel.Password);
                if (identityResult.Succeeded)
                {
                    //注册完成登录生成cookies信息
                    await _signInManager.SignInAsync(identityUser, new AuthenticationProperties { IsPersistent = true });

                    //return RedirectToAction("Index", "Home");
                    return RedirectToLocal(returnUrl);//跳转到注册之前的Url
                }
                else//注册失败
                {
                    //添加验证错误
                    AddError(identityResult);
                }
            }

            return View();
        }

        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel,string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                ViewData["ReturnUrl"] = returnUrl;

                var user = await _userManager.FindByEmailAsync(loginViewModel.Email);
                if (user == null)
                {
                    //异常先不写，后期统一收集
                }

                //账号密码先不做验证，需要可以自己写
                await _signInManager.SignInAsync(user, new AuthenticationProperties { IsPersistent = true });

                return RedirectToLocal(returnUrl);//跳转到登陆之前的Url
            }

            return View();
            
        }



        //登陆
        public IActionResult MakeLogin()
        {
            var claims=new List<Claim>(){
                new Claim(ClaimTypes.Name,"wyt"),
                new Claim(ClaimTypes.Role,"admin")
            };

            var claimIdentity= new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);

            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,new ClaimsPrincipal(claimIdentity));

            return Ok();
        }

        //登出
        public async Task<IActionResult> Logout()
        {
            //HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            //return Ok();

            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}