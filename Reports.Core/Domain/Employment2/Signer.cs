using System;

namespace Reports.Core.Domain
{
    public class Signer : AbstractEntityWithVersion
    {
        public virtual string Name { get; set; }
        public virtual string Position { get; set; }
        public virtual string PreamblePartyTemplate { get; set; } // Шаблон указания стороны в преамбуле договора
    }
}