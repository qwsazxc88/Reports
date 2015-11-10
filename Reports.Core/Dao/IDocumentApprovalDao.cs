using System;
using System.Collections.Generic;
using Reports.Core.Domain;

namespace Reports.Core.Dao
{
    /// <summary>
    /// Согласование документов.
    /// </summary>
    public interface IDocumentApprovalDao : IDao<DocumentApproval>
    {
        /// <summary>
        /// Достаем записи состояния согласования для заявки.
        /// </summary>
        /// <param name="DocId">Id заявки.</param>
        /// <param name="ApprovalType">Вид документа/заявки</param>
        /// <returns></returns>
        IList<DocumentApproval> GetDocumentApproval(int DocId, int ApprovalType);
    }
}
