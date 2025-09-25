using MediatR;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Enums;
using OrderManagement.Domain.Repositories.Tickets.Command;
using OrderManagement.Domain.Repositories.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Application.Tickets.Command
{
    public record CreateTicketCommand(string Title, string Description, Status Status, Priority Priority) : IRequest<CreateTicketResult>;
    public record CreateTicketResult(Guid Guid);
    public class CreateTicketCommandHandler : IRequestHandler<CreateTicketCommand, CreateTicketResult>
    {
        private readonly ITicketCommandRepository _ticketRepository;
        private readonly ICurrentUserService _currentUser;

        public CreateTicketCommandHandler(ITicketCommandRepository ticketRepository, ICurrentUserService currentUserService)
        {
            _ticketRepository = ticketRepository;
            _currentUser = currentUserService;
        }
        public async Task<CreateTicketResult> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
        {
            if (_currentUser.Role != UserRole.Employee)
                throw new UnauthorizedAccessException("Only employees may create tickets.");

            var createdBy = _currentUser.UserId;

            var ticket = new Ticket(request.Title, request.Description, request.Status, request.Priority, createdBy);
            var id = await _ticketRepository.CreateTicketAsync(ticket, cancellationToken);
            return new CreateTicketResult(id);
        }
    }
}
