using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;

namespace Reports.Presenters.UI.ViewModel
{
    public class EditDayModel
    {
        public int Id { get; set; }

        public string Day { get; set; }

        [Display(Name = "Значение")]
        public int StatusId { get; set; }
        public IList<IdNameDto> Statuses;

        [Required(ErrorMessageResourceName = "TimesheetListModel_Hours_Required",
            ErrorMessageResourceType = typeof (Resources))]
        [LocalizationDisplayName("TimesheetListModel_Hours_Required", typeof (Resources))]
        [Range(0.00f, 24.00f, ErrorMessage = "Часы должно быть от {1} до {2}")]
        [Display(Name = "Часы")]
        public float Hours { get; set; }
    }
}