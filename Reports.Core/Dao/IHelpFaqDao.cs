using System.Collections.Generic;
using Reports.Core.Domain;

namespace Reports.Core.Dao
{
    public interface IHelpFaqDao : IDao<HelpFaq>
    {
        List<HelpFaq> LoadAllSortedByQuestion();
    }
}