using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Reports.Presenters.UI.ViewModel;

namespace WebMvc.Controllers
{
    public class TemplateController : Controller
    {
         [HttpGet]
        public ActionResult Index()
        {
            return View(new TemplatesListModel());
        }

    }
}
