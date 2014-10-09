using System.Collections.Generic;
using System.Web.Mvc;
using Reports.Core;
using Reports.Core.Dto;
using Reports.Presenters.UI.Bl;
using Reports.Presenters.UI.ViewModel;

namespace WebMvc.Controllers
{
    [Authorize]
    public class HelpController : BaseController
    {
        protected IHelpBl helpBl;
        public IHelpBl HelpBl
        {
            get
            {
                helpBl = Ioc.Resolve<IHelpBl>();
                return Validate.Dependency(helpBl);
            }
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Version()
        {
            HelpVersionsListModel model = new HelpVersionsListModel
                                              {
                                                  IsAddAvailable = true,
                                                  Versions = new List<HelpVersionDto>()
                                              };
            return View(model);
        }
    }
}