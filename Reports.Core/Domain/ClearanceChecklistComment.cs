using System;

namespace Reports.Core.Domain
{
    /// <summary>
    /// Комментарий к обходному листу
    /// </summary>
    public class ClearanceChecklistComment : AbstractEntityWithVersion
    {
        public virtual User User { get; set; }
        public virtual Dismissal ClearanceChecklist { get; set; }
        public virtual string Comment { get; set; }
        public virtual DateTime DateCreated { get; set; }
    }
}