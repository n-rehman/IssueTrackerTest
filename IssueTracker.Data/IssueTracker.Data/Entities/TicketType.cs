﻿namespace IssueTracker.Data.Entities
{
    public class TicketType
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string Name {get; set; }
        public string Color { get; set; }
    }
}
