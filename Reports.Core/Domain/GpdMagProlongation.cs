using System;
using System.Collections.Generic;

namespace Reports.Core.Domain
{
    /// <summary>
    /// Журнал изменений даты пролонгации договора.
    /// </summary>
    public class GpdMagProlongation : AbstractEntity
    {
        public virtual int GCID { get; set; }
        public virtual DateTime? DateP { get; set; }
        public virtual int CreatorID { get; set; }

        public virtual GpdContract GpdContracts { get; set; }
    }
}
