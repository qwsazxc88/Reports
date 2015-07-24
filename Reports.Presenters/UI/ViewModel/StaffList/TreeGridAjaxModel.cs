using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;
using Reports.Core.Domain;

namespace Reports.Presenters.UI.ViewModel.StaffList
{
    public class TreeGridAjaxModel
    {
        public IList<StaffListDepartmentDto> Departments { get; set; }
        public IList<UsersListItemDto> UserPositions { get; set; }
        public string DepId { get; set; }
    }
}
