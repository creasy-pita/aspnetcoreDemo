using IdentityServer4.Services;
using IdentityServer4.Stores;
using MyCookieAuthSample.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using System.Security.Claims;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using MyCookieAuthSample.Models;

namespace MyCookieAuthSample.Services
{
    public class MyProfileService:IProfileService
    {
        private UserManager<ApplicationUser> _userManager;

        public MyProfileService(UserManager<ApplicationUser> userManager) {
            this._userManager = userManager;
        }

        private List<Claim> GetClaimsFromUser(ApplicationUser user)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(JwtClaimTypes.Subject, user.Id.ToString()));
            claims.Add(new Claim(JwtClaimTypes.PreferredUserName, user.UserName));

            //var roles = await _userManager.GetRolesAsync(user);
            //foreach (var role in roles)
            //{
            //    claims.Add(new Claim(JwtClaimTypes.Role,role));
            //}
            return claims;
        }


        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var subjectId = context.Subject.Claims.FirstOrDefault(c => c.Type == "sub").Value;
            var user = await _userManager.FindByIdAsync(subjectId);

            context.IssuedClaims = GetClaimsFromUser(user);
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            context.IsActive = true;

            var subjectId = context.Subject.Claims.FirstOrDefault(c => c.Type == "sub").Value;
            var user = await _userManager.FindByIdAsync(subjectId);
            context.IsActive = user != null;
        }
    }
}
