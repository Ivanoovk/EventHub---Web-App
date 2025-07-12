

using EventHubApp.Data.Models;

namespace EventHubApp.Data.Repository.Interfaces
{
    public interface IEventRepository
     : IRepository<Event, Guid>, IAsyncRepository<Event, Guid>
    {

    }
}
