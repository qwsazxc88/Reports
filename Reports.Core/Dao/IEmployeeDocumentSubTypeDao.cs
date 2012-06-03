using Reports.Core.Domain;

namespace Reports.Core.Dao
{
    public interface IEmployeeDocumentSubTypeDao : IDao<EmployeeDocumentSubType>
    {
        bool IsSubTypeWithNameAndOtherIdExists(string name, int id, int typeId);
    }
}