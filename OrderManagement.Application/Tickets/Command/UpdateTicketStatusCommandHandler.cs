using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using OrderManagement.Domain.Enums;
using OrderManagement.Domain.Repositories.Tickets.Command;
using OrderManagement.Domain.Repositories.Tickets.Query;
using OrderManagement.Domain.Repositories.Users;
using OrderManagement.Domain.Repositories.Users.Command;
using OrderManagement.Domain.Repositories.Users.Query;

namespace OrderManagement.Application.Tickets.Command
{
    public record UpdateTicketCommand(Guid TicketId, Status Status) : IRequest<UpdateTicketCommandResult>;
    public record UpdateTicketCommandResult(Guid TicketId, string Title, string Description, string Status, string Priority, DateTime CreatedAt);

    public class UpdateTicketStatusCommandHandler : IRequestHandler<UpdateTicketCommand, UpdateTicketCommandResult>
    {
        private readonly ITicketCommandRepository _ticketCommandRepository;
        private readonly ITicketQueryRepository _ticketQueryRepository;
        private readonly IUserQueryRepository _userQueryRepository;
        private readonly ICurrentUserService _currentUser;

        public UpdateTicketStatusCommandHandler(
            ITicketCommandRepository ticketCommandRepository, 
            ITicketQueryRepository ticketQueryRepository,
            IUserQueryRepository userQueryRepository,
            ICurrentUserService currentUser)
        {
            _ticketCommandRepository = ticketCommandRepository;
            _ticketQueryRepository = ticketQueryRepository;
            _userQueryRepository = userQueryRepository;
            _currentUser = currentUser;
        }

        public async Task<UpdateTicketCommandResult> Handle(UpdateTicketCommand request, CancellationToken cancellationToken)
        {
            if (_currentUser.Role != UserRole.Admin)
                throw new UnauthorizedAccessException("Only admin may update tickets status.");

            var currentUserId = _currentUser.UserId;
            var Adminuser = await _userQueryRepository.GetUserByIdAsync(currentUserId, cancellationToken);
            
            var ticket = await _ticketQueryRepository.GetTicketByIdAsync(request.TicketId, cancellationToken);
            if (ticket == null)
                throw new KeyNotFoundException($"Ticket with ID {request.TicketId} not found.");

            ticket.AssignTo(Adminuser);

            ticket.UpdateStatus(request.Status);

            var updtaedTicket = await _ticketCommandRepository.UpdateTicketAsync(ticket, cancellationToken);

            return new UpdateTicketCommandResult(
                updtaedTicket.Id, 
                updtaedTicket.Title, 
                updtaedTicket.Description, 
                updtaedTicket.Status.ToString(), 
                updtaedTicket.Priority.ToString(), 
                updtaedTicket.CreatedAt);
        }
    }
}
