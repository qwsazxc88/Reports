using System.Web.Mvc;
using Reports.Core;
using Reports.Presenters.UI.ViewModel;
using WebMvc.Attributes;

namespace WebMvc.Controllers
{
    public class DeductionController : BaseController
    {
        //
        // GET: /Deduction/
        [ReportAuthorize(UserRole.Accountant | UserRole.OutsourcingManager)]
        public ActionResult Index()
        {
            return View( new DeductionsListModel());
        }
    }
}