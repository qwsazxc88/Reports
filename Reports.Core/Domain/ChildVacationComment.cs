using System;

namespace Reports.Core.Domain
{
    /// <summary>
    /// Комментарий к отпуску по уходу за ребенком
    /// </summary>
    public class ChildVacationComment : AbstractEntityWithVersion
    {
        public virtual User User { get; set; }
        public virtual ChildVacation ChildVacation { get; set; }
        public virtual string Comment { get; set; }
        public virtual DateTime DateCreated { get; set; }
    }
}