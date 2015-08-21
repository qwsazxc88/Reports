using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reports.Core.Domain;
using Reports.Core.Dao;
using Reports.Core.Services;
using NHibernate.Linq;
namespace Reports.Core.Dao.Impl
{
    public class StaffMovementsDao: DefaultDao<StaffMovements>,IStaffMovementsDao
    {
        public StaffMovementsDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
            
        }
        public IList<StaffMovements> GetDocuments(int DepartmentId, string UserName, int Number, int Status)
        {
            var query=Session.Query<StaffMovements>();
            if (DepartmentId > 0)
                query.Where(x => x.SourceDepartment.Id == DepartmentId);
            if(!String.IsNullOrWhiteSpace(UserName))
                query.Where(x=>x.Creator.Name.Contains(UserName));
            if(Status>=0)
                query.Where(x=>x.Status.Id==Status);
            if(Number>0)
                query.Where(x=>x.Id==Number);
            return query.ToList();
        }
    }
}
