

namespace EventHubApp.Services.Core.Interfaces
{
    public interface IProjectionService
    {
        Task<IEnumerable<string>> GetProjectionShowtimesAsync(string? placeId, string? eventId);

        Task<int> GetAvailableTicketsCountAsync(string? placeId, string? eventId, string? showtime);
    }
}
