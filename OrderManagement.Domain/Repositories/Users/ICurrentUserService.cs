using OrderManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Domain.Repositories.Users
{
    public interface ICurrentUserService
    {
        Guid UserId { get; }
        UserRole Role { get; }
    }
}
