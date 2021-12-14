using System.Threading.Tasks;
using System.Security.Claims;
// using System.IdentityModel.Tokens.Jwt; ???:Đ???
using Microsoft.AspNetCore.Authorization;

namespace f7.Authorization
{
    public class IsRoleAdminRequirement : IAuthorizationRequirement
    {
        public string roleValue { get; set; }
        public IsRoleAdminRequirement(string rolevalue)
        {
            roleValue = rolevalue;
        }
    }
    public class HasRoleAdminHadler : AuthorizationHandler<IsRoleAdminRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsRoleAdminRequirement requirement)
        {
            if (context.User.HasClaim(ClaimTypes.Role, requirement.roleValue))
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
