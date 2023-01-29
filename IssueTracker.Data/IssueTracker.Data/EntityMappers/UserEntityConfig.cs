using IssueTracker.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace IssueTracker.Data.EntityMappers
{
    public class UserEntityConfig
    {
        public UserEntityConfig(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.Name)
               .IsRequired()
            .HasMaxLength(255);
			
            builder.HasMany(x => x.AssignedTickets).WithOne(r => r.AssignedTo).HasForeignKey(x => x.AssignedToId);


		}
	}
}
