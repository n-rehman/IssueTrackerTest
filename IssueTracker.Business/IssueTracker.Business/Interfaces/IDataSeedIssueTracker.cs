using IssueTracker.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssueTracker.Business.Interfaces
{
	public interface IDataSeedIssueTracker
	{
		void Initialize(IssueTrackerDbContext dbContext);
	}
}
