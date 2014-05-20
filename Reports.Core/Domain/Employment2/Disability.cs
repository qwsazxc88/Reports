using System;

namespace Reports.Core.Domain
{
    public class Disability : AbstractEntityWithVersion
    {
        public virtual string CertificateSeries { get; set; }
        public virtual string CertificateNumber { get; set; }
        public virtual DateTime? CertificateDateOfIssue { get; set; }
        public virtual string DisabilityDegree { get; set; }
        public virtual DateTime? CertificateExpirationDate { get; set; }
    }
}