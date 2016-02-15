using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Reports.Core;
using Reports.Core.Dao;
using Reports.Core.Domain;
using Reports.Core.Dto;
using Reports.Core.Enum;
using Reports.Presenters.Services;
using Reports.Presenters.UI.ViewModel;

namespace Reports.Presenters.UI.Bl.Impl
{
    public class HelpBl : BaseBl, IHelpBl
    {

        public const string StrCannotEditVersion = "Вам запрещено редактирование информации о версии";
        public const string StrCannotDeleteVersion = "Вам запрещено удаление информации о версии";
        public const string StrException = "Исключение:";

        public const string StrCannotEditFaq = "Вам запрещено редактирование информации";
        public const string StrCannotDeleteFaq = "Вам запрещено удаление информации";
        public const string StrNoUser = "Не указан сотрудник для заявки на услугу";
        public const string StrUserNotManager = "Вы (пользователь {0}) не являетесь руководителем или сотрудником - создание заявки запрещено";

        public const string StrServiceRequestNotFound = "Не найдена заявка на услугу (id {0}) в базе данных";
        public const string StrServiceRequestWasChanged = "Заявка была изменена другим пользователем.";

        public const string StrNoDepartmentForUser = "Не задано структурное подразделение для руководителя (id {0})";
        public const string StrNoManagerDepartments = "Не найдено структурных подразделений для руководителя (id {0}) в базе даннных.";

        public const string StrQuestionNoUser = "Не указан сотрудник";
        public const string StrQuestionRequestNotFound = "Не найдена заявка-вопрос  (id {0}) в базе данных";
        public const string StrInvalidManagerLevel = "Неверный уровень руководителя (id {0}) {1} в базе даннных.";
        public const string StrInvalidHelpRequestOwner = "Неверная роль владельца заявки (id {0}) {1} в базе даннных.";
        public const string StrInvalidUserDepartment = "Не указано структурное подразделение для пользователя (id {0}) в базе даннных.";

