using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Reports.Core;
using Reports.Core.Dto;
using Reports.Presenters.UI.Bl;
using Reports.Presenters.UI.ViewModel;
using WebMvc.Attributes;
using System.Text.RegularExpressions;
namespace WebMvc.Controllers
{
    [Authorize]
    public class ShortcodeController : BaseController
    {
        protected IShortCodeBl shortCodeBl;
        public IShortCodeBl ShortCodeBl
        {
            get
            {
                shortCodeBl = Ioc.Resolve<IShortCodeBl>();
                return Validate.Dependency(shortCodeBl);
            }
        }
        protected ILoginBl loginBl;
        public ILoginBl LoginBl
        {
            get
            {
                loginBl = Ioc.Resolve<ILoginBl>();
                return Validate.Dependency(loginBl);
            }
        }
        public RedirectToRouteResult Index(string id,int roleId=0)
        {
            if (roleId > 0)
                ShortCodeBl.SetUserRole(roleId);
            
            string regex=@"(?<id>[0-9]+)(?<type>\w+)";
            var m = Regex.Match(id, regex);
            if (m.Success)
            {
                int ids=int.Parse(m.Groups["id"].Value);
                string type = m.Groups["type"].Value.ToLower();
                RouteVal route = ShortCodeBl.GetRouteValues(ids, type);
                return RedirectToAction(route.Action, route.Controller, route.Values);
            } 
            else return RedirectToAction("LogOn", "Account");                            
        }

    }
}
