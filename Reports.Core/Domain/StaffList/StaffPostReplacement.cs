using System;
using System.Collections.Generic;

namespace Reports.Core.Domain
{
    /// <summary>
    /// Журнал замещения сотрудников
    /// </summary>
    public class StaffPostReplacement : AbstractEntity
    {
        /// <summary>
        /// Штатная расстановка.
        /// </summary>
        public virtual StaffEstablishedPostUserLinks EstablishedPostUserLinks { get; set; }
        /// <summary>
        /// Сотрудник занимающий должность.
        /// </summary>
        public virtual User User { get; set; }
        /// <summary>
        /// Замещенный сотрудник.
        /// </summary>
        public virtual User ReplacedUser { get; set; }
        /// <summary>
        /// Признак использования данной связи.
        /// </summary>
        public virtual bool IsUsed { get; set; }
        /// <summary>
        /// Основание для замещения сотрудника.
        /// </summary>
        public virtual StaffReplacementReasons ReplacementReason { get; set; }
        /// <summary>
        /// Автор записи
        /// </summary>
        public virtual User Creator { get; set; }
        /// <summary>
        /// Дата создания записи
        /// </summary>
        public virtual DateTime? CreateDate { get; set; }
        /// <summary>
        /// Редактор записи.
        /// </summary>
        public virtual User Editor { get; set; }
        /// <summary>
        /// Дата последнего редактирования
        /// </summary>
        public virtual DateTime? EditDate { get; set; }
    }
}
