using System;


namespace Reports.Core.Domain
{
    public class ExtraCharges : AbstractEntityWithVersion
    {
        public virtual string Code1C { get; set; }
        public virtual string Name { get; set; }
        public virtual PersonnelManagers PersonnelManagers { get; set; }
    }
}
