using EventHubApp.Services.Core.Admin.Interfaces;
using EventHubApp.Web.ViewModels.Admin.PlaceManagement;
using Microsoft.AspNetCore.Mvc;

using static EventHubApp.GCommon.ApplicationConstants;

namespace EventHubApp.Web.Areas.Admin.Controllers
{

    public class PlaceManagementController : BaseAdminController
    {
        private readonly IPlaceManagementService placeManagementService;
        private readonly IUserService userService;

        public PlaceManagementController(IPlaceManagementService placeManagementService,
            IUserService userService)
        {
            this.placeManagementService = placeManagementService;
            this.userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Manage()
        {
            IEnumerable<PlaceManagementIndexViewModel> allPlaces = await this.placeManagementService
                .GetPlaceManagementBoardDataAsync();

            return View(allPlaces);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            PlaceManagementAddFormModel viewModel = new PlaceManagementAddFormModel()
            {
                AppManagerEmails = await this.userService.GetManagerEmailsAsync(),
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PlaceManagementAddFormModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            try
            {
                bool success = await this.placeManagementService
                    .AddPlaceAsync(inputModel);
                if (!success)
                {
                    TempData[ErrorMessageKey] = "Error occurred while adding the place! Ensure to select a valid manager!";
                }
                else
                {
                    TempData[SuccessMessageKey] = "Place created successfully!";
                }

                return this.RedirectToAction(nameof(Manage));
            }
            catch (Exception e)
            {
                TempData[ErrorMessageKey] =
                    "Unexpected error occurred while adding the place! Please contact developer team!";

                return this.RedirectToAction(nameof(Manage));
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {
            PlaceManagementEditFormModel? editFormModel = await this.placeManagementService
                .GetPlaceEditFormModelAsync(id);
            if (editFormModel == null)
            {
                TempData[ErrorMessageKey] = "Selected Place does not exist!";

                return this.RedirectToAction(nameof(Manage));
            }

            editFormModel.AppManagerEmails = await this.userService
                .GetManagerEmailsAsync();

            return this.View(editFormModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PlaceManagementEditFormModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            try
            {
                bool success = await this.placeManagementService
                    .EditPlaceAsync(inputModel);
                if (!success)
                {
                    TempData[ErrorMessageKey] = "Error occurred while updating the place! Ensure to select a valid manager!";
                }
                else
                {
                    TempData[SuccessMessageKey] = "Place updated successfully!";
                }

                return this.RedirectToAction(nameof(Manage));
            }
            catch (Exception e)
            {
                TempData[ErrorMessageKey] =
                    "Unexpected error occurred while editing the place! Please contact developer team!";

                return this.RedirectToAction(nameof(Manage));
            }
        }

        [HttpGet]
        public async Task<IActionResult> ToggleDelete(string? id)
        {
            Tuple<bool, bool> opResult = await this.placeManagementService
                .DeleteOrRestorePlaceAsync(id);
            bool success = opResult.Item1;
            bool isRestored = opResult.Item2;

            if (!success)
            {
                TempData[ErrorMessageKey] = "Place could not be found and updated!";
            }
            else
            {
                string operation = isRestored ? "restored" : "deleted";

                TempData[SuccessMessageKey] = $"Place {operation} successfully!";
            }

            return this.RedirectToAction(nameof(Manage));
        }
    }
}
