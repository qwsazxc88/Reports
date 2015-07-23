using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;
using Reports.Core.Domain;

namespace Reports.Presenters.UI.ViewModel.StaffList
{
    public class StaffListModel
    {
        //public IList<DepartmentWithFigradPointsDto> Departments { get; set; }
        public IList<Department> Departments { get; set; }
        public IList<UsersListItemDto> UserPositions { get; set; }
        public string DepId { get; set; }
        public int PositionCount { get; set; }
    }
}
