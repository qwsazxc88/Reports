using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;

namespace Reports.Presenters.UI.ViewModel.StaffList
{
    public class TreeViewModel
    {
        public IList<DepartmentDto> Departments { get; set; }
    }
}
