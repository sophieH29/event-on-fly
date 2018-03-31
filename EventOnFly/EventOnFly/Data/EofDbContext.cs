using EventOnFly.Models;
using Microsoft.EntityFrameworkCore;

namespace EventOnFly.Data
{
    public class EofDbContext: DbContext
    {
        public EofDbContext(DbContextOptions<EofDbContext> options)
            : base(options)
        {
        }

        public DbSet<Venue> Venues { get; set; }
    }
}
