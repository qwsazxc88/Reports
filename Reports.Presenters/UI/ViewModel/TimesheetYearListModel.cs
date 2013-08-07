using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;

namespace Reports.Presenters.UI.ViewModel
{
    public class TimesheetYearListModel : IDepartmentSelect, BeginEndCreateDate
    {
        public int ManagerId { get; set; }

        [Display(Name = "Структурное подразделение")]
        public string DepartmentName { get; set; }

        public int DepartmentId { get; set; }
        public bool DepartmentReadOnly { get; set; }

        [Display(Name = "ФИО сотрудника")]
        public string UserName { get; set; }

        [Display(Name = "Период с")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? BeginDate { get; set; }
        [Display(Name = "по")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? EndDate { get; set; }


        public string DatesPeriod { get; set; }

        [DataType("TimesheetDtoList")]
        public IList<TimesheetDto> TimesheetDtos { get; set; }

        public int NumberOfPages { get; set; }
        public int CurrentPage { get; set; }
    }
}