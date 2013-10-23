using System;
using System.Collections.Generic;

namespace Reports.Core.Dto
{
    public class TimesheetDto
    {
        //public int Id { get; set; }
        //public bool IsEditable { get; set; }
        public int UserId { get; set; }
        public bool IsHoursVisible  { get; set; }
        public bool IsGraphicVisible { get; set; }
        public bool IsGraphicEditable { get; set; }
        public string MonthAndYear { get; set; }
        public string UserNameAndCode { get; set; }
        //public int OwnerId { get; set; }
        //public IList<IdNameDto> Statuses { get; set; }
        public IList<TimesheetDayDto> Days { get; set; }

    }
    public class TimesheetDayDto
    {
        public int Id { get; set; }
        public int Number { get; set; }
        //public int StatusId { get; set; }
        public string Status { get; set; }
        public string Hours { get; set; }
        public bool isHoliday { get; set; }
        public string Graphic { get; set; }
        public bool isStatRecord { get; set; }

        public string StatCode { get; set; }
    }

    public class RequestDto
    {
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TimesheetStatusId { get; set; }
        public string TimesheetCode { get; set; }
        public int? TimesheetHours { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public bool IsDismissalDay { get; set; }
        public bool IsEmploymentDay { get; set; }
    }
    public class DayRequestsDto
    {
        public DateTime Day { get; set; }
        public List<RequestDto> Requests { get; set; }

        public DayRequestsDto()
        {
            Requests = new List<RequestDto>();
        }
    }

    public class TerraGraphicDayDto
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public DateTime? Day { get; set; }
        //public int StatusId { get; set; }
        //public string Status { get; set; }
        public int? Hours { get; set; }
        public int? TerraPointId { get; set; }
        public string TerraPointName { get; set; }
        public string IsCredits { get; set; }
        public bool isHoliday { get; set; }
        //public string Graphic { get; set; }
        public bool isStatRecord { get; set; }
        public string StatCode { get; set; }
    }
    public class TerraGraphicDto
    {
        //public int Id { get; set; }
        //public bool IsEditable { get; set; }
        public int UserId { get; set; }
        //public bool IsHoursVisible { get; set; }
        //public bool IsGraphicVisible { get; set; }
        public bool IsGraphicEditable { get; set; }
        public string MonthAndYear { get; set; }
        public string UserNameAndCode { get; set; }
        public string Postion { get; set; }
        public string Rate { get; set; }
        //public int OwnerId { get; set; }
        //public IList<IdNameDto> Statuses { get; set; }
        public IList<TerraGraphicDayDto> Days { get; set; }

    }
}