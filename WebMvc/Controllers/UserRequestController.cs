using System.Web.Mvc;
using Reports.Core;
using Reports.Presenters.UI.Bl;
using Reports.Presenters.UI.ViewModel;

namespace WebMvc.Controllers
{
    public class UserRequestController : BaseController
    {
        protected IRequestBl requestBl;
        public IRequestBl RequestBl
        {
            get
            {
                requestBl = Ioc.Resolve<IRequestBl>();
                return Validate.Dependency(requestBl);
            }
        }
        //
        // GET: /UserRequestController/
         [HttpGet]
         public ActionResult CreateRequest()
         {
             int? userId = new int?();
             CreateRequestModel model = RequestBl.GetCreateRequestModel(userId);
             return View(model);
         }
     }
}