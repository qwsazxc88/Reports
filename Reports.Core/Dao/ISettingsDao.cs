using Reports.Core.Domain;

namespace Reports.Core.Dao
{
    public interface ISettingsDao : IDao<Settings>
    {
        Settings LoadFirst();
    }
}