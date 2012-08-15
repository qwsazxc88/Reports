using System;
using System.Collections.Generic;
using System.Linq;
using Reports.Core;
using Reports.Core.Dao;
using Reports.Core.Dao.Impl;
using Reports.Core.Domain;
using Reports.Core.Dto;
using Reports.Core.Enum;
using Reports.Presenters.Services;
using Reports.Presenters.UI.ViewModel;

namespace Reports.Presenters.UI.Bl.Impl
{
    public class RequestBl : BaseBl, IRequestBl
    {
        protected string SelectAll = "Все";

        public const int VacationFirstTimesheetStatisId = 8;
        public const int VacationLastTimesheetStatisId = 12;
        public const int AbsenceFirstTimesheetStatisId = 15;
        public const int AbsenceLastTimesheetStatisId = 18;

        #region DAOs
        protected IDepartmentDao departmentDao;
        protected IVacationTypeDao vacationTypeDao;
        protected IRequestStatusDao requestStatusDao;
        protected IPositionDao positionDao;
        protected IVacationDao vacationDao;
        protected IUserToDepartmentDao userToDepartmentDao;
        protected ITimesheetStatusDao timesheetStatusDao;
        protected IVacationCommentDao vacationCommentDao;
        protected IRequestNextNumberDao requestNextNumberDao;

        protected IAbsenceTypeDao absenceTypeDao;
        protected IAbsenceDao absenceDao;
        protected IAbsenceCommentDao absenceCommentDao;

        protected ISicklistTypeDao sicklistTypeDao;
        protected ISicklistPaymentRestrictTypeDao sicklistPaymentRestrictTypeDao;
        protected ISicklistPaymentPercentDao sicklistPaymentPercentDao;
        protected ISicklistDao sicklistDao;
        protected IRequestAttachmentDao requestAttachmentDao;
        protected ISicklistCommentDao sicklistCommentDao;

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

