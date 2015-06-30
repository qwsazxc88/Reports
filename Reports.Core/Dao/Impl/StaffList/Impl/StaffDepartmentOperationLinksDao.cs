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
    /// Операции проводимые подразделением.
    /// </summary>
    public class StaffDepartmentOperationLinksDao : DefaultDao<StaffDepartmentOperationLinks>, IStaffDepartmentOperationLinksDao
    {
        public StaffDepartmentOperationLinksDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        /// <summary>
        /// Достаем список операций для подразделения по заявке.
        /// </summary>
        /// <param name="DMDetailId">Id текущей заявки.</param>
        /// <returns></returns>
        public IList<DepOperationDto> GetDepartmentOperationLinks(int DMDetailId)
        {
            IQuery query = Session.CreateSQLQuery(@"SELECT B.Id, A.Id as OperationId, A.Name as OperationName, cast(case when B.Id is null then 0 else 1 end as bit) as IsUsed
                                                    FROM StaffDepartmentOperations as A
                                                    LEFT JOIN StaffDepartmentOperationLinks as B ON B.OperationId = A.id" +
                                                  (DMDetailId == 0 ? "" : " WHERE B.DMDetailId = " + DMDetailId.ToString()))
                .AddScalar("Id", NHibernateUtil.Int32)
                .AddScalar("OperationId", NHibernateUtil.Int32)
                .AddScalar("OperationName", NHibernateUtil.String)
                .AddScalar("IsUsed", NHibernateUtil.Boolean);

            return query.SetResultTransformer(Transformers.AliasToBean<DepOperationDto>()).List<DepOperationDto>();
        }
    }
}
