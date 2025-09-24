using Microsoft.EntityFrameworkCore;
using OrderManagement.Domain.Entities;

namespace Ordermanagement.Infrastructure.Persistence;

public class WriteDbContext : DbContext
{
    public WriteDbContext(DbContextOptions<WriteDbContext> dbContextOptions) :base(dbContextOptions) { }
    
    public DbSet<User> Users { get; set; } 
    public DbSet<Ticket> Ticket { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Ticket>().Property<DateTime>("CreatedAt");
        modelBuilder.Entity<Ticket>().Property<DateTime>("UpdatedAt");

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(WriteDbContext).Assembly);
    }
}
