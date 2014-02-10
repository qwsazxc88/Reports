using System.Collections.Generic;
using Reports.Core.Domain;
using Reports.Core.Dto;

namespace Reports.Core.Dao
{
    public interface IMissionPurchaseBookRecordDao : IDao<MissionPurchaseBookRecord>
    {
        IList<MissionPurchaseBookRecordDto> GetRecordsForDocumentId(int documentId);
        IList<MissionPurchaseBookRecord> GetRecordsForCost(int costId);

        IList<MissionPbRecordListDto> GetDocuments(UserRole role,
                                                   int departmentId,
                                                   string userName,
                                                   int sortBy,
                                                   bool? sortDescending);
    }
}