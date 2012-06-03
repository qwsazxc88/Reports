using Reports.Presenters.Services;
using Reports.Presenters.UI.ViewModel;

namespace Reports.Presenters.UI.Bl
{
    public interface IBaseBl
    {
        IUser CurrentUser { get; }
    }
}