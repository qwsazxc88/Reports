using System;

namespace Reports.Core.Domain
{
    /// <summary>
    /// Связи помощников с разделами приложения
    /// </summary>
    public class AssistantPartialLinks : AbstractEntityWithVersion
    {
        /// <summary>
        /// Связь согласовнта и помощника.
        /// </summary>
        public virtual Assistans Assistans { get; set; }
        /// <summary>
        /// Раздел приложения.
        /// </summary>
        public virtual PartialTypes PartialTypes { get; set; }
        /// <summary>
        /// Начало периода действия.
        /// </summary>
        public virtual DateTime? BeginDate { get; set; }
        /// <summary>
        /// Конец периода действия
        /// </summary>
        public virtual DateTime? EndDate { get; set; }
        /// <summary>
        /// Признак использования.
        /// </summary>
        public virtual bool IsUsed { get; set; }
        /// <summary>
        /// Автор записи.
        /// </summary>
        public virtual User Creator { get; set; }
        /// <summary>
        /// Дата создания записи.
        /// </summary>
        public virtual DateTime? CreateDate { get; set; }
        /// <summary>
        /// Редактор записи.
        /// </summary>
        public virtual User Editor { get; set; }
        /// <summary>
        /// Дата последнего редактирования записи.
        /// </summary>
        public virtual DateTime? EditDate { get; set; }
    }
}
