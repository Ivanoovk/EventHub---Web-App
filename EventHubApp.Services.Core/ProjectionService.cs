

using EventHubApp.Data.Models;
using EventHubApp.Data.Repository.Interfaces;
using EventHubApp.Services.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EventHubApp.Services.Core
{
    public class ProjectionService : IProjectionService
    {
        private readonly IPlaceEventRepository placeEventRepository;

        public ProjectionService(IPlaceEventRepository placeEventRepository)
        {
            this.placeEventRepository = placeEventRepository;
        }

        public async Task<IEnumerable<string>> GetProjectionShowtimesAsync(string? placeId, string? eventId)
        {
            IEnumerable<string> showtimes = new List<string>();
            if (!String.IsNullOrWhiteSpace(placeId) &&
                !String.IsNullOrWhiteSpace(eventId))
            {
                showtimes = await this.placeEventRepository
                    .GetAllAttached()
                    .Where(pe => pe.PlaceId.ToString().ToLower() == placeId.ToLower() &&
                                 pe.EventId.ToString().ToLower() == eventId.ToLower())
                    .Select(cm => cm.Showtime)
                    .ToArrayAsync();
            }

            return showtimes;
        }

        public async Task<int> GetAvailableTicketsCountAsync(string? placeId, string? eventId, string? showtime)
        {
            int availableTicketsCount = 0;
            if (!String.IsNullOrWhiteSpace(placeId) &&
                !String.IsNullOrWhiteSpace(eventId) &&
                !String.IsNullOrWhiteSpace(showtime))
            {
                PlaceEvent? projection = await this.placeEventRepository
                    .SingleOrDefaultAsync(cm => cm.PlaceId.ToString().ToLower() == placeId.ToLower() &&
                                           cm.EventId.ToString().ToLower() == eventId.ToLower() &&
                                           cm.Showtime == showtime);
                if (projection != null)
                {
                    availableTicketsCount = projection.AvailableTickets;
                }
            }

            return availableTicketsCount;
        }
    }
}
