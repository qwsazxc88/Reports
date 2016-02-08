using System;
using System.Collections.Generic;
using Reports.Core.Domain;
using System.Text;

namespace Reports.Core.Domain
{
    /// <summary>
    /// Права членов правления для согласования заявок в штатном расписании.
    /// </summary>
    public class DirectorsRight : AbstractEntity
    {
        /// <summary>
        /// Руководитель
        /// </summary>
        public virtual User User { get; set; }
        /// <summary>
        /// Принадлежность подразделения
        /// </summary>
        public virtual StaffDepartmentAccessory DepartmentAccessory { get; set; }
    }
}
