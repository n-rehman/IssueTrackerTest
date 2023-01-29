using IssueTracker.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IssueTracker.Data.EntityMappers
{
    public class TicketTypeEntityConfig
    {
        public TicketTypeEntityConfig(EntityTypeBuilder<TicketType> builder)
        {
            builder.HasOne<Project>()
                .WithMany()
                .HasForeignKey(x => x.ProjectId)
                .IsRequired();

            builder.Property(x => x.Name)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(x => x.Color)
                .HasMaxLength(25)
                .IsRequired();
        }
    }
}
