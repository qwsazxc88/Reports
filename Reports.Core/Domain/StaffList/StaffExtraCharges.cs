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
        public virtual bool IsPostOnly { get; set; }
        /// <summary>
        /// Единица измерения.
        /// </summary>
        public virtual StaffUnitReference UnitReference { get; set; }
        /// <summary>
        /// Признак необходимости учета надбавки, а не учета ее значения
        /// </summary>
        public virtual bool IsNeeded { get; set; }  
    }
}
