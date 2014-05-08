using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Core.Domain.Employment2
{
    public class Disability : AbstractEntity
    {
        public virtual User User { get; set; }
        public virtual string CertificateSeries { get; set; }
        public virtual string CertificateNumber { get; set; }
        public virtual DateTime DateOfIssue { get; set; }
        public virtual string DisabilityDegree { get; set; }
        public virtual DateTime CerificateExpirationDate { get; set; }
    }
}
