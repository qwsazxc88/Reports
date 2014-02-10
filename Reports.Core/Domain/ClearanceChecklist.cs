using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Core.Domain
{
    /// <summary>
    /// Обходной лист при увольнении
    /// </summary>
    public class ClearanceChecklist : AbstractEntityWithVersion
    {
        // Родительское увольнение
        public virtual Dismissal Dismissal { get; set; }
        // Согласования
        public virtual IList<ClearanceChecklistApproval> Approvals { get; set; }
        // Комментарии
        public virtual IList<ClearanceChecklistComment> Comments { get; set; }
    }
}
