using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Ordermanagement.Infrastructure.Configurations
{
    public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.ToTable(nameof(Ticket));
            builder.HasKey(t => t.Id);

            builder.Property(x => x.Title)
                .HasColumnName(nameof(Ticket.Title))
                .IsRequired(true);
            
            builder.Property(x => x.Description)
                .HasColumnName(nameof(Ticket.Description))
                .IsRequired(false);
            
            builder.Property(x => x.Status)
                .HasColumnName(nameof(Ticket.Status))
                .HasConversion<string>()
                .IsRequired();

            builder.Property(x => x.Priority)
                .HasColumnName(nameof(Ticket.Priority))
                .HasConversion<string>()
                .IsRequired();

            builder.Property(t => t.AssignedToUserId)
                .IsRequired(false)
                .HasColumnName(nameof(Ticket.AssignedToUser));


            builder.HasOne(x=>x.CreatedByUser)
                .WithMany(x=>x.CreatedTickets)
                .HasForeignKey(t => t.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x=>x.AssignedToUser)
                .WithMany(x=>x.AssignedTickets)
                .HasForeignKey(x=>x.AssignedToUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
