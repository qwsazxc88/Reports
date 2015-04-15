using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reports.Presenters.UI.ViewModel.StaffList;
using Reports.Core.Domain;

namespace Reports.Presenters.UI.Bl.Impl
{
    public class StaffListBl : RequestBl, IStaffListBl
    {
        public TreeViewModel GetDepartmentList()
        {
            TreeViewModel model = new TreeViewModel();
            model.Departments = DepartmentDao.GetDepartmentsTree((int)DepartmentDao.LoadAll().Where(x => x.ItemLevel == 1).Single().Id);
            return model;
        }

        public IList<Department> GetDepartmentNodes(int DepartmentId)
        {
            //IList<Department> depList = DepartmentDao.GetDepartmentsTree(DepartmentId);
            return DepartmentDao.GetDepartmentsTree(DepartmentId);
        }
    }
}
