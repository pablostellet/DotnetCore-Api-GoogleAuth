using Microsoft.AspNetCore.Identity;

namespace base_template.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}