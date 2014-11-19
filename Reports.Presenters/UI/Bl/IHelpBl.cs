using Reports.Core.Dto;
using Reports.Core.Enum;
using Reports.Presenters.UI.ViewModel;

namespace Reports.Presenters.UI.Bl
{
    public interface IHelpBl : IBaseBl
    {

        HelpServiceRequestsListModel GetServiceRequestsList();
        void SetServiceRequestsListModel(HelpServiceRequestsListModel model, bool hasError);

        CreateHelpServiceRequestModel GetCreateHelpServiceRequestModel();
        HelpServiceRequestEditModel GetServiceRequestEditModel(int id, int? userId);
        void GetDictionariesStates(int typeId, HelpServiceDictionariesStatesModel model);
        void ReloadDictionariesToModel(HelpServiceRequestEditModel model);
        bool SaveServiceRequestEditModel(HelpServiceRequestEditModel model, UploadFileDto fileDto, out string error);

        CommentsModel GetCommentsModel(int id, RequestTypeEnum typeId);
        bool SaveComment(SaveCommentModel model, RequestTypeEnum type);

        HelpServiceQuestionsListModel GetServiceQuestionsListModel();
        HelpQuestionEditModel GetHelpQuestionEditModel(int id, int? userId);
        void GetSubtypesForType(int typeId, HelpQuestionSubtypesModel model);
        void ReloadDictionariesToModel(HelpQuestionEditModel model);
        bool SaveHelpQuestionEditModel(HelpQuestionEditModel model, out string error);
        HelpQuestionRedirectModel GetQuestionRedirectModel(int id);


        HelpVersionsListModel GetVersionsModel();
        HelpEditVersionModel GetEditVersionModel(int id);
        bool SaveVersion(HelpSaveVersionModel model);
        bool DeleteVersion(DeleteAttacmentModel model);

        HelpFaqListModel GetFaqModel();
        HelpEditFaqModel GetEditQuestionModel(int id);
        bool SaveFaq(HelpSaveFaqModel model);
        bool DeleteFaq(DeleteAttacmentModel model);

        HelpTemplateListModel GetTemplateModel();
        HelpTemplateEditModel GetTemplateEditModel(int id);
        bool SaveTemplate(SaveAttacmentModel model);
        bool SaveTemplateName(SaveAttacmentModel model);
    }
}