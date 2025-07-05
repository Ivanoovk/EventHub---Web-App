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
    }
}
