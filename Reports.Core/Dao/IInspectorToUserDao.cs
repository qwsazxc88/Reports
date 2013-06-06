using Reports.Core.Domain;

namespace Reports.Core.Dao
{
    public interface IInspectorToUserDao : IDao<InspectorToUser>
    {
        bool IsInspectorToUserRecordExists(int inspectorId, int userId);
    }
}