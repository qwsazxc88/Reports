using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reports.Core.Domain;
using Reports.Core.Services;
namespace Reports.Core.Dao.Impl
{
    public class refVacationReturnStatusDao: DefaultDao<refVacationReturnStatus>, IrefVacationReturnStatusDao
    {
        public refVacationReturnStatusDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}
