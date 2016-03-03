using System;

namespace Reports.Core.Dto
{
    public class SicklistDto : VacationDto
    {
        // Налучие у владельца больничного листа заведенного стажа в ЗУП
        public bool? UserExperienceIn1C { get; set; }
        public string SicklistNumber { get; set; }
        public bool IsOriginalSended { get; set; }
        
    }
}