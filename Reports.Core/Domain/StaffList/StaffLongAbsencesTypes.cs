using System;
using System.Collections.Generic;


namespace Reports.Core.Domain
{
    /// <summary>
    /// Справочник видов длительных отсутствий.
    /// </summary>
    public class StaffLongAbsencesTypes : AbstractEntity
    {
        /// <summary>
        /// Наименование
        /// </summary>
        public virtual string Name { get; set; }
        /// <summary>
        /// Дата создания записи
        /// </summary>
        public virtual DateTime? CreateDate { get; set; }
    }
}
