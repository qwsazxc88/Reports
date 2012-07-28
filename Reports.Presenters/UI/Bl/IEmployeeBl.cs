using System.Collections.Generic;
using Reports.Core.Dto;
using Reports.Presenters.UI.ViewModel;

namespace Reports.Presenters.UI.Bl
{
    public interface IEmployeeBl : IBaseBl
    {
        EmployeeDocumentListModel GetModel(int? ownerId, bool? viewHeader,
            bool? showNew, int? subtypeId);
        EmployeeDocumentEditModel GetDocumentEditModel(int id,int ownerId);
        DocumentCommentsModel GetCommentsModel(int documentId);
        void SetStaticDataToModel(EmployeeDocumentEditModel model);
        bool SaveEditDocument(EmployeeDocumentEditModel model,
                UploadFileDto fileDto, out string Error);
        AttachmentModel GetFileContext(int attachmentId);
        bool SendToBilling(SendToBillingModel model);

        IList<IdNameDto> GetSubTypes(int typeId);
        bool SaveComment(SaveCommentModel model);
        void GetEmployeeListModel(EmployeeListModel model);
        IList<IdNameDto> GetTimesheetStatusesList();
        void GetTimesheetListModel(TimesheetListModel model);
        //void SetTimesheetsHours(TimesheetListModel model);
        //void SetTimesheet(TimesheetEditModel model);

        //void GetTimesheetEditModel(TimesheetEditModel model);
        void GetEditDayModel(EditDayModel model);

        EmployeeTimesheetListModel GetEmployeeTimesheetListModel();

    }
}