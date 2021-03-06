﻿using System;
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
        protected IDocumentApprovalDao documentApprovalDao;
        public IDocumentApprovalDao DocumentApprovalDao
        {
            get { return Validate.Dependency(documentApprovalDao); }
            set { documentApprovalDao = value; }
        }
        protected IRequestPrintFormDao requestPrintFormDao;
        public IRequestPrintFormDao RequestPrintFormDao
        {
            get { return Validate.Dependency(requestPrintFormDao); }
            set { requestPrintFormDao = value; }
        }
        protected IStaffMovementsFactDao staffMovementsFactDao;
        public IStaffMovementsFactDao StaffMovementsFactDao
        {
            get { return Validate.Dependency(staffMovementsFactDao); }
            set { staffMovementsFactDao = value; }
        }
        protected IStaffEstablishedPostChargeLinksDao staffEstablishedPostChargeLinksDao;
        public IStaffEstablishedPostChargeLinksDao StaffEstablishedPostChargeLinksDao
        {
            get { return Validate.Dependency(staffEstablishedPostChargeLinksDao); }
            set { staffEstablishedPostChargeLinksDao = value; }
        }
        protected IStaffExtraChargesDao staffExtraChargesDao;
        public IStaffExtraChargesDao StaffExtraChargesDao
        {
            get { return Validate.Dependency(staffExtraChargesDao); }
            set { staffExtraChargesDao = value; }
        }
        protected IStaffExtraChargeActionsDao staffExtraChargeActionsDao;
        public IStaffExtraChargeActionsDao StaffExtraChargeActionsDao
        {
            get { return Validate.Dependency(staffExtraChargeActionsDao); }
            set { staffExtraChargeActionsDao = value; }
        }
        protected IStaffPostChargeLinksDao staffPostChargeLinksDao;
        public IStaffPostChargeLinksDao StaffPostChargeLinksDao
        {
            get { return Validate.Dependency(staffPostChargeLinksDao); }
            set { staffPostChargeLinksDao = value; }
        }
        protected IStaffEstablishedPostUserLinksDao staffEstablishedPostUserLinksDao;
        public IStaffEstablishedPostUserLinksDao StaffEstablishedPostUserLinksDao
        {
            get { return Validate.Dependency(staffEstablishedPostUserLinksDao); }
            set { staffEstablishedPostUserLinksDao = value; }
        }
        protected IStaffEstablishedPostDao staffEstablishedPostDao;
        public IStaffEstablishedPostDao StaffEstablishedPostDao
        {
            get { return Validate.Dependency(staffEstablishedPostDao); }
            set { staffEstablishedPostDao = value; }
        }
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
        #region Public Methods
        #region Реестр факт. кадровых перемещений
        public StaffMovementsFactListModel GetFactListModel()
        {
            return new StaffMovementsFactListModel();
        }
        public GridDefinition GetFactDocuments(StaffMovementsFactListModel model)
        {
            var user = UserDao.Load(CurrentUser.Id);
            //var query= QueryCreator.Create<StaffMovementsFact, StaffMovementsFactListModel>(model, user, CurrentUser.UserRole);
            var data = StaffMovementsFactDao.GetDocuments(CurrentUser.Id, 
                CurrentUser.UserRole, 
                model.Number, 
                model.StaffEstablishedPostRequestId.HasValue?model.StaffEstablishedPostRequestId.Value:0, 
                model.StaffMovementsId.HasValue? model.StaffMovementsId.Value:0, 
                model.DepartmentId, 
                model.UserName);
            var result= data.Select(x => new StaffMovementsFactDto
            {
                Id = x.Id,
                StaffMovementsId = x.StaffMovements!=null?x.StaffMovements.Id:0,
                SendTo1C = x.SendTo1C,
                User = x.User.Name,
                UserDep3= x.User.Department.Dep3.First().Name,
                UserDep7 = x.User.Department.Name,                
                StaffEstablishedPostRequestId =x.StaffEstablishedPostRequest!=null? x.StaffEstablishedPostRequest.Id:0,
                IsOk = x.IsOk
                
            }).ToList();
            return UIGrid_Helper.GetGridDefinition(result);
        }
        #endregion
        #region фактическое перемещение редактирвоание 
        public StaffMovementsFactEditModel GetFactEditModel(int Id)
        {
            var entity = StaffMovementsFactDao.Load(Id);
            
            StaffMovementsFactEditModel model = new StaffMovementsFactEditModel();
            model.Signers = EmploymentSignersDao.LoadAll().Select(x=>new IdNameDto{ Id=x.Id, Name=x.Name}).ToList();
            model.SignerId = model.Signers.First().Id;
            model.IsDocsReceived = entity.IsDocumentsReceived;
            model.Id = entity.Id;
            model.User.Id = entity.User.Id;
            model.IsDocsAddAvailable = (CurrentUser.Id == model.User.Id || (CurrentUser.UserRole & UserRole.PersonnelManager)>0|| CheckIsChief(entity.User.Department.Id, CurrentUser)) && !model.IsDocsReceived;
            LoadUserData(model.User);
            var usr = UserDao.Load(model.User.Id);
            model.ActiveAdditions = GetUserActualAddition(model.User.Id);
            model.IsOrderAvailable = RequestPrintFormDao.QueryExpression(x => x.RequestId == Id && x.RequestTypeId == (int)RequestPrintFormTypeEnum.StaffMovementsOrder).Count>0;
            model.IsDMOAvailable = RequestPrintFormDao.QueryExpression(x => x.RequestId == Id && x.RequestTypeId == (int)RequestPrintFormTypeEnum.StaffMovementsDMO).Count > 0;
            model.IsAgreementAdditionAvailable = RequestPrintFormDao.QueryExpression(x => x.RequestId == Id && x.RequestTypeId == (int)RequestPrintFormTypeEnum.StaffMovementsAgreementAddition).Count > 0;
            model.IsAgreementAvailable = RequestPrintFormDao.QueryExpression(x => x.RequestId == Id && x.RequestTypeId == (int)RequestPrintFormTypeEnum.StaffMovementsAgreement).Count > 0;
            if (entity.AgreementAdditionalDoc.HasValue)
            {
                model.AgreementAdditionalDocDto = new UploadFileDto();
                var file = RequestAttachmentDao.Load(entity.AgreementAdditionalDoc.Value);
                model.AgreementAdditionalDocDto.FileName = file.FileName;
                model.AgreementAdditionalDocId = file.Id;
            }
            if (entity.AgreementDoc.HasValue)
            {
                model.AgreementDocDto = new UploadFileDto();
                var file = RequestAttachmentDao.Load(entity.AgreementDoc.Value);
                model.AgreementDocDto.FileName = file.FileName;
                model.AgreementDocId = file.Id;
            }
            if (entity.MaterialLiabilityDoc.HasValue)
            {
                model.MaterialLiabilityDocDto = new UploadFileDto();
                var file = RequestAttachmentDao.Load(entity.MaterialLiabilityDoc.Value);
                model.MaterialLiabilityDocDto.FileName = file.FileName;
                model.MaterialLiabilityDocAttachmentId  = file.Id;
            }
            if (entity.RequirementsOrderDoc.HasValue)
            {
                model.RequirementsOrderDocDto = new UploadFileDto();
                var file = RequestAttachmentDao.Load(entity.RequirementsOrderDoc.Value);
                model.RequirementsOrderDocDto.FileName = file.FileName;
                model.RequirementsOrderDocAttachmentId = file.Id;
            }
            if (entity.ServiceOrderDoc.HasValue)
            {
                model.ServiceOrderDocDto = new UploadFileDto();
                var file = RequestAttachmentDao.Load(entity.ServiceOrderDoc.Value);
                model.ServiceOrderDocDto.FileName = file.FileName;
                model.ServiceOrderDocAttachmentId = file.Id;
            }
            if (entity.OrderDoc.HasValue)
            {
                model.OrderDocDto = new UploadFileDto();
                var file = RequestAttachmentDao.Load(entity.OrderDoc.Value);
                model.OrderDocDto.FileName = file.FileName;
                model.OrderDocId = file.Id;
            }
            model.RegionCoefficient = StaffMovementsDao.GetUserRegionCoeff(model.User.Id);
            model.TotalSalary = StaffMovementsDao.GetUserTotalSalary(model.User.Id);
            model.Casing = StaffMovementsDao.GetUserSalary(model.User.Id);
            model.Salary = usr.Rate.HasValue ? usr.Rate.Value : 0;
            model.IsCheckByPersonelAvailable = ((CurrentUser.UserRole & UserRole.PersonnelManager) > 0 && !model.IsDocsReceived);
            model.IsSaveAvailable = model.IsDocsAddAvailable || model.IsCheckByPersonelAvailable;
            return model;
        }
        public StaffMovementsFactEditModel SaveFact(StaffMovementsFactEditModel model)
        {
            var entity = StaffMovementsFactDao.Load(model.Id);
            model.IsDocsAddAvailable = (CurrentUser.Id == model.User.Id || (CurrentUser.UserRole & UserRole.PersonnelManager) > 0 || CheckIsChief(entity.User.Department.Id, CurrentUser)) && !entity.IsDocumentsReceived;
            if ((CurrentUser.UserRole & UserRole.PersonnelManager) > 0)
            {
                entity.IsDocumentsReceived = model.IsDocsReceived;
            }
            if (model.IsDocsAddAvailable)
            {
                if (model.AgreementAdditionalDocDto != null)
                {
                    string tmp = "";
                    var id = SaveAttachment(model.Id, 0, model.AgreementAdditionalDocDto, RequestAttachmentTypeEnum.AgreementAdditionalDocDto, out tmp);
                    if (id.HasValue)
                    {
                        model.AgreementAdditionalDocId = id.Value;
                        model.AgreementAdditionalDocDto.FileName = tmp;
                        entity.AgreementAdditionalDoc = id.Value;
                    }
                }
                if (model.AgreementDocDto != null)
                {
                    string tmp = "";
                    var id = SaveAttachment(model.Id, 0, model.AgreementDocDto, RequestAttachmentTypeEnum.AgreementDocDto, out tmp);
                    if (id.HasValue)
                    {
                        model.AgreementDocId = id.Value;
                        model.AgreementDocDto.FileName = tmp;
                        entity.AgreementDoc = id.Value;
                    }
                }
                if (model.MaterialLiabilityDocDto != null)
                {
                    string tmp = "";
                    var id = SaveAttachment(model.Id, 0, model.MaterialLiabilityDocDto, RequestAttachmentTypeEnum.MaterialLiabilityDocDto, out tmp);
                    if (id.HasValue)
                    {
                        model.MaterialLiabilityDocAttachmentId = id.Value;
                        model.MaterialLiabilityDocDto.FileName = tmp;
                        entity.MaterialLiabilityDoc = id.Value;
                    }
                }
                if (model.OrderDocDto != null)
                {
                    string tmp = "";
                    var id = SaveAttachment(model.Id, 0, model.OrderDocDto, RequestAttachmentTypeEnum.OrderDocDto, out tmp);
                    if (id.HasValue)
                    {
                        model.OrderDocId = id.Value;
                        model.OrderDocDto.FileName = tmp;
                        entity.OrderDoc = id.Value;
                    }
                }
                if (model.RequirementsOrderDocDto != null)
                {
                    string tmp = "";
                    var id = SaveAttachment(model.Id, 0, model.RequirementsOrderDocDto, RequestAttachmentTypeEnum.RequirementsOrderDocDto, out tmp);
                    if (id.HasValue)
                    {
                        model.RequirementsOrderDocAttachmentId = id.Value;
                        model.RequirementsOrderDocDto.FileName = tmp;
                        entity.RequirementsOrderDoc = id.Value;
                    }
                }
                if (model.ServiceOrderDocDto != null)
                {
                    string tmp = "";
                    var id = SaveAttachment(model.Id, 0, model.ServiceOrderDocDto, RequestAttachmentTypeEnum.ServiceOrderDocDto, out tmp);
                    if (id.HasValue)
                    {
                        model.ServiceOrderDocAttachmentId = id.Value;
                        model.ServiceOrderDocDto.FileName = tmp;
                        entity.ServiceOrderDoc = id.Value;
                    }
                }
            }
            model.IsDocsAddAvailable = (CurrentUser.Id == model.User.Id) && !model.IsDocsReceived;
            model.IsSaveAvailable = model.IsDocsAddAvailable || ((CurrentUser.UserRole & UserRole.PersonnelManager) > 0 && !model.IsDocsReceived);
            model = GetFactEditModel(model.Id);
            StaffMovementsFactDao.SaveAndFlush(entity);
            return model;
        }
        #endregion
        #region Реестр заявок
        /// <summary>
        /// Получение вьюмодели для реестра заявок
        /// </summary>
        /// <returns></returns>
        public StaffMovementsListModel GetListModel()
        {
            StaffMovementsListModel model = new StaffMovementsListModel();
            model.BeginDate =new DateTime( DateTime.Now.Year,DateTime.Now.Month,1);
            model.Types = StaffMovementsTypesDao.LoadAll().Select(x => new IdNameDto { Id = x.Id, Name = x.Name }).ToList();
            model.Types.Add(new IdNameDto { Id = 0, Name = "Все" });
            model.TypeId = 0;
            model.Statuses = StaffMovementsStatusDao.LoadAll().Select(x => new IdNameDto { Id = x.Id, Name = x.Name }).ToList();
            model.Statuses.Add(new IdNameDto { Id = 0, Name = "Все" });
            model.Status = 0;
            return model;
        }
        /// <summary>
        /// Получение документов кодрового перемещения
        /// </summary>
        /// <param name="DepartmentId"></param>
        /// <param name="UserName"></param>
        /// <param name="Number"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        public IList<StaffMovementsDto> GetDocuments(int DepartmentId, string UserName, int Number, int Status, int TypeId)
        {
            var docs = StaffMovementsDao.GetDocuments(CurrentUser.Id, CurrentUser.UserRole ,DepartmentId, UserName, Number, Status, TypeId);
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
                    TypeId = x.Type.Id,
                    Salary = x.Data.Salary,
                    Position = x.User != null ? (x.User.Position!=null?x.User.Position.Name:"") : "",
                    UserName = x.User!=null?x.User.Name:"",
                    PositionCurrent = x.SourcePosition !=null ? x.SourcePosition.Name:"",
                    PositionTarget = x.Type.Id>=2?(x.TargetPosition != null ? x.TargetPosition.Name : ""):"",
                    TargetDep7Name = x.Type.Id==2?(x.TargetDepartment!=null ? x.TargetDepartment.Name:""):"",
                    TargetDep3Name = x.Type.Id==2?(x.TargetDepartment!=null ? ((x.TargetDepartment.Dep3 != null && x.TargetDepartment.Dep3.Any()) ? x.TargetDepartment.Dep3.First().Name : ""):""):""
                }).ToList();
            else return new List<StaffMovementsDto>();
        }
        #endregion
        #region Редактирование заявок
        /// <summary>
        /// Получение вьюмодели для редактирования заявки
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public StaffMovementsEditModel GetEditModel(int id)
        {
            var model = new StaffMovementsEditModel();
            if (id == 0)
            {
                model.User = new StandartUserDto();
                model.Creator = new StandartUserDto();
                SetModelAdditions(model);
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
        /// <summary>
        /// Загрузка модели
        /// </summary>
        /// <param name="model"></param>
        public void SetModel(StaffMovementsEditModel model)
        {
            
            if (model.Id > 0)
            {
                var entity = StaffMovementsDao.Load(model.Id);
                #region Стандартные поля заявки

                model.UserLinkId = entity.TargetStaffEstablishedPostRequest.Id;
                GetMoneyForStaffEstablishedPostUserLinks(entity.TargetStaffEstablishedPostRequest, model);
                model.StatusId = entity.Status.Id;
                model.Status = entity.Status.Name;
                model.Creator.Id = entity.Creator.Id;
                model.Number = String.Format("№{0}", entity.Id);
                model.User.Id = entity.User.Id;
                model.RequestType = entity.Type.Id;
                model.MovementDate = entity.MovementDate;
                var lastDate = entity.MovementDate.Value.AddMonths(1);
                lastDate = new DateTime(lastDate.Year, lastDate.Month, 5);                
                model.IsRequestBad = (DateTime.Now >= lastDate) && entity.Status.Id< 10;
                model.MovementReason = entity.Data.MovementReason;
                model.TargetSalaryCount = entity.Data.Salary;
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
                model.Chief = entity.TargetChief != null ? entity.TargetChief.Name : "";
                model.PersonnelManagerAccept = entity.PersonnelManagerAccept;
                model.PersonnelManager = entity.PersonnelManager != null ? entity.PersonnelManager.Name : "";
                #endregion
                #region Данные о переводе
                model.MovementDate = entity.MovementDate;
                model.MovementTempTo = entity.MovementTempTo;
                model.TargetPositionId = entity.TargetPosition.Id;
                model.TargetPositions = new List<IdNameDto>();
                model.TargetPositions.Add(new IdNameDto { Id = model.TargetPositionId, Name = model.TargetPositionName });
                model.TargetPositionName = entity.TargetPosition.Name;
                model.TargetDepartmentId = entity.TargetDepartment.Id;
                model.TargetDepartmentName = entity.TargetDepartment.Name;
                model.TargetManager = new StandartUserDto { Id = entity.TargetManager.Id };
                LoadUserData(model.TargetManager);
                var TargetUppers = DocumentApprovalDao.Find(x => x.DocId == entity.Id && x.Number == 2 && !x.IsArchive );
                if (TargetUppers != null && TargetUppers.Any())
                {
                    model.TargetManager.Name += String.Format("({0})", TargetUppers.First().AssistantUser.Name);
                }
                model.SourceManager = new StandartUserDto { Id = entity.SourceManager.Id };
                LoadUserData(model.SourceManager);
                var SourceUppers = DocumentApprovalDao.Find(x => x.DocId == entity.Id && x.Number == 1 && !x.IsArchive );
                if (SourceUppers != null && SourceUppers.Any())
                {
                    model.SourceManager.Name += String.Format("({0})", SourceUppers.First().AssistantUser.Name);
                }
                #endregion
                #region Для руководителей
                model.IsTempMoving = entity.IsTempMoving;
                model.Conjunction = entity.Data.Conjunction;
                model.MovementCondition = entity.Data.MovementCondition;
                model.PyrusLink = entity.Data.PyrusLink;
                #endregion
                #region Для кадровиков
                model.TargetCasingType = entity.Data.TargetCasingType;
                model.RegionCoefficient = entity.Data.RegionCoefficient;
                model.Grade = entity.Data.Grade;
                model.HoursType = entity.Data.HoursType != null ? entity.Data.HoursType.Id : 0;

                //model.AccessGroup = entity.Data.AccessGroup!=null?entity.Data.AccessGroup.Id:0;
                #endregion
                #region Files
                var docs = entity.Docs;
                if (docs != null && docs.Any())
                {                    
                    var MovementNote = docs.Where(x => x.DocType == (int)StaffMovementsDocsTypes.MovementNote).First();
                    model.MovementNoteDto = new UploadFileDto();
                    model.MovementNoteIsRequired = MovementNote != null ? MovementNote.IsRequired : false;
                    if (MovementNote != null && MovementNote.Attachment != null)
                    {
                        model.MovementNoteDto.FileName = MovementNote.Attachment.FileName;
                        model.MovementNoteAttachmentId = MovementNote.Attachment.Id;
                    }
                   
                }
                #endregion
            }
            else
            {
                var tmp = UserDao.Load(model.User.Id);
                model.HoursType = tmp.HoursType.Id;
                model.TargetSalaryCount = tmp.Rate.HasValue ? tmp.Rate.Value : 0;
            }
            //Подгружаем данные о сотруднике
            LoadUserData(model.User);
            var usertomove = UserDao.Load(model.User.Id);
            var userlinks=StaffEstablishedPostUserLinksDao.QueryExpression(x=>x.IsUsed && x.User.Id==model.User.Id);
            if (userlinks == null || !userlinks.Any())
                throw new Exception("Сотрудника нет в штатной расстановке.");
            var userlink = userlinks.First();
            model.RegionCoefficient = StaffMovementsDao.GetUserRegionCoeff(model.User.Id);
            model.TotalSalary = StaffMovementsDao.GetUserTotalSalary(model.User.Id);
            model.Casing = StaffMovementsDao.GetUserSalary(model.User.Id);
            model.Salary = usertomove.Rate.HasValue?usertomove.Rate.Value:0;
            //Подгружаем надбавки
            SetModelAdditions(model);
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
                    //model.TargetPositions = GetPositionsForDepartment(model.TargetDepartmentId);
                    //В случае если КП на изменение надбавок, нужно подгрузить текущий линк, в противном случае нельзя давать возможности выбрать ту же должность
                    model.UserLinks = GetPositionsForDepartment(model.TargetDepartmentId);
                    model.TargetPositions = model.UserLinks;
                    if (model.RequestType == 1)
                    {
                        model.UserLinkId = userlinks.First().Id;
                        if (!model.UserLinks.Any(x => x.Id == model.UserLinkId))
                        {
                            model.UserLinks.Add(new IdNameDto { Id = userlink.Id, Name = userlink.StaffEstablishedPost.Position.Name });
                        }
                    }
                    else if(model.UserLinks!=null && model.UserLinks.Any())
                    {
                        model.UserLinkId = model.UserLinks.First().Id;
                    }
                    if (model.UserLinkId > 0)
                    {
                        userlink = StaffEstablishedPostUserLinksDao.Load(model.UserLinkId);
                        GetMoneyForStaffEstablishedPostUserLinks(userlink, model);
                    }
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
        /// <summary>
        /// Сохранение модели
        /// </summary>
        /// <param name="model"></param>
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
       
        /// <summary>
        /// Получает должности для подразделения
        /// </summary>
        /// <param name="id">ид подразделения</param>
        /// <returns>список должностей</returns>
        public IList<IdNameDto> GetPositionsForDepartment(int id)
        {
            var positions = StaffEstablishedPostDao.GetStaffEstablishedArrangements(id);
            if (positions != null && positions.Any())
            {
                return positions.Where(x => x.IsVacation).Select(x => new IdNameDto { Id = x.Id, Name = x.PositionName +" "+String.Format("(Уровень:{0} Ранг:{1})",x.PositionLevel,x.PositionRank)
                    + " " + (x.IsSTD ? "- СТД " : " ") + (String.IsNullOrEmpty(x.ReplacedName)?"":"- ") + x.ReplacedName }).ToList();
            }
            else return new List<IdNameDto>();
            #region depricated
            /*var users = UserDao.Find(x => (x.Department!=null && x.Department.Id == id && x.Position != null));
            if (users != null && users.Any())
            {
                var positions = users.Select(x => x.Position).Distinct();
                return (positions!=null && positions.Any())?positions.Select(x=>new IdNameDto { Id=x.Id, Name = x.Name}).ToList():new List<IdNameDto>();
            }
            else return new List<IdNameDto>();*/
            #endregion
            #region Оставлено до лучших времен(появление штатного рассписания), если не наступят - удалить.
            //var positions=StaffEstablishedPostRequestDao.Find(x => x.Department.Id == id);
            //if (positions != null && positions.Any())
            //{
            //    return positions.Select(x => new IdNameDto { Id = x.Id, Name = x.Position.Name }).ToList();
            //}
            //else return new List<IdNameDto>();       
            #endregion
        }
        /// <summary>
        /// Проверка существования заявки
        /// </summary>
        /// <param name="date">Дата перемещения</param>
        /// <param name="UserId">Сотрудник</param>
        /// <param name="id">Идентивикатор текущей заявки</param>
        /// <returns></returns>
        public bool CheckMovementsExist(DateTime date, int UserId, int id)
        {
            var res = StaffMovementsDao.QueryExpression(x => x.MovementDate == date && x.User.Id == UserId && x.Id != id && x.Status.Id != (int)Reports.Core.Enum.StaffMovementsStatus.Canceled);
            if (res != null && res.Any())
                return true;
            else return false;
        }
        #endregion
        #endregion
        #region Private methods
        
        /// <summary>
        /// Получает руководителя для подразделени\
        /// </summary>
        /// <param name="dep">подразделение</param>
        /// <returns>руководитель</returns>
        private User GetManagerForDepartment(Department dep)
        {
            var managers = DepartmentDao.GetDepartmentManagersByDepAndManualRole(dep.Id,4);
            if (managers != null && managers.Any())
            {
                var level = managers.Max(x => x.Level.Value);
                managers = managers.Where(x => x.Level.Value == level).ToList();
                var mainmanager = managers.Where(x => x.IsMainManager);
                if (mainmanager != null && mainmanager.Any()) return mainmanager.First();
                else return managers.First();
            }
            return null;
        }
        /// <summary>
        /// Получаем список текущих надбавок
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private List<AdditionsDto> GetUserActualAddition(int userId)
        {
            return StaffPostChargeLinksDao.QueryExpression(x => x.IsActive && x.Staff.Id == userId)
                            .Select(x =>
                                new AdditionsDto
                                {
                                    Action = x.ExtraChargeActions.Id,
                                    Type = new IdNameDto { Id = x.ExtraCharges.Id, Name = x.ExtraCharges.Name },
                                    guid = x.ExtraCharges.GUID,
                                    Value = x.Salary
                                })
                            .ToList();
        }
        /// <summary>
        /// Устанавливаем надбавки
        /// </summary>
        /// <param name="model"></param>
        private void SetModelAdditions(StaffMovementsEditModel model)
        {
            model.ActiveAdditions = GetUserActualAddition(model.User.Id);
            if (model.Id > 0)
            {
                model.AdditionsToEdit = StaffPostChargeLinksDao.QueryExpression(x => x.StaffMovements.Id == model.Id)
                .Select(x =>
                    new AdditionsDto
                    {
                        Action = x.ExtraChargeActions.Id,
                        Type = new IdNameDto { Id = x.ExtraCharges.Id, Name = x.ExtraCharges.Name },
                        guid = x.ExtraCharges.GUID,
                        Value = x.Salary
                    })
                .ToList();
            }
            else
            {
                model.AdditionsToEdit = StaffPostChargeLinksDao.QueryExpression(x => x.IsActive && x.Staff.Id == model.User.Id)
                .Select(x =>
                    new AdditionsDto
                    {
                        Action = x.ExtraChargeActions.Id,
                        Type = new IdNameDto { Id = x.ExtraCharges.Id, Name = x.ExtraCharges.Name },
                        guid = x.ExtraCharges.GUID,
                        Value = x.Salary
                    })
                .ToList();
                var charges = StaffExtraChargesDao.LoadAll();
                foreach (var charge in charges)
                {
                    if (!model.AdditionsToEdit.Any(x => x.Type.Id == charge.Id))
                        model.AdditionsToEdit.Add(new AdditionsDto { Action = 4, Value = 0, Type = new IdNameDto { Id = charge.Id, Name = charge.Name }, guid = charge.GUID });
                }
            }
        }
        public EmailDto SendEmailToUser(int userid, string subj, string text)
        {
            var user = UserDao.Load(userid);
            var dto = SendEmail(user.Email, subj, text);
            return dto;
        }
        /// <summary>
        /// Справочники
        /// </summary>
        /// <param name="model"></param>
        private void LoadDictionaries(StaffMovementsEditModel model)
        {
            model.UsersTo = new List<IdNameDto>();
            model.Subject = String.Format("Заявка на кадровое перемещение №{0}", model.Number);
            try
            {
                var currentuser = UserDao.Load(CurrentUser.Id);
                var currentrole = RoleDao.Load((int)CurrentUser.UserRole);
                model.EmailMessage = String.Format("{0} {1} {2} {3}", Environment.NewLine, CurrentUser.Name, Environment.NewLine, currentuser.Position != null ? currentuser.Position.Name : currentrole.Name);
            }
            catch (Exception e) { Log.Error(e.Message); }
            if (model.StatusId > 1 && model.StatusId < 10 && model.Id>0)
            {                
                var entity = StaffMovementsDao.Get(model.Id);
                if(entity.User!=null && !String.IsNullOrEmpty(entity.User.Email))
                    model.UsersTo.Add(new IdNameDto { Id=entity.User.Id, Name=entity.User.Name });
                if (entity.SourceManager != null && !String.IsNullOrEmpty(entity.SourceManager.Email))
                    model.UsersTo.Add(new IdNameDto { Id = entity.SourceManager.Id, Name = entity.SourceManager.Name });
                if (entity.TargetManager != null && !String.IsNullOrEmpty(entity.TargetManager.Email))
                    model.UsersTo.Add(new IdNameDto { Id = entity.TargetManager.Id, Name = entity.TargetManager.Name });
                if (entity.TargetChief != null && !String.IsNullOrEmpty(entity.TargetChief.Email))
                    model.UsersTo.Add(new IdNameDto { Id = entity.TargetChief.Id, Name = entity.TargetChief.Name });
                if (entity.PersonnelManager != null && !String.IsNullOrEmpty(entity.PersonnelManager.Email))
                    model.UsersTo.Add(new IdNameDto { Id = entity.PersonnelManager.Id, Name = entity.PersonnelManager.Name });
                if (entity.PersonnelManagerBank != null && !String.IsNullOrEmpty(entity.PersonnelManagerBank.Email))
                    model.UsersTo.Add(new IdNameDto { Id = entity.PersonnelManagerBank.Id, Name = entity.PersonnelManagerBank.Name });
                var TargetUppers = DocumentApprovalDao.Find(x => x.DocId == entity.Id && x.Number == 2 && !x.IsArchive);
                if (TargetUppers != null && TargetUppers.Any())
                {
                    var upper2= TargetUppers.First().AssistantUser;
                    if (!String.IsNullOrEmpty(upper2.Email)) model.UsersTo.Add(new IdNameDto { Id = upper2.Id, Name = upper2.Name });
                }
                var SourceUppers = DocumentApprovalDao.Find(x => x.DocId == entity.Id && x.Number == 1 && !x.IsArchive);
                if (SourceUppers != null && SourceUppers.Any())
                {
                    var upper1 = SourceUppers.First().AssistantUser;
                    if (!String.IsNullOrEmpty(upper1.Email)) model.UsersTo.Add(new IdNameDto { Id = upper1.Id, Name = upper1.Name });
                }
                model.UsersTo = model.UsersTo.Distinct(new IdEqualityComparer()).ToList();
                model.IsSendEmailAvailable = (CurrentUser.UserRole & (UserRole.PersonnelManager | UserRole.ConsultantPersonnel)) > 0 
                    || model.UsersTo.Any(x=>x.Id==CurrentUser.Id);   
            }
            var extracharges = ExtraChargesDao.LoadAll();
            if (extracharges != null && extracharges.Any())
            {
                model.NorthFactorOrders = extracharges.Select(x => new IdNameDto { Id = x.Id, Name = x.Name }).ToList();
            }
            var HoursTypes = ScheduleDao.LoadAll();
            if (HoursTypes != null && HoursTypes.Any())
            {
                model.HoursTypes = HoursTypes.Select(x => new IdNameDto { Id = x.Id, Name = x.Name }).ToList();
            }
            var AccessGroups = AccessGroupDao.LoadAll();
            if (AccessGroups != null && AccessGroups.Any())
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
            if (model.TargetDepartmentId > 0)
            {
                model.UserLinks = GetPositionsForDepartment(model.TargetDepartmentId);
                if (model.UserLinks == null) model.UserLinks = new List<IdNameDto>();
                if (model.UserLinkId > 0 && !model.UserLinks.Any(x => x.Id == model.UserLinkId))
                {
                    var link = StaffEstablishedPostUserLinksDao.Load(model.UserLinkId);
                    model.UserLinks.Add(new IdNameDto { Name = link.StaffEstablishedPost.Position.Name, Id = link.Id });
                }
            }
            else model.UserLinks = new List<IdNameDto>();
        }
        /// <summary>
        /// Сохранение файлов
        /// </summary>
        /// <param name="model"></param>
        private void SaveFiles(StaffMovementsEditModel model)
        {
            //Сохраняем файлы
            var entity = StaffMovementsDao.Load(model.Id);
            var docs = entity.Docs;
            #region файлики
            var tmp = "";
            var MovementNote = docs.Where(x => x.DocType == (int)StaffMovementsDocsTypes.MovementNote).First();           
            var id = SaveAttachment(MovementNote.Id, MovementNote.Attachment != null ? MovementNote.Attachment.Id : 0, model.MovementNoteDto, RequestAttachmentTypeEnum.StaffMovements, out tmp);
            if (id.HasValue)
            {
                var at = RequestAttachmentDao.Load(id.Value);
                MovementNote.Attachment = at;
                StaffMovementsDocsDao.SaveAndFlush(MovementNote);
            }
            #endregion
        }
        /// <summary>
        /// Установка флажков
        /// </summary>
        /// <param name="model"></param>
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
            if (model.StatusId > 1 /*&& UserRole.PersonnelManager != CurrentUser.UserRole*/)
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
                    model.ISRejectAvailable = true;
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
                    model.IsCancelAvailable = true;
                    model.ISRejectAvailable = true;
                    model.IsManagerEditable = false;
                    model.IsStopButtonAvailable = true;
                    model.IsPersonnelManagerBankAcceptAvailable = true;
                    model.IsTargetManagerAcceptAvailable = false;
                    break;
                case 5://Заблокирована кадровиком банка. Доступно согласование кадровиком банка
                    model.IsCancelAvailable = true;
                    model.ISRejectAvailable = true;
                    model.IsManagerEditable = true;
                    model.IsStopButtonAvailable = true;
                    model.IsPersonnelManagerBankAcceptAvailable = true;
                    model.IsTargetManagerAcceptAvailable = true;
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
                    SetAdditionFlags(model.AdditionsToEdit, CurrentUser.UserRole, false);
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
                    SetAdditionFlags(model.AdditionsToEdit, CurrentUser.UserRole, false);
                    break;
                case UserRole.Manager:
                    bool IsUpperSource = false;
                    bool IsUpperTarget = false;
                    if (model.SourceManager != null)
                    {
                        var sourcemanager = UserDao.Load(model.SourceManager.Id);
                        if(sourcemanager!=null && sourcemanager.Department!=null)
                            IsUpperSource = CheckIsChief(sourcemanager.Department.Id, CurrentUser);
                    }
                    if (model.TargetManager != null)
                    {
                        var targetmanager = UserDao.Load(model.TargetManager.Id);
                        if (targetmanager != null && targetmanager.Department != null)
                            IsUpperTarget = CheckIsChief(targetmanager.Department.Id, CurrentUser);

                    }
                    model.IsDocsVisible = true;
                    model.IsPersonnelVisible = false;
                    model.IsManagerVisible = true;
                    model.IsPersonnelManagerEditable = false; //Редактирование кадровиком
                    model.IsManagerEditable = model.IsManagerEditable && (model.Id == 0 || (model.TargetManager != null ? (model.TargetManager.Id == CurrentUser.Id || IsUpperTarget) : false));//Редактирование руководителем, должно быть доступно только принимающему руководителю
                    model.IsDocsEditable = false;//Редактирование документов
                    model.IsDocsAddAvailable = model.IsDocsAddAvailable && true;//Добавление документов

                    model.IsUserAcceptAvailable = false; //Утверждение сотрудником
                    model.SendDate = model.SendDate.HasValue ? model.SendDate : DateTime.Now;
                    model.ISRejectAvailable = model.ISRejectAvailable && true; //Отмена
                    model.IsSourceManagerAcceptAvailable = model.IsSourceManagerAcceptAvailable && model.SourceManager != null ? (model.SourceManager.Id == CurrentUser.Id || IsUpperSource) : false;//Утверждение отпускающим руководителем. Должно быть доступно только отпускающему
                    model.IsTargetManagerAcceptAvailable = model.IsTargetManagerAcceptAvailable && model.TargetManager != null ? (model.TargetManager.Id == CurrentUser.Id || IsUpperTarget): false;//Утверждение принимающим руководителем
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
                    model.IsPositionEditable = model.IsPositionEditable && (model.Id == 0 || model.IsTargetManagerAcceptAvailable || model.IsSourceManagerAcceptAvailable);
                    SetAdditionFlags(model.AdditionsToEdit, CurrentUser.UserRole, model.IsManagerEditable);
                    break;
                case UserRole.ConsultantPersonnel:
                    model.IsDocsVisible = false;
                    model.IsPersonnelVisible = false;
                    model.IsManagerVisible = true;
                    model.IsPersonnelManagerEditable = false; //Редактирование кадровиком
                    model.IsManagerEditable = (model.IsPersonnelManagerBankAcceptAvailable) && true;//Редактирование руководителем, должно быть доступно только принимающему руководителю
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
                    SetAdditionFlags(model.AdditionsToEdit, CurrentUser.UserRole, false);
                    break;
                case UserRole.PersonnelManager:
                    
                    model.IsDocsVisible = true;
                    model.IsPersonnelVisible = true;
                    model.IsManagerVisible = true;
                    model.IsPersonnelManagerEditable = model.IsPersonnelManagerEditable && true; //Редактирование кадровиком
                    model.IsManagerEditable = (model.IsPersonnelManagerEditable || model.IsManagerEditable) && true;//Редактирование руководителем, должно быть доступно только принимающему руководителю
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
                    SetAdditionFlags(model.AdditionsToEdit, CurrentUser.UserRole, model.IsPersonnelManagerEditable);
                    /*model.IsTargetManagerAcceptAvailable = true && !model.TargetManagerAccept.HasValue;
                    model.IsSourceManagerAcceptAvailable = true && !model.SourceManagerAccept.HasValue;*/
                    break;
            }
            model.IsSaveAvailable = (model.StatusId == 1 && (model.Creator.Id == CurrentUser.Id || model.User.Id == CurrentUser.Id))
                || model.IsManagerEditable
                || model.IsPersonnelManagerEditable
                || model.IsDocsAddAvailable;
        }
        /// <summary>
        /// Флаги редактирования надбавок
        /// </summary>
        /// <param name="additions">Надбавки</param>
        /// <param name="role">Роль текущего пользовтеля</param>
        /// <param name="isEditable">Доступно ли редактирование</param>
        private void SetAdditionFlags(IList<AdditionsDto> additions, UserRole role, bool isEditable)
        {
            var ShowAllToRoles = UserRole.PersonnelManager | UserRole.OutsourcingManager;
            foreach (var element in additions)
            {
                switch (element.guid)
                {
                    //Районный коэф.
                    case "66f08438-f006-44e8-b9ee-32a8dcf557ba":
                        element.IsEditable = false;
                        break;
                    //Оклад
                    case "35c7a5dd-d8e9-4aa0-8378-2a7e501d846a":
                        element.IsEditable = false;
                        element.IsVisible = (role & ShowAllToRoles) > 0;
                        break;
                    //Оклад
                    case "537ff7ed-5e51-48d1-bf5e-4f680cb3e1b7":
                        element.IsEditable = false;
                        element.IsVisible = (role & ShowAllToRoles) > 0;
                        break;
                    //Северная автомат
                    case "1f076cf3-1ebb-11e4-80c8-002590d1e727":
                        element.IsValueEditable = false;
                        element.IsEditable = (role & UserRole.PersonnelManager) > 0 && isEditable;
                        element.IsVisible = (role & ShowAllToRoles) > 0;
                        break;
                    //Северная руч.
                    case "a5ceb324-a745-11de-b733-003048359abd":
                        element.IsValueEditable = false;
                        element.IsEditable = (role & UserRole.PersonnelManager) > 0 && isEditable;
                        element.IsVisible = false;//(role & ShowAllToRoles) > 0;
                        break;
                    //Отпуск по уходу за ребенком без оплаты
                    case "9e6ec242-49f2-4320-a5aa-024c5d607aa3":
                        element.IsEditable = false;
                        element.IsVisible = false;//(role & ShowAllToRoles) > 0;
                        break;
                    //Пособие по уходу за ребёнком до 1.5 лет#1502
                    case "1671e1b6-0281-489c-b191-50e6fb241e75":
                        element.IsEditable = false;
                        element.IsVisible = false;//(role & ShowAllToRoles) > 0;
                        break;
                    //Пособие по уходу за ребёнком до 3 лет#1503
                    case "db5cc88b-4080-4061-8bba-42f22b500bb4":
                        element.IsEditable = false;
                        element.IsVisible = false;//(role & ShowAllToRoles) > 0;
                        break;
                    //Доплата за совмещение
                    case "91a004fc-d13e-11dd-b086-00308d000000":
                        element.IsEditable = false;
                        element.IsVisible = false;//(role & ShowAllToRoles) > 0;
                        break;
                    default:
                        element.IsVisible = true;
                        element.IsEditable = isEditable;
                        break;
                }
            }
        }
        private bool CheckIsChief(int depId, Reports.Presenters.Services.IUser chief)
        {            
            var chiefs = DepartmentDao.GetDepartmentManagersByDepAndManualRole(depId, 4);
            if (chief == null || !chiefs.Any()) return false;
            return chiefs.Any(x => x.Id == chief.Id);
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
                if ((CurrentUser.UserRole & (UserRole.Manager | UserRole.PersonnelManager)) > 0)
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
                var sourcelink = StaffEstablishedPostUserLinksDao.QueryExpression(x => x.User.Id == entity.User.Id && x.IsUsed);
                entity.SourceStaffEstablishedPostRequest = sourcelink.First();
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
                    entity.TargetStaffEstablishedPostRequest = entity.SourceStaffEstablishedPostRequest;
                }
                //Тип заявки
                entity.Type = StaffMovementsTypesDao.Load(model.RequestType);
                //Данные заявки, создаём и сохраняем
                entity.Data = new StaffMovementsData();
                entity.Data.Grade = model.Grade;
                entity.Data.HoursType = ScheduleDao.Load(model.HoursType);
                entity.Data.Salary = model.TargetSalaryCount;
                StaffMovementsDataDao.SaveAndFlush(entity.Data);
                //Сохраняем надбавки
                SaveAdditions(entity, model);

                //Документы, создаём сразу все.
                var docs = new List<StaffMovementsDocs>();
                docs.Add(new StaffMovementsDocs { DocType = (int)StaffMovementsDocsTypes.AdditionalAgreementDoc, Request = entity });
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
                entity.TargetStaffEstablishedPostRequest = StaffEstablishedPostUserLinksDao.Load(model.UserLinkId);
                entity.TargetPosition = entity.TargetStaffEstablishedPostRequest.StaffEstablishedPost.Position;
            }
            #endregion
            #region Общее
            if (model.StatusId <= 1 || model.IsManagerEditable || model.IsPersonnelManagerEditable || model.IsPersonnelManagerBankAcceptAvailable)
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
                entity.Data.PyrusLink = model.PyrusLink;
                entity.Data.Salary = model.TargetSalaryCount;
                entity.Data.Grade = model.Grade;//Грейд
                entity.Data.HoursType = ScheduleDao.Load(model.HoursType);//График работы
                entity.Data.TargetCasingType = model.TargetCasingType;
                SaveAdditions(entity, model);
            }
            #endregion
            #region Для кадров
            if (model.IsPersonnelManagerEditable)
            {
                entity.Data.Grade = model.Grade;//Грейд
                entity.Data.HoursType = ScheduleDao.Load(model.HoursType);//График работы
                entity.Data.TargetCasingType = model.TargetCasingType;
                entity.Data.Salary = model.TargetSalaryCount;
                //entity.Data.AccessGroup = AccessGroupDao.Load(model.AccessGroup);//Группа доступа
                //Ставим галочки в документах
                if (model.IsDocsEditable)
                {
                    StaffMovementsDocsDao.Update(x => x.Request.Id == entity.Id && x.DocType == (int)StaffMovementsDocsTypes.MovementNote, y => y.IsRequired = model.MovementNoteIsRequired);
                    
                }
                //Сохраняем надбавки
                SaveAdditions(entity, model);
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
                        if (entity.TargetStaffEstablishedPostRequest.ReserveType != null && entity.TargetStaffEstablishedPostRequest.DocId == entity.Id)
                        {
                            entity.TargetStaffEstablishedPostRequest.ReserveType = null;
                            entity.TargetStaffEstablishedPostRequest.DocId = null;
                        }
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
                        if (entity.SourceManager != null && entity.SourceManager.Id != CurrentUser.Id)
                        {
                            //DocumentApprovalDao
                            DocumentApproval newapprove = new DocumentApproval();
                            newapprove.ApprovalType = (int)ApprovalTypeEnum.StaffMovements;
                            newapprove.ApproveUser = entity.SourceManager;
                            newapprove.AssistantUser = UserDao.Load(CurrentUser.Id);
                            newapprove.CreateDate = DateTime.Now;
                            newapprove.DocId = entity.Id;
                            newapprove.Number = 1;
                            DocumentApprovalDao.SaveAndFlush(newapprove);
                        }
                    }
                    if (model.IsAcceptButtonPressed && model.IsTargetManagerAcceptAvailable)
                    {
                        //Если согласовано принимающим руководителем
                        entity.SendDate = DateTime.Now;
                        entity.SourceManagerAccept = DateTime.Now;
                        entity.TargetManagerAccept = DateTime.Now;
                        //entity.Status = StaffMovementsStatusDao.Load((int)StaffMovementsStatus.PersonelManagerBank);
                        entity.Status = StaffMovementsStatusDao.Load((int)StaffMovementsStatus.Chief);//После принимающего идёт вышестоящий
                        if (entity.TargetManager != null && entity.TargetManager.Id != CurrentUser.Id)
                        {
                            //DocumentApprovalDao
                            DocumentApproval newapprove = new DocumentApproval();
                            newapprove.ApprovalType = (int)ApprovalTypeEnum.StaffMovements;
                            newapprove.ApproveUser = entity.TargetManager;
                            newapprove.AssistantUser = UserDao.Load(CurrentUser.Id);
                            newapprove.CreateDate = DateTime.Now;
                            newapprove.DocId = entity.Id;
                            newapprove.Number = 2;
                            DocumentApprovalDao.SaveAndFlush(newapprove);
                        }
                        entity.TargetStaffEstablishedPostRequest.DocId = entity.Id;
                        entity.TargetStaffEstablishedPostRequest.ReserveType = (int)StaffReserveTypeEnum.StaffMovements;
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
                        if (entity.TargetStaffEstablishedPostRequest.ReserveType != null && entity.TargetStaffEstablishedPostRequest.DocId == entity.Id)
                        {
                            entity.TargetStaffEstablishedPostRequest.ReserveType = null;
                            entity.TargetStaffEstablishedPostRequest.DocId = null;
                        }
                    }
                    if (model.IsAcceptButtonPressed && model.IsSourceManagerAcceptAvailable)
                    {
                        //Если согласовано отпускающим руководителем
                        if (entity.SourceManager != null && entity.SourceManager.Id != CurrentUser.Id)
                        {
                            //DocumentApprovalDao
                            DocumentApproval newapprove = new DocumentApproval();
                            newapprove.ApprovalType =(int) ApprovalTypeEnum.StaffMovements;
                            newapprove.ApproveUser = entity.SourceManager;
                            newapprove.AssistantUser = UserDao.Load(CurrentUser.Id);
                            newapprove.CreateDate = DateTime.Now;
                            newapprove.DocId = entity.Id;
                            newapprove.Number = 1;
                            DocumentApprovalDao.SaveAndFlush(newapprove);
                        }
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
                        if (entity.TargetStaffEstablishedPostRequest.ReserveType != null && entity.TargetStaffEstablishedPostRequest.DocId == entity.Id)
                        {
                            entity.TargetStaffEstablishedPostRequest.ReserveType = null;
                            entity.TargetStaffEstablishedPostRequest.DocId = null;
                        }
                    }
                    if (model.IsAcceptButtonPressed && model.IsTargetManagerAcceptAvailable)
                    {
                        //Если согласовано принимающим руководителем, в таком случае нужну забронировать вакансию
                        if (entity.TargetManager != null && entity.TargetManager.Id != CurrentUser.Id)
                        {
                            //DocumentApprovalDao
                            DocumentApproval newapprove = new DocumentApproval();
                            newapprove.ApprovalType = (int)ApprovalTypeEnum.StaffMovements;
                            newapprove.ApproveUser = entity.TargetManager;
                            newapprove.AssistantUser = UserDao.Load(CurrentUser.Id);
                            newapprove.CreateDate = DateTime.Now;
                            newapprove.DocId = entity.Id;
                            newapprove.Number = 2;
                            DocumentApprovalDao.SaveAndFlush(newapprove);
                        }
                        entity.TargetManagerAccept = DateTime.Now;
                        entity.Status = StaffMovementsStatusDao.Load((int)StaffMovementsStatus.Chief);//После принимающего идёт вышестоящий
                        //entity.Status = StaffMovementsStatusDao.Load((int)StaffMovementsStatus.PersonelManagerBank);
                        entity.TargetStaffEstablishedPostRequest.DocId = entity.Id;
                        entity.TargetStaffEstablishedPostRequest.ReserveType = (int)StaffReserveTypeEnum.StaffMovements;
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
                        if (entity.TargetStaffEstablishedPostRequest.ReserveType != null && entity.TargetStaffEstablishedPostRequest.DocId == entity.Id)
                        {
                            entity.TargetStaffEstablishedPostRequest.ReserveType = null;
                            entity.TargetStaffEstablishedPostRequest.DocId = null;
                        }
                    }
                    if (model.IsPersonnelManagerBankAcceptAvailable && model.IsAcceptButtonPressed)
                    {
                        //Если согласовано кадровиком банка
                        entity.PersonnelManagerBank = UserDao.Load(CurrentUser.Id);
                        entity.PersonnelManagerBankAccept = DateTime.Now;
                        //entity.Status = StaffMovementsStatusDao.Load((int)StaffMovementsStatus.Chief);
                        entity.Status = StaffMovementsStatusDao.Load((int)StaffMovementsStatus.Approved);//перевод оформлен
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
                        if (entity.TargetStaffEstablishedPostRequest.ReserveType != null && entity.TargetStaffEstablishedPostRequest.DocId == entity.Id)
                        {
                            entity.TargetStaffEstablishedPostRequest.ReserveType = null;
                            entity.TargetStaffEstablishedPostRequest.DocId = null;
                        }
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
                        if (entity.TargetStaffEstablishedPostRequest.ReserveType != null && entity.TargetStaffEstablishedPostRequest.DocId == entity.Id)
                        {
                            entity.TargetStaffEstablishedPostRequest.ReserveType = null;
                            entity.TargetStaffEstablishedPostRequest.DocId = null;
                        }
                    }
                    if (model.IsPersonnelManagerAcceptAvailable && model.IsAcceptButtonPressed)
                    {
                        //Если согласовано кадровиком
                        entity.PersonnelManager = UserDao.Load(CurrentUser.Id);
                        entity.PersonnelManagerAccept = DateTime.Now;
                        //entity.Status = StaffMovementsStatusDao.Load((int)StaffMovementsStatus.Approved);
                        var check = StaffMovementsDao.CheckIfPersonnalChargeChanges(entity.Id);

                        entity.Status = check ? StaffMovementsStatusDao.Load((int)StaffMovementsStatus.PersonelManagerBank) //После кадровика идёт кадровик банк
                            : StaffMovementsStatusDao.Load((int)StaffMovementsStatus.Approved); //Если нет изменений надбавок, то перевод оформлен
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
                        if (entity.TargetStaffEstablishedPostRequest.ReserveType != null && entity.TargetStaffEstablishedPostRequest.DocId == entity.Id)
                        {
                            entity.TargetStaffEstablishedPostRequest.ReserveType = null;
                            entity.TargetStaffEstablishedPostRequest.DocId = null;
                        }
                    }
                    //Проверяем все документы, если у обязательного документа нет вложения - не выходим из этого статуса
                    bool accept = true;
                    foreach (var doc in entity.Docs)
                    {
                        if (doc.IsRequired && doc.Attachment == null) accept = false;
                    }
                    if (accept) entity.Status = StaffMovementsStatusDao.Load((int)StaffMovementsStatus.DocsApproved);
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
                        if (entity.TargetStaffEstablishedPostRequest.ReserveType != null && entity.TargetStaffEstablishedPostRequest.DocId == entity.Id)
                        {
                            entity.TargetStaffEstablishedPostRequest.ReserveType = null;
                            entity.TargetStaffEstablishedPostRequest.DocId = null;
                        }
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
        /// Сохранение надвбавок
        /// </summary>
        /// <param name="entity">сущность</param>
        /// <param name="model">модель</param>
        private void SaveAdditions(StaffMovements entity, StaffMovementsEditModel model)
        {
            foreach (var addition in model.AdditionsToEdit)
            {
                if (!entity.Additions.Any(x => x.ExtraCharges != null && x.ExtraCharges.Id == addition.Type.Id))
                {
                    entity.Additions.Add(new StaffPostChargeLinks
                    {
                        CreateDate = DateTime.Now,
                        Creator = UserDao.Load(CurrentUser.Id),
                        EditDate = DateTime.Now,
                        Editor = UserDao.Load(CurrentUser.Id),
                        ExtraChargeActions = StaffExtraChargeActionsDao.Load(addition.Action),
                        ExtraCharges = StaffExtraChargesDao.Load(addition.Type.Id),
                        IsActive = false,
                        Salary = addition.Value,
                        Staff = UserDao.Load(model.User.Id),
                        StaffMovements = entity
                    });
                }
                else
                {
                    var ad = entity.Additions.Where(x => x.ExtraCharges.Id == addition.Type.Id).First();
                    ad.Salary = addition.Value;
                    ad.ExtraChargeActions = StaffExtraChargeActionsDao.Load(addition.Action);
                }
            }
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
            //entity.TargetStaffEstablishedPostRequest.re
            if (entity.TargetStaffEstablishedPostRequest.ReserveType != null && entity.TargetStaffEstablishedPostRequest.DocId == entity.Id)
            {
                entity.TargetStaffEstablishedPostRequest.ReserveType = null;
                entity.TargetStaffEstablishedPostRequest.DocId = null;
            }
            try
            {
                DocumentApprovalDao.Update(x => x.DocId == entity.Id && x.ApprovalType == (int)ApprovalTypeEnum.StaffMovements, y => y.IsArchive = true);
            }
            catch (Exception) { }
            entity.PersonnelManagerAccept = null;
        }
        /// <summary>
        /// Получение оклада и районного коэф.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="Salary"></param>
        /// <param name="Region"></param>
        private void GetMoneyForStaffEstablishedPostUserLinks(StaffEstablishedPostUserLinks request, StaffMovementsEditModel model)
        {
            model.TargetCasing = request.StaffEstablishedPost.Salary;
            //var charges = request.StaffEstablishedPost.PostChargeLinks.Where(x => x.ExtraCharges != null && x.ExtraCharges.Id == 6).ToList();
            var postReq = request.StaffEstablishedPost.EstablishedPostRequest.Where(x => x.IsUsed && x.PostChargeLinks!=null);
            if (postReq != null && postReq.Any())
            {
                var charges = postReq.First().PostChargeLinks.Where(x => x.ExtraCharges != null && x.ExtraCharges.Id == 6).ToList();
                if (charges != null && charges.Any())
                {
                    var charge = charges.First();
                    model.TargetRegion = charge.Amount.HasValue ? charge.Amount.Value : 0;
                }
            }
            else
            {
                model.TargetRegion = 0;
            }
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

        public StaffMovementsPrintModel GetPrintModel(int id, int SignerId=0)
        {
            var factentity = StaffMovementsFactDao.Load(id);
            //var entity = factentity.StaffMovements;
            StaffMovementsPrintModel model = new StaffMovementsPrintModel();
            
            #region Персонажи
            model.UserName = factentity.User.Name;
            //model.TargetPosition = entity.TargetPosition.Name;
            //model.SourcePosition = entity.SourcePosition.Name;            
            //model.Chief = entity.TargetChief!=null?entity.TargetChief.Name:"";
           // model.ChiefDepartment = entity.TargetChief != null && entity.TargetChief.Department!=null? entity.TargetChief.Department.Name:"";
           // model.ChiefPosition = entity.TargetChief != null && entity.TargetChief.Position!=null? entity.TargetChief.Position.Name:"";
           // model.TargetManager = entity.TargetManager.Name;
           // model.SourceManager = entity.SourceManager.Name;
            if(SignerId>0)
            {
                var signer = EmploymentSignersDao.Load(SignerId);
                model.SignerName = signer != null ? signer.Name : "";
                model.SignerPosition = signer != null ? signer.Position : "";
                model.SignerAdditionData = signer != null ? signer.PreamblePartyTemplate : "";
            }
           // model.PersonnelManagerBank = entity.PersonnelManagerBank!=null?entity.PersonnelManagerBank.Name:"";
           // model.PersonnelManagerBankDateAccept = entity.PersonnelManagerBankAccept.HasValue? entity.PersonnelManagerBankAccept.Value.ToString("dd.MM.yyyy"):"";
            #endregion
            /*
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
            #endregion            */
            //model.HoursType = entity.Data.HoursType!=null? entity.Data.HoursType.Name:"";
            return model;
        }
        
        #region DEPRECATED CODE TO DELETE
        //Deprecated Временно не печатаем документы отсюда
        /*}*/
        /// <summary>
        /// Сохранение модели документов. DEPRECATED
        /// </summary>
        /// <param name="model"></param>
        public void SaveDocsModel(StaffMovementsEditModel model)
        {
            
            StaffMovementsDocsDao.Update(x => x.Request.Id == model.Id && x.DocType == (int)StaffMovementsDocsTypes.MovementNote, y => y.IsRequired = model.MovementNoteIsRequired);
        }
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
        #endregion
    }
}

