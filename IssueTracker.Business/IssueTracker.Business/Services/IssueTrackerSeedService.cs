using IssueTracker.Business.Interfaces;
using IssueTracker.Data;
using IssueTracker.Data.Entities;
using IssueTracker.Shared.Requests.Tickets;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace IssueTracker.Business.Services
{
    public class IssueTrackerSeedService : IDataSeedIssueTracker
    {
        public void Initialize(IssueTrackerDbContext dbContext)
        {
            //Initial Data Mockup

            dbContext.Users.AddRange(
                    new User { Id = 1, Name = "User1", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                    new User { Id = 2, Name = "User2", DateCreated = DateTime.Now, DateModified = DateTime.Now }
                );


            dbContext.Projects.AddRange(
                            new Project { Id = 1, Title = "Project1", Description = "Test Project 1" },
                            new Project { Id = 2, Title = "Project2", Description = "Test Project 2" }
                            );


            dbContext.TicketTypes.Add(new TicketType { Id = 1, Name = "Bug", Color = "Red", ProjectId = 1 });
			dbContext.TicketTypes.Add(new TicketType { Id = 2, Name = "ChangeRequest", Color = "Green", ProjectId = 1 });
			dbContext.TicketTypes.Add(new TicketType { Id = 3, Name = "DataIssue", Color = "Amber", ProjectId = 1 });

			dbContext.Tickets.Add(new Ticket { Id = 1, Title = "Application Issue", AssignedToId = 1, TicketTypeId = 1, Description = "som ticket", ProjectId = 1, DateCreated=DateTime.Now, DateModified= DateTime.Now }); ;


            dbContext.SaveChanges();
        }
    }
}
