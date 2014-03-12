using System;

namespace Reports.Core.Domain
{
    public abstract class AbstractGeneralDocument : AbstractEntityWithVersion
    {
        // Время создания документа
        public virtual DateTime CreateDate { get; set; }
        // Номер документа
        public virtual int Number { get; set; }
        // Пользователь
        public virtual User User { get; set; }
        // Время удаления документа (до удаления документа содержит null)
        public virtual DateTime? DeleteDate { get; set; }
        // ? Время выгрузки в 1С (до выгрузки содержит null)
        public virtual DateTime? SendTo1C { get; set; }
        // ? Удалять документ после выгрузки в 1С
        public virtual bool DeleteAfterSendTo1C { get; set; }
    }
}
