using System;
using Reports.Core.Services;
using Reports.Core.Domain;


namespace Reports.Core.Dao.Impl
{
    /// <summary>
    /// Согласование документов.
    /// </summary>
    public class DocumentApprovalDao : DefaultDao<DocumentApproval>, IDocumentApprovalDao
    {
        public DocumentApprovalDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}
