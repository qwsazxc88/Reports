using Reports.Core.Domain;

namespace Reports.Core.Dao
{
    public interface IRequestNextNumberDao : IDao<RequestNextNumber>
    {
        int GetNextNumberForType(int typeId);
    }
}