using OrderManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Domain.Entities
{
    public class Ticket : Entity
    {
        public Ticket(string title, string description, Status status, Priority priority, Guid createdByUserId)
        {
            Title = title;
            Description = description;
            Status = status;
            Priority = priority;
            CreatedByUserId = createdByUserId;
        }

        private Ticket() { }

        public string Title { get; private set; }
        public string Description { get; private set; }
        public Status Status { get; private set; }
        public Priority Priority { get; private set; }
        public Guid CreatedByUserId { get; private set; }
        public User CreatedByUser { get; set; }
        public Guid? AssignedToUserId { get; private set; }
        public User? AssignedToUser { get; set; }

        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        public static Ticket Create(string title, string description, Priority priority, Guid createdByUserId)
        {
            if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Title required", nameof(title));

            if (title.Length > 250) throw new ArgumentException("Title too long", nameof(title));
            
            if (description is null) description = string.Empty;


            return new Ticket(title.Trim(), description.Trim(), Status.Open, priority, createdByUserId);
        }

        public void UpdateStatus(Status status)
        {
            Status = status;
            UpdatedAt = DateTime.UtcNow;
        }   

        public void AssignTo(User admin)
        {
            AssignedToUser = admin ?? throw new ArgumentNullException(nameof(admin));
            AssignedToUserId = admin.Id;
        }

        public void Unassign()
        {
            AssignedToUser = null;
            AssignedToUserId = null;
        }

    }
}