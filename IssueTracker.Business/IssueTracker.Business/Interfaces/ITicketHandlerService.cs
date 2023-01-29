using IssueTracker.Business.Response;
using IssueTracker.Data;
using IssueTracker.Data.Entities;
using IssueTracker.Shared.Requests.Tickets;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace IssueTracker.Business.Interfaces
{
    public interface ITicketHandlerService 
    {

		Task<ApiResponseMessage> AddTicketAsync(AddTicketRequest request);
		Task<ApiResponseMessage> UpdateTicketAsync(UpdateTicketRequest request);
		Task<ApiResponseMessage> CloseTicketAsync(CloseTicketRequest request);
		Task<ApiResponseMessage> AssignTicketAsync(AssignTicketRequest request);
		Task<ApiResponseMessage> GetTicketListAsync();
		Task<ApiResponseMessage> GetTicketByIdAsync(int id);

	}
}
