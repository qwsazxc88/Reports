using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reports.Core.Domain;
using Reports.Core.Services;
namespace Reports.Core.Dao.Impl
{
    public class VacationReturnDao: DefaultDao<VacationReturn>, IVacationReturnDao
    {
        public VacationReturnDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}
