using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using JwtAuthSample.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.IdentityModel.Tokens.Jwt;
using JwtAuthSample.ViewModels;

namespace JwtAuthSample.Controllers
{
    [Route("api/[controller]")]
    public class AuthorizeController : Controller
    {
        public JwtSettings _jwtSettings;

        public AuthorizeController(IOptions<JwtSettings> jwtSettingsAccesor)
        {
            _jwtSettings = jwtSettingsAccesor.Value;
        }
        // GET api/values
        public IActionResult Token(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                //if (!(model.User == "jesse" && model.Password == "123456"))
                //{
                //    return BadRequest();
                //}
                var claims = new Claim[]
                {
                new Claim(ClaimTypes.Name, "jesse"),
                new Claim(ClaimTypes.Role,"admin")
                };
                var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(_jwtSettings.Issuer,
                    _jwtSettings.Audience,
                    claims,
                    DateTime.Now,
                    DateTime.Now.AddMinutes(1),
                    creds);
            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
            }
            return Ok();
        }
    }
}
