using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Reports.Core;
using Reports.Core.Dao;
using Reports.Core.Domain;
using Reports.Core.Dto;
using Reports.Core.Enum;
using Reports.Core.Services;
using Reports.Presenters.Services;
using Reports.Presenters.UI.ViewModel;

namespace Reports.Presenters.UI.Bl.Impl
{
    public class AppointmentBl : BaseBl, IAppointmentBl
    {
        public const string StrException = "Исключение:";
        public const string StrCommentCreationDedied = "Добавление комментария запрещено";
        public const string StrAppointmentNotFound = "Не найдена заявка (id {0}) в базе данных";
        public const string StrAccessIsDenied = "Доступ запрещен";
        public const string StrUserNotManager = "Вы (пользователь {0}) не являетесь руководителем или членом правления - создание заявки запрещено";
        public const string StrIncorrectManagerLevel  = "Неправильный уровень {0} руководителя (id {1}) в базе данных.";
        public const string StrNoDepartmentForManager = "Не указано структурное подраздаление для руководителя (id {0}).";
        public const string StrIncorrectReasonId = "Неверное основание появления вакансии {0}.";
        public const string StrDepartmentNotFound = "Не найдено структурное подразделение (id {0}) в базе данных";
        public const string StrUserNotFound = "Не найден пользователь (id {0}) в базе данных";
        public const string StrEmailForAppointmentManagerAcceptIncorrectManagerLevel = "SendEmailForAppointmentManagerAccept - неверный уровень руководителя {0}";
        public const string StrEmailForAppointmentManagerAcceptIncorrectRole = "SendEmailForAppointmentManagerAccept - неверная роль пользователя {0}";
        public const string StrEmailForAppointmentManagerAcceptDepartment3NotFound = "SendEmailForAppointmentManagerAccept - не найдена дирекция уровня 3 для структурного подразделения (id {0})";
        public const string StrEmailForAppointmentManagerAcceptParent2NotFound = "Не найден вышестоящий руководитель для руководителя уровня 2 (id {0})";
        public const string StrEmailForAppointmentManagerAcceptParent3NotFound = "Не найден вышестоящий руководитель для руководителя уровня 3 (id {0})";
        public const string StrEmailForAppointmentManagerAcceptParentNotFound = "Не найден вышестоящий руководитель для руководителя (id {0})";
        public const string StrEmailForAppointmentManagerAcceptNoEmail = "Не указан email руководителя (id {0})";
        public const string StrEmailForAppointmentManagerAcceptNoEmails = "Не указаны email руководителей для пользователя (id {0})";
        public const string StrEmailForAppointmentManagerAcceptText = "Согласована заявка № {0} на подбор {1}, дирекция {2} сотрудником {3}";
        public const string StrEmailForAppointmentManagerAcceptSubject = "Согласование заявки";
        public const string StrEmailForPersonnalManagerNotFound = "Не задан адрес рассылки для кадровиков в конфигурационном файле";
        public const string StrEmailForStaffManagerNotFound = "Не задан адрес рассылки для сотрудников по подбору персонала в конфигурационном файле";

        public const string StrEmailForAppointmentManagerRejectText = "Отменена заявка № {0} на подбор {1}, дирекция {2} сотрудником {3}";
        public const string StrEmailForAppointmentManagerRejectSubject = "Отмена заявки";

        public const string StrAppointmentReportNotFound = "Не найден отчет (id {0}) в базе данных";
        public const string StrAppointmentReportIncorrectId = "Неправильный идентификатор отчета (0)";

        public const int MinManagerLevel = 2;
        public const int MaxManagerLevel = 6;
        public const int RequeredDepartmentLevel = 7;

        public virtual int GetRequeredDepartmentLevel()
        {
            return RequeredDepartmentLevel;
        }
        #region DAOs
        protected IAppointmentDao appointmentDao;
        public IAppointmentDao AppointmentDao
        {
            get { return Validate.Dependency(appointmentDao); }
            set { appointmentDao = value; }
        }
        protected IAppointmentCommentDao appointmentCommentDao;
        public IAppointmentCommentDao AppointmentCommentDao
        {
            get { return Validate.Dependency(appointmentCommentDao); }
            set { appointmentCommentDao = value; }
        }
        protected IPositionDao positionDao;
        public IPositionDao PositionDao
        {
            get { return Validate.Dependency(positionDao); }
            set { positionDao = value; }
        }
        protected IAppointmentReasonDao appointmentReasonDao;
        public IAppointmentReasonDao AppointmentReasonDao
        {
            get { return Validate.Dependency(appointmentReasonDao); }
            set { appointmentReasonDao = value; }
        }
        protected IAppointmentReportDao appointmentReportDao;
        public IAppointmentReportDao AppointmentReportDao
        {
            get { return Validate.Dependency(appointmentReportDao); }
            set { appointmentReportDao = value; }
        }
        #endregion
        protected IConfigurationService configurationService;
        public IConfigurationService ConfigurationService
        {
            set { configurationService = value; }
            get { return Validate.Dependency(configurationService); }
        }

