

using EventHubApp.Data.Models;
using EventHubApp.Data.Repository.Interfaces;
using EventHubApp.Services.Core.Admin.Interfaces;
using EventHubApp.Web.ViewModels.Admin.EventManagement;
using Microsoft.EntityFrameworkCore;

using static EventHubApp.GCommon.ApplicationConstants;

namespace EventHubApp.Services.Core.Admin
{
    public class EventManagementService : EventService, IEventManagementService 
    {
        private readonly IEventRepository eventRepository;

        public EventManagementService(IEventRepository eventRepository)
            : base(eventRepository)
        {
            this.eventRepository = eventRepository;
        }

        public async Task<IEnumerable<EventManagementIndexViewModel>> GetEventManagementBoardDataAsync()
        {
            IEnumerable<EventManagementIndexViewModel> allEvents = await this.eventRepository
                .GetAllAttached()
                .IgnoreQueryFilters()
                .Select(e => new EventManagementIndexViewModel()
                {
                    Id = e.Id.ToString(),
                    Title = e.Title,
                    Type = e.Type,
                    Sponsor = e.Sponsor,
                    Duration = e.Duration,
                    IsDeleted = e.IsDeleted,
                    ReleaseDate = e.ReleaseDate.ToString(AppDateFormat)
                })
                .ToArrayAsync();

            return allEvents;
        }

        public async Task<Tuple<bool, bool>> DeleteOrRestoreEventAsync(string? id)
        {
            bool result = false;
            bool isRestored = false;
            if (!String.IsNullOrWhiteSpace(id))
            {
                Event? eventt = await this.eventRepository
                    .GetAllAttached()
                    .IgnoreQueryFilters()
                    .SingleOrDefaultAsync(e => e.Id.ToString().ToLower() == id.ToLower());
                if (eventt != null)
                {
                    if (eventt.IsDeleted)
                    {
                        isRestored = true;
                    }

                    eventt.IsDeleted = !eventt.IsDeleted;

                    result = await this.eventRepository
                        .UpdateAsync(eventt);
                }
            }

            return new Tuple<bool, bool>(result, isRestored);
        }
    }
}
