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

namespace Reports.Presenters.UI.Bl.Impl
{
    public class RequestBl : BaseBl, IRequestBl
    {
       
        protected string EmptyDepartmentName = string.Empty;
        protected string ChildVacationTimesheetStatusShortName = "ОЖ";
        protected string OKTMOFormatError = "Ошибка формата ОКТМО";

        public const int VacationFirstTimesheetStatisId = 8;
        public const int VacationLastTimesheetStatisId = 12;
        public const int AbsenceFirstTimesheetStatisId = 15;
        public const int AbsenceLastTimesheetStatisId = 18;

        #region DAOs
        
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

        protected IAbsenceTypeDao absenceTypeDao;
        protected IAbsenceDao absenceDao;
        protected IAbsenceCommentDao absenceCommentDao;

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

        protected ITerraPointDao terraPointDao;
        protected ITerraPointToUserDao terraPointToUserDao;
        protected ITerraGraphicDao terraGraphicDao;


       
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
        protected IConfigurationService configurationService;
        public IConfigurationService ConfigurationService
        {
            set { configurationService = value; }
            get { return Validate.Dependency(configurationService); }
        }

        #endregion

        #region Create Request
        public CreateRequestModel GetCreateRequestModel(int? userId)
        {
            if(userId == null)
                userId = AuthenticationService.CurrentUser.Id;

            User user = UserDao.Load(userId.Value);
            UserRole role = (UserRole)user.RoleId;
            CreateRequestModel model = new CreateRequestModel
            {
                IsUserVisible = role != UserRole.Employee,
                RequestTypes = GetRequestTypes()
            };
            switch(role)
            {
                case UserRole.Employee:
                    model.Users = new List<IdNameDto>{new IdNameDto(user.Id,user.FullName)};
                    break;
                case UserRole.Manager:
                case UserRole.PersonnelManager:
                    model.Users = UserDao.GetUsersForManager(user.Id, role, 0);
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
                           new IdNameDto((int) RequestTypeEnum.Mission, "Заявка на командировку"),
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
            SetDepartmentToModel(model,user);
            SetDictionariesToModel(model, user);
            SetInitialDates(model);
            return model;   
        }
        protected void SetDepartmentToModel(AllRequestListModel model,User user)
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

            model.Statuses = GetRequestStatuses();
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
                model.SortBy,model.SortDescending).ToList().ConvertAll(x => new AllRequestDto
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
                    RequestType = x.RequestType
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
               model.SortBy, model.SortDescending).ToList().ConvertAll(x => new AllRequestDto
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
                   RequestType = x.RequestType
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
               model.SortBy, model.SortDescending).ToList().ConvertAll(x => new AllRequestDto
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
                   RequestType = x.RequestType
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
               model.SortBy, model.SortDescending).ToList().ConvertAll(x => new AllRequestDto
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
                   RequestType = x.RequestType
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
              model.SortBy, model.SortDescending).ToList().ConvertAll(x => new AllRequestDto
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
                  RequestType = x.RequestType
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
             model.SortBy, model.SortDescending).ToList().ConvertAll(x => new AllRequestDto
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
                 RequestType = x.RequestType
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
              model.BeginDate,
              model.EndDate, 
              model.UserName,
              model.SortBy, model.SortDescending).ToList().ConvertAll(x => new AllRequestDto
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
                  RequestType = x.RequestType
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
                        return result.OrderByDescending(x => x.BeginDate);
                    return result.OrderBy(x => x.BeginDate);
                case 8:
                    if (model.SortDescending.Value)
                        return result.OrderByDescending(x => x.EndDate);
                    return result.OrderBy(x => x.EndDate);
                default:
                    throw new ArgumentException(string.Format("Неправильное поля сортировки {0}",model.SortBy));
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
        public void SetEmploymentListModel(EmploymentListModel model,bool hasError)
        {
            User user = UserDao.Load(model.UserId);
            SetDictionariesToModel(model, user);
            if(hasError)
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
                if (!CheckUserRights(user, current,model.Id, true))
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
                        RequestAttachmentDao.DeleteForEntityId(model.Id);
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
        protected int? SaveAttachment(int entityId, int id, UploadFileDto dto,RequestAttachmentTypeEnum type, out string attachment)
        {
            attachment = string.Empty;
            if(dto == null)
                return new int?();
            RequestAttachment attach = id != 0 ?
               RequestAttachmentDao.Load(id) :
               new RequestAttachment
               {
                   RequestId = entityId,
                   RequestType = (int)type,
                   CreatorRole = RoleDao.Load((int)CurrentUser.UserRole),
               };
            if(id == 0)
            {
                RequestAttachment existingAttach = RequestAttachmentDao.FindByRequestIdAndTypeId(entityId, type);
                if (existingAttach != null)
                {
                    Log.InfoFormat("Found existing attachment for request id {0} and type {1} (id {2})", entityId, type,existingAttach.Id);
                    attach = existingAttach;
                }
            }
            attach.DateCreated = DateTime.Now;
            attach.UncompressContext = dto.Context;
            attach.ContextType = dto.ContextType;
            attach.FileName = dto.FileName;
            attach.CreatorRole = RoleDao.Load((int) CurrentUser.UserRole);
            RequestAttachmentDao.SaveAndFlush(attach);
            attachment = attach.FileName;
            return attach.Id;
        }
        protected void ChangeEntityProperties(IUser current, Employment entity, EmploymentEditModel model, User user)
        {
            if (current.UserRole == UserRole.Employee && current.Id == model.UserId
                && !entity.UserDateAccept.HasValue
                && model.IsApprovedByUser)
            {
                entity.UserDateAccept = DateTime.Now;
                SendEmailForUserRequest(entity.User, current, entity.Creator, false, entity.Id,
                                        entity.Number, RequestTypeEnum.Employment, false);
            }
            if (current.UserRole == UserRole.Manager && user.Manager != null
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
            if (current.UserRole == UserRole.PersonnelManager /*&& user.PersonnelManager != null
                && current.Id == user.PersonnelManager.Id*/
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
            return (decimal) ((int) (decimal.Parse(modelValue)*100))/100;
        }
        protected static decimal? GetTwoDigitNullableValue(string modelValue)
        {
            if(string.IsNullOrEmpty(modelValue))
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
            if (AuthenticationService.CurrentUser.UserRole == UserRole.Employee)
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
        public void SetTimesheetCorrectionListModel(TimesheetCorrectionListModel model,bool hasError)
        {
            User user = UserDao.Load(model.UserId);
            SetDictionariesToModel(model, user);
            if(hasError)
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
                model.SortDescending);
        }
        public TimesheetCorrectionEditModel GetTimesheetCorrectionEditModel(int id, int userId)
        {
            TimesheetCorrectionEditModel model = new TimesheetCorrectionEditModel { Id = id, UserId = userId };
            User user = UserDao.Load(userId);
            IUser current = AuthenticationService.CurrentUser;
            if (!CheckUserRights(user, current,id,false))
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
            if (AuthenticationService.CurrentUser.UserRole == UserRole.Employee)
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
                        //model.IsApprovedByUserEnable = true;
                        break;
                    case UserRole.Manager:
                        //model.IsApprovedByManagerEnable = true;
                        //model.IsStatusEditable = true;
                        break;
                    case UserRole.PersonnelManager:
                        //model.IsApprovedByPersonnelManagerEnable = true;
                        model.IsStatusEditable = true;
                        //model.IsPersonnelFieldsEditable = true;
                        break;
                }
                if (currentUserRole == UserRole.PersonnelManager || currentUserRole == UserRole.Manager)
                {
                    //model.IsApprovedByUserEnable = false;
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
                if (!CheckUserRights(user, current,model.Id,true))
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
            if (current.UserRole == UserRole.Employee && current.Id == model.UserId
                && !entity.UserDateAccept.HasValue
                && model.IsApproved)
            {
                entity.UserDateAccept = DateTime.Now;
                SendEmailForUserRequest(entity.User, current, entity.Creator, false, entity.Id,
                           entity.Number, RequestTypeEnum.TimesheetCorrection, false);
            }
            if (current.UserRole == UserRole.Manager && user.Manager != null
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
            if (current.UserRole == UserRole.PersonnelManager /*&& user.PersonnelManager != null
                && current.Id == user.PersonnelManager.Id*/
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
            if(hasError)
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
                // TODO SL: реализовать сохранение состояния свойства
                entity.IsOriginalReceived = true;
                DismissalDao.SaveAndFlush(entity);
            }
        }

        protected void ApplyPersonnelFileSentToArchive(DismissalListModel model, List<int> idsToApplyPersonnelFileSentToArchive)
        {
            List<Dismissal> entities = DismissalDao.LoadForIdsList(idsToApplyPersonnelFileSentToArchive).ToList();
            foreach (Dismissal entity in entities)
            {
                // TODO SL: реализовать сохранение состояния свойства
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
                model.UserName,
                model.SortBy,
                model.SortDescending);
        }
        protected void SetDictionariesToModel(DismissalListModel model, User user)
        {
            //model.Departments = GetDepartments(user);
            model.Types = GetDismissalTypes(true);
            model.Statuses = GetRequestStatuses();
            model.Positions = GetPositions(user);
        }
        protected List<IdNameDto> GetDismissalTypes(bool addAll)
        {
            var typeList = DismissalTypeDao.LoadAll().ToList().ConvertAll(x => new IdNameDto(x.Id, x.Name+" "+(x.Reason.Length > 64?x.Reason.Substring(0,64)+" ...":x.Reason))).OrderBy(x =>x.Name).ToList();
            if (addAll)
                typeList.Insert(0, new IdNameDto(0, SelectAll));
            return typeList;
        }

        public DismissalEditModel GetDismissalEditModel(int id, int userId)
        {
            DismissalEditModel model = new DismissalEditModel { Id = id, UserId = userId };
            User user = UserDao.Load(userId);
            IUser current = AuthenticationService.CurrentUser;
            if (!CheckUserRights(user, current,id,false))
                throw new ArgumentException("Доступ запрещен.");
            SetUserInfoModel(user, model);
            SetAttachmentToModel(model, id,RequestAttachmentTypeEnum.Dismissal);
            SetOrderScanAttachmentToModel(model, id, RequestAttachmentTypeEnum.DismissalOrderScan);
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
                model.TypeId = dismissal.Type == null? 0 : dismissal.Type.Id;
                model.EndDate = dismissal.EndDate;
                model.Compensation = dismissal.Compensation.HasValue? dismissal.Compensation.Value.ToString():string.Empty;
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
            if (id == 0)
            {
                model.IsSaveAvailable = true;
                model.IsTypeEditable = true;
                model.IsApprovedEnable = false;
                switch (currentUserRole)
                {
                    case UserRole.Employee:
                        //model.IsApprovedByUserEnable = false;
                        break;
                    case UserRole.Manager:
                        //model.IsApprovedByManagerEnable = false;
                        //model.IsStatusEditable = true;
                        break;
                    case UserRole.PersonnelManager:
                        //model.IsApprovedByPersonnelManagerEnable = false;
                        //model.IsStatusEditable = true;
                        model.IsPersonnelFieldsEditable = true;
                        break;
                }
                if (currentUserRole == UserRole.PersonnelManager || currentUserRole == UserRole.Manager)
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
                    if (!entity.UserDateAccept.HasValue && !entity.DeleteDate.HasValue)
                    {
                        if(model.AttachmentId > 0)
                            model.IsApprovedEnable = true;
                        if (!entity.ManagerDateAccept.HasValue && !entity.PersonnelManagerDateAccept.HasValue && !entity.SendTo1C.HasValue)
                            model.IsTypeEditable = true;
                    }
                    break;
                case UserRole.Manager:
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
                    }
                    break;
                case UserRole.PersonnelManager:
                    if (model.IsPostedTo1C && model.OrderScanAttachmentId <= 0)
                    {
                        model.IsConfirmationAllowed = true;
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
                    }
                    else if (!entity.SendTo1C.HasValue && !entity.DeleteDate.HasValue)
                        model.IsDeleteAvailable = true;
                    break;
                case UserRole.OutsourcingManager:
                    if (entity.SendTo1C.HasValue && !entity.DeleteDate.HasValue)
                        model.IsDeleteAvailable = true;
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
        public bool SaveDismissalEditModel(DismissalEditModel model, UploadFileDto fileDto, UploadFileDto orderScanFileDto, out string error)
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
                    //
                }
                else
                {                    
                    dismissal = DismissalDao.Load(model.Id);
                    string fileName;
                    int? attachmentId = SaveAttachment(dismissal.Id, model.AttachmentId, fileDto, RequestAttachmentTypeEnum.Dismissal, out fileName);
                    if (attachmentId.HasValue)
                    {
                        model.AttachmentId = attachmentId.Value;
                        model.Attachment = fileName;
                    }

                    // ---------------------------------------
                    int? orderScanAttachmentId = SaveAttachment(dismissal.Id, model.OrderScanAttachmentId, orderScanFileDto, RequestAttachmentTypeEnum.DismissalOrderScan, out fileName);
                    if (orderScanAttachmentId.HasValue)
                    {
                        model.OrderScanAttachmentId = orderScanAttachmentId.Value;
                        model.OrderScanAttachment = fileName;
                    }
                    // ---------------------------------------

                    if (dismissal.Version != model.Version)
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
                        model.AttachmentId = 0;
                        model.Attachment = string.Empty;
                        model.OrderScanAttachmentId = 0;
                        model.OrderScanAttachment = string.Empty;
                        if (current.UserRole == UserRole.OutsourcingManager)
                            dismissal.DeleteAfterSendTo1C = true;
                        dismissal.CreateDate = DateTime.Now;
                        dismissal.DeleteDate = DateTime.Now;
                        DismissalDao.SaveAndFlush(dismissal);
                        model.IsDelete = false;
                        SendEmailForUserRequest(dismissal.User, current, dismissal.Creator, true, dismissal.Id,
                            dismissal.Number, RequestTypeEnum.Dismissal, false);
                    }
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
                model.DocumentNumber = dismissal.Number.ToString();
                model.Version = dismissal.Version;
                //model.DaysCount = dismissal.DaysCount;
                model.CreatorLogin = dismissal.Creator.Name;
                model.DateCreated = dismissal.CreateDate.ToShortDateString();
                SetFlagsState(dismissal.Id, user, dismissal, model);
                // create CCL approvals if the Dismissal has been approved by the user and two managers
                if (model.IsApprovedByManager && model.IsApprovedByPersonnelManager && model.IsApprovedByUser)
                {
                    var clearanceChecklistRoles = ClearanceChecklistDao.GetClearanceChecklistRoles();
                    foreach (var clearanceChecklistRole in clearanceChecklistRoles)
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
            if (current.UserRole == UserRole.Employee && current.Id == model.UserId
                && !entity.UserDateAccept.HasValue
                && model.IsApproved)
            {
                entity.UserDateAccept = DateTime.Now;
                SendEmailForUserRequest(entity.User, current, entity.Creator, false, entity.Id,
                    entity.Number, RequestTypeEnum.Dismissal, false);
            }
            if (current.UserRole == UserRole.Manager && user.Manager != null
                && current.Id == user.Manager.Id)
            {
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
            if (current.UserRole == UserRole.PersonnelManager 
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
                                              : (decimal) ((int) (decimal.Parse(model.Compensation)*100))/100;
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

            //UserRole role = (UserRole)(user.RoleId & (int)CurrentUser.UserRole);
            UserRole role = UserRole.OutsourcingManager;
            model.DepartmentId = 0;
            model.PositionId = 0;
            model.Documents = ClearanceChecklistDao.GetClearanceChecklistDocuments(
                user.Id,
                role,
                //model.DepartmentId,
                //GetDepartmentId(model.Department),
                model.DepartmentId,
                model.PositionId,
                //model.TypeId,
                0,
                model.StatusId,
                //0,
                model.BeginDate,
                model.EndDate,
                model.UserName,
                model.SortBy,
                model.SortDescending);
        }

        public ClearanceChecklistEditModel GetClearanceChecklistEditModel(int id, int userId)
        {
            const int MAX_DAYS_BEFORE_DISMISSAL = 14;
            
            string[] PIT_DISPLAY_ROLES = {"Бухгалтерия - выплаты", "Кадры"};

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
                // TODO: CCL Roles
                IList<string> roleAuthorities = clearanceChecklistDao.GetClearanceChecklistRoleAuthorities(approval.ClearanceChecklistRole)
                    .Select<User, string>(roleAuthority => roleAuthority.FullName).ToList<string>();

                model.ClearanceChecklistApprovals.Add(
                    new ClearanceChecklistApprovalDto
                    {
                        Id = approval.Id,
                        ClearanceChecklistRole = approval.ClearanceChecklistRole.Description,
                        RoleAuthorities = roleAuthorities,
                        ApprovedBy = approval.ApprovedBy!=null ? approval.ApprovedBy.FullName : string.Empty,
                        ApprovalDate = approval.ApprovalDate.HasValue ? approval.ApprovalDate.Value.ToString("dd.MM.yyyy") : "",
                        Comment = approval.Comment,
                        // Checking if the authenticated user has the extended role for approval
                        // and that the user's department is allowed to approve today.
                        // If both are OK the Active property is set
                        // and the view will output the approval link in the corresponding row

                        // TODO: CCL Roles
                        Active = (this.IsRoleOwner(currentUser, approval.ClearanceChecklistRole) ? true : false) &&
                            DateTime.Now >= clearanceChecklist.EndDate.AddDays(
                                approval.ClearanceChecklistRole.DaysForApproval == null ? -MAX_DAYS_BEFORE_DISMISSAL : -(int)approval.ClearanceChecklistRole.DaysForApproval)
                    }
                );
            }
            model.IsBottomEnabled = current.UserRole == UserRole.OutsourcingManager ? true : false;
            model.RegistryNumber = clearanceChecklist.RegistryNumber;
            if(IsRoleOwner(currentUser, PIT_DISPLAY_ROLES) || (currentUser.UserRole & UserRole.OutsourcingManager) == UserRole.OutsourcingManager)
            {
                model.PersonalIncomeTax = clearanceChecklist.PersonalIncomeTax;
            }
            model.OKTMO = clearanceChecklist.OKTMO;
            model.DateCreated = clearanceChecklist.CreateDate.ToShortDateString();
            model.DocumentNumber = clearanceChecklist.Number.ToString();
            model.EndDate = clearanceChecklist.EndDate;
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
            // TODO Implementation for SaveClearanceChecklistEditModel
            error = "";
            return false;
        }

        public bool SetClearanceChecklistApproval(int approvalId, int approvedBy, out ClearanceChecklistApprovalDto modifiedApproval, out string error)
        {
            User user = UserDao.Load(AuthenticationService.CurrentUser.Id);
            // TODO: CCL Roles ?
            //if(!user.ClearanceChecklistRoleRecords.Contains<ClearanceChecklistRole>(ClearanceChecklistDao.GetApprovalById(approvalId).ClearanceChecklistRole))
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
            // TODO: CCL Roles ?
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
            
            if (current.UserRole != UserRole.OutsourcingManager)
            {
                throw new ArgumentException("Доступ запрещен.");
            }
            
            // Field format checks
            // TODO: Replace with implementation
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
            foreach(var roleDescription in roleDescriptions)
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
        public void SetMissionListModel(MissionListModel model,bool hasError)
        {
            User user = UserDao.Load(model.UserId);
            SetDictionariesToModel(model, user);
            if(hasError)
                model.Documents = new List<VacationDto>();
            else
                SetDocumentsToModel(model, user);
        }
        protected void SetDictionariesToModel(MissionListModel model, User user)
        {
            //model.Departments = GetDepartments(user);
            model.Types = GetMissionTypes(true);
            model.Statuses = GetRequestStatuses();
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
                    if(dto.Id == 1)
                        result.Insert(0,dto);
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
                model.SortDescending);
        }
        public MissionEditModel GetMissionEditModel(int id, int userId)
        {
            MissionEditModel model = new MissionEditModel { Id = id, UserId = userId };
            User user = UserDao.Load(userId);
            IUser current = AuthenticationService.CurrentUser;
            if (!CheckUserRights(user, current,id,false))
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
                if (currentUserRole == UserRole.PersonnelManager || currentUserRole == UserRole.Manager)
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
                else
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
                        if (current.UserRole == UserRole.OutsourcingManager)
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
            if (current.UserRole == UserRole.Employee && current.Id == model.UserId
                && !entity.UserDateAccept.HasValue
                && model.IsApproved)
            {
                entity.UserDateAccept = DateTime.Now;
                SendEmailForUserRequest(entity.User, current, entity.Creator, false, entity.Id,
                    entity.Number, RequestTypeEnum.Mission, false);
            }
            if (current.UserRole == UserRole.Manager && user.Manager != null
                && current.Id == user.Manager.Id)
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
            if (current.UserRole == UserRole.PersonnelManager /*&& user.PersonnelManager != null
                && current.Id == user.PersonnelManager.Id*/
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
            if(hasError)
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
                model.UserName
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
            if (!CheckUserRights(user, current,id,false))
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
                if (!CheckUserRights(user, current,model.Id,true))
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
            if (current.UserRole == UserRole.Employee && current.Id == model.UserId
                && !entity.UserDateAccept.HasValue
                && model.IsApprovedByUser)
                entity.UserDateAccept = DateTime.Now;
            if (current.UserRole == UserRole.Manager && user.Manager != null
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
            if (current.UserRole == UserRole.PersonnelManager /*&& user.PersonnelManager != null
                && current.Id == user.PersonnelManager.Id*/
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
                if (currentUserRole == UserRole.PersonnelManager || currentUserRole == UserRole.Manager)
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
            SetDictionariesToModel(model,user);
            SetInitialDates(model);
            return model;
        }
        protected List<IdNameDto> GetSicklistTypes(bool addAll,bool addEmpty)
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
        protected List<IdNameDtoSort> GetSicklisPaymentPercentTypes(bool addAll,bool addNameAll)
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
            if(addEmpty)
                typeList.Insert(0, new IdNameDto(0, "Отсутствует"));
            return typeList;
        }
        public void SetSicklistListModel(SicklistListModel model,bool hasError)
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
                // TODO SL: реализовать сохранение состояния свойства
                entity.IsOriginalReceived = true;
                SicklistDao.SaveAndFlush(entity);
            }
        }
        protected void SetDictionariesToModel(SicklistListModel model, User user)
        {
            //model.Department = GetDepartments(user);
            model.Types = GetSicklistTypes(true,false);
            model.Statuses = GetRequestStatuses();
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
                model.SortBy,
                model.SortDescending);
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
            if (!CheckUserRights(user, current,id,false))
                throw new ArgumentException("Доступ запрещен.");
            SetUserInfoModel(user, model);
            SetAttachmentToModel(model, id,RequestAttachmentTypeEnum.Sicklist);
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
                model.TypeId = sicklist.Type == null? 0 : sicklist.Type.Id;
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
                                        string.Empty:sicklist.ExperienceYears.Value.ToString();
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
        protected void SetAttachmentToModel(IAttachment model, int id,RequestAttachmentTypeEnum type)
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
        public bool SaveSicklistEditModel(SicklistEditModel model,UploadFileDto fileDto, out string error)
        {
            error = string.Empty;
            User user = null;
            try
            {
                user = UserDao.Load(model.UserId);
                IUser current = AuthenticationService.CurrentUser;
                if (!CheckUserRights(user, current,model.Id,true) /* || !CheckUserRightsForEntity(user,current,model)*/)
                {
                    error = "Редактирование заявки запрещено";
                    return false;
                }
                Sicklist sicklist;
                if (model.Id == 0)
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
                else
                {
                    sicklist = SicklistDao.Load(model.Id);
                    string fileName;
                    int? attachmentId = SaveAttachment(sicklist.Id,model.AttachmentId ,fileDto,RequestAttachmentTypeEnum.Sicklist,out fileName);
                    if(attachmentId.HasValue)
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
                        if (current.UserRole == UserRole.OutsourcingManager)
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
                        ChangeEntityProperties(current,sicklist,model,user);
                        SicklistDao.SaveAndFlush(sicklist);
                        if(sicklist.Version != model.Version)
                        {
                            sicklist.CreateDate = DateTime.Now;
                            SicklistDao.SaveAndFlush(sicklist);
                        }
                    }
                    if (sicklist.DeleteDate.HasValue)
                        model.IsDeleted = true;
                }
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
       
        protected void ChangeEntityProperties(IUser current, Sicklist entity,SicklistEditModel model,User user)
        {
            if (current.UserRole == UserRole.Employee && current.Id == model.UserId
                && !entity.UserDateAccept.HasValue
                && model.IsApproved)
            {
                entity.UserDateAccept = DateTime.Now;
                //!!! need to send e-mail
                SendEmailForUserRequest(entity.User, current, entity.Creator, false, entity.Id,
                    entity.Number, RequestTypeEnum.Sicklist, false);
            }
            if (current.UserRole == UserRole.Manager && user.Manager != null
                && current.Id == user.Manager.Id
                && !entity.ManagerDateAccept.HasValue)
            {
                if (model.IsApprovedByUser && !entity.UserDateAccept.HasValue)
                    entity.UserDateAccept = DateTime.Now;
                //entity.TimesheetStatus = TimesheetStatusDao.Load(model.TimesheetStatusId);
                if (model.IsApproved)
                {
                    entity.ManagerDateAccept = DateTime.Now;
                    entity.ApprovedByManager = UserDao.Load(current.Id);
                    //!!! need to send e-mail
                    SendEmailForUserRequest(entity.User, current, entity.Creator, false, entity.Id,
                        entity.Number, RequestTypeEnum.Sicklist, false);
                }
            }
            int? superPersonnelId = ConfigurationService.SuperPersonnelId;
            if ((current.UserRole == UserRole.PersonnelManager
                && ((superPersonnelId.HasValue && CurrentUser.Id == superPersonnelId.Value) ||
                (user.Personnels.Where(x => x.Id == current.Id).FirstOrDefault() != null)) || current.UserRole == UserRole.OutsourcingManager)
                )
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
            if(model.IsDatesEditable)
            {
                // ReSharper disable PossibleInvalidOperationException
                entity.IsContinued = model.IsContinued;
                entity.BeginDate = model.BeginDate.Value;
                entity.EndDate = model.EndDate.Value;
                // ReSharper restore PossibleInvalidOperationException
                entity.DaysCount = model.EndDate.Value.Subtract(model.BeginDate.Value).Days + 1;
                entity.SicklistNumber = model.SicklistNumber;
            }
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
        }
        protected void SetPersonnelDataFromModel(Sicklist sicklist,SicklistEditModel model)
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
            model.Types = GetSicklistTypes(false,false);
            model.PaymentPercentTypes = GetSicklisPaymentPercentTypes(!model.IsPersonnelFieldsEditable,false);
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
                        model.IsApprovedByUserEnable = false;
                        break;
                    case UserRole.Manager:
                        model.IsApprovedByManagerEnable = false;
                        //model.IsTimesheetStatusEditable = true;
                        break;
                    case UserRole.OutsourcingManager:
                        model.IsApprovedByPersonnelManagerEnable = false;
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
                if (currentUserRole == UserRole.PersonnelManager || currentUserRole == UserRole.Manager || currentUserRole == UserRole.OutsourcingManager)
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
                    if (!entity.UserDateAccept.HasValue && !entity.DeleteDate.HasValue)
                    {
                        if(model.AttachmentId > 0)
                            model.IsApprovedEnable = true;
                        if (!entity.ManagerDateAccept.HasValue && !entity.PersonnelManagerDateAccept.HasValue && !entity.SendTo1C.HasValue)
                            model.IsDatesEditable = true;
                    }
                    break;
                case UserRole.Manager:
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
                case UserRole.OutsourcingManager:
                    // Разрешить согласование для аутсорсеров, если стаж уже есть в 1С
                    /*if (!entity.PersonnelManagerDateAccept.HasValue && model.AttachmentId > 0 &&
                        user.ExperienceIn1C == true)
                    {
                        model.IsApprovedEnable = true;
                        model.IsApprovedForAllEnable = true;                        
                    }*/
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
        public bool HaveAbsencesForPeriod(DateTime beginDate,DateTime endDate, int userId,
            int currentUserId,UserRole currentUserRole)
        {
            // Не выдавать ошибки конфликта дат для расчетчиков аутсорсинга
            if(currentUserRole == UserRole.PersonnelManager)
            {
                int? superPersonnelId = ConfigurationService.SuperPersonnelId;
                if(superPersonnelId.HasValue && currentUserId == superPersonnelId.Value)
                    return true;
            }
            if (currentUserRole == UserRole.Employee)
            {
                return true;
            }
            DateTime current = DateTime.Today;
            DateTime monthBegin = new DateTime(current.Year, current.Month, 1);
            if(current.Day == 1)
                monthBegin = monthBegin.AddMonths(-1);
            if (monthBegin < endDate)
                endDate = monthBegin;
            IList<BeginEndDateDto> absences = AbsenceDao.LoadForUserAndPeriod(beginDate, endDate, userId);
            current = beginDate;
            while(current <= endDate)
            {
                if (!IsAbsenceExists(absences, current))
                {
                    Log.InfoFormat("Absence not found for {0}",current);
                    return false;
                }
                current = current.AddDays(1);
            }
            return true;
        }
        protected  bool IsAbsenceExists(IList<BeginEndDateDto> absences,DateTime date)
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
                RequestStatuses = GetRequestStatuses(),
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
        public void SetAbsenceListModel(AbsenceListModel model,bool hasError)
        {
            User user = UserDao.Load(model.UserId);
            //model.Departments = GetDepartments(user);
            model.RequestStatuses = GetRequestStatuses();
            model.Positions = GetPositions(user);
            model.AbsenceTypes = GetAbsenceTypes(true);
            if(hasError)
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
                model.SortDescending);
        }
        public AbsenceEditModel GetAbsenceEditModel(int id, int userId)
        {
            AbsenceEditModel model = new AbsenceEditModel { Id = id, UserId = userId };
            User user = UserDao.Load(userId);
            IUser current = AuthenticationService.CurrentUser;
            if (!CheckUserRights(user, current,id,false))
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
            SetFlagsState(id,user,absence,model);
            return model;
        }
        protected List<IdNameDto> GetTimesheetStatusesForAbsence()
        {
            List<IdNameDto> dtos = TimesheetStatusDao.LoadAllSorted().
                Where(x => (x.Id >= AbsenceFirstTimesheetStatisId) && (x.Id <= AbsenceLastTimesheetStatisId)).ToList().
                ConvertAll(x => new IdNameDto(x.Id, x.Name)).OrderBy(x=>x.Name).ToList();
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
                if(currentUserRole == UserRole.PersonnelManager || currentUserRole == UserRole.Manager)
                {
                    model.IsApprovedByUserEnable = false;
                    model.IsApprovedByUserHidden = model.IsApprovedByUser  = true;
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
                {
                    absence = new Absence
                    {   
                        CreateDate = DateTime.Now,
                        Creator = UserDao.Load(current.Id),
// ReSharper disable PossibleInvalidOperationException
                        BeginDate = model.BeginDate.Value,
                        EndDate = model.EndDate.Value,
// ReSharper restore PossibleInvalidOperationException
                        DaysCount = model.EndDate.Value.Subtract(model.BeginDate.Value).Days+1,
                        Number = RequestNextNumberDao.GetNextNumberForType((int)RequestTypeEnum.Absence),
                        //Status = RequestStatusDao.Load((int)RequestStatusEnum.NotApproved),
                        Type = AbsenceTypeDao.Load(model.AbsenceTypeId),
                        User = user
                    };
                    if (current.UserRole == UserRole.Employee && current.Id == model.UserId && model.IsApproved)
                    {
                        absence.UserDateAccept = DateTime.Now;
                        SendEmailForUserRequest(absence.User, current, absence.Creator, false, absence.Id,
                         absence.Number, RequestTypeEnum.Absence, false);
                    }
                    if (current.UserRole == UserRole.Manager && user.Manager != null
                        && current.Id == user.Manager.Id)
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
                    if (current.UserRole == UserRole.PersonnelManager /*&& user.PersonnelManager != null
                        && current.Id == user.PersonnelManager.Id*/
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
                    AbsenceDao.SaveAndFlush(absence);
                    model.Id = absence.Id;
                }
                else
                {
                    absence = AbsenceDao.Load(model.Id);
                    if (absence.Version != model.Version)
                    {
                        error = "Заявка была изменена другим пользователем.";
                        model.ReloadPage = true;
                        return false;
                    }
                    if (model.IsDelete)
                    {
                        if (current.UserRole == UserRole.OutsourcingManager)
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
                        if (current.UserRole == UserRole.Employee && current.Id == model.UserId
                            && !absence.UserDateAccept.HasValue
                            && model.IsApproved)
                        {
                            absence.UserDateAccept = DateTime.Now;
                            SendEmailForUserRequest(absence.User, current, absence.Creator, false, absence.Id,
                                absence.Number, RequestTypeEnum.Absence, false);
                        }
                        if (current.UserRole == UserRole.Manager && user.Manager != null
                            && current.Id == user.Manager.Id
                            && !absence.ManagerDateAccept.HasValue)
                        {
                            //absence.TimesheetStatus = TimesheetStatusDao.Load(model.TimesheetStatusId);
                            if (model.IsApproved)
                            {
                                absence.ManagerDateAccept = DateTime.Now;
                                SendEmailForUserRequest(absence.User, current, absence.Creator, false, absence.Id,
                                    absence.Number, RequestTypeEnum.Absence, false);
                            }
                        }
                        if (current.UserRole == UserRole.PersonnelManager /*&& user.PersonnelManager != null
                            && current.Id == user.PersonnelManager.Id*/
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
                                              RequestStatuses = GetRequestStatuses(),
                                              Positions = GetPositions(user)
                                          };
            SetFlagsState(user, model);
            SetInitialDates(model);
            return model;
        }
        public void SetVacationListModel(VacationListModel model,bool hasError)
        {
            User user = UserDao.Load(AuthenticationService.CurrentUser.Id);
            //model.Departments = GetDepartments(user);
            model.RequestStatuses = GetRequestStatuses();
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
                // TODO SL: реализовать сохранение состояния свойства
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

        public void SetDocumentsToModel(VacationListModel model,User user)
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
                model.SortDescending);
        }
        public List<IdNameDto> GetDepartments(User user)
        {
            //var departmentList = DepartmentDao.LoadAllSorted().ToList().ConvertAll(x => new IdNameDto(x.Id, x.Name));
            //departmentList.Insert(0,new IdNameDto(0,SelectAll));
            //var departmentList = UserToDepartmentDao.GetByUserId(user.Id).ToList();
            if((UserRole)user.RoleId != UserRole.Employee)
            {
                var departmentList = DepartmentDao.LoadAllSorted().ToList().ConvertAll(x => new IdNameDto(x.Id, x.Name));
                departmentList.Insert(0,new IdNameDto(0,SelectAll));
                return departmentList;
            }
            if(user.Department == null)
                return new List<IdNameDto> { new IdNameDto(0, SelectAll) };
            return new List<IdNameDto> { new IdNameDto(user.Department.Id, user.Department.Name) };
        }
        public List<IdNameDto> GetVacationTypes(bool addAll)
        {
            List<IdNameDto> vacationTypeList = VacationTypeDao.LoadAllSorted().ToList()
                .ConvertAll(x => new IdNameDto(x.Id, x.Name));
            /*IList<VacationType> list =  VacationTypeDao.LoadAllSorted();
            List<IdNameDto> vacationTypeList = list.
                    Where(x =>
                    x.Name == "Оплата дня сдачи крови и доп. дня отдыха донорам #1125" ||
                    x.Name == "Оплата дополнительного отпуска по календарным дням #1207" ||
                    x.Name == "Оплата дополнительных выходных дней по уходу за детьми - инвалидами #1504" ||
                    x.Name == "Оплата отпуска по календарным дням #1201" ||
                    x.Name == "Оплата учебного отпуска по календарным дням #1204" ||
                    x.Name == "Отпуск без оплаты согласно ТК РФ #1205"
                    )
                .ToList().ConvertAll(x => new IdNameDto(x.Id, x.Name));
            if (addAll
                || AuthenticationService.CurrentUser.UserRole == UserRole.Manager
                || AuthenticationService.CurrentUser.UserRole == UserRole.PersonnelManager)
            vacationTypeList.AddRange(list.Where(x => x.Name == "Отпуск по уходу за ребенком без оплаты #1802")
                    .ToList().ConvertAll(x => new IdNameDto(x.Id, x.Name)));
            vacationTypeList = vacationTypeList.OrderBy(x => x.Name).ToList();*/
            if(addAll)
                vacationTypeList.Insert(0,new IdNameDto(0,SelectAll));
            return vacationTypeList;
        }

        public List<IdNameDto> GetAdditionalVacationTypes()
        {
            List<IdNameDto> additionalVacationTypeList = AdditionalVacationTypeDao.LoadAllSorted().ToList()
                .ConvertAll(x => new IdNameDto(x.Id, x.Name));
            return additionalVacationTypeList;
        }

        public List<IdNameDto> GetRequestStatuses()
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
                                                       }.OrderBy(x =>x.Name).ToList();
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
                    ? new List<IdNameDto> {new IdNameDto(position.Id,position.Name)} 
                    : new List<IdNameDto> { new IdNameDto(0, SelectAll) };
            }
            return positionList;
        }
        #endregion

        #region Vacation edit model
        public VacationEditModel GetVacationEditModel(int id,int userId)
        {
            VacationEditModel model = new VacationEditModel {Id = id, UserId = userId};
            User user = UserDao.Load(userId);
            IUser current = AuthenticationService.CurrentUser;
            if (!CheckUserRights(user, current,id,false))
                throw new ArgumentException("Доступ запрещен.");
            SetUserInfoModel(user, model);
            SetAttachmentToModel(model, id, RequestAttachmentTypeEnum.Vacation);
            SetOrderScanAttachmentToModel(model, id, RequestAttachmentTypeEnum.VacationOrderScan);
            model.CommentsModel = GetCommentsModel(id, (int)RequestTypeEnum.Vacation);
            model.TimesheetStatuses = GetTimesheetStatusesForVacation();
            model.VacationTypes = GetVacationTypes(false);
            model.AdditionalVacationTypes = GetAdditionalVacationTypes();
            Vacation vacation = null; 
            if(id == 0)
            {
                model.CreatorLogin = current.Name;
                model.Version = 0;
                model.DateCreated = DateTime.Today.ToShortDateString();
            }
            else
            {
                vacation = VacationDao.Load(id);
                if(vacation == null)
                    throw new ArgumentException(string.Format("Заявка на отпуск (id {0}) не найдена в базе данных.",id));
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
                model.PrincipalVacationDaysLeft = vacation.PrincipalVacationDaysLeft ?? 0;
                model.AdditionalVacationDaysLeft = vacation.AdditionalVacationDaysLeft ?? 0;
                if (vacation.DeleteDate.HasValue)
                    model.IsDeleted = true;
            }
            SetFlagsState(id, user,vacation, model);
            return model;
        }
        public bool SaveVacationEditModel(VacationEditModel model, UploadFileDto fileDto, UploadFileDto orderScanFileDto, out string error)
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
                if(model.Id == 0)
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
                    if (current.UserRole == UserRole.Employee && current.Id == model.UserId
                        && model.IsApproved && !vacation.UserDateAccept.HasValue)
                    {
                        vacation.UserDateAccept = DateTime.Now;
                        SendEmailForUserRequest(vacation.User, current, vacation.Creator, false, vacation.Id,
                            vacation.Number, RequestTypeEnum.Vacation, false);
                    }
                    if (current.UserRole == UserRole.Manager && user.Manager != null
                        && current.Id == user.Manager.Id)
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
                    if (current.UserRole == UserRole.PersonnelManager /*&& user.PersonnelManager != null
                        && current.Id == user.PersonnelManager.Id*/
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

                    VacationDao.SaveAndFlush(vacation);
                    model.Id = vacation.Id;
                }
                else
                {
                    vacation = VacationDao.Load(model.Id);
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
                        model.AttachmentId = 0;
                        model.Attachment = string.Empty;
                        model.OrderScanAttachmentId = 0;
                        model.OrderScanAttachment = string.Empty;
                        if (current.UserRole == UserRole.OutsourcingManager)
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
                        if (current.UserRole == UserRole.Employee && current.Id == model.UserId
                            && !vacation.UserDateAccept.HasValue
                            && model.IsApproved)
                        {
                            vacation.UserDateAccept = DateTime.Now;
                            SendEmailForUserRequest(vacation.User, current, vacation.Creator, false, vacation.Id,
                                vacation.Number, RequestTypeEnum.Vacation, false);
                        }
                        if (current.UserRole == UserRole.Manager && user.Manager != null
                            && current.Id == user.Manager.Id
                            && !vacation.ManagerDateAccept.HasValue )
                        {
                            //vacation.TimesheetStatus = TimesheetStatusDao.Load(model.TimesheetStatusId);
                            if (model.IsApproved)
                            {
                                vacation.ManagerDateAccept = DateTime.Now;
                                SendEmailForUserRequest(vacation.User, current, vacation.Creator, false, vacation.Id,
                                    vacation.Number, RequestTypeEnum.Vacation, false);
                            }
                        }
                        if (current.UserRole == UserRole.PersonnelManager /*&& user.PersonnelManager != null
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
                        if (model.IsVacationTypeEditable)
                        {
// ReSharper disable PossibleInvalidOperationException
                            vacation.BeginDate = model.BeginDate.Value;
                            vacation.EndDate = model.EndDate.Value;
                            vacation.AdditionalVacationBeginDate = model.AdditionalVacationBeginDate;
// ReSharper restore PossibleInvalidOperationException
                            vacation.DaysCount = model.EndDate.Value.Subtract(model.BeginDate.Value).Days+1;
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
                model.DocumentNumber = vacation.Number.ToString();
                model.Version = vacation.Version;
                model.DaysCount = vacation.DaysCount;
                model.AdditionalVacationDaysCount = vacation.AdditionalVacationDaysCount;
                model.PrincipalVacationDaysLeft = vacation.PrincipalVacationDaysLeft;
                model.AdditionalVacationDaysLeft = vacation.AdditionalVacationDaysLeft;
                model.CreatorLogin = vacation.Creator.Name;
                model.DateCreated = vacation.CreateDate.ToShortDateString();
                SetFlagsState(vacation.Id,user,vacation,model);
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
            if (current.UserRole == UserRole.OutsourcingManager)
            {
                if (model.IsDeleteAvailable && model.IsDelete)
                    return true;
                throw new ArgumentException("Вам запрещено редактировать заявки.");
            }
            return true;
        }
        public bool CheckUserRights(User user, IUser current,int entityId,bool isSave)
        {
            switch (current.UserRole)
            {
                case UserRole.Employee:
                    if (user.Id != current.Id)
                    {
                        Log.ErrorFormat("CheckUserRights user.Id {0} current.Id {1}",user.Id,current.Id);
                        return false;
                    }
                    break;
                case UserRole.Manager:
                    if (user.Manager != null && user.Manager.Id != current.Id)
                    {
                        Log.ErrorFormat("CheckUserRights user.Id {0} current.Id {1} user.Manager.Id {2}", user.Id, current.Id, user.Manager.Id);
                        return false;
                    }
                    break;
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
                    if(isSave)
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
        protected void SetFlagsState(VacationEditModel model,bool state)
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
        protected void SetFlagsState(int id,User user,Vacation vacation,VacationEditModel model)
        {
            SetFlagsState(model,false);
            int? superPersonnelId = ConfigurationService.SuperPersonnelId;
            UserRole currentUserRole = AuthenticationService.CurrentUser.UserRole;
            if(id == 0)
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
                    case UserRole.PersonnelManager:
                        //model.IsApprovedByPersonnelManagerEnable = false;
                        model.IsTimesheetStatusEditable = true;
                        break;
                }
                if (currentUserRole == UserRole.PersonnelManager || currentUserRole == UserRole.Manager)
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
            
            switch(currentUserRole)
            {
                case UserRole.Employee:
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
                    if (model.IsPostedTo1C && model.OrderScanAttachmentId <= 0)
                    {
                        model.IsConfirmationAllowed = true;
                    }
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
                case UserRole.OutsourcingManager:
                    if (vacation.SendTo1C.HasValue && !vacation.DeleteDate.HasValue)
                        model.IsDeleteAvailable = true;
                    break;
            }
            model.IsSaveAvailable = model.IsVacationTypeEditable || model.IsTimesheetStatusEditable;
            //|| model.IsApprovedByManagerEnable || model.IsApprovedByUserEnable ||
            //model.IsApprovedByPersonnelManagerEnable;
        }
        protected void SetUserInfoModel(User user,UserInfoModel model)
        {
            //model.DateCreated = DateTime.Today.ToShortDateString();
            //IList<IdNameDto> departments = UserToDepartmentDao.GetByUserId(user.Id);
            //if (departments.Count > 0)
            model.Department = user.Department == null?string.Empty:user.Department.Name ;
            if(user.Manager != null)
                model.ManagerName = user.Manager.FullName;
            /*if (user.PersonnelManager != null)
                model.PersonnelName = user.PersonnelManager.FullName;*/
            if(user.Personnels.Count() > 0)
                model.PersonnelName = user.Personnels.Aggregate(string.Empty, (current, entity) => current + (entity.FullName + "; "));
            if (user.Organization != null)
                model.Organization = user.Organization.Name;
            if(user.Position != null)
                model.Position = user.Position.Name;
            model.UserName = user.FullName;
            model.UserNumber = user.Code;
            model.UserEmail = user.Email;
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
            if(AuthenticationService.CurrentUser.UserRole == UserRole.Employee)
                dtos.Insert(0,new IdNameDto(0,string.Empty));
            return dtos;
        }
        public int GetOtherRequestCountsForUserAndDates(DateTime beginDate,
            DateTime endDate,int userId,int vacationId, bool isChildVacantion)
        {
            return VacationDao.GetRequestCountsForUserAndDates(beginDate, endDate
                                                               , userId, vacationId,isChildVacantion);
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
                RequestStatuses = GetRequestStatuses(),
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
            model.RequestStatuses = GetRequestStatuses();
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
                // TODO SL: реализовать сохранение состояния свойства
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
                model.SortDescending);
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
            if (id == 0)
            {
                model.IsSaveAvailable = true;
                model.IsVacationDatesEditable = true;
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
                    case UserRole.PersonnelManager:
                        //model.IsApprovedByPersonnelManagerEnable = false;
                        model.IsPersonnelFieldsEditable = true;
                        break;
                }
                if (currentUserRole == UserRole.PersonnelManager || currentUserRole == UserRole.Manager)
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
                    if (!vacation.UserDateAccept.HasValue && !vacation.DeleteDate.HasValue)
                    {
                        if (model.AttachmentId > 0)
                            model.IsApprovedEnable = true;
                        if (!vacation.ManagerDateAccept.HasValue && !vacation.PersonnelManagerDateAccept.HasValue && !vacation.SendTo1C.HasValue)
                            model.IsVacationDatesEditable = true;
                    }
                    break;
                case UserRole.Manager:
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
                else
                {
                    childVacation = ChildVacationDao.Load(model.Id);
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
                        if (current.UserRole == UserRole.OutsourcingManager)
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
            if (current.UserRole == UserRole.Employee && current.Id == model.UserId
                && !entity.UserDateAccept.HasValue
                && model.IsApproved)
            {
                entity.UserDateAccept = DateTime.Now;
                //!!! need to send e-mail
                SendEmailForUserRequest(entity.User, current, entity.Creator, false, entity.Id,
                    entity.Number, RequestTypeEnum.ChildVacation, false);
            }
            if (current.UserRole == UserRole.Manager && user.Manager != null
                && current.Id == user.Manager.Id
                && !entity.ManagerDateAccept.HasValue)
            {
                if (model.IsApprovedByUser && !entity.UserDateAccept.HasValue)
                    entity.UserDateAccept = DateTime.Now;
                //entity.TimesheetStatus = TimesheetStatusDao.Load(model.TimesheetStatusId);
                if (model.IsApproved)
                {
                    entity.ManagerDateAccept = DateTime.Now;
                    //!!! need to send e-mail
                    SendEmailForUserRequest(entity.User, current, entity.Creator, false, entity.Id,
                        entity.Number, RequestTypeEnum.ChildVacation, false);
                }
            }
            int? superPersonnelId = ConfigurationService.SuperPersonnelId;
            if (current.UserRole == UserRole.PersonnelManager
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
            if (model.IsVacationDatesEditable)
            {
                // ReSharper disable PossibleInvalidOperationException
                entity.BeginDate = model.BeginDate.Value;
                entity.EndDate = model.EndDate.Value;
                // ReSharper restore PossibleInvalidOperationException
            }
            //if (model.IsTypeEditable)
            //{
            //    entity.Type = SicklistTypeDao.Load(model.TypeId);
            //    if (model.TypeId == sicklistTypeDao.SicklistTypeIdBabyMinding)
            //        entity.BabyMindingType = model.BabyMindingTypeId.HasValue
            //            ? SicklistBabyMindingTypeDao.Load(model.BabyMindingTypeId.Value)
            //            : null;
            //    else
            //        entity.BabyMindingType = null;
            //}
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
            model.CommentsModel = GetCommentsModel(model.Id, (int) RequestTypeEnum.ChildVacation);
        }

        #endregion

        #region Comments
        public  RequestCommentsModel GetCommentsModel(int id,int typeId)
        {
            return SetCommentsModel(id,typeId);
        }
        protected RequestCommentsModel SetCommentsModel(int id,int typeId)
        {
            RequestCommentsModel commentModel = new RequestCommentsModel 
            { 
                RequestId = id 
                ,RequestTypeId = typeId 
                ,Comments = new List<RequestCommentModel>() };
            if (id > 0)
            {
                switch(typeId)
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
                switch(model.TypeId)
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
                        Dismissal  dismissal = DismissalDao.Load(model.DocumentId);
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
                            MissionOrder  = missionOrder,
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
            if(id > 0)
            {
                Employment entity = EmploymentDao.Load(id);
                isAddAvailable = !entity.SendTo1C.HasValue && !entity.DeleteDate.HasValue;
                isDeleteAvailable = !entity.SendTo1C.HasValue && !entity.DeleteDate.HasValue;
                if(isDeleteAvailable && list.Count <= 4 )
                {
                     if((entity.UserDateAccept.HasValue && CurrentUser.UserRole == UserRole.Employee) ||
                        (entity.ManagerDateAccept.HasValue && CurrentUser.UserRole == UserRole.Manager) ||
                        (entity.PersonnelManagerDateAccept.HasValue && CurrentUser.UserRole == UserRole.PersonnelManager)
                       )
                         isDeleteAvailable = false;
                }
            }    
            RequestAttachmentsModel model = new RequestAttachmentsModel
            {
                AttachmentRequestId = id,
                AttachmentRequestTypeId =(int) typeId,
                IsAddAvailable = isAddAvailable,
                Attachments = new List<RequestAttachmentModel>()
            };
            model.Attachments =list. ConvertAll(x =>
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
            return true;
        }
        public bool DeleteAttachment(DeleteAttacmentModel model)
        {
            RequestAttachmentDao.Delete(model.Id);
            return true;
        }
        public int GetAttachmentsCount(int entityId,RequestAttachmentTypeEnum typeId)
        {
            return RequestAttachmentDao.FindManyByRequestIdAndTypeId(entityId, typeId).Count;
        }
        public string GetFileContext(string fileName)
        {
            string extension = Path.GetExtension(fileName);
            //string contextType;
            switch (extension)
            {
                case ".doc":
                case ".docx":
                    return  "application/msword";
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
            DepartmentTreeModel model = new DepartmentTreeModel {DepartmentID = departmentId};
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
            SetTreeSelection(model,list);
        }
        protected void SetTreeSelection(DepartmentTreeModel model, IList<Department> list)
        {
            if (model.DepartmentID == 0)
                return;
            int departmentId = model.DepartmentID; 
            Department selected = list.Where(x => x.Id == departmentId).FirstOrDefault();
            if(selected == null)
                throw new ArgumentException(string.Format("Не найдено структурное подразделение (id {0})",departmentId));
            int level = selected.ItemLevel.Value;
            for (int i = level; i > 1; i--)
            {
                SetSelection(model,selected);
                if (i == 2)
                    return;
                selected = list.Where(x => x.Code1C.Value == selected.ParentId.Value).FirstOrDefault();
                if (selected == null)
                    throw new ArgumentException(string.Format("Не найдено родительское структурное подразделение уровня {0}", i));
            }
        }
        protected void SetSelection(DepartmentTreeModel model,Department selected/*,int level*/)
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
                    throw new ArgumentException(string.Format("Неизвестный уровень структурного подразделения {0}",selected.ItemLevel.Value));
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

        public AttachmentModel GetPrintFormFileContext(int id,RequestPrintFormTypeEnum typeId)
        {
            RequestPrintForm printForm = RequestPrintFormDao.FindByRequestAndTypeId(id,typeId);
            if(printForm == null)
                throw new ArgumentException(string.Format("Печатная форма для заявки (Id {0}) не найдена в базе данных",id));
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
                if (!string.IsNullOrEmpty(model.AcceptDate) || currentUser.UserRole == UserRole.Manager)
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
                                        user.UserRole == UserRole.Manager;
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
            User user = null;
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
                                                       }.OrderBy(x => x.Name).ToList();
            if(addAll)
                deductionStatuses.Insert(0, new IdNameDto(0, SelectAll));
            return deductionStatuses;
        }
        public List<IdNameDto> GetDeductionTypes(bool addAll)
        {
            List<IdNameDto> deductionTypes = DeductionTypeDao.LoadAllSorted().ToList().ConvertAll(x => new IdNameDto {Id = x.Id, Name = x.Name}).ToList();
            if(addAll)
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
                model.SortDescending);
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
                model.UserId = model.Users[0].Id;
                model.MonthId = today.Year * 100 + today.Month;
            }
            else
            {
                deduction = DeductionDao.Load(id);
                if (deduction == null)
                    throw new ArgumentException(string.Format("Удержание (id {0}) не найдена в базе данных.", id));
                model.Version = deduction.Version;
                model.UserId = deduction.User.Id;
                IdNameDto dto = model.Users.Where(x => x.Id == model.UserId).FirstOrDefault();
                if(dto == null)
                    throw new ArgumentException(
               string.Format("Пользователь {0} не является сотрудником или уволен более 3 месяцев назад",deduction.User.Name));
                model.KindId = deduction.Kind.Id;
                model.Sum = deduction.Sum.ToString();
                model.TypeId = deduction.Type.Id;
                model.MonthId = deduction.DeductionDate.Year*100 + deduction.DeductionDate.Month;
                model.DateEdited = deduction.EditDate.ToShortDateString();
                model.DocumentNumber = deduction.Number.ToString();
                if(deduction.Type.Id != (int) DeductionTypeEnum.Deduction)
                {
                    model.DismissalDate = deduction.DismissalDate;
                    model.IsFastDismissal = deduction.IsFastDismissal.HasValue?deduction.IsFastDismissal.Value:false;
                }
               
                if (deduction.DeleteDate.HasValue)
                    model.IsDeleted = true;
                SetHiddenFields(model);
            }
            SetDeductionUserInfoModel(model,model.UserId);
            SetEditor(model, deduction, current);
            SetStatus(model, deduction);
            SetFlagsState(id, /*user,*/ deduction, model);
            return model;
        }
        protected void SetStatus(DeductionEditModel model,Deduction deduction)
        {
            if(model.Id == 0)
                model.Status = "Новая";
            else
            {
                model.Status = deduction.DeleteDate.HasValue
                                   ? "Отклонена"
                                   : deduction.SendTo1C.HasValue ? "Выгружена в 1С" : "Записана";
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
            model.Editor = editor.Name + (string.IsNullOrEmpty(editor.Email) ? string.Empty : ", " + editor.Email);
        }
        public void SetDeductionUserInfoModel(DeductionUserInfoModel model,int userId)
        {
            model.UserInfoError = string.Empty;
            User user = userDao.Load(userId);
            if(user == null)
                throw new ValidationException(string.Format("Не могу загрузить пользователя (id {0})", userId));
            if((user.UserRole & UserRole.Employee) == 0)
                throw new ValidationException(string.Format("Пользователь (id {0}) не является сотрудником", userId));
            model.Department = user.Department == null ? string.Empty : user.Department.Name;
            model.Position = user.Position == null ? string.Empty : user.Position.Name;
            model.Cnilc = user.Cnilc;
            DateTime? dateRelease = null;
            if(user.DateRelease.HasValue)
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
        protected void SetFlagsState(int id, /*User user,*/ Deduction deduction, DeductionEditModel model)
        {
            SetFlagsState(model, false);
            UserRole currentUserRole = AuthenticationService.CurrentUser.UserRole;
            if (id == 0 && (currentUserRole & UserRole.Accountant) > 0)
            {
                model.IsEditable = true;
                model.IsSaveAvailable = true;
                return;
            }
            switch (currentUserRole)
            {
                case UserRole.OutsourcingManager:
                    if (deduction.SendTo1C.HasValue && !deduction.DeleteDate.HasValue)
                        model.IsDeleteAvailable = true;
                    break;
                case UserRole.Accountant:
                    if (!deduction.SendTo1C.HasValue && !deduction.DeleteDate.HasValue)
                    {
                        if(deduction.EditDate.Month >= DateTime.Today.Month)
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
        protected void LoadDictionaries(DeductionEditModel model)
        {
            model.Types = GetDeductionTypes(false);
            model.Kindes = GetDeductionKinds();
            model.Monthes = GetDeductionMonthes();
            model.Users = userDao.GetUserListForDeduction();
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
            DateTime beginDate = new DateTime(today.Year,1,1);
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
            SetStatus(model,deduction);
        }
        public bool CheckDeductionUserRights(IUser current)
        {
            if((current.UserRole & UserRole.Accountant) > 0 ||
               (current.UserRole & UserRole.OutsourcingManager) > 0)
                return true;
            return false;
        }

        public bool SaveDeductionEditModel(DeductionEditModel model, out string error)
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
                    ChangeEntityProperties(deduction,model);
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
                        if (current.UserRole == UserRole.OutsourcingManager)
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
                        SendEmailToUser(model, deduction);
                        DeductionDao.SaveAndFlush(deduction);
                    }
                    if (deduction.DeleteDate.HasValue)
                        model.IsDeleted = true;
                }
                model.DocumentNumber = deduction.Number.ToString();
                model.Version = deduction.Version;
              
                SetFlagsState(deduction.Id,deduction, model);
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
        protected void SendEmailToUser(DeductionEditModel model,Deduction deduction)
        {
            User user = UserDao.Load(model.UserId);
            if(string.IsNullOrEmpty(user.Email))
            {
                Log.ErrorFormat("E-mail is empty for user {0}",user.Id);
                return;
            }
            if(!deduction.DeleteDate.HasValue)
            {
                string defaultEmail = ConfigurationService.DefaultDeductionEmail;
                string to = string.IsNullOrEmpty(defaultEmail)?user.Email:defaultEmail;
                string roubles = ((int)deduction.Sum) +" "+ GetRubleSumAsString((int)deduction.Sum);
                int cp = ((int)(deduction.Sum * 100) % 100);
                string copeck = (cp).ToString("D2") + " " + GetCopeckSumAsString(cp);
                string body =
                    string.Format(
                        @"Из Вашей  заработной платы  будет произведено  удержание  № {5} в сумме  {0} {1}, вид удержания - {2}. Автор:{3} E-mail: {4}.",
                roubles,copeck,deduction.Kind.Name,deduction.Editor.FullName,deduction.Editor.Email,deduction.Number);
                EmailDto dto = SendEmail(to, "Удержание", body);
                if (string.IsNullOrEmpty(dto.Error))
                    deduction.EmailSendToUserDate = DateTime.Now;
                else
                    Log.ErrorFormat("Cannot send email to user {0}(email {1}) about deduction {2} : {3}",
                        user.Id,to,deduction.Id,dto.Error);
            }
        }
        protected void ChangeEntityProperties(Deduction entity, DeductionEditModel model)
        {
            if(model.IsEditable)
            {
                entity.DeductionDate = new DateTime(model.MonthId/100, model.MonthId%100, 1);
                entity.Kind = DeductionKindDao.Load(model.KindId);
                entity.Type = DeductionTypeDao.Load(model.TypeId);
                entity.User = UserDao.Load(model.UserId);
                entity.Sum = ((Decimal)((int)((Decimal.Parse(model.Sum))*100)))/100;
                if(model.TypeId != (int)DeductionTypeEnum.Deduction)
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
            model.Level3 = l3.ConvertAll(x => new IdNameDto { Id = x.Id, Name = x.Name + (!string.IsNullOrEmpty(x.ShortName)?" ( "+x.ShortName+" )":string.Empty ) });
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
                throw new ArgumentException(string.Format("Не могу найти ни одной точки для уровня {0}",level));
            return l;//l.ConvertAll(x => new IdNameDto { Id = x.Id, Name = x.Name });
        }
        public TerraPointChildrenDto GetTerraPointChildren(int parentId, int level)
        {
            bool isHoliday = false;
            try
            {
                TerraPoint parent = TerraPointDao.Load(parentId);
                if (parent == null)
                    throw new ArgumentException(string.Format("Точка с Id {0} отсутствует в базе данных",parentId));
                isHoliday = parent.IsHoliday;
                List<IdNameDto> children;
                List<IdNameDto> level3Children = new List<IdNameDto>();
                string shortName;
                if(level == 2)
                {
                    List<TerraPoint> l2 = LoadTpListForLevelAndParentId(2, parent.Code1C);//TerraPointDao.FindByLevelAndParentId(2, parent.Code1C).ToList();
                    children = l2.ConvertAll(x => new IdNameDto { Id = x.Id, Name = x.Name });
                    TerraPoint p2 = l2[0];
                    List<TerraPoint> l3 = LoadTpListForLevelAndParentId(3, p2.Code1C);//TerraPointDao.FindByLevelAndParentId(3, p2.Code1C).ToList();
                    level3Children = l3.ConvertAll(x => new IdNameDto { Id = x.Id, Name = x.Name + (!string.IsNullOrEmpty(x.ShortName) ? " ( " + x.ShortName + " )" : string.Empty) });
                    TerraPoint p3 = l3[0];
                    shortName = p3.ShortName;
                }
                else if(level == 3)
                {
                    List<TerraPoint> l3 = LoadTpListForLevelAndParentId(3, parent.Code1C);//TerraPointDao.FindByLevelAndParentId(3, parent.Code1C).ToList();
                    children = l3.ConvertAll(x => new IdNameDto { Id = x.Id, Name = x.Name + (!string.IsNullOrEmpty(x.ShortName) ? " ( " + x.ShortName + " )" : string.Empty) });
                    //if (l3.Count == 0)
                    //    throw new ArgumentException(string.Format("GetTerraPointChildren:Не найдено ни одной точки для уровня 3 и ParentId {0}", parent.Code1C));
                    TerraPoint p3 = l3[0];
                    shortName = p3.ShortName;
                }
                else
                    throw new ValidationException(string.Format("Неизвестный уровень точки {0}",level));
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
                    Error = string.Format("Ошибка: {0}",ex.GetBaseException().Message),
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
        public TerraPointShortNameDto SaveTerraPointShortName(int pointId,string shortName)
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
            if(id == 0)
                throw new ArgumentException("Невозможно удалить новую точку");
            //TerraGraphic tg = TerraGraphicDao.Load(id);
            //if (tg == null)
            //    throw new ArgumentException(string.Format("Точка (ID {0}) отсутствует в базе данных", id));
            TerraGraphicDao.DeleteAndFlush(id);
        }
        public void SaveTerraPoint(TerraPointSaveModel model)
        {
            TerraGraphic tg;
            if(model.Id == 0)
            {
                tg = new TerraGraphic {Day = model.TpDay, UserId = model.UserId};
            }
            else
            {
                tg = TerraGraphicDao.Load(model.Id);
                if(tg == null)
                    throw new ArgumentException(string.Format("Точка (ID {0}) отсутствует в базе данных", model.Id));
            }
            if (model.TpDay.Date < DateTime.Today)
            {
                tg.FactPointId = model.FactPointId == 0 ? new int?() : model.FactPointId;
                tg.FactHours = string.IsNullOrEmpty(model.FactHours) ? new decimal?() : model.TpFactHours;
            }
            else
            {
                tg.Hours = string.IsNullOrEmpty(model.Hours) ? new decimal?() : model.TpHours; //model.TpHours;
                tg.IsCreditAvailable = GetIsCreditAvailable(model.IsCreditAvailable);
                tg.PointId = model.PointId == 0 ? new int?() : model.PointId;
                tg.FactPointId = model.FactPointId == 0 ? new int?() : model.FactPointId;
                tg.FactHours = string.IsNullOrEmpty(model.FactHours) ? new decimal?() : model.TpFactHours;
            }
            TerraGraphicDao.SaveAndFlush(tg);
            model.Error = string.Empty;
        }
        public bool? GetIsCreditAvailable(int credits)
        {
            switch(credits)
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
            if( tp == null)
                throw new ValidationException(string.Format("Точка (ID {0}) не найдена в базе данных",pointId));
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
            return new TerraPointSetDefaultTerraPointModel {Error = string.Empty, PointToUserId = tpToUser.Id};
        }

        public void SetEditPointDialogModel(TerraGraphicsEditPointModel model)
        {
            SetupCreditsCombo(model);
            User user = UserDao.Load(model.UserId);
            if(user == null)
                throw new ArgumentException(string.Format("Сотрудник (Id {0}) не найден в базе данных", model.UserId));
            if((user.RoleId & (int)UserRole.Employee) == 0)
                throw new ArgumentException(string.Format("Пользователь {1}(Id {0}) не является сотрудником", model.UserId,user.Name));
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
               


            if(model.Id == 0)
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
                    LoadTerraPoints23Level(tp.ParentId, tp.Id, model,false);
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
                    LoadTerraPoints23Level(tp.ParentId, tp.Id, model,true);
                }
            }
        }
        protected void LoadTerraPoints23Level(string parentId,int level3Id,TerraGraphicsEditPointModel model,bool factPoint)
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
            if(factPoint)
            {
                if (l3Point.IsHoliday)
                    model.IsFactHoliday = true;
            }
            else
            {
                if (l3Point.IsHoliday)
                    model.IsPlanHoliday = true;
            }
            IdNameDto tp2 = LoadByCode1AndPath(parentId,l3Point.Path);
            List<TerraPoint> l2 = LoadTpListForLevelAndParentId(2, tp2.Name);
            if (factPoint)
            {
                model.FactEpLevel2 = l2.ConvertAll(x => new IdNameDto { Id = x.Id, Name = x.Name });
                model.FactEpLevel2ID = tp2.Id;
            }
            else
            {
                model.EpLevel2 = l2.ConvertAll(x => new IdNameDto {Id = x.Id, Name = x.Name});
                model.EpLevel2ID = tp2.Id;
            }
            TerraPoint tp1 = LoadByCode1C(tp2.Name);
            if(factPoint)
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
        protected IdNameDto LoadByCode1AndPath(string code1C,string path)
        {
            IdNameDto terraPoint = TerraPointDao.FindByCode1CAndPath(code1C,path);
            if (terraPoint == null)
                throw new ArgumentException(string.Format("Точка с Code1C {0} и путем {1} отсутствует в базе данных", code1C,path));
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
                    list = UserDao.GetUsersForCreateMissionOrder(currentUser.Department.Path,
                                                                             new List<int> {3},2);
                    model.Users = list;
                    break;
                case 3:
                    list = UserDao.GetUsersForCreateMissionOrder(currentUser.Department.Path,
                                                                             new List<int> { 4,5 }, 3);
                    model.Users = list;
                    break;
                case 5:
                    list = UserDao.GetManagersAndEmployeesForCreateMissionOrder(currentUser.Department.Path,
                                                                             new List<int> { 6 }, 5);
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
            switch(CurrentUser.UserRole)
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
                    if(currentUser == null)
                        throw new ArgumentException(string.Format("Не могу загрузить пользователя {0} из базы даннных",
                            CurrentUser.Id));
                    model.IsAddAvailable = currentUser.IsMainManager && ((currentUser.Level == 2) ||
                                           (currentUser.Level == 3) ||
                                           (currentUser.Level == 5));
                    model.IsApproveAvailable = model.IsAddAvailable || (currentUser.MissionOrderRoleRecords
                        .Where<MissionOrderRoleRecord>(morr => morr.Role.Id == 1)
                        .FirstOrDefault<MissionOrderRoleRecord>() != null);
                    break;
                case UserRole.Director:
                    model.IsApproveAvailable = true;
                    break;
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
                if(model.IsApproveClick)
                {
                    model.IsApproveClick = false;
                    List<int> idsForApprove = model.Documents.Where(x => x.IsChecked).Select(x => x.Id).ToList();
                    ApproveOrders(model,idsForApprove);
                }
                SetDocumentsToModel(model, user);
            }
            //model.Documents = new List<MissionOrderDto>();
        }
        protected void ApproveOrders(MissionOrderListModel model,List<int> idsForApprove)
        {
            List<MissionOrder> orders = MissionOrderDao.LoadForIdsList(idsForApprove).ToList();
            foreach (MissionOrder order in orders)
                ApproveOrder(model,order);
        }
        protected void ApproveOrder(MissionOrderListModel model,MissionOrder order)
        {
            switch (CurrentUser.UserRole)
            {
                case UserRole.Manager:
                    ApproveOrderForManager(model,order);
                    break;
                case UserRole.Director:
                    ApproveOrderForDirector(model, order);
                    break;
                default:
                    model.HasErrors = true;
                    Log.ErrorFormat("Cannot approve order {0} user {1} role {2}",order.Id,CurrentUser.Id,CurrentUser.UserRole);
                    break;
            }
        }
        protected void ApproveOrderForManager(MissionOrderListModel model,MissionOrder entity)
        {
            try
            {
                if(entity.UserDateAccept.HasValue && !entity.ManagerDateAccept.HasValue)
                {
                    bool canEdit;
                    if ((IsUserManagerForEmployee(entity.User, CurrentUser, out canEdit) && canEdit) || CanUserApproveMissionOrderForEmployee(entity.User, CurrentUser, out canEdit))
                    {
                        entity.ManagerDateAccept = DateTime.Now;
                        entity.AcceptManager = UserDao.Load(CurrentUser.Id);
                        entity.EditDate = DateTime.Now;
                        if (!entity.NeedToAcceptByChief)
                        {
                            CreateMission(entity);
                            SendEmailForMissionOrderConfirm(CurrentUser, entity);
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
                Log.Error("ApproveOrderForManager",ex);
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
                    if(entity.NeedToAcceptByChief)
                    {
                        entity.ChiefDateAccept = DateTime.Now;
                        entity.EditDate = DateTime.Now;
                        entity.AcceptChief = current; 
                    }
                    CreateMission(entity);
                    SendEmailForMissionOrderConfirm(CurrentUser, entity);
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
                        SendEmailForMissionOrderConfirm(CurrentUser, entity);
                        MissionOrderDao.SaveAndFlush(entity);
                        isAccept = true;
                }
                if(!isAccept)
                {
                    Log.ErrorFormat("Cannot approve order {0} user {1} role {2}.",entity.Id, CurrentUser.Id
                        ,CurrentUser.UserRole);
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
                model.SortBy,
                model.SortDescending);
        }        
        public void SetDictionariesToModel(MissionOrderListModel model)
        {
            model.Statuses = GetMoStatuses();
        }
        public List<IdNameDto> GetMoStatuses()
        {
            //var requestStatusesList = RequestStatusDao.LoadAllSorted().ToList().ConvertAll(x => new IdNameDto(x.Id, x.Name));
            List<IdNameDto> moStatusesList = new List<IdNameDto>
                                                       {
                                                           new IdNameDto(1, "Одобрен сотрудником"),
                                                           new IdNameDto(2, "Не одобрен сотрудником"),
                                                           new IdNameDto(3, "Одобрен руководителем"),
                                                           new IdNameDto(4, "Не одобрен руководителем"),
                                                           new IdNameDto(5, "Одобрен членом правления"),
                                                           new IdNameDto(6, "Не одобрен членом правления"),
                                                           new IdNameDto(7, "Требует одобрения руководителем"),
                                                           new IdNameDto(8, "Требует одобрения членом правления"),
                                                           //new IdNameDto(10, "Отклоненные"),
                                                       }.OrderBy(x => x.Name).ToList();
            moStatusesList.Insert(0, new IdNameDto(0, SelectAll));
            return moStatusesList;
        }
        public MissionOrderEditModel GetMissionOrderEditModel(int id, int? userId)
        {
            if(id == 0 && !userId.HasValue)
            {
                if (CurrentUser.UserRole == UserRole.Employee)
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
            }
            MissionOrderEditModel model = new MissionOrderEditModel
                    {
                        Id = id,
                        UserId = id == 0 ? userId.Value : entity.User.Id
                    };
            User user = UserDao.Load(model.UserId);
            if(!user.Grade.HasValue)
                throw new ValidationException(string.Format("Не указан грейд для пользователя {0} в базе данных",user.Id));
            IUser current = AuthenticationService.CurrentUser;
           
            if (!CheckUserMoRights(user, current, id, entity, false))
                throw new ArgumentException("Доступ запрещен.");
            //model.CommentsModel = GetCommentsModel(id, (int)RequestTypeEnum.MissionOrder);
            if(id != 0)
            {
                LoadGraids(model, user.Grade.Value, entity,entity.CreateDate);
                model.AllSum = FormatSum(entity.AllSum);
                model.AllSumAir = FormatSum(entity.SumAir);
                model.AllSumDaily = FormatSum(entity.SumDaily);
                model.AllSumResidence = FormatSum(entity.SumResidence);
                model.AllSumTrain = FormatSum(entity.SumTrain);
                model.BeginMissionDate = entity.BeginDate.ToShortDateString();
                model.EndMissionDate = entity.EndDate.ToShortDateString();
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
                model.DateCreated = entity.EditDate.ToShortDateString();
                model.Version = entity.Version;
                model.UserSumCash = FormatSum(entity.UserSumCash);
                model.UserSumNotCash = FormatSum(entity.UserSumNotCash);

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
                model.DocumentNumber = entity.Number.ToString();

                MissionOrderTargetModel[] targets = entity.Targets.ToList().ConvertAll(x => new MissionOrderTargetModel
                    {
                        AirTicketTypeId = x.AirTicketType == null ? 0 : x.AirTicketType.Id,
                        AirTicketTypeName = x.AirTicketType == null? string.Empty:x.AirTicketType.Name,
                        AllDaysCount = x.DaysCount.ToString(),
                        City = x.City,
                        Country = x.Country.Name,
                        CountryId = x.Country.Id,
                        DailyAllowanceId = x.DailyAllowance == null? 0: x.DailyAllowance.Id,
                        DailyAllowanceName =  x.DailyAllowance == null? string.Empty : x.DailyAllowance.Name,
                        DateFrom = x.BeginDate.ToShortDateString(),
                        DateTo = x.EndDate.ToShortDateString(),
                        Organization = x.Organization,
                        ResidenceId = x.Residence == null? 0: x.Residence.Id,
                        ResidenceName = x.Residence == null? string.Empty: x.Residence.Name,
                        TargetDaysCount = x.RealDaysCount.ToString(),
                        TargetId = x.Id,
                        TrainTicketTypeId = x.TrainTicketType == null? 0:x.TrainTicketType.Id,
                        TrainTicketTypeName = x.TrainTicketType == null? string.Empty: x.TrainTicketType.Name,
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
            SetUserInfoModel(user, model);
            SetFlagsState(id,user,entity,model);
            LoadDictionaries(model);
            SetHiddenFields(model);
            return model;
        }
        
        public bool CheckOtherOrdersExists(MissionOrderEditModel model)
        {
            return MissionOrderDao.CheckOtherOrdersExists(model.Id, model.UserId, DateTime.Parse(model.BeginMissionDate),
                                                   DateTime.Parse(model.EndMissionDate));
        }
        public bool SaveMissionOrderEditModel(MissionOrderEditModel model, out string error)
        {
            error = string.Empty;
            User user = null;
            try
            {
                user = UserDao.Load(model.UserId);
                IUser current = AuthenticationService.CurrentUser;
                MissionOrder missionOrder = null;
                if (model.Id != 0)
                {
                    missionOrder = MissionOrderDao.Load(model.Id);
                }
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
                        if (current.UserRole == UserRole.OutsourcingManager)
                            missionOrder.DeleteAfterSendTo1C = true;
                        missionOrder.DeleteDate = DateTime.Now;
                        //missionOrder.CreateDate = DateTime.Now;
                        MissionOrderDao.SaveAndFlush(missionOrder);
                        if(missionOrder.Mission != null)
                        {
                            Mission mission = missionOrder.Mission;
                            if (mission.SendTo1C.HasValue)
                                mission.DeleteAfterSendTo1C = true;
                            mission.DeleteDate = DateTime.Now;
                            mission.CreateDate = DateTime.Now;
                            MissionDao.SaveAndFlush(mission);
                        }
                        else
                            Log.WarnFormat("No mission for mission order with id {0}",missionOrder.Id);
                        MissionReport report = MissionReportDao.GetReportForOrder(missionOrder.Id);
                        if(report != null)
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
                        if (missionOrder.Version != model.Version)
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
                SetUserInfoModel(user, model);
                LoadDictionaries(model);
                SetHiddenFields(model);
            }
        }
        protected void ChangeEntityProperties(IUser current, MissionOrder entity, MissionOrderEditModel model, User user)
        {
            bool isDirectorManager = IsDirectorManagerForEmployee(user, current);
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
                entity.IsResidencePaid = model.IsResidencePaid;
                entity.IsAirTicketsPaid = model.IsAirTicketsPaid;
                entity.IsTrainTicketsPaid = model.IsTrainTicketsPaid;
                model.IsChiefApproveNeed = IsMissionOrderLong(entity);//entity.NeedToAcceptByChief;
                SaveMissionTargets(entity, model);
            }
            if(model.IsSecritaryEditable)
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
                entity.ResidenceRequestNumber = string.IsNullOrEmpty(model.ResidenceRequestNumber)? null : model.ResidenceRequestNumber;
                entity.AirTicketsRequestNumber = string.IsNullOrEmpty(model.AirTicketsRequestNumber) ? null : model.AirTicketsRequestNumber;
                entity.TrainTicketsRequestNumber = string.IsNullOrEmpty(model.TrainTicketsRequestNumber) ? null : model.TrainTicketsRequestNumber;
                entity.AirTicketType = model.AirTicketType;
                entity.TrainTicketType = model.TrainTicketType;
            }
            
            if (current.UserRole == UserRole.Employee && current.Id == model.UserId
                && !entity.UserDateAccept.HasValue
                && model.IsUserApproved)
            {
                entity.UserDateAccept = DateTime.Now;
                entity.AcceptUser = UserDao.Load(current.Id);
                if (isDirectorManager)
                {
                    entity.NeedToAcceptByChiefAsManager = true;
                    SendEmailForMissionOrder(CurrentUser, entity, UserRole.Director);
                }
                else
                    SendEmailForMissionOrder(CurrentUser, entity, UserRole.Manager);
            }
            bool canEdit = false;
            if ((current.UserRole == UserRole.Manager && IsUserManagerForEmployee(user,current,out canEdit)) || CanUserApproveMissionOrderForEmployee(user, current, out canEdit))
            {
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
                            /*if (entity.Creator.RoleId == (int) UserRole.Manager && !entity.UserDateAccept.HasValue)
                                entity.UserDateAccept = DateTime.Now;*/
                            if (!entity.NeedToAcceptByChief)
                            {
                                CreateMission(entity);
                                SendEmailForMissionOrderConfirm(CurrentUser, entity);
                            }
                            else
                                SendEmailForMissionOrder(CurrentUser, entity, UserRole.Director);
                        }
                        else
                        {
                            model.IsManagerApproved = null;
                            if ((entity.Creator.RoleId & (int)UserRole.Manager) == 0)
                            {
                                entity.UserDateAccept = null;
                                SendEmailForMissionOrderReject(CurrentUser, entity);
                            }
                        }
                    }
                }
                /*if ((entity.Creator.RoleId == (int)UserRole.Manager) && !entity.UserDateAccept.HasValue)
                    entity.UserDateAccept = DateTime.Now;*/
            }
            if(current.UserRole == UserRole.Director)
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
                            SendEmailForMissionOrderConfirm(CurrentUser, entity);
                        }
                        else
                        {
                            entity.UserDateAccept = null;
                            model.IsManagerApproved = null;
                            SendEmailForMissionOrderReject(CurrentUser, entity);
                        }
                    }
                }
                if(entity.NeedToAcceptByChief)
                {
                    if (model.IsChiefApproved.HasValue)
                    {
                        if(model.IsChiefApproved.Value)
                        {
                            User currentUser = UserDao.Load(current.Id);
                            entity.ChiefDateAccept = DateTime.Now;
                            entity.AcceptChief = currentUser;
                            if(isDirectorManager && !entity.ManagerDateAccept.HasValue)
                            {
                                entity.ManagerDateAccept = DateTime.Now;
                                entity.AcceptManager = currentUser;
                            }
                            CreateMission(entity);
                            SendEmailForMissionOrderConfirm(CurrentUser, entity);
                        }
                        else
                        {
                            entity.UserDateAccept = null;
                            model.IsUserApproved = false;
                            entity.ManagerDateAccept = null;
                            model.IsManagerApproved = null;
                            SendEmailForMissionOrderReject(CurrentUser, entity);
                        }
                    }  
                }
            }
            
        }
        protected EmailDto SendEmailForMissionOrder(IUser current, MissionOrder entity, UserRole receiverRole)
        {
            //User currentUser = UserDao.Load(current.Id);
            //if (currentUser == null)
            //    throw new ArgumentException(string.Format("Не могу загрузить пользователя {0} из базы даннных", current.Id));
            string to = string.Empty;
            string to1 = string.Empty;
            IList<IdNameDto> managers;
            IList<IdNameDto> managers1;
            switch(receiverRole)
            {
                case UserRole.Manager:
                    User manager = UserDao.GetManagerForEmployee(entity.User.Login);
                    if(manager != null)
                    {
                        switch (manager.Level)
                        {
                            case 2:
                                if(!manager.IsMainManager)
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
                    IList<User> directors =  UserDao.GetUsersWithRole(UserRole.Director);
                    to = directors.Where(director => !string.IsNullOrEmpty(director.Email)).
                        Aggregate(string.Empty, (current1, director) => current1 + (director.Email + ";"));

                    break;
            }
            return SendEmailForMissionOrderNeedToApprove(to, entity);
        }
        protected bool IsMissionOrderLong(MissionOrder entity)
        {
            return (entity.EndDate.Subtract(entity.BeginDate).Days > 7) ||
                   (WorkingCalendarDao.GetNotWorkingCountBetweenDates(entity.BeginDate, entity.EndDate) > 0);
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
            if(MissionReportDao.IsReportForOrderExists(entity.Id))
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
            if(entity.SumDaily.HasValue && entity.SumDaily > 0)
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
                                      BeginDate = entity.BeginDate,
                                      Country = country,
                                      CreateDate = DateTime.Now,
                                      Creator = entity.Creator,
                                      //todo ???
                                      DaysCount = entity.EndDate.Subtract(entity.BeginDate).Days + 1,
                                      EndDate = entity.EndDate,
                                      FinancesSource = entity.User.Organization == null? string.Empty:entity.User.Organization.Name,
                                      Goal = entity.Goal.Name,
                                      ManagerDateAccept = DateTime.Now,
                                      Number = RequestNextNumberDao.GetNextNumberForType((int) RequestTypeEnum.Mission),
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
        protected void SaveMissionTargets(MissionOrder entity, MissionOrderEditModel model)
        {
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            JsonList list = jsonSerializer.Deserialize<JsonList>(model.Targets);
            List<MissionOrderTargetModel> targets = list.List.ToList();
            if (entity.Targets == null)
                entity.Targets = new List<MissionTarget>();
            List<MissionTarget> removed = entity.Targets.Where(x => !targets.Any(y => y.TargetId == x.Id)).ToList();
            foreach (MissionTarget target in removed)
                entity.Targets.Remove(target);
            foreach (MissionOrderTargetModel target in targets)
            {
                if(target.TargetId < 0)
                {
                    MissionTarget newTarget = new MissionTarget();
                    SetTargetProperties(target,newTarget,entity);
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
        protected void SetTargetProperties(MissionOrderTargetModel target,MissionTarget entity,MissionOrder order)
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
                if (currentUserRole == UserRole.Employee)
                    model.IsUserApprovedAvailable = true;
                else if (currentUserRole == UserRole.Manager)
                {
                    model.IsManagerApproveAvailable = true;
                    model.IsUserApproved = model.IsUserApprovedHidden = true;
                }
                return;
            }
            model.IsUserApproved = entity.UserDateAccept.HasValue;
            model.IsManagerApproved = entity.ManagerDateAccept.HasValue? true: new bool?();
            model.IsChiefApproved = entity.ChiefDateAccept.HasValue ? true : new bool?();
            switch (currentUserRole)
            {
                case UserRole.Employee:
                    if((entity.Creator.RoleId & (int)UserRole.Employee) > 0)
                    {
                        if(!entity.UserDateAccept.HasValue && !entity.DeleteDate.HasValue)
                        {
                            model.IsEditable = true;
                            model.IsUserApprovedAvailable = true;
                        }
                    }
                    break;
                case UserRole.Manager:
                    //User curUser = userDao.Load(AuthenticationService.CurrentUser.Id);
                    bool canEdit = false;
                    bool isUserManager = IsUserManagerForEmployee(user, AuthenticationService.CurrentUser, out canEdit) || CanUserApproveMissionOrderForEmployee(user, AuthenticationService.CurrentUser, out canEdit);
                    if (entity.Creator.RoleId == (int)UserRole.Manager)
                    {
                         if(!entity.ManagerDateAccept.HasValue && !entity.DeleteDate.HasValue && isUserManager && canEdit)
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
                case UserRole.OutsourcingManager:
                    if (entity.SendTo1C.HasValue && !entity.DeleteDate.HasValue)
                        model.IsDeleteAvailable = true;
                    break;
                case UserRole.Secretary:
                    if (!entity.SendTo1C.HasValue && !entity.DeleteDate.HasValue &&
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
            model.IsSaveAvailable = model.IsEditable || model.IsUserApprovedAvailable
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
        public bool CheckUserMoRights(User user, IUser current, int entityId,MissionOrder entity,bool isSave)
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
                    bool isManager = IsUserManagerForEmployee(user, current, out canEdit) || CanUserApproveMissionOrderForEmployee(user, current, out canEdit);
                    if (isManager)
                    {
                        if (isSave)
                            return canEdit;
                        return true;
                    }
                    Log.ErrorFormat("CheckUserMoRights user.Id {0} current.Id {1} ", user.Id, current.Id);
                    return false;
                case UserRole.OutsourcingManager:
                case UserRole.Secretary:
                    return true;
                case UserRole.Accountant:
                case UserRole.Findep:
                    if (isSave)
                        return false;
                    return true;
                case UserRole.Director:
                    if (entityId > 0)
                    {
                        if (entity.NeedToAcceptByChief)
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
        protected bool IsUserManagerForEmployee(User user, IUser current,out bool canEdit)
        {
            canEdit = false;
            User currentUser = UserDao.Load(current.Id);
            if (currentUser == null)
                throw new ArgumentException(string.Format("Не могу загрузить пользователя {0} из базы даннных", current.Id));
            User manager = UserDao.GetManagerForEmployee(user.Login);
            switch (currentUser.Level)
            {
                case 2:
                    if (manager != null)
                    {
                        if ((((manager.Level == 2) && !manager.IsMainManager) || (manager.Level == 3)) &&
                            manager.Department.Path.StartsWith(currentUser.Department.Path))
                        {
                            canEdit = true;
                            return true;
                        }
                    }
                    else
                        throw new ArgumentException(string.Format("Отсутствует руководитель для пользователя (Id {0})", user.Id));
                    break;
                case 3:
                    if (manager != null)
                    {
                        if ((((manager.Level == 3) && !manager.IsMainManager) || (manager.Level == 4) ||
                             (manager.Level == 5)) &&
                            manager.Department.Path.StartsWith(currentUser.Department.Path))
                        {
                            canEdit = true;
                            return true;
                        }
                    }
                    else
                        throw new ArgumentException(string.Format("Отсутствует руководитель для пользователя (Id {0})", user.Id));
                    break;
                case 4:
                    if (manager != null)
                    {
                        if ((((manager.Level == 4) && !manager.IsMainManager) || (manager.Level == 5)) &&
                            manager.Department.Path.StartsWith(currentUser.Department.Path))
                            return true;
                    }
                    else
                        throw new ArgumentException(string.Format("Отсутствует руководитель для пользователя (Id {0})", user.Id));
                    break;
                case 5:
                    if (manager != null)
                    {
                        if ((((manager.Level == 5) && !manager.IsMainManager) || (manager.Level == 6)) &&
                            manager.Department.Path.StartsWith(currentUser.Department.Path))
                        {
                            canEdit = true;
                            return true;
                        }
                    }
                    if (((user.RoleId & (int)UserRole.Employee) > 0) &&
                         user.Department.Path.StartsWith(currentUser.Department.Path))
                    {
                        canEdit = true;
                        return true;
                    }
                    break;
                case 6:
                    if (manager != null)
                    {
                        if ((((manager.Level == 6) && !manager.IsMainManager)) &&
                            manager.Department.Path.StartsWith(currentUser.Department.Path))
                            return true;
                    }
                    if (((user.RoleId & (int)UserRole.Employee) > 0) &&
                        user.Department.Path.StartsWith(currentUser.Department.Path))
                        return true;
                    break;
            }
            return false;
        }
        protected bool CanUserApproveMissionOrderForEmployee(User user, IUser current, out bool canEdit)
        {
            User currentUser = UserDao.Load(current.Id);
            if (currentUser == null)
                throw new ArgumentException(string.Format("Не могу загрузить пользователя {0} из базы даннных", current.Id));
            // Get the number of role records that allow the authenticated user to approve the current mission order
            int relevantRoleRecordsCount = currentUser.MissionOrderRoleRecords
                .Where<MissionOrderRoleRecord>(roleRecord =>
                    roleRecord.Role.Id == 1 &&
                    (roleRecord.TargetUser == user || roleRecord.TargetDepartment == user.Department))
                .ToList<MissionOrderRoleRecord>()
                .Count;
            // If any roles satisfying the conditions have been found
            canEdit = (relevantRoleRecordsCount > 0) ? true : false;
            return canEdit;
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
            SetUserInfoModel(user, model);
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
                model.DateCreated = missionOrder.EditDate.ToShortDateString();
                if (missionOrder.DeleteDate.HasValue)
                    model.IsDeleted = true;
            }
        }
        protected void LoadGraids(MissionOrderEditModel model, int gradeId,MissionOrder entity,DateTime gradeDate)
        {
            //DateTime gradeDate = DateTime.Parse(model.DateCreated);
            MissionGraid graid = MissionGraidDao.Load(gradeId);
            if(graid == null)
                throw new ValidationException(string.Format("Не найден грайд (id = {0}) в базе данных",gradeId));
            model.Grade = graid.Name;
            IList<GradeAmountDto> dailyList = MissionGraidDao.GetDailyAllowanceGradeAmountForGradeAndDate(gradeId, gradeDate);
            if (dailyList.Count != 4)
                throw new ValidationException(string.Format("Неверное число лимитов для суточных загружено из базы данных"));
            IList<GradeAmountDto> resList = MissionGraidDao.GetResidenceGradeAmountForGradeAndDate(gradeId, gradeDate);
            if (resList.Count != 2)
                throw new ValidationException(string.Format("Неверное число лимитов для авиа проживания загружено из базы данных"));
            IList<GradeAmountDto> airList = MissionGraidDao.GetAirTicketTypeGradeAmountForGradeAndDate(gradeId, gradeDate);
            if(airList.Count != 6)
                throw new ValidationException(string.Format("Неверное число лимитов для авиа билетов загружено из базы данных"));
            IList<GradeAmountDto> trainList = MissionGraidDao.GetTrainTicketTypeGradeAmountForGradeAndDate(gradeId, gradeDate);
            if (trainList.Count != 6)
                throw new ValidationException(string.Format("Неверное число лимитов для ж/д билетов загружено из базы данных"));
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            model.DailyAllowanceGrades = jsonSerializer.Serialize(dailyList.ToArray());
            model.ResidenceGrades = jsonSerializer.Serialize(resList.ToArray());
            model.AirTicketTypeGrades = jsonSerializer.Serialize(airList.ToArray());
            model.TrainTicketTypeGrades = jsonSerializer.Serialize(trainList.ToArray()); 
            if(entity != null && entity.Targets != null && entity.Targets.Count > 0)
            {
                bool needUpdate = false;
                decimal sumAir = 0;
                decimal sumTrain = 0;
                decimal sumDaily = 0;
                decimal sumRes = 0;
                foreach (MissionTarget target in entity.Targets)
                {
                    if(target.AirTicketType != null)
                    {
                        GradeAmountDto dto = airList.Where(x => x.Id == target.AirTicketType.Id).FirstOrDefault();
                        if(dto == null)
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
                        sumDaily += dto.Amount*target.DaysCount;
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
                if(entity.SumAir.HasValue)
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
                    Log.ErrorFormat("Сумма для  ж/д билетов по грейду не совпадает с суммой из базы данных для приказа {0}",entity.Id);
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
                if(entity.AllSum != allSum)
                {
                    Log.ErrorFormat("Общая сумма по грейду не совпадает с суммой из базы данных для приказа {0}", entity.Id);
                    entity.AllSum = allSum;
                    needUpdate = true;
                }
                if(needUpdate)
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
            var typeList = countries.ConvertAll(x => new IdNameDtoSort {Id = x.Id, Name = x.Name, SortOrder = x.Id == 1 ? 0 : 1}).
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
            if(order == null)
                throw new ArgumentException(string.Format("Приказ на командировку (id {0}) отсутствует в базе данных."));
            SetUserInfoModel(order.User,model);
            model.DocumentNumber = order.Number.ToString();
            model.DateCreated = order.EditDate.ToShortDateString();
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
            if(order.ManagerDateAccept.HasValue)
            {
                model.ManName = order.AcceptManager.Name;
                model.ManagerPosition = order.AcceptManager.Position == null? string.Empty:order.AcceptManager.Position.Name;
                model.ManagerDate = order.ManagerDateAccept.Value.ToShortDateString();
            }
            if(order.NeedToAcceptByChief && order.ChiefDateAccept.HasValue)
            {
                model.ChiefDate = order.ChiefDateAccept.Value.ToShortDateString();
                model.ChiefName = order.AcceptChief.Name;
                model.ChiefPosition = order.AcceptChief.Position == null? string.Empty:order.AcceptChief.Position.Name;
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
            SetUserInfoModel(order.User, model);
            model.DocumentNumber = order.Number.ToString();
            model.DateCreated = order.EditDate.ToShortDateString();
            return model;
        }

        public GradeListViewModel GetGradeListModel()
        {
            GradeListViewModel model = new GradeListViewModel();
            IList<MissionGraid> graids = MissionGraidDao.LoadAll().OrderBy(x => x.Id).ToList();
            IList<GradeAmountNameDto> dailyList = MissionGraidDao.GetDailyAllowanceGradeAmountForDate(DateTime.Today);
            model.Daily = GetTableDto(dailyList,graids);
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
            model.DocumentNumber = entity.Number.ToString();
            model.DateCreated = entity.CreateDate.ToShortDateString();
            model.Hotels = entity.Hotels;
            SetUserInfoModel(user, model);
            LoadDictionaries(model);
            SetFlagsState(id, user, entity, model);
            LoadCosts(model, entity);
            SetHiddenFields(model);
            return model;
        }
        protected void LoadTransactions(MissionReportEditModel model,MissionReportCost cost,CostDto dto)
        {
            List<TransactionDto> trans = new List<TransactionDto>();
            foreach (AccountingTransaction tran in cost.AccountingTransactions)
            {
                trans.Add(new TransactionDto
                              {
                                  TranId = tran.Id,
                                  Credit = tran.CreditAccount.Number,
                                  CreditId = tran.CreditAccount.Id,
                                  Debit = tran.DebitAccount.Number,
                                  DebitId = tran.DebitAccount.Id,
                                  Sum = tran.Sum,
                                  IsEditable = model.IsAccountantEditable,
                              });
            }
            dto.Trans = trans.ToArray();
            dto.IsTransactionAvailable = model.IsAccountantEditable 
                && ((cost.BookOfPurchaseSum.HasValue && cost.BookOfPurchaseSum.Value > 0) || !cost.IsCostFromPurchaseBook );
        }
        protected void LoadCosts(MissionReportEditModel model,MissionReport entity)
        {
            //List<MissionReportCostType> types = MissionReportCostTypeDao.LoadAll().ToList();
            List<CostDto> list = new List<CostDto>();
            decimal userSum=0;
            decimal pbSum=0;
            decimal accSum=0;
            decimal gradeSum = 0;
            if (entity.Costs != null)
            {
                foreach (MissionReportCost cost in entity.Costs.OrderBy(x => x.Type.SortOrder).ThenBy(x => x.Id))
                {
                    CostDto dto = new CostDto {
                        AccountantSum = cost.AccountantSum,
                        CostId = cost.Id,
                        CostTypeId = cost.Type.Id
                        //,Count = cost.Cnt
                        ,GradeSum = cost.Sum
                        ,Name = cost.Type.Name
                        ,PurchaseBookSum = cost.BookOfPurchaseSum
                        ,UserSum = cost.UserSum
                        ,SortOrder = cost.Type.SortOrder
                        ,IsEditable = model.IsEditable && !cost.IsCostFromOrder
                        ,IsDeleteAvailable = model.IsEditable && !cost.IsCostFromOrder
                    };
                    LoadTransactions(model,cost,dto);
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
            });
            int i = 1;
            foreach (CostDto dto in list.Where(x=> x.CostId != 0))
            {
                    dto.Number = i++;
            }
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            bool isTransactionHidden = (CurrentUser.UserRole == UserRole.Manager) || (CurrentUser.UserRole == UserRole.Employee);
            JsonCostsList res = new JsonCostsList { List = list.ToArray(),IsTransactionsHidden = isTransactionHidden};
            model.Costs = jsonSerializer.Serialize(res);
        }
        // hardcode in javascript
        protected static int GetSortOrder(MissionReportCost cost)
        {
            return cost.Type.SortOrder;
        }
        protected void SetUserInfoModel(User user,MissionReportEditModel model)
        {
            SetUserInfoModel(user,(UserInfoModel)model);
            model.UserFio ="Сотрудник " + user.FullName;
        }
        protected void SetFlagsState(int id, User user, MissionReport entity, MissionReportEditModel model)
        {
            SetFlagsState(model, false);
            UserRole currentUserRole = AuthenticationService.CurrentUser.UserRole;
            model.IsUserApproved = entity.UserDateAccept.HasValue;
            model.IsManagerApproved = entity.ManagerDateAccept.HasValue;
            model.IsAccountantApproved = entity.AccountantDateAccept.HasValue;
            model.IsDeleted = entity.DeleteDate.HasValue;
            if (entity.AcceptAccountant != null && entity.AccountantDateAccept.HasValue)
                model.AccountantFio = entity.AcceptAccountant.FullName + ", " + entity.AcceptAccountant.Email;
            
            switch (currentUserRole)
            {
                case UserRole.Employee:
                    //if ((entity.Creator.RoleId & (int)UserRole.Employee) > 0)
                    //{
                        if (!entity.UserDateAccept.HasValue && !entity.DeleteDate.HasValue)
                        {
                            model.IsEditable = true;
                            model.IsUserApprovedAvailable = true;
                        }
                    //}
                    break;
                case UserRole.Manager:
                    //User curUser = userDao.Load(AuthenticationService.CurrentUser.Id);
                    bool canEdit;
                    bool isUserManager = IsUserManagerForEmployee(user, AuthenticationService.CurrentUser, out canEdit) || CanUserApproveMissionOrderForEmployee(user, AuthenticationService.CurrentUser, out canEdit);
                    //if (entity.Creator.RoleId == (int)UserRole.Manager)
                    //{
                    //    if (!entity.ManagerDateAccept.HasValue && !entity.DeleteDate.HasValue && isUserManager && canEdit)
                    //    {
                    //        model.IsEditable = true;
                    //        model.IsManagerApprovedAvailable = true;
                    //        if (entity.UserDateAccept.HasValue)
                    //            model.IsUserApproved = true;
                    //    }
                    //}
                    //else
                    //{
                    if (!entity.ManagerDateAccept.HasValue && !entity.DeleteDate.HasValue
                        && entity.UserDateAccept.HasValue && isUserManager && canEdit)
                    {
                        model.IsManagerApproveAvailable = true;
                        model.IsManagerRejectAvailable = true;
                    }

                    //}
                    break;
                case UserRole.Accountant:
                    if (entity.ManagerDateAccept.HasValue && !entity.DeleteDate.HasValue 
                        && !entity.SendTo1C.HasValue)
                    {
                        if (!entity.AccountantDateAccept.HasValue)
                        {
                            model.IsAccountantEditable = true;
                            model.IsAccountantApproveAvailable = true;
                            if (model.IsAccountantApproveAvailable &&
                                entity.Costs.Any(x => x.IsCostFromPurchaseBook &&
                                (!x.BookOfPurchaseSum.HasValue || x.BookOfPurchaseSum == 0)))
                                model.IsAccountantApproveAvailable = false;
                            
                        }
                        else
                        {
                            model.IsAccountantRejectAvailable = true;
                        }
                    }
                    break;
                    //case UserRole.OutsourcingManager:
                    //    if (entity.SendTo1C.HasValue && !entity.DeleteDate.HasValue)
                    //        model.IsDeleteAvailable = true;
                    //    break;
                    //case UserRole.Secretary:
                    //    if (!entity.SendTo1C.HasValue && !entity.DeleteDate.HasValue &&
                    //        ((entity.NeedToAcceptByChief && entity.ChiefDateAccept.HasValue) ||
                    //         (!entity.NeedToAcceptByChief && entity.ManagerDateAccept.HasValue)) &&
                    //        (entity.IsAirTicketsPaid || entity.IsResidencePaid || entity.IsTrainTicketsPaid))
                    //        model.IsSecritaryEditable = true;
                    //    break;
                    //case UserRole.Director:
                    //    if (IsDirectorManagerForEmployee(user, AuthenticationService.CurrentUser))
                    //    {
                    //        if (!entity.ManagerDateAccept.HasValue &&
                    //            !entity.DeleteDate.HasValue && entity.UserDateAccept.HasValue)
                    //            model.IsManagerApprovedAvailable = true;
                    //    }
                    //    if (entity.NeedToAcceptByChief && !entity.ChiefDateAccept.HasValue
                    //        && !entity.DeleteDate.HasValue && entity.ManagerDateAccept.HasValue
                    //        && entity.UserDateAccept.HasValue)
                    //        model.IsChiefApproveAvailable = true;
                    //    break;
            }
            model.IsSaveAvailable = model.IsEditable || model.IsUserApprovedAvailable
                || model.IsManagerApproveAvailable 
                || model.IsAccountantEditable || model.IsAccountantApproveAvailable; //|| model.IsChiefApproveAvailable || model.IsSecritaryEditable;

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
                    bool isManager = IsUserManagerForEmployee(user, current, out canEdit) || CanUserApproveMissionOrderForEmployee(user, current, out canEdit);
                    if (isManager)
                    {
                        if (isSave)
                            return canEdit;
                        return true;
                    }
                    Log.ErrorFormat("CheckUserMrRights user.Id {0} current.Id {1} ", user.Id, current.Id);
                    return false;
                case UserRole.OutsourcingManager:
                //case UserRole.Secretary:
                //    return true;
                case UserRole.Accountant:
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
            try
            {
                user = UserDao.Load(model.UserId);
                IUser current = AuthenticationService.CurrentUser;
                //if (model.Id != 0)
                //{
                MissionReport missionReport = MissionReportDao.Load(model.Id);

                model.DocumentTitle = string.Format("Авансовый отчет № АО{0} о командировке к Приказу № {0} на командировку", missionReport.Number);
                model.DocumentNumber =  missionReport.Number.ToString();
                model.DateCreated = missionReport.CreateDate.ToShortDateString();
                //}
                if (!CheckUserMrRights(user, current, model.Id, missionReport, true))
                {
                    error = "Редактирование отчета запрещено";
                    return false;
                }
               
                //if (model.Id == 0)
                //{
                //    missionReport = new MissionOrder
                //    {
                //        CreateDate = DateTime.Now,
                //        Creator = UserDao.Load(current.Id),
                //        Number = RequestNextNumberDao.GetNextNumberForType((int)RequestTypeEnum.MissionOrder),
                //        User = user,
                //        EditDate = DateTime.Now,
                //    };
                //    ChangeEntityProperties(current, missionReport, model, user);
                //    MissionOrderDao.SaveAndFlush(missionReport);
                //    model.Id = missionReport.Id;
                //}
                //else
                //{
                    if (missionReport.Version != model.Version)
                    {
                        error = "Отчет был изменен другим пользователем.";
                        model.ReloadPage = true;
                        return false;
                    }
                    //if (model.IsDelete)
                    //{
                    //    if (current.UserRole == UserRole.OutsourcingManager)
                    //        missionReport.DeleteAfterSendTo1C = true;
                    //    missionReport.DeleteDate = DateTime.Now;
                    //    //missionOrder.CreateDate = DateTime.Now;
                    //    MissionOrderDao.SaveAndFlush(missionReport);
                    //    /*SendEmailForUserRequest(missionOrder.User, current, missionOrder.Creator, true, missionOrder.Id,
                    //        missionOrder.Number, RequestTypeEnum.ChildVacation, false);*/
                    //    model.IsDelete = false;
                    //}
                    //else
                    //{
                        ChangeEntityProperties(current, missionReport, model, user);
                        //List<string> cityList = missionOrder.Targets.Select(x => x.City).ToList();
                        //string country = GetStringForList(cityList);
                        //List<string> orgList = missionOrder.Targets.Select(x => x.Organization).ToList();
                        //string org = GetStringForList(orgList);
                        MissionReportDao.SaveAndFlush(missionReport);
                        if (missionReport.Version != model.Version)
                        {
                            missionReport.EditDate = DateTime.Now;
                            MissionReportDao.SaveAndFlush(missionReport);
                        }
                    //}
                    //if (missionReport.DeleteDate.HasValue)
                    //    model.IsDeleted = true;
                //}
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
                SetHiddenFields(model);
            }
        }
        protected void ChangeEntityProperties(IUser current, MissionReport entity, MissionReportEditModel model, User user)
        {
            //bool isDirectorManager = IsDirectorManagerForEmployee(user, current);
            if (model.IsEditable)
            {
                entity.Hotels = model.Hotels;
                SaveMissionCosts(entity, model);
            }
            if (model.IsAccountantEditable)
            {
                SaveMissionCostsTransactions(entity, model);
            }
            if (current.UserRole == UserRole.Employee && current.Id == model.UserId
                && !entity.UserDateAccept.HasValue
                && model.IsUserApproved)
            {
                entity.UserDateAccept = DateTime.Now;
                entity.AcceptUser = UserDao.Load(current.Id);
                SetMissionCostsEditable(model,false);

                //SendEmailForMissionReport(CurrentUser, entity, UserRole.Manager);
                //if (isDirectorManager)
                //{
                //    entity.NeedToAcceptByChiefAsManager = true;
                //    SendEmailForMissionOrder(CurrentUser, entity, UserRole.Director);
                //}
                //else
                //    SendEmailForMissionOrder(CurrentUser, entity, UserRole.Manager);
            }
            bool canEdit;
            if ((current.UserRole == UserRole.Manager && IsUserManagerForEmployee(user, current, out canEdit) || CanUserApproveMissionOrderForEmployee(user, current, out canEdit))
                && !entity.ManagerDateAccept.HasValue
                && entity.UserDateAccept.HasValue)
            {
                //if (entity.Creator.RoleId == (int)UserRole.Manager && !entity.UserDateAccept.HasValue)
                //{
                //    entity.UserDateAccept = DateTime.Now;
                //    entity.AcceptUser = UserDao.Load(current.Id);
                //}
                //if (!entity.ManagerDateAccept.HasValue)
                //{
                //    if (model.IsManagerApproved)
                //    {
                        //if (model.IsManagerApproved.Value)
                        //{
                            if(model.IsManagerReject)
                            {
                                entity.UserDateAccept = null;
                                entity.AcceptUser = null;
                                model.IsManagerApproved = false;
                                entity.AcceptManager = UserDao.Load(current.Id);
                            }
                            else if (model.IsManagerApproved)
                            {
                                entity.ManagerDateAccept = DateTime.Now;
                                entity.AcceptManager = UserDao.Load(current.Id);
                            }

                /*if (entity.Creator.RoleId == (int) UserRole.Manager && !entity.UserDateAccept.HasValue)
                                entity.UserDateAccept = DateTime.Now;*/
                            //SendEmailForMissionReportConfirm(CurrentUser, entity);
                            //if (!entity.NeedToAcceptByChief)
                            //{
                            //    CreateMission(entity);
                            //    SendEmailForMissionOrderConfirm(CurrentUser, entity);
                            //}
                            //else
                            //    SendEmailForMissionOrder(CurrentUser, entity, UserRole.Director);
                        //}
                        //else
                        //{
                        //    model.IsManagerApproved = null;
                        //    if ((entity.Creator.RoleId & (int)UserRole.Manager) == 0)
                        //    {
                        //        entity.UserDateAccept = null;
                        //        SendEmailForMissionOrderReject(CurrentUser, entity);
                        //    }
                        //}
                //    }
                //}
                /*if ((entity.Creator.RoleId == (int)UserRole.Manager) && !entity.UserDateAccept.HasValue)
                    entity.UserDateAccept = DateTime.Now;*/
            }
               if (current.UserRole == UserRole.Accountant && entity.ManagerDateAccept.HasValue)
                {
                   if(!entity.AccountantDateAccept.HasValue && model.IsAccountantApproved)
                   {
                       entity.AccountantDateAccept = DateTime.Now;
                       entity.AcceptAccountant = UserDao.Load(current.Id);
                       SetMissionTransactionEditable(model, false);
                   }
                   else if(entity.AccountantDateAccept.HasValue && model.IsAccountantReject)
                   {
                       entity.AccountantDateAccept = null;
                       entity.AcceptAccountant = UserDao.Load(current.Id);
                       SetMissionTransactionEditable(model, true);
                       model.IsAccountantReject = false;
                   }
                }
              //if(current.UserRole == UserRole.Director)
            //{
            //    if (isDirectorManager && !entity.ManagerDateAccept.HasValue)
            //    {
            //        if (model.IsManagerApproved.HasValue)
            //        {
            //            if (model.IsManagerApproved.Value)
            //            {
            //                User currentUser = UserDao.Load(current.Id);
            //                entity.ManagerDateAccept = DateTime.Now;
            //                entity.AcceptManager = currentUser;
            //                if (entity.NeedToAcceptByChief)
            //                {
            //                    entity.ChiefDateAccept = DateTime.Now;
            //                    entity.AcceptChief = currentUser;
            //                }
            //                CreateMission(entity);
            //                SendEmailForMissionOrderConfirm(CurrentUser, entity);
            //            }
            //            else
            //            {
            //                entity.UserDateAccept = null;
            //                model.IsManagerApproved = null;
            //                SendEmailForMissionOrderReject(CurrentUser, entity);
            //            }
            //        }
            //    }
            //    if(entity.NeedToAcceptByChief)
            //    {
            //        if (model.IsChiefApproved.HasValue)
            //        {
            //            if(model.IsChiefApproved.Value)
            //            {
            //                User currentUser = UserDao.Load(current.Id);
            //                entity.ChiefDateAccept = DateTime.Now;
            //                entity.AcceptChief = currentUser;
            //                if(isDirectorManager && !entity.ManagerDateAccept.HasValue)
            //                {
            //                    entity.ManagerDateAccept = DateTime.Now;
            //                    entity.AcceptManager = currentUser;
            //                }
            //                CreateMission(entity);
            //                SendEmailForMissionOrderConfirm(CurrentUser, entity);
            //            }
            //            else
            //            {
            //                entity.UserDateAccept = null;
            //                model.IsUserApproved = false;
            //                entity.ManagerDateAccept = null;
            //                model.IsManagerApproved = null;
            //                SendEmailForMissionOrderReject(CurrentUser, entity);
            //            }
            //        }  
            //    }
            //}
            
        }
        protected void SetMissionCostsEditable(MissionReportEditModel model,bool isEditable)
        {
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            JsonCostsList list = jsonSerializer.Deserialize<JsonCostsList>(model.Costs);
            List<CostDto> costDtos = list.List.Where(x => x.CostId != 0).ToList();
            foreach (CostDto dto in costDtos)
            {
                dto.IsEditable = isEditable;
                dto.IsDeleteAvailable = isEditable;
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
                if(dto.Trans != null && dto.Trans.Count() > 0)
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
                        throw new ArgumentException(string.Format("Не найден расход (id {0}) в базе данных",dto.CostId));
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
                if(dto.Trans == null || dto.Trans.Count() == 0)
                {
                    if(cost.AccountingTransactions != null && cost.AccountingTransactions.Count > 0)
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
                        if(tran.TranId < 0)
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
                            if(existing == null)
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
                isAddAvailable = !entity.UserDateAccept.HasValue;
                isDeleteAvailable = !entity.UserDateAccept.HasValue;
                //if (isDeleteAvailable && list.Count <= 4)
                //{
                //    if ((entity.UserDateAccept.HasValue && CurrentUser.UserRole == UserRole.Employee) ||
                //       (entity.ManagerDateAccept.HasValue && CurrentUser.UserRole == UserRole.Manager) ||
                //       (entity.PersonnelManagerDateAccept.HasValue && CurrentUser.UserRole == UserRole.PersonnelManager)
                //      )
                //        isDeleteAvailable = false;
                //}
            }
            RequestAttachmentsModel model = new RequestAttachmentsModel
            {
                AttachmentRequestId = id,
                AttachmentRequestTypeId = (int)typeId,
                IsAddAvailable = isAddAvailable && (CurrentUser.UserRole == UserRole.Employee),
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
                throw new ArgumentException(string.Format("Авансовый отчет (id {0}) отсутствует в базе данных.",id));
            SetUserInfoModel(report.User, model);
            model.OrderNumber = report.MissionOrder.Number.ToString();
            model.DocumentNumber = "АО" + report.Number;
            model.DateCreated = report.CreateDate.ToShortDateString();
            model.Hotels = report.Hotels;
            model.UserFio = report.User.FullName;
            model.UserPosition = report.User.Position.Name;
            if (report.UserDateAccept.HasValue)
                model.UserAcceptDate = report.UserDateAccept.Value.ToShortDateString();
            if(report.ManagerDateAccept.HasValue)
            {
                model.ManagerFio = report.AcceptManager.FullName;
                model.ManagerAcceptDate = report.ManagerDateAccept.Value.ToShortDateString();
                model.ManagerPosition = report.AcceptManager.Position == null? string.Empty:report.AcceptManager.Position.Name;
            }
            if (report.AccountantDateAccept.HasValue)
            {
                model.AccountantFio = report.AcceptAccountant.FullName;
                model.AccountantAcceptDate = report.AccountantDateAccept.Value.ToShortDateString();
                model.AccountantPosition = report.AcceptAccountant.Position == null? string.Empty: report.AcceptAccountant.Position.Name;
            }
            List<PrintCostModel> costs = new List<PrintCostModel>();
            int currentNumber = 1;
            //int currentCostType = 0;
            foreach(MissionReportCost cost in report.Costs.OrderBy(x => x.Type.Id))
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

        #endregion
        
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
        public void SetMissionUserDeptsListModel(MissionUserDeptsListModel model,bool showDepts,bool hasError)
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
        public void SetDocumentsToModel(MissionUserDeptsListModel model, User user,bool showDepts)
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
                model.SortDescending,showDepts);
            model.IsPrintAvailable = model.Documents.Count > 0;
        }
        public PrintMissionUserDeptsListModel PrintMissionUserDeptsListModel(int departmentId, int statusId, DateTime? beginDate,
            DateTime? endDate, string userName, int sortBy, bool? sortDescending,bool showDepts)
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
                                                                             sortDescending,showDepts)
                       };
        }
        #region Mission PurchaseBook Documents
        public MissionPurchaseBookDocListModel GetMissionPurchaseBookDocsListModel()
        {
            //User user = UserDao.Load(AuthenticationService.CurrentUser.Id);
            MissionPurchaseBookDocListModel model = new MissionPurchaseBookDocListModel();
            SetInitialDates(model);
            model.IsAddAvailable = CurrentUser.UserRole == UserRole.Accountant;
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
            if(current.UserRole != UserRole.Accountant && current.UserRole != UserRole.OutsourcingManager
                && current.UserRole != UserRole.Findep)
                throw new ArgumentException("Доступ запрещен.");
            EditMissionPbDocumentModel model = new EditMissionPbDocumentModel {UserId = current.Id, Id = id};
            if(id != 0)
            {
                MissionPurchaseBookDocument entity = MissionPurchaseBookDocumentDao.Load(id);
                if(entity == null)
                    throw new ValidationException(string.Format("Не найден документ книги покупок (id {0}) в базе данных", id));
                model.Number = entity.Number;
                model.DocumentDate = entity.DocumentDate.HasValue?entity.DocumentDate.Value.ToShortDateString():string.Empty;
                model.CfNumber = entity.CfNumber;
                model.CfDate = entity.CfDate.HasValue? entity.CfDate.Value.ToShortDateString():string.Empty;
                model.ContractorId = entity.Contractor.Id;
                model.Version = entity.Version;
            }
            model.IsEditable = current.UserRole == UserRole.Accountant;
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
                if (current.UserRole != UserRole.Accountant)
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
                entity.Number = string.IsNullOrEmpty(model.Number)?null: model.Number;
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
            model.Contractors = list.ConvertAll(x => new IdNameDto {Id = x.Id, Name = x.Name});
            if (model.ContractorId != 0)
                model.ContractorAccount = list.Where(x => x.Id == model.ContractorId).First().Account;
            else
                model.ContractorAccount = list.First().Account;
            model.RecordsModel = GetRecordsModel(model.Id,model.IsEditable);
        }
        public EditMissionPbRecordsModel GetPbRecordsModel(int documentId)
        {
            return GetRecordsModel(documentId, AuthenticationService.CurrentUser.UserRole == UserRole.Accountant);
        }
        protected EditMissionPbRecordsModel GetRecordsModel(int documentId,bool isEditable)
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
                                                        SumNds = records.Sum(x => x.SumNds)
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
                if(entity == null)
                    throw new ValidationException(string.Format("Не найден контрагент (id {0}) в базе данных",id));
                return new ContractorAccountDto { Error = string.Empty, Account = entity.Account };
            }
            catch (Exception ex)
            {
                Log.Error("Exception on GetContractorAccount:", ex);
                return new ContractorAccountDto
                {
                    Error = string.Format("Ошибка: {0}",ex.GetBaseException().Message)
                };
            }
        }

        public void SetMissionReportEditRecordModel(MissionPbEditRecordModel model)
        {
            if(model.RecordId != 0)
            {
               MissionPurchaseBookRecord entity = MissionPurchaseBookRecordDao.Load(model.RecordId);
                if(entity == null)
                    throw new ArgumentException(string.Format("Не могу загрузить запись книги покупок (id {0}) из базы данных",model.RecordId));
                model.AllSum = entity.AllSum;
                model.CostTypeId = entity.MissionReportCostType.Id;
                model.ReportId = entity.MissionReportCost.Report.Id;
                model.RequestNumber = entity.RequestNumber;
                model.Sum = entity.Sum;
                model.SumNds = entity.SumNds;
                model.RecordUserId = entity.User.Id;
            }
            LoadDictionaries(model);
        }
        protected void LoadDictionaries(MissionPbEditRecordModel model)
        {
            List<IdNameDto> users = UserDao.GetUsersWithPurchaseBookReportCosts().ToList();
            if(users.Count == 0)
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
            List<IdNameDto> reports = MissionReportDao.GetReportsWithPurchaseBookReportCosts(selectedUserId).ToList();
            if(reports.Count == 0)
            {
                model.Reports = new List<IdNameDto>();
                model.CostTypes = new List<IdNameDto>();
                return;
            }
            model.Reports = reports;
            int selectedReportId = reports[0].Id;
            if (model.RecordId != 0)
            {
                IdNameDto selectedReport = reports.Where(x => x.Id == model.ReportId).FirstOrDefault();
                if (selectedReport != null)
                    selectedReportId = selectedReport.Id;
            }
            else
                model.ReportId = selectedReportId;
            List<IdNameDto> costTypes = MissionReportCostDao.GetCostTypesWithPurchaseBookReportCosts(selectedReportId).ToList();
            if(costTypes.Count == 0)
            {
                model.CostTypes = new List<IdNameDto>();
                return; 
            }
            model.CostTypes = costTypes;
            if (model.CostTypeId == 0)
                model.CostTypeId = costTypes[0].Id;
            //IdNameDto selectedType = costTypes.Where(x => x.Id == model.CostTypeId).FirstOrDefault();
            if(model.RecordId == 0)
            {
                model.RequestNumber = GetRequestNumber(selectedReportId, model.CostTypeId);
            }
            return;
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
        public PbRecordCostTypesDto GetCostTypes(int reportId,bool isNew)
        {
            PbRecordCostTypesDto model = new PbRecordCostTypesDto{Error = string.Empty};
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
            List<IdNameDto> reports = MissionReportDao.GetReportsWithPurchaseBookReportCosts(userId).ToList();
            model.Children = reports;
            return model;
        }

        public int SavePbRecord(SavePbRecordModel model)
        {
            MissionPurchaseBookDocument doc = MissionPurchaseBookDocumentDao.Load(model.DocumentId);
            if(doc == null)
                throw new ArgumentException(string.Format("Не могу загрузить документ книги покупок (id {0}) из базы данных.",model.DocumentId));
            MissionReport report = MissionReportDao.Load(model.ReportId);
            if(report == null)
                throw new ArgumentException(string.Format("Не могу загрузить авансовый отчет (id {0}) из базы данных.",model.ReportId));
            if(report.AccountantDateAccept.HasValue)
                throw new ArgumentException(string.Format("Изменение записей книги покупок для авансового отчета (id {0}) запрещено - отчет уже проверен бухгалтером.",model.ReportId));
            MissionReportCost cost = report.Costs.Where(x => x.IsCostFromPurchaseBook && x.Type.Id == model.CostTypeId).FirstOrDefault();
            if(cost == null)
                throw new ArgumentException(string.Format("Не найден расход указанного типа (id {1}) в авансовом отчете (id {0}).",model.ReportId,model.CostTypeId));

            MissionPurchaseBookRecord rec; 
            if(model.RecordId == 0)
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
                if(rec == null)
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
            cost.BookOfPurchaseSum = sum == 0 ? null : sum;
            report.PurchaseBookAllSum = report.Costs.Sum(x => x.BookOfPurchaseSum).Value;
            MissionReportDao.SaveAndFlush(report);
            return documentVersion;
        }
        #endregion
    }
}