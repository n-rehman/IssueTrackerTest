using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssueTracker.Data.Entities
{
	public class User
	{
		public int Id { get; init; }
		public string Name { get; set; }
		public bool IsActive { get; set; }
		public DateTime DateCreated { get; set; }
		public DateTime DateModified { get; set; }
		public virtual List<Ticket> AssignedTickets { get; set; }
	}
}
