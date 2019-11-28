using base_template.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace base_template.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

    }
}