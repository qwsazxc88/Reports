using System;
using System.Web.Mvc;
using Reports.Core;
using Reports.Presenters.UI.Bl;
using Reports.Presenters.UI.ViewModel;
using WebMvc.Attributes;

namespace WebMvc.Controllers
{
    [ReportAuthorize(UserRole.Employee | UserRole.Manager | UserRole.OutsourcingManager)]
    public class GraphicsController : BaseController
    {
        protected IEmployeeBl employeeBl;
        public IEmployeeBl EmployeeBl
        {
            get
            {
                employeeBl = Ioc.Resolve<IEmployeeBl>();
                return Validate.Dependency(employeeBl);
            }
        }
        [HttpGet]
        public ActionResult Index(/*int managerId,*/ int? month, int? year)
        {
            if (!month.HasValue)
                month = DateTime.Today.Month;
            if (!year.HasValue)
                year = DateTime.Today.Year;
            GraphicsListModel model = new GraphicsListModel
            {
                //ManagerId = managerId,
                Month = month.Value,
                Year = year.Value,
            };
            EmployeeBl.SetupDepartment(model);
            EmployeeBl.GetGraphicsListModel(model);
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(GraphicsListModel model)
        {
            EmployeeBl.SetupDepartment(model);
            EmployeeBl.GetGraphicsListModel(model);
            return View(model);
        }
    }
}