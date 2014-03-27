using Reports.Core.Enum;
using Reports.Presenters.UI.ViewModel;

namespace Reports.Presenters.UI.Bl
{
    public interface IAppointmentBl : IBaseBl
    {
        AppointmentListModel GetAppointmentListModel();

        AppointmentEditModel GetAppointmentEditModel(int id);

        CommentsModel GetCommentsModel(int id, RequestTypeEnum typeId);
        bool SaveComment(SaveCommentModel model);

        void ReloadDictionaries(AppointmentEditModel model);
        bool CheckDepartment(int departmentId);
        bool SaveAppointmentEditModel(AppointmentEditModel model, out string error);
    }
}