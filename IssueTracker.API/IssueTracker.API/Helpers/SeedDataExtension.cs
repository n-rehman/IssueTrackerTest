

using IssueTracker.Business.Interfaces;
using IssueTracker.Data;

namespace IssueTracker.API.Helpers
{
    public static class SeedDataExtension
    {
        public static void SeedData(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<IssueTrackerDbContext>();
                var seedDataService = scope.ServiceProvider.GetRequiredService<IDataSeedIssueTracker>();

                seedDataService.Initialize(dbContext);
            }
        }
    }
}
