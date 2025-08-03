using EventHubApp.Services.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace EventHubApp.WebApi.Controllers
{
    public class TicketApiController : BaseExternalApiController
    {
        private readonly ITicketService ticketService;

        public TicketApiController(ITicketService ticketService)
        {
            this.ticketService = ticketService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Route("Buy")]
        [Authorize]
        public async Task<ActionResult> BuyTicket([Required] string placeId,
            [Required] string eventId, int quantity, [Required] string showtime)
        {
            string? userId = this.GetUserId();
            bool result = await this.ticketService
                .AddTicketAsync(placeId, eventId, quantity, showtime, userId);
            if (result == false)
            {
                return this.BadRequest();
            }

            return this.Ok();
        }
    }
}
