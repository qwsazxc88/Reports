using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
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
        public const string StrUserNotManager = "Вы (пользователь {0}) не являетесь руководителем,членом правления или сотрудником по подбору персонала - создание заявки запрещено";
        public const string StrIncorrectManagerLevel  = "Неправильный уровень {0} руководителя (id {1}) в базе данных.";
        public const string StrNoDepartmentForManager = "Не указано структурное подраздаление для руководителя (id {0}).";
        public const string StrIncorrectReasonId = "Неверное основание появления вакансии {0}.";
        public const string StrDepartmentNotFound = "Не найдено структурное подразделение (id {0}) в базе данных";
        public const string StrDepartmentLevelNotFound = "Не указан уровень структурного подразделения (id {0}) в базе данных";
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
        public const string StrAttachmentAlreadyExists = "Found existing attachment for appointment report id {0} and type {1} (id {2})";
        public const string StrAppointmentWasDeleted = "Заявка на подбор сотрудника отклонена, создание отчета невозможно";
        public const string CannotCreateReport = "Вам запрещено создание отчета";
        public const string StrOtherReportUserAccepted = "Дата приема на работу уже указана для другого кандидата (отчет {0})";
        public const string StrMultipleAccessError = "Отчет был изменен другим пользователем.";
        public const string StrReportWasRejected = "Отчет был отклонен.";
        public const string StrAppointmentWasRejected = "Заявка была отклонена.";
        public const string StrInvalidCommentType = "Неизвестный тип комментария {0}.";

        public const int MinManagerLevel = 2;
        public const int MaxManagerLevel = 6;
        public const int RequeredDepartmentLevel = 7;
        public const int PasswordLength = 8;

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
        protected IAppointmentEducationTypeDao appointmentEducationTypeDao;
        public IAppointmentEducationTypeDao AppointmentEducationTypeDao
        {
            get { return Validate.Dependency(appointmentEducationTypeDao); }
            set { appointmentEducationTypeDao = value; }
        }
        protected IRequestAttachmentDao requestAttachmentDao;
        public IRequestAttachmentDao RequestAttachmentDao
        {
            get { return Validate.Dependency(requestAttachmentDao); }
            set { requestAttachmentDao = value; }
        }

        protected IRoleDao roleDao;
        public IRoleDao RoleDao
        {
            get { return Validate.Dependency(roleDao); }
            set { roleDao = value; }
        }

        protected IAppointmentReportCommentDao appointmentReportCommentDao;
        public IAppointmentReportCommentDao AppointmentReportCommentDao
        {
            get { return Validate.Dependency(appointmentReportCommentDao); }
            set { appointmentReportCommentDao = value; }
        }

        protected IManualRoleRecordDao manualRoleRecordDao;
        public IManualRoleRecordDao ManualRoleRecordDao
        {
            get { return Validate.Dependency(manualRoleRecordDao); }
            set { manualRoleRecordDao = value; }
        }

        protected IEmploymentCommonDao employmentCommonDao;
        public IEmploymentCommonDao EmploymentCommonDao
        {
            get { return Validate.Dependency(employmentCommonDao); }
            set { employmentCommonDao = value; }
        	
        }

        

        #endregion
        protected IConfigurationService configurationService;
        public IConfigurationService ConfigurationService
        {
            set { configurationService = value; }
            get { return Validate.Dependency(configurationService); }
        }

        protected IEmploymentCandidateDao employmentCandidateDao;
        public IEmploymentCandidateDao EmploymentCandidateDao
        {
            get { return Validate.Dependency(employmentCandidateDao); }
            set { employmentCandidateDao = value; }
        }
        public object CopyAppointmentReport(int AppointmentNumber, int AppointmentReportId)
        {
            var apps=AppointmentDao.QueryExpression(x => x.Number == AppointmentNumber);
            if (apps != null && apps.Any())
            {
                var app = apps.First();
                if (!app.StaffDateAccept.HasValue)
                {
                    return new { status = "Error", message = "Заявка не принята в работу." };
                }
                if (app.NonActual)
                {
                    return new { status = "Error", message = "Нельзя копировать отчёт к не актуальной заявке." };
                }
                if (app.DeleteDate.HasValue)
                {
                    return new { status = "Error", message = "Нельзя копировать отчёт к отклоненной заявке." };
                }
                if (app.Recruter==2)
                {
                    return new { status = "Error", message = "Нельзя копировать отчёт к упрощенной заявке." };
                }
                var sourcereport = AppointmentReportDao.Load(AppointmentReportId);
                if (sourcereport == null) return new { status = "Error", message = "Отчёт не найден." };
                var newrepid = CreateAppointmentReport(app);
                var newrep = AppointmentReportDao.Load(newrepid);
                newrep.Name = sourcereport.Name;
                newrep.Phone = sourcereport.Phone;
                newrep.IsColloquyPassed = new bool?();
                newrep.ResumeCommentByOPINP = sourcereport.ResumeCommentByOPINP;
                newrep.Email = sourcereport.Email;                
                newrep.EducationTime = sourcereport.EducationTime;
                AppointmentReportDao.SaveAndFlush(newrep);
                RequestAttachment attach = RequestAttachmentDao.FindByRequestIdAndTypeId(sourcereport.Id, RequestAttachmentTypeEnum.AppointmentReport);
                if (attach != null)
                {
                    RequestAttachmentDao.CloneAttach(attach.Id, newrep.Id);
                }
                
                return new { status = "Ok", message = "Отчёт создан.", ReportId=newrep.Id };
            }
            else return new { status = "Error", message = "Заявка не найдена." };
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
            model.IsAddAvailable = (role & UserRole.Manager) == UserRole.Manager;
            model.IsAddForStaffAvailable = role == UserRole.StaffManager;
            //SetInitialStatus(model);
            //SetIsAvailable(model);
            return model;
        }
        public AppointmentListModel GetAppointmentReportListModel()
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
            SetDictionariesToReportsModel(model);
            model.IsAddAvailable = (role & UserRole.Manager) == UserRole.Manager;
            model.IsAddForStaffAvailable = role == UserRole.StaffManager;
            //SetInitialStatus(model);
            //SetIsAvailable(model);
            return model;
        }
        public void SetDictionariesToReportsModel(AppointmentListModel model)
        {
            model.Statuses = GetAppReportStatuses();
            var employmentBl=Ioc.Resolve<IEmploymentBl>();
            model.EmploymentStatuses = employmentBl.GetEmploymentStatuses();
        }
        public void SetDictionariesToModel(AppointmentListModel model)
        {
            model.Statuses = GetAppRequestStatuses();
            model.Reasons = AppointmentReasonDao.LoadAll().Select(x => new IdNameDto { Id = x.Id, Name = x.Name }).ToList();
            model.Reasons.Add(new IdNameDto{Id=0,Name="Все"});
        }
        public bool CheckUserDismissal(int userid)
        {
            return UserDao.CheckUserDismissal(userid);
        }
        protected List<IdNameDto> GetAppRequestStatuses()
        {
            //var requestStatusesList = RequestStatusDao.LoadAllSorted().ToList().ConvertAll(x => new IdNameDto(x.Id, x.Name));
            List<IdNameDto> moStatusesList = new List<IdNameDto>
                                                       {
                                                           new IdNameDto(1, "Черновик"),
                                                           new IdNameDto(2, "Отправлена на согласование вышестоящему руководителю"),
                                                           new IdNameDto(3, "Согласована вышестоящим руководителем"),
                                                           new IdNameDto(4, "Принята в работу"),
                                                           //new IdNameDto(5, "Отменена"),
                                                           new IdNameDto(6, "Нет вакансий"),
                                                           //new IdNameDto(4, "Не одобрен руководителем"),
                                                           //new IdNameDto(6, "Не одобрен бухгалтером"),
                                                           new IdNameDto(7, "Отправлена на согласование Специалисту УКДиУ"),
                                                           new IdNameDto(8, "Специалистом УКДиУ приостановленно согласование"),
                                                           new IdNameDto(9, "Не хватает свободных вакансий. Отправлена на согласование вышестоящему руководителю"),
                                                           new IdNameDto(10, "Согласована вышестоящим руководителем. Поиск сотрудника не требуется.")
                                                       }.OrderBy(x => x.Name).ToList();
            moStatusesList.Insert(0, new IdNameDto(0, SelectAll));
            return moStatusesList;
        }
        protected List<IdNameDto> GetAppReportStatuses()
        {
           List<IdNameDto> moStatusesList = new List<IdNameDto>
                                                       {
                                                           new IdNameDto(5, "Собеседование пройдено"),
                                                           new IdNameDto(6, "Обучение пройдено"),
                                                           new IdNameDto(7, "Обучение не пройдено"),
                                                           new IdNameDto(1, "Черновик"),
                                                           new IdNameDto(4, "Отправлена руководителю"),
                                                           new IdNameDto(3, "Кандидат выгружен в приём"),
                                                           new IdNameDto(2, "Отказано"),
                                                           new IdNameDto(8, "Welcome курс"),
                                                           new IdNameDto(9, "Собеседование назначено"),
                                                           new IdNameDto(10, "Входное тестирование"),
                                                           new IdNameDto(11,"Кандидат отказался от вакансии")
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
        public void SetAppointmentReportsListModel(AppointmentListModel model, bool hasError)
        {
            SetDictionariesToReportsModel(model);
            User user = UserDao.Load(AuthenticationService.CurrentUser.Id);
            if (hasError)
                model.Documents = new List<AppointmentDto>();
            else
                SetDocumentsReportsToModel(model, user);
        }
        public void SetDocumentsReportsToModel(AppointmentListModel model, User user)
        {
            UserRole role = AuthenticationService.CurrentUser.UserRole;
            //model.Documents = new List<AppointmentDto>();
            model.Documents = AppointmentDao.GetReportDocuments(
                user.Id,
                role,
                model.DepartmentId,
                model.StatusId,
                model.Number,
                model.BeginDate,
                model.EndDate,
                model.UserName,
                model.CandidateName,
                model.RecruteFio,
                model.SortBy,
                model.SortDescending);
        }
        public void SetDocumentsToModel(AppointmentListModel model, User user)
        {
            UserRole role = AuthenticationService.CurrentUser.UserRole;
            //model.Documents = new List<AppointmentDto>();
            model.Documents = AppointmentDao.GetDocuments(
                user.Id,
                role,
                model.DepartmentId,
                model.ReasonId,
                model.StatusId,
                model.Number,
                model.BeginDate,
                model.EndDate,
                model.UserName,
                model.RecruteFio,
                model.SortBy,
                model.SortDescending);
        }
        public NoteModel GetNoteModel(int id)
        {
            var appointment = AppointmentDao.Load(id);
            IList<string> dep = new List<string>();
            var dept = appointment.Department;

            for (int i = 3; i < dept.ItemLevel; i++)
            {
                var tmp = DepartmentDao.GetParentDepartmentWithLevel(dept, i);
                if(tmp!=null) dep.Add(tmp.Name);
            }
            dep.Add(dept.Name);
            var result = new NoteModel 
            { 
                Date=DateTime.Now,
                DateFrom=appointment.CreateDate,
                To="",
                From=appointment.AcceptManager.Name,
                Theme="Согласование кандидата на приём",
                Reason=appointment.Reason.Name,
                Position=appointment.PositionName,
                Departments=dep,
                PositionsCount=appointment.VacationCount,
                Salary=appointment.Salary,
                Premium=appointment.Bonus

            };
            return result;
        }
        public AppointmentEditModel GetAppointmentEditModel(int id,int? managerId)
        {
            AppointmentEditModel model = new AppointmentEditModel { Id = id };
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
                model.StaffCreatorId = entity.StaffCreator == null ? 0 : entity.StaffCreator.Id;
                model.StaffCreatorName = entity.StaffCreator == null ? "" : entity.StaffCreator.Name;
                model.isNeedToNotify = entity.isNotifyNeeded;
                model.FIO = entity.FIO;
                model.Priority = entity.Priority;
                model.PyrusNumber = entity.PyrusNumber;
                model.AppointmentEducationType = entity.AppointmentEducationTypeId;
                if(entity.Recruters!=null && entity.Recruters.Any())
                {
                    model.Recruter1id = entity.Recruters[0].Id;
                    if (entity.Recruters.Count > 1) model.Recruter2id = entity.Recruters[1].Id;
                    if (entity.Recruters.Count > 2) model.Recruter3id = entity.Recruters[2].Id;
                }
                model.Recruter = entity.Recruter;
                if (entity.Candidates != null && entity.Candidates.Any())
                {
                    model.Candidates = entity.Candidates.Select(x => new Reports.Core.Dto.Employment2.CandidateDto { Id = x.Id, Name = x.User.Name, EmploymentDate = (x.PersonnelManagers != null) ? x.PersonnelManagers.CompleteDate : null, Status = x.SendTo1C.HasValue ? "Выгружено в 1С" : "Не выгружено в 1С" }).ToList();
                }
                if (entity.Reports != null && entity.Reports.Any())
                {
                    model.Reports = entity.Reports.Select(x => new IdNameDto { Id = x.Id, Name = x.SecondNumber.ToString() }).ToList();
                }
                //model.AdditionalRequirements = entity.AdditionalRequirements;
                model.ShowStaff = entity.Recruter == 1;
                if(entity.BankAccountant!=null)
                model.BankAccountantName = entity.BankAccountant.Name;
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
                model.BankAccountantAccept = entity.BankAccountantAccept.HasValue?entity.BankAccountantAccept.Value:false;
                model.BankAccountantAcceptCount = entity.BankAccountantAcceptCount;
                //model.Period = entity.Period;
                model.PositionName = entity.PositionName;
                model.ReasonId = entity.Reason.Id;
                // hardcode using database Ids from [dbo].[AppointmentReason] 
                switch (entity.Reason.Id)
                {
                    case 1:
                    case 2:
                    //case 3:
                        model.ReasonPosition = null;
                        model.ReasonBeginDate = FormatDate(entity.ReasonBeginDate);
                        break;
                    case 4:
                    case 5:
                        model.ReasonPosition = entity.ReasonPosition;
                        if(entity.ReasonPositionUser!=null)
                            model.ReasonPositionId = entity.ReasonPositionUser.Id;
                        model.ReasonBeginDate = FormatDate(entity.ReasonBeginDate);
                        break;
                    case 7:
                    case 6:
                    case 3:
                        model.ReasonPosition = entity.ReasonPosition;
                        if (entity.ReasonPositionUser != null)
                            model.ReasonPositionId = entity.ReasonPositionUser.Id;
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
                model.UserId = entity.Creator.Id;
                model.VacationCount = entity.VacationCount.ToString();
                model.Version = entity.Version;
            }
            else
            {
                creator =  managerId.HasValue? UserDao.Load(managerId.Value) : currUser;
                model.ShowStaff = true;
                model.Recruter = 1;
                model.AppointmentEducationType = 1;
                model.IsVacationExists = 1;
                SetCreatorDepartment(creator, model);
                model.UserId = creator.Id;//currUser.Id;
                if(managerId.HasValue)
                    model.StaffCreatorId = currUser.Id;
            }
            //if (!CheckUserRights(current, id, entity, false)) todo ???
            //    throw new ArgumentException(StrAccessIsDenied);
            SetManagerInfoModel(creator, model,entity,null);
            LoadDictionaries(model);
            SetFlagsState(id, currUser,current.UserRole, entity, model);
            SetHiddenFields(model);
            if ((current.UserRole & UserRole.Manager) != UserRole.Manager 
                && model.StaffCreatorId != current.Id
                && current.UserRole != UserRole.ConsultantPersonnel
                &&current.UserRole != UserRole.StaffManager
                )
            { model.IsEditable = false; model.IsSaveAvailable = false; }
            return model;
        }
        protected void SetCreatorDepartment(User creator,AppointmentEditModel model)
        {
            if ((creator.UserRole & UserRole.Manager) == UserRole.Manager)
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
            if (entity != null)
            {
                model.NonActual = entity.NonActual;
                var candidates = entity.Candidates!=null?entity.Candidates.Where(x => x.Status != Reports.Core.Enum.EmploymentStatus.REJECTED):null;
                if (candidates == null || !candidates.Any())
                {
                    model.IsNonActualButtonAvailable = true;
                }
            }
            model.StaffBossId = ConfigurationService.StaffBossId.HasValue?ConfigurationService.StaffBossId.Value:0;
            if(model.Id == 0)
            {
                if ((currRole & UserRole.Manager) != UserRole.Manager && currRole!= UserRole.StaffManager)
                    throw new ArgumentException(string.Format(StrUserNotManager, current.Id));
                model.IsEditable = true;
                model.IsSaveAvailable = true;
                model.IsManagerApproveAvailable = true;
                model.ApproveForAllAvailable = true;
                return;
            }
            model.IsManagerApproved = entity.ManagerDateAccept.HasValue; 
            model.IsChiefApproved = entity.ChiefDateAccept.HasValue; 
            //model.IsPersonnelApproved = entity.PersonnelDateAccept.HasValue;
            model.IsStaffApproved = entity.StaffDateAccept.HasValue;
            model.IsDeleted = entity.DeleteDate.HasValue;
            if (entity.AcceptManager != null && entity.ManagerDateAccept.HasValue)
                model.ManagerFio = entity.AcceptManager.FullName;
            if (entity.AcceptChief != null && entity.ChiefDateAccept.HasValue)
                model.ChiefFio = entity.AcceptChief.FullName;
            //if (entity.AcceptPersonnel != null && entity.PersonnelDateAccept.HasValue)
            //    model.PersonnelFio = entity.AcceptPersonnel.FullName;
            if (entity.AcceptStaff != null && entity.StaffDateAccept.HasValue)
                model.StaffFio = entity.AcceptStaff.FullName;

            bool isApprovedReportExists = AppointmentReportDao.IsApprovedReportForAppointmentIdExists(entity.Id);
            if(entity.DeleteDate.HasValue)
            {
                model.DeleteUser = entity.DeleteUser.FullName;
                if (entity.AcceptStaff != null)
                    model.IsStaffReceiveRejectMail = true;
                // todo Need flag on entity ?
                /*if (entity.AcceptStaff != null)
                    model.StaffFio = entity.AcceptStaff.FullName;*/
            }
            switch (currRole)
            {
                case UserRole.Manager:
                    if(current.Id == entity.Creator.Id && !entity.DeleteDate.HasValue)
                    {
                            if(!isApprovedReportExists)
                                model.IsManagerRejectAvailable = true;
                            if (!entity.ManagerDateAccept.HasValue)
                            {
                                model.IsManagerApproveAvailable = true;
                                model.IsEditable = true;
                            }
                            else
                            {
                                if (entity.BankAccountantAccept.HasValue)
                                {
                                    model.IsChiefApproveAvailable = IsManagerChiefForCreator(current, entity.Creator) && entity.BankAccountantAccept.Value;
                                    
                                }
                                else
                                { if (entity.Reason.Id == 3 || entity.Reason.Id == 6)  model.IsChiefApproveAvailable = IsManagerChiefForCreator(current, entity.Creator); }
                            }
                    }
                    else if (!entity.DeleteDate.HasValue 
                            && entity.ManagerDateAccept.HasValue 
                            && entity.Creator.Id != current.Id
                            && IsManagerChiefForCreator(current, entity.Creator))
                    {
                            if (!isApprovedReportExists)
                                model.IsManagerRejectAvailable = true;
                            if (!entity.ChiefDateAccept.HasValue && entity.BankAccountantAccept.HasValue && entity.IsVacationExists && entity.BankAccountantAcceptCount>0)
                                model.IsChiefApproveAvailable = true;
                            else if (!entity.ChiefDateAccept.HasValue)
                            {
                                if (entity.Reason.Id == 3 || entity.Reason.Id == 6) model.IsChiefApproveAvailable = IsManagerChiefForCreator(current, entity.Creator);
                            }
                    }
                    break;
                case UserRole.StaffManager:
                    if (!entity.DeleteDate.HasValue )
                    {
                        if(entity.ChiefDateAccept.HasValue && !entity.StaffDateAccept.HasValue)
                            model.IsStaffApproveAvailable = true;
                        if (!entity.StaffDateAccept.HasValue && model.StaffCreatorId == current.Id)
                        {
                            model.ApproveForAllAvailable = true;
                            if (!isApprovedReportExists)
                                model.IsManagerRejectAvailable = true;
                            if (!entity.ManagerDateAccept.HasValue)
                            {
                                model.IsEditable = true;
                                model.IsManagerApproveAvailable = true;
                            }                            
                        }
                    }
                    break;
                case UserRole.Estimator:
                case UserRole.OutsourcingManager:
                    break;
                case UserRole.ConsultantPersonnel:
                case UserRole.Security:
                case UserRole.PersonnelManager:    
                    break;
                default:
                    throw new ArgumentException(string.Format("Недопустимая роль {0}",currRole));
            }

            if ((entity.Creator.Id == current.Id || (entity.StaffCreator!=null && entity.StaffCreator.Id==current.Id)) && !entity.ManagerDateAccept.HasValue)
            {
                model.IsEditable = true;
                model.IsManagerApproveAvailable = true;
            }
            model.IsSaveAvailable = model.IsEditable || model.IsManagerApproveAvailable || ((CurrentUser.UserRole & UserRole.StaffManager)>0)
                || model.IsChiefApproveAvailable || model.IsStaffApproveAvailable || (currRole == UserRole.ConsultantPersonnel && (model.IsVacationExists != 1 || model.BankAccountantAccept != true || model.BankAccountantAcceptCount < int.Parse(model.VacationCount)));
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
            var chiefs = GetChiefsForManager(creator.Id);
            //foreach(var el in manualchiefs)

            if (chiefs.Contains(current) ) return true;

            if (!current.Level.HasValue || current.Level < MinManagerLevel || current.Level > MaxManagerLevel)
                throw new ValidationException(string.Format(StrIncorrectManagerLevel,
                        current.Level.HasValue ? current.Level.Value.ToString() : "<не указан>", current.Id));
            if (!creator.Level.HasValue || creator.Level < MinManagerLevel || creator.Level > MaxManagerLevel)
                throw new ValidationException(string.Format(StrIncorrectManagerLevel,
                        creator.Level.HasValue ? creator.Level.Value.ToString() : "<не указан>", creator.Id));
            List<DepartmentDto> departments;
            switch (current.Level)
            {
                case 2:
                    IList<int> managers2 = AppointmentDao.GetChildrenManager2ForManager2(current.Id);
                    if (managers2.Any(x => x == creator.Id && creator.Level.Value == 2))
                        return true;
                    IList<int> managers = AppointmentDao.GetManager3ForManager2(current.Id);
                    if (managers.Any(x => x == creator.Id && creator.Level.Value == 3))
                        return true;
                    departments = AppointmentDao.GetDepartmentsForManager23(current.Id, 2, true).ToList();
                    return departments.Any(x => creator.Department.Path.StartsWith(x.Path) && creator.Level == 4);
                /*case 3:
                    if (creator.Level != 4)
                        return false;
                    departments = AppointmentDao.GetDepartmentsForManager23(current.Id, 3, false).ToList();
                    return departments.Any(x => creator.Department.Path.StartsWith(x.Path));
                */
                case 3:
                    // Для руководителей 3 уровня получаем список ручных привязок к подразделениям
                    return ManualRoleRecordDao.GetRoleRecords(user: current, roleCode: "000000037")
                        .Any(roleRecord => (roleRecord.TargetDepartment != null && creator.Department.Path.StartsWith(roleRecord.TargetDepartment.Path)));
                case 4:
                case 5:
                    return creator.Department.Path.StartsWith(current.Department.Path)
                        && creator.Department.Path.Length > current.Department.Path.Length;
                default:
                    if (creator.Department == null)
                        throw new ValidationException(string.Format(StrNoDepartmentForManager, creator.Id));
                    if (current.Department == null)
                        throw new ValidationException(string.Format(StrNoDepartmentForManager, current.Id));
                    return false;
            }
        }

        protected void SetFlagsState(AppointmentEditModel model, bool state)
        {
            model.IsEditable = state;
            model.IsSaveAvailable = state;
            model.IsChiefApproveAvailable = state;
            model.IsManagerApproveAvailable = state;
            model.IsManagerRejectAvailable = state;
            //model.IsPersonnelApproveAvailable = state;
            model.IsStaffApproveAvailable = state;
            model.ApproveForAllAvailable = state;
        }
        protected void LoadDictionaries(AppointmentEditModel model)
        {
            model.Priorities = new List<IdNameDto>
            {
                new IdNameDto{Id=0, Name="-"},
                new IdNameDto{Id=1, Name="Срочно"},
                new IdNameDto{Id=2, Name="Очень срочно"}
            };
            var creator=UserDao.Load(model.UserId);
            if (creator != null && creator.Department != null)
                model.UsersList = UserDao.GetUsersForManagerNotDismissed(creator.Id, UserRole.Manager, creator.Department.Id,model.DateCreated!=null?model.DateCreated:DateTime.Now.ToShortDateString());
            if( model.ReasonPositionId>0 && model.UsersList!=null && !model.UsersList.Any(x=>x.Id==model.ReasonPositionId))
            {
                var u = UserDao.Load(model.ReasonPositionId);
                model.UsersList.Add(new IdNameDto { Id = u.Id, Name = u.Name });
            }
            if( model.Id>0 && model.DateCreated!= null && DateTime.Parse(model.DateCreated)<new DateTime(2015,7,22))
                model.UsersList.Add(new IdNameDto{ Id = 0, Name ="-"});
            model.Personnels = EmploymentCandidateDao.GetPersonnels();
            model.AppointmentEducationTypes=AppointmentEducationTypeDao.LoadAll().Select(x=>new IdNameDto{ Id=x.Id, Name=x.Name}).ToList();
            model.Recruters = UserDao.GetStaffList().Select(x => new IdNameDto { Id = x.Id, Name = x.Name }).ToList();
            model.Recruters.Add(new IdNameDto { Id = 0, Name = "-" });
            model.DepartmentRequiredLevel = 7;
            model.CommentsModel = GetCommentsModel(model.Id,RequestTypeEnum.Appointment);
            model.Types = new List<IdNameDto>
                              {
                                  new IdNameDto {Id = 0,Name = "Бессрочный"},
                                  new IdNameDto {Id = 1,Name = "Срочный"},
                              };
            //model.Positions = PositionDao.LoadAllSorted().ToList().ConvertAll(x => new IdNameDto {Id = x.Id, Name = x.Name});
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
            //model.PositionIdHidden = model.PositionId;
            model.ReasonIdHidden = model.ReasonId;
            model.TypeIdHidden = model.TypeId;
            model.IsManagerApprovedHidden = model.IsManagerApproved;
            model.IsChiefApprovedHidden = model.IsChiefApproved;
            //model.IsPersonnelApprovedHidden = model.IsPersonnelApproved;
            model.IsStaffApprovedHidden = model.IsStaffApproved;
            //model.DateCreatedHidden = model.DateCreated;
        }
        protected void SetManagerInfoModel(User user, ManagerInfoModel model,Appointment appointment,
            AppointmentReport appointmentReport)
        {
            if(appointmentReport != null)
                model.StaffName = appointmentReport.Creator.FullName;
            else if(appointment != null)
            {
                AppointmentReport rep = AppointmentReportDao.LoadForAppointmentId(appointment.Id).FirstOrDefault();
                if(rep != null)
                    model.StaffName = rep.Creator.FullName;
            }

            #region Заполнение списка вышестоящих руководителей

            IList<User> chiefs = GetChiefsForManager(user.Id)
                //.Where<User>(chief => chief.Level >= 4)
                .OrderByDescending<User, int?>(chief => chief.Level)
                .ToList<User>();

            // + руководители по ручным привязкам
            IList<User> manualRoleManagers = ManualRoleRecordDao.GetManualRoleHoldersForUser(user.Id, UserManualRole.ApprovesEmployment);
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

            model.Chiefs = chiefsBuilder.ToString();

            #endregion
            if(appointment!=null)
            model.DocumentNumber = appointment.Number.ToString();
            model.Department = user.Department == null ? string.Empty : user.Department.Name;
            model.Organization = user.Organization != null ? user.Organization.Name : string.Empty;
            model.Position = user.Position != null ? user.Position.Name : string.Empty;
            model.UserName = user.FullName;
            model.ManagerId = user.Id;
        }
        public bool CheckDepartment(AppointmentEditModel model,out int level)
        {
            level = 0;
            int departmentId = model.DepartmentId;
            if ((CurrentUser.UserRole & UserRole.Manager) != UserRole.Manager && model.StaffCreatorId != CurrentUser.Id)
                return true;
            Department dep = DepartmentDao.Load(departmentId);
            if(dep == null)
                throw new ArgumentException(string.Format(StrDepartmentNotFound,departmentId));
            if (!dep.ItemLevel.HasValue)
                throw new ArgumentException(string.Format(StrDepartmentLevelNotFound, departmentId));
            level = dep.ItemLevel.Value;
            if (dep.ItemLevel.Value != RequeredDepartmentLevel)
                return false;

            User currUser;
            if ((CurrentUser.UserRole & UserRole.Manager) > 0) currUser = UserDao.Load(CurrentUser.Id);
            else currUser = UserDao.Load(model.UserId);// если еще раз тут будешь, вспомни, что Улькина может создавать за сотрудника
            if (CurrentUser.UserRole == UserRole.StaffManager) return true;
            if(currUser == null)
                throw new ArgumentException(string.Format(StrUserNotFound, model.UserId));
            if (currUser.Level < MinManagerLevel || currUser.Level > MaxManagerLevel)
                throw new ValidationException(string.Format(StrIncorrectManagerLevel, currUser.Level, currUser.Id));
            List<DepartmentDto> departments;

            try//ЗАПЛАТКА
            {
                var mr=ManualRoleRecordDao.QueryExpression(x => x.User.Id == CurrentUser.Id && dep.Path.Contains(x.TargetDepartment.Path));
                if (mr != null && mr.Any()) return true;
            }
            catch (Exception e) { }

            if (currUser.Department != null && dep.Path.StartsWith(currUser.Department.Path)) return true;
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
            Appointment entity = null;
            LoadDictionaries(model);
            if (model.Id == 0)
            {
                model.DateCreated = DateTime.Today.ToShortDateString();
            }
            else
            {
                entity = AppointmentDao.Load(model.Id);
                model.DocumentNumber = entity.Number.ToString();
                model.DateCreated = entity.CreateDate.ToShortDateString();
                if (entity.DeleteDate.HasValue)
                    model.IsDeleted = true;
                // no need to load ManagerFio and other because no error in model after manager approve
            }
            SetManagerInfoModel(user, model,entity,null);
            
        }
        public bool SaveAppointmentEditModel(AppointmentEditModel model, out string error)
        {
            error = string.Empty;
            User creator = null;
            Appointment entity = null;

            try
            {
                creator = UserDao.Load(model.UserId);
                IUser current = AuthenticationService.CurrentUser;
               
                if (model.Id == 0)
                {
                    entity = new Appointment
                    {
                        Recruter = model.Recruter,
                        CreateDate = DateTime.Now,
                        Creator = creator,//UserDao.Load(current.Id),
                        Number = RequestNextNumberDao.GetNextNumberForType((int)RequestTypeEnum.Appointment),
                        EditDate = DateTime.Now,
                        AppointmentEducationTypeId=model.AppointmentEducationType
                    };
                    if ((CurrentUser.UserRole & UserRole.StaffManager) > 0) entity.StaffCreator = UserDao.Load(CurrentUser.Id);
                    ChangeEntityProperties(current, entity, model, creator,out error);
                    AppointmentDao.SaveAndFlush(entity);
                    model.Id = entity.Id;
                }
                else
                {
                    entity = AppointmentDao.Get(model.Id);
                    if (entity == null)
                        throw new ValidationException(string.Format(StrAppointmentNotFound, model.Id));
                    if (entity.Version != model.Version)
                    {
                        error = "Заявка была изменена другим пользователем.";
                        model.ReloadPage = true;
                        return false;
                    }
                    ChangeEntityProperties(current, entity, model, creator, out error);
                    AppointmentDao.SaveAndFlush(entity);
                    if (entity.Version != model.Version)
                    {
                        entity.EditDate = DateTime.Now;
                        AppointmentDao.SaveAndFlush(entity);
                    }
                }
                if (entity.DeleteDate.HasValue)
                    model.IsDeleted = true;
                if (entity.Reports != null && entity.Reports.Any())
                {
                    model.Reports = entity.Reports.Select(x => new IdNameDto { Id = x.Id, Name = x.SecondNumber.ToString() }).ToList();
                }
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
                SetManagerInfoModel(creator, model,entity,null);
                LoadDictionaries(model);
                SetHiddenFields(model);
            }
        }
        protected void RejectAppointment(Appointment entity)
        {
            entity.AcceptChief = null;
            entity.AcceptManager = null;
            entity.AcceptStaff = null;
            entity.BankAccountantAccept = null;
            entity.ChiefDateAccept = null;
            entity.ManagerDateAccept = null;
            entity.StaffDateAccept = null;                            
        }
        /// <summary>
        /// Заплатка для старых заявок, для которых не создались отчёты
        /// </summary>
        /// <param name="id"></param>
        public int CreateReportForOldAppointment(int id)
        {
            var entity = AppointmentDao.Load(id);
            if (entity.Recruter == 2)
            {
                entity.AcceptStaff = entity.Creator;
            }

            return CreateAppointmentReport(entity);
        }
        protected void ChangeEntityProperties(IUser current, Appointment entity, AppointmentEditModel model, 
            User user,out string error)
        {
            error = string.Empty;
            User currUser = UserDao.Load(current.Id);
            if (!model.IsDelete && (model.IsEditable || model.IsSaveAvailable))
            {
                //entity.AdditionalRequirements = model.AdditionalRequirements;
                entity.FIO = model.FIO;
                entity.isNotifyNeeded=model.isNeedToNotify;
                entity.Bonus = Decimal.Parse(model.Bonus);
                entity.PyrusNumber = model.PyrusNumber;
                entity.City = model.City;
                entity.Compensation =(String.IsNullOrWhiteSpace(model.Compensation))?"-":model.Compensation;
                entity.Department = DepartmentDao.Load(model.DepartmentId);
                entity.EducationRequirements = (String.IsNullOrWhiteSpace(model.EducationRequirements))?"-":model.EducationRequirements;
                entity.ExperienceRequirements =(String.IsNullOrWhiteSpace( model.ExperienceRequirements))?"-":model.ExperienceRequirements;
                entity.OtherRequirements =(String.IsNullOrWhiteSpace( model.OtherRequirements))?"-":model.OtherRequirements;
                //entity.Period = model.Period;
                entity.PositionName = model.PositionName;//PositionDao.Load(model.PositionId);
                entity.Reason = AppointmentReasonDao.Load(model.ReasonId);
                entity.ReasonBeginDate = (model.ReasonId != 3 && !String.IsNullOrWhiteSpace(model.ReasonBeginDate)) ? DateTime.Parse(model.ReasonBeginDate) : new DateTime?();
                entity.DesirableBeginDate = model.ShowStaff ? DateTime.Parse(model.DesirableBeginDate) : entity.ReasonBeginDate.HasValue ? entity.ReasonBeginDate.Value+TimeSpan.FromDays(14) : DateTime.Now;
                //entity.AppointmentEducationTypeId = model.AppointmentEducationType;
                entity.ReasonPosition =  model.ReasonId != 1 && model.ReasonId != 2 ?model.ReasonPosition:null;
                if (model.ReasonId > 2 && model.ReasonId < 7 && model.ReasonPositionId>0)
                {
                    entity.ReasonPositionUser = UserDao.Load(model.ReasonPositionId);
                }
                entity.Responsibility = (String.IsNullOrWhiteSpace(model.Responsibility))?"-":model.Responsibility;
                entity.Salary = Decimal.Parse(model.Salary);
                entity.Schedule = (String.IsNullOrWhiteSpace(model.Schedule))?"-":model.Schedule;
                entity.Type = model.TypeId == 1?true:false;
                if (current.UserRole == UserRole.ConsultantPersonnel)
                {
                    entity.BankAccountant = UserDao.Load(current.Id);
                    entity.IsVacationExists = model.IsVacationExists == 1 ? true : false;
                    model.BankAccountantAccept = true;
                    entity.BankAccountantAccept = model.BankAccountantAccept;
                    entity.BankAccountantAcceptCount = model.BankAccountantAcceptCount;
                }
                entity.VacationCount = int.Parse(model.VacationCount);
                if (model.StaffCreatorId != 0)
                    entity.StaffCreator = UserDao.Load(model.StaffCreatorId);
            }
            switch (current.UserRole)
            {
                case UserRole.ConsultantPersonnel:
                    if (entity.BankAccountantAccept.HasValue && entity.IsVacationExists)
                    {
                        EmailDto dto = SendEmailForAppointmentManagerAccept(entity.Creator, entity);
                        if (!string.IsNullOrEmpty(dto.Error))
                            error = string.Format("Заявка обработана успешно,но есть ошибка при отправке оповещений: {0}",
                                    dto.Error);
                    }
                    if (entity.BankAccountantAccept.HasValue && !entity.IsVacationExists)
                    {
                        EmailDto dto = SendEmailForBankAccountantReject(entity.Creator, entity);
                        if (!string.IsNullOrEmpty(dto.Error))
                            error = string.Format("Заявка обработана успешно,но есть ошибка при отправке оповещений: {0}",
                                    dto.Error);
                        
                    }
                    break;

                case UserRole.Manager:
                    if(current.Id == entity.Creator.Id)
                    {
                        if (model.NonActual)
                        {
                            entity.NonActual = model.NonActual;
                            EmploymentCandidateDao.CancelCandidatesByAppointmentId(model.Id);
                            RejectReports(entity.Id, currUser, "Заявка отклонена");
                        }
                        if(model.IsManagerRejectAvailable && !entity.DeleteDate.HasValue
                            && model.IsDelete)
                        {
                            /*entity.DeleteDate = DateTime.Now;
                            entity.DeleteUser = currUser;*/
                            RejectAppointment(entity);
                            EmailDto dto = SendEmailForAppointmentReject(currUser, entity);
                            if (!string.IsNullOrEmpty(dto.Error))
                                error = string.Format("Заявка обработана успешно,но есть ошибка при отправке оповещений: {0}",dto.Error);
                            RejectReports(entity.Id, currUser, "Заявка отклонена");
                            //if(entity.AcceptStaff != null)
                            //    SendEmailForAppointmentReject(entity.AcceptStaff, entity);
                        }
                        if(!entity.DeleteDate.HasValue && model.IsManagerApproveAvailable 
                            && model.IsManagerApproved)
                        {
                            entity.ManagerDateAccept = DateTime.Now;
                            entity.AppointmentEducationTypeId = model.AppointmentEducationType;
                            entity.AcceptManager = currUser;
                            //entity.AppointmentEducationTypeId = model.AppointmentEducationType;
                            EmailDto dto = SendEmailForBankAccountant(entity.Creator, entity); //dto = SendEmailForAppointmentManagerAccept(entity.Creator, entity);
                            if (!string.IsNullOrEmpty(dto.Error))
                                error = string.Format("Заявка обработана успешно,но есть ошибка при отправке оповещений: {0}",
                                        dto.Error);
                            var dto2=SendEmailForStaffManager(entity);
                            if (!string.IsNullOrEmpty(dto2.Error))
                                error = string.Format("Заявка обработана успешно,но есть ошибка при отправке оповещений: {0}",
                                        dto2.Error);
                        }
                        if(IsManagerChiefForCreator(currUser,entity.Creator))
                            if (!entity.DeleteDate.HasValue && model.IsChiefApproveAvailable
                            && model.IsChiefApproved)
                            {
                                entity.ChiefDateAccept = DateTime.Now;
                                entity.AcceptChief = currUser;
                                EmailDto dto = SendEmailForAppointmentChiefAccept(currUser, entity);
                                if (!string.IsNullOrEmpty(dto.Error))
                                    error = string.Format("Заявка обработана успешно,но есть ошибка при отправке оповещений: {0}",
                                            dto.Error);
                                if (entity.Recruter == 2)
                                {
                                    entity.AcceptStaff = entity.Creator;
                                    CreateAppointmentReport(entity);
                                }
                            }
                    }
                    else if(IsManagerChiefForCreator(currUser,entity.Creator))
                    {
                        if (model.NonActual)
                        {
                            entity.NonActual = model.NonActual;
                            EmploymentCandidateDao.CancelCandidatesByAppointmentId(model.Id);
                            RejectReports(entity.Id, currUser, "Заявка отклонена");
                        }
                        if (model.IsManagerRejectAvailable && !entity.DeleteDate.HasValue
                           && model.IsDelete)
                        {
                            /*entity.DeleteDate = DateTime.Now;
                            entity.DeleteUser = currUser;*/
                            RejectAppointment(entity);
                            EmailDto dto = SendEmailForAppointmentReject(currUser, entity);
                            if (!string.IsNullOrEmpty(dto.Error))
                                error = string.Format("Заявка обработана успешно,но есть ошибка при отправке оповещений: {0}",dto.Error);
                            RejectReports(entity.Id, currUser, "Заявка отклонена");
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
                            if (entity.Recruter == 2)
                            {
                                entity.AcceptStaff = entity.Creator;
                                CreateAppointmentReport(entity);
                            }
                        }
                    }
                    break;
                
                case UserRole.StaffManager:
                    if (model.AppointmentEducationType != entity.AppointmentEducationTypeId)
                    {
                        entity.AppointmentEducationTypeId = model.AppointmentEducationType;
                        AppointmentReportDao.Update(x => x.Appointment.Id == entity.Id, y => y.Type = AppointmentEducationTypeDao.Load(entity.AppointmentEducationTypeId));
                    }
                    if (model.NonActual)
                    {
                        entity.NonActual = model.NonActual;
                        EmploymentCandidateDao.CancelCandidatesByAppointmentId(model.Id);
                        RejectReports(entity.Id, currUser, "Заявка отклонена");
                    }
                    if (!entity.DeleteDate.HasValue)
                    {
                        entity.NonActual = model.NonActual;
                        if (model.StaffCreatorId == current.Id || entity.Creator.Id == current.Id)
                        {
                            if (model.ApproveForAll)
                            {
                                entity.ManagerDateAccept = DateTime.Now;
                                entity.AcceptManager = currUser;
                                entity.ChiefDateAccept = DateTime.Now;
                                entity.AcceptChief = currUser;
                                entity.StaffDateAccept = DateTime.Now;
                                entity.AcceptStaff = currUser;
                                if (entity.Id == 0)
                                    AppointmentDao.SaveAndFlush(entity);
                                CreateAppointmentReport(entity);
                            }
                            else if (model.IsManagerRejectAvailable && model.IsDelete)
                            {
                                /*entity.DeleteDate = DateTime.Now;
                                entity.DeleteUser = currUser;*/
                                RejectAppointment(entity);
                                RejectReports(entity.Id, currUser, "Заявка отклонена");
                            }
                            else if (model.IsChiefApproveAvailable && model.IsChiefApproved)
                            {
                                /*if (model.StaffCreatorId == current.Id)
                                {*/
                                    entity.ChiefDateAccept = DateTime.Now;
                                    entity.AcceptChief = currUser;
                                //}
                            }
                            else if (model.IsManagerApproveAvailable && model.IsManagerApproved)
                            {
                                entity.ManagerDateAccept = DateTime.Now;
                                entity.AcceptManager = currUser;
                                EmailDto dto = SendEmailForAppointmentManagerAccept(entity.Creator, entity);
                                if (!string.IsNullOrEmpty(dto.Error))
                                    error = string.Format("Заявка обработана успешно,но есть ошибка при отправке оповещений: {0}",
                                            dto.Error);
                            }
                            else if (entity.ChiefDateAccept.HasValue &&
                                 !entity.StaffDateAccept.HasValue
                                 && model.IsStaffApproveAvailable
                                 && model.IsStaffApproved)
                            {
                                entity.StaffDateAccept = DateTime.Now;
                                var recruters = new List<User>();
                                if(model.Recruter1id>0) recruters.Add( UserDao.Load(model.Recruter1id));
                                if(model.Recruter2id>0) recruters.Add( UserDao.Load(model.Recruter2id));
                                if(model.Recruter3id>0) recruters.Add( UserDao.Load(model.Recruter3id));
                                entity.Recruters = recruters;
                                //entity.AppointmentEducationTypeId = model.AppointmentEducationType;
                                entity.AcceptStaff = currUser;
                                entity.Priority = model.Priority;
                                CreateAppointmentReport(entity);
                            }
                        }
                        else if (entity.ChiefDateAccept.HasValue &&
                                 !entity.StaffDateAccept.HasValue
                                 && model.IsStaffApproveAvailable
                                 && model.IsStaffApproved)
                        {
                            entity.StaffDateAccept = DateTime.Now;
                            entity.AcceptStaff = currUser;
                            //entity.AppointmentEducationTypeId = model.AppointmentEducationType;
                            var recruters = new List<User>();
                            if (model.Recruter1id > 0) recruters.Add(UserDao.Load(model.Recruter1id));
                            if (model.Recruter2id > 0) recruters.Add(UserDao.Load(model.Recruter2id));
                            if (model.Recruter3id > 0) recruters.Add(UserDao.Load(model.Recruter3id));
                            entity.Recruters = recruters;
                            entity.Priority = model.Priority;
                            CreateAppointmentReport(entity);
                        }
                    }
                    break;
                case UserRole.Estimator:
                case UserRole.OutsourcingManager:
                    break;
                default:
                    throw new ArgumentException(string.Format("Недопустимая роль {0}", current.UserRole));
            }
        }
        public int CreateAppointmentReport(Appointment entity/*,User creator*/)
        {
            int id = 0;
            int secondnumber = 0;
            var reports = AppointmentReportDao.QueryExpression(x => x.Appointment.Id == entity.Id);
            if (reports != null && reports.Any())
            {
                foreach (var el in reports)
                {
                    if (el.SecondNumber > secondnumber) secondnumber = el.SecondNumber;
                }
            }
            secondnumber++;
            AppointmentReport report = new AppointmentReport
                                               {
                                                   Appointment = entity,
                                                   Creator = entity.AcceptStaff!=null?entity.AcceptStaff:UserDao.Load(CurrentUser.Id) ,
                                                   CreateDate = DateTime.Now,
                                                   EditDate = DateTime.Now,
                                                   Email = string.Empty,
                                                   Name = entity.FIO!=null?entity.FIO:"",
                                                   Number = RequestNextNumberDao.GetNextNumberForType((int)RequestTypeEnum.AppointmentReport),
                                                   SecondNumber = secondnumber,
                                                   Phone = string.Empty,
                                                   Type = AppointmentEducationTypeDao.Get(entity.AppointmentEducationTypeId),
                                                   IsColloquyPassed=entity.Recruter==2?true:new bool?(),
                                                                                                };
            if (entity.Recruter == 2) report.StaffDateAccept = DateTime.Now;
                AppointmentReportDao.Save(report);
                id = report.Id;
            
            return id;
        }
        protected void RejectReports(int appointmentId,User user, string rejectReason)
        {
            List<AppointmentReport> list = AppointmentReportDao.LoadForAppointmentId(appointmentId);
            foreach (AppointmentReport report in list)
            {
                if (!report.DeleteDate.HasValue)
                {
                    if (report.DateAccept.HasValue)
                    {
                        Log.ErrorFormat(StrOtherReportUserAccepted, report.Number);
                        throw new ValidationException(string.Format(StrOtherReportUserAccepted, report.Number));
                    }
                    report.DeleteUser = user;
                    report.DeleteDate = DateTime.Now;
                    report.RejectReason = rejectReason;
                    AppointmentReportDao.SaveAndFlush(report);
                }
            }
        }
        #region Emails
        public int SendEmailForAppointmentManager(int AppId)
        {
            var app=AppointmentDao.Load(AppId);
            app.IsStoped = true;
            AppointmentDao.SaveAndFlush(app);
            string addr = app.Creator.Email;
            if (String.IsNullOrEmpty(addr))
                return 1;
            string subj=String.Format("Заявка на подбор персонала №{0}",app.Number);
            string body=String.Format("Ваша заявка на подбор персонала №{0} от {1} рассмотренна кадровой службой. Просьба дать ответ на комментарий.",app.Number,app.CreateDate.ToShortDateString());
            var dto= SendEmail(addr, subj, body);
            if (String.IsNullOrEmpty(dto.Error))
                return 0;
            else return 2;
        }
        protected EmailDto SendEmailForBankAccountantReject(User user, Appointment entity)
        {
            string BankAccountantEmail = user.Email;
            if (string.IsNullOrEmpty(BankAccountantEmail))
                throw new ValidationException("У пользователя нет почты");
            string body = String.Format("Заявка №{0} от {1} на поиск кандидата не одобрена.", entity.Number, entity.CreateDate);
            string subject = "Заявка на поиск сотрудника не одобрена";
            return SendEmail(BankAccountantEmail, subject, body);
        }
        protected EmailDto SendEmailForBankAccountant(User user, Appointment entity)
        {
            string BankAccountantEmail = ConfigurationService.AppointmentPersonnelManagerEmail;
            if (string.IsNullOrEmpty(BankAccountantEmail))
                throw new ValidationException(StrEmailForStaffManagerNotFound);
            string body=String.Format("Создана заявка №{0} от {1} на поиск кандидата в {2}",entity.Number,entity.CreateDate,entity.Department.Name);
            string subject = "Создана заявка на поиск сотрудника";
            return SendEmail(BankAccountantEmail, subject, body);
        }
        protected EmailDto SendEmailForAppointmentChiefAccept(User chief, Appointment entity)
        {
            string staffManagerEmail = ConfigurationService.AppointmentStaffManagerEmail;
            if (string.IsNullOrEmpty(staffManagerEmail))
                throw new ValidationException(StrEmailForStaffManagerNotFound);
            string body;
            string subject = GetSubjectAndBodyForAppointmentManagerAcceptRequest(chief, entity, out body);
            return SendEmail(staffManagerEmail, subject, body);
        }
        
        protected EmailDto SendEmailForStaffManager(Appointment entity)
        {
            string staffManagerEmail = ConfigurationService.AppointmentStaffManagerEmail;
            if (string.IsNullOrEmpty(staffManagerEmail))
                throw new ValidationException(StrEmailForStaffManagerNotFound);
            DepartmentDto dep3 = AppointmentDao.GetDepartmentForPathAndLevel(entity.Department.Path, 3);
            if (dep3 == null)
                Log.ErrorFormat(StrEmailForAppointmentManagerAcceptDepartment3NotFound, entity.Department.Id);
            string body = string.Format("Создана заявка № {0} на подбор {1}, дирекция {2} сотрудником {3}",
                                 entity.Number,
                                 entity.PositionName,
                                 dep3 == null ? "<не найдено в базе данных>" : dep3.Name,
                                 entity.Creator.Name);
            string subject = "Создана заявка на поиск кандидата.";
            return SendEmail(staffManagerEmail, subject, body);
        }
        /*protected EmailDto SendEmailForAppointmentPersonnelAccept(User personnel, Appointment entity)
        {
            string staffManagerEmail = ConfigurationService.AppointmentStaffManagerEmail;
            if (string.IsNullOrEmpty(staffManagerEmail))
                throw new ValidationException(StrEmailForStaffManagerNotFound);
            string body;
            string subject = GetSubjectAndBodyForAppointmentManagerAcceptRequest(personnel, entity, out body);
            return SendEmail(staffManagerEmail, subject, body);
        }*/
        
        protected EmailDto SendEmailForAppointmentManagerAccept(User creator, Appointment entity)
        {
            string to = string.Empty;
            //IdNameDto user;
            List<IdNameDto> users;
            var chiefs = GetChiefsForManager(creator.Id).OrderByDescending(x=>x.Level);
            if (chiefs.Any())
                foreach(var el in chiefs)
                {
                    if (String.IsNullOrWhiteSpace(el.Email)) continue;
                    string b;
                    string s = GetSubjectAndBodyForAppointmentManagerAcceptRequest(creator, entity, out b);
                    return SendEmail(el.Email, s, b);
                }
            #region Анахронизм
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
                            users = AppointmentDao.GetForManagersParentDepartment(creator.Department.Id);
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
            #endregion
        }
        protected string GetSubjectAndBodyForAppointmentManagerAcceptRequest(User user, Appointment entity, out string body)
        {
            DepartmentDto dep3 = AppointmentDao.GetDepartmentForPathAndLevel(entity.Department.Path, 3);
            if (dep3 == null)
                Log.ErrorFormat(StrEmailForAppointmentManagerAcceptDepartment3NotFound,entity.Department.Id);
            body = string.Format(StrEmailForAppointmentManagerAcceptText,
                                 entity.Number,
                                 entity.PositionName,
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
            /*if (entity.PersonnelDateAccept.HasValue)
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
            }*/
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
                                 entity.PositionName,
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
                IsAddAvailable = /*AuthenticationService.CurrentUser.UserRole == UserRole.PersonnelManager &&*/ id > 0,
            };
            if (id == 0)
                return commentModel;
             switch (typeId)
             {
                 case RequestTypeEnum.Appointment:
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
                     break;
                 case RequestTypeEnum.AppointmentReport:
                     AppointmentReport rep = AppointmentReportDao.Load(id);
                     if ((rep.Comments != null) && (rep.Comments.Count() > 0))
                     {
                         commentModel.Comments = rep.Comments.OrderBy(x => x.DateCreated).ToList().
                             ConvertAll(x => new RequestCommentModel
                             {
                                 Comment = x.Comment,
                                 CreatedDate = x.DateCreated.ToString(),
                                 Creator = x.User.FullName,
                             });
                     }
                     break;
                 default:
                     throw new ValidationException(string.Format(StrInvalidCommentType, (int)typeId));
                    
            }
            return commentModel;
            //Vacation vacation = VacationDao.Load(id);
        }
        public bool SaveComment(SaveCommentModel model,RequestTypeEnum type)
        {
            try
            {
                User user = UserDao.Load(AuthenticationService.CurrentUser.Id);
                switch (type)
                {
                    case RequestTypeEnum.Appointment:
                        Appointment entity = AppointmentDao.Load(model.DocumentId);
                        AppointmentComment comment = new AppointmentComment
                                                         {
                                                             Comment = model.Comment,
                                                             Appointment = entity,
                                                             DateCreated = DateTime.Now,
                                                             User = user,
                                                         };
                        AppointmentCommentDao.MergeAndFlush(comment);
                        break;
                    case RequestTypeEnum.AppointmentReport:
                        AppointmentReport rep = AppointmentReportDao.Load(model.DocumentId);
                        AppointmentReportComment comm = new AppointmentReportComment
                        {
                            Comment = model.Comment,
                            AppointmentReport = rep,
                            DateCreated = DateTime.Now,
                            User = user,
                        };
                        AppointmentReportCommentDao.MergeAndFlush(comm);
                        break;
                    default:
                        throw new ValidationException(string.Format(StrInvalidCommentType,(int)type));
                }
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
        #region Appointment Report
        public AppointmentReportEditModel GetAppointmentReportEditModel(int id)
        {
            AppointmentReportEditModel model = new AppointmentReportEditModel { Id = id };
            //User creator;
            model.Personnels = EmploymentCandidateDao.GetPersonnels();
            
            IUser current = AuthenticationService.CurrentUser;
            User currUser = UserDao.Load(current.Id);
            if(id == 0)
                throw new ValidationException(StrAppointmentReportIncorrectId);
            AppointmentReport entity = AppointmentReportDao.Get(id);
            if (entity == null)
                throw new ValidationException(string.Format(StrAppointmentReportNotFound, id));
            if (entity.Candidates != null && entity.Candidates.Any())
            {
                model.Candidates = entity.Candidates.Select(x => new Reports.Core.Dto.Employment2.CandidateDto { Id = x.Id, Name = x.User.Name, EmploymentDate = (x.PersonnelManagers!=null)?x.PersonnelManagers.CompleteDate:null, Status=x.SendTo1C.HasValue?"Выгружено в 1С":"Не выгружено в 1С" }).ToList();
                var candidates = entity.Appointment.Candidates.Where(x => x.Status != Reports.Core.Enum.EmploymentStatus.REJECTED);
                if (candidates.Count() >= entity.Appointment.BankAccountantAcceptCount)  model.IsClosed = true;
            }
            if (entity.Appointment.Recruter == 2)
            {
                model.ShowStaff = false;
                model.IsColloquyPassed = 1;
            }
            else model.ShowStaff = true;
            model.TestingResult = entity.TestingResult;
            model.AppId = entity.Appointment.Id;
            model.Version = entity.Version;
            model.DateCreated = FormatDate(entity.CreateDate);
            model.TypeId = entity.Type.Id;
            model.IsEducationExists = !entity.IsEducationExists.HasValue ? -1 : (entity.IsEducationExists.Value ? 1 : 0);
            model.IsColloquyPassed = !entity.IsColloquyPassed.HasValue ? -1 : (entity.IsColloquyPassed.Value ? 1 : 0);
            model.UserId = entity.Appointment.Creator.Id;
            model.Name = entity.Name;
            model.DocumentNumber = entity.Appointment.Number+"/"+entity.SecondNumber;
            model.Phone = entity.Phone;
            model.Email = entity.Email;
            model.ColloquyDate = FormatDate(entity.ColloquyDate);
            model.EducationTime = entity.EducationTime;
            model.RejectReason = entity.RejectReason;
            model.DateAccept = FormatDate(entity.DateAccept);
            model.ResumeComment = entity.ResumeComment;
            model.ResumeCommentByOPINP = entity.ResumeCommentByOPINP;
            model.LessonDate = entity.LessonDate.HasValue ? entity.LessonDate.Value.ToShortDateString() : "";
            SetManagerInfoModel(entity.Appointment.Creator, model,null,entity);
            SetAttachmentToModel(model, id, RequestAttachmentTypeEnum.AppointmentReport);
            LoadDictionaries(model);
            SetFlagsState(id, currUser, current.UserRole, entity, model);
            SetHiddenFields(model,entity);
            return model;
        }
        protected void LoadDictionaries(AppointmentReportEditModel model)
        {
            model.Personnels = EmploymentCandidateDao.GetPersonnels();
            model.CommentsModel = GetCommentsModel(model.Id, RequestTypeEnum.AppointmentReport);
            model.IsEducationExistsValues = new List<IdNameDto>
                              {
                                  new IdNameDto {Id = -1,Name = string.Empty},
                                  new IdNameDto {Id = 0,Name = "Нет"},
                                  new IdNameDto {Id = 1,Name = "Да"},
                              }.OrderBy(x => x.Name).ToList();
            model.TestingResults = new List<IdNameDto>
            {
                new IdNameDto {Id=0, Name="-"},
                new IdNameDto {Id=1, Name="Тест не пройден(отклонение)"},
                new IdNameDto {Id=2, Name="худший(отклонение)"},
                new IdNameDto {Id=3, Name="ниже среднего"},
                new IdNameDto {Id=4, Name="средний"},
                new IdNameDto {Id=5, Name="выше среднего"},
                new IdNameDto {Id=6, Name="лучший"}
            };
            model.IsColloquyPassedValues = new List<IdNameDto>
                              {
                                  new IdNameDto {Id = -1,Name = string.Empty},
                                  new IdNameDto {Id = 0,Name = "Нет"},
                                  new IdNameDto {Id = 1,Name = "Да"},
                              }.OrderBy(x => x.Name).ToList();
            model.Types = AppointmentEducationTypeDao.LoadAll().ToList().
                          ConvertAll(x => new IdNameDto { Id = x.Id, Name = x.Name });
        }
        protected void SetFlagsState(AppointmentReportEditModel model, bool state)
        {
            model.IsEditable = state;
            model.IsSaveAvailable = state;
            model.IsManagerApproveAvailable = state;
            model.IsManagerRejectAvailable = state;
            model.IsStaffApproveAvailable = state;
            model.IsDeleteScanAvailable = state;
            model.IsManagerEditable = state;
            model.IsAddAvailable = state;
            model.IsPrintLoginAvailable = state;
            model.IsColloquyDateEditable = state;
            model.IsStaffSetDateAcceptAvailable = state;
            //model.ApproveForAllAvailable = state;
        }
        protected void SetFlagsState(int id, User current, UserRole currRole, AppointmentReport entity, AppointmentReportEditModel model)
        {
            SetFlagsState(model, false);
            model.AppId = entity.Appointment.Id;
            if (entity.CandidateRejectDate.HasValue)
            {
                model.CandidateRejectDate = entity.CandidateRejectDate;
                model.CandidateRejectedBy = entity.CandidateRejectedBy != null ? entity.CandidateRejectedBy.Name : "";
            }
            model.IsManagerApproved = entity.ManagerDateAccept.HasValue;
            model.IsBankAccountantAccept = entity.Appointment.BankAccountantAccept.HasValue && entity.Appointment.BankAccountantAccept.HasValue && entity.Appointment.BankAccountantAcceptCount > 0;
            model.IsStaffApproved = entity.StaffDateAccept.HasValue;
            model.IsDeleted = entity.DeleteDate.HasValue;
            if (entity.AcceptManager != null && entity.ManagerDateAccept.HasValue)
                model.ManagerFio = entity.AcceptManager.FullName;
            if (entity.AcceptStaff != null && entity.StaffDateAccept.HasValue)
                model.StaffFio = entity.AcceptStaff.FullName;
            if (entity.DeleteDate.HasValue)
                model.DeleteUser = entity.DeleteUser.FullName;
            model.ShowStaff = entity.Appointment.Recruter == 1;
            switch (currRole)
            {
                case UserRole.Manager:
                    if (current.Id == entity.Appointment.Creator.Id && !entity.DeleteDate.HasValue)
                    {
                        if (!entity.StaffDateAccept.HasValue)
                        {
                            model.IsStaffApproveAvailable = true;
                        }
                        model.IsEditable = true;
                        if (entity.AcceptManager != null && !string.IsNullOrEmpty(entity.TempLogin))
                                model.IsPrintLoginAvailable = true;
                        
                        if (!entity.DateAccept.HasValue)
                        {
                            if (entity.StaffDateAccept.HasValue)
                            {
                                model.IsManagerRejectAvailable = true;
                                if (!entity.ManagerDateAccept.HasValue)
                                {
                                    model.IsManagerApproveAvailable = true;
                                    model.IsColloquyDateEditable = true;
                                }
                                else
                                {
                                    model.IsManagerEditable = true;
                                    if (!string.IsNullOrEmpty(entity.TempLogin))
                                        model.IsPrintLoginAvailable = true;
                                }
                            }
                        }
                    }
                    break;
                case UserRole.StaffManager:
                    if (!entity.DeleteDate.HasValue /*&& (current.Id == entity.Appointment.AcceptStaff.Id || (entity.Appointment.Recruters!=null?entity.Appointment.Recruters.Any(x=>x.Id==current.Id):false))*/)
                    {
                        if (entity.AcceptManager != null && entity.AcceptManager.Id == current.Id && 
                                !string.IsNullOrEmpty(entity.TempLogin))
                            model.IsPrintLoginAvailable = true;
                        
                        if (!entity.DateAccept.HasValue)
                        {
                            if (!entity.StaffDateAccept.HasValue)
                            {
                                model.IsStaffApproveAvailable = true;
                                model.IsEditable = true;
                                if (model.AttachmentId > 0)
                                {
                                    
                                    model.IsDeleteScanAvailable = true;
                                    model.IsManagerApproveAvailable = true;
                                    model.IsColloquyDateEditable = true;
                                    model.IsManagerEditable = false;
                                }
                            }
                            else if (!entity.ManagerDateAccept.HasValue)
                            {
                                model.IsManagerRejectAvailable = true;
                                model.IsManagerApproveAvailable = true;
                                model.IsColloquyDateEditable = true;
                                model.IsManagerEditable = false;
                            }
                            else if (entity.AcceptManager.Id == current.Id)
                            {
                                model.IsManagerRejectAvailable = true;
                                model.IsManagerEditable = false;
                            }
                            /*if (!entity.StaffDateAccept.HasValue)
                            {
                                model.IsEditable = true;
                                if (model.AttachmentId > 0)
                                {
                                    model.IsStaffApproveAvailable = true;
                                    model.IsDeleteScanAvailable = true;
                                    model.IsManagerApproveAvailable = true;
                                    model.IsColloquyDateEditable = true;
                                }
                            } 
                            else
                            {
                                model.IsManagerRejectAvailable = true;
                                if (!entity.ManagerDateAccept.HasValue)
                                {
                                    model.IsManagerApproveAvailable = true;
                                    model.IsColloquyDateEditable = true;
                                }
                                else
                                {
                                    model.IsManagerEditable = true;
                                    if (!string.IsNullOrEmpty(entity.TempLogin))
                                        model.IsPrintLoginAvailable = true;
                                }
                            }*/
                            if (entity.ManagerDateAccept.HasValue && model.IsColloquyPassed == 1)
                                model.IsStaffSetDateAcceptAvailable = true;
                            model.IsAddAvailable = true;
                            /*if (model.AttachmentId > 0)
                                model.ApproveForAllAvailable = true;*/
                        }
                    }
                    break;
                case UserRole.Trainer:
                    model.IsTrainerCanSave = model.IsColloquyPassed == 1;
                    break;
                case UserRole.ConsultantPersonnel:
                case UserRole.PersonnelManager:
                case UserRole.Estimator:
                case UserRole.OutsourcingManager:
                case UserRole.Security:
                    break;
                default:
                    throw new ArgumentException(string.Format("Недопустимая роль {0}", currRole));
            }             
            model.IsSaveAvailable = model.IsEditable || model.IsManagerEditable || model.IsManagerApproveAvailable
                                    || model.IsStaffApproveAvailable || model.IsStaffSetDateAcceptAvailable | model.IsTrainerCanSave;
        }
        protected void SetHiddenFields(AppointmentReportEditModel model, AppointmentReport entity)
        {
            if (entity != null)
            {
                model.DepartmentName = entity.Appointment.Department.Name;
                model.DepartmentId = entity.Appointment.Department.Id;
                model.ManagerId = entity.Appointment.Creator.Id;
                model.City = entity.Appointment.City;
                model.CandidatePosition = entity.Appointment.PositionName;
                model.VacationCount = entity.Appointment.BankAccountantAcceptCount.ToString();
                model.Reason = entity.Appointment.Reason.Name;
                model.AppointmentNumber = entity.Appointment.Number.ToString();
            }
            model.TypeIdHidden = model.TypeId;
            model.IsEducationExistsHidden = model.IsEducationExists;
            model.IsColloquyPassedHidden = model.IsColloquyPassed;
            model.IsManagerApprovedHidden = model.IsManagerApproved;
            model.IsStaffApprovedHidden = model.IsStaffApproved;
            //model.DateCreatedHidden = model.DateCreated;
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
        public void ReloadDictionariesToModel(AppointmentReportEditModel model)
        {
            User user = UserDao.Load(model.UserId);
            LoadDictionaries(model);
            AppointmentReport entity = AppointmentReportDao.Load(model.Id);
            SetManagerInfoModel(user, model,null,entity);
            model.DocumentNumber = entity.Number.ToString();
            model.DateCreated = entity.CreateDate.ToShortDateString();
            if (entity.DeleteDate.HasValue)
                model.IsDeleted = true;
        }
        public bool SaveAppointmentReportEditModel(AppointmentReportEditModel model, UploadFileDto fileDto,out string error)
        {
            error = string.Empty;
            User creator = null;
            AppointmentReport entity = null;
            try
            {
                IUser current = AuthenticationService.CurrentUser;
                entity = AppointmentReportDao.Get(model.Id);
                if (entity == null)
                    throw new ValidationException(string.Format(StrAppointmentReportNotFound, model.Id));
                creator = UserDao.Load(entity.Appointment.Creator.Id);

                if (entity.Version != model.Version)
                {
                    error = StrMultipleAccessError;
                    model.ReloadPage = true;
                    return false;
                }
                if (entity.DeleteDate.HasValue)
                {
                    error = StrReportWasRejected;
                    model.ReloadPage = true;
                    return false;
                }
                if (entity.Appointment.DeleteDate.HasValue)
                {
                    error = StrAppointmentWasRejected;
                    model.ReloadPage = true;
                    return false;
                }
                string fileName;
                int? attachmentId = SaveAttachment(entity.Id, model.AttachmentId, fileDto, RequestAttachmentTypeEnum.AppointmentReport, out fileName);
                if (attachmentId.HasValue)
                {
                    model.AttachmentId = attachmentId.Value;
                    model.Attachment = fileName;
                }
                if (model.IsDelete)
                {
                    switch (current.UserRole)
                    {
                        case UserRole.StaffManager:
                            if (!entity.DeleteDate.HasValue && !entity.ManagerDateAccept.HasValue
                                 && entity.Appointment.AcceptStaff.Id == current.Id)
                            {
                                entity.DeleteDate = DateTime.Now;
                                entity.DeleteUser = entity.Appointment.AcceptStaff;
                            }
                            break;
                        case UserRole.Manager:
                            if (!entity.DeleteDate.HasValue && entity.StaffDateAccept.HasValue
                                && entity.Appointment.Creator.Id == current.Id
                                && !entity.DateAccept.HasValue)
                            {
                                entity.DeleteDate = DateTime.Now;
                                entity.DeleteUser = entity.Appointment.Creator;
                                entity.RejectReason = model.RejectReason;
                            }
                            break;
                        case UserRole.Trainer:
                            
                            break;
                        default:
                            throw new ArgumentException(string.Format("Недопустимая роль {0}", current.UserRole));
                    }
                }
                else
                {
                    ChangeEntityProperties(current, entity, model, creator, out error);
                    AppointmentReportDao.SaveAndFlush(entity);
                    if (entity.Version != model.Version)
                    {
                        entity.EditDate = DateTime.Now;
                        AppointmentReportDao.SaveAndFlush(entity);
                    }
                }
                if (entity.DeleteDate.HasValue)
                    model.IsDeleted = true;
                model.DocumentNumber = entity.Number.ToString();
                model.Version = entity.Version;
                model.DateCreated = entity.CreateDate.ToShortDateString();
                model.IsEducationExists = entity.IsEducationExists.HasValue ? (entity.IsEducationExists.Value ? 1 : 0) : -1;
                model.IsColloquyPassed = entity.IsColloquyPassed.HasValue?(entity.IsColloquyPassed.Value?1:0):-1;
                SetFlagsState(entity.Id, UserDao.Load(current.Id), current.UserRole, entity, model);
                //Задолбала Улькина
                model.ReloadPage = true;
                return true;
            }
            catch (Exception ex)
            {
                AppointmentDao.RollbackTran();
                Log.Error("Error on SaveAppointmentReportEditModel:", ex);
                error = StrException + ex.GetBaseException().Message;
                return false;
            }
            finally
            {
                SetManagerInfoModel(creator, model,null,entity);
                LoadDictionaries(model);
                SetHiddenFields(model,entity);
            }
        }
        protected void ChangeEntityProperties(IUser current, AppointmentReport entity, AppointmentReportEditModel model,
           User user, out string error)
        {
            error = string.Empty;
            User currUser = UserDao.Get(current.Id);
            bool dateAcceptSet = false;
            if (model.CandidateRejectDate.HasValue)
            {
                entity.CandidateRejectDate = DateTime.Now;
                entity.CandidateRejectedBy = UserDao.Load(CurrentUser.Id);
            }
            if (!model.IsDelete)
            {
                if (model.IsEditable)
                {
                    //model.IsEducationExists = !entity.IsEducationExists.HasValue ? 0 : (entity.IsEducationExists.Value ? 1 : 0);
                    //model.UserId = entity.Creator.Id;
                    //entity.Type = AppointmentEducationTypeDao.Get(model.TypeId);
                    entity.Name = model.Name;
                    entity.Phone = model.Phone;
                    entity.Email = model.Email;
                    //entity.ColloquyDate = DateTime.Parse(model.ColloquyDate);
                    entity.EducationTime =  model.EducationTime ;
                    //entity.RejectReason = model.RejectReason;
                }
                if (!String.IsNullOrWhiteSpace(model.LessonDate))
                {
                    DateTime res;
                    if (DateTime.TryParse(model.LessonDate, out res)) { entity.LessonDate = res; };
                }
                if(model.IsColloquyDateEditable)
                {
                    entity.ColloquyDate = string.IsNullOrEmpty(model.ColloquyDate)
                        ? new DateTime?()
                        : DateTime.Parse(model.ColloquyDate);
                }
                if (model.IsManagerEditable)
                {

                    entity.ResumeComment = model.ResumeComment;
                   
                    if (model.IsColloquyPassed >= 0)
                        entity.IsColloquyPassed = model.IsColloquyPassed == 1 ? true : false;
                    else
                        entity.IsColloquyPassed = new bool?();
                    if (!string.IsNullOrEmpty(model.DateAccept))
                    {
                        entity.DateAccept = DateTime.Parse(model.DateAccept);
                        dateAcceptSet = true;
                    }
                }
                if (model.IsStaffSetDateAcceptAvailable)
                {
                    /*if (model.IsEducationExists >= 0)
                        entity.IsEducationExists = model.IsEducationExists == 1 ? true : false;
                    else
                        entity.IsEducationExists = new bool?();*/
                    if (!string.IsNullOrEmpty(model.DateAccept) && entity.IsEducationExists.HasValue)
                    {
                        entity.DateAccept = DateTime.Parse(model.DateAccept);
                        dateAcceptSet = true;
                    }
                }
            }
            switch (current.UserRole)
            {
                case UserRole.StaffManager:
                {
                    if (!entity.DeleteDate.HasValue /*&& (entity.Appointment.AcceptStaff.Id == current.Id || entity.Appointment.Recruters.Any(x=>x.Id==current.Id))*/)
                    {
                        entity.TestingResult = model.TestingResult;
                        entity.ResumeCommentByOPINP = model.ResumeCommentByOPINP;
                            if (!entity.StaffDateAccept.HasValue && model.IsStaffApproved && model.AttachmentId > 0)
                            {
                                entity.StaffDateAccept = DateTime.Now;
                                entity.AcceptStaff = currUser;
                            } 
                            if (entity.StaffDateAccept.HasValue && !entity.ManagerDateAccept.HasValue && model.IsManagerApproved)
                            {
                                entity.ManagerDateAccept = DateTime.Now;
                                entity.AcceptManager = currUser;
                                entity.TempLogin = entity.Id.ToString();
                                entity.TempPassword = CreatePassword(PasswordLength);
                            }
                            /*if (entity.Appointment.Creator.Id == current.Id && dateAcceptSet)
                            {
                                RejectReportsExceptId(entity.Appointment.Id, entity.Id, entity.Appointment.Creator,
                                                      string.Format("Другой кандидат принят на работу (отчет № {0})",
                                                                    entity.Number));
                            }*/
                            if (entity.AcceptStaff != null && dateAcceptSet)
                            {
                                RejectReportsExceptId(entity.Appointment.Id, entity.Id, entity.Creator,
                                                      string.Format("Другой кандидат принят на работу (отчет № {0})",
                                                                    entity.Number));
                            }
                        //}
                    }
                }
                break;
                case UserRole.Manager:
                {
                    if (!entity.StaffDateAccept.HasValue && model.IsStaffApproved && model.AttachmentId > 0)
                    {
                        entity.StaffDateAccept = DateTime.Now;
                        entity.AcceptStaff = currUser;
                    } 
                    if(!entity.DeleteDate.HasValue && entity.StaffDateAccept.HasValue
                        && !entity.ManagerDateAccept.HasValue 
                        && entity.Appointment.Creator.Id == current.Id 
                        && model.IsManagerApproved)
                    {
                        entity.ManagerDateAccept = DateTime.Now;
                        entity.AcceptManager = currUser;
                        entity.TempLogin = entity.Id.ToString();
                        entity.TempPassword = CreatePassword(PasswordLength);
                        entity.ResumeComment = model.ResumeComment;
                        CreateCandidate(entity);
                    }
                    if (!entity.DeleteDate.HasValue && entity.Appointment.Creator.Id == current.Id && dateAcceptSet)
                    {
                        RejectReportsExceptId(entity.Appointment.Id, entity.Id, entity.Appointment.Creator,
                                              string.Format("Другой кандидат принят на работу (отчет № {0})",
                                                            entity.Number));
                    }
                }
                break;
                case UserRole.Trainer:
                if (model.IsEducationExists >= 0)
                    entity.IsEducationExists = model.IsEducationExists == 1 ? true : false;
                else
                    entity.IsEducationExists = new bool?();
                break;
                case UserRole.Estimator:
                case UserRole.OutsourcingManager:
                break;
                default:
                    throw new ArgumentException(string.Format("Недопустимая роль {0}", current.UserRole));
            }
        }

        private void CreateCandidate(AppointmentReport entity)
        {
            User newUserEntity = new User();
            newUserEntity.Login = entity.TempLogin;
            newUserEntity.Password = entity.TempPassword;            
            newUserEntity.IsFirstTimeLogin = true;
            newUserEntity.IsActive = true;
            newUserEntity.IsNew = true;
            newUserEntity.Name = entity.Name;
            newUserEntity.RoleId = (int)UserRole.Candidate;
            newUserEntity.GivesCredit = false;
            newUserEntity.IsMainManager = false;
            //UserDao.SaveAndFlush(newUserEntity);
            EmploymentCommonDao.SaveAndFlush(new EmploymentCandidate { User = newUserEntity });
        }

        protected void RejectReportsExceptId(int appointmentId,int exceptReportId,User user,string rejectReason)
        {
            List<AppointmentReport> list = AppointmentReportDao.LoadForAppointmentId(appointmentId);
            foreach (AppointmentReport report in list)
            {
                if(report.Id != exceptReportId && !report.DeleteDate.HasValue)
                {
                    if (report.DateAccept.HasValue)
                    {
                        Log.ErrorFormat(StrOtherReportUserAccepted, report.Number);
                        throw new ValidationException(string.Format(StrOtherReportUserAccepted, report.Number));
                    }
                    report.DeleteUser = user;
                    report.DeleteDate = DateTime.Now;
                    report.RejectReason = rejectReason;
                    AppointmentReportDao.SaveAndFlush(report);
                }
            }
        }
        public int CreateNewReport(int otherReportId)
        {
            AppointmentReport entity = AppointmentReportDao.Get(otherReportId);
            if (entity == null)
                throw new ValidationException(string.Format(StrAppointmentReportNotFound, otherReportId));
            if(entity.Appointment.DeleteDate.HasValue)
                throw new ValidationException(string.Format(StrAppointmentWasDeleted));
            /*if(CurrentUser.Id != entity.Appointment.AcceptStaff.Id && !entity.Appointment.Recruters.Any(x=>x.Id==CurrentUser.Id))
                throw new ValidationException(string.Format(CannotCreateReport));*/
            //return CreateAppointmentReport(entity.Appointment/*,entity.Appointment.AcceptStaff*/);
            int secondnumber = 0;
            var reports = AppointmentReportDao.QueryExpression(x => x.Appointment.Id == entity.Appointment.Id);
            if (reports != null && reports.Any())
            {
                foreach (var el in reports)
                {
                    if (el.SecondNumber > secondnumber) secondnumber = el.SecondNumber;
                }
            }
            secondnumber++;
            AppointmentReport report = new AppointmentReport
            {
                Appointment = entity.Appointment,
                Creator = entity.Appointment.AcceptStaff,
                CreateDate = DateTime.Now,
                EditDate = DateTime.Now,
                Email = string.Empty,
                Name = string.Empty,
                Number = entity.Number,
                SecondNumber = secondnumber,
                Phone = string.Empty,
                Type = AppointmentEducationTypeDao.Get(1),
            };
            AppointmentReportDao.SaveAndFlush(report);
            return report.Id;

        }
        public static string CreatePassword(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            string res = string.Empty;
            Random rnd = new Random();
            while (0 < length--)
                res += valid[rnd.Next(valid.Length)];
            return res;
        }
        public PrintLoginFormModel GetPrintLoginFormModel(int id)
        {
            AppointmentReport entity = AppointmentReportDao.Get(id);
            if(entity == null)
                throw new ValidationException(string.Format(StrAppointmentReportNotFound, id));
            if (entity.AcceptManager == null || CurrentUser.Id != entity.AcceptManager.Id)
                throw new ValidationException(StrAccessIsDenied);
            return new PrintLoginFormModel
                       {
                           Login = entity.TempLogin,
                           Password = entity.TempPassword,
                           Name = entity.Name,
                       };
        }
        #endregion

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
        protected int? SaveAttachment(int entityId, int id, UploadFileDto dto, RequestAttachmentTypeEnum type, out string attachment)
        {
            attachment = string.Empty;
            if (dto == null)
                return new int?();
            RequestAttachment attach = id != 0 ? 
               RequestAttachmentDao.Get(id) :
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
                    Log.InfoFormat(StrAttachmentAlreadyExists, entityId, type, existingAttach.Id);
                    attach = existingAttach;
                }
            }
            attach.DateCreated = DateTime.Now;
            attach.UncompressContext = dto.Context;
            attach.ContextType = dto.ContextType;
            attach.FileName = dto.FileName;
            attach.CreatorRole = RoleDao.Load((int)CurrentUser.UserRole);
            RequestAttachmentDao.SaveAndFlush(attach);
            attachment = attach.FileName;
            return attach.Id;
        }
        public bool DeleteAttachment(DeleteAttacmentModel model)
        {
            RequestAttachmentDao.Delete(model.Id);
            return true;
        }


        public AppointmentSelectManagerModel GetSelectManagerModel()
        {
            return new AppointmentSelectManagerModel
            {
                Managers = UserDao.GetManagersWithDepartments().ToList(),
            };
        }

        
    }
}