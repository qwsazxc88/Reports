using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reports.Core.Domain;
using Reports.Core.Dto;
using Reports.Core.Services;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;

namespace Reports.Core.Dao.Impl
{
    public class ChildVacationDao : DefaultDao<ChildVacation>, IChildVacationDao
    {
        public ChildVacationDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
        public IList<VacationDto> GetDocuments(
            int userId,
            UserRole role,
            int departmentId,
            int positionId,
            int vacationTypeId,
            int requestStatusId,
            DateTime? beginDate,
            DateTime? endDate,
            string userName, 
            int sortedBy,
            bool? sortDescending,
            string Number)
        {
            string sqlQuery = string.Format(sqlSelectForListChildVacation,
                                DeleteRequestText,
                                string.Empty,
                                "v.[CreateDate]",
                                "Отпуск по уходу за ребенком",
                                "[dbo].[ChildVacation]",
                                 "v.[BeginDate]",
                                 "v.[EndDate]");

            return GetDefaultDocuments(userId, role, departmentId,
                positionId, vacationTypeId,
                requestStatusId, beginDate, endDate,userName, 
                sqlQuery, sortedBy, sortDescending, Number);
        }

        public override IQuery CreateQuery(string sqlQuery)
        {
            return Session.CreateSQLQuery(sqlQuery).
                AddScalar("Id", NHibernateUtil.Int32).
                AddScalar("UserId", NHibernateUtil.Int32).
                AddScalar("Name", NHibernateUtil.String).
                AddScalar("Date", NHibernateUtil.DateTime).
                AddScalar("BeginDate", NHibernateUtil.DateTime).
                AddScalar("EndDate", NHibernateUtil.DateTime).
                AddScalar("Number", NHibernateUtil.Int32).
                AddScalar("UserName", NHibernateUtil.String).
                AddScalar("RequestType", NHibernateUtil.String).
                AddScalar("RequestStatus", NHibernateUtil.String).
                AddScalar("IsOriginalReceived", NHibernateUtil.Boolean).
                AddScalar("IsOriginalRequestReceived", NHibernateUtil.Boolean).
                AddScalar("OriginalReceivedDate", NHibernateUtil.DateTime).
                AddScalar("OriginalRequestReceivedDate", NHibernateUtil.DateTime).
                AddScalar("Dep7Name",NHibernateUtil.String).
                AddScalar("Dep3Name",NHibernateUtil.String).
                AddScalar("Position",NHibernateUtil.String).
                AddScalar("ManagerName",NHibernateUtil.String);
        }

        public IList<ChildVacation> LoadForIdsList(List<int> ids)
        {
            if (ids.Count == 0)
                return new List<ChildVacation>();
            ICriteria criteria = Session.CreateCriteria(typeof(ChildVacation));
            criteria.Add(Restrictions.In("Id", ids));
            return criteria.List<ChildVacation>();
        }
    }
}
