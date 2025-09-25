using Microsoft.EntityFrameworkCore;
using Ordermanagement.Infrastructure.Persistence;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Enums;
using OrderManagement.Domain.Repositories.Tickets.Query;

namespace Ordermanagement.Infrastructure.Repositories.Tickets.Query;

public class TicketQueryRepository : ITicketQueryRepository
{
    private readonly ReadDbContext _readDbContext;

    public TicketQueryRepository(ReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }

    public async Task<IReadOnlyList<Ticket>> GetTicketsByCreatorAsync(Guid employeeId, CancellationToken cancellationToken)
    {
        var ticket = await _readDbContext.Tickets
            .Where(x => x.CreatedByUserId == employeeId)
            .OrderByDescending(x=>x.CreatedAt)
            .ToListAsync(cancellationToken);

        return ticket;
    }

    public async Task<IEnumerable<Ticket>> GetTicketsAsync(CancellationToken cancellationToken)
    {
        var tickets = await _readDbContext.Tickets
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync(cancellationToken);

        return tickets;
    }

    public Task<Ticket?> GetTicketByIdAsync(Guid ticketId, CancellationToken cancellationToken)
    {
        var ticket = _readDbContext.Tickets.FirstOrDefaultAsync(t => t.Id == ticketId, cancellationToken);
        return ticket;
    }

    public async Task<int> GetTicketCountByStatusAsync(Status status, CancellationToken cancellationToken)
    {
        var count = await _readDbContext.Tickets.Where(t => t.Status == status).CountAsync();  
        return count;
    }
}
