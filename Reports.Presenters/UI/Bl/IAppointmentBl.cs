using Reports.Presenters.UI.ViewModel;

namespace Reports.Presenters.UI.Bl
{
    public interface IAppointmentBl : IBaseBl
    {
        AppointmentListModel GetAppointmentListModel();
    }
}