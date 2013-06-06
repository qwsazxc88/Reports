using Reports.Core.Domain;

namespace Reports.Core.Dao
{
    public interface IChiefToUserDao : IDao<ChiefToUser>
    {
        bool IsChiefToUserRecordExists(int chiefId, int userId);
    }
}