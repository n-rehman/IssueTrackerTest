using IssueTracker.Business.Services;
using IssueTracker.Data;
using IssueTracker.Data.Entities;
using IssueTracker.Shared.Enums;
using IssueTracker.Shared.Requests.Tickets;
using IssueTracker.Shared.Requests.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using System.Diagnostics;

namespace IssueTracker.Business.UnitTests
{
	[TestClass]
	public class UserHandlerServiceUnitTests
	{
		private IssueTrackerDbContext _issueTrackerDbContext;
		private UserHandlerService _userHandlerService;
		private Microsoft.Extensions.Logging.ILogger<UserHandlerService> _logger;

		public UserHandlerServiceUnitTests()
		{
	

			var serviceProvider = new ServiceCollection()
			.AddLogging()
			.BuildServiceProvider();

			var factory = serviceProvider.GetService<ILoggerFactory>();

			_logger = factory.CreateLogger<UserHandlerService>();

			InitialiseTestData();

		}
		private void InitialiseTestData()
		{

			var options = new DbContextOptionsBuilder<IssueTrackerDbContext>()
			.UseInMemoryDatabase(databaseName: "IssueTrackerAPITestDB")
			.Options;

			_issueTrackerDbContext = new IssueTrackerDbContext(options);


			//Initial Data Mockup

			if (_issueTrackerDbContext.Users.Count() == 0)
			{
				_issueTrackerDbContext.Users.AddRange(
						new User { Id = 1, Name = "User1" },
						new User { Id = 2, Name = "User2" }
					);
			}

			if (_issueTrackerDbContext.Projects.Count() == 0)
			{
				_issueTrackerDbContext.Projects.AddRange(
							new Project { Id = 1, Title = "Project1", Description = "Test Project 1" },
							new Project { Id = 2, Title = "Project2", Description = "Test Project 2" }
							);
			}

			if (_issueTrackerDbContext.TicketTypes.Count() == 0)
			{
				_issueTrackerDbContext.TicketTypes.Add(new TicketType { Id = 1, Name = "Bug", Color = "Red", ProjectId = 1 }); ;
				_issueTrackerDbContext.TicketTypes.Add(new TicketType { Id = 2, Name = "ChangeRequest", Color = "Green", ProjectId = 1 });
				_issueTrackerDbContext.TicketTypes.Add(new TicketType { Id = 3, Name = "DataIssue", Color = "Amber", ProjectId = 1 });

			}

			_issueTrackerDbContext.SaveChanges();

			
			_userHandlerService = new UserHandlerService(_issueTrackerDbContext, _logger);

		}


		[TestMethod]
		public void TicketHandler_Raise_New_User()
		{

			//Arrange
			AddUserRequest userRequest = new AddUserRequest { Name = "Test New User1"};

			//Act
			var response = (User)_userHandlerService.AddUserAsync(userRequest).Result.ApiResponseResult.Value;

			//Assert
			Assert.AreEqual(response.Name, "Test New User1");
		}

		[TestMethod]
		public void TicketHandler_Get_UserById()
		{

			//Arrange
			AddUserRequest userRequest = new AddUserRequest { Name = "Test New User1" };

			//Act
			var response = (User)_userHandlerService.AddUserAsync(userRequest).Result.ApiResponseResult.Value;
			var getResponse = (User)_userHandlerService.GetUserByIdAsync(response.Id).Result.ApiResponseResult.Value;
			
			//Assert
			Assert.AreEqual(response.Id, getResponse.Id);
			Assert.AreEqual("Test New User1", getResponse.Name);
		}
		[TestMethod]
		public void TicketHandler_Update_ExistingUser_Name()
		{

			//Arrange
			AddUserRequest userRequest = new AddUserRequest { Name = "Test New User1" };

			//Act
			var response = (User)_userHandlerService.AddUserAsync(userRequest).Result.ApiResponseResult.Value;
			UpdateUserRequest userRequestUpdate = new UpdateUserRequest {Id= response.Id,  Name = "Test New User1-Updated" };
			var responseUpdate = (User)_userHandlerService.UpdateUserSync(userRequestUpdate).Result.ApiResponseResult.Value;

			Assert.IsNotNull(responseUpdate);

			var getResponse = (User)_userHandlerService.GetUserByIdAsync(responseUpdate.Id).Result.ApiResponseResult.Value;

			//Assert
			Assert.AreEqual(response.Id, getResponse.Id);
			Assert.AreEqual("Test New User1-Updated", getResponse.Name);
		}
		
	}
}