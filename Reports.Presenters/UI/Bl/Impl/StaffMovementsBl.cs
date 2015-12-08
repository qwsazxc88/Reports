using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reports.Presenters.UI.Bl;
using Reports.Core.Domain;
using Reports.Core.Dao;
using Reports.Core.Dao.Impl;
using Reports.Core;
using Reports.Presenters.UI.ViewModel;
using Reports.Core.Dto;
using Reports.Core.Enum;
namespace Reports.Presenters.UI.Bl.Impl
{
    public class StaffMovementsBl: BaseBl,IStaffMovementsBl
    {
        #region Ctor
        #endregion
        #region Constants
        #endregion
        #region Dao
        protected IExtraChargesDao extraChargesDao;
        public IExtraChargesDao ExtraChargesDao
        {
            get { return Validate.Dependency(extraChargesDao); }
            set { extraChargesDao = value; }
        }
        protected IPositionDao positionDao;
        public IPositionDao PositionDao
        {
            get { return Validate.Dependency(positionDao); }
            set { positionDao = value; }
        }
        protected IRoleDao roleDao;
        public IRoleDao RoleDao
        {
            get { return Validate.Dependency(roleDao); }
            set { roleDao = value; }
        }
        protected IRequestAttachmentDao requestAttachmentDao;
        public IRequestAttachmentDao RequestAttachmentDao
        {
            get { return Validate.Dependency(requestAttachmentDao); }
            set { requestAttachmentDao = value; }
        }
        protected IStaffMovementsDao staffMovementsDao;
        public IStaffMovementsDao StaffMovementsDao
        {
            get { return Validate.Dependency(staffMovementsDao); }
            set { staffMovementsDao = value; }
        }
        protected IStaffMovementsDocsDao staffMovementsDocsDao;
        public IStaffMovementsDocsDao StaffMovementsDocsDao
        {
            get { return Validate.Dependency(staffMovementsDocsDao); }
            set { staffMovementsDocsDao = value; }
        }
        protected IStaffMovementsDataDao staffMovementsDataDao;
        public IStaffMovementsDataDao StaffMovementsDataDao
        {
            get { return Validate.Dependency(staffMovementsDataDao); }
            set { staffMovementsDataDao = value; }
        }
        protected IrefStaffMovementsTypesDao staffMovementsTypesDao;
        public IrefStaffMovementsTypesDao StaffMovementsTypesDao
        {
            get { return Validate.Dependency(staffMovementsTypesDao); }
            set { staffMovementsTypesDao = value; }
        }
        protected IrefStaffMovementsRejectReasonDao staffMovementsRejectReasonDao;
        public IrefStaffMovementsRejectReasonDao StaffMovementsRejectReasonDao
        {
            get { return Validate.Dependency(staffMovementsRejectReasonDao); }
            set { staffMovementsRejectReasonDao = value; }
        }
        protected IrefStaffMovementsStatusDao staffMovementsStatusDao;
        public IrefStaffMovementsStatusDao StaffMovementsStatusDao
        {
            get { return Validate.Dependency(staffMovementsStatusDao); }
            set { staffMovementsStatusDao = value; }
        }
        protected IScheduleDao scheduleDao;
        public IScheduleDao ScheduleDao
        {
            get { return Validate.Dependency(scheduleDao); }
            set { scheduleDao = value; }
        }
        protected IAccessGroupDao accessGroupDao;
        public IAccessGroupDao AccessGroupDao
        {
            get { return Validate.Dependency(accessGroupDao); }
            set { accessGroupDao = value; }
        }
        protected IEmploymentSignersDao employmentSignersDao;
        public IEmploymentSignersDao EmploymentSignersDao
        {
            get { return Validate.Dependency(employmentSignersDao); }
            set { employmentSignersDao = value; }
        }
        #endregion
        #region Methods
        public StaffMovementsEditModel GetEditModel(int id)
        {
            var model = new StaffMovementsEditModel();
            if (id == 0)
            {
                model.User = new StandartUserDto();
                model.Creator = new StandartUserDto();
                SetFlagState(model);
            }
            else
            {
                model.Id = id;
                SetModel(model);
            }
            LoadDictionaries(model);

            return model;
        }
        public StaffMovementsListModel GetListModel()
        {
            StaffMovementsListModel model = new StaffMovementsListModel();
            model.BeginDate =new DateTime( DateTime.Now.Year,DateTime.Now.Month,1);
            model.Statuses = StaffMovementsStatusDao.LoadAll().Select(x => new IdNameDto { Id = x.Id, Name = x.Name }).ToList();
            model.Statuses.Add(new IdNameDto { Id = 0, Name = "Все" });
            model.Status = 0;
            return model;
        }
        public IList<StaffMovementsDto> GetDocuments(int DepartmentId, string UserName, int Number, int Status)
        {
            var docs = StaffMovementsDao.GetDocuments(CurrentUser.Id, CurrentUser.UserRole ,DepartmentId, UserName, Number, Status);
            int iterator = 1;
            if (docs != null && docs.Any())
                return docs.Select(x => new StaffMovementsDto
                {
                    CreateDate = x.CreateDate,
                    Creator = x.Creator!=null?x.Creator.Name:"",
                    Dep3Name = x.SourceDepartment!=null?((x.SourceDepartment.Dep3 != null && x.SourceDepartment.Dep3.Any()) ? x.SourceDepartment.Dep3.First().Name : ""):"",
                    Dep7Name = x.SourceDepartment!=null?x.SourceDepartment.Name:"",
                    TargetManagerName = x.TargetManager!=null ? x.TargetManager.Name:"",
                    SourceManagerName = x.SourceManager != null ? x.SourceManager.Name : "",
                    Status =x.Status!=null? (x.Status.Id==3 && x.Type.Id!=2)?"Отправлена на согласование руководителю": x.Status.Name:"",
                    MoveDate = x.MovementDate,
                    NPP = iterator++,
                    Number = x.Id,
                    Salary = x.Data.Salary,
                    Position = x.User != null ? (x.User.Position!=null?x.User.Position.Name:"") : "",
                    UserName = x.User!=null?x.User.Name:"",
                    PositionCurrent = x.SourcePosition !=null ? x.SourcePosition.Name:"",
                    PositionTarget = x.TargetPosition != null ? x.TargetPosition.Name : "",
                    TargetDep7Name = x.TargetDepartment!=null ? x.TargetDepartment.Name:"",
                    TargetDep3Name = x.TargetDepartment!=null ? ((x.TargetDepartment.Dep3 != null && x.TargetDepartment.Dep3.Any()) ? x.TargetDepartment.Dep3.First().Name : ""):""
                }).ToList();
            else return new List<StaffMovementsDto>();
        }
        public void SetModel(StaffMovementsEditModel model)
        {
            if (model.Id > 0)
            {
                var entity = StaffMovementsDao.Load(model.Id);
                #region Стандартные поля заявки
                model.StatusId = entity.Status.Id;
                model.Status = entity.Status.Name;
                model.Creator.Id = entity.Creator.Id;
                model.Number = String.Format("№{0}",entity.Id);
                model.User.Id = entity.User.Id;
                model.RequestType = entity.Type.Id;
                model.MovementDate = entity.MovementDate;
                model.MovementReason = entity.Data.MovementReason;
                model.CreateDate = entity.CreateDate;
                var targetposition = entity.TargetPosition;
                if (targetposition != null)
                {
                    model.TargetPositionId = targetposition.Id;
                    model.TargetPositionName = targetposition.Name;
                    //Убрано. нет связи со штаткой
                    //model.TargetSalary = targetposition.Salary;
                }

                #endregion
                #region Согласования
                model.SendDate = entity.SendDate;
                model.SourceManagerAccept = entity.SourceManagerAccept;
                model.TargetManagerAccept = entity.TargetManagerAccept;
                model.PersonnelManagerBankAccept = entity.PersonnelManagerBankAccept;
                model.PersonnelManagerBank = entity.PersonnelManagerBank != null ? entity.PersonnelManagerBank.Name : "";
                model.ChiefAccept = entity.TargetChiefAccept;
                model.Chief = entity.TargetChief!=null?entity.TargetChief.Name:"";
                model.PersonnelManagerAccept = entity.PersonnelManagerAccept;
                model.PersonnelManager = entity.PersonnelManager != null ? entity.PersonnelManager.Name : "";                
                #endregion
                #region Данные о переводе
                model.MovementDate = entity.MovementDate;
                model.MovementTempTo = entity.MovementTempTo;
                model.TargetPositionId = entity.TargetPosition.Id;
                model.TargetPositions = new List<IdNameDto>();
                model.TargetPositions.Add(new IdNameDto {  Id= model.TargetPositionId, Name=model.TargetPositionName});
                model.TargetPositionName = entity.TargetPosition.Name;
                model.TargetDepartmentId = entity.TargetDepartment.Id;
                model.TargetDepartmentName = entity.TargetDepartment.Name;
                model.TargetManager = new StandartUserDto { Id = entity.TargetManager.Id };
                LoadUserData(model.TargetManager);
                model.SourceManager = new StandartUserDto { Id = entity.SourceManager.Id };
                LoadUserData(model.SourceManager);
                #endregion
                #region Для руководителей
                model.IsTempMoving = entity.IsTempMoving;
                model.Conjunction = entity.Data.Conjunction;
                model.MovementCondition = entity.Data.MovementCondition;
                #endregion
                #region Для кадровиков
                model.TargetCasingType = entity.Data.TargetCasingType;
                model.RegionCoefficient = entity.Data.RegionCoefficient;
                model.Grade = entity.Data.Grade;
                model.HoursType = entity.Data.HoursType!=null?entity.Data.HoursType.Id:0;
                model.NorthFactor = entity.Data.NorthFactor;
                model.NorthFactorOrder = entity.Data.NorthFactorOrder;
                model.NorthFactorAddition = entity.Data.NorthFactorAddition;
                model.NorthFactorAdditionAction = entity.Data.NorthFactorAdditionAction;
                model.NorthFactorDay = entity.Data.NorthFactorDay;
                model.NorthFactorMonth = entity.Data.NorthFactorMonth;
                model.NorthFactorYear = entity.Data.NorthFactorYear;
                model.AccessGroup = entity.Data.AccessGroup!=null?entity.Data.AccessGroup.Id:0;
                #endregion
                #region Files
                var docs = entity.Docs;
                if (docs != null && docs.Any())
                {
                    
                    var MaterialLiabilityDoc = docs.Where(x => x.DocType == (int)StaffMovementsDocsTypes.MaterialLiabilityDoc).First();
                    model.MaterialLiabilityDocDto = new UploadFileDto();
                    model.MaterialLiabilityDocIsRequired =MaterialLiabilityDoc!=null? MaterialLiabilityDoc.IsRequired:false;
                    if (MaterialLiabilityDoc != null && MaterialLiabilityDoc.Attachment != null)
                    {
                        model.MaterialLiabilityDocDto.FileName = MaterialLiabilityDoc.Attachment.FileName;
                        model.MaterialLiabilityDocAttachmentId = MaterialLiabilityDoc.Attachment.Id;
                    }
                    var MovementNote = docs.Where(x => x.DocType == (int)StaffMovementsDocsTypes.MovementNote).First();
                    model.MovementNoteDto = new UploadFileDto();
                    model.MovementNoteIsRequired = MovementNote!=null?MovementNote.IsRequired:false;
                    if (MovementNote != null && MovementNote.Attachment != null)
                    {
                        model.MovementNoteDto.FileName = MovementNote.Attachment.FileName;
                        model.MovementNoteAttachmentId = MovementNote.Attachment.Id;
                    }
                    
                    var RequirementsOrderDoc = docs.Where(x => x.DocType == (int)StaffMovementsDocsTypes.RequirementsOrderDoc).First();
                    model.RequirementsOrderDocDto = new UploadFileDto();
                    model.RequirementsOrderDocIsRequired = RequirementsOrderDoc!=null?RequirementsOrderDoc.IsRequired:false;
                    if (RequirementsOrderDoc != null && RequirementsOrderDoc.Attachment != null)
                    {
                        model.RequirementsOrderDocDto.FileName = RequirementsOrderDoc.Attachment.FileName;
                        model.RequirementsOrderDocAttachmentId = RequirementsOrderDoc.Attachment.Id;
                    }
                    var ServiceOrderDoc = docs.Where(x => x.DocType == (int)StaffMovementsDocsTypes.ServiceOrderDoc).First();
                    model.ServiceOrderDocDto = new UploadFileDto();
                    model.ServiceOrderDocIsRequired = ServiceOrderDoc!=null? ServiceOrderDoc.IsRequired: false;
                    if (ServiceOrderDoc != null && ServiceOrderDoc.Attachment != null)
                    {
                        model.ServiceOrderDocDto.FileName = ServiceOrderDoc.Attachment.FileName;
                        model.ServiceOrderDocAttachmentId = ServiceOrderDoc.Attachment.Id;                        
                    }
                }
                #endregion
            }
            //Подгружаем данные о сотруднике
            LoadUserData(model.User);
            //Заполняем справочники
            LoadDictionaries(model);
            //Подгружаем данные о создателе заявки
            if ((model.Creator==null || model.Creator.Id == 0) && model.Id==0)
            {
                if (model.RequestType == 1 || model.RequestType == 3)
                {
                    model.TargetDepartmentId = model.User.Dep7Id;
                    model.TargetDepartmentName = model.User.Dep7Name;
                    model.TargetPositionId = model.User.PositionId;
                    model.TargetPositions = GetPositionsForDepartment(model.TargetDepartmentId);
                }
                model.Creator = new StandartUserDto();
                model.Creator.Id = CurrentUser.Id;
                model.CreateDate = DateTime.Now;
                LoadUserData(model.Creator);
                SetFlagState(model);
                return;
            }            
            LoadUserData(model.Creator);
            SetFlagState(model);
        }
        private void LoadDictionaries(StaffMovementsEditModel model)
        {            
            var extracharges = ExtraChargesDao.LoadAll();
            if (extracharges != null && extracharges.Any())
            {
                model.NorthFactorOrders = extracharges.Select(x => new IdNameDto { Id = x.Id, Name = x.Name }).ToList();
            }
            var HoursTypes = ScheduleDao.LoadAll();
            if(HoursTypes!=null && HoursTypes.Any())
            {
                model.HoursTypes = HoursTypes.Select(x => new IdNameDto { Id=x.Id,Name=x.Name}).ToList();
            }
            var AccessGroups = AccessGroupDao.LoadAll();
            if(AccessGroups!=null && AccessGroups.Any())
            {
                model.AccessGroupsList = AccessGroups.Select(x => new IdNameDto { Id = x.Id, Name = x.Name }).ToList();
            }
            
            var RequestTypes = StaffMovementsTypesDao.LoadAll();
            if (RequestTypes != null && RequestTypes.Any())
            {
                model.RequestTypes = RequestTypes.Select(x => new IdNameDto { Id = x.Id, Name = x.Name }).ToList();
                if (model.Id == 0 && CurrentUser.UserRole == UserRole.Employee)
                {
                    model.RequestTypes = model.RequestTypes.Where(x => x.Id != 1).ToList();
                }
            }
            model.NorthFactors = GetNorthExperienceTypes();
            model.AdditionActions = new List<IdNameDto>();
            model.AdditionActions.Add(new IdNameDto { Id = 1, Name = "Начать" });
            model.AdditionActions.Add(new IdNameDto { Id = 2, Name = "Изменить" });
            model.AdditionActions.Add(new IdNameDto { Id = 3, Name = "Не изменять" });
            model.AdditionActions.Add(new IdNameDto { Id = 4, Name = "Прекратить" });
        }
        public void SaveModel(StaffMovementsEditModel model)
        {
            StaffMovements entity;
            if (model.Id == 0)
            {
                //Если идентификатор = 0, то нужно создать новую сущность
                entity = new StaffMovements();
            }
            else
            {
                //Загружаем сущьность если идентификатор отличен от 0
                entity = StaffMovementsDao.Load(model.Id);
            }
            
            //Меняем поля и сохраняем
            //файлики
            if (model.IsDocsAddAvailable && model.Id > 0)
                SaveFiles(model);
            ChangeEntityProperties(entity, model);
            StaffMovementsDao.SaveAndFlush(entity);
            //файлики только для первого раза
            if (model.IsDocsAddAvailable && model.Id == 0 && entity.Id > 0)
            {
                model.Id = entity.Id;//Нужно присвоить модели идентификатор
                SaveFiles(model);
            }
            
        }
        public void SaveDocsModel(StaffMovementsEditModel model)
        {            
            StaffMovementsDocsDao.Update(x =>x.Request.Id == model.Id && x.DocType == (int)StaffMovementsDocsTypes.MaterialLiabilityDoc, y => y.IsRequired = model.MaterialLiabilityDocIsRequired);
            StaffMovementsDocsDao.Update(x =>x.Request.Id == model.Id && x.DocType == (int)StaffMovementsDocsTypes.MovementNote, y => y.IsRequired = model.MovementNoteIsRequired);
            StaffMovementsDocsDao.Update(x =>x.Request.Id == model.Id && x.DocType == (int)StaffMovementsDocsTypes.RequirementsOrderDoc, y => y.IsRequired = model.RequirementsOrderDocIsRequired);
            StaffMovementsDocsDao.Update(x =>x.Request.Id == model.Id && x.DocType == (int)StaffMovementsDocsTypes.ServiceOrderDoc, y => y.IsRequired = model.ServiceOrderDocIsRequired);
        }
        private void SaveFiles(StaffMovementsEditModel model)
        {
            //Сохраняем файлы
            var entity = StaffMovementsDao.Load(model.Id);
            var docs = entity.Docs;
            #region файлики
            var tmp = "";
            var MaterialLiabilityDoc = docs.Where(x => x.DocType == (int)StaffMovementsDocsTypes.MaterialLiabilityDoc).First();
            SaveAttachment(MaterialLiabilityDoc.Id, MaterialLiabilityDoc.Attachment != null ? MaterialLiabilityDoc.Attachment.Id : 0, model.MaterialLiabilityDocDto, RequestAttachmentTypeEnum.StaffMovements, out tmp);
            var MovementNote = docs.Where(x => x.DocType == (int)StaffMovementsDocsTypes.MovementNote).First();
            SaveAttachment(MovementNote.Id, MovementNote.Attachment != null ? MovementNote.Attachment.Id : 0, model.MovementNoteDto, RequestAttachmentTypeEnum.StaffMovements, out tmp);
            var RequirementsOrderDoc = docs.Where(x => x.DocType == (int)StaffMovementsDocsTypes.RequirementsOrderDoc).First();
            SaveAttachment(RequirementsOrderDoc.Id, RequirementsOrderDoc.Attachment != null ? RequirementsOrderDoc.Attachment.Id : 0, model.RequirementsOrderDocDto, RequestAttachmentTypeEnum.StaffMovements, out tmp);
            var ServiceOrderDoc = docs.Where(x => x.DocType == (int)StaffMovementsDocsTypes.ServiceOrderDoc).First();
            SaveAttachment(ServiceOrderDoc.Id, ServiceOrderDoc.Attachment != null ? ServiceOrderDoc.Attachment.Id : 0, model.ServiceOrderDocDto, RequestAttachmentTypeEnum.StaffMovements, out tmp);
            #endregion
        }
        private void SetFlagState(StaffMovementsEditModel model)
        {
            #region Сначала сбросим все флаги в false
            model.IsAcceptButtonPressed = false;
            model.IsCancelAvailable = false;
            model.IsCancelButtonPressed = false;
            model.IsChiefAcceptAvailable = false;
            model.IsConfirmButtonAvailable = false;
            model.IsConfirmButtonPressed = false;
            model.IsDepartmentEditable = false;
            model.IsDocsAddAvailable = false;
            model.IsDocsEditable = false;
            model.IsManagerEditable = false;
            model.IsPersonnelManagerAcceptAvailable = false;
            model.IsPersonnelManagerBankAcceptAvailable = false;
            model.IsPersonnelManagerEditable = false;
            model.IsPositionEditable = false;
            model.ISRejectAvailable = false;
            model.IsRejectButtonPressed = false;
            model.IsSaveAvailable = false;
            model.IsSourceManagerAcceptAvailable = false;
            model.IsStopButtonAvailable = false;
            model.IsStopButtonPressed = false;
            model.IsTargetManagerAcceptAvailable = false;
            model.IsUserAcceptAvailable = false;
            #endregion
            //Флаги по типу модели
            switch (model.RequestType)
            {
                case 1: model.IsDepartmentEditable = false;
                    model.IsPositionEditable = false;
                    model.IsTargetManagerAcceptAvailable = true;
                    model.IsSourceManagerAcceptAvailable = false;
                    break;
                case 2:
                    model.IsDepartmentEditable = true;
                    model.IsPositionEditable = true;
                    break;
                case 3: 
                    model.IsDepartmentEditable = false;
                    model.IsPositionEditable = true;
                    model.IsTargetManagerAcceptAvailable = true;
                    model.IsSourceManagerAcceptAvailable = false;
                    break;
            }
            //Если вышло из состояния черновика, то редактировать подразделение и должность нельзя. Заисключением кадровиков
            if (model.StatusId > 1 && UserRole.PersonnelManager != CurrentUser.UserRole)
            {
                model.IsDepartmentEditable = false;
                model.IsPositionEditable = false;
            }
            //Флаги по статусу
            switch (model.StatusId)
            {
                case 1: //Черновик. Могут редактировать руководитель и сотрудник
                    model.IsManagerEditable = true;
                    model.IsSourceManagerAcceptAvailable = true;
                    model.IsTargetManagerAcceptAvailable = model.IsTargetManagerAcceptAvailable && true;
                    model.IsUserAcceptAvailable = true;
                    model.IsDocsAddAvailable = true;
                    break;
                case 2://Отправлена на согласование руководителю отпускающему. Может редактировать руководитель
                    model.IsCancelAvailable = true;
                    model.ISRejectAvailable = true;
                    model.IsManagerEditable = true;
                    model.IsSourceManagerAcceptAvailable = true;
                    model.IsDocsAddAvailable = true;
                    break;
                case 3://Отправлена на согласование руководителю принимающему. может редактировать руководитель принимающий
                    model.IsCancelAvailable = true;
                    model.ISRejectAvailable = true;
                    model.IsManagerEditable = true;
                    model.IsDocsAddAvailable = true;
                    model.IsTargetManagerAcceptAvailable = true;
                    break;
                case 4://Отправлена на согласование кадровику банка
                case 5://Заблокирована кадровиком банка. Доступно согласование кадровиком банка
                    model.IsCancelAvailable = true;
                    model.ISRejectAvailable = true;
                    model.IsManagerEditable = true;
                    model.IsStopButtonAvailable = true;
                    model.IsPersonnelManagerBankAcceptAvailable = true;
                    model.IsTargetManagerAcceptAvailable = false;
                    break;
                case 6://Отправлена на согласование вышестоящему руководителю
                    model.IsCancelAvailable = true;
                    model.ISRejectAvailable = true;
                    model.IsChiefAcceptAvailable = true;
                    model.IsTargetManagerAcceptAvailable = false;
                    break;
                case 7://Оформление кадры. Доступно редактирование кадровикам, проставление галок в документах
                    model.IsCancelAvailable = true;
                    model.ISRejectAvailable = true;
                    model.IsPersonnelManagerEditable = true;
                    model.IsPersonnelManagerAcceptAvailable = true;
                    model.IsDocsEditable = true;
                    model.IsDocsAddAvailable = true;
                    model.IsTargetManagerAcceptAvailable = false;
                    break;
                case 8://Контроль руководителя - пакет документов на подпись. Доступно вложение документов
                    model.IsCancelAvailable = true;
                    model.ISRejectAvailable = true;
                    model.IsDocsAddAvailable = true;
                    model.IsDocsEditable = true;
                    model.IsTargetManagerAcceptAvailable = false;
                    break;
                case 9://Документы подписаны
                    model.IsCancelAvailable = true;
                    model.ISRejectAvailable = true;
                    model.IsConfirmButtonAvailable = true;
                    model.IsDocsEditable = true;
                    model.IsTargetManagerAcceptAvailable = false;
                    break;
                case 10://Перевод оформлен
                    model.IsCancelAvailable = true;
                    model.ISRejectAvailable = true;
                    model.IsTargetManagerAcceptAvailable = false;
                    break;
                case 11://'Отклонен' Уже ничо нельзя сделать. Поезд ушёл.
                    model.IsTargetManagerAcceptAvailable = false;
                    break;
                case 12://'Выгружен в 1С' Уже ничо нельзя сделать. Поезд ушёл.
                    model.IsTargetManagerAcceptAvailable = false;
                    break;
            }
            //Флаги по ролям
            switch (CurrentUser.UserRole)
            {
                case UserRole.OutsourcingManager:
                    model.IsDocsVisible = true;
                    model.IsPersonnelVisible = true;
                    model.IsManagerVisible = true;

                    model.IsPersonnelManagerEditable = false; //Редактирование кадровиком
                    model.IsManagerEditable = false;//Редактирование руководителем
                    model.IsDocsEditable = false;//Редактирование документов
                    model.IsDocsAddAvailable = false;//Добавление документов

                    model.IsUserAcceptAvailable = false; //Утверждение сотрудником
                    model.ISRejectAvailable = false; //Отмена
                    model.IsCancelAvailable = false;
                    model.IsSourceManagerAcceptAvailable = false;//Утверждение отпускающим руководителем
                    model.IsTargetManagerAcceptAvailable = false;//Утверждение принимающим руководителем
                    model.IsPersonnelManagerAcceptAvailable = false;//Утверждение кадровиком
                    model.IsPersonnelManagerBankAcceptAvailable = false;//Утверждение кадровиком банка
                    model.IsChiefAcceptAvailable = false;//Утверждение вышестоящим руководителем                    

                    model.IsConfirmButtonAvailable = false;//Кнопка утверждения документов                   
                    model.IsStopButtonAvailable = false;//Конпка приостановки  
                    break;
                case UserRole.Employee:
                    model.IsDocsVisible = true;
                    model.IsPersonnelVisible = false;
                    model.IsManagerVisible = true;
                    model.IsPersonnelManagerEditable = false; //Редактирование кадровиком
                    model.IsManagerEditable = false;//Редактирование руководителем
                    model.IsDocsEditable = false;//Редактирование документов
                    model.IsDocsAddAvailable = model.IsDocsAddAvailable = true;
                    model.IsUserAcceptAvailable = model.IsUserAcceptAvailable && true; //Утверждение сотрудником
                    model.ISRejectAvailable = model.ISRejectAvailable && true; //Отмена
                    model.IsSourceManagerAcceptAvailable = false;//Утверждение отпускающим руководителем
                    model.IsTargetManagerAcceptAvailable = false;//Утверждение принимающим руководителем
                    model.IsPersonnelManagerAcceptAvailable = false;//Утверждение кадровиком
                    model.IsPersonnelManagerBankAcceptAvailable = false;//Утверждение кадровиком банка
                    model.IsChiefAcceptAvailable = false;//Утверждение вышестоящим руководителем                    

                    model.IsConfirmButtonAvailable = false;//Кнопка утверждения документов                   
                    model.IsStopButtonAvailable = false;//Конпка приостановки                 
                    break;
                case UserRole.Manager:
                    model.IsDocsVisible = true;
                    model.IsPersonnelVisible = false;
                    model.IsManagerVisible = true;
                    model.IsPersonnelManagerEditable = false; //Редактирование кадровиком
                    model.IsManagerEditable = model.IsManagerEditable && (model.Id == 0 || (model.TargetManager!=null?model.TargetManager.Id==CurrentUser.Id:false));//Редактирование руководителем, должно быть доступно только принимающему руководителю
                    model.IsDocsEditable = false;//Редактирование документов
                    model.IsDocsAddAvailable = model.IsDocsAddAvailable && true;//Добавление документов
                    
                    model.IsUserAcceptAvailable = false; //Утверждение сотрудником
                    model.SendDate = model.SendDate.HasValue ? model.SendDate : DateTime.Now;
                    model.ISRejectAvailable = model.ISRejectAvailable && true; //Отмена
                    model.IsSourceManagerAcceptAvailable = model.IsSourceManagerAcceptAvailable && model.SourceManager!=null?model.SourceManager.Id==CurrentUser.Id:false;//Утверждение отпускающим руководителем. Должно быть доступно только отпускающему
                    model.IsTargetManagerAcceptAvailable = model.IsTargetManagerAcceptAvailable && model.TargetManager!=null?model.TargetManager.Id==CurrentUser.Id:false;//Утверждение принимающим руководителем
                    model.IsPersonnelManagerAcceptAvailable = false;//Утверждение кадровиком
                    model.IsPersonnelManagerBankAcceptAvailable = false;//Утверждение кадровиком банка
                    if (model.TargetManager != null)
                    {
                        var chiefs = GetChiefsForManager(model.TargetManager.Id);
                        model.IsChiefAcceptAvailable = model.IsChiefAcceptAvailable && chiefs.Any(x => x.Id == CurrentUser.Id);//Утверждение вышестоящим руководителем                    
                    }
                    else model.IsChiefAcceptAvailable = false;
                    model.IsConfirmButtonAvailable = false;//Кнопка утверждения документов                   
                    model.IsStopButtonAvailable = false;//Конпка приостановки        
                    model.IsPositionEditable = model.IsPositionEditable && (model.Id==0 || model.IsTargetManagerAcceptAvailable || model.IsSourceManagerAcceptAvailable);
                    break;
                case UserRole.ConsultantPersonnel:
                    model.IsDocsVisible = false;
                    model.IsPersonnelVisible = false;
                    model.IsManagerVisible = true;
                    model.IsPersonnelManagerEditable = false; //Редактирование кадровиком
                    model.IsManagerEditable = (model.IsPersonnelManagerBankAcceptAvailable ) && true;//Редактирование руководителем, должно быть доступно только принимающему руководителю
                    model.IsDocsEditable = false;//Редактирование документов
                    model.IsDocsAddAvailable = false;//Добавление документов

                    model.IsUserAcceptAvailable = false; //Утверждение сотрудником
                    model.ISRejectAvailable = model.ISRejectAvailable && true; //Отмена
                    model.IsSourceManagerAcceptAvailable = false;//Утверждение отпускающим руководителем. Должно быть доступно только отпускающему
                    model.IsTargetManagerAcceptAvailable = false;//Утверждение принимающим руководителем
                    model.IsPersonnelManagerAcceptAvailable = false;//Утверждение кадровиком
                    model.IsPersonnelManagerBankAcceptAvailable = model.IsPersonnelManagerBankAcceptAvailable && true;//Утверждение кадровиком банка
                    model.IsChiefAcceptAvailable = false;//Утверждение вышестоящим руководителем                    

                    model.IsConfirmButtonAvailable = false;//Кнопка утверждения документов                   
                    model.IsStopButtonAvailable = true;//Конпка приостановки  
                    break;
                case UserRole.PersonnelManager:
                    model.IsDocsVisible = true;
                    model.IsPersonnelVisible = true;
                    model.IsManagerVisible = true;
                    model.IsPersonnelManagerEditable = model.IsPersonnelManagerEditable && true; //Редактирование кадровиком
                    model.IsManagerEditable = (model.IsPersonnelManagerEditable|| model.IsManagerEditable) && true;//Редактирование руководителем, должно быть доступно только принимающему руководителю
                    model.IsDocsEditable = model.IsDocsEditable && true;//Редактирование документов
                    model.IsDocsAddAvailable = model.IsDocsAddAvailable && true;//Добавление документов
                    
                    model.IsUserAcceptAvailable = false; //Утверждение сотрудником
                    model.SendDate = model.SendDate.HasValue ? model.SendDate : DateTime.Now;
                    model.ISRejectAvailable = model.ISRejectAvailable && true; //Отмена
                    model.IsSourceManagerAcceptAvailable = false;//Утверждение отпускающим руководителем. Должно быть доступно только отпускающему
                    model.IsTargetManagerAcceptAvailable = false;//Утверждение принимающим руководителем
                    model.IsPersonnelManagerAcceptAvailable = model.IsPersonnelManagerAcceptAvailable && true;//Утверждение кадровиком
                    model.IsPersonnelManagerBankAcceptAvailable = false;//Утверждение кадровиком банка
                    model.IsChiefAcceptAvailable = false;//Утверждение вышестоящим руководителем                    

                    model.IsConfirmButtonAvailable = model.IsConfirmButtonAvailable && true;//Кнопка утверждения документов                   
                    model.IsStopButtonAvailable = false;//Конпка приостановки  
                    break;
            }
            model.IsSaveAvailable = (model.StatusId == 1 && (model.Creator.Id==CurrentUser.Id || model.User.Id==CurrentUser.Id) )  
                || model.IsManagerEditable 
                || model.IsPersonnelManagerEditable
                || model.IsDocsAddAvailable;
        }
        /// <summary>
        /// Меняем поля сущности. Все сохранения зависят от флагов(нельзя - значит нельзя.)
        /// </summary>
        /// <param name="entity">Сущьность</param>
        /// <param name="model">Модель</param>
        private void ChangeEntityProperties(StaffMovements entity, StaffMovementsEditModel model)
        {
            #region Тот случай, когда сохранять заявку не нужно
            //Если нажали кнопку отмена, то возвращаемся в черновик и всё. В остальных случаях продолжаем работать
            if (model.IsCancelAvailable && model.IsCancelButtonPressed)
            {
                ChangeEntityStatusToTemp(entity);
                return;
            }
            #endregion
            #region Стандартные поля заявки, нужно заполнять только если заявка новая
            if (model.Id == 0)
            {
                //Если идентификатор = 0, значит заявка новая, нужно сохранить основные поля.
                //Дата создания
                entity.CreateDate = model.CreateDate;
                //Создатель
                entity.Creator = UserDao.Load(model.Creator.Id);
                if ((CurrentUser.UserRole & (UserRole.Manager | UserRole.PersonnelManager))>0)
                {
                    entity.SendDate = DateTime.Now;
                    if (model.RequestType == 2)
                    {
                        entity.Status = StaffMovementsStatusDao.Load((int)StaffMovementsStatus.SourceManager);
                    }
                    else
                    {
                        entity.Status = StaffMovementsStatusDao.Load((int)StaffMovementsStatus.TargetManager);
                    }
                }
                //Сотрудник
                entity.User = UserDao.Load(model.User.Id);
                //Статус // сначала черновик
                entity.Status = StaffMovementsStatusDao.Load((int)StaffMovementsStatus.Temp);
                //Данные исходной позиции
                entity.SourcePosition = entity.User.Position;
                entity.SourceDepartment = entity.User.Department;
                entity.SourceManager = GetManagerForDepartment(entity.SourceDepartment);
                //Если запрещено редактировать подразделение на момент создания заявки, то нужно сохранить исходное
                if (!model.IsDepartmentEditable)
                {
                    entity.TargetDepartment = entity.SourceDepartment;
                    entity.TargetManager = entity.SourceManager;
                }
                if (!model.IsPositionEditable)
                {
                    entity.TargetPosition = entity.SourcePosition;
                } 
                //Тип заявки
                entity.Type = StaffMovementsTypesDao.Load(model.RequestType);
                //Данные заявки, создаём и сохраняем
                entity.Data = new StaffMovementsData();
                StaffMovementsDataDao.SaveAndFlush(entity.Data);
                //Документы, создаём сразу все.
                var docs=new List<StaffMovementsDocs>();
                docs.Add(new StaffMovementsDocs { DocType = (int)StaffMovementsDocsTypes.AdditionalAgreementDoc, Request=entity });
                docs.Add(new StaffMovementsDocs { DocType = (int)StaffMovementsDocsTypes.MaterialLiabilityDoc, Request = entity });
                docs.Add(new StaffMovementsDocs { DocType = (int)StaffMovementsDocsTypes.MovementNote, Request = entity });
                docs.Add(new StaffMovementsDocs { DocType = (int)StaffMovementsDocsTypes.MovementOrderDoc, Request = entity });
                docs.Add(new StaffMovementsDocs { DocType = (int)StaffMovementsDocsTypes.RequirementsOrderDoc, Request = entity });
                docs.Add(new StaffMovementsDocs { DocType = (int)StaffMovementsDocsTypes.ServiceOrderDoc, Request = entity });
                entity.Docs = docs;                
            }
            #endregion
            #region Данные о переводе, заполняет персонаж или руководитель            
            if (model.IsDepartmentEditable)
            {
                if (entity.TargetDepartment != null && entity.TargetDepartment.Id != model.TargetDepartmentId)
                {                    
                    entity.TargetManagerAccept = null;
                    entity.TargetChief = null;
                    entity.TargetChiefAccept = null;
                }
                entity.TargetDepartment = DepartmentDao.Load(model.TargetDepartmentId);
                entity.TargetManager = GetManagerForDepartment(entity.TargetDepartment);
            }
            if (model.IsPositionEditable)
            {
                entity.TargetPosition = PositionDao.Load(model.TargetPositionId);
            }       
            #endregion
            #region Общее
            if (model.StatusId<=1 || model.IsManagerEditable || model.IsPersonnelManagerEditable || model.IsPersonnelManagerBankAcceptAvailable)
            {
                entity.MovementDate = model.MovementDate;
                entity.MovementTempTo = model.MovementTempTo;
                entity.Data.MovementReason = model.MovementReason;
            }
            #endregion
            #region Для руководителей
            if (model.IsManagerEditable)
            {
                entity.IsTempMoving = model.IsTempMoving;
                entity.Data.MovementCondition = model.MovementCondition;//Условие перевода
                entity.Data.Conjunction = model.Conjunction;                
            }
            #endregion
            #region Для кадров
            if (model.IsPersonnelManagerEditable)
            {                
                entity.Data.Grade = model.Grade;//Грейд
                entity.Data.HoursType = ScheduleDao.Load(model.HoursType);//График работы
                entity.Data.NorthFactor = model.NorthFactor;//Северный стаж
                entity.Data.NorthFactorAddition = model.NorthFactorAddition;
                entity.Data.NorthFactorYear = model.NorthFactorYear;
                entity.Data.NorthFactorMonth = model.NorthFactorMonth;
                entity.Data.NorthFactorDay = model.NorthFactorDay;
                entity.Data.NorthFactorOrder = model.NorthFactorOrder;
                entity.Data.AccessGroup = AccessGroupDao.Load(model.AccessGroup);//Группа доступа
                entity.Data.NorthFactorAdditionAction = model.NorthFactorAdditionAction;
                //Ставим галочки в документах
                if (model.IsDocsEditable)
                {                    
                    StaffMovementsDocsDao.Update(x => x.Request.Id == entity.Id && x.DocType == (int)StaffMovementsDocsTypes.MaterialLiabilityDoc, y => y.IsRequired = model.MaterialLiabilityDocIsRequired);
                    StaffMovementsDocsDao.Update(x => x.Request.Id == entity.Id && x.DocType == (int)StaffMovementsDocsTypes.MovementNote, y => y.IsRequired = model.MovementNoteIsRequired);
                    StaffMovementsDocsDao.Update(x => x.Request.Id == entity.Id && x.DocType == (int)StaffMovementsDocsTypes.RequirementsOrderDoc, y => y.IsRequired = model.RequirementsOrderDocIsRequired);
                    StaffMovementsDocsDao.Update(x => x.Request.Id == entity.Id && x.DocType == (int)StaffMovementsDocsTypes.ServiceOrderDoc, y => y.IsRequired = model.ServiceOrderDocIsRequired);
                }
            }
            #endregion
            
            #region Согласования, утверждения, отмены и изменение статуса
            switch (model.StatusId)
            {
                case 1:
                    if (model.ISRejectAvailable && model.IsRejectButtonPressed)
                    {
                        //Если нажали кнопку отказа, то отказ и всё поезд ушёл.
                        entity.DeleteDate = DateTime.Now;
                        entity.RejectDate = DateTime.Now;
                        entity.RejectedBy = UserDao.Load(CurrentUser.Id);
                        entity.RejectReason = StaffMovementsRejectReasonDao.Load((int)StaffMovementsRejectReasonEnum.UserReject);
                        entity.Status = StaffMovementsStatusDao.Load((int)StaffMovementsStatus.Canceled);
                    }
                    if (model.IsAcceptButtonPressed && model.IsUserAcceptAvailable)
                    {
                        //Если согласовано пользователем
                        entity.SendDate = DateTime.Now;
                        if (entity.Type.Id == 2)
                        {
                            entity.Status = StaffMovementsStatusDao.Load((int)StaffMovementsStatus.SourceManager);
                        }
                        else
                        {
                            entity.Status = StaffMovementsStatusDao.Load((int)StaffMovementsStatus.TargetManager);
                        }
                    }
                    if (model.IsAcceptButtonPressed && model.IsSourceManagerAcceptAvailable)
                    {
                        //Если согласовано отпускающим руководителем
                        entity.SendDate = DateTime.Now;
                        entity.SourceManagerAccept = DateTime.Now;
                        entity.Status = StaffMovementsStatusDao.Load((int)StaffMovementsStatus.TargetManager);
                    }
                    if (model.IsAcceptButtonPressed && model.IsTargetManagerAcceptAvailable)
                    {
                        //Если согласовано отпускающим руководителем
                        entity.SendDate = DateTime.Now;
                        entity.SourceManagerAccept = DateTime.Now;
                        entity.TargetManagerAccept = DateTime.Now;
                        entity.Status = StaffMovementsStatusDao.Load((int)StaffMovementsStatus.PersonelManagerBank);
                    }
                    break;
                case 2:
                    if (model.ISRejectAvailable && model.IsRejectButtonPressed)
                    {
                        //Если нажали кнопку отказа, то отказ и всё поезд ушёл.
                        entity.DeleteDate = DateTime.Now;
                        entity.RejectDate = DateTime.Now;
                        entity.RejectedBy = UserDao.Load(CurrentUser.Id);
                        entity.RejectReason = StaffMovementsRejectReasonDao.Load((int)StaffMovementsRejectReasonEnum.SourceManagerReject);
                        entity.Status = StaffMovementsStatusDao.Load((int)StaffMovementsStatus.Canceled);
                    }
                    if (model.IsAcceptButtonPressed && model.IsSourceManagerAcceptAvailable)
                    {
                        //Если согласовано отпускающим руководителем
                        entity.SourceManagerAccept = DateTime.Now;
                        entity.Status = StaffMovementsStatusDao.Load((int)StaffMovementsStatus.TargetManager);
                    }
                    break;
                case 3:
                    if (model.ISRejectAvailable && model.IsRejectButtonPressed)
                    {
                        //Если нажали кнопку отказа, то отказ и всё поезд ушёл.
                        entity.DeleteDate = DateTime.Now;
                        entity.RejectDate = DateTime.Now;
                        entity.RejectedBy = UserDao.Load(CurrentUser.Id);
                        entity.RejectReason = StaffMovementsRejectReasonDao.Load((int)StaffMovementsRejectReasonEnum.TargetManagerReject);
                        entity.Status = StaffMovementsStatusDao.Load((int)StaffMovementsStatus.Canceled);
                    }
                    if (model.IsAcceptButtonPressed && model.IsTargetManagerAcceptAvailable)
                    {
                        //Если согласовано принимающим руководителем
                        entity.TargetManagerAccept = DateTime.Now;
                        entity.Status = StaffMovementsStatusDao.Load((int)StaffMovementsStatus.PersonelManagerBank);
                    }
                    break;
                case 4:                    
                case 5:
                    if (model.IsStopButtonAvailable && model.IsStopButtonPressed)
                    {
                        //Если нажали кнопку приостановки согласования
                        entity.Status = StaffMovementsStatusDao.Load((int)StaffMovementsStatus.Blocked);
                        //Тут еще нужно отправить письмо с угрозами руководителям и сотруднику
                    }                    
                    if (model.ISRejectAvailable && model.IsRejectButtonPressed)
                    {
                        //Если нажали кнопку отказа, то отказ и всё поезд ушёл.
                        entity.DeleteDate = DateTime.Now;
                        entity.RejectDate = DateTime.Now;
                        entity.RejectedBy = UserDao.Load(CurrentUser.Id);
                        entity.RejectReason = StaffMovementsRejectReasonDao.Load((int)StaffMovementsRejectReasonEnum.PersonnelManagerBankReject);
                        entity.Status = StaffMovementsStatusDao.Load((int)StaffMovementsStatus.Canceled);
                    }
                    if (model.IsPersonnelManagerBankAcceptAvailable && model.IsAcceptButtonPressed)
                    { 
                        //Если согласовано кадровиком банка
                        entity.PersonnelManagerBank = UserDao.Load(CurrentUser.Id);
                        entity.PersonnelManagerBankAccept = DateTime.Now;
                        entity.Status = StaffMovementsStatusDao.Load((int)StaffMovementsStatus.Chief);
                    }
                    break;
                case 6:
                    if (model.ISRejectAvailable && model.IsRejectButtonPressed)
                    {
                        //Если нажали кнопку отказа, то отказ и всё поезд ушёл.
                        entity.DeleteDate = DateTime.Now;
                        entity.RejectDate = DateTime.Now;
                        entity.RejectedBy = UserDao.Load(CurrentUser.Id);
                        entity.RejectReason = StaffMovementsRejectReasonDao.Load((int)StaffMovementsRejectReasonEnum.ChiefManagerReject);
                        entity.Status = StaffMovementsStatusDao.Load((int)StaffMovementsStatus.Canceled);
                    }
                    if (model.IsChiefAcceptAvailable && model.IsAcceptButtonPressed)
                    { 
                        //Если согласовано вышестоящим руководителем
                        entity.TargetChief = UserDao.Load(CurrentUser.Id);
                        entity.TargetChiefAccept = DateTime.Now;
                        entity.Status = StaffMovementsStatusDao.Load((int)StaffMovementsStatus.Personnel);
                    }
                    break;
                case 7:
                    if (model.ISRejectAvailable && model.IsRejectButtonPressed)
                    {
                        //Если нажали кнопку отказа, то отказ и всё поезд ушёл.
                        entity.DeleteDate = DateTime.Now;
                        entity.RejectDate = DateTime.Now;
                        entity.RejectedBy = UserDao.Load(CurrentUser.Id);
                        entity.RejectReason = StaffMovementsRejectReasonDao.Load((int)StaffMovementsRejectReasonEnum.PersonnelManagerReject);
                        entity.Status = StaffMovementsStatusDao.Load((int)StaffMovementsStatus.Canceled);
                    }
                    if (model.IsPersonnelManagerAcceptAvailable && model.IsAcceptButtonPressed)
                    {
                        //Если согласовано кадровиком
                        entity.PersonnelManager = UserDao.Load(CurrentUser.Id);
                        entity.PersonnelManagerAccept = DateTime.Now;
                        entity.Status = StaffMovementsStatusDao.Load((int)StaffMovementsStatus.ChiefControl);
                    }
                    break;
                case 8:
                    if (model.ISRejectAvailable && model.IsRejectButtonPressed)
                    {
                        //Если нажали кнопку отказа, то отказ и всё поезд ушёл.
                        entity.DeleteDate = DateTime.Now;
                        entity.RejectDate = DateTime.Now;
                        entity.RejectedBy = UserDao.Load(CurrentUser.Id);
                        entity.RejectReason = StaffMovementsRejectReasonDao.Load((int)StaffMovementsRejectReasonEnum.TargetManagerReject);
                        entity.Status = StaffMovementsStatusDao.Load((int)StaffMovementsStatus.Canceled);
                    }
                    //Проверяем все документы, если у обязательного документа нет вложения - не выходим из этого статуса
                    bool accept = true;
                    foreach (var doc in entity.Docs)
                    {
                        if (doc.IsRequired && doc.Attachment == null) accept = false;
                    }
                    if(accept) entity.Status = StaffMovementsStatusDao.Load((int)StaffMovementsStatus.DocsApproved);
                    break;
                case 9:
                    //Проверяем все документы, если у обязательного документа нет вложения - нужно вернутся обратно
                    bool accept_ = true;
                    foreach (var doc in entity.Docs)
                    {
                        if (doc.IsRequired && doc.Attachment == null) accept_ = false;
                    }
                    if (!accept_) entity.Status = StaffMovementsStatusDao.Load((int)StaffMovementsStatus.ChiefControl);
                    if (model.ISRejectAvailable && model.IsRejectButtonPressed)
                    {
                        //Если нажали кнопку отказа, то отказ и всё поезд ушёл.
                        entity.DeleteDate = DateTime.Now;
                        entity.RejectDate = DateTime.Now;
                        entity.RejectedBy = UserDao.Load(CurrentUser.Id);
                        entity.RejectReason = StaffMovementsRejectReasonDao.Load((int)StaffMovementsRejectReasonEnum.PersonnelManagerReject);
                        entity.Status = StaffMovementsStatusDao.Load((int)StaffMovementsStatus.Canceled);
                    }
                    if (model.IsConfirmButtonAvailable && model.IsConfirmButtonPressed)
                    {
                        //Заявка подтверждена. все щасливы, ждём выгрузки в одноэс
                        //entity.PersonnelManager = UserDao.Load(CurrentUser.Id);
                        //entity.PersonnelManagerAccept = DateTime.Now;
                        entity.Status = StaffMovementsStatusDao.Load((int)StaffMovementsStatus.Approved);
                    }
                    break;
                case 10:
                    break;
                case 11:
                    break;
            }
            #endregion
        }
        /// <summary>
        /// Возвращаем заявку в черновик
        /// </summary>
        /// <param name="entity">сущность заявки</param>
        private void ChangeEntityStatusToTemp(StaffMovements entity)
        {
            entity.Status = StaffMovementsStatusDao.Load((int)StaffMovementsStatus.Temp);
            entity.SendDate = null;
            entity.SourceManagerAccept = null;
            entity.TargetManagerAccept = null;
            entity.PersonnelManagerBankAccept = null;
            entity.TargetChiefAccept = null;
            entity.PersonnelManagerAccept = null;
        }
        /// <summary>
        /// Получает должности для подразделения
        /// </summary>
        /// <param name="id">ид подразделения</param>
        /// <returns>список должностей</returns>
        public IList<IdNameDto> GetPositionsForDepartment(int id)
        {
            var users = UserDao.Find(x => (x.Department!=null && x.Department.Id == id && x.Position != null));
            if (users != null && users.Any())
            {
                var positions = users.Select(x => x.Position).Distinct();
                return (positions!=null && positions.Any())?positions.Select(x=>new IdNameDto { Id=x.Id, Name = x.Name}).ToList():new List<IdNameDto>();
            }
            else return new List<IdNameDto>();
            #region Оставлено до лучших времен(появление штатного рассписания), если не наступят - удалить.
            //var positions=StaffEstablishedPostRequestDao.Find(x => x.Department.Id == id);
            //if (positions != null && positions.Any())
            //{
            //    return positions.Select(x => new IdNameDto { Id = x.Id, Name = x.Position.Name }).ToList();
            //}
            //else return new List<IdNameDto>();       
            #endregion
        }
        #endregion
        #region Files
        public AttachmentModel GetFileContext(int id)
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
                    Log.InfoFormat("Файл уже существует", entityId, type, existingAttach.Id);
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
            if (type == RequestAttachmentTypeEnum.StaffMovements)
            {
                var doc=StaffMovementsDocsDao.Load(entityId);
                doc.Attachment = attach;
                StaffMovementsDocsDao.SaveAndFlush(doc);
            }
            return attach.Id;
        }
        public bool DeleteAttachment(int id)
        {
            RequestAttachmentDao.Delete(id);
            return true;
        }
        #endregion
        #region Dictionaries
        public static string[] GetAgreementEntriesTemplate(string entry)
        {
            Dictionary<string, string[]> Entries = new Dictionary<string, string[]>();
            //пункт 1.2
            string[] entry1_2 = new string[] 
            {
                "Работник переводится, с его согласия, с должности {{SourcePosition}} {{SourceDepartment}} на должность {{TargetPosition}} {{TargetDepartment}} с {{MovementDate}} г. ",
                "Работник переводится, с его согласия, с должности {{SourcePosition}} {{SourceDepartment}} на должность {{TargetPosition}} {{TargetDepartment}} с {{MovementDate}} г. временно, {{Field}} ",
                "Работник переводится, с его согласия,  с временной должности {{SourcePosition}} {{Field}} {{SourceDepartment}} на постоянную должность {{TargetPosition}} {{TargetDepartment}} с {{MovementDate}} г.",
                "Не выбран"
            };
            Entries.Add("1_2", entry1_2);
            //Пункт 1.6
            string[] entry1_6 = new string[] 
            {
                "Фактическое место работы Работника: {{Field}}",
                "Не выбран"
            };
            Entries.Add("1_6", entry1_6);
            //Пункт 2.2.1
            string[] entry2_2_1 = new string[] 
            {
                "Должностные обязанности изменяются согласно должностной инструкции {{TargetPosition}} {{TargetDepartment}}",
                "Не выбран"
            };
            Entries.Add("2_2_1", entry2_2_1);
            //Пункт 4_2
            string[] entry4_2 = new string[] 
            {
                "РАБОТНИКУ устанавливается c {{MovementDate}} г. "+Environment.NewLine+"- базовый должностной оклад в размере {{TargetSalary}} рублей в месяц {0}",
                "РАБОТНИКУ устанавливается c {{MovementDate}} г. "+Environment.NewLine+"- базовый должностной оклад в размере {{TargetSalary}} рублей в месяц {0}"+Environment.NewLine+"Оплата труда производится пропорционально отработанному времени, исходя из оклада, что составляет {{Field}} рублей в месяц",
                "Не выбран"
            };
            Entries.Add("4_2", entry4_2);
            //Пункт 5.1
            string[] entry5_1 = new string[] 
            {
                "РАБОТНИКУ устанавливается следующий режим рабочего времени: пятидневная рабочая неделя с двумя выходными днями, продолжительность ежедневной работы 8 часов.",
                "РАБОТНИКУ устанавливается следующий режим рабочего времени: рабочая неделя с предоставлением выходных дней по скользящему графику с суммированным учетом рабочего времени за учетный период квартал.",
                "РАБОТНИКУ устанавливается следующий режим рабочего времени: рабочая неделя с предоставлением выходных дней по скользящему графику с суммированным учетом рабочего времени за учетный период 1 календарный год.",
                "РАБОТНИКУ устанавливается следующий режим рабочего времени: пятидневная рабочая неделя с двумя выходными днями, продолжительность ежедневной работы 4 часа.",
                "{{Field}}",
                "Не выбран"
            };
            Entries.Add("5_1", entry5_1);
            return Entries[entry];
        }
        public IList<IdNameDto> GetNorthExperienceTypes()
        {
            IList<IdNameDto> inDto = new List<IdNameDto> { };

            inDto.Add(new IdNameDto { Id = 1, Name = "Сотруднику не полагается северная надбавка" });
            inDto.Add(new IdNameDto { Id = 2, Name = "Северный стаж сотрудника отсутствует, начать начисление стажа с даты приема" });
            inDto.Add(new IdNameDto { Id = 3, Name = "Северный стаж у сотрудника имеется, указать количество северного стажа" });

            return inDto;
        }
        /// <summary>
        /// Получает руководителя для подразделени\
        /// </summary>
        /// <param name="dep">подразделение</param>
        /// <returns>руководитель</returns>
        public User GetManagerForDepartment(Department dep)
        {
            var managers=DepartmentDao.GetDepartmentManagers(dep.Id,true);
            if (managers != null && managers.Any())
            {
                var level=managers.Max(x => x.Level.Value);
                managers = managers.Where(x => x.Level.Value == level).ToList();
                var mainmanager = managers.Where(x => x.IsMainManager);
                if (mainmanager != null && mainmanager.Any()) return mainmanager.First();
                else return managers.First();
            }
            return null;
        }
        #endregion
        //Deprecated Временно не печатаем документы отсюда
        /*public StaffMovementsPrintModel GetPrintModel(int id)
        {
            var entity = StaffMovementsDao.Load(id);
            StaffMovementsPrintModel model = new StaffMovementsPrintModel();
            #region Money
            var bablo = entity.Data.TargetCasing;
            model.TargetCasing = String.Format("{0}({1}) рублей в месяц", bablo, RequestBl.GetSummString(bablo));
            model.Additions = new List<string>();
            if (entity.Data.AdditionPersonnelAction != 4 && entity.Data.AdditionPersonnel > 0)
            {
                var sum =entity.Data.AdditionPersonnel;
                model.Additions.Add(String.Format(" - Надбавка персональная в размере {0}({1}) рублей в месяц;",sum, RequestBl.GetSummString(sum)));
            }
            if (entity.Data.AdditionFrontAction != 4 && entity.Data.AdditionFront > 0)
            {
                var sum = entity.Data.AdditionFront;
                model.Additions.Add(String.Format(" - Надбавка за работу специалистом фронт-офиса в размере {0}({1}) рублей в месяц;", sum, RequestBl.GetSummString(sum)));
            }
            if (entity.Data.AdditionQualityAction != 4 && entity.Data.AdditionQuality > 0)
            {
                var sum = entity.Data.AdditionQuality;
                model.Additions.Add(String.Format(" - Надбавка квалификационная в размере {0}({1}) рублей в месяц;", sum, RequestBl.GetSummString(sum)));
            }
            if (entity.Data.AdditionTerritoryAction != 4 && entity.Data.AdditionTerritory > 0)
            {
                var sum = entity.Data.AdditionTerritory;
                model.Additions.Add(String.Format(" - Надбавка территориальная в размере {0}({1}) рублей в месяц;", sum, RequestBl.GetSummString(sum)));
            }
            if (entity.Data.AdditionTravelingAction != 4 && entity.Data.AdditionTraveling > 0)
            {
                var sum = entity.Data.AdditionTraveling;
                model.Additions.Add(String.Format(" - Надбавка за разъездной характер работы в размере {0}({1}) рублей в месяц;", sum, RequestBl.GetSummString(sum)));
            }
            if (entity.Data.NorthFactorAdditionAction!=4)
            {
                var sum = entity.Data.AdditionTraveling;
                model.Additions.Add(String.Format(" - Надбавка за северный стаж;"));
            }
#endregion
            #region Персонажи
            model.UserName = entity.User.Name;
            model.TargetPosition = entity.TargetPosition.Name;
            model.SourcePosition = entity.SourcePosition.Name;
            model.Chief = entity.TargetChief!=null?entity.TargetChief.Name:"";
            model.ChiefDepartment = entity.TargetChief != null && entity.TargetChief.Department!=null? entity.TargetChief.Department.Name:"";
            model.ChiefPosition = entity.TargetChief != null && entity.TargetChief.Position!=null? entity.TargetChief.Position.Name:"";
            model.TargetManager = entity.TargetManager.Name;
            model.SourceManager = entity.SourceManager.Name;
            model.SignerName = entity.Data.Signatory!=null?entity.Data.Signatory.Name:"";
            model.SignerPosition = entity.Data.Signatory!=null?entity.Data.Signatory.Position:"";
            model.SignerAdditionData= entity.Data.Signatory!=null?entity.Data.Signatory.PreamblePartyTemplate:"";
            model.PersonnelManagerBank = entity.PersonnelManagerBank!=null?entity.PersonnelManagerBank.Name:"";
            model.PersonnelManagerBankDateAccept = entity.PersonnelManagerBankAccept.HasValue? entity.PersonnelManagerBankAccept.Value.ToString("dd.MM.yyyy"):"";
            #endregion
            #region Numbers 
            model.MaterialDocNumber = entity.Data.AdditionalAgreementNumber;
            
            #endregion
            #region Dates
            model.MovementDate = entity.MovementDate.HasValue?entity.MovementDate.Value.ToString("dd.MM.yyyy") :"";
            model.PersonnelManagerBankDateAccept = entity.PersonnelManagerAccept.HasValue ? entity.PersonnelManagerAccept.Value.ToString("dd.MM.yyyy") : "";            
            model.CreateDate = entity.CreateDate.ToString("dd.MM.yyyy");
            #endregion
            #region Depts
            var dep7 = entity.SourceDepartment;
            model.SourceDepartment = dep7.Name;            
            model.Dep7 = dep7.Name;
            var dep6 = DepartmentDao.GetParentDepartmentWithLevel(dep7, 6);
            model.Dep6 = dep6 != null ? dep6.Name : "";
            var dep5 = DepartmentDao.GetParentDepartmentWithLevel(dep7, 5);
            model.Dep5 = dep5 != null ? dep5.Name : "";
            var dep4 = DepartmentDao.GetParentDepartmentWithLevel(dep7, 4);
            model.Dep4 = dep4 != null ? dep4.Name : "";
            var dep3 = DepartmentDao.GetParentDepartmentWithLevel(dep7, 3);
            model.Dep3 = dep3 != null ? dep3.Name : "";
            var dep2 = DepartmentDao.GetParentDepartmentWithLevel(dep7, 2);
            model.Dep2 = dep2 != null ? dep2.Name : "";
            var Targetdep7 = entity.TargetDepartment;
            model.TargetDep7 = Targetdep7.Name;
            model.TargetDepartment = Targetdep7.Name;
            var Targetdep6 = DepartmentDao.GetParentDepartmentWithLevel(Targetdep7, 6);
            model.TargetDep6 = Targetdep6 != null ? Targetdep6.Name : "";
            var Targetdep5 = DepartmentDao.GetParentDepartmentWithLevel(Targetdep7, 5);
            model.TargetDep5 = Targetdep5 != null ? Targetdep5.Name : "";
            var Targetdep4 = DepartmentDao.GetParentDepartmentWithLevel(Targetdep7, 4);
            model.TargetDep4 = Targetdep4 != null ? Targetdep4.Name : "";
            var Targetdep3 = DepartmentDao.GetParentDepartmentWithLevel(Targetdep7, 3);
            model.TargetDep3 = Targetdep3 != null ? Targetdep3.Name : "";
            var Targetdep2 = DepartmentDao.GetParentDepartmentWithLevel(Targetdep7, 2);
            model.TargetDep2 = Targetdep2 != null ? Targetdep2.Name : "";
            #endregion
            #region fields
            var e1_2 = entity.Data.AgreementEntry1_2;
            model.AgreementEntry1_2 = GetAgreementEntriesTemplate("1_2")[e1_2-1]
                .Replace("{{SourcePosition}}", model.SourcePosition)
                .Replace("{{SourceDepartment}}", model.SourceDepartment)
                .Replace("{{TargetPosition}}", model.TargetDepartment)
                .Replace("{{TargetDepartment}}", model.TargetDepartment)
                .Replace("{{MovementDate}}", model.MovementDate);
            if (e1_2 >= 2)
            {
                model.AgreementEntry1_2 = model.AgreementEntry1_2.Replace("{{Field}}", entity.Data.AgreementField1_2);
            }

            var e1_6 = entity.Data.AgreementEntry1_6;
            model.AgreementEntry1_6 = GetAgreementEntriesTemplate("1_6")[e1_6-1];
            if (e1_6 == 1)
            {
                model.AgreementEntry1_6 = model.AgreementEntry1_6.Replace("{{Field}}", entity.Data.AgreementField1_6);
            }

            var e2_2_1 = entity.Data.AgreementEntry2_2_1;
            model.AgreementEntry2_2_1 = GetAgreementEntriesTemplate("2_2_1")[e2_2_1 - 1]
                .Replace("{{TargetPosition}}", model.TargetDepartment)
                .Replace("{{TargetDepartment}}", model.TargetDepartment);

            var e4_2 = entity.Data.AgreementEntry4_2;
            var e5_1 = entity.Data.AgreementEntry5_1;
            
            
            if (e1_6 == 1) model.AgreementField1_6[0] = entity.Data.AgreementField1_6;
            if (e4_2 == 2) model.AgreementField4_2[0] = entity.Data.AgreementField4_2;
            if (e5_1 == 5) model.AgreementField5_1[0] = entity.Data.AgreementField5_1;
            
            

            #endregion
            model.HoursType = entity.Data.HoursType!=null? entity.Data.HoursType.Name:"";
            return model;
        }*/
        public bool CheckMovementsExist(DateTime date, int UserId, int id)
        {
            var res= StaffMovementsDao.Find(x => x.MovementDate == date && x.User.Id == UserId && x.Id != id && x.Status.Id != (int)Reports.Core.Enum.StaffMovementsStatus.Canceled);
            if (res != null && res.Any())
                return true;
            else return false;
        }
    }
}

