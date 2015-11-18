using System;
using Reports.Core.Services;
using Reports.Core.Domain;

namespace Reports.Core.Dao.Impl
{
    /// <summary>
    /// Помощники.
    /// </summary>
    public class AssistansDao : DefaultDao<Assistans>, IAssistansDao
    {
        public AssistansDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}
