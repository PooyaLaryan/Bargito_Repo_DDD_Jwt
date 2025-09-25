using OrderManagement.Domain.Entities;

namespace OrderManagement.Domain.Repositories.Tickets.Command;

public interface ITicketCommandRepository
{
    Task<Guid> CreateTicketAsync(Ticket ticket, CancellationToken cancellationToken);
    Task<Ticket> UpdateTicketAsync(Ticket ticket, CancellationToken cancellationToken);
    Task DeleteTicketAsync(Ticket ticket, CancellationToken cancellationToken);
}
