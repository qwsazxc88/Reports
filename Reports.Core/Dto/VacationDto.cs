using System;

namespace Reports.Core.Dto
{
    public class VacationDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        //public string Type { get; set; }
        public DateTime Date { get; set; }
        //public bool IsApproved { get; set; }
        //public int OwnerId { get; set; }
    }
    public class AllRequestDto : VacationDto
    {
        public string EditUrl { get; set; }
    }
}