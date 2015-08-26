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
                #endregion
                #region Для руководителей
                #endregion
                #region Для бухгалтеров
                model.OrderDate = entity.OrderDate;
                #endregion
            }
            LoadUserData(model.User);
            LoadDictionaries(model);
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
            entity.SourcePosition = StaffEstablishedPostDao.Get(model.User.StaffEstablishedPostId);
            entity.SourceDepartment = entity.SourcePosition.Department;
            entity.TargetPosition = StaffEstablishedPostRequestDao.Load(model.TargetPositionId);
            entity.TargetDepartment = entity.TargetPosition.Department;
            #endregion
            #region Для руководителей
            #endregion
            #region Для бухгалтеров
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
            else return null;                 
        }
        #endregion
    }
}
