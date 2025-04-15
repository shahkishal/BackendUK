using testingUK.Model;
using testingUK.Model.Dto;

namespace testingUK.Repositories
{
    public interface ITravelTypeRepository
    {
        Task<List<TravelType>> GetAll();
        Task<TravelType?> GetById(Guid Id);
        Task<TravelType> Create(TravelType travelType);
        Task<TravelType?> Update(Guid Id , AddTravelTypeDto travelTypeDto);
        Task<TravelType?> Delete(Guid Id);


    }
}
