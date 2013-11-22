using System;
using Reports.Core.Dto;
using Reports.Core.Enum;
using Reports.Presenters.UI.ViewModel;

namespace Reports.Presenters.UI.Bl
{
    public interface IRequestBl : IBaseBl
    {
        CreateRequestModel GetCreateRequestModel(int? userId);
        DepartmentChildrenDto GetUsersForDepartment(int departmentId);

        VacationListModel GetVacationListModel();
        void SetVacationListModel(VacationListModel model, bool hasError);

        AllRequestListModel GetAllRequestListModel();
        void SetAllRequestListModel(AllRequestListModel model, bool hasError);

        VacationEditModel GetVacationEditModel(int id, int userId);
        bool SaveVacationEditModel(VacationEditModel model, UploadFileDto fileDto, out string error);
        void ReloadDictionariesToModel(VacationEditModel model);

        int GetOtherRequestCountsForUserAndDates(DateTime beginDate,
                                    DateTime endDate, int userId, int vacationId, bool isChildVacantion);

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

        void GetConstantListModel(ConstantListModel model);
        void GetConstantEditModel(ConstantEditModel model);
        void ReloadDictionariesToModel(ConstantEditModel model);
        bool SaveConstantEditModel(ConstantEditModel model, out string error);

        ChildVacationListModel GetChildVacationListModel();
        void SetChildVacationListModel(ChildVacationListModel model, bool hasError);
        ChildVacationEditModel GetChildVacationEditModel(int id, int userId);
        void ReloadDictionariesToModel(ChildVacationEditModel model);
        bool SaveChildVacationEditModel(ChildVacationEditModel model, UploadFileDto fileDto, out string error);

        DeductionListModel GetDeductionListModel();
        void SetDeductionListModel(DeductionListModel model, bool hasError);
        DeductionEditModel GetDeductionEditModel(int id);
        void SetDeductionUserInfoModel(DeductionUserInfoModel model, int userId);
        void ReloadDictionariesToModel(DeductionEditModel model);
        bool SaveDeductionEditModel(DeductionEditModel model, out string error);

        TerraGraphicsSetShortNameModel SetShortNameModel();
        TerraPointChildrenDto GetTerraPointChildren(int parentId, int level);
        TerraPointShortNameDto GetTerraPointShortName(int pointId);
        TerraPointShortNameDto SaveTerraPointShortName(int pointId, string shortName);

        void SetEditPointDialogModel(TerraGraphicsEditPointModel model);
        TerraPointSetDefaultTerraPointModel SetDefaultTerraPoint(int pointId/*, int userId*/);
        void SaveTerraPoint(TerraPointSaveModel model);
        void DeleteTerraPoint(int id);

        MissionOrderListModel GetMissionOrderListModel();
        void SetMissionOrderListModel(MissionOrderListModel model, bool hasError);
        MissionOrderEditModel GetMissionOrderEditModel(int id, int? userId);
        void SetMissionOrderEditTargetModel(MissionOrderEditTargetModel model);


    }
}