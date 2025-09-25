using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using OrderManagement.Domain.Enums;
using OrderManagement.Domain.Repositories.Tickets.Query;
using OrderManagement.Domain.Repositories.Users;

namespace OrderManagement.Application.Tickets.Query
{
    public record GetTicketByIdQuery(Guid TicketId) : IRequest<GetDetailTicketByIdQueryResult>;
    public record GetDetailTicketByIdQueryResult(Guid Id, string Title, string Description, string Status, string Priority, DateTime CreatedAt, DateTime UpdatedAt, Guid CreatedByUserId, Guid? AssignedToUserId);
    public class GetTicketByIdQueryHandler : IRequestHandler<GetTicketByIdQuery, GetDetailTicketByIdQueryResult>
    {
        private readonly ITicketQueryRepository _ticketQueryRepository;
        private readonly ICurrentUserService _currentUser;

        public GetTicketByIdQueryHandler(ITicketQueryRepository ticketQueryRepository, ICurrentUserService currentUser)
        {
            _ticketQueryRepository = ticketQueryRepository;
            _currentUser = currentUser;
        }

        public async Task<GetDetailTicketByIdQueryResult> Handle(GetTicketByIdQuery request, CancellationToken cancellationToken)
        {
            var ticket = await _ticketQueryRepository.GetTicketByIdAsync(request.TicketId, cancellationToken);

            if (ticket == null)
            {
                throw new KeyNotFoundException($"Ticket with ID {request.TicketId} not found.");
            }

            var currentUserId = _currentUser.UserId;

            if (ticket.AssignedToUserId != currentUserId && ticket.CreatedByUserId != currentUserId)
            {
                throw new UnauthorizedAccessException("You are not allowed to view this ticket");
            }

            return new GetDetailTicketByIdQueryResult
            (
                Id: ticket.Id,
                Title: ticket.Title,
                Description: ticket.Description,
                Status: ticket.Status.ToString(),
                Priority: ticket.Priority.ToString(),
                CreatedAt: ticket.CreatedAt,
                UpdatedAt: ticket.UpdatedAt,
                CreatedByUserId: ticket.CreatedByUserId,
                AssignedToUserId: ticket.AssignedToUserId
            );
        }
    }
}
