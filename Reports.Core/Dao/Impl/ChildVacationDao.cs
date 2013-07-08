using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reports.Core.Domain;
using Reports.Core.Dto;
using Reports.Core.Services;

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
            int sortedBy,
            bool? sortDescending)
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
                requestStatusId, beginDate, endDate, sqlQuery, sortedBy, sortDescending);
        }
    }
}
