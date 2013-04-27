using System;

namespace Reports.Core.Dto
{
    public class VacationDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }

        public string UserName { get; set; }
        public string RequestType { get; set; }
        public string RequestStatus { get; set; }
    }
    public class AllRequestDto : VacationDto
    {
        public string EditUrl { get; set; }
    }
}