using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Transform;
using Reports.Core.Domain;
using Reports.Core.Dto;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class ClearanceChecklistDao : DefaultDao<ClearanceChecklist>, IClearanceChecklistDao
    {
        public ClearanceChecklistDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        public IList<VacationDto> GetDocuments(
            int userId,
            UserRole role,
            int departmentId,
            int positionId,
            int typeId,
            int statusId,
            DateTime? beginDate,
            DateTime? endDate,
            string userName,
            int sortedBy,
            bool? sortDescending
            )
        {
            // TODO Replace this stub with necessary code
            string sqlQuery =
                string.Format(sqlSelectForListClearanceChecklist,
                                DeleteRequestText,
                                "v.[CreateDate]",
                                "Обходной лист",
                                "[dbo].[ClearanceChecklist]",
                                "'" + DateTime.MinValue.ToShortDateString() + "'",
                                "'" + DateTime.MinValue.ToShortDateString() + "'",
                                typeId
                );
            return GetDefaultDocuments(userId, role, departmentId,
                positionId, typeId,
                statusId, beginDate, endDate, userName,
                sqlQuery, sortedBy, sortDescending);
            //return new List<VacationDto>();
        }
    }
}
