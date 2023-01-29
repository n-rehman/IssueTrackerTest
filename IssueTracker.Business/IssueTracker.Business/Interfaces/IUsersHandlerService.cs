using IssueTracker.Business.Response;
using IssueTracker.Data.Entities;
using IssueTracker.Shared.Requests.Tickets;
using IssueTracker.Shared.Requests.Users;


namespace IssueTracker.Business.Interfaces
{
    public interface IUsersHandlerService
	{
		Task<ApiResponseMessage> AddUserAsync(AddUserRequest request);
		Task<ApiResponseMessage> UpdateUserSync(UpdateUserRequest request);
		Task<ApiResponseMessage> DeactivateUserAsync(DeactivateUserRequest request);
		Task<ApiResponseMessage> GetUsersListAsync();
		Task<ApiResponseMessage> GetUserByIdAsync(int id);

	}
}
