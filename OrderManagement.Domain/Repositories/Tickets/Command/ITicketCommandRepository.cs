using OrderManagement.Domain.Entities;

namespace OrderManagement.Domain.Repositories.Tickets.Command;

public interface ITicketCommandRepository
{
    Task<Guid> CreateTicketAsync(Ticket ticket);
}
