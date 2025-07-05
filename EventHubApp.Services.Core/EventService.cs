

using EventHubApp.Data;
using EventHubApp.Services.Core.Interfaces;
using EventHubApp.Web.ViewModels.Event;
using Microsoft.EntityFrameworkCore;
using static EventHubApp.GCommon.ApplicationConstants;

namespace EventHubApp.Services.Core
{
    public class EventService : IEventService
    {
        private readonly EventHubAppDbContext dbContext;

        public EventService(EventHubAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<AllEventsIndexViewModel>> GetAllEventsAsync()
        {
            IEnumerable<AllEventsIndexViewModel> allEvents = await this.dbContext
                .Events
                .AsNoTracking()
                .Select(e => new AllEventsIndexViewModel()
                {
                    Id = e.Id.ToString(),
                    Title = e.Title,
                    Type = e.Type,
                    ReleaseDate = e.ReleaseDate.ToString(AppDateFormat),
                    Sponsor = e.Sponsor,
                    ImageUrl = e.ImageUrl,
                })
                .ToListAsync();
            foreach (AllEventsIndexViewModel eventt in allEvents)
            {
                if (String.IsNullOrEmpty(eventt.ImageUrl))
                {
                    eventt.ImageUrl = $"/images/{NoImageUrl}";
                }
            }

            return allEvents;
        }
    }
}
