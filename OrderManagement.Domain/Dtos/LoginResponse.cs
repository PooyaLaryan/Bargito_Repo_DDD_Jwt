using OrderManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Domain.Dtos
{
    public record LoginResponse
    {
        public required string Token { get; set; }
        public required string FullName { get; set; }
        public required UserRole Role { get; set; }
    }
}
