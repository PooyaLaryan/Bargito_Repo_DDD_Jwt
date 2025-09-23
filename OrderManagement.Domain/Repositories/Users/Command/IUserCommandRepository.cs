using OrderManagement.Domain.Dtos;
using OrderManagement.Domain.Entities;

namespace OrderManagement.Domain.Repositories.Users.Command;

public interface IUserCommandRepository
{
    Task<Guid> RegisterAsync(User user);
}
