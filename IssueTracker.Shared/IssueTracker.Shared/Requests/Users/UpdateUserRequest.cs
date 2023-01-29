using IssueTracker.Shared.Enums;

namespace IssueTracker.Shared.Requests.Users
{
    public class UpdateUserRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
		public bool IsActive { get; set; }
	}
}
