using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Enums;

namespace OrderManagement.Domain.Repositories.Tickets.Query;

public interface ITicketQueryRepository
{
    Task<IReadOnlyList<Ticket>> GetTicketsByCreatorAsync(Guid employeeId, CancellationToken cancellationToken);
    Task<IEnumerable<Ticket>> GetTicketsAsync(CancellationToken cancellationToken);
    Task<Ticket?> GetTicketByIdAsync(Guid ticketId, CancellationToken cancellationToken);
    Task<int> GetTicketCountByStatusAsync(Status status, CancellationToken cancellationToken);
}
