using MediatR;
using OrderManagement.Domain.Enums;
using OrderManagement.Domain.Repositories.Tickets.Query;
using OrderManagement.Domain.Services;

namespace OrderManagement.Application.Tickets.Query
{
    public record GetMyTicketsQuery() : IRequest<IReadOnlyList<MyTicketQueryResult>>;
    public record MyTicketQueryResult(Guid TicketId, string Title, string Description, string Status,string Priority,DateTime CreatedAt);
    public class GetMyTicketsQueryHandler : IRequestHandler<GetMyTicketsQuery, IReadOnlyList<MyTicketQueryResult>>
    {
        private readonly ITicketQueryRepository _ticketQueryRepository;
        private readonly ICurrentUserService _currentUser;

        public GetMyTicketsQueryHandler(ITicketQueryRepository ticketQueryRepository, ICurrentUserService currentUser)
        {
            _ticketQueryRepository = ticketQueryRepository;
            _currentUser = currentUser;
        }
        public async Task<IReadOnlyList<MyTicketQueryResult>> Handle(GetMyTicketsQuery request, CancellationToken cancellationToken)
        {
            if (_currentUser.Role != UserRole.Employee)
                throw new UnauthorizedAccessException("Only employees can view their tickets.");

            var currentUserId = _currentUser.UserId;    

            var tickets = await _ticketQueryRepository.GetTicketsByCreatorAsync(currentUserId, cancellationToken);
            return tickets.Select(t => new MyTicketQueryResult(t.Id, t.Title, t.Description, t.Status.ToString(), t.Priority.ToString(), t.CreatedAt)).ToList();
        }
    }
}
