using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Core.Domain
{
    public class StandartRequestEntity: AbstractEntityWithVersion
    {
        #region Основные поля
        /// <summary>
        /// Номер заявки
        /// </summary>
        public virtual int Number { get; set; }
        /// <summary>
        /// Тип заявки
        /// </summary>
        public virtual int Type { get; set; }
        #endregion
        #region Пользователи
        /// <summary>
        /// Создатель заявки
        /// </summary>
        public virtual User Creator { get; set; }
        /// <summary>
        /// Кем отменено
        /// </summary>
        public virtual User RejectedBy { get; set; }
        /// <summary>
        /// Пользователь
        /// </summary>
        public virtual User User { get; set; }
        #endregion
        #region Справочники
        /// <summary>
        /// Причина отмены заявки
        /// </summary>
        public virtual AbstractReferencyBookEntity RejectReason { get; set; }
        /// <summary>
        /// Статус заявки
        /// </summary>
        public virtual AbstractReferencyBookEntity Status { get; set; }
        #endregion
        #region Даты
        /// <summary>
        /// Дата создания заявки
        /// </summary>
        public virtual DateTime CreateDate { get; set; }
        /// <summary>
        /// Дата отправки заявки
        /// </summary>
        public virtual DateTime? SendDate { get; set; }
        /// <summary>
        /// Дата отмены заявки
        /// </summary>
        public virtual DateTime? RejectDate { get; set; }
        /// <summary>
        /// Дата выгрузки в 1С
        /// </summary>
        public virtual DateTime? SendTo1C { get; set; }
        /// <summary>
        /// Дата удаления заявки
        /// </summary>
        public virtual DateTime? DeleteDate { get; set; }
        #endregion
    }
}
