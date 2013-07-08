using System.Web.Mvc;

namespace WebMvc.Controllers
{
    public class ErrorController : BaseController
    {
        public ActionResult Unauthorized()
        {
            return View();
        }
        public ActionResult DoubleSubmitError()
        {
            return View();
        }
    }
}