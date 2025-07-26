

using EventHubApp.Data.Models;
using EventHubApp.Data.Repository.Interfaces;
using EventHubApp.Services.Core.Interfaces;
using EventHubApp.Web.ViewModels.Ticket;
using Microsoft.EntityFrameworkCore;
using static EventHubApp.GCommon.ApplicationConstants;

namespace EventHubApp.Services.Core
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository ticketRepository;
        private readonly IPlaceEventRepository placeEventRepository;

        public TicketService(ITicketRepository ticketRepository,
            IPlaceEventRepository placeEventRepository)
        {
            this.ticketRepository = ticketRepository;
            this.placeEventRepository = placeEventRepository;
        }

        public async Task<IEnumerable<TicketIndexViewModel>> GetUserTicketsAsync(string? userId)
        {
            IEnumerable<TicketIndexViewModel> userTickets = new List<TicketIndexViewModel>();
            if (!String.IsNullOrWhiteSpace(userId))
            {
                userTickets = await this.ticketRepository
                    .GetAllAttached()
                    .Where(t => t.UserId.ToLower() == userId.ToLower())
                    .Select(t => new TicketIndexViewModel()
                    {
                        EventTitle = t.PlaceEventProjection.Event.Title,
                        EventImageUrl = t.PlaceEventProjection.Event.ImageUrl ?? $"/images/{NoImageUrl}",
                        PlaceName = t.PlaceEventProjection.Place.Name,
                        Showtime = t.PlaceEventProjection.Showtime,
                        TicketCount = t.Quantity,
                        TicketPrice = t.Price.ToString("F2"),
                        TotalPrice = (t.Quantity * t.Price).ToString("F2"),
                    })
                    .ToArrayAsync();
            }

            return userTickets;
        }

        public async Task<bool> AddTicketAsync(string? placeId, string? eventId, int quantity, string? showtime, string? userId)
        {
            bool result = false;
            if (!String.IsNullOrWhiteSpace(placeId) &&
                !String.IsNullOrWhiteSpace(eventId) &&
                !String.IsNullOrWhiteSpace(showtime) &&
                !String.IsNullOrWhiteSpace(userId) &&
                quantity > 0)
            {
                PlaceEvent? projection = await this.placeEventRepository
                    .SingleOrDefaultAsync(pe => pe.PlaceId.ToString().ToLower() == placeId.ToLower() &&
                                                pe.EventId.ToString().ToLower() == eventId.ToLower() &&
                                                pe.Showtime == showtime);
                if (projection != null &&
                    projection.AvailableTickets >= quantity)
                {

                    Ticket newTicket = new Ticket()
                    {
                        Quantity = quantity,
                        PlaceEventProjection = projection,
                        UserId = userId,
                        Price = 5,
                    };

                    await this.ticketRepository.AddAsync(newTicket);

                    projection.AvailableTickets -= quantity;
                    result = await this.placeEventRepository.UpdateAsync(projection);
                }
            }

            return result;
        }
    }
}
