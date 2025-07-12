

using EventHubApp.Data;
using EventHubApp.Data.Models;
using EventHubApp.Services.Core.Interfaces;
using EventHubApp.Web.ViewModels.Event;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using static EventHubApp.GCommon.ApplicationConstants;

using EventHubApp.Data.Repository.Interfaces;

namespace EventHubApp.Services.Core
{
    public class EventService : IEventService
    {
        private readonly IEventRepository eventRepository;

        public EventService(IEventRepository eventRepository)
        {
            this.eventRepository = eventRepository;
        }

        public async Task<IEnumerable<AllEventsIndexViewModel>> GetAllEventsAsync()
        {
            IEnumerable<AllEventsIndexViewModel> allEvents = await this.eventRepository
                .GetAllAttached()
                .AsNoTracking()
                .Select(m => new AllEventsIndexViewModel()
                {
                    Id = m.Id.ToString(),
                    Title = m.Title,
                    Type = m.Type,
                    ReleaseDate = m.ReleaseDate.ToString(AppDateFormat),
                    Sponsor = m.Sponsor,
                    ImageUrl = m.ImageUrl,
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


        public async Task AddEventAsync(EventFormInputModel inputModel)
        {
            Event newEvent = new Event()
            {
                Title = inputModel.Title,
                Type = inputModel.Type,
                Sponsor = inputModel.Sponsor,
                Description = inputModel.Description,
                Duration = inputModel.Duration,
                ImageUrl = inputModel.ImageUrl,
                ReleaseDate = DateOnly
                    .ParseExact(inputModel.ReleaseDate, AppDateFormat,
                        CultureInfo.InvariantCulture, DateTimeStyles.None),
            };

            await this.eventRepository.AddAsync(newEvent);
        }










    }
}
