using MediatR;
using OrderManagement.Domain.Enums;
using OrderManagement.Domain.Repositories.Tickets.Query;
using OrderManagement.Domain.Services;

namespace OrderManagement.Application.Tickets.Query;

public record GetAllTicketsQuery() : IRequest<IReadOnlyList<AllTicketsQueryResult>>;
public record AllTicketsQueryResult(Guid TicketId, string Title, string Description, DateTime CreatedAt, string Status);
public class GetAllTicketsQueryHandler : IRequestHandler<GetAllTicketsQuery, IReadOnlyList<AllTicketsQueryResult>>
{
    private readonly ITicketQueryRepository _ticketQueryRepository;
    private readonly ICurrentUserService _currentUser;

    public GetAllTicketsQueryHandler(ITicketQueryRepository ticketQueryRepository, ICurrentUserService currentUserService)
    {
        _ticketQueryRepository = ticketQueryRepository;
        _currentUser = currentUserService;
    }

    public async Task<IReadOnlyList<AllTicketsQueryResult>> Handle(GetAllTicketsQuery request, CancellationToken cancellationToken)
    {
        if (_currentUser.Role != UserRole.Admin)
            throw new UnauthorizedAccessException("Only admin can view their tickets.");

        var tickets = await _ticketQueryRepository.GetTicketsAsync(cancellationToken);

        return tickets.Select(t => new AllTicketsQueryResult(t.Id, t.Title, t.Description, t.CreatedAt, t.Status.ToString())).ToList(); 
    }
}
