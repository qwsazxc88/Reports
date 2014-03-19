using System.Web.Mvc;
using Reports.Core;
using Reports.Presenters.UI.Bl;
using Reports.Presenters.UI.ViewModel;
using WebMvc.Attributes;

namespace WebMvc.Controllers
{
    [PreventSpam]
    [ReportAuthorize(UserRole.Director | UserRole.OutsourcingManager | UserRole.Manager | UserRole.PersonnelManager | UserRole.StaffManager)]
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
        [HttpGet]
        public ActionResult AppointmentEdit(int id)
        {
            AppointmentEditModel model = AppointmentBl.GetAppointmentEditModel(id);
            return View(model);
        }
    }
}