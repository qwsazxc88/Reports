using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reports.Core.Domain;
using Reports.Core.Services;
namespace Reports.Core.Dao.Impl
{
    public class refVacationReturnTypesDao: DefaultDao<refVacationReturnTypes> , IrefVacationReturnTypesDao
    {
        public refVacationReturnTypesDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}
