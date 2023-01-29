using IssueTracker.Shared.Enums;

namespace IssueTracker.Shared.Requests.Tickets
{
    public class UpdateTicketRequest
    {
        public int Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public TicketPriority Priority { get; set; }
		public TicketStatus Status { get; set; }
		public int TypeId { get; set; }
		public int ProjectId { get; set; }
		public int AssignedToId { get; set; }
	}
}
