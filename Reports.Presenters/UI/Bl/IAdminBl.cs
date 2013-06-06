using Reports.Presenters.UI.ViewModel;

namespace Reports.Presenters.UI.Bl
{
    public interface IAdminBl : IBaseBl
    {
        void GetUserListModel(UserListModel model);
        void GetUserEditModel(UserEditModel model);
        void SaveUser(UserEditModel model);
        void SetStaticToModel(UserEditModel model,bool setStatic);
        void SetModel(DocumentTypeListModel model);

        void GetEditTypeModel(EditTypeModel model);
        bool SaveType(EditTypeModel model);

        void SetModel(DocumentSubtypeListModel model);
        void GetEditSubTypeModel(EditSubTypeModel model);
        bool SaveSubType(EditSubTypeModel model);

        void SetModel(SettingsModel model);
        void SaveSettings(SettingsModel model);
        bool DeleteEmployees(DeleteEmployeesModel model);

        void SetModel(ActionListModel model);
        void ImportFile(ActionListModel model);
        void AutoImport(AutoImportModel model);
        void ExportFile(ActionListModel model);
        void AutoExport(AutoImportModel model);

        void GetInformationListModel(InformationListModel model);
        void GetEditInfoModel(EditInfoModel model);
        bool SaveInfo(EditInfoModel model);
    }
}