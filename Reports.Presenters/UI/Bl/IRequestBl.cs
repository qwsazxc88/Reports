using System;
using Reports.Core.Dto;
using Reports.Core.Enum;
using Reports.Presenters.UI.ViewModel;

namespace Reports.Presenters.UI.Bl
{
    public interface IRequestBl : IBaseBl
    {
        CreateRequestModel GetCreateRequestModel(int? userId);
        VacationListModel GetVacationListModel();
        void SetVacationListModel(VacationListModel model, bool hasError);

        AllRequestListModel GetAllRequestListModel();
        void SetAllRequestListModel(AllRequestListModel model, bool hasError);

        VacationEditModel GetVacationEditModel(int id, int userId);
        bool SaveVacationEditModel(VacationEditModel model, UploadFileDto fileDto, out string error);
        void ReloadDictionariesToModel(VacationEditModel model);

        int GetOtherRequestCountsForUserAndDates(DateTime beginDate,
                                    DateTime endDate, int userId, int vacationId);

        RequestCommentsModel GetCommentsModel(int id, int typeId);
        bool SaveComment(SaveCommentModel model);

        AbsenceListModel GetAbsenceListModel();
        void SetAbsenceListModel(AbsenceListModel model, bool hasError);

        AbsenceEditModel GetAbsenceEditModel(int id, int userId);
        void ReloadDictionariesToModel(AbsenceEditModel model);
        bool SaveAbsenceEditModel(AbsenceEditModel model, out string error);

        SicklistListModel GetSicklistListModel();
        void SetSicklistListModel(SicklistListModel model, bool hasError);
        SicklistEditModel GetSicklistEditModel(int id, int userId);
        void ReloadDictionariesToModel(SicklistEditModel model);
        bool SaveSicklistEditModel(SicklistEditModel model,UploadFileDto fileDto, out string error);

        HolidayWorkListModel GetHolidayWorkListModel();
        void SetHolidayWorkListModel(HolidayWorkListModel model, bool hasError);
        HolidayWorkEditModel GetHolidayWorkEditModel(int id, int userId);
        void ReloadDictionariesToModel(HolidayWorkEditModel model);
        bool SaveHolidayWorkEditModel(HolidayWorkEditModel model,out string error);

        MissionListModel GetMissionListModel();
        void SetMissionListModel(MissionListModel model, bool hasError);
        MissionEditModel GetMissionEditModel(int id, int userId);
        void ReloadDictionariesToModel(MissionEditModel model);
        bool SaveMissionEditModel(MissionEditModel model, out string error);

        DismissalListModel GetDismissalListModel();
        void SetDismissalListModel(DismissalListModel model, bool hasError);
        DismissalEditModel GetDismissalEditModel(int id, int userId);
        bool SaveDismissalEditModel(DismissalEditModel model, UploadFileDto fileDto, out string error);
        void ReloadDictionariesToModel(DismissalEditModel model);

        TimesheetCorrectionListModel GetTimesheetCorrectionListModel();
        void SetTimesheetCorrectionListModel(TimesheetCorrectionListModel model, bool hasError);
        TimesheetCorrectionEditModel GetTimesheetCorrectionEditModel(int id, int userId);
        void ReloadDictionariesToModel(TimesheetCorrectionEditModel model);
        bool SaveTimesheetCorrectionEditModel(TimesheetCorrectionEditModel model, out string error);

        EmploymentListModel GetEmploymentListModel();
        void SetEmploymentListModel(EmploymentListModel model, bool hasError);
        EmploymentEditModel GetEmploymentEditModel(int id, int userId);
        void ReloadDictionariesToModel(EmploymentEditModel model);
        bool SaveEmploymentEditModel(EmploymentEditModel model, /*UploadFilesDto filesDto,*/ out string error);

        AttachmentModel GetFileContext(int id/*, int typeId*/);
        RequestAttachmentsModel GetAttachmentsModel(int id, RequestAttachmentTypeEnum typeId);
        bool SaveAttachment(SaveAttacmentModel model);
        bool DeleteAttachment(DeleteAttacmentModel model);
        int GetAttachmentsCount(int entityId,RequestAttachmentTypeEnum typeId);

        /*void CreateVacationOrder(int id, string templatePath, string filePath);

        VacationPrintModel GetVacationPrintModel(int id);*/
        AttachmentModel GetPrintFormFileContext(int id, RequestPrintFormTypeEnum typeId);

        void GetAcceptRequestsModel(AcceptRequestsModel model);
        void SetAcceptDate(AcceptRequestsModel model);

        DepartmentTreeModel GetDepartmentTreeModel(int departmentId);
        DepartmentChildrenDto GetChildren(int parentId, int level);
    }
}