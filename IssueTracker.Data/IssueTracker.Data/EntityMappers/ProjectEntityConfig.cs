using IssueTracker.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IssueTracker.Data.EntityMappers
{
    public class ProjectEntityConfig
    {
        public ProjectEntityConfig(EntityTypeBuilder<Project> builder)
        {
            builder.Property(x => x.Title)
               .IsRequired()
               .HasMaxLength(255);
        }
    }
}
