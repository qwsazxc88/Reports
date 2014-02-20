using System;
using System.Collections.Generic;
using Iesi.Collections.Generic;
using Reports.Core.Domain;
using Reports.Core.Enum;

namespace Reports.Core.Domain
{
    /// <summary>
    /// Обходной лист при увольнении
    /// </summary>
    public class ClearanceChecklist : AbstractGeneralDocument
    {
        // Родительское увольнение
        public virtual Dismissal Dismissal { get; set; }
        // Согласования
        public virtual IList<ClearanceChecklistApproval> Approvals { get; set; }
    }
}
