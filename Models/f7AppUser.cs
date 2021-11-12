using Microsoft.AspNetCore.Identity;
namespace f7.Models
{
    public class f7AppUser : IdentityUser
    {
        public string DisplayName { get; set; }
    }
}
