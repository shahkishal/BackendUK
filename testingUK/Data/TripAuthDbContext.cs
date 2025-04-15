using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace testingUK.Data
{
    public class TripAuthDbContext : IdentityDbContext

    {
        public TripAuthDbContext(DbContextOptions<TripAuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var userRoleId = "7d41c1a7-6367-4e92-8027-c54cfdc9dff2";
            var adminRoleId = "5f4b5098-ca0b-4b53-a3bb-51139ef16335";

            var roles = new List<IdentityRole>
            {

                new IdentityRole
                {
                    Id = userRoleId,
                    ConcurrencyStamp = userRoleId,
                    Name = "User",
                    NormalizedName = "User".ToUpper()
                },  
                new IdentityRole
                {
                    Id = adminRoleId,
                    ConcurrencyStamp = adminRoleId,
                    Name = "Admin",
                    NormalizedName = "Admin".ToUpper()
                }

            };

            builder.Entity<IdentityRole>().HasData(roles);

        }

    }
}
