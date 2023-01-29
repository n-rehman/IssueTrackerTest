using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssueTracker.Shared.Requests.Tickets
{
	public class AssignTicketRequest
	{
		public int TicketId { get; set; }
		public int AssignedToUserId { get; set; }
		public string Comment { get; set; }

	}
}
