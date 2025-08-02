

using EventHubApp.Services.Core.Interfaces;
using EventHubApp.Web.ViewModels.Admin.PlaceManagement;

namespace EventHubApp.Services.Core.Admin.Interfaces
{
    public interface IPlaceManagementService : IPlaceService
    {
        Task<IEnumerable<PlaceManagementIndexViewModel>> GetPlaceManagementBoardDataAsync();

        Task<bool> AddPlaceAsync(PlaceManagementAddFormModel? inputModel);

        Task<PlaceManagementEditFormModel?> GetPlaceEditFormModelAsync(string? id);

        Task<bool> EditPlaceAsync(PlaceManagementEditFormModel? inputModel);

        Task<Tuple<bool, bool>> DeleteOrRestorePlaceAsync(string? id);
    }
}
