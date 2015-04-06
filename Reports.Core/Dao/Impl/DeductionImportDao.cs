using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reports.Core.Domain;
using Reports.Core.Dto;
using Reports.Core.Services;
using Reports.Core.Domain;
using NHibernate.Criterion;
namespace Reports.Core.Dao.Impl
{
    public class DeductionImportDao: DefaultDao<DeductionImport>, IDeductionImportDao{
        public DeductionImportDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
        public bool IsFileExist(string hash)
        {
            var crit=Session.CreateCriteria(typeof(DeductionImport));
            crit.Add(Restrictions.Eq("InputFileHash",hash));
            return (crit.List().Count > 0);
        }
        public DeductionImport LoadByHash(string hash)
        {
            var crit = Session.CreateCriteria(typeof(DeductionImport));
            crit.Add(Restrictions.Eq("InputFileHash", hash));
            var res = crit.List<DeductionImport>();
            if (res.Count > 0) return res[0];
            else return null;
                 
        }
    
    }
}
