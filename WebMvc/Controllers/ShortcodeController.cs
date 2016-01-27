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

        public RedirectToRouteResult Index(string id)
        {
            
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
