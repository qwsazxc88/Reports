using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class MissionReportCommentDao : DefaultDao<MissionReportComment>, IMissionReportCommentDao
    {
        public MissionReportCommentDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}