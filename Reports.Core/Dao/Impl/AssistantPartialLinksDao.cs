using System;
using Reports.Core.Services;
using Reports.Core.Domain;

namespace Reports.Core.Dao.Impl
{
    /// <summary>
    /// Связи помощников с разделами приложения.
    /// </summary>
    public class AssistantPartialLinksDao : DefaultDao<AssistantPartialLinks>, IAssistantPartialLinksDao
    {
        public AssistantPartialLinksDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}
