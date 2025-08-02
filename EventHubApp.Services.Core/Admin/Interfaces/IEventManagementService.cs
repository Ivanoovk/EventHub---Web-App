

using EventHubApp.Services.Core.Interfaces;
using EventHubApp.Web.ViewModels.Admin.EventManagement;

namespace EventHubApp.Services.Core.Admin.Interfaces
{
    public interface IEventManagementService : IEventService
    {
        Task<IEnumerable<EventManagementIndexViewModel>> GetEventManagementBoardDataAsync();

        Task<Tuple<bool, bool>> DeleteOrRestoreEventAsync(string? id);
    }
}
