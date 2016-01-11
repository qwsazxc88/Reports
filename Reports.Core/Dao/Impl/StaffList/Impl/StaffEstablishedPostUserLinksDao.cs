using System.Collections.Generic;
using Reports.Core.Dto;
using Reports.Core.Domain;
using Reports.Core.Services;
using NHibernate.Transform;
using NHibernate;
using NHibernate.Criterion;
using System;
using System.Linq;
using NHibernate.Linq;


namespace Reports.Core.Dao.Impl
{
    /// <summary>
    /// Связи штатных единиц с сотрудниками (штатная расстановка)
    /// </summary>
    public class StaffEstablishedPostUserLinksDao : DefaultDao<StaffEstablishedPostUserLinks>, IStaffEstablishedPostUserLinksDao
    {
        public StaffEstablishedPostUserLinksDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
        /// <summary>
        /// Достаем строку штатной расстановки по привязке к документу.
        /// </summary>
        /// <param name="DocId">Id документа.</param>
        /// <param name="ReserveType">Тип резервирования (StaffReserveTypeEnum)</param>
        /// <returns></returns>
        public StaffEstablishedPostUserLinks GetPostUserLinkByDocId(int DocId, int ReserveType)
        {
            return Session.Query<StaffEstablishedPostUserLinks>().Where(x => x.DocId == DocId && x.ReserveType == ReserveType).FirstOrDefault();
        }

        /// <summary>
        /// Достаем строку штатной расстановки по Id сотрудника.
        /// </summary>
        /// <param name="UserId">Id документа.</param>
        /// <returns></returns>
        public StaffEstablishedPostUserLinks GetPostUserLinkByUserId(int UserId)
        {
            return Session.Query<StaffEstablishedPostUserLinks>().Where(x => x.User.Id == UserId).FirstOrDefault();
        }
    }
}
