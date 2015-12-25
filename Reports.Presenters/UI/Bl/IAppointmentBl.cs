using Reports.Core.Dto;
using Reports.Core.Enum;
using Reports.Presenters.UI.ViewModel;

namespace Reports.Presenters.UI.Bl
{
    public interface IAppointmentBl : IBaseBl
    {
        AppointmentListModel GetAppointmentListModel();
        bool CheckUserDismissal(int userid);
        void SetAppointmentListModel(AppointmentListModel model, bool hasError);
        void SetAppointmentReportsListModel(AppointmentListModel model, bool hasError);
        AppointmentEditModel GetAppointmentEditModel(int id, int? managerId);
        AppointmentListModel GetAppointmentReportListModel();
        CommentsModel GetCommentsModel(int id, RequestTypeEnum typeId);
        bool SaveComment(SaveCommentModel model, RequestTypeEnum type);
        NoteModel GetNoteModel(int id);
        void ReloadDictionaries(AppointmentEditModel model);
        bool CheckDepartment(AppointmentEditModel model, out int level);
        int GetRequeredDepartmentLevel();
        bool SaveAppointmentEditModel(AppointmentEditModel model, out string error);
        int CreateReportForOldAppointment(int id);
        int SendEmailForAppointmentManager(int AppId);
        AppointmentReportEditModel GetAppointmentReportEditModel(int id);
        void ReloadDictionariesToModel(AppointmentReportEditModel model);
        bool SaveAppointmentReportEditModel(AppointmentReportEditModel model, UploadFileDto fileDto, out string error);
        int CreateNewReport(int otherReportId);
        PrintLoginFormModel GetPrintLoginFormModel(int id);
        object CopyAppointmentReport(int AppointmentNumber, int AppointmentReportId);
        AttachmentModel GetFileContext(int id /*,int typeId*/);
        bool DeleteAttachment(DeleteAttacmentModel model);
        ViewModel.Employment2.CreateCandidateModel FillCreateCandidateModelByReportId(int id);
        AppointmentSelectManagerModel GetSelectManagerModel();
    }
}