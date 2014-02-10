using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Core.Domain
{
    /// <summary>
    /// Комментарий к обходному листу при увольнении
    /// </summary>
    public class ClearanceChecklistComment : AbstractEntityWithVersion
    {
        public virtual User User { get; set; }
        public virtual ClearanceChecklist ClearanceChecklist { get; set; }
        public virtual string Comment { get; set; }
        public virtual DateTime DateCreated { get; set; }
    }
}
