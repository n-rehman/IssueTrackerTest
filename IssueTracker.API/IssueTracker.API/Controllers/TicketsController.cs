using IssueTracker.Business.Interfaces;
using IssueTracker.Business.Services;
using IssueTracker.Data.Entities;
using IssueTracker.Shared.Requests.Tickets;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text;

namespace IssueTracker.API.Controllers
{
	[ApiController]
	[Route("tickets")]
	public class TicketsController : ControllerBase
	{
		private readonly ITicketHandlerService _ticketHandlerService;

		public TicketsController(ITicketHandlerService ticketHandlerService)
		{
			_ticketHandlerService = ticketHandlerService;
		}

		[HttpGet("")]
		public ActionResult GetTickets()
		{

			var response = _ticketHandlerService.GetTicketListAsync();
			return response.Result.ApiResponseResult;

		}
		[HttpGet("ticket/{id}")]
		public ActionResult GetTicketByIdAsync(int ticketId)
		{

			var response = _ticketHandlerService.GetTicketByIdAsync(ticketId);
			return response.Result.ApiResponseResult;
		}


		[HttpPost("ticket")]
		public ActionResult AddTicket([FromBody] AddTicketRequest ticketRequest)
		{

			var response = _ticketHandlerService.AddTicketAsync(ticketRequest);
			return response.Result.ApiResponseResult;
		}

		[HttpPut("ticket")]
		public ActionResult UpdateTicket([FromBody] UpdateTicketRequest updateTicketRequest)
		{

			var response = _ticketHandlerService.UpdateTicketAsync(updateTicketRequest);
			return response.Result.ApiResponseResult;

		}
		[HttpPut("closeticket")]
		public ActionResult CloseTicket([FromBody] CloseTicketRequest closeTicketRequest)
		{

			var response = _ticketHandlerService.CloseTicketAsync(closeTicketRequest);
			return response.Result.ApiResponseResult;

		}
		[HttpPut("assignticket")]
		public ActionResult AssignTicket([FromBody] AssignTicketRequest assignTicketRequest)
		{

			var response = _ticketHandlerService.AssignTicketAsync(assignTicketRequest);
			return response.Result.ApiResponseResult;

		}

	}
}
