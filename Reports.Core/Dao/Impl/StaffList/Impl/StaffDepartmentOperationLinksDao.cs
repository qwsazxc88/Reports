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
        /// Список связей с операциями.
        /// </summary>
        /// <param name="OperGroupId">Id группы операций.</param>
        /// <returns></returns>
        public IList<StaffDepartmentOperationLinksDto> GetOperationGroupLinks(int OperGroupId)
        {
            IQuery query = Session.CreateSQLQuery(@"SELECT C.Id, A.Id as OperationId, A.Name as OperationName, isnull(C.IsUsed, 0) as IsLink
                                                    FROM (SELECT *, 1 as Link FROM StaffDepartmentOperations) as A
                                                    INNER JOIN (SELECT *, 1 as Link FROM StaffDepartmentOperationGroups) as B ON B.Link = A.Link
                                                    LEFT JOIN StaffDepartmentOperationLinks as C ON C.OperationId = A.Id and C.OperGroupId = B.Id
                                                    WHERE B.Id = " + OperGroupId.ToString())
                .AddScalar("Id", NHibernateUtil.Int32)
                .AddScalar("OperationId", NHibernateUtil.Int32)
                .AddScalar("OperationName", NHibernateUtil.String)
                .AddScalar("IsLink", NHibernateUtil.Boolean);

            return query.SetResultTransformer(Transformers.AliasToBean<StaffDepartmentOperationLinksDto>()).List<StaffDepartmentOperationLinksDto>();
        }
    }
}
