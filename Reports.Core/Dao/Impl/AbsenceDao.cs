using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Transform;
using Reports.Core.Domain;
using Reports.Core.Dto;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class AbsenceDao : DefaultDao<Absence>, IAbsenceDao
    {
        public AbsenceDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
        public IList<VacationDto> GetDocuments(
                int userId, 
               UserRole role,
               int departmentId,
               int positionId,
               int absenceTypeId,
               int requestStatusId,
               DateTime? beginDate,
               DateTime? endDate,
               string userName, 
               int sortedBy,
               bool? sortDescending)
        {
            string sqlQuery = string.Format(sqlSelectForList,
                                DeleteRequestText,
                                "dbo.AbsenceType",
                                "v.[CreateDate]",
                                "Неявка",
                                "[dbo].[Absence]",
                                "v.[BeginDate]",
                                "v.[EndDate]");


            return GetDefaultDocuments(userId, role, departmentId, 
                positionId, absenceTypeId,
                requestStatusId, beginDate, endDate,userName,
                sqlQuery,sortedBy,sortDescending);
        }
        public IList<BeginEndDateDto> LoadForUserAndPeriod(DateTime beginDate, DateTime endDate, int userId)
        {
            string sqlQuery =
                @"select v.BeginDate as BeginDate,
                         v.EndDate as EndDate
                from [dbo].[Absence] v
                where ((v.BeginDate between :beginDate and :endDate) or
                                 (v.EndDate between :beginDate and :endDate) or 
                                 (:beginDate between v.BeginDate and v.EndDate) or
                                 (:endDate between v.BeginDate and v.EndDate))
                          and v.DeleteDate is null 
                          and v.SendTo1C is not null
                          and v.UserId = :userId";
            IQuery query = Session.CreateSQLQuery(sqlQuery).
              AddScalar("BeginDate", NHibernateUtil.DateTime).
              AddScalar("EndDate", NHibernateUtil.DateTime).
              SetDateTime("beginDate", beginDate).
              SetDateTime("endDate", endDate).
              SetInt32("userId", userId);
            return query.SetResultTransformer(Transformers.AliasToBean(typeof(BeginEndDateDto))).List<BeginEndDateDto>();
        }
    }
}