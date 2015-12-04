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
    /// Связь надбавок с штатными единицами.
    /// </summary>
    public class StaffEstablishedPostChargeLinksDao : DefaultDao<StaffEstablishedPostChargeLinks>, IStaffEstablishedPostChargeLinksDao
    {
        public StaffEstablishedPostChargeLinksDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        /// <summary>
        /// Достаем список надбавок для заявки к штатной единице.
        /// </summary>
        /// <param name="Id">Id заявки штатного расписания.</param>
        /// <returns></returns>
        public IList<StaffEstablishedPostChargeLinksDto> GetChargesForRequests(int Id)
        {
            IQuery query = Session.CreateSQLQuery(@"SELECT B.Id, A.Id as ChargeId, A.Name as ChargeName, B.SEPRequestId, B.SEPId, B.Amount, D.Name as UnitName, B.IsUsed, A.IsNeeded, B.ActionId,
                                                           isnull((SELECT top 1 AA.Amount FROM StaffEstablishedPostChargeLinks as AA
																    INNER JOIN StaffEstablishedPostRequest as BB ON BB.Id = AA.SEPRequestId and BB.DateAccept is not null and BB.BeginAccountDate < getdate()
																    WHERE AA.StaffExtraChargeId = B.StaffExtraChargeId), 0) as AmountPrev
                                                    FROM StaffExtraCharges as A
                                                    LEFT JOIN StaffEstablishedPostChargeLinks as B ON B.StaffExtraChargeId = A.Id and B.SEPRequestId = :SEPRequestId
                                                    LEFT JOIN StaffEstablishedPostRequest as C ON C.Id = B.SEPRequestId
                                                    LEFT JOIN StaffUnitReference as D ON D.Id = A.UnitId
                                                    WHERE A.IsPostOnly = 1")
                .AddScalar("Id", NHibernateUtil.Int32)
                .AddScalar("ChargeId", NHibernateUtil.Int32)
                .AddScalar("ChargeName", NHibernateUtil.String)
                .AddScalar("SEPRequestId", NHibernateUtil.Int32)
                .AddScalar("SEPId", NHibernateUtil.Int32)
                .AddScalar("Amount", NHibernateUtil.Decimal)
                .AddScalar("AmountPrev", NHibernateUtil.Decimal)
                .AddScalar("UnitName", NHibernateUtil.String)
                .AddScalar("IsUsed", NHibernateUtil.Boolean)
                .AddScalar("IsNeeded", NHibernateUtil.Boolean)
                .AddScalar("ActionId", NHibernateUtil.Int32)
                .SetInt32("SEPRequestId", Id);

            return query.SetResultTransformer(Transformers.AliasToBean<StaffEstablishedPostChargeLinksDto>()).List<StaffEstablishedPostChargeLinksDto>();
        }

        /// <summary>
        /// Достаем список надбавок для штатной единицы.
        /// </summary>
        /// <param name="Id">Id штатной единицы.</param>
        /// <returns></returns>
        public IList<StaffEstablishedPostChargeLinksDto> GetChargesForEstablishedPosts(int Id)
        {
            IQuery query = Session.CreateSQLQuery(@"SELECT B.Id, A.Id as ChargeId, A.Name as ChargeName, B.SEPRequestId, B.SEPId, B.Amount, D.Name as UnitName, B.IsUsed, A.IsNeeded, B.ActionId,
                                                            isnull((SELECT top 1 AA.Amount FROM StaffEstablishedPostChargeLinks as AA
																    INNER JOIN StaffEstablishedPostRequest as BB ON BB.Id = AA.SEPRequestId and BB.DateAccept is not null and BB.BeginAccountDate < getdate()
																    WHERE AA.StaffExtraChargeId = B.StaffExtraChargeId), 0) as AmountPrev
                                                    FROM StaffExtraCharges as A
                                                    LEFT JOIN StaffEstablishedPostChargeLinks as B ON B.StaffExtraChargeId = A.Id
                                                    LEFT JOIN StaffEstablishedPost as C ON C.Id = B.SEPId and C.Id = :Id
                                                    LEFT JOIN StaffUnitReference as D ON D.Id = A.UnitId
                                                    WHERE A.IsPostOnly = 1")
                .AddScalar("Id", NHibernateUtil.Int32)
                .AddScalar("ChargeId", NHibernateUtil.Int32)
                .AddScalar("ChargeName", NHibernateUtil.String)
                .AddScalar("SEPRequestId", NHibernateUtil.Int32)
                .AddScalar("SEPId", NHibernateUtil.Int32)
                .AddScalar("Amount", NHibernateUtil.Decimal)
                .AddScalar("AmountPrev", NHibernateUtil.Decimal)
                .AddScalar("UnitName", NHibernateUtil.String)
                .AddScalar("IsUsed", NHibernateUtil.Boolean)
                .AddScalar("IsNeeded", NHibernateUtil.Boolean)
                .AddScalar("ActionId", NHibernateUtil.Int32)
                .SetInt32("Id", Id);

            return query.SetResultTransformer(Transformers.AliasToBean<StaffEstablishedPostChargeLinksDto>()).List<StaffEstablishedPostChargeLinksDto>();
        }
    }
}
