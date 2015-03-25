using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Core.Dto.Employment2
{
    public class EmploymentCandidateCommentDto
    {
        public string Creator { get; set; }
        public string CreatorPosition { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Comment { get; set; }
        public int CommentTypeId { get; set; }
    }
}
