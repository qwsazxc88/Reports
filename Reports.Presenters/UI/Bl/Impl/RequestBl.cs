using Reports.Core;
using Reports.Core.Domain;
using Reports.Presenters.UI.ViewModel;

namespace Reports.Presenters.UI.Bl.Impl
{
    public class RequestBl : BaseBl, IRequestBl
    {
        public CreateRequestModel GetCreateRequestModel(int? userId)
        {
            UserRole role;
            if(userId == null)
            {
                userId = AuthenticationService.CurrentUser.Id;
                role = AuthenticationService.CurrentUser.UserRole;
            }
            else
            {
                User user = UserDao.Load(userId.Value);
                role = (UserRole)user.Role.Id;
            }

            CreateRequestModel model = new CreateRequestModel
                                           {
                                               IsUserVisible = role != UserRole.Employee
                                           };
            //model.RequestTypes =  GetRequestTypes();
            return model;
        }
    }

}