using System;
using System.Linq;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using f7.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.Text;
using Microsoft.Extensions.Options;

namespace f7.Services
{
    public class JwtAuthenticationService
    {
        f7DbContext _dbContext;
        IEmailSender _emailSender;
        IConfiguration _configuration;
        JwtAuthenticationOptions _option;

        public JwtAuthenticationService(
            f7DbContext dbContext,
            IEmailSender emailSender,
            IConfiguration configuration,
            IOptions<JwtAuthenticationOptions> option
        )
        {
            _dbContext = dbContext;
            _emailSender = emailSender;
            _configuration = configuration;
            _option = option.Value;
        }
        public string GenerateJwtToken(f7AppUser model)
        {
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
            var securityKey = new SymmetricSecurityKey(key);
            var Credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var Claims = new List<Claim>(){
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, model.UserName),
                new Claim(JwtRegisteredClaimNames.Email, model.Email),
                new Claim("role", _dbContext.UserClaims.Where(c => (c.ClaimType == "role" && c.ClaimValue == "admin"))
                                                       .Select(c => c.ClaimValue)
                                                       .FirstOrDefault()
                                                       .ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: Claims,
                expires: DateTime.Now.AddHours(10),
                signingCredentials: Credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string ValidateToken(string token)
        {
            SecurityToken validateToken;
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var tokenValidationParam = new TokenValidationParameters
            {
                ValidIssuer = "local host 5001",
                ValidateIssuer = true,
                IssuerSigningKey = key,
            };

            ClaimsPrincipal user = tokenHandler.ValidateToken(
                token,
                _option.tokenValidationParam,
                out validateToken
            );
            return "";
        }
    }
    public class InputModel
    {
        public string username { get; set; }
        public string password { get; set; }
    }
}
