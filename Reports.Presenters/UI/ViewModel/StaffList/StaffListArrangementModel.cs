using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;
using Reports.Core.Domain;

namespace Reports.Presenters.UI.ViewModel.StaffList
{
    public class StaffListArrangementModel
    {
        //public IList<DepartmentWithFigradPointsDto> Departments { get; set; }
        public IList<StaffListDepartmentDto> Departments { get; set; }
        //public IList<UsersListItemDto> UserPositions { get; set; }    // должности сотрудников
        public IList<StaffEstablishedPostDto> EstablishedPosts { get; set; } //штатные единицы
        public string DepId { get; set; }
        public int PositionCount { get; set; }
        //public StandartUserDto UserId { get; set; }
    }
}
