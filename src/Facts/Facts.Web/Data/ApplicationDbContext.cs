using Facts.Web.Data.Dto;
using Microsoft.EntityFrameworkCore;

namespace Facts.Web.Data
{
    public class ApplicationDbContext : DbContextBase
    {
        public DbSet<Fact> Facts { get; set; }
        public DbSet<Tag> Tags { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
    }
}
