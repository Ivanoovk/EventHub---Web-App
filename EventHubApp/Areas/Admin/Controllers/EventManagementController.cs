using EventHubApp.Services.Core.Admin.Interfaces;
using EventHubApp.Web.ViewModels.Event;
using EventHubApp.Web.ViewModels.Admin.EventManagement;
using Microsoft.AspNetCore.Mvc;


using static EventHubApp.GCommon.ApplicationConstants;

namespace EventHubApp.Web.Areas.Admin.Controllers
{

    public class EventManagementController : BaseAdminController
    {
        private readonly IEventManagementService eventManagementService;
        private readonly ILogger<EventManagementController> logger;

        public EventManagementController(IEventManagementService eventManagementService,
            ILogger<EventManagementController> logger)
        {
            this.eventManagementService = eventManagementService;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Manage()
        {
            IEnumerable<EventManagementIndexViewModel> allEvents = await this.eventManagementService
                .GetEventManagementBoardDataAsync();

            return View(allEvents);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EventFormInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            try
            {
                await this.eventManagementService.AddEventAsync(inputModel);
                TempData[SuccessMessageKey] = "Event added successfully!";

                return this.RedirectToAction(nameof(Manage));
            }
            catch (Exception e)
            {
                this.logger.LogCritical(e.Message);
                TempData[ErrorMessageKey] = "Fatal error occurred while adding your event! Please try again later!";

                return this.RedirectToAction(nameof(Manage));
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {
            try
            {
                EventFormInputModel? editableEvent = await this.eventManagementService
                    .GetEditableEventByIdAsync(id);
                if (editableEvent == null)
                {
                    return this.NotFound();
                }

                return this.View(editableEvent);
            }
            catch (Exception e)
            {
                this.logger.LogCritical(e.Message);
                TempData[ErrorMessageKey] = "Fatal error occurred while updating the event! Please try again later!";

                return this.RedirectToAction(nameof(Manage));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EventFormInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            try
            {
                bool editSuccess = await this.eventManagementService
                    .EditEventAsync(inputModel);
                if (!editSuccess)
                {
                    TempData[ErrorMessageKey] = "Selected Event does not exist!";
                }
                else
                {
                    TempData[SuccessMessageKey] = "Event updated successfully!";
                }

                return this.RedirectToAction(nameof(Manage));
            }
            catch (Exception e)
            {
                this.logger.LogCritical(e.Message);
                TempData[ErrorMessageKey] = "Fatal error occurred while updating the Event! Please try again later!";

                return this.RedirectToAction(nameof(Manage));
            }
        }

        [HttpGet]
        public async Task<IActionResult> ToggleDelete(string? id)
        {
            Tuple<bool, bool> opResult = await this.eventManagementService
                .DeleteOrRestoreEventAsync(id);
            bool success = opResult.Item1;
            bool isRestored = opResult.Item2;

            if (!success)
            {
                TempData[ErrorMessageKey] = "Event could not be found and updated!";
            }
            else
            {
                string operation = isRestored ? "restored" : "deleted";

                TempData[SuccessMessageKey] = $"Event {operation} successfully!";
            }

            return this.RedirectToAction(nameof(Manage));
        }
    }
}
