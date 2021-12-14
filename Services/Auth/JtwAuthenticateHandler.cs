using System.Text.Encodings.Web;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.IdentityModel.Tokens;

namespace f7.Services
{
    public class JwtAuthenticationHandler : AuthenticationHandler<JwtAuthenticationOptions>
    {
        public JwtAuthenticationHandler(IOptionsMonitor<JwtAuthenticationOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock) { }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            // SecurityToken validatedToken;

            ClaimsPrincipal principal = ValidateToken(out SecurityToken validatedToken);
            if (principal == null) return AuthenticateResult.NoResult();

            AuthenticationTicket ticket = new AuthenticationTicket(principal, Scheme.Name);
            Logger.LogInformation($"validatedToken is: {validatedToken}\n");

            return AuthenticateResult.Success(ticket);
        }

        private ClaimsPrincipal ValidateToken(out SecurityToken validatedToken)
        {
            var validateParam = Options.tokenValidationParam.Clone();
            var validator = Options.tokenHandler;
            string token = null;
            validatedToken = null;

            string authorizationHeader = Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(authorizationHeader))
            {
                Logger.LogError("token null after retreive 'authorization' header");
                return null;
            }

            if (authorizationHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                token = authorizationHeader.Remove(0, 6).Trim();

            if (string.IsNullOrEmpty(token))
            {
                Logger.LogError("token null after remove 'Bearer'");
                return null;
            }
            ClaimsPrincipal result = validator.ValidateToken(token, validateParam, out validatedToken);
            return result;
        }
    }
}
