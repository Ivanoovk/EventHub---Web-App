using EventHubApp.Data.Models;
using EventHubApp.Data.Repository.Interfaces;


namespace EventHubApp.Data.Repository
{
    public class ManagerRepository : BaseRepository<Manager, Guid>, IManagerRepository
    {
        public ManagerRepository(EventHubAppDbContext dbContext)
            : base(dbContext)
        {

        }
    }
}
