using Reports.Presenters.UI.ViewModel;

namespace Reports.Presenters.UI.Bl
{
    public interface ILoginBl : IBaseBl
    {
        void InitForm(LogOnModel model);
        void OnLogin(LogOnModel model);
        void OnChangePassword(ChangePasswordModel model);
        void CheckAndSetUserId(ChangePasswordModel model);
        void OnLoginRecovery(LoginRecoveryModel model);
        bool SendToSupport(SendToSupportModel model);
        void LogOff();
        void OnChangePwd(ChangePwdModel model);
        ChangeRoleModel GetChangeRoleModel();
        void SetUserRole(ChangeRoleModel model);
    }
}