using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class HelpQuestionTypeDao : DefaultDaoSorted<HelpQuestionType>, IHelpQuestionTypeDao
    {
        public HelpQuestionTypeDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}