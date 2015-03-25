using System;
//using Reports.Core.Domain;

namespace Reports.Core.Domain
{
    public class EmploymentCandidateComment : AbstractEntityWithVersion
    {
        public virtual int? UserId { get; set; } //ok
        public virtual int? CandidateId { get; set; } //ok
        public virtual int? CommentTypeId { get; set; } //ok
        public virtual DateTime? CreatedDate { get; set; } //ok
        public virtual string Comment { get; set; } //ok
    }
}
