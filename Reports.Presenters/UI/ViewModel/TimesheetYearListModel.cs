using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;

namespace Reports.Presenters.UI.ViewModel
{
    public class TimesheetYearListModel:IDepartmentSelect
    {
        public int ManagerId { get; set; }

        [Display(Name = "Структурное подразделение")]
        public string DepartmentName { get; set; }

        public int DepartmentId { get; set; }
        public bool DepartmentReadOnly { get; set; }

        [Display(Name = "ФИО сотрудника")]
        public string UserName { get; set; }

        public string DatesPeriod { get; set; }

        [DataType("TimesheetDtoList")]
        public IList<TimesheetDto> TimesheetDtos { get; set; }

        public int NumberOfPages { get; set; }
        public int CurrentPage { get; set; }
    }
}