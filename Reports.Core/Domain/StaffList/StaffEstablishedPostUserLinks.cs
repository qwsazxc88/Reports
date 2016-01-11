using System;
using System.Collections.Generic;

namespace Reports.Core.Domain
{
    /// <summary>
    /// Связи штатных единиц с сотрудниками (штатная расстановка)
    /// </summary>
    public class StaffEstablishedPostUserLinks : AbstractEntityWithVersion
    {
        /// <summary>
        /// Штатная единица.
        /// </summary>
        public virtual StaffEstablishedPost StaffEstablishedPost { get; set; }
        /// <summary>
        /// Сотрудник занимающий штатную единицу.
        /// </summary>
        public virtual User User { get; set; }
        /// <summary>
        /// Признак использования данной связи.
        /// </summary>
        public virtual bool IsUsed { get; set; }
        /// <summary>
        /// Тип бронирования вакансии (StaffReserveTypeEnum).
        /// </summary>
        public virtual int? ReserveType { get; set; }
        /// <summary>
        /// Id документа/заявки
        /// </summary>
        public virtual int? DocId { get; set; }
        /// <summary>
        /// Признак сокращения
        /// </summary>
        public virtual bool IsDismissal { get; set; }
        /// <summary>
        /// Дата выдачи уведомления о сокращении
        /// </summary>
        public virtual DateTime? DateDistribNote { get; set; }
        /// <summary>
        /// Дата получения уведомления о сокращении
        /// </summary>
        public virtual DateTime? DateReceivNote { get; set; }
        /// <summary>
        /// Признак временной вакансии.
        /// </summary>
        public virtual bool IsTemporary { get; set; }
        /// <summary>
        /// Дата начала периода действия временной вакансии.
        /// </summary>
        public virtual DateTime? DateTempBegin { get; set; }
        /// <summary>
        /// Дата конца периода действия временной вакансии.
        /// </summary>
        public virtual DateTime? DateTempEnd { get; set; }
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
        /// <summary>
        /// Замещения.
        /// </summary>
        public virtual IList<StaffPostReplacement> StaffPostReplacements { get; set; }
    }
}
