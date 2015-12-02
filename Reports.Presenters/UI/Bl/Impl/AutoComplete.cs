using System.Collections.Generic;
using System.Linq;
using Reports.Core;
using Reports.Core.Dao;
using Reports.Core.Dto;


namespace Reports.Presenters.UI.Bl.Impl 
{
    public class AutoComplete : IAutoComplete
    {
        #region dao
        protected IDepartmentDao departmentDao;
        public IDepartmentDao DepartmentDao
        {
            get
            {
                departmentDao = Ioc.Resolve<IDepartmentDao>();
                return Validate.Dependency(departmentDao);
            }
        }
        protected IUserDao userDao;
        public IUserDao UserDao
        {
            get
            {
                userDao = Ioc.Resolve<IUserDao>();
                return Validate.Dependency(userDao);
            }
        }
        #endregion
        public List<IdNameDto> SearchDepartments(string name)
        {
            return DepartmentDao.SearchByName(name).ToList().
                ConvertAll( x => new IdNameDto { Id = x.Id, Name = x.Name} );
        }
        public List<IdNameDto> SearchUsers(string name)
        {
            var users = UserDao.Find(x => x.Name.Contains(name) && x.IsActive && (x.UserRole & UserRole.Employee) > 0 && x.Department != null && x.Position != null);
            if (users != null && users.Any())
                return users.Select(x => new IdNameDto { Id = x.Id, Name = string.Format("{0} ({1}) {2}", x.Name, x.Position.Name, x.Department.Name) }).ToList();
            return new List<IdNameDto>();
        }
        /*public List<IDNameDictionary> SearchVendors(string name)
        {
            return Ioc.Resolve<ISupplierDAO>().SearchByName(name).ConvertAll(x => new IDNameDictionary { ID = x.Id, Name = x.Name });
        }*/
    }
}
