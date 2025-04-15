using Microsoft.EntityFrameworkCore;
using testingUK.Data;
using testingUK.Model;
using testingUK.Model.Dto;

namespace testingUK.Repositories
{
    public class TravelTypeRepository : ITravelTypeRepository
    {
        private readonly TripDbContext dbContext;

        public TravelTypeRepository(TripDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<TravelType> Create(TravelType travelType)
        {
            await dbContext.TravelType.AddAsync(travelType);
            await dbContext.SaveChangesAsync();
            return travelType;
        }

        public async Task<TravelType?> Delete(Guid Id)
        {
            var domainTravelType = await dbContext.TravelType
                .FirstOrDefaultAsync(x => x.Id == Id);

            if (domainTravelType == null) return null;

            dbContext.TravelType.Remove(domainTravelType);
            await dbContext.SaveChangesAsync();

            return domainTravelType;

        }

        public async Task<List<TravelType>> GetAll()
        {
            return await dbContext.TravelType.ToListAsync();
        }

        public async Task<TravelType?> GetById(Guid Id)
        {
            return await dbContext.TravelType.FirstOrDefaultAsync(x=>x.Id==Id);

        }

        public async Task<TravelType?> Update(Guid Id, AddTravelTypeDto travelTypeDto)
        {
            var domainTravelType = await dbContext.TravelType.FirstOrDefaultAsync(x => x.Id == Id);

            if (domainTravelType == null) return null;

            var traverseDomain = domainTravelType.GetType().GetProperties();
            var traverseDto = travelTypeDto.GetType().GetProperties();

            for(int i=0; i<traverseDto.Length; i++)
            {
                if (traverseDto[i].GetValue(travelTypeDto) ==null || 
                    traverseDomain[i + 1].GetValue(domainTravelType) 
                    != traverseDto[i].GetValue(travelTypeDto))
                {
                    traverseDomain[i+1].SetValue(domainTravelType , traverseDto[i].GetValue(travelTypeDto));
                }
            }


            await dbContext.SaveChangesAsync();
            return domainTravelType;


        }
    }
}
