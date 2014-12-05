using System.Collections.Generic;
using Reports.Core.Domain;

namespace Reports.Core.Dao
{
    public interface IHelpQuestionSubtypeDao : IDaoSorted<HelpQuestionSubtype>
    {
        List<HelpQuestionSubtype> LoadForTypeIdSortedByOrder(int typeId);
    }
}