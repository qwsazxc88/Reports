using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reports.Core.Domain;
using Reports.Core;
using Reports.Core.Services;
namespace Reports.Core.Dao.Impl
{
    public class BugReportDao: DefaultDao<BugReport>, IBugReportDao
    {
        public BugReportDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}
