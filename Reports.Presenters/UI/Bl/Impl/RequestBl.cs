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
    }

}