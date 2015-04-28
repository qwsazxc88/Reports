using System.Web.Mvc;
using Reports.Presenters.UI.ViewModel;
using Reports.Core;
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

       
        [HttpGet]
        
        public JsonResult News(int page=0)
        {
            var result = Ioc.Resolve<Reports.Core.Dao.INewsDao>().GetNews(page, 10);
            return Json(result,JsonRequestBehavior.AllowGet);
        }
    }
}