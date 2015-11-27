using System;
using System.Collections.Generic;


namespace Reports.Core.Domain
{
    /// <summary>
    /// Справочник единиц измерения
    /// </summary>
    public class StaffUnitReference : AbstractEntity
    {
        public virtual string Name { get; set; }
    }
}
