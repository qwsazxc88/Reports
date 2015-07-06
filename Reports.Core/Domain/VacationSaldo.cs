using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Core.Domain
{
    public class VacationSaldo:AbstractEntity
    {
        public virtual decimal SaldoPrimary { get; set; }
        public virtual decimal SaldoAdditional { get; set; }
        public virtual User User { get; set; }
        public virtual DateTime Date { get; set; }
    }
}
