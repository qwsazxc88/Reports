using System.Web.Mvc;
using Reports.Core;
using Reports.Presenters.UI.Bl;
using WebMvc.Attributes;

namespace WebMvc.Controllers
{
    [PreventSpam]
    [ReportAuthorize(UserRole.Director | UserRole.OutsourcingManager | UserRole.Manager | UserRole.PersonnelManager)]
    public class AppointmentController : BaseController
    {
        protected IAppointmentBl appointmentBl;
        public IAppointmentBl AppointmentBl
        {
            get
            {
                appointmentBl = Ioc.Resolve<IAppointmentBl>();
                return Validate.Dependency(appointmentBl);
            }
        }
        [HttpGet]
        public ActionResult Index()
        {
            var model = AppointmentBl.GetAppointmentListModel();
            return View(model);
        }
    }
}