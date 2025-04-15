using Microsoft.EntityFrameworkCore;
using testingUK.Model.Dto;

namespace testingUK.Data
{
    public class LikeDbContext : DbContext
    {
        public LikeDbContext(DbContextOptions<LikeDbContext> options) : base(options)
        {

        }

        public DbSet<Like> Like { get; set; }
    }
}
