using testingUK.Model;
using testingUK.Model.Dto;

namespace testingUK.Repositories
{
    public interface ITripRepository
    {
        Task<GetAllReturnDto> GetAll(string? filterOn=null , string? filterQuery = null
                                , string? sortBy = null, bool isAscending = true,
                                int pageNumber=1 , int pageSize=20 , string? userId = null);
        Task<Trip?> GetById(Guid Id);
        Task<Trip> Create(Trip trip);

        Task<Trip?> Update(Guid Id , AddTripDto trip);

        Task<Trip?> Delete(Guid Id);

    }
}
