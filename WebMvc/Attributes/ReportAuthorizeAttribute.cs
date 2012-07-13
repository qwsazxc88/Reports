using System.Linq;
using System.Web.Mvc;
using Reports.Core;

namespace WebMvc.Attributes
{
    public class ReportAuthorizeAttribute : AuthorizeAttribute
    {
        private bool _isAuthorized;
        public const string RedirectUrl = "~/Error/Unauthorized";

        public ReportAuthorizeAttribute(UserRole roles)
        {
            Roles = ReportRoleConstants.Mapper.Aggregate("", (sum, role) => sum + ((role.Key & roles) == role.Key ? role.Value + "," : ""), sum => sum);
        }
        protected override bool AuthorizeCore(System.Web.HttpContextBase httpContext)
        {
            _isAuthorized = base.AuthorizeCore(httpContext);
            return _isAuthorized;
        }
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            if (!_isAuthorized && filterContext.RequestContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.RequestContext.HttpContext.Response.Redirect(RedirectUrl);
            }

        }
    }
}