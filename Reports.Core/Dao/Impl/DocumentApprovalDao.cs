using System.Collections.Generic;
using Reports.Core.Dto;
using Reports.Core.Domain;
using Reports.Core.Services;
using NHibernate.Transform;
using NHibernate;
using NHibernate.Criterion;
using System;
using System.Linq;
using NHibernate.Linq;


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

        /// <summary>
        /// Достаем записи состояния согласования для заявки.
        /// </summary>
        /// <param name="DocId">Id заявки.</param>
        /// <param name="ApprovalType">Вид документа/заявки</param>
        /// <returns></returns>
        public IList<DocumentApproval> GetDocumentApproval(int DocId, int ApprovalType)
        {
            return Session.Query<DocumentApproval>().Where(x => x.DocId == DocId && x.ApprovalType == ApprovalType).ToList();
        }
    }
}
