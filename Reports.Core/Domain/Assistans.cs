using System;

namespace Reports.Core.Domain
{
    /// <summary>
    /// Помощники.
    /// </summary>
    public class Assistans : AbstractEntityWithVersion
    {
        /// <summary>
        /// Согласовант.
        /// </summary>
        public virtual User ApproveUser { get; set; }
        /// <summary>
        /// Помощник.
        /// </summary>
        public virtual User AssistantUser { get; set; }
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
        public virtual User Creator { get; set; }
        public virtual DateTime? CreateDate { get; set; }
        public virtual User Editor { get; set; }
        public virtual DateTime? EditDate { get; set; }
    }
}
