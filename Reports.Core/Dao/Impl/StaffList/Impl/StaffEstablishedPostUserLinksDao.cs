using System.Collections.Generic;
using Reports.Core.Dto;
using Reports.Core.Domain;
using Reports.Core.Services;


namespace Reports.Core.Dao.Impl
{
    /// <summary>
    /// Связи штатных единиц с сотрудниками (штатная расстановка)
    /// </summary>
    public class StaffEstablishedPostUserLinksDao : DefaultDao<StaffEstablishedPostUserLinks>, IStaffEstablishedPostUserLinksDao
    {
        public StaffEstablishedPostUserLinksDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}
