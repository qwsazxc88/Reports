using System.Collections.Generic;
using Reports.Core.Domain;
using Reports.Core.Dto;

namespace Reports.Core.Dao
{
	public interface IDocumentDao : IDao<Document>
	{
	    IList<DocumentDto> GetDocumentsForOwner(int userId);
	    DocumentAndAttachmentDto GetDocumentAndAttachmentForId(int documentId);
	    IList<DocumentDto> GetDocumentsListForManager(int managerId,
            UserRole managerRole, bool showNew, int? ownerId, int subtypeId);
	    //IList<CalcListDto> GetDocumentByType(ReportType documentType,int userId);
	    //IList<CalcListDto> FindByFilter(CalcListFilter filter, bool isAdmin, int daysDelay, out int count);
	    //int MaxDateDiffInDaysForNotAdminUser { get; }
	}
}
