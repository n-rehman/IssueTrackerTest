using IssueTracker.Data.Entities;
using IssueTracker.Shared.Requests.Tickets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssueTracker.Business.Interfaces
{
	public interface ICommentManager
	{

		Task<Comment> AddTicketCommentAsync(int ticketId, string comment);
		IEnumerable<Comment> GetTicketCommentsList();
	}


}
