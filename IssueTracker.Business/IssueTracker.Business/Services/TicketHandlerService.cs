using IssueTracker.Business.Interfaces;
using IssueTracker.Business.Response;
using IssueTracker.Business.TicketComments;
using IssueTracker.Data;
using IssueTracker.Data.Entities;
using IssueTracker.Shared.Requests.Tickets;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace IssueTracker.Business.Services
{
	public class TicketHandlerService : ITicketHandlerService
	{
		private readonly IssueTrackerDbContext _context;
		private readonly ICommentManager _commentManager;
		private Microsoft.Extensions.Logging.ILogger _logger;

		public TicketHandlerService(IssueTrackerDbContext context, ILogger<TicketHandlerService> logger)
		{
			_context = context;
			_logger = logger;
			_commentManager = new TicketCommentManager(context);//TBD via DI
		}

		public async Task<ApiResponseMessage> AddTicketAsync(AddTicketRequest request)
		{
			
			var apiResponse = new ApiResponseMessage();
			
			var newTicket = new Ticket
			{
				Title = request.Title,
				Description = request.Description,
				Priority = request.Priority,
				Status = request.Status,
				ProjectId = request.ProjectId,
				TicketTypeId = request.TypeId,
				AssignedToId = request.AssignedToId,
				DateCreated = DateTime.Now,
				DateModified= DateTime.Now,
			};

			try
			{
				
				var taskResult = await _context.Tickets.AddAsync(newTicket);
				await _context.SaveChangesAsync();


				apiResponse.ApiResponseResult = new OkObjectResult(taskResult.Entity);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);
				var errorMessage = $"Unable to save ticket {request.Title}";
				apiResponse.ApiResponseResult = new NotFoundObjectResult(newTicket);
			}

			return apiResponse;

		}
		public async Task<ApiResponseMessage> UpdateTicketAsync(UpdateTicketRequest request)
		{
			var apiResponse = new ApiResponseMessage();

			
			var existingTicket = await _context.Tickets.FirstOrDefaultAsync(x => x.Id == request.Id);
			if (existingTicket == null)
			{
				var errorMessage = $"invalid request for ticket Id: {request.Id} not found in system";
				_logger.LogError(errorMessage);
				apiResponse.ApiResponseResult = new NotFoundObjectResult(errorMessage);
				return apiResponse;
			}
			try
			{
				existingTicket.Title = request.Title;
				existingTicket.Description = request.Description;
				existingTicket.AssignedToId = request.AssignedToId;
				existingTicket.Status = request.Status;
				existingTicket.TicketTypeId = request.TypeId;
				existingTicket.ProjectId = request.ProjectId;
				existingTicket.DateModified = DateTime.Now;
				var ticketUpdateResult = _context.Tickets.Update(existingTicket);
				var modifiedRows = await _context.SaveChangesAsync();

				apiResponse.ApiResponseResult = new OkObjectResult(ticketUpdateResult.Entity);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);
				var errorMessage = $"Unable to update ticket {request.Title}";
				apiResponse.ApiResponseResult = new NotFoundObjectResult(errorMessage);
			}

			return apiResponse;

		}
		public async Task<ApiResponseMessage> CloseTicketAsync(CloseTicketRequest request)
		{

			var apiResponse = new ApiResponseMessage();
			var existingTicket = await _context.Tickets.FirstOrDefaultAsync(x => x.Id == request.Id);
			
			if (existingTicket == null)
			{
				var errorMessage = $"invalid request for ticket Id: {request.Id} not found in system";
				_logger.LogError(errorMessage);
				apiResponse.ApiResponseResult = new NotFoundObjectResult(errorMessage);
				return apiResponse;
			}
			try
			{
				existingTicket.Status = Shared.Enums.TicketStatus.Resolved;
				existingTicket.DateModified = DateTime.Now;
				var updateTicketResult = _context.Tickets.Update(existingTicket);
			
				apiResponse.ApiResponseResult = new OkObjectResult(updateTicketResult.Entity);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);
				var errorMessage = $"Unable to close ticket with id: {request.Id}";
				apiResponse.ApiResponseResult = new NotFoundObjectResult(errorMessage);
			}

			return apiResponse;

		}
		public async Task<ApiResponseMessage> AssignTicketAsync(AssignTicketRequest request)
		{
			var apiResponse = new ApiResponseMessage();
			
			
			var existingTicket = await _context.Tickets.FirstOrDefaultAsync(x => x.Id == request.TicketId);

			if (existingTicket == null)
			{
				var errorMessage = $"invalid request for ticket Id: {request.TicketId} not found in system";
				_logger.LogError(errorMessage);
				apiResponse.ApiResponseResult = new NotFoundObjectResult(errorMessage);
				return apiResponse;
			}
			try
			{
				existingTicket.AssignedToId = request.AssignedToUserId;
				existingTicket.DateModified = DateTime.Now;
				var updateTicketResult = _context.Tickets.Update(existingTicket);
				var modifiedRows = await _context.SaveChangesAsync();
				var commentResult = await _commentManager.AddTicketCommentAsync(existingTicket.Id, request.Comment);


				apiResponse.ApiResponseResult = new OkObjectResult(updateTicketResult.Entity);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);
				var errorMessage = $"Unable to assign ticket with id: {request.TicketId}";
				apiResponse.ApiResponseResult = new NotFoundObjectResult(errorMessage);
			}

			return apiResponse;


		}
		public async Task<ApiResponseMessage> GetTicketListAsync()
		{
			var apiResponse = new ApiResponseMessage();
			apiResponse.ApiResponseResult = new OkObjectResult(new { value = _context.Tickets.Select(b => b).AsAsyncEnumerable() });

			return apiResponse;

		}
		public IIncludableQueryable<Ticket, User> GetTicketListIncludeUsers()
		{
			return _context.Tickets.Select(b => b).Include(u => u.AssignedTo);


		}
		public async Task<ApiResponseMessage> GetTicketByIdAsync(int id)
		{
			var apiResponse = new ApiResponseMessage();
			
			var result = await _context.Tickets.Where(b => b.Id == id).FirstOrDefaultAsync();
			apiResponse.ApiResponseResult = new OkObjectResult(result);

			if (result == null)
			{
				var errorMessage = $"invalid request for ticket Id: {id} not found in system";
				_logger.LogError(errorMessage);
				apiResponse.ApiResponseResult = new NotFoundObjectResult(errorMessage);
			}

			return apiResponse;
		}

	}
}
