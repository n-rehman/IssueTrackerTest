using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssueTracker.Business.Response
{
    public class ApiResponseMessage
    {
        public ObjectResult ApiResponseResult { get; set; }
    }
}
