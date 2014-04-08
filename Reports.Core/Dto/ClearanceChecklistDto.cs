using System;

namespace Reports.Core.Dto
{
    public class ClearanceChecklistDto : VacationDto
    {
        // Номер реестра
        public int? RegistryNumber { get; set; }
        // НДФЛ
        public decimal? PersonalIncomeTax { get; set; }
        // ОКТМО
        public string OKTMO { get; set; }
    }
}