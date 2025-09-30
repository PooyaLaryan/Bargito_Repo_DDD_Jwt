using MediatR;
using OrderManagement.Domain.Enums;
using OrderManagement.Domain.Repositories.Tickets.Query;
using OrderManagement.Domain.Services;

namespace OrderManagement.Application.Tickets.Query;

public record GetTicketCountQuery(Status Status) : IRequest<int>;
public class GetTicketCountQueryHandler : IRequestHandler<GetTicketCountQuery, int>
{
    private readonly ITicketQueryRepository _ticketQueryRepository;
    private readonly ICurrentUserService _currentUser;

    public GetTicketCountQueryHandler(ITicketQueryRepository ticketQueryRepository, ICurrentUserService currentUser)
    {
        _ticketQueryRepository = ticketQueryRepository;
        _currentUser = currentUser;
    }
    public async Task<int> Handle(GetTicketCountQuery request, CancellationToken cancellationToken)
    {
        if (_currentUser.Role != UserRole.Admin)
            throw new UnauthorizedAccessException("Only admin can view count of tickets.");

        var count = await _ticketQueryRepository.GetTicketCountByStatusAsync(request.Status, cancellationToken);
        return count;
    }
}
