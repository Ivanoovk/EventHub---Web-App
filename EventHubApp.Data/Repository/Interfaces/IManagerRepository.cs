

using EventHubApp.Data.Models;

namespace EventHubApp.Data.Repository.Interfaces
{
    public interface IManagerRepository
    : IRepository<Manager, Guid>, IAsyncRepository<Manager, Guid>
    {

    }
}
