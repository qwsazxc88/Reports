using System;
using System.Collections.Generic;
using Reports.Core.Domain;
using Reports.Core.Dto;

namespace Reports.Core.Dao
{
    public interface IMissionPurchaseBookDocumentDao : IDao<MissionPurchaseBookDocument>
    {
        IList<MissionPurchaseBookDocDto> GetDocuments(
            UserRole role,
            DateTime? beginDate,
            DateTime? endDate,
            int sortBy,
            bool? sortDescending);
    }
}