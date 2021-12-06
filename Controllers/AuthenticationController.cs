using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

using f7;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Collections.Generic;
using System.Security.Claims;

namespace f7.Controllers
{
    [Authorize]
    [Route("Authentication/[action]")]
    public class AuthenticationController : Controller
    {
        private IConfiguration Configuration;
        public AuthenticationController(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        [AllowAnonymous]
        // public async Task<IActionResult> Login(UserModel model)
        public IActionResult Login(UserModel model)
        {
            IActionResult result = Unauthorized();
            if (ModelState.IsValid)
            {
                if (model.Password == "123" && model.UserName == "admin")
                {
                    result = Ok(GenerateJwtToken(model: model));
                }
            }
            // await Task.FromResult(result);
            return result;
        }
        private string GenerateJwtToken(UserModel model)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]));
            var Credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var Claims = new List<Claim>(){
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, model.UserName),
                new Claim(JwtRegisteredClaimNames.Email, "huy20022001@gmail.com")
            };

            var token = new JwtSecurityToken(
                issuer: Configuration["Jwt:Issuer"],
                audience: Configuration["Jwt:Audience"],
                claims: Claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: Credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }
    public class UserModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
