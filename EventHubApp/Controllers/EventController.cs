using EventHubApp.Services.Core.Interfaces;
using EventHubApp.Web.ViewModels.Event;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    }
}
