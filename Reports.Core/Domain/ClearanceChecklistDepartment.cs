using System;

namespace Reports.Core.Domain
{
    public class ClearanceChecklistDepartment : AbstractEntity
    {
        public virtual string Name { get; set; }
        public virtual ClearanceChecklistRole ClearanceChecklistRole { get; set; }
        public virtual int? DaysForApproval { get; set; }
    }
}
