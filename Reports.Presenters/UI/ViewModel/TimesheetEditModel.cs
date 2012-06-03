using System;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;

namespace Reports.Presenters.UI.ViewModel
{
    public class TimesheetEditModel : UserModel, IEmailDtoSupport
    {
        [DataType("TimesheetDto")]
        public TimesheetDto TimesheetDto { get; set; }

        public bool ViewHeader { get; set; }

        public bool IsNotApprovedByUserEnable { get; set; }
        [Display(Name = "Не согласен Сотрудник")]
        public bool IsNotApprovedByUser { get; set; }
        public bool IsNotApprovedByUserHidden { get; set; }

        public bool IsNotApprovedByPersonnelEnable { get; set; }
        [Display(Name = "Не согласен Кадровик")]
        public bool IsNotApprovedByPersonnel { get; set; }
        public bool IsNotApprovedByPersonnelHidden { get; set; }

        public bool IsSaveAvailable { get; set; }

        public int Id { get; set; }
        public DateTime Month { get; set; }

        public EmailDto EmailDto { get; set; }
    }
}