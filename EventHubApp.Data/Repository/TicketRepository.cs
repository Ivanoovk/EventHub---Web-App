

using EventHubApp.Data.Models;
using EventHubApp.Data.Repository.Interfaces;

namespace EventHubApp.Data.Repository
{
    public class TicketRepository : BaseRepository<Ticket, Guid>, ITicketRepository
    {
        public TicketRepository(EventHubAppDbContext dbContext)
            : base(dbContext)
        {

        }
    }
}
