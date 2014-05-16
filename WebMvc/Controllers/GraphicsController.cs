using System;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Script.Serialization;
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
        [ReportAuthorize(UserRole.Manager)]
        public ActionResult EditPointDialog(int id,string day,int userId)
        {
            try
            {
                DateTime tpDay;
                if(!DateTime.TryParse(day,CultureInfo.GetCultureInfo("ru-RU"),DateTimeStyles.None,out tpDay))
                    throw new ArgumentException(string.Format("Неправильная дата {0}",day));
                TerraGraphicsEditPointModel model = new TerraGraphicsEditPointModel
                                                        {
                                                            Id = id,
                                                            Day = day,
                                                            UserId = userId,
                                                            tpDay = tpDay,
                                                        };
                RequestBl.SetEditPointDialogModel(model);
                return PartialView(model);
            }
            catch (Exception ex)
            {
                Log.Error("Exception", ex);
                string error = "Ошибка при загрузке данных: " + ex.GetBaseException().Message;
                return PartialView("EpDialogError", new DialogErrorModel { Error = error });
            }
        }
        [HttpGet]
        [ReportAuthorize(UserRole.Manager)]
        public ContentResult SetDefaultTerraPoint(int pointId, int userId)
        {
            TerraPointSetDefaultTerraPointModel model;
            try
            {
                model = RequestBl.SetDefaultTerraPoint(pointId/*, userId*/);
            }
            catch (Exception ex)
            {
                Log.Error("Exception on SetDefaultTerraPoint:", ex);
                string error = ex.GetBaseException().Message;
                model = new TerraPointSetDefaultTerraPointModel
                {
                    Error = string.Format("Ошибка: {0}", error),
                };
            }
            var jsonSerializer = new JavaScriptSerializer();
            string jsonString = jsonSerializer.Serialize(model);
            return Content(jsonString);
        }
        [HttpGet]
        [ReportAuthorize(UserRole.Manager)]
        public ContentResult DeleteTerraPoint(int id)
        {
            TerraPointSetDefaultTerraPointModel model = new TerraPointSetDefaultTerraPointModel {Error = string.Empty};
            try
            {
                RequestBl.DeleteTerraPoint(id);
            }
            catch (Exception ex)
            {
                Log.Error("Exception on DeleteTerraPoint:", ex);
                string error = ex.GetBaseException().Message;
                model = new TerraPointSetDefaultTerraPointModel
                {
                    Error = string.Format("Ошибка: {0}", error),
                };
            }
            var jsonSerializer = new JavaScriptSerializer();
            string jsonString = jsonSerializer.Serialize(model);
            return Content(jsonString);
        }

        [HttpGet]
        [ReportAuthorize(UserRole.Manager)]
        public ContentResult SaveTerraPoint(int pointId, int id, int userId, string day, string hours, int credits,
            int factPointId, string factHours)
        {
            TerraPointSaveModel model = new TerraPointSaveModel
                                            {
                                                Id = id,
                                                IsCreditAvailable = credits,
                                                Day = day,
                                                Hours = hours,
                                                PointId = pointId,
                                                UserId = userId,
                                                FactHours = factHours,
                                                FactPointId = factPointId,
                                            };
            try
            {
                if (ValidateSaveTpModel(model))
                    RequestBl.SaveTerraPoint(model);
            }
            catch (Exception ex)
            {
                Log.Error("Exception on SaveTerraPoint:", ex);
                string error = ex.GetBaseException().Message;
                model.Error = string.Format("Ошибка: {0}", error);
            }
            var jsonSerializer = new JavaScriptSerializer();
            string jsonString = jsonSerializer.Serialize(model);
            return Content(jsonString);
        }
        protected bool ValidateSaveTpModel(TerraPointSaveModel model)
        {
            DateTime tpDay;
            if (!DateTime.TryParse(model.Day, CultureInfo.GetCultureInfo("ru-RU"), DateTimeStyles.None, out tpDay))
            {
                //ModelState.AddModelError(string.Empty, "Неправильная дата");
                model.Error = "Неправильная дата";
            }
            model.TpDay = tpDay;
            if (string.IsNullOrEmpty(model.Hours))
            {
                //ModelState.AddModelError("Hours", "Необходимо указать поле 'План'");
                model.Error = "Необходимо указать поле 'План'";
            }
            else
            {
                decimal hours;
                if (!decimal.TryParse(model.Hours, out hours) || hours < 0 || hours > 24)
                {
                    //ModelState.AddModelError("Hours", "Поле 'План' должно быть целым числом от 0 до 24");
                    model.Error = "Поле 'План' должно быть числом от 0 до 24";
                }
                else
                    model.TpHours = hours;
            }
            if (!string.IsNullOrEmpty(model.FactHours))
            {
                decimal hours;
                if (!decimal.TryParse(model.FactHours, out hours) || hours < 0 || hours > 24)
                {
                    //ModelState.AddModelError("Hours", "Поле 'План' должно быть целым числом от 0 до 24");
                    model.Error = "Поле 'Факт' должно быть числом от 0 до 24";
                }
                else
                    model.TpFactHours = hours;
            }
            return string.IsNullOrEmpty(model.Error);
        }
    }
}