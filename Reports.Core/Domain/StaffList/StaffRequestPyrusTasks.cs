using System;
using System.Collections.Generic;

namespace Reports.Core.Domain
{
    /// <summary>
    /// Задачи в Пайрусе.
    /// </summary>
    public class StaffRequestPyrusTasks : AbstractEntity
    {
        /// <summary>
        /// Заявка для подазделения
        /// </summary>
        public virtual StaffDepartmentRequest DepRequest { get; set; }
        ///// <summary>
        ///// Заявка для штатной единицы
        ///// </summary>
        public virtual StaffEstablishedPostRequest EPRequest { get; set; }
        ///// <summary>
        ///// Согласование заявок.
        ///// </summary>
        public virtual DocumentApproval DocumentApproval { get; set; }
        ///// <summary>
        ///// Номер задачи в Пайрусе
        ///// </summary>
        public virtual string NumberTask { get; set; }
        ///// <summary>
        ///// Автор записи.
        ///// </summary>
        public virtual User Creator { get; set; }
        ///// <summary>
        ///// Дата создания записи
        ///// </summary>
        public virtual DateTime? CreateDate { get; set; }
    }
}
