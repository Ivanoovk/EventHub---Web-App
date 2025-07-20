using EventHubApp.Services.Core.Interfaces;
using EventHubApp.Web.ViewModels.Event;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using static EventHubApp.Web.ViewModels.ValidationMessages.Event;

namespace EventHubApp.Web.Controllers
{
    public class EventController : BaseController
    {
        private readonly IEventService eventService;
        private readonly IWatchlistService watchlistService;

        
        public EventController(IEventService eventService, IWatchlistService watchlistService)
        {
            this.eventService = eventService;
            this.watchlistService = watchlistService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            IEnumerable<AllEventsIndexViewModel> allEvents = await this.eventService
                .GetAllEventsAsync();
            if (this.IsUserAuthenticated())
            {
                foreach (AllEventsIndexViewModel eventIndexVM in allEvents)
                {
                    eventIndexVM.IsAddedToUserWatchlist = await this.watchlistService
                        .IsEventAddedToWatchlist(eventIndexVM.Id, this.GetUserId());
                }
            }

            return View(allEvents);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(EventFormInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            try
            {
                await this.eventService.AddEventAsync(inputModel);

                return this.RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);

                this.ModelState.AddModelError(string.Empty, ServiceCreateError);
                return this.View(inputModel);
            }
        }



        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(string? id)
        {
            try
            {
                EventDetailsViewModel? eventDetails = await this.eventService
                    .GetEventDetailsByIdAsync(id);
                if (eventDetails == null)
                {

                    return this.RedirectToAction(nameof(Index));
                }

                return this.View(eventDetails);
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);

                return this.RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> DetailsPartial(string? id)
        {
            // TODO: Refactor the functionality to use WebAPI
            try
            {
                EventDetailsViewModel? eventDetails = await this.eventService
                    .GetEventDetailsByIdAsync(id);
                if (eventDetails == null)
                {

                    return this.RedirectToAction(nameof(Index));
                }

                return this.View("_EventDetailsPartial", eventDetails);
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);

                return this.RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {
            try
            {
                EventFormInputModel? editableEvent = await this.eventService
                    .GetEditableEventByIdAsync(id);
                if (editableEvent == null)
                {

                    return this.RedirectToAction(nameof(Index));
                }

                return this.View(editableEvent);
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);

                return this.RedirectToAction(nameof(Index));
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
                bool editSuccess = await this.eventService.EditEventAsync(inputModel);
                if (!editSuccess)
                {

                    return this.RedirectToAction(nameof(Index));
                }

                return this.RedirectToAction(nameof(Details), new { id = inputModel.Id });
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);

                return this.RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string? id)
        {
            try
            {
                DeleteEventViewModel? eventToBeDeleted = await this.eventService
                    .GetEventDeleteDetailsByIdAsync(id);
                if (eventToBeDeleted == null)
                {

                    return this.RedirectToAction(nameof(Index));
                }

                return this.View(eventToBeDeleted);
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);

                return this.RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteEventViewModel inputModel)
        {
            try
            {
                if (!this.ModelState.IsValid)
                {
                    // TODO: Implement JS notifications
                    return this.RedirectToAction(nameof(Index));
                }

                bool deleteResult = await this.eventService
                    .SoftDeleteEventAsync(inputModel.Id);
                if (deleteResult == false)
                {

                    return this.RedirectToAction(nameof(Index));
                }


                return this.RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);

                return this.RedirectToAction(nameof(Index));
            }
        }







    }
}
