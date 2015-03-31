using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reports.Core.Domain;
using Reports.Core.Dto.Employment2;

namespace Reports.Core.Dao
{
    public interface IEmploymentCandidateCommentDao : IDao<EmploymentCandidateComment>
    {
        IList<EmploymentCandidateCommentDto> GetComments(int CandidateId, int CommentTypeId);
    }
}
