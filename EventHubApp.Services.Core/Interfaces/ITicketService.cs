

using EventHubApp.Web.ViewModels.Ticket;

namespace EventHubApp.Services.Core.Interfaces
{
    public interface ITicketService
    {
        Task<IEnumerable<TicketIndexViewModel>> GetUserTicketsAsync(string? userId);
        Task<bool> AddTicketAsync(string? placeId, string? eventId, int quantity, string? showtime, string? userId);
    }
}
