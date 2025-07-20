

using EventHubApp.Web.ViewModels.Event;

namespace EventHubApp.Services.Core.Interfaces
{
    public interface IEventService
    {
        Task<IEnumerable<AllEventsIndexViewModel>> GetAllEventsAsync();

        Task AddEventAsync(EventFormInputModel inputModel);

        Task<EventDetailsViewModel?> GetEventDetailsByIdAsync(string? id);

        Task<EventFormInputModel?> GetEditableEventByIdAsync(string? id);

        Task<bool> EditEventAsync(EventFormInputModel inputModel);

        Task<DeleteEventViewModel?> GetEventDeleteDetailsByIdAsync(string? id);

        Task<bool> SoftDeleteEventAsync(string? id);

        Task<bool> DeleteEventAsync(string? id);
    }
}
