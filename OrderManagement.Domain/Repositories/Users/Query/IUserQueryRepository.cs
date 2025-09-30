using OrderManagement.Domain.Entities;

namespace OrderManagement.Domain.Repositories.Users.Query;

public interface IUserQueryRepository
{
    Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken);
    Task<User> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken);
    Task<IReadOnlyList<User>> GetAllUsersAsync(CancellationToken cancellationToken);

}
