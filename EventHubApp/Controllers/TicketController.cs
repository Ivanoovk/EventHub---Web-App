using EventHubApp.Services.Core.Interfaces;
using EventHubApp.Web.ViewModels.Ticket;
using Microsoft.AspNetCore.Mvc;

namespace EventHubApp.Web.Controllers
{
    public class TicketController : BaseController
    {
        private readonly ITicketService ticketService;

        public TicketController(ITicketService ticketService)
        {
            this.ticketService = ticketService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string? userId = this.GetUserId();

            IEnumerable<TicketIndexViewModel> allTickets = await this.ticketService
                .GetUserTicketsAsync(userId);

            return View(allTickets);
        }
    }
}
