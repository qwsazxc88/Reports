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
        protected IRequestBl requestBl;
        public IEmployeeBl EmployeeBl
        {
            get
            {
                employeeBl = Ioc.Resolve<IEmployeeBl>();
                return Validate.Dependency(employeeBl);
            }
        }
        public IRequestBl RequestBl
        {
            get
            {
                requestBl = Ioc.Resolve<IRequestBl>();
                return Validate.Dependency(requestBl);
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

        [HttpGet]
        public PartialViewResult Table(int month, int year, int departmentId, string userName,int? currentPage)
        {
            GraphicsListModel model = new GraphicsListModel
            {
               CurrentPage = currentPage.HasValue?currentPage.Value:0,
               DepartmentId = departmentId,
               Month = month,
               UserName = userName,
               Year = year,
            };
            EmployeeBl.GetGraphicsListModel(model);
            return PartialView("Table", model);
        }

        [HttpGet]
        public ActionResult EditPointDialog(int id,string day,int userId)
        {
            try
            {
                TerraGraphicsEditPointModel model = new TerraGraphicsEditPointModel{Id = id,Day = day,UserId = userId};
                RequestBl.SetEditPointDialogModel(model);
                //model.Credits = new List<IdNameDto>();
                //model.EpLevel1 = new List<IdNameDto>();
                //model.EpLevel2 = new List<IdNameDto>();
                //model.EpLevel3 = new List<IdNameDto>();
                //DepartmentTreeModel model = new DepartmentTreeModel { DepartmentID = id };
                //TerraGraphicsSetShortNameModel model = RequestBl.SetShortNameModel();
                return PartialView(model);
            }
            catch (Exception ex)
            {
                Log.Error("Exception", ex);
                string error = "Ошибка при загрузке данных: " + ex.GetBaseException().Message;
                return PartialView("EpDialogError", new DialogErrorModel { Error = error });
            }
        }

    }
}