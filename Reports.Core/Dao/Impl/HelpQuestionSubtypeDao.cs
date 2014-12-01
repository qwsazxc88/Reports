using System.Collections.Generic;
using System.Linq;
using NHibernate.Linq;
using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class HelpQuestionSubtypeDao : DefaultDaoSorted<HelpQuestionSubtype>, IHelpQuestionSubtypeDao
    {
        public HelpQuestionSubtypeDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
        public virtual List<HelpQuestionSubtype> LoadForTypeIdSortedByOrder(int typeId)
        {
            return Session.Query<HelpQuestionSubtype>()
                .Where(x => x.Type.Id == typeId)
                .OrderBy(a => a.SortOrder)
                .ToList();
        }
    }
}