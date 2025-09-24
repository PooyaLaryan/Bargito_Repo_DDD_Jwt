using MediatR;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> CreateTicket([FromBody]CreateTicketCommand createTicketCommand)
        {
            var id = await _mediator.Send(createTicketCommand);
            return Ok(id);
        }

        [HttpGet("my")]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> GetMyTickets()
        {
            var result = await _mediator.Send(new GetMyTicketsQuery());
            return Ok(result);
        }
    }
}
