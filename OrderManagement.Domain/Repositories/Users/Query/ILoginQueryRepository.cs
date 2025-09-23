using OrderManagement.Domain.Dtos;
using OrderManagement.Domain.Entities;

namespace OrderManagement.Domain.Repositories.Users.Query;

public interface ILoginQueryRepository
{
    Task<User?> GetUserByEmailAsync(string email);
}
