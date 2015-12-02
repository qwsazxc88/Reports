using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reports.Core.Domain;
using Reports.Core.Services;
namespace Reports.Core.Dao.Impl
{
    public class DocumentMovementsDao: DefaultDao<DocumentMovements>, IDocumentMovementsDao
    {
        public DocumentMovementsDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
    public class DocumentMovements_DocTypesDao : DefaultDao<DocumentMovements_DocTypes>, IDocumentMovements_DocTypesDao
    {
        public DocumentMovements_DocTypesDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
    public class DocumentMovements_SelectedDocsDao : DefaultDao<DocumentMovements_SelectedDocs>, IDocumentMovements_SelectedDocsDao
    {
        public DocumentMovements_SelectedDocsDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
    public class DocumentMovementsRoleRecordsDao : DefaultDao<DocumentMovementsRoleRecords>, IDocumentMovementsRoleRecordsDao
    {
        public DocumentMovementsRoleRecordsDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}
