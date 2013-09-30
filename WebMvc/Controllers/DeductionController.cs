using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;
using Reports.Core;
using Reports.Core.Enum;
using Reports.Presenters.UI.Bl;
using Reports.Presenters.UI.ViewModel;
using WebMvc.Attributes;

namespace WebMvc.Controllers
{
    public class DeductionController : BaseController
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

        [ReportAuthorize(UserRole.Accountant | UserRole.OutsourcingManager)]
        public ActionResult Index()
        {
            DeductionListModel model = RequestBl.GetDeductionListModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(DeductionListModel model)
        {
            RequestBl.SetDeductionListModel(model, !ValidateModel(model));
            return View(model);
        }
        protected bool ValidateModel(DeductionListModel model)
        {
            if (model.BeginDate.HasValue && model.EndDate.HasValue &&
                model.BeginDate.Value > model.EndDate.Value)
                ModelState.AddModelError("BeginDate", "Дата в поле <Период с> не может быть больше даты в поле <по>.");
            return ModelState.IsValid;
        }

        [HttpGet]
        public ActionResult DeductionEdit(int id)
        {
            DeductionEditModel model = RequestBl.GetDeductionEditModel(id);
            return View(model);
        }
        [HttpPost]
        public ActionResult DeductionEdit(DeductionEditModel model)
        {
            CorrectCheckboxes(model);
            CorrectDropdowns(model);
            //UploadFileDto fileDto = GetFileContext();
            if (!ValidateDeductionEditModel(model))
            {
                //model.IsApproved = false;
                //model.IsApprovedForAll = false;
                RequestBl.ReloadDictionariesToModel(model);
                return View(model);
            }

            string error;
            if (!RequestBl.SaveDeductionEditModel(model,out error))
            {

                if (model.ReloadPage)
                {
                    ModelState.Clear();
                    if (!string.IsNullOrEmpty(error))
                        ModelState.AddModelError("", error);
                    return View(RequestBl.GetDeductionEditModel(model.Id));
                }
                if (!string.IsNullOrEmpty(error))
                    ModelState.AddModelError("", error);
            }
            return View(model);
        }
        protected void CorrectCheckboxes(DeductionEditModel model)
        {
            if (!model.IsEditable && model.IsFastDismissalHidden)
            {
                if (ModelState.ContainsKey("IsFastDismissal"))
                    ModelState.Remove("IsFastDismissal");
                model.IsFastDismissal = model.IsFastDismissalHidden;
            }
        }
        protected void CorrectDropdowns(DeductionEditModel model)
        {
            if (!model.IsEditable)
            {
                //model.TypeId = model.TypeIdHidden;
                //model.KindId = model.KindIdHidden;
                //model.MonthId = model.MonthIdHidden;
                model.UserId = model.UserIdHidden;
            }
        }
        protected bool ValidateDeductionEditModel(DeductionEditModel model)
        {
            if(string.IsNullOrEmpty(model.Sum))
                ModelState.AddModelError("Sum", "'Сумма' - обязательное поле.");
            else
            {
                decimal sum;
                if(!Decimal.TryParse(model.Sum,out sum)) 
                    ModelState.AddModelError("Sum", "Поле 'Сумма' должно быть числом.");
                else if (sum <= 0)
                    ModelState.AddModelError("Sum", "Поле 'Сумма' должно быть положительным числом.");
            }
            if (model.TypeId != (int)DeductionTypeEnum.Deduction)
            {
                if(!model.DismissalDate.HasValue)
                    ModelState.AddModelError("DismissalDate", "'Дата увольнения' - обязательное поле.");
            }
            return ModelState.IsValid;
        }

        public ActionResult UserChange(int userId)
        {
            DeductionUserInfoModel model = new DeductionUserInfoModel();
            try
            {
                RequestBl.SetDeductionUserInfoModel(model, userId);
            }
            catch (Exception ex)
            {
                Log.Error("Exception on UserChange",ex);
                model.UserInfoError = string.Format("Ошибка при загрузке данных сотрудника:{0}",ex.GetBaseException().Message);
            }
            
            return PartialView("DeductionUserInfo",model);
        }

        [HttpGet]
        public ActionResult PrintDeductionList(DateTime? beginDate, DateTime? endDate, int? departmentId,
                    int requestStatusId, int typeId, string userName, int? sortBy, bool? sortDescending)
        {

            DeductionListModel model = new DeductionListModel
                                           {
                                               BeginDate = beginDate,
                                               EndDate = endDate,
                                               DepartmentId = departmentId.HasValue?departmentId.Value:0,
                                               RequestStatusId = requestStatusId,
                                               TypeId = typeId,
                                               UserName = string.IsNullOrEmpty(userName)?string.Empty:Server.UrlDecode(userName),
                                               SortBy = sortBy.HasValue?sortBy.Value:0,
                                               SortDescending = sortDescending.HasValue?sortDescending.Value:new bool?(),
                                           };
            RequestBl.SetDeductionListModel(model, !ValidateModel(model));
            return View(model);
        }
        [HttpGet]
        public ActionResult RenderToPdf(DateTime? beginDate, DateTime? endDate, int? departmentId,
                    int? requestStatusId, int? typeId, string userName, int? sortBy, bool? sortDescending)
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
                argumrnts.AppendFormat("{0} --cookie {1} {2}",
                    GetConverterCommandParam(beginDate, endDate, departmentId, 
                    requestStatusId, typeId,userName,sortBy, sortDescending)
                    , cookieName, authCookie.Value);
                //argumrnts.AppendFormat("\"{0}\"", GetConverterCommandPtaram(id));
                argumrnts.AppendFormat(" \"{0}\"", filePath);
                var serverSideProcess = new Process
                {
                    StartInfo =
                    {
                        FileName = ConfigurationManager.AppSettings["PdfConverterCommandLineTemplate"],
                        Arguments = argumrnts.ToString(),
                        UseShellExecute = true
                    },
                    EnableRaisingEvents = true
                };
                serverSideProcess.Start();
                serverSideProcess.WaitForExit();
                return GetFile(filePath, fileName, @"application/pdf");
            }
            catch (Exception ex)
            {
                Log.Error("Exception on RenderToPdf", ex);
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
        protected string GetConverterCommandParam(DateTime? beginDate, DateTime? endDate, int? departmentId, 
                    int? requestStatusId, int? typeId,string userName,int? sortBy,bool? sortDescending)
        {
            var localhostUrl = ConfigurationManager.AppSettings["localhost"];
            const string urlTemplate = "Deduction/PrintDeductionList";
            string args =
                string.Format(
                    @"?beginDate={0}&endDate={1}&departmentId={2}&requestStatusId={3}&typeId={4}
                        &userName={5}&sortBy={6}&sortDescending={7}",
                    beginDate.HasValue?beginDate.Value.ToShortDateString():string.Empty,
                    endDate.HasValue ? endDate.Value.ToShortDateString() : string.Empty,
                    departmentId.HasValue?departmentId.Value.ToString():string.Empty,
                    requestStatusId.HasValue?requestStatusId.Value:0, 
                    typeId.HasValue?typeId.Value:0,
                    string.IsNullOrEmpty(userName)?string.Empty:Server.UrlEncode(userName),
                    sortBy.HasValue?sortBy.Value.ToString():string.Empty,
                    sortDescending.HasValue ? sortDescending.Value.ToString() : string.Empty);
            return !string.IsNullOrEmpty(localhostUrl)
                       ? string.Format(@"{0}/{1}{2}",localhostUrl, urlTemplate,args)
                       : Url.Content(string.Format(@"{0}{1}",urlTemplate, args));
        }
        protected ActionResult GetFile(string filePath, string fileName, string contentType)
        {
            byte[] value;
            using (FileStream stream = System.IO.File.Open(filePath, FileMode.Open))
            {
                value = new byte[stream.Length];
                stream.Read(value, 0, (int)stream.Length);
            }

            //const string contentType = "application/pdf";
            Response.Clear();
            if (Request.Browser.Browser == "IE")
            {
                string attachment = String.Format("attachment; filename=\"{0}\"", Server.UrlPathEncode(fileName));
                Response.AddHeader("Content-Disposition", attachment);
            }
            else
                Response.AddHeader("Content-Disposition", "attachment; filename=\"" + fileName + "\"");

            Response.ContentType = contentType;
            Response.Charset = "utf-8";
            Response.HeaderEncoding = Encoding.UTF8;
            Response.ContentEncoding = Encoding.UTF8;
            Response.BinaryWrite(value);
            Response.End();
            return null;
        }
    }
}