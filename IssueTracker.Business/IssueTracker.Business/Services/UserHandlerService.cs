using IssueTracker.Business.Interfaces;
using IssueTracker.Business.Response;
using IssueTracker.Data;
using IssueTracker.Data.Entities;
using IssueTracker.Shared.Requests.Tickets;
using IssueTracker.Shared.Requests.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace IssueTracker.Business.Services
{
    public class UserHandlerService : IUsersHandlerService
    {
        private readonly IssueTrackerDbContext _context;
		private Microsoft.Extensions.Logging.ILogger _logger;


		public UserHandlerService(IssueTrackerDbContext context, ILogger<UserHandlerService> logger )
        {
            _context = context;
			_logger = logger;
        }

        public async Task<ApiResponseMessage> GetUsersListAsync()
        {
		
			var apiResponse = new ApiResponseMessage();
			apiResponse.ApiResponseResult = new OkObjectResult(new { value = _context.Users.Select(u => u).AsAsyncEnumerable() });

			return apiResponse;
			
		}
        public async Task<ApiResponseMessage> GetUserByIdAsync(int id)
        {
			var apiResponse = new ApiResponseMessage();
			var result = await _context.Users.Where(b => b.Id == id).FirstOrDefaultAsync();
			apiResponse.ApiResponseResult = new OkObjectResult(result);
			
			if (result == null)
			{
				var errorMessage = $"invalid request for user Id: {id} not found in system";
				_logger.LogError(errorMessage);
				apiResponse.ApiResponseResult = new NotFoundObjectResult(errorMessage);
			}
			return apiResponse;
        }
		public async Task<ApiResponseMessage> AddUserAsync(AddUserRequest request)
		{
			var apiResponse = new ApiResponseMessage();

			try
			{
				var newUser = new User
				{
					Name = request.Name,
					IsActive = true,
					DateCreated= DateTime.Now,
					DateModified = DateTime.Now,
				};

				var taskResult = await _context.Users.AddAsync(newUser);
				await _context.SaveChangesAsync();
				apiResponse.ApiResponseResult = new OkObjectResult(taskResult.Entity);
			}
			catch (Exception ex) 
			{ 
				_logger.LogError(ex.Message);
				var errorMessage = $"Unable to save user {request.Name}";
				apiResponse.ApiResponseResult = new NotFoundObjectResult(errorMessage);
			}
			
			return apiResponse;
		}
		public async Task<ApiResponseMessage> UpdateUserSync(UpdateUserRequest request)
		{
			var apiResponse = new ApiResponseMessage();

			var existingUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.Id);
			if (existingUser == null)
			{
				var errorMessage = $"invalid request for user Id: {request.Id} not found in system";
				_logger.LogError(errorMessage);
				apiResponse.ApiResponseResult = new NotFoundObjectResult(existingUser);
				return apiResponse;
			}
			try
			{
				existingUser.Name = request.Name;
				existingUser.IsActive = request.IsActive;
				existingUser.DateModified = DateTime.Now;
				var userSaveResult = _context.Users.Update(existingUser);
				await _context.SaveChangesAsync();
				apiResponse.ApiResponseResult = new OkObjectResult(userSaveResult.Entity);
			}
			catch(Exception ex)
			{
				_logger.LogError(ex.Message);
				var errorMessage = $"Unable to update user {request.Name}";
				apiResponse.ApiResponseResult = new NotFoundObjectResult(errorMessage);
			}
			
			return apiResponse;
		}
		public async Task<ApiResponseMessage> DeactivateUserAsync(DeactivateUserRequest request)
		{

			var apiResponse = new ApiResponseMessage();
			var existingUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.Id);

			if (existingUser == null)
			{
				var errorMessage = $"invalid request for user Id: {request.Id} not found in system";
				_logger.LogError(errorMessage);
				apiResponse.ApiResponseResult = new NotFoundObjectResult(errorMessage);
				return apiResponse;
			}
			try
			{
				existingUser.IsActive = false;
				existingUser.DateModified = DateTime.Now;
				var updateUserResult = _context.Users.Update(existingUser);

				apiResponse.ApiResponseResult = new OkObjectResult(updateUserResult.Entity);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);
				var errorMessage = $"Unable to de-activate user with id: {request.Id}";
				apiResponse.ApiResponseResult = new NotFoundObjectResult(errorMessage);
			}

			return apiResponse;

		}
	}
}