

using Microsoft.EntityFrameworkCore;

namespace SportsEvents.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }
        public DbSet<Sport> Sport { get; set; }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<Membership> Membership { get; set; }
        public DbSet<Member> Member { get; set; }
    }
}
