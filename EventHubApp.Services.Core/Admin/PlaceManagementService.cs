

using EventHubApp.Data.Models;
using EventHubApp.Data.Repository.Interfaces;
using EventHubApp.Services.Core.Admin.Interfaces;
using EventHubApp.Web.ViewModels.Admin.PlaceManagement;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EventHubApp.Services.Core.Admin
{
    public class PlaceManagementService : PlaceService, IPlaceManagementService
    {
        private readonly IPlaceRepository placeRepository;
        private readonly IManagerRepository managerRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public PlaceManagementService(IPlaceRepository placeRepository,
            IManagerRepository managerRepository, UserManager<ApplicationUser> userManager) : base(placeRepository)
        {
            this.placeRepository = placeRepository;
            this.managerRepository = managerRepository;
            this.userManager = userManager;
        }

        public async Task<IEnumerable<PlaceManagementIndexViewModel>> GetPlaceManagementBoardDataAsync()
        {
            IEnumerable<PlaceManagementIndexViewModel> allPlaces = await this.placeRepository
                .GetAllAttached()
                .IgnoreQueryFilters()
                .Select(c => new PlaceManagementIndexViewModel()
                {
                    Id = c.Id.ToString(),
                    Name = c.Name,
                    Location = c.Location,
                    IsDeleted = c.IsDeleted,
                    ManagerName = c.Manager != null ?
                        c.Manager.User.UserName : null,
                })
                .ToArrayAsync();

            return allPlaces;
        }

        public async Task<bool> AddPlaceAsync(PlaceManagementAddFormModel? inputModel)
        {
            bool result = false;
            if (inputModel != null)
            {
                ApplicationUser? managerUser = await this.userManager
                    .FindByNameAsync(inputModel.ManagerEmail);
                if (managerUser != null)
                {
                    Manager? manager = await this.managerRepository
                        .GetAllAttached()
                        .SingleOrDefaultAsync(m => m.UserId.ToLower() == managerUser.Id.ToLower());
                    if (manager != null)
                    {
                        Place newPlace = new Place()
                        {
                            Name = inputModel.Name,
                            Location = inputModel.Location,
                            Manager = manager,
                        };

                        await this.placeRepository.AddAsync(newPlace);

                        result = true;
                    }
                }
            }

            return result;
        }

        public async Task<PlaceManagementEditFormModel?> GetPlaceEditFormModelAsync(string? id)
        {
            PlaceManagementEditFormModel? formModel = null;
            if (!String.IsNullOrWhiteSpace(id))
            {
                Place? placeToEdit = await this.placeRepository
                    .GetAllAttached()
                    .Include(c => c.Manager)
                    .ThenInclude(m => m.User)
                    .IgnoreQueryFilters()
                    .SingleOrDefaultAsync(c => c.Id.ToString().ToLower() == id.ToLower());
                if (placeToEdit != null)
                {
                    formModel = new PlaceManagementEditFormModel()
                    {
                        Id = placeToEdit.Id.ToString(),
                        Name = placeToEdit.Name,
                        Location = placeToEdit.Location,
                        ManagerEmail = placeToEdit.Manager != null ?
                            placeToEdit.Manager.User.Email ?? string.Empty : string.Empty,
                    };
                }
            }

            return formModel;
        }

        public async Task<bool> EditPlaceAsync(PlaceManagementEditFormModel? inputModel)
        {
            bool result = false;
            if (inputModel != null)
            {
                ApplicationUser? managerUser = await this.userManager
                    .FindByNameAsync(inputModel.ManagerEmail);
                if (managerUser != null)
                {
                    Manager? manager = await this.managerRepository
                        .GetAllAttached()
                        .SingleOrDefaultAsync(m => m.UserId.ToLower() == managerUser.Id.ToLower());
                    Place? placeToEdit = await this.placeRepository
                        .SingleOrDefaultAsync(c => c.Id.ToString().ToLower() == inputModel.Id.ToLower());
                    if (manager != null &&
                        placeToEdit != null)
                    {
                        placeToEdit.Name = inputModel.Name;
                        placeToEdit.Location = inputModel.Location;
                        placeToEdit.Manager = manager;

                        result = await this.placeRepository
                            .UpdateAsync(placeToEdit);
                    }
                }
            }

            return result;
        }

        public async Task<Tuple<bool, bool>> DeleteOrRestorePlaceAsync(string? id)
        {
            bool result = false;
            bool isRestored = false;
            if (!String.IsNullOrWhiteSpace(id))
            {
                Place? place = await this.placeRepository
                    .GetAllAttached()
                    .IgnoreQueryFilters()
                    .SingleOrDefaultAsync(c => c.Id.ToString().ToLower() == id.ToLower());
                if (place != null)
                {
                    if (place.IsDeleted)
                    {
                        isRestored = true;
                    }

                    place.IsDeleted = !place.IsDeleted;

                    result = await this.placeRepository
                        .UpdateAsync(place);
                }
            }

            return new Tuple<bool, bool>(result, isRestored);
        }
    }
}
