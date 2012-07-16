using System;
using System.Collections.Generic;
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
    public class RequestBl : BaseBl, IRequestBl
    {
        protected string SelectAll = "Все";

        public const int VacationFirstTimesheetStatisId = 8;
        public const int VacationLastTimesheetStatisId = 12;

        protected IDepartmentDao departmentDao;
        protected IVacationTypeDao vacationTypeDao;
        protected IRequestStatusDao requestStatusDao;
        protected IPositionDao positionDao;
        protected IVacationDao vacationDao;
        protected IUserToDepartmentDao userToDepartmentDao;
        protected ITimesheetStatusDao timesheetStatusDao;
        protected IVacationCommentDao vacationCommentDao;
        protected IRequestNextNumberDao requestNextNumberDao;

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
            return new List<IdNameDto>{new IdNameDto((int)RequestTypeEnum.Vacation,"Заявка на отпуск")};
        }

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
                vacation = vacationDao.Load(id);
                if(vacation == null)
                    throw new ArgumentException(string.Format("Заявка на отпуск (id {0}) не найдена в базе данных.",id));
                model.Version = vacation.Version;
                model.VacationTypeId = vacation.Type.Id;
                model.BeginDate = vacation.BeginDate;//new DateTimeDto(vacation.BeginDate);//
                model.EndDate = vacation.EndDate;
                model.TimesheetStatusId = vacation.TimesheetStatus == null ? 0 : vacation.TimesheetStatus.Id;
                model.DaysCount = vacation.DaysCount;
                model.CreatorLogin = vacation.Creator.Login;
                model.DocumentNumber = vacation.Number.ToString();
                model.DateCreated = vacation.CreateDate.ToShortDateString();
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
                Vacation vacation = null;
                if(model.Id == 0)
                {
                    vacation = new Vacation
                                            {
                                                BeginDate = model.BeginDate.Value,
                                                CreateDate = DateTime.Now,
                                                Creator = UserDao.Load(current.Id),
                                                EndDate = model.EndDate.Value,
                                                DaysCount = model.EndDate.Value.Subtract(model.BeginDate.Value).Days,
                                                Number = RequestNextNumberDao.GetNextNumberForType((int)RequestTypeEnum.Vacation),
                                                Status = RequestStatusDao.Load((int) RequestStatusEnum.NotApproved),
                                                Type = VacationTypeDao.Load(model.VacationTypeId),
                                                User = user
                                             };
                    VacationDao.SaveAndFlush(vacation);
                    model.Id = vacation.Id;
                }
                else
                {
                    vacation = VacationDao.Load(model.Id);
                    if(vacation.Version != model.Version)
                    {
                        error = "Заявка была изменена другим пользователем.";
                        model.ReloadPage = true;
                        return false;
                    }
                    if (current.UserRole == UserRole.Employee && current.Id == model.UserId
                        && vacation.StatusId == RequestStatusEnum.NotApproved
                        && model.IsApprovedByUser)
                        vacation.Status = RequestStatusDao.Load((int) RequestStatusEnum.ApprovedByUser);
                    if (current.UserRole == UserRole.Manager && user.Manager != null
                        && current.Id == user.Manager.Id
                        && vacation.StatusId == RequestStatusEnum.ApprovedByUser
                        && model.IsApprovedByManager)
                    {
                        vacation.Status = RequestStatusDao.Load((int) RequestStatusEnum.ApprovedByManager);
                        vacation.ManagerDateAccept = DateTime.Now;
                    }
                    if (current.UserRole == UserRole.PersonnelManager && user.PersonnelManager != null
                        && current.Id == user.PersonnelManager.Id
                        && vacation.StatusId == RequestStatusEnum.ApprovedByManager
                        )
                    {
                        if (model.IsApprovedByPersonnelManager && model.TimesheetStatusId == 0)
                        {
                            error = "Необходимо указать значение поля 'Заполнение табеля'";
                            return false;
                        }
                        if (model.TimesheetStatusId != 0)
                            vacation.TimesheetStatus = TimesheetStatusDao.Load(model.TimesheetStatusId);
                        if (model.IsApprovedByPersonnelManager)
                        {
                            vacation.Status = RequestStatusDao.Load((int) RequestStatusEnum.ApprovedByPersonnel);
                            vacation.PersonnelManagerDateAccept = DateTime.Now;
                        }
                    }
                    if (model.IsVacationTypeEditable)
                    {
                        vacation.BeginDate = model.BeginDate.Value;
                        vacation.EndDate = model.EndDate.Value;
                        vacation.DaysCount = model.EndDate.Value.Subtract(model.BeginDate.Value).Days;
                        vacation.Type = VacationTypeDao.Load(model.VacationTypeId);
                    }
                    VacationDao.SaveAndFlush(vacation);
                }
                model.DocumentNumber = vacation.Number.ToString();
                model.Version = vacation.Version;
                model.DaysCount = vacation.DaysCount;
                model.CreatorLogin = vacation.Creator.Login;
                SetFlagsState(vacation.Id,user,vacation,model);
                return true;
            }
            catch (Exception ex)
            {
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
        }
        protected void SetFlagsState(int id,User user,Vacation vacation,VacationEditModel model)
        {
            SetFlagsState(model,false);
            if(id == 0)
            {
                model.IsSaveAvailable = true;
                model.IsVacationTypeEditable = true;
                return;
            }
            UserRole currentUserRole = AuthenticationService.CurrentUser.UserRole;
            model.IsApprovedByUserHidden = model.IsApprovedByUser = vacation.StatusId >= RequestStatusEnum.ApprovedByUser;
            model.IsApprovedByManagerHidden = model.IsApprovedByManager = vacation.StatusId >= RequestStatusEnum.ApprovedByManager;
            model.IsApprovedByPersonnelManagerHidden = model.IsApprovedByPersonnelManager = vacation.StatusId >= RequestStatusEnum.ApprovedByPersonnel;
            model.IsPostedTo1CHidden = model.IsPostedTo1C = vacation.StatusId >= RequestStatusEnum.SendTo1C;
            switch(currentUserRole)
            {
                case UserRole.Employee:
                    if (vacation.StatusId == RequestStatusEnum.NotApproved)
                    {
                        model.IsApprovedByUserEnable = true;
                        model.IsSaveAvailable = true;
                        model.IsVacationTypeEditable = true;
                    }
                    break;
                case UserRole.Manager:
                    if (vacation.StatusId == RequestStatusEnum.ApprovedByUser)
                    {
                        //model.IsApprovedByManager = true;
                        //model.IsApprovedByManagerHidden = true;
                        model.IsApprovedByManagerEnable = true;
                        model.IsSaveAvailable = true;
                        model.IsVacationTypeEditable = true;
                    }
                    break;
                case UserRole.PersonnelManager:
                    if (vacation.StatusId == RequestStatusEnum.ApprovedByManager)
                    {
                        //model.IsApprovedByPersonnelManager = true;
                        //model.IsApprovedByPersonnelManagerHidden = true;
                        model.IsApprovedByPersonnelManagerEnable = true;
                        model.IsSaveAvailable = true;
                        model.IsVacationTypeEditable = true;
                        model.IsTimesheetStatusEditable = true;
                    }
                    break;
            }
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
                ConvertAll(x => new IdNameDto(x.Id, x.Name));
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
                    case (int)RequestTypeEnum.Vacation:
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
        #endregion
    }

}