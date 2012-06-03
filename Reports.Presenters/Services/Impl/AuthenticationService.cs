using System;
using System.Reflection;
using System.Web;
using System.Web.Security;
using log4net;
using Reports.Core;
using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Presenters.Services.Impl
{
    public class AuthenticationService : IAuthenticationService
    {

        #region Fields
        protected static ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public const string SessionStorageKey = "UserInfo";
        public const string ChangePasswordCookie = "ChangePasswordCookie";
        private IHttpContextService _httpContextService;

        #endregion

        #region Dependencies

        public IHttpContextService HttpContextService
        {
            get { return Validate.Dependency(_httpContextService); }
            set { _httpContextService = value; }
        }

        #endregion

        #region IAuthenticationService Members
        public void setAuthTicket(IUser user)
        {
            string userData = user.Serialize();
            var authTicket = new FormsAuthenticationTicket(
                                                          1,
                                                          user.Name,  //user id
                                                          DateTime.Now,
                                                          DateTime.Now.AddMinutes(960),  // expiry
                                                          false,  //true to remember
                                                          userData, //roles,
                                                          FormsAuthentication .FormsCookiePath
                                                        );

            //encrypt the ticket and add it to a cookie
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(authTicket));
            HttpContextService.Response.Cookies.Add(cookie);
        }
        public void SetChangePasswordCookie(IUser user)
        {
            string userData = user.Serialize();
            var authTicket = new FormsAuthenticationTicket(
                                                          1,
                                                          user.Name,  //user id
                                                          DateTime.Now,
                                                          DateTime.Now.AddMinutes(5),  // expiry
                                                          false,  //true to remember
                                                          userData, //roles,
                                                          string.Empty 
                                                        );

            //encrypt the ticket and add it to a cookie
            HttpCookie cookie = new HttpCookie(ChangePasswordCookie, FormsAuthentication.Encrypt(authTicket));
            HttpContextService.Response.Cookies.Add(cookie);
        }
        public int? GetUserIdFromChangePasswordCookue()
        {
            try
            {
                HttpCookie passwordCookie = HttpContext.Current.Request.Cookies.Get(ChangePasswordCookie);
                if(passwordCookie == null)
                    return null;
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(passwordCookie.Value);
                if(ticket == null)
                    return null;
                if(ticket.Expired)
                {
                    Log.Error("Change password cookue ticket is expired.");
                    return null;
                }
                IUser user = UserDto.Deserialize(ticket.UserData);
                return user == null ? null : new int?(user.Id);
            }
            catch (Exception ex)
            {
                Log.Error("Exception:",ex.GetBaseException());
                return null;
            }

        }
        public void ClearChangePasswordCookue()
        {
            HttpCookie cookie = new HttpCookie(ChangePasswordCookie,null);
            HttpContextService.Response.Cookies.Add(cookie);
        }



        public IUser CurrentUser
        {
            get
            {
                if(HttpContextService.User != null && HttpContextService.User.Identity.IsAuthenticated)
                {
                    FormsIdentity identity = (FormsIdentity)HttpContext.Current.User.Identity;
                    FormsAuthenticationTicket ticket = identity.Ticket; 
                    return UserDto.Deserialize(ticket.UserData);
                }
                return null;
            }
//            set { _httpContextService.Items[SessionStorageKey] = value; }
        }
        public IUser CreateUser(User user)
        {
            return UserDto.CreateUser(user);
        }

        #endregion
    }
}