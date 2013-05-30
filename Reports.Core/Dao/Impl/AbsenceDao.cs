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
               int sortedBy,
               bool? sortDescending)
        {
//            string sqlQuery =
//                string.Format(@"select v.Id as Id,
//                         u.Id as UserId,
//                         'Неявка '+ u.Name + case when [DeleteDate] is not null then N' ({0})' else '' end as Name,
//                         v.[CreateDate] as Date    
//            from [dbo].[Absence] v
//            inner join [dbo].[Users] u on u.Id = v.UserId",DeleteRequestText);
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
                requestStatusId, beginDate, endDate, sqlQuery,sortedBy,sortDescending);
        }
    }
}