using System.Web.Mvc;
using Reports.Presenters.UI.ViewModel;

namespace WebMvc.Controllers
{
    [Authorize]
    public class InfoController : BaseController
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View(new InfoModel());
        }
    }
}