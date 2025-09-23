using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderManagement.Domain.Entities;

namespace Ordermanagement.Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(nameof(User));

        builder.HasKey(c => c.Id);
        builder.Property(c => c.FullName).IsRequired().HasColumnName(nameof(User.FullName)).HasMaxLength(500);
        builder.Property(c => c.Email).IsRequired().HasColumnName(nameof(User.Email));
        builder.Property(x=>x.Role).IsRequired().HasColumnName(nameof(User.Role)); 
        builder.Property(x=>x.Password).IsRequired().HasColumnName(nameof(User.Password));
    }
}
