using MediatR;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Enums;
using OrderManagement.Domain.Repositories.Tickets.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Application.Tickets.Command
{
    public record CreateTicketCommand(string Title, string Description, Status Status, Priority Priority, Guid UserId) : IRequest<CreateTicketResult>;
    public record CreateTicketResult(Guid Guid);
    public class CreateTicketCommandHandler : IRequestHandler<CreateTicketCommand, CreateTicketResult>
    {
        private readonly ITicketCommandRepository _ticketRepository;

        public CreateTicketCommandHandler(ITicketCommandRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }
        public async Task<CreateTicketResult> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
        {
            var ticket = new Ticket(request.Title, request.Description, request.Status, request.Priority, request.UserId);
            var id = await _ticketRepository.CreateTicketAsync(ticket);
            return new CreateTicketResult(id);
        }
    }
}
