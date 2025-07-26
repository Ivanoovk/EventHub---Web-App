using EventHubApp.Services.Core.Interfaces;
using EventHubApp.Web.ViewModels.Place;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventHubApp.Web.Controllers
{
    public class PlaceController : BaseController
    {
        private readonly IPlaceService placeService;

        public PlaceController(IPlaceService placeService)
        {
            this.placeService = placeService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            try
            {
                IEnumerable<UsersPlaceIndexViewModel> allPlacesUserView = await this.placeService
                    .GetAllPlacesUserViewAsync();

                return View(allPlacesUserView);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

                return this.RedirectToAction(nameof(Index), "Home");
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Program(string? id)
        {
            try
            {
                // TODO: Implement showing Showtimes on Program and choosing Showtime when buying a Ticket
                PlaceProgramViewModel? placeProgram = await this.placeService
                    .GetPlaceProgramAsync(id);
                if (placeProgram == null)
                {
                    return this.RedirectToAction(nameof(Index));
                }

                return this.View(placeProgram);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

                return this.RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(string? id)
        {
            try
            {
                PlaceDetailsViewModel? placeProgram = await this.placeService
                    .GetPlaceDetailsAsync(id);
                if (placeProgram == null)
                {
                    return this.RedirectToAction(nameof(Index));
                }

                return this.View(placeProgram);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

                return this.RedirectToAction(nameof(Index));
            }
        }
    }

}
