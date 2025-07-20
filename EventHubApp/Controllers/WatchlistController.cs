using EventHubApp.Services.Core.Interfaces;
using EventHubApp.Web.ViewModels.Watchlist;
using Microsoft.AspNetCore.Mvc;

namespace EventHubApp.Web.Controllers
{
    public class WatchlistController : BaseController
    {
        private readonly IWatchlistService watchlistService;

        public WatchlistController(IWatchlistService watchlistService)
        {
            this.watchlistService = watchlistService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                string? userId = this.GetUserId();
                if (userId == null)
                {
                    return this.Forbid();
                }

                IEnumerable<WatchlistViewModel> userWatchlist = await this.watchlistService
                    .GetUserWatchlistAsync(userId);
                return View(userWatchlist);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                return this.RedirectToAction(nameof(Index), "Home");
            }
        }


        [HttpPost]
        public async Task<IActionResult> Add(string? eventId)
        {
            try
            {
                string? userId = this.GetUserId();
                if (userId == null)
                {
                    return this.Forbid();
                }

                bool result = await this.watchlistService
                    .AddEventToUserWatchlistAsync(eventId, userId);
                if (result == false)
                {
                    return this.RedirectToAction(nameof(Index), "Event");
                }

                return this.RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                return this.RedirectToAction(nameof(Index), "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Remove(string? eventId)
        {
            try
            {
                string? userId = this.GetUserId();
                if (userId == null)
                {
                    return this.Forbid();
                }

                bool result = await this.watchlistService
                    .RemoveEventFromWatchlistAsync(eventId, userId);
                if (result == false)
                {
                    return this.RedirectToAction(nameof(Index));
                }

                return this.RedirectToAction(nameof(Index), "Event");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                return this.RedirectToAction(nameof(Index), "Home");
            }
        }




    }
}
