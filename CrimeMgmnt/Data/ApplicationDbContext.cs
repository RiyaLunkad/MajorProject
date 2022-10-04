using Microsoft.EntityFrameworkCore;
using CrimeMgmnt.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CrimeMgmnt.Data
{
    public class ApplicationDbContext: IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<User> users { get; set; }

        public DbSet<CyberCell> cyberCells { get; set; }
        
    }
}
