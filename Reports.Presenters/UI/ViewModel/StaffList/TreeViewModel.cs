using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;
using Reports.Core.Domain;

namespace Reports.Presenters.UI.ViewModel.StaffList
{
    public class TreeViewModel
    {
        public IList<Department> Departments { get; set; }
        public string DepId { get; set; }
        public string ParentId { get; set; }
    }
}
