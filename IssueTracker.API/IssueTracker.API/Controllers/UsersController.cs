using IssueTracker.Business.Interfaces;
using IssueTracker.Business.Services;
using IssueTracker.Shared.Requests.Tickets;
using IssueTracker.Shared.Requests.Users;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Collections.Generic;
using System.Text;

namespace IssueTracker.API.Controllers
{
	[ApiController]
	[Route("users")]
	public class UsersController : ControllerBase
	{
		private readonly IUsersHandlerService _userHandlerService;
		private readonly static Serilog.ILogger _logger = Log.ForContext(typeof(UsersController));


		public UsersController(IUsersHandlerService userHandlerService)
		{
			_userHandlerService = userHandlerService;
		}

		[HttpGet("")]
		public ActionResult GetUsersListAsync()
		{

			var resultResponse = _userHandlerService.GetUsersListAsync();	
			return resultResponse.Result.ApiResponseResult;
			

		}
		[HttpGet("user/{id}")]
		public ActionResult GetUserByIdAsync(int userId)
		{
			var response = _userHandlerService.GetUserByIdAsync(userId);
			return response.Result.ApiResponseResult;
			
		}
		[HttpPost("user")]
		public ActionResult AddUser([FromBody] AddUserRequest userRequest)
		{
			var responseMessage = _userHandlerService.AddUserAsync(userRequest);
			return responseMessage.Result.ApiResponseResult;
		}

		[HttpPut("user")]
		public ActionResult UpdateUser([FromBody] UpdateUserRequest updateUserRequest)
		{

			var responseMessage = _userHandlerService.UpdateUserSync(updateUserRequest);
			return responseMessage.Result.ApiResponseResult;

		}
		[HttpPut("deactivateuser")]
		public ActionResult DeactivateUser([FromBody] DeactivateUserRequest deactivateUserRequest)
		{

			var response = _userHandlerService.DeactivateUserAsync(deactivateUserRequest);
			return response.Result.ApiResponseResult;

		}
	}
}
