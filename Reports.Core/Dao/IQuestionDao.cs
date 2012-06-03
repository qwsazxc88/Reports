using System;
using System.Collections.Generic;
using System.Text;
using Reports.Core.Domain;
using Reports.Core.Dto;
using Reports.Core.Filters;

namespace Reports.Core.Dao
{
    public interface IQuestionDao : IUsedDao<Question>
    {
        IList<QuestionDto> FindByFilterForUser(QuestionFilter filter, out int count);
    }
}
