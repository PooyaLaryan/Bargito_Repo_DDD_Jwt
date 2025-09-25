using MediatR;
using OrderManagement.Domain.Enums;
using OrderManagement.Domain.Repositories.Tickets.Command;
using OrderManagement.Domain.Repositories.Tickets.Query;
using OrderManagement.Domain.Repositories.Users;

namespace OrderManagement.Application.Tickets.Command;

public record DeleteTicketCommand(Guid TicketId) : IRequest;
internal class DeleteTicketHandler : IRequestHandler<DeleteTicketCommand>
{
    private readonly ITicketCommandRepository _ticketCommandRepository;
    private readonly ITicketQueryRepository _ticketQueryRepository;
    private readonly ICurrentUserService _currentUser;

    public DeleteTicketHandler(ITicketCommandRepository ticketCommandRepository, ITicketQueryRepository ticketQueryRepository,ICurrentUserService currentUser)
    {
        _ticketCommandRepository = ticketCommandRepository;
        _ticketQueryRepository = ticketQueryRepository;
        _currentUser = currentUser;
    }
    public async Task Handle(DeleteTicketCommand request, CancellationToken cancellationToken)
    {
        if (_currentUser.Role != UserRole.Admin)
            throw new UnauthorizedAccessException("Only admin may update tickets status.");

        var ticket = await _ticketQueryRepository.GetTicketByIdAsync(request.TicketId, cancellationToken);

        if (ticket == null)
            throw new KeyNotFoundException($"Ticket with ID {request.TicketId} not found.");    

        await _ticketCommandRepository.DeleteTicketAsync(ticket, cancellationToken);
    }
}
