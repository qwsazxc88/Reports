using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reports.Presenters.UI.ViewModel.StaffList;
using Reports.Core.Domain;

namespace Reports.Presenters.UI.Bl
{
    public interface IStaffListBl : IBaseBl
    {
        TreeViewModel GetDepartmentList();
    }
}
