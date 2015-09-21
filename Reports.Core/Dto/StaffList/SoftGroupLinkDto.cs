using System;
using System.Collections.Generic;

namespace Reports.Core.Dto
{
    /// <summary>
    /// Связи Групп с ПО.
    /// </summary>
    public class SoftGroupLinkDto
    {
        public int Id { get; set; }
        public int SoftId { get; set; }
        public string Name { get; set; }
        public bool IsUsed { get; set; }
    }
}
