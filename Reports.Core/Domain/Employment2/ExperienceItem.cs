using System;

namespace Reports.Core.Domain
{
    public class ExperienceItem : AbstractEntityWithVersion
    {
        public virtual DateTime? BeginningDate { get; set; }
        public virtual DateTime? EndDate { get; set; }
        public virtual string Company { get; set; }
        public virtual string Position { get; set; }
        public virtual string CompanyContacts { get; set; }
    }
}
