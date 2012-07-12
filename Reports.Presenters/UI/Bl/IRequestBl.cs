using Reports.Presenters.UI.ViewModel;

namespace Reports.Presenters.UI.Bl
{
    public interface IRequestBl : IBaseBl
    {
        CreateRequestModel GetCreateRequestModel(int? userId);
        VacationListModel GetVacationListModel();
        void SetVacationListModel(VacationListModel model);

        VacationEditModel GetVacationEditModel(int id, int userId);
        RequestCommentsModel GetCommentsModel(int id, int typeId);
        bool SaveComment(SaveCommentModel model);
    }
}