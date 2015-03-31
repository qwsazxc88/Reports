using System;
using System.Collections.Generic;
using Reports.Core.Domain;
using Reports.Core.Dto;

namespace Reports.Core.Dao
{
    public interface ISurchargeDao : IDao<Surcharge>
    {
        IList<SurchargeDto> GetDocuments(
            int userId,
            UserRole role,
            int departmentId,
            int statusId,
            DateTime? beginDate,
            DateTime? endDate,
            string userName,
            int sortedBy,
            bool? sortDescending,
            string Number);
        void AddDocument(int userId, decimal sum, int creatorId, DateTime editDate, int missionReportId);
    }
}