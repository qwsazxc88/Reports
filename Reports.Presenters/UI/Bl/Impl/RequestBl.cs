using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web.Script.Serialization;
using System.Text.RegularExpressions;
using Reports.Core;
using Reports.Core.Dao;
using Reports.Core.Dao.Impl;
using Reports.Core.Domain;
using Reports.Core.Dto;
using Reports.Core.Enum;
using Reports.Core.Services;
using Reports.Presenters.Services;
using Reports.Presenters.UI.ViewModel;
using System.Text;
using System.Web.Mvc;
using System.Linq.Expressions;
using Reports.Core.Utils;
namespace Reports.Presenters.UI.Bl.Impl
{
    public class RequestBl : BaseBl, IRequestBl
    {
        #region Constants
        protected string EmptyDepartmentName = string.Empty;
        protected string ChildVacationTimesheetStatusShortName = "ОЖ";
        protected string OKTMOFormatError = "Ошибка формата ОКТМО";

        public const int VacationFirstTimesheetStatisId = 8;
        public const int VacationLastTimesheetStatisId = 12;
        public const int AbsenceFirstTimesheetStatisId = 15;
        public const int AbsenceLastTimesheetStatisId = 18;

        public const int DailyCostTypeId = 1;

        public const int LastValidDay = 5;
        #endregion

        #region DAOs
        protected IBugReportDao bugReportDao;
        protected IVacationReturnDao vacationReturnDao;
        protected IrefVacationReturnStatusDao refVacationReturnStatusDao;
        protected IrefVacationReturnTypesDao refVacationReturnTypesDao;
        protected IVacationTypeDao vacationTypeDao;
        protected IAdditionalVacationTypeDao additionalVacationTypeDao;
        protected IRequestStatusDao requestStatusDao;
        protected IPositionDao positionDao;
        protected IVacationDao vacationDao;
        //protected IUserToDepartmentDao userToDepartmentDao;
        protected ITimesheetStatusDao timesheetStatusDao;
        protected IVacationCommentDao vacationCommentDao;
        //protected IRequestNextNumberDao requestNextNumberDao;
        protected IRoleDao roleDao;
        protected IAnalyticalStatementDao analyticalStatementDao;
        protected IAbsenceTypeDao absenceTypeDao;
        protected IAbsenceDao absenceDao;
        protected IAbsenceCommentDao absenceCommentDao;
        protected IMailListDao maillistDao;

        protected ISicklistTypeDao sicklistTypeDao;
        protected ISicklistPaymentRestrictTypeDao sicklistPaymentRestrictTypeDao;
        protected ISicklistPaymentPercentDao sicklistPaymentPercentDao;
        protected ISicklistDao sicklistDao;
        protected IRequestAttachmentDao requestAttachmentDao;
        protected ISicklistCommentDao sicklistCommentDao;
        protected ISicklistBabyMindingTypeDao sicklistBabyMindingTypeDao;

        protected IHolidayWorkTypeDao holidayWorkTypeDao;
        protected IHolidayWorkDao holidayWorkDao;
        protected IHolidayWorkCommentDao holidayWorkCommentDao;

        protected IMissionTypeDao missionTypeDao;
        protected IMissionDao missionDao;
        protected IMissionCommentDao missionCommentDao;

        protected IDismissalTypeDao dismissalTypeDao;
        protected IDismissalDao dismissalDao;
        protected IDismissalCommentDao dismissalCommentDao;

        protected IClearanceChecklistCommentDao clearanceChecklistCommentDao;

        protected ITimesheetCorrectionTypeDao timesheetCorrectionTypeDao;
        protected ITimesheetCorrectionDao timesheetCorrectionDao;
        protected ITimesheetCorrectionCommentDao timesheetCorrectionCommentDao;

        protected IEmploymentTypeDao employmentTypeDao;
        protected IEmploymentHoursTypeDao employmentHoursTypeDao;
        protected IEmploymentDao employmentDao;
        protected IEmploymentCommentDao employmentCommentDao;
        protected IEmploymentAdditionDao employmentAdditionDao;

        protected IRequestPrintFormDao requestPrintFormDao;

        protected IInspectorToUserDao inspectorToUserDao;
        protected IChiefToUserDao chiefToUserDao;
        protected IWorkingDaysConstantDao workingDaysConstantDao;

        protected IChildVacationDao childVacationDao;
        protected IChildVacationCommentDao childVacationCommentDao;

        protected IDeductionTypeDao deductionTypeDao;
        protected IDeductionKindDao deductionKindDao;
        protected IDeductionDao deductionDao;
        protected IManualDeductionDao manualDeductionDao;
        protected ITerraPointDao terraPointDao;
        protected ITerraPointToUserDao terraPointToUserDao;
        protected ITerraGraphicDao terraGraphicDao;
        protected IDeductionImportDao deductionImportDao;
        protected ISurchargeNoteDao surcharcheNoteDao;

        public IBugReportDao BugReportDao
        {
            get { return Validate.Dependency(bugReportDao); }
            set {bugReportDao=value;}
        }
        public IVacationReturnDao VacationReturnDao
        {
            get { return Validate.Dependency(vacationReturnDao); }
            set { vacationReturnDao = value; }
        }
        public IrefVacationReturnTypesDao RefVacationReturnTypesDao
        {
            get { return Validate.Dependency(refVacationReturnTypesDao); }
            set { refVacationReturnTypesDao = value; }
        }
        public IrefVacationReturnStatusDao RefVacationReturnStatusDao
        {
            get { return Validate.Dependency(refVacationReturnStatusDao); }
            set { refVacationReturnStatusDao = value; }
        }
        public IMailListDao MailListDao
        {
            get { return Validate.Dependency(maillistDao); }
            set { maillistDao = value; }
        }
        public IManualDeductionDao ManualDeductionDao
        {
            get { return Validate.Dependency(manualDeductionDao);}
            set { manualDeductionDao = value; }
        }
        public ISurchargeNoteDao SurchargeNoteDao
        {
            get { return Validate.Dependency(surcharcheNoteDao); }
            set { surcharcheNoteDao = value; }
        }
        public IAnalyticalStatementDao AnalyticalStatementDao
        {
            get { return Validate.Dependency(analyticalStatementDao); }
            set { analyticalStatementDao = value; }
        }
        public IVacationTypeDao VacationTypeDao
        {
            get { return Validate.Dependency(vacationTypeDao); }
            set { vacationTypeDao = value; }
        }
        public IAdditionalVacationTypeDao AdditionalVacationTypeDao
        {
            get { return Validate.Dependency(additionalVacationTypeDao); }
            set { additionalVacationTypeDao = value; }
        }
        public IRequestStatusDao RequestStatusDao
        {
            get { return Validate.Dependency(requestStatusDao); }
            set { requestStatusDao = value; }
        }
        public IPositionDao PositionDao
        {
            get { return Validate.Dependency(positionDao); }
            set { positionDao = value; }
        }
        public IVacationDao VacationDao
        {
            get { return Validate.Dependency(vacationDao); }
            set { vacationDao = value; }
        }
        /*public IUserToDepartmentDao UserToDepartmentDao
        {
            get { return Validate.Dependency(userToDepartmentDao); }
            set { userToDepartmentDao = value; }
        }*/
        public ITimesheetStatusDao TimesheetStatusDao
        {
            get { return Validate.Dependency(timesheetStatusDao); }
            set { timesheetStatusDao = value; }
        }
        public IVacationCommentDao VacationCommentDao
        {
            get { return Validate.Dependency(vacationCommentDao); }
            set { vacationCommentDao = value; }
        }
        /*public IRequestNextNumberDao RequestNextNumberDao
        {
            get { return Validate.Dependency(requestNextNumberDao); }
            set { requestNextNumberDao = value; }
        }*/
        public IAbsenceTypeDao AbsenceTypeDao
        {
            get { return Validate.Dependency(absenceTypeDao); }
            set { absenceTypeDao = value; }
        }
        public IAbsenceDao AbsenceDao
        {
            get { return Validate.Dependency(absenceDao); }
            set { absenceDao = value; }
        }
        public IAbsenceCommentDao AbsenceCommentDao
        {
            get { return Validate.Dependency(absenceCommentDao); }
            set { absenceCommentDao = value; }
        }

        public ISicklistTypeDao SicklistTypeDao
        {
            get { return Validate.Dependency(sicklistTypeDao); }
            set { sicklistTypeDao = value; }
        }
        public ISicklistBabyMindingTypeDao SicklistBabyMindingTypeDao
        {
            get { return Validate.Dependency(sicklistBabyMindingTypeDao); }
            set { sicklistBabyMindingTypeDao = value; }
        }
        public ISicklistPaymentRestrictTypeDao SicklistPaymentRestrictTypeDao
        {
            get { return Validate.Dependency(sicklistPaymentRestrictTypeDao); }
            set { sicklistPaymentRestrictTypeDao = value; }
        }
        public ISicklistPaymentPercentDao SicklistPaymentPercentDao
        {
            get { return Validate.Dependency(sicklistPaymentPercentDao); }
            set { sicklistPaymentPercentDao = value; }
        }
        public ISicklistDao SicklistDao
        {
            get { return Validate.Dependency(sicklistDao); }
            set { sicklistDao = value; }
        }
        public IRequestAttachmentDao RequestAttachmentDao
        {
            get { return Validate.Dependency(requestAttachmentDao); }
            set { requestAttachmentDao = value; }
        }
        public ISicklistCommentDao SicklistCommentDao
        {
            get { return Validate.Dependency(sicklistCommentDao); }
            set { sicklistCommentDao = value; }
        }

        public IHolidayWorkTypeDao HolidayWorkTypeDao
        {
            get { return Validate.Dependency(holidayWorkTypeDao); }
            set { holidayWorkTypeDao = value; }
        }
        public IHolidayWorkDao HolidayWorkDao
        {
            get { return Validate.Dependency(holidayWorkDao); }
            set { holidayWorkDao = value; }
        }
        public IHolidayWorkCommentDao HolidayWorkCommentDao
        {
            get { return Validate.Dependency(holidayWorkCommentDao); }
            set { holidayWorkCommentDao = value; }
        }
        public IMissionTypeDao MissionTypeDao
        {
            get { return Validate.Dependency(missionTypeDao); }
            set { missionTypeDao = value; }
        }
        public IMissionDao MissionDao
        {
            get { return Validate.Dependency(missionDao); }
            set { missionDao = value; }
        }
        public IMissionCommentDao MissionCommentDao
        {
            get { return Validate.Dependency(missionCommentDao); }
            set { missionCommentDao = value; }
        }
        public IDismissalTypeDao DismissalTypeDao
        {
            get { return Validate.Dependency(dismissalTypeDao); }
            set { dismissalTypeDao = value; }
        }
        public IDismissalDao DismissalDao
        {
            get { return Validate.Dependency(dismissalDao); }
            set { dismissalDao = value; }
        }
        public IDismissalCommentDao DismissalCommentDao
        {
            get { return Validate.Dependency(dismissalCommentDao); }
            set { dismissalCommentDao = value; }
        }

        public IClearanceChecklistCommentDao ClearanceChecklistCommentDao
        {
            get { return Validate.Dependency(clearanceChecklistCommentDao); }
            set { clearanceChecklistCommentDao = value; }
        }

        protected IClearanceChecklistDao clearanceChecklistDao;
        public IClearanceChecklistDao ClearanceChecklistDao
        {
            get { return Validate.Dependency(clearanceChecklistDao); }
            set { clearanceChecklistDao = value; }
        }

        public ITimesheetCorrectionTypeDao TimesheetCorrectionTypeDao
        {
            get { return Validate.Dependency(timesheetCorrectionTypeDao); }
            set { timesheetCorrectionTypeDao = value; }
        }
        public ITimesheetCorrectionDao TimesheetCorrectionDao
        {
            get { return Validate.Dependency(timesheetCorrectionDao); }
            set { timesheetCorrectionDao = value; }
        }
        public ITimesheetCorrectionCommentDao TimesheetCorrectionCommentDao
        {
            get { return Validate.Dependency(timesheetCorrectionCommentDao); }
            set { timesheetCorrectionCommentDao = value; }
        }

        public IEmploymentTypeDao EmploymentTypeDao
        {
            get { return Validate.Dependency(employmentTypeDao); }
            set { employmentTypeDao = value; }
        }
        public IEmploymentHoursTypeDao EmploymentHoursTypeDao
        {
            get { return Validate.Dependency(employmentHoursTypeDao); }
            set { employmentHoursTypeDao = value; }
        }
        public IEmploymentDao EmploymentDao
        {
            get { return Validate.Dependency(employmentDao); }
            set { employmentDao = value; }
        }
        public IEmploymentCommentDao EmploymentCommentDao
        {
            get { return Validate.Dependency(employmentCommentDao); }
            set { employmentCommentDao = value; }
        }
        public IEmploymentAdditionDao EmploymentAdditionDao
        {
            get { return Validate.Dependency(employmentAdditionDao); }
            set { employmentAdditionDao = value; }
        }
        public IRoleDao RoleDao
        {
            get { return Validate.Dependency(roleDao); }
            set { roleDao = value; }
        }
        public IRequestPrintFormDao RequestPrintFormDao
        {
            get { return Validate.Dependency(requestPrintFormDao); }
            set { requestPrintFormDao = value; }
        }
        public IInspectorToUserDao InspectorToUserDao
        {
            get { return Validate.Dependency(inspectorToUserDao); }
            set { inspectorToUserDao = value; }
        }

        public IChiefToUserDao ChiefToUserDao
        {
            get { return Validate.Dependency(chiefToUserDao); }
            set { chiefToUserDao = value; }
        }

        public IWorkingDaysConstantDao WorkingDaysConstantDao
        {
            get { return Validate.Dependency(workingDaysConstantDao); }
            set { workingDaysConstantDao = value; }
        }

        public IChildVacationDao ChildVacationDao
        {
            get { return Validate.Dependency(childVacationDao); }
            set { childVacationDao = value; }
        }
        public IChildVacationCommentDao ChildVacationCommentDao
        {
            get { return Validate.Dependency(childVacationCommentDao); }
            set { childVacationCommentDao = value; }
        }
        public IDeductionTypeDao DeductionTypeDao
        {
            get { return Validate.Dependency(deductionTypeDao); }
            set { deductionTypeDao = value; }
        }
        public IDeductionKindDao DeductionKindDao
        {
            get { return Validate.Dependency(deductionKindDao); }
            set { deductionKindDao = value; }
        }
        public IDeductionDao DeductionDao
        {
            get { return Validate.Dependency(deductionDao); }
            set { deductionDao = value; }
        }

        public ITerraPointDao TerraPointDao
        {
            get { return Validate.Dependency(terraPointDao); }
            set { terraPointDao = value; }
        }
        public ITerraPointToUserDao TerraPointToUserDao
        {
            get { return Validate.Dependency(terraPointToUserDao); }
            set { terraPointToUserDao = value; }
        }
        public ITerraGraphicDao TerraGraphicDao
        {
            get { return Validate.Dependency(terraGraphicDao); }
            set { terraGraphicDao = value; }
        }
        protected IMissionGoalDao missionGoalDao;
        public IMissionGoalDao MissionGoalDao
        {
            get { return Validate.Dependency(missionGoalDao); }
            set { missionGoalDao = value; }
        }
        protected IMissionAirTicketTypeDao missionAirTicketTypeDao;
        public IMissionAirTicketTypeDao MissionAirTicketTypeDao
        {
            get { return Validate.Dependency(missionAirTicketTypeDao); }
            set { missionAirTicketTypeDao = value; }
        }
        protected IMissionCountryDao missionCountryDao;
        public IMissionCountryDao MissionCountryDao
        {
            get { return Validate.Dependency(missionCountryDao); }
            set { missionCountryDao = value; }
        }
        protected IMissionDailyAllowanceDao missionDailyAllowanceDao;
        public IMissionDailyAllowanceDao MissionDailyAllowanceDao
        {
            get { return Validate.Dependency(missionDailyAllowanceDao); }
            set { missionDailyAllowanceDao = value; }
        }
        protected IMissionResidenceDao missionResidenceDao;
        public IMissionResidenceDao MissionResidenceDao
        {
            get { return Validate.Dependency(missionResidenceDao); }
            set { missionResidenceDao = value; }
        }
        protected IMissionTrainTicketTypeDao missionTrainTicketTypeDao;
        public IMissionTrainTicketTypeDao MissionTrainTicketTypeDao
        {
            get { return Validate.Dependency(missionTrainTicketTypeDao); }
            set { missionTrainTicketTypeDao = value; }
        }
        protected IMissionGraidDao missionGraidDao;
        public IMissionGraidDao MissionGraidDao
        {
            get { return Validate.Dependency(missionGraidDao); }
            set { missionGraidDao = value; }
        }
        protected IMissionOrderDao missionOrderDao;
        public IMissionOrderDao MissionOrderDao
        {
            get { return Validate.Dependency(missionOrderDao); }
            set { missionOrderDao = value; }
        }
        protected IMissionHotelsDao missionHotelDao;
        public IMissionHotelsDao MissionHotelDao
        {
            get { return Validate.Dependency(missionHotelDao); }
            set { missionHotelDao = value; }
        }
        protected IMissionOrderCommentDao missionOrderCommentDao;
        public IMissionOrderCommentDao MissionOrderCommentDao
        {
            get { return Validate.Dependency(missionOrderCommentDao); }
            set { missionOrderCommentDao = value; }
        }
        protected IWorkingCalendarDao workingCalendarDao;
        public IWorkingCalendarDao WorkingCalendarDao
        {
            get { return Validate.Dependency(workingCalendarDao); }
            set { workingCalendarDao = value; }
        }
        protected IMissionTargetDao missionTargetDao;
        public IMissionTargetDao MissionTargetDao
        {
            get { return Validate.Dependency(missionTargetDao); }
            set { missionTargetDao = value; }
        }

        protected IMissionReportDao missionReportDao;
        public IMissionReportDao MissionReportDao
        {
            get { return Validate.Dependency(missionReportDao); }
            set { missionReportDao = value; }
        }
        protected IMissionReportCostTypeDao missionReportCostTypeDao;
        public IMissionReportCostTypeDao MissionReportCostTypeDao
        {
            get { return Validate.Dependency(missionReportCostTypeDao); }
            set { missionReportCostTypeDao = value; }
        }
        protected IMissionReportCostDao missionReportCostDao;
        public IMissionReportCostDao MissionReportCostDao
        {
            get { return Validate.Dependency(missionReportCostDao); }
            set { missionReportCostDao = value; }
        }
        protected IMissionReportCommentDao missionReportCommentDao;
        public IMissionReportCommentDao MissionReportCommentDao
        {
            get { return Validate.Dependency(missionReportCommentDao); }
            set { missionReportCommentDao = value; }
        }

        protected IAccountDao accountDao;
        public IAccountDao AccountDao
        {
            get { return Validate.Dependency(accountDao); }
            set { accountDao = value; }
        }
        protected IContractorDao contractorDao;
        public IContractorDao ContractorDao
        {
            get { return Validate.Dependency(contractorDao); }
            set { contractorDao = value; }
        }
        protected IMissionPurchaseBookDocumentDao missionPurchaseBookDocumentDao;
        public IMissionPurchaseBookDocumentDao MissionPurchaseBookDocumentDao
        {
            get { return Validate.Dependency(missionPurchaseBookDocumentDao); }
            set { missionPurchaseBookDocumentDao = value; }
        }
        protected IMissionPurchaseBookRecordDao missionPurchaseBookRecordDao;
        public IMissionPurchaseBookRecordDao MissionPurchaseBookRecordDao
        {
            get { return Validate.Dependency(missionPurchaseBookRecordDao); }
            set { missionPurchaseBookRecordDao = value; }
        }
        protected IManualRoleRecordDao manualRoleRecordDao;
        public IManualRoleRecordDao ManualRoleRecordDao
        {
            get { return Validate.Dependency(manualRoleRecordDao); }
            set { manualRoleRecordDao = value; }
        }

        protected IConfigurationService configurationService;
        public IConfigurationService ConfigurationService
        {
            set { configurationService = value; }
            get { return Validate.Dependency(configurationService); }
        }
        public IDeductionImportDao DeductionImport_Dao
        {
            set { deductionImportDao = value; }
            get { return Validate.Dependency(deductionImportDao); }
        }

        protected IAccessGroupDao accessGroupDao;
        public IAccessGroupDao AccessGroupDao
        {
            get { return Validate.Dependency(accessGroupDao); }
            set { accessGroupDao = value; }
        }
        #endregion

        #region Create Request
        public CreateRequestModel GetCreateRequestModel(int? userId)
        {
            if (userId == null)
                userId = AuthenticationService.CurrentUser.Id;

            User user = UserDao.Load(userId.Value);
            UserRole role = (UserRole)user.RoleId;
            CreateRequestModel model = new CreateRequestModel
            {
                IsUserVisible = (role & UserRole.Employee) != UserRole.Employee,
                RequestTypes = GetRequestTypes()
            };
            switch (role)
            {
                case UserRole.Employee:
                    model.Users = new List<IdNameDto> { new IdNameDto(user.Id, user.FullName) };
                    break;
                case UserRole.Manager:
                case UserRole.PersonnelManager:
                    model.Users = UserDao.GetUsersForManager(user.Id, role, 0);
                    //model.Users = UserDao.GetEmployeesForCreateHelpServiceRequest(new List<int> { user.Department.Id }, "");
                    break;
            }
            return model;
        }
        public DepartmentChildrenDto GetUsersForDepartment(int departmentId)
        {
            IUser user = AuthenticationService.CurrentUser;
            IList<IdNameDto> users = UserDao.GetUsersForManager
                (user.Id, user.UserRole, departmentId);
            return new DepartmentChildrenDto { Error = string.Empty, Children = users.ToList() };
        }
        protected IList<IdNameDto> GetRequestTypes()
        {
            return new List<IdNameDto>
                       {
                           new IdNameDto((int) RequestTypeEnum.Vacation, "Заявка на отпуск"),
                           new IdNameDto((int) RequestTypeEnum.ChildVacation, "Заявка на отпуск по уходу за ребенком"),
                           new IdNameDto((int) RequestTypeEnum.Absence, "Заявка на неявку"),
                           new IdNameDto((int) RequestTypeEnum.Sicklist, "Заявка на больничный"),
                           //new IdNameDto((int) RequestTypeEnum.HolidayWork, "Заявка на оплату праздничных и выходных дней"),
                           //new IdNameDto((int) RequestTypeEnum.Mission, "Заявка на командировку"),
                           new IdNameDto((int) RequestTypeEnum.Dismissal, "Заявка на увольнение"),
                           //new IdNameDto((int) RequestTypeEnum.TimesheetCorrection, "Заявка на корректировку табеля")
                          // new IdNameDto((int) RequestTypeEnum.Employment, "Заявка на прием на работу")
                       }.OrderBy(x => x.Name).ToList();
        }
        #endregion

        #region All requests
        public AllRequestListModel GetAllRequestListModel()
        {
            User user = UserDao.Load(AuthenticationService.CurrentUser.Id);
            AllRequestListModel model = new AllRequestListModel
            {
                UserId = AuthenticationService.CurrentUser.Id,
            };
            SetDepartmentToModel(model, user);
            SetDictionariesToModel(model, user);
            SetInitialDates(model);
            return model;
        }
        protected void SetDepartmentToModel(AllRequestListModel model, User user)
        {
            IdNameReadonlyDto dep = GetDepartmentDto(user);
            model.DepartmentName = dep.Name;
            model.DepartmentId = dep.Id;
            model.DepartmentReadOnly = dep.IsReadOnly;
        }
        protected void SetDictionariesToModel(AllRequestListModel model, User user)
        {
            //model.Departments = GetDepartments(user);
            //model.Types = GetEmploymentTypes(true);
            //model.GraphicTypes = GetEmploymentGraphicTypes(true);

            model.Statuses = GetRequestStatuses(CurrentUser.UserRole);
            //model.Positions = GetPositions(user);
        }
        public void SetAllRequestListModel(AllRequestListModel model, bool hasError)
        {
            User user = UserDao.Load(model.UserId);
            SetDictionariesToModel(model, user);
            if (hasError)
                model.Documents = new List<AllRequestDto>();
            else
                SetDocumentsToModel(model, user);
        }
        public void SetDocumentsToModel(AllRequestListModel model, User user)
        {
            //model.Documents = new List<VacationDto>();

            List<AllRequestDto> result = new List<AllRequestDto>();
            UserRole role = (UserRole)(user.RoleId & (int)CurrentUser.UserRole);


            result.AddRange(SicklistDao.GetSicklistDocuments(
                user.Id,
                role,
                model.DepartmentId,
                0,
                0,
                model.StatusId,
                0,
                model.BeginDate,
                model.EndDate,
                model.UserName,
                null,
                model.SortBy, model.SortDescending, model.Number).ToList().ConvertAll(x => new AllRequestDto
                {
                    Date = x.Date,
                    BeginDate = x.BeginDate,
                    EndDate = x.EndDate,
                    EditUrl = "SicklistEdit",
                    Id = x.Id,
                    Name = x.Name,
                    Number = x.Number,
                    UserId = x.UserId,
                    UserName = x.UserName,
                    RequestStatus = x.RequestStatus,
                    RequestType = x.RequestType,
                    Dep3Name = x.Dep3Name,
                    Dep7Name = x.Dep7Name,
                    Position = x.Position
                }));
            result.AddRange(MissionDao.GetDocuments(
               user.Id,
               role,
               model.DepartmentId,
               0,
               0,
               model.StatusId,
               model.BeginDate,
               model.EndDate,
               model.UserName,
               model.SortBy, model.SortDescending, model.Number).ToList().ConvertAll(x => new AllRequestDto
               {
                   Date = x.Date,
                   BeginDate = x.BeginDate,
                   EndDate = x.EndDate,
                   EditUrl = "MissionEdit",
                   Id = x.Id,
                   Name = x.Name,
                   Number = x.Number,
                   UserId = x.UserId,
                   UserName = x.UserName,
                   RequestStatus = x.RequestStatus,
                   RequestType = x.RequestType,
                   Dep3Name = x.Dep3Name,
                   Dep7Name = x.Dep7Name,
                   Position = x.Position
               }));
            result.AddRange(TimesheetCorrectionDao.GetDocuments(
               user.Id,
               role,
               model.DepartmentId,
               0,
               0,
               model.StatusId,
               model.BeginDate,
               model.EndDate,
               model.UserName,
               model.SortBy, model.SortDescending, model.Number).ToList().ConvertAll(x => new AllRequestDto
               {
                   Date = x.Date,
                   BeginDate = x.BeginDate,
                   EndDate = x.EndDate,
                   EditUrl = "TimesheetCorrectionEdit",
                   Id = x.Id,
                   Name = x.Name,
                   Number = x.Number,
                   UserId = x.UserId,
                   UserName = x.UserName,
                   RequestStatus = x.RequestStatus,
                   RequestType = x.RequestType,
                   Dep3Name = x.Dep3Name,
                   Dep7Name = x.Dep7Name,
                   Position = x.Position
               }));
            result.AddRange(AbsenceDao.GetDocuments(
               user.Id,
               role,
               model.DepartmentId,
               0,
               0,
               model.StatusId,
               model.BeginDate,
               model.EndDate,
               model.UserName,
               model.SortBy, model.SortDescending, model.Number).ToList().ConvertAll(x => new AllRequestDto
               {
                   Date = x.Date,
                   BeginDate = x.BeginDate,
                   EndDate = x.EndDate,
                   EditUrl = "AbsenceEdit",
                   Id = x.Id,
                   Name = x.Name,
                   Number = x.Number,
                   UserId = x.UserId,
                   UserName = x.UserName,
                   RequestStatus = x.RequestStatus,
                   RequestType = x.RequestType,
                   Dep3Name = x.Dep3Name,
                   Dep7Name = x.Dep7Name,
                   Position = x.Position
               }));
            /*result.AddRange(HolidayWorkDao.GetDocuments(
              user.Id,
              role,
              model.DepartmentId,
              0,
              0,
              model.StatusId,
              model.BeginDate,
              model.EndDate).ToList().ConvertAll(x => new AllRequestDto
              {
                  Date = x.Date,
                  EditUrl = "HolidayWorkEdit",
                  Id = x.Id,
                  Name = x.Name,
                  UserId = x.UserId
              }));*/
            result.AddRange(VacationDao.GetDocuments(
              user.Id,
              role,
              model.DepartmentId,
              0,
              0,
              model.StatusId,
              model.BeginDate,
              model.EndDate,
              model.UserName,
              model.SortBy, model.SortDescending, model.Number).ToList().ConvertAll(x => new AllRequestDto
              {
                  Date = x.Date,
                  BeginDate = x.BeginDate,
                  EndDate = x.EndDate,
                  EditUrl = "VacationEdit",
                  Id = x.Id,
                  Name = x.Name,
                  Number = x.Number,
                  UserId = x.UserId,
                  UserName = x.UserName,
                  RequestStatus = x.RequestStatus,
                  RequestType = x.RequestType,
                  Dep3Name = x.Dep3Name,
                  Dep7Name = x.Dep7Name,
                  Position = x.Position
              }));

            result.AddRange(ChildVacationDao.GetDocuments(
             user.Id,
             role,
             model.DepartmentId,
             0,
             0,
             model.StatusId,
             model.BeginDate,
             model.EndDate,
             model.UserName,
             model.SortBy, model.SortDescending, model.Number).ToList().ConvertAll(x => new AllRequestDto
             {
                 Date = x.Date,
                 BeginDate = x.BeginDate,
                 EndDate = x.EndDate,
                 EditUrl = "ChildVacationEdit",
                 Id = x.Id,
                 Name = x.Name,
                 Number = x.Number,
                 UserId = x.UserId,
                 UserName = x.UserName,
                 RequestStatus = x.RequestStatus,
                 RequestType = x.RequestType,
                 Dep3Name = x.Dep3Name,
                 Dep7Name = x.Dep7Name,
                 Position = x.Position
             }));
            /*result.AddRange(EmploymentDao.GetDocuments(
               user.Id,
               role,
               model.DepartmentId,
               0,
               0,
               model.StatusId,
               model.BeginDate,
               model.EndDate).ToList().ConvertAll(x => new AllRequestDto
               {
                   Date = x.Date,
                   EditUrl = "EmploymentEdit",
                   Id = x.Id,
                   Name = x.Name,
                   UserId = x.UserId
               }));*/
            result.AddRange(DismissalDao.GetDocuments(
              user.Id,
              role,
              model.DepartmentId,
              0,
              0,
              model.StatusId,
              null, null,
              model.BeginDate,
              model.EndDate,              
              model.UserName,
              model.SortBy, model.SortDescending, model.Number).ToList().ConvertAll(x => new AllRequestDto
              {
                  Date = x.Date,
                  BeginDate = x.BeginDate,
                  EndDate = x.EndDate,
                  EditUrl = "DismissalEdit",
                  Id = x.Id,
                  Name = x.Name,
                  Number = x.Number,
                  UserId = x.UserId,
                  UserName = x.UserName,
                  RequestStatus = x.RequestStatus,
                  RequestType = x.RequestType,
                  Dep3Name = x.Dep3Name,
                  Dep7Name = x.Dep7Name,
                  Position = x.Position
              }));
            IEnumerable<AllRequestDto> res = SortResults(model, result);
            model.Documents = res.ToList();
        }
        protected IEnumerable<AllRequestDto> SortResults(AllRequestListModel model, List<AllRequestDto> result)
        {
            if (model.SortBy == 0 || !model.SortDescending.HasValue)
                return result;
            switch (model.SortBy)
            {
                case 1:
                    if (model.SortDescending.Value)
                        return result.OrderByDescending(x => x.Name);
                    return result.OrderBy(x => x.Name);
                case 2:
                    if (model.SortDescending.Value)
                        return result.OrderByDescending(x => x.UserName);
                    return result.OrderBy(x => x.UserName);
                case 3:
                    if (model.SortDescending.Value)
                        return result.OrderByDescending(x => x.Date);
                    return result.OrderBy(x => x.Date);
                case 4:
                    if (model.SortDescending.Value)
                        return result.OrderByDescending(x => x.RequestType);
                    return result.OrderBy(x => x.RequestType);
                case 5:
                    if (model.SortDescending.Value)
                        return result.OrderByDescending(x => x.RequestStatus);
                    return result.OrderBy(x => x.RequestStatus);
                case 6:
                    if (model.SortDescending.Value)
                        return result.OrderByDescending(x => x.Number);
                    return result.OrderBy(x => x.Number);
                case 7:
                    if (model.SortDescending.Value)
                        return result.OrderByDescending(x => x.Position);
                    return result.OrderBy(x => x.Position);
                case 8:
                    if (model.SortDescending.Value)
                        return result.OrderByDescending(x => x.Dep3Name);
                    return result.OrderBy(x => x.Dep3Name);
                case 9:
                    if (model.SortDescending.Value)
                        return result.OrderByDescending(x => x.Dep7Name);
                    return result.OrderBy(x => x.Dep7Name);
                default:
                    throw new ArgumentException(string.Format("Неправильное поля сортировки {0}", model.SortBy));
            }
        }
        #endregion

        #region Employment
        public EmploymentListModel GetEmploymentListModel()
        {
            User user = UserDao.Load(AuthenticationService.CurrentUser.Id);
            EmploymentListModel model = new EmploymentListModel
            {
                UserId = AuthenticationService.CurrentUser.Id,
            };
            SetDictionariesToModel(model, user);
            return model;
        }
        public void SetEmploymentListModel(EmploymentListModel model, bool hasError)
        {
            User user = UserDao.Load(model.UserId);
            SetDictionariesToModel(model, user);
            if (hasError)
                model.Documents = new List<VacationDto>();
            else
                SetDocumentsToModel(model, user);
        }
        public void SetDocumentsToModel(EmploymentListModel model, User user)
        {
            //model.Documents = new List<VacationDto>();

            UserRole role = (UserRole)(user.RoleId & (int)CurrentUser.UserRole);
            model.Documents = EmploymentDao.GetDocuments(
                user.Id,
                role,
                //model.DepartmentId,
                model.PositionId,
                model.TypeId,
                model.GraphicTypeId,
                model.StatusId,
                model.BeginDate,
                model.EndDate);
        }
        protected void SetDictionariesToModel(EmploymentListModel model, User user)
        {
            //model.Departments = GetDepartments(user);
            model.Types = GetEmploymentTypes(true);
            model.GraphicTypes = GetEmploymentGraphicTypes(true);
            model.Statuses = GetRequestStatuses();
            model.Positions = GetPositions(user);
        }
        protected List<IdNameDto> GetEmploymentGraphicTypes(bool addAll)
        {
            var typeList = EmploymentHoursTypeDao.LoadAllSorted().ToList().ConvertAll(x => new IdNameDto(x.Id, x.Name));
            if (addAll)
                typeList.Insert(0, new IdNameDto(0, SelectAll));
            return typeList;
        }
        protected List<IdNameDto> GetEmploymentTypes(bool addAll)
        {
            var typeList = EmploymentTypeDao.LoadAllSorted().ToList().ConvertAll(x => new IdNameDto(x.Id, x.Name));
            if (addAll)
                typeList.Insert(0, new IdNameDto(0, SelectAll));
            return typeList;
        }
        public EmploymentEditModel GetEmploymentEditModel(int id, int userId)
        {
            EmploymentEditModel model = new EmploymentEditModel { Id = id, UserId = userId };
            User user = UserDao.Load(userId);
            IUser current = AuthenticationService.CurrentUser;
            if (!CheckUserRights(user, current, id, false))
                throw new ArgumentException("Доступ запрещен.");
            SetUserInfoModel(user, model);
            SetAttachmentsToModel(model, id);
            Employment employment = null;
            if (id == 0)
            {
                model.CreatorLogin = current.Name;
                model.Version = 0;
                model.DateCreated = DateTime.Today.ToShortDateString();
            }
            else
            {
                employment = EmploymentDao.Load(id);
                if (employment == null)
                    throw new ArgumentException(string.Format("Прием на работу (id {0}) не найдена в базе данных.", id));
                model.Version = employment.Version;
                model.TypeId = employment.Type.Id;
                model.BeginDate = employment.BeginDate;
                //model.EndDate = employment.EndDate;
                model.GraphicTypeId = employment.HoursType.Id;
                model.PositionId = employment.Position.Id;
                model.AdditionId = employment.Addition == null ? 0 : employment.Addition.Id;
                model.TimesheetStatusId = employment.TimesheetStatus == null ? 0 : employment.TimesheetStatus.Id;
                model.Salary = employment.Salary.ToString();
                model.RegionFactor = GetModelValue(employment.RegionFactor);
                model.NorthFactor = GetModelValue(employment.NorthFactor);
                model.RegionAddition = GetModelValue(employment.RegionAddition);
                model.PersonalAddition = GetModelValue(employment.PersonalAddition);
                model.TravelWorkAddition = GetModelValue(employment.TravelWorkAddition);
                model.SkillAddition = GetModelValue(employment.SkillAddition);
                model.LongWorkAddition = GetModelValue(employment.LongWorkAddition);
                model.Probaion = employment.Probaion.ToString();
                model.Reason = employment.Reason;
                model.CreatorLogin = employment.Creator.Name;
                model.DocumentNumber = employment.Number.ToString();
                model.DateCreated = employment.CreateDate.ToShortDateString();
                SetHiddenFields(model);
                if (employment.DeleteDate.HasValue)
                    model.IsDeleted = true;
            }
            SetFlagsState(id, user, employment, model);
            LoadDictionaries(model);
            return model;
        }
        protected string GetModelValue(decimal? value)
        {
            return value.HasValue ? value.Value.ToString() : string.Empty;
        }
        public void ReloadDictionariesToModel(EmploymentEditModel model)
        {
            User user = UserDao.Load(model.UserId);
            IUser current = AuthenticationService.CurrentUser;
            SetUserInfoModel(user, model);
            LoadDictionaries(model);
            if (model.Id == 0)
            {
                model.CreatorLogin = current.Name;
                model.DateCreated = DateTime.Today.ToShortDateString();
            }
            else
            {
                Employment employment = EmploymentDao.Load(model.Id);
                model.CreatorLogin = employment.Creator.Name;
                model.DocumentNumber = employment.Number.ToString();
                model.DateCreated = employment.CreateDate.ToShortDateString();
            }
        }
        public bool SaveEmploymentEditModel(EmploymentEditModel model, /*UploadFilesDto filesDto,*/ out string error)
        {
            error = string.Empty;
            User user = null;
            try
            {
                user = UserDao.Load(model.UserId);
                IUser current = AuthenticationService.CurrentUser;
                if (!CheckUserRights(user, current, model.Id, true))
                {
                    error = "Редактирование заявки запрещено";
                    return false;
                }
                Employment employment;
                if (model.Id == 0)
                {
                    employment = new Employment
                    {
                        CreateDate = DateTime.Now,
                        Creator = UserDao.Load(current.Id),
                        Number = RequestNextNumberDao.GetNextNumberForType((int)RequestTypeEnum.Employment),
                        User = user
                    };
                    ChangeEntityProperties(current, employment, model, user);
                    EmploymentDao.SaveAndFlush(employment);
                    /*SendEmailForUserRequest(user, current, employment.Id, employment.Number,
                                            RequestTypeEnum.Employment, false);*/
                    model.Id = employment.Id;
                }
                else
                {
                    employment = EmploymentDao.Load(model.Id);
                    //SaveAttachments(employment.Id, filesDto, model);
                    if (employment.Version != model.Version)
                    {
                        error = "Заявка была изменена другим пользователем.";
                        model.ReloadPage = true;
                        return false;
                    }
                    if (model.IsDelete)
                    {
                        employment.DeleteDate = DateTime.Now;
                        RequestAttachmentDao.DeleteForEntityId(model.Id, RequestAttachmentTypeEnum.Employment);
                        EmploymentDao.SaveAndFlush(employment);
                        model.IsDelete = false;

                    }
                    else
                    {
                        ChangeEntityProperties(current, employment, model, user);
                        EmploymentDao.SaveAndFlush(employment);
                        //if(model.Version != employment.Version)
                        /*SendEmailForUserRequest(user, current, employment.Id, employment.Number,
                                        RequestTypeEnum.Employment, false);*/
                    }
                    if (employment.DeleteDate.HasValue)
                        model.IsDeleted = true;
                }
                model.DocumentNumber = employment.Number.ToString();
                model.Version = employment.Version;
                model.CreatorLogin = employment.Creator.Name;
                model.DateCreated = employment.CreateDate.ToShortDateString();
                SetFlagsState(employment.Id, user, employment, model);
                return true;
            }
            catch (Exception ex)
            {
                EmploymentDao.RollbackTran();
                Log.Error("Error on SaveEmploymentEditModel:", ex);
                error = string.Format("Исключение:{0}", ex.GetBaseException().Message);
                return false;
            }
            finally
            {
                SetUserInfoModel(user, model);
                LoadDictionaries(model);
                SetHiddenFields(model);
            }
        }
        //protected void SaveAttachments(int entityId, UploadFilesDto filesDto, EmploymentEditModel model)
        //{
        //    string fileName;
        //    int? attachmentId = SaveAttachment(entityId, model.AttachmentId, filesDto.attachment, RequestAttachmentTypeEnum.Employment , out fileName);
        //    if (attachmentId.HasValue)
        //    {
        //        model.AttachmentId = attachmentId.Value;
        //        model.Attachment = fileName;
        //    }
        //    attachmentId = SaveAttachment(entityId, model.PensAttachmentId, filesDto.penAttachment, RequestAttachmentTypeEnum.EmploymentPen, out fileName);
        //    if (attachmentId.HasValue)
        //    {
        //        model.PensAttachmentId = attachmentId.Value;
        //        model.PensAttachment = fileName;
        //    }
        //    attachmentId = SaveAttachment(entityId, model.InnAttachmentId, filesDto.innAttachment, RequestAttachmentTypeEnum.EmploymentInn, out fileName);
        //    if (attachmentId.HasValue)
        //    {
        //        model.InnAttachmentId = attachmentId.Value;
        //        model.InnAttachment = fileName;
        //    }
        //    attachmentId = SaveAttachment(entityId, model.NdflAttachmentId, filesDto.ndflAttachment, RequestAttachmentTypeEnum.EmploymentNdfl, out fileName);
        //    if (attachmentId.HasValue)
        //    {
        //        model.NdflAttachmentId = attachmentId.Value;
        //        model.NdflAttachment = fileName;
        //    }
        //}
        public int? SaveAttachment(int entityId, int id, UploadFileDto dto, RequestAttachmentTypeEnum type, out string attachment)
        {
            attachment = string.Empty;
            if (dto == null)
                return new int?();
            RequestAttachment attach = id != 0 ?
               RequestAttachmentDao.Load(id) :
               new RequestAttachment
               {
                   RequestId = entityId,
                   RequestType = (int)type,
                   CreatorRole = RoleDao.Load((int)CurrentUser.UserRole),
               };
            if (id == 0)
            {
                RequestAttachment existingAttach = RequestAttachmentDao.FindByRequestIdAndTypeId(entityId, type);
                if (existingAttach != null)
                {
                    Log.InfoFormat("Found existing attachment for request id {0} and type {1} (id {2})", entityId, type, existingAttach.Id);
                    attach = existingAttach;
                }
            }
            attach.DateCreated = DateTime.Now;
            attach.UncompressContext = dto.Context;
            attach.ContextType = dto.ContextType;
            attach.FileName = dto.FileName;
            attach.CreatorRole = RoleDao.Load((int)CurrentUser.UserRole);
            attach.Creator = UserDao.Load(CurrentUser.Id);
            RequestAttachmentDao.SaveAndFlush(attach);
            attachment = attach.FileName;
            return attach.Id;
        }
        protected void ChangeEntityProperties(IUser current, Employment entity, EmploymentEditModel model, User user)
        {
            if ((current.UserRole & UserRole.Employee) == UserRole.Employee && current.Id == model.UserId
                && !entity.UserDateAccept.HasValue
                && model.IsApprovedByUser)
            {
                entity.UserDateAccept = DateTime.Now;
                SendEmailForUserRequest(entity.User, current, entity.Creator, false, entity.Id,
                                        entity.Number, RequestTypeEnum.Employment, false);
            }
            if ((current.UserRole & UserRole.Manager) == UserRole.Manager && user.Manager != null
                && current.Id == user.Manager.Id
                && !entity.ManagerDateAccept.HasValue)
            {
                entity.TimesheetStatus = TimesheetStatusDao.Load(model.TimesheetStatusId);
                if (model.IsApprovedByManager)
                {
                    entity.ManagerDateAccept = DateTime.Now;
                    SendEmailForUserRequest(entity.User, current, entity.Creator, false, entity.Id,
                                        entity.Number, RequestTypeEnum.Employment, false);
                }
            }
            if ((current.UserRole & UserRole.PersonnelManager) == UserRole.PersonnelManager
                && (user.Personnels.Where(x => x.Id == current.Id).FirstOrDefault() != null)
                && !entity.PersonnelManagerDateAccept.HasValue)
            {
                entity.TimesheetStatus = TimesheetStatusDao.Load(model.TimesheetStatusId);
                if (model.IsApprovedByPersonnelManager)
                {
                    entity.PersonnelManagerDateAccept = DateTime.Now;
                    SendEmailForUserRequest(entity.User, current, entity.Creator, false, entity.Id,
                                        entity.Number, RequestTypeEnum.Employment, false);
                }
            }
            if (model.IsTypeEditable)
            {
                // ReSharper disable PossibleInvalidOperationException
                entity.BeginDate = model.BeginDate.Value;
                // ReSharper restore PossibleInvalidOperationException
                //entity.EndDate = model.EndDate;
                entity.Salary = GetTwoDigitValue(model.Salary);
                entity.RegionFactor = GetTwoDigitNullableValue(model.RegionFactor);
                entity.NorthFactor = GetTwoDigitNullableValue(model.NorthFactor);
                entity.RegionAddition = GetIntValue(model.RegionAddition);
                entity.PersonalAddition = GetIntValue(model.PersonalAddition);
                entity.TravelWorkAddition = GetIntValue(model.TravelWorkAddition);
                entity.SkillAddition = GetIntValue(model.SkillAddition);
                entity.LongWorkAddition = GetIntValue(model.LongWorkAddition);
                entity.Probaion = string.IsNullOrEmpty(model.Probaion) ? new int?() : Int32.Parse(model.Probaion);
                entity.Type = EmploymentTypeDao.Load(model.TypeId);
                entity.HoursType = EmploymentHoursTypeDao.Load(model.GraphicTypeId);
                entity.Addition = model.AdditionId == 0 ? null : EmploymentAdditionDao.Load(model.AdditionId);
                entity.Position = PositionDao.Load(model.PositionId);
            }
        }
        protected static decimal GetTwoDigitValue(string modelValue)
        {
            return (decimal)((int)(decimal.Parse(modelValue) * 100)) / 100;
        }
        protected static decimal? GetTwoDigitNullableValue(string modelValue)
        {
            if (string.IsNullOrEmpty(modelValue))
                return new decimal?();
            return (decimal)((int)(decimal.Parse(modelValue) * 100)) / 100;
        }
        protected static int? GetIntValue(string modelValue)
        {
            if (string.IsNullOrEmpty(modelValue))
                return new int?();
            return int.Parse(modelValue);
        }
        protected void SetAttachmentsToModel(EmploymentEditModel model, int id)
        {
            model.AttachmentsModel = GetAttachmentsModel(id, RequestAttachmentTypeEnum.Employment);
            //if (id == 0)
            //    return;
            //RequestAttachment attach = RequestAttachmentDao.FindByRequestIdAndTypeId(id, RequestAttachmentTypeEnum.Employment);
            //if (attach != null)
            //{
            //    model.AttachmentId = attach.Id;
            //    model.Attachment = attach.FileName;
            //}
            //RequestAttachment attachPen = RequestAttachmentDao.FindByRequestIdAndTypeId(id, RequestAttachmentTypeEnum.EmploymentPen);
            //if (attachPen != null)
            //{
            //    model.PensAttachmentId = attachPen.Id;
            //    model.PensAttachment = attachPen.FileName;
            //}
            //RequestAttachment attachInn = RequestAttachmentDao.FindByRequestIdAndTypeId(id, RequestAttachmentTypeEnum.EmploymentInn);
            //if (attachInn != null)
            //{
            //    model.InnAttachmentId = attachInn.Id;
            //    model.InnAttachment = attachInn.FileName;
            //}
            //RequestAttachment attachNdfl = RequestAttachmentDao.FindByRequestIdAndTypeId(id, RequestAttachmentTypeEnum.EmploymentNdfl);
            //if (attachNdfl != null)
            //{
            //    model.NdflAttachmentId = attachNdfl.Id;
            //    model.NdflAttachment = attachNdfl.FileName;
            //}
        }
        protected void LoadDictionaries(EmploymentEditModel model)
        {
            model.CommentsModel = GetCommentsModel(model.Id, (int)RequestTypeEnum.Employment);
            model.AttachmentsModel = GetAttachmentsModel(model.Id, RequestAttachmentTypeEnum.Employment);
            model.TimesheetStatuses = GetTimesheetStatusesForEmployment();
            model.Types = GetEmploymentTypes(false);
            model.GraphicTypes = GetEmploymentGraphicTypes(false);
            model.Additions = GetEmploymentAdditions();
            model.Positions = GetPositionsForEmployment();
        }
        protected List<IdNameDto> GetEmploymentAdditions()
        {
            var list = EmploymentAdditionDao.LoadAllSorted().ToList().ConvertAll(x => new IdNameDto(x.Id, x.Name));
            list.Insert(0, new IdNameDto(0, string.Empty));
            return list;
        }
        protected List<IdNameDto> GetPositionsForEmployment()
        {
            var list = PositionDao.LoadAllSorted().ToList().ConvertAll(x => new IdNameDto(x.Id, x.Name));
            //if (addAll)
            //    typeList.Insert(0, new IdNameDto(0, SelectAll));
            return list;
        }
        protected void SetHiddenFields(EmploymentEditModel model)
        {
            model.TypeIdHidden = model.TypeId;
            model.AdditionIdHidden = model.AdditionId;
            model.GraphicTypeIdHidden = model.GraphicTypeId;
            model.PositionIdHidden = model.PositionId;
            model.TimesheetStatusIdHidden = model.TimesheetStatusId;
        }
        protected List<IdNameDto> GetTimesheetStatusesForEmployment()
        {
            List<IdNameDto> dtos = TimesheetStatusDao.LoadAllSorted().ToList().ConvertAll(x => new IdNameDto(x.Id, x.Name));
            if ((AuthenticationService.CurrentUser.UserRole & UserRole.Employee) == UserRole.Employee)
                dtos.Insert(0, new IdNameDto(0, string.Empty));
            return dtos;
        }
        protected void SetFlagsState(int id, User user, Employment entity, EmploymentEditModel model)
        {
            SetFlagsState(model, false);
            UserRole currentUserRole = AuthenticationService.CurrentUser.UserRole;
            if (id == 0)
            {
                model.IsSaveAvailable = true;
                model.IsTypeEditable = true;
                switch (currentUserRole)
                {
                    case UserRole.Employee:
                        //model.IsApprovedByUserEnable = true;
                        break;
                    case UserRole.Manager:
                        model.IsApprovedByManagerEnable = true;
                        model.IsTimesheetStatusEditable = true;
                        break;
                    case UserRole.PersonnelManager:
                        model.IsApprovedByPersonnelManagerEnable = true;
                        model.IsTimesheetStatusEditable = true;
                        //model.IsPersonnelFieldsEditable = true;
                        break;
                }
                return;
            }
            model.IsApprovedByUserHidden = model.IsApprovedByUser = entity.UserDateAccept.HasValue;
            model.IsApprovedByManagerHidden = model.IsApprovedByManager = entity.ManagerDateAccept.HasValue;
            model.IsApprovedByPersonnelManagerHidden = model.IsApprovedByPersonnelManager = entity.PersonnelManagerDateAccept.HasValue;
            model.IsPostedTo1CHidden = model.IsPostedTo1C = entity.SendTo1C.HasValue;
            switch (currentUserRole)
            {
                case UserRole.Employee:
                    if (!entity.UserDateAccept.HasValue && !entity.DeleteDate.HasValue)
                    {
                        model.IsApprovedByUserEnable = true;
                        if (!entity.ManagerDateAccept.HasValue && !entity.PersonnelManagerDateAccept.HasValue && !entity.SendTo1C.HasValue)
                            model.IsTypeEditable = true;
                    }
                    break;
                case UserRole.Manager:
                    if (!entity.ManagerDateAccept.HasValue && !entity.DeleteDate.HasValue)
                    {
                        model.IsApprovedByManagerEnable = true;
                        if (!entity.PersonnelManagerDateAccept.HasValue && !entity.SendTo1C.HasValue)
                        {
                            model.IsTypeEditable = true;
                            model.IsTimesheetStatusEditable = true;
                        }
                    }
                    break;
                case UserRole.PersonnelManager:
                    if (!entity.PersonnelManagerDateAccept.HasValue)
                    {
                        model.IsApprovedByPersonnelManagerEnable = true;
                        if (!entity.SendTo1C.HasValue)
                        {
                            model.IsTypeEditable = true;
                            model.IsTimesheetStatusEditable = true;
                        }
                    }
                    else if (!entity.SendTo1C.HasValue && !entity.DeleteDate.HasValue)
                        model.IsDeleteAvailable = true;
                    break;
            }
            model.IsSaveAvailable = model.IsTypeEditable || model.IsTimesheetStatusEditable
                                    || model.IsApprovedByManagerEnable || model.IsApprovedByUserEnable ||
                                    model.IsApprovedByPersonnelManagerEnable;
        }
        protected void SetFlagsState(EmploymentEditModel model, bool state)
        {
            model.IsApprovedByManager = state;
            model.IsApprovedByManagerHidden = state;
            model.IsApprovedByManagerEnable = state;

            model.IsApprovedByPersonnelManager = state;
            model.IsApprovedByPersonnelManagerHidden = state;
            model.IsApprovedByPersonnelManagerEnable = state;

            model.IsApprovedByUser = state;
            model.IsApprovedByUserHidden = state;
            model.IsApprovedByUserEnable = state;

            model.IsPostedTo1C = state;
            model.IsPostedTo1CHidden = state;
            model.IsPostedTo1CEnable = state;

            model.IsSaveAvailable = state;
            model.IsTimesheetStatusEditable = state;
            model.IsTypeEditable = state;

            model.IsDelete = state;
            model.IsDeleteAvailable = state;
        }

        #endregion

        #region Timesheet Correction
        public TimesheetCorrectionListModel GetTimesheetCorrectionListModel()
        {
            User user = UserDao.Load(AuthenticationService.CurrentUser.Id);
            IdNameReadonlyDto dep = GetDepartmentDto(user);
            TimesheetCorrectionListModel model = new TimesheetCorrectionListModel
            {
                UserId = AuthenticationService.CurrentUser.Id,
                DepartmentName = dep.Name,
                DepartmentId = dep.Id,
                DepartmentReadOnly = dep.IsReadOnly,
                //Department = GetDepartmentDto(user),
            };
            SetDictionariesToModel(model, user);
            return model;
        }
        protected void SetDictionariesToModel(TimesheetCorrectionListModel model, User user)
        {
            //model.Departments = GetDepartments(user);
            model.Types = GetTimesheetCorrectionTypes(true);
            model.Statuses = GetRequestStatuses();
            model.Positions = GetPositions(user);
        }
        protected List<IdNameDto> GetTimesheetCorrectionTypes(bool addAll)
        {
            var typeList = TimesheetCorrectionTypeDao.LoadAllSorted().ToList().ConvertAll(x => new IdNameDto(x.Id, x.Name));
            if (addAll)
                typeList.Insert(0, new IdNameDto(0, SelectAll));
            return typeList;
        }
        public void SetTimesheetCorrectionListModel(TimesheetCorrectionListModel model, bool hasError)
        {
            User user = UserDao.Load(model.UserId);
            SetDictionariesToModel(model, user);
            if (hasError)
                model.Documents = new List<VacationDto>();
            else
                SetDocumentsToModel(model, user);
        }
        public void SetDocumentsToModel(TimesheetCorrectionListModel model, User user)
        {

            UserRole role = (UserRole)(user.RoleId & (int)CurrentUser.UserRole);
            model.Documents = TimesheetCorrectionDao.GetDocuments(
                user.Id,
                role,
                model.DepartmentId,
                //GetDepartmentId(model.Department),
                model.PositionId,
                model.TypeId,
                //0,
                model.StatusId,
                model.BeginDate,
                model.EndDate,
                model.UserName,
                model.SortBy,
                model.SortDescending,
                model.Number);
        }
        public TimesheetCorrectionEditModel GetTimesheetCorrectionEditModel(int id, int userId)
        {
            TimesheetCorrectionEditModel model = new TimesheetCorrectionEditModel { Id = id, UserId = userId };
            User user = UserDao.Load(userId);
            IUser current = AuthenticationService.CurrentUser;
            if (!CheckUserRights(user, current, id, false))
                throw new ArgumentException("Доступ запрещен.");
            SetUserInfoModel(user, model);
            TimesheetCorrection timesheetCorrection = null;
            if (id == 0)
            {
                model.CreatorLogin = current.Name;
                model.Version = 0;
                model.DateCreated = DateTime.Today.ToShortDateString();
            }
            else
            {
                timesheetCorrection = TimesheetCorrectionDao.Load(id);
                if (timesheetCorrection == null)
                    throw new ArgumentException(string.Format("Корректировка табеля (id {0}) не найдена в базе данных.", id));
                model.Version = timesheetCorrection.Version;
                model.TypeId = timesheetCorrection.Type.Id;
                model.EventDate = timesheetCorrection.EventDate;
                //model.Compensation = timesheetCorrection.Compensation.HasValue ? timesheetCorrection.Compensation.Value.ToString() : string.Empty;
                model.StatusId = timesheetCorrection.TimesheetStatus == null ? 0 : timesheetCorrection.TimesheetStatus.Id;
                model.Hours = timesheetCorrection.Hours.ToString();
                model.CreatorLogin = timesheetCorrection.Creator.Name;
                model.DocumentNumber = timesheetCorrection.Number.ToString();
                model.DateCreated = timesheetCorrection.CreateDate.ToShortDateString();
                SetHiddenFields(model);
                if (timesheetCorrection.DeleteDate.HasValue)
                    model.IsDeleted = true;
            }
            SetFlagsState(id, user, timesheetCorrection, model);
            LoadDictionaries(model);
            return model;
        }
        protected void LoadDictionaries(TimesheetCorrectionEditModel model)
        {
            model.CommentsModel = GetCommentsModel(model.Id, (int)RequestTypeEnum.TimesheetCorrection);
            model.Statuses = GetTimesheetStatusesForTimesheetCorrection();
            model.Types = GetTimesheetCorrectionTypes(false);
        }
        protected void SetHiddenFields(TimesheetCorrectionEditModel model)
        {
            model.TypeIdHidden = model.TypeId;
            model.StatusIdHidden = model.StatusId;
        }
        protected List<IdNameDto> GetTimesheetStatusesForTimesheetCorrection()
        {
            List<IdNameDto> dtos = TimesheetStatusDao.LoadAllSorted().
                Where(x =>
                    (
                        x.Name == "Вечерние часы" ||
                        x.Name == "Время простоя по вине работодателя" ||
                        x.Name == "Выходные и нерабочие дни" ||
                        x.Name == "Неявки по невыясненным причинам" ||
                        x.Name == "Ночные часы" ||
                        x.Name == "Прогулы (отсутствие на рабочем месте без уваж. причин в теч. времени, уст. законодательством)" ||
                        x.Name == "Продолжительность работы в выходные и нерабочие, праздничные дни" ||
                        x.Name == "Продолжительность сверхурочной работы" ||
                        x.Name == "Простои по вине сотрудника" ||
                        x.Name == "Явка"
                    )
                ).
                ToList().ConvertAll(x => new IdNameDto(x.Id, x.Name));
            if ((AuthenticationService.CurrentUser.UserRole & UserRole.Employee) == UserRole.Employee)
                dtos.Insert(0, new IdNameDto(0, string.Empty));
            return dtos;
        }
        protected void SetFlagsState(int id, User user, TimesheetCorrection entity, TimesheetCorrectionEditModel model)
        {
            SetFlagsState(model, false);
            UserRole currentUserRole = AuthenticationService.CurrentUser.UserRole;
            if (id == 0)
            {
                model.IsSaveAvailable = true;
                model.IsTypeEditable = true;
                model.IsApprovedEnable = true;
                switch (currentUserRole)
                {
                    case UserRole.Employee:
                        break;
                    case UserRole.Manager:
                        break;
                    case UserRole.PersonnelManager:
                        model.IsStatusEditable = true;
                        break;
                }
                if ((currentUserRole & UserRole.PersonnelManager) == UserRole.PersonnelManager || (currentUserRole & UserRole.Manager) == UserRole.Manager)
                {
                    model.IsApprovedByUserHidden = model.IsApprovedByUser = true;
                }
                return;
            }
            model.IsApprovedByUserHidden = model.IsApprovedByUser = entity.UserDateAccept.HasValue;
            model.IsApprovedByManagerHidden = model.IsApprovedByManager = entity.ManagerDateAccept.HasValue;
            model.IsApprovedByPersonnelManagerHidden = model.IsApprovedByPersonnelManager = entity.PersonnelManagerDateAccept.HasValue;
            model.IsPostedTo1CHidden = model.IsPostedTo1C = entity.SendTo1C.HasValue;
            switch (currentUserRole)
            {
                case UserRole.Employee:
                    if (!entity.UserDateAccept.HasValue && !entity.DeleteDate.HasValue)
                    {
                        //model.IsApprovedByUserEnable = true;
                        model.IsApprovedEnable = true;
                        if (!entity.ManagerDateAccept.HasValue && !entity.PersonnelManagerDateAccept.HasValue && !entity.SendTo1C.HasValue)
                            model.IsTypeEditable = true;
                    }
                    break;
                case UserRole.Manager:
                    if (!entity.ManagerDateAccept.HasValue && !entity.DeleteDate.HasValue)
                    {
                        model.IsApprovedEnable = true;
                        //model.IsApprovedByManagerEnable = true;
                        if (!entity.PersonnelManagerDateAccept.HasValue && !entity.SendTo1C.HasValue)
                        {
                            model.IsTypeEditable = true;
                            //model.IsStatusEditable = true;
                        }
                    }
                    break;
                case UserRole.PersonnelManager:
                    if (!entity.PersonnelManagerDateAccept.HasValue)
                    {
                        //model.IsApprovedByPersonnelManagerEnable = true;
                        model.IsApprovedEnable = true;
                        if (!entity.SendTo1C.HasValue)
                        {
                            model.IsTypeEditable = true;
                            model.IsStatusEditable = true;
                            //model.IsPersonnelFieldsEditable = true;
                        }
                    }
                    else if (!entity.SendTo1C.HasValue && !entity.DeleteDate.HasValue)
                        model.IsDeleteAvailable = true;
                    break;
            }
            model.IsSaveAvailable = model.IsTypeEditable || model.IsStatusEditable; //|| model.IsApprovedEnable;
            //|| model.IsApprovedByManagerEnable || model.IsApprovedByUserEnable || model.IsApprovedByPersonnelManagerEnable;
        }
        protected void SetFlagsState(TimesheetCorrectionEditModel model, bool state)
        {
            model.IsApprovedByManager = state;
            model.IsApprovedByManagerHidden = state;
            model.IsApprovedByManagerEnable = state;

            model.IsApprovedByPersonnelManager = state;
            model.IsApprovedByPersonnelManagerHidden = state;
            model.IsApprovedByPersonnelManagerEnable = state;

            model.IsApprovedByUser = state;
            model.IsApprovedByUserHidden = state;
            model.IsApprovedByUserEnable = state;

            model.IsPostedTo1C = state;
            model.IsPostedTo1CHidden = state;
            model.IsPostedTo1CEnable = state;

            model.IsSaveAvailable = state;
            model.IsStatusEditable = state;
            model.IsTypeEditable = state;
            //model.IsPersonnelFieldsEditable = state;

            model.IsDelete = state;
            model.IsDeleteAvailable = state;

            model.IsApproved = state;
            model.IsApprovedEnable = state;
        }
        public bool SaveTimesheetCorrectionEditModel(TimesheetCorrectionEditModel model, out string error)
        {
            error = string.Empty;
            User user = null;
            try
            {
                user = UserDao.Load(model.UserId);
                IUser current = AuthenticationService.CurrentUser;
                if (!CheckUserRights(user, current, model.Id, true))
                {
                    error = "Редактирование заявки запрещено";
                    return false;
                }
                TimesheetCorrection timesheetCorrection;
                if (model.Id == 0)
                {
                    timesheetCorrection = new TimesheetCorrection
                    {
                        CreateDate = DateTime.Now,
                        Creator = UserDao.Load(current.Id),
                        Number = RequestNextNumberDao.GetNextNumberForType((int)RequestTypeEnum.TimesheetCorrection),
                        User = user
                    };
                    ChangeEntityProperties(current, timesheetCorrection, model, user);
                    TimesheetCorrectionDao.SaveAndFlush(timesheetCorrection);
                    model.Id = timesheetCorrection.Id;
                }
                else
                {
                    timesheetCorrection = TimesheetCorrectionDao.Load(model.Id);
                    if (timesheetCorrection.Version != model.Version)
                    {
                        error = "Заявка была изменена другим пользователем.";
                        model.ReloadPage = true;
                        return false;
                    }
                    if (model.IsDelete)
                    {
                        timesheetCorrection.CreateDate = DateTime.Now;
                        timesheetCorrection.DeleteDate = DateTime.Now;
                        TimesheetCorrectionDao.SaveAndFlush(timesheetCorrection);
                        model.IsDelete = false;
                        SendEmailForUserRequest(timesheetCorrection.User, current,
                         timesheetCorrection.Creator, true, timesheetCorrection.Id,
                         timesheetCorrection.Number, RequestTypeEnum.TimesheetCorrection, false);
                    }
                    else
                    {
                        ChangeEntityProperties(current, timesheetCorrection, model, user);
                        TimesheetCorrectionDao.SaveAndFlush(timesheetCorrection);
                        if (timesheetCorrection.Version != model.Version)
                        {
                            timesheetCorrection.CreateDate = DateTime.Now;
                            TimesheetCorrectionDao.SaveAndFlush(timesheetCorrection);
                        }
                    }
                    if (timesheetCorrection.DeleteDate.HasValue)
                        model.IsDeleted = true;
                }
                model.DocumentNumber = timesheetCorrection.Number.ToString();
                model.Version = timesheetCorrection.Version;
                //model.DaysCount = dismissal.DaysCount;
                model.CreatorLogin = timesheetCorrection.Creator.Name;
                model.DateCreated = timesheetCorrection.CreateDate.ToShortDateString();
                SetFlagsState(timesheetCorrection.Id, user, timesheetCorrection, model);
                return true;
            }
            catch (Exception ex)
            {
                TimesheetCorrectionDao.RollbackTran();
                Log.Error("Error on SaveTimesheetCorrectionEditModel:", ex);
                error = string.Format("Исключение:{0}", ex.GetBaseException().Message);
                return false;
            }
            finally
            {
                SetUserInfoModel(user, model);
                LoadDictionaries(model);
                SetHiddenFields(model);
            }
        }
        protected void ChangeEntityProperties(IUser current, TimesheetCorrection entity, TimesheetCorrectionEditModel model, User user)
        {
            if ((current.UserRole & UserRole.Employee) == UserRole.Employee && current.Id == model.UserId
                && !entity.UserDateAccept.HasValue
                && model.IsApproved)
            {
                entity.UserDateAccept = DateTime.Now;
                SendEmailForUserRequest(entity.User, current, entity.Creator, false, entity.Id,
                           entity.Number, RequestTypeEnum.TimesheetCorrection, false);
            }
            if ((current.UserRole & UserRole.Manager) == UserRole.Manager && user.Manager != null
                && current.Id == user.Manager.Id)
            {
                if (model.IsApprovedByUser && !entity.UserDateAccept.HasValue)
                    entity.UserDateAccept = DateTime.Now;
                if (!entity.ManagerDateAccept.HasValue)
                {
                    //entity.TimesheetStatus = TimesheetStatusDao.Load(model.StatusId);
                    if (model.IsApproved)
                    {
                        entity.ManagerDateAccept = DateTime.Now;
                        SendEmailForUserRequest(entity.User, current, entity.Creator, false, entity.Id,
                         entity.Number, RequestTypeEnum.TimesheetCorrection, false);
                    }
                }
            }
            if ((current.UserRole & UserRole.Manager) == UserRole.PersonnelManager
                && (user.Personnels.Where(x => x.Id == current.Id).FirstOrDefault() != null)
                )
            {
                if (model.IsApprovedByUser && !entity.UserDateAccept.HasValue)
                    entity.UserDateAccept = DateTime.Now;
                if (!entity.PersonnelManagerDateAccept.HasValue)
                {
                    entity.TimesheetStatus = TimesheetStatusDao.Load(model.StatusId);
                    //entity.Compensation = string.IsNullOrEmpty(model.Compensation) ? new decimal?() : (decimal)((int)(decimal.Parse(model.Compensation) * 100)) / 100;
                    if (model.IsApproved)
                    {
                        entity.PersonnelManagerDateAccept = DateTime.Now;
                        SendEmailForUserRequest(entity.User, current, entity.Creator, false, entity.Id,
                         entity.Number, RequestTypeEnum.TimesheetCorrection, false);
                    }
                }
            }
            if (model.IsTypeEditable)
            {
                // ReSharper disable PossibleInvalidOperationException
                entity.EventDate = model.EventDate.Value;
                // ReSharper restore PossibleInvalidOperationException
                entity.Hours = Int32.Parse(model.Hours);
                entity.Type = TimesheetCorrectionTypeDao.Load(model.TypeId);
            }
        }
        public void ReloadDictionariesToModel(TimesheetCorrectionEditModel model)
        {
            User user = UserDao.Load(model.UserId);
            IUser current = AuthenticationService.CurrentUser;
            SetUserInfoModel(user, model);
            LoadDictionaries(model);
            if (model.Id == 0)
            {
                model.CreatorLogin = current.Name;
                model.DateCreated = DateTime.Today.ToShortDateString();
            }
            else
            {
                TimesheetCorrection timesheetCorrection = TimesheetCorrectionDao.Load(model.Id);
                model.CreatorLogin = timesheetCorrection.Creator.Name;
                model.DocumentNumber = timesheetCorrection.Number.ToString();
                model.DateCreated = timesheetCorrection.CreateDate.ToShortDateString();
            }
        }
        #endregion

        #region Dismissal
        public DismissalListModel GetDismissalListModel()
        {
            User user = UserDao.Load(AuthenticationService.CurrentUser.Id);
            IdNameReadonlyDto dep = GetDepartmentDto(user);
            DismissalListModel model = new DismissalListModel
            {
                UserId = AuthenticationService.CurrentUser.Id,
                DepartmentName = dep.Name,
                DepartmentId = dep.Id,
                DepartmentReadOnly = dep.IsReadOnly,
                SortBy = 0,
                SortDescending = null,
                //Department = GetDepartmentDto(user),
            };
            SetDictionariesToModel(model, user);
            SetFlagsState(user, model);
            SetInitialDates(model);
            return model;
        }
        public void SetDismissalListModel(DismissalListModel model, bool hasError)
        {
            User user = UserDao.Load(AuthenticationService.CurrentUser.Id);
            SetDictionariesToModel(model, user);
            if (hasError)
                model.Documents = new List<VacationDto>();
            else
            {
                if (model.Documents != null && ((user.UserRole & UserRole.PersonnelManager) == UserRole.PersonnelManager))
                {
                    if (model.IsOriginalReceivedModified)
                    {
                        model.IsOriginalReceivedModified = false;
                        List<int> idsToApplyReceivedOriginals = model.Documents.Where(x => x.IsOriginalReceived).Select(x => x.Id).ToList();
                        ApplyReceivedOriginals(model, idsToApplyReceivedOriginals);
                    }
                    if (model.IsPersonnelFileSentToArchiveModified)
                    {
                        model.IsPersonnelFileSentToArchiveModified = false;
                        List<int> idsToApplyPersonnelFileSentToArchive = model.Documents.Where(x => x.IsPersonnelFileSentToArchive).Select(x => x.Id).ToList();
                        ApplyPersonnelFileSentToArchive(model, idsToApplyPersonnelFileSentToArchive);
                    }
                }
                SetDocumentsToModel(model, user);
            }
            SetFlagsState(user, model);
        }

        protected void ApplyReceivedOriginals(DismissalListModel model, List<int> idsToApplyReceivedOriginals)
        {
            List<Dismissal> entities = DismissalDao.LoadForIdsList(idsToApplyReceivedOriginals).ToList();
            foreach (Dismissal entity in entities)
            {
                entity.IsOriginalReceived = true;
                DismissalDao.SaveAndFlush(entity);
            }
        }

        protected void ApplyPersonnelFileSentToArchive(DismissalListModel model, List<int> idsToApplyPersonnelFileSentToArchive)
        {
            List<Dismissal> entities = DismissalDao.LoadForIdsList(idsToApplyPersonnelFileSentToArchive).ToList();
            foreach (Dismissal entity in entities)
            {
                entity.IsPersonnelFileSentToArchive = true;
                DismissalDao.SaveAndFlush(entity);
            }
        }

        protected void SetFlagsState(User user, DismissalListModel model)
        {
            if ((user.UserRole & UserRole.PersonnelManager) == UserRole.PersonnelManager)
            {
                model.IsOriginalReceivedEditable = true;
                model.IsPersonnelFileSentToArchiveEditable = true;
            }
        }

        public void SetDocumentsToModel(DismissalListModel model, User user)
        {

            UserRole role = (UserRole)(user.RoleId & (int)CurrentUser.UserRole);
            model.Documents = DismissalDao.GetDocuments(
                user.Id,
                role,
                //model.DepartmentId,
                //GetDepartmentId(model.Department),
                model.DepartmentId,
                model.PositionId,
                model.TypeId,
                model.StatusId,
                //0,
                model.BeginDate,
                model.EndDate,
                model.BeginCreateDate,
                model.EndCreateDate,
                model.UserName,
                model.SortBy,
                model.SortDescending,
                model.Number);
        }
        protected void SetDictionariesToModel(DismissalListModel model, User user)
        {
            //model.Departments = GetDepartments(user);
            model.Types = GetDismissalTypes(true);
            model.Statuses = GetRequestStatuses(CurrentUser.UserRole);
            model.Positions = GetPositions(user);
        }
        protected List<IdNameDto> GetDismissalTypes(bool addAll)
        {
            var typeList = DismissalTypeDao.LoadAll().ToList().ConvertAll(x => new IdNameDto(x.Id, x.Name + " " + (x.Reason.Length > 64 ? x.Reason.Substring(0, 64) + " ..." : x.Reason))).OrderBy(x => x.Name).ToList();
            if (addAll)
                typeList.Insert(0, new IdNameDto(0, SelectAll));
            return typeList;
        }

        public DismissalEditModel GetDismissalEditModel(int id, int userId)
        {
            DismissalEditModel model = new DismissalEditModel { Id = id, UserId = userId };
            User user = UserDao.Load(userId);
            IUser current = AuthenticationService.CurrentUser;
            if (!CheckUserRights(user, current, id, false))
                throw new ArgumentException("Доступ запрещен.");
            SetUserInfoModel(user, model);
            SetAttachmentToModel(model, id, RequestAttachmentTypeEnum.Dismissal);
            SetOrderScanAttachmentToModel(model, id, RequestAttachmentTypeEnum.DismissalOrderScan);
            SetUnsignedOrderScanAttachmentToModel(model, id, RequestAttachmentTypeEnum.UnsignedDismissalOrderScan);
            SetT2ScanAttachmentToModel(model, id, RequestAttachmentTypeEnum.T2Scan);
            SetUnsignedT2ScanAttachmentToModel(model, id, RequestAttachmentTypeEnum.UnsignedT2Scan);
            SetDismissalAgreementScanAttachmentToModel(model, id, RequestAttachmentTypeEnum.DismissalAgreementScan);
            SetUnsignedDismissalAgreementScanAttachmentToModel(model, id, RequestAttachmentTypeEnum.UnsignedDismissalAgreementScan);
            SetF182NScanAttachmentToModel(model, id, RequestAttachmentTypeEnum.F182NScan);
            SetF2NDFLScanAttachmentToModel(model, id, RequestAttachmentTypeEnum.F2NDFLScan);
            SetWorkbookRequestScanAttachmentToModel(model, id, RequestAttachmentTypeEnum.WorkbookRequestScan);
            Dismissal dismissal = null;
            if (id == 0)
            {
                model.CreatorLogin = current.Name;
                model.Version = 0;
                model.DateCreated = DateTime.Today.ToShortDateString();
            }
            else
            {
                dismissal = DismissalDao.Load(id);
                if (dismissal == null)
                    throw new ArgumentException(string.Format("Командировка (id {0}) не найдена в базе данных.", id));
                model.Version = dismissal.Version;
                model.TypeId = dismissal.Type == null ? 0 : dismissal.Type.Id;
                model.EndDate = dismissal.EndDate;
                model.Compensation = dismissal.Compensation.HasValue ? dismissal.Compensation.Value.ToString() : string.Empty;
                model.Reduction = dismissal.Reduction.HasValue ? dismissal.Reduction.Value.ToString() : string.Empty;
                //model.StatusId = dismissal.TimesheetStatus == null ? 0 : dismissal.TimesheetStatus.Id;
                model.Reason = dismissal.Reason;
                model.CreatorLogin = dismissal.Creator.Name;
                model.DocumentNumber = dismissal.Number.ToString();
                model.DateCreated = dismissal.CreateDate.ToShortDateString();
                SetHiddenFields(model);
                if (dismissal.DeleteDate.HasValue)
                    model.IsDeleted = true;
            }
            SetFlagsState(id, user, dismissal, model);
            LoadDictionaries(model);
            return model;
        }
        protected void LoadDictionaries(DismissalEditModel model)
        {
            model.CommentsModel = GetCommentsModel(model.Id, (int)RequestTypeEnum.Dismissal);
            //model.Statuses = GetTimesheetStatusesForDismissal();
            model.Types = GetDismissalTypes(false);
        }
        protected void SetHiddenFields(DismissalEditModel model)
        {
            model.TypeIdHidden = model.TypeId;
            //model.StatusIdHidden = model.StatusId;
            //model.DaysCountHidden = model.DaysCount;
        }
        protected List<IdNameDto> GetTimesheetStatusesForDismissal()
        {
            List<IdNameDto> dtos = TimesheetStatusDao.LoadAllSorted().ToList().ConvertAll(x => new IdNameDto(x.Id, x.Name));

            if (AuthenticationService.CurrentUser.UserRole == UserRole.Employee)
                dtos.Insert(0, new IdNameDto(0, string.Empty));
            return dtos;
        }
        protected void SetFlagsState(int id, User user, Dismissal entity, DismissalEditModel model)
        {
            SetFlagsState(model, false);
            UserRole currentUserRole = AuthenticationService.CurrentUser.UserRole;
            model.IsPyrusLinkAvailable = true;
            if (id == 0)
            {
                model.IsSaveAvailable = true;
                model.IsTypeEditable = true;
                model.IsApprovedEnable = false;
                switch (currentUserRole)
                {
                    case UserRole.Employee:
                        model.IsPyrusLinkAvailable = false ;
                        //model.IsApprovedByUserEnable = false;
                        break;
                    case UserRole.Manager:
                        
                        //model.IsApprovedByManagerEnable = false;
                        //model.IsStatusEditable = true;
                        break;
                    case UserRole.PersonnelManager:
                        model.IsPyrusLinkAvailableForPersonnel = true;
                        //model.IsApprovedByPersonnelManagerEnable = false;
                        //model.IsStatusEditable = true;
                        model.IsPersonnelFieldsEditable = true;
                        break;
                }
                if ((currentUserRole & UserRole.PersonnelManager) == UserRole.PersonnelManager || (currentUserRole & UserRole.Manager) == UserRole.Manager)
                {
                    model.IsApprovedByUserEnable = false;
                    model.IsApprovedByUserHidden = model.IsApprovedByUser = true;
                }
                return;
            }
            model.IsApprovedByUserHidden = model.IsApprovedByUser = entity.UserDateAccept.HasValue;
            model.IsApprovedByManagerHidden = model.IsApprovedByManager = entity.ManagerDateAccept.HasValue;
            model.IsApprovedByPersonnelManagerHidden = model.IsApprovedByPersonnelManager = entity.PersonnelManagerDateAccept.HasValue;
            model.IsPostedTo1CHidden = model.IsPostedTo1C = entity.SendTo1C.HasValue;
            // hack to uncheck checkbox on UI
            if (entity.DeleteDate.HasValue && !entity.DeleteAfterSendTo1C)
                model.IsApprovedByPersonnelManager = false;

            RequestPrintForm form = RequestPrintFormDao.FindByRequestAndTypeId(id, RequestPrintFormTypeEnum.Dismissal);
            model.IsPrintAvailable = form != null;

            switch (currentUserRole)
            {
                case UserRole.Employee:
                    model.IsPyrusLinkAvailable = false;
                    if (!entity.UserDateAccept.HasValue && !entity.DeleteDate.HasValue)
                    {
                        if (model.AttachmentId > 0)
                            model.IsApprovedEnable = true;
                        if (!entity.ManagerDateAccept.HasValue && !entity.PersonnelManagerDateAccept.HasValue && !entity.SendTo1C.HasValue)
                            model.IsTypeEditable = true;

                        // Сотрудник может прикрепить заявление на выдачу ТК при статусе "Черновик"                        
                        model.IsWorkbookRequestAllowed = true;
                    }

                    if (model.IsPostedTo1C)
                    {
                        // Сотрудник может прикрепить сканы подписанных приказа, Т2 и соглашения
                        model.IsConfirmationAllowed = true;
                        model.IsT2Allowed = true;
                        model.IsDismissalAgreementAllowed = true;

                        model.IsViewDismissalAgreementAllowed = true;
                        model.IsViewF182NAllowed = true;
                        model.IsViewF2NDFLAllowed = true;
                    }

                    break;
                case UserRole.DismissedEmployee:
                    if (!entity.UserDateAccept.HasValue && !entity.DeleteDate.HasValue)
                    {
                        if (model.AttachmentId > 0)
                            model.IsApprovedEnable = true;
                        if (!entity.ManagerDateAccept.HasValue && !entity.PersonnelManagerDateAccept.HasValue && !entity.SendTo1C.HasValue)
                            model.IsTypeEditable = true;

                        // Уволенный может прикрепить заявление на выдачу ТК при статусе "Черновик"                        
                        model.IsWorkbookRequestAllowed = true;
                    }

                    if (model.IsPostedTo1C)
                    {
                        // Уволенный может прикрепить сканы подписанных приказа, Т2 и соглашения
                        model.IsConfirmationAllowed = true;
                        model.IsT2Allowed = true;
                        model.IsDismissalAgreementAllowed = true;

                        model.IsViewDismissalAgreementAllowed = true;
                        model.IsViewF182NAllowed = true;
                        model.IsViewF2NDFLAllowed = true;
                    }

                    break;
                case UserRole.Manager:
                    model.IsPyrusLinkAvailable = true;
                    //RequestPrintForm formMan = RequestPrintFormDao.FindByRequestAndTypeId(id, RequestPrintFormTypeEnum.Dismissal);
                    //model.IsPrintAvailable = formMan != null;
                    if (!entity.ManagerDateAccept.HasValue && !entity.DeleteDate.HasValue)
                    {
                        if (model.AttachmentId > 0)
                            model.IsApprovedEnable = true;
                        if (!entity.PersonnelManagerDateAccept.HasValue && !entity.SendTo1C.HasValue)
                        {
                            model.IsTypeEditable = true;
                            //model.IsStatusEditable = true;
                        }
                        // Руководитель может прикрепить за сотрудника заявление на выдачу ТК до согласования                        
                        model.IsWorkbookRequestAllowed = true;
                    }

                    if (model.IsPostedTo1C)
                    {
                        // Руководитель может прикрепить сканы подписанных приказа и Т-2 за сотрудника после выгрузки в 1С
                        model.IsConfirmationAllowed = true;
                        model.IsT2Allowed = true;
                    }

                    break;
                case UserRole.PersonnelManager:
                    // Кадровик может за сотрудника прикрепить скан подписанного приказа
                    model.IsPyrusLinkAvailableForPersonnel = true;
                    if (model.IsPostedTo1C && model.OrderScanAttachmentId <= 0)
                    {
                        model.IsConfirmationAllowed = true;
                    }

                    if (model.IsPostedTo1C)
                    {
                        // Кадровик загружает сканы на подпись сотруднику, если еще нет сканов подписанных документов
                        model.IsUnsignedConfirmationAllowed = !(model.OrderScanAttachmentId > 0);
                        model.IsUnsignedT2Allowed = !(model.T2ScanAttachmentId > 0);
                        model.IsUnsignedDismissalAgreementAllowed = !(model.DismissalAgreementScanAttachmentId > 0);
                        // а также 182-Н и 2-НДФЛ для ознакомления сотрудником
                        model.IsF182NAllowed = true;
                        model.IsF2NDFLAllowed = true;

                        // Кадровик имеет право на просмотр документов с ограничениями просмотра
                        model.IsViewDismissalAgreementAllowed = true;
                        model.IsViewF182NAllowed = true;
                        model.IsViewF2NDFLAllowed = true;
                    }

                    if (!entity.PersonnelManagerDateAccept.HasValue)
                    {
                        if (model.AttachmentId > 0)
                        {
                            model.IsApprovedEnable = true;
                            model.IsApprovedForAllEnable = true;
                        }
                        if (!entity.SendTo1C.HasValue)
                        {
                            model.IsTypeEditable = true;
                            //model.IsStatusEditable = true;
                            model.IsPersonnelFieldsEditable = true;
                        }

                        // Кадровик может прикрепить заявление на выдачу ТК до согласования
                        model.IsWorkbookRequestAllowed = true;
                    }
                    else if (!entity.SendTo1C.HasValue && !entity.DeleteDate.HasValue)
                        model.IsDeleteAvailable = true;
                    break;
                case UserRole.Estimator:
                case UserRole.OutsourcingManager:
                    if (entity.SendTo1C.HasValue && !entity.DeleteDate.HasValue)
                    {
                        model.IsDeleteAvailable = true;
                        model.IsViewDismissalAgreementAllowed = true;
                        model.IsViewF182NAllowed = true;
                        model.IsViewF2NDFLAllowed = true;
                    }
                    break;
            }
            model.IsSaveAvailable = model.IsTypeEditable /*|| model.IsStatusEditable*/|| model.IsPersonnelFieldsEditable;
            //|| model.IsApprovedByManagerEnable || model.IsApprovedByUserEnable ||
            //model.IsApprovedByPersonnelManagerEnable ;
        }
        protected void SetFlagsState(DismissalEditModel model, bool state)
        {
            model.IsApprovedByManager = state;
            model.IsApprovedByManagerHidden = state;
            model.IsApprovedByManagerEnable = state;

            model.IsApprovedByPersonnelManager = state;
            model.IsApprovedByPersonnelManagerHidden = state;
            model.IsApprovedByPersonnelManagerEnable = state;

            model.IsApprovedByUser = state;
            model.IsApprovedByUserHidden = state;
            model.IsApprovedByUserEnable = state;

            model.IsPostedTo1C = state;
            model.IsPostedTo1CHidden = state;
            model.IsPostedTo1CEnable = state;

            model.IsSaveAvailable = state;
            //model.IsStatusEditable = state;
            model.IsTypeEditable = state;
            model.IsPersonnelFieldsEditable = state;

            model.IsDelete = state;
            model.IsDeleteAvailable = state;
            model.IsPrintAvailable = state;

            model.IsApproved = state;
            model.IsApprovedEnable = state;

            model.IsApprovedForAll = state;
            model.IsApprovedForAllEnable = state;
        }

        public bool SaveDismissalEditModel(DismissalEditModel model, IDictionary<RequestAttachmentTypeEnum, UploadFileDto> fileDtos, out string error)
        {
            error = string.Empty;
            User user = null;
            try
            {
                user = UserDao.Load(model.UserId);
                IUser current = AuthenticationService.CurrentUser;
                if (!CheckUserRights(user, current, model.Id, true) || !CheckUserRightsForEntity(user, current, model))
                {
                    error = "Редактирование заявки запрещено";
                    return false;
                }
                Dismissal dismissal;

                if (model.Id == 0)
                #region Сохранение нового увольнения
                {
                    dismissal = new Dismissal
                    {
                        CreateDate = DateTime.Now,
                        Creator = UserDao.Load(current.Id),
                        Number = RequestNextNumberDao.GetNextNumberForType((int)RequestTypeEnum.Dismissal),
                        User = user
                    };
                    ChangeEntityProperties(current, dismissal, model, user);
                    DismissalDao.SaveAndFlush(dismissal);
                    model.Id = dismissal.Id;
                    //Отпарвка почты если кому-то в подборе оно нужно
                    var appointmentDao=Ioc.Resolve<IAppointmentDao>();
                    var appointments=appointmentDao.GetAppointmentForReasonPosition(model.UserId);
                    if (appointments!=null)
                        foreach (var el in appointments)
                        {
                            var email = el.Creator.Email;
                            if (String.IsNullOrWhiteSpace(email)) continue;
                            string body = String.Format("Создано заявление на увольнение №{0} для сотрудника {1}. Номер заявки на подбор {2}.",dismissal.Number,dismissal.User.Name,el.Number);
                            var res=SendEmail(email, "Создано заявление на увольнение", body);
                        }                   
                }
                #endregion
                else
                #region Сохранение существующего увольнения
                {
                    dismissal = DismissalDao.Load(model.Id);

                    #region Сканы
                    string fileName;
                    int? attachmentId = SaveAttachment(dismissal.Id,
                        model.AttachmentId,
                        fileDtos[RequestAttachmentTypeEnum.Dismissal],
                        RequestAttachmentTypeEnum.Dismissal,
                        out fileName);
                    if (attachmentId.HasValue)
                    {
                        model.AttachmentId = attachmentId.Value;
                        model.Attachment = fileName;
                    }

                    // ---------------------------------------
                    int? orderScanAttachmentId = SaveAttachment(dismissal.Id,
                        model.OrderScanAttachmentId,
                        fileDtos[RequestAttachmentTypeEnum.DismissalOrderScan],
                        RequestAttachmentTypeEnum.DismissalOrderScan,
                        out fileName);
                    if (orderScanAttachmentId.HasValue)
                    {
                        model.OrderScanAttachmentId = orderScanAttachmentId.Value;
                        model.OrderScanAttachment = fileName;
                    }
                    // ---------------------------------------
                    int? unsignedOrderScanAttachmentId = SaveAttachment(dismissal.Id,
                        model.UnsignedOrderScanAttachmentId,
                        fileDtos[RequestAttachmentTypeEnum.UnsignedDismissalOrderScan],
                        RequestAttachmentTypeEnum.UnsignedDismissalOrderScan,
                        out fileName);
                    if (unsignedOrderScanAttachmentId.HasValue)
                    {
                        model.UnsignedOrderScanAttachmentId = unsignedOrderScanAttachmentId.Value;
                        model.UnsignedOrderScanAttachment = fileName;
                    }
                    // ---------------------------------------
                    int? unsignedT2ScanAttachmentId = SaveAttachment(dismissal.Id,
                        model.UnsignedT2ScanAttachmentId,
                        fileDtos[RequestAttachmentTypeEnum.UnsignedT2Scan],
                        RequestAttachmentTypeEnum.UnsignedT2Scan,
                        out fileName);
                    if (unsignedT2ScanAttachmentId.HasValue)
                    {
                        model.UnsignedT2ScanAttachmentId = unsignedT2ScanAttachmentId.Value;
                        model.UnsignedT2ScanAttachment = fileName;
                    }
                    // ---------------------------------------
                    int? t2ScanAttachmentId = SaveAttachment(dismissal.Id,
                        model.T2ScanAttachmentId,
                        fileDtos[RequestAttachmentTypeEnum.T2Scan],
                        RequestAttachmentTypeEnum.T2Scan,
                        out fileName);
                    if (t2ScanAttachmentId.HasValue)
                    {
                        model.T2ScanAttachmentId = t2ScanAttachmentId.Value;
                        model.T2ScanAttachment = fileName;
                    }
                    // ---------------------------------------
                    int? unsignedDismissalAgreementScanAttachmentId = SaveAttachment(dismissal.Id,
                        model.UnsignedDismissalAgreementScanAttachmentId,
                        fileDtos[RequestAttachmentTypeEnum.UnsignedDismissalAgreementScan],
                        RequestAttachmentTypeEnum.UnsignedDismissalAgreementScan,
                        out fileName);
                    if (unsignedDismissalAgreementScanAttachmentId.HasValue)
                    {
                        model.UnsignedDismissalAgreementScanAttachmentId = unsignedDismissalAgreementScanAttachmentId.Value;
                        model.UnsignedDismissalAgreementScanAttachment = fileName;
                    }
                    // ---------------------------------------
                    int? dismissalAgreementScanAttachmentId = SaveAttachment(dismissal.Id,
                        model.DismissalAgreementScanAttachmentId,
                        fileDtos[RequestAttachmentTypeEnum.DismissalAgreementScan],
                        RequestAttachmentTypeEnum.DismissalAgreementScan,
                        out fileName);
                    if (dismissalAgreementScanAttachmentId.HasValue)
                    {
                        model.DismissalAgreementScanAttachmentId = dismissalAgreementScanAttachmentId.Value;
                        model.DismissalAgreementScanAttachment = fileName;
                    }
                    // ---------------------------------------
                    int? f182NScanAttachmentId = SaveAttachment(dismissal.Id,
                        model.F182NScanAttachmentId,
                        fileDtos[RequestAttachmentTypeEnum.F182NScan],
                        RequestAttachmentTypeEnum.F182NScan,
                        out fileName);
                    if (f182NScanAttachmentId.HasValue)
                    {
                        model.F182NScanAttachmentId = f182NScanAttachmentId.Value;
                        model.F182NScanAttachment = fileName;
                    }
                    // ---------------------------------------
                    int? f2NDFLScanAttachmentId = SaveAttachment(dismissal.Id,
                        model.F2NDFLScanAttachmentId,
                        fileDtos[RequestAttachmentTypeEnum.F2NDFLScan],
                        RequestAttachmentTypeEnum.F2NDFLScan,
                        out fileName);
                    if (f2NDFLScanAttachmentId.HasValue)
                    {
                        model.F2NDFLScanAttachmentId = f2NDFLScanAttachmentId.Value;
                        model.F2NDFLScanAttachment = fileName;
                    }
                    // ---------------------------------------
                    int? workbookRequestScanAttachmentId = SaveAttachment(dismissal.Id,
                        model.WorkbookRequestScanAttachmentId,
                        fileDtos[RequestAttachmentTypeEnum.WorkbookRequestScan],
                        RequestAttachmentTypeEnum.WorkbookRequestScan,
                        out fileName);
                    if (workbookRequestScanAttachmentId.HasValue)
                    {
                        model.WorkbookRequestScanAttachmentId = workbookRequestScanAttachmentId.Value;
                        model.WorkbookRequestScanAttachment = fileName;
                    }
                    // --------------------------------------- 
                    #endregion

                    if (dismissal.Version != model.Version)
                    {
                        error = "Заявка была изменена другим пользователем.";
                        model.ReloadPage = true;
                        return false;
                    }
                    if (model.IsDelete)
                    #region Отклонение увольнения
                    {
                        #region Удаление сканов
                        if (model.AttachmentId > 0)
                            RequestAttachmentDao.Delete(model.AttachmentId);
                        // ----------------------------
                        if (model.OrderScanAttachmentId > 0)
                            RequestAttachmentDao.Delete(model.OrderScanAttachmentId);
                        // ----------------------------
                        if (model.UnsignedOrderScanAttachmentId > 0)
                            RequestAttachmentDao.Delete(model.UnsignedOrderScanAttachmentId);
                        // ----------------------------
                        if (model.UnsignedT2ScanAttachmentId > 0)
                            RequestAttachmentDao.Delete(model.UnsignedT2ScanAttachmentId);
                        // ----------------------------
                        if (model.T2ScanAttachmentId > 0)
                            RequestAttachmentDao.Delete(model.T2ScanAttachmentId);
                        // ----------------------------
                        if (model.UnsignedDismissalAgreementScanAttachmentId > 0)
                            RequestAttachmentDao.Delete(model.UnsignedDismissalAgreementScanAttachmentId);
                        // ----------------------------
                        if (model.DismissalAgreementScanAttachmentId > 0)
                            RequestAttachmentDao.Delete(model.DismissalAgreementScanAttachmentId);
                        // ----------------------------
                        if (model.F182NScanAttachmentId > 0)
                            RequestAttachmentDao.Delete(model.F182NScanAttachmentId);
                        // ----------------------------
                        if (model.F2NDFLScanAttachmentId > 0)
                            RequestAttachmentDao.Delete(model.F2NDFLScanAttachmentId);
                        // ----------------------------
                        if (model.WorkbookRequestScanAttachmentId > 0)
                            RequestAttachmentDao.Delete(model.WorkbookRequestScanAttachmentId);
                        // ---------------------------- 

                        model.AttachmentId = 0;
                        model.Attachment = string.Empty;
                        model.OrderScanAttachmentId = 0;
                        model.OrderScanAttachment = string.Empty;
                        model.UnsignedOrderScanAttachmentId = 0;
                        model.UnsignedOrderScanAttachment = string.Empty;
                        model.UnsignedT2ScanAttachmentId = 0;
                        model.UnsignedT2ScanAttachment = string.Empty;
                        model.UnsignedDismissalAgreementScanAttachmentId = 0;
                        model.UnsignedDismissalAgreementScanAttachment = string.Empty;
                        model.DismissalAgreementScanAttachmentId = 0;
                        model.DismissalAgreementScanAttachment = string.Empty;
                        model.F182NScanAttachmentId = 0;
                        model.F182NScanAttachment = string.Empty;
                        model.F2NDFLScanAttachmentId = 0;
                        model.F2NDFLScanAttachment = string.Empty;
                        model.WorkbookRequestScanAttachmentId = 0;
                        model.WorkbookRequestScanAttachment = string.Empty;
                        #endregion

                        if ((current.UserRole & UserRole.OutsourcingManager) == UserRole.OutsourcingManager || (current.UserRole & UserRole.Estimator) == UserRole.Estimator)
                            dismissal.DeleteAfterSendTo1C = true;
                        dismissal.CreateDate = DateTime.Now;
                        dismissal.DeleteDate = DateTime.Now;
                        DismissalDao.SaveAndFlush(dismissal);
                        model.IsDelete = false;
                        SendEmailForUserRequest(dismissal.User, current, dismissal.Creator, true, dismissal.Id,
                            dismissal.Number, RequestTypeEnum.Dismissal, false);
                    }
                    #endregion
                    else
                    {
                        ChangeEntityProperties(current, dismissal, model, user);
                        DismissalDao.SaveAndFlush(dismissal);
                        if (dismissal.Version != model.Version)
                        {
                            dismissal.CreateDate = DateTime.Now;
                            DismissalDao.SaveAndFlush(dismissal);
                        }

                    }
                    if (dismissal.DeleteDate.HasValue)
                        model.IsDeleted = true;
                }
                #endregion

                model.DocumentNumber = dismissal.Number.ToString();
                model.Version = dismissal.Version;
                //model.DaysCount = dismissal.DaysCount;
                model.CreatorLogin = dismissal.Creator.Name;
                model.DateCreated = dismissal.CreateDate.ToShortDateString();
                SetFlagsState(dismissal.Id, user, dismissal, model);
                // create CCL approvals if the Dismissal has been approved by the user and two managers and CCL approvals have not been created before
                if (model.IsApprovedByManager && model.IsApprovedByPersonnelManager && model.IsApprovedByUser && dismissal.ClearanceChecklistApprovals.Count == 0)
                {
                    var activeClearanceChecklistRoles = ClearanceChecklistDao.GetClearanceChecklistRoles().Where<ClearanceChecklistRole>(role => role.DeleteDate == null);
                    foreach (var clearanceChecklistRole in activeClearanceChecklistRoles)
                    {
                        dismissal.ClearanceChecklistApprovals.Add(new ClearanceChecklistApproval
                        {
                            Dismissal = dismissal,
                            ClearanceChecklistRole = clearanceChecklistRole
                        });
                    }
                    DismissalDao.SaveAndFlush(dismissal);
                    SendEmailForClearanceChecklistNeedToApprove(ClearanceChecklistDao.GetClearanceChecklistApprovingAuthorities(), dismissal);
                }
                return true;
            }
            catch (Exception ex)
            {
                DismissalDao.RollbackTran();
                Log.Error("Error on SaveDismissalEditModel:", ex);
                error = string.Format("Исключение:{0}", ex.GetBaseException().Message);
                return false;
            }
            finally
            {
                SetUserInfoModel(user, model);
                LoadDictionaries(model);
                SetHiddenFields(model);
            }
        }
        protected void ChangeEntityProperties(IUser current, Dismissal entity, DismissalEditModel model, User user)
        {
            #region Согласование сотрудником
            if ((current.UserRole & UserRole.Employee) == UserRole.Employee && current.Id == model.UserId
                    && !entity.UserDateAccept.HasValue
                    && model.IsApproved)
            {
                entity.UserDateAccept = DateTime.Now;
                SendEmailForUserRequest(entity.User, current, entity.Creator, false, entity.Id,
                    entity.Number, RequestTypeEnum.Dismissal, false);
            }
            #endregion

            #region Согласование уволенным
            if ((current.UserRole & UserRole.DismissedEmployee) == UserRole.DismissedEmployee && current.Id == model.UserId
                    && !entity.UserDateAccept.HasValue
                    && model.IsApproved)
            {
                entity.UserDateAccept = DateTime.Now;
                SendEmailForUserRequest(entity.User, current, entity.Creator, false, entity.Id,
                    entity.Number, RequestTypeEnum.Dismissal, false);
            }
            #endregion

            #region Согласование руководителем
            bool canEdit = false;

            if (((current.UserRole & UserRole.Manager) == UserRole.Manager && IsCurrentManagerForUser(user, current, out canEdit))
                    || HasCurrentManualRoleForUser(user, current, UserManualRole.ApprovesCommonRequests, out canEdit))
            {
                if (!String.IsNullOrEmpty(model.PyrusNumber))
                    entity.PirusNumber = model.PyrusNumber;
                //entity.TimesheetStatus = TimesheetStatusDao.Load(model.StatusId);
                if (model.IsApprovedByUser && !entity.UserDateAccept.HasValue)
                    entity.UserDateAccept = DateTime.Now;
                if (!entity.ManagerDateAccept.HasValue)
                {
                    if (model.IsApproved)
                    {
                        entity.ManagerDateAccept = DateTime.Now;
                        SendEmailForUserRequest(entity.User, current, entity.Creator, false, entity.Id,
                            entity.Number, RequestTypeEnum.Dismissal, false);
                    }
                }
            }
            #endregion

            #region Согласование кадровиком
            if ((current.UserRole & UserRole.PersonnelManager) == UserRole.PersonnelManager
                    && (user.Personnels.Where(x => x.Id == current.Id).FirstOrDefault() != null)
                    )
            {
                if (model.IsApprovedByUser && !entity.UserDateAccept.HasValue)
                    entity.UserDateAccept = DateTime.Now;
                if (!entity.PersonnelManagerDateAccept.HasValue)
                {
                    entity.Reason = model.Reason;
                    entity.Type = DismissalTypeDao.Load(model.TypeId);
                    //entity.TimesheetStatus = TimesheetStatusDao.Load(model.StatusId);
                    entity.Compensation = string.IsNullOrEmpty(model.Compensation)
                                              ? new decimal?()
                                              : (decimal)((int)(decimal.Parse(model.Compensation) * 100)) / 100;
                    entity.Reduction = string.IsNullOrEmpty(model.Reduction)
                                             ? new decimal?()
                                             : (decimal)((int)(decimal.Parse(model.Reduction) * 100)) / 100;
                    if (model.IsApproved)
                    {
                        entity.PersonnelManagerDateAccept = DateTime.Now;
                        SendEmailForUserRequest(entity.User, current, entity.Creator, false, entity.Id,
                            entity.Number, RequestTypeEnum.Dismissal, false);
                    }
                    if (model.IsApprovedForAll && !entity.ManagerDateAccept.HasValue)
                        entity.ManagerDateAccept = DateTime.Now;
                }
            }
            #endregion

            if (model.IsTypeEditable)
            {
                // ReSharper disable PossibleInvalidOperationException
                entity.EndDate = model.EndDate.Value;
                // ReSharper restore PossibleInvalidOperationException

            }
        }

        public void ReloadDictionariesToModel(DismissalEditModel model)
        {
            User user = UserDao.Load(model.UserId);
            IUser current = AuthenticationService.CurrentUser;
            SetUserInfoModel(user, model);
            LoadDictionaries(model);
            if (model.Id == 0)
            {
                model.CreatorLogin = current.Name;
                model.DateCreated = DateTime.Today.ToShortDateString();
            }
            else
            {
                Dismissal dismissal = DismissalDao.Load(model.Id);
                model.CreatorLogin = dismissal.Creator.Name;
                model.DocumentNumber = dismissal.Number.ToString();
                model.DateCreated = dismissal.CreateDate.ToShortDateString();
            }
        }
        #endregion

        #region ClearanceChecklist

        public ClearanceChecklistListModel GetClearanceChecklistListModel()
        {
            User user = UserDao.Load(AuthenticationService.CurrentUser.Id);
            IdNameReadonlyDto dep = GetDepartmentDto(user);
            var model = new ClearanceChecklistListModel
            {
                UserId = AuthenticationService.CurrentUser.Id,
                DepartmentName = dep.Name,
                DepartmentId = dep.Id,
                DepartmentReadOnly = dep.IsReadOnly,
                SortBy = 0,
                SortDescending = null,
                //Department = GetDepartmentDto(user),
            };
            SetDictionariesToModel(model, user);
            SetInitialDates(model);
            return model;
        }

        public void SetClearanceChecklistListModel(ClearanceChecklistListModel model, bool hasError)
        {
            User user = UserDao.Load(model.UserId);
            SetDictionariesToModel(model, user);
            if (hasError)
                model.Documents = new List<ClearanceChecklistDto>();
            else
                SetDocumentsToModel(model, user);
        }

        protected void SetDictionariesToModel(ClearanceChecklistListModel model, User user)
        {
            //model.Departments = GetDepartments(user);
            //model.Types = GetDismissalTypes(true);
            model.Statuses = GetRequestStatuses();
            model.Positions = GetPositions(user);
        }

        protected void SetDocumentsToModel(ClearanceChecklistListModel model, User user)
        {
            model.DepartmentId = 0;
            model.PositionId = 0;
            model.Documents = ClearanceChecklistDao.GetClearanceChecklistDocuments(
                user.Id,
                user.UserRole,
                model.DepartmentId,
                model.PositionId,
                0,
                model.StatusId,
                model.BeginDate,
                model.EndDate,
                model.UserName,
                model.SortBy,
                model.SortDescending);
        }

        public ClearanceChecklistEditModel GetClearanceChecklistEditModel(int id, int userId)
        {
            const int MAX_DAYS_BEFORE_DISMISSAL = 14;

            string[] PIT_DISPLAY_ROLES = { "Бухгалтерия - выплаты", "Кадры" };

            var model = new ClearanceChecklistEditModel { Id = id, UserId = userId };

            // User Access Control
            // User user = UserDao.Load(AuthenticationService.CurrentUser.Id);
            User user = UserDao.Load(userId);
            IUser current = AuthenticationService.CurrentUser;
            User currentUser = UserDao.Load(current.Id);
            if (!CheckUserRights(user, current, id, false) && !IsRoleOwner(currentUser))
                throw new ArgumentException("Доступ запрещен.");
            // End User Access Control

            var clearanceChecklist = ClearanceChecklistDao.Load(id);
            if (clearanceChecklist == null)
                throw new ArgumentException(string.Format("Обходной лист (id {0}) не найден в базе данных.", id));
            foreach (var approval in clearanceChecklist.ClearanceChecklistApprovals)
            {
                IList<string> roleAuthorities = clearanceChecklistDao.GetClearanceChecklistRoleAuthorities(approval.ClearanceChecklistRole)
                    .Select<User, string>(roleAuthority => roleAuthority.FullName).ToList<string>();

                model.ClearanceChecklistApprovals.Add(
                    new ClearanceChecklistApprovalDto
                    {
                        Id = approval.Id,
                        ClearanceChecklistRole = approval.ClearanceChecklistRole.Description,
                        RoleAuthorities = roleAuthorities,
                        ApprovedBy = approval.ApprovedBy != null ? approval.ApprovedBy.FullName : string.Empty,
                        ApprovalDate = approval.ApprovalDate.HasValue ? approval.ApprovalDate.Value.ToString("dd.MM.yyyy") : "",
                        Comment = approval.Comment,

                        // Checking if the authenticated user has the extended role for approval
                        // and that the user's department is allowed to approve today.
                        // If both are OK the Active property is set
                        // and the view will output the approval link in the corresponding row
                        Active = (this.IsRoleOwner(currentUser, approval.ClearanceChecklistRole) ? true : false) &&
                            DateTime.Now >= clearanceChecklist.EndDate.AddDays(
                                approval.ClearanceChecklistRole.DaysForApproval == null ? -MAX_DAYS_BEFORE_DISMISSAL : -(int)approval.ClearanceChecklistRole.DaysForApproval)
                    }
                );
            }
            model.IsBottomEnabled = (current.UserRole & UserRole.OutsourcingManager) == UserRole.OutsourcingManager || (current.UserRole & UserRole.Estimator) == UserRole.Estimator ? true : false;
            model.RegistryNumber = clearanceChecklist.RegistryNumber;
            if (IsRoleOwner(currentUser, PIT_DISPLAY_ROLES) || (currentUser.UserRole & UserRole.OutsourcingManager) == UserRole.OutsourcingManager || (currentUser.UserRole & UserRole.Estimator) == UserRole.Estimator)
            {
                model.PersonalIncomeTax = clearanceChecklist.PersonalIncomeTax;
            }
            model.OKTMO = clearanceChecklist.OKTMO;
            model.DateCreated = clearanceChecklist.CreateDate.ToShortDateString();
            model.DocumentNumber = clearanceChecklist.Number.ToString();
            model.EndDate = clearanceChecklist.EndDate;
            model.CommentsModel = GetCommentsModel(model.Id, (int)RequestTypeEnum.ClearanceChecklist);
            SetUserInfoModel(user, model);

            return model;
        }

        public ClearanceChecklistEditModel GetClearanceChecklistEditModelByParentId(int parentId, int userId)
        {
            var parent = DismissalDao.Load(parentId);
            if (parent == null || parent == null || parent.Id == 0)
                throw new ArgumentException(string.Format("Обходной лист для увольнения (id {0}) не найден в базе данных.", parentId));
            var model = GetClearanceChecklistEditModel(parent.Id, userId);
            return model;
        }

        public bool SaveClearanceChecklistEditModel(ClearanceChecklistEditModel model, out string error)
        {
            // STUB: CCL SaveClearanceChecklistEditModel
            error = "";
            return false;
        }

        public bool SetClearanceChecklistApproval(int approvalId, int approvedBy, out ClearanceChecklistApprovalDto modifiedApproval, out string error)
        {
            User user = UserDao.Load(AuthenticationService.CurrentUser.Id);

            if (!IsRoleOwner(user, ClearanceChecklistDao.GetApprovalById(approvalId).ClearanceChecklistRole))
            {
                throw new ArgumentException("Доступ запрещен.");
            }
            if (clearanceChecklistDao.SetApproval(approvalId, approvedBy, out modifiedApproval))
            {
                error = "";
                return true;
            }
            else
            {
                error = "Error updating the record";
                return false;
            }
        }

        public bool SetClearanceChecklistComment(int approvalId, string comment, out string error)
        {
            const int MAX_COMMENT_LENGTH = 255;
            error = String.Empty;

            User user = UserDao.Load(AuthenticationService.CurrentUser.Id);

            if (!IsRoleOwner(user, ClearanceChecklistDao.GetApprovalById(approvalId).ClearanceChecklistRole))
            {
                throw new ArgumentException("Доступ запрещен.");
            }

            if (comment.Length > MAX_COMMENT_LENGTH) comment = comment.Substring(0, MAX_COMMENT_LENGTH);
            if (clearanceChecklistDao.SetComment(approvalId, comment))
            {
                return true;
            }
            else
            {
                error = "Error updating comment";
                return false;
            }
        }

        public bool SetClearanceChecklistBottomFields(int id, int? registryNumber, decimal? personalIncomeTax, string oKTMO, out string error)
        {
            IUser current = AuthenticationService.CurrentUser;
            Regex oKTMORegEx = new Regex(@"^\d{8}$|^$");
            error = String.Empty;

            if ((current.UserRole & UserRole.OutsourcingManager) != UserRole.OutsourcingManager && (current.UserRole & UserRole.Estimator) != UserRole.Estimator)
            {
                throw new ArgumentException("Доступ запрещен.");
            }

            // Field format checks
            if (!oKTMORegEx.IsMatch(oKTMO))
            {
                error += OKTMOFormatError;
            }

            if (error == String.Empty)
            {
                if (clearanceChecklistDao.SetBottomFields(id, registryNumber, personalIncomeTax, oKTMO))
                {
                    return true;
                }
                else
                {
                    error = "Error updating fields";
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Checks if the given user owns the given clearance checklist role
        /// </summary>
        /// <param name="user"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        private bool IsRoleOwner(User user, ClearanceChecklistRole role)
        {
            IEnumerable<int> userRoleIds = user.ClearanceChecklistRoleRecords.Select(roleRecord => roleRecord.Role.Id);
            return userRoleIds.Contains<int>(role.Id) ? true : false;
        }

        private bool IsRoleOwner(User user, string roleDescription)
        {
            IEnumerable<string> userRoleDescriptions = user.ClearanceChecklistRoleRecords.Select(roleRecord => roleRecord.Role.Description);
            return userRoleDescriptions.Contains<string>(roleDescription) ? true : false;
        }

        private bool IsRoleOwner(User user, string[] roleDescriptions)
        {
            bool isRoleOwner = false;
            IEnumerable<string> userRoleDescriptions = user.ClearanceChecklistRoleRecords.Select(roleRecord => roleRecord.Role.Description);
            foreach (var roleDescription in roleDescriptions)
            {
                if (userRoleDescriptions.Contains<string>(roleDescription))
                {
                    isRoleOwner = true;
                }
            }

            return isRoleOwner;
        }

        /// <summary>
        /// Checks if the given user owns any clearance checklist role
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private bool IsRoleOwner(User user)
        {
            return (user.ClearanceChecklistRoleRecords != null && user.ClearanceChecklistRoleRecords.Count > 0);
        }

        #endregion

        #region Mission
        public AnalyticalStatementModel GetAnalyticalStatementModel()
        {
            IdNameReadonlyDto dep = GetDepartmentDto(UserDao.Load(CurrentUser.Id));
            var result = new AnalyticalStatementModel
            {
                UserId = CurrentUser.Id,
                DepartmentId = dep.Id,
                DepartmentName = dep.Name,
                DepartmentReadOnly = dep.IsReadOnly
            };
            return result;
        }
        public IList<AnalyticalStatementDto> GetAnalyticalStatements(string name, int departamentId, DateTime? beginDate, DateTime? endDate, string Number, int sortBy, bool? SortDescending)
        {
            return AnalyticalStatementDao.GetDocuments(
                CurrentUser.Id,
                CurrentUser.UserRole,
                departamentId,
                beginDate,
                endDate,
                name,
                Number,
                sortBy,
                SortDescending);

        }
        public MissionListModel GetMissionListModel()
        {
            User user = UserDao.Load(AuthenticationService.CurrentUser.Id);
            IdNameReadonlyDto dep = GetDepartmentDto(user);
            MissionListModel model = new MissionListModel
            {
                UserId = AuthenticationService.CurrentUser.Id,
                DepartmentName = dep.Name,
                DepartmentId = dep.Id,
                DepartmentReadOnly = dep.IsReadOnly,
                //Department = GetDepartmentDto(user),
            };
            SetDictionariesToModel(model, user);
            SetInitialDates(model);
            return model;
        }
        public void SetMissionListModel(MissionListModel model, bool hasError)
        {
            User user = UserDao.Load(model.UserId);
            SetDictionariesToModel(model, user);
            if (hasError)
                model.Documents = new List<MissionDto>();
            else
            {
                if (model.IsApproveClick)
                {
                    model.HasErrors = false;
                    model.IsApproveClick = false;
                    if (model.Documents != null)
                    {
                        List<int> idsForApprove = model.Documents.Where(x => x.IsChecked).Select(x => x.Id).ToList();
                        SetRecalculateDateToMissions(model, idsForApprove);
                    }
                }
                SetDocumentsToModel(model, user);
            }
        }
        protected void SetRecalculateDateToMissions(MissionListModel model, List<int> idsForApprove)
        {
            if (idsForApprove.Count == 0)
                return;
            try
            {
                MissionDao.SetRecalculateDate(idsForApprove);
            }
            catch (Exception ex)
            {
                Log.Error("Exception on SetRecalculateDate", ex);
                model.HasErrors = true;
            }
        }
        protected void SetDictionariesToModel(MissionListModel model, User user)
        {
            //model.Departments = GetDepartments(user);
            model.Types = GetMissionTypes(true);
            model.Statuses = GetRequestStatuses(CurrentUser.UserRole);
            model.Positions = GetPositions(user);
        }
        protected List<IdNameDto> GetMissionTypes(bool addAll)
        {
            var typeList = MissionTypeDao.LoadAllSorted().ToList().ConvertAll(x => new IdNameDto(x.Id, x.Name));
            if (addAll)
                typeList.Insert(0, new IdNameDto(0, SelectAll));
            else
            {
                List<IdNameDto> result = new List<IdNameDto>();
                foreach (IdNameDto dto in typeList)
                {
                    if (dto.Id == 1)
                        result.Insert(0, dto);
                    else
                        result.Add(dto);
                }
                return result;
            }
            return typeList;
        }
        public void SetDocumentsToModel(MissionListModel model, User user)
        {
            UserRole role = (UserRole)(user.RoleId & (int)CurrentUser.UserRole);
            model.Documents = MissionDao.GetDocuments(
                user.Id,
                role,
                //GetDepartmentId(model.Department),
                model.DepartmentId,
                model.PositionId,
                model.TypeId,
                //0,
                model.StatusId,
                model.BeginDate,
                model.EndDate,
                model.UserName,
                model.SortBy,
                model.SortDescending,
                model.Number);
        }
        public MissionEditModel GetMissionEditModel(int id, int userId)
        {
            MissionEditModel model = new MissionEditModel { Id = id, UserId = userId };
            User user = UserDao.Load(userId);
            IUser current = AuthenticationService.CurrentUser;
            if (!CheckUserRights(user, current, id, false))
                throw new ArgumentException("Доступ запрещен.");
            SetUserInfoModel(user, model);
            SetOrderScanAttachmentToModel(model, id, RequestAttachmentTypeEnum.MissionOrderScan);
            Mission mission = null;
            if (id == 0)
            {
                model.CreatorLogin = current.Name;
                model.Version = 0;
                model.DateCreated = DateTime.Today.ToShortDateString();
            }
            else
            {
                mission = MissionDao.Load(id);
                if (mission == null)
                    throw new ArgumentException(string.Format("Командировка (id {0}) не найдена в базе данных.", id));
                model.Version = mission.Version;
                model.TypeId = mission.Type.Id;
                model.BeginDate = mission.BeginDate;
                model.EndDate = mission.EndDate;
                model.DaysCount = mission.DaysCount;
                model.TimesheetStatusId = mission.TimesheetStatus == null ? 0 : mission.TimesheetStatus.Id;
                model.Country = mission.Country;
                model.MissionOrganization = mission.Organization;
                model.Goal = mission.Goal;
                model.FinancesSource = mission.FinancesSource;
                model.Reason = mission.Reason;
                model.CreatorLogin = mission.Creator.Name;
                model.DocumentNumber = mission.Number.ToString();
                model.DateCreated = mission.CreateDate.ToShortDateString();
                SetHiddenFields(model);
                if (mission.DeleteDate.HasValue)
                    model.IsDeleted = true;
            }
            SetFlagsState(id, user, mission, model);
            LoadDictionaries(model);
            return model;
        }
        protected void SetFlagsState(int id, User user, Mission entity, MissionEditModel model)
        {
            SetFlagsState(model, false);
            UserRole currentUserRole = AuthenticationService.CurrentUser.UserRole;
            if (id == 0)
            {
                model.IsSaveAvailable = true;
                model.IsTypeEditable = true;
                model.IsApprovedEnable = true;

                switch (currentUserRole)
                {
                    case UserRole.Employee:
                        //model.IsApprovedByUserEnable = false;
                        break;
                    case UserRole.Manager:
                        //model.IsApprovedByManagerEnable = false;
                        //model.IsTimesheetStatusEditable = true;
                        break;
                    case UserRole.PersonnelManager:
                        //model.IsApprovedByPersonnelManagerEnable = false;
                        model.IsTimesheetStatusEditable = true;
                        model.IsApprovedForAllEnable = true;
                        model.IsReasonEditable = true;
                        break;
                }
                if ((currentUserRole & UserRole.PersonnelManager) == UserRole.PersonnelManager || (currentUserRole & UserRole.Manager) == UserRole.Manager)
                {
                    model.IsApprovedByUserEnable = false;
                    model.IsApprovedByUserHidden = model.IsApprovedByUser = true;
                }
                return;
            }
            model.IsApprovedByUserHidden = model.IsApprovedByUser = entity.UserDateAccept.HasValue;
            model.IsApprovedByManagerHidden = model.IsApprovedByManager = entity.ManagerDateAccept.HasValue;
            model.IsApprovedByPersonnelManagerHidden = model.IsApprovedByPersonnelManager = entity.PersonnelManagerDateAccept.HasValue;
            model.IsPostedTo1CHidden = model.IsPostedTo1C = entity.SendTo1C.HasValue;

            // hack to uncheck checkbox on UI
            if (entity.DeleteDate.HasValue && !entity.DeleteAfterSendTo1C)
                model.IsApprovedByPersonnelManager = false;

            RequestPrintForm form = RequestPrintFormDao.FindByRequestAndTypeId(id, RequestPrintFormTypeEnum.MissionOrder);
            model.IsPrintOrderAvailable = form != null;
            RequestPrintForm formCertificate = RequestPrintFormDao.FindByRequestAndTypeId(id, RequestPrintFormTypeEnum.MissionCertificate);
            model.IsPrintCertificateAvailable = formCertificate != null;

            switch (currentUserRole)
            {
                case UserRole.Employee:
                    if (!entity.UserDateAccept.HasValue && !entity.DeleteDate.HasValue)
                    {
                        //model.IsApprovedByUserEnable = true;
                        model.IsApprovedEnable = true;
                        if (!entity.ManagerDateAccept.HasValue && !entity.PersonnelManagerDateAccept.HasValue
                            && !entity.SendTo1C.HasValue)
                            model.IsTypeEditable = true;
                    }
                    break;
                case UserRole.Manager:
                    if (!entity.ManagerDateAccept.HasValue && !entity.DeleteDate.HasValue)
                    {
                        //model.IsApprovedByManagerEnable = true;
                        model.IsApprovedEnable = true;
                        if (!entity.PersonnelManagerDateAccept.HasValue && !entity.SendTo1C.HasValue)
                        {
                            model.IsTypeEditable = true;
                            //model.IsTimesheetStatusEditable = true;
                        }
                    }
                    break;
                case UserRole.PersonnelManager:
                    if (model.IsPostedTo1C && model.OrderScanAttachmentId <= 0)
                    {
                        model.IsConfirmationAllowed = true;
                    }
                    if (!entity.PersonnelManagerDateAccept.HasValue)
                    {
                        //model.IsApprovedByPersonnelManagerEnable = true;
                        model.IsApprovedEnable = true;
                        model.IsApprovedForAllEnable = true;
                        if (!entity.SendTo1C.HasValue)
                        {
                            model.IsTypeEditable = true;
                            model.IsTimesheetStatusEditable = true;
                            model.IsReasonEditable = true;
                        }
                    }
                    else if (!entity.SendTo1C.HasValue && !entity.DeleteDate.HasValue)
                        model.IsDeleteAvailable = true;
                    break;
                case UserRole.Estimator:
                case UserRole.OutsourcingManager:
                    if (entity.SendTo1C.HasValue && !entity.DeleteDate.HasValue)
                        model.IsDeleteAvailable = true;
                    break;
            }
            model.IsSaveAvailable = model.IsTypeEditable || model.IsTimesheetStatusEditable
                /*|| model.IsApprovedByManagerEnable || model.IsApprovedByUserEnable || model.IsApprovedByPersonnelManagerEnable*/
                                    || model.IsReasonEditable; //|| model.IsApprovedEnable;
        }
        protected void SetFlagsState(MissionEditModel model, bool state)
        {
            model.IsApprovedByManager = state;
            model.IsApprovedByManagerHidden = state;
            model.IsApprovedByManagerEnable = state;

            model.IsApprovedByPersonnelManager = state;
            model.IsApprovedByPersonnelManagerHidden = state;
            model.IsApprovedByPersonnelManagerEnable = state;

            model.IsApprovedByUser = state;
            model.IsApprovedByUserHidden = state;
            model.IsApprovedByUserEnable = state;

            model.IsPostedTo1C = state;
            model.IsPostedTo1CHidden = state;
            model.IsPostedTo1CEnable = state;

            model.IsSaveAvailable = state;
            model.IsTimesheetStatusEditable = state;
            model.IsTypeEditable = state;
            model.IsReasonEditable = state;

            model.IsDelete = state;
            model.IsDeleteAvailable = state;

            model.IsPrintOrderAvailable = state;
            model.IsPrintCertificateAvailable = state;

            model.IsApproved = state;
            model.IsApprovedEnable = state;

            model.IsApprovedForAll = state;
            model.IsApprovedForAllEnable = state;
        }
        protected void LoadDictionaries(MissionEditModel model)
        {
            model.CommentsModel = GetCommentsModel(model.Id, (int)RequestTypeEnum.Mission);
            model.TimesheetStatuses = GetTimesheetStatusesForMission();
            model.Types = GetMissionTypes(false);
        }
        protected List<IdNameDto> GetTimesheetStatusesForMission()
        {
            List<IdNameDto> dtos = TimesheetStatusDao.LoadAllSorted().
                Where(x => (x.Id == 7)).ToList().
                ConvertAll(x => new IdNameDto(x.Id, x.Name)).OrderBy(x => x.Name).ToList();
            if (AuthenticationService.CurrentUser.UserRole == UserRole.Employee)
                dtos.Insert(0, new IdNameDto(0, string.Empty));
            return dtos;
        }
        protected void SetHiddenFields(MissionEditModel model)
        {
            model.TypeIdHidden = model.TypeId;
            model.TimesheetStatusIdHidden = model.TimesheetStatusId;
            model.DaysCountHidden = model.DaysCount;
        }
        public bool SaveMissionEditModel(MissionEditModel model, UploadFileDto orderScanFileDto, out string error)
        {
            error = string.Empty;
            User user = null;
            try
            {
                user = UserDao.Load(model.UserId);
                IUser current = AuthenticationService.CurrentUser;
                if (!CheckUserRights(user, current, model.Id, true) || !CheckUserRightsForEntity(user, current, model))
                {
                    error = "Редактирование заявки запрещено";
                    return false;
                }
                Mission mission;

                if (model.Id == 0)
                #region Сохранение новой командировки
                {
                    mission = new Mission
                    {
                        CreateDate = DateTime.Now,
                        Creator = UserDao.Load(current.Id),
                        Number = RequestNextNumberDao.GetNextNumberForType((int)RequestTypeEnum.Mission),
                        User = user
                    };
                    ChangeEntityProperties(current, mission, model, user);
                    MissionDao.SaveAndFlush(mission);
                    model.Id = mission.Id;
                }
                #endregion
                else
                #region Сохранение существующей командировки
                {
                    mission = MissionDao.Load(model.Id);
                    string fileName;
                    // ---------------------------------------
                    int? orderScanAttachmentId = SaveAttachment(mission.Id, model.OrderScanAttachmentId, orderScanFileDto, RequestAttachmentTypeEnum.MissionOrderScan, out fileName);
                    if (orderScanAttachmentId.HasValue)
                    {
                        model.OrderScanAttachmentId = orderScanAttachmentId.Value;
                        model.OrderScanAttachment = fileName;
                    }
                    // ---------------------------------------
                    if (mission.Version != model.Version)
                    {
                        error = "Заявка была изменена другим пользователем.";
                        model.ReloadPage = true;
                        return false;
                    }
                    if (model.IsDelete)
                    {
                        if ((current.UserRole & UserRole.OutsourcingManager) == UserRole.OutsourcingManager || (current.UserRole & UserRole.Estimator) == UserRole.Estimator)
                            mission.DeleteAfterSendTo1C = true;
                        // ----------------------------
                        if (model.OrderScanAttachmentId > 0)
                            RequestAttachmentDao.Delete(model.OrderScanAttachmentId);
                        // ----------------------------
                        mission.DeleteDate = DateTime.Now;
                        mission.CreateDate = DateTime.Now;
                        MissionDao.SaveAndFlush(mission);
                        model.IsDelete = false;
                        model.OrderScanAttachmentId = 0;
                        model.OrderScanAttachment = string.Empty;
                        SendEmailForUserRequest(mission.User, current, mission.Creator, true, mission.Id,
                            mission.Number, RequestTypeEnum.Mission, false);
                    }
                    else
                    {
                        ChangeEntityProperties(current, mission, model, user);
                        MissionDao.SaveAndFlush(mission);
                        if (mission.Version != model.Version)
                        {
                            mission.CreateDate = DateTime.Now;
                            MissionDao.SaveAndFlush(mission);
                        }
                    }
                    if (mission.DeleteDate.HasValue)
                        model.IsDeleted = true;
                }
                #endregion
                model.DocumentNumber = mission.Number.ToString();
                model.Version = mission.Version;
                model.DaysCount = mission.DaysCount;
                model.CreatorLogin = mission.Creator.Name;
                model.DateCreated = mission.CreateDate.ToShortDateString();
                SetFlagsState(mission.Id, user, mission, model);
                return true;
            }
            catch (Exception ex)
            {
                MissionDao.RollbackTran();
                Log.Error("Error on SaveMissionEditModel:", ex);
                error = string.Format("Исключение:{0}", ex.GetBaseException().Message);
                return false;
            }
            finally
            {
                SetUserInfoModel(user, model);
                LoadDictionaries(model);
                SetHiddenFields(model);
            }
        }
        public void ReloadDictionariesToModel(MissionEditModel model)
        {
            User user = UserDao.Load(model.UserId);
            IUser current = AuthenticationService.CurrentUser;
            SetUserInfoModel(user, model);
            LoadDictionaries(model);
            if (model.Id == 0)
            {
                model.CreatorLogin = current.Name;
                model.DateCreated = DateTime.Today.ToShortDateString();
            }
            else
            {
                Mission mission = MissionDao.Load(model.Id);
                model.CreatorLogin = mission.Creator.Name;
                model.DocumentNumber = mission.Number.ToString();
                model.DateCreated = mission.CreateDate.ToShortDateString();
            }
        }
        protected void ChangeEntityProperties(IUser current, Mission entity, MissionEditModel model, User user)
        {
            #region Согласование сотрудником
            if ((current.UserRole & UserRole.Employee) == UserRole.Employee && current.Id == model.UserId
                    && !entity.UserDateAccept.HasValue
                    && model.IsApproved)
            {
                entity.UserDateAccept = DateTime.Now;
                SendEmailForUserRequest(entity.User, current, entity.Creator, false, entity.Id,
                    entity.Number, RequestTypeEnum.Mission, false);
            }
            #endregion
            #region Согласование руководителем
            bool canEdit = false;

            if (((current.UserRole & UserRole.Manager) == UserRole.Manager && IsCurrentManagerForUser(user, current, out canEdit))
                    || HasCurrentManualRoleForUser(user, current, UserManualRole.ApprovesCommonRequests, out canEdit))
            {
                if (model.IsApprovedByUser && !entity.UserDateAccept.HasValue)
                    entity.UserDateAccept = DateTime.Now;
                if (!entity.ManagerDateAccept.HasValue)
                {
                    //entity.TimesheetStatus = TimesheetStatusDao.Load(model.TimesheetStatusId);
                    if (model.IsApproved)
                    {
                        entity.ManagerDateAccept = DateTime.Now;
                        SendEmailForUserRequest(entity.User, current, entity.Creator, false, entity.Id,
                            entity.Number, RequestTypeEnum.Mission, false);
                    }
                }
            }
            #endregion
            #region Согласование кадровиком
            if ((current.UserRole & UserRole.PersonnelManager) == UserRole.PersonnelManager
                    && (user.Personnels.Where(x => x.Id == current.Id).FirstOrDefault() != null)
                    )
            {
                if (model.IsApprovedByUser && !entity.UserDateAccept.HasValue)
                    entity.UserDateAccept = DateTime.Now;
                if (!entity.PersonnelManagerDateAccept.HasValue)
                {
                    entity.TimesheetStatus = TimesheetStatusDao.Load(model.TimesheetStatusId);
                    entity.Reason = model.Reason;
                    if (model.IsApproved)
                    {
                        entity.PersonnelManagerDateAccept = DateTime.Now;
                        SendEmailForUserRequest(entity.User, current, entity.Creator, false, entity.Id,
                            entity.Number, RequestTypeEnum.Mission, false);
                    }
                    if (model.IsApprovedForAll && !entity.ManagerDateAccept.HasValue)
                        entity.ManagerDateAccept = DateTime.Now;
                }
            }
            #endregion
            if (model.IsTypeEditable)
            {
                // ReSharper disable PossibleInvalidOperationException
                entity.BeginDate = model.BeginDate.Value;
                entity.EndDate = model.EndDate.Value;
                // ReSharper restore PossibleInvalidOperationException
                entity.DaysCount = model.EndDate.Value.Subtract(model.BeginDate.Value).Days + 1;
                entity.Country = model.Country;
                entity.Organization = model.MissionOrganization;
                entity.Goal = model.Goal;
                entity.FinancesSource = model.FinancesSource;
                //entity.Reason = model.Reason;
                entity.Type = MissionTypeDao.Load(model.TypeId);
            }
        }
        #endregion

        #region HolidayWork
        public HolidayWorkListModel GetHolidayWorkListModel()
        {
            User user = UserDao.Load(AuthenticationService.CurrentUser.Id);
            HolidayWorkListModel model = new HolidayWorkListModel
            {
                UserId = AuthenticationService.CurrentUser.Id,
                Department = GetDepartmentDto(user),
            };
            SetDictionariesToModel(model, user);
            return model;
        }
        public void SetHolidayWorkListModel(HolidayWorkListModel model, bool hasError)
        {
            User user = UserDao.Load(model.UserId);
            SetDictionariesToModel(model, user);
            if (hasError)
                model.Documents = new List<VacationDto>();
            else
                SetDocumentsToModel(model, user);
        }
        public void SetDocumentsToModel(HolidayWorkListModel model, User user)
        {
            UserRole role = (UserRole)(user.RoleId & (int)CurrentUser.UserRole);
            model.Documents = HolidayWorkDao.GetDocuments(
                user.Id,
                role,
                GetDepartmentId(model.Department),
                model.PositionId,
                model.TypeId,
                model.StatusId,
                model.BeginDate,
                model.EndDate,
                model.UserName,
                model.Number
                //model.PaymentPercentType
                );
        }
        protected void SetDictionariesToModel(HolidayWorkListModel model, User user)
        {
            //model.Departments = GetDepartments(user);
            model.Types = GetHolidayWorkTypes(true);
            model.Statuses = GetRequestStatuses();
            model.Positions = GetPositions(user);
        }
        protected List<IdNameDto> GetHolidayWorkTypes(bool addAll)
        {
            var typeList = HolidayWorkTypeDao.LoadAllSorted().ToList().ConvertAll(x => new IdNameDto(x.Id, x.Name));
            if (addAll)
                typeList.Insert(0, new IdNameDto(0, SelectAll));
            return typeList;
        }
        public HolidayWorkEditModel GetHolidayWorkEditModel(int id, int userId)
        {
            HolidayWorkEditModel model = new HolidayWorkEditModel { Id = id, UserId = userId };
            User user = UserDao.Load(userId);
            IUser current = AuthenticationService.CurrentUser;
            if (!CheckUserRights(user, current, id, false))
                throw new ArgumentException("Доступ запрещен.");
            SetUserInfoModel(user, model);
            HolidayWork holidayWork = null;
            if (id == 0)
            {
                model.CreatorLogin = current.Name;
                model.Version = 0;
                model.DateCreated = DateTime.Today.ToShortDateString();
            }
            else
            {
                holidayWork = HolidayWorkDao.Load(id);
                if (holidayWork == null)
                    throw new ArgumentException(string.Format("Больничный (id {0}) не найдена в базе данных.", id));
                model.Version = holidayWork.Version;
                model.TypeId = holidayWork.Type.Id;
                model.Date = holidayWork.WorkDate;
                model.TimesheetStatusId = holidayWork.TimesheetStatus == null ? 0 : holidayWork.TimesheetStatus.Id;
                //model.Rate = holidayWork.Rate.ToString();
                model.Hours = holidayWork.Hours.ToString();
                model.CreatorLogin = holidayWork.Creator.Name;
                model.DocumentNumber = holidayWork.Number.ToString();
                model.DateCreated = holidayWork.CreateDate.ToShortDateString();

                SetHiddenFields(model);
                if (holidayWork.DeleteDate.HasValue)
                    model.IsDeleted = true;
            }
            SetFlagsState(id, user, holidayWork, model);
            LoadDictionaries(model);
            return model;
        }
        public bool SaveHolidayWorkEditModel(HolidayWorkEditModel model, /*UploadFileDto fileDto,*/ out string error)
        {
            error = string.Empty;
            User user = null;
            try
            {
                user = UserDao.Load(model.UserId);
                IUser current = AuthenticationService.CurrentUser;
                if (!CheckUserRights(user, current, model.Id, true))
                {
                    error = "Редактирование заявки запрещено";
                    return false;
                }
                HolidayWork holidayWork;
                if (model.Id == 0)
                {
                    holidayWork = new HolidayWork
                    {
                        CreateDate = DateTime.Now,
                        Creator = UserDao.Load(current.Id),
                        Number = RequestNextNumberDao.GetNextNumberForType((int)RequestTypeEnum.HolidayWork),
                        User = user
                    };
                    ChangeEntityProperties(current, holidayWork, model, user);
                    HolidayWorkDao.SaveAndFlush(holidayWork);
                    model.Id = holidayWork.Id;
                }
                else
                {
                    holidayWork = HolidayWorkDao.Load(model.Id);
                    //SaveAttachment(holidayWork, fileDto, model, RequestTypeEnum.Sicklist);
                    if (holidayWork.Version != model.Version)
                    {
                        error = "Заявка была изменена другим пользователем.";
                        model.ReloadPage = true;
                        return false;
                    }
                    if (model.IsDelete)
                    {
                        //if (model.AttachmentId > 0)
                        //    RequestAttachmentDao.Delete(model.AttachmentId);
                        holidayWork.DeleteDate = DateTime.Now;
                        HolidayWorkDao.SaveAndFlush(holidayWork);
                        model.IsDelete = false;
                        //model.AttachmentId = 0;
                        //model.Attachment = string.Empty;
                    }
                    else
                    {
                        ChangeEntityProperties(current, holidayWork, model, user);
                        HolidayWorkDao.SaveAndFlush(holidayWork);
                    }
                    if (holidayWork.DeleteDate.HasValue)
                        model.IsDeleted = true;
                }
                model.DocumentNumber = holidayWork.Number.ToString();
                model.Version = holidayWork.Version;
                //model.DaysCount = holidayWork.DaysCount;
                model.CreatorLogin = holidayWork.Creator.Name;
                model.DateCreated = holidayWork.CreateDate.ToShortDateString();
                SetFlagsState(holidayWork.Id, user, holidayWork, model);
                return true;
            }
            catch (Exception ex)
            {
                HolidayWorkDao.RollbackTran();
                Log.Error("Error on SaveHolidayWorkEditModel:", ex);
                error = string.Format("Исключение:{0}", ex.GetBaseException().Message);
                return false;
            }
            finally
            {
                SetUserInfoModel(user, model);
                LoadDictionaries(model);
                SetHiddenFields(model);
            }
        }
        protected void ChangeEntityProperties(IUser current, HolidayWork entity, HolidayWorkEditModel model, User user)
        {
            if ((current.UserRole & UserRole.Employee) == UserRole.Employee && current.Id == model.UserId
                && !entity.UserDateAccept.HasValue
                && model.IsApprovedByUser)
                entity.UserDateAccept = DateTime.Now;
            if ((current.UserRole & UserRole.Manager) == UserRole.Manager && user.Manager != null
                && current.Id == user.Manager.Id)
            {
                if (model.IsApprovedByUser && !entity.UserDateAccept.HasValue)
                    entity.UserDateAccept = DateTime.Now;
                if (!entity.ManagerDateAccept.HasValue)
                {
                    entity.TimesheetStatus = TimesheetStatusDao.Load(model.TimesheetStatusId);
                    if (model.IsApprovedByManager)
                        entity.ManagerDateAccept = DateTime.Now;
                }
            }
            if ((current.UserRole & UserRole.PersonnelManager) == UserRole.PersonnelManager
                && (user.Personnels.Where(x => x.Id == current.Id).FirstOrDefault() != null)
                )
            {
                if (model.IsApprovedByUser && !entity.UserDateAccept.HasValue)
                    entity.UserDateAccept = DateTime.Now;
                if (!entity.PersonnelManagerDateAccept.HasValue)
                {
                    entity.TimesheetStatus = TimesheetStatusDao.Load(model.TimesheetStatusId);
                    if (model.IsApprovedByPersonnelManager)
                        entity.PersonnelManagerDateAccept = DateTime.Now;
                }
            }
            if (model.IsTypeEditable)
            {
                // ReSharper disable PossibleInvalidOperationException
                entity.WorkDate = model.Date.Value;
                // ReSharper restore PossibleInvalidOperationException
                entity.Hours = Int32.Parse(model.Hours);
                //entity.Rate = Int32.Parse(model.Rate);
                entity.Type = HolidayWorkTypeDao.Load(model.TypeId);
            }
        }
        protected void SetFlagsState(int id, User user, HolidayWork entity, HolidayWorkEditModel model)
        {
            SetFlagsState(model, false);
            UserRole currentUserRole = AuthenticationService.CurrentUser.UserRole;
            if (id == 0)
            {
                model.IsSaveAvailable = true;
                model.IsTypeEditable = true;
                switch (currentUserRole)
                {
                    case UserRole.Employee:
                        model.IsApprovedByUserEnable = true;
                        break;
                    case UserRole.Manager:
                        model.IsApprovedByManagerEnable = true;
                        model.IsTimesheetStatusEditable = true;
                        break;
                    case UserRole.PersonnelManager:
                        model.IsApprovedByPersonnelManagerEnable = true;
                        model.IsTimesheetStatusEditable = true;
                        break;
                }
                if ((currentUserRole & UserRole.PersonnelManager) == UserRole.PersonnelManager || (currentUserRole & UserRole.Manager) == UserRole.Manager)
                {
                    model.IsApprovedByUserEnable = false;
                    model.IsApprovedByUserHidden = model.IsApprovedByUser = true;
                }
                return;
            }
            model.IsApprovedByUserHidden = model.IsApprovedByUser = entity.UserDateAccept.HasValue;
            model.IsApprovedByManagerHidden = model.IsApprovedByManager = entity.ManagerDateAccept.HasValue;
            model.IsApprovedByPersonnelManagerHidden = model.IsApprovedByPersonnelManager = entity.PersonnelManagerDateAccept.HasValue;
            model.IsPostedTo1CHidden = model.IsPostedTo1C = entity.SendTo1C.HasValue;
            switch (currentUserRole)
            {
                case UserRole.Employee:
                    if (!entity.UserDateAccept.HasValue && !entity.DeleteDate.HasValue)
                    {
                        model.IsApprovedByUserEnable = true;
                        if (!entity.ManagerDateAccept.HasValue && !entity.PersonnelManagerDateAccept.HasValue && !entity.SendTo1C.HasValue)
                            model.IsTypeEditable = true;
                    }
                    break;
                case UserRole.Manager:
                    if (!entity.ManagerDateAccept.HasValue && !entity.DeleteDate.HasValue)
                    {
                        model.IsApprovedByManagerEnable = true;
                        if (!entity.PersonnelManagerDateAccept.HasValue && !entity.SendTo1C.HasValue)
                        {
                            model.IsTypeEditable = true;
                            model.IsTimesheetStatusEditable = true;
                        }
                    }
                    break;
                case UserRole.PersonnelManager:
                    if (!entity.PersonnelManagerDateAccept.HasValue)
                    {
                        model.IsApprovedByPersonnelManagerEnable = true;
                        if (!entity.SendTo1C.HasValue)
                        {
                            model.IsTypeEditable = true;
                            model.IsTimesheetStatusEditable = true;
                        }
                    }
                    else if (!entity.SendTo1C.HasValue && !entity.DeleteDate.HasValue)
                        model.IsDeleteAvailable = true;
                    break;
            }
            model.IsSaveAvailable = model.IsTypeEditable || model.IsTimesheetStatusEditable
                                    || model.IsApprovedByManagerEnable || model.IsApprovedByUserEnable ||
                                    model.IsApprovedByPersonnelManagerEnable;
        }
        protected void SetFlagsState(HolidayWorkEditModel model, bool state)
        {
            model.IsApprovedByManager = state;
            model.IsApprovedByManagerHidden = state;
            model.IsApprovedByManagerEnable = state;

            model.IsApprovedByPersonnelManager = state;
            model.IsApprovedByPersonnelManagerHidden = state;
            model.IsApprovedByPersonnelManagerEnable = state;

            model.IsApprovedByUser = state;
            model.IsApprovedByUserHidden = state;
            model.IsApprovedByUserEnable = state;

            model.IsPostedTo1C = state;
            model.IsPostedTo1CHidden = state;
            model.IsPostedTo1CEnable = state;

            model.IsSaveAvailable = state;
            model.IsTimesheetStatusEditable = state;
            model.IsTypeEditable = state;

            model.IsDelete = state;
            model.IsDeleteAvailable = state;
        }
        protected void SetHiddenFields(HolidayWorkEditModel model)
        {
            model.TypeIdHidden = model.TypeId;
            model.TimesheetStatusIdHidden = model.TimesheetStatusId;
        }
        protected void LoadDictionaries(HolidayWorkEditModel model)
        {
            model.CommentsModel = GetCommentsModel(model.Id, (int)RequestTypeEnum.HolidayWork);
            model.TimesheetStatuses = GetTimesheetStatusesForHolidayWork();
            model.Types = GetHolidayWorkTypes(false);
        }
        protected List<IdNameDto> GetTimesheetStatusesForHolidayWork()
        {
            List<IdNameDto> dtos = TimesheetStatusDao.LoadAllSorted().
                Where(x => (x.Id == 6) || (x.Id == 13)).ToList().
                ConvertAll(x => new IdNameDto(x.Id, x.Name)).OrderBy(x => x.Name).ToList();
            if (AuthenticationService.CurrentUser.UserRole == UserRole.Employee)
                dtos.Insert(0, new IdNameDto(0, string.Empty));
            return dtos;
        }
        public void ReloadDictionariesToModel(HolidayWorkEditModel model)
        {
            User user = UserDao.Load(model.UserId);
            IUser current = AuthenticationService.CurrentUser;
            SetUserInfoModel(user, model);
            LoadDictionaries(model);
            if (model.Id == 0)
            {
                model.CreatorLogin = current.Name;
                model.DateCreated = DateTime.Today.ToShortDateString();
            }
            else
            {
                HolidayWork holidayWork = HolidayWorkDao.Load(model.Id);
                model.CreatorLogin = holidayWork.Creator.Name;
                model.DocumentNumber = holidayWork.Number.ToString();
                model.DateCreated = holidayWork.CreateDate.ToShortDateString();
            }
        }
        #endregion

        #region Sicklist
        public SicklistListModel GetSicklistListModel()
        {
            User user = UserDao.Load(AuthenticationService.CurrentUser.Id);
            IdNameReadonlyDto dep = GetDepartmentDto(user);
            SicklistListModel model = new SicklistListModel
            {
                UserId = AuthenticationService.CurrentUser.Id,
                DepartmentName = dep.Name,
                DepartmentId = dep.Id,
                DepartmentReadOnly = dep.IsReadOnly
                //Department = GetDepartmentDto(user),
            };
            SetFlagsState(user, model);
            SetDictionariesToModel(model, user);
            SetInitialDates(model);
            return model;
        }
        protected List<IdNameDto> GetSicklistTypes(bool addAll, bool addEmpty)
        {
            var typeList = SicklistTypeDao.LoadAllSorted().ToList().ConvertAll(x => new IdNameDto(x.Id, x.Name));
            if (addAll)
                typeList.Insert(0, new IdNameDto(0, SelectAll));
            else if (addEmpty)
                typeList.Insert(0, new IdNameDto(0, string.Empty));
            return typeList;
        }
        protected List<IdNameDto> GetBabyMindingTypes(bool addAll)
        {
            var typeList = SicklistBabyMindingTypeDao.LoadAllSorted().ToList().ConvertAll(x => new IdNameDto(x.Id, x.Name));
            if (addAll)
                typeList.Insert(0, new IdNameDto(0, SelectAll));
            return typeList;
        }
        protected List<IdNameDtoSort> GetSicklisPaymentPercentTypes(bool addAll, bool addNameAll)
        {
            List<IdNameDtoSort> typeList = SicklistPaymentPercentDao.LoadAll().ToList().
                ConvertAll(
                    x =>
                    new IdNameDtoSort
                    {
                        Id = x.Id,
                        Name = x.SicklistPercent.ToString() + "%",
                        SortOrder = x.SortOrder
                    }).OrderBy(x => x.SortOrder).ToList();
            if (addAll)
                typeList.Insert(0,
                                new IdNameDtoSort
                                {
                                    Id = 0,
                                    Name = addNameAll ? SelectAll : string.Empty,
                                    SortOrder = -100
                                });
            return typeList;
        }
        protected List<IdNameDto> GetSicklisPaymentRestrictTypes(bool addEmpty)
        {
            var typeList = SicklistPaymentRestrictTypeDao.LoadAllSorted().ToList().ConvertAll(x => new IdNameDto(x.Id, x.Name));
            if (addEmpty)
                typeList.Insert(0, new IdNameDto(0, "Отсутствует"));
            return typeList;
        }
        public void SetSicklistListModel(SicklistListModel model, bool hasError)
        {
            User user = UserDao.Load(AuthenticationService.CurrentUser.Id);
            SetDictionariesToModel(model, user);
            if (hasError)
                model.Documents = new List<SicklistDto>();
            else
            {
                if (model.Documents != null && model.IsOriginalReceivedModified && ((user.UserRole & UserRole.PersonnelManager) == UserRole.PersonnelManager))
                {
                    model.IsOriginalReceivedModified = false;
                    List<int> idsToApplyReceivedOriginals = model.Documents.Where(x => x.IsOriginalReceived).Select(x => x.Id).ToList();
                    ApplyReceivedOriginals(model, idsToApplyReceivedOriginals);
                }
                SetDocumentsToModel(model, user);
            }
            SetFlagsState(user, model);
        }
        protected void ApplyReceivedOriginals(SicklistListModel model, List<int> idsToApplyReceivedOriginals)
        {
            List<Sicklist> entities = SicklistDao.LoadForIdsList(idsToApplyReceivedOriginals).ToList();
            foreach (Sicklist entity in entities)
            {
                entity.IsOriginalReceived = true;
                SicklistDao.SaveAndFlush(entity);
            }
        }
        protected void SetDictionariesToModel(SicklistListModel model, User user)
        {
            //model.Department = GetDepartments(user);
            model.Types = GetSicklistTypes(true, false);
            model.Statuses = GetRequestStatuses(CurrentUser.UserRole);
            model.Positions = GetPositions(user);
            //model.PaymentPercentTypes = GetSicklisPaymentPercentTypes(true,true);
        }
        public void SetDocumentsToModel(SicklistListModel model, User user)
        {
            UserRole role = (UserRole)(user.RoleId & (int)CurrentUser.UserRole);
            model.Documents = SicklistDao.GetSicklistDocuments(
                user.Id,
                role,
                //GetDepartmentId(model.Department),
                model.DepartmentId,
                model.PositionId,
                model.TypeId,
                model.StatusId,
                0,
                model.BeginDate,
                model.EndDate,
                model.UserName,
                model.SicklistNumber,
                model.SortBy,
                model.SortDescending,
                model.Number);
        }

        protected void SetFlagsState(User user, SicklistListModel model)
        {
            if ((user.UserRole & UserRole.PersonnelManager) == UserRole.PersonnelManager)
            {
                model.IsOriginalReceivedEditable = true;
            }
        }

        public SicklistEditModel GetSicklistEditModel(int id, int userId)
        {
            SicklistEditModel model = new SicklistEditModel { Id = id, UserId = userId };
            User user = UserDao.Load(userId);
            IUser current = AuthenticationService.CurrentUser;
            if (!CheckUserRights(user, current, id, false))
                throw new ArgumentException("Доступ запрещен.");
            SetUserInfoModel(user, model);
            SetAttachmentToModel(model, id, RequestAttachmentTypeEnum.Sicklist);
            Sicklist sicklist = null;
            if (id == 0)
            {
                model.CreatorLogin = current.Name;
                model.Version = 0;
                model.DateCreated = DateTime.Today.ToShortDateString();
            }
            else
            {
                sicklist = SicklistDao.Load(id);
                if (sicklist == null)
                    throw new ArgumentException(string.Format("Больничный (id {0}) не найдена в базе данных.", id));
                model.Version = sicklist.Version;
                model.TypeId = sicklist.Type == null ? 0 : sicklist.Type.Id;
                model.BabyMindingTypeId = sicklist.BabyMindingType == null ? new int?() : sicklist.BabyMindingType.Id;
                model.BeginDate = sicklist.BeginDate;//new DateTimeDto(vacation.BeginDate);//
                model.EndDate = sicklist.EndDate;
                model.TimesheetStatusId = sicklist.TimesheetStatus == null ? 0 : sicklist.TimesheetStatus.Id;
                model.DaysCount = sicklist.DaysCount;
                model.CreatorLogin = sicklist.Creator.Name;
                model.DocumentNumber = sicklist.Number.ToString();
                model.SicklistNumber = sicklist.SicklistNumber;
                model.DateCreated = sicklist.CreateDate.ToShortDateString();

                model.PaymentBeginDate = sicklist.PaymentBeginDate;
                model.ExperienceYears = !sicklist.ExperienceYears.HasValue || sicklist.ExperienceYears.Value == 0 ?
                                        string.Empty : sicklist.ExperienceYears.Value.ToString();
                model.ExperienceMonthes = !sicklist.ExperienceMonthes.HasValue || sicklist.ExperienceMonthes.Value == 0 ?
                                        string.Empty : sicklist.ExperienceMonthes.Value.ToString();
                model.PaymentPercentTypeId = sicklist.PaymentPercent == null ? 0 : sicklist.PaymentPercent.Id;
                model.PaymentRestrictTypeId = sicklist.RestrictType == null ? 0 : sicklist.RestrictType.Id;
                model.PaymentDecreaseDate = sicklist.PaymentDecreaseDate;
                model.IsPreviousPaymentCounted = sicklist.IsPreviousPaymentCounted;
                model.IsContinued = sicklist.IsContinued;
                //model.Is2010Calculate = entity.Is2010Calculate;
                model.IsAddToFullPayment = sicklist.IsAddToFullPayment;
                model.ExperienceIn1C = user.ExperienceIn1C;
                model.ApprovedByManager = sicklist.ApprovedByManager != null ? sicklist.ApprovedByManager.FullName : " - ";
                model.ApprovedByPersonnelManager = sicklist.ApprovedByPersonnelManager != null ? sicklist.ApprovedByPersonnelManager.FullName : " - ";
                SetHiddenFields(model);
                if (sicklist.DeleteDate.HasValue)
                    model.IsDeleted = true;
            }
            SetFlagsState(id, user, sicklist, model);
            LoadDictionaries(model);
            return model;
        }
        protected void SetHiddenFields(SicklistEditModel model)
        {
            model.TypeIdHidden = model.TypeId;
            model.BabyMindingTypeIdHidden = model.BabyMindingTypeId;
            model.TimesheetStatusIdHidden = model.TimesheetStatusId;
            model.DaysCountHidden = model.DaysCount;
            model.PaymentPercentTypeIdHidden = model.PaymentPercentTypeId;
            model.PaymentRestrictTypeIdHidden = model.PaymentRestrictTypeId;
            //model.Is2010CalculateHidden = model.Is2010Calculate;
            model.IsPreviousPaymentCountedHidden = model.IsPreviousPaymentCounted;
            model.IsContinuedHidden = model.IsContinued;
            model.IsAddToFullPaymentHidden = model.IsAddToFullPaymentHidden;
        }
        protected void SetAttachmentToModel(IAttachment model, int id, RequestAttachmentTypeEnum type)
        {
            if (id == 0)
                return;
            RequestAttachment attach = RequestAttachmentDao.FindByRequestIdAndTypeId(id, type);
            if (attach == null)
                return;
            model.AttachmentId = attach.Id;
            model.Attachment = attach.FileName;
        }
        protected void SetOrderScanAttachmentToModel(IOrderScanAttachment model, int id, RequestAttachmentTypeEnum type)
        {
            if (id == 0)
                return;
            RequestAttachment attach = RequestAttachmentDao.FindByRequestIdAndTypeId(id, type);
            if (attach == null)
                return;
            model.OrderScanAttachmentId = attach.Id;
            model.OrderScanAttachment = attach.FileName;
        }
        protected void SetUnsignedOrderScanAttachmentToModel(IUnsignedOrderScanAttachment model, int id, RequestAttachmentTypeEnum type)
        {
            if (id == 0)
                return;
            RequestAttachment attach = RequestAttachmentDao.FindByRequestIdAndTypeId(id, type);
            if (attach == null)
                return;
            model.UnsignedOrderScanAttachmentId = attach.Id;
            model.UnsignedOrderScanAttachment = attach.FileName;
        }
        protected void SetT2ScanAttachmentToModel(IT2ScanAttachment model, int id, RequestAttachmentTypeEnum type)
        {
            if (id == 0)
                return;
            RequestAttachment attach = RequestAttachmentDao.FindByRequestIdAndTypeId(id, type);
            if (attach == null)
                return;
            model.T2ScanAttachmentId = attach.Id;
            model.T2ScanAttachment = attach.FileName;
        }
        protected void SetUnsignedT2ScanAttachmentToModel(IUnsignedT2ScanAttachment model, int id, RequestAttachmentTypeEnum type)
        {
            if (id == 0)
                return;
            RequestAttachment attach = RequestAttachmentDao.FindByRequestIdAndTypeId(id, type);
            if (attach == null)
                return;
            model.UnsignedT2ScanAttachmentId = attach.Id;
            model.UnsignedT2ScanAttachment = attach.FileName;
        }
        protected void SetDismissalAgreementScanAttachmentToModel(IDismissalAgreementScanAttachment model, int id, RequestAttachmentTypeEnum type)
        {
            if (id == 0)
                return;
            RequestAttachment attach = RequestAttachmentDao.FindByRequestIdAndTypeId(id, type);
            if (attach == null)
                return;
            model.DismissalAgreementScanAttachmentId = attach.Id;
            model.DismissalAgreementScanAttachment = attach.FileName;
        }
        protected void SetUnsignedDismissalAgreementScanAttachmentToModel(IUnsignedDismissalAgreementScanAttachment model, int id, RequestAttachmentTypeEnum type)
        {
            if (id == 0)
                return;
            RequestAttachment attach = RequestAttachmentDao.FindByRequestIdAndTypeId(id, type);
            if (attach == null)
                return;
            model.UnsignedDismissalAgreementScanAttachmentId = attach.Id;
            model.UnsignedDismissalAgreementScanAttachment = attach.FileName;
        }
        protected void SetF182NScanAttachmentToModel(IF182NScanAttachment model, int id, RequestAttachmentTypeEnum type)
        {
            if (id == 0)
                return;
            RequestAttachment attach = RequestAttachmentDao.FindByRequestIdAndTypeId(id, type);
            if (attach == null)
                return;
            model.F182NScanAttachmentId = attach.Id;
            model.F182NScanAttachment = attach.FileName;
        }
        protected void SetF2NDFLScanAttachmentToModel(IF2NDFLScanAttachment model, int id, RequestAttachmentTypeEnum type)
        {
            if (id == 0)
                return;
            RequestAttachment attach = RequestAttachmentDao.FindByRequestIdAndTypeId(id, type);
            if (attach == null)
                return;
            model.F2NDFLScanAttachmentId = attach.Id;
            model.F2NDFLScanAttachment = attach.FileName;
        }
        protected void SetWorkbookRequestScanAttachmentToModel(IWorkbookRequestScanAttachment model, int id, RequestAttachmentTypeEnum type)
        {
            if (id == 0)
                return;
            RequestAttachment attach = RequestAttachmentDao.FindByRequestIdAndTypeId(id, type);
            if (attach == null)
                return;
            model.WorkbookRequestScanAttachmentId = attach.Id;
            model.WorkbookRequestScanAttachment = attach.FileName;
        }
        public bool SaveSicklistEditModel(SicklistEditModel model, UploadFileDto fileDto, out string error)
        {
            error = string.Empty;
            User user = null;
            try
            {
                user = UserDao.Load(model.UserId);
                IUser current = AuthenticationService.CurrentUser;
                if (!CheckUserRights(user, current, model.Id, true) /* || !CheckUserRightsForEntity(user,current,model)*/)
                {
                    error = "Редактирование заявки запрещено";
                    return false;
                }
                Sicklist sicklist;
                
                if (model.Id == 0)
                #region Сохранение нового БЛ
                {
                    sicklist = new Sicklist
                    {
                        CreateDate = DateTime.Now,
                        Creator = UserDao.Load(current.Id),
                        Number = RequestNextNumberDao.GetNextNumberForType((int)RequestTypeEnum.Sicklist),
                        User = user
                    };
                    ChangeEntityProperties(current, sicklist, model, user);
                    SicklistDao.SaveAndFlush(sicklist);
                    model.Id = sicklist.Id;
                }
                #endregion
                else
                #region Сохранение существующего БЛ
                {
                    sicklist = SicklistDao.Load(model.Id);

                    if (sicklist.SendTo1C.HasValue)
                    {
                        error = "Редактирование заявки запрещено, так как она выгружена в 1С!";
                        return false;
                    }

                    string fileName;
                    int? attachmentId = SaveAttachment(sicklist.Id, model.AttachmentId, fileDto, RequestAttachmentTypeEnum.Sicklist, out fileName);
                    if (attachmentId.HasValue)
                    {
                        model.AttachmentId = attachmentId.Value;
                        model.Attachment = fileName;
                    }
                    if (sicklist.Version != model.Version)
                    {
                        error = "Заявка была изменена другим пользователем.";
                        model.ReloadPage = true;
                        return false;
                    }
                    if (model.IsDelete)
                    {
                        if ((current.UserRole & UserRole.OutsourcingManager) == UserRole.OutsourcingManager || (current.UserRole & UserRole.Estimator) == UserRole.Estimator)
                            sicklist.DeleteAfterSendTo1C = true;
                        if (model.AttachmentId > 0)
                            RequestAttachmentDao.Delete(model.AttachmentId);
                        sicklist.DeleteDate = DateTime.Now;
                        sicklist.CreateDate = DateTime.Now;
                        SicklistDao.SaveAndFlush(sicklist);
                        SendEmailForUserRequest(sicklist.User, current, sicklist.Creator, true, sicklist.Id,
                            sicklist.Number, RequestTypeEnum.Sicklist, false);
                        model.IsDelete = false;
                        model.AttachmentId = 0;
                        model.Attachment = string.Empty;
                    }
                    else
                    {
                        ChangeEntityProperties(current, sicklist, model, user);
                        SicklistDao.SaveAndFlush(sicklist);
                        if (sicklist.Version != model.Version)
                        {
                            sicklist.CreateDate = DateTime.Now;
                            SicklistDao.SaveAndFlush(sicklist);
                        }
                    }
                    if (sicklist.DeleteDate.HasValue)
                        model.IsDeleted = true;
                }
                #endregion

                model.DocumentNumber = sicklist.Number.ToString();
                model.Version = sicklist.Version;
                model.DaysCount = sicklist.DaysCount;
                model.CreatorLogin = sicklist.Creator.Name;
                model.DateCreated = sicklist.CreateDate.ToShortDateString();
                SetFlagsState(sicklist.Id, user, sicklist, model);
                //throw new ArgumentException("Test exception");
                return true;
            }
            catch (Exception ex)
            {
                SicklistDao.RollbackTran();
                Log.Error("Error on SaveSicklistEditModel:", ex);
                error = string.Format("Исключение:{0}", ex.GetBaseException().Message);
                return false;
            }
            finally
            {
                SetUserInfoModel(user, model);
                LoadDictionaries(model);
                SetHiddenFields(model);
            }
        }
        //protected void SaveAttachment(int entityId, UploadFileDto fileDto,SicklistEditModel model, RequestAttachmentTypeEnum type)
        //{
        //    if (fileDto == null)
        //        return;
        //    RequestAttachment attach = model.AttachmentId != 0 ?
        //        RequestAttachmentDao.Load(model.AttachmentId) :
        //        new RequestAttachment
        //            {
        //                RequestId = entityId,
        //                RequestType = (int) type,
        //            };

        //    attach.DateCreated = DateTime.Now;
        //    attach.UncompressContext = fileDto.Context;
        //    attach.ContextType = fileDto.ContextType;
        //    attach.FileName = fileDto.FileName;
        //    RequestAttachmentDao.SaveAndFlush(attach);
        //    model.AttachmentId = attach.Id;
        //    model.Attachment = attach.FileName;
        //}

        protected void ChangeEntityProperties(IUser current, Sicklist entity, SicklistEditModel model, User user)
        {
            #region Согласование сотрудником

            if ((current.UserRole & UserRole.Employee) == UserRole.Employee && current.Id == model.UserId
                && !entity.UserDateAccept.HasValue
                && model.IsApproved)
            {
                entity.UserDateAccept = DateTime.Now;
                //!!! need to send e-mail
                SendEmailForUserRequest(entity.User, current, entity.Creator, false, entity.Id,
                    entity.Number, RequestTypeEnum.Sicklist, false);
            }

            #endregion

            #region Согласование руководителем

            bool canEdit = false;

            if (((current.UserRole & UserRole.Manager) == UserRole.Manager && IsCurrentManagerForUser(user, current, out canEdit))
                || HasCurrentManualRoleForUser(user, current, UserManualRole.ApprovesCommonRequests, out canEdit))
            {
                if (!String.IsNullOrEmpty(model.PyrusNumber))
                    entity.PirusNumber = model.PyrusNumber;
                if (!entity.ManagerDateAccept.HasValue)
                {
                    if (model.IsApprovedByUser && !entity.UserDateAccept.HasValue)
                        entity.UserDateAccept = DateTime.Now;

                    if (model.IsApproved)
                    {
                        entity.ManagerDateAccept = DateTime.Now;
                        entity.ApprovedByManager = UserDao.Get(current.Id);
                        SendEmailForUserRequest(entity.User, current, entity.Creator, false, entity.Id,
                            entity.Number, RequestTypeEnum.Sicklist, false);
                    }
                }
            }

            #endregion

            #region Согласование кадровиком

            int? superPersonnelId = ConfigurationService.SuperPersonnelId;
            // Для расчетчика, кадровика или аутсорсинга
            if (((current.UserRole & UserRole.PersonnelManager) == UserRole.PersonnelManager && ((superPersonnelId.HasValue && CurrentUser.Id == superPersonnelId.Value)
                || (user.Personnels.Where(x => x.Id == current.Id).FirstOrDefault() != null))
                || (current.UserRole & UserRole.OutsourcingManager) == UserRole.OutsourcingManager
                || (current.UserRole & UserRole.Estimator) == UserRole.Estimator))
            {
                if (model.IsApprovedByUser && !entity.UserDateAccept.HasValue)
                    entity.UserDateAccept = DateTime.Now;
                if (!entity.PersonnelManagerDateAccept.HasValue)
                {
                    entity.TimesheetStatus = TimesheetStatusDao.Load(model.TimesheetStatusId);
                    if (model.IsPersonnelFieldsEditable)
                        SetPersonnelDataFromModel(entity, model);
                    if (model.IsApproved)
                    {
                        entity.PersonnelManagerDateAccept = DateTime.Now;
                        entity.ApprovedByPersonnelManager = UserDao.Load(current.Id);
                        SendEmailForUserRequest(entity.User, current, entity.Creator, false, entity.Id,
                            entity.Number, RequestTypeEnum.Sicklist, false);
                    }
                    if (model.IsApprovedForAll && !entity.ManagerDateAccept.HasValue)
                    {
                        entity.ManagerDateAccept = DateTime.Now;
                        entity.ApprovedByManager = UserDao.Load(current.Id);
                    }

                }
            }

            #endregion

            #region Согласование консультантом за всех
            if ((current.UserRole & UserRole.ConsultantOutsourcing) == UserRole.ConsultantOutsourcing && model.IsApproveForAllByConsultant)
            {
                if (!entity.UserDateAccept.HasValue && model.IsApproveForAllByConsultant)
                {
                    entity.UserDateAccept = DateTime.Now;
                }
                if (!entity.ManagerDateAccept.HasValue && model.IsApproveForAllByConsultant)
                {
                    entity.ManagerDateAccept = DateTime.Now;
                    entity.ApprovedByManager = UserDao.Load(current.Id);
                }
                if (!entity.PersonnelManagerDateAccept.HasValue && model.IsApproveForAllByConsultant)
                {
                    if (model.IsPersonnelFieldsEditable)
                        SetPersonnelDataFromModel(entity, model);

                    entity.PersonnelManagerDateAccept = DateTime.Now;
                    entity.ApprovedByPersonnelManager = UserDao.Load(current.Id);
                }
            }
            #endregion

            #region Date edits

            if (model.IsDatesEditable)
            {
                // ReSharper disable PossibleInvalidOperationException
                entity.IsContinued = model.IsContinued;
                entity.BeginDate = model.BeginDate.Value;
                entity.EndDate = model.EndDate.Value;
                // ReSharper restore PossibleInvalidOperationException
                entity.DaysCount = model.EndDate.Value.Subtract(model.BeginDate.Value).Days + 1;
                entity.SicklistNumber = model.SicklistNumber;
            }

            #endregion

            #region Type edits

            if (model.IsTypeEditable)
            {
                entity.Type = SicklistTypeDao.Load(model.TypeId);
                if (model.TypeId == sicklistTypeDao.SicklistTypeIdBabyMinding)
                    entity.BabyMindingType = model.BabyMindingTypeId.HasValue
                        ? SicklistBabyMindingTypeDao.Load(model.BabyMindingTypeId.Value)
                        : null;
                else
                    entity.BabyMindingType = null;
            }

            #endregion
        }
        protected void SetPersonnelDataFromModel(Sicklist sicklist, SicklistEditModel model)
        {
            sicklist.PaymentBeginDate = model.PaymentBeginDate;
            sicklist.ExperienceYears = GetIntFromModel(model.ExperienceYears);
            sicklist.ExperienceMonthes = GetIntFromModel(model.ExperienceMonthes);
            sicklist.PaymentPercent = model.PaymentPercentTypeId == 0 ? null : SicklistPaymentPercentDao.Load(model.PaymentPercentTypeId);
            //entity.RestrictType = model.PaymentRestrictTypeId == 0 ? null : SicklistPaymentRestrictTypeDao.Load(model.PaymentRestrictTypeId);
            //entity.PaymentDecreaseDate = model.PaymentDecreaseDate;
            sicklist.IsPreviousPaymentCounted = model.IsPreviousPaymentCounted;
            //entity.Is2010Calculate = model.Is2010Calculate;
            sicklist.IsAddToFullPayment = model.IsAddToFullPayment;
        }
        protected int? GetIntFromModel(string modelValue)
        {
            if (!string.IsNullOrEmpty(modelValue))
            {
                int experienceYears;
                if (Int32.TryParse(modelValue, out experienceYears))
                    return experienceYears;
                throw new ArgumentException("Значение поля не является целым числом.");
            }
            return null;
        }
        public void ReloadDictionariesToModel(SicklistEditModel model)
        {
            User user = UserDao.Load(model.UserId);
            IUser current = AuthenticationService.CurrentUser;
            SetUserInfoModel(user, model);
            LoadDictionaries(model);
            if (model.Id == 0)
            {
                model.CreatorLogin = current.Name;
                model.DateCreated = DateTime.Today.ToShortDateString();
            }
            else
            {
                Sicklist sicklist = SicklistDao.Load(model.Id);
                model.CreatorLogin = sicklist.Creator.Name;
                model.DocumentNumber = sicklist.Number.ToString();
                model.DateCreated = sicklist.CreateDate.ToShortDateString();
            }
        }
        protected void LoadDictionaries(SicklistEditModel model)
        {
            model.CommentsModel = GetCommentsModel(model.Id, (int)RequestTypeEnum.Sicklist);
            model.TimesheetStatuses = GetTimesheetStatusesForSicklist();
            model.Types = GetSicklistTypes(false, false);
            model.PaymentPercentTypes = GetSicklisPaymentPercentTypes(!model.IsPersonnelFieldsEditable, false);
            model.PaymentRestrictTypes = GetSicklisPaymentRestrictTypes(false);
            model.BabyMindingTypes = GetBabyMindingTypes(false);
            model.SicklistTypeIdBabyMindingHidden = SicklistTypeDao.SicklistTypeIdBabyMinding;
        }
        protected List<IdNameDto> GetTimesheetStatusesForSicklist()
        {
            List<IdNameDto> dtos = TimesheetStatusDao.LoadAllSorted().
                Where(x => (x.Id == 2) || (x.Id == 3) || (x.Id == 11) || (x.Id == 12)).ToList().
                ConvertAll(x => new IdNameDto(x.Id, x.Name)).OrderBy(x => x.Name).ToList();
            if (AuthenticationService.CurrentUser.UserRole == UserRole.Employee)
                dtos.Insert(0, new IdNameDto(0, string.Empty));
            return dtos;
        }

        protected void SetFlagsState(int id, User user, Sicklist entity, SicklistEditModel model)
        {
            SetFlagsState(model, false);
            UserRole currentUserRole = AuthenticationService.CurrentUser.UserRole;
            if (entity != null)
                model.IsPyrusLinkAvailable = entity.Type != null ? entity.Type.Id == 12 : false;
            else
                model.IsPyrusLinkAvailable = true;
            int? superPersonnelId = ConfigurationService.SuperPersonnelId;
            if (id == 0)
            {
                model.IsSaveAvailable = true;
                model.IsBabyMindingTypeEditable = false;
                model.IsDatesEditable = true;
                model.IsApprovedEnable = false;               
                switch (currentUserRole)
                {
                    case UserRole.Employee:
                        model.IsPyrusLinkAvailable = false;
                        model.IsApprovedByUserEnable = false;
                        break;
                    case UserRole.Manager:
                        
                        model.IsApprovedByManagerEnable = false;
                        //model.IsTimesheetStatusEditable = true;
                        break;
                    case UserRole.Estimator:
                        model.IsApprovedByPersonnelManagerEnable = false;
                        model.IsTimesheetStatusEditable = true;
                        model.IsPersonnelFieldsEditable = true;
                        model.IsTypeEditable = true;
                        break;
                    case UserRole.OutsourcingManager:
                        model.IsApprovedByPersonnelManagerEnable = false;
                        break;
                    case UserRole.ConsultantPersonnel:
                        model.IsApprovedByPersonnelManagerEnable = false;
                        model.IsTimesheetStatusEditable = true;
                        model.IsPersonnelFieldsEditable = true;
                        model.IsExperienceEditable = true;
                        model.IsTypeEditable = true;
                        break;
                    case UserRole.PersonnelManager:
                        model.IsApprovedByPersonnelManagerEnable = false;
                        model.IsTimesheetStatusEditable = true;
                        model.IsPersonnelFieldsEditable = true;
                        // Разрешение редактирования стажа только для кадровиков банка
                        if (superPersonnelId.HasValue && AuthenticationService.CurrentUser.Id != superPersonnelId.Value)
                        {
                            model.IsExperienceEditable = true;
                        }
                        model.IsTypeEditable = true;
                        break;
                    
                }
                if ((currentUserRole & UserRole.PersonnelManager) == UserRole.PersonnelManager
                    || (currentUserRole & UserRole.Manager) == UserRole.Manager
                    || (currentUserRole & UserRole.OutsourcingManager) == UserRole.OutsourcingManager
                    || (currentUserRole & UserRole.Estimator) == UserRole.Estimator)
                {
                    model.IsApprovedByUserEnable = false;
                    model.IsApprovedByUserHidden = model.IsApprovedByUser = true;
                }
                return;
            }
            model.IsApprovedByUserHidden = model.IsApprovedByUser = entity.UserDateAccept.HasValue;
            model.IsApprovedByManagerHidden = model.IsApprovedByManager = entity.ManagerDateAccept.HasValue;
            model.IsApprovedByPersonnelManagerHidden = model.IsApprovedByPersonnelManager = entity.PersonnelManagerDateAccept.HasValue;
            model.IsPostedTo1CHidden = model.IsPostedTo1C = entity.SendTo1C.HasValue;

            // hack to uncheck checkbox on UI
            if (entity.DeleteDate.HasValue && !entity.DeleteAfterSendTo1C)
                model.IsApprovedByPersonnelManager = false;

            bool isSuperPersonnelManager = superPersonnelId.HasValue && AuthenticationService.CurrentUser.Id == superPersonnelId.Value;

            switch (currentUserRole)
            {
                case UserRole.Employee:
                    model.IsPyrusLinkAvailable = false;
                    if (!entity.UserDateAccept.HasValue && !entity.DeleteDate.HasValue)
                    {
                        if (model.AttachmentId > 0)
                            model.IsApprovedEnable = true;
                        if (!entity.ManagerDateAccept.HasValue && !entity.PersonnelManagerDateAccept.HasValue && !entity.SendTo1C.HasValue)
                            model.IsDatesEditable = true;
                    }
                    break;
                case UserRole.Manager:
                    model.IsPyrusLinkAvailable = entity.Type != null ? entity.Type.Id == 12 : false;
                    if (!entity.ManagerDateAccept.HasValue && !entity.DeleteDate.HasValue)
                    {
                        if (model.AttachmentId > 0)
                            model.IsApprovedEnable = true;
                        //model.IsApprovedByManagerEnable = true;
                        if (!entity.PersonnelManagerDateAccept.HasValue && !entity.SendTo1C.HasValue)
                        {
                            model.IsDatesEditable = true;
                            //model.IsTimesheetStatusEditable = true;
                        }
                    }
                    break;
                case UserRole.Estimator:
                    if (!entity.PersonnelManagerDateAccept.HasValue)
                    {
                        if (model.AttachmentId > 0)
                        {
                            
                            if (entity.ManagerDateAccept.HasValue)
                            {
                                model.IsApprovedEnable = true;
                            }
                            else
                            {
                                model.IsApprovedForAllEnable = true;
                            }
                                
                            // и кадровики, и расчетчики могут послать уведомление об ошибках пользователю, если заявка отправлена пользователем на согласование, но еще не выгружена в 1С
                            if (entity.UserDateAccept != null && entity.SendTo1C == null)
                            {
                                model.IsErrorNotificationAvailable = true;
                            }
                        }                        

                        // разрешить редактирование документа кадровиками, если он еще не выгружен в 1С
                        if (!entity.SendTo1C.HasValue)
                        {
                            model.IsTypeEditable = true;
                            model.IsTimesheetStatusEditable = true;
                            model.IsPersonnelFieldsEditable = true;
                            model.IsDatesEditable = true;
                        }
                    }
                    // Разрешить удаление, если согласовано всеми и выгружено в 1С
                    else if (entity.SendTo1C.HasValue && !entity.DeleteDate.HasValue && isSuperPersonnelManager)
                        model.IsDeleteAvailable = true;
                    else if (entity.PersonnelManagerDateAccept.HasValue && entity.ManagerDateAccept.HasValue && entity.UserDateAccept.HasValue) //в состоянии 'Согласованно кадровиком' показываем кнопку 'отклонить заявку'
                        model.IsDeleteAvailable = true;
                    break;
                case UserRole.OutsourcingManager:
                    // Разрешить согласование для аутсорсеров, если стаж уже есть в 1С
                    /*if (!entity.PersonnelManagerDateAccept.HasValue && model.AttachmentId > 0 &&
                        user.ExperienceIn1C == true)
                    {
                        model.IsApprovedEnable = true;
                        model.IsApprovedForAllEnable = true;                        
                    }*/
                    break;
                case UserRole.ConsultantOutsourcing:
                    if (!entity.SendTo1C.HasValue && ((!entity.UserDateAccept.HasValue && !entity.DeleteDate.HasValue) || (!entity.ManagerDateAccept.HasValue && !entity.DeleteDate.HasValue) || !entity.PersonnelManagerDateAccept.HasValue))
                    {
                        if (!entity.SendTo1C.HasValue)
                        {
                            model.IsTypeEditable = true;
                            model.IsTimesheetStatusEditable = true;
                            model.IsPersonnelFieldsEditable = true;
                            // Разрешение редактирования стажа только для кадровиков банка
                            if (!isSuperPersonnelManager)
                            {
                                model.IsExperienceEditable = true;
                            }
                            model.IsDatesEditable = true;
                        }
                        model.IsApproveForAllByConsultantEnable = true;
                    }
                    break;
                case UserRole.PersonnelManager:
                    // Разрешить согласование для кадровиков банка и расчетчиков аутсорсинга
                    // Если нет согласования кадровиком
                    if (!entity.PersonnelManagerDateAccept.HasValue)
                    {
                        if (model.AttachmentId > 0)
                        {
                            // Расчетчики аутсорсинга
                            if (isSuperPersonnelManager)
                            {
                                // могут согласовать в любом случае
                                //if (user.ExperienceIn1C == true)
                                //{
                                if (entity.ManagerDateAccept.HasValue)
                                {
                                    model.IsApprovedEnable = true;
                                }
                                else
                                {
                                    model.IsApprovedForAllEnable = true;
                                }
                                //}
                            }
                            // Кадровики банка могут согласовать,
                            else
                            {
                                // если стаж добавлен вручную
                                if (user.ExperienceIn1C != true && (model.ExperienceYears.Length > 0 || model.ExperienceMonthes.Length > 0) && entity.ManagerDateAccept.HasValue)
                                {
                                    model.IsApprovedEnable = true;
                                }

                                //делаем доступными кнопки согласования для кадровиков
                                //если состояние 'Отправлено кадровику', то делаем доступной кнопку 'Отправлено на согласование'
                                if (!entity.PersonnelManagerDateAccept.HasValue && entity.ManagerDateAccept.HasValue && entity.UserDateAccept.HasValue)
                                    model.IsApprovedEnable = true;
                                else
                                {//если состояние 'Отправлено руководителю', то делаем доступной кнопку 'за всех'
                                    if (!entity.ManagerDateAccept.HasValue && entity.UserDateAccept.HasValue)
                                        model.IsApprovedForAllEnable = true;
                                }
                            }


                            // и кадровики, и расчетчики могут послать уведомление об ошибках пользователю, если заявка отправлена пользователем на согласование, но еще не выгружена в 1С
                            if (entity.UserDateAccept != null && entity.SendTo1C == null)
                            {
                                model.IsErrorNotificationAvailable = true;
                            }
                        }

                        //model.IsApprovedByPersonnelManagerEnable = true;

                        // разрешить редактирование документа кадровиками, если он еще не выгружен в 1С
                        if (!entity.SendTo1C.HasValue)
                        {
                            model.IsTypeEditable = true;
                            model.IsTimesheetStatusEditable = true;
                            model.IsPersonnelFieldsEditable = true;
                            // Разрешение редактирования стажа только для кадровиков банка
                            if (!isSuperPersonnelManager)
                            {
                                model.IsExperienceEditable = true;
                            }
                            model.IsDatesEditable = true;
                        }
                    }
                    // Разрешить удаление, если согласовано всеми и выгружено в 1С
                    else if (entity.SendTo1C.HasValue && !entity.DeleteDate.HasValue && isSuperPersonnelManager)
                        model.IsDeleteAvailable = true;
                    else if (entity.PersonnelManagerDateAccept.HasValue && entity.ManagerDateAccept.HasValue && entity.UserDateAccept.HasValue) //в состоянии 'Согласованно кадровиком' показываем кнопку 'отклонить заявку'
                        model.IsDeleteAvailable = true;
                    break;
                /*
            case UserRole.OutsourcingManager:
                if (entity.SendTo1C.HasValue && !entity.DeleteDate.HasValue)
                    model.IsDeleteAvailable = true;
                break;*/
            }

            model.IsBabyMindingTypeEditable = model.IsTypeEditable && (model.TypeId == SicklistTypeDao.SicklistTypeIdBabyMinding);
            //|| model.IsApprovedByManagerEnable || model.IsApprovedByUserEnable ||
            //model.IsApprovedByPersonnelManagerEnable 
            model.IsSaveAvailable = model.IsTypeEditable || model.IsTimesheetStatusEditable
                                    || model.IsPersonnelFieldsEditable  /*|| model.IsApprovedEnable*/
                                    || model.IsDatesEditable;
        }

        /// <summary>
        /// Set all model flags to the same state
        /// </summary>
        /// <param name="model">Model</param>
        /// <param name="state">State to set the model flags to</param>
        protected void SetFlagsState(SicklistEditModel model, bool state)
        {
            model.IsApprovedByManager = state;
            model.IsApprovedByManagerHidden = state;
            model.IsApprovedByManagerEnable = state;

            model.IsApprovedByPersonnelManager = state;
            model.IsApprovedByPersonnelManagerHidden = state;
            model.IsApprovedByPersonnelManagerEnable = state;

            model.IsApprovedByUser = state;
            model.IsApprovedByUserHidden = state;
            model.IsApprovedByUserEnable = state;

            model.IsPostedTo1C = state;
            model.IsPostedTo1CHidden = state;
            model.IsPostedTo1CEnable = state;

            model.IsSaveAvailable = state;
            model.IsTimesheetStatusEditable = state;
            model.IsTypeEditable = state;
            model.IsBabyMindingTypeEditable = state;
            model.IsDatesEditable = state;

            model.IsDelete = state;
            model.IsDeleteAvailable = state;

            model.IsPersonnelFieldsEditable = state;
            model.IsExperienceEditable = state;

            model.IsApproved = state;
            model.IsApprovedEnable = state;

            model.IsApprovedForAll = state;
            model.IsApprovedForAllEnable = state;

        }
        public bool HaveAbsencesForPeriod(DateTime beginDate, DateTime endDate, int userId,
            int currentUserId, UserRole currentUserRole)
        {
            // Не выдавать ошибки конфликта дат для расчетчиков аутсорсинга
            if ((CurrentUser.UserRole & UserRole.Estimator) > 0) return true;
            if ((currentUserRole & UserRole.PersonnelManager) == UserRole.PersonnelManager)
            {
                int? superPersonnelId = ConfigurationService.SuperPersonnelId;
                if (superPersonnelId.HasValue && currentUserId == superPersonnelId.Value)
                    return true;
            }
            if ((currentUserRole & UserRole.Employee) == UserRole.Employee)
            {
                return true;
            }
            DateTime current = DateTime.Today;
            DateTime monthBegin = new DateTime(current.Year, current.Month, 1);
            if (current.Day == 1)
                monthBegin = monthBegin.AddMonths(-1);
            if (monthBegin < endDate)
                endDate = monthBegin;
            IList<BeginEndDateDto> absences = AbsenceDao.LoadForUserAndPeriod(beginDate, endDate, userId);
            current = beginDate;
            while (current <= endDate)
            {
                if (!IsAbsenceExists(absences, current))
                {
                    Log.InfoFormat("Absence not found for {0}", current);
                    return false;
                }
                current = current.AddDays(1);
            }
            return true;
        }
        protected bool IsAbsenceExists(IList<BeginEndDateDto> absences, DateTime date)
        {
            return absences.Any(x => x.BeginDate <= date && x.EndDate >= date);
        }

        public bool ResetSicklistApprovals(int id, out string error)
        {
            error = String.Empty;

            Sicklist sicklist = SicklistDao.Load(id);

            if (sicklist != null && SendEmailForSicklistError(sicklist) && sicklistDao.ResetApprovals(id))
            {
                return true;
            }
            else
            {
                error = "Error updating comment";
                return false;
            }
        }
        #endregion

        #region Absence
        public AbsenceListModel GetAbsenceListModel()
        {
            User user = UserDao.Load(AuthenticationService.CurrentUser.Id);
            //Department parent = user.Department.Parent;
            IdNameReadonlyDto dep = GetDepartmentDto(user);
            AbsenceListModel model = new AbsenceListModel
            {
                UserId = AuthenticationService.CurrentUser.Id,
                DepartmentName = dep.Name,
                DepartmentId = dep.Id,
                DepartmentReadOnly = dep.IsReadOnly,
                AbsenceTypes = GetAbsenceTypes(true),
                RequestStatuses = GetRequestStatuses(CurrentUser.UserRole),
                Positions = GetPositions(user)
            };
            SetInitialDates(model);
            return model;
        }

        protected List<IdNameDto> GetAbsenceTypes(bool addAll)
        {
            var typeList = AbsenceTypeDao.LoadAllSorted().
                /*Where(x => x.Code.CompareTo("55") == 0 || x.Code.CompareTo("56") == 0).*/
                ToList().ConvertAll(x => new IdNameDto(x.Id, x.Name));
            if (addAll)
                typeList.Insert(0, new IdNameDto(0, SelectAll));
            return typeList;
        }
        public void SetAbsenceListModel(AbsenceListModel model, bool hasError)
        {
            User user = UserDao.Load(model.UserId);
            //model.Departments = GetDepartments(user);
            model.RequestStatuses = GetRequestStatuses(CurrentUser.UserRole);
            model.Positions = GetPositions(user);
            model.AbsenceTypes = GetAbsenceTypes(true);
            if (hasError)
                model.Documents = new List<VacationDto>();
            else
                SetDocumentsToModel(model, user);
        }
        public void SetDocumentsToModel(AbsenceListModel model, User user)
        {
            UserRole role = (UserRole)(user.RoleId & (int)CurrentUser.UserRole);
            model.Documents = AbsenceDao.GetDocuments(
                user.Id,
                role,
                //model.DepartmentId,
                //GetDepartmentId(model.Department),
                model.DepartmentId,
                model.PositionId,
                model.AbsenceTypeId,
                //0,
                model.RequestStatusId,
                model.BeginDate,
                model.EndDate,
                model.UserName,
                model.SortBy,
                model.SortDescending,
                model.Number);
        }
        public AbsenceEditModel GetAbsenceEditModel(int id, int userId)
        {
            AbsenceEditModel model = new AbsenceEditModel { Id = id, UserId = userId };
            User user = UserDao.Load(userId);
            IUser current = AuthenticationService.CurrentUser;
            if (!CheckUserRights(user, current, id, false))
                throw new ArgumentException("Доступ запрещен.");
            SetUserInfoModel(user, model);
            model.CommentsModel = GetCommentsModel(id, (int)RequestTypeEnum.Absence);
            model.TimesheetStatuses = GetTimesheetStatusesForAbsence();
            model.AbsenceTypes = GetAbsenceTypes(false);
            Absence absence = null;
            if (id == 0)
            {
                model.CreatorLogin = current.Name;
                model.Version = 0;
                model.DateCreated = DateTime.Today.ToShortDateString();
            }
            else
            {
                absence = AbsenceDao.Load(id);
                if (absence == null)
                    throw new ArgumentException(string.Format("Заявка на неявку (id {0}) не найдена в базе данных.", id));
                model.Version = absence.Version;
                model.AbsenceTypeId = absence.Type.Id;
                model.AbsenceTypeIdHidden = model.AbsenceTypeId;
                model.BeginDate = absence.BeginDate;//new DateTimeDto(vacation.BeginDate);//
                model.EndDate = absence.EndDate;
                model.TimesheetStatusId = absence.TimesheetStatus == null ? 0 : absence.TimesheetStatus.Id;
                model.TimesheetStatusIdHidden = model.TimesheetStatusId;
                model.DaysCount = absence.DaysCount;
                model.DaysCountHidden = model.DaysCount;
                model.CreatorLogin = absence.Creator.Name;
                model.DocumentNumber = absence.Number.ToString();
                model.DateCreated = absence.CreateDate.ToShortDateString();
                if (absence.DeleteDate.HasValue)
                    model.IsDeleted = true;
            }
            SetFlagsState(id, user, absence, model);
            return model;
        }
        protected List<IdNameDto> GetTimesheetStatusesForAbsence()
        {
            List<IdNameDto> dtos = TimesheetStatusDao.LoadAllSorted().
                Where(x => (x.Id >= AbsenceFirstTimesheetStatisId) && (x.Id <= AbsenceLastTimesheetStatisId)).ToList().
                ConvertAll(x => new IdNameDto(x.Id, x.Name)).OrderBy(x => x.Name).ToList();
            if (AuthenticationService.CurrentUser.UserRole == UserRole.Employee)
                dtos.Insert(0, new IdNameDto(0, string.Empty));
            return dtos;
        }
        protected void SetFlagsState(int id, User user, Absence absence, AbsenceEditModel model)
        {
            SetFlagsState(model, false);
            UserRole currentUserRole = AuthenticationService.CurrentUser.UserRole;
            if (id == 0)
            {
                model.IsSaveAvailable = true;
                model.IsAbsenceTypeEditable = true;
                model.IsApprovedEnable = true;
                switch (currentUserRole)
                {
                    case UserRole.Employee:
                        //model.IsApprovedByUserEnable = true;
                        break;
                    case UserRole.Manager:
                        //model.IsApprovedByManagerEnable = true;
                        //model.IsTimesheetStatusEditable = true;
                        break;
                    case UserRole.PersonnelManager:
                        //model.IsApprovedByPersonnelManagerEnable = true;
                        model.IsTimesheetStatusEditable = true;
                        model.IsApprovedForAllEnable = true;
                        break;
                }
                if ((currentUserRole & UserRole.PersonnelManager) == UserRole.PersonnelManager || (currentUserRole & UserRole.Manager) == UserRole.Manager)
                {
                    model.IsApprovedByUserEnable = false;
                    model.IsApprovedByUserHidden = model.IsApprovedByUser = true;
                }
                return;
            }
            model.IsApprovedByUserHidden = model.IsApprovedByUser = absence.UserDateAccept.HasValue;
            model.IsApprovedByManagerHidden = model.IsApprovedByManager = absence.ManagerDateAccept.HasValue;
            model.IsApprovedByPersonnelManagerHidden = model.IsApprovedByPersonnelManager = absence.PersonnelManagerDateAccept.HasValue;
            model.IsPostedTo1CHidden = model.IsPostedTo1C = absence.SendTo1C.HasValue;

            // hack to uncheck checkbox on UI
            if (absence.DeleteDate.HasValue && !absence.DeleteAfterSendTo1C)
                model.IsApprovedByPersonnelManager = false;

            switch (currentUserRole)
            {
                case UserRole.Employee:
                    if (!absence.UserDateAccept.HasValue && !absence.DeleteDate.HasValue)
                    {
                        //model.IsApprovedByUserEnable = true;
                        model.IsApprovedEnable = true;
                        //model.IsSaveAvailable = true;
                        if (!absence.ManagerDateAccept.HasValue && !absence.PersonnelManagerDateAccept.HasValue && !absence.SendTo1C.HasValue)
                            model.IsAbsenceTypeEditable = true;
                    }
                    break;
                case UserRole.Manager:
                    if (!absence.ManagerDateAccept.HasValue && !absence.DeleteDate.HasValue)
                    {
                        //model.IsApprovedByManagerEnable = true;
                        model.IsApprovedEnable = true;
                        if (!absence.PersonnelManagerDateAccept.HasValue && !absence.SendTo1C.HasValue)
                        {
                            model.IsAbsenceTypeEditable = true;
                            //model.IsTimesheetStatusEditable = true;
                        }
                    }
                    break;
                case UserRole.PersonnelManager:
                    if (!absence.PersonnelManagerDateAccept.HasValue)
                    {
                        //model.IsApprovedByPersonnelManagerEnable = true;
                        model.IsApprovedEnable = true;
                        model.IsApprovedForAllEnable = true;
                        if (!absence.SendTo1C.HasValue)
                        {
                            model.IsAbsenceTypeEditable = true;
                            model.IsTimesheetStatusEditable = true;
                        }
                    }
                    else if (!absence.SendTo1C.HasValue && !absence.DeleteDate.HasValue)
                        model.IsDeleteAvailable = true;
                    break;
                case UserRole.ConsultantOutsourcing:
                    if ((!absence.UserDateAccept.HasValue && !absence.DeleteDate.HasValue) || (!absence.ManagerDateAccept.HasValue && !absence.DeleteDate.HasValue) || !absence.PersonnelManagerDateAccept.HasValue)
                    {
                        if (!absence.PersonnelManagerDateAccept.HasValue)
                        {
                            //model.IsApprovedByPersonnelManagerEnable = true;
                            model.IsApprovedEnable = true;
                            model.IsApprovedForAllEnable = true;
                            if (!absence.SendTo1C.HasValue)
                            {
                                model.IsAbsenceTypeEditable = true;
                                model.IsTimesheetStatusEditable = true;
                            }
                        }
                        model.IsApproveForAllByConsultantEnable = true;
                    }
                    break;
                case UserRole.Estimator:
                case UserRole.OutsourcingManager:
                    if (absence.SendTo1C.HasValue && !absence.DeleteDate.HasValue)
                        model.IsDeleteAvailable = true;
                    break;
            }
            model.IsSaveAvailable = model.IsAbsenceTypeEditable || model.IsTimesheetStatusEditable;
            //|| model.IsApprovedByManagerEnable || model.IsApprovedByUserEnable ||
            //model.IsApprovedByPersonnelManagerEnable;
        }
        protected void SetFlagsState(AbsenceEditModel model, bool state)
        {
            model.IsApprovedByManager = state;
            model.IsApprovedByManagerHidden = state;
            model.IsApprovedByManagerEnable = state;

            model.IsApprovedByPersonnelManager = state;
            model.IsApprovedByPersonnelManagerHidden = state;
            model.IsApprovedByPersonnelManagerEnable = state;

            model.IsApprovedByUser = state;
            model.IsApprovedByUserHidden = state;
            model.IsApprovedByUserEnable = state;

            model.IsPostedTo1C = state;
            model.IsPostedTo1CHidden = state;
            model.IsPostedTo1CEnable = state;

            model.IsSaveAvailable = state;
            model.IsTimesheetStatusEditable = state;
            model.IsAbsenceTypeEditable = state;

            model.IsDelete = state;
            model.IsDeleteAvailable = state;

            model.IsApproved = state;
            model.IsApprovedEnable = state;

            model.IsApprovedForAll = state;
            model.IsApprovedForAllEnable = state;
        }
        public void ReloadDictionariesToModel(AbsenceEditModel model)
        {
            User user = UserDao.Load(model.UserId);
            IUser current = AuthenticationService.CurrentUser;
            SetUserInfoModel(user, model);
            model.CommentsModel = GetCommentsModel(model.Id, (int)RequestTypeEnum.Absence);
            model.TimesheetStatuses = GetTimesheetStatusesForAbsence();
            model.AbsenceTypes = GetAbsenceTypes(false);
            if (model.Id == 0)
            {
                model.CreatorLogin = current.Name;
                model.DateCreated = DateTime.Today.ToShortDateString();
            }
            else
            {
                Absence absence = AbsenceDao.Load(model.Id);
                model.CreatorLogin = absence.Creator.Name;
                model.DocumentNumber = absence.Number.ToString();
                model.DateCreated = absence.CreateDate.ToShortDateString();
                model.DaysCount = absence.DaysCount;
                model.DaysCountHidden = model.DaysCount;
                if (absence.DeleteDate.HasValue)
                    model.IsDeleted = true;
            }
        }
        public bool SaveAbsenceEditModel(AbsenceEditModel model, out string error)
        {
            error = string.Empty;
            User user = null;
            try
            {
                user = UserDao.Load(model.UserId);
                IUser current = AuthenticationService.CurrentUser;
                if (!CheckUserRights(user, current, model.Id, true) || !CheckUserRightsForEntity(user, current, model))
                {
                    error = "Редактирование заявки запрещено";
                    return false;
                }
                Absence absence;

                if (model.Id == 0)
                #region Сохранение новой неявки
                {
                    absence = new Absence
                    {
                        CreateDate = DateTime.Now,
                        Creator = UserDao.Load(current.Id),
                        // ReSharper disable PossibleInvalidOperationException
                        BeginDate = model.BeginDate.Value,
                        EndDate = model.EndDate.Value,
                        // ReSharper restore PossibleInvalidOperationException
                        DaysCount = model.EndDate.Value.Subtract(model.BeginDate.Value).Days + 1,
                        Number = RequestNextNumberDao.GetNextNumberForType((int)RequestTypeEnum.Absence),
                        //Status = RequestStatusDao.Load((int)RequestStatusEnum.NotApproved),
                        Type = AbsenceTypeDao.Load(model.AbsenceTypeId),
                        User = user
                    };

                    #region Согласование сотрудником

                    if ((current.UserRole & UserRole.Employee) == UserRole.Employee && current.Id == model.UserId && model.IsApproved)
                    {
                        absence.UserDateAccept = DateTime.Now;
                        SendEmailForUserRequest(absence.User, current, absence.Creator, false, absence.Id,
                         absence.Number, RequestTypeEnum.Absence, false);
                    }

                    #endregion

                    #region Согласование руководителем

                    bool canEdit = false;

                    if (((current.UserRole & UserRole.Manager) == UserRole.Manager && IsCurrentManagerForUser(user, current, out canEdit))
                        || HasCurrentManualRoleForUser(user, current, UserManualRole.ApprovesCommonRequests, out canEdit))
                    {
                        if (model.IsApprovedByUser && !absence.UserDateAccept.HasValue)
                            absence.UserDateAccept = DateTime.Now;
                        //absence.TimesheetStatus = TimesheetStatusDao.Load(model.TimesheetStatusId);
                        if (model.IsApproved)
                        {
                            absence.ManagerDateAccept = DateTime.Now;
                            SendEmailForUserRequest(absence.User, current, absence.Creator, false, absence.Id,
                                absence.Number, RequestTypeEnum.Absence, false);
                        }
                    }
                    #endregion

                    #region Согласование кадровиком
                    if ((current.UserRole & UserRole.PersonnelManager) == UserRole.PersonnelManager
                        && (user.Personnels.Where(x => x.Id == current.Id).FirstOrDefault() != null)
                        )
                    {
                        if (model.IsApprovedByUser && !absence.UserDateAccept.HasValue)
                            absence.UserDateAccept = DateTime.Now;
                        absence.TimesheetStatus = TimesheetStatusDao.Load(model.TimesheetStatusId);
                        if (model.IsApproved)
                        {
                            absence.PersonnelManagerDateAccept = DateTime.Now;
                            SendEmailForUserRequest(absence.User, current, absence.Creator, false, absence.Id,
                                absence.Number, RequestTypeEnum.Absence, false);
                        }
                        if (model.IsApprovedForAll && !absence.ManagerDateAccept.HasValue)
                            absence.ManagerDateAccept = DateTime.Now;
                    }
                    #endregion

                    AbsenceDao.SaveAndFlush(absence);
                    model.Id = absence.Id;
                }
                #endregion
                else
                #region Сохранение существующей неявки
                {
                    absence = AbsenceDao.Load(model.Id);
                    if (absence.SendTo1C.HasValue)
                    {
                        error = "Редактирование заявки запрещено, так как она выгружена в 1С!";
                        return false;
                    }
                    if (absence.Version != model.Version)
                    {
                        error = "Заявка была изменена другим пользователем.";
                        model.ReloadPage = true;
                        return false;
                    }
                    if (model.IsDelete)
                    {
                        if ((current.UserRole & UserRole.OutsourcingManager) == UserRole.OutsourcingManager || (current.UserRole & UserRole.Estimator) == UserRole.Estimator)
                            absence.DeleteAfterSendTo1C = true;
                        absence.CreateDate = DateTime.Now;
                        absence.DeleteDate = DateTime.Now;
                        AbsenceDao.SaveAndFlush(absence);
                        //model.TimesheetStatusId = absence.TimesheetStatus == null? 0:absence.TimesheetStatus.Id;
                        //model.AbsenceTypeId = absence.Type.Id;
                        model.IsDelete = false;
                        SendEmailForUserRequest(absence.User, current, absence.Creator, true, absence.Id,
                            absence.Number, RequestTypeEnum.Absence, false);
                    }
                    else
                    {
                        #region Согласование сотрудником
                        if ((current.UserRole & UserRole.Employee) == UserRole.Employee && current.Id == model.UserId
                                            && !absence.UserDateAccept.HasValue
                                            && model.IsApproved)
                        {
                            absence.UserDateAccept = DateTime.Now;
                            SendEmailForUserRequest(absence.User, current, absence.Creator, false, absence.Id,
                                absence.Number, RequestTypeEnum.Absence, false);
                        }
                        #endregion

                        #region Согласование руководителем
                        bool canEdit = false;

                        if (((current.UserRole & UserRole.Manager) == UserRole.Manager && IsCurrentManagerForUser(user, current, out canEdit))
                                || HasCurrentManualRoleForUser(user, current, UserManualRole.ApprovesCommonRequests, out canEdit))
                        {
                            if (!absence.ManagerDateAccept.HasValue && model.IsApproved)
                            {
                                absence.ManagerDateAccept = DateTime.Now;
                                SendEmailForUserRequest(absence.User, current, absence.Creator, false, absence.Id,
                                    absence.Number, RequestTypeEnum.Absence, false);
                            }
                        }
                        #endregion

                        #region Согласование кадровиком
                        if ((current.UserRole & UserRole.PersonnelManager) == UserRole.PersonnelManager
                                            && (user.Personnels.Where(x => x.Id == current.Id).FirstOrDefault() != null)
                                            && !absence.PersonnelManagerDateAccept.HasValue
                                            )
                        {
                            absence.TimesheetStatus = TimesheetStatusDao.Load(model.TimesheetStatusId);
                            if (model.IsApproved)
                            {
                                absence.PersonnelManagerDateAccept = DateTime.Now;
                                SendEmailForUserRequest(absence.User, current, absence.Creator, false, absence.Id,
                                    absence.Number, RequestTypeEnum.Absence, false);
                            }
                            if (model.IsApprovedForAll && !absence.ManagerDateAccept.HasValue)
                                absence.ManagerDateAccept = DateTime.Now;
                        }
                        #endregion

                        #region Согласование консультантом за всех
                        if ((current.UserRole & UserRole.ConsultantOutsourcing) == UserRole.ConsultantOutsourcing && model.IsApproveForAllByConsultant)
                        {
                            absence.TimesheetStatus = TimesheetStatusDao.Load(model.TimesheetStatusId);
                            if (!absence.UserDateAccept.HasValue && model.IsApproveForAllByConsultant)
                            {
                                absence.UserDateAccept = DateTime.Now;
                            }
                            if (!absence.ManagerDateAccept.HasValue && model.IsApproveForAllByConsultant)
                            {
                                absence.ManagerDateAccept = DateTime.Now;
                            }
                            if (!absence.PersonnelManagerDateAccept.HasValue && model.IsApproveForAllByConsultant)
                            {
                                absence.PersonnelManagerDateAccept = DateTime.Now;
                            }
                        }
                        #endregion

                        if (model.IsAbsenceTypeEditable)
                        {
                            // ReSharper disable PossibleInvalidOperationException
                            absence.BeginDate = model.BeginDate.Value;
                            absence.EndDate = model.EndDate.Value;
                            // ReSharper restore PossibleInvalidOperationException
                            absence.DaysCount = model.EndDate.Value.Subtract(model.BeginDate.Value).Days + 1;
                            absence.Type = AbsenceTypeDao.Load(model.AbsenceTypeId);
                        }
                        AbsenceDao.SaveAndFlush(absence);
                        if (absence.Version != model.Version)
                        {
                            absence.CreateDate = DateTime.Now;
                            AbsenceDao.SaveAndFlush(absence);
                        }

                    }
                    if (absence.DeleteDate.HasValue)
                        model.IsDeleted = true;
                }
                #endregion

                model.DocumentNumber = absence.Number.ToString();
                model.Version = absence.Version;
                model.DaysCount = absence.DaysCount;
                model.CreatorLogin = absence.Creator.Name;
                model.DateCreated = absence.CreateDate.ToShortDateString();
                SetFlagsState(absence.Id, user, absence, model);
                //throw new ArgumentException("Test exception");
                return true;
            }
            catch (Exception ex)
            {
                AbsenceDao.RollbackTran();
                Log.Error("Error on SaveAbsenceEditModel:", ex);
                error = string.Format("Исключение:{0}", ex.GetBaseException().Message);
                return false;
            }
            finally
            {
                SetUserInfoModel(user, model);
                model.CommentsModel = GetCommentsModel(model.Id, (int)RequestTypeEnum.Absence);
                model.TimesheetStatuses = GetTimesheetStatusesForAbsence();
                model.AbsenceTypes = GetAbsenceTypes(false);
                model.TimesheetStatusIdHidden = model.TimesheetStatusId;
                model.AbsenceTypeIdHidden = model.AbsenceTypeId;
                model.DaysCountHidden = model.DaysCount;
            }
        }
        #endregion

        #region Vacation list model
        public VacationListModel GetVacationListModel()
        {
            User user = UserDao.Load(AuthenticationService.CurrentUser.Id);
            IdNameReadonlyDto dep = GetDepartmentDto(user);
            VacationListModel model = new VacationListModel
            {
                UserId = AuthenticationService.CurrentUser.Id,
                //Department = GetDepartmentDto(user),
                DepartmentName = dep.Name,
                DepartmentId = dep.Id,
                DepartmentReadOnly = dep.IsReadOnly,
                VacationTypes = GetVacationTypes(true),
                RequestStatuses = GetRequestStatuses(CurrentUser.UserRole),
                Positions = GetPositions(user)
            };
            SetFlagsState(user, model);
            SetInitialDates(model);
            return model;
        }
        public void SetVacationListModel(VacationListModel model, bool hasError)
        {
            User user = UserDao.Load(AuthenticationService.CurrentUser.Id);
            //model.Departments = GetDepartments(user);
            model.RequestStatuses = GetRequestStatuses(CurrentUser.UserRole);
            model.Positions = GetPositions(user);
            model.VacationTypes = GetVacationTypes(true);
            if (hasError)
                model.Documents = new List<VacationDto>();
            else
            {
                if (model.Documents != null && model.IsOriginalReceivedModified && ((user.UserRole & UserRole.PersonnelManager) == UserRole.PersonnelManager))
                {
                    model.IsOriginalReceivedModified = false;
                    List<int> idsToApplyReceivedOriginals = model.Documents.Where(x => x.IsOriginalReceived).Select(x => x.Id).ToList();
                    ApplyReceivedOriginals(model, idsToApplyReceivedOriginals);
                }
                SetDocumentsToModel(model, user);
            }
            SetFlagsState(user, model);
        }

        protected void ApplyReceivedOriginals(VacationListModel model, List<int> idsToApplyReceivedOriginals)
        {
            List<Vacation> entities = VacationDao.LoadForIdsList(idsToApplyReceivedOriginals).ToList();
            foreach (Vacation entity in entities)
            {
                entity.IsOriginalReceived = true;
                VacationDao.SaveAndFlush(entity);
            }
        }

        protected void SetFlagsState(User user, VacationListModel model)
        {
            if ((user.UserRole & UserRole.PersonnelManager) == UserRole.PersonnelManager)
            {
                model.IsOriginalReceivedEditable = true;
            }
        }

        public void SetDocumentsToModel(VacationListModel model, User user)
        {
            UserRole role = (UserRole)(user.RoleId & (int)CurrentUser.UserRole);
            /*Department dep = null;
            if(model.Department.Id != 0)
             dep = DepartmentDao.SearchByNameDistinct(model.Department.Name);*/
            model.Documents = VacationDao.GetDocuments(user.Id,
                role,
                model.DepartmentId,
                //GetDepartmentId(model.Department),
                model.PositionId,
                model.VacationTypeId,
                //0,
                model.RequestStatusId,
                model.BeginDate,
                model.EndDate,
                model.UserName,
                model.SortBy,
                model.SortDescending,
                model.Number);
        }
        public List<IdNameDto> GetDepartments(User user)
        {
            //var departmentList = DepartmentDao.LoadAllSorted().ToList().ConvertAll(x => new IdNameDto(x.Id, x.Name));
            //departmentList.Insert(0,new IdNameDto(0,SelectAll));
            //var departmentList = UserToDepartmentDao.GetByUserId(user.Id).ToList();
            if ((UserRole)user.RoleId != UserRole.Employee)
            {
                var departmentList = DepartmentDao.LoadAllSorted().ToList().ConvertAll(x => new IdNameDto(x.Id, x.Name));
                departmentList.Insert(0, new IdNameDto(0, SelectAll));
                return departmentList;
            }
            if (user.Department == null)
                return new List<IdNameDto> { new IdNameDto(0, SelectAll) };
            return new List<IdNameDto> { new IdNameDto(user.Department.Id, user.Department.Name) };
        }
        public List<IdNameDto> GetVacationTypes(bool addAll)
        {
            List<IdNameDto> vacationTypeList = VacationTypeDao.LoadAllSorted().ToList()
                .ConvertAll(x => new IdNameDto(x.Id, x.Name));
            if (addAll)
                vacationTypeList.Insert(0, new IdNameDto(0, SelectAll));
            return vacationTypeList;
        }

        public List<IdNameDto> GetAdditionalVacationTypes()
        {
            List<IdNameDto> additionalVacationTypeList = AdditionalVacationTypeDao.LoadAllSorted().ToList()
                .ConvertAll(x => new IdNameDto(x.Id, x.Name));
            return additionalVacationTypeList;
        }

        public List<IdNameDto> GetRequestStatuses(UserRole adaptForRole = UserRole.NoRole)
        {
            //var requestStatusesList = RequestStatusDao.LoadAllSorted().ToList().ConvertAll(x => new IdNameDto(x.Id, x.Name));
            List<IdNameDto> requestStatusesList = new List<IdNameDto>
                                                       {
                                                           new IdNameDto(1, "Новые"),
                                                           new IdNameDto(2, "Одобренные сотрудником"),
                                                           new IdNameDto(3, "Не одобренные сотрудником"),
                                                           new IdNameDto(4, "Одобренные руководителем"),
                                                           new IdNameDto(5, "Не одобренные руководителем"),
                                                           new IdNameDto(6, "Одобренные кадровиком"),
                                                           new IdNameDto(7, "Не одобренные кадровиком"),
                                                           new IdNameDto(8, "Одобренные всеми"),
                                                           new IdNameDto(9, "Выгруженные в 1С"),
                                                           new IdNameDto(10, "Отклоненные"),
                                                       }.OrderBy(x => x.Name).ToList();
            switch (adaptForRole)
            {
                case UserRole.Estimator:
                case UserRole.OutsourcingManager:
                case UserRole.Manager:
                    requestStatusesList.Insert(0, new IdNameDto(11, "Требует моего одобрения"));
                    break;
                default:
                    break;
            }
            requestStatusesList.Insert(0, new IdNameDto(0, SelectAll));
            return requestStatusesList;
        }
        public List<IdNameDto> GetPositions(User user)
        {
            List<IdNameDto> positionList;
            if ((UserRole)user.RoleId != UserRole.Employee)
            {
                positionList = PositionDao.LoadAllSorted().ToList().ConvertAll(x => new IdNameDto(x.Id, x.Name));
                positionList.Insert(0, new IdNameDto(0, SelectAll));
            }
            else
            {
                Position position = user.Position;
                positionList = position != null
                    ? new List<IdNameDto> { new IdNameDto(position.Id, position.Name) }
                    : new List<IdNameDto> { new IdNameDto(0, SelectAll) };
            }
            return positionList;
        }
        #endregion

        #region Vacation edit model
        public VacationEditModel GetVacationEditModel(int id, int userId)
        {
            VacationEditModel model = new VacationEditModel { Id = id, UserId = userId };
            User user = UserDao.Load(userId);
            IUser current = AuthenticationService.CurrentUser;
            if (!CheckUserRights(user, current, id, false))
                throw new ArgumentException("Доступ запрещен.");
            SetUserInfoModel(user, model);
            SetAttachmentToModel(model, id, RequestAttachmentTypeEnum.Vacation);
            SetOrderScanAttachmentToModel(model, id, RequestAttachmentTypeEnum.VacationOrderScan);
            SetUnsignedOrderScanAttachmentToModel(model, id, RequestAttachmentTypeEnum.UnsignedVacationOrderScan);
            model.CommentsModel = GetCommentsModel(id, (int)RequestTypeEnum.Vacation);
            model.TimesheetStatuses = GetTimesheetStatusesForVacation();
            model.VacationTypes = GetVacationTypes(false);
            model.AdditionalVacationTypes = GetAdditionalVacationTypes();
            Vacation vacation = null;
            if (id == 0)
            {
                model.CreatorLogin = current.Name;
                model.Version = 0;
                model.DateCreated = DateTime.Today.ToShortDateString();
                /*if (user != null && user.VacationSaldo != null && user.VacationSaldo.Any())
                {
                    var saldos = user.VacationSaldo.Where(x => x.Date < DateTime.Now);
                    if (saldos != null && saldos.Any())
                    {
                        saldos = saldos.OrderByDescending(x => x.Date);
                        var saldo = saldos.First();
                        model.PrincipalVacationDaysLeft = saldo.SaldoPrimary;
                        model.AdditionalVacationDaysLeft = saldo.SaldoAdditional;
                    }
                }
                else
                {
                    model.PrincipalVacationDaysLeft = 0;
                    model.AdditionalVacationDaysLeft = 0;
                }*/
            }
            else
            {
                vacation = VacationDao.Load(id);
                if (vacation == null)
                    throw new ArgumentException(string.Format("Заявка на отпуск (id {0}) не найдена в базе данных.", id));
                model.Version = vacation.Version;
                model.VacationTypeId = vacation.Type.Id;
                model.VacationTypeIdHidden = model.VacationTypeId;
                model.AdditionalVacationTypeId = vacation.AdditionalVacationType != null ? vacation.AdditionalVacationType.Id : model.AdditionalVacationTypes[0].Id;
                model.BeginDate = vacation.BeginDate;//new DateTimeDto(vacation.BeginDate);//
                model.EndDate = vacation.EndDate;
                model.AdditionalVacationBeginDate = vacation.AdditionalVacationBeginDate;
                model.TimesheetStatusId = vacation.TimesheetStatus == null ? 0 : vacation.TimesheetStatus.Id;
                model.TimesheetStatusIdHidden = model.TimesheetStatusId;
                model.DaysCount = vacation.DaysCount;
                model.AdditionalVacationDaysCount = vacation.AdditionalVacationDaysCount;
                model.DaysCountHidden = model.DaysCount;
                model.CreatorLogin = vacation.Creator.Name;
                model.DocumentNumber = vacation.Number.ToString();
                model.DateCreated = vacation.CreateDate.ToShortDateString();
                if (vacation.User != null && vacation.User.VacationSaldo != null && vacation.User.VacationSaldo.Any())
                {
                    var saldos = vacation.User.VacationSaldo.Where(x => x.Date < vacation.CreateDate);
                    if (saldos != null && saldos.Any())
                    {
                        saldos = saldos.OrderByDescending(x => x.Date);
                        var saldo = saldos.First();
                        model.PrincipalVacationDaysLeft = saldo.SaldoPrimary;
                        model.AdditionalVacationDaysLeft = saldo.SaldoAdditional;
                    }
                }
                else
                {
                    model.PrincipalVacationDaysLeft = 0;
                    model.AdditionalVacationDaysLeft = 0;
                }
                if (vacation.DeleteDate.HasValue)
                    model.IsDeleted = true;
            }
            SetFlagsState(id, user, vacation, model);
            return model;
        }
        public Result SaveVacationFileAfter1C(VacationEditModel model, UploadFileDto fileDto)
        {
            if(model.Id<=0) return new Result(false,"");
            var vacation = VacationDao.Load(model.Id);
            if (!vacation.SendTo1C.HasValue) 
                return new Result(false, "");
            if ( vacation.User.Id != CurrentUser.Id)
                return new Result(false, "");
            string fileName;
            int? orderScanAttachmentId = SaveAttachment(vacation.Id, model.OrderScanAttachmentId, fileDto, RequestAttachmentTypeEnum.VacationOrderScan, out fileName);
            if (orderScanAttachmentId.HasValue)
            {
                model.OrderScanAttachmentId = orderScanAttachmentId.Value;
                model.OrderScanAttachment = fileName;
                return new Result(true, "Файл успешно сохранен.");
            }
            return new Result(false, "");
        }
        public bool SaveVacationEditModel(VacationEditModel model, UploadFileDto fileDto, UploadFileDto unsignedOrderScanFileDto, UploadFileDto orderScanFileDto, out string error)
        {
            error = string.Empty;
            User user = null;
            try
            {
                user = UserDao.Load(model.UserId);
                IUser current = AuthenticationService.CurrentUser;
                if (!CheckUserRights(user, current, model.Id, true) || !CheckUserRightsForEntity(user, current, model))
                {
                    error = "Редактирование заявки запрещено";
                    return false;
                }
                Vacation vacation;

                if (model.Id == 0)
                #region Сохранение нового отпуска
                {
                    vacation = new Vacation
                    {
                        // ReSharper disable PossibleInvalidOperationException
                        BeginDate = model.BeginDate.Value,
                        EndDate = model.EndDate.Value,
                        AdditionalVacationBeginDate = model.AdditionalVacationBeginDate,
                        // ReSharper restore PossibleInvalidOperationException
                        CreateDate = DateTime.Now,
                        Creator = UserDao.Load(current.Id),
                        DaysCount = model.EndDate.Value.Subtract(model.BeginDate.Value).Days + 1,
                        AdditionalVacationDaysCount = model.AdditionalVacationBeginDate.HasValue ? model.EndDate.Value.Subtract(model.AdditionalVacationBeginDate.Value).Days + 1 : 0,
                        Number = RequestNextNumberDao.GetNextNumberForType((int)RequestTypeEnum.Vacation),
                        //Status = RequestStatusDao.Load((int) RequestStatusEnum.NotApproved),
                        Type = VacationTypeDao.Load(model.VacationTypeId),
                        AdditionalVacationType = IsAdditionalVacationTypeNecessary(model) ? additionalVacationTypeDao.Get(model.AdditionalVacationTypeId) : null,
                        User = user,
                        //UserFullNameForPrint = user.FullName, 
                    };

                    #region Согласование сотрудником
                    if ((current.UserRole & UserRole.Employee) == UserRole.Employee && current.Id == model.UserId
                        && model.IsApproved && !vacation.UserDateAccept.HasValue)
                    {
                        vacation.UserDateAccept = DateTime.Now;
                        SendEmailForUserRequest(vacation.User, current, vacation.Creator, false, vacation.Id,
                            vacation.Number, RequestTypeEnum.Vacation, false);
                    }
                    #endregion

                    #region Согласовано руководителем
                    bool canEdit = false;

                    if (((current.UserRole & UserRole.Manager) == UserRole.Manager && IsCurrentManagerForUser(user, current, out canEdit))
                            || HasCurrentManualRoleForUser(user, current, UserManualRole.ApprovesCommonRequests, out canEdit))
                    {
                        //vacation.TimesheetStatus = TimesheetStatusDao.Load(model.TimesheetStatusId);
                        if (model.IsApprovedByUser && !vacation.UserDateAccept.HasValue)
                            vacation.UserDateAccept = DateTime.Now;
                        if (model.IsApproved && !vacation.ManagerDateAccept.HasValue)
                        {
                            vacation.ManagerDateAccept = DateTime.Now;
                            SendEmailForUserRequest(vacation.User, current, vacation.Creator, false, vacation.Id,
                                vacation.Number, RequestTypeEnum.Vacation, false);
                        }
                    }
                    #endregion

                    #region Согласовано кадровиком
                    if ((current.UserRole & UserRole.PersonnelManager) == UserRole.PersonnelManager
                                    && (user.Personnels.Where(x => x.Id == current.Id).FirstOrDefault() != null)
                                    )
                    {
                        vacation.TimesheetStatus = TimesheetStatusDao.Load(model.TimesheetStatusId);
                        if (model.IsApprovedByUser && !vacation.UserDateAccept.HasValue)
                            vacation.UserDateAccept = DateTime.Now;
                        if (model.IsApproved && !vacation.PersonnelManagerDateAccept.HasValue)
                        {
                            vacation.PersonnelManagerDateAccept = DateTime.Now;
                            SendEmailForUserRequest(vacation.User, current, vacation.Creator, false, vacation.Id,
                                vacation.Number, RequestTypeEnum.Vacation, false);
                        }
                        if (model.IsApprovedForAll && !vacation.ManagerDateAccept.HasValue)
                            vacation.ManagerDateAccept = DateTime.Now;
                    }
                    #endregion

                    #region Согласование консультантом за всех
                    if ((current.UserRole & UserRole.ConsultantOutsourcing) == UserRole.ConsultantOutsourcing && model.IsApproveForAllByConsultant)
                    {
                        if (!vacation.UserDateAccept.HasValue && model.IsApproveForAllByConsultant)
                        {
                            vacation.UserDateAccept = DateTime.Now;
                        }
                        if (!vacation.ManagerDateAccept.HasValue && model.IsApproveForAllByConsultant)
                        {
                            vacation.ManagerDateAccept = DateTime.Now;
                        }
                        if (!vacation.PersonnelManagerDateAccept.HasValue && model.IsApproveForAllByConsultant)
                        {
                            vacation.PersonnelManagerDateAccept = DateTime.Now;
                        }
                    }
                    #endregion

                    VacationDao.SaveAndFlush(vacation);
                    model.Id = vacation.Id;
                }
                #endregion
                else
                #region Сохранение существующего отпуска
                {
                    vacation = VacationDao.Load(model.Id);
                    if (vacation.SendTo1C.HasValue)
                    {
                        error = "Редактирование заявки запрещено, так как она выгружена в 1С!";
                        return false;
                    }

                    string fileName;
                    int? attachmentId = SaveAttachment(vacation.Id, model.AttachmentId, fileDto, RequestAttachmentTypeEnum.Vacation, out fileName);
                    if (attachmentId.HasValue)
                    {
                        model.AttachmentId = attachmentId.Value;
                        model.Attachment = fileName;
                    }
                    // ---------------------------------------
                    int? orderScanAttachmentId = SaveAttachment(vacation.Id, model.OrderScanAttachmentId, orderScanFileDto, RequestAttachmentTypeEnum.VacationOrderScan, out fileName);
                    if (orderScanAttachmentId.HasValue)
                    {
                        model.OrderScanAttachmentId = orderScanAttachmentId.Value;
                        model.OrderScanAttachment = fileName;
                    }
                    // ---------------------------------------
                    int? unsignedOrderScanAttachmentId = SaveAttachment(vacation.Id, model.UnsignedOrderScanAttachmentId, unsignedOrderScanFileDto, RequestAttachmentTypeEnum.UnsignedVacationOrderScan, out fileName);
                    if (unsignedOrderScanAttachmentId.HasValue)
                    {
                        model.UnsignedOrderScanAttachmentId = unsignedOrderScanAttachmentId.Value;
                        model.UnsignedOrderScanAttachment = fileName;
                    }
                    // ---------------------------------------
                    if (vacation.Version != model.Version)
                    {
                        error = "Заявка была изменена другим пользователем.";
                        model.ReloadPage = true;
                        return false;
                    }
                    if (model.IsDelete)
                    {
                        if (model.AttachmentId > 0)
                            RequestAttachmentDao.Delete(model.AttachmentId);
                        // ----------------------------
                        if (model.OrderScanAttachmentId > 0)
                            RequestAttachmentDao.Delete(model.OrderScanAttachmentId);
                        // ----------------------------
                        if (model.UnsignedOrderScanAttachmentId > 0)
                            RequestAttachmentDao.Delete(model.UnsignedOrderScanAttachmentId);
                        // ----------------------------
                        model.AttachmentId = 0;
                        model.Attachment = string.Empty;
                        model.OrderScanAttachmentId = 0;
                        model.OrderScanAttachment = string.Empty;
                        model.UnsignedOrderScanAttachmentId = 0;
                        model.UnsignedOrderScanAttachment = string.Empty;
                        if ((current.UserRole & UserRole.OutsourcingManager) == UserRole.OutsourcingManager || (current.UserRole & UserRole.Estimator) == UserRole.Estimator)
                            vacation.DeleteAfterSendTo1C = true;
                        vacation.CreateDate = DateTime.Now;
                        vacation.DeleteDate = DateTime.Now;
                        VacationDao.SaveAndFlush(vacation);
                        model.IsDelete = false;
                        SendEmailForUserRequest(vacation.User, current, vacation.Creator, true, vacation.Id,
                            vacation.Number, RequestTypeEnum.Vacation, false);
                        //model.VacationTypeId = vacation.Type.Id;
                        //model.TimesheetStatusId = vacation.TimesheetStatus == null ? 0 : vacation.TimesheetStatus.Id;
                    }
                    else
                    {
                        #region Согласование сотрудником
                        if ((current.UserRole & UserRole.Employee) == UserRole.Employee && current.Id == model.UserId
                            && !vacation.UserDateAccept.HasValue
                            && model.IsApproved)
                        {
                            vacation.UserDateAccept = DateTime.Now;
                            SendEmailForUserRequest(vacation.User, current, vacation.Creator, false, vacation.Id,
                                vacation.Number, RequestTypeEnum.Vacation, false);
                        }
                        #endregion

                        #region Согласование руководителем
                        bool canEdit = false;

                        if (((current.UserRole & UserRole.Manager) == UserRole.Manager && IsCurrentManagerForUser(user, current, out canEdit))
                                || HasCurrentManualRoleForUser(user, current, UserManualRole.ApprovesCommonRequests, out canEdit))
                        {
                            if (!vacation.ManagerDateAccept.HasValue)
                            {
                                //vacation.TimesheetStatus = TimesheetStatusDao.Load(model.TimesheetStatusId);
                                if (model.IsApproved)
                                {
                                    vacation.ManagerDateAccept = DateTime.Now;
                                    SendEmailForUserRequest(vacation.User, current, vacation.Creator, false, vacation.Id,
                                        vacation.Number, RequestTypeEnum.Vacation, false);
                                }
                            }
                        }
                        #endregion

                        #region Согласование кадровиком
                        if ((current.UserRole & UserRole.PersonnelManager) == UserRole.PersonnelManager /*&& user.PersonnelManager != null
                            && current.Id == user.PersonnelManager.Id*/
                            && (user.Personnels.Where(x => x.Id == current.Id).FirstOrDefault() != null)
                            && !vacation.PersonnelManagerDateAccept.HasValue)
                        {
                            vacation.TimesheetStatus = TimesheetStatusDao.Load(model.TimesheetStatusId);
                            vacation.PrincipalVacationDaysLeft = model.PrincipalVacationDaysLeft;
                            vacation.AdditionalVacationDaysLeft = model.AdditionalVacationDaysLeft;
                            if (model.IsApproved)
                            {
                                vacation.PersonnelManagerDateAccept = DateTime.Now;
                                SendEmailForUserRequest(vacation.User, current, vacation.Creator, false, vacation.Id,
                                    vacation.Number, RequestTypeEnum.Vacation, false);
                            }
                            if (model.IsApprovedForAll && !vacation.ManagerDateAccept.HasValue)
                                vacation.ManagerDateAccept = DateTime.Now;

                        }
                        #endregion

                        #region Согласование консультантом за всех
                        if ((current.UserRole & UserRole.ConsultantOutsourcing) == UserRole.ConsultantOutsourcing && model.IsApproveForAllByConsultant)
                        {
                            if (!vacation.UserDateAccept.HasValue)
                            {
                                vacation.UserDateAccept = DateTime.Now;
                            }

                            if (!vacation.ManagerDateAccept.HasValue)
                            {
                                vacation.ManagerDateAccept = DateTime.Now;
                            }

                            if (!vacation.PersonnelManagerDateAccept.HasValue)
                            {
                                vacation.TimesheetStatus = TimesheetStatusDao.Load(model.TimesheetStatusId);
                                vacation.PrincipalVacationDaysLeft = model.PrincipalVacationDaysLeft;
                                vacation.AdditionalVacationDaysLeft = model.AdditionalVacationDaysLeft;
                            }
                        }
                        #endregion

                        if (model.IsVacationTypeEditable)
                        {
                            // ReSharper disable PossibleInvalidOperationException
                            vacation.BeginDate = model.BeginDate.Value;
                            vacation.EndDate = model.EndDate.Value;
                            vacation.AdditionalVacationBeginDate = model.AdditionalVacationBeginDate;
                            // ReSharper restore PossibleInvalidOperationException
                            vacation.DaysCount = model.EndDate.Value.Subtract(model.BeginDate.Value).Days + 1;
                            vacation.Type = VacationTypeDao.Load(model.VacationTypeId);
                            vacation.AdditionalVacationType = IsAdditionalVacationTypeNecessary(model) ? AdditionalVacationTypeDao.Load(model.AdditionalVacationTypeId) : null;
                            vacation.AdditionalVacationDaysCount = model.AdditionalVacationBeginDate.HasValue ? model.EndDate.Value.Subtract(model.AdditionalVacationBeginDate.Value).Days + 1 : 0;
                        }
                        //vacation.UserFullNameForPrint = user.FullName;
                        VacationDao.SaveAndFlush(vacation);
                        if (vacation.Version != model.Version)
                        {
                            vacation.CreateDate = DateTime.Now;
                            VacationDao.SaveAndFlush(vacation);
                        }

                    }
                    if (vacation.DeleteDate.HasValue)
                        model.IsDeleted = true;
                }
                #endregion

                model.DocumentNumber = vacation.Number.ToString();
                model.Version = vacation.Version;
                model.DaysCount = vacation.DaysCount;
                model.AdditionalVacationDaysCount = vacation.AdditionalVacationDaysCount;
                model.PrincipalVacationDaysLeft = vacation.PrincipalVacationDaysLeft;
                model.AdditionalVacationDaysLeft = vacation.AdditionalVacationDaysLeft;
                model.CreatorLogin = vacation.Creator.Name;
                model.DateCreated = vacation.CreateDate.ToShortDateString();
                SetFlagsState(vacation.Id, user, vacation, model);
                return true;
            }
            catch (Exception ex)
            {
                AbsenceDao.RollbackTran();
                Log.Error("Error on GetVacationEditModel:", ex);
                error = string.Format("Исключение:{0}", ex.GetBaseException().Message);
                return false;
            }
            finally
            {
                SetUserInfoModel(user, model);
                model.CommentsModel = GetCommentsModel(model.Id, (int)RequestTypeEnum.Vacation);
                model.TimesheetStatuses = GetTimesheetStatusesForVacation();
                model.VacationTypes = GetVacationTypes(false);
                model.VacationTypeIdHidden = model.VacationTypeId;
                model.AdditionalVacationTypes = GetAdditionalVacationTypes();
                model.TimesheetStatusIdHidden = model.TimesheetStatusId;
                model.DaysCountHidden = model.DaysCount;
            }
        }

        public bool CheckUserRightsForEntity(User user, IUser current, ICheckForEntity model)
        {
            if ((current.UserRole & UserRole.OutsourcingManager) == UserRole.OutsourcingManager || (current.UserRole & UserRole.Estimator) == UserRole.Estimator)
            {
                if (model.IsDeleteAvailable && model.IsDelete)
                    return true;
                throw new ArgumentException("Вам запрещено редактировать заявки.");
            }
            return true;
        }
        public bool CheckUserRights(User user, IUser current, int entityId, bool isSave)
        {
            switch (current.UserRole)
            {
                case UserRole.Employee:
                    if (user.Id != current.Id)
                    {
                        Log.ErrorFormat("CheckUserRights user.Id {0} current.Id {1}", user.Id, current.Id);
                        return false;
                    }
                    break;
                case UserRole.DismissedEmployee:
                    if (user.Id != current.Id)
                    {
                        Log.ErrorFormat("CheckUserRights user.Id {0} current.Id {1}", user.Id, current.Id);
                        return false;
                    }
                    break;
                case UserRole.Manager:

                    bool canEdit = false;

                    if (!IsCurrentManagerForUser(user, current, out canEdit) && !HasCurrentManualRoleForUser(user, current, UserManualRole.ApprovesCommonRequests, out canEdit))
                    {
                        Log.ErrorFormat("CheckUserRights user.Id {0} current.Id {1} user.Manager.Id {2}", user.Id, current.Id, user.Manager.Id);
                        return false;
                    }
                    break;
                case UserRole.Estimator:
                    return true;
                case UserRole.PersonnelManager:
                    int? superPersonnelId = ConfigurationService.SuperPersonnelId;
                    if (superPersonnelId.HasValue && CurrentUser.Id == superPersonnelId.Value)
                        return true;
                    if (/*user.PersonnelManager != null && user.PersonnelManager.Id != current.Id*/
                        user.Personnels.Where(x => x.Id == current.Id).FirstOrDefault() == null
                        )
                    {
                        Log.ErrorFormat("CheckUserRights  PersonnelManager user.Id {0} current.Id {1}", user.Id, current.Id);
                        return false;
                    }
                    break;
                case UserRole.Inspector:
                    Log.WarnFormat("CheckUserRights Inspector user.Id {0} current.Id {1}", user.Id, current.Id);
                    if (entityId == 0)
                        throw new ArgumentException("Вам запрещено создавать новые заявки.");
                    if (isSave)
                        throw new ArgumentException("Вам запрещено редактировать заявки.");
                    return InspectorToUserDao.IsInspectorToUserRecordExists(current.Id, user.Id);
                case UserRole.Chief:
                    Log.WarnFormat("CheckUserRights Chief user.Id {0} current.Id {1}", user.Id, current.Id);
                    if (entityId == 0)
                        throw new ArgumentException("Вам запрещено создавать новые заявки.");
                    if (isSave)
                        throw new ArgumentException("Вам запрещено редактировать заявки.");
                    return ChiefToUserDao.IsChiefToUserRecordExists(current.Id, user.Id);
            }
            return true;
        }
        public void ReloadDictionariesToModel(VacationEditModel model)
        {
            User user = UserDao.Load(model.UserId);
            IUser current = AuthenticationService.CurrentUser;
            SetUserInfoModel(user, model);
            model.CommentsModel = GetCommentsModel(model.Id, (int)RequestTypeEnum.Vacation);
            model.TimesheetStatuses = GetTimesheetStatusesForVacation();
            model.VacationTypes = GetVacationTypes(false);
            model.AdditionalVacationTypes = GetAdditionalVacationTypes();
            if (model.Id == 0)
            {
                model.CreatorLogin = current.Name;
                model.DateCreated = DateTime.Today.ToShortDateString();
            }
            else
            {
                Vacation vacation = VacationDao.Load(model.Id);
                model.CreatorLogin = vacation.Creator.Name;
                model.DocumentNumber = vacation.Number.ToString();
                model.DateCreated = vacation.CreateDate.ToShortDateString();
                model.DaysCount = vacation.DaysCount;
                model.DaysCountHidden = model.DaysCount;
                if (vacation.DeleteDate.HasValue)
                    model.IsDeleted = true;
            }
        }
        protected void SetFlagsState(VacationEditModel model, bool state)
        {
            model.IsApprovedByManager = state;
            model.IsApprovedByManagerHidden = state;
            model.IsApprovedByManagerEnable = state;

            model.IsApprovedByPersonnelManager = state;
            model.IsApprovedByPersonnelManagerHidden = state;
            model.IsApprovedByPersonnelManagerEnable = state;

            model.IsApprovedByUser = state;
            model.IsApprovedByUserHidden = state;
            model.IsApprovedByUserEnable = state;

            model.IsPostedTo1C = state;
            model.IsPostedTo1CHidden = state;
            model.IsPostedTo1CEnable = state;

            model.IsSaveAvailable = state;
            model.IsTimesheetStatusEditable = state;
            model.IsVacationTypeEditable = state;

            model.IsDelete = state;
            model.IsDeleteAvailable = state;
            model.IsPrintAvailable = state;

            model.IsApproved = state;
            model.IsApprovedEnable = state;

            model.IsApprovedForAll = state;
            model.IsApprovedForAllEnable = state;
        }
        protected void SetFlagsState(int id, User user, Vacation vacation, VacationEditModel model)
        {
            SetFlagsState(model, false);
            int? superPersonnelId = ConfigurationService.SuperPersonnelId;
            UserRole currentUserRole = AuthenticationService.CurrentUser.UserRole;
            if (id == 0)
            {
                model.IsSaveAvailable = true;
                model.IsVacationTypeEditable = true;
                model.IsApprovedEnable = false;
                switch (currentUserRole)
                {
                    case UserRole.Employee:
                        //model.IsApprovedByUserEnable = false;
                        break;
                    case UserRole.Manager:
                        //model.IsApprovedByManagerEnable = false;
                        //model.IsTimesheetStatusEditable = true;
                        break;
                    case UserRole.Estimator:
                    case UserRole.PersonnelManager:
                        //model.IsApprovedByPersonnelManagerEnable = false;
                        model.IsTimesheetStatusEditable = true;
                        break;
                }
                if ((currentUserRole & UserRole.PersonnelManager) == UserRole.PersonnelManager|| (CurrentUser.UserRole & UserRole.Estimator)>0 || (currentUserRole & UserRole.Manager) == UserRole.Manager)
                {
                    model.IsApprovedByUserEnable = false;
                    model.IsApprovedByUserHidden = model.IsApprovedByUser = true;
                }
                return;
            }

            model.IsApprovedByUserHidden = model.IsApprovedByUser = vacation.UserDateAccept.HasValue;
            model.IsApprovedByManagerHidden = model.IsApprovedByManager = vacation.ManagerDateAccept.HasValue;
            model.IsApprovedByPersonnelManagerHidden =
                model.IsApprovedByPersonnelManager = vacation.PersonnelManagerDateAccept.HasValue;
            model.IsPostedTo1CHidden = model.IsPostedTo1C = vacation.SendTo1C.HasValue;

            // hack to uncheck checkbox on UI
            if (vacation.DeleteDate.HasValue && !vacation.DeleteAfterSendTo1C)
                model.IsApprovedByPersonnelManager = false;

            RequestPrintForm form = RequestPrintFormDao.FindByRequestAndTypeId(id, RequestPrintFormTypeEnum.Vacation);
            model.IsPrintAvailable = form != null;

            switch (currentUserRole)
            {
                case UserRole.Employee:
                    if (model.IsPostedTo1C && model.OrderScanAttachmentId <= 0)
                    {
                        model.IsConfirmationAllowed = true;
                    }
                    if (!vacation.UserDateAccept.HasValue && !vacation.DeleteDate.HasValue)
                    {
                        if (model.AttachmentId > 0)
                            model.IsApprovedEnable = true;
                        if (!vacation.ManagerDateAccept.HasValue && !vacation.PersonnelManagerDateAccept.HasValue && !vacation.SendTo1C.HasValue)
                        {
                            model.IsVacationTypeEditable = true;
                            if (IsAdditionalVacationTypeNecessary(model))
                            {
                                model.IsAdditionalVacationTypeEditable = true;
                            }
                        }
                    }
                    break;
                case UserRole.Manager:
                    if (!vacation.ManagerDateAccept.HasValue && !vacation.DeleteDate.HasValue)
                    {
                        if (model.AttachmentId > 0)
                            model.IsApprovedEnable = true;
                        if (!vacation.PersonnelManagerDateAccept.HasValue && !vacation.SendTo1C.HasValue)
                        {
                            model.IsVacationTypeEditable = true;
                            //model.IsTimesheetStatusEditable = true;                            
                            if (IsAdditionalVacationTypeNecessary(model))
                            {
                                model.IsAdditionalVacationTypeEditable = true;
                            }
                        }
                    }
                    break;
                case UserRole.PersonnelManager:
                    model.IsUnsignedConfirmationAllowed = true;
                    if (!vacation.PersonnelManagerDateAccept.HasValue && (!superPersonnelId.HasValue || AuthenticationService.CurrentUser.Id != superPersonnelId.Value))
                    {
                        if (model.AttachmentId > 0)
                        {
                            model.IsApprovedEnable = true;
                            model.IsApprovedForAllEnable = true;

                            // расчетчики
                            if (superPersonnelId.HasValue && AuthenticationService.CurrentUser.Id == superPersonnelId.Value)
                            {
                                // могут послать уведомление об ошибках пользователю, если заявка отправлена пользователем на согласование, но еще не выгружена в 1С
                                if (vacation.UserDateAccept != null && vacation.SendTo1C == null)
                                {
                                    model.IsErrorNotificationAvailable = true;
                                }
                            }
                        }
                        if (!vacation.SendTo1C.HasValue)
                        {
                            model.IsVacationTypeEditable = true;
                            model.IsTimesheetStatusEditable = true;
                        }
                        model.IsDaysLeftEditable = true;
                    }
                    else if (!vacation.SendTo1C.HasValue &&
                             !vacation.DeleteDate.HasValue)
                        model.IsDeleteAvailable = true;

                    break;
                case UserRole.Estimator:
                    model.IsUnsignedConfirmationAllowed = true;
                    if (!vacation.PersonnelManagerDateAccept.HasValue)
                    {
                        if (model.AttachmentId > 0)
                        {
                            model.IsApprovedEnable = true;
                            model.IsApprovedForAllEnable = true;                            
                            // могут послать уведомление об ошибках пользователю, если заявка отправлена пользователем на согласование, но еще не выгружена в 1С
                            if (vacation.UserDateAccept != null && vacation.SendTo1C == null)
                            {
                                model.IsErrorNotificationAvailable = true;
                            }                            
                        }
                        if (!vacation.SendTo1C.HasValue)
                        {
                            model.IsVacationTypeEditable = true;
                            model.IsTimesheetStatusEditable = true;
                        }
                        model.IsDaysLeftEditable = true;
                    }
                    else if (!vacation.SendTo1C.HasValue &&
                             !vacation.DeleteDate.HasValue)
                        model.IsDeleteAvailable = true;

                    break;
                case UserRole.OutsourcingManager:
                    if (vacation.SendTo1C.HasValue && !vacation.DeleteDate.HasValue)
                        model.IsDeleteAvailable = true;
                    break;
                case UserRole.ConsultantOutsourcing:
                    if ((!vacation.UserDateAccept.HasValue && !vacation.DeleteDate.HasValue) || (!vacation.ManagerDateAccept.HasValue && !vacation.DeleteDate.HasValue) || !vacation.PersonnelManagerDateAccept.HasValue)
                    {
                        model.IsApproveForAllByConsultantEnable = true;

                        if (!vacation.ManagerDateAccept.HasValue)
                        {
                            if (IsAdditionalVacationTypeNecessary(model))
                                model.IsAdditionalVacationTypeEditable = true;
                        }

                        //дальше все как у кадровиков
                        model.IsUnsignedConfirmationAllowed = true;
                        if (!vacation.PersonnelManagerDateAccept.HasValue && (!superPersonnelId.HasValue || AuthenticationService.CurrentUser.Id != superPersonnelId.Value))
                        {
                            if (model.AttachmentId > 0)
                            {
                                model.IsApprovedEnable = true;
                                model.IsApprovedForAllEnable = true;

                                // расчетчики
                                if (superPersonnelId.HasValue && AuthenticationService.CurrentUser.Id == superPersonnelId.Value)
                                {
                                    // могут послать уведомление об ошибках пользователю, если заявка отправлена пользователем на согласование, но еще не выгружена в 1С
                                    if (vacation.UserDateAccept != null && vacation.SendTo1C == null)
                                    {
                                        model.IsErrorNotificationAvailable = true;
                                    }
                                }
                            }
                            if (!vacation.SendTo1C.HasValue)
                            {
                                model.IsVacationTypeEditable = true;
                                model.IsTimesheetStatusEditable = true;
                            }
                            model.IsDaysLeftEditable = true;
                        }
                        else if (!vacation.SendTo1C.HasValue &&
                                 !vacation.DeleteDate.HasValue)
                            model.IsDeleteAvailable = true;
                    }
                    break;
            }
            model.IsSaveAvailable = model.IsVacationTypeEditable || model.IsTimesheetStatusEditable;
            //|| model.IsApprovedByManagerEnable || model.IsApprovedByUserEnable ||
            //model.IsApprovedByPersonnelManagerEnable;
        }
        protected void SetUserInfoModel(User user, UserInfoModel model, UserManualRole manualRoleForManagersList = UserManualRole.ApprovesCommonRequests)
        {
            //model.DateCreated = DateTime.Today.ToShortDateString();
            //IList<IdNameDto> departments = UserToDepartmentDao.GetByUserId(user.Id);
            //if (departments.Count > 0)
            model.Department = user.Department == null ? string.Empty : user.Department.Name;
            if (user.Manager != null)
                model.ManagerName = user.Manager.FullName;

            // Руководители до 4 уровня по ветке
            IList<User> managers = null;
            //для ветки руководства скб
            if (user.Department != null && user.Department.Path.StartsWith("9900424.9900426.9900427."))
            {
                //для приказов на командировки, авансовых отчетов убираем руководителей 4 уровня
                if (model.GetType().Name == "MissionOrderEditModel" || model.GetType().Name == "MissionReportEditModel" || model.GetType().Name == "SicklistEditModel" ||
                    model.GetType().Name == "MissionEditModel" || model.GetType().Name == "AbsenceEditModel" || model.GetType().Name == "VacationEditModel" ||
                    model.GetType().Name == "ChildVacationEditModel" || model.GetType().Name == "DismissalEditModel" || model.GetType().Name == "ClearanceChecklistEditModel")
                    //managers.Clear();
                    managers = GetManagersForEmployee(user.Id, 4)
                        //.Where<User>(manager => (manager.Level != 4 && manager.Department.Id != 4395) && (manager.Level != 6 && manager.Department.Id != 6409))
                        .Where<User>(manager => (manager.Level == 2 && (manager.Id == 111 || manager.Id == 143)))
                        .OrderByDescending<User, int?>(manager => manager.Level)
                        .ToList<User>();
                else
                {
                    managers = GetManagersForEmployee(user.Id, 4)
                    .OrderByDescending<User, int?>(manager => manager.Level)
                    .ToList<User>();
                }
            }
            else
            {
                managers = GetManagersForEmployee(user.Id, 4)
                .OrderByDescending<User, int?>(manager => manager.Level)
                .ToList<User>();
            }





            // + руководители по ручным привязкам
            IList<User> manualRoleManagers = ManualRoleRecordDao.GetManualRoleHoldersForUser(user.Id, manualRoleForManagersList);
            foreach (var manualRoleManager in manualRoleManagers)
            {
                if (!managers.Contains(manualRoleManager))
                {
                    managers.Add(manualRoleManager);
                }
            }

            StringBuilder managersBuilder = new StringBuilder();
            foreach (var manager in managers)
            {
                managersBuilder.AppendFormat("{0} ({1}), ", manager.Name, manager.Position == null ? "<не указана>" : manager.Position.Name);
            }

            // Cut off trailing ", "
            if (managersBuilder.Length >= 2)
            {
                managersBuilder.Remove(managersBuilder.Length - 2, 2);
            }

            model.Managers = managersBuilder.ToString();


            /*if (user.PersonnelManager != null)
                model.PersonnelName = user.PersonnelManager.FullName;*/
            if (user.Personnels.Count() > 0)
                model.PersonnelName = user.Personnels.Aggregate(string.Empty, (current, entity) => current + (entity.FullName + "; "));
            if (user.Organization != null)
                model.Organization = user.Organization.Name;
            if (user.Position != null)
                model.Position = user.Position.Name;
            model.UserName = user.FullName;
            model.UserNumber = user.Code;
            model.UserEmail = user.Email;
        }

        /// <summary>
        /// Получить руководителей сотрудника по ветке
        /// </summary>
        /// <param name="user">Сотрудник, для которого требуется найти руководителей</param>
        /// <returns>Список руководителей</returns>
        public IList<User> GetManagersForEmployee(int userId, int? minManagerLevel = null)
        {
            IList<User> managers = new List<User>();

            User user = UserDao.Load(userId);
            User managerAccount = UserDao.GetManagerForEmployee(user.Login);

            IList<User> mainManagers;

            // Для руководителей-замов ближайшие руководители находится на том же уровне
            if (managerAccount != null && !managerAccount.IsMainManager)
            {
                mainManagers = DepartmentDao.GetDepartmentManagers(managerAccount.Department != null ? managerAccount.Department.Id : 0)
                    //.Where<User>(manager => manager.IsMainManager) //только руководители
                    .Where<User>(manager => manager.Email != user.Email) //руководители, заместители, специалисты, кроме самого пользователя, если он есть в числе начальства
                    .ToList<User>();

                foreach (var mainManager in mainManagers)
                {
                    managers.Add(mainManager);
                }
            }

            // Руководители вышележащих уровней для всех
            User currentUserOrManagerAccount = managerAccount ?? user;
            mainManagers = DepartmentDao.GetDepartmentManagers(currentUserOrManagerAccount.Department != null ? currentUserOrManagerAccount.Department.Id : 0, true)
                .Where<User>(manager => (currentUserOrManagerAccount.Department.ItemLevel ?? 0) > (manager.Department.ItemLevel ?? 0)
                && ((minManagerLevel != null && manager.Department.ItemLevel != null) ? manager.Department.ItemLevel >= minManagerLevel : true))
                .ToList<User>();

            foreach (var mainManager in mainManagers)
            {
                managers.Add(mainManager);
            }

            return managers;
        }

        protected bool IsAdditionalVacationTypeNecessary(VacationEditModel model)
        {
            IdNameDto currentVacationType = GetVacationTypes(false).Where(t => t.Id == model.VacationTypeId).FirstOrDefault();
            return currentVacationType != null && currentVacationType.Name.IndexOf("учебный") == -1;
        }
        protected List<IdNameDto> GetTimesheetStatusesForVacation()
        {
            List<IdNameDto> dtos = TimesheetStatusDao.LoadAllSorted().
                Where(x => (x.Id >= VacationFirstTimesheetStatisId) && (x.Id <= VacationLastTimesheetStatisId)).ToList().
                ConvertAll(x => new IdNameDto(x.Id, x.Name)).OrderBy(x => x.Name).ToList();
            if (AuthenticationService.CurrentUser.UserRole == UserRole.Employee)
                dtos.Insert(0, new IdNameDto(0, string.Empty));
            return dtos;
        }

        public int GetOtherRequestCountsForUserAndDates(DateTime beginDate,
            DateTime endDate, int userId, int vacationId, bool isChildVacantion)
        {
            return VacationDao.GetRequestCountsForUserAndDates(beginDate, endDate
                                                               , userId, vacationId, isChildVacantion);
        }

        public int GetOtherRequestCountsForUserAndDates(DateTime beginDate, DateTime endDate, int userId, int requestId, RequestTypeEnum requestType)
        {
            switch (requestType)
            {
                case RequestTypeEnum.Sicklist:
                    return SicklistDao.GetRequestCountsForUserAndDates(beginDate, endDate, userId, requestId);
                default:
                    return -1;
            }
        }

        public bool ResetVacationApprovals(int id, out string error)
        {
            error = String.Empty;

            Vacation vacation = VacationDao.Load(id);

            if (vacation != null && SendEmailForVacationError(vacation) && vacationDao.ResetApprovals(id))
            {
                return true;
            }
            else
            {
                error = "Error updating comment";
                return false;
            }
        }

        #endregion
        #region VacationReturn
        public List<VacationReturnDto> GetDocuments(VacationReturnListModel model)
        {
            var currentuser = UserDao.Load(CurrentUser.Id);
            Expression<Func<VacationReturn,bool>> query = QueryCreator.Create<VacationReturn, VacationReturnListModel>(model,currentuser, CurrentUser.UserRole);
            int npp = 1;
            var result = VacationReturnDao.QueryExpression(query).Select(x => new VacationReturnDto 
            { 
                NPP = npp++,
                Id= x.Id,
                CreateDate = x.CreateDate,
                Manager = x.Manager.Name,
                UserName = x.User.Name,
                Dep3Name = x.User.Department!=null?(x.User.Department.Dep3!=null && x.User.Department.Dep3.Any())?x.User.Department.Dep3.First().Name:"":"",
                Dep7Name = x.User.Department!=null? x.User.Department.Name:"",
                Position = x.User.Position!=null?x.User.Position.Name:"",
                ReturnType = x.ReturnType.Name,
                Vacation = x.Vacation!=null?"Отпуск №"+x.Vacation.Number: x.ChildVacation!=null? "Отпуск по уходу за ребенком №"+x.ChildVacation.Number:"",
                ReturnDate = x.ReturnDate.Value,
                ContinueDate = x.ContinueDate.Value,
                Type = x.ReturnType.Name,
                Status = x.Status.Name
            }).ToList(); 
            return result;
        }
        public VacationReturnListModel GetVacationReturnListModel()
        {
            VacationReturnListModel result = new VacationReturnListModel();
            result.IsCreateAvailable = (CurrentUser.UserRole & UserRole.Manager) > 0;
            result.Statuses = RefVacationReturnStatusDao.LoadAll().Select(x => new IdNameDto { Id = x.Id, Name = x.Name }).ToList();
            result.Statuses.Add(new IdNameDto { Id = 0, Name = "Все" });
            result.Statuses.Add(new IdNameDto { Id = 1, Name = "Не одобрено руководителем" });
            result.Statuses.Add(new IdNameDto { Id = 2, Name = "Не одобрено вышестоящим руководителем" });
            result.Statuses.Add(new IdNameDto { Id = 3, Name = "Не одобрено кадровиком" });
            result.BeginDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            
            return result;
        }
        
        public VacationReturnCreateViewModel GetCreateModel()
        {
            VacationReturnCreateViewModel model = new VacationReturnCreateViewModel();
            var user= UserDao.Load(CurrentUser.Id);
            var users = UserDao.GetUsersForManager(CurrentUser.Id, UserRole.Manager, user.Department.Id).Select(x=>x.Id).ToList();
            //Отпуска        

            model.Users = VacationDao.QueryExpression(GetVacationSearchExpression<Vacation>(users)).Select(x=>new IdNameDto{ Id = x.User.Id, Name=x.User.Name + " Отпуск №"+x.Number}).ToList();
            //Отпуск по уходу за ребенком

            model.Users.AddRange(ChildVacationDao.QueryExpression(GetVacationSearchExpression<ChildVacation>(users)).Select(x => new IdNameDto { Id = x.User.Id, Name = x.User.Name + " Отпуск по уходу за ребенком №" + x.Number }).ToList());
            return model;
        }
        private void SetFlagState(VacationReturnViewModel model,VacationReturn entity)
        {
            model.IsEditable = false;
            model.Manager.IsEditable = false;
            model.Chief.IsEditable = false;
            model.PersonnelManager.IsEditable = false;
            model.IsAdminEditable = false;
            model.ScanAddAvailable = false;
            switch (CurrentUser.UserRole)
            {
                case UserRole.ConsultantOutsourcing:
                    model.IsEditable = true;
                    model.IsAdminEditable = true;
                    model.Manager.IsEditable = true;
                    model.Chief.IsEditable = true;
                    model.PersonnelManager.IsEditable = true;
                    model.IsCancelAvailable = true;
                    model.ScanAddAvailable = true;
                    break;
                case UserRole.OutsourcingManager:
                    model.IsEditable = false;
                    break;
                case UserRole.Employee:
                    model.IsEditable = false;
                    break;
                case UserRole.Manager:
                    model.IsEditable = true && model.StatusId == 1;
                    model.Manager.IsEditable = true && model.StatusId == 1 && CheckIsChief(model.User.Id,CurrentUser.Id);
                    if(entity != null && entity.Manager!=null)
                        model.Chief.IsEditable = true && model.StatusId == 2 && CheckIsChief(entity.Manager.Id,CurrentUser.Id);
                    model.ScanAddAvailable = true && model.StatusId == 1;
                    model.IsCancelAvailable = true;
                    break;
                case UserRole.PersonnelManager:
                    model.PersonnelManager.IsEditable = true && model.StatusId==3;
                    model.IsEditable = true && model.StatusId==3;
                    model.ScanAddAvailable = true && model.StatusId ==3;
                    model.IsCancelAvailable = true && model.StatusId <4;
                    break;
                default:
                    throw new Exception("Нет доступа к заявке.");
            }           
            
            model.IsSaveAvailable = model.IsEditable || model.Manager.IsEditable || model.Chief.IsEditable || model.PersonnelManager.IsEditable ;
        }
        private void LoadDictionaries(VacationReturnViewModel model)
        {
            model.ReturnTypes = RefVacationReturnTypesDao.FindAll().Select(x => new IdNameDto { Id = x.Id, Name = x.Name }).ToList();
            model.Statuses = RefVacationReturnStatusDao.FindAll().Select(x => new IdNameDto { Id = x.Id, Name = x.Name }).ToList();
        }
        private void SetModel(VacationReturnViewModel model, VacationReturn entity)
        {
            if (entity != null)
            {
                model.Id = entity.Id;
                model.Number = entity.Id.ToString();
                model.CreateDate = entity.CreateDate;
                model.User.Id = entity.User.Id;
                model.Creator.Id = entity.Creator.Id;
                model.ReturnType = entity.ReturnType.Id;
                model.StatusId = entity.Status.Id;
                model.Status = entity.Status.Name;
                model.ReturnDate = entity.ReturnDate;
                model.ContinueDate = entity.ContinueDate;
                model.ReturnReason = entity.ReturnReason;
                model.VacationStartDate = entity.Vacation != null ? entity.Vacation.BeginDate : entity.ChildVacation.BeginDate;
                model.VacationEndDate = entity.Vacation != null ? entity.Vacation.EndDate : entity.ChildVacation.EndDate;
                model.DaysCount = entity.DaysNotUsedCount;
                if (entity.ManagerDateAccept.HasValue)
                {
                    model.Manager.IsChecked = true;
                    model.Manager.CheckDate = entity.ManagerDateAccept.Value;
                    model.Manager.Name = entity.Manager != null ? entity.Manager.Name : "";                    
                }
                if (entity.ChiefDateAccept.HasValue)
                {
                    model.Chief.IsChecked = true;
                    model.Chief.CheckDate = entity.ChiefDateAccept.Value;
                    model.Chief.Name = entity.Chief != null ? entity.Chief.Name : "";
                }
                if (entity.PersonnelManagerDateAccept.HasValue)
                {
                    model.PersonnelManager.IsChecked = true;
                    model.PersonnelManager.CheckDate = entity.PersonnelManagerDateAccept.Value;
                    model.PersonnelManager.Name = entity.PersonnelManager != null ? entity.PersonnelManager.Name : "";
                }
                var attach = RequestAttachmentDao.FindByRequestIdAndTypeId(entity.Id, RequestAttachmentTypeEnum.VacationReturn);
                if (attach != null)
                {
                    model.FileName = attach.FileName;
                    model.FileId = attach.Id;
                    model.IsScanVisible = true;
                    model.ScanAddAvailable = false;
                }

            }
            
            LoadUserData(model.Creator);
            LoadUserData(model.User);
            LoadDictionaries(model);
            SetFlagState(model,entity);
        }
        private void SetEntityStatus(VacationReturn entity)
        {
            entity.Status = RefVacationReturnStatusDao.Load(1);//Черновик
            if(entity.ManagerDateAccept.HasValue)
                entity.Status = RefVacationReturnStatusDao.Load(2);//Согласовано руководителем
            if (entity.ManagerDateAccept.HasValue && entity.ChiefDateAccept.HasValue)
                entity.Status = RefVacationReturnStatusDao.Load(3);//Согласовано вышестоящим руководителем
            if (entity.ManagerDateAccept.HasValue && entity.ChiefDateAccept.HasValue && entity.PersonnelManagerDateAccept.HasValue)
                entity.Status = RefVacationReturnStatusDao.Load(4);//Согласовано вышестоящим руководителем
        }
        private Expression<Func<T, bool>> GetVacationSearchExpression<T>(List<int> users)
        {
            ParameterExpression param = ParameterExpression.Parameter(typeof(T),"x");
            var now = ConstantExpression.Constant(DateTime.Now);
            var begin = param.GetProperty("BeginDate");
            var end = param.GetProperty("EndDate");
            var deletedate = param.GetProperty("DeleteDate");
            var user = param.GetProperty("User.Id");
            var searcher = Expression.And(Expression.LessThanOrEqual(begin,now),Expression.GreaterThanOrEqual(end,now));
            searcher = Expression.And(searcher, Expression.Equal(deletedate, Expression.Constant(null)));
            Expression sub = Expression.Constant(false);
            foreach (var u in users)
            {
                ConstantExpression cu = Expression.Constant(u);
                sub = Expression.Or(sub, Expression.Equal(user, cu));
            }
            searcher = Expression.And(searcher,sub);
            return Expression.Lambda<Func<T,bool>>(searcher,param) ;
        }
        public VacationReturnViewModel GetNewVacationReturnViewModel(int UserId)
        {
            VacationReturnViewModel model = new VacationReturnViewModel();
            model.Creator.Id = CurrentUser.Id;
            model.User.Id = UserId;
            model.StatusId = 1;
            var childvacations = ChildVacationDao.QueryExpression(GetVacationSearchExpression<ChildVacation>(new List<int> { UserId }));
            var vacations =VacationDao.QueryExpression(GetVacationSearchExpression<Vacation>(new List<int> { UserId }));
            vacations.NotNullAndAny()
                .OnSuccess((x) =>
                    {
                        var vac = vacations.First();
                        model.VacationStartDate = vac.BeginDate;
                        model.VacationEndDate = vac.EndDate;
                    })
                .OnError((x) =>
                    {
                        childvacations.NotNullAndAny()
                            .OnSuccess(y =>
                                {
                                    var cvac = childvacations.First();
                                    model.VacationStartDate = cvac.BeginDate;
                                    model.VacationEndDate = cvac.EndDate;
                                });
                    }
                );
            
            SetModel(model, null);
            return model;
        }
        public Result<VacationReturnViewModel> GetVacationReturnEditModel(int id)
        {
            Result<VacationReturnViewModel> result;
            VacationReturnViewModel model = new VacationReturnViewModel();
            if (id == 0)
            {
                result = new Result<VacationReturnViewModel>(false, "Не корректно указан идентификатор", null);
            }
            else
            {
                var entity = VacationReturnDao.Load(id);
                SetModel(model, entity);
                result = new Result<VacationReturnViewModel>(true, "Ok", model);
            }
            return result;
        }
        private void ChangeEntityProperties(VacationReturn entity, VacationReturnViewModel model)
        {
            //Сохраняем поля заявки            
            entity.ReturnType = RefVacationReturnTypesDao.Load(model.ReturnType);
            entity.ReturnReason = model.ReturnReason;
            entity.ReturnDate = model.ReturnDate;
            entity.ContinueDate = model.ContinueDate;
            TimeSpan vacationdays = (entity.VacationEndDate.Value - entity.VacationStartDate.Value);
            TimeSpan returndays = (entity.ReturnDate.Value - entity.ContinueDate.Value);
            entity.DaysNotUsedCount = vacationdays.Days - returndays.Days;
        }
        public Result<VacationReturnViewModel> SaveVacationReturnEditModel(VacationReturnViewModel model)
        {
            if ((CurrentUser.UserRole & (UserRole.Manager | UserRole.PersonnelManager | UserRole.ConsultantOutsourcing))==0)
                return new Result<VacationReturnViewModel>(false, "Нет прав для редактирования заявки.", null);
            VacationReturn entity;
            VacationReturnViewModel oldmodel = null;
            if (model.Id == 0)
            {
                entity = new VacationReturn();
                entity.CreateDate = DateTime.Now;
                var childvacations = ChildVacationDao.QueryExpression(GetVacationSearchExpression<ChildVacation>(new List<int> { model.User.Id }));
                var vacations = VacationDao.QueryExpression(GetVacationSearchExpression<Vacation>(new List<int> { model.User.Id }));
                vacations.NotNullAndAny()
                    .OnSuccess((x) =>
                    {
                        entity.Vacation = vacations.First();
                    })
                    .OnError((x) =>
                    {
                        childvacations.NotNullAndAny()
                            .OnSuccess(y =>
                            {
                                entity.ChildVacation = childvacations.First();
                            });
                    }
                    );
                if(entity.Vacation == null && entity.ChildVacation == null)
                    return new Result<VacationReturnViewModel>(false, "При сохранении заявки произошла ошибка: для сотрудника не найден отпуск.", null);
                entity.Creator = UserDao.Load(CurrentUser.Id);
                entity.User = UserDao.Load(model.User.Id);
                entity.VacationStartDate = entity.Vacation!=null?entity.Vacation.BeginDate:entity.ChildVacation.BeginDate;
                entity.VacationEndDate = entity.Vacation != null ? entity.Vacation.EndDate : entity.ChildVacation.EndDate;
            }
            else
            {
                entity = VacationReturnDao.Load(model.Id);
                if (entity == null) return new Result<VacationReturnViewModel>(false, "При сохранении заявки произошла ошибка: заявка не найдена в БД.", null);
                
                GetVacationReturnEditModel(model.Id).OnSuccess(x => oldmodel = x.Value);
                if (oldmodel != null && (!oldmodel.IsEditable && !oldmodel.PersonnelManager.IsEditable && !oldmodel.Manager.IsEditable && !oldmodel.Chief.IsEditable)) return new Result<VacationReturnViewModel>(false, "Редактирование заявки недоступно.", null);                
            }            
            switch (CurrentUser.UserRole)
            {
                case UserRole.Manager:
                    if (entity.Manager != null && entity.Manager.Id!=CurrentUser.Id && entity.Chief == null)
                    {
                        if (!CheckIsChief(entity.Manager.Id, CurrentUser.Id)) return new Result<VacationReturnViewModel>(false, "Нет прав для сохранения заявки.", null);
                        if(model.Chief.IsChecked)
                        {
                            entity.Chief = UserDao.Load(CurrentUser.Id);
                            entity.ChiefDateAccept = DateTime.Now;
                        }
                    }
                    if (entity.Manager == null)
                    {                        
                        if (!CheckIsChief(entity.User.Id,CurrentUser.Id)) return new Result<VacationReturnViewModel>(false, "Нет прав для сохранения заявки.", null);
                        if (model.Manager.IsChecked)
                        {
                            entity.Manager = UserDao.Load(CurrentUser.Id);
                            entity.ManagerDateAccept = DateTime.Now;
                        }
                    }
                    if ((entity.Manager != null && entity.Manager.Id == CurrentUser.Id) || entity.Creator.Id == CurrentUser.Id)
                    {
                        ChangeEntityProperties(entity, model);
                    }

                    break;
                case UserRole.ConsultantOutsourcing:
                    ChangeEntityProperties(entity, model);
                    if (entity.ManagerDateAccept.HasValue != model.Manager.IsChecked)
                    {
                        entity.ManagerDateAccept = model.Manager.IsChecked ? DateTime.Now : new DateTime?();
                        entity.Manager = model.Manager.IsChecked ?UserDao.Load(CurrentUser.Id):null;
                    }
                    if (entity.ChiefDateAccept.HasValue != model.Chief.IsChecked)
                    {
                        entity.ChiefDateAccept = model.Chief.IsChecked ? DateTime.Now : new DateTime?();
                        entity.Chief = model.Chief.IsChecked ? UserDao.Load(CurrentUser.Id):null;
                    }
                    if (entity.PersonnelManagerDateAccept.HasValue != model.PersonnelManager.IsChecked)
                    {
                        entity.PersonnelManagerDateAccept = model.PersonnelManager.IsChecked ? DateTime.Now : new DateTime?();
                        entity.PersonnelManager = model.PersonnelManager.IsChecked ? UserDao.Load(CurrentUser.Id) : null;
                    }
                    break;
                case UserRole.PersonnelManager:
                    if (!entity.User.Personnels.Any(x => x.Id == CurrentUser.Id)) return new Result<VacationReturnViewModel>(false, "Нет прав для сохранения заявки.", null);
                    ChangeEntityProperties(entity, model);
                    if (entity.PersonnelManager == null && model.PersonnelManager.IsChecked)
                    {
                        entity.PersonnelManager = UserDao.Load(CurrentUser.Id);
                        entity.PersonnelManagerDateAccept = DateTime.Now;
                    }
                    
                    break;
            }
            SetEntityStatus(entity);
            VacationReturnDao.SaveAndFlush(entity);
            if (model.FileDto != null)
            {
                string filename;
                var attach = RequestAttachmentDao.FindByRequestIdAndTypeId(entity.Id, RequestAttachmentTypeEnum.VacationReturn);
                if (attach != null)
                    RequestAttachmentDao.DeleteAndFlush(attach);
                SaveAttachment(entity.Id, 0, model.FileDto, RequestAttachmentTypeEnum.VacationReturn,out filename);
            }
            SetModel(model, entity);
            return new Result<VacationReturnViewModel>(true,"Заявка успешно сохраннена",model);
        }
        private bool CheckIsChief(int ManagerId,int chiefId)
        {
            var chiefs = GetChiefsForManager(ManagerId);
            return chiefs.Any(x => x.Id == chiefId);
        }
        #endregion

        #region Child Vacation
        public ChildVacationListModel GetChildVacationListModel()
        {
            User user = UserDao.Load(AuthenticationService.CurrentUser.Id);
            IdNameReadonlyDto dep = GetDepartmentDto(user);
            ChildVacationListModel model = new ChildVacationListModel
            {
                UserId = AuthenticationService.CurrentUser.Id,
                //Department = GetDepartmentDto(user),
                DepartmentName = dep.Name,
                DepartmentId = dep.Id,
                DepartmentReadOnly = dep.IsReadOnly,
                //VacationTypes = GetVacationTypes(true),
                RequestStatuses = GetRequestStatuses(CurrentUser.UserRole),
                Positions = GetPositions(user)
            };
            SetFlagsState(user, model);
            SetInitialDates(model);
            return model;
        }
        public void SetChildVacationListModel(ChildVacationListModel model, bool hasError)
        {
            User user = UserDao.Load(AuthenticationService.CurrentUser.Id);
            //model.Departments = GetDepartments(user);
            model.RequestStatuses = GetRequestStatuses(CurrentUser.UserRole);
            model.Positions = GetPositions(user);
            //model.VacationTypes = GetVacationTypes(true);
            if (hasError)
                model.Documents = new List<VacationDto>();
            else
            {
                if (model.Documents != null && model.IsOriginalReceivedModified && ((user.UserRole & UserRole.PersonnelManager) == UserRole.PersonnelManager))
                {
                    model.IsOriginalReceivedModified = false;
                    List<int> idsToApplyReceivedOriginals = model.Documents.Where(x => x.IsOriginalReceived).Select(x => x.Id).ToList();
                    ApplyReceivedOriginals(model, idsToApplyReceivedOriginals);
                }

                SetDocumentsToModel(model, user);
            }
            SetFlagsState(user, model);
        }

        protected void ApplyReceivedOriginals(ChildVacationListModel model, List<int> idsToApplyReceivedOriginals)
        {
            List<ChildVacation> entities = ChildVacationDao.LoadForIdsList(idsToApplyReceivedOriginals).ToList();
            foreach (ChildVacation entity in entities)
            {
                entity.IsOriginalReceived = true;
                ChildVacationDao.SaveAndFlush(entity);
            }
        }

        protected void SetFlagsState(User user, ChildVacationListModel model)
        {
            if ((user.UserRole & UserRole.PersonnelManager) == UserRole.PersonnelManager)
            {
                model.IsOriginalReceivedEditable = true;
            }
        }

        public void SetDocumentsToModel(ChildVacationListModel model, User user)
        {
            UserRole role = (UserRole)(user.RoleId & (int)CurrentUser.UserRole);
            /*Department dep = null;
            if(model.Department.Id != 0)
             dep = DepartmentDao.SearchByNameDistinct(model.Department.Name);*/
            model.Documents = ChildVacationDao.GetDocuments(user.Id,
                role,
                model.DepartmentId,
                //GetDepartmentId(model.Department),
                model.PositionId,
                0,
                //0,
                model.RequestStatusId,
                model.BeginDate,
                model.EndDate,
                model.UserName,
                model.SortBy,
                model.SortDescending,
                model.Number);
        }
        public ChildVacationEditModel GetChildVacationEditModel(int id, int userId)
        {
            ChildVacationEditModel model = new ChildVacationEditModel { Id = id, UserId = userId };
            User user = UserDao.Load(userId);
            IUser current = AuthenticationService.CurrentUser;
            if (!CheckUserRights(user, current, id, false))
                throw new ArgumentException("Доступ запрещен.");
            SetUserInfoModel(user, model);
            SetAttachmentToModel(model, id, RequestAttachmentTypeEnum.ChildVacation);
            SetOrderScanAttachmentToModel(model, id, RequestAttachmentTypeEnum.ChildVacationOrderScan);
            model.CommentsModel = GetCommentsModel(id, (int)RequestTypeEnum.ChildVacation);
            //model.TimesheetStatuses = GetTimesheetStatusesForVacation();
            //model.VacationTypes = GetVacationTypes(false);
            ChildVacation vacation = null;
            if (id == 0)
            {
                model.CreatorLogin = current.Name;
                model.Version = 0;
                model.DateCreated = DateTime.Today.ToShortDateString();
            }
            else
            {
                vacation = ChildVacationDao.Load(id);
                if (vacation == null)
                    throw new ArgumentException(string.Format("Заявка на отпуск (id {0}) не найдена в базе данных.", id));
                model.Version = vacation.Version;
                //model.VacationTypeId = vacation.Type.Id;
                //model.VacationTypeIdHidden = model.VacationTypeId;
                model.BeginDate = vacation.BeginDate;//new DateTimeDto(vacation.BeginDate);//
                model.EndDate = vacation.EndDate;
                model.ChildrenCount = vacation.ChildrenCount.HasValue ? vacation.ChildrenCount.ToString() : string.Empty;
                model.PaidToDate = vacation.PaidToDate;
                model.PaidToDate1 = vacation.PaidToDate1;
                model.IsFirstChild = vacation.IsFirstChild;

                model.IsFreeRate = vacation.FreeRate;

                model.IsPreviousPaymentCounted = vacation.IsPreviousPaymentCounted;
                //model.IsPreviousPaymentCountedHidden = vacation.IsPreviousPaymentCounted;
                //model.TimesheetStatusId = vacation.TimesheetStatus == null ? 0 : vacation.TimesheetStatus.Id;
                //model.TimesheetStatusIdHidden = model.TimesheetStatusId;
                //model.DaysCount = vacation.DaysCount;
                //model.DaysCountHidden = model.DaysCount;
                model.CreatorLogin = vacation.Creator.Name;
                model.DocumentNumber = vacation.Number.ToString();
                model.DateCreated = vacation.CreateDate.ToShortDateString();
                if (vacation.DeleteDate.HasValue)
                    model.IsDeleted = true;
                SetHiddenFields(model);
            }
            SetFlagsState(id, user, vacation, model);
            return model;
        }
        protected void SetHiddenFields(ChildVacationEditModel model)
        {
            model.IsFirstChildHidden = model.IsFirstChild;
            model.IsFreeRateHidden = model.IsFreeRate;
            model.IsPreviousPaymentCountedHidden = model.IsPreviousPaymentCounted;
        }
        protected void SetFlagsState(int id, User user, ChildVacation vacation, ChildVacationEditModel model)
        {
            SetFlagsState(model, false);
            UserRole currentUserRole = AuthenticationService.CurrentUser.UserRole;
            model.IsPyrusLinkAvailable = true;
            if (id == 0)
            {
                model.IsSaveAvailable = true;
                model.IsVacationDatesEditable = true;
                model.IsApprovedEnable = false;
                switch (currentUserRole)
                {
                    case UserRole.Employee:
                        model.IsPyrusLinkAvailable = false;
                    //model.IsApprovedByUserEnable = false;
                        break;
                    case UserRole.Manager:
                        
                        //model.IsApprovedByManagerEnable = false;
                        //model.IsTimesheetStatusEditable = true;
                        break;
                    case UserRole.PersonnelManager:
                        //model.IsApprovedByPersonnelManagerEnable = false;
                        model.IsPersonnelFieldsEditable = true;
                        break;
                }
                if ((currentUserRole & UserRole.PersonnelManager) == UserRole.PersonnelManager || (currentUserRole & UserRole.Manager) == UserRole.Manager)
                {
                    model.IsApprovedByUserEnable = false;
                    model.IsApprovedByUserHidden = model.IsApprovedByUser = true;
                }
                return;
            }

            model.IsApprovedByUserHidden = model.IsApprovedByUser = vacation.UserDateAccept.HasValue;
            model.IsApprovedByManagerHidden = model.IsApprovedByManager = vacation.ManagerDateAccept.HasValue;
            model.IsApprovedByPersonnelManagerHidden =
                model.IsApprovedByPersonnelManager = vacation.PersonnelManagerDateAccept.HasValue;
            model.IsPostedTo1CHidden = model.IsPostedTo1C = vacation.SendTo1C.HasValue;

            // hack to uncheck checkbox on UI
            if (vacation.DeleteDate.HasValue && !vacation.DeleteAfterSendTo1C)
                model.IsApprovedByPersonnelManager = false;

            RequestPrintForm form = RequestPrintFormDao.FindByRequestAndTypeId(id, RequestPrintFormTypeEnum.ChildVacation);
            model.IsPrintAvailable = form != null;

            switch (currentUserRole)
            {
                case UserRole.Employee:
                    model.IsPyrusLinkAvailable = false;
                    if (!vacation.UserDateAccept.HasValue && !vacation.DeleteDate.HasValue)
                    {
                        if (model.AttachmentId > 0)
                            model.IsApprovedEnable = true;
                        if (!vacation.ManagerDateAccept.HasValue && !vacation.PersonnelManagerDateAccept.HasValue && !vacation.SendTo1C.HasValue)
                            model.IsVacationDatesEditable = true;
                    }
                    break;
                case UserRole.Manager:
                    model.IsPyrusLinkAvailable = true;
                    if (!vacation.ManagerDateAccept.HasValue && !vacation.DeleteDate.HasValue)
                    {
                        if (model.AttachmentId > 0)
                            model.IsApprovedEnable = true;
                        if (!vacation.PersonnelManagerDateAccept.HasValue && !vacation.SendTo1C.HasValue)
                        {
                            model.IsVacationDatesEditable = true;
                            //model.IsTimesheetStatusEditable = true;
                        }
                    }
                    break;
                case UserRole.PersonnelManager:
                    if (model.IsPostedTo1C && model.OrderScanAttachmentId <= 0)
                    {
                        model.IsConfirmationAllowed = true;
                    }
                    if (!vacation.PersonnelManagerDateAccept.HasValue)
                    {
                        if (model.AttachmentId > 0)
                        {
                            model.IsApprovedEnable = true;
                            model.IsApprovedForAllEnable = true;
                        }
                        if (!vacation.SendTo1C.HasValue)
                        {
                            model.IsPersonnelFieldsEditable = true;
                            model.IsVacationDatesEditable = true;
                            //model.IsTimesheetStatusEditable = true;
                        }
                    }
                    else if (!vacation.SendTo1C.HasValue &&
                             !vacation.DeleteDate.HasValue)
                        model.IsDeleteAvailable = true;

                    break;
                case UserRole.ConsultantOutsourcing:
                    if ((!vacation.UserDateAccept.HasValue && !vacation.DeleteDate.HasValue) || (!vacation.ManagerDateAccept.HasValue && !vacation.DeleteDate.HasValue) || !vacation.PersonnelManagerDateAccept.HasValue)
                    {
                        model.IsApproveForAllByConsultantEnable = true;

                        //условия кадровика
                        if (model.IsPostedTo1C && model.OrderScanAttachmentId <= 0)
                        {
                            model.IsConfirmationAllowed = true;
                        }
                        if (!vacation.PersonnelManagerDateAccept.HasValue)
                        {
                            if (model.AttachmentId > 0)
                            {
                                model.IsApprovedEnable = true;
                                model.IsApprovedForAllEnable = true;
                            }
                            if (!vacation.SendTo1C.HasValue)
                            {
                                model.IsPersonnelFieldsEditable = true;
                                model.IsVacationDatesEditable = true;
                                //model.IsTimesheetStatusEditable = true;
                            }
                        }
                        else if (!vacation.SendTo1C.HasValue &&
                                 !vacation.DeleteDate.HasValue)
                            model.IsDeleteAvailable = true;
                    }
                    break;
                case UserRole.Estimator:
                case UserRole.OutsourcingManager:
                    if (vacation.SendTo1C.HasValue && !vacation.DeleteDate.HasValue)
                        model.IsDeleteAvailable = true;
                    break;
            }
            model.IsSaveAvailable = model.IsVacationDatesEditable || model.IsPersonnelFieldsEditable;
            //|| model.IsApprovedByManagerEnable || model.IsApprovedByUserEnable ||
            //model.IsApprovedByPersonnelManagerEnable;
        }
        protected void SetFlagsState(ChildVacationEditModel model, bool state)
        {
            model.IsApprovedByManager = state;
            model.IsApprovedByManagerHidden = state;
            model.IsApprovedByManagerEnable = state;

            model.IsApprovedByPersonnelManager = state;
            model.IsApprovedByPersonnelManagerHidden = state;
            model.IsApprovedByPersonnelManagerEnable = state;

            model.IsApprovedByUser = state;
            model.IsApprovedByUserHidden = state;
            model.IsApprovedByUserEnable = state;

            model.IsPostedTo1C = state;
            model.IsPostedTo1CHidden = state;
            model.IsPostedTo1CEnable = state;

            model.IsSaveAvailable = state;
            //model.IsTimesheetStatusEditable = state;
            model.IsVacationDatesEditable = state;
            model.IsPersonnelFieldsEditable = state;

            model.IsDelete = state;
            model.IsDeleteAvailable = state;
            model.IsPrintAvailable = state;

            model.IsApproved = state;
            model.IsApprovedEnable = state;

            model.IsApprovedForAll = state;
            model.IsApprovedForAllEnable = state;
        }
        public void ReloadDictionariesToModel(ChildVacationEditModel model)
        {
            User user = UserDao.Load(model.UserId);
            IUser current = AuthenticationService.CurrentUser;
            SetUserInfoModel(user, model);
            model.CommentsModel = GetCommentsModel(model.Id, (int)RequestTypeEnum.ChildVacation);
            //model.TimesheetStatuses = GetTimesheetStatusesForVacation();
            //model.VacationTypes = GetVacationTypes(false);
            if (model.Id == 0)
            {
                model.CreatorLogin = current.Name;
                model.DateCreated = DateTime.Today.ToShortDateString();
            }
            else
            {
                ChildVacation childVacation = ChildVacationDao.Load(model.Id);
                model.CreatorLogin = childVacation.Creator.Name;
                model.DocumentNumber = childVacation.Number.ToString();
                model.DateCreated = childVacation.CreateDate.ToShortDateString();
                //model.DaysCount = vacation.DaysCount;
                //model.DaysCountHidden = model.DaysCount;
                if (childVacation.DeleteDate.HasValue)
                    model.IsDeleted = true;
            }
        }

        public bool SaveChildVacationEditModel(ChildVacationEditModel model, UploadFileDto fileDto, UploadFileDto orderScanFileDto, out string error)
        {
            error = string.Empty;
            User user = null;
            try
            {
                user = UserDao.Load(model.UserId);
                IUser current = AuthenticationService.CurrentUser;
                if (!CheckUserRights(user, current, model.Id, true) || !CheckUserRightsForEntity(user, current, model))
                {
                    error = "Редактирование заявки запрещено";
                    return false;
                }
                ChildVacation childVacation;

                if (model.Id == 0)
                #region Сохранение нового ОУЗР
                {
                    childVacation = new ChildVacation
                    {
                        CreateDate = DateTime.Now,
                        Creator = UserDao.Load(current.Id),
                        Number = RequestNextNumberDao.GetNextNumberForType((int)RequestTypeEnum.ChildVacation),
                        User = user,
                        TimesheetStatus = TimesheetStatusDao.GetStatusForShortName(ChildVacationTimesheetStatusShortName),
                    };
                    ChangeEntityProperties(current, childVacation, model, user);
                    ChildVacationDao.SaveAndFlush(childVacation);
                    model.Id = childVacation.Id;
                }
                #endregion
                else
                #region Сохранение существующего ОУЗР
                {
                    childVacation = ChildVacationDao.Load(model.Id);
                    if (childVacation.SendTo1C.HasValue)
                    {
                        error = "Редактирование заявки запрещено, так как она выгружена в 1С!";
                        return false;
                    }
                    string fileName;
                    int? attachmentId = SaveAttachment(childVacation.Id, model.AttachmentId, fileDto, RequestAttachmentTypeEnum.ChildVacation, out fileName);
                    if (attachmentId.HasValue)
                    {
                        model.AttachmentId = attachmentId.Value;
                        model.Attachment = fileName;
                    }
                    // ---------------------------------------
                    int? orderScanAttachmentId = SaveAttachment(childVacation.Id, model.OrderScanAttachmentId, orderScanFileDto, RequestAttachmentTypeEnum.ChildVacationOrderScan, out fileName);
                    if (orderScanAttachmentId.HasValue)
                    {
                        model.OrderScanAttachmentId = orderScanAttachmentId.Value;
                        model.OrderScanAttachment = fileName;
                    }
                    // ---------------------------------------
                    if (childVacation.Version != model.Version)
                    {
                        error = "Заявка была изменена другим пользователем.";
                        model.ReloadPage = true;
                        return false;
                    }
                    if (model.IsDelete)
                    {
                        if ((current.UserRole & UserRole.OutsourcingManager) == UserRole.OutsourcingManager || (current.UserRole & UserRole.Estimator) == UserRole.Estimator)
                            childVacation.DeleteAfterSendTo1C = true;
                        if (model.AttachmentId > 0)
                            RequestAttachmentDao.Delete(model.AttachmentId);
                        // ----------------------------
                        if (model.OrderScanAttachmentId > 0)
                            RequestAttachmentDao.Delete(model.OrderScanAttachmentId);
                        // ----------------------------
                        childVacation.DeleteDate = DateTime.Now;
                        childVacation.CreateDate = DateTime.Now;
                        ChildVacationDao.SaveAndFlush(childVacation);
                        SendEmailForUserRequest(childVacation.User, current, childVacation.Creator, true, childVacation.Id,
                            childVacation.Number, RequestTypeEnum.ChildVacation, false);
                        model.IsDelete = false;
                        model.AttachmentId = 0;
                        model.Attachment = string.Empty;
                        model.OrderScanAttachmentId = 0;
                        model.OrderScanAttachment = string.Empty;
                    }
                    else
                    {
                        ChangeEntityProperties(current, childVacation, model, user);
                        ChildVacationDao.SaveAndFlush(childVacation);
                        if (childVacation.Version != model.Version)
                        {
                            childVacation.CreateDate = DateTime.Now;
                            ChildVacationDao.SaveAndFlush(childVacation);
                        }
                    }
                    if (childVacation.DeleteDate.HasValue)
                        model.IsDeleted = true;
                }
                #endregion

                model.DocumentNumber = childVacation.Number.ToString();
                model.Version = childVacation.Version;
                //model.DaysCount = childVacation.DaysCount;
                model.CreatorLogin = childVacation.Creator.Name;
                model.DateCreated = childVacation.CreateDate.ToShortDateString();
                SetFlagsState(childVacation.Id, user, childVacation, model);
                //throw new ArgumentException("Test exception");
                return true;
            }
            catch (Exception ex)
            {
                ChildVacationDao.RollbackTran();
                Log.Error("Error on SaveChildVacationEditModel:", ex);
                error = string.Format("Исключение:{0}", ex.GetBaseException().Message);
                return false;
            }
            finally
            {
                SetUserInfoModel(user, model);
                LoadDictionaries(model);
                SetHiddenFields(model);
            }
        }
        protected void ChangeEntityProperties(IUser current, ChildVacation entity, ChildVacationEditModel model, User user)
        {
            #region Согласование сотрудником
            if ((current.UserRole & UserRole.Employee) == UserRole.Employee && current.Id == model.UserId
                    && !entity.UserDateAccept.HasValue
                    && model.IsApproved)
            {
                entity.UserDateAccept = DateTime.Now;
                //!!! need to send e-mail
                SendEmailForUserRequest(entity.User, current, entity.Creator, false, entity.Id,
                    entity.Number, RequestTypeEnum.ChildVacation, false);
            }
            #endregion

            #region Согласование руководителем
            bool canEdit = false;

            if (((current.UserRole & UserRole.Manager) == UserRole.Manager && IsCurrentManagerForUser(user, current, out canEdit))
                    || HasCurrentManualRoleForUser(user, current, UserManualRole.ApprovesCommonRequests, out canEdit))
            {
                if(!String.IsNullOrEmpty(model.PyrusNumber))
                    entity.PirusNumber = model.PyrusNumber;
                if (!entity.ManagerDateAccept.HasValue)
                {
                    if (model.IsApprovedByUser && !entity.UserDateAccept.HasValue)
                        entity.UserDateAccept = DateTime.Now;
                    if (model.IsApproved)
                    {
                        entity.ManagerDateAccept = DateTime.Now;
                        //!!! need to send e-mail
                        SendEmailForUserRequest(entity.User, current, entity.Creator, false, entity.Id,
                            entity.Number, RequestTypeEnum.ChildVacation, false);
                    }
                }
            }
            #endregion

            #region Согласование кадровиком
            int? superPersonnelId = ConfigurationService.SuperPersonnelId;
            if ((current.UserRole & UserRole.Estimator) == UserRole.Estimator ||
                (current.UserRole & UserRole.PersonnelManager) == UserRole.PersonnelManager
                && ((superPersonnelId.HasValue && CurrentUser.Id == superPersonnelId.Value) ||                    
                    (user.Personnels.Where(x => x.Id == current.Id).FirstOrDefault() != null))
                )
            {
                if (model.IsApprovedByUser && !entity.UserDateAccept.HasValue)
                    entity.UserDateAccept = DateTime.Now;
                if (!entity.PersonnelManagerDateAccept.HasValue)
                {
                    //entity.TimesheetStatus = TimesheetStatusDao.Load(model.TimesheetStatusId);
                    if (model.IsPersonnelFieldsEditable)
                        SetPersonnelDataFromModel(entity, model);
                    if (model.IsApproved)
                    {
                        entity.PersonnelManagerDateAccept = DateTime.Now;
                        SendEmailForUserRequest(entity.User, current, entity.Creator, false, entity.Id,
                            entity.Number, RequestTypeEnum.ChildVacation, false);
                    }
                    if (model.IsApprovedForAll && !entity.ManagerDateAccept.HasValue)
                        entity.ManagerDateAccept = DateTime.Now;
                }
            }
            #endregion

            #region Согласование консультантом за всех
            if ((current.UserRole & UserRole.ConsultantOutsourcing) == UserRole.ConsultantOutsourcing && model.IsApproveForAllByConsultant)
            {
                if (!entity.UserDateAccept.HasValue && model.IsApproveForAllByConsultant)
                {
                    entity.UserDateAccept = DateTime.Now;
                }
                if (!entity.ManagerDateAccept.HasValue && model.IsApproveForAllByConsultant)
                {
                    entity.ManagerDateAccept = DateTime.Now;
                }
                if (!entity.PersonnelManagerDateAccept.HasValue && model.IsApproveForAllByConsultant)
                {
                    if (model.IsPersonnelFieldsEditable)
                        SetPersonnelDataFromModel(entity, model);

                    entity.PersonnelManagerDateAccept = DateTime.Now;
                }
            }
            #endregion

            if (model.IsVacationDatesEditable)
            {
                // ReSharper disable PossibleInvalidOperationException
                entity.BeginDate = model.BeginDate.Value;
                entity.EndDate = model.EndDate.Value;
                // ReSharper restore PossibleInvalidOperationException
            }
        }
        protected void SetPersonnelDataFromModel(ChildVacation entity, ChildVacationEditModel model)
        {
            entity.FreeRate = model.IsFreeRate;
            entity.IsFirstChild = model.IsFirstChild;
            entity.IsPreviousPaymentCounted = model.IsPreviousPaymentCounted;
            entity.PaidToDate = model.PaidToDate;
            entity.PaidToDate1 = model.PaidToDate1;
            entity.ChildrenCount = string.IsNullOrEmpty(model.ChildrenCount)
                                       ? new int?()
                                       : Int32.Parse(model.ChildrenCount);
        }
        protected void LoadDictionaries(ChildVacationEditModel model)
        {
            model.CommentsModel = GetCommentsModel(model.Id, (int)RequestTypeEnum.ChildVacation);
        }

        #endregion

        #region AccessGroupsList
        public AccessGroupsListModel GetAccessGroupsListModel()
        {
            AccessGroupsListModel model = new AccessGroupsListModel();
            model.AccessGroups = AccessGroupDao.GetAccessGroups().ToList().ConvertAll(x => new SelectListItem { Value = x.Code, Text = x.Name }).OrderBy(x => x.Value);
            return model;
        }

        public AccessGroupsListModel SetAccessGroupsListModel(AccessGroupsListModel model)
        {
            Department dep = null;
            if (model.DepartmentId != 0)
                dep = DepartmentDao.Load(model.DepartmentId);
            var user=UserDao.Load(CurrentUser.Id);
            model.AccessGroups = AccessGroupDao.GetAccessGroups().ToList().ConvertAll(x => new SelectListItem { Value = x.Code, Text = x.Name }).OrderBy(x => x.Value);
            model.AccessGroupList = AccessGroupDao.GetAccessGroupList(user,dep, model.AccessGroupCode, model.UserName, model.Manager6, model.Manager5, model.Manager4, model.IsManagerShow, model.SortBy, model.SortDescending);
            return model;
        }
        #endregion

        #region Comments
        public RequestCommentsModel GetCommentsModel(int id, int typeId, string addCommentText = null, bool hasParent = false)
        {
            return SetCommentsModel(id, typeId, hasParent: hasParent);
        }
        protected RequestCommentsModel SetCommentsModel(int id, int typeId, bool hasParent = false)
        {
            RequestCommentsModel commentModel = new RequestCommentsModel
            {
                RequestId = id
                ,
                RequestTypeId = typeId
                ,
                HasParent = hasParent
                ,
                Comments = new List<RequestCommentModel>()
            };
            if (id > 0)
            {
                switch (typeId)
                {
                    case (int)RequestTypeEnum.Vacation:
                        Vacation vacation = VacationDao.Load(id);
                        if ((vacation.Comments != null) && (vacation.Comments.Count() > 0))
                        {
                            commentModel.Comments = vacation.Comments.OrderBy(x => x.DateCreated).ToList().
                                ConvertAll(x => new RequestCommentModel
                                {
                                    Comment = x.Comment,
                                    CreatedDate = x.DateCreated.ToString(),
                                    Creator = x.User.FullName,
                                });
                        }
                        break;
                    case (int)RequestTypeEnum.ChildVacation:
                        ChildVacation childVacation = ChildVacationDao.Load(id);
                        if ((childVacation.Comments != null) && (childVacation.Comments.Count() > 0))
                        {
                            commentModel.Comments = childVacation.Comments.OrderBy(x => x.DateCreated).ToList().
                                ConvertAll(x => new RequestCommentModel
                                {
                                    Comment = x.Comment,
                                    CreatedDate = x.DateCreated.ToString(),
                                    Creator = x.User.FullName,
                                });
                        }
                        break;
                    case (int)RequestTypeEnum.Absence:
                        Absence absence = AbsenceDao.Load(id);
                        if ((absence.Comments != null) && (absence.Comments.Count() > 0))
                        {
                            commentModel.Comments = absence.Comments.OrderBy(x => x.DateCreated).ToList().
                                ConvertAll(x => new RequestCommentModel
                                {
                                    Comment = x.Comment,
                                    CreatedDate = x.DateCreated.ToString(),
                                    Creator = x.User.FullName,
                                });
                        }
                        break;
                    case (int)RequestTypeEnum.Sicklist:
                        Sicklist sicklist = SicklistDao.Load(id);
                        if ((sicklist.Comments != null) && (sicklist.Comments.Count() > 0))
                        {
                            commentModel.Comments = sicklist.Comments.OrderBy(x => x.DateCreated).ToList().
                                ConvertAll(x => new RequestCommentModel
                                {
                                    Comment = x.Comment,
                                    CreatedDate = x.DateCreated.ToString(),
                                    Creator = x.User.FullName,
                                });
                        }
                        break;
                    case (int)RequestTypeEnum.HolidayWork:
                        HolidayWork holidayWork = HolidayWorkDao.Load(id);
                        if ((holidayWork.Comments != null) && (holidayWork.Comments.Count() > 0))
                        {
                            commentModel.Comments = holidayWork.Comments.OrderBy(x => x.DateCreated).ToList().
                                ConvertAll(x => new RequestCommentModel
                                {
                                    Comment = x.Comment,
                                    CreatedDate = x.DateCreated.ToString(),
                                    Creator = x.User.FullName,
                                });
                        }
                        break;
                    case (int)RequestTypeEnum.Mission:
                        Mission mission = MissionDao.Load(id);
                        if ((mission.Comments != null) && (mission.Comments.Count() > 0))
                        {
                            commentModel.Comments = mission.Comments.OrderBy(x => x.DateCreated).ToList().
                                ConvertAll(x => new RequestCommentModel
                                {
                                    Comment = x.Comment,
                                    CreatedDate = x.DateCreated.ToString(),
                                    Creator = x.User.FullName,
                                });
                        }
                        break;
                    case (int)RequestTypeEnum.Dismissal:
                        Dismissal dismissal = DismissalDao.Load(id);
                        if ((dismissal.Comments != null) && (dismissal.Comments.Count() > 0))
                        {
                            commentModel.Comments = dismissal.Comments.OrderBy(x => x.DateCreated).ToList().
                                ConvertAll(x => new RequestCommentModel
                                {
                                    Comment = x.Comment,
                                    CreatedDate = x.DateCreated.ToString(),
                                    Creator = x.User.FullName,
                                });
                        }
                        break;
                    case (int)RequestTypeEnum.ClearanceChecklist:
                        Dismissal clearanceChecklist = DismissalDao.Load(id);
                        if ((clearanceChecklist.ClearanceChecklistComments != null) && (clearanceChecklist.ClearanceChecklistComments.Count() > 0))
                        {
                            commentModel.Comments = clearanceChecklist.ClearanceChecklistComments.OrderBy(x => x.DateCreated).ToList().
                                ConvertAll(x => new RequestCommentModel
                                {
                                    Comment = x.Comment,
                                    CreatedDate = x.DateCreated.ToString(),
                                    Creator = x.User.FullName,
                                });
                        }
                        break;
                    case (int)RequestTypeEnum.TimesheetCorrection:
                        TimesheetCorrection timesheetCorrection = TimesheetCorrectionDao.Load(id);
                        if ((timesheetCorrection.Comments != null) && (timesheetCorrection.Comments.Count() > 0))
                        {
                            commentModel.Comments = timesheetCorrection.Comments.OrderBy(x => x.DateCreated).ToList().
                                ConvertAll(x => new RequestCommentModel
                                {
                                    Comment = x.Comment,
                                    CreatedDate = x.DateCreated.ToString(),
                                    Creator = x.User.FullName,
                                });
                        }
                        break;
                    case (int)RequestTypeEnum.Employment:
                        Employment employment = EmploymentDao.Load(id);
                        if ((employment.Comments != null) && (employment.Comments.Count() > 0))
                        {
                            commentModel.Comments = employment.Comments.OrderBy(x => x.DateCreated).ToList().
                                ConvertAll(x => new RequestCommentModel
                                {
                                    Comment = x.Comment,
                                    CreatedDate = x.DateCreated.ToString(),
                                    Creator = x.User.FullName,
                                });
                        }
                        break;
                    case (int)RequestTypeEnum.MissionOrder:
                        MissionOrder missionOrder = MissionOrderDao.Load(id);
                        if ((missionOrder.Comments != null) && (missionOrder.Comments.Count() > 0))
                        {
                            commentModel.Comments = missionOrder.Comments.OrderBy(x => x.DateCreated).ToList().
                                ConvertAll(x => new RequestCommentModel
                                {
                                    Comment = x.Comment,
                                    CreatedDate = x.DateCreated.ToString(),
                                    Creator = x.User.FullName,
                                });
                        }
                        break;
                    case (int)RequestTypeEnum.MissionReport:
                        MissionReport missionReport = MissionReportDao.Load(id);
                        if ((missionReport.Comments != null) && (missionReport.Comments.Count() > 0))
                        {
                            commentModel.Comments = missionReport.Comments.OrderBy(x => x.DateCreated).ToList().
                                ConvertAll(x => new RequestCommentModel
                                {
                                    Comment = x.Comment,
                                    CreatedDate = x.DateCreated.ToString(),
                                    Creator = x.User.FullName,
                                });
                        }
                        break;
                }
            }
            return commentModel;
        }
        public bool SaveComment(SaveCommentModel model)
        {
            try
            {
                int userId = AuthenticationService.CurrentUser.Id;
                User user;
                switch (model.TypeId)
                {
                    case (int)RequestTypeEnum.Vacation:
                        Vacation vacation = VacationDao.Load(model.DocumentId);
                        user = UserDao.Load(userId);
                        VacationComment comment = new VacationComment
                        {
                            Comment = model.Comment,
                            Vacation = vacation,
                            DateCreated = DateTime.Now,
                            User = user,
                        };
                        VacationCommentDao.MergeAndFlush(comment);
                        break;
                    case (int)RequestTypeEnum.ChildVacation:
                        ChildVacation childVacation = ChildVacationDao.Load(model.DocumentId);
                        user = UserDao.Load(userId);
                        ChildVacationComment childVacationComment = new ChildVacationComment
                        {
                            Comment = model.Comment,
                            ChildVacation = childVacation,
                            DateCreated = DateTime.Now,
                            User = user,
                        };
                        ChildVacationCommentDao.MergeAndFlush(childVacationComment);
                        break;
                    case (int)RequestTypeEnum.Absence:
                        Absence absence = AbsenceDao.Load(model.DocumentId);
                        user = UserDao.Load(userId);
                        AbsenceComment absenceComment = new AbsenceComment
                        {
                            Comment = model.Comment,
                            Absence = absence,
                            DateCreated = DateTime.Now,
                            User = user,
                        };
                        AbsenceCommentDao.MergeAndFlush(absenceComment);
                        break;
                    case (int)RequestTypeEnum.Sicklist:
                        Sicklist sicklist = SicklistDao.Load(model.DocumentId);
                        user = UserDao.Load(userId);
                        SicklistComment sicklistComment = new SicklistComment
                        {
                            Comment = model.Comment,
                            Sicklist = sicklist,
                            DateCreated = DateTime.Now,
                            User = user,
                        };
                        SicklistCommentDao.MergeAndFlush(sicklistComment);
                        break;
                    case (int)RequestTypeEnum.HolidayWork:
                        HolidayWork holidayWork = HolidayWorkDao.Load(model.DocumentId);
                        user = UserDao.Load(userId);
                        HolidayWorkComment holidayWorkComment = new HolidayWorkComment
                        {
                            Comment = model.Comment,
                            HolidayWork = holidayWork,
                            DateCreated = DateTime.Now,
                            User = user,
                        };
                        HolidayWorkCommentDao.MergeAndFlush(holidayWorkComment);
                        break;
                    case (int)RequestTypeEnum.Mission:
                        Mission mission = MissionDao.Load(model.DocumentId);
                        user = UserDao.Load(userId);
                        MissionComment missionComment = new MissionComment
                        {
                            Comment = model.Comment,
                            Mission = mission,
                            DateCreated = DateTime.Now,
                            User = user,
                        };
                        MissionCommentDao.MergeAndFlush(missionComment);
                        break;
                    case (int)RequestTypeEnum.Dismissal:
                        Dismissal dismissal = DismissalDao.Load(model.DocumentId);
                        user = UserDao.Load(userId);
                        DismissalComment dismissalComment = new DismissalComment
                        {
                            Comment = model.Comment,
                            Dismissal = dismissal,
                            DateCreated = DateTime.Now,
                            User = user,
                        };
                        DismissalCommentDao.MergeAndFlush(dismissalComment);
                        break;
                    case (int)RequestTypeEnum.ClearanceChecklist:
                        Dismissal clearanceChecklist = DismissalDao.Load(model.DocumentId);
                        user = UserDao.Load(userId);
                        ClearanceChecklistComment clearanceChecklistComment = new ClearanceChecklistComment
                        {
                            Comment = model.Comment,
                            ClearanceChecklist = clearanceChecklist,
                            DateCreated = DateTime.Now,
                            User = user,
                        };
                        ClearanceChecklistCommentDao.MergeAndFlush(clearanceChecklistComment);
                        break;
                    case (int)RequestTypeEnum.TimesheetCorrection:
                        TimesheetCorrection timesheetCorrection = TimesheetCorrectionDao.Load(model.DocumentId);
                        user = UserDao.Load(userId);
                        TimesheetCorrectionComment timesheetCorrectionComment = new TimesheetCorrectionComment
                        {
                            Comment = model.Comment,
                            TimesheetCorrection = timesheetCorrection,
                            DateCreated = DateTime.Now,
                            User = user,
                        };
                        TimesheetCorrectionCommentDao.MergeAndFlush(timesheetCorrectionComment);
                        break;
                    case (int)RequestTypeEnum.Employment:
                        Employment employment = EmploymentDao.Load(model.DocumentId);
                        user = UserDao.Load(userId);
                        EmploymentComment employmentComment = new EmploymentComment
                        {
                            Comment = model.Comment,
                            Employment = employment,
                            DateCreated = DateTime.Now,
                            User = user,
                        };
                        EmploymentCommentDao.MergeAndFlush(employmentComment);
                        /*SendEmailForUserRequest(employment.User, AuthenticationService.CurrentUser, employment.Id,
                                                employment.Number, RequestTypeEnum.Employment, true);*/
                        break;
                    case (int)RequestTypeEnum.MissionOrder:
                        MissionOrder missionOrder = MissionOrderDao.Load(model.DocumentId);
                        user = UserDao.Load(userId);
                        MissionOrderComment missionOrderComment = new MissionOrderComment
                        {
                            Comment = model.Comment,
                            MissionOrder = missionOrder,
                            DateCreated = DateTime.Now,
                            User = user,
                        };
                        MissionOrderCommentDao.MergeAndFlush(missionOrderComment);
                        break;
                    case (int)RequestTypeEnum.MissionReport:
                        MissionReport missionReport = MissionReportDao.Load(model.DocumentId);
                        user = UserDao.Load(userId);
                        MissionReportComment missionReportComment = new MissionReportComment
                        {
                            Comment = model.Comment,
                            MissionReport = missionReport,
                            DateCreated = DateTime.Now,
                            User = user,
                        };
                        MissionReportCommentDao.MergeAndFlush(missionReportComment);
                        break;
                }
                //doc.Comments.Add(comment);
                //DocumentDao.MergeAndFlush(doc);
                return true;
            }
            catch (Exception ex)
            {
                VacationDao.RollbackTran();
                Log.Error("Exception", ex);
                model.Error = "Исключение: " + ex.GetBaseException().Message;
                return false;
            }
        }
        #endregion

        #region Attachment
        public RequestAttachmentsModel GetAttachmentsModel(int id, RequestAttachmentTypeEnum typeId)
        {
            bool isAddAvailable = false;
            bool isDeleteAvailable = false;
            List<RequestAttachment> list = RequestAttachmentDao.FindManyByRequestIdAndTypeId(id, typeId).ToList();
            if (id > 0)
            {
                Employment entity = EmploymentDao.Load(id);
                isAddAvailable = !entity.SendTo1C.HasValue && !entity.DeleteDate.HasValue;
                isDeleteAvailable = !entity.SendTo1C.HasValue && !entity.DeleteDate.HasValue;
                if (isDeleteAvailable && list.Count <= 4)
                {
                    if ((entity.UserDateAccept.HasValue && (CurrentUser.UserRole & UserRole.Employee) == UserRole.Employee) ||
                       (entity.ManagerDateAccept.HasValue && (CurrentUser.UserRole & UserRole.Manager) == UserRole.Manager) ||
                       (entity.PersonnelManagerDateAccept.HasValue && (CurrentUser.UserRole & UserRole.PersonnelManager) == UserRole.PersonnelManager)
                      )
                        isDeleteAvailable = false;
                }
            }
            RequestAttachmentsModel model = new RequestAttachmentsModel
            {
                AttachmentRequestId = id,
                AttachmentRequestTypeId = (int)typeId,
                IsAddAvailable = isAddAvailable,
                Attachments = new List<RequestAttachmentModel>()
            };
            model.Attachments = list.ConvertAll(x =>
                            new RequestAttachmentModel
                            {
                                Attachment = x.FileName,
                                AttachmentId = x.Id,
                                Description = x.Description,
                                IsDeleteAvailable = (x.CreatorUserRole == CurrentUser.UserRole) && isDeleteAvailable,
                            });
            return model;
        }
        public AttachmentModel GetFileContext(int id/*,int typeId*/)
        {
            RequestAttachment attachment = RequestAttachmentDao.Load(id);
            return new AttachmentModel
            {
                Context = attachment.UncompressContext,
                FileName = attachment.FileName,
                ContextType = attachment.ContextType
            };
        }
        public bool SaveAttachment(SaveAttacmentModel model)
        {
            RequestAttachment attach = new RequestAttachment
            {
                ContextType = GetFileContext(model.FileDto.FileName),
                DateCreated = DateTime.Now,
                Description = model.Description,
                FileName = model.FileDto.FileName,
                RequestId = model.EntityId,
                RequestType = (int)model.EntityTypeId,
                UncompressContext = model.FileDto.Context,
                CreatorRole = RoleDao.Load((int)CurrentUser.UserRole)

            };
            RequestAttachmentDao.SaveAndFlush(attach);
            model.Id = attach.Id;
            return true;
        }
        public bool SaveUniqueAttachment(SaveAttacmentModel model)
        {
            List<RequestAttachment> existing =
                RequestAttachmentDao.FindManyByRequestIdAndTypeId(model.EntityId, model.EntityTypeId).ToList();
            foreach (RequestAttachment attachment in existing)
                RequestAttachmentDao.Delete(attachment);

            RequestAttachment attach = new RequestAttachment
            {
                ContextType = GetFileContext(model.FileDto.FileName),
                DateCreated = DateTime.Now,
                Description = model.Description,
                FileName = model.FileDto.FileName,
                RequestId = model.EntityId,
                RequestType = (int)model.EntityTypeId,
                UncompressContext = model.FileDto.Context,
                CreatorRole = RoleDao.Load((int)CurrentUser.UserRole)

            };
            RequestAttachmentDao.SaveAndFlush(attach);
            model.Id = attach.Id;
            return true;
        }
        public bool DeleteAttachment(DeleteAttacmentModel model)
        {
            RequestAttachmentDao.Delete(model.Id);
            return true;
        }
        public int GetAttachmentsCount(int entityId, RequestAttachmentTypeEnum typeId)
        {
            return RequestAttachmentDao.FindManyByRequestIdAndTypeId(entityId, typeId).Count;
        }
        public static string GetFileContext(string fileName)
        {
            string extension = Path.GetExtension(fileName);
            //string contextType;
            switch (extension)
            {
                case ".doc":
                case ".docx":
                    return "application/msword";
                case ".xls":
                case ".xlsx":
                    return "application/ms-excel";
                default:
                    return "application/octet-stream";
            }
        }
        #endregion

        #region Department Tree
        public DepartmentTreeModel GetDepartmentTreeModel(int departmentId)
        {
            DepartmentTreeModel model = new DepartmentTreeModel { DepartmentID = departmentId };
            /*List<Department> list = departmentDao.LoadAll().ToList();
            Department rootDep = list.Where(x => x.ParentId == null).FirstOrDefault();
            if(rootDep == null)
                throw new ArgumentException("Отсутствует вершина дерева структурных подразделений");
            Child root = new Child {
                DepartmentID = rootDep.Id,
                Id = rootDep.Code1C.Value,
                Children = new List<Child>(),
                Name = rootDep.Name,
                Parent = null,
            };
            SetChildren(root,list);
            model.Root = root;*/
            if (departmentId == 0)
                departmentId = DepartmentDao.GetRootDepartment().Id;
            IList<Department> list = DepartmentDao.GetDepartmentsTree(departmentId);
            SetModel(model, list);
            return model;
        }
        /*protected void SetChildren(Child root, IList<Department> list)
        {
            List<Department> children = list.Where(x => x.ParentId == root.Id).ToList();
            if(children.Count > 0)
            {
                foreach (Department dep in children)
                {
                    Child child = new Child
                    {
                        DepartmentID = dep.Id,
                        Id = dep.Code1C.Value,
                        Children = new List<Child>(),
                        Name = dep.Name,
                        Parent = root.Id,
                    };
                    SetChildren(child,list);
                    root.Children.Add(child);
                }
            }
        }*/
        protected void SetModel(DepartmentTreeModel model, IList<Department> list)
        {
            model.Level2 = setLevelDropdown(list, 2);
            model.Level3 = setLevelDropdown(list, 3);
            model.Level4 = setLevelDropdown(list, 4);
            model.Level5 = setLevelDropdown(list, 5);
            model.Level6 = setLevelDropdown(list, 6);
            model.Level7 = setLevelDropdown(list, 7);
            SetTreeSelection(model, list);
        }
        protected void SetTreeSelection(DepartmentTreeModel model, IList<Department> list)
        {
            if (model.DepartmentID == 0)
                return;
            int departmentId = model.DepartmentID;
            Department selected = list.Where(x => x.Id == departmentId).FirstOrDefault();
            if (selected == null)
                throw new ArgumentException(string.Format("Не найдено структурное подразделение (id {0})", departmentId));
            int level = selected.ItemLevel.Value;
            for (int i = level; i > 1; i--)
            {
                SetSelection(model, selected);
                if (i == 2)
                    return;
                selected = list.Where(x => x.Code1C.Value == selected.ParentId.Value).FirstOrDefault();
                if (selected == null)
                    throw new ArgumentException(string.Format("Не найдено родительское структурное подразделение уровня {0}", i));
            }
        }
        protected void SetSelection(DepartmentTreeModel model, Department selected/*,int level*/)
        {
            switch (selected.ItemLevel.Value)
            {
                case 7:
                    model.Level7ID = selected.Id;
                    break;
                case 6:
                    model.Level6ID = selected.Id;
                    break;
                case 5:
                    model.Level5ID = selected.Id;
                    break;
                case 4:
                    model.Level4ID = selected.Id;
                    break;
                case 3:
                    model.Level3ID = selected.Id;
                    break;
                case 2:
                    model.Level2ID = selected.Id;
                    break;
                default:
                    throw new ArgumentException(string.Format("Неизвестный уровень структурного подразделения {0}", selected.ItemLevel.Value));
            }
        }
        protected List<IdNameDto> setLevelDropdown(IList<Department> list, int level)
        {

            List<IdNameDto> destList = new List<IdNameDto>
                            {
                                new IdNameDto {Id = 0, Name = EmptyDepartmentName}
                            };
            List<Department> levelList = list.Where(x => x.ItemLevel == level).ToList();
            if (levelList.Count > 0)
                destList.AddRange(levelList.ToList().ConvertAll(x => new IdNameDto
                {
                    Id = x.Id,
                    Name = x.Name
                }).OrderBy(x => x.Name).ToList());
            return destList;
        }

        public DepartmentChildrenDto GetChildren(int parentId, int level)
        {
            try
            {
                Department parent = DepartmentDao.Load(parentId);
                if (parent == null)
                    throw new ArgumentException(string.Format("Подразделение с Id {0} отсутствует в базе данных",
                                                              parentId));
                List<IdNameDto> children = DepartmentDao.
                    SearchByParentId(parent.Code1C.Value).
                    ToList().ConvertAll(x => new IdNameDto { Id = x.Id, Name = x.Name });
                return new DepartmentChildrenDto { Error = string.Empty, Children = children };
            }
            catch (Exception ex)
            {
                Log.Error("Exception on GetChildren:", ex);
                return new DepartmentChildrenDto
                {
                    Error = string.Format("Ошибка: {0}",
                         ex.GetBaseException().Message),
                    Children = new List<IdNameDto>(),
                };
            }
        }
        #endregion

        #region ManualDeduction
        public IList<ManualDeductionDto> GetManualDeductionDocs(int DepartmentId, int Status, string UserName)
        {
            Department dep=null;
            if(DepartmentId>0)
                dep=DepartmentDao.Load(DepartmentId);
            return ManualDeductionDao.GetDocuments(UserDao.Load(CurrentUser.Id),UserName, Status,dep);
        }
        #endregion
        public AttachmentModel GetPrintFormFileContext(int id, RequestPrintFormTypeEnum typeId)
        {
            RequestPrintForm printForm = RequestPrintFormDao.FindByRequestAndTypeId(id, typeId);
            if (printForm == null)
                throw new ArgumentException(string.Format("Печатная форма для заявки (Id {0}) не найдена в базе данных", id));
            return new AttachmentModel
            {
                Context = printForm.Context,
                FileName = "PrintForm.pdf",
                ContextType = "application/pdf"
            };
        }

        /*public VacationPrintModel GetVacationPrintModel(int id)
        {
            Vacation vacation = VacationDao.Load(id);

            return new VacationPrintModel
            {
                AllBeginDateDay = vacation.BeginDate.Day.ToString(),
                AllBeginDateMonth = GetMonthName(vacation.BeginDate.Month),
                AllBeginDateYear = vacation.BeginDate.Year.ToString(),
                AllDaysNumber = vacation.DaysCount.ToString(),
                AllEndDateDay = vacation.EndDate.Day.ToString(),
                AllEndDateMonth = GetMonthName(vacation.EndDate.Month),
                AllEndDateYear = vacation.EndDate.Year.ToString(),
                BeginDateDay = vacation.BeginDate.Day.ToString(),
                BeginDateMonth = GetMonthName(vacation.BeginDate.Month),
                BeginDateYear = vacation.BeginDate.Year.ToString(),
                CreateDate = vacation.CreateDate.ToShortDateString(),
                DaysNumber = vacation.DaysCount.ToString(),
                Department = vacation.User.Department == null?string.Empty:vacation.User.Department.Name,
                DocNumber = vacation.Number.ToString(),
                EndDateDay = vacation.EndDate.Day.ToString(),
                EndDateMonth = GetMonthName(vacation.EndDate.Month),
                EndDateYear = vacation.EndDate.Year.ToString(),
                FIO = vacation.User.FullName,
                //ManagerFIO = 
                //ManagerPosition = 
                OrgName = vacation.User.Organization.Name,
                //PeriodBeginDateDay = vacation.BeginDate.Day.ToString(),
                //PeriodBeginDateMonth = GetMonthName(vacation.BeginDate.Month),
                //PeriodBeginDateYear = vacation.BeginDate.Year.ToString(),
                //PeriodEndDateDay = vacation.EndDate.Day.ToString(),
                //PeriodEndDateMonth = GetMonthName(vacation.EndDate.Month),
                //PeriodEndDateYear = vacation.EndDate.Year.ToString(),
                Position = vacation.User.Position.Name,
                TabNumber = vacation.User.Code,
            };
        }
        public void CreateVacationOrder(int id,string templatePath,string filePath)
        {
            _Application word = null;
            _Document sdoc = null;
            _Document ddoc = null;
            object noSave = WdSaveOptions.wdDoNotSaveChanges;
            object oMissing = Missing.Value;
            try
            {

                Vacation vacation = VacationDao.Load(id);
                List<PrintVacationOrderDto> list = GetdDtoList(vacation);
                
                //разделитель страниц http://msdn.microsoft.com/en-us/library/bb213704%28office.12%29.aspx
                //object pageBreak = WdBreakType.wdPageBreak;
                //не сохранять изменения
                word = new Application {Visible = false};
                //word.Options.DefaultFilePath[WdDefaultFilePath.wdUserTemplatesPath] = folderPath;
                //Можем сделать его видимым и смотреть как скачут слова, абзацы и страницы
                object objTemplatePath = templatePath;
                //Создаем временный документ, в котором будем заменять ключевые слова на наши
                sdoc = word.Documents.Add(ref objTemplatePath, ref oMissing, ref oMissing, ref oMissing);
                for (int i = 0; i < sdoc.Words.Count; i++)
                {
                    //Log.DebugFormat("Text {0} at position {1} ", sdoc.Words[i + 1].Text, i + 1);
                    foreach (PrintVacationOrderDto dto in list)
                    {
                        if (sdoc.Words[i + 1].Text.Trim() == dto.Keyword) //не забываем, что ворд считает с единицы, а не нуля
                        {
                            Log.DebugFormat("Found keyword {0} (word number {1}) ",dto.Keyword, i+1);
                            dto.Position = i + 1;
                            dto.spacesAfter = sdoc.Words[i + 1].Text.Remove(0, dto.Keyword.Length);
                            //keyWordEntries.Add(new keyWordEntry(keyWord, i + 1, sdoc.Words[i + 1].Text.Remove(0, keyWord.Length)));
                        }
                    }
                }
                /?ddoc = word.Documents.Add(ref objTemplatePath, ref oMissing, ref oMissing, ref oMissing);
                ddoc.Range(ref oMissing, ref oMissing).Delete(ref oMissing, ref oMissing);
                ddoc.Range(ref oMissing, ref oMissing).InsertParagraphAfter();?/
                int positionCorrection = 0;
                foreach (PrintVacationOrderDto dto in list)
                {
                    /?if (dto.Keyword == "FIO")
                        positionCorrection = positionCorrection+2;?/
                    Log.DebugFormat("Setting {0} {1} {3} '{2}' to document", dto.Keyword, dto.Text, dto.spacesAfter,dto.Position+positionCorrection);
                    sdoc.Words[dto.Position + positionCorrection].Text = dto.Text + dto.spacesAfter;
                    string[] words = dto.Text.Split(new [] {' '});
                    positionCorrection += words.Count() - 1;
                    string[] wordsPoint = dto.Text.Split(new[] { '.'});
                    positionCorrection += 2*(wordsPoint.Count() - 1);
                }
                /?for (int i = 0; i < sdoc.Words.Count; i++)
                    Log.DebugFormat("Text {0} at position {1} ", sdoc.Words[i + 1].Text, i + 1);?/
                ddoc = word.Documents.Add(ref objTemplatePath, ref oMissing, ref oMissing, ref oMissing);
                ddoc.Range(ref oMissing, ref oMissing).Delete(ref oMissing, ref oMissing);
                ddoc.Range(ref oMissing, ref oMissing).InsertParagraphAfter();
                sdoc.Range(ref oMissing, ref oMissing).Copy();
                ddoc.Paragraphs[1].Range.Paste();
                sdoc.Close(ref noSave, ref oMissing, ref oMissing);
                sdoc = null;
                object objFilePath = filePath;
                //сахраняем полученный документ
                ddoc.SaveAs(ref objFilePath, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing);
                //закрываем полученный документ
                ddoc.Close(ref oMissing, ref oMissing, ref oMissing);
                ddoc = null;
                //завершаем наш процесс ворда
                word.Quit(ref oMissing, ref oMissing, ref oMissing);
                word = null;
            }
            catch (Exception ex)
            {
                Log.Error("Exception on CreateVacationOrder:",ex);
                throw;
            }
            finally
            {
                if(sdoc != null)
                    sdoc.Close(ref noSave,ref oMissing, ref oMissing);
                if(ddoc != null)
                    ddoc.Close(ref oMissing, ref oMissing, ref oMissing);
                if(word != null)
                    word.Quit(ref oMissing, ref oMissing, ref oMissing);
            }
            
        }
        protected List<PrintVacationOrderDto> GetdDtoList(Vacation vacation)
        {
            return new List<PrintVacationOrderDto>
                       {
                           new PrintVacationOrderDto { Keyword = "ORGNAME",Text = vacation.User.Organization.Name},
                           new PrintVacationOrderDto { Keyword = "DOCNUM",Text = vacation.Number.ToString()},
                           new PrintVacationOrderDto { Keyword = "DOCDATE",Text = vacation.CreateDate.ToShortDateString()},
                           new PrintVacationOrderDto { Keyword = "FIO",Text = vacation.User.FullName},
                           new PrintVacationOrderDto { Keyword = "TABNUM",Text = vacation.User.Code},
                           new PrintVacationOrderDto { Keyword = "DEPARTMENT",Text = "Тест"},
                           new PrintVacationOrderDto { Keyword = "POSITION",Text = vacation.User.Position.Name},
                           new PrintVacationOrderDto { Keyword = "VT",Text = vacation.DaysCount.ToString()},
                           new PrintVacationOrderDto { Keyword = "BDD",Text = vacation.BeginDate.Day.ToString()},
                           new PrintVacationOrderDto { Keyword = "BM",Text = GetMonthName(vacation.BeginDate.Month)},
                           new PrintVacationOrderDto { Keyword = "Y",Text = vacation.BeginDate.Year.ToString().Substring(2)},
                           new PrintVacationOrderDto { Keyword = "ED",Text = vacation.EndDate.Day.ToString()},
                           new PrintVacationOrderDto { Keyword = "EDM",Text = GetMonthName(vacation.BeginDate.Month)},
                           new PrintVacationOrderDto { Keyword = "EY",Text = vacation.EndDate.Year.ToString().Substring(2)},
                           new PrintVacationOrderDto { Keyword = "MANPOS",Text = vacation.User.Manager.Position.Name},
                           new PrintVacationOrderDto { Keyword = "MANFIO",Text = vacation.User.Manager.Name},
                       };
        }*/

        protected static string GetMonthNamerRP(int month)
        {
            switch (month)
            {
                case 1:
                    return "января";
                case 2:
                    return "февраля";
                case 3:
                    return "марта";
                case 4:
                    return "апреля";
                case 5:
                    return "мая";
                case 6:
                    return "июня";
                case 7:
                    return "июля";
                case 8:
                    return "августа";
                case 9:
                    return "cентября";
                case 10:
                    return "октября";
                case 11:
                    return "ноября";
                case 12:
                    return "декабря";
                default:
                    throw new ArgumentException(string.Format("Неизвестный месяц {0}", month));
            }
        }
        private static string[,] orderNames =
        {            
	        { "миллиард", "миллиарда", "миллиардов" },
	        { "миллион", "миллиона", "миллионов" },
	        { "тысяча", "тысячи", "тысяч" },
	        { "", "", "" },
        };
        public static string GetSummString(decimal sum)
        {
            StringBuilder resultBuilder = new StringBuilder();

            long rubles = (long)sum;
            int copecks = (int)Math.Round((sum - rubles) * 100);

            int[] rublesParts = 
            {
	            (int)((rubles % 1000000000000 - rubles % 1000000000) / 1000000000), // миллиарды
	            (int)((rubles % 1000000000 - rubles % 1000000) / 1000000), // миллионы
	            (int)((rubles % 1000000 - rubles % 1000) / 1000), // тысячи
	            (int)(rubles % 1000), // 
            };

            int[] partDigits = new int[3];
            for (int i = 0; i < rublesParts.Length; i++)
            {
                if (rublesParts[i] != 0)
                {
                    partDigits[0] = (rublesParts[i] - rublesParts[i] % 100) / 100; // сотни
                    partDigits[1] = (rublesParts[i] % 100 - rublesParts[i] % 10) / 10; // десятки
                    partDigits[2] = rublesParts[i] % 10; // единицы

                    #region Названия сотен
                    switch (partDigits[0])
                    {
                        case 1: resultBuilder.Append(" сто"); break;
                        case 2: resultBuilder.Append(" двести"); break;
                        case 3: resultBuilder.Append(" триста"); break;
                        case 4: resultBuilder.Append(" четыреста"); break;
                        case 5: resultBuilder.Append(" пятьсот"); break;
                        case 6: resultBuilder.Append(" шестьсот"); break;
                        case 7: resultBuilder.Append(" семьсот"); break;
                        case 8: resultBuilder.Append(" восемьсот"); break;
                        case 9: resultBuilder.Append(" девятьсот"); break;
                    }
                    #endregion

                    #region Названия десятков и чисел от 11 до 19
                    switch (partDigits[1])
                    {
                        case 1:
                            switch (partDigits[2])
                            {
                                case 0: resultBuilder.Append(" десять"); break;
                                case 1: resultBuilder.Append(" одиннадцать"); break;
                                case 2: resultBuilder.Append(" двенадцать"); break;
                                case 3: resultBuilder.Append(" тринадцать"); break;
                                case 4: resultBuilder.Append(" четырнадцать"); break;
                                case 5: resultBuilder.Append(" пятнадцать"); break;
                                case 6: resultBuilder.Append(" шестнадцать"); break;
                                case 7: resultBuilder.Append(" семнадцать"); break;
                                case 8: resultBuilder.Append(" восемнадцать"); break;
                                case 9: resultBuilder.Append(" девятнадцать"); break;
                            }
                            break;
                        case 2: resultBuilder.Append(" двадцать"); break;
                        case 3: resultBuilder.Append(" тридцать"); break;
                        case 4: resultBuilder.Append(" сорок"); break;
                        case 5: resultBuilder.Append(" пятьдесят"); break;
                        case 6: resultBuilder.Append(" шестьдесят"); break;
                        case 7: resultBuilder.Append(" семьдесят"); break;
                        case 8: resultBuilder.Append(" восемьдесят"); break;
                        case 9: resultBuilder.Append(" девяносто"); break;
                    }
                    #endregion

                    #region Названия единиц
                    if (partDigits[1] != 1)
                    {
                        switch (partDigits[2])
                        {
                            case 1: resultBuilder.Append(i != 2 ? " один" : " одна"); break;
                            case 2: resultBuilder.Append(i != 2 ? " два" : " две"); break;
                            case 3: resultBuilder.Append(" три"); break;
                            case 4: resultBuilder.Append(" четыре"); break;
                            case 5: resultBuilder.Append(" пять"); break;
                            case 6: resultBuilder.Append(" шесть"); break;
                            case 7: resultBuilder.Append(" семь"); break;
                            case 8: resultBuilder.Append(" восемь"); break;
                            case 9: resultBuilder.Append(" девять"); break;
                        }
                    }
                    #endregion

                    switch (partDigits[2])
                    {
                        case 1:
                            resultBuilder.Append(partDigits[1] != 1 ? " " + orderNames[i, 0] : " " + orderNames[i, 2]); break;
                        case 2:
                        case 3:
                        case 4: resultBuilder.Append(partDigits[1] != 1 ? " " + orderNames[i, 1] : " " + orderNames[i, 2]); break;
                        default: resultBuilder.AppendFormat(" {0}", orderNames[i, 2]); break;
                    }
                }
            }

            if (rubles == 0)
                resultBuilder.AppendFormat(" {0}", "0");

            switch (partDigits[2])
            {
                case 1:
                    resultBuilder.Append(partDigits[1] != 1 ? " рубль" : " рублей"); break;
                case 2:
                case 3:
                case 4: resultBuilder.Append(" рубля"); break;
                default: resultBuilder.AppendFormat(" рублей"); break;
            }


            #region Добавление копеек
            switch (copecks % 10)
            {
                case 1:
                    resultBuilder.AppendFormat(copecks != 11 ? " {0} копейка" : " {0} копеек", copecks); break;
                case 2:
                case 3:
                case 4: resultBuilder.AppendFormat(" {0} копейки", copecks); break;
                default: resultBuilder.AppendFormat(" {0} копеек", copecks); break;
            }
            #endregion

            if (resultBuilder[0] == ' ')
            {
                resultBuilder.Remove(0, 1);
            }
            return resultBuilder.ToString();
        }
        #region AcceptRequest
        public void GetAcceptRequestsModel(AcceptRequestsModel model)
        {
            SetListboxes(model);
            SetWeekDtos(model);
        }
        public void SetAcceptDate(AcceptRequestsModel model)
        {
            try
            {
                IUser currentUser = AuthenticationService.CurrentUser;
                if (!string.IsNullOrEmpty(model.AcceptDate) || (currentUser.UserRole & UserRole.Manager) == UserRole.Manager)
                {
                    DateTime acceptDate;
                    if (DateTime.TryParse(model.AcceptDate, out acceptDate))
                    {
                        User user = UserDao.Load(currentUser.Id);
                        AcceptRequestDate entity = user.AcceptRequests.Where(x => x.DateAccept == acceptDate).FirstOrDefault();
                        if (entity == null)
                        {
                            entity = new AcceptRequestDate
                            {
                                DateAccept = acceptDate,
                                DateCreate = DateTime.Now,
                                User = user,
                            };
                            user.AcceptRequests.Add(entity);
                            UserDao.Save(user);
                            //EmailDto emailDto = new EmailDto();
                            EmailDto emailDto = SendEmailForManagerAcceptRequests(user, acceptDate);
                        }
                        else
                            Log.WarnFormat("Request already accepted for user {0} date {1} at {2}",
                                user.Id, entity.DateAccept, entity.DateCreate);

                    }
                }
            }
            catch (Exception ex)
            {
                model.Error = string.Format("Исключение:{0}", ex.GetBaseException().Message);
                Log.Error("Exception on SetAccept", ex);

            }
            finally
            {
                model.AcceptDate = null;
                SetListboxes(model);
                SetWeekDtos(model);
            }
        }
        protected void SetWeekDtos(AcceptRequestsModel model)
        {
            DateTime month = new DateTime(model.Year, model.Month, 1);
            IList<AcceptWeekDto> list = GetWeeksDtoList(month);
            DateTime beginDate = list[0].Monday;
            DateTime endDate = list[5].Friday;
            IUser user = AuthenticationService.CurrentUser;
            IList<AcceptRequestDateDto> acceptDates =
                UserDao.GetAcceptDatesForManager(user.Id, user.UserRole, beginDate, endDate);
            List<IdNameDto> allUserIds = new List<IdNameDto>();
            foreach (AcceptRequestDateDto dto in acceptDates)
            {
                if (allUserIds.Where(x => x.Id == dto.UserId).FirstOrDefault() == null)
                    allUserIds.Add(new IdNameDto(dto.UserId, dto.UserName));
            }
            IList<UserAcceptWeekDto> resultList = new List<UserAcceptWeekDto>();
            foreach (IdNameDto idNameDto in allUserIds)
            {
                IList<AcceptRequestWeekDto> listFroUser = new List<AcceptRequestWeekDto>();
                IList<AcceptRequestDateDto> userDto =
                    acceptDates.Where(x => x.UserId == idNameDto.Id).ToList();
                foreach (AcceptWeekDto acceptWeekDto in list)
                {
                    AcceptRequestWeekDto newDto = new AcceptRequestWeekDto
                    {
                        Friday = acceptWeekDto.Friday,
                        IsAccepted =
                        userDto.Where(x => x.DateAccept == acceptWeekDto.Friday).
                        FirstOrDefault() != null,
                        IsEditable = acceptWeekDto.Friday == DateTime.Today ||
                        acceptWeekDto.Friday.AddDays(3) == DateTime.Today,
                    };
                    newDto.IsEditable = newDto.IsEditable &&
                                        !newDto.IsAccepted &&
                                        (user.UserRole & UserRole.Manager) == UserRole.Manager;
                    newDto.IsHidden = DateTime.Today < acceptWeekDto.Friday;
                    listFroUser.Add(newDto);
                }
                resultList.Add(new UserAcceptWeekDto
                {
                    UserId = idNameDto.Id,
                    UserName = idNameDto.Name,
                    dtos = listFroUser
                }
                    );
            }
            model.WeeksList = list;
            model.UsersList = resultList;
        }
        protected void SetListboxes(AcceptRequestsModel model)
        {
            model.Monthes = GetMonthesList();
            model.Years = GetYearsList();
        }
        protected IList<AcceptWeekDto> GetWeeksDtoList(DateTime month)
        {
            IList<AcceptWeekDto> list = new List<AcceptWeekDto>();
            DateTime firstMonday = month.AddDays((int)month.DayOfWeek == 0 ? -6 : -(int)month.DayOfWeek + 1);
            for (int i = 0; i < 6; i++)
            {
                DateTime monday = firstMonday.AddDays(i * 7);
                DateTime friday = firstMonday.AddDays(i * 7 + 4);
                //if(monday.Month != month.Month && friday.Month != month.Month)
                //    continue;
                list.Add(new AcceptWeekDto
                {
                    Monday = monday,
                    Friday = friday
                });
            }
            return list;
        }
        #endregion

        #region Constant
        public void GetConstantListModel(ConstantListModel model)
        {
            SetListboxes(model);
            SetMonthList(model);
        }
        protected void SetListboxes(ConstantListModel model)
        {
            //model.Monthes = GetMonthesList();
            model.Years = GetYearsList();
        }
        protected void SetMonthList(ConstantListModel model)
        {
            List<WorkingDaysConstant> list = workingDaysConstantDao.LoadDataForYear(model.Year);
            model.Months = list.ConvertAll(x => new MonthConstModel
            {
                Days = x.Days,
                Hours = x.Hours,
                Month = x.Month.Month,
                MonthName = GetMonth(x.Month),
                Year = x.Month.Year,
                Id = x.Id,
            });
            int firstAvailableAddMonth = 0;
            for (int i = 1; i < 13; i++)
            {
                WorkingDaysConstant entity = list.Where(x => x.Month == new DateTime(model.Year, i, 1)).FirstOrDefault();
                if (entity == null)
                {
                    firstAvailableAddMonth = i;
                    break;
                }
            }
            model.FirtsAvailableAddMonth = firstAvailableAddMonth;
        }
        public void GetConstantEditModel(ConstantEditModel model)
        {
            ReloadDictionariesToModel(model);
            WorkingDaysConstant entity;
            if (model.Id != 0)
            {
                entity = WorkingDaysConstantDao.Load(model.Id);
                if (entity == null)
                    throw new ArgumentException(string.Format("Не могу загрузить константу (id {0}) из базы данных", model.Id));
                model.Year = entity.Month.Year;
                model.Month = entity.Month.Month;
            }
            else if (model.Month != 0)
                entity = WorkingDaysConstantDao.LoadDataForMonth(model.Month, model.Year);
            else
            {
                model.Month = DateTime.Today.Month;
                entity = WorkingDaysConstantDao.LoadDataForMonth(model.Month, model.Year);
            }
            if (entity != null)
            {
                model.Id = entity.Id;
                model.TS = entity.Version;
                model.Days = entity.Days.ToString();
                model.Hours = entity.Hours.ToString();
            }
            else
            {
                model.Id = 0;
                model.TS = 0;
                model.Days = string.Empty;
                model.Hours = string.Empty;
            }
        }
        public void ReloadDictionariesToModel(ConstantEditModel model)
        {
            model.Months = GetMonthesList();
        }
        public bool SaveConstantEditModel(ConstantEditModel model, out string error)
        {
            error = string.Empty;
            try
            {
                int days;
                Int32.TryParse(model.Days, out days);
                int hours;
                Int32.TryParse(model.Hours, out hours);
                WorkingDaysConstant entity;
                if (model.Id == 0)
                {
                    entity = new WorkingDaysConstant
                    {
                        Days = days,
                        Hours = hours,
                        Month = new DateTime(model.Year, model.Month, 1),
                    };
                    //ChangeEntityProperties(current, mission, model, user);
                    WorkingDaysConstantDao.SaveAndFlush(entity);
                    model.Id = entity.Id;
                }
                else
                {
                    entity = WorkingDaysConstantDao.Load(model.Id);
                    if (model.TS != entity.Version)
                    {
                        error = "Константа была изменена другим пользователем.";
                        model.ReloadPage = true;
                        return false;
                    }
                    entity.Days = days;
                    entity.Hours = hours;
                    //ChangeEntityProperties(current, mission, model, user);
                    WorkingDaysConstantDao.SaveAndFlush(entity);
                }
                //model.DocumentNumber = mission.Number.ToString();
                model.TS = entity.Version;
                //model.DaysCount = mission.DaysCount;
                //model.CreatorLogin = mission.Creator.Login;
                //model.DateCreated = mission.CreateDate.ToShortDateString();
                //SetFlagsState(mission.Id, user, mission, model);
                return true;
            }
            catch (Exception ex)
            {
                WorkingDaysConstantDao.RollbackTran();
                Log.Error("Error on SaveConstantEditModel:", ex);
                error = string.Format("Исключение:{0}", ex.GetBaseException().Message);
                return false;
            }
            finally
            {
                ReloadDictionariesToModel(model);
            }
        }
        #endregion

        #region Deduction
        public DeductionImportModel GetDeductionImportModel(DeductionImportModel model = null)
        {
            if (model == null) model = new DeductionImportModel();
            model.DateEdited = DateTime.Now.ToShortDateString();
            model.Editor = CurrentUser.Name;
            LoadDictionaries(model);
            return model;
        }
        public IList<DeductionDto> ImportDeductionFromFile(ref string path, ref List<string> Errors, ref bool isFileExist)
        {
            var inp = new StreamReader(path);
            var hash = Reports.Presenters.Utils.WebUtils.GetMd5Hash(inp.ReadToEnd());
            inp.Close();
            DeductionImport import = DeductionImport_Dao.LoadByHash(hash);
            List<Deduction> Deductions;
            if (import != null)
            {
                isFileExist = true;
                Deductions = DeductionDao.GetDeductionsByImportId(import.Id).ToList();
                StreamReader reader = new StreamReader(Path.Combine(Path.GetDirectoryName(path), import.ReportFile));
                path = Path.Combine(Path.GetDirectoryName(path), import.InputFile);
                while (!reader.EndOfStream)
                {
                    Errors.Add(reader.ReadLine());
                }
                reader.Close();
            }
            else
            {
                import = new DeductionImport();
                import.Creator = UserDao.Load(CurrentUser.Id);
                import.InputFileHash = hash;
                import.ImportDate = DateTime.Now;
                import.InputFile = path.Substring(path.LastIndexOf('\\') + 1);
                DeductionImport_Dao.SaveAndFlush(import);
                Deductions = new List<Deduction>();
                Encoding enc=Encoding.GetEncoding("Windows-1251");
                StreamReader reader = new StreamReader(path,enc);
                var type = DeductionTypeDao.Load(1);
                var kinds = DeductionKindDao.LoadAll();
                while (!reader.EndOfStream)
                {
                    string data = reader.ReadLine();
                    Match m = Regex.Match(data, "^[\"']*\\d+[\"']*\\s*[;:]\\s*[\"']*(?<Department>[^\"']+)['\"]*\\s*[:;]\\s*['\"]*(?<Surname>[^'\"]+)['\"]*\\s*[:;]\\s*['\"]*(?<Name>[^'\"]+)['\"]*\\s*[:;]\\s*['\"]*(?<Patronymic>[^'\"]+)[\"']*\\s*[:;]\\s*['\"]*(?<Cnilc>[^'\"]+)['\"]*\\s*[;:]\\s*['\"]*(?<Sum>[^'\"]+)['\"]*\\s*[:;]\\s*['\"]*[^#'\"]+(?<DeductionKind>#\\d+)['\"]*\\s*[:;]\\s*['\"]*(?<Period>[^'\"]+)['\"]*[^\\r\\n$]*$");
                    if (!m.Success) { Errors.Add("Неправильный формат данных.>" + data); continue; }
                    var el = new Deduction();
                    try
                    {
                        el.Sum = decimal.Parse(m.Groups["Sum"].Value.Replace(',','.'), System.Globalization.CultureInfo.InvariantCulture);
                        el.Kind = kinds.Where(x => x.Name.Contains(m.Groups["DeductionKind"].Value.Trim())).First();
                        el.DeductionDate = DateTime.Parse(m.Groups["Period"].Value);
                        el.EditDate = DateTime.Now;
                        el.PhoneNumber = m.Groups["Phone"].Value.Trim();
                        if (el.Kind == null) { Errors.Add("Не найден вид удержания.>" + data); continue; }
                        el.Type = type;
                        var foundedUsers = userDao.FindByCnilc(m.Groups["Cnilc"].Value.Trim());
                        if (foundedUsers == null || !foundedUsers.Any()) { Errors.Add("Пользователь не найден по СНИЛС.>" + data); continue; };
                        foundedUsers = foundedUsers.Where(x => x.Name.Contains(m.Groups["Surname"].Value) && x.Name.Contains(m.Groups["Name"].Value) && x.Name.Contains(m.Groups["Patronymic"].Value)).ToList();
                        if (foundedUsers == null || !foundedUsers.Any()) { Errors.Add("Пользователь найден по СНИЛС,но не найден по ФИО.>" + data); continue; };
                        if (!foundedUsers.Any(x => (x.UserRole & UserRole.Employee) > 0))
                        {
                            if (foundedUsers.Any(x => (x.UserRole & UserRole.DismissedEmployee) > 0))
                            {
                                Errors.Add("Пользователь уволен.>" + data); continue;
                            }
                            Errors.Add("Пользователь не является сотрудником.>" + data); continue;
                        }
                        foundedUsers = foundedUsers.Where(x => (x.UserRole & UserRole.Employee) > 0).ToList();

                        el.User = foundedUsers.Any(x => !x.ContractType.HasValue)? foundedUsers.First(x => !x.ContractType.HasValue):null;
                        if (el.User == null)
                        {
                            Errors.Add("Пользователь по совместительству.>" + data); continue;
                        }
                        el.Number = RequestNextNumberDao.GetNextNumberForType((int)RequestTypeEnum.Deduction);
                        el.Editor = UserDao.Load(CurrentUser.Id);
                        el.IsFastDismissal = false;
                        el.UploadingDocType = 4;
                        if (Deductions.Any(x => x.User == el.User && x.DeductionDate == el.DeductionDate && x.Sum == el.Sum && x.Kind == el.Kind && x.PhoneNumber == el.PhoneNumber)) { Errors.Add("Дубликат!>" + data); continue; }
                        el.DeductionImport = import;
                        DeductionDao.SaveAndFlush(el);
                        Deductions.Add(el);
                    }
                    catch (Exception ex) { Errors.Add("Ошибка при обработке данных. Не корректные данные.>" + data); continue; }
                }
                reader.Close();
                Deductions = Deductions.Distinct().ToList();
                var report = path.Replace(".input.csv", ".report.txt");
                StreamWriter writer = new StreamWriter(report);
                foreach (var err in Errors)
                    writer.WriteLine(err);
                writer.Flush();
                writer.Close();
                report = import.InputFile.Replace(".input.csv", ".report.txt");
                import.ReportFile = report;
                DeductionImport_Dao.SaveAndFlush(import);
            }
            return Deductions.ConvertAll<DeductionDto>(new Converter<Deduction, DeductionDto>(x => new DeductionDto()
            {
                UserId = x.User.Id,
                UserName = x.User.Name,
                Sum = x.Sum,
                Position = x.User.Position.Name,
                DeductionDate = x.DeductionDate,
                Kind = x.Kind.Name,
                Id = x.Id,
                Number = x.Number.ToString(),

            }
                 ));
        }
        public DeductionListModel GetDeductionListModel()
        {
            User user = UserDao.Load(AuthenticationService.CurrentUser.Id);
            IdNameReadonlyDto dep = GetDepartmentDto(user);
            DeductionListModel model = new DeductionListModel
            {
                UserId = AuthenticationService.CurrentUser.Id,
                DepartmentName = dep.Name,
                DepartmentId = dep.Id,
                DepartmentReadOnly = dep.IsReadOnly,
                RequestStatuses = GetDeductionStatuses(true),
                Types = GetDeductionTypes(true)
            };
            SetInitialDates(model);
            return model;
        }
        public List<IdNameDto> GetDeductionStatuses(bool addAll)
        {
            List<IdNameDto> deductionStatuses = new List<IdNameDto>
                                                       {
                                                           new IdNameDto(1, "Записана"),
                                                           new IdNameDto(2, "Выгружена в 1С"),
                                                           new IdNameDto(3, "Отклонена"),
                                                           new IdNameDto(4, "Автовыгрузка"),
                                                           new IdNameDto(5, "Автоудержание")
                                                       }.OrderBy(x => x.Name).ToList();
            if (addAll)
                deductionStatuses.Insert(0, new IdNameDto(0, SelectAll));
            return deductionStatuses;
        }
        public List<IdNameDto> GetDeductionTypes(bool addAll)
        {
            List<IdNameDto> deductionTypes = DeductionTypeDao.LoadAllSorted().ToList().ConvertAll(x => new IdNameDto { Id = x.Id, Name = x.Name }).ToList();
            if (addAll)
                deductionTypes.Insert(0, new IdNameDto(0, SelectAll));
            return deductionTypes;
        }
        public void SetDeductionListModel(DeductionListModel model, bool hasError)
        {
            User user = UserDao.Load(model.UserId);
            model.RequestStatuses = GetDeductionStatuses(true);
            model.Types = GetDeductionTypes(true);
            if (hasError)
                model.Documents = new List<DeductionDto>();
            else
                SetDocumentsToModel(model, user);
        }
        public void SetDocumentsToModel(DeductionListModel model, User user)
        {
            UserRole role = CurrentUser.UserRole;//(UserRole)(user.RoleId & (int)CurrentUser.UserRole);
            //model.Documents = new List<DeductionDto>();
            /*Department dep = null;
            if(model.Department.Id != 0)
             dep = DepartmentDao.SearchByNameDistinct(model.Department.Name);*/
            model.Documents = DeductionDao.GetDocuments(user.Id,
                role,
                model.DepartmentId,
                //model.PositionId,
                model.TypeId,
                model.RequestStatusId,
                model.BeginDate,
                model.EndDate,
                model.UserName,
                model.SortBy,
                model.SortDescending,
                model.Number);
            if (model.Documents != null && model.Documents.Count > 0)
                model.Total = model.Documents.Sum(x => x.Sum);
        }

        public DeductionEditModel GetDeductionEditModel(int id)
        {
            DeductionEditModel model = new DeductionEditModel { Id = id };
            IUser current = AuthenticationService.CurrentUser;
            if (!CheckDeductionUserRights(current))
                throw new ArgumentException("Доступ запрещен.");

            LoadDictionaries(model);
            Deduction deduction = null;

            if (id == 0)
            {
                //editor = userDao.Load(current.Id);
                //if(editor == null)
                //    throw new ValidationException(string.Format("Не могу загрузить пользователя (id {0})",current.Id));
                model.Version = 0;
                DateTime today = DateTime.Today;
                model.DateEdited = today.ToShortDateString();
                //model.UserId = model.Users[0].Id;
                model.UserId = model.UserId;
                model.MonthId = today.Year * 100 + today.Month;
            }
            else
            {
                deduction = DeductionDao.Load(id);
                if (deduction == null)
                    throw new ArgumentException(string.Format("Удержание (id {0}) не найдена в базе данных.", id));
                model.Version = deduction.Version;
                model.UserId = deduction.User.Id;
                model.Surname = userDao.GetUserListForDeduction(null, deduction.User.Id).Single().Name;
                // IdNameDto dto = model.Users.Where(x => x.Id == model.UserId).FirstOrDefault();
                // if(dto == null)
                //     throw new ArgumentException(
                //string.Format("Пользователь {0} не является сотрудником или уволен более 3 месяцев назад",deduction.User.Name));
                model.KindId = deduction.Kind.Id;
                model.Sum = deduction.Sum.ToString();
                model.TypeId = deduction.Type.Id;
                model.MonthId = deduction.DeductionDate.Year * 100 + deduction.DeductionDate.Month;
                model.DateEdited = deduction.EditDate.ToShortDateString();
                model.DocumentNumber = deduction.Number.ToString();
                if (deduction.MissionReport != null && deduction.MissionReport.Any())
                    model.MissionReportNumber = deduction.MissionReport.First().Number;
                else
                {
                    try
                    {
                        var storno = MissionReportDao.QueryExpression(x =>x.StornoDeduction!=null && x.StornoDeduction.Id == deduction.Id);
                        if (storno != null && storno.Any())
                            model.MissionReportNumber = storno.First().Number;
                    }
                    catch (Exception ex) { }
                }
                if (deduction.Type.Id != (int)DeductionTypeEnum.Deduction)
                {
                    model.DismissalDate = deduction.DismissalDate;
                    model.IsFastDismissal = deduction.IsFastDismissal.HasValue ? deduction.IsFastDismissal.Value : false;
                }
                if (deduction.DeleteDate.HasValue)
                    model.IsDeleted = true;
                SetHiddenFields(model);
            }
            SetDeductionUserInfoModel(model, model.UserId);
            SetEditor(model, deduction, current);
            SetStatus(model, deduction);
            SetFlagsState(id, /*user,*/ deduction, model);
            return model;
        }
        protected void SetStatus(DeductionEditModel model, Deduction deduction)
        {
            if (model.Id == 0)
                model.Status = "Новая";
            else
            {
                model.Status = deduction.DeleteDate.HasValue
                                   ? "Отклонена"
                                   : deduction.SendTo1C.HasValue ? "Выгружена в 1С" : !deduction.UploadingDocType.HasValue ? "Записана" : deduction.UploadingDocType == 4 ? "Загруженно из файла" : "Автовыгрузка";
            }
        }
        protected void SetEditor(DeductionEditModel model, Deduction deduction, IUser current)
        {
            User editor;
            if (model.Id == 0)
            {
                editor = userDao.Load(current.Id);
                if (editor == null)
                    throw new ValidationException(string.Format("Не могу загрузить пользователя (id {0})", current.Id));
            }
            else
                editor = deduction.Editor;
            model.Editor = editor.Name + (string.IsNullOrEmpty(editor.Email) ? string.Empty : ", " + ((editor.Email == "info@ruscount.ru") ? "polyak@sovcombank.ru" : editor.Email));
        }
        public void SetDeductionUserInfoModel(DeductionUserInfoModel model, int userId)
        {
            model.UserInfoError = string.Empty;
            User user = userDao.Load(userId);
            if (user == null)
                throw new ValidationException(string.Format("Не могу загрузить пользователя (id {0})", userId));
            //if((user.UserRole & UserRole.Employee) == 0)
            //    throw new ValidationException(string.Format("Пользователь (id {0}) не является сотрудником", userId));
            if (userId != 0)
            {
                model.Department = user.Department == null ? string.Empty : user.Department.Name;
                model.Position = user.Position == null ? string.Empty : user.Position.Name;
                model.Cnilc = user.Cnilc;
                DateTime? dateRelease = null;
                if (user.DateRelease.HasValue)
                    dateRelease = user.DateRelease.Value;
                //else
                //{
                //    DateTime? releaseDate = dismissalDao.GetDismissalDateForUser(user.Id);
                //    if(releaseDate.HasValue)
                //        dateRelease = releaseDate.Value;
                //}
                if (dateRelease.HasValue)
                {
                    model.DateRelease = dateRelease.Value.ToShortDateString();
                    if (dateRelease.Value < DateTime.Today.AddMonths(-3))
                        model.UserInfoError = "Сотрудник уволен более 3 месяцев назад";
                }
            }

        }
        protected void SetFlagsState(int id, /*User user,*/ Deduction deduction, DeductionEditModel model)
        {
            SetFlagsState(model, false);

            if (deduction!=null && deduction.ManualDeduction != null)
            {
                model.MissionReportNumber = deduction.ManualDeduction.MissionReport.Number;
            }
            UserRole currentUserRole = AuthenticationService.CurrentUser.UserRole;
            if (id == 0 && (currentUserRole & UserRole.Accountant) > 0)
            {
                model.IsEditable = true;
                model.IsSaveAvailable = true;
                return;
            }
            switch (currentUserRole)
            {
                case UserRole.Estimator:
                case UserRole.OutsourcingManager:
                    if (deduction.SendTo1C.HasValue && !deduction.DeleteDate.HasValue)
                        model.IsDeleteAvailable = true;
                    break;
                case UserRole.Accountant:
                    if (!deduction.SendTo1C.HasValue && !deduction.DeleteDate.HasValue)
                    {
                        if (deduction.EditDate.Month == DateTime.Today.Month && deduction.EditDate.Year == DateTime.Today.Year)
                            model.IsEditable = true;
                        model.IsDeleteAvailable = true;
                    }
                    model.IsCreateButtonVisible = true;
                    break;
            }
            model.IsSaveAvailable = model.IsEditable;
        }
        protected void SetFlagsState(DeductionEditModel model, bool state)
        {
            model.IsSaveAvailable = state;
            model.IsDelete = state;
            model.IsDeleteAvailable = state;
            model.IsEditable = state;
            model.IsCreateButtonVisible = state;
        }
        protected void SetHiddenFields(DeductionEditModel model)
        {
            model.UserIdHidden = model.UserId;
            model.KindIdHidden = model.KindId;
            model.TypeIdHidden = model.TypeId;
            model.MonthIdHidden = model.MonthId;
            model.IsFastDismissalHidden = model.IsFastDismissalHidden;
        }
        public User GetUser(int Id)
        {
            return UserDao.Load(Id);
        }
        public IList<IdNameDto> GetUserListForDeduction(string Name, int UserId)
        {
            return userDao.GetUserListForDeduction(Name, UserId);
        }
        protected void LoadDictionaries(DeductionEditModel model)
        {
            model.Types = GetDeductionTypes(false);
            model.Kindes = GetDeductionKinds();
            if (model.Id == 0 && DateTime.Now >= new DateTime(2015, 4, 1, 17, 15, 00)) model.Kindes = model.Kindes.Where(x => x.Id != 3).ToList();
            model.Monthes = GetDeductionMonthes();
            //model.Users = userDao.GetUserListForDeduction();
        }
        protected void LoadDictionaries(DeductionImportModel model)
        {
            model.Types = GetDeductionTypes(false);
            model.Kindes = GetDeductionKinds();
            if (DateTime.Now >= new DateTime(2015, 4, 1, 17, 15, 00)) model.Kindes = model.Kindes.Where(x => x.Id != 3).ToList();
            model.Monthes = GetDeductionMonthes();
            //model.Users = userDao.GetUserListForDeduction();
        }
        protected List<IdNameDto> GetDeductionKinds()
        {
            List<IdNameDto> deductionKinds = deductionKindDao.LoadAllSorted().ToList().
                ConvertAll(x => new IdNameDto { Id = x.Id, Name = x.Name }).ToList();
            return deductionKinds;
        }
        protected List<IdNameDto> GetDeductionMonthes()
        {
            List<IdNameDto> list = new List<IdNameDto>();
            DateTime today = DateTime.Today;
            DateTime beginDate = new DateTime(today.Year, 1, 1);
            DateTime endDate = new DateTime(today.Year, 1, 1).AddYears(1).AddMilliseconds(-1);
            DateTime? beginDateFromDb = deductionDao.GetMinDeductionPeriod();
            if (beginDateFromDb.HasValue && beginDateFromDb.Value < beginDate)
                beginDate = beginDateFromDb.Value;

            DateTime currDate = beginDate;
            while (currDate < endDate)
            {
                list.Add(new IdNameDto
                {
                    Id = currDate.Year * 100 + currDate.Month,
                    Name = string.Format("{0} {1}", GetMonthName(currDate.Month), currDate.Year),
                });
                currDate = currDate.AddMonths(1);
            }
            //int currentYear = DateTime.Today.Year;
            //for (int i = 1; i < 13; i++)
            //{

            //    list.Add(new IdNameDto
            //                 {
            //                     Id = currentYear*100+i,
            //                     Name = string.Format("{0} {1}",GetMonthName(i),currentYear),
            //                 });
            //}
            return list;
        }
        public void ReloadDictionariesToModel(DeductionEditModel model)
        {
            LoadDictionaries(model);
            SetDeductionUserInfoModel(model, model.UserId);
            Deduction deduction = null;
            if (model.Id > 0)
            {
                deduction = DeductionDao.Load(model.Id);
                if (deduction == null)
                    throw new ArgumentException(string.Format("Удержание (id {0}) не найдена в базе данных.", model.Id));
                model.DateEdited = deduction.EditDate.ToShortDateString();
            }
            else
                model.DateEdited = DateTime.Today.ToShortDateString();

            SetEditor(model, deduction, AuthenticationService.CurrentUser);
            SetStatus(model, deduction);
        }
        public bool CheckDeductionUserRights(IUser current)
        {
            if ((current.UserRole & UserRole.Accountant) > 0 ||
                (current.UserRole & UserRole.Estimator)>0||
               (current.UserRole & UserRole.OutsourcingManager) > 0)
                return true;
            return false;
        }
        public bool ExportFromMissionReportToDeduction(IEnumerable<int> DocIds, int typeId, int kindId, int uploadingType, bool isFastDissmissal, bool EnableSendEmail)
        {
            List<Deduction> MailList = new List<Deduction>();
            ///В случае ошибки нужно откатить транзакции.
            //DeductionDao.BeginTran();
            //MissionReportDao.BeginTran();
            try
            {
                foreach (var id in DocIds)
                {
                    var report = MissionReportDao.Load(id);
                    if (report.Deduction != null || ((!report.AccountantDateAccept.HasValue) && uploadingType != 2)) continue;
                    var deduction = new Deduction
                    {
                        Number = RequestNextNumberDao.GetNextNumberForType((int)RequestTypeEnum.Deduction),
                        User = report.User,
                        Editor = UserDao.Load(CurrentUser.Id),
                        EditDate = DateTime.Now,
                        DeductionDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1),
                        Type = DeductionTypeDao.Load(typeId),
                        Kind = DeductionKindDao.Load(kindId),
                        Sum = Math.Abs(report.AccountantAllSum - report.PurchaseBookAllSum - report.UserSumReceived),
                        DeleteAfterSendTo1C = false,
                        UploadingDocType = uploadingType,
                        IsFastDismissal = isFastDissmissal,
                        DismissalDate = DismissalDao.GetDismissalDateForUser(report.User.Id)
                    };

                    DeductionDao.SaveAndFlush(deduction);
                    report.Deduction = deduction;
                    MissionReportDao.SaveAndFlush(report);
                    MailList.Add(deduction);
                }
            }
            catch (Exception ex)
            {
                //DeductionDao.RollbackTran();
                //MissionReportDao.RollbackTran();
                Log.Error("Во время экспорта записей произошла ошибка.", ex);
                return false;
            }
            //DeductionDao.CommitTran();
            //MissionReportDao.CommitTran();
            if (EnableSendEmail)
                foreach (var el in MailList)
                    SendEmailToUser(null, el);
            return true;
        }
        /// <summary>
        /// deprecated
        /// </summary>
        /// <param name="deductionNumber"></param>
        /// <param name="MissionReportid"></param>
        /// <returns></returns>
        public string SetDeductionDoc(int deductionNumber, int MissionReportid)
        {
            var list = DeductionDao.LoadAll().Where(x => x.Number == deductionNumber);
            Deduction d;
            if (list != null && list.Any())
                d = list.First();
            else return "Не найдено удержание. ";
            var m = MissionReportDao.Load(MissionReportid);
            if (m.User.Id != d.User.Id) return "Удержание указано не верно. ";
            if (m.Deduction != null && m.Deduction.Id != d.Id) return "Удержание указано не верно. ";
            m.Deduction = d;
            MissionReportDao.SaveAndFlush(m);
            return "";
        }
        public bool ChangeNotUseInAnalyticalStatement(int[] ids, bool[] notuse)
        {
            DeductionDao.BeginTran();
            try
            {
                for (int i = 0; i < ids.Length; i++)
                {
                    Deduction entity = DeductionDao.Load(ids[i]);
                    if (entity.NotUseInAnalyticalStatement == notuse[i]) continue;
                    entity.NotUseInAnalyticalStatement = notuse[i];
                    DeductionDao.Save(entity);
                }
                DeductionDao.Flush();
            }
            catch (Exception ex)
            {
                Log.Error("При изменении записи произошла ошибка.", ex);
                DeductionDao.RollbackTran();
                return false;
            }
            DeductionDao.CommitTran();
            return true;
        }
        public bool SaveDeductionEditModel(DeductionEditModel model, bool EnableSendEmail, out string error)
        {
            error = string.Empty;
            //User user = null;
            try
            {
                //user = UserDao.Load(model.UserId);
                IUser current = AuthenticationService.CurrentUser;
                if (!CheckDeductionUserRights(current))
                    throw new ArgumentException("Доступ запрещен.");
                Deduction deduction;
                if (model.Id == 0)
                {
                    deduction = new Deduction
                    {
                        Number = RequestNextNumberDao.GetNextNumberForType((int)RequestTypeEnum.Deduction),
                        Editor = UserDao.Load(current.Id),
                        EditDate = DateTime.Now,
                    };
                    ChangeEntityProperties(deduction, model);

                    if (EnableSendEmail)
                        SendEmailToUser(model, deduction);
                    DeductionDao.SaveAndFlush(deduction);
                    model.Id = deduction.Id;
                }
                else
                {
                    deduction = DeductionDao.Load(model.Id);
                    if (deduction.Version != model.Version)
                    {
                        error = "Заявка была изменена другим пользователем.";
                        model.ReloadPage = true;
                        return false;
                    }
                    ChangeEntityProperties(deduction, model);
                    if (model.IsDelete)
                    {
                        if ((current.UserRole & UserRole.OutsourcingManager) == UserRole.OutsourcingManager || (current.UserRole & UserRole.Estimator) == UserRole.Estimator)
                            deduction.DeleteAfterSendTo1C = true;
                        //deduction.EditDate = DateTime.Now;
                        deduction.DeleteDate = DateTime.Now;
                        DeductionDao.SaveAndFlush(deduction);
                        model.IsDelete = false;
                        //SendEmailForUserRequest(deduction.User, current, deduction.Creator, true, deduction.Id,
                        //    deduction.Number, RequestTypeEnum.Absence, false);
                    }
                    else
                        DeductionDao.SaveAndFlush(deduction);

                    if (deduction.Version != model.Version)
                    {
                        deduction.EditDate = DateTime.Now;
                        deduction.Editor = UserDao.Load(current.Id);
                        if (EnableSendEmail)
                            SendEmailToUser(model, deduction);
                        DeductionDao.SaveAndFlush(deduction);
                    }
                    if (deduction.DeleteDate.HasValue)
                        model.IsDeleted = true;
                }
                model.DocumentNumber = deduction.Number.ToString();
                model.Version = deduction.Version;

                SetFlagsState(deduction.Id, deduction, model);
                return true;
            }
            catch (Exception ex)
            {
                DeductionDao.RollbackTran();
                Log.Error("Error on SaveDeductionEditModel:", ex);
                error = string.Format("Исключение:{0}", ex.GetBaseException().Message);
                return false;
            }
            finally
            {
                //SetDeductionUserInfoModel(model, model.UserId);
                ReloadDictionariesToModel(model);
                SetHiddenFields(model);
            }
        }

        protected void SendEmailToUser(DeductionEditModel model, Deduction deduction)
        {
            User user = UserDao.Load((model != null) ? model.UserId : deduction.User.Id);
            if (string.IsNullOrEmpty(user.Email))
            {
                Log.ErrorFormat("E-mail is empty for user {0}", user.Id);
                return;
            }
            if (!deduction.DeleteDate.HasValue)
            {
                string defaultEmail = ConfigurationService.DefaultDeductionEmail;
                string to = string.IsNullOrEmpty(defaultEmail) ? user.Email : defaultEmail;
                string roubles = ((int)deduction.Sum) + " " + GetRubleSumAsString((int)deduction.Sum);
                int cp = ((int)(deduction.Sum * 100) % 100);
                string copeck = (cp).ToString("D2") + " " + GetCopeckSumAsString(cp);
                string body =
                    string.Format(
                        @"Из Вашей  заработной платы  будет произведено  удержание  № {5} в сумме  {0} {1}, вид удержания - {2}. Автор:{3} E-mail: {4}.",
                roubles, copeck, deduction.Kind.Name, deduction.Editor.FullName, deduction.Editor.Email, deduction.Number);
                EmailDto dto = SendEmail(to, "Удержание", body);
                if (string.IsNullOrEmpty(dto.Error))
                    deduction.EmailSendToUserDate = DateTime.Now;
                else
                    Log.ErrorFormat("Cannot send email to user {0}(email {1}) about deduction {2} : {3}",
                        user.Id, to, deduction.Id, dto.Error);
            }
        }
        public bool SendNotifyEmailToUser(int MissionReportId)
        {
            var mr = MissionReportDao.Load(MissionReportId);
            if (String.IsNullOrWhiteSpace(mr.User.Email)) return false;
            var creator = UserDao.Load(CurrentUser.Id);
            String body = String.Format("По авансовому отчёту №{0} с Вас будет удержанно {1:0.00} рублей. \r\n Автор: {2}, e-mail: {3}."
                , mr.Number
                , mr.UserSumReceived + mr.PurchaseBookAllSum - mr.AccountantAllSum
                , creator.Name
                , creator.Email);
            EmailDto dto = SendEmail(mr.User.Email, "Удержание", body);
            if (string.IsNullOrEmpty(dto.Error))
                return true;
            else
                return false;
        }
        public bool sendEmail(string to, string subj, string body)
        {
            EmailDto dto = SendEmail(to, subj, body);
            if (string.IsNullOrEmpty(dto.Error))
                return true;
            else
                return false;
        }
        protected void ChangeEntityProperties(Deduction entity, DeductionEditModel model)
        {
            if (model.IsEditable)
            {
                entity.DeductionDate = new DateTime(model.MonthId / 100, model.MonthId % 100, 1);
                entity.Kind = DeductionKindDao.Load(model.KindId);
                entity.Type = DeductionTypeDao.Load(model.TypeId);
                entity.User = UserDao.Load(model.UserId);
                entity.Sum = ((Decimal)((int)((Decimal.Parse(model.Sum)) * 100))) / 100;
                if (model.TypeId != (int)DeductionTypeEnum.Deduction)
                {
                    entity.DismissalDate = model.DismissalDate;
                    entity.IsFastDismissal = model.IsFastDismissal;
                }
                else
                {
                    entity.DismissalDate = null;
                    entity.IsFastDismissal = null;
                }
            }
        }
        #endregion

        #region Terra Graphics
        public TerraGraphicsSetShortNameModel SetShortNameModel()
        {
            TerraGraphicsSetShortNameModel model = new TerraGraphicsSetShortNameModel();
            List<TerraPoint> l1 = LoadTpListForLevelAndParentId(1, string.Empty);
            model.Level1 = l1.ConvertAll(x => new IdNameDto { Id = x.Id, Name = x.Name });
            TerraPoint p1 = l1[0];
            List<TerraPoint> l2 = LoadTpListForLevelAndParentId(2, p1.Code1C);//TerraPointDao.FindByLevelAndParentId(2, p1.Code1C).ToList();
            model.Level2 = l2.ConvertAll(x => new IdNameDto { Id = x.Id, Name = x.Name });
            TerraPoint p2 = l2[0];
            List<TerraPoint> l3 = LoadTpListForLevelAndParentId(3, p2.Code1C);//TerraPointDao.FindByLevelAndParentId(3, p2.Code1C).ToList();
            model.Level3 = l3.ConvertAll(x => new IdNameDto { Id = x.Id, Name = x.Name + (!string.IsNullOrEmpty(x.ShortName) ? " ( " + x.ShortName + " )" : string.Empty) });
            TerraPoint p3 = l3[0];
            model.ShortName = p3.ShortName;
            UserRole role = CurrentUser.UserRole;
            model.IsShortNameEditable = ((role & UserRole.Manager) > 0);
            return model;
        }
        protected List<TerraPoint> LoadTpListForLevelAndParentId(int level, string parentId)
        {
            List<TerraPoint> l = TerraPointDao.FindByLevelAndParentId(level, parentId).ToList();
            if (l.Count == 0)
                throw new ArgumentException(string.Format("Не могу найти ни одной точки для уровня {0}", level));
            return l;//l.ConvertAll(x => new IdNameDto { Id = x.Id, Name = x.Name });
        }
        public TerraPointChildrenDto GetTerraPointChildren(int parentId, int level)
        {
            bool isHoliday = false;
            try
            {
                TerraPoint parent = TerraPointDao.Load(parentId);
                if (parent == null)
                    throw new ArgumentException(string.Format("Точка с Id {0} отсутствует в базе данных", parentId));
                isHoliday = parent.IsHoliday;
                List<IdNameDto> children;
                List<IdNameDto> level3Children = new List<IdNameDto>();
                string shortName;
                if (level == 2)
                {
                    List<TerraPoint> l2 = LoadTpListForLevelAndParentId(2, parent.Code1C);//TerraPointDao.FindByLevelAndParentId(2, parent.Code1C).ToList();
                    children = l2.ConvertAll(x => new IdNameDto { Id = x.Id, Name = x.Name });
                    TerraPoint p2 = l2[0];
                    List<TerraPoint> l3 = LoadTpListForLevelAndParentId(3, p2.Code1C);//TerraPointDao.FindByLevelAndParentId(3, p2.Code1C).ToList();
                    level3Children = l3.ConvertAll(x => new IdNameDto { Id = x.Id, Name = x.Name + (!string.IsNullOrEmpty(x.ShortName) ? " ( " + x.ShortName + " )" : string.Empty) });
                    TerraPoint p3 = l3[0];
                    shortName = p3.ShortName;
                }
                else if (level == 3)
                {
                    List<TerraPoint> l3 = LoadTpListForLevelAndParentId(3, parent.Code1C);//TerraPointDao.FindByLevelAndParentId(3, parent.Code1C).ToList();
                    children = l3.ConvertAll(x => new IdNameDto { Id = x.Id, Name = x.Name + (!string.IsNullOrEmpty(x.ShortName) ? " ( " + x.ShortName + " )" : string.Empty) });
                    //if (l3.Count == 0)
                    //    throw new ArgumentException(string.Format("GetTerraPointChildren:Не найдено ни одной точки для уровня 3 и ParentId {0}", parent.Code1C));
                    TerraPoint p3 = l3[0];
                    shortName = p3.ShortName;
                }
                else
                    throw new ValidationException(string.Format("Неизвестный уровень точки {0}", level));
                return new TerraPointChildrenDto
                {
                    Error = string.Empty,
                    Children = children,
                    Level3Children = level3Children,
                    ShortName = shortName,
                    IsHoliday = isHoliday,
                };
            }
            catch (Exception ex)
            {
                Log.Error("Exception on GetTerraPointChildren:", ex);
                return new TerraPointChildrenDto
                {
                    Error = string.Format("Ошибка: {0}", ex.GetBaseException().Message),
                    Children = new List<IdNameDto>(),
                };
            }
        }
        public TerraPointShortNameDto GetTerraPointShortName(int pointId)
        {
            try
            {
                TerraPoint point = TerraPointDao.Load(pointId);
                if (point == null)
                    throw new ArgumentException(string.Format("Точка с Id {0} отсутствует в базе данных", point));
                return new TerraPointShortNameDto
                {
                    Error = string.Empty,
                    ShortName = point.ShortName,
                };
            }
            catch (Exception ex)
            {
                Log.Error("Exception on GetTerraPointShortName:", ex);
                return new TerraPointShortNameDto
                {
                    Error = string.Format("Ошибка: {0}", ex.GetBaseException().Message),
                    ShortName = string.Empty,
                };
            }
        }
        public TerraPointShortNameDto SaveTerraPointShortName(int pointId, string shortName)
        {
            try
            {
                TerraPoint point = TerraPointDao.Load(pointId);
                if (point == null)
                    throw new ArgumentException(string.Format("Точка с Id {0} отсутствует в базе данных", point));
                point.ShortName = shortName;
                TerraPointDao.SaveAndFlush(point);
                return new TerraPointShortNameDto
                {
                    Error = string.Empty,
                    ShortName = point.ShortName,
                };
            }
            catch (Exception ex)
            {
                Log.Error("Exception on SaveTerraPointShortName:", ex);
                return new TerraPointShortNameDto
                {
                    Error = string.Format("Ошибка: {0}", ex.GetBaseException().Message),
                    ShortName = string.Empty,
                };
            }
        }
        public void DeleteTerraPoint(int id)
        {
            if (id == 0)
                throw new ArgumentException("Невозможно удалить новую точку");
            //TerraGraphic tg = TerraGraphicDao.Load(id);
            //if (tg == null)
            //    throw new ArgumentException(string.Format("Точка (ID {0}) отсутствует в базе данных", id));
            TerraGraphicDao.DeleteAndFlush(id);
        }
        public void SaveTerraPoint(TerraPointSaveModel model)
        {
            TerraGraphic tg;
            if (model.Id == 0)
            {
                tg = new TerraGraphic { Day = model.TpDay, UserId = model.UserId };
            }
            else
            {
                tg = TerraGraphicDao.Load(model.Id);
                if (tg == null)
                    throw new ArgumentException(string.Format("Точка (ID {0}) отсутствует в базе данных", model.Id));
            }

            // Если дата точки не больше текущей даты, Факт заполняется значениями, внесенными пользователем
            if (model.TpDay.Date < DateTime.Today)
            {
                tg.FactPointId = model.FactPointId == 0 ? new int?() : model.FactPointId;
                tg.FactHours = string.IsNullOrEmpty(model.FactHours) ? new decimal?() : model.TpFactHours;
                tg.IsFactCreditAvailable = GetIsCreditAvailable(model.IsFactCreditAvailable);
            }
            else
            {
                tg.Hours = string.IsNullOrEmpty(model.Hours) ? new decimal?() : model.TpHours; //model.TpHours;
                tg.IsCreditAvailable = GetIsCreditAvailable(model.IsCreditAvailable);
                tg.IsFactCreditAvailable = GetIsCreditAvailable(model.IsFactCreditAvailable);
                tg.PointId = model.PointId == 0 ? new int?() : model.PointId;
                tg.FactPointId = model.FactPointId == 0 ? new int?() : model.FactPointId;
                tg.FactHours = string.IsNullOrEmpty(model.FactHours) ? new decimal?() : model.TpFactHours;
            }
            TerraGraphicDao.SaveAndFlush(tg);
            model.Error = string.Empty;
        }
        public bool? GetIsCreditAvailable(int credits)
        {
            switch (credits)
            {
                case 0:
                    return new bool?();
                case 1:
                    return true;
                case 2:
                    return false;
                default:
                    throw new ArgumentException("Неизвестное значение поля 'Кредиты'");
            }
        }
        public int GetCredits(bool? isCreditAvailable)
        {
            if (!isCreditAvailable.HasValue)
                return 0;
            return isCreditAvailable.Value ? 1 : 2;
        }
        public TerraPointSetDefaultTerraPointModel SetDefaultTerraPoint(int pointId/*,int userId*/)
        {
            int userId = CurrentUser.Id;
            TerraPoint tp = TerraPointDao.Load(pointId);
            if (tp == null)
                throw new ValidationException(string.Format("Точка (ID {0}) не найдена в базе данных", pointId));
            TerraPointToUser tpToUser = TerraPointToUserDao.FindByUserId(userId);
            if (tpToUser == null)
                tpToUser = new TerraPointToUser
                {
                    TerraPoint = tp,
                    UserId = userId,
                };

            else
                tpToUser.TerraPoint = tp;
            TerraPointToUserDao.SaveAndFlush(tpToUser);
            return new TerraPointSetDefaultTerraPointModel { Error = string.Empty, PointToUserId = tpToUser.Id };
        }

        public void SetEditPointDialogModel(TerraGraphicsEditPointModel model)
        {
            SetupCreditsCombo(model);
            User user = UserDao.Load(model.UserId);
            if (user == null)
                throw new ArgumentException(string.Format("Сотрудник (Id {0}) не найден в базе данных", model.UserId));
            if ((user.RoleId & (int)UserRole.Employee) == 0)
                throw new ArgumentException(string.Format("Пользователь {1}(Id {0}) не является сотрудником", model.UserId, user.Name));
            model.IsCreditsEditable = user.GivesCredit;
            List<TerraPoint> l1 = LoadTpListForLevelAndParentId(1, string.Empty);
            List<IdNameDto> l1List = l1.ConvertAll(x => new IdNameDto { Id = x.Id, Name = x.Name });
            //l1List.Add(new IdNameDto {Id = -1,Name = ""});
            model.FactEpLevel1 = l1List;
            if (model.Id == 0 && model.tpDay.Date < DateTime.Today)
            {
                List<IdNameDto> l1List1 = l1.ConvertAll(x => new IdNameDto { Id = x.Id, Name = x.Name });
                l1List1.Add(new IdNameDto { Id = 0, Name = string.Empty });
                model.EpLevel1 = l1List1;
            }
            else
                model.EpLevel1 = l1List;


            model.IsDeleteEnable = model.tpDay.Date >= DateTime.Today && model.Id > 0;
            if (model.tpDay.Date >= DateTime.Today)
            {
                model.IsPlanEditable = true;

            }
            if (model.tpDay.Date <= DateTime.Today)
                model.IsFactVisible = true;



            if (model.Id == 0)
            {
                if (model.tpDay.Date < DateTime.Today)
                {
                    model.EpLevel1ID = 0;
                    model.EpLevel2 = new List<IdNameDto>();
                    model.EpLevel3 = new List<IdNameDto>();
                    TerraPoint p1 = l1[0];
                    List<TerraPoint> l2 = LoadTpListForLevelAndParentId(2, p1.Code1C);//TerraPointDao.FindByLevelAndParentId(2, p1.Code1C).ToList();
                    //model.EpLevel2 = l2.ConvertAll(x => new IdNameDto { Id = x.Id, Name = x.Name });
                    model.FactEpLevel2 = l2.ConvertAll(x => new IdNameDto { Id = x.Id, Name = x.Name });
                    TerraPoint p2 = l2[0];
                    List<TerraPoint> l3 = LoadTpListForLevelAndParentId(3, p2.Code1C);//TerraPointDao.FindByLevelAndParentId(3, p2.Code1C).ToList();
                    //model.EpLevel3 = l3.ConvertAll(x => new IdNameDto { Id = x.Id, Name = x.Name + (!string.IsNullOrEmpty(x.ShortName) ? " ( " + x.ShortName + " )" : string.Empty) });
                    model.FactEpLevel3 = l3.ConvertAll(x => new IdNameDto { Id = x.Id, Name = x.Name + (!string.IsNullOrEmpty(x.ShortName) ? " ( " + x.ShortName + " )" : string.Empty) });
                    TerraPoint p3 = l3[0];
                    if (p3.IsHoliday)
                        model.IsPlanHoliday = true;
                }
                else
                {
                    TerraPointToUser tpToUser = TerraPointToUserDao.FindByUserId(CurrentUser.Id);
                    if (tpToUser == null)
                    {
                        TerraPoint p1 = l1[0];
                        List<TerraPoint> l2 = LoadTpListForLevelAndParentId(2, p1.Code1C);//TerraPointDao.FindByLevelAndParentId(2, p1.Code1C).ToList();
                        model.EpLevel2 = l2.ConvertAll(x => new IdNameDto { Id = x.Id, Name = x.Name });
                        model.FactEpLevel2 = l2.ConvertAll(x => new IdNameDto { Id = x.Id, Name = x.Name });
                        TerraPoint p2 = l2[0];
                        List<TerraPoint> l3 = LoadTpListForLevelAndParentId(3, p2.Code1C);//TerraPointDao.FindByLevelAndParentId(3, p2.Code1C).ToList();
                        model.EpLevel3 = l3.ConvertAll(x => new IdNameDto { Id = x.Id, Name = x.Name + (!string.IsNullOrEmpty(x.ShortName) ? " ( " + x.ShortName + " )" : string.Empty) });
                        model.FactEpLevel3 = l3.ConvertAll(x => new IdNameDto { Id = x.Id, Name = x.Name + (!string.IsNullOrEmpty(x.ShortName) ? " ( " + x.ShortName + " )" : string.Empty) });
                        TerraPoint p3 = l3[0];
                        if (p3.IsHoliday)
                            model.IsPlanHoliday = true;
                    }
                    else
                    {
                        LoadTerraPoints23Level(tpToUser.TerraPoint.ParentId, tpToUser.TerraPoint.Id, model, false);
                        LoadTerraPoints23Level(tpToUser.TerraPoint.ParentId, tpToUser.TerraPoint.Id, model, true);
                    }
                }
            }
            else
            {
                TerraGraphic tg = TerraGraphicDao.Load(model.Id);
                model.Credit = GetCredits(tg.IsCreditAvailable);
                model.FactCredit = GetCredits(tg.IsFactCreditAvailable);
                // model.FactCredit = GetCredits(tg.IsFactCreditAvailable);
                model.Day = tg.Day.ToString("dd.MM.yyyy");
                model.IsEditable = true;
                //model.UserId = tg.UserId;
                model.Hours = FormatSum(tg.Hours);//tg.Hours.ToString();
                model.FactHours = FormatSum(tg.FactHours);//tg.FactHours.HasValue?tg.Hours.ToString():string.Empty;
                if (!tg.PointId.HasValue)
                {
                    model.EpLevel1ID = 0;
                    model.EpLevel1 = new List<IdNameDto>();
                    model.EpLevel2 = new List<IdNameDto>();
                    model.EpLevel3 = new List<IdNameDto>();
                }
                else
                {
                    TerraPoint tp = TerraPointDao.Load(tg.PointId.Value);
                    if (tp == null)
                        throw new ArgumentException(string.Format("Точка (ID {0}) отсутствует в базе данных", tg.PointId));
                    LoadTerraPoints23Level(tp.ParentId, tp.Id, model, false);
                }

                if (!tg.FactPointId.HasValue)
                {
                    TerraPoint p1 = l1[0];
                    List<TerraPoint> l2 = LoadTpListForLevelAndParentId(2, p1.Code1C);//TerraPointDao.FindByLevelAndParentId(2, p1.Code1C).ToList();
                    //model.EpLevel2 = l2.ConvertAll(x => new IdNameDto { Id = x.Id, Name = x.Name });
                    model.FactEpLevel2 = l2.ConvertAll(x => new IdNameDto { Id = x.Id, Name = x.Name });
                    TerraPoint p2 = l2[0];
                    List<TerraPoint> l3 = LoadTpListForLevelAndParentId(3, p2.Code1C);//TerraPointDao.FindByLevelAndParentId(3, p2.Code1C).ToList();
                    //model.EpLevel3 = l3.ConvertAll(x => new IdNameDto { Id = x.Id, Name = x.Name + (!string.IsNullOrEmpty(x.ShortName) ? " ( " + x.ShortName + " )" : string.Empty) });
                    model.FactEpLevel3 = l3.ConvertAll(x => new IdNameDto { Id = x.Id, Name = x.Name + (!string.IsNullOrEmpty(x.ShortName) ? " ( " + x.ShortName + " )" : string.Empty) });
                    TerraPoint p3 = l3[0];
                    if (p3.IsHoliday)
                        model.IsPlanHoliday = true;
                    //List<IdNameDto> l1List1 = l1.ConvertAll(x => new IdNameDto { Id = x.Id, Name = x.Name });
                    //l1List1.Add(new IdNameDto { Id = 0, Name = string.Empty });
                    //model.FactEpLevel1 = l1List1;
                    //model.FactEpLevel1ID = 0;
                    //model.FactEpLevel2 = new List<IdNameDto>();
                    //model.FactEpLevel3 = new List<IdNameDto>(); 
                }
                else
                {
                    TerraPoint tp = TerraPointDao.Load(tg.FactPointId.Value);
                    if (tp == null)
                        throw new ArgumentException(string.Format("Фактическая точка (ID {0}) отсутствует в базе данных", tg.FactPointId));
                    LoadTerraPoints23Level(tp.ParentId, tp.Id, model, true);
                }
            }
        }
        protected void LoadTerraPoints23Level(string parentId, int level3Id, TerraGraphicsEditPointModel model, bool factPoint)
        {
            List<TerraPoint> l3 = LoadTpListForLevelAndParentId(3, parentId);
            if (factPoint)
            {
                model.FactEpLevel3 = l3.ConvertAll(x => new IdNameDto { Id = x.Id, Name = x.Name + (!string.IsNullOrEmpty(x.ShortName) ? " ( " + x.ShortName + " )" : string.Empty) });
                model.FactEpLevel3ID = level3Id;
            }
            else
            {
                model.EpLevel3 = l3.ConvertAll(x => new IdNameDto { Id = x.Id, Name = x.Name + (!string.IsNullOrEmpty(x.ShortName) ? " ( " + x.ShortName + " )" : string.Empty) });
                model.EpLevel3ID = level3Id;
            }
            TerraPoint l3Point = TerraPointDao.Load(level3Id);
            if (l3Point == null)
                throw new ArgumentException(string.Format("Точка (ID {0}) отсутствует в базе данных", level3Id));
            if (factPoint)
            {
                if (l3Point.IsHoliday)
                    model.IsFactHoliday = true;
            }
            else
            {
                if (l3Point.IsHoliday)
                    model.IsPlanHoliday = true;
            }
            IdNameDto tp2 = LoadByCode1AndPath(parentId, l3Point.Path);
            List<TerraPoint> l2 = LoadTpListForLevelAndParentId(2, tp2.Name);
            if (factPoint)
            {
                model.FactEpLevel2 = l2.ConvertAll(x => new IdNameDto { Id = x.Id, Name = x.Name });
                model.FactEpLevel2ID = tp2.Id;
            }
            else
            {
                model.EpLevel2 = l2.ConvertAll(x => new IdNameDto { Id = x.Id, Name = x.Name });
                model.EpLevel2ID = tp2.Id;
            }
            TerraPoint tp1 = LoadByCode1C(tp2.Name);
            if (factPoint)
                model.FactEpLevel1ID = tp1.Id;
            else
                model.EpLevel1ID = tp1.Id;
        }
        protected TerraPoint LoadByCode1C(string code1C)
        {
            TerraPoint terraPoint = TerraPointDao.FindByCode1C(code1C);
            if (terraPoint == null)
                throw new ArgumentException(string.Format("Точка с Code1C {0} отсутствует в базе данных", code1C));
            return terraPoint;
        }
        protected IdNameDto LoadByCode1AndPath(string code1C, string path)
        {
            IdNameDto terraPoint = TerraPointDao.FindByCode1CAndPath(code1C, path);
            if (terraPoint == null)
                throw new ArgumentException(string.Format("Точка с Code1C {0} и путем {1} отсутствует в базе данных", code1C, path));
            return terraPoint;
        }
        protected void SetupCreditsCombo(TerraGraphicsEditPointModel model)
        {
            model.Credits = new List<IdNameDto>
                                {
                                    new IdNameDto{Id = 0,Name = string.Empty},
                                    new IdNameDto{Id = 1,Name = "Да"},
                                    new IdNameDto{Id = 2,Name = "Нет"},
                                };
        }
        #endregion

        #region Mission Order
        public CreateMissionOrderModel GetCreateMissionOrderModel()
        {
            CreateMissionOrderModel model = new CreateMissionOrderModel();
            User currentUser = UserDao.Load(CurrentUser.Id);
            if (currentUser == null)
                throw new ArgumentException(string.Format("Не могу загрузить пользователя {0} из базы даннных",
                    CurrentUser.Id));
            IList<IdNameDto> list;
            switch (currentUser.Level)
            {
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                    list = UserDao.GetManagersAndEmployeesForCreateMissionOrder(currentUser.Department.Path, currentUser.Level.Value);
                    model.Users = list;
                    break;
            }
            return model;
        }
        public MissionOrderListModel GetMissionOrderListModel()
        {
            User user = UserDao.Load(AuthenticationService.CurrentUser.Id);
            IdNameReadonlyDto dep = GetDepartmentDto(user);
            MissionOrderListModel model = new MissionOrderListModel
            {
                UserId = AuthenticationService.CurrentUser.Id,
                DepartmentName = dep.Name,
                DepartmentId = dep.Id,
                DepartmentReadOnly = dep.IsReadOnly,
                //RequestStatuses = GetDeductionStatuses(true),
                //Types = GetDeductionTypes(true)
            };
            SetInitialDates(model);
            SetDictionariesToModel(model);
            SetInitialStatus(model);
            SetIsAvailable(model);
            return model;
        }
        protected void SetInitialStatus(MissionOrderListModel model)
        {
            switch (CurrentUser.UserRole)
            {
                case UserRole.Director:
                    model.StatusId = 8;
                    break;
                case UserRole.Manager:
                    model.StatusId = 7;
                    break;
                default:
                    break;
            }
        }
        protected void SetIsAvailable(MissionOrderListModel model)
        {
            switch (CurrentUser.UserRole)
            {
                case UserRole.Manager:
                    User currentUser = UserDao.Load(CurrentUser.Id);
                    if (currentUser == null)
                        throw new ArgumentException(string.Format("Не могу загрузить пользователя {0} из базы даннных",
                            CurrentUser.Id));
                    model.IsAddAvailable = ((currentUser.UserRole & UserRole.Manager) == UserRole.Manager) && currentUser.Level.HasValue && currentUser.Level >= 3;
                    model.IsApproveAvailable = model.IsAddAvailable || (currentUser.ManualRoleRecords
                        .Where<ManualRoleRecord>(mrr => mrr.Role.Id == 1)
                        .FirstOrDefault<ManualRoleRecord>() != null);
                    break;
                case UserRole.Director:
                    model.IsApproveAvailable = true;
                    break;
            }

            int? superPersonnelId = ConfigurationService.SuperPersonnelId;
            if ((superPersonnelId.HasValue && superPersonnelId.Value == CurrentUser.Id)
                || ((CurrentUser.UserRole & UserRole.OutsourcingManager) == UserRole.OutsourcingManager)
                || ((CurrentUser.UserRole & UserRole.Estimator) == UserRole.Estimator)
                || (CurrentUser.UserRole == UserRole.Accountant))
            {
                model.IsCorrectionsOnlyModeAvailable = true;
            }
            if (superPersonnelId.HasValue && superPersonnelId.Value == CurrentUser.Id || (CurrentUser.UserRole & UserRole.Estimator) == UserRole.Estimator)
            {
                model.IsRecalculationAvailable = true;
            }
        }
        public void SetMissionOrderListModel(MissionOrderListModel model, bool hasError)
        {
            SetDictionariesToModel(model);
            SetIsAvailable(model);
            User user = UserDao.Load(model.UserId);
            //model.RequestStatuses = GetDeductionStatuses(true);
            //model.Types = GetDeductionTypes(true);
            if (hasError)
                model.Documents = new List<MissionOrderDto>();
            else
            {
                if (model.IsApproveClick && model.Documents != null)
                {
                    model.IsApproveClick = false;
                    List<int> idsForApprove = model.Documents.Where(x => x.IsChecked).Select(x => x.Id).ToList();
                    ApproveOrders(model, idsForApprove);
                }
                if (model.IsSaveIsRecalculatedClick && model.Documents != null)
                {
                    model.IsSaveIsRecalculatedClick = false;
                    List<int> idsToSetIsRecalculated = model.Documents.Where(x => x.IsRecalculated).Select(x => x.Id).ToList();
                    List<int> idsToResetIsRecalculated = model.Documents.Where(x => !x.IsRecalculated).Select(x => x.Id).ToList();
                    SaveIsRecalculated(model, idsToSetIsRecalculated, idsToResetIsRecalculated);
                }
                SetDocumentsToModel(model, user);
            }
            //model.Documents = new List<MissionOrderDto>();
        }
        /// <summary>
        /// При добавлении/редактировании счетов гостиниц проверяем поля на правильность заполнения.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="hasError"></param>
        public void CheckFillFields(MissionHotelsEditModel model, System.Web.Mvc.ModelStateDictionary ms)
        {
            if (model.Name == null)
                ms.AddModelError("Name", "Заполните поле 'Наименование контрагента'");
            if (model.Name != null && model.Name.Trim().Length > 256)
                ms.AddModelError("Name", "Превышено допустимое количество символов!");
            if (model.Account == null)
                ms.AddModelError("Account", "Заполните поле '№ счета'");
            if (model.Account != null && model.Account.Trim().Length > 32)
                ms.AddModelError("Account", "Превышено допустимое количество символов!");

            UserRole role = CurrentUser.UserRole;
            MissionHotelsModel md = new MissionHotelsModel();
            md.Documents = MissionHotelDao.GetHotels(role, md.Id, md.Name, md.Account);
            int i = 0;
            foreach (MissionHotelsDto mhdto in md.Documents)
            {
                if (mhdto.Account == model.Account)
                    i = i + 1;
            }
            if (i > (int)(model.flgNew.HasValue && model.flgNew.Value ? 0 : 1)) ms.AddModelError("Account", "В базе данных уже есть запись с таким номером счета!");
        }
        /// <summary>
        /// Загрузка списка гостиниц.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="hasError"></param>
        public void SetMissionHotelsListModel(MissionHotelsModel model, bool hasError)
        {
            SetHotelListToModel(model);
        }
        /// <summary>
        /// Гостиницы
        /// </summary>
        /// <param name="model"></param>
        public void SetHotelListToModel(MissionHotelsModel model)
        {
            UserRole role = CurrentUser.UserRole;
            model.Documents = MissionHotelDao.GetHotels(role,
                model.Id,
                model.Name,
                model.Account);
        }
        /// <summary>
        /// Сохраняем запись в базе данных.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public bool SaveMissionHotelsEditModel(MissionHotelsEditModel model, out string error)
        {
            error = string.Empty;
            MissionHotels missionHotels = MissionHotelDao.Get(model.Id);
            try
            {
                if (missionHotels == null)
                {
                    missionHotels = new MissionHotels
                    {
                        Id = model.Id,
                        Name = model.Name,
                        Account = model.Account
                    };
                }
                else
                {
                    missionHotels.Id = model.Id;
                    missionHotels.Name = model.Name;
                    missionHotels.Account = model.Account;
                }
                //ChangeEntityProperties(current, missionHotels, model, user);
                MissionHotelDao.SaveAndFlush(missionHotels);
                model.Id = missionHotels.Id;

                return true;
            }
            catch (Exception ex)
            {
                MissionHotelDao.RollbackTran();
                Log.Error("Error on SaveMissionOrderEditModel:", ex);
                error = string.Format("Исключение:{0}", ex.GetBaseException().Message);
                return false;
            }
            finally
            {
                //SetUserInfoModel(user, model);
                //SetStaticFields(model, missionHotels);
                //LoadDictionaries(model);
                //SetHiddenFields(model);
            }
        }
        protected void ApproveOrders(MissionOrderListModel model, List<int> idsForApprove)
        {
            List<MissionOrder> orders = MissionOrderDao.LoadForIdsList(idsForApprove).ToList();
            foreach (MissionOrder order in orders)
                ApproveOrder(model, order);
        }
        protected void SaveIsRecalculated(MissionOrderListModel model, List<int> idsToSetIsRecalculated, List<int> idsToResetIsRecalculated)
        {
            List<MissionOrder> orders = MissionOrderDao.LoadForIdsList(idsToSetIsRecalculated).ToList();
            if (CurrentUser.Id == ConfigurationService.SuperPersonnelId || (CurrentUser.UserRole & UserRole.Estimator) == UserRole.Estimator)
            {
                foreach (MissionOrder order in orders)
                {
                    order.IsRecalculated = true;
                    MissionOrderDao.SaveAndFlush(order);
                }

                orders = MissionOrderDao.LoadForIdsList(idsToResetIsRecalculated).ToList();
                foreach (MissionOrder order in orders)
                {
                    order.IsRecalculated = false;
                    MissionOrderDao.Save(order);
                }
                MissionOrderDao.Flush();
            }
        }
        protected void ApproveOrder(MissionOrderListModel model, MissionOrder order)
        {
            try
            {
                if (MissionOrderDao.CheckOtherOrdersExists(order.Id, order.User.Id, order.BeginDate.Value, order.EndDate.Value) ||
                    !CheckOrderBeginDate(order.BeginDate.Value.ToString()))
                {
                    model.HasErrors = true;
                    Log.ErrorFormat("Cannot approve order {0} user {1} role {2} - order check fail", order.Id, CurrentUser.Id, CurrentUser.UserRole);
                    return;
                }
                switch (CurrentUser.UserRole)
                {
                    case UserRole.Manager:
                        ApproveOrderForManager(model, order);
                        break;
                    case UserRole.Director:
                        ApproveOrderForDirector(model, order);
                        break;
                    default:
                        model.HasErrors = true;
                        Log.ErrorFormat("Cannot approve order {0} user {1} role {2}", order.Id, CurrentUser.Id, CurrentUser.UserRole);
                        break;
                }
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("Cannot approve order {0} user {1} role {2}", order.Id, CurrentUser.Id, CurrentUser.UserRole), ex);
                model.HasErrors = true;
            }
        }
        protected void ApproveOrderForManager(MissionOrderListModel model, MissionOrder entity)
        {
            try
            {
                if (entity.UserDateAccept.HasValue && !entity.ManagerDateAccept.HasValue)
                {
                    bool canEdit;
                    if ((IsCurrentManagerForUser(entity.User, CurrentUser, out canEdit) && canEdit) || HasCurrentManualRoleForUser(entity.User, CurrentUser, UserManualRole.ApprovesMissionOrders, out canEdit))
                    {
                        entity.ManagerDateAccept = DateTime.Now;
                        entity.AcceptManager = UserDao.Load(CurrentUser.Id);
                        entity.EditDate = DateTime.Now;
                        if (!entity.NeedToAcceptByChief)
                        {
                            CreateMission(entity);
                            SendEmailForMissionOrderConfirm(CurrentUser, entity, false);
                        }
                        MissionOrderDao.SaveAndFlush(entity);
                    }
                    else
                    {
                        Log.ErrorFormat("Cannot approve order {0} user {1} role {2} - IsUserManagerForEmployee",
                            entity.Id, CurrentUser.Id, CurrentUser.UserRole);
                        model.HasErrors = true;
                    }
                }
                else
                {
                    Log.ErrorFormat("Cannot approve order {0} user {1} role {2} - entity.UserDateAccept.HasValue && !entity.ManagerDateAccept.HasValue",
                        entity.Id, CurrentUser.Id, CurrentUser.UserRole);
                    model.HasErrors = true;
                }
            }
            catch (Exception ex)
            {
                Log.Error("ApproveOrderForManager", ex);
                model.HasErrors = true;
            }
        }
        protected void ApproveOrderForDirector(MissionOrderListModel model, MissionOrder entity)
        {
            try
            {
                bool isAccept = false;
                if (entity.NeedToAcceptByChiefAsManager && entity.UserDateAccept.HasValue
                    && !entity.ManagerDateAccept.HasValue)
                {
                    User current = UserDao.Load(CurrentUser.Id);
                    entity.ManagerDateAccept = DateTime.Now;
                    entity.EditDate = DateTime.Now;
                    entity.AcceptManager = current;
                    if (entity.NeedToAcceptByChief)
                    {
                        entity.ChiefDateAccept = DateTime.Now;
                        entity.EditDate = DateTime.Now;
                        entity.AcceptChief = current;
                    }
                    CreateMission(entity);
                    SendEmailForMissionOrderConfirm(CurrentUser, entity, false);
                    MissionOrderDao.SaveAndFlush(entity);
                    isAccept = true;
                }
                if (entity.UserDateAccept.HasValue && entity.ManagerDateAccept.HasValue &&
                    entity.NeedToAcceptByChief && !entity.ChiefDateAccept.HasValue)
                {
                    entity.ChiefDateAccept = DateTime.Now;
                    entity.EditDate = DateTime.Now;
                    entity.AcceptChief = UserDao.Load(CurrentUser.Id);
                    CreateMission(entity);
                    SendEmailForMissionOrderConfirm(CurrentUser, entity, false);
                    MissionOrderDao.SaveAndFlush(entity);
                    isAccept = true;
                }
                if (!isAccept)
                {
                    Log.ErrorFormat("Cannot approve order {0} user {1} role {2}.", entity.Id, CurrentUser.Id
                        , CurrentUser.UserRole);
                    model.HasErrors = true;
                }
            }
            catch (Exception ex)
            {
                Log.Error("ApproveOrderForDirector", ex);
                model.HasErrors = true;
            }
        }
        public void SetDocumentsToModel(MissionOrderListModel model, User user)
        {
            UserRole role = CurrentUser.UserRole;
            model.Documents = MissionOrderDao.GetDocuments(user.Id,
                role,
                model.DepartmentId,
                model.StatusId,
                model.BeginDate,
                model.EndDate,
                model.UserName,
                model.Number,
                model.SortBy,
                model.SortDescending);
        }
        public void SetDictionariesToModel(MissionOrderListModel model)
        {
            model.Statuses = GetMoStatuses(CurrentUser.UserRole);
        }
        public List<IdNameDto> GetMoStatuses(UserRole adaptForRole = UserRole.NoRole)
        {
            List<IdNameDto> moStatusesList = new List<IdNameDto>
                                                       {
                                                           new IdNameDto(1, "Одобрен сотрудником"),
                                                           new IdNameDto(2, "Не одобрен сотрудником"),
                                                           new IdNameDto(3, "Одобрен руководителем"),
                                                           new IdNameDto(4, "Не одобрен руководителем"),
                                                           new IdNameDto(5, "Одобрен членом правления"),
                                                           new IdNameDto(6, "Не одобрен членом правления"),
                                                           new IdNameDto(8, "Требует одобрения руководителем"),
                                                           new IdNameDto(9, "Требует одобрения членом правления"),
                                                           new IdNameDto(10, "Выгружен в 1С"),
                                                       }.OrderBy(x => x.Name).ToList();
            switch (adaptForRole)
            {
                case UserRole.Manager:
                    // moStatusesList.Insert(0, new IdNameDto(7, "Требует моего одобрения"));
                    break;
                default:
                    break;
            }
            moStatusesList.Insert(0, new IdNameDto(0, SelectAll));

            return moStatusesList;
        }
        public MissionOrderEditModel GetMissionOrderEditModel(int id, int? userId)
        {
            if (id == 0 && !userId.HasValue)
            {
                if ((CurrentUser.UserRole & UserRole.Employee) == UserRole.Employee)
                    userId = CurrentUser.Id;
                else
                    throw new ValidationException("Не указан пользователь для приказа на командировку");
            }
            MissionOrder entity = null;
            if (id != 0)
            {
                entity = MissionOrderDao.Load(id);
                if (entity == null)
                    throw new ValidationException(string.Format("Не найден приказ на командировку (id {0}) в базе данных", id));
                if (entity.IsAdditional)
                    throw new ValidationException("Редактирование изменения приказа на командировку невозможно через форму приказа.");
            }
            MissionOrderEditModel model = new MissionOrderEditModel
            {
                Id = id,
                UserId = id == 0 ? userId.Value : entity.User.Id
            };
            User user = UserDao.Load(model.UserId);
            if (!user.Grade.HasValue)
                throw new ValidationException(string.Format("Не указан грейд для пользователя {0} в базе данных", user.Id));
            IUser current = AuthenticationService.CurrentUser;

            if (!CheckUserMoRights(user, current, id, entity, false))
                throw new ArgumentException("Доступ запрещен.");
            //model.CommentsModel = GetCommentsModel(id, (int)RequestTypeEnum.MissionOrder);
            if (id != 0)
            {

                LoadGraids(model, user.Grade.Value, entity, entity.CreateDate);
                model.AllSum = FormatSum(entity.AllSum);
                model.AllSumAir = FormatSum(entity.SumAir);
                model.AllSumDaily = FormatSum(entity.SumDaily);
                model.AllSumResidence = FormatSum(entity.SumResidence);
                model.AllSumTrain = FormatSum(entity.SumTrain);
                model.BeginMissionDate = FormatDate(entity.BeginDate);//entity.BeginDate.ToShortDateString();
                model.EndMissionDate = FormatDate(entity.EndDate);//entity.EndDate.ToShortDateString();
                model.GoalId = entity.Goal.Id;
                model.Id = entity.Id;
                model.TypeId = entity.Type.Id;
                model.Kind = entity.Kind;
                model.UserId = entity.User.Id;
                model.UserAllSum = FormatSum(entity.UserAllSum);
                model.UserAllSumAir = FormatSum(entity.UserSumAir);
                model.UserAllSumDaily = FormatSum(entity.UserSumDaily);
                model.UserAllSumResidence = FormatSum(entity.UserSumResidence);
                model.UserAllSumTrain = FormatSum(entity.UserSumTrain);
                model.DateCreated = entity.CreateDate.ToShortDateString();
                model.Version = entity.Version;
                model.UserSumCash = FormatSum(entity.UserSumCash);
                model.UserSumNotCash = FormatSum(entity.UserSumNotCash);
                var analytical = MissionOrderDao.GetAnalyticalStatementDetails(entity.User.Id,0,false);
                model.UserDept = (analytical != null && analytical.Any()) ? analytical.Last().SaldoEnd : 0f;//.Aggregate(0f,(sum,next)=>sum+ (next.Reported-next.Ordered));
                model.IsResidencePaid = entity.IsResidencePaid;
                model.IsAirTicketsPaid = entity.IsAirTicketsPaid;
                model.IsTrainTicketsPaid = entity.IsTrainTicketsPaid;

                model.ResidenceRequestNumber = entity.ResidenceRequestNumber;
                model.AirTicketsRequestNumber = entity.AirTicketsRequestNumber;
                model.TrainTicketsRequestNumber = entity.TrainTicketsRequestNumber;
                model.SecretaryFio = entity.Secretary == null ? string.Empty : entity.Secretary.FullName;
                model.AirTicketType = entity.AirTicketType;
                model.TrainTicketType = entity.TrainTicketType;

                model.IsChiefApproveNeed = IsMissionOrderLong(entity);//entity.NeedToAcceptByChief;
                model.LongTermReason = entity.LongTermReason;
                model.DocumentNumber = entity.Number.ToString();

                MissionOrderTargetModel[] targets = entity.Targets.ToList().ConvertAll(x => new MissionOrderTargetModel
                {
                    AirTicketTypeId = x.AirTicketType == null ? 0 : x.AirTicketType.Id,
                    AirTicketTypeName = x.AirTicketType == null ? string.Empty : x.AirTicketType.Name,
                    AllDaysCount = x.DaysCount.ToString(),
                    City = x.City,
                    Country = x.Country.Name,
                    CountryId = x.Country.Id,
                    DailyAllowanceId = x.DailyAllowance == null ? 0 : x.DailyAllowance.Id,
                    DailyAllowanceName = x.DailyAllowance == null ? string.Empty : x.DailyAllowance.Name,
                    DateFrom = x.BeginDate.ToShortDateString(),
                    DateTo = x.EndDate.ToShortDateString(),
                    Organization = x.Organization,
                    ResidenceId = x.Residence == null ? 0 : x.Residence.Id,
                    ResidenceName = x.Residence == null ? string.Empty : x.Residence.Name,
                    TargetDaysCount = x.RealDaysCount.ToString(),
                    TargetId = x.Id,
                    TrainTicketTypeId = x.TrainTicketType == null ? 0 : x.TrainTicketType.Id,
                    TrainTicketTypeName = x.TrainTicketType == null ? string.Empty : x.TrainTicketType.Name,
                }).ToArray();
                JsonList list = new JsonList { List = targets };
                JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
                model.Targets = jsonSerializer.Serialize(list);
                if (entity.DeleteDate.HasValue)
                    model.IsDeleted = true;
            }
            else
            {
                JsonList list = new JsonList { List = new MissionOrderTargetModel[0] };
                JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
                model.Targets = jsonSerializer.Serialize(list);
                model.DateCreated = DateTime.Today.ToShortDateString();
                LoadGraids(model, user.Grade.Value, entity, DateTime.Today);
                //model.IsEditable = true;
            }
            SetUserInfoModel(user, model, UserManualRole.ApprovesMissionOrders);
            SetFlagsState(id, user, entity, model);
            SetStaticFields(model, entity);
            LoadDictionaries(model);
            SetHiddenFields(model);
            return model;
        }

        public bool CheckOtherOrdersExists(MissionOrderEditModel model)
        {
            return MissionOrderDao.CheckOtherOrdersExists(model.Id, model.UserId, DateTime.Parse(model.BeginMissionDate),
                                                   DateTime.Parse(model.EndMissionDate));
        }
        public bool CheckAnyOtherOrdersExists(MissionOrderEditModel model)
        {
            return MissionOrderDao.CheckAnyOtherOrdersExists(model.Id, model.UserId, DateTime.Parse(model.BeginMissionDate),
                                                   DateTime.Parse(model.EndMissionDate));
        }
        public bool CheckOrderBeginDate(string beginMissionDate)
        {
            if (AuthenticationService.CurrentUser.UserRole == UserRole.Secretary)
                return true;
            DateTime beginDate = DateTime.Parse(beginMissionDate);
            DateTime first = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            if (DateTime.Today.Day < 6)
                first = first.AddMonths(-1);
            return beginDate >= first;
        }
        public bool SaveMissionOrderEditModel(MissionOrderEditModel model, out string error)
        {
            error = string.Empty;
            User user = null;
            MissionOrder missionOrder = null;
            try
            {
                user = UserDao.Load(model.UserId);
                IUser current = AuthenticationService.CurrentUser;
                if (model.Id != 0)
                    missionOrder = MissionOrderDao.Load(model.Id);
                if (!CheckUserMoRights(user, current, model.Id, missionOrder, true))
                {
                    error = "Редактирование заявки запрещено";
                    return false;
                }
                if (model.Id == 0)
                {
                    missionOrder = new MissionOrder
                    {
                        CreateDate = DateTime.Now,
                        Creator = UserDao.Load(current.Id),
                        Number = RequestNextNumberDao.GetNextNumberForType((int)RequestTypeEnum.MissionOrder),
                        User = user,
                        EditDate = DateTime.Now,
                    };
                    ChangeEntityProperties(current, missionOrder, model, user);
                    MissionOrderDao.SaveAndFlush(missionOrder);
                    model.Id = missionOrder.Id;
                }
                else
                {
                    if (missionOrder.Version != model.Version)
                    {
                        error = "Приказ был изменен другим пользователем.";
                        model.ReloadPage = true;
                        return false;
                    }
                    if (model.IsDelete)
                    {
                        if ((current.UserRole & UserRole.OutsourcingManager) == UserRole.OutsourcingManager || (current.UserRole & UserRole.Estimator) == UserRole.Estimator)
                            missionOrder.DeleteAfterSendTo1C = true;
                        missionOrder.DeleteDate = DateTime.Now;
                        //missionOrder.CreateDate = DateTime.Now;
                        MissionOrderDao.SaveAndFlush(missionOrder);
                        if (missionOrder.Mission != null)
                        {
                            Mission mission = missionOrder.Mission;
                            if (mission.SendTo1C.HasValue)
                                mission.DeleteAfterSendTo1C = true;
                            mission.DeleteDate = DateTime.Now;
                            mission.CreateDate = DateTime.Now;
                            MissionDao.SaveAndFlush(mission);
                        }
                        else
                            Log.WarnFormat("No mission for mission order with id {0}", missionOrder.Id);
                        MissionReport report = MissionReportDao.GetReportForOrder(missionOrder.Id);
                        if (report != null)
                        {
                            report.DeleteDate = DateTime.Now;
                            report.EditDate = DateTime.Now;
                            MissionReportDao.SaveAndFlush(report);
                        }
                        else
                            Log.WarnFormat("No mission report for mission order with id {0}", missionOrder.Id);
                        /*SendEmailForUserRequest(missionOrder.User, current, missionOrder.Creator, true, missionOrder.Id,
                            missionOrder.Number, RequestTypeEnum.ChildVacation, false);*/
                        model.IsDelete = false;
                    }
                    else
                    {
                        ChangeEntityProperties(current, missionOrder, model, user);
                        //List<string> cityList = missionOrder.Targets.Select(x => x.City).ToList();
                        //string country = GetStringForList(cityList);
                        //List<string> orgList = missionOrder.Targets.Select(x => x.Organization).ToList();
                        //string org = GetStringForList(orgList);
                        MissionOrderDao.SaveAndFlush(missionOrder);
                        if (missionOrder.Version != model.Version && !model.IsTicketsEditable)
                        {
                            missionOrder.EditDate = DateTime.Now;
                            MissionOrderDao.SaveAndFlush(missionOrder);
                        }
                    }
                    if (missionOrder.DeleteDate.HasValue)
                        model.IsDeleted = true;
                }
                model.DocumentNumber = missionOrder.Number.ToString();
                model.Version = missionOrder.Version;
                model.DateCreated = missionOrder.CreateDate.ToShortDateString();
                SetFlagsState(missionOrder.Id, user, missionOrder, model);
                return true;
            }
            catch (Exception ex)
            {
                MissionOrderDao.RollbackTran();
                Log.Error("Error on SaveMissionOrderEditModel:", ex);
                error = string.Format("Исключение:{0}", ex.GetBaseException().Message);
                return false;
            }
            finally
            {
                SetUserInfoModel(user, model, UserManualRole.ApprovesMissionOrders);
                SetStaticFields(model, missionOrder);
                LoadDictionaries(model);
                SetHiddenFields(model);
            }
        }
        private void ChangeCosts(MissionOrder entity, MissionOrderEditModel model)
        {
            var report = MissionReportDao.GetReportForOrder(entity.Id);
            if (report==null) return;
            IList<MissionReportCost> list = report.Costs !=null?report.Costs:new List<MissionReportCost>();
            IList<MissionReportCostType> types = MissionReportCostTypeDao.LoadAll();
            if (!entity.IsResidencePaid && model.IsResidencePaid)
            {
                MissionReportCost cost = new MissionReportCost
                {
                    IsCostFromOrder = true,
                    IsCostFromPurchaseBook = true,
                    Report = report,
                    Type = types.Where(x => x.Id == 2).First(),
                    Sum = entity.SumResidence,
                    UserSum = null//entity.IsResidencePaid ? null:entity.UserSumResidence,
                };
                list.Add(cost);
            }
            if (!entity.IsAirTicketsPaid && model.IsAirTicketsPaid)
            {
                MissionReportCost cost = new MissionReportCost
                {
                    IsCostFromOrder = true,
                    IsCostFromPurchaseBook = true,
                    Report = report,
                    Type = types.Where(x => x.Id == 3).First(),
                    Sum = entity.SumAir,
                    UserSum = null//entity.IsAirTicketsPaid ? null : entity.UserSumAir,
                };
                list.Add(cost);
            }

            if (!entity.IsTrainTicketsPaid && model.IsTrainTicketsPaid)
            {
                MissionReportCost cost = new MissionReportCost
                {
                    IsCostFromOrder = true,
                    IsCostFromPurchaseBook = true,
                    Report = report,
                    Type = types.Where(x => x.Id == 4).First(),
                    Sum = entity.SumTrain,
                    UserSum = null//entity.IsTrainTicketsPaid ? null : entity.UserSumTrain,
                };
                list.Add(cost);
            }
            report.Costs = list;
            MissionReportDao.SaveAndFlush(report);
        }
        protected void ChangeEntityProperties(IUser current, MissionOrder entity, MissionOrderEditModel model, User user)
        {
            bool isDirectorManager = IsDirectorManagerForEmployee(user, current);

            #region Common props edits

            if (model.IsTicketsEditable)
            {
                ChangeCosts(entity, model);
                entity.IsResidencePaid = model.IsResidencePaid;
                entity.IsAirTicketsPaid = model.IsAirTicketsPaid;
                entity.IsTrainTicketsPaid = model.IsTrainTicketsPaid;
                SaveMissionTargets(entity, model.Targets);
                return;
            }
            if (model.IsEditable)
            {
                entity.BeginDate = DateTime.Parse(model.BeginMissionDate);
                entity.EndDate = DateTime.Parse(model.EndMissionDate);
                entity.Goal = MissionGoalDao.Load(model.GoalId);
                entity.Type = MissionTypeDao.Load(model.TypeId);
                entity.Kind = model.Kind;
                entity.UserAllSum = Decimal.Parse(model.UserAllSum);
                entity.UserSumDaily = GetSum(model.UserAllSumDaily);
                entity.UserSumResidence = GetSum(model.UserAllSumResidence);
                entity.UserSumAir = GetSum(model.UserAllSumAir);
                entity.UserSumTrain = GetSum(model.UserAllSumTrain);
                entity.AllSum = Decimal.Parse(model.AllSum);
                entity.SumDaily = Decimal.Parse(model.AllSumDaily);
                entity.SumResidence = Decimal.Parse(model.AllSumResidence);
                entity.SumAir = Decimal.Parse(model.AllSumAir);
                entity.SumTrain = Decimal.Parse(model.AllSumTrain);
                entity.UserSumCash = GetSum(model.UserSumCash);
                entity.UserSumNotCash = GetSum(model.UserSumNotCash);
                entity.NeedToAcceptByChiefAsManager = isDirectorManager;
                entity.NeedToAcceptByChief = IsMissionOrderLong(entity);
                entity.LongTermReason = string.IsNullOrEmpty(model.LongTermReason) ? null : model.LongTermReason;
                entity.IsResidencePaid = model.IsResidencePaid;
                entity.IsAirTicketsPaid = model.IsAirTicketsPaid;
                entity.IsTrainTicketsPaid = model.IsTrainTicketsPaid;
                model.IsChiefApproveNeed = IsMissionOrderLong(entity);//entity.NeedToAcceptByChief;
                SaveMissionTargets(entity, model.Targets);
            }
            #endregion

            #region Secretary edits

            if (model.IsSecritaryEditable)
            {
                if (entity.ResidenceRequestNumber != model.ResidenceRequestNumber ||
                    entity.AirTicketsRequestNumber != model.AirTicketsRequestNumber ||
                    entity.TrainTicketsRequestNumber != model.TrainTicketsRequestNumber ||
                    model.AirTicketType != entity.AirTicketType ||
                    model.TrainTicketType != entity.TrainTicketType)
                {
                    entity.Secretary = UserDao.Load(current.Id);
                    model.SecretaryFio = entity.Secretary.FullName;
                }
                entity.ResidenceRequestNumber = string.IsNullOrEmpty(model.ResidenceRequestNumber) ? null : model.ResidenceRequestNumber;
                entity.AirTicketsRequestNumber = string.IsNullOrEmpty(model.AirTicketsRequestNumber) ? null : model.AirTicketsRequestNumber;
                entity.TrainTicketsRequestNumber = string.IsNullOrEmpty(model.TrainTicketsRequestNumber) ? null : model.TrainTicketsRequestNumber;
                entity.AirTicketType = model.AirTicketType;
                entity.TrainTicketType = model.TrainTicketType;
            }

            #endregion

            #region Согласование сотрудником

            if ((current.UserRole & UserRole.Employee) == UserRole.Employee && current.Id == model.UserId
                && !entity.UserDateAccept.HasValue
                && model.IsUserApproved)
            {
                entity.UserDateAccept = DateTime.Now;
                entity.AcceptUser = UserDao.Load(current.Id);
                if (isDirectorManager)
                {
                    entity.NeedToAcceptByChiefAsManager = true;
                    SendEmailForMissionOrder(CurrentUser, entity, UserRole.Director, false);
                }
                else
                    SendEmailForMissionOrder(CurrentUser, entity, UserRole.Manager, false);
            }

            #endregion

            #region Согласование руководителем

            bool canEdit = false;

            if (((current.UserRole & UserRole.Manager) == UserRole.Manager && IsCurrentManagerForUser(user, current, out canEdit))
                || HasCurrentManualRoleForUser(user, current, UserManualRole.ApprovesMissionOrders, out canEdit))
            {
                // Согласование за сотрудника при создании заявки руководителем за сотрудника
                if (entity.Creator.RoleId == (int)UserRole.Manager && !entity.UserDateAccept.HasValue)
                {
                    entity.UserDateAccept = DateTime.Now;
                    entity.AcceptUser = UserDao.Load(current.Id);
                }

                if (!entity.ManagerDateAccept.HasValue)
                {
                    if (model.IsManagerApproved.HasValue)
                    {
                        if (model.IsManagerApproved.Value)
                        {
                            entity.ManagerDateAccept = DateTime.Now;
                            entity.AcceptManager = UserDao.Load(current.Id);

                            if (!entity.NeedToAcceptByChief)
                            {
                                CreateMission(entity);
                                SendEmailForMissionOrderConfirm(CurrentUser, entity, false);
                            }
                            else
                                SendEmailForMissionOrder(CurrentUser, entity, UserRole.Director, false);
                        }
                        else
                        {
                            model.IsManagerApproved = null;
                            if ((entity.Creator.RoleId & (int)UserRole.Manager) == 0)
                            {
                                entity.UserDateAccept = null;
                                SendEmailForMissionOrderReject(CurrentUser, entity, false);
                            }
                        }
                    }
                }
            }

            #endregion

            #region Director edits

            if ((current.UserRole & UserRole.Director) == UserRole.Director)
            {
                if (isDirectorManager && !entity.ManagerDateAccept.HasValue)
                {
                    if (model.IsManagerApproved.HasValue)
                    {
                        if (model.IsManagerApproved.Value)
                        {
                            User currentUser = UserDao.Load(current.Id);
                            entity.ManagerDateAccept = DateTime.Now;
                            entity.AcceptManager = currentUser;
                            if (entity.NeedToAcceptByChief)
                            {
                                entity.ChiefDateAccept = DateTime.Now;
                                entity.AcceptChief = currentUser;
                            }
                            CreateMission(entity);
                            SendEmailForMissionOrderConfirm(CurrentUser, entity, false);
                        }
                        else
                        {
                            entity.UserDateAccept = null;
                            model.IsManagerApproved = null;
                            SendEmailForMissionOrderReject(CurrentUser, entity, false);
                        }
                    }
                }
                if (entity.NeedToAcceptByChief)
                {
                    if (model.IsChiefApproved.HasValue)
                    {
                        if (model.IsChiefApproved.Value)
                        {
                            User currentUser = UserDao.Load(current.Id);
                            entity.ChiefDateAccept = DateTime.Now;
                            entity.AcceptChief = currentUser;
                            if (isDirectorManager && !entity.ManagerDateAccept.HasValue)
                            {
                                entity.ManagerDateAccept = DateTime.Now;
                                entity.AcceptManager = currentUser;
                            }
                            CreateMission(entity);
                            SendEmailForMissionOrderConfirm(CurrentUser, entity, false);
                        }
                        else
                        {
                            entity.UserDateAccept = null;
                            model.IsUserApproved = false;
                            entity.ManagerDateAccept = null;
                            model.IsManagerApproved = null;
                            SendEmailForMissionOrderReject(CurrentUser, entity, false);
                        }
                    }
                }
            }

            #endregion

        }

        protected EmailDto SendEmailForMissionOrder(IUser current, MissionOrder entity, UserRole receiverRole, bool isAdditional)
        {
            //User currentUser = UserDao.Load(current.Id);
            //if (currentUser == null)
            //    throw new ArgumentException(string.Format("Не могу загрузить пользователя {0} из базы даннных", current.Id));

            string to = string.Empty;
            string to1 = string.Empty;
            /*if (isAdditional)
            {
                switch (receiverRole)
                {
                    case UserRole.Manager:
                        to = entity.MainOrder.AcceptManager.Email+";";
                        break;
                    case UserRole.Director:
                        to = entity.MainOrder.AcceptChief.Email + ";";
                        break;
                }
                return SendEmailForMissionOrderNeedToApprove(to, entity, isAdditional);
            }*/
            IList<IdNameDto> managers;
            IList<IdNameDto> managers1;
            switch (receiverRole)
            {
                case UserRole.Manager:
                    User manager = UserDao.GetManagerForEmployee(entity.User.Login);
                    if (manager != null)
                    {
                        switch (manager.Level)
                        {
                            case 2:
                                if (!manager.IsMainManager)
                                {
                                    managers = UserDao.GetMainManagersForLevelDepartment(2, manager.Department.Path);
                                    to = managers.Where(man => !string.IsNullOrEmpty(man.Name)).
                                           Aggregate(string.Empty, (current1, man) => current1 + (man.Name + ";"));
                                }
                                break;
                            case 3:
                                managers = UserDao.GetMainManagersForLevelDepartment(2, manager.Department.Path);
                                to = managers.Where(man => !string.IsNullOrEmpty(man.Name)).
                                       Aggregate(string.Empty, (current1, man) => current1 + (man.Name + ";"));
                                if (!manager.IsMainManager)
                                {
                                    managers1 = UserDao.GetMainManagersForLevelDepartment(3, manager.Department.Path);
                                    to1 = managers1.Where(man => !string.IsNullOrEmpty(man.Name)).
                                           Aggregate(string.Empty, (current1, man) => current1 + (man.Name + ";"));
                                    to += to1;
                                }
                                break;
                            case 4:
                                managers = UserDao.GetMainManagersForLevelDepartment(3, manager.Department.Path);
                                to = managers.Where(man => !string.IsNullOrEmpty(man.Name)).
                                       Aggregate(string.Empty, (current1, man) => current1 + (man.Name + ";"));
                                break;
                            case 5:
                                managers = UserDao.GetMainManagersForLevelDepartment(3, manager.Department.Path);
                                to = managers.Where(man => !string.IsNullOrEmpty(man.Name)).
                                       Aggregate(string.Empty, (current1, man) => current1 + (man.Name + ";"));
                                if (!manager.IsMainManager)
                                {
                                    managers1 = UserDao.GetMainManagersForLevelDepartment(5, manager.Department.Path);
                                    to1 = managers1.Where(man => !string.IsNullOrEmpty(man.Name)).
                                        Aggregate(string.Empty, (current1, man) => current1 + (man.Name + ";"));
                                    to += to1;
                                }
                                break;
                            case 6:
                                managers = UserDao.GetMainManagersForLevelDepartment(5, manager.Department.Path);
                                to = managers.Where(man => !string.IsNullOrEmpty(man.Name)).
                                       Aggregate(string.Empty, (current1, man) => current1 + (man.Name + ";"));
                                break;
                        }
                    }
                    else
                    {
                        managers = UserDao.GetMainManagersForLevelDepartment(5, entity.User.Department.Path);
                        to = managers.Where(man => !string.IsNullOrEmpty(man.Name)).
                                Aggregate(string.Empty, (current1, man) => current1 + (man.Name + ";"));
                    }
                    break;
                case UserRole.Director:
                    IList<User> directors = UserDao.GetUsersWithRole(UserRole.Director);
                    to = directors.Where(director => !string.IsNullOrEmpty(director.Email)).
                        Aggregate(string.Empty, (current1, director) => current1 + (director.Email + ";"));

                    break;
            }
            //return SendEmailForMissionOrderNeedToApprove(to, entity,isAdditional);
            return new EmailDto();
        }
        protected bool IsMissionOrderLong(MissionOrder entity)
        {
            if (!entity.BeginDate.HasValue || !entity.EndDate.HasValue)
                return false;
            /*return (entity.EndDate.Value.Subtract(entity.BeginDate.Value).Days > 7) ||
                   (WorkingCalendarDao.GetNotWorkingCountBetweenDates(entity.BeginDate.Value, entity.EndDate.Value) > 0);*/
            return IsMissionOrderLong(entity.EndDate.Value, entity.BeginDate.Value);
        }
        public bool IsMissionOrderLong(DateTime endDate, DateTime beginDate)
        {
            return (endDate.Subtract(beginDate).Days > 7) ||
                  (WorkingCalendarDao.GetNotWorkingCountBetweenDates(beginDate, endDate) > 0);
        }
        protected string GetStringForList(List<string> list)
        {
            string result = string.Empty;
            foreach (string t in list)
            {
                if (result.Length + t.Length > 249)
                {
                    result += " ...  ";
                    break;
                }
                result += t + ", ";
            }
            return result.Length > 2 ? result.Substring(0, result.Length - 2) : result;
        }
        protected void CreateMissionReport(MissionOrder entity)
        {
            try
            {
                if (entity.Id == 0)
                    MissionOrderDao.SaveAndFlush(entity);
                if (entity.IsAdditional)
                    throw new ArgumentException("Невозможно создать авансовый отчет для изменения приказа");
                if (MissionReportDao.IsReportForOrderExists(entity.Id))
                    throw new ArgumentException("Для приказа уже существует авансовый отчет");
                IList<MissionReportCostType> types = MissionReportCostTypeDao.LoadAll();
                MissionReport report = new MissionReport
                {
                    CreateDate = DateTime.Now,
                    EditDate = DateTime.Now,
                    Creator = UserDao.Load(CurrentUser.Id),
                    MissionOrder = entity,
                    Number = entity.Number,
                    User = entity.User,
                    AllSum = entity.AllSum,
                    UserSumReceived = entity.UserAllSum,
                    UserAllSum = 0,
                };
                List<MissionReportCost> list = new List<MissionReportCost>();
                if (entity.SumDaily.HasValue && entity.SumDaily > 0)
                {
                    MissionReportCost cost = new MissionReportCost
                    {
                        IsCostFromOrder = true,
                        IsCostFromPurchaseBook = false,
                        Report = report,
                        Type = types.Where(x => x.Id == 1).First(),
                        Sum = entity.SumDaily,
                        UserSum = null//entity.UserSumDaily,
                    };
                    list.Add(cost);
                }
                if (entity.SumResidence.HasValue && entity.SumResidence > 0)
                {
                    MissionReportCost cost = new MissionReportCost
                    {
                        IsCostFromOrder = true,
                        IsCostFromPurchaseBook = entity.IsResidencePaid,
                        Report = report,
                        Type = types.Where(x => x.Id == 2).First(),
                        Sum = entity.SumResidence,
                        UserSum = null//entity.IsResidencePaid ? null:entity.UserSumResidence,
                    };
                    list.Add(cost);
                }
                if (entity.SumAir.HasValue && entity.SumAir > 0)
                {
                    MissionReportCost cost = new MissionReportCost
                    {
                        IsCostFromOrder = true,
                        IsCostFromPurchaseBook = entity.IsAirTicketsPaid,
                        Report = report,
                        Type = types.Where(x => x.Id == 3).First(),
                        Sum = entity.SumAir,
                        UserSum = null//entity.IsAirTicketsPaid ? null : entity.UserSumAir,
                    };
                    list.Add(cost);
                }
                if (entity.SumTrain.HasValue && entity.SumTrain > 0)
                {
                    MissionReportCost cost = new MissionReportCost
                    {
                        IsCostFromOrder = true,
                        IsCostFromPurchaseBook = entity.IsTrainTicketsPaid,
                        Report = report,
                        Type = types.Where(x => x.Id == 4).First(),
                        Sum = entity.SumTrain,
                        UserSum = null//entity.IsTrainTicketsPaid ? null : entity.UserSumTrain,
                    };
                    list.Add(cost);
                }
                report.Costs = list;
                //report.UserAllSum = report.Costs.Sum(x => x.UserSum).Value;
                MissionReportDao.SaveAndFlush(report);
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("Exception on CreateMissionReport: orderId {0}", entity.Id), ex);
            }
        }


        protected void CreateMission(MissionOrder entity)
        {

            CreateMissionReport(entity);
            if (entity.Mission != null)
                throw new ArgumentException(
                    string.Format("Для приказа уже существует заявка на командировку (Id {0})", entity.Mission.Id));
            //MissionTarget target = entity.Targets[0];

            List<string> cityList = entity.Targets.Select(x => x.City).ToList();
            string country = GetStringForList(cityList);
            List<string> orgList = entity.Targets.Select(x => x.Organization).ToList();
            string org = GetStringForList(orgList);
            GetStringForList(cityList);
            Mission mission = new Mission
            {
                BeginDate = entity.BeginDate.Value,
                Country = country,
                CreateDate = DateTime.Now,
                Creator = entity.Creator,
                DaysCount = entity.EndDate.Value.Subtract(entity.BeginDate.Value).Days + 1,
                EndDate = entity.EndDate.Value,
                FinancesSource = entity.User.Organization == null ? string.Empty : entity.User.Organization.Name,
                Goal = entity.Goal.Name,
                ManagerDateAccept = DateTime.Now,
                Number = RequestNextNumberDao.GetNextNumberForType((int)RequestTypeEnum.Mission),
                Organization = org,
                Reason = string.Format("Приказ № {0}", entity.Number),
                TimesheetStatus = TimesheetStatusDao.Load(7),
                Type = entity.Type,
                User = entity.User,
                UserDateAccept = DateTime.Now
            };
            MissionDao.SaveAndFlush(mission);
            entity.Mission = mission;
        }
        protected static decimal? GetSum(string sum)
        {
            return string.IsNullOrEmpty(sum)
                                   ? new decimal?()
                                   : Decimal.Parse(sum);
        }
        protected void SaveMissionTargets(MissionOrder entity, /*MissionOrderEditModel model*/string targetsString)
        {
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            JsonList list = jsonSerializer.Deserialize<JsonList>(targetsString);
            List<MissionOrderTargetModel> targets = list.List.ToList();
            if (entity.Targets == null)
                entity.Targets = new List<MissionTarget>();
            List<MissionTarget> removed = entity.Targets.Where(x => !targets.Any(y => y.TargetId == x.Id)).ToList();
            foreach (MissionTarget target in removed)
                entity.Targets.Remove(target);
            foreach (MissionOrderTargetModel target in targets)
            {
                if (target.TargetId < 0)
                {
                    MissionTarget newTarget = new MissionTarget();
                    SetTargetProperties(target, newTarget, entity);
                    entity.Targets.Add(newTarget);
                }
                else
                {
                    MissionTarget old = entity.Targets.Where(x => x.Id == target.TargetId).FirstOrDefault();
                    if (old == null)
                        throw new ArgumentException(string.Format("Не найдено место назначения (id {0}) в базе данных",
                            target.TargetId));
                    SetTargetProperties(target, old, entity);
                    MissionTargetDao.SaveAndFlush(old);
                }
            }

        }
        protected void SetTargetProperties(MissionOrderTargetModel target, MissionTarget entity, MissionOrder order)
        {
            entity.AirTicketType = target.AirTicketTypeId == 0
                    ? null
                    : MissionAirTicketTypeDao.Load(target.AirTicketTypeId);
            entity.BeginDate = DateTime.Parse(target.DateFrom);
            entity.City = target.City;
            entity.Country = MissionCountryDao.Load(target.CountryId);
            entity.DailyAllowance = target.DailyAllowanceId == 0 ? null :
                                                        MissionDailyAllowanceDao.Load(target.DailyAllowanceId);
            entity.DaysCount = int.Parse(target.AllDaysCount);
            entity.EndDate = DateTime.Parse(target.DateTo);
            entity.MissionOrder = order;
            entity.Organization = target.Organization;
            entity.RealDaysCount = int.Parse(target.TargetDaysCount);
            entity.Residence = target.ResidenceId == 0 ? null :
                                                  MissionResidenceDao.Load(target.ResidenceId);
            entity.TrainTicketType = target.TrainTicketTypeId == 0 ? null :
                                                  MissionTrainTicketTypeDao.Load(target.TrainTicketTypeId);
        }
        protected void SetHiddenFields(MissionOrderEditModel model)
        {
            model.IsChiefApprovedHidden = model.IsChiefApproved;
            model.IsChiefApproveNeedHidden = model.IsChiefApproveNeed;
            model.IsManagerApprovedHidden = model.IsManagerApproved;
            model.IsUserApprovedHidden = model.IsUserApproved;
            model.GoalIdHidden = model.GoalId;
            model.TypeIdHidden = model.TypeId;
            model.KindHidden = model.Kind;
            model.IsResidencePaidHidden = model.IsResidencePaid;
            model.IsAirTicketsPaidHidden = model.IsAirTicketsPaid;
            model.IsTrainTicketsPaidHidden = model.IsTrainTicketsPaid;
            model.AirTicketTypeHidden = model.AirTicketType;
            model.TrainTicketTypeHidden = model.TrainTicketType;
        }
        protected void SetFlagsState(int id, User user, MissionOrder entity, MissionOrderEditModel model)
        {
            SetFlagsState(model, false);
            UserRole currentUserRole = AuthenticationService.CurrentUser.UserRole;
            if (id == 0)
            {
                model.IsSaveAvailable = true;
                model.IsEditable = true;
                if ((currentUserRole & UserRole.Employee) == UserRole.Employee)
                    model.IsUserApprovedAvailable = true;
                else if ((currentUserRole & UserRole.Manager) == UserRole.Manager)
                {
                    model.IsManagerApproveAvailable = true;
                    model.IsUserApproved = model.IsUserApprovedHidden = true;
                }
                return;
            }
            model.IsUserApproved = entity.UserDateAccept.HasValue;
            model.IsManagerApproved = entity.ManagerDateAccept.HasValue ? true : new bool?();
            model.IsChiefApproved = entity.ChiefDateAccept.HasValue ? true : new bool?();

            switch (currentUserRole)
            {
                case UserRole.Employee:
                    if ((entity.Creator.RoleId & (int)UserRole.Employee) > 0)
                    {
                        if (!entity.UserDateAccept.HasValue && !entity.DeleteDate.HasValue)
                        {
                            model.IsEditable = true;
                            model.IsUserApprovedAvailable = true;
                        }
                    }
                    break;
                case UserRole.Manager:
                    //User curUser = userDao.Load(AuthenticationService.CurrentUser.Id);
                    bool canEdit = false;
                    bool isUserManager = IsCurrentManagerForUser(user, AuthenticationService.CurrentUser, out canEdit) || HasCurrentManualRoleForUser(user, AuthenticationService.CurrentUser, UserManualRole.ApprovesMissionOrders, out canEdit);
                    if (entity.Creator.RoleId == (int)UserRole.Manager)
                    {                        
                        if (!entity.ManagerDateAccept.HasValue && !entity.DeleteDate.HasValue && isUserManager && canEdit)
                        {
                            model.IsEditable = true;
                            model.IsManagerApproveAvailable = true;
                            if (entity.UserDateAccept.HasValue)
                                model.IsUserApproved = true;
                        }
                    }
                    else
                    {
                        if (!entity.ManagerDateAccept.HasValue && !entity.DeleteDate.HasValue
                            && entity.UserDateAccept.HasValue && isUserManager && canEdit)
                            model.IsManagerApproveAvailable = true;

                    }
                    break;
                case UserRole.Accountant:
                    var rep = MissionReportDao.GetReportForOrder(entity.Id);
                    if (rep == null)
                        model.IsTicketsEditable = true;
                    else
                    {
                        model.IsTicketsEditable = !rep.SendTo1C.HasValue;
                    }
                    break;
                case UserRole.Estimator:
                case UserRole.OutsourcingManager:
                    if (entity.SendTo1C.HasValue && !entity.DeleteDate.HasValue)
                        model.IsDeleteAvailable = true;
                    break;
                case UserRole.Secretary:
                    if (/*!entity.SendTo1C.HasValue &&*/ !entity.DeleteDate.HasValue &&
                        ((entity.NeedToAcceptByChief && entity.ChiefDateAccept.HasValue) ||
                         (!entity.NeedToAcceptByChief && entity.ManagerDateAccept.HasValue)) &&
                        (entity.IsAirTicketsPaid || entity.IsResidencePaid || entity.IsTrainTicketsPaid))
                        model.IsSecritaryEditable = true;
                    break;
                case UserRole.Director:
                    if (IsDirectorManagerForEmployee(user, AuthenticationService.CurrentUser))
                    {
                        if (!entity.ManagerDateAccept.HasValue &&
                            !entity.DeleteDate.HasValue && entity.UserDateAccept.HasValue)
                            model.IsManagerApproveAvailable = true;
                    }
                    if (entity.NeedToAcceptByChief && !entity.ChiefDateAccept.HasValue
                        && !entity.DeleteDate.HasValue && entity.ManagerDateAccept.HasValue
                        && entity.UserDateAccept.HasValue)
                        model.IsChiefApproveAvailable = true;
                    break;
            }
            model.IsSaveAvailable = model.IsEditable || model.IsTicketsEditable || model.IsUserApprovedAvailable
                || model.IsManagerApproveAvailable || model.IsChiefApproveAvailable || model.IsSecritaryEditable;

        }
        protected void SetFlagsState(MissionOrderEditModel model, bool state)
        {
            model.IsEditable = state;
            model.IsDeleteAvailable = state;
            model.IsChiefApproveAvailable = state;
            model.IsManagerApproveAvailable = state;
            model.IsUserApprovedAvailable = state;
            model.IsSaveAvailable = state;
            model.IsSecritaryEditable = state;

            model.IsChiefApproved = null;
            //model.IsChiefApprovedHidden = null;
            //model.IsChiefApproveNeed = state;
            //model.IsChiefApproveNeedHidden = state;
            model.IsDelete = state;
            //model.IsDeleted = state;
            model.IsManagerApproved = null;
            //model.IsManagerApprovedHidden = null;
            model.IsUserApproved = state;
            //model.IsUserApprovedHidden = state;


        }
        /*protected MissionOrderTargetModel[] AddTestData()
        {
            List<MissionOrderTargetModel> data = new List<MissionOrderTargetModel>
                                                     {
                                                         new MissionOrderTargetModel
                                                             {
                                                                 AirTicketTypeId = 1,
                                                                 AirTicketTypeName = "Данные по авиа билетам",
                                                                 AllDaysCount = 2.ToString(),
                                                                 City = "Город",
                                                                 Country = "Страна",
                                                                 CountryId = 2,
                                                                 DailyAllowanceId = 3,
                                                                 DailyAllowanceName = "Данные по суточным",
                                                                 DateFrom = "20.11.2013",
                                                                 DateTo = "22.11.2013",
                                                                 Organization = "Данные по организации",
                                                                 ResidenceId = 1,
                                                                 ResidenceName = "Данные по проживанию",
                                                                 TargetDaysCount = 1.ToString(),
                                                                 TargetId = 1,
                                                                 TrainTicketTypeId = 2,
                                                                 TrainTicketTypeName = "Данные по жел. билетам"
                                                             },
                                                             new MissionOrderTargetModel
                                                             {
                                                                 AirTicketTypeId = 1,
                                                                 AirTicketTypeName = "Данные по авиа билетам",
                                                                 AllDaysCount = 2.ToString(),
                                                                 City = "Город",
                                                                 Country = "Страна",
                                                                 CountryId = 2,
                                                                 DailyAllowanceId = 3,
                                                                 DailyAllowanceName = "Данные по суточным",
                                                                 DateFrom = "20.11.2013",
                                                                 DateTo = "22.11.2013",
                                                                 Organization = "Данные по организации",
                                                                 ResidenceId = 1,
                                                                 ResidenceName = "Данные по проживанию",
                                                                 TargetDaysCount = 1.ToString(),
                                                                 TargetId = 2,
                                                                 TrainTicketTypeId = 2,
                                                                 TrainTicketTypeName = "Данные по жел. билетам"
                                                             },

                                                     };
            return data.ToArray();
        }*/
        public bool CheckUserMoRights(User user, IUser current, int entityId, MissionOrder entity, bool isSave)
        {
            switch (current.UserRole)
            {
                case UserRole.Employee:
                    if (user.Id != current.Id)
                    {
                        Log.ErrorFormat("CheckUserMoRights user.Id {0} current.Id {1}", user.Id, current.Id);
                        return false;
                    }
                    return true;
                case UserRole.Manager:
                    bool canEdit;
                    bool isManager = IsCurrentManagerForUser(user, current, out canEdit) || HasCurrentManualRoleForUser(user, current, UserManualRole.ApprovesMissionOrders, out canEdit);
                    if (isManager)
                    {
                        if (isSave)
                            return canEdit;
                        return true;
                    }
                    Log.ErrorFormat("CheckUserMoRights user.Id {0} current.Id {1} ", user.Id, current.Id);
                    return false;
                case UserRole.PersonnelManager:
                    int? superPersonnelId = ConfigurationService.SuperPersonnelId;
                    if (superPersonnelId.HasValue && CurrentUser.Id == superPersonnelId.Value)
                    {
                        return true;
                    }
                    else
                    {
                        Log.ErrorFormat("CheckUserRights  PersonnelManager user.Id {0} current.Id {1}", user.Id, current.Id);
                        return false;
                    }
                case UserRole.Estimator:
                case UserRole.OutsourcingManager:
                case UserRole.Secretary:
                    return true;
                case UserRole.Accountant:
                    return true;
                case UserRole.Findep:
                    if (isSave)
                        return false;
                    return true;
                case UserRole.Director:
                    if (entityId > 0)
                    {
                        if (entity.NeedToAcceptByChief || MissionOrderDao.CheckAdditionalOrderExists(entity.Id))
                            return true;
                        return IsDirectorManagerForEmployee(user, current);
                    }
                    return false;
            }
            return false;
        }
        protected bool IsDirectorManagerForEmployee(User user, IUser current)
        {
            User currentUser = UserDao.Load(current.Id);
            if (currentUser == null)
                throw new ArgumentException(string.Format("Не могу загрузить пользователя {0} из базы даннных", current.Id));
            User manager = UserDao.GetManagerForEmployee(user.Login);
            if (manager != null && manager.Level == 2 && manager.IsMainManager)
                return true;
            return false;
        }

        /// <summary>
        /// Checks if the authenticated user is a manager of the specified employee
        /// </summary>
        /// <param name="user">Employee</param>
        /// <param name="current">Authenticated user</param>
        /// <param name="canEdit">Result output</param>
        /// <returns>true/false for check success/failure</returns>
        protected bool IsCurrentManagerForUser(User user, IUser current, out bool canEdit)
        {
            canEdit = false;

            User currentUser = UserDao.Load(current.Id);
            if (currentUser == null)
                throw new ArgumentException(string.Format("Не могу загрузить пользователя {0} из базы даннных", current.Id));

            // Руководительская учетная запись сотрудника, подчиненность которого текущему пользователю проверяется
            User usersManagerAccount = UserDao.GetManagerForEmployee(user.Login);

            switch (currentUser.Level)
            {
                // Права руководителей уровней 2, 3 определяются только по ручной привязке

                // Руководителям уровней 4-6 подчинены их замы, руководители нижележащих уровней и рядовые сотрудники

                case 4:
                case 5:
                case 6:

                    // Определение подчиненности руководителей
                    if (usersManagerAccount != null)
                    {
                        // Если зам
                        if (((usersManagerAccount.Level == currentUser.Level && !usersManagerAccount.IsMainManager)
                            // или нижележащий руководитель
                            || usersManagerAccount.Level > currentUser.Level)

                            && usersManagerAccount.Department != null
                            // не в ветке руководства
                            && !usersManagerAccount.Department.Path.StartsWith("4128.4205.4297.")
                            // в ветке текущего пользователя
                            && usersManagerAccount.Department.Path.StartsWith(currentUser.Department.Path))
                        {
                            canEdit = true;
                            return true;
                        }
                        //Видимо этот код нужен для ветки руководства, исправил код подразделения, убрал воскл.знак перед usersManagerAccount. Артём 01.02.2016
                        if (usersManagerAccount == currentUser && usersManagerAccount.Department.Path.StartsWith("4128.4205.4297."))
                        {
                            canEdit = true;
                            return true;
                        }
                    }

                    // Определение подчиненности рядовых сотрудников
                    else if (((user.RoleId & (int)UserRole.Employee) > 0 || (user.RoleId & (int)UserRole.DismissedEmployee) > 0)
                        && user.Department.Path.StartsWith(currentUser.Department.Path))
                    {
                        canEdit = true;
                        return true;
                    }

                    break;
                default:
                    canEdit = false;
                    return false;
            }
            return false;
        }

        protected bool HasCurrentManualRoleForUser(User user, IUser current, UserManualRole manualRole, out bool canEdit)
        {
            User currentUser = UserDao.Load(current.Id);
            if (currentUser == null)
                throw new ArgumentException(string.Format("Не могу загрузить пользователя {0} из базы даннных", current.Id));

            // Получаем количество ручных привязок по данной роли
            int relevantRoleRecordsCount = currentUser.ManualRoleRecords
                .Where<ManualRoleRecord>(roleRecord =>
                    roleRecord.Role.Id == (int)manualRole
                    && (roleRecord.TargetUser == user
                        || (user.Department != null && roleRecord.TargetDepartment != null && user.Department.Path.StartsWith(roleRecord.TargetDepartment.Path))))
                .ToList<ManualRoleRecord>()
                .Count;

            // Если ручные привязки найдены
            canEdit = (relevantRoleRecordsCount > 0) ? true : false;
            return canEdit;
        }

        protected bool IsUserManagerForDepartment(Department department, User user, UserManualRole manualRole = UserManualRole.ApprovesCommonRequests)
        {
            return
                (
                    (
                // Подчиненность подразделения по должности
                        (user.UserRole & UserRole.Manager) == UserRole.Manager
                        && user.Level > 3
                        && user.Department != null
                        && department.Path.StartsWith(user.Department.Path)
                    )
                    ||
                    (
                // Подчиненность подразделения по ручным привязкам
                        user.ManualRoleRecords
                            .Where(roleRecord =>
                                roleRecord.Role.Id == (int)manualRole
                                && roleRecord.TargetDepartment != null
                                && department.Path.StartsWith(roleRecord.TargetDepartment.Path)
                            )
                            .Count() > 0
                    )
                );
        }

        protected void LoadDictionaries(MissionOrderEditModel model)
        {
            //model.CommentsModel = GetCommentsModel(model.Id, (int)RequestTypeEnum.Dismissal);
            //model.Statuses = GetTimesheetStatusesForDismissal();
            model.Types = GetMissionTypes(false);
            model.Kinds = GetMissionOrderKinds();
            model.Goals = GetMissionGoals(false);
            model.AirTicketTypes = GetAirTicketTypes();
            model.TrainTicketTypes = GetTrainTicketTypes();
            model.CommentsModel = GetCommentsModel(model.Id, (int)RequestTypeEnum.MissionOrder);
        }
        protected List<IdNameDto> GetMissionOrderKinds()
        {
            return new List<IdNameDto>
                       {
                           new IdNameDto {Id = 1,Name = "Внутренняя"},
                           new IdNameDto {Id = 2,Name = "Внешняя"},
                       };
        }
        protected List<IdNameDto> GetAirTicketTypes()
        {
            return new List<IdNameDto>
                       {
                           new IdNameDto {Id = 0,Name = string.Empty},
                           new IdNameDto {Id = 1,Name = "Бизнес"},
                           new IdNameDto {Id = 2,Name = "Эконом"},
                       };
        }
        protected List<IdNameDto> GetTrainTicketTypes()
        {
            return new List<IdNameDto>
                       {
                           new IdNameDto {Id = 0,Name = string.Empty},
                           new IdNameDto {Id = 1,Name = "Купе"},
                           new IdNameDto {Id = 2,Name = "СВ"},
                       };
        }
        public void ReloadDictionaries(MissionOrderEditModel model)
        {
            User user = UserDao.Load(model.UserId);
            SetUserInfoModel(user, model, UserManualRole.ApprovesMissionOrders);
            //model.CommentsModel = GetCommentsModel(model.Id, (int)RequestTypeEnum.MissionOrder);
            LoadDictionaries(model);
            if (model.Id == 0)
            {
                model.DateCreated = DateTime.Today.ToShortDateString();
            }
            else
            {
                MissionOrder missionOrder = MissionOrderDao.Load(model.Id);
                model.DocumentNumber = missionOrder.Number.ToString();
                model.DateCreated = missionOrder.CreateDate.ToShortDateString();
                if (missionOrder.DeleteDate.HasValue)
                    model.IsDeleted = true;
                SetStaticFields(model, missionOrder);
            }
        }
        protected void SetStaticFields(MissionOrderEditModel model, MissionOrder entity)
        {
            if (entity == null)
            {
                Log.Warn("SetStaticFields: entity == null");
                return;
            }
            if (entity.AcceptManager != null && entity.ManagerDateAccept.HasValue)
                model.ManagerFio = entity.AcceptManager.FullName + " " +
                    entity.ManagerDateAccept.Value.ToShortDateString();// +", " + entity.AcceptAccountant.Email;
            if (entity.AcceptChief != null && entity.ChiefDateAccept.HasValue)
                model.ChiefFio = entity.AcceptChief.FullName + " " +
                    entity.ChiefDateAccept.Value.ToShortDateString();// +", " + entity.AcceptAccountant.Email;
        }
        protected void LoadGraids(MissionOrderEditModel model, int gradeId, MissionOrder entity, DateTime gradeDate)
        {
            //DateTime gradeDate = DateTime.Parse(model.DateCreated);
            MissionGraid graid = MissionGraidDao.Load(gradeId);
            if (graid == null)
                throw new ValidationException(string.Format("Не найден грайд (id = {0}) в базе данных", gradeId));
            model.Grade = graid.Name;
            IList<GradeAmountDto> dailyList = MissionGraidDao.GetDailyAllowanceGradeAmountForGradeAndDate(gradeId, gradeDate);
            if (dailyList.Count != 4)
                throw new ValidationException(string.Format("Неверное число лимитов для суточных загружено из базы данных"));
            IList<GradeAmountDto> resList = MissionGraidDao.GetResidenceGradeAmountForGradeAndDate(gradeId, gradeDate);
            if (resList.Count != 2)
                throw new ValidationException(string.Format("Неверное число лимитов для авиа проживания загружено из базы данных"));
            IList<GradeAmountDto> airList = MissionGraidDao.GetAirTicketTypeGradeAmountForGradeAndDate(gradeId, gradeDate);
            if (airList.Count != 6)
                throw new ValidationException(string.Format("Неверное число лимитов для авиа билетов загружено из базы данных"));
            IList<GradeAmountDto> trainList = MissionGraidDao.GetTrainTicketTypeGradeAmountForGradeAndDate(gradeId, gradeDate);
            if (trainList.Count != 6)
                throw new ValidationException(string.Format("Неверное число лимитов для ж/д билетов загружено из базы данных"));
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            model.DailyAllowanceGrades = jsonSerializer.Serialize(dailyList.ToArray());
            model.ResidenceGrades = jsonSerializer.Serialize(resList.ToArray());
            model.AirTicketTypeGrades = jsonSerializer.Serialize(airList.ToArray());
            model.TrainTicketTypeGrades = jsonSerializer.Serialize(trainList.ToArray());
            if (entity != null && entity.Targets != null && entity.Targets.Count > 0)
            {
                bool needUpdate = false;
                decimal sumAir = 0;
                decimal sumTrain = 0;
                decimal sumDaily = 0;
                decimal sumRes = 0;
                foreach (MissionTarget target in entity.Targets)
                {
                    if (target.AirTicketType != null)
                    {
                        GradeAmountDto dto = airList.Where(x => x.Id == target.AirTicketType.Id).FirstOrDefault();
                        if (dto == null)
                            throw new ArgumentException(string.Format("Не найден грейд для авиабилетов (id {0})  в базе данных",
                                target.AirTicketType.Id));
                        sumAir += dto.Amount;
                    }
                    if (target.TrainTicketType != null)
                    {
                        GradeAmountDto dto = trainList.Where(x => x.Id == target.TrainTicketType.Id).FirstOrDefault();
                        if (dto == null)
                            throw new ArgumentException(string.Format("Не найден грейд для ж/д билетов (id {0})  в базе данных",
                                target.AirTicketType.Id));
                        sumTrain += dto.Amount;
                    }
                    if (target.DailyAllowance != null)
                    {
                        GradeAmountDto dto = dailyList.Where(x => x.Id == target.DailyAllowance.Id).FirstOrDefault();
                        if (dto == null)
                            throw new ArgumentException(string.Format("Не найден грейд для суточных (id {0})  в базе данных",
                                target.AirTicketType.Id));
                        sumDaily += dto.Amount * target.DaysCount;
                    }
                    if (target.Residence != null)
                    {
                        GradeAmountDto dto = resList.Where(x => x.Id == target.Residence.Id).FirstOrDefault();
                        if (dto == null)
                            throw new ArgumentException(string.Format("Не найден грейд для проживания (id {0})  в базе данных",
                                target.AirTicketType.Id));
                        sumRes += dto.Amount * target.RealDaysCount;
                    }
                }
                if (entity.SumAir.HasValue)
                {
                    if (entity.SumAir.Value != sumAir)
                    {
                        Log.ErrorFormat("Сумма для авиабилетов по грейду не совпадает с суммой из базы данных для приказа {0}", entity.Id);
                        entity.SumAir = sumAir;
                        needUpdate = true;
                    }
                }
                else if (sumAir != 0)
                {
                    Log.ErrorFormat("Сумма для авиабилетов по грейду не совпадает с суммой из базы данных для приказа {0}", entity.Id);
                    entity.SumAir = sumAir;
                    needUpdate = true;
                }

                if (entity.SumTrain.HasValue)
                {
                    if (entity.SumTrain.Value != sumTrain)
                    {
                        Log.ErrorFormat("Сумма для  ж/д билетов по грейду не совпадает с суммой из базы данных для приказа {0}", entity.Id);
                        entity.SumTrain = sumTrain;
                        needUpdate = true;
                    }
                }
                else if (sumTrain != 0)
                {
                    Log.ErrorFormat("Сумма для  ж/д билетов по грейду не совпадает с суммой из базы данных для приказа {0}", entity.Id);
                    entity.SumTrain = sumTrain;
                    needUpdate = true;
                }

                if (entity.SumDaily.HasValue)
                {
                    if (entity.SumDaily.Value != sumDaily)
                    {
                        Log.ErrorFormat("Сумма для суточных по грейду не совпадает с суммой из базы данных для приказа {0}", entity.Id);
                        entity.SumDaily = sumDaily;
                        needUpdate = true;
                    }
                }
                else if (sumDaily != 0)
                {
                    Log.ErrorFormat("Сумма для суточных по грейду не совпадает с суммой из базы данных для приказа {0}", entity.Id);
                    entity.SumDaily = sumDaily;
                    needUpdate = true;
                }

                if (entity.SumResidence.HasValue)
                {
                    if (entity.SumResidence.Value != sumRes)
                    {
                        Log.ErrorFormat("Сумма для проживания по грейду не совпадает с суммой из базы данных для приказа {0}", entity.Id);
                        entity.SumResidence = sumRes;
                        needUpdate = true;
                    }
                }
                else if (sumRes != 0)
                {
                    Log.ErrorFormat("Сумма для проживания по грейду не совпадает с суммой из базы данных для приказа {0}", entity.Id);
                    entity.SumResidence = sumRes;
                    needUpdate = true;
                }
                decimal allSum = sumAir + sumTrain + sumDaily + sumRes;
                if (entity.AllSum != allSum)
                {
                    Log.ErrorFormat("Общая сумма по грейду не совпадает с суммой из базы данных для приказа {0}", entity.Id);
                    entity.AllSum = allSum;
                    needUpdate = true;
                }
                if (needUpdate)
                {
                    MissionOrderDao.SaveAndFlush(entity);
                    Log.ErrorFormat("Сумма(ы) по грейду изменены в базе данных для приказа {0}", entity.Id);
                }
            }
        }
        protected List<IdNameDto> GetMissionGoals(bool addAll)
        {
            var typeList = MissionGoalDao.LoadAllSorted().ToList().ConvertAll(x => new IdNameDto(x.Id, x.Name));
            if (addAll)
                typeList.Insert(0, new IdNameDto(0, SelectAll));
            return typeList;
        }
        public void SetMissionOrderEditTargetModel(MissionOrderEditTargetModel model)
        {
            model.AirTicketTypes = GetMissionAirTicketTypes(true);
            model.TrainTicketTypes = GetMissionTrainTicketTypes(true);
            model.Residences = GetMissionResidences(true);
            model.DailyAllowances = GetMissionDailyAllowances(true);
            model.Countries = GetMissionCountries(false);
        }
        protected List<IdNameDto> GetMissionAirTicketTypes(bool addEmpty)
        {
            var typeList = MissionAirTicketTypeDao.LoadAllSorted().ToList().ConvertAll(x => new IdNameDto(x.Id, x.Name));
            if (addEmpty)
                typeList.Insert(0, new IdNameDto(0, string.Empty));
            return typeList;
        }
        protected List<IdNameDto> GetMissionTrainTicketTypes(bool addEmpty)
        {
            var typeList = MissionTrainTicketTypeDao.LoadAll().ToList().ConvertAll(x => new IdNameDto(x.Id, x.Name));
            if (addEmpty)
                typeList.Insert(0, new IdNameDto(0, string.Empty));
            return typeList;
        }
        protected List<IdNameDto> GetMissionResidences(bool addEmpty)
        {
            var typeList = MissionResidenceDao.LoadAllSorted().ToList().ConvertAll(x => new IdNameDto(x.Id, x.Name));
            if (addEmpty)
                typeList.Insert(0, new IdNameDto(0, string.Empty));
            return typeList;
        }
        protected List<IdNameDto> GetMissionDailyAllowances(bool addEmpty)
        {
            var typeList = MissionDailyAllowanceDao.LoadAllSorted().ToList().ConvertAll(x => new IdNameDto(x.Id, x.Name));
            if (addEmpty)
                typeList.Insert(0, new IdNameDto(0, string.Empty));
            return typeList;
        }
        protected List<IdNameDto> GetMissionCountries(bool addEmpty)
        {
            List<MissionCountry> countries = MissionCountryDao.LoadAllSorted().ToList();
            var typeList = countries.ConvertAll(x => new IdNameDtoSort { Id = x.Id, Name = x.Name, SortOrder = x.Id == 1 ? 0 : 1 }).
                OrderBy(x => x.SortOrder).ThenBy(x => x.Name).ToList().ConvertAll(x => new IdNameDto(x.Id, x.Name));
            //var typeList = MissionCountryDao.LoadAllSorted().ToList().ConvertAll(x => new IdNameDto(x.Id, x.Name));
            if (addEmpty)
                typeList.Insert(0, new IdNameDto(0, string.Empty));
            return typeList;
        }

        public PrintMissionOrderViewModel GetPrintMissionOrderModel(int id)
        {
            PrintMissionOrderViewModel model = new PrintMissionOrderViewModel();
            MissionOrder order = MissionOrderDao.Load(id);
            if (order == null)
                throw new ArgumentException(string.Format("Приказ на командировку (id {0}) отсутствует в базе данных."));
            SetUserInfoModel(order.User, model, UserManualRole.ApprovesMissionOrders);
            model.DocumentNumber = order.Number.ToString();
            model.DateCreated = order.CreateDate.ToShortDateString();
            model.Goal = order.Goal.Name;
            int i = 1;
            List<PrintMissionTargetViewModel> targets = order.Targets.Select(target => new PrintMissionTargetViewModel
            {
                BeginDay = target.BeginDate.Day.ToString(),
                BeginMonth = GetMonthNamerRP(target.BeginDate.Month),
                BeginYear = target.BeginDate.Year.ToString(),
                Country = target.Country.Name + ", " + target.City,
                Days = target.DaysCount,
                EndDay = target.EndDate.Day.ToString(),
                EndMonth = GetMonthNamerRP(target.EndDate.Month),
                EndYear = target.EndDate.Year.ToString(),
                Organization = target.Organization,
                RealDays = target.RealDaysCount,
                Number = i++,
            }).ToList();
            model.Targets = targets;
            model.GradeAllSum = FormatSum(order.AllSum);
            model.GradeAviaSum = FormatSum(order.SumAir);
            model.GradeDailySum = FormatSum(order.SumDaily);
            model.GradeResSum = FormatSum(order.SumResidence);
            model.GradeTrainSum = FormatSum(order.SumTrain);
            model.UserAllSum = FormatSum(order.UserAllSum);
            model.UserAviaSum = FormatSum(order.UserSumAir);
            model.UserDailySum = FormatSum(order.UserSumDaily);
            model.UserResSum = FormatSum(order.UserSumResidence);
            model.UserTrainSum = FormatSum(order.UserSumTrain);
            model.UserCashSum = FormatSum(order.UserSumCash);
            model.UserNotCashSum = FormatSum(order.UserSumNotCash);
            if (order.ManagerDateAccept.HasValue)
            {
                model.ManName = order.AcceptManager.Name;
                model.ManagerPosition = order.AcceptManager.Position == null ? string.Empty : order.AcceptManager.Position.Name;
                model.ManagerDate = order.ManagerDateAccept.Value.ToShortDateString();
            }
            if (order.NeedToAcceptByChief && order.ChiefDateAccept.HasValue)
            {
                model.ChiefDate = order.ChiefDateAccept.Value.ToShortDateString();
                model.ChiefName = order.AcceptChief.Name;
                model.ChiefPosition = order.AcceptChief.Position == null ? string.Empty : order.AcceptChief.Position.Name;
            }
            model.IsLongOrder = IsMissionOrderLong(order);
            return model;
        }
        public UserInfoModel GetPrintMissionOrderDocumentModel(int id)
        {
            UserInfoModel model = new UserInfoModel();
            MissionOrder order = MissionOrderDao.Load(id);
            if (order == null)
                throw new ArgumentException(string.Format("Приказ на командировку (id {0}) отсутствует в базе данных."));
            SetUserInfoModel(order.User, model, UserManualRole.ApprovesMissionOrders);
            model.DocumentNumber = order.Number.ToString();
            model.DateCreated = order.CreateDate.ToShortDateString();
            return model;
        }

        public GradeListViewModel GetGradeListModel()
        {
            GradeListViewModel model = new GradeListViewModel();
            IList<MissionGraid> graids = MissionGraidDao.LoadAll().OrderBy(x => x.Id).ToList();
            IList<GradeAmountNameDto> dailyList = MissionGraidDao.GetDailyAllowanceGradeAmountForDate(DateTime.Today);
            model.Daily = GetTableDto(dailyList, graids);
            IList<GradeAmountNameDto> resList = MissionGraidDao.GetResidenceGradeAmountForDate(DateTime.Today);
            model.Residence = GetTableDto(resList, graids);
            IList<GradeAmountNameDto> airList = MissionGraidDao.GetAirTicketTypeGradeAmountForDate(DateTime.Today);
            model.AirTicket = GetTableDto(airList, graids);
            IList<GradeAmountNameDto> trainList = MissionGraidDao.GetTrainTicketTypeGradeAmountForDate(DateTime.Today);
            model.TrainTicket = GetTableDto(trainList, graids);
            return model;
        }
        protected TableDto GetTableDto(IList<GradeAmountNameDto> list, IList<MissionGraid> graids)
        {
            List<int> ids = list.Select(x => x.Id).Distinct().ToList();
            TableDto table = new TableDto { rows = new List<RowDto>() };
            RowDto head = new RowDto();
            List<string> values = new List<string> { "Лимит на расходы" };
            foreach (MissionGraid graid in graids)
                values.Add(graid.Name);
            head.Values = values;
            table.rows.Add(head);
            foreach (int id in ids)
            {
                List<GradeAmountNameDto> dtoForId = list.ToList().Where(x => x.Id == id).OrderBy(x => x.GradeId).ToList();
                values = new List<string> { dtoForId[0].Name };
                foreach (GradeAmountNameDto dto in dtoForId)
                    values.Add(FormatSum(dto.Amount));
                RowDto row = new RowDto { Values = values };
                table.rows.Add(row);
            }
            return table;
        }
        #endregion
        #region Additional Mission Order
        public int CreateAdditionalOrder(int missionReportId)
        {
            MissionReport report = MissionReportDao.Load(missionReportId);
            if (report == null)
                throw new ValidationException(string.Format("Не найден авансовый отчет (id {0}) в базе данных", missionReportId));
            MissionOrder order = report.MissionOrder;
            if (MissionOrderDao.CheckAnyAdditionalOrdersExists(order.Id))
                throw new ValidationException("Приказ на изменение уже существует для данного приказа");
            MissionOrder additionalOrder = new MissionOrder
            {
                CreateDate = DateTime.Now,
                Creator = order.User,
                Comments = new List<MissionOrderComment>(),
                EditDate = DateTime.Now,
                Goal = order.Goal,
                IsAdditional = true,
                Kind = order.Kind,
                Mission = order.Mission,
                Number = order.Number,
                Targets = new List<MissionTarget>(),
                User = order.User,
                Type = order.Type,
                NeedToAcceptByChief = order.NeedToAcceptByChief,
                NeedToAcceptByChiefAsManager = order.NeedToAcceptByChiefAsManager,
                MainOrder = order,
                BeginDate = order.BeginDate,
                EndDate = order.EndDate,
            };
            MissionOrderDao.SaveAndFlush(additionalOrder);
            report.AdditionalMissionOrder = additionalOrder;
            MissionReportDao.SaveAndFlush(report);
            return additionalOrder.Id;
        }
        public AdditionalMissionOrderEditModel GetAdditionalMissionOrderEditModel(int id)
        {
            MissionOrder entity = MissionOrderDao.Load(id);
            if (entity == null)
                throw new ValidationException(string.Format("Не найдено изменение приказа на командировку (id {0}) в базе данных", id));
            if (entity.MainOrder == null)
                throw new ValidationException(string.Format("В изменении приказа на командировку (id {0}) не указан приказ", id));
            User user = entity.User;
            //IUser current = AuthenticationService.CurrentUser;
            //if (!CheckUserAmoRights(user, current, id, entity, false))
            //    throw new ArgumentException("Доступ запрещен.");


            AdditionalMissionOrderEditModel model = new AdditionalMissionOrderEditModel
            {
                Id = entity.Id,
                UserId = entity.User.Id,
                BeginMissionDate = FormatDate(entity.BeginDate),
                DateCreated = FormatDate(entity.CreateDate),
                EndMissionDate = FormatDate(entity.EndDate),
                Goal = entity.Goal.Name,
                Kind = entity.Kind == 1 ? "Внутренняя" : "Внешняя",
                Type = entity.Type.Name,
                TypeId = entity.Type.Id,
                Version = entity.Version
            };
            if (!entity.User.Grade.HasValue)
                throw new ValidationException(string.Format("Не указан грейд для пользователя {0} в базе данных", user.Id));
            MissionGraid graid = MissionGraidDao.Load(entity.User.Grade.Value);
            if (graid == null)
                throw new ValidationException(string.Format("Не найден грайд (id = {0}) в базе данных", entity.User.Grade.Value));
            model.Grade = graid.Name;
            if (entity.DeleteDate.HasValue)
                model.IsDeleted = true;
            model.IsChiefApproveNeed = IsAdditionalMissionOrderLong(entity);//entity.NeedToAcceptByChief;
            model.DocumentNumber = entity.Number + "-изм";

            MissionOrderTargetModel[] targets = entity.Targets.ToList().ConvertAll(x => new MissionOrderTargetModel
            {
                AirTicketTypeId = x.AirTicketType == null ? 0 : x.AirTicketType.Id,
                AirTicketTypeName = x.AirTicketType == null ? string.Empty : x.AirTicketType.Name,
                AllDaysCount = x.DaysCount.ToString(),
                City = x.City,
                Country = x.Country.Name,
                CountryId = x.Country.Id,
                DailyAllowanceId = x.DailyAllowance == null ? 0 : x.DailyAllowance.Id,
                DailyAllowanceName = x.DailyAllowance == null ? string.Empty : x.DailyAllowance.Name,
                DateFrom = x.BeginDate.ToShortDateString(),
                DateTo = x.EndDate.ToShortDateString(),
                Organization = x.Organization,
                ResidenceId = x.Residence == null ? 0 : x.Residence.Id,
                ResidenceName = x.Residence == null ? string.Empty : x.Residence.Name,
                TargetDaysCount = x.RealDaysCount.ToString(),
                TargetId = x.Id,
                TrainTicketTypeId = x.TrainTicketType == null ? 0 : x.TrainTicketType.Id,
                TrainTicketTypeName = x.TrainTicketType == null ? string.Empty : x.TrainTicketType.Name,
            }).ToArray();
            JsonList list = new JsonList { List = targets };
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            model.Targets = jsonSerializer.Serialize(list);
            SetUserInfoModel(user, model, UserManualRole.ApprovesMissionOrders);
            SetFlagsState(id, user, entity, model);
            SetHiddenFields(model);
            return model;
        }
        protected bool IsAdditionalMissionOrderLong(MissionOrder entity)
        {
            /*if (!entity.BeginDate.HasValue || !entity.EndDate.HasValue)
                return false;
            return (entity.EndDate.Value.Subtract(entity.BeginDate.Value).Days > 7) ||
                   (WorkingCalendarDao.GetNotWorkingCountBetweenDates(entity.BeginDate.Value, entity.EndDate.Value) > 0);*/
            if (IsMissionOrderLong(entity.MainOrder))
                return true;
            if (!entity.BeginDate.HasValue || !entity.EndDate.HasValue)
                return false;
            DateTime minDate = entity.BeginDate.Value; /*entity.MainOrder.BeginDate.Value < entity.BeginDate.Value
                                   ? entity.MainOrder.BeginDate.Value
                                   : entity.BeginDate.Value;*/
            DateTime maxDate = entity.EndDate.Value;/*entity.MainOrder.EndDate.Value > entity.EndDate.Value
                                   ? entity.MainOrder.EndDate.Value
                                   : entity.EndDate.Value;*/
            return (maxDate.Subtract(minDate).Days > 7) ||
                  (WorkingCalendarDao.GetNotWorkingCountBetweenDates(minDate, maxDate) > 0);

        }
        public bool CheckUserAmoRights(User user, IUser current, int entityId, MissionOrder entity, bool isSave)
        {
            switch (current.UserRole)
            {
                case UserRole.Employee:
                    if (user.Id != current.Id)
                    {
                        Log.ErrorFormat("CheckUserAmoRights user.Id {0} current.Id {1}", user.Id, current.Id);
                        return false;
                    }
                    return true;
                case UserRole.Manager:
                    //bool canEdit;
                    //bool isManager = IsUserManagerForEmployee(user, current, out canEdit) || CanUserApproveMissionOrderForEmployee(user, current, out canEdit);
                    //if (isManager)
                    //{
                    //    if (isSave)
                    //        return canEdit ;
                    //    return true;
                    //}
                    if (entity.MainOrder.AcceptManager.Id == current.Id)
                        return true;
                    Log.ErrorFormat("CheckUserAmoRights user.Id {0} current.Id {1} ", user.Id, current.Id);
                    return false;
                case UserRole.Estimator:
                case UserRole.OutsourcingManager:
                    //case UserRole.Secretary:
                    return true;
                case UserRole.Accountant:
                case UserRole.Findep:
                    if (isSave)
                        return false;
                    return true;
                case UserRole.Director:
                    //if (entityId > 0)
                    //{
                    //    if (entity.NeedToAcceptByChief)
                    //        return true;
                    //    return IsDirectorManagerForEmployee(user, current);
                    //}
                    if (entity.MainOrder.AcceptChief.Id == current.Id)
                        return true;
                    Log.ErrorFormat("CheckUserAmoRights user.Id {0} current.Id {1} ", user.Id, current.Id);
                    return false;
            }
            return false;
        }
        protected void SetFlagsState(int id, User user, MissionOrder entity, AdditionalMissionOrderEditModel model)
        {
            SetFlagsState(model, false);
            UserRole currentUserRole = AuthenticationService.CurrentUser.UserRole;

            model.IsUserApproved = entity.UserDateAccept.HasValue;
            model.IsManagerApproved = entity.ManagerDateAccept.HasValue ? true : new bool?();
            model.IsChiefApproved = entity.ChiefDateAccept.HasValue ? true : new bool?();
            switch (currentUserRole)
            {
                case UserRole.Employee:
                    if ((entity.Creator.RoleId & (int)UserRole.Employee) > 0)
                    {
                        if (!entity.UserDateAccept.HasValue && !entity.DeleteDate.HasValue)
                        {
                            model.IsEditable = true;
                            model.IsUserApprovedAvailable = true;
                        }
                    }
                    break;
                case UserRole.Manager:
                    bool canEdit = false;
                    //за уволенного сотрудника
                    if ((user.UserRole & UserRole.DismissedEmployee) == UserRole.DismissedEmployee)
                    {
                        if (!entity.UserDateAccept.HasValue && !entity.DeleteDate.HasValue)
                        {
                            model.IsEditable = true;
                            model.IsUserApprovedAvailable = true;
                        }
                    }

                    bool isUserManager = IsCurrentManagerForUser(user, AuthenticationService.CurrentUser, out canEdit)
                        || HasCurrentManualRoleForUser(user, AuthenticationService.CurrentUser, UserManualRole.ApprovesMissionOrders, out canEdit);

                    if (!entity.ManagerDateAccept.HasValue && !entity.DeleteDate.HasValue
                        && entity.UserDateAccept.HasValue && isUserManager /*&& canEdit*/)
                        model.IsManagerApproveAvailable = true;

                    break;
                case UserRole.Estimator:
                case UserRole.OutsourcingManager:
                    /*if (entity.SendTo1C.HasValue && !entity.DeleteDate.HasValue)
                        model.IsDeleteAvailable = true;*/
                    break;
                /*case UserRole.Secretary:
                    if (!entity.SendTo1C.HasValue && !entity.DeleteDate.HasValue &&
                        ((entity.NeedToAcceptByChief && entity.ChiefDateAccept.HasValue) ||
                         (!entity.NeedToAcceptByChief && entity.ManagerDateAccept.HasValue)) &&
                        (entity.IsAirTicketsPaid || entity.IsResidencePaid || entity.IsTrainTicketsPaid))
                        model.IsSecritaryEditable = true;
                    break;*/
                case UserRole.Director:
                    if (/*entity.MainOrder.AcceptManager.Id == AuthenticationService.CurrentUser.Id*/
                        IsDirectorManagerForEmployee(user, AuthenticationService.CurrentUser))
                    {
                        if (!entity.ManagerDateAccept.HasValue &&
                            !entity.DeleteDate.HasValue && entity.UserDateAccept.HasValue)
                            model.IsManagerApproveAvailable = true;
                    }
                    if (entity.NeedToAcceptByChief && !entity.ChiefDateAccept.HasValue
                            && !entity.DeleteDate.HasValue && entity.ManagerDateAccept.HasValue
                            && entity.UserDateAccept.HasValue)
                        model.IsChiefApproveAvailable = true;
                    /*if ( ((entity.MainOrder.AcceptChief != null && entity.MainOrder.AcceptChief.Id == AuthenticationService.CurrentUser.Id) ||
                           entity.MainOrder.AcceptChief == null) &&
                                entity.NeedToAcceptByChief && !entity.ChiefDateAccept.HasValue
                                && !entity.DeleteDate.HasValue && entity.ManagerDateAccept.HasValue
                                && entity.UserDateAccept.HasValue)
                    {
                            model.IsChiefApproveAvailable = true;
                    }*/

                    break;
            }
            model.IsSaveAvailable = model.IsEditable || model.IsUserApprovedAvailable
                || model.IsManagerApproveAvailable || model.IsChiefApproveAvailable /*|| model.IsSecritaryEditable*/;

        }
        protected void SetFlagsState(AdditionalMissionOrderEditModel model, bool state)
        {
            model.IsEditable = state;
            model.IsDeleteAvailable = state;
            model.IsChiefApproveAvailable = state;
            model.IsManagerApproveAvailable = state;
            model.IsUserApprovedAvailable = state;
            model.IsSaveAvailable = state;
            model.IsDelete = state;
            model.IsUserApproved = state;
            model.IsChiefApproved = null;
            model.IsManagerApproved = null;
        }
        protected void SetHiddenFields(AdditionalMissionOrderEditModel model)
        {
            model.IsChiefApprovedHidden = model.IsChiefApproved;
            model.IsChiefApproveNeedHidden = model.IsChiefApproveNeed;
            model.IsManagerApprovedHidden = model.IsManagerApproved;
            model.IsUserApprovedHidden = model.IsUserApproved;
            //model.GoalIdHidden = model.GoalId;
            //model.TypeIdHidden = model.TypeId;
            //model.KindHidden = model.Kind;
            //model.IsResidencePaidHidden = model.IsResidencePaid;
            //model.IsAirTicketsPaidHidden = model.IsAirTicketsPaid;
            //model.IsTrainTicketsPaidHidden = model.IsTrainTicketsPaid;
            //model.AirTicketTypeHidden = model.AirTicketType;
            //model.TrainTicketTypeHidden = model.TrainTicketType;
        }

        public bool SaveAdditionalMissionOrderEditModel(AdditionalMissionOrderEditModel model, out string error)
        {
            error = string.Empty;
            User user = null;
            try
            {
                user = UserDao.Load(model.UserId);
                IUser current = AuthenticationService.CurrentUser;
                MissionOrder missionOrder = MissionOrderDao.Load(model.Id);
                if (missionOrder.Version != model.Version)
                {
                    error = "Приказ был изменен другим пользователем.";
                    model.ReloadPage = true;
                    return false;
                }
                if (model.IsDelete)
                {
                    missionOrder.DeleteDate = DateTime.Now;
                    MissionOrderDao.SaveAndFlush(missionOrder);
                    model.IsDelete = false;
                }
                else
                {
                    ChangeEntityProperties(current, missionOrder, model, user);
                    MissionOrderDao.SaveAndFlush(missionOrder);
                    if (missionOrder.Version != model.Version)
                    {
                        missionOrder.EditDate = DateTime.Now;
                        MissionOrderDao.SaveAndFlush(missionOrder);
                    }
                }
                if (missionOrder.DeleteDate.HasValue)
                    model.IsDeleted = true;
                model.DocumentNumber = missionOrder.Number + "-изм";
                model.Version = missionOrder.Version;
                model.DateCreated = missionOrder.CreateDate.ToShortDateString();
                SetFlagsState(missionOrder.Id, user, missionOrder, model);
                return true;
            }
            catch (Exception ex)
            {
                MissionOrderDao.RollbackTran();
                Log.Error("Error on SaveAdditionalMissionOrderEditModel:", ex);
                error = string.Format("Исключение:{0}", ex.GetBaseException().Message);
                return false;
            }
            finally
            {
                SetUserInfoModel(user, model, UserManualRole.ApprovesMissionOrders);
                //LoadDictionaries(model);
                SetHiddenFields(model);
            }
        }
        protected void ChangeEntityProperties(IUser current, MissionOrder entity, AdditionalMissionOrderEditModel model, User user)
        {

            if (model.IsEditable)
            {
                entity.NeedToAcceptByChiefAsManager = entity.MainOrder.NeedToAcceptByChiefAsManager;

                SaveMissionTargets(entity, model.Targets);
                DateTime additionalBeginDate = entity.Targets.Min(x => x.BeginDate);
                DateTime additionalEndDate = entity.Targets.Max(x => x.EndDate);
                entity.BeginDate = additionalBeginDate;
                entity.EndDate = additionalEndDate;
                entity.NeedToAcceptByChief = IsAdditionalMissionOrderLong(entity);
                model.IsChiefApproveNeed = IsAdditionalMissionOrderLong(entity);
            }
            bool isDirectorManager = IsDirectorManagerForEmployee(user, current);

            if ((current.UserRole & UserRole.Employee) == UserRole.Employee && current.Id == model.UserId
                && !entity.UserDateAccept.HasValue
                && model.IsUserApproved)
            {
                entity.UserDateAccept = DateTime.Now;
                entity.AcceptUser = UserDao.Load(current.Id);
                if (isDirectorManager)
                {
                    entity.NeedToAcceptByChiefAsManager = true;
                    SendEmailForMissionOrder(CurrentUser, entity, UserRole.Director, true);
                }
                else
                    SendEmailForMissionOrder(CurrentUser, entity, UserRole.Manager, true);
            }

            //за уволенного сотрудника
            if ((user.UserRole & UserRole.DismissedEmployee) == UserRole.DismissedEmployee && (current.UserRole & UserRole.Manager) == UserRole.Manager
                && !entity.UserDateAccept.HasValue
                && model.IsUserApproved)
            {
                entity.UserDateAccept = DateTime.Now;
                entity.AcceptUser = UserDao.Load(current.Id);
                if (isDirectorManager)
                {
                    entity.NeedToAcceptByChiefAsManager = true;
                    SendEmailForMissionOrder(CurrentUser, entity, UserRole.Director, true);
                }
                else
                    SendEmailForMissionOrder(CurrentUser, entity, UserRole.Manager, true);
            }

            bool canEdit = false;
            if (((current.UserRole & UserRole.Manager) == UserRole.Manager &&
                IsCurrentManagerForUser(user, current, out canEdit))
                || HasCurrentManualRoleForUser(user, current, UserManualRole.ApprovesMissionOrders, out canEdit))
            {
                if (!entity.ManagerDateAccept.HasValue)
                {
                    if (model.IsManagerApproved.HasValue)
                    {
                        if (model.IsManagerApproved.Value)
                        {
                            entity.ManagerDateAccept = DateTime.Now;
                            entity.AcceptManager = UserDao.Load(current.Id);
                            if (!entity.NeedToAcceptByChief)
                            {
                                UpdateMissionFlag(entity);
                                SendEmailForMissionOrderConfirm(CurrentUser, entity, true);
                            }
                            else
                            {
                                SendEmailForMissionOrder(CurrentUser, entity, UserRole.Director, true);
                            }
                        }
                        else
                        {
                            model.IsManagerApproved = null;
                            if ((entity.Creator.RoleId & (int)UserRole.Manager) == 0)
                            {
                                entity.UserDateAccept = null;
                                SendEmailForMissionOrderReject(CurrentUser, entity, true);
                            }
                        }
                    }
                }
            }
            if ((current.UserRole & UserRole.Director) == UserRole.Director)
            {
                if (isDirectorManager && !entity.ManagerDateAccept.HasValue)
                {
                    if (model.IsManagerApproved.HasValue)
                    {
                        if (model.IsManagerApproved.Value)
                        {
                            User currentUser = UserDao.Load(current.Id);
                            entity.ManagerDateAccept = DateTime.Now;
                            entity.AcceptManager = currentUser;
                            if (entity.NeedToAcceptByChief)
                            {
                                entity.ChiefDateAccept = DateTime.Now;
                                entity.AcceptChief = currentUser;
                            }
                            UpdateMissionFlag(entity);
                            SendEmailForMissionOrderConfirm(CurrentUser, entity, true);
                        }
                        else
                        {
                            entity.UserDateAccept = null;
                            model.IsManagerApproved = null;
                            SendEmailForMissionOrderReject(CurrentUser, entity, true);
                        }
                    }
                }
                if (entity.NeedToAcceptByChief)
                {
                    if (model.IsChiefApproved.HasValue)
                    {
                        if (model.IsChiefApproved.Value)
                        {
                            User currentUser = UserDao.Load(current.Id);
                            entity.ChiefDateAccept = DateTime.Now;
                            entity.AcceptChief = currentUser;
                            if (isDirectorManager && !entity.ManagerDateAccept.HasValue)
                            {
                                entity.ManagerDateAccept = DateTime.Now;
                                entity.AcceptManager = currentUser;
                            }
                            UpdateMissionFlag(entity);
                            SendEmailForMissionOrderConfirm(CurrentUser, entity, true);
                        }
                        else
                        {
                            entity.UserDateAccept = null;
                            model.IsUserApproved = false;
                            entity.ManagerDateAccept = null;
                            model.IsManagerApproved = null;
                            SendEmailForMissionOrderReject(CurrentUser, entity, true);
                        }
                    }
                }
            }

        }
        protected void UpdateMissionFlag(MissionOrder entity)
        {
            Mission mission = entity.MainOrder.Mission;
            mission.IsAdditionalOrderExists = true;
            MissionDao.SaveAndFlush(mission);
        }
        #endregion
        #region Mission Report
        public MissionReportsListModel GetMissionReportsListModel()
        {
            User user = UserDao.Load(AuthenticationService.CurrentUser.Id);
            IdNameReadonlyDto dep = GetDepartmentDto(user);
            MissionReportsListModel model = new MissionReportsListModel
            {
                UserId = AuthenticationService.CurrentUser.Id,
                DepartmentName = dep.Name,
                DepartmentId = dep.Id,
                DepartmentReadOnly = dep.IsReadOnly,
                //RequestStatuses = GetDeductionStatuses(true),
                //Types = GetDeductionTypes(true)
            };
            SetInitialDates(model);
            SetDictionariesToModel(model);
            SetInitialStatus(model);
            //SetIsAvailable(model);
            return model;
        }
        protected void SetInitialStatus(MissionReportsListModel model)
        {
            switch (CurrentUser.UserRole)
            {
                case UserRole.Accountant:
                    model.StatusId = 8;
                    break;
                case UserRole.Manager:
                    model.StatusId = 7;
                    break;
                default:
                    break;
            }
        }
        public void SetDictionariesToModel(MissionReportsListModel model)
        {
            model.Statuses = GetMrStatuses();
        }
        public Result AddStorno(int MissionReportId, decimal StornoSum, string StornoComment, int StornoDeductionNumber)
        {
            
            var mr=MissionReportDao.Load(MissionReportId);
            var deductions = DeductionDao.QueryExpression(x => x.Number == StornoDeductionNumber);
            if (deductions == null || !deductions.Any())
            {
                return new Result(false, "Не найдено удержание");
            }
            mr.StornoDeduction = deductions.First();
            mr.StornoAddedBy = UserDao.Load(CurrentUser.Id);
            mr.StornoSum = StornoSum;
            mr.StornoAddedDate = DateTime.Now;
            mr.StornoComment = StornoComment;
            MissionReportDao.SaveAndFlush(mr);
            if (mr != null)
            {
                var md = ManualDeductionDao.QueryExpression(x => x.MissionReport.Id == mr.Id);
                if (md!=null && md.Any())
                {
                    foreach (var el in md)
                    {
                        var deduction = DeductionDao.QueryExpression(x => x.ManualDeduction.Id == el.Id);
                        if (deduction != null && deduction.Any()) continue;
                        //MR.UserSumReceived+MR.PurchaseBookAllSum-MR.StornoSum
                        el.AllSum = mr.UserSumReceived + mr.PurchaseBookAllSum - mr.StornoSum;
                        ManualDeductionDao.SaveAndFlush(el);
                    }
                }
            }
            return new Result(true,"Готово.");
        }
        public List<IdNameDto> GetMrStatuses()
        {
            //var requestStatusesList = RequestStatusDao.LoadAllSorted().ToList().ConvertAll(x => new IdNameDto(x.Id, x.Name));
            List<IdNameDto> moStatusesList = new List<IdNameDto>
                                                       {
                                                           new IdNameDto(1, "Одобрен сотрудником"),
                                                           new IdNameDto(2, "Не одобрен сотрудником"),
                                                           new IdNameDto(3, "Одобрен руководителем"),
                                                           new IdNameDto(4, "Не одобрен руководителем"),
                                                           new IdNameDto(5, "Одобрен бухгалтером"),
                                                           new IdNameDto(6, "Не одобрен бухгалтером"),
                                                           new IdNameDto(7, "Требует одобрения руководителем"),
                                                           new IdNameDto(8, "Требует одобрения бухгалтером"),
                                                           new IdNameDto(10, "Отклонен"),
                                                           new IdNameDto(9, "Выгружен в 1С"),
                                                           new IdNameDto(11, "Выгружен в удержания")
                                                       }.OrderBy(x => x.Name).ToList();
            moStatusesList.Insert(0, new IdNameDto(0, SelectAll));
            return moStatusesList;
        }
        public void SetMissionReportsListModel(MissionReportsListModel model, bool hasError)
        {
            SetDictionariesToModel(model);
            //SetIsAvailable(model);
            User user = UserDao.Load(model.UserId);
            //model.RequestStatuses = GetDeductionStatuses(true);
            //model.Types = GetDeductionTypes(true);
            if (hasError)
                model.Documents = new List<MissionReportDto>();
            else
            {
                //if(model.IsApproveClick)
                //{
                //    model.IsApproveClick = false;
                //    List<int> idsForApprove = model.Documents.Where(x => x.IsChecked).Select(x => x.Id).ToList();
                //    ApproveOrders(model,idsForApprove);
                //}
                SetDocumentsToModel(model, user);
            }
            //model.Documents = new List<MissionOrderDto>();
        }
        public void SetDocumentsToModel(MissionReportsListModel model, User user)
        {
            UserRole role = CurrentUser.UserRole;
            //model.Documents = new List<MissionOrderDto>();
            model.Documents = MissionReportDao.GetDocuments(
                user.Id,
                role,
                //GetDepartmentId(model.Department),
                model.DepartmentId,
                //model.PositionId,
                //model.TypeId,
                //0,
                model.StatusId,
                model.BeginDate,
                model.EndDate,
                model.UserName,
                model.Number,
                model.SortBy,
                model.SortDescending);
        }

        public MissionReportEditModel GetMissionReportEditModel(int id)
        {
            MissionReport entity = null;
            if (id != 0)
            {
                entity = MissionReportDao.Load(id);
                if (entity == null)
                    throw new ValidationException(string.Format("Не найден авансовый отчет (id {0}) в базе данных", id));
            }
            MissionReportEditModel model = new MissionReportEditModel
            {
                Id = id,
                UserId = entity.User.Id
            };
            User user = UserDao.Load(model.UserId);
            IUser current = AuthenticationService.CurrentUser;
            if (!CheckUserMrRights(user, current, id, entity, false))
                throw new ArgumentException("Доступ запрещен.");
            model.Id = entity.Id;
            model.Version = entity.Version;
            model.DocumentTitle = string.Format("Авансовый отчет № АО{0} о командировке к Приказу № {0} на командировку", entity.Number);
            model.OrderDates = FormatDate(entity.MissionOrder.BeginDate) + " - " +
                               FormatDate(entity.MissionOrder.EndDate);
            //entity.MissionOrder.BeginDate.ToShortDateString() + " - " + entity.MissionOrder.EndDate.ToShortDateString();
            model.AdditionalOrderDates = entity.AdditionalMissionOrder != null
                ? FormatDate(entity.AdditionalMissionOrder.BeginDate) + " - " + FormatDate(entity.AdditionalMissionOrder.EndDate)
                : string.Empty;

            model.DocumentNumber = entity.Number.ToString();
            if (user.Dismissals != null)
                model.IsUserDismissal = user.Dismissals.Any(x => x.DeleteDate == null && x.UserDateAccept != null);
            if (entity.Deduction != null)
                model.DeductionDocNumber = entity.Deduction.Number;
            model.DateCreated = entity.CreateDate.ToShortDateString();
            model.Hotels = entity.Hotels;
            model.ArchiveDate = FormatDate(entity.ArchiveDate);
            model.ArchiveNumber = entity.ArchiveNumber;
            model.IsSend1C = entity.SendTo1C.HasValue;
            SetUserInfoModel(user, model);
            LoadDictionaries(model);
            SetFlagsState(id, user, entity, model);
            LoadCosts(model, entity);
            SetStaticFields(model, entity);
            SetHiddenFields(model);
            return model;
        }
        protected void LoadTransactions(MissionReportEditModel model, MissionReportCost cost, CostDto dto)
        {
            List<TransactionDto> trans = new List<TransactionDto>();
            if (cost.AccountingTransactions != null)
            {
                trans.AddRange(cost.AccountingTransactions.Select(tran => new TransactionDto
                {
                    TranId = tran.Id,
                    Credit = tran.CreditAccount.Number,
                    CreditId = tran.CreditAccount.Id,
                    Debit = tran.DebitAccount.Number,
                    DebitId = tran.DebitAccount.Id,
                    Sum = tran.Sum,
                    IsEditable = model.IsAccountantEditable,
                }));
            }
            dto.Trans = trans.ToArray();
            dto.IsTransactionAvailable = model.IsAccountantEditable
                && ((cost.BookOfPurchaseSum.HasValue && cost.BookOfPurchaseSum.Value > 0) || !cost.IsCostFromPurchaseBook);
        }
        protected void LoadCosts(MissionReportEditModel model, MissionReport entity)
        {
            //List<MissionReportCostType> types = MissionReportCostTypeDao.LoadAll().ToList();
            List<CostDto> list = new List<CostDto>();
            decimal userSum = 0;
            decimal pbSum = 0;
            decimal accSum = 0;
            decimal gradeSum = 0;
            if (entity.Costs != null)
            {
                List<IdEntityIdDto> attachments = RequestAttachmentDao.LoadAttachmentsForEntitiesIdsList(entity.Costs.ToList().ConvertAll(x => x.Id),
                                                                       RequestAttachmentTypeEnum.MissionReportCost).ToList();
                foreach (MissionReportCost cost in entity.Costs.OrderBy(x => x.Type.SortOrder).ThenBy(x => x.Id))
                {
                    IdEntityIdDto attachment = attachments.Where(x => x.EntityId == cost.Id).FirstOrDefault();
                    CostDto dto = new CostDto
                    {
                        AccountantSum = cost.AccountantSum,
                        CostId = cost.Id,
                        CostTypeId = cost.Type.Id
                            //,Count = cost.Cnt
                        ,
                        GradeSum = cost.Sum
                        ,
                        Name = cost.Type.Name
                        ,
                        PurchaseBookSum = cost.BookOfPurchaseSum
                        ,
                        UserSum = cost.UserSum
                        ,
                        SortOrder = cost.Type.SortOrder
                        ,
                        IsEditable = model.IsEditable && !cost.IsCostFromPurchaseBook//!cost.IsCostFromOrder
                        ,
                        IsDeleteAvailable = model.IsEditable && !cost.IsCostFromOrder
                        ,
                        ScanId = attachment == null ? 0 : attachment.Id
                        ,
                        AddScanAvailable = model.IsEditable && !cost.IsCostFromPurchaseBook && (cost.Type.Id != DailyCostTypeId)
                        ,
                        DeleteScanAvailable = model.IsEditable && attachment != null
                    };
                    if (!cost.IsCostFromPurchaseBook && (cost.Type.Id != DailyCostTypeId) && (attachment == null) && model.IsEditable)
                        model.IsAttachmentsInvalid = true;
                    LoadTransactions(model, cost, dto);
                    list.Add(dto);
                }
                gradeSum = entity.Costs.Sum(x => x.Sum).Value;
                userSum = entity.Costs.Sum(x => x.UserSum).Value;
                pbSum = entity.Costs.Sum(x => x.BookOfPurchaseSum).Value;
                accSum = entity.Costs.Sum(x => x.AccountantSum).Value;
            }
            list.Add(new CostDto
            {
                SortOrder = -1,
                CostId = 0,
                Name = "Итого расходов",
                GradeSum = gradeSum,
                UserSum = userSum,
                PurchaseBookSum = pbSum,
                AccountantSum = accSum,
            });
            list.Add(new CostDto
            {
                SortOrder = -2,
                CostId = 0,
                Name = "Получено в подотчет",
                UserSum = entity.UserSumReceived,
            });
            list.Add(new CostDto
            {
                SortOrder = -3,
                CostId = 0,
                Name = @"""-"" Долг за сотрудником/""+"" Долг за организацией",
                UserSum = accSum - pbSum - entity.UserSumReceived,
                IsHidden = !entity.AccountantDateAccept.HasValue
            });
            int i = 1;
            foreach (CostDto dto in list.Where(x => x.CostId != 0))
            {
                dto.Number = i++;
            }
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            bool isTransactionHidden = ((CurrentUser.UserRole & UserRole.Manager) == UserRole.Manager) || ((CurrentUser.UserRole & UserRole.Employee) == UserRole.Employee);
            JsonCostsList res = new JsonCostsList { List = list.ToArray(), IsTransactionsHidden = isTransactionHidden };
            model.Costs = jsonSerializer.Serialize(res);
        }
        // hardcode in javascript
        protected static int GetSortOrder(MissionReportCost cost)
        {
            return cost.Type.SortOrder;
        }
        protected void SetUserInfoModel(User user, MissionReportEditModel model)
        {
            SetUserInfoModel(user, (UserInfoModel)model, UserManualRole.ApprovesMissionOrders);
            model.UserFio = "Сотрудник " + user.FullName;
        }
        protected void SetStaticFields(MissionReportEditModel model, MissionReport entity)
        {
            if (entity == null)
            {
                Log.Warn("MissionReportEdit setStaticFields: entity == null");
                return;
            }
            if (entity.AcceptManager != null && entity.ManagerDateAccept.HasValue)
                model.ManagerFio = entity.AcceptManager.FullName + " " +
                    entity.ManagerDateAccept.Value.ToShortDateString();// +", " + entity.AcceptAccountant.Email;
            if (entity.AcceptAccountant != null && entity.AccountantDateAccept.HasValue)
                model.AccountantFio = entity.AcceptAccountant.FullName + " " +
                    entity.AccountantDateAccept.Value.ToShortDateString();// +", " + entity.AcceptAccountant.Email;
            if (entity.Archivist != null)
                model.ArchivistFio = entity.Archivist.FullName;
        }
        protected void SetFlagsState(int id, User user, MissionReport entity, MissionReportEditModel model)
        {
            SetFlagsState(model, false);
            var surchargeDao = Ioc.Resolve<ISurchargeDao>();
            if (surchargeDao != null)
                model.IsSurchargeAvailable = !surchargeDao.IsSurchargeAvailable(entity.Id);
            else model.IsSurchargeAvailable = true;
            if (entity.StornoAddedDate.HasValue)
            {
                model.StornoAddedDate = entity.StornoAddedDate.Value;
                model.StornoComment = entity.StornoComment;
                model.StornoAddedBy = entity.StornoAddedBy!=null?entity.StornoAddedBy.Name:"";
                model.StornoSum = entity.StornoSum;
            }
            UserRole currentUserRole = AuthenticationService.CurrentUser.UserRole;
            if (entity.ManualDeductions != null && entity.ManualDeductions.Any())
            {
                var manualded = entity.ManualDeductions.Where(x => x.Deductions.Any(d=>d.SendTo1C.HasValue));
                if (manualded!=null )
                {
                    List<Deduction> deductionList=new List<Deduction>();
                   foreach(var el in manualded)
                   {
                       deductionList.AddRange(el.Deductions.Where(x=>x.SendTo1C.HasValue).ToArray());
                   }
                   if(deductionList.Any())
                       model.ManualDeductions=deductionList.Select(x=> new ManualDeductionDto { UserId = x.Id, AllSum = x.Sum, DeductionDate = x.DeductionDate, SendTo1C = x.SendTo1C.HasValue ? x.SendTo1C.Value.ToShortDateString() : "", UserName = x.User.Name, DeleteDate = x.DeleteDate.HasValue ? x.DeleteDate.Value.ToShortDateString() : "" }).ToList();
                }                
            }
            model.IsUserApproved = entity.UserDateAccept.HasValue;
            model.IsManagerApproved = entity.ManagerDateAccept.HasValue;
            model.IsAccountantApproved = entity.AccountantDateAccept.HasValue;
            model.IsDeleted = entity.DeleteDate.HasValue;
            switch (currentUserRole)
            {
                case UserRole.Employee:
                    //if ((entity.Creator.RoleId & (int)UserRole.Employee) > 0)
                    //{
                    if (!entity.UserDateAccept.HasValue && !entity.DeleteDate.HasValue)
                    {
                        if (entity.AdditionalMissionOrder == null)
                        {
                            model.IsCreateAdditionalOrderAvailable = true;
                            model.IsEditable = true;
                            model.IsUserApprovedAvailable = true;
                        }
                        else if (IsAdditionalMissionOrderConfirmByUserOrExpired(entity.AdditionalMissionOrder))
                        {
                            model.IsEditable = true;
                            model.IsUserApprovedAvailable = true;
                        }
                    }
                    if (entity.AccountantDateAccept.HasValue && !entity.DeleteDate.HasValue)
                    {
                        model.IsPrintArchivistAddressAvailable = true;
                        if (!entity.IsDocumentsSaveToArchive)
                            model.IsDocumentsSaveToArchiveAvailable = true;
                    }
                    //}
                    break;
                case UserRole.Manager:
                    bool canEdit;
                    bool isUserManager = IsCurrentManagerForUser(user, AuthenticationService.CurrentUser, out canEdit) || HasCurrentManualRoleForUser(user, AuthenticationService.CurrentUser, UserManualRole.ApprovesMissionOrders, out canEdit);

                    if (!entity.ManagerDateAccept.HasValue && !entity.DeleteDate.HasValue
                        && entity.UserDateAccept.HasValue && isUserManager && canEdit)
                    {
                        model.IsManagerRejectAvailable = true;
                        if (entity.AdditionalMissionOrder == null ||
                            IsAdditionalMissionOrderConfirmRejectOrExpired(entity.AdditionalMissionOrder))
                            model.IsManagerApproveAvailable = true;
                    }

                    //за уволенного сотрудника
                    if ((user.UserRole & UserRole.DismissedEmployee) == UserRole.DismissedEmployee)
                    {
                        if (!entity.UserDateAccept.HasValue && !entity.DeleteDate.HasValue)
                        {
                            if (entity.AdditionalMissionOrder == null)
                            {
                                model.IsCreateAdditionalOrderAvailable = true;
                                model.IsEditable = true;
                                model.IsUserApprovedAvailable = true;
                            }
                            else if (IsAdditionalMissionOrderConfirmByUserOrExpired(entity.AdditionalMissionOrder))
                            {
                                model.IsEditable = true;
                                model.IsUserApprovedAvailable = true;
                            }
                        }
                        if (entity.AccountantDateAccept.HasValue && !entity.DeleteDate.HasValue)
                        {
                            model.IsPrintArchivistAddressAvailable = true;
                            if (!entity.IsDocumentsSaveToArchive)
                                model.IsDocumentsSaveToArchiveAvailable = true;
                        }
                    }

                    //}
                    break;
                case UserRole.Accountant:
                    if (entity.ManagerDateAccept.HasValue && !entity.DeleteDate.HasValue
                        && !entity.SendTo1C.HasValue)
                    {
                        if (!entity.AccountantDateAccept.HasValue)
                            model.IsAccountantRejectAvailable = true;
                        if (entity.AdditionalMissionOrder == null ||
                            IsAdditionalMissionOrderConfirmRejectOrExpired(entity.AdditionalMissionOrder))
                        {
                            if (!entity.AccountantDateAccept.HasValue)
                            {
                                model.IsAccountantEditable = true;
                                model.IsAccountantApproveAvailable = true;
                                //model.IsAccountantRejectAvailable = true;
                                if (model.IsAccountantApproveAvailable &&
                                    entity.Costs.Any(x => x.IsCostFromPurchaseBook &&
                                                          (!x.BookOfPurchaseSum.HasValue /*|| x.BookOfPurchaseSum == 0*/)))
                                    model.IsAccountantApproveAvailable = false;

                            }
                            /*else
                            {
                                model.IsAccountantRejectAvailable = true;
                            }*/
                        }
                    }
                    break;
                case UserRole.Archivist:
                    if (entity.AccountantDateAccept.HasValue && !entity.DeleteDate.HasValue && !entity.ArchiveDate.HasValue)
                        model.IsArchivistEditable = true;
                    break;
            }
            model.IsSaveAvailable = model.IsEditable || model.IsUserApprovedAvailable
                || model.IsManagerApproveAvailable
                || model.IsAccountantEditable
                || model.IsAccountantApproveAvailable
                || model.IsArchivistEditable;

        }
        protected bool IsAdditionalMissionOrderConfirmByUserOrExpired(MissionOrder entity)
        {
            return ((entity.ManagerDateAccept.HasValue && !entity.NeedToAcceptByChief) ||
                    (entity.ChiefDateAccept.HasValue && entity.NeedToAcceptByChief) ||
                   (DateTime.Today > entity.CreateDate.AddMonths(1)));
        }
        protected bool IsAdditionalMissionOrderConfirmRejectOrExpired(MissionOrder entity)
        {
            return (entity.NeedToAcceptByChief && entity.ChiefDateAccept.HasValue) ||
                   (!entity.NeedToAcceptByChief && entity.ManagerDateAccept.HasValue) ||
                   entity.DeleteDate.HasValue ||
                   (DateTime.Today > entity.CreateDate.AddMonths(1));
        }
        protected void SetFlagsState(MissionReportEditModel model, bool state)
        {
            model.IsEditable = state;
            model.IsAccountantEditable = state;
            model.IsManagerApproveAvailable = state;
            model.IsUserApprovedAvailable = state;
            model.IsAccountantApproveAvailable = state;
            model.IsSaveAvailable = state;
            model.IsManagerApproved = state;
            model.IsUserApproved = state;
            model.IsAccountantApproved = state;
            model.IsManagerRejectAvailable = state;
            model.IsAccountantRejectAvailable = state;
            model.IsDocumentsSaveToArchiveAvailable = state;
            model.IsPrintArchivistAddressAvailable = state;
            model.IsArchivistEditable = state;
            model.IsCreateAdditionalOrderAvailable = state;
        }
        protected void SetHiddenFields(MissionReportEditModel model)
        {
            model.IsManagerApprovedHidden = model.IsManagerApproved;
            model.IsUserApprovedHidden = model.IsUserApproved;
            model.IsAccountantApprovedHidden = model.IsAccountantApproved;
        }
        protected void LoadDictionaries(MissionReportEditModel model)
        {
            //model.CommentsModel = GetCommentsModel(model.Id, (int)RequestTypeEnum.Dismissal);
            //model.Statuses = GetTimesheetStatusesForDismissal();
            //model.Types = GetMissionTypes(false);
            //model.Kinds = GetMissionOrderKinds();
            //model.Goals = GetMissionGoals(false);
            model.CommentsModel = GetCommentsModel(model.Id, (int)RequestTypeEnum.MissionReport);
            model.AttachmentsModel = GetMrAttachmentsModel(model.Id, RequestAttachmentTypeEnum.MissionReport);
        }
        public bool CheckUserMrRights(User user, IUser current, int entityId, MissionReport entity, bool isSave)
        {
            switch (current.UserRole)
            {
                case UserRole.Employee:
                    if (user.Id != current.Id)
                    {
                        Log.ErrorFormat("CheckUserMrRights user.Id {0} current.Id {1}", user.Id, current.Id);
                        return false;
                    }
                    return true;
                case UserRole.Manager:
                    bool canEdit;
                    bool isManager = IsCurrentManagerForUser(user, current, out canEdit) || HasCurrentManualRoleForUser(user, current, UserManualRole.ApprovesMissionOrders, out canEdit);
                    if (isManager)
                    {
                        if (isSave)
                            return canEdit;
                        return true;
                    }
                    Log.ErrorFormat("CheckUserMrRights user.Id {0} current.Id {1} ", user.Id, current.Id);
                    return false;
                case UserRole.Estimator:
                case UserRole.OutsourcingManager:
                //case UserRole.Secretary:
                //    return true;
                case UserRole.Accountant:
                case UserRole.Archivist:
                    return true;
                case UserRole.Findep:
                    if (isSave)
                        return false;
                    return true;
                //case UserRole.Director:
                //    if (entityId > 0)
                //    {
                //        if (entity.NeedToAcceptByChief)
                //            return true;
                //        return IsDirectorManagerForEmployee(user, current);
                //    }
                //    return false;
            }
            return false;
        }
        public bool SaveMissionReportEditModel(MissionReportEditModel model, out string error)
        {
            error = string.Empty;
            User user = null;
            
            MissionReport missionReport = null;
            try
            {
                user = UserDao.Load(model.UserId);
                IUser current = AuthenticationService.CurrentUser;
                missionReport = MissionReportDao.Load(model.Id);
                model.DocumentTitle = string.Format("Авансовый отчет № АО{0} о командировке к Приказу № {0} на командировку", missionReport.Number);
                model.DocumentNumber = missionReport.Number.ToString();
                model.DateCreated = missionReport.CreateDate.ToShortDateString();
                if (!CheckUserMrRights(user, current, model.Id, missionReport, true))
                {
                    error = "Редактирование отчета запрещено";
                    return false;
                }
                if (missionReport.Version != model.Version)
                {
                    error = "Отчет был изменен другим пользователем.";
                    model.ReloadPage = true;
                    return false;
                }
                ChangeEntityProperties(current, missionReport, model, user, out error);
                MissionReportDao.SaveAndFlush(missionReport);
                if (missionReport.Version != model.Version)
                {
                    missionReport.EditDate = DateTime.Now;
                    MissionReportDao.SaveAndFlush(missionReport);
                }
                model.Version = missionReport.Version;
                SetFlagsState(missionReport.Id, user, missionReport, model);
                return true;
            }
            catch (Exception ex)
            {
                MissionReportDao.RollbackTran();
                Log.Error("Error on SaveMissionReportEditModel:", ex);
                error = string.Format("Исключение:{0}", ex.GetBaseException().Message);
                return false;
            }
            finally
            {
                SetUserInfoModel(user, model);
                LoadDictionaries(model);
                SetStaticFields(model, missionReport);
                SetHiddenFields(model);
            }
        }
        protected void ChangeEntityProperties(IUser current, MissionReport entity,
            MissionReportEditModel model, User user, out string error)
        {
            try{
                var serial = Newtonsoft.Json.JsonConvert.SerializeObject(model);
                Log.Debug("Редактирование Авансового Отчета "+entity.Number+" "+ CurrentUser.Name+" Состояние модели:"+ serial);
            }
            catch(Exception){}
            
            error = string.Empty;
            //bool isDirectorManager = IsDirectorManagerForEmployee(user, current);
            if (model.IsEditable)
            {
                entity.Hotels = model.Hotels;
                SaveMissionCosts(entity, model);
                LoadCosts(model, entity);
            }
            ///Обработка автоматических удержаний
            ///
            try
            {
                if (entity != null)
                {
                    var md = ManualDeductionDao.QueryExpression(x => x.MissionReport.Id == entity.Id);
                    if (md != null && md.Any())
                    {
                        foreach (var el in md)
                        {
                            //MR.UserSumReceived+MR.PurchaseBookAllSum-MR.StornoSum
                            el.AllSum = entity.UserSumReceived + entity.PurchaseBookAllSum - entity.StornoSum;
                            ManualDeductionDao.SaveAndFlush(el);
                        }
                    }
                }
            }catch(Exception){}
            if (model.IsAccountantEditable)
            {
                if (model.IsAccountantReject) //?? flag can be set when AccountantEditable is not set
                {
                    foreach (MissionReportCost c in entity.Costs)
                    {
                        List<AccountingTransaction> deleted = c.AccountingTransactions.ToList();
                        foreach (AccountingTransaction tran in deleted)
                            c.AccountingTransactions.Remove(tran);
                        c.AccountantSum = 0;

                    }
                    entity.AccountantAllSum = entity.Costs.Sum(x => x.AccountantSum).Value;
                    MissionReportDao.SaveAndFlush(entity);
                    //model.IsEditable = false;
                    model.IsAccountantEditable = false;
                    LoadCosts(model, entity);
                }
                else
                    SaveMissionCostsTransactions(entity, model);
            }
            if (entity.AccountantDateAccept.HasValue &&
                !entity.DeleteDate.HasValue &&
                !entity.ArchiveDate.HasValue &&
                (current.UserRole & UserRole.Archivist) == UserRole.Archivist)
            {
                entity.ArchiveDate = DateTime.Parse(model.ArchiveDate);
                entity.ArchiveNumber = model.ArchiveNumber;
                entity.Archivist = UserDao.Load(current.Id);
            }
            if ((current.UserRole & UserRole.Employee) == UserRole.Employee && current.Id == model.UserId
                && !entity.UserDateAccept.HasValue
                && model.IsUserApproved)
            {
                Log.Debug("Сотрудник установил флажок согласования: "+DateTime.Now+" в АО №"+entity.Number);
                if (model.IsAttachmentsInvalid)
                {
                    error = string.Format(@"Ордер сохранен успешно, но не может быть согласован - не ко всем статьям расходов прикреплены документы.");
                    Log.Debug(DateTime.Now+" в АО №"+entity.Number +" "+error);
                }
                else
                {
                    entity.UserDateAccept = DateTime.Now;
                    entity.AcceptUser = UserDao.Load(current.Id);
                    SetMissionCostsEditable(model, false);
                }
            }

            //за уволенных сотруднико можно подтвердить
            if ((user.UserRole & UserRole.DismissedEmployee) == UserRole.DismissedEmployee && (current.UserRole & UserRole.Manager) == UserRole.Manager
            && !entity.UserDateAccept.HasValue
            && model.IsUserApproved)
            {
                Log.Debug("Сотрудник(уволеный?) установил флажок согласования: "+DateTime.Now+" в АО №"+entity.Number);
                if (model.IsAttachmentsInvalid)
                {
                    error = string.Format(@"Ордер сохранен успешно, но не может быть согласован - не ко всем статьям расходов прикреплены документы.");
                    Log.Debug(DateTime.Now+" в АО №"+entity.Number +" "+error);
                }
                else
                {
                    entity.UserDateAccept = DateTime.Now;
                    entity.AcceptUser = UserDao.Load(current.Id);
                    SetMissionCostsEditable(model, false);
                }
            }

            bool canEdit;
            if (((current.UserRole & UserRole.Manager) == UserRole.Manager && IsCurrentManagerForUser(user, current, out canEdit)
                || HasCurrentManualRoleForUser(user, current, UserManualRole.ApprovesMissionOrders, out canEdit))
                && !entity.ManagerDateAccept.HasValue
                && entity.UserDateAccept.HasValue)
            {
                if (model.IsManagerReject)
                {
                    #region Отправка писем сотруднику об отклонении отчета
                    string address = entity.User.Email;
                    if (!string.IsNullOrWhiteSpace(address))
                    {
                        SendEmail(address, "Руководитель отклонил Ваш авансовый отчёт.", String.Format("Руководитель {0}   отклонил   Ваш АО №{1}", CurrentUser.Name, entity.Number));
                    }
                    #endregion
                    entity.UserDateAccept = null;
                    entity.AcceptUser = null;
                    model.IsManagerApproved = false;
                    entity.AcceptManager = UserDao.Load(current.Id);
                }
                else if (model.IsManagerApproved)
                {
                    #region Отправка писем сотруднику об отклонении отчета
                    string address = entity.User.Email;
                    if (!string.IsNullOrWhiteSpace(address))
                    {
                        SendEmail(address, "Руководитель согласовал Ваш авансовый отчёт.", String.Format("Руководитель {0} согласовал Ваш АО №{1}", CurrentUser.Name, entity.Number));
                    }
                    #endregion
                    entity.ManagerDateAccept = DateTime.Now;
                    entity.AcceptManager = UserDao.Load(current.Id);
                }
            }
            if (!model.IsManagerReject && model.IsManagerApproved && !entity.ManagerDateAccept.HasValue)
            {
                Log.ErrorFormat(@"Logic error: model.IsManagerApproved is set but entity.ManagerDateAccept is not set for 
                        mission report {0}, currentUserId {1} ", entity.Id, current.Id);
            }
            if ((current.UserRole & UserRole.Accountant) == UserRole.Accountant && entity.ManagerDateAccept.HasValue)
            {
                if (!entity.AccountantDateAccept.HasValue)
                {
                    if (model.IsAccountantReject)
                    {
                        #region Отправка писем руководителю и сотруднику об отклонении отчета
                        string address = entity.User.Email;
                        if(!string.IsNullOrWhiteSpace(address))
                        {
                            SendEmail(address,"Бухгалтер отклонил Ваш авансовый отчёт.",String.Format("Бухгалтер {0}   отклонил   Ваш АО №{1}",CurrentUser.Name,entity.Number));
                        }
                        /*address = entity.AcceptManager!=null?entity.AcceptManager.Email:"";
                        if(!string.IsNullOrWhiteSpace(address))
                        {
                            SendEmail(address, String.Format("Бухгалтер отклонил АО №{0} сотрудника {1}", entity.Number, entity.User.Name), String.Format("Бухгалтер {0} отклонил АО №{1} сотрудника {2}", CurrentUser.Name, entity.Number, entity.User.Name));
                        }*///Письма руководителю слать только если галку снимаем
                        #endregion
                        entity.AccountantDateAccept = null;
                        entity.AcceptAccountant = UserDao.Load(current.Id);
                        //entity.ManagerDateAccept = null;
                        //entity.AcceptManager = null;  руководителя галку  не снимаем с пор не давних. письмо отправлять нужно, если галку снимать
                        entity.UserDateAccept = null;
                        entity.AcceptUser = null;
                        //SetMissionTransactionEditable(model, true);
                        model.IsAccountantReject = false;
                        model.IsAccountantApproved = false;
                        model.IsUserApproved = false;
                    }
                    else if (model.IsAccountantApproved)
                    {
                        #region Отправка писем руководителю и сотруднику о согласовании отчета
                        string address = entity.User.Email;
                        if (!string.IsNullOrWhiteSpace(address))
                        {
                            SendEmail(address, "Бухгалтер принял Ваш авансовый отчёт.", String.Format("Бухгалтер {0} принял Ваш АО №{1}", CurrentUser.Name, entity.Number));
                        }
                        address = entity.AcceptManager != null ? entity.AcceptManager.Email : "";
                        if (!string.IsNullOrWhiteSpace(address))
                        {
                            SendEmail(address, String.Format("Бухгалтер принял АО №{0} сотрудника {1}", entity.Number, entity.User.Name), String.Format("Бухгалтер {0} принял АО №{1} сотрудника {2}", CurrentUser.Name, entity.Number, entity.User.Name));
                        }
                        #endregion
                        entity.AccountantDateAccept = DateTime.Now;
                        entity.AcceptAccountant = UserDao.Load(current.Id);
                        SetMissionTransactionEditable(model, false);
                    }
                }
                if (model.IsAccountantApprovedCancel)
                {
                    entity.AccountantDateAccept = null;
                    entity.AcceptAccountant = UserDao.Load(current.Id);
                    model.IsAccountantApprovedCancel = false;
                }
                /*else if(entity.AccountantDateAccept.HasValue && model.IsAccountantReject)
                {
                    entity.AccountantDateAccept = null;
                    entity.AcceptAccountant = UserDao.Load(current.Id);
                    SetMissionTransactionEditable(model, true);
                    model.IsAccountantReject = false;
                }*/
            }
        }
        protected void SetMissionCostsEditable(MissionReportEditModel model, bool isEditable)
        {
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            JsonCostsList list = jsonSerializer.Deserialize<JsonCostsList>(model.Costs);
            List<CostDto> costDtos = list.List.Where(x => x.CostId != 0).ToList();
            foreach (CostDto dto in costDtos)
            {
                dto.IsEditable = isEditable;
                dto.IsDeleteAvailable = isEditable;
                dto.DeleteScanAvailable = isEditable;
                dto.AddScanAvailable = isEditable;
            }
            JsonCostsList res = new JsonCostsList { List = list.List.ToArray(), IsTransactionsHidden = list.IsTransactionsHidden };
            model.Costs = jsonSerializer.Serialize(res);
        }
        protected void SetMissionTransactionEditable(MissionReportEditModel model, bool isEditable)
        {
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            JsonCostsList list = jsonSerializer.Deserialize<JsonCostsList>(model.Costs);
            List<CostDto> costDtos = list.List.Where(x => x.CostId != 0).ToList();
            foreach (CostDto dto in costDtos)
            {
                dto.IsTransactionAvailable = isEditable;
                if (dto.Trans != null && dto.Trans.Count() > 0)
                {
                    foreach (TransactionDto tran in dto.Trans)
                    {
                        tran.IsEditable = isEditable;
                    }
                }
            }
            JsonCostsList res = new JsonCostsList { List = list.List.ToArray(), IsTransactionsHidden = list.IsTransactionsHidden };
            model.Costs = jsonSerializer.Serialize(res);
        }
        protected void SaveMissionCosts(MissionReport entity, MissionReportEditModel model)
        {
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            JsonCostsList list = jsonSerializer.Deserialize<JsonCostsList>(model.Costs);
            List<CostDto> costDtos = list.List.Where(x => x.CostId != 0).ToList();
            if (entity.Costs == null)
                entity.Costs = new List<MissionReportCost>();
            List<MissionReportCost> removed = entity.Costs.Where(x => !costDtos.Any(y => y.CostId == x.Id)).ToList();
            RequestAttachmentDao.DeleteAttachmentsForEntitiesIdsList(removed.ConvertAll(x => x.Id),
                                                                     RequestAttachmentTypeEnum.MissionReportCost);
            foreach (MissionReportCost cost in removed)
                entity.Costs.Remove(cost);
            foreach (CostDto dto in costDtos)
            {
                if (dto.CostId < 0)
                {
                    MissionReportCost newCost = new MissionReportCost();
                    SetCostProperties(dto, newCost, entity);
                    entity.Costs.Add(newCost);
                    //MissionReportCostDao.SaveAndFlush(newCost);
                }
                else
                {
                    MissionReportCost old = entity.Costs.Where(x => x.Id == dto.CostId).FirstOrDefault();
                    if (old == null)
                        throw new ArgumentException(string.Format("Не найден расход (id {0}) в базе данных", dto.CostId));
                    SetCostProperties(dto, old, entity);
                    MissionReportCostDao.SaveAndFlush(old);
                }
            }
            entity.AllSum = entity.Costs.Sum(x => x.Sum).Value;
            entity.UserAllSum = entity.Costs.Sum(x => x.UserSum).Value;
            entity.AccountantAllSum = entity.Costs.Sum(x => x.AccountantSum).Value;
            entity.PurchaseBookAllSum = entity.Costs.Sum(x => x.BookOfPurchaseSum).Value;
        }
        protected void SaveMissionCostsTransactions(MissionReport entity, MissionReportEditModel model)
        {
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            JsonCostsList list = jsonSerializer.Deserialize<JsonCostsList>(model.Costs);
            List<CostDto> costDtos = list.List.Where(x => x.CostId != 0).ToList();
            List<Account> accounts = AccountDao.LoadAll().ToList();
            foreach (CostDto dto in costDtos)
            {
                MissionReportCost cost = entity.Costs.Where(x => x.Id == dto.CostId).FirstOrDefault();
                if (cost == null)
                    throw new ArgumentException(string.Format("Не найден расход (id {0}) в базе данных", dto.CostId));
                if (dto.Trans == null || dto.Trans.Count() == 0)
                {
                    if (cost.AccountingTransactions != null && cost.AccountingTransactions.Count > 0)
                    {
                        List<AccountingTransaction> deleted = cost.AccountingTransactions.ToList();
                        foreach (AccountingTransaction tran in deleted)
                            cost.AccountingTransactions.Remove(tran);
                    }
                    cost.AccountantSum = 0;
                }
                else
                {
                    List<AccountingTransaction> deleted = cost.AccountingTransactions.Where(x => !dto.Trans.Any(y => y.TranId == x.Id)).ToList();
                    foreach (AccountingTransaction tran in deleted)
                        cost.AccountingTransactions.Remove(tran);
                    foreach (TransactionDto tran in dto.Trans)
                    {
                        if (tran.TranId < 0)
                        {
                            AccountingTransaction newTran = new AccountingTransaction
                            {
                                Cost = cost,
                                CreditAccount = accounts.Where(x => x.Id == tran.CreditId && !x.IsDebitAccount).First(),
                                DebitAccount = accounts.Where(x => x.Id == tran.DebitId && x.IsDebitAccount).First(),
                                Sum = tran.Sum,
                            };
                            cost.AccountingTransactions.Add(newTran);
                        }
                        else
                        {
                            AccountingTransaction existing = cost.AccountingTransactions.Where(x => x.Id == tran.TranId).FirstOrDefault();
                            if (existing == null)
                                throw new ArgumentException(string.Format("Не найдена проводка (id {0}) в базе данных", tran.TranId));
                            existing.CreditAccount = accounts.Where(x => x.Id == tran.CreditId && !x.IsDebitAccount).First();
                            existing.DebitAccount = accounts.Where(x => x.Id == tran.DebitId && x.IsDebitAccount).First();
                            existing.Sum = tran.Sum;
                        }
                    }
                }
                decimal sum = cost.AccountingTransactions.Sum(x => x.Sum);
                cost.AccountantSum = sum;
                missionReportCostDao.SaveAndFlush(cost);
            }
            entity.AccountantAllSum = entity.Costs.Sum(x => x.AccountantSum).Value;
        }
        protected void SetCostProperties(CostDto dto, MissionReportCost cost, MissionReport entity)
        {
            //cost.AccountantSum = dto.AccountantSum;
            //cost.BookOfPurchaseSum = dto.PurchaseBookSum;
            //cost.Sum = dto.GradeSum;
            //cost.Cnt = dto.Count;
            cost.Report = entity;
            cost.Type = MissionReportCostTypeDao.Load(dto.CostTypeId);
            cost.UserSum = dto.UserSum;
        }
        public void SetMissionReportEditCostModel(MissionReportEditCostModel model)
        {
            model.CostTypes = GetCostTypes(false);
        }
        protected List<IdNameDto> GetCostTypes(bool addEmpty)
        {
            List<IdNameDto> typeList = MissionReportCostTypeDao.LoadAll().OrderBy(x => x.SortOrder)
                .ToList().ConvertAll(x => new IdNameDto(x.Id, x.Name));
            if (addEmpty)
                typeList.Insert(0, new IdNameDto(0, string.Empty));
            return typeList;
        }
        public void SetMissionReportEditTranModel(MissionReportEditTranModel model)
        {
            List<Account> list = AccountDao.LoadAll().ToList();
            model.DebitAccounts =
                list.Where(x => x.IsDebitAccount).OrderBy(x => x.Number).ToList().ConvertAll(x => new IdNameDto(x.Id, x.Number));
            model.CreditAccounts =
                list.Where(x => !x.IsDebitAccount).OrderBy(x => x.Number).ToList().ConvertAll(x => new IdNameDto(x.Id, x.Number));
        }
        public RequestAttachmentsModel GetMrAttachmentsModel(int id, RequestAttachmentTypeEnum typeId)
        {
            bool isAddAvailable = false;
            bool isDeleteAvailable = false;
            List<RequestAttachment> list = RequestAttachmentDao.FindManyByRequestIdAndTypeId(id, typeId).ToList();
            if (id > 0)
            {
                MissionReport entity = MissionReportDao.Load(id);
                isAddAvailable = !entity.UserDateAccept.HasValue && (entity.AdditionalMissionOrder == null || IsAdditionalMissionOrderConfirmByUserOrExpired(entity.AdditionalMissionOrder));
                isDeleteAvailable = !entity.UserDateAccept.HasValue && (entity.AdditionalMissionOrder == null || IsAdditionalMissionOrderConfirmByUserOrExpired(entity.AdditionalMissionOrder));
            }
            RequestAttachmentsModel model = new RequestAttachmentsModel
            {
                AttachmentRequestId = id,
                AttachmentRequestTypeId = (int)typeId,
                IsAddAvailable = isAddAvailable && ((CurrentUser.UserRole & UserRole.Employee) == UserRole.Employee),
                Attachments = new List<RequestAttachmentModel>()
            };
            model.Attachments = list.ConvertAll(x =>
                            new RequestAttachmentModel
                            {
                                Attachment = x.FileName,
                                AttachmentId = x.Id,
                                Description = x.Description,
                                IsDeleteAvailable = ((x.CreatorUserRole & CurrentUser.UserRole) > 0) && isDeleteAvailable,
                            });
            return model;
        }
        public PrintMissionReportViewModel GetPrintMissionReportModel(int id)
        {
            PrintMissionReportViewModel model = new PrintMissionReportViewModel();
            MissionReport report = MissionReportDao.Load(id);
            if (report == null)
                throw new ArgumentException(string.Format("Авансовый отчет (id {0}) отсутствует в базе данных.", id));
            SetUserInfoModel(report.User, model, UserManualRole.ApprovesMissionOrders);
            model.OrderNumber = report.MissionOrder.Number.ToString();
            model.DocumentNumber = "АО" + report.Number;
            model.DateCreated = report.CreateDate.ToShortDateString();
            model.Hotels = report.Hotels;
            model.UserFio = report.User.FullName;
            model.UserPosition = report.User.Position.Name;
            if (report.UserDateAccept.HasValue)
                model.UserAcceptDate = report.UserDateAccept.Value.ToShortDateString();
            if (report.ManagerDateAccept.HasValue)
            {
                model.ManagerFio = report.AcceptManager.FullName;
                model.ManagerAcceptDate = report.ManagerDateAccept.Value.ToShortDateString();
                model.ManagerPosition = report.AcceptManager.Position == null ? string.Empty : report.AcceptManager.Position.Name;
            }
            if (report.AccountantDateAccept.HasValue)
            {
                model.AccountantFio = report.AcceptAccountant.FullName;
                model.AccountantAcceptDate = report.AccountantDateAccept.Value.ToShortDateString();
                model.AccountantPosition = report.AcceptAccountant.Position == null ? string.Empty : report.AcceptAccountant.Position.Name;
            }
            List<PrintCostModel> costs = new List<PrintCostModel>();
            int currentNumber = 1;
            //int currentCostType = 0;
            foreach (MissionReportCost cost in report.Costs.OrderBy(x => x.Type.Id))
            {
                PrintCostModel costModel = new PrintCostModel
                {
                    AccountantSum = FormatSum(cost.AccountantSum),
                    CostTypeName = cost.Type.Name,
                    GradeSum = FormatSum(cost.Sum),
                    PurchaseBoolSum = FormatSum(cost.BookOfPurchaseSum),
                    UserSum = FormatSum(cost.UserSum)
                };
                costModel.Number = currentNumber.ToString();
                currentNumber++;
                /*if(currentCostType != cost.Type.Id)
                {
                    costModel.Number = currentNumber.ToString();
                    currentNumber++;
                    currentCostType = cost.Type.Id;
                }*/
                List<PrintTransactionModel> trans = cost.AccountingTransactions.Select(tran =>
                    new PrintTransactionModel
                    {
                        Credit = tran.CreditAccount.Number,
                        Debet = tran.DebitAccount.Number,
                        Sum = FormatSum(tran.Sum)
                    }).ToList();
                costModel.Transactions = trans;
                costs.Add(costModel);
            }
            costs.Add(new PrintCostModel
            {
                AccountantSum = FormatSum(report.AccountantAllSum),
                CostTypeName = "ИТОГО Расходов (руб)",
                GradeSum = FormatSum(report.AllSum),
                Number = string.Empty,//currentNumber.ToString(),
                PurchaseBoolSum = FormatSum(report.PurchaseBookAllSum),
                UserSum = FormatSum(report.UserAllSum),
            });
            //currentNumber++;
            costs.Add(new PrintCostModel
            {

                CostTypeName = "Получено в подотчет",
                Number = string.Empty,//currentNumber.ToString(),
                UserSum = FormatSum(report.UserSumReceived),
            });
            //currentNumber++;
            costs.Add(new PrintCostModel
            {

                CostTypeName = @"""-""Долг за сотрудником/""+""Долг за организацией",
                Number = string.Empty,//currentNumber.ToString(),
                UserSum = FormatSum(report.AccountantAllSum - report.UserSumReceived - report.PurchaseBookAllSum),
            });
            //currentNumber++;
            //model.DocumentNumber = "АО" + model.DocumentNumber;
            model.Costs = costs;
            return model;
        }
        public PrintMissionReportListViewModel GetPrintMissionReportListModel(int id, int reportId)
        {
            PrintMissionReportListViewModel model = new PrintMissionReportListViewModel();
            MissionReport report = MissionReportDao.Load(reportId);
            if (report == null)
                throw new ArgumentException(string.Format("Авансовый отчет (id {0}) отсутствует в базе данных.", id));
            User user = UserDao.Load(id);
            if (user == null)
                throw new ArgumentException(string.Format("Не могу найти архивариуса (id={0}) в базе данных", id));
            model.To = user.FullName;
            model.Address = user.Address;
            model.Costs = report.Costs.Where(x => !x.IsCostFromPurchaseBook).OrderBy(x => x.Type.SortOrder).Select(x => x.Type.Name).ToList();
            return model;
        }

        #endregion
        #region UsersPersonnelData
        public UsersPersonnelDataViewModel GetUsersPersonnelDataListModel()
        {
            var model = new UsersPersonnelDataViewModel();
            return model;
        }
        public UsersPersonnelDataViewModel SetDocuments(UsersPersonnelDataViewModel model)
        {
            model.Documents = UserDao.GetUserPersonnelData(CurrentUser.Id, CurrentUser.UserRole, model.FIO, model.DepartmentId);
            return model;
        }
        public string GetUserInn(int persid)
        {
            try
            {
                 string res = UserDao.GetUserInn(persid);
                return (res);
            }
            catch (Exception ex)
            {
                return ("");
            }
        }
        public string SetUserInn(int persid, string inn)
        {
            try
            {
                UserDao.UpdateUserInn(persid, inn);
                return("Ok");
            }
            catch (Exception ex)
            {
                return ("Fail: " + ex.Message);
            }
        }
        #endregion 
        #region SurchageNote
        public void GetDictionaries(SurchargeNoteEditModel model)
        {

            if (model.Id > 0)
            {
                var attach = RequestAttachmentDao.FindByRequestIdAndTypeId(model.Id, RequestAttachmentTypeEnum.SurchargeNoteAttachment);
                if (attach != null)
                {
                    model.AttachmentId = attach.Id;
                    model.AttachmentName = attach.FileName;
                }
            }
            if (model.Id == 0)
            {
                model.CreatorId = CurrentUser.Id;
                model.CreatorName = CurrentUser.Name;
                model.CreateDate = DateTime.Now;
            }
            var user = UserDao.Load(model.CreatorId);
            if (user != null)
            {
                model.Position = user.Position.Name;
                model.CreatorDepartment = user.Department.Name;
            }
            if (model.CountantDateAccept.HasValue || model.PersonnelDateAccept.HasValue) model.IsEditable = false;
            else model.IsEditable = true;
            model.CountantAccept = model.CountantDateAccept.HasValue;
            model.PersonnelAccept = model.PersonnelDateAccept.HasValue;
            var chiefs = GetChiefsForManager(model.CreatorId);
            IList<User> manualRoleManagers = ManualRoleRecordDao.GetManualRoleHoldersForUser(user.Id, UserManualRole.ApprovesCommonRequests);
            foreach (var manualRoleManager in manualRoleManagers)
            {
                if (!chiefs.Contains(manualRoleManager))
                {
                    chiefs.Add(manualRoleManager);
                }
            }
            StringBuilder chiefsBuilder = new StringBuilder();
            foreach (var chief in chiefs)
            {
                chiefsBuilder.AppendFormat("{0} ({1}), ", chief.Name, chief.Position == null ? "<не указана>" : chief.Position.Name);
            }
            // Cut off trailing ", "
            if (chiefsBuilder.Length >= 2)
            {
                chiefsBuilder.Remove(chiefsBuilder.Length - 2, 2);
            }
            var creator = UserDao.Load(model.CreatorId);
            if (creator != null && creator.Department != null)
            {
                var dep3 = DepartmentDao.GetParentDepartmentWithLevel(creator.Department, 3);
                if (dep3 != null)
                    model.CreatorDepartment3 = dep3.Name;

            }
            model.Chiefs = chiefsBuilder.ToString();
            if (user != null)
            {
                var personnels = user.Personnels;
                if (personnels != null && personnels.Any())
                {
                    model.Personnels = personnels.Aggregate("", (sum, next) =>  sum += next.Name + ',');
                    model.PersonnelsApproved = personnels.Select(x => new IdNameDto { Id = x.Id, Name = x.Name }).ToList();
                }
            }
            model.PayTypes = new List<IdNameDto> { new IdNameDto{Id=1,Name="Фитнес-Плюс компенсационная выплата (#3511)"},
                                                new IdNameDto{Id=2,Name="Скидка на покупку страховой коробочки (#3512)"},
                                                //new IdNameDto{Id=3,Name="Суточные сверх нормы (#4103)"},
                                                //new IdNameDto{Id=4,Name="Стоимость билетов (#4103)"},
                                                new IdNameDto{Id=5,Name="Возмещение ГСМ для командировки (#4103)"},
                                                new IdNameDto{Id=6,Name="Штраф за нарушение ПДД (#4103)"},
                                                new IdNameDto{Id=7,Name="Подарочные сертификаты стимулирующего характера (#3404)"},
                                                new IdNameDto{Id=8,Name="Подарки сотрудникам к праздничным дням (#3401)"},
                                                new IdNameDto{Id=9,Name="Возмещение расходов по переезду работника в другую местность (#3508)"},
                                                new IdNameDto{Id=10,Name="Подарки детям к праздничным дням (#3403)"},
                                                new IdNameDto{Id=11,Name="Подарки денежные стимулирующего характера (#3405)"},
                                                new IdNameDto{Id=12,Name="Начисление (возврат) суммы за ДМС (#3510)"},
                                                new IdNameDto{Id=13,Name="Прочие начисления (#4103)"},
                                                new IdNameDto{Id=14,Name="Начисление (возврат) суммы страхования от несчастных случаев и болезней (#3513)"}
            };
            model.MonthTypes = new List<IdNameDto> { new IdNameDto{Id=1,Name="1-й месяц"}, new IdNameDto{ Id=2, Name="2-й месяц"} };
        }
        public void GetDictionaries(SurchargeNoteListModel model)
        {
            model.Statuses = new List<IdNameDto>();
            model.Statuses.Add(new IdNameDto { Id = 0, Name = "Все" });
            model.Statuses.Add(new IdNameDto { Id = 1, Name = "Заявка создана" });
            model.Statuses.Add(new IdNameDto { Id = 2, Name = "Заявка отработана отделом кадров" });
            model.Statuses.Add(new IdNameDto { Id = 3, Name = "Заявка отработана расчётным отделом" });
            model.Statuses.Add(new IdNameDto { Id = 4, Name = "Заявка отклонена" });
            model.Statuses.Add(new IdNameDto { Id = 5, Name = "Заявка отработана УКДиУ" });
            model.PayTypes = new List<IdNameDto> { new IdNameDto{Id=1,Name="Фитнес-Плюс компенсационная выплата (#3511)"},
                                                new IdNameDto{Id=2,Name="Скидка на покупку страховой коробочки (#3512)"},
                                                //new IdNameDto{Id=3,Name="Суточные сверх нормы (#4103)"},
                                                //new IdNameDto{Id=4,Name="Стоимость билетов (#4103)"},
                                                new IdNameDto{Id=5,Name="Возмещение ГСМ для командировки (#4103)"},
                                                new IdNameDto{Id=6,Name="Штраф за нарушение ПДД (#4103)"},
                                                new IdNameDto{Id=7,Name="Подарочные сертификаты стимулирующего характера (#3404)"},
                                                new IdNameDto{Id=8,Name="Подарки сотрудникам к праздничным дням (#3401)"},
                                                new IdNameDto{Id=9,Name="Возмещение расходов по переезду работника в другую местность (#3508)"},
                                                new IdNameDto{Id=10,Name="Подарки детям к праздничным дням (#3403)"},
                                                new IdNameDto{Id=11,Name="Подарки денежные стимулирующего характера (#3405)"},
                                                new IdNameDto{Id=12,Name="Начисление (возврат) суммы за ДМС (#3510)"},
                                                new IdNameDto{Id=13,Name="Прочие начисления (#4103)"},
                                                new IdNameDto{Id=14,Name="Начисление (возврат) суммы страхования от несчастных случаев и болезней (#3513)"}
            };
            model.MonthTypes = new List<IdNameDto> { new IdNameDto { Id = 1, Name = "1-й месяц" }, new IdNameDto { Id = 2, Name = "2-й месяц" } };
        }
        public SurchargeNoteEditModel GetSurchargeNoteEditModel(int id)
        {
            var user = UserDao.Load(CurrentUser.Id);
            SurchargeNoteEditModel model = new SurchargeNoteEditModel();
            model.DepartmentRequiredLevel = 7;
            if (id == 0)
            {
                model.CreateDate = DateTime.Now;
                model.CreatorId = CurrentUser.Id;
                model.CreatorName = user.Name;
                model.PayDay = DateTime.Now;
                var d3 = DepartmentDao.GetParentDepartmentWithLevel(user.Department, 3);
                model.Dep3Name = d3 != null ? d3.Name : "";
            }
            else
            {
                var entity = SurchargeNoteDao.Load(id);
                model.IsDelete = entity.DeleteDate.HasValue;
                model.PersonnelManagerBankAccept = entity.PersonnelManagerDateAccept.HasValue;
                model.PersonnelManagerBankDateAccept = entity.PersonnelManagerDateAccept;
                model.PersonnelManagerBankName =entity.PersonnelManagerBank!=null? entity.PersonnelManagerBank.Name:"";
                model.Id = entity.Id;
                model.CreateDate = entity.CreateDate;
                model.CreatorId = entity.Creator.Id;
                model.CreatorName = entity.Creator.Name;
                model.Position = entity.Creator.Position.Name;
                model.Number = entity.Number.ToString();
                model.CreatorDepartment = entity.Creator.Department.Name;
                model.PayDay = entity.PayDay;
                model.CountantDateAccept = entity.CountantDateAccept;
                model.CountantName = entity.Countant != null ? entity.Countant.Name : "";
                model.PersonnelDateAccept = entity.PersonnelDateAccept;
                model.PersonnelName = entity.Personnel != null ? entity.Personnel.Name : "";
                model.NoteType = entity.NoteType;
                model.Dep3Name = entity.DocDep3.Name;
                model.DepartmentName = entity.DocDep7.Name;
                model.DepartmentId = entity.DocDep7.Id;
                model.MonthId = entity.MonthId;
                model.PayType = entity.PayType;
                model.PayDayEnd = entity.PayDayEnd;
                model.DismissalDate = entity.DismissalDate;
            }
            GetDictionaries(model);
            if (model.IsDelete) model.IsEditable = false;
                
            return model;
        }
        public SurchargeNoteListModel GetSurchargeNoteListModel()
        {
            SurchargeNoteListModel model = new SurchargeNoteListModel();
            GetDictionaries(model);
            return model;
        }
        public void SaveSurchargeNote(SurchargeNoteEditModel model)
        {
            var creator = UserDao.Load(CurrentUser.Id);

            if (model.Id == 0)
            {
                var entity = new SurchargeNote
                {
                    Creator = creator,
                    CreateDate = DateTime.Now,
                    NoteType = model.NoteType,
                    PayDay = model.NoteType!=3?model.PayDay:DateTime.Now,
                    PayDayEnd = model.PayDayEnd,
                    DismissalDate = model.DismissalDate,
                    PayType = model.PayType,
                    MonthId = model.MonthId,
                    DocDep7 = DepartmentDao.Load(model.DepartmentId),
                    Number = RequestNextNumberDao.GetNextNumberForType((int)((model.NoteType == 0) ? RequestTypeEnum.SurchargeNote0 : 
                                                                             (model.NoteType == 1) ? RequestTypeEnum.SurchargeNote1 :
                                                                             (model.NoteType == 2) ? RequestTypeEnum.SurchargeNote2 :
                                                                             (model.NoteType == 3) ? RequestTypeEnum.SurchargeNote3 :
                                                                             (model.NoteType == 4) ? RequestTypeEnum.SurchargeNote4 :
                                                                             (model.NoteType == 5) ? RequestTypeEnum.SurchargeNote5 :
                                                                             RequestTypeEnum.SurchargeNote6
                                                                             ))
                };
                entity.DocDep3 = DepartmentDao.GetParentDepartmentWithLevel(entity.DocDep7, 3);
                SurchargeNoteDao.SaveAndFlush(entity);
                ChangeModelProperties(model, entity);
            }
            else
            {
                var entity = SurchargeNoteDao.Get(model.Id);
                
                if (model.IsDelete)
                {
                    entity.DeleteDate = DateTime.Now;
                }
                if (!model.PersonnelDateAccept.HasValue && !model.CountantDateAccept.HasValue && model.CreatorId == CurrentUser.Id)
                {
                    //entity.DocumentDepartment = model.DepartmentId;
                    entity.DocDep7 = DepartmentDao.Load(model.DepartmentId);
                    entity.DocDep3 = DepartmentDao.GetParentDepartmentWithLevel(entity.DocDep7, 3);
                    entity.MonthId = model.MonthId;
                    entity.PayDayEnd = model.PayDayEnd;
                    entity.PayType = model.PayType;
                    entity.DismissalDate = model.DismissalDate;
                    entity.PayDay = model.PayDay;
                }
                if (CurrentUser.UserRole == UserRole.ConsultantPersonnel)
                {
                    if (model.PersonnelManagerBankAccept)
                    {
                        entity.PersonnelManagerDateAccept = DateTime.Now;
                        entity.PersonnelManagerBank = UserDao.Load(CurrentUser.Id);

                        model.PersonnelManagerBankDateAccept = entity.PersonnelManagerDateAccept.Value;
                        model.PersonnelManagerBankName = entity.PersonnelManagerBank.Name;
                    }
                }
                if (CurrentUser.UserRole == UserRole.PersonnelManager)
                {
                    if (model.PersonnelAccept)
                    {
                        entity.PersonnelDateAccept = DateTime.Now;
                        entity.Personnel = UserDao.Load(CurrentUser.Id);

                        model.PersonnelDateAccept = entity.PersonnelDateAccept;
                        model.PersonnelName = entity.Personnel.Name;
                        model.PersonnelsId = entity.Personnel.Id;
                    }
                }
                if (CurrentUser.Id == 10 || (CurrentUser.UserRole & UserRole.Estimator)>0)
                {
                    if (model.CountantAccept)
                    {
                        entity.CountantDateAccept = DateTime.Now;
                        entity.Countant = UserDao.Load(CurrentUser.Id);

                        model.CountantDateAccept = entity.CountantDateAccept;
                        model.CountantName = entity.Countant.Name;
                        model.CountantId = entity.Countant.Id;
                    }
                }
                SurchargeNoteDao.SaveAndFlush(entity);
                ChangeModelProperties(model, entity);
            }

            GetDictionaries(model);
        }
        private void ChangeModelProperties(SurchargeNoteEditModel model, SurchargeNote entity)
        {
            model.Id = entity.Id;
            model.Number = entity.Number.ToString();
            model.NoteType = entity.NoteType;
            model.CreateDate = entity.CreateDate;
            model.CreatorName = entity.Creator.Name;
            model.CreatorDepartment = entity.Creator.Department.Name;
            var dep3 = DepartmentDao.GetParentDepartmentWithLevel(entity.Creator.Department, 3);
            model.CreatorDepartment3 = dep3 != null ? dep3.Name : "";
            model.CreatorId = entity.Creator.Id;
            model.PayDay = entity.PayDay;
            model.DepartmentId = entity.DocDep7.Id;
            model.Position = entity.Creator.Position.Name;
            if (entity.PersonnelManagerBank != null)
            {
                model.PersonnelManagerBankName = entity.PersonnelManagerBank.Name;
                
            }
            model.PersonnelManagerBankDateAccept = entity.PersonnelManagerDateAccept;
            if (entity.Personnel != null)
            {
                model.PersonnelName = entity.Personnel.Name;
                model.PersonnelsId = entity.Personnel.Id;
            }
            model.PersonnelDateAccept = entity.PersonnelDateAccept;
            if (entity.Countant != null)
            {
                model.CountantId = entity.Countant.Id;
                model.CountantName = entity.Countant.Name;
            }
            model.CountantDateAccept = entity.CountantDateAccept;
        }
        public void SetDocumentsToModel(SurchargeNoteListModel model)
        {
            //if (model.DepartmentId > 0)
            //{
            //    var user = UserDao.Load(CurrentUser.Id);
            //    var dep = DepartmentDao.Load(model.DepartmentId);
            //    if(user.
            //}
            model.Documents = SurchargeNoteDao.GetDocuments(CurrentUser.Id, CurrentUser.UserRole, model.DepartmentId, model.NoteType, model.StatusId, model.BeginDate, model.EndDate, model.UserName, model.SortBy, model.SortDescending, model.Number);

            GetDictionaries(model);
        }
        public bool CheckDepartmentLevel(int id, int level)
        {
            var dep = DepartmentDao.Load(id);
            return dep.ItemLevel == level;
        }
        public bool CheckDepartment(SurchargeNoteEditModel model, out int level)
        {
            level = 0;
            int departmentId = model.DepartmentId;
            
            Department dep = DepartmentDao.Load(departmentId);
            if (dep == null)
                throw new ArgumentException(string.Format("Не найдено подразделение {0}", departmentId));
            if (!dep.ItemLevel.HasValue)
                throw new ArgumentException(string.Format("Не найдено подразделение {0}", departmentId));
            level = dep.ItemLevel.Value;

            User currUser = UserDao.Load(model.CreatorId);

            if (currUser == null)
                throw new ArgumentException(string.Format(" Пользователь не найден {0}", model.UserId));
            List<DepartmentDto> departments;
            if (currUser.Department != null && dep.Path.StartsWith(currUser.Department.Path)) return true;
            switch (currUser.Level)
            {
                case 2:
                    departments = DepartmentDao.GetDepartmentsForManager23(currUser.Id, 2, false).ToList();
                    return departments.Any(x => dep.Path.StartsWith(x.Path));
                case 3:
                    departments = DepartmentDao.GetDepartmentsForManager23(currUser.Id, 3, false).ToList();
                    return departments.Any(x => dep.Path.StartsWith(x.Path));
                default:
                    if (currUser.Department == null)
                        throw new ValidationException(string.Format("Не найдено подразделение для пользователя {0}", currUser.Id));
                    return dep.Path.StartsWith(currUser.Department.Path);
            }
        }
        #endregion
        #region MailList
        public void SendMail()
        {
            var mails = MailListDao.GetMails();
            foreach (var mail in mails)
            {
                var address = mail.To.Email;
                if(!string.IsNullOrEmpty(address)) 
                    SendEmail(address, mail.MailSubject, mail.MailText);
                mail.SendDate = DateTime.Now;
                MailListDao.SaveAndFlush(mail);
            }
        }
        #endregion
        public void SendBugReport(BugReportModel model, string Guid)
        {
            BugReport entity = new BugReport();
            entity.Browser = model.Browser;
            entity.BrowserVersion = model.BrowserVersion;
            entity.Summary = model.Summary;
            entity.Description = model.Description;
            entity.Guid = Guid;
            entity.User = UserDao.Load(CurrentUser.Id);
            entity.UserRole = (int)CurrentUser.UserRole;
            BugReportDao.SaveAndFlush(entity);
            try
            {
                SendEmail("administrator@ruscount.ru", "[Кадровый портал] Новое сообщение об ошибке", String.Format("<a href='https://ruscount.com:8002/Home/BugReportEdit/{0}'>{1}</a><br/>{2}",entity.Id,entity.Summary,entity.Description));
            }
            catch (Exception) { }
        }
        public BugReportEditModel GetBugEditModel(int id, string path)
        {
            var entity = BugReportDao.Load(id);
            BugReportEditModel model = new BugReportEditModel();
            model.Browser = entity.Browser;
            model.BrowserVersion = entity.BrowserVersion;
            model.Description = entity.Description;
            model.Summary = entity.Summary;
            model.UserName = entity.User.Name;
            model.UserId = entity.User.Id;
            try
            {
                model.UserRole = ReportRoleConstants.Mapper[(UserRole)entity.UserRole];
            }
            catch (Exception) { }
            model.Files = new List<string>();
            path = path+ "\\Content\\BugReport\\"+ entity.Guid;
            DirectoryInfo dir = new DirectoryInfo(path);
            var files = dir.GetFiles();
            foreach (var file in files)
            {
                model.Files.Add(Path.Combine("\\Content\\BugReport", entity.Guid, Path.GetFileName(file.FullName)));
            }
            return model;
        }
        public BugReportListModel GetBugListModel()
        {
            BugReportListModel model = new BugReportListModel();
            model.Documents = BugReportDao.GetDocuments();
            return model;
        }
        public MissionUserDeptsListModel GetMissionUserDeptsListModel()
        {
            User user = UserDao.Load(AuthenticationService.CurrentUser.Id);
            IdNameReadonlyDto dep = GetDepartmentDto(user);
            MissionUserDeptsListModel model = new MissionUserDeptsListModel
            {
                UserId = AuthenticationService.CurrentUser.Id,
                DepartmentName = dep.Name,
                DepartmentId = dep.Id,
                DepartmentReadOnly = dep.IsReadOnly,
            };
            SetInitialDates(model);
            SetDictionariesToModel(model);
            //SetInitialStatus(model);
            //SetIsAvailable(model);
            return model;
        }
        public void SetDictionariesToModel(MissionUserDeptsListModel model)
        {
            model.Statuses = GetUserDeptsListStatuses();
        }
        protected List<IdNameDto> GetUserDeptsListStatuses()
        {
            List<IdNameDto> statusesList = new List<IdNameDto>
                                                       {
                                                           new IdNameDto(1, "Выгружено в 1С"),
                                                           new IdNameDto(2, "Не выгружено в 1С"),
                                                       }.OrderBy(x => x.Name).ToList();
            statusesList.Insert(0, new IdNameDto(0, SelectAll));
            return statusesList;
        }
        public void SetMissionUserDeptsListModel(MissionUserDeptsListModel model, bool showDepts, bool hasError)
        {
            SetDictionariesToModel(model);
            //SetIsAvailable(model);
            User user = UserDao.Load(model.UserId);
            if (hasError)
                model.Documents = new List<MissionUserDeptsListDto>();
            else
            {
                //if (model.IsApproveClick)
                //{
                //    model.IsApproveClick = false;
                //    List<int> idsForApprove = model.Documents.Where(x => x.IsChecked).Select(x => x.Id).ToList();
                //    ApproveOrders(model, idsForApprove);
                //}
                SetDocumentsToModel(model, user, showDepts);
            }
        }
        public void SetDocumentsToModel(MissionUserDeptsListModel model, User user, bool showDepts)
        {
            UserRole role = CurrentUser.UserRole;
            model.Documents = MissionOrderDao.GetUserDeptsDocuments(user.Id,
                role,
                model.DepartmentId,
                model.StatusId,
                model.BeginDate,
                model.EndDate,
                model.UserName,
                model.SortBy,
                model.SortDescending, showDepts);
            model.IsPrintAvailable = model.Documents.Count > 0;
        }
        public AnalyticalStatementDetailsModel GetAnalyticalStatementDetails(AnalyticalStatementDetailsModel model)
        {
            model.Documents = MissionOrderDao.GetAnalyticalStatementDetails(model.id, model.SortBy, model.SortDescending);
            var user = UserDao.Load(model.id);
            model.DateCreated = DateTime.Now.ToString("dd.MM.yyyy");
            model.DocumentNumber = model.id.ToString();
            SetUserInfoModel(user, model);
            return model;
        }
        public PrintMissionUserDeptsListModel PrintMissionUserDeptsListModel(int departmentId, int statusId, DateTime? beginDate,
            DateTime? endDate, string userName, int sortBy, bool? sortDescending, bool showDepts)
        {
            UserRole role = CurrentUser.UserRole;
            return new PrintMissionUserDeptsListModel
            {
                Documents = MissionOrderDao.GetUserDeptsDocuments(CurrentUser.Id,
                                                                  role,
                                                                  departmentId,
                                                                  statusId,
                                                                  beginDate,
                                                                  endDate,
                                                                  userName,
                                                                  sortBy,
                                                                  sortDescending, showDepts)
            };
        }
        #region Mission PurchaseBook Documents
        public MissionPurchaseBookDocListModel GetMissionPurchaseBookDocsListModel()
        {
            //User user = UserDao.Load(AuthenticationService.CurrentUser.Id);
            MissionPurchaseBookDocListModel model = new MissionPurchaseBookDocListModel();
            SetInitialDates(model);
            model.IsAddAvailable = (CurrentUser.UserRole & UserRole.Accountant) == UserRole.Accountant;
            return model;
        }
        public MissionPurchaseBookRecordsListModel GetMissionPurchaseBookRecordsListModel()
        {
            User user = UserDao.Load(AuthenticationService.CurrentUser.Id);
            IdNameReadonlyDto dep = GetDepartmentDto(user);

            MissionPurchaseBookRecordsListModel model = new MissionPurchaseBookRecordsListModel
            {
                UserId = AuthenticationService.CurrentUser.Id,
                DepartmentName = dep.Name,
                DepartmentId = dep.Id,
                DepartmentReadOnly = dep.IsReadOnly,
            };
            return model;
        }
        public void SetMissionPurchaseBookRecordsListModel(MissionPurchaseBookRecordsListModel model)
        {
            SetDocumentsToModel(model);
        }
        public void SetDocumentsToModel(MissionPurchaseBookRecordsListModel model)
        {
            UserRole role = CurrentUser.UserRole;
            model.Documents = MissionPurchaseBookRecordDao.GetDocuments(
                role,
                model.DepartmentId,
                model.UserName,
                model.SortBy,
                model.SortDescending);
        }
        public void SetMissionPurchaseBookDocsModel(MissionPurchaseBookDocListModel model, bool hasError)
        {
            //User user = UserDao.Load(model.UserId);
            if (hasError)
                model.Documents = new List<MissionPurchaseBookDocDto>();
            else
            {
                SetDocumentsToModel(model);
            }
        }
        public void SetDocumentsToModel(MissionPurchaseBookDocListModel model)
        {
            UserRole role = CurrentUser.UserRole;
            model.Documents = MissionPurchaseBookDocumentDao.GetDocuments(
                //user.Id,
                role,
                model.BeginDate,
                model.EndDate,
                model.SortBy,
                model.SortDescending);
        }

        public EditMissionPbDocumentModel GetEditMissionPbDocumentModel(int id)
        {
            IUser current = AuthenticationService.CurrentUser;
            if ((current.UserRole & UserRole.Accountant) != UserRole.Accountant && (current.UserRole & UserRole.OutsourcingManager) != UserRole.OutsourcingManager && (current.UserRole & UserRole.Estimator) != UserRole.Estimator
                && (current.UserRole & UserRole.Findep) != UserRole.Findep)
                throw new ArgumentException("Доступ запрещен.");
            EditMissionPbDocumentModel model = new EditMissionPbDocumentModel { UserId = current.Id, Id = id };
            if (id != 0)
            {
                MissionPurchaseBookDocument entity = MissionPurchaseBookDocumentDao.Load(id);
                if (entity == null)
                    throw new ValidationException(string.Format("Не найден документ книги покупок (id {0}) в базе данных", id));
                model.Number = entity.Number;
                model.DocumentDate = entity.DocumentDate.HasValue ? entity.DocumentDate.Value.ToShortDateString() : string.Empty;
                model.CfNumber = entity.CfNumber;
                model.CfDate = entity.CfDate.HasValue ? entity.CfDate.Value.ToShortDateString() : string.Empty;
                model.ContractorId = entity.Contractor.Id;
                model.Version = entity.Version;
            }
            model.IsEditable = (current.UserRole & UserRole.Accountant) == UserRole.Accountant;
            LoadDictionaries(model);
            model.ContractorIdHidden = model.ContractorId;
            return model;
        }

        public bool SaveMissionPbDocumentEditModel(EditMissionPbDocumentModel model, out string error)
        {
            error = string.Empty;
            //User user = null;
            try
            {
                //user = UserDao.Load(model.UserId);
                IUser current = AuthenticationService.CurrentUser;
                if ((current.UserRole & UserRole.Accountant) != UserRole.Accountant)
                {
                    error = "Редактирование отчета запрещено";
                    return false;
                }
                MissionPurchaseBookDocument entity = new MissionPurchaseBookDocument();
                //if (model.Id != 0)
                //{
                if (model.Id > 0)
                {
                    entity = MissionPurchaseBookDocumentDao.Load(model.Id);
                    if (entity == null)
                        throw new ValidationException(string.Format("Не найден документ книги покупок (id {0}) в базе данных", model.Id));
                    if (entity.Version != model.Version)
                    {
                        error = "Документ был изменен другим пользователем.";
                        model.ReloadPage = true;
                        return false;
                    }
                }
                else
                {
                    entity.CreateDate = DateTime.Now;
                    entity.Editor = UserDao.Load(current.Id);
                    entity.EditDate = DateTime.Now;
                }
                entity.Number = string.IsNullOrEmpty(model.Number) ? null : model.Number;
                entity.DocumentDate = string.IsNullOrEmpty(model.DocumentDate) ? new DateTime?() : DateTime.Parse(model.DocumentDate);
                entity.CfNumber = string.IsNullOrEmpty(model.CfNumber) ? null : model.CfNumber;
                entity.CfDate = string.IsNullOrEmpty(model.CfDate) ? new DateTime?() : DateTime.Parse(model.CfDate);
                entity.Contractor = ContractorDao.Load(model.ContractorId);
                MissionPurchaseBookDocumentDao.SaveAndFlush(entity);
                if (model.Id != 0 && entity.Version != model.Version)
                {
                    entity.EditDate = DateTime.Now;
                    entity.Editor = UserDao.Load(current.Id);
                    MissionPurchaseBookDocumentDao.SaveAndFlush(entity);
                }
                if (model.Id == 0)
                    model.Id = entity.Id;
                model.Version = entity.Version;
                model.ContractorIdHidden = model.ContractorId;
                return true;
            }
            catch (Exception ex)
            {
                MissionPurchaseBookDocumentDao.RollbackTran();
                Log.Error("Error on SaveMissionPbDocumentEditModel:", ex);
                error = string.Format("Исключение:{0}", ex.GetBaseException().Message);
                return false;
            }
            finally
            {
                LoadDictionaries(model);
            }
        }
        protected void LoadDictionaries(EditMissionPbDocumentModel model)
        {
            List<Contractor> list = ContractorDao.LoadAllSorted().ToList();
            model.Contractors = list.ConvertAll(x => new IdNameDto { Id = x.Id, Name = x.Name });
            if (model.ContractorId != 0)
                model.ContractorAccount = list.Where(x => x.Id == model.ContractorId).First().Account;
            else
                model.ContractorAccount = list.First().Account;
            model.RecordsModel = GetRecordsModel(model.Id, model.IsEditable);
        }
        public EditMissionPbRecordsModel GetPbRecordsModel(int documentId)
        {
            return GetRecordsModel(documentId, AuthenticationService.CurrentUser.UserRole == UserRole.Accountant);
        }
        protected EditMissionPbRecordsModel GetRecordsModel(int documentId, bool isEditable)
        {
            if (documentId == 0)
                return new EditMissionPbRecordsModel();

            List<MissionPurchaseBookRecordDto> records =
                    MissionPurchaseBookRecordDao.GetRecordsForDocumentId(documentId).ToList();
            foreach (MissionPurchaseBookRecordDto dto in records)
                dto.IsEditable = dto.IsEditable && isEditable;

            EditMissionPbRecordsModel model = new EditMissionPbRecordsModel
            {
                DocumentId = documentId,
                Records = records,
                AllSum = records.Sum(x => x.AllSum),
                Sum = records.Sum(x => x.Sum),
                SumNds = records.Sum(x => x.SumNds.HasValue ? x.SumNds.Value : 0)
            };
            return model;
        }
        public void ReloadDictionaries(EditMissionPbDocumentModel model)
        {
            LoadDictionaries(model);
        }
        public ContractorAccountDto GetContractorAccount(int id)
        {
            try
            {
                Contractor entity = ContractorDao.Load(id);
                if (entity == null)
                    throw new ValidationException(string.Format("Не найден контрагент (id {0}) в базе данных", id));
                return new ContractorAccountDto { Error = string.Empty, Account = entity.Account };
            }
            catch (Exception ex)
            {
                Log.Error("Exception on GetContractorAccount:", ex);
                return new ContractorAccountDto
                {
                    Error = string.Format("Ошибка: {0}", ex.GetBaseException().Message)
                };
            }
        }

        public void SetMissionReportEditRecordModel(MissionPbEditRecordModel model)
        {
            if (model.RecordId != 0)
            {
                MissionPurchaseBookRecord entity = MissionPurchaseBookRecordDao.Load(model.RecordId);
                if (entity == null)
                    throw new ArgumentException(string.Format("Не могу загрузить запись книги покупок (id {0}) из базы данных", model.RecordId));
                model.AllSum = entity.AllSum;
                model.CostTypeId = entity.MissionReportCostType.Id;
                model.ReportId = entity.MissionReportCost.Report.Id;
                model.RequestNumber = entity.RequestNumber;
                model.Sum = entity.Sum;
                model.SumNds = entity.SumNds;
                model.IsWithNds = entity.SumNds.HasValue /*&& (entity.SumNds.Value != 0)*/;
                model.RecordUserId = entity.User.Id;
            }
            LoadDictionaries(model);
        }
        protected void LoadDictionaries(MissionPbEditRecordModel model)
        {
            List<IdNameDto> users = UserDao.GetUsersWithPurchaseBookReportCosts().ToList();
            if (users.Count == 0)
            {
                model.Users = new List<IdNameDto>();
                model.Reports = new List<IdNameDto>();
                model.CostTypes = new List<IdNameDto>();
                return;
            }
            model.Users = users;
            int selectedUserId = users[0].Id;
            if (model.RecordUserId != 0)
            {
                IdNameDto selectedUser = users.Where(x => x.Id == model.RecordUserId).FirstOrDefault();
                if (selectedUser != null)
                    selectedUserId = selectedUser.Id;
            }
            else
                model.RecordUserId = selectedUserId;
            List<MissionReport> reports = MissionReportDao.GetReportsWithPurchaseBookReportCosts(selectedUserId).ToList();
            if (reports.Count == 0)
            {
                model.Reports = new List<IdNameDto>();
                model.CostTypes = new List<IdNameDto>();
                return;
            }
            model.Reports = GetComboList(reports);
            int selectedReportId = reports[0].Id;
            if (model.RecordId != 0)
            {
                MissionReport selectedReport = reports.Where(x => x.Id == model.ReportId).FirstOrDefault();
                if (selectedReport != null)
                    selectedReportId = selectedReport.Id;
            }
            else
                model.ReportId = selectedReportId;
            List<IdNameDto> costTypes = MissionReportCostDao.GetCostTypesWithPurchaseBookReportCosts(selectedReportId).ToList();
            if (costTypes.Count == 0)
            {
                model.CostTypes = new List<IdNameDto>();
                return;
            }
            model.CostTypes = costTypes;
            if (model.CostTypeId == 0)
                model.CostTypeId = costTypes[0].Id;
            //IdNameDto selectedType = costTypes.Where(x => x.Id == model.CostTypeId).FirstOrDefault();
            if (model.RecordId == 0)
            {
                model.RequestNumber = GetRequestNumber(selectedReportId, model.CostTypeId);
            }
            return;
        }
        protected static List<IdNameDto> GetComboList(List<MissionReport> reports)
        {
            List<IdNameDto> result = new List<IdNameDto>();
            foreach (MissionReport report in reports)
            {
                if (result.Any(x => x.Id == report.Id))
                    continue;
                string name = "AO" + report.Number;
                name += " " + FormatDate(report.MissionOrder.BeginDate) + " - " + FormatDate(report.MissionOrder.EndDate);
                if (report.MissionOrder.Targets.Count() > 0)
                    name += " " + report.MissionOrder.Targets.First().City;
                result.Add(new IdNameDto { Id = report.Id, Name = name });
            }
            return result;
        }
        protected string GetRequestNumber(int reportId, int costTypeId)
        {
            MissionReport report = MissionReportDao.Load(reportId);
            MissionOrder order = report.MissionOrder;
            switch (costTypeId)
            {
                case 2:
                    return order.ResidenceRequestNumber;
                case 3:
                    return order.AirTicketsRequestNumber;
                case 4:
                    return order.TrainTicketsRequestNumber;
                default:
                    throw new ValidationException(string.Format("Недопустимый вид расхода (id {0})", costTypeId));
            }
        }
        public PbRecordCostTypesDto GetCostTypes(int reportId, bool isNew)
        {
            PbRecordCostTypesDto model = new PbRecordCostTypesDto { Error = string.Empty };
            List<IdNameDto> costTypes = MissionReportCostDao.GetCostTypesWithPurchaseBookReportCosts(reportId).ToList();
            model.Children = costTypes;
            if (isNew && costTypes.Count > 0)
                model.RequestNumber = GetRequestNumber(reportId, costTypes[0].Id);
            return model;
        }

        public ContractorAccountDto GetRequestNumberForCostType(int reportId, int costTypeId)
        {
            ContractorAccountDto model = new ContractorAccountDto
            {
                Error = string.Empty,
                Account = GetRequestNumber(reportId, costTypeId)
            };
            return model;
        }

        public PbRecordCostTypesDto GetReportsForPbUserId(int userId)
        {
            PbRecordCostTypesDto model = new PbRecordCostTypesDto { Error = string.Empty };
            List<MissionReport> reports = MissionReportDao.GetReportsWithPurchaseBookReportCosts(userId);
            model.Children = GetComboList(reports);
            return model;
        }

        public int SavePbRecord(SavePbRecordModel model)
        {
            MissionPurchaseBookDocument doc = MissionPurchaseBookDocumentDao.Load(model.DocumentId);
            if (doc == null)
                throw new ArgumentException(string.Format("Не могу загрузить документ книги покупок (id {0}) из базы данных.", model.DocumentId));
            MissionReport report = MissionReportDao.Load(model.ReportId);
            if (report == null)
                throw new ArgumentException(string.Format("Не могу загрузить авансовый отчет (id {0}) из базы данных.", model.ReportId));
            if (report.AccountantDateAccept.HasValue)
                throw new ArgumentException(string.Format("Изменение записей книги покупок для авансового отчета (id {0}) запрещено - отчет уже проверен бухгалтером.", model.ReportId));
            MissionReportCost cost = report.Costs.Where(x => x.IsCostFromPurchaseBook && x.Type.Id == model.CostTypeId).FirstOrDefault();
            if (cost == null)
                throw new ArgumentException(string.Format("Не найден расход указанного типа (id {1}) в авансовом отчете (id {0}).", model.ReportId, model.CostTypeId));

            MissionPurchaseBookRecord rec;
            if (model.RecordId == 0)
            {
                rec = new MissionPurchaseBookRecord
                {
                    Document = doc,
                    MissionOrder = report.MissionOrder,
                    MissionReportCost = cost,
                    MissionReportCostType = cost.Type,
                    User = UserDao.Load(model.UserId),
                };
                doc.Records.Add(rec);
            }
            else
            {
                rec = doc.Records.Where(x => x.Id == model.RecordId).FirstOrDefault();
                if (rec == null)
                    throw new ArgumentException(string.Format("Не найдена запись книги покупок (id {0}).", model.RecordId));
            }
            rec.AllSum = model.AllSum;
            rec.Sum = model.Sum;
            rec.SumNds = model.SumNds;
            rec.RequestNumber = model.RequestNumber ?? string.Empty;
            rec.EditDate = DateTime.Now;
            rec.Editor = UserDao.Load(CurrentUser.Id);
            doc.Sum = doc.Records.Sum(x => x.AllSum);
            MissionPurchaseBookDocumentDao.SaveAndFlush(doc);

            int documentVersion = doc.Version;
            List<MissionPurchaseBookRecord> costRecords = MissionPurchaseBookRecordDao.GetRecordsForCost(cost.Id).ToList();
            cost.BookOfPurchaseSum = costRecords.Sum(x => x.AllSum);
            report.PurchaseBookAllSum = report.Costs.Sum(x => x.BookOfPurchaseSum).Value;
            MissionReportDao.SaveAndFlush(report);
            return documentVersion;
        }
        public int DeletePbRecord(DeletePbRecordModel model)
        {
            MissionPurchaseBookRecord rec = MissionPurchaseBookRecordDao.Load(model.Id);
            if (rec == null)
                throw new ArgumentException(string.Format("Не могу загрузить запись книги покупок (id {0}) из базы данных.", model.Id));
            MissionReport report = rec.MissionReportCost.Report;
            if (report.AccountantDateAccept.HasValue)
                throw new ArgumentException(string.Format("Удаление записей книги покупок для авансового отчета (id {0}) запрещено - отчет уже проверен бухгалтером.", report.Id));
            MissionReportCost cost = rec.MissionReportCost;
            MissionPurchaseBookDocument doc = rec.Document;
            doc.Records.Remove(rec);
            doc.Sum = doc.Records.Sum(x => x.AllSum);
            MissionPurchaseBookDocumentDao.SaveAndFlush(doc);
            int documentVersion = doc.Version;

            List<MissionPurchaseBookRecord> costRecords = MissionPurchaseBookRecordDao.GetRecordsForCost(cost.Id).ToList();
            decimal? sum = costRecords.Sum(x => x.AllSum);
            cost.BookOfPurchaseSum = costRecords.Count == 0 ? null : sum;
            report.PurchaseBookAllSum = report.Costs.Sum(x => x.BookOfPurchaseSum).Value;
            MissionReportDao.SaveAndFlush(report);
            return documentVersion;
        }
        #endregion

        public void SaveDocumentsToArchive(DeletePbRecordModel model)
        {
            MissionReport report = MissionReportDao.Load(model.Id);
            if (report == null)
                throw new ArgumentException(string.Format("Не найден авансовый отчет (id {0}).", model.Id));
            report.IsDocumentsSaveToArchive = true;
            MissionReportDao.SaveAndFlush(report);
        }

        public void SetPrintArchivistAddressModel(PrintArchivistAddressModel model)
        {
            //model.CostTypes = GetCostTypes(false);
            //model.Archivists = new List<IdNameDto>();
            List<IdNameAddressDto> archivists = UserDao.GetArchivistAddresses().ToList();
            if (archivists.Count == 0)
            {
                /*model.Archivists = new List<IdNameDto>();
                model.Error = "Не найдено архивариусов с почтовыми адресами в базе данных";
                return;*/
                throw new ValidationException("Не найдено архивариусов с почтовыми адресами в базе данных");
            }
            model.Archivists = archivists.ConvertAll(x => (IdNameDto)x);
            model.ArchivistId = archivists.First().Id;
            model.Address = archivists.First().Address;
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            model.AddressList = jsonSerializer.Serialize(archivists.ConvertAll(x => new IdNameDto { Id = x.Id, Name = x.Address }).ToArray());
        }
        public PrintArchivistAddressFormModel GetPrintArchivistAddressFormModel(int id)
        {
            User user = UserDao.Load(id);
            if (user == null)
                throw new ArgumentException(string.Format("Не могу найти архивариуса (id={0}) в базе данных", id));

            return new PrintArchivistAddressFormModel
            {
                Address = user.Address,
                To = user.FullName,
                From = CurrentUser.Name,
                Index = GetIndex(user.Address),
            };
        }
        protected string GetIndex(string address)
        {
            if (string.IsNullOrEmpty(address))
                return null;
            string[] parts = address.Split(',');
            if (parts.Length < 2)
                return null;
            string index = parts[0].Trim();
            Regex r = new Regex(@"^\d{6}$");
            if (r.IsMatch(index))
                return index;
            return null;
        }
        public IList<Terrapoint_DepartmentDto> GetTP_D_list()
        {
            return DepartmentDao.GetTP_D_list();
        }
        public IList<Department_TerrapointDto> GetD_TP_list()
        {
            return DepartmentDao.GetD_TP_list();
        }
    }
}