        public const string StrAllEstimators = "Все расчетчики";
        public const string StrAllConsultantOutsorsingManagers = "Все консультанты ОК";
        public const string StrCannotCreatePersonnelBilling = "Вам запрещено сознание запроса";
        public const string StrInvalidAttachmentType = "Неизвестный тип прикрепленных файлов {0}";
        public const string StrCannotLoadRecipientForId = "Ошибка при загрузке получателя запроса (Id = {0}) из базы данных.";
        public const string StrPersonnalBillingRequestNotFound = "Не найден запрос (внутренний биллинг,id {0}) в базе данных";
        #region DAOs
        protected IHelpVersionDao helpVersionDao;
        public IHelpVersionDao HelpVersionDao
        {
            get { return Validate.Dependency(helpVersionDao); }
            set { helpVersionDao = value; }
        }
        protected IHelpFaqDao helpFaqDao;
        public IHelpFaqDao HelpFaqDao
        {
            get { return Validate.Dependency(helpFaqDao); }
            set { helpFaqDao = value; }
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
        protected IHelpServicePeriodDao helpServicePeriodDao;
        public IHelpServicePeriodDao HelpServicePeriodDao
        {
            get { return Validate.Dependency(helpServicePeriodDao); }
            set { helpServicePeriodDao = value; }
        }
        protected IHelpServiceProductionTimeDao helpServiceProductionTimeDao;
        public IHelpServiceProductionTimeDao HelpServiceProductionTimeDao
        {
            get { return Validate.Dependency(helpServiceProductionTimeDao); }
            set { helpServiceProductionTimeDao = value; }
        }
        protected IHelpServiceTransferMethodDao helpServiceTransferMethodDao;
        public IHelpServiceTransferMethodDao HelpServiceTransferMethodDao
        {
            get { return Validate.Dependency(helpServiceTransferMethodDao); }
            set { helpServiceTransferMethodDao = value; }
        }
        protected IHelpServiceTypeDao helpServiceTypeDao;
        public IHelpServiceTypeDao HelpServiceTypeDao
        {
            get { return Validate.Dependency(helpServiceTypeDao); }
            set { helpServiceTypeDao = value; }
        }
        protected IHelpServiceRequestDao helpServiceRequestDao;
        public IHelpServiceRequestDao HelpServiceRequestDao
        {
            get { return Validate.Dependency(helpServiceRequestDao); }
            set { helpServiceRequestDao = value; }
        }
        protected IHelpServiceRequestCommentDao helpServiceRequestCommentDao;
        public IHelpServiceRequestCommentDao HelpServiceRequestCommentDao
        {
            get { return Validate.Dependency(helpServiceRequestCommentDao); }
            set { helpServiceRequestCommentDao = value; }
        }
        protected IManualRoleRecordDao manualRoleRecordDao;
        public IManualRoleRecordDao ManualRoleRecordDao
        {
            get { return Validate.Dependency(manualRoleRecordDao); }
            set { manualRoleRecordDao = value; }
        }

        protected IHelpQuestionTypeDao helpQuestionTypeDao;
        public IHelpQuestionTypeDao HelpQuestionTypeDao
        {
            get { return Validate.Dependency(helpQuestionTypeDao); }
            set { helpQuestionTypeDao = value; }
        }
        protected IHelpQuestionSubtypeDao helpQuestionSubtypeDao;
        public IHelpQuestionSubtypeDao HelpQuestionSubtypeDao
        {
            get { return Validate.Dependency(helpQuestionSubtypeDao); }
            set { helpQuestionSubtypeDao = value; }
        }
        protected INoteTypeDao noteTypeDao;
        public INoteTypeDao NoteTypeDao
        {
            get { return Validate.Dependency(noteTypeDao); }
            set { noteTypeDao = value; }
        }
        protected IHelpQuestionRequestDao helpQuestionRequestDao;
        public IHelpQuestionRequestDao HelpQuestionRequestDao
        {
            get { return Validate.Dependency(helpQuestionRequestDao); }
            set { helpQuestionRequestDao = value; }
        }

        protected IHelpBillingTitleDao helpBillingTitleDao;
        public IHelpBillingTitleDao HelpBillingTitleDao
        {
            get { return Validate.Dependency(helpBillingTitleDao); }
            set { helpBillingTitleDao = value; }
        }
        protected IHelpBillingUrgencyDao helpBillingUrgencyDao;
        public IHelpBillingUrgencyDao HelpBillingUrgencyDao
        {
            get { return Validate.Dependency(helpBillingUrgencyDao); }
            set { helpBillingUrgencyDao = value; }
        }
        protected IHelpPersonnelBillingRequestDao helpPersonnelBillingRequestDao;
        public IHelpPersonnelBillingRequestDao HelpPersonnelBillingRequestDao
        {
            get { return Validate.Dependency(helpPersonnelBillingRequestDao); }
            set { helpPersonnelBillingRequestDao = value; }
        }
        protected IHelpPersonnelBillingCommentDao helpPersonnelBillingCommentDao;
        public IHelpPersonnelBillingCommentDao HelpPersonnelBillingCommentDao
        {
            get { return Validate.Dependency(helpPersonnelBillingCommentDao); }
            set { helpPersonnelBillingCommentDao = value; }
        }

        protected IHelpBillingExecutorTaskDao helpBillingExecutorTaskDao;
        public IHelpBillingExecutorTaskDao HelpBillingExecutorTaskDao
        {
            get { return Validate.Dependency(helpBillingExecutorTaskDao); }
            set { helpBillingExecutorTaskDao = value; }
        }

        protected IRequestPrintFormDao requestPrintFormDao;
        public IRequestPrintFormDao RequestPrintFormDao
        {
            get { return Validate.Dependency(requestPrintFormDao); }
            set { requestPrintFormDao = value; }
        }
        #endregion

        #region Service Requests List
        public HelpServiceRequestsListModel GetServiceRequestsList()
        {
            User user = UserDao.Load(AuthenticationService.CurrentUser.Id);
            IdNameReadonlyDto dep = GetDepartmentDto(user);
            HelpServiceRequestsListModel model = new HelpServiceRequestsListModel
            {
                UserId = AuthenticationService.CurrentUser.Id,
                DepartmentName = dep.Name,
                DepartmentId = dep.Id,
                DepartmentReadOnly = dep.IsReadOnly,
            };
            SetInitialDates(model);
            SetDictionariesToModel(model);
            //SetInitialStatus(model);
            SetIsOriginalDocsVisible(model);
            SetIsAvailable(model);
            return model;
        }
        protected void SetIsOriginalDocsVisible(HelpServiceRequestsListModel model)
        {
            List<UserRole> RolesToShow=new List<UserRole>();
            RolesToShow.AddRange(new List<UserRole>{
                UserRole.OutsourcingManager,
                UserRole.PersonnelManager,
                UserRole.ConsultantOutsourcing,
                UserRole.ConsultantPersonnel,
                UserRole.Estimator
               
            });
            model.IsOriginalDocsVisible = RolesToShow.Contains(CurrentUser.UserRole);
        }
        protected void SetIsOriginalDocsEditable(HelpServiceRequestsListModel model)
        {
            List<UserRole> RolesToEdit = new List<UserRole>{
                
                UserRole.PersonnelManager,
                UserRole.Estimator
                // UserRole.ConsultantOutsorsingManager Deprecated
            };
            model.IsOriginalDocsEditable = RolesToEdit.Contains(CurrentUser.UserRole) || CurrentUser.Id==10;
        }
        protected void SetIsAvailable(HelpServiceRequestsListModel model)
        {
            model.IsAddAvailable = model.IsAddAvailable = ((CurrentUser.UserRole & UserRole.ConsultantPersonnel) == UserRole.ConsultantPersonnel ||(CurrentUser.UserRole & UserRole.Manager) == UserRole.Manager) || ((CurrentUser.UserRole & UserRole.PersonnelManager) == UserRole.PersonnelManager);
        }
        public void SetDictionariesToModel(HelpServiceRequestsListModel model)
        {
            model.Statuses = GetServiceRequestsStatuses();
            List<HelpServiceType> types = HelpServiceTypeDao.LoadAllSortedByOrder();
            //types=FilteServiceRequestTypes(types);
            types.Insert(0,new HelpServiceType() { Id = 0, Name = "Все" });
            model.Types = types.ConvertAll(x => new IdNameDto { Id = x.Id, Name = x.Name });
        }
        public List<IdNameDto> GetServiceRequestsStatuses()
        {
            List<IdNameDto> moStatusesList = new List<IdNameDto>
                                                       {
                                                           new IdNameDto(1, "Черновик сотрудника"),
                                                           new IdNameDto(2, "Услуга запрошена"),
                                                           new IdNameDto(3, "Услуга формируется"),
                                                           //new IdNameDto(4, "Услуга сформирована"),
                                                           new IdNameDto(5, "Услуга оказана")
                                                           //new IdNameDto(4, "Не одобрен руководителем"),
                                                           //new IdNameDto(5, "Одобрен членом правления"),
                                                           //new IdNameDto(6, "Не одобрен членом правления"),
                                                           //new IdNameDto(7, "Требует одобрения руководителем"),
                                                           //new IdNameDto(8, "Требует одобрения членом правления"),
                                                           //new IdNameDto(9, "Выгружен в 1С"),
                                                       }.OrderBy(x => x.Name).ToList();
            moStatusesList.Insert(0, new IdNameDto(0, SelectAll));
            return moStatusesList;
        }

        public void SetServiceRequestsListModel(HelpServiceRequestsListModel model, bool hasError)
        {
            SetDictionariesToModel(model);
            User user = UserDao.Load(model.UserId);

            if (hasError)
                model.Documents = new List<HelpServiceRequestDto>();
            else
                SetDocumentsToModel(model, user);
        }
        public void SetDocumentsToModel(HelpServiceRequestsListModel model, User user)
        {
            UserRole role = CurrentUser.UserRole;
            //model.Documents = new List<HelpServiceRequestDto>();
            model.Documents = HelpServiceRequestDao.GetDocuments(
                user.Id,
                role,
                model.DepartmentId,
                model.StatusId,
                model.BeginDate,
                model.EndDate,
                model.UserName,
                model.Number,
                model.SortBy,
                model.SortDescending,
                model.Address,
                model.TypeId);
            SetIsOriginalDocsVisible(model);
            SetIsOriginalDocsEditable(model);
        }
        public void SaveDocumentsToModel(HelpServiceRequestsListModel model)
        {
            List<UserRole> ApprovedUsers = new List<UserRole>
            {
                UserRole.ConsultantPersonnel,
                //UserRole.ConsultantOutsorsingManager, Deprecated
                UserRole.PersonnelManager
            };
            if (!ApprovedUsers.Contains(CurrentUser.UserRole) || model.Documents==null) return;
            foreach (var doc in model.Documents)
            {
                HelpServiceRequest entity = helpServiceRequestDao.FindById(doc.Id);
                if (entity == null) continue;
                entity.IsOriginalReceived = doc.IsOriginalReceived;
                helpServiceRequestDao.SaveAndFlush(entity);
            }
        }
        #endregion
        #region Service Requests Edit
        public CreateHelpServiceRequestModel GetCreateHelpServiceRequestModel()
        {
            CreateHelpServiceRequestModel model = new CreateHelpServiceRequestModel();
            User currentUser = UserDao.Load(CurrentUser.Id);
            
            //if (currentUser == null)
            //    throw new ArgumentException(string.Format("Не могу загрузить пользователя {0} из базы даннных",
            //        CurrentUser.Id));
            IList<IdNameDto> list;
            if ((CurrentUser.UserRole & UserRole.ConsultantPersonnel) > 0)
            {
                var users = UserDao.GetUsersForConsultantBank();
                model.Users = users;
            }
            else
            switch (currentUser.Level)
            {
                case 2:
                case 3:
                    IList<Department> depList =  ManualRoleRecordDao.LoadDepartmentsForUserId(currentUser.Id);
                    if(depList == null || depList.Count() == 0)
                            throw new ArgumentException(string.Format(StrNoManagerDepartments, currentUser.Id));
                    list = UserDao.GetEmployeesForCreateHelpServiceRequest(depList.Select(x => x.Id).Distinct().ToList(), null);
                    model.Users = list;
                    break;
                case 4:
                case 5:
                case 6:
                    if (currentUser.Department == null)
                        throw new ValidationException(string.Format(StrNoDepartmentForUser,currentUser.Id));
                    list = UserDao.GetEmployeesForCreateHelpServiceRequest(new List<int> {currentUser.Department.Id}, null);
                    model.Users = list;
                    break;
            }
            return model;
        }
        /// <summary>
        /// Автозаполнение физических лиц
        /// </summary>
        /// <param name="Name">ФИО физического лица</param>
        /// <param name="PersonID">ID физического лица</param>
        /// <returns></returns>
        public IList<IdNameDto> GetPersonAutocomplete(string Name)
        {
            User currentUser = UserDao.Load(CurrentUser.Id);
            
            //if (currentUser == null)
            //    throw new ArgumentException(string.Format("Не могу загрузить пользователя {0} из базы даннных",
            //        CurrentUser.Id));
            IList<IdNameDto> list = null;
            if ((CurrentUser.UserRole & UserRole.ConsultantPersonnel) > 0)
            {
                var users = UserDao.GetUsersForConsultantBank().Where(x => x.Name.ToLower().StartsWith(Name.ToLower())).ToList();
                return users;
            }
            else if ((CurrentUser.UserRole & UserRole.PersonnelManager) > 0)
            {
                var users = UserDao.GetEmployeesForCreateHelpServiceRequestOK(Name, AuthenticationService.CurrentUser.Id);//UserDao.GetUsersForPersonnel(CurrentUser.Id).Where(x => x.Name.ToLower().StartsWith(Name.ToLower())).Select(x => new IdNameDto { Id = x.Id, Name = x.Name }).ToList();
                return users;
            }
            else
                switch (currentUser.Level)
                {
                    case 2:
                    case 3:
                        IList<Department> depList = ManualRoleRecordDao.LoadDepartmentsForUserId(currentUser.Id);
                        if (depList == null || depList.Count() == 0)
                            throw new ArgumentException(string.Format(StrNoManagerDepartments, currentUser.Id));
                        list = UserDao.GetEmployeesForCreateHelpServiceRequest(depList.Select(x => x.Id).Distinct().ToList(), Name);
                        //model.Users = list;
                        break;
                    case 4:
                    case 5:
                    case 6:
                        if (currentUser.Department == null)
                            throw new ValidationException(string.Format(StrNoDepartmentForUser, currentUser.Id));
                        list = UserDao.GetEmployeesForCreateHelpServiceRequest(new List<int> { currentUser.Department.Id }, Name);
                        //model.Users = list;
                        break;
                    default:
                        list = UserDao.GetEmployeesForCreateHelpServiceRequestOK(Name, AuthenticationService.CurrentUser.Id);
                        break;
                }
            return list.ToList();
        }
        public HelpServiceRequestEditModel GetServiceRequestEditModel(int id, int? userId)
        {
            IUser current = AuthenticationService.CurrentUser;
            if (id == 0 && !userId.HasValue)
            {
                if ((current.UserRole & UserRole.Employee) == UserRole.Employee || (current.UserRole & UserRole.DismissedEmployee) == UserRole.DismissedEmployee)
                    userId = current.Id;
                else
                    throw new ValidationException(StrNoUser);
            }
            HelpServiceRequest entity = null;
            if (id != 0)
                entity = HelpServiceRequestDao.Load(id);
            HelpServiceRequestEditModel model = new HelpServiceRequestEditModel
            {
                Id = id,
                UserId = id == 0 ? userId.Value : entity.User.Id,
                IsUserEmployee=CurrentUser.UserRole==UserRole.Employee
            };
            User user = UserDao.Load(model.UserId);
            User currUser = UserDao.Load(current.Id);
            if(id == 0)
            {
                entity = new HelpServiceRequest
                             {
                                 User = user,
                                 Creator = currUser,
                                 CreateDate = DateTime.Now,
                                 EditDate = DateTime.Now,
                                 UserBirthDate= DateTime.Now
                             };
            }
            else
            {
                model.TypeId = entity.Type.Id;
                model.IsForGEMoney = entity.IsForGEMoney;
                model.ProductionTimeTypeId = entity.ProductionTime.Id;
                model.TransferMethodTypeId = entity.TransferMethod.Id;
                model.PeriodId = entity.Period == null? new int?() : entity.Period.Id;
                model.FiredUserName = entity.FiredUserName;
                model.FiredUserPatronymic = entity.FiredUserPatronymic;
                model.FiredUserSurname = entity.FiredUserSurname;
                model.UserBirthDate = entity.UserBirthDate==null?String.Empty : entity.UserBirthDate.Value.ToString("dd.MM.yyyy");
                model.IsForFiredUser = (entity.UserBirthDate != null);//Если дата рождения заполнена - значит форма для уволенного сотрудника
                model.Note = entity.Note==null?0 : entity.Note.Id;
                model.Requirements = entity.Requirements;
                model.Version = entity.Version;
                model.DocumentNumber = entity.Number.ToString();
                model.DateCreated = FormatDate(entity.CreateDate);
                model.Creator = entity.Creator.FullName;
                model.Address = entity.Address;
                RequestAttachment attachment = RequestAttachmentDao.FindByRequestIdAndTypeId(entity.Id,
                    RequestAttachmentTypeEnum.HelpServiceRequestTemplate);
                if(attachment != null)
                {
                    model.AttachmentId = attachment.Id;
                    model.Attachment = attachment.FileName;
                }
                RequestAttachment serviceAttach = RequestAttachmentDao.FindByRequestIdAndTypeId(entity.Id,
                    RequestAttachmentTypeEnum.HelpServiceRequest);
                if (serviceAttach != null)
                {
                    model.ServiceAttachmentId = serviceAttach.Id;
                    model.ServiceAttachment = serviceAttach.FileName;
                    model.DocumentsCount = serviceAttach.DocumentsCount;
                }
                
                RequestPrintForm form = RequestPrintFormDao.FindByRequestAndTypeId(id, RequestPrintFormTypeEnum.ServiceRequest);
                model.IsPrintFormAvailable = form != null;

                if (entity.Consultant != null)
                    model.Worker = entity.Consultant.FullName;
                if (entity.EndWorkDate.HasValue)
                    model.WorkerEndDate = entity.EndWorkDate.Value.ToShortDateString();
                if (entity.ConfirmWorkDate.HasValue)
                    model.ConfirmDate = entity.ConfirmWorkDate.Value.ToShortDateString();
            }
            model.NoteList = noteTypeDao.GetAllNoteTypeDto();
            SetUserInfoModel(user, model);
            LoadDictionaries(model);
            SetFlagsState(id, currUser, entity, model);
            //SetStaticFields(model, entity);
            
            SetHiddenFields(model);
            return model;
        }
        protected void SetHiddenFields(HelpServiceRequestEditModel model)
        {
            model.TransferMethodTypeIdHidden = model.TransferMethodTypeId;
            model.ProductionTimeTypeIdHidden = model.ProductionTimeTypeIdHidden;
            model.TypeIdHidden = model.TypeId;
            model.PeriodIdHidden = model.PeriodId;

        }
        protected void SetFlagsState(int id, User current, HelpServiceRequest entity, HelpServiceRequestEditModel model)
        {
            UserRole currentRole = AuthenticationService.CurrentUser.UserRole;
            SetFlagsState(model, false);
            if (model.Id == 0)
            {
                if ((currentRole & UserRole.ConsultantPersonnel) != UserRole.ConsultantPersonnel && (currentRole & UserRole.Manager) != UserRole.Manager && (currentRole & UserRole.Employee) != UserRole.Employee && (currentRole & UserRole.DismissedEmployee) != UserRole.DismissedEmployee && (currentRole & UserRole.PersonnelManager) != UserRole.PersonnelManager)
                    throw new ArgumentException(string.Format(StrUserNotManager, current.Id));
                model.IsEditable = true;
                model.IsSaveAvailable = true;
                return;
            }
            if (entity!=null && entity.FiredUserDepartment != null)
            {
                model.DepartmentId = entity.FiredUserDepartment.Id;
                model.DepartmentName = entity.FiredUserDepartment.Name;
            }
            switch (currentRole)
            {
                case UserRole.ConsultantPersonnel:
                    case UserRole.Employee:
                    if (entity.Creator.Id == current.Id)
                    {
                        if(!entity.SendDate.HasValue)
                        {
                            model.IsEditable = true;
                            model.IsSaveAvailable = true;
                            //if (model.AttachmentId > 0 || !model.IsAttachmentVisible)
                            model.IsSendAvailable = true;
                        }
                        if (entity.EndWorkDate.HasValue && !entity.ConfirmWorkDate.HasValue)
                            model.IsEndAvailable = true;
                    }
                    break;
                    case UserRole.DismissedEmployee:
                    if (entity.Creator.Id == current.Id)
                    {
                        if (!entity.SendDate.HasValue)
                        {
                            model.IsEditable = true;
                            model.IsSaveAvailable = true;
                            //if (model.AttachmentId > 0 || !model.IsAttachmentVisible)
                            model.IsSendAvailable = true;
                        }
                        if (entity.EndWorkDate.HasValue && !entity.ConfirmWorkDate.HasValue)
                            model.IsEndAvailable = true;
                    }
                    break;
                case UserRole.Manager:
                    if (entity.Creator.Id == current.Id)
                    {
                        if (!entity.SendDate.HasValue)
                        {
                            model.IsEditable = true;
                            model.IsSaveAvailable = true;
                            //if (model.AttachmentId > 0 || !model.IsAttachmentVisible)
                            model.IsSendAvailable = true;
                        }
                        if (entity.EndWorkDate.HasValue && !entity.ConfirmWorkDate.HasValue)
                            model.IsEndAvailable = true;
                    }
                    break;
                case UserRole.ConsultantOutsourcing:
                    if (entity.Consultant == null || (entity.Consultant.Id == current.Id))
                    {
                        if (entity.Consultant != null && entity.Consultant.Id == current.Id
                            && entity.BeginWorkDate.HasValue && !entity.EndWorkDate.HasValue)
                        {
                            model.IsEndWorkAvailable = true;
                            model.IsNotEndWorkAvailable = true;
                            model.IsConsultantOutsourcingEditable = true;
                            model.IsSaveAvailable = true;
                        }
                        if (entity.SendDate.HasValue && !entity.BeginWorkDate.HasValue)
                            model.IsBeginWorkAvailable = true;
                    }
                    //кнопка принятия в работу доступна пока не сформируется услуга не зависимо от того, кто ее принял в работу
                    if (entity.SendDate.HasValue && entity.BeginWorkDate.HasValue && !entity.EndWorkDate.HasValue)
                        model.IsBeginWorkAvailable = true;
                    break;
                /*case UserRole.PersonnelManager:
                    if (entity.Consultant == null || (entity.Consultant.Id == current.Id))
                    {
                        if (entity.Consultant != null && entity.Consultant.Id == current.Id
                            && entity.BeginWorkDate.HasValue && !entity.EndWorkDate.HasValue)
                        {
                            if (current.Id == 10)
                            {
                                model.IsNotEndWorkAvailable = true;
                            }
                            model.IsEndWorkAvailable = true;
                            model.IsConsultantOutsourcingEditable = true;
                            model.IsSaveAvailable = true;
                        }
                        if (entity.SendDate.HasValue && !entity.BeginWorkDate.HasValue)
                            model.IsBeginWorkAvailable = true;
                    }
                    //кнопка принятия в работу доступна пока не сформируется услуга не зависимо от того, кто ее принял в работу
                    if (entity.SendDate.HasValue && entity.BeginWorkDate.HasValue && !entity.EndWorkDate.HasValue)
                        model.IsBeginWorkAvailable = true;
                    break;*/ //DEPRECATED MAY BE PROBLEM
                case UserRole.Estimator:
                case UserRole.PersonnelManager:
                    if (entity.Consultant == null || (entity.Consultant.Id == current.Id))
                    {
                        if (entity.Consultant != null && entity.Consultant.Id == current.Id
                            && entity.BeginWorkDate.HasValue && !entity.EndWorkDate.HasValue)
                        {
                            model.IsEndWorkAvailable = true;
                            model.IsNotEndWorkAvailable = true;
                            model.IsConsultantOutsourcingEditable = true;
                            model.IsSaveAvailable = true;
                        }
                        if (entity.SendDate.HasValue && !entity.BeginWorkDate.HasValue)
                        {
                            model.IsBeginWorkAvailable = true;
                        }
                    }
                    //кнопка принятия в работу доступна пока не сформируется услуга не зависимо от того, кто ее принял в работу
                    if (entity.SendDate.HasValue && entity.BeginWorkDate.HasValue && !entity.EndWorkDate.HasValue)
                        model.IsBeginWorkAvailable = true;

                    //чтобы видно было кадровикам, но в работу не принималось и скан не выкачивался
                    List<int> EstimatorList = new List<int> { 2, 4, 5, 7, 8, 10, 11, 12, 16, 17, 20, 21, 26, 27 };
                    List<int> PersonnelList = new List<int> { 1, 3, 6, 8, 9, 12, 13, 14, 15, 16, 18, 19, 20, 22, 23, 24, 25, 28, 4, 2, 5, 7, 10, 11, 21, 26, 27 };
                    if (AuthenticationService.CurrentUser.Id != 10 && (CurrentUser.UserRole & UserRole.Estimator) == 0)
                    {
                        if (entity.Type.Id == 4 || entity.Type.Id == 2 || entity.Type.Id == 5 || entity.Type.Id == 10 || entity.Type.Id == 11 || entity.Type.Id == 21 || entity.Type.Id == 7 || entity.Type.Id == 26 || entity.Type.Id == 27)
                        {
                            model.IsNotScanView = entity.Type.Id == 26 || entity.Type.Id == 27 ? false : true;
                            model.IsBeginWorkAvailable = false;
                        }
                        else
                            model.IsNotScanView = false;
                    }
                    else
                    {
                        if (!EstimatorList.Contains(entity.Type.Id))
                        {
                            model.IsBeginWorkAvailable = false;
                            model.IsNotScanView = false;
                        }
                    }
                    //консультант составляет за сотрудника
                    if (entity.Creator.Id == current.Id)
                    {
                        if (!entity.SendDate.HasValue)
                        {
                            model.IsEditable = true;
                            model.IsSaveAvailable = true;
                            //if (model.AttachmentId > 0 || !model.IsAttachmentVisible)
                            model.IsSendAvailable = true;
                        }
                        if (entity.EndWorkDate.HasValue && !entity.ConfirmWorkDate.HasValue)
                            model.IsEndAvailable = true;
                    }
                    break;
            }
        }
        protected List<HelpServiceType> FilteServiceRequestTypes(List<HelpServiceType> types)
        {
            List<int> hiddenIds=new List<int>{15};
            //Фильтрация доступных услуг
            types=types.Where(x=>!hiddenIds.Contains(x.Id)).ToList();
            
            return types;
        }
        protected void SetFlagsState(HelpServiceRequestEditModel model, bool state)
        {
            model.IsBeginWorkAvailable = state;
            model.IsEditable = state;
            model.IsEndAvailable = state;
            model.IsEndWorkAvailable = state;
            model.IsNotEndWorkAvailable = state;
            model.IsSaveAvailable = state;
            model.IsSendAvailable = state;
            model.IsConsultantOutsourcingEditable = state;
            //model.IsServiceAttachmentVisible = state;
        }
        protected void LoadDictionaries(HelpServiceRequestEditModel model)
        {
            model.CommentsModel = GetCommentsModel(model.Id, RequestTypeEnum.HelpServiceRequest);
            List<HelpServiceType> types = HelpServiceTypeDao.LoadAllSortedByOrder();
            //types = FilteServiceRequestTypes(types);
            model.Types = types.ConvertAll(x => new IdNameDto { Id = x.Id,Name = x.Name});
            model.ProductionTimeTypes = HelpServiceProductionTimeDao.LoadAllSortedByOrder().
                ConvertAll(x => new IdNameDto { Id = x.Id, Name = x.Name }).
                Where(x => x.Id != 1).ToList<IdNameDto>();//исключил одну строку
            if (model.Id == 0)
                model.ProductionTimeTypeId = 2;
            model.TransferMethodTypes = HelpServiceTransferMethodDao.LoadAllSortedByOrder().
               ConvertAll(x => new IdNameDto { Id = x.Id, Name = x.Name }).
               Where(x => x.Id != 3 && x.Id != 2).ToList<IdNameDto>(); //удалил из списка почту России и курьер-сервис

            if(model.TypeId == 0)
                model.TypeId = types.First().Id;
            HelpServiceType type = types.Where(x => x.Id == model.TypeId).First();
            SetDistionariesFlag(model,type);
        }
        protected void SetDistionariesFlag(IHelpServiceDictionariesStates model, HelpServiceType type)
        {
            model.IsAttachmentVisible = type.IsAttachmentAvailable;
            model.IsPeriodVisible = type.IsPeriodAvailable;
            model.IsRequirementsVisible = type.IsRequirementsAvailable;
            if (type.IsPeriodAvailable)
            {
                List<HelpServicePeriod> periods = HelpServicePeriodDao.LoadForPeriodSortedByOrder(type.Id);
                model.Periods = periods.ConvertAll(x => new IdNameDto {Id = x.Id, Name = x.Name});
                if(!model.PeriodId.HasValue)
                {
                    int currMonth = DateTime.Today.Year * 100 + DateTime.Today.Month;
                    //if(type.Id == 4) //todo hardcode from DB
                    //    currMonth = DateTime.Today.Year*100 + DateTime.Today.Month;
                    //else
                    //    currMonth = DateTime.Today.Year*100 + 1;
                    HelpServicePeriod currPeriod = periods.OrderByDescending(x => x.PeriodMonth).
                        Where(x => x.PeriodMonth <= currMonth).FirstOrDefault();
                    if (currPeriod != null)
                        model.PeriodId = currPeriod.Id;
                }
            }
            else
                model.Periods = new List<IdNameDto>();
        }
        protected void SetUserInfoModel(User user, HelpUserInfoModel model)
        {
            if (user == null)
                return;
            if (user.Position != null)
                model.Position = user.Position.Name;
            model.UserName = user.FullName;
            
            model.Email = user.Email;
            if(user.Department != null)
            {
                model.Department2 = user.Department.Name;
                Department dep3 = DepartmentDao.GetParentDepartmentWithLevel(user.Department, 3);
                if (dep3 != null)
                    model.Department1 = dep3.Name;

                //Были внесены изменения по отображению начальников над подчиненым сотрудником.
                //ниже закомментарен старый метод отображения
                //добавлен ниже новый (кусок взят из RequestBl)

                //string managers = DepartmentDao.GetDepartmentManagers(user.Department.Id, true)
                //                       .Where(x => x.Email != user.Email)
                //                       .OrderByDescending(x => x.Level).
                //                       Aggregate(string.Empty, (current, x) =>
                //                       current + string.Format("{0} ({1}), ", x.FullName, x.Position == null ? "<не указана>" : x.Position.Name));
                //if (managers.Length >= 2)
                //    managers = managers.Remove(managers.Length - 2);
                //model.ManagerName = managers;

                
                
                IList<User> managerslist = GetManagersForEmployee(user.Id)
                .Where<User>(manager => manager.Level >= 3)
                .OrderByDescending<User, int?>(manager => manager.Level)
                .ToList<User>();
                System.Text.StringBuilder managersBuilder = new System.Text.StringBuilder();
                foreach (var manager in managerslist)
                {
                    managersBuilder.AppendFormat("{0} ({1}), ", manager.Name, manager.Position == null ? "<не указана>" : manager.Position.Name);
                }
                // Cut off trailing ", "
                if (managersBuilder.Length >= 2)
                {
                    managersBuilder.Remove(managersBuilder.Length - 2, 2);
                }

                model.ManagerName = managersBuilder.ToString();
            }

            
            
        }
        /// <summary>
        /// Получить всех руководителей сотрудника
        /// </summary>
        /// <param name="user">Сотрудник, для которого требуется найти руководителей</param>
        /// <returns>Словарь&lt;Уровень, Руководитель&gt;</returns>
        public IList<User> GetManagersForEmployee(int userId)
        {
            IList<User> managers = new List<User>();

            User user = UserDao.Load(userId);
            User managerAccount = UserDao.GetManagerForEmployee(user.Login);

            IList<User> mainManagers;

            // Для руководителей-замов ближайшие руководители находится на том же уровне
            if (managerAccount != null && !managerAccount.IsMainManager)
            {
                mainManagers = DepartmentDao.GetDepartmentManagers(managerAccount.Department != null ? managerAccount.Department.Id : 0)
                    .Where<User>(manager => manager.IsMainManager)
                    .ToList<User>();

                foreach (var mainManager in mainManagers)
                {
                    managers.Add(mainManager);
                }
            }

            // Руководители вышележащих уровней для всех
            User currentUserOrManagerAccount = managerAccount ?? user;
            mainManagers = DepartmentDao.GetDepartmentManagers(currentUserOrManagerAccount.Department != null ? currentUserOrManagerAccount.Department.Id : 0, true)
                .Where<User>(manager => (currentUserOrManagerAccount.Department.ItemLevel ?? 0) > (manager.Department.ItemLevel ?? 0))
                .ToList<User>();

            foreach (var mainManager in mainManagers)
            {
                managers.Add(mainManager);
            }

            return managers;
        }
        public void ReloadDictionariesToModel(HelpServiceRequestEditModel model)
        {
            LoadDictionaries(model);
        }
        public bool SaveServiceRequestEditModel(HelpServiceRequestEditModel model, UploadFileDto fileDto, out string error)
        {
            error = string.Empty;
            User currUser = null;
            User user = null;
            HelpServiceRequest entity = null;
            try
            {
                
                IUser current = AuthenticationService.CurrentUser;
                currUser = UserDao.Load(current.Id);
                user = UserDao.Load(model.UserId);
               
                if (model.Id == 0)
                {
                    entity = new HelpServiceRequest
                    {
                        CreateDate = DateTime.Now,
                        Creator = currUser,//UserDao.Load(current.Id),
                        Number = RequestNextNumberDao.GetNextNumberForType((int)RequestTypeEnum.HelpServiceRequest),
                        EditDate = DateTime.Now,
                        User = user,
                        FiredUserName=model.FiredUserName,
                        FiredUserSurname=model.FiredUserSurname,
                        FiredUserPatronymic=model.FiredUserPatronymic,
                        Note=noteTypeDao.Load(model.Note),
                        IsForGEMoney=model.IsForGEMoney
                    };
                    if (model.UserBirthDate != null) entity.UserBirthDate = DateTime.Parse(model.UserBirthDate);
                    
                    ChangeEntityProperties(entity, model,fileDto,currUser,out error);
                    HelpServiceRequestDao.SaveAndFlush(entity);
                    model.Id = entity.Id;
                    model.DocumentNumber = entity.Number.ToString();
                    model.DateCreated = entity.CreateDate.ToShortDateString();
                    model.Creator = entity.Creator.FullName;
                    if (fileDto != null && entity.Type.IsAttachmentAvailable)
                        ChangeEntityProperties(entity, model, fileDto, currUser, out error);//если создается черновик, то цепляем файл образца
                }
                else
                {
                    entity = HelpServiceRequestDao.Get(model.Id);
                    if (entity == null)
                        throw new ValidationException(string.Format(StrServiceRequestNotFound, model.Id));
                    if (entity.Version != model.Version)
                    {
                        error = StrServiceRequestWasChanged;
                        model.ReloadPage = true;
                        return false;
                    }
                    ChangeEntityProperties(entity, model, fileDto, currUser, out error);
                    HelpServiceRequestDao.SaveAndFlush(entity);
                    if (entity.Version != model.Version)
                    {
                        if((entity.Creator !=null && entity.Creator.Id == CurrentUser.Id) || (entity.User!=null && entity.User.Id==CurrentUser.Id))
                            entity.EditDate = DateTime.Now;
                        HelpServiceRequestDao.SaveAndFlush(entity);
                    }
                }
                    //if (entity.DeleteDate.HasValue)
                    //    model.IsDeleted = true;
                //}
                model.Version = entity.Version;
                model.Worker = entity.Consultant != null ? entity.Consultant.FullName : string.Empty;
                model.WorkerEndDate = entity.EndWorkDate.HasValue ? 
                                      entity.EndWorkDate.Value.ToShortDateString() : string.Empty;
                model.ConfirmDate = entity.ConfirmWorkDate.HasValue ?
                                     entity.ConfirmWorkDate.Value.ToShortDateString() : string.Empty;

                SetFlagsState(entity.Id, currUser,entity, model);
                return true;
            }
            catch (Exception ex)
            {
                HelpServiceRequestDao.RollbackTran();
                Log.Error("Error on SaveServiceRequestEditModel:", ex);
                error = StrException + ex.GetBaseException().Message;
                return false;
            }
            finally
            {
                //SetUserInfoModel(user, model);
                LoadDictionaries(model);
                SetHiddenFields(model);
            }
        }
        protected void  ChangeEntityProperties(HelpServiceRequest entity,HelpServiceRequestEditModel model,
            UploadFileDto fileDto, User currUser,out string error)
        {
            error = string.Empty;
            UserRole currRole = AuthenticationService.CurrentUser.UserRole;
            if (model.IsEditable)
            {
                HelpServiceType type = HelpServiceTypeDao.Load(model.TypeId);
                if (model.Id != 0)
                {
                    if (fileDto != null && entity.Type.IsAttachmentAvailable && model.AttachmentId != 0)
                        RequestAttachmentDao.DeleteAndFlush(model.AttachmentId);
                }
                entity.Type = type;
                entity.IsForGEMoney = model.IsForGEMoney;
                entity.Note = noteTypeDao.Load(model.Note);
                entity.FiredUserName = model.FiredUserName;
                entity.FiredUserSurname = model.FiredUserSurname;
                entity.FiredUserPatronymic = model.FiredUserPatronymic;
                if (model.DepartmentId > 0)
                {
                    var dep = DepartmentDao.Load(model.DepartmentId);
                    entity.FiredUserDepartment = dep;
                }
                if(model.UserBirthDate!=null) entity.UserBirthDate = DateTime.Parse(model.UserBirthDate);
                entity.ProductionTime = HelpServiceProductionTimeDao.Load(model.ProductionTimeTypeId);
                entity.TransferMethod = helpServiceTransferMethodDao.Load(model.TransferMethodTypeId);
                entity.Requirements = type.IsRequirementsAvailable ? model.Requirements : null;
                entity.Period = type.IsPeriodAvailable
                                    ? model.PeriodId.HasValue ? helpServicePeriodDao.Load(model.PeriodId.Value) : null
                                    : null;
                entity.Address = model.Address;
                if(fileDto != null && entity.Type.IsAttachmentAvailable && entity.Id != 0)
                {
                    RequestAttachment attachment = new RequestAttachment
                                                       {
                                                           UncompressContext = fileDto.Context,
                                                           ContextType = fileDto.ContextType,
                                                           CreatorRole = RoleDao.Load((int)currRole),
                                                           DateCreated = DateTime.Now,
                                                           FileName = fileDto.FileName,
                                                           RequestId = entity.Id,
                                                           RequestType = (int)RequestAttachmentTypeEnum.HelpServiceRequestTemplate,
                                                       };
                    RequestAttachmentDao.SaveAndFlush(attachment);
                    model.AttachmentId = attachment.Id;
                    model.Attachment = attachment.FileName;
                }
                if (!entity.Type.IsAttachmentAvailable && model.AttachmentId != 0)
                    RequestAttachmentDao.DeleteAndFlush(model.AttachmentId);
            }
            if (model.IsConsultantOutsourcingEditable)
            {
                if (fileDto != null && model.ServiceAttachmentId != 0)
                    RequestAttachmentDao.DeleteAndFlush(model.ServiceAttachmentId);
                if (fileDto != null)
                {
                    RequestAttachment attachment = new RequestAttachment
                    {
                        UncompressContext = fileDto.Context,
                        ContextType = fileDto.ContextType,
                        CreatorRole = RoleDao.Load((int)currRole),
                        DateCreated = DateTime.Now,
                        FileName = fileDto.FileName,
                        RequestId = entity.Id,
                        RequestType = (int)RequestAttachmentTypeEnum.HelpServiceRequest,
                        DocumentsCount =  model.DocumentsCount>0?model.DocumentsCount:1
                    };
                    RequestAttachmentDao.SaveAndFlush(attachment);
                    model.ServiceAttachmentId = attachment.Id;
                    model.ServiceAttachment = attachment.FileName;
                }
            }
            switch (currRole)
            {
                case UserRole.ConsultantPersonnel:
                case UserRole.Employee:
                    if (entity.Creator.Id == currUser.Id)
                    {
                        if (model.Operation == 1 && !entity.SendDate.HasValue)
                            entity.SendDate = DateTime.Now;
                        if(entity.EndWorkDate.HasValue)
                        {
                            if(model.Operation == 4)
                                entity.ConfirmWorkDate = DateTime.Now;
                            else if(model.Operation == 5)
                            {
                                entity.SendDate = null;
                                entity.BeginWorkDate = null;
                                entity.EndWorkDate = null;
                            }
                        }
                    }
                    break;
                case UserRole.DismissedEmployee:
                    if (entity.Creator.Id == currUser.Id)
                    {
                        if (model.Operation == 1 && !entity.SendDate.HasValue)
                            entity.SendDate = DateTime.Now;
                        if (entity.EndWorkDate.HasValue)
                        {
                            if (model.Operation == 4)
                                entity.ConfirmWorkDate = DateTime.Now;
                            else if (model.Operation == 5)
                            {
                                entity.SendDate = null;
                                entity.BeginWorkDate = null;
                                entity.EndWorkDate = null;
                            }
                        }
                    }
                    break;
                case UserRole.Manager:
                    if (entity.Creator.Id == currUser.Id)
                    {
                        if (model.Operation == 1 && !entity.SendDate.HasValue)
                            entity.SendDate = DateTime.Now;
                        if (entity.EndWorkDate.HasValue)
                        {
                            if (model.Operation == 4)
                                entity.ConfirmWorkDate = DateTime.Now;
                            else if (model.Operation == 5)
                            {
                                entity.SendDate = null;
                                entity.BeginWorkDate = null;
                                entity.EndWorkDate = null;
                            }
                        }
                    }
                    break;
                case UserRole.ConsultantOutsourcing:
                    if (entity.Consultant == null || (entity.Consultant.Id == currUser.Id))
                    {
                        if (model.Operation == 2 && entity.SendDate.HasValue)
                        {
                            entity.BeginWorkDate = DateTime.Now;
                            entity.Consultant = currUser;
                        }
                        if (entity.Consultant != null && entity.Consultant.Id == currUser.Id
                            && model.Operation == 3 && entity.BeginWorkDate.HasValue)
                        {
                            entity.EndWorkDate = DateTime.Now;
                            entity.ConfirmWorkDate = DateTime.Now;
                        }
                        if (entity.Consultant != null && entity.Consultant.Id == currUser.Id
                            && model.Operation == 6 && entity.BeginWorkDate.HasValue)
                        {
                            entity.EndWorkDate = DateTime.Now;
                            entity.NotEndWorkDate = DateTime.Now;
                        }
                    }
                    //кнопка принятия в работу доступна пока не сформируется услуга не зависимо от того, кто ее принял в работу
                    if (model.Operation == 2 && entity.SendDate.HasValue && !entity.NotEndWorkDate.HasValue)
                    {
                        entity.BeginWorkDate = DateTime.Now;
                        entity.Consultant = currUser;
                    }
                    break;
                case UserRole.PersonnelManager:
                    if (entity.Consultant == null || (entity.Consultant.Id == currUser.Id))
                    {
                        if (model.Operation == 2 && entity.SendDate.HasValue)
                        {
                            entity.BeginWorkDate = DateTime.Now;
                            entity.Consultant = currUser;
                        }
                        if (entity.Consultant != null && entity.Consultant.Id == currUser.Id
                            && model.Operation == 3 && entity.BeginWorkDate.HasValue)
                        {
                            entity.EndWorkDate = DateTime.Now;
                            entity.ConfirmWorkDate = DateTime.Now;
                        }
                        if (entity.Consultant != null && entity.Consultant.Id == currUser.Id
                            && model.Operation == 6 && entity.BeginWorkDate.HasValue)
                        {
                            entity.EndWorkDate = DateTime.Now;
                            entity.NotEndWorkDate = DateTime.Now;
                        }
                    }
                    //кнопка принятия в работу доступна пока не сформируется услуга не зависимо от того, кто ее принял в работу
                    if (model.Operation == 2 && entity.SendDate.HasValue && !entity.NotEndWorkDate.HasValue)
                    {
                        entity.BeginWorkDate = DateTime.Now;
                        entity.Consultant = currUser;
                    }

                    //если консультант создает заявку за сотрудника
                    if (entity.Creator.Id == currUser.Id && model.Operation == 1 && !entity.SendDate.HasValue)
                        entity.SendDate = DateTime.Now;
                    break;
                /*case UserRole.PersonnelManager: //DEPRECATED MAY BE PROBLEM
                    if (entity.Consultant == null || (entity.Consultant.Id == currUser.Id))
                    {
                        if (model.Operation == 2 && entity.SendDate.HasValue)
                        {
                            entity.BeginWorkDate = DateTime.Now;
                            entity.Consultant = currUser;
                        }
                        if (entity.Consultant != null && entity.Consultant.Id == currUser.Id
                            && model.Operation == 3 && entity.BeginWorkDate.HasValue)
                        {
                            entity.EndWorkDate = DateTime.Now;
                            entity.ConfirmWorkDate = DateTime.Now;
                        }
                        if (entity.Consultant != null && entity.Consultant.Id == currUser.Id
                            && model.Operation == 6 && entity.BeginWorkDate.HasValue && currUser.Id == 10)
                        {
                            entity.EndWorkDate = DateTime.Now;
                            entity.NotEndWorkDate = DateTime.Now;
                        }
                    }
                    //кнопка принятия в работу доступна пока не сформируется услуга не зависимо от того, кто ее принял в работу
                    if (model.Operation == 2 && entity.SendDate.HasValue && !entity.NotEndWorkDate.HasValue)
                    {
                        entity.BeginWorkDate = DateTime.Now;
                        entity.Consultant = currUser;
                    }
                    break;*/
            }
        }
        public void GetDictionariesStates(int typeId,HelpServiceDictionariesStatesModel model)
        {
            HelpServiceType type = HelpServiceTypeDao.Load(typeId);
            SetDistionariesFlag(model, type);
        }
        public IList<NoteTypeDto> GetAllNodeTypesDto()
        {
            return noteTypeDao.GetAllNoteTypeDto();
        }
        #region Comments
        public CommentsModel GetCommentsModel(int id, RequestTypeEnum typeId)
        {
            bool isAddAvailable = id > 0;
            if (isAddAvailable)
            {
                if (typeId == RequestTypeEnum.HelpServiceRequest)
                {
                    HelpServiceRequest request = HelpServiceRequestDao.Load(id);
                    isAddAvailable =  (((CurrentUser.Id == request.Creator.Id) &&
                                        ((CurrentUser.UserRole & UserRole.Manager) == UserRole.Manager || (CurrentUser.UserRole & UserRole.ConsultantPersonnel) == UserRole.ConsultantPersonnel || (CurrentUser.UserRole & UserRole.Employee) == UserRole.Employee)) ||
                                        (request.Consultant != null && 
                                            CurrentUser.Id == request.Consultant.Id && 
                                            CurrentUser.UserRole == UserRole.ConsultantOutsourcing) ||
                                         (request.Consultant != null &&
                                            CurrentUser.Id == request.Consultant.Id &&
                                            CurrentUser.UserRole == UserRole.PersonnelManager) ||
                                         (request.Consultant != null &&
                                            CurrentUser.Id == request.Consultant.Id &&
                                            CurrentUser.UserRole == UserRole.ConsultantPersonnel)
                                        );
                }
            }
            CommentsModel commentModel = new CommentsModel
            {
                RequestId = id,
                RequestTypeId = (int)typeId,
                Comments = new List<RequestCommentModel>(),
                IsAddAvailable = isAddAvailable ,
            };
            if (id == 0)
                return commentModel;
            switch (typeId)
            {
                case RequestTypeEnum.HelpServiceRequest:
                    HelpServiceRequest entity = HelpServiceRequestDao.Load(id);
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
                default:
                    throw new ValidationException(string.Format(AppointmentBl.StrInvalidCommentType, (int)typeId));

            }
            return commentModel;
        }
        public bool SaveComment(SaveCommentModel model, RequestTypeEnum type)
        {
            try
            {
                User user = UserDao.Load(AuthenticationService.CurrentUser.Id);
                switch (type)
                {
                    case RequestTypeEnum.HelpServiceRequest:
                        HelpServiceRequest entity = HelpServiceRequestDao.Load(model.DocumentId);
                        HelpServiceRequestComment comment = new HelpServiceRequestComment
                        {
                            Comment = model.Comment,
                            Request = entity,
                            DateCreated = DateTime.Now,
                            User = user,
                        };
                        HelpServiceRequestCommentDao.MergeAndFlush(comment);
                        break;
                    //case RequestTypeEnum.AppointmentReport:
                    //    AppointmentReport rep = AppointmentReportDao.Load(model.DocumentId);
                    //    AppointmentReportComment comm = new AppointmentReportComment
                    //    {
                    //        Comment = model.Comment,
                    //        AppointmentReport = rep,
                    //        DateCreated = DateTime.Now,
                    //        User = user,
                    //    };
                    //    AppointmentReportCommentDao.MergeAndFlush(comm);
                    //    break;
                    default:
                        throw new ValidationException(string.Format(AppointmentBl.StrInvalidCommentType, (int)type));
                }
                return true;
            }
            catch (Exception ex)
            {
                //AppointmentCommentDao.RollbackTran();
                Log.Error("Exception", ex);
                model.Error = StrException + ex.GetBaseException().Message;
                return false;
            }
        }
        #endregion
        #endregion
        #region Service Questions List
        public HelpServiceQuestionsListModel GetServiceQuestionsListModel()
        {
            User user = UserDao.Load(AuthenticationService.CurrentUser.Id);
            IdNameReadonlyDto dep = GetDepartmentDto(user);
            HelpServiceQuestionsListModel model = new HelpServiceQuestionsListModel
            {
                UserId = AuthenticationService.CurrentUser.Id,
                DepartmentName = dep.Name,
                DepartmentId = dep.Id,
                DepartmentReadOnly = dep.IsReadOnly,
            };
            SetInitialDates(model);
            SetDictionariesToModel(model);
            //SetInitialStatus(model);
            SetIsAvailable(model);
            return model;
        }
        protected void SetIsAvailable(HelpServiceQuestionsListModel model)
        {
            model.IsAddAvailable = model.IsAddAvailable = ((CurrentUser.UserRole & UserRole.Manager) == UserRole.Manager || (CurrentUser.UserRole & UserRole.ConsultantPersonnel) == UserRole.ConsultantPersonnel);
        }
        public void SetDictionariesToModel(HelpServiceQuestionsListModel model)
        {
            model.Statuses = GetServiceQuestionsStatuses();
            model.IsManagerColumnVisible = (CurrentUser.UserRole & UserRole.Employee) != UserRole.Employee;
        }
        public List<IdNameDto> GetServiceQuestionsStatuses()
        {
            List<IdNameDto> moStatusesList = new List<IdNameDto>
                                                       {
                                                           new IdNameDto(1, "Черновик"),
                                                           new IdNameDto(2, "Вопрос задан"),
                                                           new IdNameDto(3, "Вопрос принят в работу"),
                                                           new IdNameDto(4, "Ответ на вопрос получен"),
                                                           new IdNameDto(5, "Ответ на вопрос подтвержден")
                                                           //new IdNameDto(5, "Услуга оказана")
                                                           //new IdNameDto(4, "Не одобрен руководителем"),
                                                           //new IdNameDto(5, "Одобрен членом правления"),
                                                           //new IdNameDto(6, "Не одобрен членом правления"),
                                                           //new IdNameDto(7, "Требует одобрения руководителем"),
                                                           //new IdNameDto(8, "Требует одобрения членом правления"),
                                                           //new IdNameDto(9, "Выгружен в 1С"),
                                                       }.OrderBy(x => x.Name).ToList();
            moStatusesList.Insert(0, new IdNameDto(0, SelectAll));
            return moStatusesList;
        }
        public void SetServiceQuestionsListModel(HelpServiceQuestionsListModel model, bool hasError)
        {
            SetDictionariesToModel(model);
            User user = UserDao.Load(model.UserId);
            if (hasError)
                model.Documents = new List<HelpServiceQuestionDto>();
            else
                SetDocumentsToModel(model, user);
        }
        public void SetDocumentsToModel(HelpServiceQuestionsListModel model, User user)
        {
            UserRole role = CurrentUser.UserRole;
            //model.Documents = new List<HelpServiceRequestDto>();
            model.Documents = HelpQuestionRequestDao.GetDocuments(
                user.Id,
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
        #endregion
        #region Question Edit
        public HelpQuestionEditModel GetHelpQuestionEditModel(int id, int? userId)
        {
            IUser current = AuthenticationService.CurrentUser;
            if (id == 0 && !userId.HasValue)
            {
                if ((current.UserRole & UserRole.Employee) == UserRole.Employee || (current.UserRole & UserRole.Manager) == UserRole.Manager || (current.UserRole & UserRole.DismissedEmployee) == UserRole.DismissedEmployee)
                    userId = current.Id;
                else
                    throw new ValidationException(StrQuestionNoUser);
            }
            HelpQuestionRequest entity = null;
            if (id != 0)
                entity = HelpQuestionRequestDao.Load(id);
            HelpQuestionEditModel model = new HelpQuestionEditModel
            {
                Id = id,
                UserId = id == 0 ? userId.Value : entity.User.Id
            };
            User user = UserDao.Load(model.UserId);
            User currUser = UserDao.Load(current.Id);
            if (id == 0)
            {
                entity = new HelpQuestionRequest
                {
                    User = user,
                    Creator = currUser,
                    CreateDate = DateTime.Now,
                    EditDate = DateTime.Now
                };
            }
            else
            {
                model.TypeId = entity.Type.Id;
                model.SubtypeId = entity.Subtype.Id;
                model.Question = entity.Question;
                model.Answer = entity.Answer;
                
                SetStaticFields(model, entity);
            }
            if (!CheckUserRights(currUser, current.UserRole, entity))
            {
                Log.ErrorFormat("GetHelpQuestionEditModel:CheckUserRights return false for user {0}, request {1}", current.Id, entity.Id);
                throw new ArgumentException("Доступ запрещен.");
            }
            model.IsForQuestion = true;
            SetUserInfoModel(user, model);
            LoadDictionaries(model,entity);
            SetFlagsState(id, currUser, entity, model);
            

            SetHiddenFields(model);
            return model;
        }
        public bool CheckUserRights(User current,UserRole currRole, HelpQuestionRequest entity)
        {
            if ((current.Id == entity.User.Id && ((currRole & UserRole.Manager) == UserRole.Manager || (currRole & UserRole.Employee) == UserRole.Employee)) ||
                (current.Id == entity.Creator.Id && (currRole & UserRole.Manager) == UserRole.Manager))
                return true;
            switch (currRole)
            {
                case UserRole.Employee:
                    return entity.User.Id == current.Id;
                case UserRole.DismissedEmployee:
                    return entity.User.Id == current.Id;
                case UserRole.Manager:
                    if (entity.User.Department == null)
                       throw new ArgumentException(string.Format(StrInvalidUserDepartment, entity.User.Id));
                    if (current.Department == null)
                       throw new ArgumentException(string.Format(StrInvalidUserDepartment, current.Id));
                    switch (current.Level)
                    {
                        case 2:
                        case 3:
                            IList<Department> deps = ManualRoleRecordDao.LoadDepartmentsForUserId(current.Id);
                            if ((entity.User.RoleId & (int)UserRole.Employee) > 0)
                                return deps.Any(x => entity.User.Department.Path.Contains(x.Path));
                            if ((entity.User.RoleId & (int)UserRole.Manager) > 0)
                            {
                                User owner = entity.User;
                                if (owner.Level < current.Level || (owner.Level == current.Level &&
                                    ((owner.IsMainManager && !current.IsMainManager) ||
                                    (!owner.IsMainManager && !current.IsMainManager) ||
                                    (owner.IsMainManager && current.IsMainManager))))
                                    return false;
                                return deps.Any(x => entity.User.Department.Path.Contains(x.Path));
                            }
                            throw new ArgumentException(string.Format(StrInvalidHelpRequestOwner, entity.User.Id,
                                   entity.User.RoleId));
                        case 4:
                        case 5:
                        case 6:
                            if ((entity.User.RoleId & (int)UserRole.Employee) > 0)
                                return entity.User.Department.Path.Contains(current.Department.Path);
                            if ((entity.User.RoleId & (int)UserRole.Manager) > 0)
                            {
                                User owner = entity.User;
                                if (owner.Level < current.Level || (owner.Level == current.Level &&
                                    ((owner.IsMainManager && !current.IsMainManager) || 
                                    (!owner.IsMainManager && !current.IsMainManager) ||
                                    (owner.IsMainManager && current.IsMainManager))))
                                    return false;
                                return entity.User.Department.Path.Contains(current.Department.Path);
                            }
                            throw new ArgumentException(string.Format(StrInvalidHelpRequestOwner, entity.User.Id,
                                    entity.User.RoleId));
                        default:
                            throw new ArgumentException(string.Format(StrInvalidManagerLevel, current.Id,
                                current.Level));

                    }
                case UserRole.ConsultantOutsourcing:
                case UserRole.ConsultantPersonnel:
                case UserRole.Accountant:
                case UserRole.OutsourcingManager:
                case UserRole.Estimator:
                case UserRole.PersonnelManager:
                //case UserRole.ConsultantOutsorsingManager:
                case UserRole.Admin:
                    return true;
            }
            return false;
        }

        protected void SetStaticFields(HelpQuestionEditModel model, HelpQuestionRequest entity)
        {
            model.Version = entity.Version;
            model.DocumentNumber = entity.Number.ToString();
            model.DateCreated = FormatDate(entity.CreateDate);
            model.Creator = entity.Creator.FullName;
            
            if (entity.ConsultantOutsourcing != null)
                model.Worker = entity.ConsultantOutsourcing.FullName;
                        
                    
            if (entity.ConsultantPersonnel != null)
                model.Worker = entity.ConsultantPersonnel.FullName;
                        
                   
            if (entity.ConsultantAccountant != null)
                model.Worker = entity.ConsultantAccountant.FullName;
                       
                   
            if (entity.ConsultantOutsorsingManager != null)
                model.Worker = entity.ConsultantOutsorsingManager.FullName;
                        
                
            
            if (entity.SendDate.HasValue)
                model.QuestionSendDate = entity.SendDate.Value.ToShortDateString();
            if (entity.EndWorkDate.HasValue)
                model.WorkerEndDate = entity.EndWorkDate.Value.ToShortDateString();
            if (entity.ConfirmWorkDate.HasValue)
                model.ConfirmDate = entity.ConfirmWorkDate.Value.ToShortDateString();
        }
        protected void SetHiddenFields(HelpQuestionEditModel model)
        {
            model.TypeIdHidden = model.TypeId;
            model.SubtypeIdHidden = model.SubtypeId;
        }
        protected void LoadDictionaries(HelpQuestionEditModel model,HelpQuestionRequest entity)
        {
            LoadHistory(model, entity);
            List<HelpQuestionType> types = HelpQuestionTypeDao.LoadAllSortedByOrder();
            model.Types = types.ConvertAll(x => new IdNameDto { Id = x.Id, Name = x.Name });
            if (model.TypeId == 0)
                model.TypeId = types.First().Id;
            model.Subtypes = HelpQuestionSubtypeDao.LoadForTypeIdSortedByOrder(model.TypeId)
                .ConvertAll(x => new IdNameDto { Id = x.Id, Name = x.Name });
        }
        protected void LoadHistory(HelpQuestionEditModel model, HelpQuestionRequest entity)
        {
            if (entity.HistoryEntities == null || entity.HistoryEntities.Count == 0)
                return;
            model.HistoryEntities = entity.HistoryEntities
                .OrderByDescending(x => x.CreateDate).ToList()
                .ConvertAll(x => new HistoryEntityModel
                                     {
                                         Answer = x.Answer,
                                         CreateDate = x.CreateDate.ToString(),
                                         CreatorName = x.Creator.FullName,
                                         Question = x.Question,
                                         Message = GetMessage(x)
                                     });
                                                                                        
        }
        protected string GetMessage(HelpQuestionHistoryEntity entity)
        {
            switch (entity.Type)
            {
                case 1://send
                    return string.Format("Вопрос задан");
                case 2://begin work
                    return string.Format("Вопрос принят в работу");
                case 3://end work
                    return string.Format("Ответ на вопрос дан");
                case 4://confirm
                    return string.Format("Ответ на вопрос подтвержден");
                case 5://reject
                    return string.Format("Ответ на вопрос не подтвержден");
                case 6://redirect
                    Role targetRole = RoleDao.Load(entity.RecipientRoleId);
                    return string.Format("Заявка перенаправлена роли {0}", targetRole.Name);
                default:
                    throw new ArgumentException(string.Format("Неизвестный тип записи в истории {0}",entity.Type));
            }
        }
        protected void SetFlagsState(int id, User current, HelpQuestionRequest entity, HelpQuestionEditModel model)
        {
            UserRole currentRole = AuthenticationService.CurrentUser.UserRole;
            SetFlagsState(model, false);
            if (model.Id == 0)
            {
                if ((currentRole & UserRole.ConsultantPersonnel) != UserRole.ConsultantPersonnel && (currentRole & UserRole.Manager) != UserRole.Manager && (currentRole & UserRole.Employee) != UserRole.Employee && (currentRole & UserRole.DismissedEmployee) != UserRole.DismissedEmployee)
                    throw new ArgumentException(string.Format(StrUserNotManager, current.Id));
                model.IsTypeEditable = true;
                model.IsQuestionEditable = true;
                model.IsSaveAvailable = true;
                model.IsSendAvailable = true;
                return;
            }
            switch (currentRole)
            {
                case UserRole.Employee:
                    if (entity.Creator.Id == current.Id)
                    {
                        if (!entity.SendDate.HasValue)
                        {
                            if (entity.HistoryEntities == null || entity.HistoryEntities.Count == 0)
                                model.IsTypeEditable = true;
                            model.IsQuestionEditable = true;
                            model.IsSendAvailable = true;
                            model.IsSaveAvailable = true;
                        }
                        if (entity.EndWorkDate.HasValue && !entity.ConfirmWorkDate.HasValue)
                            model.IsEndAvailable = true;
                    }
                    break;
                case UserRole.DismissedEmployee:
                    if (entity.Creator.Id == current.Id)
                    {
                        if (!entity.SendDate.HasValue)
                        {
                            if (entity.HistoryEntities == null || entity.HistoryEntities.Count == 0)
                                model.IsTypeEditable = true;
                            model.IsQuestionEditable = true;
                            model.IsSendAvailable = true;
                            model.IsSaveAvailable = true;
                        }
                        if (entity.EndWorkDate.HasValue && !entity.ConfirmWorkDate.HasValue)
                            model.IsEndAvailable = true;
                    }
                    break;
                case UserRole.Manager:
                    if (entity.Creator.Id == current.Id)
                    {
                        if (!entity.SendDate.HasValue)
                        {
                            if (entity.HistoryEntities == null || entity.HistoryEntities.Count == 0)
                                model.IsTypeEditable = true;
                            model.IsQuestionEditable = true;
                            model.IsSendAvailable = true;
                            model.IsSaveAvailable = true;
                        }
                        if (entity.EndWorkDate.HasValue && !entity.ConfirmWorkDate.HasValue)
                            model.IsEndAvailable = true;
                    }
                    break;
                case UserRole.ConsultantOutsourcing:
                    //if ((entity.ConsultantOutsourcing == null || (entity.ConsultantOutsourcing.Id == current.Id))
                    //    && (!entity.ConsultantRoleId.HasValue || 
                    //         entity.ConsultantRoleId.Value == (int) UserRole.ConsultantOutsourcing))
                    //{
                    //}
                    //могут отвечать на любые открытые вопросы, не важно кому направленные
                    //вытащил кусок из закомментаренного условия

                    if (entity.ConsultantOutsourcing != null && entity.ConsultantOutsourcing.Id == current.Id
                            && entity.BeginWorkDate.HasValue && !entity.EndWorkDate.HasValue)
                        {
                            model.IsEndWorkAvailable = true;
                            model.IsRedirectAvailable = true;
                            model.IsSaveAvailable = true;
                            model.IsAnswerEditable = true;
                        }
                        if (entity.SendDate.HasValue &&  !entity.EndWorkDate.HasValue/*!entity.BeginWorkDate.HasValue*/)
                        {
                            model.IsRedirectAvailable = true;
                            model.IsBeginWorkAvailable = true;
                        }
                        if (entity.EndWorkDate.HasValue && !entity.ConfirmWorkDate.HasValue)
                                model.IsEndAvailable = true;//могут закрывать тему
                        if (!entity.EndWorkDate.HasValue) model.IsBaseAvailable = true;
                        else  model.IsBaseAvailable = entity.Base;
                    break;
                case UserRole.ConsultantPersonnel:
                    if (entity.Creator.Id == current.Id)
                    {
                        if (!entity.SendDate.HasValue)
                        {
                            if (entity.HistoryEntities == null || entity.HistoryEntities.Count == 0)
                                model.IsTypeEditable = true;
                            model.IsQuestionEditable = true;
                            model.IsSendAvailable = true;
                            model.IsSaveAvailable = true;
                        }
                        if (entity.EndWorkDate.HasValue && !entity.ConfirmWorkDate.HasValue)
                            model.IsEndAvailable = true;
                    }
                    if ((entity.ConsultantPersonnel == null || (entity.ConsultantPersonnel.Id == current.Id))
                        && (!entity.ConsultantRoleId.HasValue ||
                             entity.ConsultantRoleId.Value == (int)UserRole.ConsultantPersonnel))
                    {
                        if (entity.ConsultantPersonnel != null && entity.ConsultantPersonnel.Id == current.Id
                            && entity.BeginWorkDate.HasValue && !entity.EndWorkDate.HasValue)
                        {
                            model.IsEndWorkAvailable = true;
                            model.IsRedirectAvailable = true;
                            model.IsSaveAvailable = true;
                            model.IsAnswerEditable = true;
                        }
                        if (entity.SendDate.HasValue && !entity.BeginWorkDate.HasValue)
                        {
                            model.IsRedirectAvailable = true;
                            model.IsBeginWorkAvailable = true;
                        }
                    }
                    break;
                case UserRole.PersonnelManager:
                    if ((entity.ConsultantOutsorsingManager == null || (entity.ConsultantOutsorsingManager.Id == current.Id))
                        && (!entity.ConsultantRoleId.HasValue ||
                             entity.ConsultantRoleId.Value == (int)UserRole.PersonnelManager))
                    {
                        if (//entity.ConsultantOutsorsingManager != null && entity.ConsultantOutsorsingManager.Id == current.Id
                            //&& 
                            entity.BeginWorkDate.HasValue && !entity.EndWorkDate.HasValue)
                        {
                            model.IsEndWorkAvailable = true;
                            model.IsRedirectAvailable = true;//разрешил перенаправлять
                            model.IsSaveAvailable = false;
                            model.IsAnswerEditable = true;
                        }
                        if (entity.SendDate.HasValue && !entity.BeginWorkDate.HasValue)
                        {
                            model.IsRedirectAvailable = false;
                            model.IsBeginWorkAvailable = true;
                        }
                    }
                    break;
                //case UserRole.PersonnelManager:
                //    if ((entity.PersonnelManager == null || (entity.PersonnelManager.Id == current.Id))
                //        && (!entity.ConsultantRoleId.HasValue ||
                //             entity.ConsultantRoleId.Value == (int)UserRole.PersonnelManager))
                //    {
                //        if (entity.PersonnelManager != null && entity.PersonnelManager.Id == current.Id
                //            && entity.BeginWorkDate.HasValue && !entity.EndWorkDate.HasValue)
                //        {
                //            model.IsEndWorkAvailable = true;
                //            model.IsRedirectAvailable = true;
                //            model.IsSaveAvailable = true;
                //            model.IsAnswerEditable = true;
                //        }
                //        if (entity.SendDate.HasValue && !entity.BeginWorkDate.HasValue)
                //        {
                //            model.IsRedirectAvailable = true;
                //            model.IsBeginWorkAvailable = true;
                //        }
                //    }
                //    break;
                case UserRole.Accountant:
                    if ((entity.ConsultantAccountant == null || (entity.ConsultantAccountant.Id == current.Id))
                        && (!entity.ConsultantRoleId.HasValue ||
                             entity.ConsultantRoleId.Value == (int)UserRole.Accountant))
                    {
                        if (entity.ConsultantAccountant != null && entity.ConsultantAccountant.Id == current.Id
                            && entity.BeginWorkDate.HasValue && !entity.EndWorkDate.HasValue)
                        {
                            model.IsEndWorkAvailable = true;
                            model.IsRedirectAvailable = true;
                            model.IsSaveAvailable = true;
                            model.IsAnswerEditable = true;
                        }
                        if (entity.SendDate.HasValue && !entity.BeginWorkDate.HasValue)
                        {
                            model.IsRedirectAvailable = true;
                            model.IsBeginWorkAvailable = true;
                        }
                    }
                    break;
            }
        }
        protected void SetFlagsState(HelpQuestionEditModel model, bool state)
        {
            model.IsTypeEditable = state;
            model.IsQuestionEditable = state;
            model.IsAnswerEditable = state;

            model.IsBeginWorkAvailable = state;
            model.IsEndAvailable = state;
            model.IsEndWorkAvailable = state;
            model.IsSaveAvailable = state;
            model.IsSendAvailable = state;
            model.IsRedirectAvailable = state;
        }

        public void GetSubtypesForType(int typeId, HelpQuestionSubtypesModel model)
        {
            model.Subtypes = HelpQuestionSubtypeDao.LoadForTypeIdSortedByOrder(typeId)
                 .ConvertAll(x => new IdNameDto { Id = x.Id, Name = x.Name });
        }
        public void ReloadDictionariesToModel(HelpQuestionEditModel model)
        {

            HelpQuestionRequest entity = HelpQuestionRequestDao.Load(model.Id);
            LoadDictionaries(model,entity);
        }
        public bool SaveHelpQuestionEditModel(HelpQuestionEditModel model, out string error)
        {
            error = string.Empty;
            User currUser = null;
            User user = null;
            HelpQuestionRequest entity = null;
            try
            {

                IUser current = AuthenticationService.CurrentUser;
                currUser = UserDao.Load(current.Id);
                user = UserDao.Load(model.UserId);

                /*if (model.Id != 0)
                    entity = AppointmentDao.Get(model.Id);*/
                /*if (!CheckUserMoRights(user, current, model.Id, entity, true))
                {
                    error = "Редактирование заявки запрещено";
                    return false;
                }*/

                if (model.Id == 0)
                {
                    entity = new HelpQuestionRequest
                    {
                        CreateDate = DateTime.Now,
                        Creator = currUser,//UserDao.Load(current.Id),
                        Number = RequestNextNumberDao.GetNextNumberForType((int)RequestTypeEnum.HelpQuestionRequest),
                        EditDate = DateTime.Now,
                        User = user,
                        CreatorRoleId = (int)(current.Id == user.Id?current.UserRole:UserRole.Employee),
                        HistoryEntities = new List<HelpQuestionHistoryEntity>()
                    };
                    ChangeEntityProperties(entity, model, currUser, out error);
                    HelpQuestionRequestDao.SaveAndFlush(entity);
                    model.Id = entity.Id;
                    model.DocumentNumber = entity.Number.ToString();
                    model.DateCreated = entity.CreateDate.ToShortDateString();
                    model.Creator = entity.Creator.FullName;
                }
                else
                {
                    entity = HelpQuestionRequestDao.Get(model.Id);
                    if (entity == null)
                        throw new ValidationException(string.Format(StrQuestionRequestNotFound, model.Id));
                    if (entity.Version != model.Version)
                    {
                        error = StrServiceRequestWasChanged;
                        model.ReloadPage = true;
                        return false;
                    }
                    ChangeEntityProperties(entity, model, currUser, out error);
                    HelpQuestionRequestDao.SaveAndFlush(entity);
                    if (entity.Version != model.Version)
                    {
                        if((entity.Creator!=null && entity.Creator.Id==CurrentUser.Id) || (entity.User!=null && entity.User.Id==CurrentUser.Id))
                            entity.EditDate = DateTime.Now;
                        HelpQuestionRequestDao.SaveAndFlush(entity);
                    }
                }
                //if (entity.DeleteDate.HasValue)
                //    model.IsDeleted = true;
                //}
                //model.Version = entity.Version;
                /*model.Worker = entity.Consultant != null ? entity.Consultant.FullName : string.Empty;
                model.WorkerEndDate = entity.EndWorkDate.HasValue ?
                                      entity.EndWorkDate.Value.ToShortDateString() : string.Empty;
                model.ConfirmDate = entity.ConfirmWorkDate.HasValue ?
                                     entity.ConfirmWorkDate.Value.ToShortDateString() : string.Empty;*/
                SetStaticFields(model, entity);
                SetFlagsState(entity.Id, currUser, entity, model);
                return true;
            }
            catch (Exception ex)
            {
                HelpServiceRequestDao.RollbackTran();
                Log.Error("Error on SaveServiceRequestEditModel:", ex);
                error = StrException + ex.GetBaseException().Message;
                return false;
            }
            finally
            {
                //SetUserInfoModel(user, model);
                LoadDictionaries(model,entity);
                SetHiddenFields(model);
            }
        }
        protected void ChangeEntityProperties(HelpQuestionRequest entity, HelpQuestionEditModel model,
            User currUser, out string error)
        {
            error = string.Empty;
            UserRole currRole = AuthenticationService.CurrentUser.UserRole;
            if (model.IsTypeEditable)
            {
                entity.Type = HelpQuestionTypeDao.Load(model.TypeId);
                entity.Subtype = HelpQuestionSubtypeDao.Load(model.SubtypeId);
            }
            if (model.IsQuestionEditable)
                entity.Question = model.Question;
            if(model.IsAnswerEditable)
                entity.Answer = model.Answer;
            switch (currRole)
            {
                case UserRole.Employee:
                    if (entity.Creator.Id == currUser.Id)
                    {
                        if (model.Operation == 1 && !entity.SendDate.HasValue)
                        {
                            entity.SendDate = DateTime.Now;
                            entity.ConsultantRoleId = (int)UserRole.ConsultantOutsourcing;
                            HelpQuestionHistoryEntity send = new HelpQuestionHistoryEntity
                            {
                                CreateDate = DateTime.Now,
                                Creator = currUser,
                                CreatorRoleId = (int)currRole,
                                Question = entity.Question,
                                RecipientRoleId = (int)UserRole.ConsultantOutsourcing,
                                Request = entity,
                                Type = 1,// send
                            };
                            entity.HistoryEntities.Add(send);
                        }
                        if (entity.EndWorkDate.HasValue)
                        {
                            if (model.Operation == 4)
                            {
                                entity.ConfirmWorkDate = DateTime.Now;
                                HelpQuestionHistoryEntity confirm = new HelpQuestionHistoryEntity
                                {
                                    Answer = entity.Answer,
                                    CreateDate = DateTime.Now,
                                    Creator = currUser,
                                    CreatorRoleId = (int)currRole,
                                    Question = entity.Question,
                                    RecipientRoleId = (int)currRole,
                                    Request = entity,
                                    Type = 4,// confirm
                                };
                                entity.HistoryEntities.Add(confirm);
                            }
                            else if (model.Operation == 5)
                            {
                                entity.SendDate = null;
                                entity.BeginWorkDate = null;
                                entity.EndWorkDate = null;
                                HelpQuestionHistoryEntity reject = new HelpQuestionHistoryEntity
                                {
                                    Answer = entity.Answer,
                                    CreateDate = DateTime.Now,
                                    Creator = currUser,
                                    CreatorRoleId = (int)currRole,
                                    Question = entity.Question,
                                    RecipientRoleId = (int)currRole,
                                    Request = entity,
                                    Type = 5,// reject
                                };
                                entity.HistoryEntities.Add(reject);
                                model.Answer = null;
                                model.Question = null;
                                entity.Answer = null;
                                entity.Question = null;
                                entity.ConsultantRoleId = null;
                            }
                        }
                    }
                    break;
                case UserRole.DismissedEmployee:
                    if (entity.Creator.Id == currUser.Id)
                    {
                        if (model.Operation == 1 && !entity.SendDate.HasValue)
                        {
                            entity.SendDate = DateTime.Now;
                            entity.ConsultantRoleId = (int)UserRole.ConsultantOutsourcing;
                            HelpQuestionHistoryEntity send = new HelpQuestionHistoryEntity
                            {
                                CreateDate = DateTime.Now,
                                Creator = currUser,
                                CreatorRoleId = (int)currRole,
                                Question = entity.Question,
                                RecipientRoleId = (int)UserRole.ConsultantOutsourcing,
                                Request = entity,
                                Type = 1,// send
                            };
                            entity.HistoryEntities.Add(send);
                        }
                        if (entity.EndWorkDate.HasValue)
                        {
                            if (model.Operation == 4)
                            {
                                entity.ConfirmWorkDate = DateTime.Now;
                                HelpQuestionHistoryEntity confirm = new HelpQuestionHistoryEntity
                                {
                                    Answer = entity.Answer,
                                    CreateDate = DateTime.Now,
                                    Creator = currUser,
                                    CreatorRoleId = (int)currRole,
                                    Question = entity.Question,
                                    RecipientRoleId = (int)currRole,
                                    Request = entity,
                                    Type = 4,// confirm
                                };
                                entity.HistoryEntities.Add(confirm);
                            }
                            else if (model.Operation == 5)
                            {
                                entity.SendDate = null;
                                entity.BeginWorkDate = null;
                                entity.EndWorkDate = null;
                                HelpQuestionHistoryEntity reject = new HelpQuestionHistoryEntity
                                {
                                    Answer = entity.Answer,
                                    CreateDate = DateTime.Now,
                                    Creator = currUser,
                                    CreatorRoleId = (int)currRole,
                                    Question = entity.Question,
                                    RecipientRoleId = (int)currRole,
                                    Request = entity,
                                    Type = 5,// reject
                                };
                                entity.HistoryEntities.Add(reject);
                                model.Answer = null;
                                model.Question = null;
                                entity.Answer = null;
                                entity.Question = null;
                                entity.ConsultantRoleId = null;
                            }
                        }
                    }
                    break;
                case UserRole.Manager:
                    if (entity.Creator.Id == currUser.Id)
                    {
                        if (model.Operation == 1 && !entity.SendDate.HasValue)
                        {
                            entity.SendDate = DateTime.Now;
                            entity.ConsultantRoleId = (int) UserRole.ConsultantOutsourcing;
                            HelpQuestionHistoryEntity send = new HelpQuestionHistoryEntity
                            {
                                CreateDate = DateTime.Now,
                                Creator = currUser,
                                CreatorRoleId = (int)currRole,
                                Question = entity.Question,
                                RecipientRoleId = (int)UserRole.ConsultantOutsourcing,
                                Request = entity,
                                Type = 1,// send
                            };
                            entity.HistoryEntities.Add(send);
                        }
                        if (entity.EndWorkDate.HasValue)
                        {
                            if (model.Operation == 4)
                            {
                                entity.ConfirmWorkDate = DateTime.Now;
                                HelpQuestionHistoryEntity confirm = new HelpQuestionHistoryEntity
                                {
                                    Answer = entity.Answer,
                                    CreateDate = DateTime.Now,
                                    Creator = currUser,
                                    CreatorRoleId = (int)currRole,
                                    Question = entity.Question,
                                    RecipientRoleId = (int)currRole,
                                    Request = entity,
                                    Type = 4,// confirm
                                };
                                entity.HistoryEntities.Add(confirm);
                            }
                            else if (model.Operation == 5)
                            {
                                entity.SendDate = null;
                                entity.BeginWorkDate = null;
                                entity.EndWorkDate = null;
                                HelpQuestionHistoryEntity reject = new HelpQuestionHistoryEntity
                                {
                                    Answer = entity.Answer,
                                    CreateDate = DateTime.Now,
                                    Creator = currUser,
                                    CreatorRoleId = (int)currRole,
                                    Question = entity.Question,
                                    RecipientRoleId = (int)currRole,
                                    Request = entity,
                                    Type = 5,// reject
                                };
                                entity.HistoryEntities.Add(reject);
                                model.Answer = null;
                                model.Question = null;
                                entity.Answer = null;
                                entity.Question = null;
                            }
                        }
                    }
                    break;
                case UserRole.ConsultantOutsourcing:
                    //закомментарил чтобы консультанты могли перекрывать друг друга
                    //if (entity.ConsultantOutsourcing == null || (entity.ConsultantOutsourcing.Id == currUser.Id))
                    //{
                        if (model.Operation == 2 && entity.SendDate.HasValue)
                        {
                            entity.BeginWorkDate = DateTime.Now;
                            entity.ConsultantOutsourcing = currUser;
                            entity.ConsultantRoleId = (int) UserRole.ConsultantOutsourcing;
                            HelpQuestionHistoryEntity beginWork = new HelpQuestionHistoryEntity
                            {
                                //Answer = entity.Answer,
                                Consultant = currUser,
                                CreateDate = DateTime.Now,
                                Creator = currUser,
                                CreatorRoleId = (int)currRole,
                                Question = entity.Question,
                                RecipientRoleId = (int)currRole,
                                Request = entity,
                                Type = 2,// beginWork
                            };
                            entity.HistoryEntities.Add(beginWork);
                            

                        }
                        if (/*entity.ConsultantOutsourcing != null && entity.ConsultantOutsourcing.Id == currUser.Id
                            &&*/ model.Operation == 3 && entity.BeginWorkDate.HasValue)
                        {
                            entity.EndWorkDate = DateTime.Now;
                            HelpQuestionHistoryEntity endWork = new HelpQuestionHistoryEntity
                            {
                                Answer = entity.Answer,
                                Consultant = currUser,
                                CreateDate = DateTime.Now,
                                Creator = currUser,
                                CreatorRoleId = (int)currRole,
                                Question = entity.Question,
                                RecipientRoleId = (int)currRole,
                                Request = entity,
                                Type = 3,// endWork
                            };
                            entity.HistoryEntities.Add(endWork);
                        }

                        if (entity.EndWorkDate.HasValue)//можно закрыть тему
                        {
                            if (model.Operation == 4)
                            {
                                entity.ConfirmWorkDate = DateTime.Now;
                                HelpQuestionHistoryEntity confirm = new HelpQuestionHistoryEntity
                                {
                                    Answer = entity.Answer,
                                    CreateDate = DateTime.Now,
                                    Creator = currUser,
                                    CreatorRoleId = (int)currRole,
                                    Question = entity.Question,
                                    RecipientRoleId = (int)currRole,
                                    Request = entity,
                                    Type = 4,// confirm
                                };
                                entity.HistoryEntities.Add(confirm);
                            }
                        }

                        if (model.Operation == 6 && entity.SendDate.HasValue && !entity.EndWorkDate.HasValue) //redirect
                        {
                            entity.ConsultantRoleId = model.RedirectRoleId;
                            entity.BeginWorkDate = null;
                            HelpQuestionHistoryEntity redirect = new HelpQuestionHistoryEntity
                                                                     {
                                                                         Answer = entity.Answer,
                                                                         Consultant = currUser,
                                                                         CreateDate = DateTime.Now,
                                                                         Creator = currUser,
                                                                         CreatorRoleId = (int)currRole,
                                                                         Question = entity.Question,
                                                                         RecipientRoleId = model.RedirectRoleId,
                                                                         Request = entity,
                                                                         Type = 6,// redirect
                                                                     };
                            entity.HistoryEntities.Add(redirect);
                            entity.Answer = null;
                            model.Answer = null;
                        }
                        if (model.Operation == 7)
                        {
                            entity.Base = true;
                        }
                    //}
                    break;
                case UserRole.ConsultantPersonnel:
                    if (entity.ConsultantPersonnel == null || (entity.ConsultantPersonnel.Id == currUser.Id))
                    {
                        if (model.Operation == 1 && !entity.SendDate.HasValue)
                        {
                            entity.SendDate = DateTime.Now;
                            entity.ConsultantRoleId = (int)UserRole.ConsultantOutsourcing;
                            HelpQuestionHistoryEntity send = new HelpQuestionHistoryEntity
                            {
                                CreateDate = DateTime.Now,
                                Creator = currUser,
                                CreatorRoleId = (int)currRole,
                                Question = entity.Question,
                                RecipientRoleId = (int)UserRole.ConsultantOutsourcing,
                                Request = entity,
                                Type = 1,// send
                            };
                            entity.HistoryEntities.Add(send);
                        }
                        if (model.Operation == 2 && entity.SendDate.HasValue)
                        {
                            entity.BeginWorkDate = DateTime.Now;
                            entity.ConsultantPersonnel = currUser;
                            entity.ConsultantRoleId = (int)UserRole.ConsultantPersonnel;
                            HelpQuestionHistoryEntity beginWork = new HelpQuestionHistoryEntity
                            {
                                //Answer = entity.Answer,
                                Consultant = currUser,
                                CreateDate = DateTime.Now,
                                Creator = currUser,
                                CreatorRoleId = (int)currRole,
                                Question = entity.Question,
                                RecipientRoleId = (int)currRole,
                                Request = entity,
                                Type = 2,// beginWork
                            };
                            entity.HistoryEntities.Add(beginWork);
                        }
                        if (entity.ConsultantPersonnel != null && entity.ConsultantPersonnel.Id == currUser.Id
                            && model.Operation == 3 && entity.BeginWorkDate.HasValue)
                        {
                            entity.EndWorkDate = DateTime.Now;
                            HelpQuestionHistoryEntity endWork = new HelpQuestionHistoryEntity
                            {
                                Answer = entity.Answer,
                                Consultant = currUser,
                                CreateDate = DateTime.Now,
                                Creator = currUser,
                                CreatorRoleId = (int)currRole,
                                Question = entity.Question,
                                RecipientRoleId = (int)currRole,
                                Request = entity,
                                Type = 3,// endWork
                            };
                            entity.HistoryEntities.Add(endWork);
                        }
                        if (model.Operation == 6 && entity.SendDate.HasValue && !entity.EndWorkDate.HasValue) //redirect
                        {
                            entity.ConsultantRoleId = model.RedirectRoleId;
                            entity.BeginWorkDate = null;
                            HelpQuestionHistoryEntity redirect = new HelpQuestionHistoryEntity
                            {
                                Answer = entity.Answer,
                                Consultant = currUser,
                                CreateDate = DateTime.Now,
                                Creator = currUser,
                                CreatorRoleId = (int)currRole,
                                Question = entity.Question,
                                RecipientRoleId = model.RedirectRoleId,
                                Request = entity,
                                Type = 6,// redirect
                            };
                            entity.HistoryEntities.Add(redirect);
                            entity.Answer = null;
                            model.Answer = null;
                        }
                    }
                    break;
                case UserRole.Accountant:
                    if (entity.ConsultantAccountant == null || (entity.ConsultantAccountant.Id == currUser.Id))
                    {
                        if (model.Operation == 2 && entity.SendDate.HasValue)
                        {
                            entity.BeginWorkDate = DateTime.Now;
                            entity.ConsultantAccountant = currUser;
                            entity.ConsultantRoleId = (int)UserRole.Accountant;
                            HelpQuestionHistoryEntity beginWork = new HelpQuestionHistoryEntity
                            {
                                //Answer = entity.Answer,
                                Consultant = currUser,
                                CreateDate = DateTime.Now,
                                Creator = currUser,
                                CreatorRoleId = (int)currRole,
                                Question = entity.Question,
                                RecipientRoleId = (int)currRole,
                                Request = entity,
                                Type = 2,// beginWork
                            };
                            entity.HistoryEntities.Add(beginWork);
                        }
                        if (entity.ConsultantAccountant != null && entity.ConsultantAccountant.Id == currUser.Id
                            && model.Operation == 3 && entity.BeginWorkDate.HasValue)
                        {
                            entity.EndWorkDate = DateTime.Now;
                            HelpQuestionHistoryEntity endWork = new HelpQuestionHistoryEntity
                            {
                                Answer = entity.Answer,
                                Consultant = currUser,
                                CreateDate = DateTime.Now,
                                Creator = currUser,
                                CreatorRoleId = (int)currRole,
                                Question = entity.Question,
                                RecipientRoleId = (int)currRole,
                                Request = entity,
                                Type = 3,// endWork
                            };
                            entity.HistoryEntities.Add(endWork);
                        }
                        if (model.Operation == 6 && entity.SendDate.HasValue && !entity.EndWorkDate.HasValue) //redirect
                        {
                            entity.ConsultantRoleId = model.RedirectRoleId;
                            entity.BeginWorkDate = null;
                            HelpQuestionHistoryEntity redirect = new HelpQuestionHistoryEntity
                            {
                                Answer = entity.Answer,
                                Consultant = currUser,
                                CreateDate = DateTime.Now,
                                Creator = currUser,
                                CreatorRoleId = (int)currRole,
                                Question = entity.Question,
                                RecipientRoleId = model.RedirectRoleId,
                                Request = entity,
                                Type = 6,// redirect
                            };
                            entity.HistoryEntities.Add(redirect);
                            entity.Answer = null;
                            model.Answer = null;
                        }
                    }
                    break;
                //case UserRole.PersonnelManager:
                //    if (entity.PersonnelManager == null || (entity.PersonnelManager.Id == currUser.Id))
                //    {
                //        if (model.Operation == 2 && entity.SendDate.HasValue)
                //        {
                //            entity.BeginWorkDate = DateTime.Now;
                //            entity.PersonnelManager = currUser;
                //            entity.ConsultantRoleId = (int)UserRole.PersonnelManager;
                //            HelpQuestionHistoryEntity beginWork = new HelpQuestionHistoryEntity
                //            {
                //                //Answer = entity.Answer,
                //                Consultant = currUser,
                //                CreateDate = DateTime.Now,
                //                Creator = currUser,
                //                CreatorRoleId = (int)currRole,
                //                Question = entity.Question,
                //                RecipientRoleId = (int)currRole,
                //                Request = entity,
                //                Type = 2,// beginWork
                //            };
                //            entity.HistoryEntities.Add(beginWork);
                //        }
                //        if (entity.PersonnelManager != null && entity.PersonnelManager.Id == currUser.Id
                //            && model.Operation == 3 && entity.BeginWorkDate.HasValue)
                //        {
                //            entity.EndWorkDate = DateTime.Now;
                //            HelpQuestionHistoryEntity endWork = new HelpQuestionHistoryEntity
                //            {
                //                Answer = entity.Answer,
                //                Consultant = currUser,
                //                CreateDate = DateTime.Now,
                //                Creator = currUser,
                //                CreatorRoleId = (int)currRole,
                //                Question = entity.Question,
                //                RecipientRoleId = (int)currRole,
                //                Request = entity,
                //                Type = 3,// endWork
                //            };
                //            entity.HistoryEntities.Add(endWork);
                //        }
                //        if (model.Operation == 6 && entity.SendDate.HasValue && !entity.EndWorkDate.HasValue) //redirect
                //        {
                //            entity.ConsultantRoleId = model.RedirectRoleId;
                //            entity.BeginWorkDate = null;
                //            HelpQuestionHistoryEntity redirect = new HelpQuestionHistoryEntity
                //            {
                //                Answer = entity.Answer,
                //                Consultant = currUser,
                //                CreateDate = DateTime.Now,
                //                Creator = currUser,
                //                CreatorRoleId = (int)currRole,
                //                Question = entity.Question,
                //                RecipientRoleId = model.RedirectRoleId,
                //                Request = entity,
                //                Type = 6,// redirect
                //            };
                //            entity.HistoryEntities.Add(redirect);
                //            entity.Answer = null;
                //            model.Answer = null;
                //        }
                //    }
                //    break;
                case UserRole.PersonnelManager:
                    if (entity.ConsultantOutsorsingManager == null || (entity.ConsultantOutsorsingManager.Id == currUser.Id))
                    {
                        if (model.Operation == 2 && entity.SendDate.HasValue)
                        {
                            entity.BeginWorkDate = DateTime.Now;
                            entity.ConsultantOutsorsingManager = currUser;
                            entity.ConsultantRoleId = (int)UserRole.PersonnelManager;
                            HelpQuestionHistoryEntity beginWork = new HelpQuestionHistoryEntity
                            {
                                //Answer = entity.Answer,
                                Consultant = currUser,
                                CreateDate = DateTime.Now,
                                Creator = currUser,
                                CreatorRoleId = (int)currRole,
                                Question = entity.Question,
                                RecipientRoleId = (int)currRole,
                                Request = entity,
                                Type = 2,// beginWork
                            };
                            entity.HistoryEntities.Add(beginWork);
                        }
                        if (//entity.ConsultantOutsorsingManager != null && entity.ConsultantOutsorsingManager.Id == currUser.Id
                            //&& 
                            model.Operation == 3 && entity.BeginWorkDate.HasValue)
                        {
                            entity.EndWorkDate = DateTime.Now;
                            HelpQuestionHistoryEntity endWork = new HelpQuestionHistoryEntity
                            {
                                Answer = entity.Answer,
                                Consultant = currUser,
                                CreateDate = DateTime.Now,
                                Creator = currUser,
                                CreatorRoleId = (int)currRole,
                                Question = entity.Question,
                                RecipientRoleId = (int)currRole,
                                Request = entity,
                                Type = 3,// endWork
                            };
                            entity.HistoryEntities.Add(endWork);
                        }
                        if (model.Operation == 6 && entity.SendDate.HasValue && !entity.EndWorkDate.HasValue) //redirect
                        {
                            entity.ConsultantRoleId = model.RedirectRoleId;
                            entity.BeginWorkDate = null;
                            HelpQuestionHistoryEntity redirect = new HelpQuestionHistoryEntity
                            {
                                Answer = entity.Answer,
                                Consultant = currUser,
                                CreateDate = DateTime.Now,
                                Creator = currUser,
                                CreatorRoleId = (int)currRole,
                                Question = entity.Question,
                                RecipientRoleId = model.RedirectRoleId,
                                Request = entity,
                                Type = 6,// redirect
                            };
                            entity.HistoryEntities.Add(redirect);
                            entity.Answer = null;
                            model.Answer = null;
                        }
                    }
                    break;
            }
        }
        public HelpQuestionRedirectModel GetQuestionRedirectModel(int id)
        {
            HelpQuestionRedirectModel model = new HelpQuestionRedirectModel{Roles = new List<IdNameDto>()};
            HelpQuestionRequest entity = helpQuestionRequestDao.Load(id);
            if ((entity.ConsultantRoleId.HasValue &&
                entity.ConsultantRoleId.Value != (int)UserRole.ConsultantOutsourcing &&
               entity.ConsultantRoleId.Value != (int)UserRole.ConsultantPersonnel &&
               entity.ConsultantRoleId.Value != (int)UserRole.Accountant &&
               entity.ConsultantRoleId.Value != (int)UserRole.PersonnelManager) ||//&&
               //entity.ConsultantRoleId.Value != (int)UserRole.PersonnelManager) ||
               !entity.SendDate.HasValue || entity.EndWorkDate.HasValue)
                throw new ArgumentException("В текущем состоянии перенаправление заявки невозможно");
            if ((CurrentUser.UserRole & UserRole.ConsultantOutsourcing) != UserRole.ConsultantOutsourcing &&
                    (CurrentUser.UserRole & UserRole.ConsultantPersonnel) != UserRole.ConsultantPersonnel &&
                    CurrentUser.UserRole != UserRole.Accountant &&
                    CurrentUser.UserRole != UserRole.PersonnelManager)// &&
                    //CurrentUser.UserRole != UserRole.PersonnelManager)
                throw new ArgumentException("Перенаправление заявки невозможно - неверная роль пользователя");
            List<Role> roles = RoleDao.LoadAll().ToList();
            if ((CurrentUser.UserRole & UserRole.ConsultantOutsourcing) != UserRole.ConsultantOutsourcing)
            {
                Role role = GetRoleForId(roles, (int) UserRole.ConsultantOutsourcing);
                model.Roles.Add(new IdNameDto{Id = role.Id,Name = role.Name});
            }
            if ((CurrentUser.UserRole & UserRole.PersonnelManager) != UserRole.PersonnelManager)
            {
                Role role = GetRoleForId(roles, (int)UserRole.PersonnelManager);
                model.Roles.Add(new IdNameDto { Id = role.Id, Name = role.Name });
            }
            if (CurrentUser.UserRole == UserRole.PersonnelManager) return model;//для консультантов КО разрешаем перенаправление вопроса консультанту аутсора

            if ((CurrentUser.UserRole & UserRole.ConsultantPersonnel) != UserRole.ConsultantPersonnel)
            {
                Role role = GetRoleForId(roles, (int)UserRole.ConsultantPersonnel);
                model.Roles.Add(new IdNameDto { Id = role.Id, Name = role.Name });
            }
            if ((CurrentUser.UserRole & UserRole.Accountant) != UserRole.Accountant)
            {
                Role role = GetRoleForId(roles, (int)UserRole.Accountant);
                model.Roles.Add(new IdNameDto { Id = role.Id, Name = role.Name });
            }
            /*if (CurrentUser.UserRole != UserRole.ConsultantOutsorsingManager)
            {
                Role role = GetRoleForId(roles, (int)UserRole.ConsultantOutsorsingManager);
                model.Roles.Add(new IdNameDto { Id = role.Id, Name = role.Name });
            }*/
            return model;
        }
        protected Role GetRoleForId(List<Role> roles,int roleId)
        {
            Role role = roles.Where(x => x.Id == roleId).FirstOrDefault();
            if (role == null)
                throw new ArgumentException(string.Format("Роль (Id {0}) отсутствует в базе данных", roleId));
            return role;
        }
        #endregion
        #region Version
        public HelpVersionsListModel GetVersionsModel()
        {
            return new HelpVersionsListModel
                       {
                           IsAddAvailable = (AuthenticationService.CurrentUser.UserRole & UserRole.Admin) == UserRole.Admin,
                           Versions = HelpVersionDao.LoadAllSortedByDate().ConvertAll(
                                x => new HelpVersionDto
                                         {
                                             Id = x.Id,
                                             Comment = x.Comment,
                                             ReleaseDate = x.ReleaseDate
                                         }
                           )
                       };
        }
        public HelpEditVersionModel GetEditVersionModel(int id)
        {
            HelpEditVersionModel model = new HelpEditVersionModel {Id = id};
            if (id == 0)
                model.ReleaseDate = DateTime.Today.ToShortDateString();
            else
            {
                HelpVersion version = HelpVersionDao.Load(id);
                model.ReleaseDate = version.ReleaseDate.ToShortDateString();
                model.Comment = version.Comment;
            }
            return model;
        }
        public bool SaveVersion(HelpSaveVersionModel model)
        {
            try
            {
                if (AuthenticationService.CurrentUser.UserRole != UserRole.Admin)
                {
                    model.Error = StrCannotEditVersion;
                    return false;
                }
                User user = UserDao.Load(AuthenticationService.CurrentUser.Id);
                HelpVersion entity = new HelpVersion();                
                if(model.Id > 0)
                    entity = HelpVersionDao.Load(model.Id);
                entity.Comment = model.Comment;
                entity.DateCreated = DateTime.Now;
                entity.User = user;
                entity.ReleaseDate = model.ReleaseDate;
                HelpVersionDao.SaveAndFlush(entity);
                return true;
            }
            catch (Exception ex)
            {
                helpVersionDao.RollbackTran();
                Log.Error("Exception", ex);
                model.Error = StrException + ex.GetBaseException().Message;
                return false;
            }
        }
        public bool DeleteVersion(DeleteAttacmentModel model)
        {
            if (AuthenticationService.CurrentUser.UserRole != UserRole.Admin)
            {
                model.Error = StrCannotDeleteVersion;
                return false;
            }
            helpVersionDao.DeleteAndFlush(model.Id);
            return true;
        }
        #endregion
        #region Faq
        public HelpFaqListModel GetFaqModel()
        {
            return new HelpFaqListModel
            {
                IsAddAvailable = (AuthenticationService.CurrentUser.UserRole & UserRole.Admin) == UserRole.Admin,
                Questions = HelpFaqDao.LoadAllSortedByQuestion().ConvertAll(
                     x => new HelpFaqDto
                     {
                         Id = x.Id,
                         Question = x.Question,
                         Answer = x.Answer
                     }
                )
            };
        }
        public HelpEditFaqModel GetEditQuestionModel(int id)
        {
            HelpEditFaqModel model = new HelpEditFaqModel { Id = id };
            if (id != 0)
            {
                HelpFaq entity = HelpFaqDao.Load(id);
                model.Question = entity.Question;
                model.Answer = entity.Answer;
            }
            return model;
        }
        public bool SaveFaq(HelpSaveFaqModel model)
        {
            try
            {
                if (AuthenticationService.CurrentUser.UserRole != UserRole.Admin)
                {
                    model.Error = StrCannotEditFaq;
                    return false;
                }
                User user = UserDao.Load(AuthenticationService.CurrentUser.Id);
                HelpFaq entity = new HelpFaq();
                if (model.Id > 0)
                    entity = HelpFaqDao.Load(model.Id);
                entity.Question = model.Question;
                entity.Answer = model.Answer;
                entity.DateCreated = DateTime.Now;
                entity.User = user;
                HelpFaqDao.SaveAndFlush(entity);
                return true;
            }
            catch (Exception ex)
            {
                helpFaqDao.RollbackTran();
                Log.Error("Exception", ex);
                model.Error = StrException + ex.GetBaseException().Message;
                return false;
            }
        }
        public bool DeleteFaq(DeleteAttacmentModel model)
        {
            if (AuthenticationService.CurrentUser.UserRole != UserRole.Admin)
            {
                model.Error = StrCannotDeleteFaq;
                return false;
            }
            helpFaqDao.DeleteAndFlush(model.Id);
            return true;
        }
        #endregion
        #region Template
        public HelpTemplateListModel GetTemplateModel()
        {
            return new HelpTemplateListModel
            {
                IsAddAvailable = (AuthenticationService.CurrentUser.UserRole & UserRole.Admin) == UserRole.Admin,
                Documents = RequestAttachmentDao.FindManyByRequestIdAndTypeId(0,RequestAttachmentTypeEnum.HelpTemplate).
                            ToList().ConvertAll(x => new HelpTemplateDto
                            {
                                Id = x.Id,
                                AttachmentId = x.Id,
                                Name = x.Description,
                            }).OrderBy(x => x.Name).ToList()
            };
        }
        public HelpTemplateEditModel GetTemplateEditModel(int id)
        {
            HelpTemplateEditModel model = new HelpTemplateEditModel { Id = id };
            if (id != 0)
            {
                RequestAttachment entity = RequestAttachmentDao.Load(id);
                model.Id = entity.Id;
                model.Name = entity.Description;
            }
            return model;
        }

        public bool SaveTemplate(SaveAttacmentModel model)
        {
            if (AuthenticationService.CurrentUser.UserRole != UserRole.Admin)
            {
                model.Error = StrCannotEditFaq;
                return false;
            }
            RequestAttachment attach;
            if (model.Id == 0)
            {
                attach = new RequestAttachment
                    {
                        ContextType = RequestBl.GetFileContext(model.FileDto.FileName),
                        DateCreated = DateTime.Now,
                        Description = model.Description,
                        FileName = model.FileDto.FileName,
                        RequestId = 0,
                        RequestType = (int) model.EntityTypeId,
                        UncompressContext = model.FileDto.Context,
                        CreatorRole = RoleDao.Load((int) CurrentUser.UserRole)
                    };
            }
            else
            {
                attach = RequestAttachmentDao.Load(model.Id);
                attach.ContextType = RequestBl.GetFileContext(model.FileDto.FileName);
                attach.DateCreated = DateTime.Now;
                attach.Description = model.Description;
                attach.FileName = model.FileDto.FileName;
                attach.UncompressContext = model.FileDto.Context;
                attach.CreatorRole = RoleDao.Load((int) CurrentUser.UserRole);
            }
            RequestAttachmentDao.SaveAndFlush(attach);
            model.Id = attach.Id;
            return true;
        }
        public bool SaveTemplateName(SaveAttacmentModel model)
        {
            if (AuthenticationService.CurrentUser.UserRole != UserRole.Admin)
            {
                model.Error = StrCannotEditFaq;
                return false;
            }
            RequestAttachment attach = RequestAttachmentDao.Load(model.Id);
            attach.DateCreated = DateTime.Now;
            attach.Description = model.Description;
            attach.CreatorRole = RoleDao.Load((int)CurrentUser.UserRole);
            RequestAttachmentDao.SaveFileNotChangeAndFlush(attach);
            return true;
        }

        #endregion
        #region Personnel Billing List
        public HelpPersonnelBillingListModel GetPersonnelBillingList()
        {
            User user = UserDao.Load(AuthenticationService.CurrentUser.Id);
            IdNameReadonlyDto dep = GetDepartmentDto(user);
            HelpPersonnelBillingListModel model = new HelpPersonnelBillingListModel
            {
                UserId = AuthenticationService.CurrentUser.Id,
                DepartmentName = dep.Name,
                DepartmentId = dep.Id,
                //DepartmentReadOnly = dep.IsReadOnly,
            };
            SetInitialDates(model);
            SetDictionariesToModel(model);
            //SetInitialStatus(model);
            //SetIsOriginalDocsVisible(model);
            SetIsAvailable(model);
            return model;
        }
        public void SetDictionariesToModel(HelpPersonnelBillingListModel model)
        {
            model.Statuses = GetPersonnelBillingStatuses();
            model.Urgencies = GetPersonnelBillingUrgencies(true);
            model.Titles = GetPersonnelBillingTitles(true);
        }
        protected List<IdNameDto> GetPersonnelBillingStatuses()
        {
            List<IdNameDto> statusesList = new List<IdNameDto>
                                                       {
                                                           new IdNameDto(1, "Черновик"),
                                                           new IdNameDto(2, "Запрос отправлен"),
                                                           new IdNameDto(3, "Запрос прочитан"),
                                                           new IdNameDto(4, "Запрос обработан")
                                                       }.OrderBy(x => x.Name).ToList();
            statusesList.Insert(0, new IdNameDto(0, SelectAll));
            return statusesList;
        }
        protected List<IdNameDto> GetPersonnelBillingUrgencies(bool addAll)
        {
            List<IdNameDto> list = HelpBillingUrgencyDao.LoadAllSortedByOrder().ConvertAll(x => new IdNameDto { Id = x.Id, Name = x.Name });
            if(addAll)
                list.Insert(0, new IdNameDto(0, SelectAll));
            return list;
        }
        protected List<IdNameDto> GetPersonnelBillingTitles(bool addAll)
        {
            List<int> removedThemes = new List<int>() { 1, 7 };
            List<IdNameDto> list = HelpBillingTitleDao.LoadAllSortedByOrder().ConvertAll(x => new IdNameDto { Id = x.Id, Name = x.Name }).Where(x => !removedThemes.Contains(x.Id)).ToList();
            if(addAll)
                list.Insert(0, new IdNameDto(0, SelectAll));
            return list;
        }
        protected void SetIsAvailable(HelpPersonnelBillingListModel model)
        {
            //могут создавать задачи все кто имеет доступ к пункту меню, кроме просмотровой учетки
            model.IsAddAvailable = CurrentUser.UserRole != UserRole.OutsourcingManager || CurrentUser.UserRole != UserRole.Estimator || ((CurrentUser.UserRole & UserRole.PersonnelManager) > 0 && CurrentUser.Id == 10);
        }

        public void SetPersonnelBillingListModel(HelpPersonnelBillingListModel model, bool hasError)
        {
            SetDictionariesToModel(model);
            //User user = UserDao.Load(model.UserId);
            if (hasError)
                model.Documents = new List<HelpPersonnelBillingRequestDto>();
            else
                SetDocumentsToModel(model);
        }
        public void SetDocumentsToModel(HelpPersonnelBillingListModel model)
        {
            //UserRole role = CurrentUser.UserRole;
            //model.Documents = new List<HelpPersonnelBillingRequestDto>();
            model.Documents = HelpPersonnelBillingRequestDao.GetDocuments(
                CurrentUser.Id,
                CurrentUser.UserRole,
                model.DepartmentId,
                model.StatusId,
                model.BeginDate,
                model.EndDate,
                model.InitiatorUserName,
                model.WorkerUserName,
                model.Number,
                model.TitleId,
                model.UrgencyId,
                model.SortBy,
                model.SortDescending
               );
        }
        #endregion
        #region Personnel Billing Edit
        public EditPersonnelBillingRequestViewModel GetPersonnelBillingRequestEditModel(int id)
        {
            IUser current = AuthenticationService.CurrentUser;
            int userId = 0;
            HelpPersonnelBillingRequest entity = null;
            if (id == 0)
            {
                if (CurrentUser.UserRole != UserRole.OutsourcingManager || CurrentUser.UserRole == UserRole.Estimator || ((CurrentUser.UserRole & UserRole.PersonnelManager) > 0 && CurrentUser.Id == 10))
                    userId = current.Id;
                else
                    throw new ValidationException(StrCannotCreatePersonnelBilling);
            }
            else
                entity = HelpPersonnelBillingRequestDao.Load(id);
            EditPersonnelBillingRequestViewModel model = new EditPersonnelBillingRequestViewModel
            {
                Id = id,
                UserId = id == 0 ? userId : entity.Creator.Id,
            };
            //User user = UserDao.Load(model.UserId);
            User currUser = UserDao.Load(current.Id);
            if (id == 0)
            {
                entity = new HelpPersonnelBillingRequest
                {
                    Creator = currUser,
                    CreateDate = DateTime.Now,
                    EditDate = DateTime.Now,
                    CreatorRoleId = (int)current.UserRole
                };
            }
            else
            {
                model.Answer = entity.Answer;
                model.DepartmentId = entity.Department == null ? 0 : entity.Department.Id;
                model.DepartmentName = entity.Department == null ? string.Empty : entity.Department.Name;
                model.Question = entity.Question;
                model.TitleId = entity.Title.Id;
                model.UrgencyId = entity.Urgency.Id;
                model.UserName = entity.UserName;
                model.Version = entity.Version;
                model.UserId = entity.Creator.Id;
                model.Version = entity.Version;
            }
            SetBillingRequestInfoModel(entity, model);
            model.AttachmentsModel = GetHelpPersonnelBillingAttachmentsModel(entity, RequestAttachmentTypeEnum.HelpPersonnelBillingRequest);
            LoadDictionaries(model);
            SetFlagsState(id, currUser, entity, model);
            SetHiddenFields(model);
            return model;
        }
        protected void SetHiddenFields(EditPersonnelBillingRequestViewModel model)
        {
            model.TitleIdHidden = model.TitleId;
            model.UrgencyIdHidden = model.UrgencyId;
            model.IsWorkBeginHidden = model.IsWorkBegin;
        }
        protected void LoadDictionaries(EditPersonnelBillingRequestViewModel model)
        {
            model.Urgencies = GetPersonnelBillingUrgencies(false);
            model.Titles = GetPersonnelBillingTitles(false);
            model.Comments = HelpPersonnelBillingCommentDao.GetComments(model.Id);
            model.RecipientList = HelpBillingExecutorTaskDao.GetHelpBillingRecipients(model.Id, string.IsNullOrEmpty(model.DateSended));
            model.RecipientGroups = HelpBillingExecutorTaskDao.GetHelpBillingRecipientGroups(model.Id);
            
        }
        protected void SetBillingRequestInfoModel(HelpPersonnelBillingRequest entity, BillingRequestInfoViewModel model)
        {
            model.CreatorName = entity.Creator.FullName;
            model.DateBeginWork = FormatDate(entity.BeginWorkDate);
            model.DateCreated = FormatDate(entity.CreateDate);
            model.DateEndWork = FormatDate(entity.EndWorkDate);
            model.DateSended = FormatDate(entity.SendDate);
            model.Department3Name = string.Empty;
            if(entity.Department != null)
            {
                Department dep3 = DepartmentDao.GetParentDepartmentWithLevel(entity.Department, 3);
                if (dep3 != null)
                    model.Department3Name = dep3.Name;
            }
            model.DocumentNumber = entity.Id == 0 ? string.Empty : entity.Number.ToString();
        }
        
        protected void SetFlagsState(int id, User current, HelpPersonnelBillingRequest entity, EditPersonnelBillingRequestViewModel model)
        {
            UserRole currentRole = AuthenticationService.CurrentUser.UserRole;
            SetFlagsState(model, false);
            if (model.Id == 0)
            {
                model.IsEditable = true;
                model.IsSaveAvailable = true;
                model.IsSendAvailable = true; 
                return;
            }


            if (AuthenticationService.CurrentUser.Id == 10 || AuthenticationService.CurrentUser.UserRole == UserRole.ConsultantOutsourcing ||
                AuthenticationService.CurrentUser.UserRole == UserRole.PersonnelManager || AuthenticationService.CurrentUser.UserRole == UserRole.Estimator||
                AuthenticationService.CurrentUser.UserRole == UserRole.ConsultantPersonnel ||
                AuthenticationService.CurrentUser.UserRole == UserRole.Accountant ||
                AuthenticationService.CurrentUser.UserRole == UserRole.TaxCollector ||
                AuthenticationService.CurrentUser.UserRole == UserRole.Estimator
                )
            {
                if (entity.Creator.Id == current.Id)
                {
                    if (!entity.EndWorkDate.HasValue)
                    {
                        model.IsEditable = true;
                        model.IsSaveAvailable = entity.SendDate.HasValue ? false : true;
                        model.IsSendAvailable = true;
                    }
                }
                else
                {
                    //кому доступно сообщение в реестре могут отвечать
                    //консультант может закрыть тему созданную другими
                    if (entity.SendDate.HasValue && !entity.BeginWorkDate.HasValue)
                    {
                        model.IsWorkBeginAvailable = true;
                        //model.IsSaveAvailable = true;
                        model.IsAnswerEditable = true;
                    }

                    if (entity.BeginWorkDate.HasValue && !entity.EndWorkDate.HasValue)
                    {
                        model.IsSendAvailable = AuthenticationService.CurrentUser.UserRole == UserRole.ConsultantOutsourcing ? true : false; //консультант может закрыть тему созданную другими
                        model.IsAnswerEditable = true;
                    }
                }
            }

            
            //SetRecepientsDictionary(entity,model);
        }
        protected void SetFlagsState(EditPersonnelBillingRequestViewModel model, bool state)
        {
            model.IsAnswerEditable = state;
            model.IsEditable = state;
            model.IsSaveAvailable = state;
            model.IsSendAvailable = state;
            model.IsWorkBeginAvailable = state;
        }
        public RequestAttachmentsModel GetHelpPersonnelBillingAttachmentsModel(HelpPersonnelBillingRequest entity, RequestAttachmentTypeEnum typeId)
        {
            if(entity.Id == 0)
            {
                return new RequestAttachmentsModel
                           {
                               AttachmentRequestId = 0,
                               AttachmentRequestTypeId = (int) typeId,
                               Attachments = new List<RequestAttachmentModel>(),
                               IsAddAvailable = false
                           };
            }

            //открыто для создающего и отвечающих, кроме просмотровой роли
            bool isAddAvailable = (!entity.SendDate.HasValue && (entity.Creator.Id == CurrentUser.Id)) ||
                ((entity.BeginWorkDate.HasValue && !entity.EndWorkDate.HasValue && (AuthenticationService.CurrentUser.UserRole != UserRole.OutsourcingManager) && (AuthenticationService.CurrentUser.UserRole != UserRole.Estimator)));

            List<RequestAttachment> list = RequestAttachmentDao.FindManyByRequestIdAndTypeId(entity.Id, typeId).ToList();
            RequestAttachmentsModel model = new RequestAttachmentsModel
            {
                AttachmentRequestId = entity.Id,
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
                                IsDeleteAvailable = ((x.CreatorUserRole & CurrentUser.UserRole) > 0 || AuthenticationService.CurrentUser.UserRole == UserRole.ConsultantOutsourcing) && isAddAvailable,
                            });
            return model;
        }
        public void ReloadDictionariesToModel(EditPersonnelBillingRequestViewModel model)
        {
            HelpPersonnelBillingRequest entity = HelpPersonnelBillingRequestDao.Get(model.Id);
            model.AttachmentsModel = GetHelpPersonnelBillingAttachmentsModel(entity, RequestAttachmentTypeEnum.HelpPersonnelBillingRequest);
            LoadDictionaries(model);
        }
        
        public bool SavePersonnelBillingRequestModel(EditPersonnelBillingRequestViewModel model, out string error)
        {
            error = string.Empty;
            //User user = null;
            HelpPersonnelBillingRequest entity;

            try
            {
                IUser current = AuthenticationService.CurrentUser;
                User currUser = UserDao.Load(current.Id);
                //user = UserDao.Load(model.UserId);
                if (model.Id == 0)
                {
                    entity = new HelpPersonnelBillingRequest
                    {
                        CreateDate = DateTime.Now,
                        Creator = currUser,
                        Number = RequestNextNumberDao.GetNextNumberForType((int)RequestTypeEnum.HelpPersonnelBillingRequest),
                        EditDate = DateTime.Now,
                        CreatorRoleId = (int)current.UserRole
                    };
                    ChangeEntityProperties(entity, model, currUser, out error);
                    HelpPersonnelBillingRequestDao.SaveAndFlush(entity);
                    model.Id = entity.Id;
                }
                else
                {
                    entity = HelpPersonnelBillingRequestDao.Get(model.Id);
                    if (entity == null)
                        throw new ValidationException(string.Format(StrPersonnalBillingRequestNotFound, model.Id));
                    if (entity.Version != model.Version)
                    {
                        error = StrServiceRequestWasChanged;
                        model.ReloadPage = true;
                        return false;
                    }

                    if (entity.EndWorkDate.HasValue)
                    {
                        error = "Данная тема закрыта автором!";
                        return false;
                    }


                    ChangeEntityProperties(entity, model, currUser, out error);
                    HelpPersonnelBillingRequestDao.SaveAndFlush(entity);
                    if (entity.Version != model.Version)
                    {
                        entity.EditDate = DateTime.Now;
                        HelpPersonnelBillingRequestDao.SaveAndFlush(entity);
                    }
                }


                //переписка
                if (model.Operation != 0 && !entity.EndWorkDate.HasValue)
                {
                    SaveComments(model.Id, model.IsSendAvailable, model.IsSendAvailable ? model.Question : model.Answer, out error);
                    if (!string.IsNullOrEmpty(error))
                        throw new ValidationException(error);
                }

                model.Version = entity.Version;
                SetFlagsState(entity.Id, currUser, entity, model);
                SetBillingRequestInfoModel(entity, model);
                LoadDictionaries(model);
                model.AttachmentsModel = GetHelpPersonnelBillingAttachmentsModel(entity, RequestAttachmentTypeEnum.HelpPersonnelBillingRequest);
                return true;
            }
            catch (Exception ex)
            {
                HelpServiceRequestDao.RollbackTran();
                Log.Error("Error on SavePersonnelBillingRequestModel:", ex);
                error = StrException + ex.GetBaseException().Message;
                return false;
            }
            finally
            {
                //SetUserInfoModel(user, model);
                LoadDictionaries(model);
                SetHiddenFields(model);
            }
        }

        /// <summary>
        /// Сохраняем список ответственных за задачу.
        /// </summary>
        /// <param name="entity">Заявка</param>
        /// <param name="model">Модель заявки.</param>
        /// <param name="currUser">Текущий пользователь.</param>
        /// <returns></returns>
        protected void SaveExecutorTaskList(HelpPersonnelBillingRequest entity, EditPersonnelBillingRequestViewModel model)
        {
            foreach (var item in model.RecipientList)
            {
                if (entity.Executors == null || (item.IsRecipient && entity.Executors.Where(x => x.Worker.Id == item.UserId).Count() == 0))//вновь добавляемый
                {
                    entity.Executors.Add(new HelpBillingExecutorTasks
                    {
                        Worker = UserDao.Load(item.UserId),
                        CreatedDate = DateTime.Now
                    });
                }
                else//существующий
                {
                    //если птица снята, то удаляем
                    if (!item.IsRecipient && entity.Executors.Where(x => x.Worker.Id == item.UserId).Count() != 0)
                    {
                        foreach (var w in entity.Executors)
                        {
                            if (w.Worker.Id == item.UserId)
                            {
                                entity.Executors.Remove(w);
                                break;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Добавляем комментарий
        /// </summary>
        /// <param name="CandidateId">Id кандидата</param>
        /// <param name="CommentTypeId">Вид журнала, к которому относится комментаий</param>
        /// <param name="Comment">Текст комментария</param>
        /// <param name="error">сообщение об ошибке</param>
        /// <returns></returns>
        protected bool SaveComments(int HelpBillingId, bool IsQuestion, string Comment, out string error)
        {
            HelpPersonnelBillingComments entity = new HelpPersonnelBillingComments
            {
                UserId = AuthenticationService.CurrentUser.Id,
                HelpBillingId = HelpBillingId,
                IsQuestion = IsQuestion,
                Comment = Comment,
                CreatedDate = DateTime.Now
            };
            try
            {
                HelpPersonnelBillingCommentDao.SaveAndFlush(entity);
                //EmploymentCandidateCommentDao.CommitTran();
                error = string.Empty;//"Комментарий добавлен!";
                return true;
            }
            catch (Exception ex)
            {
                HelpPersonnelBillingCommentDao.RollbackTran();
                error = string.Format("Ошибка при сохранении сообщения! Исключение:{0}", ex.GetBaseException().Message);
                return false;
            }
        }

        protected void ChangeEntityProperties(HelpPersonnelBillingRequest entity, EditPersonnelBillingRequestViewModel model, User currUser, out string error)
        {
            error = string.Empty;
            UserRole currRole = AuthenticationService.CurrentUser.UserRole;
            if (model.IsEditable)
            {
                entity.Department = model.DepartmentId != 0 ? DepartmentDao.Load(model.DepartmentId) : null;
                entity.Question = model.Question;
                entity.Title = HelpBillingTitleDao.Load(model.TitleId);
                entity.Urgency = HelpBillingUrgencyDao.Load(model.UrgencyId);
                entity.UserName = model.UserName;

                //сохраняем список ответственных за выполнение поставленной партией задачи
                if (!entity.SendDate.HasValue && model.RecipientList != null)
                {
                    SaveExecutorTaskList(entity, model);
                }
            }


            if (model.IsAnswerEditable)
                entity.Answer = model.Answer;


            if (AuthenticationService.CurrentUser.Id == 10 || AuthenticationService.CurrentUser.UserRole == UserRole.ConsultantOutsourcing ||
                AuthenticationService.CurrentUser.UserRole == UserRole.PersonnelManager || AuthenticationService.CurrentUser.UserRole == UserRole.Estimator||
                AuthenticationService.CurrentUser.UserRole == UserRole.ConsultantPersonnel ||
                AuthenticationService.CurrentUser.UserRole == UserRole.Accountant ||
                AuthenticationService.CurrentUser.UserRole == UserRole.TaxCollector
                )
            {
                if (entity.Creator.Id == currUser.Id)
                {
                    if (!entity.SendDate.HasValue && model.Operation == 1) // send
                    {
                        entity.SendDate = DateTime.Now;
                    }

                    if (model.Operation == 3)
                    {
                        entity.EndWorkDate = DateTime.Now;
                        entity.BeginWorkDate = entity.BeginWorkDate.HasValue ? entity.BeginWorkDate.Value : DateTime.Now;
                    }
                }
                else 
                {
                    //кому направлена тема
                    if (AuthenticationService.CurrentUser.UserRole != UserRole.OutsourcingManager && AuthenticationService.CurrentUser.UserRole != UserRole.Estimator)
                    {
                        if (entity.SendDate.HasValue && !entity.BeginWorkDate.HasValue && entity.Creator.Id != AuthenticationService.CurrentUser.Id)
                        {
                            entity.BeginWorkDate = DateTime.Now;
                        }

                        //консультант может закрыть тему созданную другими
                        if (AuthenticationService.CurrentUser.UserRole == UserRole.ConsultantOutsourcing)
                        {
                            if (model.Operation == 3)
                            {
                                entity.EndWorkDate = DateTime.Now;
                                entity.BeginWorkDate = entity.BeginWorkDate.HasValue ? entity.BeginWorkDate.Value : DateTime.Now;
                            }
                        }
                    }

                }
            }


            
        }

        public RequestAttachmentsModel GetBillingAttachmentsModel(int id,RequestAttachmentTypeEnum type)
        {
            switch (type)
            {
                case RequestAttachmentTypeEnum.HelpPersonnelBillingRequest:
                    return GetHelpPersonnelBillingAttachmentsModel(HelpPersonnelBillingRequestDao.Load(id),type);
                default:
                    throw new ValidationException(string.Format(StrInvalidAttachmentType,type));
            }
        }
        /// <summary>
        /// По выбранным группам и отдельным сотрудникам формируем список ответственных за поставленную задачу.
        /// </summary>
        /// <param name="RecipientList">Список сотрудников.</param>
        /// <param name="RecipientGroups">Список групп сотрудников.</param>
        public HelpPersonnelBillingExecutorsDto GetRecipients(IList<HelpPersonnelBillingRecipientDto> RecipientList, IList<HelpPersonnelBillingRecipientGroupsDto> RecipientGroups)
        {
            foreach (var groupitem in RecipientGroups)
            {
                //если группа была изменена, то метим сотрудников этой группы
                if (groupitem.IsRecipient != groupitem.IsRecipientOld)
                {
                    foreach (var item in RecipientList.Where(x => x.RoleId == groupitem.RoleId))
                    {
                        item.IsRecipient = groupitem.IsRecipient;
                    }
                    groupitem.IsRecipientOld = groupitem.IsRecipient;
                }
            }

            HelpPersonnelBillingExecutorsDto ExecutorList = new HelpPersonnelBillingExecutorsDto { RecipientList = RecipientList, RecipientGroups = RecipientGroups };

            return ExecutorList;
        }
        #endregion
    }
}