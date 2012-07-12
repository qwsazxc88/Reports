using System;
using System.Collections.Generic;
using System.Linq;
using Reports.Core;
using Reports.Core.Dao;
using Reports.Core.Domain;
using Reports.Core.Dto;
using Reports.Presenters.UI.ViewModel;

namespace Reports.Presenters.UI.Bl.Impl
{
    public class RequestBl : BaseBl, IRequestBl
    {
        protected string SelectAll = "Все";

        protected IDepartmentDao departmentDao;
        protected IVacationTypeDao vacationTypeDao;
        protected IRequestStatusDao requestStatusDao;
        protected IPositionDao positionDao;
        protected IVacationDao vacationDao;
        protected IUserToDepartmentDao userToDepartmentDao;
        protected ITimesheetStatusDao timesheetStatusDao;
        protected IVacationCommentDao vacationCommentDao;

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

        public CreateRequestModel GetCreateRequestModel(int? userId)
        {
            UserRole role;
            if(userId == null)
            {
                userId = AuthenticationService.CurrentUser.Id;
                role = AuthenticationService.CurrentUser.UserRole;
            }
            else
            {
                User user = UserDao.Load(userId.Value);
                role = (UserRole)user.Role.Id;
            }

            CreateRequestModel model = new CreateRequestModel
                                           {
                                               IsUserVisible = role != UserRole.Employee
                                           };
            //model.RequestStatuses =  GetRequestTypes();
            return model;
        }

        #region Vacation list model
        public VacationListModel GetVacationListModel()
        {
            User user = UserDao.Load(AuthenticationService.CurrentUser.Id);
            VacationListModel model = new VacationListModel
                                          {
                                              UserId = AuthenticationService.CurrentUser.Id,
                                              Departments = GetDepartments(user),
                                              VacationTypes = GetVacationTypes(),
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
            model.VacationTypes = GetVacationTypes();
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
        public List<IdNameDto> GetVacationTypes()
        {
            var vacationTypeList = VacationTypeDao.LoadAllSorted().ToList().ConvertAll(x => new IdNameDto(x.Id, x.Name));
            vacationTypeList.Insert(0,new IdNameDto(0,SelectAll));
            return vacationTypeList;
        }
        public List<IdNameDto> GetRequestStatuses()
        {
            var requestStatusesList = RequestStatusDao.LoadAllSorted().ToList().ConvertAll(x => new IdNameDto(x.Id, x.Name));
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
            SetUserInfoModel(user,model);
            model.CommentsModel = GetCommentsModel(id, 1);
            model.TimesheetStatuses = GetTimesheetStatusesForVacation();
            model.VacationTypes = GetVacationTypes();
            SetFlagsState(id,user,model);
            if(id == 0)
            {
                model.CreatorLogin = user.Login;
                model.Version = 0;
            }
            else
            {
                
            }
            return model;
        }
        protected void SetFlagsState(int id,User user,VacationEditModel model)
        {
            if(id == 0)
            {
                model.IsApprovedByManager = false;
                model.IsApprovedByManagerHidden = false;
                model.IsApprovedByManagerEnable = false;
                
                model.IsApprovedByPersonnelManager = false;
                model.IsApprovedByPersonnelManagerHidden = false;
                model.IsApprovedByPersonnelManagerEnable = false;

                model.IsApprovedByUser = false;
                model.IsApprovedByUserHidden = false;
                model.IsApprovedByUserEnable = false;

                model.IsPostedTo1C = false;
                model.IsPostedTo1CHidden = false;
                model.IsPostedTo1CEnable = false;

                model.IsSaveAvailable = true;
                model.IsTimesheetStatusEditable = false;
                return;
            }
        }
        protected void SetUserInfoModel(User user,UserInfoModel model)
        {
            model.DateCreated = DateTime.Today.ToShortDateString();
            IList<IdNameDto> departments = UserToDepartmentDao.GetByUserId(user.Id);
            if (departments.Count > 0)
                model.Department = departments[0].Name;
            if(user.Manager != null)
                model.ManagerName = user.Manager.FullName;
            if (user.PersonnelManager != null)
                model.PersonnelName = user.PersonnelManager.FullName;
            model.Organization = user.Organization.Name;
            model.Position = user.Position.Name;
            model.UserName = user.FullName;
            model.UserNumber = user.Code;
        }
        protected List<IdNameDto> GetTimesheetStatusesForVacation()
        {
            List<IdNameDto> dtos = TimesheetStatusDao.LoadAllSorted().Where(x => (x.Id >= 8) && (x.Id <= 12)).ToList().ConvertAll(
                x => new IdNameDto(x.Id, x.Name));
            dtos.Insert(0,new IdNameDto(0,string.Empty));
            return dtos;
        }
        #endregion

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
                    case 1:
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
                }
            }
            return commentModel;
        }
        public bool SaveComment(SaveCommentModel model)
        {
            try
            {
                int userId = AuthenticationService.CurrentUser.Id;
                switch(model.TypeId)
                {
                    case 1:
                        Vacation vacation = VacationDao.Load(model.DocumentId);
                        User user = UserDao.Load(userId);
                        VacationComment comment = new VacationComment
                                                      {
                                                          Comment = model.Comment,
                                                          Vacation = vacation,
                                                          DateCreated = DateTime.Now,
                                                          User = user,
                                                      };
                        VacationCommentDao.MergeAndFlush(comment);
                        break;
                }
                //doc.Comments.Add(comment);
                //DocumentDao.MergeAndFlush(doc);
                return true;
            }
            catch (Exception ex)
            {
                Log.Error("Exception", ex);
                model.Error = "Исключение: " + ex.GetBaseException().Message;
                return false;
            }
        }
    }

}