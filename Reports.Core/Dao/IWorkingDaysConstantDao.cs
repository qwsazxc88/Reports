using System.Collections.Generic;
using Reports.Core.Domain;

namespace Reports.Core.Dao
{
    public interface IWorkingDaysConstantDao : IDao<WorkingDaysConstant>
    {
        List<WorkingDaysConstant> LoadDataForYear(int year);
        WorkingDaysConstant LoadDataForMonth(int month, int year);
    }
}