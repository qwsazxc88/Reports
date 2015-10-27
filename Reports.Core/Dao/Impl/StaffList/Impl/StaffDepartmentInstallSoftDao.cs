using System.Collections.Generic;
using Reports.Core.Dto;
using Reports.Core.Domain;
using Reports.Core.Services;
using NHibernate.Transform;
using NHibernate;
using NHibernate.Criterion;

namespace Reports.Core.Dao.Impl
{
    /// <summary>
    /// Справочник банковского ПО.
    /// </summary>
    public class StaffDepartmentInstallSoftDao : DefaultDao<StaffDepartmentInstallSoft>, IStaffDepartmentInstallSoftDao
    {
        public StaffDepartmentInstallSoftDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        /// <summary>
        /// Список Установленного ПО.
        /// </summary>
        /// <returns></returns>
        public IList<StaffDepartmentInstallSoftDto> GetInstallSoft()
        {
            IQuery query = Session.CreateSQLQuery("SELECT Id as sId, Name as sName FROM StaffDepartmentInstallSoft")
                .AddScalar("sId", NHibernateUtil.Int32)
                .AddScalar("sName", NHibernateUtil.String);

            return query.SetResultTransformer(Transformers.AliasToBean<StaffDepartmentInstallSoftDto>()).List<StaffDepartmentInstallSoftDto>();
        }
    }
}
