using System;
using Reports.Core;
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
        bool SaveVacationEditModel(VacationEditModel model, UploadFileDto fileDto, UploadFileDto orderScanFileDto, out string error);
        void ReloadDictionariesToModel(VacationEditModel model);
        bool ResetVacationApprovals(int id, out string error);

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

        bool HaveAbsencesForPeriod(DateTime beginDate, DateTime endDate, int userId,
                                   int currentUserId, UserRole currentUserRole);
        bool ResetSicklistApprovals(int id, out string error);

        HolidayWorkListModel GetHolidayWorkListModel();
        void SetHolidayWorkListModel(HolidayWorkListModel model, bool hasError);
        HolidayWorkEditModel GetHolidayWorkEditModel(int id, int userId);
        void ReloadDictionariesToModel(HolidayWorkEditModel model);
        bool SaveHolidayWorkEditModel(HolidayWorkEditModel model,out string error);

        MissionListModel GetMissionListModel();
        void SetMissionListModel(MissionListModel model, bool hasError);
        MissionEditModel GetMissionEditModel(int id, int userId);
        void ReloadDictionariesToModel(MissionEditModel model);
        bool SaveMissionEditModel(MissionEditModel model, UploadFileDto orderScanFileDto, out string error);

        DismissalListModel GetDismissalListModel();
        void SetDismissalListModel(DismissalListModel model, bool hasError);
        DismissalEditModel GetDismissalEditModel(int id, int userId);
        bool SaveDismissalEditModel(DismissalEditModel model, UploadFileDto fileDto, UploadFileDto orderScanFileDto, out string error);
        void ReloadDictionariesToModel(DismissalEditModel model);
                
        ClearanceChecklistListModel GetClearanceChecklistListModel();
        void SetClearanceChecklistListModel(ClearanceChecklistListModel model, bool hasError);
        ClearanceChecklistEditModel GetClearanceChecklistEditModel(int id, int userId);
        ClearanceChecklistEditModel GetClearanceChecklistEditModelByParentId(int parentId, int userId);
        bool SaveClearanceChecklistEditModel(ClearanceChecklistEditModel model, out string error);
        bool SetClearanceChecklistApproval(int approvalId, int approvedBy, out ClearanceChecklistApprovalDto modifiedApproval, out string error);
        bool SetClearanceChecklistComment(int approvalId, string comment, out string error);
        bool SetClearanceChecklistBottomFields(int id, int? registryNumber, decimal? personalIncomeTax, string oKTMO, out string error);

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
        bool SaveChildVacationEditModel(ChildVacationEditModel model, UploadFileDto fileDto, UploadFileDto orderScanFileDto, out string error);

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
        CreateMissionOrderModel GetCreateMissionOrderModel();
        void ReloadDictionaries(MissionOrderEditModel model);
        bool CheckOtherOrdersExists(MissionOrderEditModel model);
        bool SaveMissionOrderEditModel(MissionOrderEditModel model, out string error);
        PrintMissionOrderViewModel GetPrintMissionOrderModel(int id);
        UserInfoModel GetPrintMissionOrderDocumentModel(int id);
        GradeListViewModel GetGradeListModel();

        MissionReportsListModel GetMissionReportsListModel();
        void SetMissionReportsListModel(MissionReportsListModel model, bool hasError);


        MissionReportEditModel GetMissionReportEditModel(int id);

        void SetMissionReportEditCostModel(MissionReportEditCostModel model);
        bool SaveMissionReportEditModel(MissionReportEditModel model, out string error);
        RequestAttachmentsModel GetMrAttachmentsModel(int id, RequestAttachmentTypeEnum typeId);
        void SetMissionReportEditTranModel(MissionReportEditTranModel model);

        MissionPurchaseBookDocListModel GetMissionPurchaseBookDocsListModel();
        void SetMissionPurchaseBookDocsModel(MissionPurchaseBookDocListModel model, bool hasError);
        EditMissionPbDocumentModel GetEditMissionPbDocumentModel(int id);
        ContractorAccountDto GetContractorAccount(int id);
        void ReloadDictionaries(EditMissionPbDocumentModel model);
        bool SaveMissionPbDocumentEditModel(EditMissionPbDocumentModel model, out string error);
        EditMissionPbRecordsModel GetPbRecordsModel(int documentId);
        void SetMissionReportEditRecordModel(MissionPbEditRecordModel model);
        PbRecordCostTypesDto GetCostTypes(int reportId, bool isNew);
        ContractorAccountDto GetRequestNumberForCostType(int reportId, int costTypeId);
        PbRecordCostTypesDto GetReportsForPbUserId(int userId);
        int SavePbRecord(SavePbRecordModel model);
        int DeletePbRecord(DeletePbRecordModel model);

        MissionPurchaseBookRecordsListModel GetMissionPurchaseBookRecordsListModel();
        void SetMissionPurchaseBookRecordsListModel(MissionPurchaseBookRecordsListModel model);

        MissionUserDeptsListModel GetMissionUserDeptsListModel();
        void SetMissionUserDeptsListModel(MissionUserDeptsListModel model, bool hasError,bool showDepts);

        PrintMissionUserDeptsListModel PrintMissionUserDeptsListModel(int departmentId, int statusId,
                                                                      DateTime? beginDate,
                                                                      DateTime? endDate, string userName, int sortBy,
                                                                      bool? sortDescending,bool showDepts);

        PrintMissionReportViewModel GetPrintMissionReportModel(int id);

        void SaveDocumentsToArchive(DeletePbRecordModel model);
        void SetPrintArchivistAddressModel(PrintArchivistAddressModel model);
        PrintArchivistAddressFormModel GetPrintArchivistAddressFormModel(int id);
        bool SaveUniqueAttachment(SaveAttacmentModel model);
    }
}