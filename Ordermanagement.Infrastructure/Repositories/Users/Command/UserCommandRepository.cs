using Ordermanagement.Infrastructure.Persistence;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Repositories.Users.Command;

namespace Ordermanagement.Infrastructure.Repositories.Users.Command;

public class UserCommandRepository : IUserCommandRepository
{
    private readonly WriteDbContext _writeDbContext;

    public UserCommandRepository(WriteDbContext writeDbContext)
    {
        _writeDbContext = writeDbContext;
    }
    public async Task<Guid> RegisterAsync(User user, CancellationToken cancellationToken)
    {
        await _writeDbContext.Users.AddAsync(user);
        await _writeDbContext.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}
