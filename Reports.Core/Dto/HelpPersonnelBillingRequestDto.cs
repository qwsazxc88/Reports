using System;

namespace Reports.Core.Dto
{
    public class HelpPersonnelBillingRequestDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Urgency { get; set; }
        public DateTime CreateDate { get; set; }
        public string ForUserName { get; set; }
        public string Dep3Name { get; set; }
        public string Dep7Name { get; set; }
        public int RequestNumber { get; set; }
        public string CreatorName { get; set; }
        public string RepicientName { get; set; }
        public int StatusNumber { get; set; }
        public string Status { get; set; }
    }
}