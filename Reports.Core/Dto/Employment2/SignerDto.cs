using System;

namespace Reports.Core.Dto.Employment2
{
    public class SignerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string PreamblePartyTemplate { get; set; } // Шаблон указания стороны в преамбуле договора
    }
}
