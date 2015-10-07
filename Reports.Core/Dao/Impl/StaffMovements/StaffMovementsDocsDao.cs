using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reports.Core.Dao;
using Reports.Core.Domain;
using Reports.Core.Services;
namespace Reports.Core.Dao.Impl
{
    public class StaffMovementsDocsDao:DefaultDao<StaffMovementsDocs>, IStaffMovementsDocsDao
    {
        public StaffMovementsDocsDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}
