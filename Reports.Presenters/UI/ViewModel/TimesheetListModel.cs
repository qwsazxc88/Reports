using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;

namespace Reports.Presenters.UI.ViewModel
{
    public class TimesheetListModel
    {
        public int ManagerId { get; set; }

        [Display(Name = "Месяц")]
        public DateTime Month { get; set; }
        public IList<DateDto> Monthes;

        [Display(Name = "Дата")]
        public DateTime Date { get; set; }
        public IList<DateDto> Dates;

        [Display(Name = "Значение")]
        public int Status { get; set; }
        public IList<IdNameDto> Statuses;

        [Required(ErrorMessageResourceName = "TimesheetListModel_Hours_Required",
            ErrorMessageResourceType = typeof(Resources))]
        [LocalizationDisplayName("TimesheetListModel_Hours_Required", typeof(Resources))]
        //[Range(0.00, 24.00, ErrorMessage = "Часы должно быть от {1} до {2}")]
        [Display(Name = "Часы")]
        public float Hours { get; set; }

        public bool IsEditable { get; set; }

        [DataType("TimesheetDtoList")]
        public IList<TimesheetDto> TimesheetDtos { get; set; }
    }
}