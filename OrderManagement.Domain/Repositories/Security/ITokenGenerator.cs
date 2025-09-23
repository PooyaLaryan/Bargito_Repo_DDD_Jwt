using OrderManagement.Domain.Entities;

namespace OrderManagement.Domain.Repositories.Security;
public interface ITokenGenerator
{
    string GenerateToken(User user);
}
