using System;
using System.Collections.Generic;
using Reports.Core.Domain;
using Reports.Core.Dto;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class VacationDao : DefaultDao<Vacation>, IVacationDao
    {
        public VacationDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        public IList<VacationDto> GetDocuments(
                UserRole role,
                int departmentId,
                int positionId,
                int vacationTypeId,
                int requestStatusId,
                DateTime? beginDate,
                DateTime? endDate)
        {
            return  new List<VacationDto>();
        }
    }
}