using System;
using System.Collections.Generic;
using Reports.Core.Domain;

namespace Reports.Core.Domain
{
    /// <summary>
    /// Заявки на временно освобождение вакансии.
    /// </summary>
    public class StaffTemporaryReleaseVacancyRequest : AbstractEntityWithVersion
    {
        /// <summary>
        /// Штатная расстановка.
        /// </summary>
        public virtual StaffEstablishedPostUserLinks EstablishedPostUserLinks { get; set; }
        /// <summary>
        /// Замещаемый/отсутствующий сотрудник
        /// </summary>
        public virtual User ReplacedUser { get; set; }
        /// <summary>
        /// Дата начала периода действия временной вакансии.
        /// </summary>
        public virtual DateTime? DateBegin { get; set; }
        /// <summary>
        /// Дата конца периода действия временной вакансии.
        /// </summary>
        public virtual DateTime? DateEnd { get; set; }
        /// <summary>
        /// Вид длительного отсутствия.
        /// </summary>
        public virtual StaffLongAbsencesTypes AbsencesType { get; set; }
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
        /// Дата последнего изменения записи.
        /// </summary>
        public virtual DateTime? EditDate { get; set; }
    }
}
