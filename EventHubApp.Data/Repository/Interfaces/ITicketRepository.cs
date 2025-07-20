

using EventHubApp.Data.Models;

namespace EventHubApp.Data.Repository.Interfaces
{
    public interface ITicketRepository
        : IRepository<Ticket, Guid>, IAsyncRepository<Ticket, Guid>
    {

    }
}
