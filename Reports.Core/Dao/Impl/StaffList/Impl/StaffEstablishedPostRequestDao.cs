using System;
using Reports.Core.Domain;
using Reports.Core.Services;
using Reports.Core.Dto;
using System.Collections.Generic;
using NHibernate.Transform;
using NHibernate;
using NHibernate.Criterion;

namespace Reports.Core.Dao.Impl
{
    /// <summary>
    /// Заявки по штатным единицам.
    /// </summary>
    public class StaffEstablishedPostRequestDao : DefaultDao<StaffEstablishedPostRequest>, IStaffEstablishedPostRequestDao
    {
        public StaffEstablishedPostRequestDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        /// <summary>
        /// Достаем Id действующей заявки для данной штатной единицы.
        /// </summary>
        /// <param name="Id">Id штатной единицы.</param>
        /// <returns></returns>
        public int GetCurrentRequestId(int SEPId)
        {
            return Session.CreateSQLQuery(@"SELECT A.Id
                                            FROM StaffEstablishedPostRequest as A
                                            WHERE A.IsUsed = 1 and A.SEPId = :SEPId")
                .AddScalar("Id", NHibernateUtil.Int32)
                .SetInt32("SEPId", SEPId)
                .UniqueResult<int>();
        }
    }
}
