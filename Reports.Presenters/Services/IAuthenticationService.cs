using Reports.Core;
using Reports.Core.Domain;

namespace Reports.Presenters.Services
{
    public interface IAuthenticationService
    {
        IUser CurrentUser { get; }
        IUser CreateUser(User user,UserRole roleId);
        void setAuthTicket(IUser user);
        void SetChangePasswordCookie(IUser user);
        int? GetUserIdFromChangePasswordCookue();
        void ClearChangePasswordCookue();
    }
}
