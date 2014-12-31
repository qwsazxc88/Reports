using System;

namespace Reports.Core.Domain
{
    /// <summary>
    /// Комментарий к акту ГПД
    /// </summary>
    public class GpdActComment : AbstractEntityWithVersion
    {
        public virtual User UserId { get; set; }
        public virtual GpdAct GpdActs { get; set; }
        public virtual string Comment { get; set; }
        public virtual DateTime CreateDate { get; set; }
    }
}