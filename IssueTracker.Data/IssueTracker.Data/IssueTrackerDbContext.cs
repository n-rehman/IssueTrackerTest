using IssueTracker.Data.Entities;
using IssueTracker.Data.EntityMappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Diagnostics;
using System.Reflection.Emit;
using System.Threading;
using System.Threading.Tasks;

namespace IssueTracker.Data
{
    public class IssueTrackerDbContext : DbContext
    {
        public IssueTrackerDbContext()
        {

        }
        public IssueTrackerDbContext(DbContextOptions<IssueTrackerDbContext> options) : base(options)
        {
            
        }

        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketType> TicketTypes { get; set; }
		public DbSet<Project> Projects { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<Comment> Comments { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
			new TicketTypeEntityConfig(modelBuilder.Entity<TicketType>());
			new ProjectEntityConfig(modelBuilder.Entity<Project>());
			new UserEntityConfig(modelBuilder.Entity<User>());
			new TicketEntityConfig(modelBuilder.Entity<Ticket>());
			new CommentEntityConfig(modelBuilder.Entity<Comment>());

		}

       
    }
}