using Microsoft.EntityFrameworkCore;
using testingUK.Model;

namespace testingUK.Data
{
    public class TripDbContext : DbContext
    {
        public TripDbContext(DbContextOptions<TripDbContext> options) : base(options)
        {
            
        }

        public DbSet<Trip> Trips { get; set; }
        public DbSet<TravelType> TravelType { get; set; }

    }
}