        public AppointmentListModel GetAppointmentListModel()
        {
            //User user = UserDao.Load(AuthenticationService.CurrentUser.Id);
            //IdNameReadonlyDto dep = GetDepartmentDto(user);
            UserRole role = AuthenticationService.CurrentUser.UserRole;
            AppointmentListModel model = new AppointmentListModel
            {
                //UserId = AuthenticationService.CurrentUser.Id,
                DepartmentName = string.Empty,
                DepartmentId = 0,
                DepartmentReadOnly = false,
            };
            SetInitialDates(model);
            SetDictionariesToModel(model);
            model.IsAddAvailable = role == UserRole.Manager;
            //SetInitialStatus(model);
            //SetIsAvailable(model);
            return model;
        }
        public void SetDictionariesToModel(AppointmentListModel model)
        {
            model.Statuses = GetAppRequestStatuses();
        }
        protected List<IdNameDto> GetAppRequestStatuses()
        {
            //var requestStatusesList = RequestStatusDao.LoadAllSorted().ToList().ConvertAll(x => new IdNameDto(x.Id, x.Name));
            List<IdNameDto> moStatusesList = new List<IdNameDto>
                                                       {
                                                           new IdNameDto(1, "Заявка создана"),
                                                           new IdNameDto(2, "Не одобрена вышестоящим руководителем"),
                                                           new IdNameDto(3, "Одобрена вышестоящим руководителем"),
                                                           new IdNameDto(4, "Принята в работу"),
                                                           new IdNameDto(5, "Отменена"),
                                                           //new IdNameDto(4, "Не одобрен руководителем"),
                                                           //new IdNameDto(6, "Не одобрен бухгалтером"),
                                                           //new IdNameDto(7, "Требует одобрения руководителем"),
                                                           //new IdNameDto(8, "Требует одобрения бухгалтером"),
                                                       }.OrderBy(x => x.Name).ToList();
            moStatusesList.Insert(0, new IdNameDto(0, SelectAll));
            return moStatusesList;
        }

        public void SetAppointmentListModel(AppointmentListModel model, bool hasError)
        {
            SetDictionariesToModel(model);
            User user = UserDao.Load(AuthenticationService.CurrentUser.Id);
            if (hasError)
                model.Documents = new List<AppointmentDto>();
            else
                SetDocumentsToModel(model, user);
        }
        public void SetDocumentsToModel(AppointmentListModel model, User user)
        {
            UserRole role = AuthenticationService.CurrentUser.UserRole;
            //model.Documents = new List<AppointmentDto>();
            model.Documents = AppointmentDao.GetDocuments(
                user.Id,
                role,
                model.DepartmentId,
                model.StatusId,
                model.BeginDate,
                model.EndDate,
                model.UserName,
                model.SortBy,
                model.SortDescending);
        }

