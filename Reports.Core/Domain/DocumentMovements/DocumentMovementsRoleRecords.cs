using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Core.Domain
{
    public class DocumentMovementsRoleRecords:AbstractEntity
    {
        public virtual User User { get; set; }
    }
}
