using IssueTracker.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using System.Reflection.Metadata;

namespace IssueTracker.Data.EntityMappers
{
    public class TicketEntityConfig
    {
        public TicketEntityConfig(EntityTypeBuilder<Ticket> builder)
        {
			builder.HasIndex(x => x.Id);
			builder.HasIndex(x => x.Title);
            builder.HasIndex(x => x.Status);
            builder.HasIndex(x => x.Priority);

            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(255);

            builder.HasMany(x => x.Comments)
                .WithOne()
                .IsRequired();

            builder.HasOne(x => x.TicketType)
                .WithMany()
                .HasForeignKey(x => x.TicketTypeId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

			builder.HasOne(x => x.AssignedTo).WithMany(r => r.AssignedTickets).HasForeignKey(x => x.AssignedToId);
			//builder.HasMany(x => x.Comments).WithOne(r => r.RelatedToTicket).HasForeignKey(x => x.TicketId);

			builder
	   .Navigation(b => b.AssignedTo)
	   .UsePropertyAccessMode(PropertyAccessMode.Property);


			//builder
	  // .Navigation(b => b.Comments)
	  // .UsePropertyAccessMode(PropertyAccessMode.Property);

		}
    }
}
