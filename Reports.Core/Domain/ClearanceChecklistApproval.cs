using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Core.Domain
{
    public class ClearanceChecklistApproval : AbstractEntityWithVersion
    {
        // Родительский обходной лист
        public virtual Dismissal Dismissal { get; set; }

        // Расширенная роль для согласования
        public virtual ClearanceChecklistRole ClearanceChecklistRole { get; set; }

        // Согласующий; заполняется по факту согласования
        public virtual User ApprovedBy { get; set; }
        public virtual DateTime? ApprovalDate { get; set; }

        public virtual string Comment { get; set; }
    }
}
