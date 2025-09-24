using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Application.Tickets.Command;

namespace OrderManagement.Api.Controllers
{
    [ApiController]
    [Route("Tickets")]
    public class TicketsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TicketsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTicket([FromBody]CreateTicketCommand createTicketCommand)
        {
            var id = await _mediator.Send(createTicketCommand);
            return Ok(id);
        }
    }
}
