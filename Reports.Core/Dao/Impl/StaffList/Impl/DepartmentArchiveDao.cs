using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    /// <summary>
    /// Архив справочника подразделений.
    /// </summary>
    public class DepartmentArchiveDao : DefaultDao<DepartmentArchive>, IDepartmentArchiveDao
    {
        public DepartmentArchiveDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}
