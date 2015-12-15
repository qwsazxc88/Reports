using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using NHibernate.Linq;
using NHibernate;
using Reports.Core.Services;
using Reports.Core.Domain;
using Reports.Core.Dto;
using Reports.Core.Dao;
namespace Reports.Core.Dao.Impl
{
    public class StaffMovementsFactDao: DefaultDao<StaffMovementsFact>, IStaffMovementsFactDao
    {
        public StaffMovementsFactDao(ISessionManager sessionManager)
            : base(sessionManager)
        {         
        }        
    }
}
