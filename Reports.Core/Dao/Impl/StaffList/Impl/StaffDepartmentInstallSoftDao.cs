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
        public IList<IdNameWithOldNameDto> GetInstallSoft()
        {
            IQuery query = Session.CreateSQLQuery("SELECT Id, Name, Name as OldName FROM StaffDepartmentInstallSoft")
                .AddScalar("Id", NHibernateUtil.Int32)
                .AddScalar("Name", NHibernateUtil.String)
                .AddScalar("OldName", NHibernateUtil.String);

            return query.SetResultTransformer(Transformers.AliasToBean<IdNameWithOldNameDto>()).List<IdNameWithOldNameDto>();
        }
    }
}
