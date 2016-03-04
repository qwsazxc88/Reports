using System;

namespace Reports.Core.Dto
{
    public class SicklistDto : VacationDto
    {
        // Налучие у владельца больничного листа заведенного стажа в ЗУП
        public bool? UserExperienceIn1C { get; set; }
        public string SicklistNumber { get; set; }
        public bool IsOriginalSended { get; set; }
        public bool IsOriginalFilled {get;set;}
        public DateTime? OriginalFilledDate { get; set; }
        public DateTime? OriginalReceivedDate {get;set;}
        public DateTime? OriginalSendDate { get; set; }
    }
}