using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Core.Dto.Employment2
{
    public class EmploymentCandidateDocNeededDto
    {
        public int Id { get; set; }
        public int CandidateId { get; set; }
        public int CreatorId { get; set; }
        public int EditorId { get; set; }
        public int DocTypeId { get; set; }
        public bool IsNeeded { get; set; }
        public DateTime? DateCreate { get; set; }
        public DateTime? DateEdit { get; set; }
    }
}
