using testingUK.Model;

namespace testingUK.Repositories
{
    public interface IBrowseRepository
    {
        public Task<List<Trip>> getAllPublic();
    }
}
