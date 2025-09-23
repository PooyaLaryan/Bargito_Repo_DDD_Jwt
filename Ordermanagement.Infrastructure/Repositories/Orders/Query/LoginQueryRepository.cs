using Microsoft.EntityFrameworkCore;
using Ordermanagement.Infrastructure.Persistence;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Repositories.Users.Query;

namespace Ordermanagement.Infrastructure.Repositories.Users.Query;

public class LoginQueryRepository : ILoginQueryRepository
{
    private readonly ReadDbContext _readDbContext;

    public LoginQueryRepository(ReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        var user = await _readDbContext.Users.FirstOrDefaultAsync(x => x.Email == email);
        return user;
    }
}
