using IssueTracker.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IssueTracker.Data.EntityMappers
{
    public class CommentEntityConfig
    {
        public CommentEntityConfig(EntityTypeBuilder<Comment> builder)
        {
           
            builder.Property(x => x.Text)
                .IsRequired();

			//builder.HasOne(x => x.RelatedToTicket).WithMany(r => r.Comments).HasForeignKey(x => x.TicketId);

		}
	}
}
