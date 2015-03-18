using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class EmploymentEducationTypeDao : DefaultDao<EmploymentEducationType>, IEmploymentEducationTypeDao
    {
        public EmploymentEducationTypeDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}
