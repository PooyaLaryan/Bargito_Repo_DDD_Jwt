using OrderManagement.Domain.Enums;

namespace OrderManagement.Domain.Entities;

public class User : Entity
{
    public User(string fullName, string email, string password, UserRole role)
    {
        FullName = fullName;
        Email = email;
        Password = password;
        Role = role;
    }

    private User() { }

    public string FullName { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public UserRole Role { get; private set; }


    public Task<Guid> Login() 
    {
        return Task.FromResult(Guid.Empty);
    }
}