        public IDepartmentDao DepartmentDao
        {
            get { return Validate.Dependency(departmentDao); }
            set { departmentDao = value; }
        }
        public IVacationTypeDao VacationTypeDao
        {
            get { return Validate.Dependency(vacationTypeDao); }
            set { vacationTypeDao = value; }
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
        public IUserToDepartmentDao UserToDepartmentDao
        {
            get { return Validate.Dependency(userToDepartmentDao); }
            set { userToDepartmentDao = value; }
        }
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
        public IRequestNextNumberDao RequestNextNumberDao
        {
            get { return Validate.Dependency(requestNextNumberDao); }
            set { requestNextNumberDao = value; }
        }
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
        #endregion
        #region Create Request
        public CreateRequestModel GetCreateRequestModel(int? userId)
        {
            if(userId == null)
                userId = AuthenticationService.CurrentUser.Id;

            User user = UserDao.Load(userId.Value);
            UserRole role = (UserRole)user.Role.Id;
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
                    model.Users = UserDao.GetUsersForManager(user.Id, role);
                    break;


            }
            return model;
        }
        protected IList<IdNameDto> GetRequestTypes()
        {
            return new List<IdNameDto>
                       {
                           new IdNameDto((int) RequestTypeEnum.Vacation, "Заявка на отпуск"),
                           new IdNameDto((int) RequestTypeEnum.Absence, "Заявка на неявку"),
                           new IdNameDto((int) RequestTypeEnum.Sicklist, "Заявка на больничный"),
                           new IdNameDto((int) RequestTypeEnum.HolidayWork, "Заявка на оплату праздничных и выходных дней"),
                           new IdNameDto((int) RequestTypeEnum.Mission, "Заявка на командировку"),
                           new IdNameDto((int) RequestTypeEnum.Dismissal, "Заявка на увольнение"),
                           new IdNameDto((int) RequestTypeEnum.TimesheetCorrection, "Заявка на корректировку табеля")
                       };
        }
        #endregion
        #region Timesheet Correction
        public TimesheetCorrectionListModel GetTimesheetCorrectionListModel()
        {
            User user = UserDao.Load(AuthenticationService.CurrentUser.Id);
            TimesheetCorrectionListModel model = new TimesheetCorrectionListModel
            {
                UserId = AuthenticationService.CurrentUser.Id,
            };
            SetDictionariesToModel(model, user);
            return model;
        }
        protected void SetDictionariesToModel(TimesheetCorrectionListModel model, User user)
        {
            model.Departments = GetDepartments(user);
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
        public void SetTimesheetCorrectionListModel(TimesheetCorrectionListModel model)
        {
            User user = UserDao.Load(model.UserId);
            SetDictionariesToModel(model, user);
            SetDocumentsToModel(model, user);
        }
        public void SetDocumentsToModel(TimesheetCorrectionListModel model, User user)
        {

            UserRole role = (UserRole)user.Role.Id;
            model.Documents = TimesheetCorrectionDao.GetDocuments(
                role,
                model.DepartmentId,
                model.PositionId,
                model.TypeId,
                model.StatusId);
            //model.BeginDate,
            //model.EndDate);
        }
        public TimesheetCorrectionEditModel GetTimesheetCorrectionEditModel(int id, int userId)
        {
            TimesheetCorrectionEditModel model = new TimesheetCorrectionEditModel { Id = id, UserId = userId };
            User user = UserDao.Load(userId);
            IUser current = AuthenticationService.CurrentUser;
            if (!CheckUserRights(user, current))
                throw new ArgumentException("Доступ запрещен.");
            SetUserInfoModel(user, model);
            TimesheetCorrection timesheetCorrection = null;
            if (id == 0)
            {
                model.CreatorLogin = current.Login;
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
                model.CreatorLogin = timesheetCorrection.Creator.Login;
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
            List<IdNameDto> dtos = TimesheetStatusDao.LoadAllSorted().ToList().ConvertAll(x => new IdNameDto(x.Id, x.Name));
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
                switch (currentUserRole)
                {
                    case UserRole.Employee:
                        model.IsApprovedByUserEnable = true;
                        break;
                    case UserRole.Manager:
                        model.IsApprovedByManagerEnable = true;
                        model.IsStatusEditable = true;
                        break;
                    case UserRole.PersonnelManager:
                        model.IsApprovedByPersonnelManagerEnable = true;
                        model.IsStatusEditable = true;
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
                            model.IsStatusEditable = true;
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
                            model.IsStatusEditable = true;
                            //model.IsPersonnelFieldsEditable = true;
                        }
                    }
                    else if (!entity.SendTo1C.HasValue && !entity.DeleteDate.HasValue)
                        model.IsDeleteAvailable = true;
                    break;
            }
            model.IsSaveAvailable = model.IsTypeEditable || model.IsStatusEditable
                                    || model.IsApprovedByManagerEnable || model.IsApprovedByUserEnable ||
                                    model.IsApprovedByPersonnelManagerEnable;
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
        }
        public bool SaveTimesheetCorrectionEditModel(TimesheetCorrectionEditModel model, out string error)
        {
            error = string.Empty;
            User user = null;
            try
            {
                user = UserDao.Load(model.UserId);
                IUser current = AuthenticationService.CurrentUser;
                if (!CheckUserRights(user, current))
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
                        timesheetCorrection.DeleteDate = DateTime.Now;
                        TimesheetCorrectionDao.SaveAndFlush(timesheetCorrection);
                        model.IsDelete = false;
                    }
                    else
                    {
                        ChangeEntityProperties(current, timesheetCorrection, model, user);
                        TimesheetCorrectionDao.SaveAndFlush(timesheetCorrection);
                    }
                    if (timesheetCorrection.DeleteDate.HasValue)
                        model.IsDeleted = true;
                }
                model.DocumentNumber = timesheetCorrection.Number.ToString();
                model.Version = timesheetCorrection.Version;
                //model.DaysCount = dismissal.DaysCount;
                model.CreatorLogin = timesheetCorrection.Creator.Login;
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
                && model.IsApprovedByUser)
                entity.UserDateAccept = DateTime.Now;
            if (current.UserRole == UserRole.Manager && user.Manager != null
                && current.Id == user.Manager.Id
                && !entity.ManagerDateAccept.HasValue)
            {
                entity.TimesheetStatus = TimesheetStatusDao.Load(model.StatusId);
                if (model.IsApprovedByManager)
                    entity.ManagerDateAccept = DateTime.Now;
            }
            if (current.UserRole == UserRole.PersonnelManager && user.PersonnelManager != null
                && current.Id == user.PersonnelManager.Id
                && !entity.PersonnelManagerDateAccept.HasValue)
            {
                entity.TimesheetStatus = TimesheetStatusDao.Load(model.StatusId);
                //entity.Compensation = string.IsNullOrEmpty(model.Compensation) ? new decimal?() : (decimal)((int)(decimal.Parse(model.Compensation) * 100)) / 100;
                if (model.IsApprovedByPersonnelManager)
                    entity.PersonnelManagerDateAccept = DateTime.Now;
            }
            if (model.IsTypeEditable)
            {
                entity.EventDate = model.EventDate.Value;
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
                model.CreatorLogin = current.Login;
                model.DateCreated = DateTime.Today.ToShortDateString();
            }
            else
            {
                TimesheetCorrection timesheetCorrection = TimesheetCorrectionDao.Load(model.Id);
                model.CreatorLogin = timesheetCorrection.Creator.Login;
                model.DocumentNumber = timesheetCorrection.Number.ToString();
                model.DateCreated = timesheetCorrection.CreateDate.ToShortDateString();
            }
        }
        #endregion
        #region Dismissal
        public DismissalListModel GetDismissalListModel()
        {
            User user = UserDao.Load(AuthenticationService.CurrentUser.Id);
            DismissalListModel model = new DismissalListModel
            {
                UserId = AuthenticationService.CurrentUser.Id,
            };
            SetDictionariesToModel(model, user);
            return model;
        }
        public void SetDismissalListModel(DismissalListModel model)
        {
            User user = UserDao.Load(model.UserId);
            SetDictionariesToModel(model, user);
            SetDocumentsToModel(model, user);
        }
        public void SetDocumentsToModel(DismissalListModel model, User user)
        {

            UserRole role = (UserRole)user.Role.Id;
            model.Documents = DismissalDao.GetDocuments(
                role,
                model.DepartmentId,
                model.PositionId,
                model.TypeId,
                model.StatusId,
                //model.BeginDate,
                model.EndDate);
        }
        protected void SetDictionariesToModel(DismissalListModel model, User user)
        {
            model.Departments = GetDepartments(user);
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
            if (!CheckUserRights(user, current))
                throw new ArgumentException("Доступ запрещен.");
            SetUserInfoModel(user, model);
            Dismissal dismissal = null;
            if (id == 0)
            {
                model.CreatorLogin = current.Login;
                model.Version = 0;
                model.DateCreated = DateTime.Today.ToShortDateString();
            }
            else
            {
                dismissal = DismissalDao.Load(id);
                if (dismissal == null)
                    throw new ArgumentException(string.Format("Командировка (id {0}) не найдена в базе данных.", id));
                model.Version = dismissal.Version;
                model.TypeId = dismissal.Type.Id;
                model.EndDate = dismissal.EndDate;
                model.Compensation = dismissal.Compensation.HasValue? dismissal.Compensation.Value.ToString():string.Empty;
                model.StatusId = dismissal.TimesheetStatus == null ? 0 : dismissal.TimesheetStatus.Id;
                model.Reason = dismissal.Reason;
                model.CreatorLogin = dismissal.Creator.Login;
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
            model.Statuses = GetTimesheetStatusesForDismissal();
            model.Types = GetDismissalTypes(false);
        }
        protected void SetHiddenFields(DismissalEditModel model)
        {
            model.TypeIdHidden = model.TypeId;
            model.StatusIdHidden = model.StatusId;
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
                switch (currentUserRole)
                {
                    case UserRole.Employee:
                        model.IsApprovedByUserEnable = true;
                        break;
                    case UserRole.Manager:
                        model.IsApprovedByManagerEnable = true;
                        model.IsStatusEditable = true;
                        break;
                    case UserRole.PersonnelManager:
                        model.IsApprovedByPersonnelManagerEnable = true;
                        model.IsStatusEditable = true;
                        model.IsPersonnelFieldsEditable = true;
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
                            model.IsStatusEditable = true;
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
                            model.IsStatusEditable = true;
                            model.IsPersonnelFieldsEditable = true;
                        }
                    }
                    else if (!entity.SendTo1C.HasValue && !entity.DeleteDate.HasValue)
                        model.IsDeleteAvailable = true;
                    break;
            }
            model.IsSaveAvailable = model.IsTypeEditable || model.IsStatusEditable
                                    || model.IsApprovedByManagerEnable || model.IsApprovedByUserEnable ||
                                    model.IsApprovedByPersonnelManagerEnable || model.IsPersonnelFieldsEditable;
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
            model.IsStatusEditable = state;
            model.IsTypeEditable = state;
            model.IsPersonnelFieldsEditable = state;

            model.IsDelete = state;
            model.IsDeleteAvailable = state;
        }
        public bool SaveDismissalEditModel(DismissalEditModel model, out string error)
        {
            error = string.Empty;
            User user = null;
            try
            {
                user = UserDao.Load(model.UserId);
                IUser current = AuthenticationService.CurrentUser;
                if (!CheckUserRights(user, current))
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
                }
                else
                {
                    dismissal = DismissalDao.Load(model.Id);
                    if (dismissal.Version != model.Version)
                    {
                        error = "Заявка была изменена другим пользователем.";
                        model.ReloadPage = true;
                        return false;
                    }
                    if (model.IsDelete)
                    {
                        dismissal.DeleteDate = DateTime.Now;
                        DismissalDao.SaveAndFlush(dismissal);
                        model.IsDelete = false;
                    }
                    else
                    {
                        ChangeEntityProperties(current, dismissal, model, user);
                        DismissalDao.SaveAndFlush(dismissal);
                    }
                    if (dismissal.DeleteDate.HasValue)
                        model.IsDeleted = true;
                }
                model.DocumentNumber = dismissal.Number.ToString();
                model.Version = dismissal.Version;
                //model.DaysCount = dismissal.DaysCount;
                model.CreatorLogin = dismissal.Creator.Login;
                model.DateCreated = dismissal.CreateDate.ToShortDateString();
                SetFlagsState(dismissal.Id, user, dismissal, model);
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
                && model.IsApprovedByUser)
                entity.UserDateAccept = DateTime.Now;
            if (current.UserRole == UserRole.Manager && user.Manager != null
                && current.Id == user.Manager.Id
                && !entity.ManagerDateAccept.HasValue)
            {
                entity.TimesheetStatus = TimesheetStatusDao.Load(model.StatusId);
                if (model.IsApprovedByManager)
                    entity.ManagerDateAccept = DateTime.Now;
            }
            if (current.UserRole == UserRole.PersonnelManager && user.PersonnelManager != null
                && current.Id == user.PersonnelManager.Id
                && !entity.PersonnelManagerDateAccept.HasValue)
            {
                entity.TimesheetStatus = TimesheetStatusDao.Load(model.StatusId);
                entity.Compensation = string.IsNullOrEmpty(model.Compensation)?new decimal?() : (decimal)((int)(decimal.Parse(model.Compensation) * 100)) / 100;
                if (model.IsApprovedByPersonnelManager)
                    entity.PersonnelManagerDateAccept = DateTime.Now;
            }
            if (model.IsTypeEditable)
            {
                entity.EndDate = model.EndDate.Value;
                entity.Reason = model.Reason;
                entity.Type = DismissalTypeDao.Load(model.TypeId);
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
                model.CreatorLogin = current.Login;
                model.DateCreated = DateTime.Today.ToShortDateString();
            }
            else
            {
                Dismissal dismissal = DismissalDao.Load(model.Id);
                model.CreatorLogin = dismissal.Creator.Login;
                model.DocumentNumber = dismissal.Number.ToString();
                model.DateCreated = dismissal.CreateDate.ToShortDateString();
            }
        }
        #endregion
        #region Mission
        public MissionListModel GetMissionListModel()
        {
            User user = UserDao.Load(AuthenticationService.CurrentUser.Id);
            MissionListModel model = new MissionListModel
            {
                UserId = AuthenticationService.CurrentUser.Id,
            };
            SetDictionariesToModel(model, user);
            return model;
        }
        public void SetMissionListModel(MissionListModel model)
        {
            User user = UserDao.Load(model.UserId);
            SetDictionariesToModel(model, user);
            SetDocumentsToModel(model, user);
        }
        protected void SetDictionariesToModel(MissionListModel model, User user)
        {
            model.Departments = GetDepartments(user);
            model.Types = GetMissionTypes(true);
            model.Statuses = GetRequestStatuses();
            model.Positions = GetPositions(user);
        }
        protected List<IdNameDto> GetMissionTypes(bool addAll)
        {
            var typeList = MissionTypeDao.LoadAllSorted().ToList().ConvertAll(x => new IdNameDto(x.Id, x.Name));
            if (addAll)
                typeList.Insert(0, new IdNameDto(0, SelectAll));
            return typeList;
        }
        public void SetDocumentsToModel(MissionListModel model, User user)
        {
            UserRole role = (UserRole)user.Role.Id;
            model.Documents = MissionDao.GetDocuments(
                role,
                model.DepartmentId,
                model.PositionId,
                model.TypeId,
                model.StatusId,
                model.BeginDate,
                model.EndDate);
        }
        public MissionEditModel GetMissionEditModel(int id, int userId)
        {
            MissionEditModel model = new MissionEditModel { Id = id, UserId = userId };
            User user = UserDao.Load(userId);
            IUser current = AuthenticationService.CurrentUser;
            if (!CheckUserRights(user, current))
                throw new ArgumentException("Доступ запрещен.");
            SetUserInfoModel(user, model);
            Mission mission = null;
            if (id == 0)
            {
                model.CreatorLogin = current.Login;
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
                model.CreatorLogin = mission.Creator.Login;
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
                        model.IsReasonEditable = true;
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
                            model.IsReasonEditable = true;
                        }
                    }
                    else if (!entity.SendTo1C.HasValue && !entity.DeleteDate.HasValue)
                        model.IsDeleteAvailable = true;
                    break;
            }
            model.IsSaveAvailable = model.IsTypeEditable || model.IsTimesheetStatusEditable
                                    || model.IsApprovedByManagerEnable || model.IsApprovedByUserEnable ||
                                    model.IsApprovedByPersonnelManagerEnable || model.IsReasonEditable;
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
        public bool SaveMissionEditModel(MissionEditModel model,out string error)
        {
            error = string.Empty;
            User user = null;
            try
            {
                user = UserDao.Load(model.UserId);
                IUser current = AuthenticationService.CurrentUser;
                if (!CheckUserRights(user, current))
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
                    if (mission.Version != model.Version)
                    {
                        error = "Заявка была изменена другим пользователем.";
                        model.ReloadPage = true;
                        return false;
                    }
                    if (model.IsDelete)
                    {
                        mission.DeleteDate = DateTime.Now;
                        MissionDao.SaveAndFlush(mission);
                        model.IsDelete = false;
                    }
                    else
                    {
                        ChangeEntityProperties(current, mission, model, user);
                        MissionDao.SaveAndFlush(mission);
                    }
                    if (mission.DeleteDate.HasValue)
                        model.IsDeleted = true;
                }
                model.DocumentNumber = mission.Number.ToString();
                model.Version = mission.Version;
                model.DaysCount = mission.DaysCount;
                model.CreatorLogin = mission.Creator.Login;
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
                model.CreatorLogin = current.Login;
                model.DateCreated = DateTime.Today.ToShortDateString();
            }
            else
            {
                Mission mission = MissionDao.Load(model.Id);
                model.CreatorLogin = mission.Creator.Login;
                model.DocumentNumber = mission.Number.ToString();
                model.DateCreated = mission.CreateDate.ToShortDateString();
            }
        }
        protected void ChangeEntityProperties(IUser current, Mission entity, MissionEditModel model, User user)
        {
            if (current.UserRole == UserRole.Employee && current.Id == model.UserId
                && !entity.UserDateAccept.HasValue
                && model.IsApprovedByUser)
                entity.UserDateAccept = DateTime.Now;
            if (current.UserRole == UserRole.Manager && user.Manager != null
                && current.Id == user.Manager.Id
                && !entity.ManagerDateAccept.HasValue)
            {
                entity.TimesheetStatus = TimesheetStatusDao.Load(model.TimesheetStatusId);
                if (model.IsApprovedByManager)
                    entity.ManagerDateAccept = DateTime.Now;
            }
            if (current.UserRole == UserRole.PersonnelManager && user.PersonnelManager != null
                && current.Id == user.PersonnelManager.Id
                && !entity.PersonnelManagerDateAccept.HasValue)
            {
                entity.TimesheetStatus = TimesheetStatusDao.Load(model.TimesheetStatusId);
                entity.Reason = model.Reason;
                if (model.IsApprovedByPersonnelManager)
                    entity.PersonnelManagerDateAccept = DateTime.Now;
            }
            if (model.IsTypeEditable)
            {
                entity.BeginDate = model.BeginDate.Value;
                entity.EndDate = model.EndDate.Value;
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
            };
            SetDictionariesToModel(model, user);
            return model;
        }
        public void SetHolidayWorkListModel(HolidayWorkListModel model)
        {
            User user = UserDao.Load(model.UserId);
            SetDictionariesToModel(model, user);
            SetDocumentsToModel(model, user);
        }
        public void SetDocumentsToModel(HolidayWorkListModel model, User user)
        {
            UserRole role = (UserRole)user.Role.Id;
            model.Documents = HolidayWorkDao.GetDocuments(
                role,
                model.DepartmentId,
                model.PositionId,
                model.TypeId,
                model.StatusId
                //model.PaymentPercentType
                );
        }
        protected void SetDictionariesToModel(HolidayWorkListModel model, User user)
        {
            model.Departments = GetDepartments(user);
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
            if (!CheckUserRights(user, current))
                throw new ArgumentException("Доступ запрещен.");
            SetUserInfoModel(user, model);
            HolidayWork holidayWork = null;
            if (id == 0)
            {
                model.CreatorLogin = current.Login;
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
                model.CreatorLogin = holidayWork.Creator.Login;
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
                if (!CheckUserRights(user, current))
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
                model.CreatorLogin = holidayWork.Creator.Login;
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
                && current.Id == user.Manager.Id
                && !entity.ManagerDateAccept.HasValue)
            {
                entity.TimesheetStatus = TimesheetStatusDao.Load(model.TimesheetStatusId);
                if (model.IsApprovedByManager)
                    entity.ManagerDateAccept = DateTime.Now;
            }
            if (current.UserRole == UserRole.PersonnelManager && user.PersonnelManager != null
                && current.Id == user.PersonnelManager.Id
                && !entity.PersonnelManagerDateAccept.HasValue)
            {
                entity.TimesheetStatus = TimesheetStatusDao.Load(model.TimesheetStatusId);
                if (model.IsApprovedByPersonnelManager)
                    entity.PersonnelManagerDateAccept = DateTime.Now;
            }
            if (model.IsTypeEditable)
            {
                entity.WorkDate = model.Date.Value;
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
                model.CreatorLogin = current.Login;
                model.DateCreated = DateTime.Today.ToShortDateString();
            }
            else
            {
                HolidayWork holidayWork = HolidayWorkDao.Load(model.Id);
                model.CreatorLogin = holidayWork.Creator.Login;
                model.DocumentNumber = holidayWork.Number.ToString();
                model.DateCreated = holidayWork.CreateDate.ToShortDateString();
            }
        }
        #endregion
        #region Sicklist
        public SicklistListModel GetSicklistListModel()
        {
            User user = UserDao.Load(AuthenticationService.CurrentUser.Id);
            SicklistListModel model = new SicklistListModel
            {
                UserId = AuthenticationService.CurrentUser.Id,
            };
            SetDictionariesToModel(model,user);
            return model;
        }

        protected List<IdNameDto> GetSicklistTypes(bool addAll)
        {
            var typeList = SicklistTypeDao.LoadAllSorted().ToList().ConvertAll(x => new IdNameDto(x.Id, x.Name));
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
        public void SetSicklistListModel(SicklistListModel model)
        {
            User user = UserDao.Load(model.UserId);
            SetDictionariesToModel(model, user);
            SetDocumentsToModel(model, user);
        }
        protected void SetDictionariesToModel(SicklistListModel model, User user)
        {
            model.Departments = GetDepartments(user);
            model.Types = GetSicklistTypes(true);
            model.Statuses = GetRequestStatuses();
            model.Positions = GetPositions(user);
            model.PaymentPercentTypes = GetSicklisPaymentPercentTypes(true,true);
       }
        public void SetDocumentsToModel(SicklistListModel model, User user)
        {
            UserRole role = (UserRole)user.Role.Id;
            model.Documents = SicklistDao.GetDocuments(
                role,
                model.DepartmentId,
                model.PositionId,
                model.TypeId,
                model.StatusId,
                model.PaymentPercentType);
        }
        public SicklistEditModel GetSicklistEditModel(int id, int userId)
        {
            SicklistEditModel model = new SicklistEditModel { Id = id, UserId = userId };
            User user = UserDao.Load(userId);
            IUser current = AuthenticationService.CurrentUser;
            if (!CheckUserRights(user, current))
                throw new ArgumentException("Доступ запрещен.");
            SetUserInfoModel(user, model);
            SetAttachmentToModel(model, id);
            Sicklist sicklist = null;
            if (id == 0)
            {
                model.CreatorLogin = current.Login;
                model.Version = 0;
                model.DateCreated = DateTime.Today.ToShortDateString();
            }
            else
            {
                sicklist = SicklistDao.Load(id);
                if (sicklist == null)
                    throw new ArgumentException(string.Format("Больничный (id {0}) не найдена в базе данных.", id));
                model.Version = sicklist.Version;
                model.TypeId = sicklist.Type.Id;
                model.BeginDate = sicklist.BeginDate;//new DateTimeDto(vacation.BeginDate);//
                model.EndDate = sicklist.EndDate;
                model.TimesheetStatusId = sicklist.TimesheetStatus == null ? 0 : sicklist.TimesheetStatus.Id;
                model.DaysCount = sicklist.DaysCount;
                model.CreatorLogin = sicklist.Creator.Login;
                model.DocumentNumber = sicklist.Number.ToString();
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
                model.Is2010Calculate = sicklist.Is2010Calculate;
                model.IsAddToFullPayment = sicklist.IsAddToFullPayment;
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
            model.TimesheetStatusIdHidden = model.TimesheetStatusId;
            model.DaysCountHidden = model.DaysCount;
            model.PaymentPercentTypeIdHidden = model.PaymentPercentTypeId;
            model.PaymentRestrictTypeIdHidden = model.PaymentRestrictTypeId;
            model.Is2010CalculateHidden = model.Is2010Calculate;
            model.IsPreviousPaymentCountedHidden = model.IsPreviousPaymentCounted;
            model.IsAddToFullPaymentHidden = model.IsAddToFullPaymentHidden;
        }
        protected void SetAttachmentToModel(SicklistEditModel model,int id)
        {
            if (id == 0)
                return;
            RequestAttachment attach = RequestAttachmentDao.FindByRequestIdAndTypeId(id, RequestTypeEnum.Sicklist);
            if (attach == null) 
                return;
            model.AttachmentId = attach.Id;
            //model.AttachmentTypeId = attach.RequestType;
            model.Attachment = attach.FileName;
        }
        public bool SaveSicklistEditModel(SicklistEditModel model,UploadFileDto fileDto, out string error)
        {
            error = string.Empty;
            User user = null;
            try
            {
                user = UserDao.Load(model.UserId);
                IUser current = AuthenticationService.CurrentUser;
                if (!CheckUserRights(user, current))
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
                    SaveAttachment(sicklist, fileDto, model ,RequestTypeEnum.Sicklist);
                    if (sicklist.Version != model.Version)
                    {
                        error = "Заявка была изменена другим пользователем.";
                        model.ReloadPage = true;
                        return false;
                    }
                    if (model.IsDelete)
                    {
                        if (model.AttachmentId > 0)
                            RequestAttachmentDao.Delete(model.AttachmentId);
                        sicklist.DeleteDate = DateTime.Now;
                        SicklistDao.SaveAndFlush(sicklist);
                        model.IsDelete = false;
                        model.AttachmentId = 0;
                        model.Attachment = string.Empty;
                    }
                    else
                    {
                        ChangeEntityProperties(current,sicklist,model,user);   
                        SicklistDao.SaveAndFlush(sicklist);
                    }
                    if (sicklist.DeleteDate.HasValue)
                        model.IsDeleted = true;
                }
                model.DocumentNumber = sicklist.Number.ToString();
                model.Version = sicklist.Version;
                model.DaysCount = sicklist.DaysCount;
                model.CreatorLogin = sicklist.Creator.Login;
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
        protected void SaveAttachment(Sicklist sicklist, UploadFileDto fileDto, SicklistEditModel model, RequestTypeEnum type)
        {
            if (fileDto == null)
                return;
            RequestAttachment attach =
                RequestAttachmentDao.Load(model.AttachmentId) ??
                new RequestAttachment
                    {
                        RequestId = sicklist.Id,
                        RequestType = (int) type,
                    };

            attach.DateCreated = DateTime.Now;
            attach.UncompressContext = fileDto.Context;
            attach.ContextType = fileDto.ContextType;
            attach.FileName = fileDto.FileName;
            RequestAttachmentDao.SaveAndFlush(attach);
            model.AttachmentId = attach.Id;
            //model.AttachmentTypeId = attach.RequestType;
            model.Attachment = attach.FileName;
        }
        protected void ChangeEntityProperties(IUser current, Sicklist sicklist,SicklistEditModel model,User user)
        {
            if (current.UserRole == UserRole.Employee && current.Id == model.UserId
                && !sicklist.UserDateAccept.HasValue
                && model.IsApprovedByUser)
                sicklist.UserDateAccept = DateTime.Now;
            if (current.UserRole == UserRole.Manager && user.Manager != null
                && current.Id == user.Manager.Id
                && !sicklist.ManagerDateAccept.HasValue)
            {
                sicklist.TimesheetStatus = TimesheetStatusDao.Load(model.TimesheetStatusId);
                if (model.IsApprovedByManager)
                    sicklist.ManagerDateAccept = DateTime.Now;
            }
            if (current.UserRole == UserRole.PersonnelManager && user.PersonnelManager != null
                && current.Id == user.PersonnelManager.Id
                && !sicklist.PersonnelManagerDateAccept.HasValue)
            {
                if (model.IsPersonnelFieldsEditable)
                    SetPersonnelDataFromModel(sicklist, model);
                if (model.IsApprovedByPersonnelManager)
                    sicklist.PersonnelManagerDateAccept = DateTime.Now;
            }
            if (model.IsTypeEditable)
            {
                sicklist.BeginDate = model.BeginDate.Value;
                sicklist.EndDate = model.EndDate.Value;
                sicklist.DaysCount = model.EndDate.Value.Subtract(model.BeginDate.Value).Days + 1;
                sicklist.Type = SicklistTypeDao.Load(model.TypeId);
            }
        }
        protected void SetPersonnelDataFromModel(Sicklist sicklist,SicklistEditModel model)
        {
            sicklist.PaymentBeginDate = model.PaymentBeginDate;
            sicklist.ExperienceYears = GetIntFromModel(model.ExperienceYears);
            sicklist.ExperienceMonthes = GetIntFromModel(model.ExperienceMonthes); 
            sicklist.PaymentPercent = model.PaymentPercentTypeId == 0 ? null : SicklistPaymentPercentDao.Load(model.PaymentPercentTypeId);
            sicklist.RestrictType = model.PaymentRestrictTypeId == 0 ? null : SicklistPaymentRestrictTypeDao.Load(model.PaymentRestrictTypeId);
            sicklist.PaymentDecreaseDate = model.PaymentDecreaseDate;
            sicklist.IsPreviousPaymentCounted = model.IsPreviousPaymentCounted;
            sicklist.Is2010Calculate = model.Is2010Calculate;
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
                model.CreatorLogin = current.Login;
                model.DateCreated = DateTime.Today.ToShortDateString();
            }
            else
            {
                Sicklist sicklist = SicklistDao.Load(model.Id);
                model.CreatorLogin = sicklist.Creator.Login;
                model.DocumentNumber = sicklist.Number.ToString();
                model.DateCreated = sicklist.CreateDate.ToShortDateString();
            }
        }
        protected void LoadDictionaries(SicklistEditModel model)
        {
            model.CommentsModel = GetCommentsModel(model.Id, (int)RequestTypeEnum.Sicklist);
            model.TimesheetStatuses = GetTimesheetStatusesForSicklist();
            model.Types = GetSicklistTypes(false);
            model.PaymentPercentTypes = GetSicklisPaymentPercentTypes(!model.IsPersonnelFieldsEditable,false);
            model.PaymentRestrictTypes = GetSicklisPaymentRestrictTypes(true);
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
            if (id == 0)
            {
                model.IsSaveAvailable = true;
                model.IsTypeEditable = true;
                switch (currentUserRole)
                {
                    case UserRole.Employee:
                        model.IsApprovedByUserEnable = false;
                        break;
                    case UserRole.Manager:
                        model.IsApprovedByManagerEnable = false;
                        model.IsTimesheetStatusEditable = true;
                        break;
                    case UserRole.PersonnelManager:
                        model.IsApprovedByPersonnelManagerEnable = false;
                        model.IsTimesheetStatusEditable = true;
                        model.IsPersonnelFieldsEditable = true;
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
                            model.IsPersonnelFieldsEditable = true;
                        }
                    }
                    else if (!entity.SendTo1C.HasValue && !entity.DeleteDate.HasValue)
                        model.IsDeleteAvailable = true;
                    break;
            }
            model.IsSaveAvailable = model.IsTypeEditable || model.IsTimesheetStatusEditable
                                    || model.IsApprovedByManagerEnable || model.IsApprovedByUserEnable ||
                                    model.IsApprovedByPersonnelManagerEnable || model.IsPersonnelFieldsEditable;
        }
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

            model.IsDelete = state;
            model.IsDeleteAvailable = state;

            model.IsPersonnelFieldsEditable = false;
        }
        #endregion
        #region Absence
        public AbsenceListModel GetAbsenceListModel()
        {
            User user = UserDao.Load(AuthenticationService.CurrentUser.Id);
            AbsenceListModel model = new AbsenceListModel
            {
                UserId = AuthenticationService.CurrentUser.Id,
                Departments = GetDepartments(user),
                AbsenceTypes = GetAbsenceTypes(true),
                RequestStatuses = GetRequestStatuses(),
                Positions = GetPositions(user)
            };
            return model;
        }
        protected List<IdNameDto> GetAbsenceTypes(bool addAll)
        {
            var typeList = AbsenceTypeDao.LoadAllSorted().ToList().ConvertAll(x => new IdNameDto(x.Id, x.Name));
            if (addAll)
                typeList.Insert(0, new IdNameDto(0, SelectAll));
            return typeList;
        }
        public void SetAbsenceListModel(AbsenceListModel model)
        {
            User user = UserDao.Load(model.UserId);
            model.Departments = GetDepartments(user);
            model.RequestStatuses = GetRequestStatuses();
            model.Positions = GetPositions(user);
            model.AbsenceTypes = GetAbsenceTypes(true);
            SetDocumentsToModel(model, user);
        }
        public void SetDocumentsToModel(AbsenceListModel model, User user)
        {
            UserRole role = (UserRole)user.Role.Id;
            model.Documents = AbsenceDao.GetDocuments(
                role,
                model.DepartmentId,
                model.PositionId,
                model.AbsenceTypeId,
                model.RequestStatusId,
                model.BeginDate,
                model.EndDate);
        }
        public AbsenceEditModel GetAbsenceEditModel(int id, int userId)
        {
            AbsenceEditModel model = new AbsenceEditModel { Id = id, UserId = userId };
            User user = UserDao.Load(userId);
            IUser current = AuthenticationService.CurrentUser;
            if (!CheckUserRights(user, current))
                throw new ArgumentException("Доступ запрещен.");
            SetUserInfoModel(user, model);
            model.CommentsModel = GetCommentsModel(id, (int)RequestTypeEnum.Absence);
            model.TimesheetStatuses = GetTimesheetStatusesForAbsence();
            model.AbsenceTypes = GetAbsenceTypes(false);
            Absence absence = null;
            if (id == 0)
            {
                model.CreatorLogin = current.Login;
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
                model.CreatorLogin = absence.Creator.Login;
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
                return;
            }
            model.IsApprovedByUserHidden = model.IsApprovedByUser = absence.UserDateAccept.HasValue;
            model.IsApprovedByManagerHidden = model.IsApprovedByManager = absence.ManagerDateAccept.HasValue;
            model.IsApprovedByPersonnelManagerHidden = model.IsApprovedByPersonnelManager = absence.PersonnelManagerDateAccept.HasValue;
            model.IsPostedTo1CHidden = model.IsPostedTo1C = absence.SendTo1C.HasValue;
            switch (currentUserRole)
            {
                case UserRole.Employee:
                    if (!absence.UserDateAccept.HasValue && !absence.DeleteDate.HasValue)
                    {
                        model.IsApprovedByUserEnable = true;
                        //model.IsSaveAvailable = true;
                        if (!absence.ManagerDateAccept.HasValue && !absence.PersonnelManagerDateAccept.HasValue && !absence.SendTo1C.HasValue)
                            model.IsAbsenceTypeEditable = true;
                    }
                    break;
                case UserRole.Manager:
                    if (!absence.ManagerDateAccept.HasValue && !absence.DeleteDate.HasValue)
                    {
                        model.IsApprovedByManagerEnable = true;
                        if (!absence.PersonnelManagerDateAccept.HasValue && !absence.SendTo1C.HasValue)
                        {
                            model.IsAbsenceTypeEditable = true;
                            model.IsTimesheetStatusEditable = true;
                        }
                    }
                    break;
                case UserRole.PersonnelManager:
                    if (!absence.PersonnelManagerDateAccept.HasValue)
                    {
                        model.IsApprovedByPersonnelManagerEnable = true;
                        if (!absence.SendTo1C.HasValue)
                        {
                            model.IsAbsenceTypeEditable = true;
                            model.IsTimesheetStatusEditable = true;
                        }
                    }
                    else if (!absence.SendTo1C.HasValue && !absence.DeleteDate.HasValue)
                            model.IsDeleteAvailable = true;
                    break;
            }
            model.IsSaveAvailable = model.IsAbsenceTypeEditable || model.IsTimesheetStatusEditable
                                    || model.IsApprovedByManagerEnable || model.IsApprovedByUserEnable ||
                                    model.IsApprovedByPersonnelManagerEnable;
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
                model.CreatorLogin = current.Login;
                model.DateCreated = DateTime.Today.ToShortDateString();
            }
            else
            {
                Absence absence = AbsenceDao.Load(model.Id);
                model.CreatorLogin = absence.Creator.Login;
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
                if (!CheckUserRights(user, current))
                {
                    error = "Редактирование заявки запрещено";
                    return false;
                }
                Absence absence;
                if (model.Id == 0)
                {
                    absence = new Absence
                    {
                        BeginDate = model.BeginDate.Value,
                        CreateDate = DateTime.Now,
                        Creator = UserDao.Load(current.Id),
                        EndDate = model.EndDate.Value,
                        DaysCount = model.EndDate.Value.Subtract(model.BeginDate.Value).Days+1,
                        Number = RequestNextNumberDao.GetNextNumberForType((int)RequestTypeEnum.Absence),
                        //Status = RequestStatusDao.Load((int)RequestStatusEnum.NotApproved),
                        Type = AbsenceTypeDao.Load(model.AbsenceTypeId),
                        User = user
                    };
                    if (current.UserRole == UserRole.Employee && current.Id == model.UserId && model.IsApprovedByUser)
                        absence.UserDateAccept = DateTime.Now;
                    if (current.UserRole == UserRole.Manager && user.Manager != null
                        && current.Id == user.Manager.Id)
                    {
                        absence.TimesheetStatus = TimesheetStatusDao.Load(model.TimesheetStatusId);
                        if(model.IsApprovedByManager)
                            absence.ManagerDateAccept = DateTime.Now;
                    }
                    if (current.UserRole == UserRole.PersonnelManager && user.PersonnelManager != null
                        && current.Id == user.PersonnelManager.Id)
                    {
                        absence.TimesheetStatus = TimesheetStatusDao.Load(model.TimesheetStatusId);
                        if (model.IsApprovedByPersonnelManager)
                            absence.PersonnelManagerDateAccept = DateTime.Now;
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
                        absence.DeleteDate = DateTime.Now;
                        AbsenceDao.SaveAndFlush(absence);
                        //model.TimesheetStatusId = absence.TimesheetStatus == null? 0:absence.TimesheetStatus.Id;
                        //model.AbsenceTypeId = absence.Type.Id;
                        model.IsDelete = false;
                    }
                    else
                    {
                        if (current.UserRole == UserRole.Employee && current.Id == model.UserId
                            && !absence.UserDateAccept.HasValue
                            && model.IsApprovedByUser)
                            absence.UserDateAccept = DateTime.Now;
                        if (current.UserRole == UserRole.Manager && user.Manager != null
                            && current.Id == user.Manager.Id
                            && !absence.ManagerDateAccept.HasValue)
                        {
                            absence.TimesheetStatus = TimesheetStatusDao.Load(model.TimesheetStatusId);
                            if(model.IsApprovedByManager)
                                absence.ManagerDateAccept = DateTime.Now;
                        }
                        if (current.UserRole == UserRole.PersonnelManager && user.PersonnelManager != null
                            && current.Id == user.PersonnelManager.Id
                            && !absence.PersonnelManagerDateAccept.HasValue
                            )
                        {
                            absence.TimesheetStatus = TimesheetStatusDao.Load(model.TimesheetStatusId);
                            if (model.IsApprovedByPersonnelManager)
                                absence.PersonnelManagerDateAccept = DateTime.Now;

                        }
                        if (model.IsAbsenceTypeEditable)
                        {
                            absence.BeginDate = model.BeginDate.Value;
                            absence.EndDate = model.EndDate.Value;
                            absence.DaysCount = model.EndDate.Value.Subtract(model.BeginDate.Value).Days + 1;
                            absence.Type = AbsenceTypeDao.Load(model.AbsenceTypeId);
                        }
                        AbsenceDao.SaveAndFlush(absence);
                    }
                    if (absence.DeleteDate.HasValue)
                        model.IsDeleted = true;
                }
                model.DocumentNumber = absence.Number.ToString();
                model.Version = absence.Version;
                model.DaysCount = absence.DaysCount;
                model.CreatorLogin = absence.Creator.Login;
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
            VacationListModel model = new VacationListModel
                                          {
                                              UserId = AuthenticationService.CurrentUser.Id,
                                              Departments = GetDepartments(user),
                                              VacationTypes = GetVacationTypes(true),
                                              RequestStatuses = GetRequestStatuses(),
                                              Positions = GetPositions(user)
                                          };
            return model;
        }
        public void SetVacationListModel(VacationListModel model)
        {
            User user = UserDao.Load(model.UserId);
            model.Departments = GetDepartments(user);
            model.RequestStatuses = GetRequestStatuses();
            model.Positions = GetPositions(user);
            model.VacationTypes = GetVacationTypes(true);
            SetDocumentsToModel(model,user);
        }
        public void SetDocumentsToModel(VacationListModel model,User user)
        {
            UserRole role = (UserRole)user.Role.Id;
            model.Documents = VacationDao.GetDocuments(
                role,
                model.DepartmentId,
                model.PositionId,
                model.VacationTypeId,
                model.RequestStatusId,
                model.BeginDate,
                model.EndDate);
        }
        public List<IdNameDto> GetDepartments(User user)
        {
            //var departmentList = DepartmentDao.LoadAllSorted().ToList().ConvertAll(x => new IdNameDto(x.Id, x.Name));
            //departmentList.Insert(0,new IdNameDto(0,SelectAll));
            var departmentList = UserToDepartmentDao.GetByUserId(user.Id).ToList();
            if((UserRole)user.Role.Id != UserRole.Employee)
                departmentList.Insert(0, new IdNameDto(0, SelectAll));
            return departmentList;
        }
        public List<IdNameDto> GetVacationTypes(bool addAll)
        {
            var vacationTypeList = VacationTypeDao.LoadAllSorted().ToList().ConvertAll(x => new IdNameDto(x.Id, x.Name));
            if(addAll)
                vacationTypeList.Insert(0,new IdNameDto(0,SelectAll));
            return vacationTypeList;
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
                                                       };
            requestStatusesList.Insert(0, new IdNameDto(0, SelectAll));
            return requestStatusesList;
        }
        public List<IdNameDto> GetPositions(User user)
        {
            List<IdNameDto> positionList = null;
            if ((UserRole)user.Role.Id != UserRole.Employee)
            {
                positionList = PositionDao.LoadAllSorted().ToList().ConvertAll(x => new IdNameDto(x.Id, x.Name));
                positionList.Insert(0, new IdNameDto(0, SelectAll));
            }
            else
            {
                Position position = user.Position;
                if(position != null)
                    positionList = new List<IdNameDto> {new IdNameDto(position.Id,position.Name)};
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
            if (!CheckUserRights(user, current))
                throw new ArgumentException("Доступ запрещен.");
            SetUserInfoModel(user, model);
            model.CommentsModel = GetCommentsModel(id, (int)RequestTypeEnum.Vacation);
            model.TimesheetStatuses = GetTimesheetStatusesForVacation();
            model.VacationTypes = GetVacationTypes(false);
            Vacation vacation = null; 
            if(id == 0)
            {
                model.CreatorLogin = current.Login;
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
                model.BeginDate = vacation.BeginDate;//new DateTimeDto(vacation.BeginDate);//
                model.EndDate = vacation.EndDate;
                model.TimesheetStatusId = vacation.TimesheetStatus == null ? 0 : vacation.TimesheetStatus.Id;
                model.TimesheetStatusIdHidden = model.TimesheetStatusId; 
                model.DaysCount = vacation.DaysCount;
                model.DaysCountHidden = model.DaysCount;
                model.CreatorLogin = vacation.Creator.Login;
                model.DocumentNumber = vacation.Number.ToString();
                model.DateCreated = vacation.CreateDate.ToShortDateString();
                if (vacation.DeleteDate.HasValue)
                    model.IsDeleted = true;
            }
            SetFlagsState(id, user,vacation, model);
            return model;
        }
        public bool SaveVacationEditModel(VacationEditModel model, out string error)
        {
            error = string.Empty;
            User user = null;
            try
            {
                user = UserDao.Load(model.UserId);
                IUser current = AuthenticationService.CurrentUser;
                if (!CheckUserRights(user, current))
                {
                    error = "Редактирование заявки запрещено";
                    return false;
                }
                Vacation vacation;
                if(model.Id == 0)
                {
                    vacation = new Vacation
                                            {
                                                BeginDate = model.BeginDate.Value,
                                                CreateDate = DateTime.Now,
                                                Creator = UserDao.Load(current.Id),
                                                EndDate = model.EndDate.Value,
                                                DaysCount = model.EndDate.Value.Subtract(model.BeginDate.Value).Days+1,
                                                Number = RequestNextNumberDao.GetNextNumberForType((int)RequestTypeEnum.Vacation),
                                                //Status = RequestStatusDao.Load((int) RequestStatusEnum.NotApproved),
                                                Type = VacationTypeDao.Load(model.VacationTypeId),
                                                User = user
                                             };
                    if (current.UserRole == UserRole.Employee && current.Id == model.UserId && model.IsApprovedByUser)
                        vacation.UserDateAccept = DateTime.Now;
                    if (current.UserRole == UserRole.Manager && user.Manager != null
                        && current.Id == user.Manager.Id)
                    {
                        vacation.TimesheetStatus = TimesheetStatusDao.Load(model.TimesheetStatusId);
                        if(model.IsApprovedByManager)
                            vacation.ManagerDateAccept = DateTime.Now;
                    }
                    if (current.UserRole == UserRole.PersonnelManager && user.PersonnelManager != null
                        && current.Id == user.PersonnelManager.Id )
                    {
                        vacation.TimesheetStatus = TimesheetStatusDao.Load(model.TimesheetStatusId);
                        if (model.IsApprovedByPersonnelManager)
                            vacation.PersonnelManagerDateAccept = DateTime.Now;
                    }

                    VacationDao.SaveAndFlush(vacation);
                    model.Id = vacation.Id;
                }
                else
                {
                    vacation = VacationDao.Load(model.Id);
                    if (vacation.Version != model.Version)
                    {
                        error = "Заявка была изменена другим пользователем.";
                        model.ReloadPage = true;
                        return false;
                    }
                    if (model.IsDelete)
                    {
                        vacation.DeleteDate = DateTime.Now;
                        VacationDao.SaveAndFlush(vacation);
                        model.IsDelete = false;
                        //model.VacationTypeId = vacation.Type.Id;
                        //model.TimesheetStatusId = vacation.TimesheetStatus == null ? 0 : vacation.TimesheetStatus.Id;
                    }
                    else
                    {
                        if (current.UserRole == UserRole.Employee && current.Id == model.UserId
                            && !vacation.UserDateAccept.HasValue 
                            && model.IsApprovedByUser)
                            vacation.UserDateAccept = DateTime.Now;
                        
                        if (current.UserRole == UserRole.Manager && user.Manager != null
                            && current.Id == user.Manager.Id
                            && !vacation.ManagerDateAccept.HasValue )
                        {
                            vacation.TimesheetStatus = TimesheetStatusDao.Load(model.TimesheetStatusId);
                            if(model.IsApprovedByManager)
                                vacation.ManagerDateAccept = DateTime.Now;
                        }
                        if (current.UserRole == UserRole.PersonnelManager && user.PersonnelManager != null
                            && current.Id == user.PersonnelManager.Id
                            && !vacation.PersonnelManagerDateAccept.HasValue)
                        {
                            vacation.TimesheetStatus = TimesheetStatusDao.Load(model.TimesheetStatusId);
                            if (model.IsApprovedByPersonnelManager)
                                vacation.PersonnelManagerDateAccept = DateTime.Now;

                        }
                        if (model.IsVacationTypeEditable)
                        {
                            vacation.BeginDate = model.BeginDate.Value;
                            vacation.EndDate = model.EndDate.Value;
                            vacation.DaysCount = model.EndDate.Value.Subtract(model.BeginDate.Value).Days+1;
                            vacation.Type = VacationTypeDao.Load(model.VacationTypeId);
                        }
                        VacationDao.SaveAndFlush(vacation);
                    }
                    if (vacation.DeleteDate.HasValue)
                        model.IsDeleted = true;
                }
                model.DocumentNumber = vacation.Number.ToString();
                model.Version = vacation.Version;
                model.DaysCount = vacation.DaysCount;
                model.CreatorLogin = vacation.Creator.Login;
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
                model.TimesheetStatusIdHidden = model.TimesheetStatusId;
                model.DaysCountHidden = model.DaysCount;
            }
        }
        public bool CheckUserRights(User user, IUser current)
        {
            switch (current.UserRole)
            {
                case UserRole.Employee:
                    if (user.Id != current.Id)
                        return false;
                    break;
                case UserRole.Manager:
                    if (user.Manager != null && user.Manager.Id != current.Id)
                        return false;
                    break;
                case UserRole.PersonnelManager:
                    if (user.PersonnelManager != null && user.PersonnelManager.Id != current.Id)
                        return false;
                    break;
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
                model.CreatorLogin = current.Login;
                model.DateCreated = DateTime.Today.ToShortDateString();
            }
            else
            {
                Vacation vacation = VacationDao.Load(model.Id);
                model.CreatorLogin = vacation.Creator.Login;
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
        }
        protected void SetFlagsState(int id,User user,Vacation vacation,VacationEditModel model)
        {
            SetFlagsState(model,false);
            UserRole currentUserRole = AuthenticationService.CurrentUser.UserRole;
            if(id == 0)
            {
                model.IsSaveAvailable = true;
                model.IsVacationTypeEditable = true;
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
                return;
            }
            
            model.IsApprovedByUserHidden = model.IsApprovedByUser = vacation.UserDateAccept.HasValue;
            model.IsApprovedByManagerHidden = model.IsApprovedByManager = vacation.ManagerDateAccept.HasValue;
            model.IsApprovedByPersonnelManagerHidden =
                model.IsApprovedByPersonnelManager = vacation.PersonnelManagerDateAccept.HasValue;
            model.IsPostedTo1CHidden = model.IsPostedTo1C = vacation.SendTo1C.HasValue;
            switch(currentUserRole)
            {
                case UserRole.Employee:
                    if (!vacation.UserDateAccept.HasValue && !vacation.DeleteDate.HasValue)
                    {
                        model.IsApprovedByUserEnable = true;
                        if(!vacation.ManagerDateAccept.HasValue && !vacation.PersonnelManagerDateAccept.HasValue && !vacation.SendTo1C.HasValue)
                            model.IsVacationTypeEditable = true;
                    }
                    break;
                case UserRole.Manager:
                    if (!vacation.ManagerDateAccept.HasValue && !vacation.DeleteDate.HasValue)
                    {
                        model.IsApprovedByManagerEnable = true;
                       if (!vacation.PersonnelManagerDateAccept.HasValue && !vacation.SendTo1C.HasValue)
                        {
                            model.IsVacationTypeEditable = true;
                            model.IsTimesheetStatusEditable = true;
                        }
                    }
                    break;
                case UserRole.PersonnelManager:
                    if (!vacation.PersonnelManagerDateAccept.HasValue)
                    {
                        model.IsApprovedByPersonnelManagerEnable = true;
                        if (!vacation.SendTo1C.HasValue)
                        {
                            model.IsVacationTypeEditable = true;
                            model.IsTimesheetStatusEditable = true;
                        }
                    }
                    else if (!vacation.SendTo1C.HasValue && 
                             !vacation.DeleteDate.HasValue)
                        model.IsDeleteAvailable = true;
                    
                    break;
            }
            model.IsSaveAvailable = model.IsVacationTypeEditable || model.IsTimesheetStatusEditable
                                    || model.IsApprovedByManagerEnable || model.IsApprovedByUserEnable ||
                                    model.IsApprovedByPersonnelManagerEnable;
        }
        protected void SetUserInfoModel(User user,UserInfoModel model)
        {
            //model.DateCreated = DateTime.Today.ToShortDateString();
            IList<IdNameDto> departments = UserToDepartmentDao.GetByUserId(user.Id);
            if (departments.Count > 0)
                model.Department = departments[0].Name;
            if(user.Manager != null)
                model.ManagerName = user.Manager.FullName;
            if (user.PersonnelManager != null)
                model.PersonnelName = user.PersonnelManager.FullName;
            if (user.Organization != null)
                model.Organization = user.Organization.Name;
            if(user.Position != null)
                model.Position = user.Position.Name;
            model.UserName = user.FullName;
            model.UserNumber = user.Code;
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
        #endregion
    }

}