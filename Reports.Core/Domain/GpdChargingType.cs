using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Core.Domain
{
    public class GpdChargingType:AbstractEntity
    {
        public virtual string Name { get; set; }
        public virtual int Code { get; set; }
    }
}
