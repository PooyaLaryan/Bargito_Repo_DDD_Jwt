using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Ordermanagement.Infrastructure.Persistence;
using Ordermanagement.Infrastructure.Repositories.Security;
using Ordermanagement.Infrastructure.Repositories.Users.Command;
using Ordermanagement.Infrastructure.Repositories.Users.Query;
using OrderManagement.Application.Users;
using OrderManagement.Application.Users.Command;
using OrderManagement.Application.Users.Query;
using OrderManagement.Domain.Repositories.Security;
using OrderManagement.Domain.Repositories.Users.Command;
using OrderManagement.Domain.Repositories.Users.Query;
using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();
        builder.Services.AddSwaggerGen();

        // JWT
        var jwtSettings = builder.Configuration.GetSection("JwtSettings");
        var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings["Issuer"],
                ValidAudience = jwtSettings["Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };
        });

        builder.Services.AddAuthorization();

        //Db
        builder.Services.AddDbContext<WriteDbContext>(option => option.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
        builder.Services.AddDbContext<ReadDbContext>(option => option.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

        // MediatR
        builder.Services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(RegisterUserHandler).Assembly));
        builder.Services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(LoginUserHandler).Assembly));

        //Repo
        builder.Services.AddScoped<DbSeeder>();
        builder.Services.AddScoped<IUserCommandRepository, UserCommandRepository>();
        builder.Services.AddScoped<ILoginQueryRepository, LoginQueryRepository>();
        builder.Services.AddScoped<ITokenGenerator, JwtTokenGenerator>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();


        using (var scope = app.Services.CreateScope())
        {
            var seeder = scope.ServiceProvider.GetRequiredService<DbSeeder>();
            seeder.SeedAsync().GetAwaiter().GetResult();
        }

        app.Run();
    }
}