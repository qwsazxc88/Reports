using System;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Reports.Core;
using Reports.Presenters.UI.Bl;
using Reports.Presenters.UI.ViewModel;
using WebMvc.Attributes;
using System.Web;
using System.Configuration;
using System.IO;
using System.Web.Security;
using System.Text;
using System.Diagnostics;
using System.Reflection;
using WebMvc.Helpers;
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
            if (model.ForPrint)
            {
                return GetPrintForm("Graphics", "IndexPrint", model.ToParamsString(), false);
            }
            EmployeeBl.SetupDepartment(model);
            EmployeeBl.GetGraphicsListModel(model);
            return View(model);
        }
        [HttpGet]
        public ActionResult IndexPrint(GraphicsListModel model)
        {
            return Index(model);
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
        public ContentResult SaveTerraPoint(int pointId, int id, int userId, string day, string hours, int credits, int factCredits,
            int factPointId, string factHours)
        {
            TerraPointSaveModel model = new TerraPointSaveModel
                                            {
                                                Id = id,
                                                IsCreditAvailable = credits,
                                                IsFactCreditAvailable = factCredits,
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
                {
                    // Если дата точки больше текущей даты, Факт заполняется значениями из Плана
                    if(model.TpDay.Date > DateTime.Today)
                    {
                        model.FactPointId = model.PointId;
                        model.FactHours = model.Hours;
                        model.TpFactHours = model.TpHours;
                        model.IsFactCreditAvailable = model.IsCreditAvailable;
                    }
                    RequestBl.SaveTerraPoint(model);
                }
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
            if (string.IsNullOrEmpty(model.Hours) && (tpDay.Date > DateTime.Today))
            {
                //ModelState.AddModelError("Hours", "Необходимо указать поле 'План'");
                model.Error = "Необходимо указать плановые часы";
                return false;
            }
            if (string.IsNullOrEmpty(model.FactHours) && (tpDay.Date < DateTime.Today))
            {
                //ModelState.AddModelError("Hours", "Необходимо указать поле 'План'");
                model.Error = "Необходимо указать фактические часы";
                return false;
            }
            if (string.IsNullOrEmpty(model.FactHours) && string.IsNullOrEmpty(model.Hours) 
                && (tpDay.Date == DateTime.Today))
            {
                //ModelState.AddModelError("Hours", "Необходимо указать поле 'План'");
                model.Error = "Необходимо указать плановые или фактические часы";
                return false;
            }

            if (!string.IsNullOrEmpty(model.Hours))
            {
                decimal hours;
                if (!decimal.TryParse(model.Hours, out hours) || hours < 0 || hours > 24)
                {
                    //ModelState.AddModelError("Hours", "Поле 'План' должно быть целым числом от 0 до 24");
                    model.Error = "Плановые часы должны быть числом от 0 до 24";
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
                    model.Error = "Фактические часы быть числом от 0 до 24";
                }
                else
                    model.TpFactHours = hours;
            }
            return string.IsNullOrEmpty(model.Error);
        }

        #region Print
        [HttpGet]
        public ActionResult GetPrintForm(string controller, string actionName, string param, bool isLandscape)
        {
            string filePath = null;
            try
            {
                var folderPath = ConfigurationManager.AppSettings["PresentationFolderPath"];
                var fileName = string.Format("{0}.pdf", Guid.NewGuid());

                folderPath = HttpContext.Server.MapPath(folderPath);
                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);
                filePath = Path.Combine(folderPath, fileName);

                var argumrnts = new StringBuilder();

                var cookieName = FormsAuthentication.FormsCookieName;
                var authCookie = Request.Cookies[cookieName];
                if (authCookie == null || authCookie.Value == null)
                    throw new ArgumentException("Ошибка авторизации.");
                if (isLandscape)
                    argumrnts.AppendFormat(" --orientation Landscape {0}  --cookie {1} {2}",
                    GetConverterCommandParam(param, actionName,controller)
                    , cookieName, authCookie.Value);
                else
                    argumrnts.AppendFormat("{0} --cookie {1} {2}",
                    GetConverterCommandParam(param, actionName,controller)
                    , cookieName, authCookie.Value);
                argumrnts.AppendFormat(" \"{0}\"", filePath);
                var serverSideProcess = new Process
                {
                    StartInfo =
                    {
                        FileName = ConfigurationManager.AppSettings["PdfConverterCommandLineTemplate"],
                        Arguments = argumrnts.ToString(),
                        UseShellExecute = true,
                    },
                    EnableRaisingEvents = true,

                };
                serverSideProcess.Start();
                serverSideProcess.WaitForExit();
                return GetFile(Response, Request, Server, filePath, fileName, @"application/pdf", controller+".pdf");
            }
            catch (Exception ex)
            {
                Log.Error("Exception on GetPrintForm", ex);
                throw;
            }
            finally
            {
                if (!string.IsNullOrEmpty(filePath) && System.IO.File.Exists(filePath))
                {
                    try
                    {
                        System.IO.File.Delete(filePath);
                    }
                    catch (Exception ex)
                    {
                        Log.Warn(string.Format("Exception on delete file {0}", filePath), ex);
                    }
                }
            }
        }
        public static ActionResult GetFile(HttpResponseBase Response, HttpRequestBase Request, HttpServerUtilityBase Server,
            string filePath, string fileName, string contentType, string userFileName)
        {
            byte[] value;
            using (FileStream stream = System.IO.File.Open(filePath, FileMode.Open))
            {
                value = new byte[stream.Length];
                stream.Read(value, 0, (int)stream.Length);
            }
            //const string userFileName = "MissionOrder.pdf";
            //const string contentType = "application/pdf";
            Response.Clear();
            if (Request.Browser.Browser == "IE")
            {
                string attachment = String.Format("attachment; filename=\"{0}\"", Server.UrlPathEncode(userFileName));
                Response.AddHeader("Content-Disposition", attachment);
            }
            else
                Response.AddHeader("Content-Disposition", "attachment; filename=\"" + userFileName + "\"");

            Response.ContentType = contentType;
            Response.Charset = "utf-8";
            Response.HeaderEncoding = Encoding.UTF8;
            Response.ContentEncoding = Encoding.UTF8;
            Response.BinaryWrite(value);
            Response.End();
            return null;
        }
        protected string GetConverterCommandParam(string param, string actionName, string controller)
        {
            var localhostUrl = ConfigurationManager.AppSettings["localhost"];
            string urlTemplate = string.Format("{0}/{1}", controller, actionName);
            string args = @"?" + param;
            return !string.IsNullOrEmpty(localhostUrl)
                       ? string.Format(@"{0}/{1}{2}", localhostUrl, urlTemplate, args)
                       : Url.Content(string.Format(@"{0}{1}", urlTemplate, args));
        }
        #endregion
    }
}