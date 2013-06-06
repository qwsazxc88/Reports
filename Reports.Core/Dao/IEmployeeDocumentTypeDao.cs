using System.Collections.Generic;
using Reports.Core.Domain;

namespace Reports.Core.Dao
{
    public interface IEmployeeDocumentTypeDao : IDao<EmployeeDocumentType>
    {
        //IList<EmployeeDocumentType> LoadAllSorted();
        IList<EmployeeDocumentSubType> LoadAllSubtype();
        bool IsTypeWithNameAndOtherIdExists(string name, int id);

        IList<EmployeeDocumentSubType> LoadSubtypeForType(int typeId);
        //EmployeeDocumentSubType LoadSubtype(int subtypeId);
    }
}