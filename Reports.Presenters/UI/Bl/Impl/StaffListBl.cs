using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reports.Presenters.UI.ViewModel.StaffList;

namespace Reports.Presenters.UI.Bl.Impl
{
    public class StaffListBl : RequestBl, IStaffListBl
    {
        public TreeViewModel GetDepartmentList()
        {
            TreeViewModel model = new TreeViewModel();
            model.Departments = DepartmentDao.LoadAll();
            return model;
        }
    }
}
