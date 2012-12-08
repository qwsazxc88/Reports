using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;

namespace Reports.Presenters.UI.ViewModel
{
    public class AcceptRequestsModel
    {
        [Display(Name = "Месяц")]
        public int Month { get; set; }
        public IList<IdNameDto> Monthes { get; set; }

        [Display(Name = "Год")]
        public int Year { get; set; }
        public IList<IdNameDto> Years { get; set; }

        public IList<AcceptWeekDto> WeeksList { get; set; }
        public IList<UserAcceptWeekDto> UsersList { get; set; }

        public string AcceptDate { get; set; }
        public string Error { get; set; }
        //public EmailDto EmailDto { get; set; }
    }
    public class AcceptWeekDto
    {
        public DateTime Monday { get; set; }
        public DateTime Friday { get; set; }
    }
    public class AcceptRequestWeekDto
    {
        public DateTime Friday { get; set; }
        public bool IsAccepted { get; set; }
        public bool IsEditable { get; set; }
        public bool IsHidden  { get; set; }
    }
    public class UserAcceptWeekDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public IList<AcceptRequestWeekDto> dtos { get; set; }
    }
}