using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using f7.Services;
using f7.Models;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using System.Net.Http;

namespace f7.Areas.Identity.Controllers
{

    // [ApiController]
    [Area("Identity")]
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly UserManager<f7AppUser> _userManager;
        private readonly SignInManager<f7AppUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger<AccountController> _logger;

        // [BindProperty]
        // public InputModel _input { get; set; }

        [BindProperty]
        public string _payloadContent { get; set; }

        public AccountController(
            SignInManager<f7AppUser> signInManager,
            ILogger<AccountController> logger,
            UserManager<f7AppUser> userManager,
            IEmailSender emailSender)
        {
            _emailSender = emailSender;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> RegisterAsync([FromBody] InputModel inp)
        {
            return Json(new { didServerListened = "Yup!" });

            if (ModelState.IsValid)
            {
                var newUser = new f7AppUser()
                {
                    UserName = inp.UserName ?? inp.Email,
                    Email = inp.Email,
                };
                var result = await _userManager.CreateAsync(newUser, inp.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("Register new user");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = newUser.Id, code = code },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(inp.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    return StatusCode(201);
                }
            }
            return StatusCode(400);
        }
        [HttpGet]
        public async Task<IActionResult> ConfirmEmailAsync(string userId, string code)
        {
            if (userId == null || code == null)
            {
                this.Response.StatusCode = 401;
                return BadRequest();
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return BadRequest();
            }

            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _userManager.ConfirmEmailAsync(user, code);
            return result.Succeeded ? BadRequest() : Ok();
        }
        //*/
        [HttpPost]
        public async Task<IActionResult> LoginAsync(string UserName, string Password)
        {
            if (this.ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(UserName, Password, isPersistent: false, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    return StatusCode(201);
                }
            }
            return StatusCode(401);
        }

        /*/
        public async Task<IActionResult> LoginAsync(InputModel input)
        {
            if (this.ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(
                    new f7AppUser {UserName = input.UserName },
                    password: input.Password,
                    isPersistent: false,
                    lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    return StatusCode(201);
                }
            }
            return StatusCode(401);
        }
        //*/
    }
    public class InputModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 4)]
        public string UserName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Password must be at least {2} characters", MinimumLength = 8)]
        public string Password { get; set; }
    }


}
