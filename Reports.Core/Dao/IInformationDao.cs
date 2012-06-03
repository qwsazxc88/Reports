using System.Collections.Generic;
using Reports.Core.Domain;

namespace Reports.Core.Dao
{
    public interface IInformationDao : IDao<Information>
    {
        IList<Information> LoadAllSorted();
    }
}