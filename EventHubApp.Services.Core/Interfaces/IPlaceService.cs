

using EventHubApp.Web.ViewModels.Place;

namespace EventHubApp.Services.Core.Interfaces
{
    public interface IPlaceService
    {
        Task<IEnumerable<UsersPlaceIndexViewModel>> GetAllPlacesUserViewAsync();

        Task<PlaceProgramViewModel?> GetPlaceProgramAsync(string? placeId);

        Task<PlaceDetailsViewModel?> GetPlaceDetailsAsync(string? placeId);
    }
}
