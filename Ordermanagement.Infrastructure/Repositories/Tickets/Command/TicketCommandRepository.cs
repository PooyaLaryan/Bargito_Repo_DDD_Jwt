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
    public async Task<Guid> CreateTicketAsync(Ticket ticket)
    {
        await _writeDbContext.Ticket.AddAsync(ticket);
        await _writeDbContext.SaveChangesAsync();

        return ticket.Id;
    }
}
