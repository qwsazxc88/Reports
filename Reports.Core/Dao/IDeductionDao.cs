using System;
using System.Collections.Generic;
using Reports.Core.Domain;
using Reports.Core.Dto;

namespace Reports.Core.Dao
{
    public interface IDeductionDao : IDao<Deduction>
    {
        IList<DeductionDto> GetDocuments(
            int userId,
            UserRole role,
            int departmentId,
            //int positionId,
            int typeId,
            int statusId,
            DateTime? beginDate,
            DateTime? endDate,
            string userName,
            //string sqlQuery,
            int sortedBy,
            bool? sortDescending);

        DateTime? GetMinDeductionPeriod();
    }
}