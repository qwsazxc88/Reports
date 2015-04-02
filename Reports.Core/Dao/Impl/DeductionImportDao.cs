using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reports.Core.Domain;
using Reports.Core.Dto;
using Reports.Core.Services;
using Reports.Core.Domain;
namespace Reports.Core.Dao.Impl
{
    public class DeductionImportDao: DefaultDao<DeductionImport>, IDeductionImportDao{
        public DeductionImportDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    
    }
}
