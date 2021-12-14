using System;
using Microsoft.AspNetCore.Authentication;

namespace f7.Services
{
    public static class JwtAuthenticationExtension
    {
        public static AuthenticationBuilder AddJwtAuthentication(
            this AuthenticationBuilder builder,
            Action<JwtAuthenticationOptions> options,
            string authScheme = "JwtAuth",
            string displayName = "Jwt Authentication")
        {
            return builder.AddScheme<JwtAuthenticationOptions, JwtAuthenticationHandler>(
                authScheme, displayName, options);
        }
    }
}
