using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;

namespace Reports.Presenters.UI.ViewModel
{
    public class MissionListModel
    {
        public int UserId { get; set; }

        [Display(Name = "Структурное подразделение")]
        public string DepartmentName { get; set; }
        public int DepartmentId { get; set; }
        public bool DepartmentReadOnly { get; set; }

        //[AutoComplete("AutoComplete", "Departments", "searchText")]
        //public IdNameReadonlyDto Department { get; set; }
        //public int DepartmentId { get; set; }
        //public IList<IdNameDto> Departments;

        [Display(Name = "Должность")]
        public int PositionId { get; set; }
        public IList<IdNameDto> Positions;
        [Display(Name = "Вид командировки")]
        public int TypeId { get; set; }
        public IList<IdNameDto> Types;

        [Display(Name = "Период с")]
        public DateTime? BeginDate { get; set; }
        [Display(Name = "по")]
        public DateTime? EndDate { get; set; }

        //[Display(Name = "Статус заявки")]
        //public int StatusId { get; set; }
        //public IList<IdNameDto> Statuses;

        public IList<VacationDto> Documents { get; set; }

    }

    
}