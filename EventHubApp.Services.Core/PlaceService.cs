

using EventHubApp.Data.Models;
using EventHubApp.Data.Repository.Interfaces;
using EventHubApp.Services.Core.Interfaces;
using EventHubApp.Web.ViewModels.Place;
using Microsoft.EntityFrameworkCore;

using static EventHubApp.GCommon.ApplicationConstants;

namespace EventHubApp.Services.Core
{
    public class PlaceService : IPlaceService
    {
        private readonly IPlaceRepository placeRepository;

        public PlaceService(IPlaceRepository placeRepository)
        {
            this.placeRepository = placeRepository;
        }

        public async Task<IEnumerable<UsersPlaceIndexViewModel>> GetAllPlacesUserViewAsync()
        {
            IEnumerable<UsersPlaceIndexViewModel> allPlacesUsersView = await this.placeRepository
                .GetAllAttached()
                .Select(c => new UsersPlaceIndexViewModel()
                {
                    Id = c.Id.ToString(),
                    Name = c.Name,
                    Location = c.Location,
                })
                .ToArrayAsync();

            return allPlacesUsersView;
        }

        public async Task<PlaceProgramViewModel?> GetPlaceProgramAsync(string? placeId)
        {
            PlaceProgramViewModel? placeProgram = null;
            if (!String.IsNullOrWhiteSpace(placeId))
            {
                Place? place = await this.placeRepository
                    .GetAllAttached()
                    .Include(p => p.PlaceEvents)
                    .ThenInclude(pe => pe.Event)
                    .SingleOrDefaultAsync(p => p.Id.ToString().ToLower() == placeId.ToLower());
                if (place != null)
                {
                    placeProgram = new PlaceProgramViewModel()
                    {
                        PlaceId = place.Id.ToString(),
                        PlaceName = place.Name,
                        PlaceData = place.Name + " - " + place.Location,
                        Events = place.PlaceEvents
                            .Select(pe => pe.Event)
                            .DistinctBy(e => e.Id)
                            .Select(e => new PlaceProgramEventViewModel()
                            {
                                Id = e.Id.ToString(),
                                Title = e.Title,
                                ImageUrl = e.ImageUrl ?? $"/images/{NoImageUrl}",
                                Sponsor = e.Sponsor
                            })
                            .ToArray(),
                    };
                }
            }

            return placeProgram;
        }

        public async Task<PlaceDetailsViewModel?> GetPlaceDetailsAsync(string? placeId)
        {
            PlaceDetailsViewModel? placeDetails = null;
            if (!String.IsNullOrWhiteSpace(placeId))
            {
                Place? place = await this.placeRepository
                    .GetAllAttached()
                    .Include(p => p.PlaceEvents)
                    .ThenInclude(pe => pe.Event)
                    .SingleOrDefaultAsync(c => c.Id.ToString().ToLower() == placeId.ToLower());
                if (place != null)
                {
                    placeDetails = new PlaceDetailsViewModel()
                    {
                        Name = place.Name,
                        Location = place.Location,
                        Events = place.PlaceEvents
                            .Select(pe => pe.Event)
                            .DistinctBy(e => e.Id)
                            .Select(e => new PlaceDetailsEventViewModel()
                            {
                                Title = e.Title,
                                Duration = e.Duration.ToString(),
                            })
                            .ToArray(),
                    };
                }
            }

            return placeDetails;
        }

    }
}
