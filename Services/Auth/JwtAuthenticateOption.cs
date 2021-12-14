using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;

namespace f7.Services
{
    public class JwtAuthenticationOptions : AuthenticationSchemeOptions
    {
        public TokenValidationParameters tokenValidationParam { get; set; }
        public SecurityTokenHandler tokenHandler { get; set; }
    }
}
