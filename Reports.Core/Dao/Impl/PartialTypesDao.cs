using System;
using Reports.Core.Services;
using Reports.Core.Domain;

namespace Reports.Core.Dao.Impl
{
    /// <summary>
    /// Справочник разделов приложения.
    /// </summary>
    public class PartialTypesDao : DefaultDao<PartialTypes>, IPartialTypesDao
    {
        public PartialTypesDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}
