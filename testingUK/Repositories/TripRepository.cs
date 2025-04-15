using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using testingUK.Data;
using testingUK.Model;
using testingUK.Model.Dto;

namespace testingUK.Repositories
{
    public class TripRepository : ITripRepository
    {
        private readonly TripDbContext dbContext;

        public TripRepository(TripDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Trip> Create(Trip trip)
        {
            await dbContext.Trips.AddAsync(trip);
            await dbContext.SaveChangesAsync();
            return trip;
        }

        public async Task<Trip?> Delete(Guid Id)
        {
            var domain = await dbContext.Trips.FirstOrDefaultAsync(x => x.Id == Id);

            if (domain == null) return null;

            dbContext.Trips.Remove(domain);

            await dbContext.SaveChangesAsync();

            return domain;
        }

        

        public async Task<GetAllReturnDto> GetAll(string? filterOn = null, 
            string? filterQuery = null, string? sortBy = null, bool isAscending = true
            ,   int pageNumber=1 , int pageSize=20 , string? userId=null
            )
        {
           
            var trip = dbContext.Trips.Include("TravelType").AsQueryable();

            trip = trip.Where(x => x.UserId == userId);

            if(string.IsNullOrWhiteSpace(filterOn)==false)
            {
                if (filterOn.Equals("Destination", StringComparison.OrdinalIgnoreCase))
                {
                    trip = trip.Where(x => x.Destination.Contains(filterQuery));
                }
                if (filterOn.Equals("Source", StringComparison.OrdinalIgnoreCase))
                {
                    trip = trip.Where(x => x.Source.Contains(filterQuery));
                }
            }


            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Destination", StringComparison.OrdinalIgnoreCase))
                {
                    if (isAscending)
                        trip = trip.OrderBy(x => x.Destination);
                    else
                        trip = trip.OrderByDescending(x => x.Destination);
                }
            }

            int totalTrips = await trip.CountAsync();

            int skipResult = (pageNumber - 1) * pageSize;
            var data = await trip.Skip(skipResult).Take(pageSize).ToListAsync();

            return new GetAllReturnDto
            {
                Data = data,
                TotalTrips= totalTrips
            } ;
            
        }

        public async Task<Trip?> GetById(Guid Id)
        {
            var trip = await dbContext.Trips.FirstOrDefaultAsync(x=>x.Id==Id);

            if (trip == null)
            {
                return null;

            }

            return trip;
        }

        public async Task<Trip?> Update(Guid Id, AddTripDto trip)
        {
            var domainTrip = await dbContext.Trips.FirstOrDefaultAsync(x => x.Id == Id);

            if (domainTrip == null)
            {
                return null;
            }

            var DomainTraverse = domainTrip.GetType().GetProperties();
            var DtoTraverse = trip.GetType().GetProperties();

            for (int i = 0; i < DtoTraverse.Length; i++)
            {
                if (DtoTraverse[i].GetValue(trip) != null &&
                    DomainTraverse[i + 1].GetValue(domainTrip) != DtoTraverse[i].GetValue(trip))
                {

                    DomainTraverse[i + 1].SetValue(domainTrip, DtoTraverse[i].GetValue(trip));
                }
            }

            await dbContext.SaveChangesAsync();
            return domainTrip;
        }
    }
}
