using System;

namespace Reports.Core.Dto
{
    public class VacationDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Number { get; set; }
        public string Position { get; set; }
        public string Dep3Name { get; set; }
        public string Dep7Name { get; set; }
        public string UserName { get; set; }
        public string RequestType { get; set; }
        public string RequestStatus { get; set; }
        public bool IsOriginalReceived { get; set; }
        public bool IsOriginalRequestReceived { get; set; }
        public bool IsPersonnelFileSentToArchive { get; set; }
        public string ManagerName { get; set; }
    }
    public class MissionDto : VacationDto
    {
        public bool IsChecked { get; set; }
        public bool? Flag { get; set; }
        public string IsAdditionalOrderExists { get; set; }
    }
    public class AllRequestDto : VacationDto
    {
        public string EditUrl { get; set; }
    }
}