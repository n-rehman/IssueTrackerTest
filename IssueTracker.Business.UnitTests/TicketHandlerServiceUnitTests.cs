using IssueTracker.Business.Services;
using IssueTracker.Data;
using IssueTracker.Data.Entities;
using IssueTracker.Shared.Enums;
using IssueTracker.Shared.Requests.Tickets;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using System.Diagnostics;

namespace IssueTracker.Business.UnitTests
{
	[TestClass]
	public class TicketHandlerServiceUnitTests
	{
		private IssueTrackerDbContext _issueTrackerDbContext;
		private TicketHandlerService _ticketHandlerService;
		private Microsoft.Extensions.Logging.ILogger<TicketHandlerService> _logger;

		public  TicketHandlerServiceUnitTests()
		{
			

			var serviceProvider = new ServiceCollection()
			.AddLogging()
			.BuildServiceProvider();

			var factory = serviceProvider.GetService<ILoggerFactory>();

			_logger = factory.CreateLogger<TicketHandlerService>();


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

			
			_ticketHandlerService = new TicketHandlerService(_issueTrackerDbContext, _logger);

		}


		[TestMethod]
		public void TicketHandler_Raise_NewTicket_Bug()
		{

			//Arrange
			AddTicketRequest ticketRequest = new AddTicketRequest { Title = "Test New Ticket 1", TypeId = 1, Description = "Test New Ticket 1 Description", ProjectId = 1 };

			//Act
			var response = _ticketHandlerService.AddTicketAsync(ticketRequest);

			//Assert
			Assert.AreEqual(((Ticket)response.Result.ApiResponseResult.Value).Title, "Test New Ticket 1");
		}

		[TestMethod]
		public void TicketHandler_Get_TicketById()
		{

			//Arrange
			AddTicketRequest ticketRequest = new AddTicketRequest { Title = "Test New Ticket 2", TypeId = 1, Description = "Test New Ticket 2 Description", ProjectId = 1 };

			//Act
			var response = (Ticket)_ticketHandlerService.AddTicketAsync(ticketRequest).Result.ApiResponseResult.Value;
			var getResponse = (Ticket)_ticketHandlerService.GetTicketByIdAsync(response.Id).Result.ApiResponseResult.Value;
			//Assert
			Assert.AreEqual(response.Id, getResponse.Id);
		}
		[TestMethod]
		public void TicketHandler_Update_ExistingTicket_Title()
		{

			//Arrange
			AddTicketRequest ticketRequest = new AddTicketRequest { Title = "Test New Ticket 3", TypeId = 1, Description = "Test New Ticket 3 Description", ProjectId = 1 };

			//Act
			var addResponse = _ticketHandlerService.AddTicketAsync(ticketRequest);
			var getResponse = _ticketHandlerService.GetTicketByIdAsync(((Ticket)addResponse.Result.ApiResponseResult.Value).Id);
			//update 
			UpdateTicketRequest ticketUpdateReq = new UpdateTicketRequest {Id= ((Ticket)getResponse.Result.ApiResponseResult.Value).Id, Title = ((Ticket)getResponse.Result.ApiResponseResult.Value).Title + "Updated" };
			var updateResponse = (Ticket)_ticketHandlerService.UpdateTicketAsync(ticketUpdateReq).Result.ApiResponseResult.Value;
			//Assert
			Assert.AreEqual("Test New Ticket 3Updated", updateResponse.Title);
		}
		[TestMethod]
		public void TicketHandler_Update_ExistingTicket_Description()
		{

			//Arrange
			AddTicketRequest ticketRequest = new AddTicketRequest { Title = "Test Ticket Description update", TypeId = 1, Description = "Test Ticket Description update", ProjectId = 1 };

			//Act
			var addResponse = (Ticket)_ticketHandlerService.AddTicketAsync(ticketRequest).Result.ApiResponseResult.Value;
			var getResponse = (Ticket)_ticketHandlerService.GetTicketByIdAsync(addResponse.Id).Result.ApiResponseResult.Value;
			//update 
			UpdateTicketRequest ticketUpdateReq = new UpdateTicketRequest { Id = getResponse.Id, Description = getResponse.Description + "-Updated" };
			var updateResponse = (Ticket)_ticketHandlerService.UpdateTicketAsync(ticketUpdateReq).Result.ApiResponseResult.Value;
			//Assert
			Assert.AreEqual("Test Ticket Description update-Updated", updateResponse.Description);
		}
		[TestMethod]
		public void TicketHandler_Close_ExistingTicket()
		{

			//Arrange
			AddTicketRequest ticketRequest = new AddTicketRequest { Title = "Test Ticket Closure", TypeId = 1, Description = "Test Ticket Closure", ProjectId = 1 };

			//Act
			var addResponse = (Ticket)_ticketHandlerService.AddTicketAsync(ticketRequest).Result.ApiResponseResult.Value;
			//update 
			CloseTicketRequest ticketUpdateReq = new CloseTicketRequest { Id = addResponse.Id };
			var updateResponse = _ticketHandlerService.CloseTicketAsync(ticketUpdateReq);
			var getResponse = _ticketHandlerService.GetTicketByIdAsync(addResponse.Id);

			//Assert
			Assert.AreEqual( TicketStatus.Resolved, ((Ticket)getResponse.Result.ApiResponseResult.Value).Status);
		}
		[TestMethod]
		public void TicketHandler_Assign_ExistingTicket()
		{

			//Arrange
			AddTicketRequest ticketRequest = new AddTicketRequest { Title = "Test Ticket Assignment", TypeId = 1, Description = "Test Ticket Assignment", ProjectId = 1 };

			//Act
			var addResponse = (Ticket)_ticketHandlerService.AddTicketAsync(ticketRequest).Result.ApiResponseResult.Value;
			//update 
			AssignTicketRequest ticketAssignReq = new AssignTicketRequest { TicketId = addResponse.Id, AssignedToUserId = 2, Comment ="Assigning to user for investigation" };
			var assignResponse = (Ticket)_ticketHandlerService.AssignTicketAsync(ticketAssignReq).Result.ApiResponseResult.Value;
			var getResponse = (Ticket)_ticketHandlerService.GetTicketByIdAsync(assignResponse.Id).Result.ApiResponseResult.Value;

			//Assert
			Assert.AreEqual(2, getResponse.AssignedToId);
			Assert.AreEqual("Assigning to user for investigation", getResponse.Comments.First().Text);
		}
	}
}