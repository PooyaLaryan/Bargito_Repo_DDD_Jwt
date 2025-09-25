using Microsoft.EntityFrameworkCore;
using Ordermanagement.Infrastructure.Persistence;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Repositories.Users.Query;

namespace Ordermanagement.Infrastructure.Repositories.Users.Query;

public class LoginQueryRepository : IUserQueryRepository
{
    private readonly ReadDbContext _readDbContext;

    public LoginQueryRepository(ReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }

    public async Task<IReadOnlyList<User>> GetAllUsersAsync(CancellationToken cancellationToken)
    {
        return await _readDbContext.Users.ToListAsync(cancellationToken);
    }

    public async Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
    {
        var user = await _readDbContext.Users.FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
        return user;
    }

    public async Task<User> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        var user = await _readDbContext.Users.FirstAsync(x => x.Id == userId, cancellationToken);
        return user;
    }
}
