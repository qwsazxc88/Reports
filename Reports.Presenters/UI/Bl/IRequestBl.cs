using Reports.Presenters.UI.ViewModel;

namespace Reports.Presenters.UI.Bl
{
    public interface IRequestBl : IBaseBl
    {
        CreateRequestModel GetCreateRequestModel(int? userId);
        VacationListModel GetVacationListModel();
        void SetVacationListModel(VacationListModel model);


        VacationEditModel GetVacationEditModel(int id, int userId);
        bool SaveVacationEditModel(VacationEditModel model,out string error);
        void ReloadDictionariesToModel(VacationEditModel model);

        RequestCommentsModel GetCommentsModel(int id, int typeId);
        bool SaveComment(SaveCommentModel model);

        AbsenceListModel GetAbsenceListModel();
        void SetAbsenceListModel(AbsenceListModel model);

        AbsenceEditModel GetAbsenceEditModel(int id, int userId);
        void ReloadDictionariesToModel(AbsenceEditModel model);
        bool SaveAbsenceEditModel(AbsenceEditModel model, out string error);
    }
}