using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reports.Core.Domain;
using Reports.Core.Dao;
using Reports.Core.Services;
namespace Reports.Core.Dao.Impl
{
    public class refStaffMovementsStatusDao: DefaultDao<refStaffMovementsStatus>, IrefStaffMovementsStatusDao
    {
        public refStaffMovementsStatusDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}
