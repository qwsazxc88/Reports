using System.Web.Mvc;

namespace WebMvc.Controllers
{
    [Authorize]
    public class HelpController : BaseController
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}