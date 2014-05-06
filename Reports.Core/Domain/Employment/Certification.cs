using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Core.Domain.Employment
{
    public class Certification : AbstractEntity
    {
        public virtual User User { get; set; }
        public virtual DateTime CertificationDate { get; set; }
        public virtual string CertificateNumber { get; set; }
        public virtual DateTime CertificateDateOfIssue { get; set; }
        public virtual string Order { get; set; }
    }
}
