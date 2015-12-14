using System;
using Reports.Core;
using Reports.Core.Dto;
using Reports.Core.Enum;
using Reports.Presenters.UI.ViewModel;
using System.Collections.Generic;
using Reports.Core.Domain;

namespace Reports.Presenters.UI.Bl
{
    public interface IRequestBl : IBaseBl
    {
        CreateRequestModel GetCreateRequestModel(int? userId);
        DepartmentChildrenDto GetUsersForDepartment(int departmentId);
        IList<ManualDeductionDto> GetManualDeductionDocs(int DepartmentId, int Status,string UserName);
        bool CheckDepartment(SurchargeNoteEditModel model, out int level);
        Result AddStorno(int MissionReportId, decimal StornoSum, string StornoComment, int StornoDeductionNumber);
        VacationListModel GetVacationListModel();
        void SetVacationListModel(VacationListModel model, bool hasError);

        AllRequestListModel GetAllRequestListModel();
        void SetAllRequestListModel(AllRequestListModel model, bool hasError);

        VacationEditModel GetVacationEditModel(int id, int userId);
        Result SaveVacationFileAfter1C(VacationEditModel model, UploadFileDto fileDto);
        bool SaveVacationEditModel(VacationEditModel model, UploadFileDto fileDto, UploadFileDto unsignedOrderScanFileDto, UploadFileDto orderScanFileDto, out string error);
        void ReloadDictionariesToModel(VacationEditModel model);
        bool ResetVacationApprovals(int id, out string error);

        int GetOtherRequestCountsForUserAndDates(DateTime beginDate, DateTime endDate, int userId, int vacationId, bool isChildVacantion);
        int GetOtherRequestCountsForUserAndDates(DateTime beginDate, DateTime endDate, int userId, int requestId, RequestTypeEnum requestType);

        RequestCommentsModel GetCommentsModel(int id, int typeId, string addCommentText = null, bool hasParent = false);
        bool SaveComment(SaveCommentModel model);

        AbsenceListModel GetAbsenceListModel();
        void SetAbsenceListModel(AbsenceListModel model, bool hasError);
        void SendMail();
        AbsenceEditModel GetAbsenceEditModel(int id, int userId);
        void ReloadDictionariesToModel(AbsenceEditModel model);
        bool SaveAbsenceEditModel(AbsenceEditModel model, out string error);

        SicklistListModel GetSicklistListModel();
        void SetSicklistListModel(SicklistListModel model, bool hasError);
        SicklistEditModel GetSicklistEditModel(int id, int userId);
        void ReloadDictionariesToModel(SicklistEditModel model);
        bool SaveSicklistEditModel(SicklistEditModel model,UploadFileDto fileDto, out string error);
        bool ChangeNotUseInAnalyticalStatement(int[] ids, bool[] notuse);
        bool HaveAbsencesForPeriod(DateTime beginDate, DateTime endDate, int userId,
                                   int currentUserId, UserRole currentUserRole);
        bool ResetSicklistApprovals(int id, out string error);

        HolidayWorkListModel GetHolidayWorkListModel();
        void SetHolidayWorkListModel(HolidayWorkListModel model, bool hasError);
        HolidayWorkEditModel GetHolidayWorkEditModel(int id, int userId);
        void ReloadDictionariesToModel(HolidayWorkEditModel model);
        bool SaveHolidayWorkEditModel(HolidayWorkEditModel model,out string error);
        AnalyticalStatementModel GetAnalyticalStatementModel();
        AnalyticalStatementDetailsModel GetAnalyticalStatementDetails(AnalyticalStatementDetailsModel model);
        IList<AnalyticalStatementDto> GetAnalyticalStatements(string name,int departamentId, DateTime? beginDate, DateTime? endDate, string Number, int sortBy, bool? SortDescending);
        MissionListModel GetMissionListModel();
        void SetMissionListModel(MissionListModel model, bool hasError);
        MissionEditModel GetMissionEditModel(int id, int userId);
        void ReloadDictionariesToModel(MissionEditModel model);
        bool SaveMissionEditModel(MissionEditModel model, UploadFileDto orderScanFileDto, out string error);

