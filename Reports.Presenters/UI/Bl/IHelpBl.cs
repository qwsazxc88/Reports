using Reports.Core.Dto;
using Reports.Presenters.UI.ViewModel;

namespace Reports.Presenters.UI.Bl
{
    public interface IHelpBl : IBaseBl
    {
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
    }
}