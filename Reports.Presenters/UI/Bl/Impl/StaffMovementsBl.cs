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
        protected IEmploymentHoursTypeDao employmentHoursTypeDao;
        public IEmploymentHoursTypeDao EmploymentHoursTypeDao
        {
            get { return Validate.Dependency(employmentHoursTypeDao); }
            set { employmentHoursTypeDao = value; }
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
            model.Statuses = StaffMovementsStatusDao.LoadAll().Select(x => new IdNameDto { Id = x.Id, Name = x.Name }).ToList();
            return model;
        }
        public IList<StaffMovementsDto> GetDocuments(int DepartmentId, string UserName, int Number, int Status)
        {
            var docs = StaffMovementsDao.GetDocuments(DepartmentId, UserName, Number, Status);
            int iterator = 1;
            if (docs != null && docs.Any())
                return docs.Select(x => new StaffMovementsDto
                {
                    CreateDate = x.CreateDate,
                    Creator = x.Creator!=null?x.Creator.Name:"",
                    Dep3Name = x.SourceDepartment!=null?((x.SourceDepartment.Dep3 != null && x.SourceDepartment.Dep3.Any()) ? x.SourceDepartment.Dep3.First().Name : ""):"",
                    Dep7Name = x.SourceDepartment!=null?x.SourceDepartment.Name:"",
                    TargetManagerName = x.TargetManager!=null?x.TargetManager.Name:"",
                    SourceMangerName = x.SourceManager!=null?x.TargetManager.Name:"",
                    Status =x.Status!=null? x.Status.Name:"",
                    MoveDate = x.MovementDate,
                    NPP = iterator++,
                    Number = x.Id,
                    Position = x.User != null ? (x.User.Position!=null?x.User.Position.Name:"") : "",
                    UserName = x.User!=null?x.User.Name:"",
                    PositionCurrent = x.SourcePosition!=null?(x.SourcePosition.Position!=null?x.SourcePosition.Position.Name:""):"",
                    PositionTarget = x.TargetPosition != null ? (x.TargetPosition.Position != null ? x.TargetPosition.Position.Name : "") : "",
                    TargetDep7Name = x.TargetDepartment!=null?x.TargetDepartment.Name:"",
                    TargetDep3Name = x.TargetDepartment!=null?((x.TargetDepartment.Dep3 != null && x.TargetDepartment.Dep3.Any()) ? x.TargetDepartment.Dep3.First().Name : ""):""
                }).ToList();
            else return new List<StaffMovementsDto>();
        }
        public void SetModel(StaffMovementsEditModel model)
        {
            if (model.Id > 0)
            {
                var entity = StaffMovementsDao.Load(model.Id);
                #region Стандартные поля заявки
                model.Creator.Id = entity.Creator.Id;
                model.User.Id = entity.User.Id;
                model.RequestType = entity.Type.Id;
                model.MovementDate = entity.MovementDate;
                model.CreateDate = entity.CreateDate;
                var targetposition = entity.TargetPosition;
                if (targetposition != null)
                {
                    model.TargetPositionId = targetposition.Id;
                    model.TargetPositionName = targetposition.Position.Name;
                    model.TargetSalary = targetposition.Salary;
                }

                #endregion
                #region Данные о переводе
                model.MovementDate = entity.MovementDate;
                model.TargetPositionId = entity.TargetPosition.Id;
                model.TargetPositions = new List<IdNameDto>();
                model.TargetPositions.Add(new IdNameDto {  Id= model.TargetPositionId, Name=model.TargetPositionName});
                model.TargetPositionName = entity.TargetPosition.Position.Name;
                model.TargetDepartmentId = entity.TargetDepartment.Id;
                model.TargetDepartmentName = entity.TargetDepartment.Name;
                model.TargetManager = new StandartUserDto { Id = entity.TargetManager.Id };
                LoadUserData(model.TargetManager);
                model.SourceManager = new StandartUserDto { Id = entity.TargetManager.Id };
                LoadUserData(model.SourceManager);
                #endregion
                #region Для руководителей
                model.MovementCondition = entity.Data.MovementCondition;
                model.AdditionPersonnel = entity.Data.AdditionPersonnel;
                model.AdditionPersonnelTo = entity.Data.AdditionPersonnelTo;
                model.AdditionPosition = entity.Data.AdditionPosition;
                model.AdditionPositionTo = entity.Data.AdditionPositionTo;
                model.AdditionQuality = entity.Data.AdditionQuality;
                model.AdditionQualityTo = entity.Data.AdditionQualityTo;
                #endregion
                #region Для бухгалтеров
                model.OrderDate = entity.OrderDate;
                model.AdditionalAgreementDate = entity.Data.AdditionalAgreementDate;
                model.AdditionalAgreementNumber = entity.Data.AdditionalAgreementNumber;
                model.IsHourly = entity.Data.SalaryType == 1;
                model.RegionCoefficient = entity.Data.RegionCoefficient;
                model.AdditionTerritory = entity.Data.AdditionTerritory;
                model.AdditionTraveling = entity.Data.AdditionTraveling;
                model.AdditionFront = entity.Data.AdditionFront;
                model.AdditionFrontTo = entity.Data.AdditionFrontTo;
                model.Grade = entity.Data.Grade;
                model.HoursType = entity.Data.HoursType.Id;
                model.NorthFactor = entity.Data.NorthFactor;
                model.AdditionalAgreementEnties = entity.Data.AdditionalAgreementEntries;
                model.AgreementDate = entity.Data.AgreementDate;
                model.ChangesToAgreement = entity.Data.ChangesToAgreement;
                model.ChangesToAgreementEnties = entity.Data.ChangesToAgreementEntries;
                model.MovementReason = entity.Data.MovementReason;
                model.AccessGroup = entity.Data.AccessGroup.Id;
                model.SignatoryId = entity.Data.Signatory.Id;
                model.SignatoryName = entity.Data.Signatory.Name;
                #endregion
                #region Files
                var docs = entity.Docs;
                if (docs != null && docs.Any())
                {
                    var AdditionalAgreementDoc = docs.Where(x => x.DocType == (int)StaffMovementsDocsTypes.AdditionalAgreementDoc).First();
                    model.AdditionalAgreementDocDto = new UploadFileDto();
                    if (AdditionalAgreementDoc != null && AdditionalAgreementDoc.Attachment!=null)
                    {                        
                        model.AdditionalAgreementDocDto.FileName = AdditionalAgreementDoc.Attachment.FileName;
                        model.AdditionalAgreementDocAttachmentId = AdditionalAgreementDoc.Attachment.Id;
                    }
                    var MaterialLiabilityDoc = docs.Where(x => x.DocType == (int)StaffMovementsDocsTypes.MaterialLiabilityDoc).First();
                    model.MaterialLiabilityDocDto = new UploadFileDto();
                    if (MaterialLiabilityDoc != null && MaterialLiabilityDoc.Attachment != null)
                    {
                        model.MaterialLiabilityDocDto.FileName = MaterialLiabilityDoc.Attachment.FileName;
                        model.MaterialLiabilityDocAttachmentId = MaterialLiabilityDoc.Attachment.Id;
                    }
                    var MovementNote = docs.Where(x => x.DocType == (int)StaffMovementsDocsTypes.MovementNote).First();
                    model.MovementNoteDto = new UploadFileDto();
                    if (MovementNote != null && MovementNote.Attachment != null)
                    {
                        model.MovementNoteDto.FileName = MovementNote.Attachment.FileName;
                        model.MovementNoteAttachmentId = MovementNote.Attachment.Id;
                    }
                    var MovementOrderDoc = docs.Where(x => x.DocType == (int)StaffMovementsDocsTypes.MovementOrderDoc).First();
                    model.MovementOrderDocDto = new UploadFileDto();
                    if (MovementOrderDoc != null && MovementOrderDoc.Attachment != null)
                    {
                        model.MovementOrderDocDto.FileName = MovementOrderDoc.Attachment.FileName;
                        model.MovementOrderDocAttachmentId = MovementOrderDoc.Attachment.Id;
                    }
                    var RequirementsOrderDoc = docs.Where(x => x.DocType == (int)StaffMovementsDocsTypes.RequirementsOrderDoc).First();
                    model.RequirementsOrderDocDto = new UploadFileDto();
                    if (RequirementsOrderDoc != null && RequirementsOrderDoc.Attachment != null)
                    {
                        model.RequirementsOrderDocDto.FileName = RequirementsOrderDoc.Attachment.FileName;
                        model.RequirementsOrderDocAttachmentId = RequirementsOrderDoc.Attachment.Id;
                    }
                    var ServiceOrderDoc = docs.Where(x => x.DocType == (int)StaffMovementsDocsTypes.ServiceOrderDoc).First();
                    model.ServiceOrderDocDto = new UploadFileDto();
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
                model.Creator = new StandartUserDto();
                model.Creator.Id = CurrentUser.Id;
                model.CreateDate = DateTime.Now;
                model.IsEditable = true;
                LoadUserData(model.Creator);
                return;
            }            
            LoadUserData(model.Creator);
        }
        public void LoadDictionaries(StaffMovementsEditModel model)
        {
            var HoursTypes = EmploymentHoursTypeDao.LoadAll();
            if(HoursTypes!=null && HoursTypes.Any())
            {
                model.HoursTypes = HoursTypes.Select(x => new IdNameDto { Id=x.Id,Name=x.Name}).ToList();
            }
            var AccessGroups = AccessGroupDao.LoadAll();
            if(AccessGroups!=null && AccessGroups.Any())
            {
                model.AccessGroupsList = AccessGroups.Select(x => new IdNameDto { Id = x.Id, Name = x.Name }).ToList();
            }
            var SignatoryList= EmploymentSignersDao.LoadAll();
            if(SignatoryList.Any())
            {
                model.SignatoryList = SignatoryList.Select(x => new IdNameDto { Id = x.Id, Name = x.Name }).ToList();
            }
            var RequestTypes = StaffMovementsTypesDao.LoadAll();
            if (RequestTypes != null && RequestTypes.Any())
            {
                model.RequestTypes = RequestTypes.Select(x => new IdNameDto { Id = x.Id, Name = x.Name }).ToList();
            }
            var NorthFactors = GetNorthExperienceTypes();
        }
        public void SaveModel(StaffMovementsEditModel model)
        {
            StaffMovements entity;
            if (model.Id == 0)
            {
                entity = new StaffMovements();
            }
            else
            {
                entity = StaffMovementsDao.Load(model.Id);
            }
            ChangeEntityProperties(entity, model);
            StaffMovementsDao.SaveAndFlush(entity);
            #region файлы
            if (entity.Docs == null || !entity.Docs.Any())
            {
                for(int i=1;i<=6;i++)
                {
                    //Создаём все документы сразу
                    StaffMovementsDocs doc = new StaffMovementsDocs { Request = entity, DocType = i };
                    StaffMovementsDocsDao.SaveAndFlush(doc);
                }
            }
            SaveFiles(model);
            #endregion
        }
        private void SaveFiles(StaffMovementsEditModel model)
        {
            var entity = StaffMovementsDao.Load(model.Id);
            var docs = entity.Docs;
            #region файлики
            var tmp = "";
            var AdditionalAgreementDoc= docs.Where(x => x.DocType == (int)StaffMovementsDocsTypes.AdditionalAgreementDoc).First();
            SaveAttachment(AdditionalAgreementDoc.Id, AdditionalAgreementDoc.Attachment != null ? AdditionalAgreementDoc.Attachment.Id : 0, model.AdditionalAgreementDocDto, RequestAttachmentTypeEnum.StaffMovements,out tmp);
            var MaterialLiabilityDoc = docs.Where(x => x.DocType == (int)StaffMovementsDocsTypes.MaterialLiabilityDoc).First();
            SaveAttachment(MaterialLiabilityDoc.Id, MaterialLiabilityDoc.Attachment != null ? MaterialLiabilityDoc.Attachment.Id : 0, model.MaterialLiabilityDocDto, RequestAttachmentTypeEnum.StaffMovements, out tmp);
            var MovementNote = docs.Where(x => x.DocType == (int)StaffMovementsDocsTypes.MovementNote).First();
            SaveAttachment(MovementNote.Id, MovementNote.Attachment != null ? MovementNote.Attachment.Id : 0, model.MovementNoteDto, RequestAttachmentTypeEnum.StaffMovements, out tmp);
            var MovementOrderDoc = docs.Where(x => x.DocType == (int)StaffMovementsDocsTypes.MovementOrderDoc).First();
            SaveAttachment(MovementOrderDoc.Id, MovementOrderDoc.Attachment != null ? MovementOrderDoc.Attachment.Id : 0, model.MovementOrderDocDto, RequestAttachmentTypeEnum.StaffMovements, out tmp);
            var RequirementsOrderDoc = docs.Where(x => x.DocType == (int)StaffMovementsDocsTypes.RequirementsOrderDoc).First();
            SaveAttachment(RequirementsOrderDoc.Id, RequirementsOrderDoc.Attachment != null ? RequirementsOrderDoc.Attachment.Id : 0, model.RequirementsOrderDocDto, RequestAttachmentTypeEnum.StaffMovements, out tmp);
            var ServiceOrderDoc = docs.Where(x => x.DocType == (int)StaffMovementsDocsTypes.ServiceOrderDoc).First();
            SaveAttachment(ServiceOrderDoc.Id, ServiceOrderDoc.Attachment != null ? ServiceOrderDoc.Attachment.Id : 0, model.ServiceOrderDocDto, RequestAttachmentTypeEnum.StaffMovements, out tmp);
            #endregion
        }

        private void ChangeEntityProperties(StaffMovements entity, StaffMovementsEditModel model)
        {
            ChangeEntityStatus(entity, model);
            #region Стандартные поля заявки
            if (model.Id == 0)
            {
                entity.CreateDate = model.CreateDate;
                entity.Creator = UserDao.Load(model.Creator.Id);
                entity.Data = new StaffMovementsData();
                StaffMovementsDataDao.SaveAndFlush(entity.Data);
                var docs=new List<StaffMovementsDocs>();
                docs.Add(new StaffMovementsDocs { DocType = (int)StaffMovementsDocsTypes.AdditionalAgreementDoc, Request=entity });
                docs.Add(new StaffMovementsDocs { DocType = (int)StaffMovementsDocsTypes.MaterialLiabilityDoc, Request = entity });
                docs.Add(new StaffMovementsDocs { DocType = (int)StaffMovementsDocsTypes.MovementNote, Request = entity });
                docs.Add(new StaffMovementsDocs { DocType = (int)StaffMovementsDocsTypes.MovementOrderDoc, Request = entity });
                docs.Add(new StaffMovementsDocs { DocType = (int)StaffMovementsDocsTypes.RequirementsOrderDoc, Request = entity });
                docs.Add(new StaffMovementsDocs { DocType = (int)StaffMovementsDocsTypes.ServiceOrderDoc, Request = entity });
                entity.Docs = docs;                
            }                      
            entity.User = UserDao.Load(model.User.Id);
            entity.Type = StaffMovementsTypesDao.Load(model.RequestType);
            #endregion
            #region Данные о переводе
            entity.MovementDate = model.MovementDate;
            entity.OrderDate = model.OrderDate;
            entity.SourcePosition = StaffEstablishedPostRequestDao.Get(model.User.StaffEstablishedPostId);
            entity.SourceDepartment = entity.SourcePosition.Department;
            entity.TargetPosition = StaffEstablishedPostRequestDao.Load(model.TargetPositionId);
            entity.TargetDepartment = entity.TargetPosition.Department;
            entity.SourceManager = GetManagerForDepartment(entity.SourceDepartment);
            entity.TargetManager = GetManagerForDepartment(entity.TargetDepartment);
            #endregion
            #region Для руководителей
            entity.Data.MovementCondition = model.MovementCondition;
            entity.Data.AdditionPersonnel = model.AdditionPersonnel;
            entity.Data.AdditionPersonnelTo = model.AdditionPersonnelTo;
            entity.Data.AdditionPosition = model.AdditionPosition;
            entity.Data.AdditionPositionTo = model.AdditionPositionTo;
            entity.Data.AdditionQuality = model.AdditionQuality;
            entity.Data.AdditionQualityTo = model.AdditionQualityTo;
            #endregion
            #region Для бухгалтеров
            entity.Data.AdditionalAgreementDate = model.AdditionalAgreementDate;
            entity.Data.AdditionalAgreementNumber = model.AdditionalAgreementNumber;
            entity.OrderDate = model.OrderDate;
            entity.Data.SalaryType = model.IsHourly ? 1 : 0;
            entity.Data.RegionCoefficient = model.RegionCoefficient;
            entity.Data.AdditionTerritory = model.AdditionTerritory;
            entity.Data.AdditionTraveling = model.AdditionTraveling;
            entity.Data.AdditionFront = model.AdditionFront;
            entity.Data.AdditionFrontTo = model.AdditionFrontTo;
            entity.Data.Grade = model.Grade;
            entity.Data.HoursType = EmploymentHoursTypeDao.Load(model.HoursType);
            entity.Data.NorthFactor = model.NorthFactor;
            entity.Data.AdditionalAgreementEntries = model.AdditionalAgreementEnties;
            entity.Data.AgreementDate = model.AgreementDate;
            entity.Data.ChangesToAgreement = model.ChangesToAgreement;
            entity.Data.ChangesToAgreementEntries = model.ChangesToAgreementEnties;
            entity.Data.MovementReason = model.MovementReason;
            entity.Data.AccessGroup = AccessGroupDao.Load(model.AccessGroup);
            entity.Data.Signatory = EmploymentSignersDao.Load(model.SignatoryId);
            #endregion
        }
        private void ChangeEntityStatus(StaffMovements entity, StaffMovementsEditModel model)
        {
            entity.Status = StaffMovementsStatusDao.Load(1);
        }
        public IList<IdNameDto> GetPositionsForDepartment(int id)
        {
            var positions=StaffEstablishedPostRequestDao.Find(x => x.Department.Id == id);
            if (positions != null && positions.Any())
            {
                return positions.Select(x => new IdNameDto { Id = x.Id, Name = x.Position.Name }).ToList();
            }
            else return new List<IdNameDto>();                 
        }
        #endregion
        #region Files
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
        public bool DeleteAttachment(DeleteAttacmentModel model)
        {
            RequestAttachmentDao.Delete(model.Id);
            return true;
        }

        #endregion
        #region Dictionaries
        public IList<IdNameDto> GetNorthExperienceTypes()
        {
            IList<IdNameDto> inDto = new List<IdNameDto> { };

            inDto.Add(new IdNameDto { Id = 1, Name = "Сотруднику не полагается северная надбавка" });
            inDto.Add(new IdNameDto { Id = 2, Name = "Северный стаж сотрудника отсутствует, начать начисление стажа с даты приема" });
            inDto.Add(new IdNameDto { Id = 3, Name = "Северный стаж у сотрудника имеется, указать количество северного стажа" });

            return inDto;
        }
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
    }
}
