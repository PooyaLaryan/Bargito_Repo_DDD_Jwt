using Ordermanagement.Infrastructure.Persistence;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Repositories.Tickets.Command;

namespace Ordermanagement.Infrastructure.Repositories.Tickets.Command;

public class TicketCommandRepository : ITicketCommandRepository
{
    private readonly WriteDbContext _writeDbContext;

    public TicketCommandRepository(WriteDbContext writeDbContext)
    {
        _writeDbContext = writeDbContext;
    }
    public async Task<Guid> CreateTicketAsync(Ticket ticket, CancellationToken cancellationToken)
    {
        await _writeDbContext.Ticket.AddAsync(ticket);
        await _writeDbContext.SaveChangesAsync(cancellationToken);

        return ticket.Id;
    }

    public Task DeleteTicketAsync(Ticket ticket, CancellationToken cancellationToken)
    {
        _writeDbContext.Ticket.Remove(ticket);
        return _writeDbContext.SaveChangesAsync(cancellationToken);

    }

    public async Task<Ticket> UpdateTicketAsync(Ticket ticket, CancellationToken cancellationToken)
    {
        _writeDbContext.Update(ticket);
        await _writeDbContext.SaveChangesAsync(cancellationToken);
        return ticket;
    }
}
