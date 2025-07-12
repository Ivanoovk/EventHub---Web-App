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
                // TODO: Implement it with the ILogger
                Console.WriteLine(e.Message);

                this.ModelState.AddModelError(string.Empty, ServiceCreateError);
                return this.View(inputModel);
            }
        }
    }
}
