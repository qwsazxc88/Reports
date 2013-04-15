using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;

namespace Reports.Presenters.UI.ViewModel
{
    public class DismissalListModel:BeginEndCreateDate
    {
        public int UserId { get; set; }

        [Display(Name = "Структурное подразделение")]
        [AutoComplete("AutoComplete", "Departments", "searchText")]
        public IdNameReadonlyDto Department { get; set; }
        //public int DepartmentId { get; set; }
        //public IList<IdNameDto> Departments;
        [Display(Name = "Должность")]
        public int PositionId { get; set; }
        public IList<IdNameDto> Positions;
        [Display(Name = "Основание увольнения")]
        public int TypeId { get; set; }
        public IList<IdNameDto> Types;

        /*[Display(Name = "Дата окончания Т Д.")]
        public DateTime? EndDate { get; set; }*/
        [Display(Name = "Период с")]
        public DateTime? BeginDate { get; set; }
        [Display(Name = "по")]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Статус заявки")]
        public int StatusId { get; set; }
        public IList<IdNameDto> Statuses;

        //[Display(Name = "Документы")]
        public IList<VacationDto> Documents { get; set; }

    }

    
}