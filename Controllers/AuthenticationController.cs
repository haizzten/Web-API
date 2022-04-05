using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

using f7.Models.Models;
using f7.Services;
using f7.Models;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace f7.Models.Models.Controllers
{
    [Authorize]
    [Route("api/[action]")]
    public class AuthController : Controller
    {
        f7DbContext _dbContext;
        JwtAuthenticationService _authService;
        private IConfiguration Configuration;
        UserManager<f7AppUser> _userManager;
        public AuthController(
            f7DbContext dbContext,
            IConfiguration configuration,
            JwtAuthenticationService authService,
            UserManager<f7AppUser> userManager)
        {
            _dbContext = dbContext;
            _authService = authService;
            Configuration = configuration;
            _userManager = userManager;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Authorization([FromBody] InputModel model)
        {
            IActionResult result = Unauthorized();
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(userName: model.username);
                if (user != null)
                {
                    if (await _userManager.CheckPasswordAsync(user, model.password))
                    {
                        return Ok(new LoginResult
                        {
                            Principal = "local host",
                            Issuer = "local host too",
                            AccessToken = _authService.GenerateJwtToken(model: user)
                        });
                    }
                }
            }
            return result;
        }

        [Authorize("Admin role")]
        [HttpGet]
        public async Task<IActionResult> GetAllUser()
        {
            return Ok(await _dbContext.Users
                .Select(u => new { u.DisplayName, u.Email, u.Id })
                .Take(10)
                .ToListAsync());
        }
    }
    public class LoginResult
    {
        [JsonPropertyName("principal")]
        public string Principal { get; set; }

        [JsonPropertyName("isser")]
        public string Issuer { get; set; }

        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }
    }
}
