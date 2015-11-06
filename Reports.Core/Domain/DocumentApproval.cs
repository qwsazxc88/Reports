using System;

namespace Reports.Core.Domain
{
    /// <summary>
    /// Согласование документов.
    /// </summary>
    public class DocumentApproval : AbstractEntityWithVersion
    {
        /// <summary>
        /// Тип документа/заявки (ApprovalTypeEnum)
        /// </summary>
        public virtual int ApprovalType { get; set; }
        /// <summary>
        /// Id документа/заявки
        /// </summary>
        public virtual int DocId { get; set; }
        /// <summary>
        /// Согласовант.
        /// </summary>
        public virtual User ApproveUser { get; set; }
        /// <summary>
        /// Помощник.
        /// </summary>
        public virtual User AssistantUser { get; set; }
        /// <summary>
        /// Номер позиции в цепочке согласования документа.
        /// </summary>
        public virtual int Number { get; set; }
        /// <summary>
        /// Признак архива. Если в процессе согласования предусмотрена отмена согласования или что-то в этом роде, то уже согласованные позиции отправляются в архив.
        /// </summary>
        public virtual bool IsArchive { get; set; }
        /// <summary>
        /// Дата создания записи.
        /// </summary>
        public virtual DateTime? CreateDate { get; set; }
        /// <summary>
        /// Дата архивирования.
        /// </summary>
        public virtual DateTime? ArchiveDate { get; set; }
    }
}
