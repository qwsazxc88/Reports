using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;

namespace Reports.Presenters.UI.ViewModel
{
    public interface IDepartmentSelect
    {
        string DepartmentName { get; set; }
        int DepartmentId { get; set; }
        bool DepartmentReadOnly { get; set; }
    }
    public class TimesheetListModel:IDepartmentSelect
    {
        public int ManagerId { get; set; }

        [Display(Name = "Месяц")]
        public int Month { get; set; }
        public IList<IdNameDto> Monthes;

        [Display(Name = "Год")]
        public int Year { get; set; }
        public IList<IdNameDto> Years;

        [Display(Name = "Структурное подразделение")]
        public string DepartmentName { get; set; }
        public int DepartmentId { get; set; }
        public bool DepartmentReadOnly { get; set; }

        [Display(Name = "ФИО сотрудника")]
        public string UserName { get; set; }

        //[Display(Name = "Дата")]
        //public DateTime Date { get; set; }
        //public IList<DateDto> Dates;

        //[Display(Name = "Значение")]
        //public int Status { get; set; }
        //public IList<IdNameDto> Statuses;

        //[Required(ErrorMessageResourceName = "TimesheetListModel_Hours_Required",
        //    ErrorMessageResourceType = typeof(Resources))]
        //[LocalizationDisplayName("TimesheetListModel_Hours_Required", typeof(Resources))]
        //[Display(Name = "Часы")]
        //public float Hours { get; set; }

        //public bool IsEditable { get; set; }

        [DataType("TimesheetDtoList")]
        public IList<TimesheetDto> TimesheetDtos { get; set; }

        public int NumberOfPages { get; set; }
        public int CurrentPage { get; set; }

        public bool IsSaveVisible { get; set; }
        public bool IsSaveNeed { get; set; }
    }
}