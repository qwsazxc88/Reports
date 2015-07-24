using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;
using Reports.Core.Domain;

namespace Reports.Presenters.UI.ViewModel.StaffList
{
    public class TreeViewAjaxModel
    {
        public IList<StaffListDepartmentDto> Departments { get; set; }
        public string DepId { get; set; }
        //public string ParentId { get; set; }
        public string FactAddress { get; set; }
    }
}
