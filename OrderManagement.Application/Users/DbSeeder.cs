using MediatR;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Repositories.Users.Command;
using OrderManagement.Domain.Repositories.Users.Query;
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
        private readonly IUserQueryRepository _userQueryRepository;

        public DbSeeder(IUserCommandRepository userCommandRepository, IUserQueryRepository userQueryRepository)
        {
            _userCommandRepository = userCommandRepository;
            _userQueryRepository = userQueryRepository;
        }

        public async Task SeedAsync()
        {
            string hashed = BCrypt.Net.BCrypt.HashPassword("123");

            var users = await _userQueryRepository.GetAllUsersAsync(default);

            if (users.Any())
                return; 

            await _userCommandRepository.RegisterAsync(new User
            (
                fullName:"Pooya Lariyan",
                email: "pooya.laryan@gmail.com",
                password: hashed,
                role : Domain.Enums.UserRole.Admin
            ), default);

            await _userCommandRepository.RegisterAsync(new User
            (
                fullName: "admin admin",
                email: "admin@bargito.ir",
                password: hashed,
                role: Domain.Enums.UserRole.Admin
            ), default);

            await _userCommandRepository.RegisterAsync(new User
            (
                fullName: "client client",
                email: "client@bargito.ir",
                password: hashed,
                role: Domain.Enums.UserRole.Employee
            ), default);
        }
    }
}
