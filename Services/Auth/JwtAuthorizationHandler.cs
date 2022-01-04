using System.Threading.Tasks;
using System.Security.Claims;
// using System.IdentityModel.Tokens.Jwt; ???:Đ???
using Microsoft.AspNetCore.Authorization;

namespace f7.Authorization
{
    public class IsRoleRequirement : IAuthorizationRequirement
    {
        public string roleValue { get; set; }
        public IsRoleRequirement(string rolevalue)
        {
            roleValue = rolevalue;
        }
    }
    public class HasRoleAdminHadler : AuthorizationHandler<IsRoleRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsRoleRequirement requirement)
        {
            if (context.User.HasClaim(ClaimTypes.Role, requirement.roleValue))
            {
                context.Succeed(requirement);
            }
            else if (context.User.IsInRole("Administrator"))
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
