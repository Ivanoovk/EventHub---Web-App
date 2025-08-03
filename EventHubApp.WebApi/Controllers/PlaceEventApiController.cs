using EventHubApp.Services.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace EventHubApp.WebApi.Controllers
{
    public class PlaceEventApiController : BaseExternalApiController
    {
        private readonly IProjectionService projectionService;

        public PlaceEventApiController(IProjectionService projectionService)
        {
            this.projectionService = projectionService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("Showtimes")]
        public async Task<ActionResult<IEnumerable<string>>> GetProjectionShowtimes([Required] string placeId, [Required] string eventId)
        {
            IEnumerable<string> showtimes = await this.projectionService
                .GetProjectionShowtimesAsync(placeId, eventId);

            return this.Ok(showtimes);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("AvailableTickets")]
        public async Task<ActionResult<int>> GetAvailableTickets([Required] string placeId,
            [Required] string eventId, [Required] string showtime)
        {
            int availableTickets = await this.projectionService
                .GetAvailableTicketsCountAsync(placeId, eventId, showtime);

            return this.Ok(availableTickets);
        }
    }

}
