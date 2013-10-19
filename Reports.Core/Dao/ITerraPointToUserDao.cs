using Reports.Core.Domain;

namespace Reports.Core.Dao
{
    public interface ITerraPointToUserDao : IDao<TerraPointToUser>
    {
        TerraPointToUser FindByUserId(int userId);
    }
}