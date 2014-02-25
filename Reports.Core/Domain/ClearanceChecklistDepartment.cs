using System;

namespace Reports.Core.Domain
{
    public class ClearanceChecklistDepartment : AbstractEntity
    {
        public virtual string Name { get; set; }
        public virtual ExtendedRole ExtendedRole { get; set; }
        public virtual int? DaysForApproval { get; set; }
    }
}