        DismissalListModel GetDismissalListModel();
        void SetDismissalListModel(DismissalListModel model, bool hasError);
        DismissalEditModel GetDismissalEditModel(int id, int userId);
        bool SaveDismissalEditModel(DismissalEditModel model, IDictionary<RequestAttachmentTypeEnum, UploadFileDto> fileDtos, out string error);
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
        DeductionImportModel GetDeductionImportModel(DeductionImportModel model = null);
        IList<DeductionDto> ImportDeductionFromFile(ref string path, ref List<string> Errors, ref bool isFileExist);
        AttachmentModel GetFileContext(int id/*, int typeId*/);
        RequestAttachmentsModel GetAttachmentsModel(int id, RequestAttachmentTypeEnum typeId);
        bool SaveAttachment(SaveAttacmentModel model);
        int? SaveAttachment(int entityId, int id, UploadFileDto dto, RequestAttachmentTypeEnum type, out string attachment);
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
        void SaveDeductionEditAdminModel(DeductionEditModel model);
        bool SaveDeductionEditModel(DeductionEditModel model, bool EnableSendEmail, out string error);
        IList<IdNameDto> GetUserListForDeduction(string Name, int UserId);
        User GetUser(int Id);

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
        void SetMissionHotelsListModel(MissionHotelsModel model, bool hasError);
        void CheckFillFields(MissionHotelsEditModel model, System.Web.Mvc.ModelStateDictionary ms);
        bool SaveMissionHotelsEditModel(MissionHotelsEditModel model, out string error);
        MissionOrderEditModel GetMissionOrderEditModel(int id, int? userId);
        void SetMissionOrderEditTargetModel(MissionOrderEditTargetModel model);
        CreateMissionOrderModel GetCreateMissionOrderModel();
        void ReloadDictionaries(MissionOrderEditModel model);
        bool CheckOtherOrdersExists(MissionOrderEditModel model);
        bool CheckAnyOtherOrdersExists(MissionOrderEditModel model);
        bool CheckOrderBeginDate(string beginMissionDate);
        bool IsMissionOrderLong(DateTime endDate, DateTime beginDate);
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
        string SetDeductionDoc(int deductionNumber, int MissionReportid);
        PrintMissionUserDeptsListModel PrintMissionUserDeptsListModel(int departmentId, int statusId,
                                                                      DateTime? beginDate,
                                                                      DateTime? endDate, string userName, int sortBy,
                                                                      bool? sortDescending,bool showDepts);

        PrintMissionReportViewModel GetPrintMissionReportModel(int id);
        PrintMissionReportListViewModel GetPrintMissionReportListModel(int id,int reportId);

        bool ExportFromMissionReportToDeduction(IEnumerable<int> DocIds, int typeId, int kindId, int uploadingType, bool isFastDissmissal, bool EnableSendEmail);
        bool SendNotifyEmailToUser(int MissionReportId);
        bool sendEmail(string to, string subj, string body);

        void SaveDocumentsToArchive(DeletePbRecordModel model);
        void SetPrintArchivistAddressModel(PrintArchivistAddressModel model);
        PrintArchivistAddressFormModel GetPrintArchivistAddressFormModel(int id);
        bool SaveUniqueAttachment(SaveAttacmentModel model);

        int CreateAdditionalOrder(int missionReportId);
        AdditionalMissionOrderEditModel GetAdditionalMissionOrderEditModel(int id);
        bool SaveAdditionalMissionOrderEditModel(AdditionalMissionOrderEditModel model, out string error);

        IList<User> GetManagersForEmployee(int userId, int? minManagerLevel = null);

        AccessGroupsListModel GetAccessGroupsListModel();
        AccessGroupsListModel SetAccessGroupsListModel(AccessGroupsListModel model);


        void SaveSurchargeNote(SurchargeNoteEditModel model);
        SurchargeNoteEditModel GetSurchargeNoteEditModel(int id);
        SurchargeNoteListModel GetSurchargeNoteListModel();
        void SetDocumentsToModel(SurchargeNoteListModel model);
        void GetDictionaries(SurchargeNoteEditModel model);
        bool CheckDepartmentLevel(int id, int level);
        IList<Terrapoint_DepartmentDto> GetTP_D_list();
        IList<Department_TerrapointDto> GetD_TP_list();
        #region VacationReturn
         VacationReturnCreateViewModel GetCreateModel();
         List<VacationReturnDto> GetDocuments(VacationReturnListModel model);
         VacationReturnListModel GetVacationReturnListModel();
         VacationReturnViewModel GetNewVacationReturnViewModel(int UserId);
         Result<VacationReturnViewModel> GetVacationReturnEditModel(int id);
         Result<VacationReturnViewModel> SaveVacationReturnEditModel(VacationReturnViewModel model);
        #endregion
    }
}