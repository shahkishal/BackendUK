using Microsoft.EntityFrameworkCore;
using testingUK.Model.Dto;

namespace testingUK.Data
{
    public class CommentDbContext : DbContext
    {
        public CommentDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Comment> Comment { get; set; }
    }
}
