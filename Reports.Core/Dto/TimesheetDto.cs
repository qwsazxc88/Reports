using System.Collections.Generic;

namespace Reports.Core.Dto
{
    public class TimesheetDto
    {
        public int Id { get; set; }
        public bool IsEditable { get; set; }
        public string MonthAndYear { get; set; }
        public string UserNameAndCode { get; set; }
        public int OwnerId { get; set; }
        //public IList<IdNameDto> Statuses { get; set; }
        public IList<TimesheetDayDto> Days { get; set; }

    }
    public class TimesheetDayDto
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int StatusId { get; set; }
        public string Status { get; set; }
        public float Hours { get; set; }
    }
}