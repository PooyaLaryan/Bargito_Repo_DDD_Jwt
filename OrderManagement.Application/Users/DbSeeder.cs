using MediatR;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Repositories.Users.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Application.Users
{
    public class DbSeeder
    {
        private readonly IUserCommandRepository _userCommandRepository;

        public DbSeeder(IUserCommandRepository userCommandRepository)
        {
            _userCommandRepository = userCommandRepository;
        }

        public async Task SeedAsync()
        {
            string hashed = BCrypt.Net.BCrypt.HashPassword("123");

            await _userCommandRepository.RegisterAsync(new User
            (
                fullName:"Pooya Lariyan",
                email: "pooya.laryan@gmail.com",
                password: hashed,
                role : Domain.Enums.UserRole.Admin
            ));
        }
    }
}
