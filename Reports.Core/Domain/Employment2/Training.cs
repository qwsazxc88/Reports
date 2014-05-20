using System;

namespace Reports.Core.Domain
{
    public class Training : AbstractEntityWithVersion
    {
        public virtual string CertificateIssuedBy { get; set; }
        public virtual string Series { get; set; }
        public virtual string Number { get; set; }
        public virtual DateTime? BeginningDate { get; set; }
        public virtual DateTime? EndDate { get; set; }
        public virtual string Speciality { get; set; }
    }
}