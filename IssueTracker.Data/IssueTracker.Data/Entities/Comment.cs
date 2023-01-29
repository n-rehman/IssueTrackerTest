namespace IssueTracker.Data.Entities
{
    public class Comment 
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime DateCreated { get; set; } 
		public int TicketId { get; set; }
		//public virtual Ticket RelatedToTicket { get; set; }
	}
}
