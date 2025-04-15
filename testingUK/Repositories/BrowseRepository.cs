using Microsoft.EntityFrameworkCore;
using testingUK.Data;
using testingUK.Model;

namespace testingUK.Repositories
{
    public class BrowseRepository : IBrowseRepository
    {
        private readonly TripDbContext dbContext;

        public BrowseRepository(TripDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<Trip>> getAllPublic() {

            var trips = dbContext.Trips.AsQueryable();

            trips = trips.Where(x=>x.IsPublic==true);

            return await trips.ToListAsync();

        
        }
    }
}