        public AppointmentEditModel GetAppointmentEditModel(int id)
        {
            AppointmentEditModel model = new AppointmentEditModel {Id = id};
            Appointment entity = null;
            User creator;
            IUser current = AuthenticationService.CurrentUser;
            User currUser = UserDao.Load(current.Id);
            if (id != 0)
            {
                entity = AppointmentDao.Load(id);
                if (entity == null)
                    throw new ValidationException(string.Format(StrAppointmentNotFound, id));
                creator = entity.Creator;
                model.AdditionalRequirements = entity.AdditionalRequirements;
                model.Bonus = FormatSum(entity.Bonus);
                model.City = entity.City;
                model.Compensation = entity.Compensation;
                model.DateCreated = FormatDate(entity.CreateDate);
                model.DepartmentId = entity.Department.Id;
                model.DepartmentName = entity.Department.Name;
                model.DesirableBeginDate = FormatDate(entity.DesirableBeginDate);
                model.EducationRequirements = entity.EducationRequirements;
                model.ExperienceRequirements = entity.ExperienceRequirements;
                model.IsVacationExists = entity.IsVacationExists ? 1 : 0;
                model.DocumentNumber = entity.Number.ToString();
                model.OtherRequirements = entity.OtherRequirements;
                model.Period = entity.Period;
                model.PositionId = entity.Position.Id;
                model.ReasonId = entity.Reason.Id;
                // hardcode using database Ids from [dbo].[AppointmentReason] 
                switch (entity.Reason.Id)
                {
                    case 1:
                    case 2:
                    //case 3:
                    case 4:
                    case 5:
                        model.ReasonPosition = entity.ReasonPosition;
                        model.ReasonBeginDate = FormatDate(entity.ReasonBeginDate);
                        break;
                    case 3:
                        model.ReasonPosition = entity.ReasonPosition;
                        model.ReasonBeginDate = string.Empty;
                        break;
                    /*case 4:
                    case 5:
                        model.ReasonPosition = entity.ReasonUser;
                        model.ReasonBeginDate = FormatDate(entity.ReasonBeginDate);
                        break;*/
                    default:
                        throw new ArgumentException(string.Format(StrIncorrectReasonId,entity.Reason.Id));
                }
                model.Responsibility = entity.Responsibility;
                model.Salary = FormatSum(entity.Salary);
                model.Schedule = entity.Schedule;
                model.TypeId = entity.Type?1:0;
                model.UserId = entity.Creator.Id;//todo ???
                model.VacationCount = entity.VacationCount.ToString();
                model.Version = entity.Version;
            }
            else
            {
                creator = currUser;
                model.IsVacationExists = 1;
                SetCreatorDepartment(creator, model);
                model.UserId = currUser.Id;
            }
            //if (!CheckUserRights(current, id, entity, false)) todo ???
            //    throw new ArgumentException(StrAccessIsDenied);
            SetManagerInfoModel(creator, model);
            LoadDictionaries(model);
            SetFlagsState(id, currUser,current.UserRole, entity, model);
            SetHiddenFields(model);
            return model;
        }
        protected void SetCreatorDepartment(User creator,AppointmentEditModel model)
        {
            if (creator.UserRole == UserRole.Manager)
            {
                List<DepartmentDto> departments;
                switch (creator.Level)
                {
                    case 2:
                        departments = AppointmentDao.GetDepartmentsForManager23(creator.Id, 2, false).ToList();
                        if(departments.Count > 0)
                        {
                            model.DepartmentId = departments[0].Id;
                            model.DepartmentName = departments[0].Name;
                        }
                        break;
                    case 3:
                        departments = AppointmentDao.GetDepartmentsForManager23(creator.Id, 3, false).ToList();
                        if(departments.Count > 0)
                        {
                            model.DepartmentId = departments[0].Id;
                            model.DepartmentName = departments[0].Name;
                        }
                        break;
                    default:     
                        if (creator.Department == null)
                            throw new ValidationException(string.Format(StrNoDepartmentForManager, creator.Id));
                        model.DepartmentId = creator.Department.Id;
                        model.DepartmentName = creator.Department.Name;
                        break;
                }
            }
        }
        protected void SetFlagsState(int id, User current,UserRole  currRole, Appointment entity, AppointmentEditModel model)
        {
            SetFlagsState(model, false);
            if(model.Id == 0)
            {
                if (currRole != UserRole.Manager)
                    throw new ArgumentException(string.Format(StrUserNotManager, current.Id));
                model.IsEditable = true;
                model.IsSaveAvailable = true;
                model.IsManagerApproveAvailable = true;
                return;
            }
            model.IsManagerApproved = entity.ManagerDateAccept.HasValue; 
            model.IsChiefApproved = entity.ChiefDateAccept.HasValue; 
            model.IsPersonnelApproved = entity.PersonnelDateAccept.HasValue;
            model.IsStaffApproved = entity.StaffDateAccept.HasValue;
            model.IsDeleted = entity.DeleteDate.HasValue;
            if (entity.AcceptManager != null && entity.ManagerDateAccept.HasValue)
                model.ManagerFio = entity.AcceptManager.FullName;
            if (entity.AcceptChief != null && entity.ChiefDateAccept.HasValue)
                model.ChiefFio = entity.AcceptChief.FullName;
            if (entity.AcceptPersonnel != null && entity.PersonnelDateAccept.HasValue)
                model.PersonnelFio = entity.AcceptPersonnel.FullName;
            if (entity.AcceptStaff != null && entity.StaffDateAccept.HasValue)
                model.StaffFio = entity.AcceptStaff.FullName;

            if(entity.DeleteDate.HasValue)
            {
                /*if (entity.DeleteUser.Id == entity.Creator.Id)
                    model.ManagerFio = entity.DeleteUser.FullName;
                else
                    model.ChiefFio = entity.DeleteUser.FullName;*/
                model.DeleteUser = entity.DeleteUser.FullName;
                if (entity.AcceptStaff != null)
                    model.IsStaffReceiveRejectMail = true;// todo Need flag on entity ?
                /*if (entity.AcceptStaff != null)
                    model.StaffFio = entity.AcceptStaff.FullName;*/
            }
            switch (currRole)
            {
                case UserRole.Manager:
                    if(current.Id == entity.Creator.Id && !entity.DeleteDate.HasValue)
                    {
                            model.IsManagerRejectAvailable = true;
                            if (!entity.ManagerDateAccept.HasValue)
                            {
                                model.IsManagerApproveAvailable = true;
                                model.IsEditable = true;
                            }
                    }
                    else if (!entity.DeleteDate.HasValue 
                            && entity.ManagerDateAccept.HasValue 
                            && entity.Creator.Id != current.Id
                            && IsManagerChiefForCreator(current, entity.Creator))
                    {
                            model.IsManagerRejectAvailable = true;
                            if (!entity.ChiefDateAccept.HasValue)
                                model.IsChiefApproveAvailable = true;
                    }
                    break;
                //case UserRole.Director:
                //    if (!entity.DeleteDate.HasValue && IsDirectorChiefForCreator(current,entity.Creator)
                //            /*&& (entity.Creator.Department.ItemLevel == 2)
                //            && entity.Creator.Id != current.Id*/)
                //    {
                //        model.IsManagerRejectAvailable = true;
                //        if (!entity.ChiefDateAccept.HasValue )
                //            model.IsChiefApproveAvailable = true;
                //    }
                //    break;
                case UserRole.PersonnelManager:
                    if (!entity.DeleteDate.HasValue && entity.ChiefDateAccept.HasValue &&
                        !entity.PersonnelDateAccept.HasValue)
                        model.IsPersonnelApproveAvailable = true;
                    
                    break;
                case UserRole.StaffManager:
                    if (!entity.DeleteDate.HasValue && 
                        entity.PersonnelDateAccept.HasValue && !entity.StaffDateAccept.HasValue)
                        model.IsStaffApproveAvailable = true;
                    break;
                case UserRole.OutsourcingManager:
                    break;
            }
            model.IsSaveAvailable = model.IsEditable || model.IsManagerApproveAvailable 
                || model.IsChiefApproveAvailable || /*model.IsManagerRejectAvailable ||*/
                model.IsPersonnelApproveAvailable || model.IsStaffApproveAvailable;
        }
        /*protected bool IsDirectorChiefForCreator(User current, User creator)
        {
            if (!creator.Level.HasValue || creator.Level < MinManagerLevel || creator.Level > MaxManagerLevel)
                throw new ValidationException(string.Format(StrIncorrectManagerLevel,
                        creator.Level.HasValue ? creator.Level.Value.ToString() : "<не указан>", creator.Id));
            return (creator.Level.Value == 2);
        }*/
        protected bool IsManagerChiefForCreator(User current, User creator)
        {
            if( !current.Level.HasValue || current.Level < MinManagerLevel || current.Level > MaxManagerLevel)
                throw new ValidationException(string.Format(StrIncorrectManagerLevel,
                        current.Level.HasValue? current.Level.Value.ToString():"<не указан>" ,current.Id)); 
            if( !creator.Level.HasValue || creator.Level < MinManagerLevel || creator.Level > MaxManagerLevel)
                throw new ValidationException(string.Format(StrIncorrectManagerLevel,
                        creator.Level.HasValue?creator.Level.Value.ToString():"<не указан>",creator.Id));
            List<DepartmentDto> departments;
            switch (current.Level)
            {
                case 2:
                    IList<int> managers2 = AppointmentDao.GetChildrenManager2ForManager2(current.Id);
                    if(managers2.Any(x => x == creator.Id && creator.Level.Value == 2))
                        return true;
                    IList<int> managers = AppointmentDao.GetManager3ForManager2(current.Id);
                    if(managers.Any(x => x == creator.Id && creator.Level.Value == 3))
                        return true;
                    departments = AppointmentDao.GetDepartmentsForManager23(current.Id, 2 , true).ToList();
                    return departments.Any(x => creator.Department.Path.StartsWith(x.Path) && creator.Level == 4);
                case 3:
                    if (creator.Level != 4)
                        return false;
                    departments = AppointmentDao.GetDepartmentsForManager23(current.Id, 3, false).ToList();
                    return departments.Any(x => creator.Department.Path.StartsWith(x.Path));
                default:
                    if(creator.Department == null)
                        throw new ValidationException(string.Format(StrNoDepartmentForManager, creator.Id));
                    if (current.Department == null)
                        throw new ValidationException(string.Format(StrNoDepartmentForManager, current.Id));
                    return current.Level + 1 == creator.Level &&
                           creator.Department.Path.StartsWith(current.Department.Path);
            }
        }
        protected void SetFlagsState(AppointmentEditModel model, bool state)
        {
            model.IsEditable = state;
            model.IsSaveAvailable = state;
            model.IsChiefApproveAvailable = state;
            model.IsManagerApproveAvailable = state;
            model.IsManagerRejectAvailable = state;
            model.IsPersonnelApproveAvailable = state;
            model.IsStaffApproveAvailable = state;
        }
        protected void LoadDictionaries(AppointmentEditModel model)
        {
            model.DepartmentRequiredLevel = 7;
            model.CommentsModel = GetCommentsModel(model.Id,RequestTypeEnum.Appointment);
            model.Types = new List<IdNameDto>
                              {
                                  new IdNameDto {Id = 0,Name = "Бессрочная"},
                                  new IdNameDto {Id = 1,Name = "Срочная"},
                              };
            model.Positions = PositionDao.LoadAllSorted().ToList().ConvertAll(x => new IdNameDto {Id = x.Id, Name = x.Name});
            model.Reasons = AppointmentReasonDao.LoadAll().ToList().ConvertAll(x => new IdNameDto { Id = x.Id, Name = x.Name });
            model.IsVacationExistsValues = new List<IdNameDto>
                              {
                                  new IdNameDto {Id = 1,Name = "Есть"},
                                  new IdNameDto {Id = 0,Name = "Нет"},
                              };
        }
        protected void SetHiddenFields(AppointmentEditModel model)
        {
            model.IsVacationExistsHidden = model.IsVacationExists;
            model.PositionIdHidden = model.PositionId;
            model.ReasonIdHidden = model.ReasonId;
            model.TypeIdHidden = model.TypeId;
            model.IsManagerApprovedHidden = model.IsManagerApproved;
            model.IsChiefApprovedHidden = model.IsChiefApproved;
            model.IsPersonnelApprovedHidden = model.IsPersonnelApproved;
            model.IsStaffApprovedHidden = model.IsStaffApproved;
            model.DateCreatedHidden = model.DateCreated;
        }
        protected void SetManagerInfoModel(User user, ManagerInfoModel model)
        {
            model.Department = user.Department == null ? string.Empty : user.Department.Name;
            if (user.Organization != null)
                model.Organization = user.Organization.Name;
            if (user.Position != null)
                model.Position = user.Position.Name;
            model.UserName = user.FullName;
        }
        public bool CheckDepartment(int departmentId,out int level)
        {
            level = 0;
            if (CurrentUser.UserRole != UserRole.Manager)
                return true;
            Department dep = DepartmentDao.Load(departmentId);
            if(dep == null)
                throw new ArgumentException(string.Format(StrDepartmentNotFound,departmentId));
            level = dep.ItemLevel.Value;
            if (dep.ItemLevel.Value != RequeredDepartmentLevel)
                return false;
            /*if (AuthenticationService.CurrentUser.UserRole == UserRole.Director)
                return true;*/
            User currUser = UserDao.Load(AuthenticationService.CurrentUser.Id);
            if(currUser == null)
                throw new ArgumentException(string.Format(StrUserNotFound, authenticationService.CurrentUser.Id));
            if (currUser.Level < MinManagerLevel || currUser.Level > MaxManagerLevel)
                throw new ValidationException(string.Format(StrIncorrectManagerLevel, currUser.Level, currUser.Id));
            List<DepartmentDto> departments;
            switch (currUser.Level)
            {
                case 2:
                    IList<int> managers2 = AppointmentDao.GetChildrenManager2ForManager2(currUser.Id);
                    if (managers2.Count > 0)
                        return true;
                    departments = AppointmentDao.GetDepartmentsForManager23(currUser.Id, 2, false).ToList();
                    return departments.Any(x => dep.Path.StartsWith(x.Path));
                case 3:
                    departments = AppointmentDao.GetDepartmentsForManager23(currUser.Id, 3, false).ToList();
                    return departments.Any(x => dep.Path.StartsWith(x.Path));
                default:
                    if (currUser.Department == null)
                        throw new ValidationException(string.Format(StrNoDepartmentForManager, currUser.Id));
                    return dep.Path.StartsWith(currUser.Department.Path);
            }
        }
        public void ReloadDictionaries(AppointmentEditModel model)
        {
            User user = UserDao.Load(model.UserId);
            SetManagerInfoModel(user, model);
            LoadDictionaries(model);
            if (model.Id == 0)
            {
                model.DateCreated = DateTime.Today.ToShortDateString();
            }
            else
            {
                Appointment entity = AppointmentDao.Load(model.Id);
                model.DocumentNumber = entity.Number.ToString();
                model.DateCreated = entity.CreateDate.ToShortDateString();
                if (entity.DeleteDate.HasValue)
                    model.IsDeleted = true;
                // no need to load ManagerFio and other because no error in model after manager approve
            }
        }
        public bool SaveAppointmentEditModel(AppointmentEditModel model, out string error)
        {
            error = string.Empty;
            User creator = null;
            try
            {
                creator = UserDao.Load(model.UserId);
                IUser current = AuthenticationService.CurrentUser;
                Appointment entity = null;
                if (model.Id != 0)
                {
                    entity = AppointmentDao.Load(model.Id);
                    if(entity == null)
                        throw new ValidationException(string.Format(StrAppointmentNotFound, model.Id));
                }
                /*if (!CheckUserMoRights(user, current, model.Id, entity, true))
                {
                    error = "Редактирование заявки запрещено";
                    return false;
                }*/

                if (model.Id == 0)
                {
                    entity = new Appointment
                    {
                        CreateDate = DateTime.Now,
                        Creator = creator,//UserDao.Load(current.Id),
                        Number = RequestNextNumberDao.GetNextNumberForType((int)RequestTypeEnum.Appointment),
                        EditDate = DateTime.Now,
                    };
                    ChangeEntityProperties(current, entity, model, creator,out error);
                    AppointmentDao.SaveAndFlush(entity);
                    model.Id = entity.Id;
                }
                else
                {
                    if (entity.Version != model.Version)
                    {
                        error = "Приказ был изменен другим пользователем.";
                        model.ReloadPage = true;
                        return false;
                    }
                    //if (model.IsDelete)
                    //{
                    //    if (current.UserRole == UserRole.OutsourcingManager)
                    //        entity.DeleteAfterSendTo1C = true;
                    //    entity.DeleteDate = DateTime.Now;
                    //    //missionOrder.CreateDate = DateTime.Now;
                    //    MissionOrderDao.SaveAndFlush(entity);
                    //    if (entity.Mission != null)
                    //    {
                    //        Mission mission = entity.Mission;
                    //        if (mission.SendTo1C.HasValue)
                    //            mission.DeleteAfterSendTo1C = true;
                    //        mission.DeleteDate = DateTime.Now;
                    //        mission.CreateDate = DateTime.Now;
                    //        MissionDao.SaveAndFlush(mission);
                    //    }
                    //    else
                    //        Log.WarnFormat("No mission for mission order with id {0}", entity.Id);
                    //    MissionReport report = MissionReportDao.GetReportForOrder(entity.Id);
                    //    if (report != null)
                    //    {
                    //        report.DeleteDate = DateTime.Now;
                    //        report.EditDate = DateTime.Now;
                    //        MissionReportDao.SaveAndFlush(report);
                    //    }
                    //    else
                    //        Log.WarnFormat("No mission report for mission order with id {0}", entity.Id);
                    //    /*SendEmailForUserRequest(missionOrder.User, current, missionOrder.Creator, true, missionOrder.Id,
                    //        missionOrder.Number, RequestTypeEnum.ChildVacation, false);*/
                    //    model.IsDelete = false;
                    //}
                    //else
                    //{
                    ChangeEntityProperties(current, entity, model, creator, out error);
                        //List<string> cityList = missionOrder.Targets.Select(x => x.City).ToList();
                        //string country = GetStringForList(cityList);
                        //List<string> orgList = missionOrder.Targets.Select(x => x.Organization).ToList();
                        //string org = GetStringForList(orgList);
                        AppointmentDao.SaveAndFlush(entity);
                        if (entity.Version != model.Version)
                        {
                            entity.EditDate = DateTime.Now;
                            AppointmentDao.SaveAndFlush(entity);
                        }
                    }
                    if (entity.DeleteDate.HasValue)
                        model.IsDeleted = true;
                //}
                model.DocumentNumber = entity.Number.ToString();
                model.Version = entity.Version;
                model.DateCreated = entity.CreateDate.ToShortDateString();
                SetFlagsState(entity.Id, UserDao.Load(current.Id),current.UserRole, entity, model);
                return true;
            }
            catch (Exception ex)
            {
                AppointmentDao.RollbackTran();
                Log.Error("Error on SaveAppointmentEditModel:", ex);
                error = StrException + ex.GetBaseException().Message;
                return false;
            }
            finally
            {
                SetManagerInfoModel(creator, model);
                LoadDictionaries(model);
                SetHiddenFields(model);
            }
        }
        protected void ChangeEntityProperties(IUser current, Appointment entity, AppointmentEditModel model, 
            User user,out string error)
        {
            error = string.Empty;
            //bool isDirectorManager = IsDirectorManagerForEmployee(user, current);
            User currUser = UserDao.Load(current.Id);
            if (!model.IsDelete && model.IsEditable)
            {
                entity.AdditionalRequirements = model.AdditionalRequirements;
                entity.Bonus = Decimal.Parse(model.Bonus);
                entity.City = model.City;
                entity.Compensation = model.Compensation;
                entity.Department = DepartmentDao.Load(model.DepartmentId);
                entity.DesirableBeginDate = DateTime.Parse(model.DesirableBeginDate);
                entity.EducationRequirements = model.EducationRequirements;
                entity.ExperienceRequirements = model.ExperienceRequirements;
                entity.IsVacationExists = model.IsVacationExists == 1?true:false;
                entity.OtherRequirements = model.OtherRequirements;
                entity.Period = model.Period;
                entity.Position = PositionDao.Load(model.PositionId);
                entity.Reason = AppointmentReasonDao.Load(model.ReasonId);
                entity.ReasonBeginDate = model.ReasonId != 3 ? DateTime.Parse(model.ReasonBeginDate) : new DateTime?();
                entity.ReasonPosition = model.ReasonPosition;
                entity.Responsibility = model.Responsibility;
                entity.Salary = Decimal.Parse(model.Salary);
                entity.Schedule = model.Schedule;
                entity.Type = model.TypeId == 1?true:false;
                entity.VacationCount = int.Parse(model.VacationCount);
            }
            switch (current.UserRole)
            {
                case UserRole.Manager:
                    if(current.Id == entity.Creator.Id)
                    {
                        if(model.IsManagerRejectAvailable && !entity.DeleteDate.HasValue
                            && model.IsDelete)
                        {
                            entity.DeleteDate = DateTime.Now;
                            entity.DeleteUser = currUser;
                            SendEmailForAppointmentReject(currUser, entity);
                            //todo need to send email and reject reports
                            //if(entity.AcceptStaff != null)
                            //    SendEmailForAppointmentReject(entity.AcceptStaff, entity);
                        }
                        if(!entity.DeleteDate.HasValue && model.IsManagerApproveAvailable 
                            && model.IsManagerApproved)
                        {
                            entity.ManagerDateAccept = DateTime.Now;
                            entity.AcceptManager = currUser;
                            EmailDto dto = SendEmailForAppointmentManagerAccept(entity.Creator, entity);
                            if (!string.IsNullOrEmpty(dto.Error))
                                error = string.Format("Заявка обработана успешно,но есть ошибка при отправке оповещений: {0}",
                                        dto.Error);
                        }
                    }
                    else if(IsManagerChiefForCreator(currUser,entity.Creator))
                    {
                        if (model.IsManagerRejectAvailable && !entity.DeleteDate.HasValue
                           && model.IsDelete)
                        {
                            entity.DeleteDate = DateTime.Now;
                            entity.DeleteUser = currUser;
                            SendEmailForAppointmentReject(currUser, entity);
                            //todo need to send email and reject reports
                            //if(entity.AcceptStaff != null)
                            //    SendEmailForAppointmentReject(entity.AcceptStaff, entity);
                        }
                        if (!entity.DeleteDate.HasValue && model.IsChiefApproveAvailable
                            && model.IsChiefApproved)
                        {
                            entity.ChiefDateAccept = DateTime.Now;
                            entity.AcceptChief = currUser;
                            EmailDto dto = SendEmailForAppointmentChiefAccept(currUser, entity);
                            if (!string.IsNullOrEmpty(dto.Error))
                                error = string.Format("Заявка обработана успешно,но есть ошибка при отправке оповещений: {0}",
                                        dto.Error);
                        }
                    }
                    break;
                /*case UserRole.Director:
                    if (current.Id == entity.Creator.Id)
                    {
                        if (model.IsManagerRejectAvailable && !entity.DeleteDate.HasValue
                             && model.IsDelete)
                        {
                            entity.DeleteDate = DateTime.Now;
                            entity.DeleteUser = currUser;
                            //todo need to send email and reject reports
                            //if(entity.AcceptStaff != null)
                            //    SendEmailForAppointmentReject(entity.AcceptStaff, entity);
                        }
                        if (!entity.DeleteDate.HasValue && model.IsManagerApproveAvailable
                            && model.IsManagerApproved)
                        {
                            entity.ManagerDateAccept = DateTime.Now;
                            entity.AcceptManager = currUser;
                            //SendEmailForAppointmentManagerAccept(entity.Creator, entity);
                        }
                    }
                    else if (IsDirectorChiefForCreator(currUser, entity.Creator))
                    {
                        if (model.IsManagerRejectAvailable && !entity.DeleteDate.HasValue
                           && model.IsDelete)
                        {
                            entity.DeleteDate = DateTime.Now;
                            entity.DeleteUser = currUser;
                            //todo need to send email and reject reports
                            //if(entity.AcceptStaff != null)
                            //    SendEmailForAppointmentReject(entity.AcceptStaff, entity);
                        }
                        if (!entity.DeleteDate.HasValue && model.IsChiefApproveAvailable
                            && model.IsChiefApproved)
                        {
                            entity.ChiefDateAccept = DateTime.Now;
                            entity.AcceptChief = currUser;
                            //SendEmailForAppointmentChiefAccept(currUser, entity);
                        }
                    }
                    break;*/
                case UserRole.PersonnelManager:
                    if (!entity.DeleteDate.HasValue && entity.ChiefDateAccept.HasValue &&
                        !entity.PersonnelDateAccept.HasValue 
                        && model.IsPersonnelApproveAvailable
                        && model.IsPersonnelApproved)
                    {
                        entity.PersonnelDateAccept = DateTime.Now;
                        entity.AcceptPersonnel = currUser;
                        EmailDto dto = SendEmailForAppointmentPersonnelAccept(currUser, entity);
                        if (!string.IsNullOrEmpty(dto.Error))
                            error = string.Format("Заявка обработана успешно,но есть ошибка при отправке оповещений: {0}",
                                    dto.Error);
                    }
                    break;
                case UserRole.StaffManager:
                    if (!entity.DeleteDate.HasValue && entity.PersonnelDateAccept.HasValue &&
                       !entity.StaffDateAccept.HasValue
                       && model.IsStaffApproveAvailable
                       && model.IsStaffApproved)
                    {
                        entity.StaffDateAccept = DateTime.Now;
                        entity.AcceptStaff = currUser;
                        //todo need to create report
                    }
                    break;
                case UserRole.OutsourcingManager:
                    break;
            }
        }
        #region Emails
        protected EmailDto SendEmailForAppointmentChiefAccept(User chief, Appointment entity)
        {
            string personnelManagerEmail = ConfigurationService.AppointmentPersonnelManagerEmail;
            if(string.IsNullOrEmpty(personnelManagerEmail))
                throw new ValidationException(StrEmailForPersonnalManagerNotFound);
            string body;
            string subject = GetSubjectAndBodyForAppointmentManagerAcceptRequest(chief, entity, out body);
            return SendEmail(personnelManagerEmail, subject, body);
        }
        protected EmailDto SendEmailForAppointmentPersonnelAccept(User personnel, Appointment entity)
        {
            string staffManagerEmail = ConfigurationService.AppointmentStaffManagerEmail;
            if (string.IsNullOrEmpty(staffManagerEmail))
                throw new ValidationException(StrEmailForStaffManagerNotFound);
            string body;
            string subject = GetSubjectAndBodyForAppointmentManagerAcceptRequest(personnel, entity, out body);
            return SendEmail(staffManagerEmail, subject, body);
        }
        protected EmailDto SendEmailForAppointmentManagerAccept(User creator, Appointment entity)
        {
            string to = string.Empty;
            IdNameDto user;
            List<IdNameDto> users;
            switch (creator.UserRole)
            {
                case UserRole.Manager:
                    switch (creator.Level)
                    {
                        case 2:
                            users = AppointmentDao.GetParentForManager2(creator.Id);
                            if (users.Count == 0)
                            {
                                Log.ErrorFormat(StrEmailForAppointmentManagerAcceptParent2NotFound, creator.Id);
                                return new EmailDto { Error = string.Format(StrEmailForAppointmentManagerAcceptParent2NotFound, creator.Id) };
                            }
                            foreach (IdNameDto usr in users)
                            {
                                if (string.IsNullOrEmpty(usr.Name))
                                    Log.WarnFormat("No email for manager (id {0})", usr.Id);
                                else
                                {
                                    if (string.IsNullOrEmpty(to))
                                        to = usr.Name;
                                    else
                                        to += ";" + usr.Name;
                                }
                            }
                            /*if (user == null)
                            {
                                Log.ErrorFormat(StrEmailForAppointmentManagerAcceptParent2NotFound, creator.Id);
                                return new EmailDto { Error = string.Format(StrEmailForAppointmentManagerAcceptParent2NotFound, creator.Id) };
                            }
                            if (string.IsNullOrEmpty(user.Name))
                            {
                                Log.ErrorFormat(StrEmailForAppointmentManagerAcceptNoEmail, user.Id);
                                return new EmailDto { Error = string.Format(StrEmailForAppointmentManagerAcceptNoEmail, user.Id) };
                            }
                            to = user.Name;*/
                            break;
                        case 3:
                            users = AppointmentDao.GetParentForManager3(creator.Id);
                            if (users.Count == 0)
                            {
                                Log.ErrorFormat(StrEmailForAppointmentManagerAcceptParent3NotFound, creator.Id);
                                return new EmailDto { Error = string.Format(StrEmailForAppointmentManagerAcceptParent3NotFound, creator.Id) };
                            }
                            foreach (IdNameDto usr in users)
                            {
                                if (string.IsNullOrEmpty(usr.Name))
                                    Log.WarnFormat("No email for manager (id {0})", usr.Id);
                                else
                                {
                                    if (string.IsNullOrEmpty(to))
                                        to = usr.Name;
                                    else
                                        to += ";" + usr.Name;
                                }
                            }
                            /*if(user == null)
                            {
                                Log.ErrorFormat(StrEmailForAppointmentManagerAcceptParent3NotFound, creator.Id);
                                return new EmailDto { Error = string.Format(StrEmailForAppointmentManagerAcceptParent3NotFound, creator.Id) };
                            }
                            if (string.IsNullOrEmpty(user.Name))
                            {
                                Log.ErrorFormat(StrEmailForAppointmentManagerAcceptNoEmail, user.Id);
                                return new EmailDto { Error = string.Format(StrEmailForAppointmentManagerAcceptNoEmail, user.Id) };
                            }
                            to = user.Name;*/
                            break;
                        case 4:
                            users = AppointmentDao.GetParentForManager4Department(creator.Department.Id);
                            if (users.Count == 0)
                            {
                                Log.ErrorFormat(StrEmailForAppointmentManagerAcceptParentNotFound, creator.Id);
                                return new EmailDto { Error = string.Format(StrEmailForAppointmentManagerAcceptParentNotFound, creator.Id) };
                            }
                            foreach (IdNameDto usr in users)
                            {
                                if (string.IsNullOrEmpty(usr.Name))
                                    Log.WarnFormat("No email for manager (id {0})", usr.Id);
                                else
                                {
                                    if (string.IsNullOrEmpty(to))
                                        to = usr.Name;
                                    else
                                        to += ";" + usr.Name;
                                }
                            }
                            
                            break;
                        case 5:
                        case 6:
                            users = AppointmentDao.GetParentForManagerDepartment(creator.Department.Id);
                            if (users.Count == 0)
                            {
                                Log.ErrorFormat(StrEmailForAppointmentManagerAcceptParentNotFound, creator.Id);
                                return new EmailDto { Error = string.Format(StrEmailForAppointmentManagerAcceptParentNotFound, creator.Id) };
                            }
                            foreach (IdNameDto usr in users)
                            {
                                if (string.IsNullOrEmpty(usr.Name))
                                    Log.WarnFormat("No email for manager (id {0})", usr.Id);
                                else
                                {
                                    if (string.IsNullOrEmpty(to))
                                        to = usr.Name;
                                    else
                                        to += ";"+ usr.Name;
                                }
                            }
                            break;
                        default:
                            throw new ArgumentException(string.Format(StrEmailForAppointmentManagerAcceptIncorrectManagerLevel, creator.Id));
                    }
                    break;
                default:
                    throw new ArgumentException(string.Format(StrEmailForAppointmentManagerAcceptIncorrectRole, creator.Id));
            }
            if (string.IsNullOrEmpty(to))
            {
                Log.ErrorFormat(StrEmailForAppointmentManagerAcceptNoEmails, creator.Id);
                return new EmailDto { Error = string.Format(StrEmailForAppointmentManagerAcceptNoEmails, creator.Id) };
            }
            string body;
            string subject = GetSubjectAndBodyForAppointmentManagerAcceptRequest(creator, entity, out body);
            return SendEmail(to, subject, body);
        }
        protected string GetSubjectAndBodyForAppointmentManagerAcceptRequest(User user, Appointment entity, out string body)
        {
            DepartmentDto dep3 = AppointmentDao.GetDepartmentForPathAndLevel(entity.Department.Path, 3);
            if (dep3 == null)
                Log.ErrorFormat(StrEmailForAppointmentManagerAcceptDepartment3NotFound,entity.Department.Id);
            body = string.Format(StrEmailForAppointmentManagerAcceptText,
                                 entity.Number,
                                 entity.Position.Name,
                                 dep3 == null?"<не найдено в базе данных>":dep3.Name,
                                 user.FullName);
            const string subject = StrEmailForAppointmentManagerAcceptSubject;
            return subject;
        }
        protected EmailDto SendEmailForAppointmentReject(User user, Appointment entity)
        {
            string to = string.Empty;
            if (entity.ManagerDateAccept.HasValue)
            {
                if (!string.IsNullOrEmpty(entity.AcceptManager.Email))
                    to += entity.AcceptManager.Email;
                else
                    Log.ErrorFormat("No email for manager (id {0})",entity.AcceptManager.Id);
            }
            if (entity.ChiefDateAccept.HasValue)
            {
                if (!string.IsNullOrEmpty(entity.AcceptChief.Email))
                {
                    if (string.IsNullOrEmpty(to))
                        to = entity.AcceptChief.Email;
                    else
                        to += ";" + entity.AcceptChief.Email;
                }
                else
                    Log.ErrorFormat("No email for chief (id {0})", entity.AcceptChief.Id);
            }
            if (entity.PersonnelDateAccept.HasValue)
            {
                if (!string.IsNullOrEmpty(entity.AcceptPersonnel.Email))
                {
                    if (string.IsNullOrEmpty(to))
                        to = entity.AcceptPersonnel.Email;
                    else
                        to += ";" + entity.AcceptPersonnel.Email;
                }
                else
                    Log.ErrorFormat("No email for personnel (id {0})", entity.AcceptPersonnel.Id);
            }
            if (entity.StaffDateAccept.HasValue)
            {
                if (!string.IsNullOrEmpty(entity.AcceptStaff.Email))
                {
                    if (string.IsNullOrEmpty(to))
                        to = entity.AcceptStaff.Email;
                    else
                        to += ";" + entity.AcceptStaff.Email;
                }
                else
                    Log.ErrorFormat("No email for staff (id {0})", entity.AcceptStaff.Id);
            }
            string body;
            string subject = GetSubjectAndBodyForAppointmentRejectRequest(user, entity, out body);
            return SendEmail(to, subject, body);
        }
        protected string GetSubjectAndBodyForAppointmentRejectRequest(User user, Appointment entity, out string body)
        {
            DepartmentDto dep3 = AppointmentDao.GetDepartmentForPathAndLevel(entity.Department.Path, 3);
            if (dep3 == null)
                Log.ErrorFormat(StrEmailForAppointmentManagerAcceptDepartment3NotFound, entity.Department.Id);
            body = string.Format(StrEmailForAppointmentManagerRejectText,
                                 entity.Number,
                                 entity.Position.Name,
                                 dep3 == null ? "<не найдена в базе данных>" : dep3.Name,
                                 user.FullName);
            const string subject = StrEmailForAppointmentManagerRejectSubject;
            return subject;
        }
        #endregion
        #region Comments
        public CommentsModel GetCommentsModel(int id, RequestTypeEnum typeId)
        {
            CommentsModel commentModel = new CommentsModel
            {
                RequestId = id,
                RequestTypeId = (int)typeId,
                Comments = new List<RequestCommentModel>(),
                IsAddAvailable = AuthenticationService.CurrentUser.UserRole == UserRole.PersonnelManager && id > 0,
            };
            if (id == 0)
                return commentModel;
            Appointment entity = AppointmentDao.Load(id);
            if ((entity.Comments != null) && (entity.Comments.Count() > 0))
            {
                commentModel.Comments = entity.Comments.OrderBy(x => x.DateCreated).ToList().
                    ConvertAll(x => new RequestCommentModel
                    {
                        Comment = x.Comment,
                        CreatedDate = x.DateCreated.ToString(),
                        Creator = x.User.FullName,
                    });
            }
            return commentModel;
            //Vacation vacation = VacationDao.Load(id);
        }
        public bool SaveComment(SaveCommentModel model)
        {
            try
            {
                if (AuthenticationService.CurrentUser.UserRole != UserRole.PersonnelManager)
                {
                    model.Error = StrCommentCreationDedied;
                    return false;
                }
                User user = UserDao.Load(AuthenticationService.CurrentUser.Id);
                Appointment entity = AppointmentDao.Load(model.DocumentId);
                AppointmentComment comment = new AppointmentComment
                {
                    Comment = model.Comment,
                    Appointment = entity,
                    DateCreated = DateTime.Now,
                    User = user,
                };
                AppointmentCommentDao.MergeAndFlush(comment);
                return true;
            }
            catch (Exception ex)
            {
                AppointmentCommentDao.RollbackTran();
                Log.Error("Exception", ex);
                model.Error = StrException + ex.GetBaseException().Message;
                return false;
            }
        }
        #endregion

        public AppointmentReportEditModel GetAppointmentReportEditModel(int id)
        {
            AppointmentReportEditModel model = new AppointmentReportEditModel { Id = id };
            AppointmentReport entity = null;
            User creator;
            IUser current = AuthenticationService.CurrentUser;
            User currUser = UserDao.Load(current.Id);
            if(id == 0)
                throw new ValidationException(StrAppointmentReportIncorrectId);
            entity = AppointmentReportDao.Get(id);
            if (entity == null)
                throw new ValidationException(string.Format(StrAppointmentReportNotFound, id));


            return model;
        }
    }
}