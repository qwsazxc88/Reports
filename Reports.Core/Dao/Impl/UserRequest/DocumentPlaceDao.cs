using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reports.Core.Domain;
using Reports.Core.Services;
namespace Reports.Core.Dao.Impl
{
    public class DocumentPlaceDao: DefaultDao<DocumentPlace>, IDocumentPlaceDao
    {
        public DocumentPlaceDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}
