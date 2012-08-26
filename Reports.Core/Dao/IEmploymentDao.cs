using System;
using System.Collections.Generic;
using Reports.Core.Domain;
using Reports.Core.Dto;

namespace Reports.Core.Dao
{
    public interface IEmploymentDao : IDao<Employment>
    {
        IList<VacationDto> GetDocuments(
            UserRole role,
            //int departmentId,
            int positionId,
            int typeId,
            int graphicTypeId,
            int requestStatusId,
            DateTime? beginDate,
            DateTime? endDate);
    }
}