using Microsoft.AspNetCore.Identity;

namespace PlayingForKeepers.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string City { get; set; }
    }
}
