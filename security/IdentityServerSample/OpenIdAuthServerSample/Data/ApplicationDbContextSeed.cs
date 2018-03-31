using Microsoft.AspNetCore.Identity;
using MyCookieAuthSample.Data;
using MyCookieAuthSample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace MyCookieAuthSample.Data
{
    public class ApplicationDbContextSeed
    {
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<ApplicationUserRole> _roleManager;

        public async Task SeedAsync(ApplicationDbContext context, IServiceProvider services)
        {
            if (!context.Roles.Any())
            {
                _roleManager = services.GetRequiredService<RoleManager<ApplicationUserRole>>();

                var role = new ApplicationUserRole
                {
                    Name = "Administrator",
                    NormalizedName = "Administrator"
                };

                var result = await _roleManager.CreateAsync(role);
                if (!result.Succeeded)
                {
                    throw new Exception("初始默认角色失败");
                }
            }


            if (!context.Users.Any())
            {
                _userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

                var defaultUser = new ApplicationUser
                {
                    UserName = "Administrator",
                    Email = "ljq@qq.com",
                    NormalizedUserName = "admin"
                };

                var result = await _userManager.CreateAsync(defaultUser, "123456");
                await _userManager.AddToRoleAsync(defaultUser, "Administrator");
                if (!result.Succeeded)
                {
                    throw new Exception("初始默认用户失败");
                }
            }
        }
    }
}
