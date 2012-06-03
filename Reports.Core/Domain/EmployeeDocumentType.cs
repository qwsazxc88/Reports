using System.Collections.Generic;

namespace Reports.Core.Domain
{
    public class EmployeeDocumentType : AbstractEntityWithVersion
    {
        public virtual string Name { get; set; }
        public virtual IList<EmployeeDocumentSubType> SubTypes { get; set; }
    }
}