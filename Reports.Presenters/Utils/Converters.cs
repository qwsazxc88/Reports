using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reports.Core;
using Reports.Presenters.Services;
namespace Reports.Presenters.Utils
{
    public static class Converters
    {
        private static IAuthenticationService authenticationService;
        public static IAuthenticationService AuthenticationService
        {
            get { return authenticationService == null ? (authenticationService = Ioc.Resolve<IAuthenticationService>()) : authenticationService; }
            set { authenticationService = value; }
        }
        public static bool RoleAccessChecker(IList<UserRole> Role)
        {
            var role=AuthenticationService.CurrentUser.UserRole;
            return Role.Contains(role);
        }
    }
}
