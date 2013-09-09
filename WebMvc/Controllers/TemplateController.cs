using System.Web.Mvc;
using Reports.Presenters.UI.ViewModel;

namespace WebMvc.Controllers
{
    [Authorize]
    public class TemplateController : BaseController
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View(new TemplatesListModel());
        }
    }
}