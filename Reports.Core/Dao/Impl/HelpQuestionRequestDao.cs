using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class HelpQuestionRequestDao : DefaultDao<HelpQuestionRequest>, IHelpQuestionRequestDao
    {
        public HelpQuestionRequestDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}