using Microsoft.AspNetCore.Identity;

namespace OAuth_Project.Models
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        
    }
}
