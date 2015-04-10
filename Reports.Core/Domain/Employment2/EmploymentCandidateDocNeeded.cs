using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reports.Core.Enum;
using Reports.Core.Dto.Employment2;

namespace Reports.Core.Domain
{
    public class EmploymentCandidateDocNeeded : AbstractEntityWithVersion
    {
        public virtual EmploymentCandidate Candidate { get; set; }
        public virtual User Creator { get; set; }
        public virtual User Editor { get; set; }
        public virtual int DocTypeId { get; set; }
        public virtual bool IsNeeded { get; set; }
        public virtual DateTime? DateCreate { get; set; }
        public virtual DateTime? DateEdit { get; set; }
    }
}
