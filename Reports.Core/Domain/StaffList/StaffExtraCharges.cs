using System;
using System.Collections.Generic;

namespace Reports.Core.Domain
{
    /// <summary>
    /// Справочник надбавок.
    /// </summary>
    public class StaffExtraCharges : AbstractEntity
    {
        public virtual string GUID { get; set; } 
        public virtual string Name { get; set; }
        /// <summary>
        /// Должностная надбавка
        /// </summary>
        public virtual bool IsPost { get; set; }
        /// <summary>
        /// Персональная надбавка
        /// </summary>
        public virtual bool IsPersonnel { get; set; }
    }
}
