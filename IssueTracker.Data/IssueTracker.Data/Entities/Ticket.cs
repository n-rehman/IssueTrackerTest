using IssueTracker.Shared.Enums;
using System.Collections.Generic;

namespace IssueTracker.Data.Entities
{
    public class Ticket
    {
        public int Id { get; init; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TicketPriority Priority { get; set; }
        public TicketStatus Status { get; set; }

        public int ProjectId { get; set; }
        public Project Project { get; private set; }

        public int TicketTypeId { get; set; }
        public TicketType TicketType { get; private set; }

		public int AssignedToId { get; set; }
		public virtual User AssignedTo { get; set; }
		
        public DateTime DateCreated { get; set; }
		public DateTime DateModified { get; set; }

		public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
