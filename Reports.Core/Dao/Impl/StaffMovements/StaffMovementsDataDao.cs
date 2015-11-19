using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reports.Core.Domain;
using Reports.Core.Dao;
using Reports.Core.Services;
namespace Reports.Core.Dao.Impl
{
    public class StaffMovementsDataDao:DefaultDao<StaffMovementsData>,IStaffMovementsDataDao
    {
        public StaffMovementsDataDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}
