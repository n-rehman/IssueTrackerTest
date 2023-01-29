using IssueTracker.Business.Interfaces;
using IssueTracker.Data;
using IssueTracker.Data.Entities;
using IssueTracker.Shared.Requests.Tickets;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssueTracker.Business.TicketComments
{
	public class TicketCommentManager : ICommentManager
	{
		private readonly IssueTrackerDbContext _context;

		public TicketCommentManager(IssueTrackerDbContext context)
		{
			_context = context;
		
		}
		public async Task<Comment> AddTicketCommentAsync(int ticketId, string comment)
		{
			var newComment = new Comment
			{
				TicketId = ticketId,
				Text = comment,
				DateCreated = DateTime.Now
			};

			var taskResult = await _context.Comments.AddAsync(newComment);
			await _context.SaveChangesAsync();

			return taskResult.Entity;

		}

		public IEnumerable<Comment> GetTicketCommentsList()
		{
			throw new NotImplementedException();
		}
	}
}
