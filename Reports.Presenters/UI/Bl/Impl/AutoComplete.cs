using System.Collections.Generic;
using System.Linq;
using Reports.Core;
using Reports.Core.Dao;
using Reports.Core.Dto;


namespace Reports.Presenters.UI.Bl.Impl 
{
    public class AutoComplete : IAutoComplete
    {
        protected IDepartmentDao departmentDao;
        public IDepartmentDao DepartmentDao
        {
            get
            {
                departmentDao = Ioc.Resolve<IDepartmentDao>();
                return Validate.Dependency(departmentDao);
            }
        }
        public List<IdNameDto> SearchDepartments(string name)
        {
            return DepartmentDao.SearchByName(name).ToList().
                ConvertAll( x => new IdNameDto { Id = x.Id, Name = x.Name} );
        }
        /*public List<IDNameDictionary> SearchVendors(string name)
        {
            return Ioc.Resolve<ISupplierDAO>().SearchByName(name).ConvertAll(x => new IDNameDictionary { ID = x.Id, Name = x.Name });
        }*/
    }
}
