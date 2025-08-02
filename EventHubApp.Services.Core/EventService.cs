

using System.Globalization;

using Microsoft.EntityFrameworkCore;

using EventHubApp.Data.Models;
using EventHubApp.Data.Repository.Interfaces;
using EventHubApp.Services.Core.Interfaces;
using EventHubApp.Web.ViewModels.Event;

using static EventHubApp.GCommon.ApplicationConstants;


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


        public async Task<EventDetailsViewModel?> GetEventDetailsByIdAsync(string? id)
        {
            EventDetailsViewModel? eventDetails = null;

            bool isIdValidGuid = Guid.TryParse(id, out Guid eventId);
            if (isIdValidGuid)
            {
                eventDetails = await this.eventRepository
                    .GetAllAttached()
                    .AsNoTracking()
                    .Where(e => e.Id == eventId)
                    .Select(e => new EventDetailsViewModel()
                    {
                        Id = e.Id.ToString(),
                        Description = e.Description,
                        Sponsor = e.Sponsor,
                        Duration = e.Duration,
                        Type = e.Type,
                        ImageUrl = e.ImageUrl ?? $"/images/{NoImageUrl}",
                        ReleaseDate = e.ReleaseDate.ToString(AppDateFormat),
                        Title = e.Title
                    })
                    .SingleOrDefaultAsync();
            }

            return eventDetails;
        }

        public async Task<EventFormInputModel?> GetEditableEventByIdAsync(string? id)
        {
            EventFormInputModel? editableEvent = null;

            bool isIdValidGuid = Guid.TryParse(id, out Guid eventId);
            if (isIdValidGuid)
            {
                editableEvent = await this.eventRepository
                    .GetAllAttached()
                    .AsNoTracking()
                    .Where(e => e.Id == eventId)
                    .Select(e => new EventFormInputModel()
                    {
                        Description = e.Description,
                        Sponsor = e.Sponsor,
                        Duration = e.Duration,
                        Type = e.Type,
                        ImageUrl = e.ImageUrl ?? $"/images/{NoImageUrl}",
                        ReleaseDate = e.ReleaseDate.ToString(AppDateFormat),
                        Title = e.Title
                    })
                    .SingleOrDefaultAsync();
            }

            return editableEvent;
        }

        public async Task<bool> EditEventAsync(EventFormInputModel inputModel)
        {
            bool result = false;

            Event? editableEvent = await this.FindEventByStringId(inputModel.Id);
            if (editableEvent == null)
            {
                return false;
            }

            DateOnly movieReleaseDate = DateOnly
                .ParseExact(inputModel.ReleaseDate, AppDateFormat,
                    CultureInfo.InvariantCulture, DateTimeStyles.None);
            editableEvent.Title = inputModel.Title;
            editableEvent.Description = inputModel.Description;
            editableEvent.Sponsor = inputModel.Sponsor;
            editableEvent.Duration = inputModel.Duration;
            editableEvent.Type = inputModel.Type;
            editableEvent.ImageUrl = inputModel.ImageUrl ?? $"/images/{NoImageUrl}";
            editableEvent.ReleaseDate = movieReleaseDate;

            result = await this.eventRepository.UpdateAsync(editableEvent);

            return result;
        }

        public async Task<DeleteEventViewModel?> GetEventDeleteDetailsByIdAsync(string? id)
        {
            DeleteEventViewModel? deleteEventViewModel = null;

            Event? eventToBeDeleted = await this.FindEventByStringId(id);
            if (eventToBeDeleted != null)
            {
                deleteEventViewModel = new DeleteEventViewModel()
                {
                    Id = eventToBeDeleted.Id.ToString(),
                    Title = eventToBeDeleted.Title,
                    ImageUrl = eventToBeDeleted.ImageUrl ?? $"/images/{NoImageUrl}",
                };
            }

            return deleteEventViewModel;
        }

        public async Task<bool> SoftDeleteEventAsync(string? id)
        {
            bool result = false;
            Event? eventToDelete = await this.FindEventByStringId(id);
            if (eventToDelete == null)
            {
                return false;
            }

            // Soft Delete <=> Edit of IsDeleted property
            result = await this.eventRepository.DeleteAsync(eventToDelete);

            return result;
        }

        public async Task<bool> DeleteEventAsync(string? id)
        {
            Event? eventToDelete = await this.FindEventByStringId(id);
            if (eventToDelete == null)
            {
                return false;
            }

            // TODO: To be investigated when relations to Movie entity are introduced
            await this.eventRepository
                .HardDeleteAsync(eventToDelete);

            return true;
        }

        // TODO: Implement as generic method in BaseService
        private async Task<Event?> FindEventByStringId(string? id)
        {
            Event? evennt = null;

            if (!string.IsNullOrWhiteSpace(id))
            {
                bool isGuidValid = Guid.TryParse(id, out Guid eventGuid);
                if (isGuidValid)
                {
                    evennt = await this.eventRepository
                        .GetByIdAsync(eventGuid);
                }
            }

            return evennt;
        }







    }
}
