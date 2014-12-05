using System.Collections.Generic;
using Reports.Core.Domain;

namespace Reports.Core.Dao
{
    public interface IHelpVersionDao : IDao<HelpVersion>
    {
        List<HelpVersion> LoadAllSortedByDate();
    }
}