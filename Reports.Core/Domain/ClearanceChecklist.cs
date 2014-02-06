using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Core.Domain
{
    /// <summary>
    /// Обходной лист при увольнении
    /// </summary>
    class ClearanceChecklist : AbstractEntityWithVersion
    {
        public virtual Dismissal Dismissal { get; set; }
        public virtual IList<ClearanceChecklistComment> Comments { get; set; }
    }
}
