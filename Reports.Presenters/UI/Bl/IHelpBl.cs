using Reports.Presenters.UI.ViewModel;

namespace Reports.Presenters.UI.Bl
{
    public interface IHelpBl : IBaseBl
    {
        HelpVersionsListModel GetVersionsModel();
    }
}