using System;

namespace Reports.Core.Domain
{
    public class Certification : AbstractEntityWithVersion
    {
        public virtual DateTime? CertificationDate { get; set; }
        public virtual string CertificateNumber { get; set; }
        public virtual DateTime? CertificateDateOfIssue { get; set; }
        public virtual string InitiatingOrder { get; set; }
    }
}
