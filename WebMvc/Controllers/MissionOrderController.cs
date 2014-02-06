using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;
using Reports.Core;
using Reports.Core.Dto;
using Reports.Core.Enum;
using Reports.Presenters.UI.Bl;
using Reports.Presenters.UI.ViewModel;
using WebMvc.Attributes;
using System.Web.Routing;

namespace WebMvc.Controllers
{
    [ReportAuthorize(UserRole.Employee | UserRole.Manager | UserRole.Accountant | UserRole.OutsourcingManager | 
        UserRole.Director | UserRole.Secretary | UserRole.Findep)]
    public class MissionOrderController : BaseController
    {
        public const int MaxFileSize = 2 * 1024 * 1024;
        public const int MaxCommentLength = 256;

        protected IRequestBl requestBl;
        public IRequestBl RequestBl
        {
            get
            {
                requestBl = Ioc.Resolve<IRequestBl>();
                return Validate.Dependency(requestBl);
            }
        }
        [HttpGet]
        public ActionResult Index()
        {
            var model = RequestBl.GetMissionOrderListModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(MissionOrderListModel model)
        {
            ModelState.Clear();
            RequestBl.SetMissionOrderListModel(model, !ValidateModel(model));
            if(model.HasErrors)
                ModelState.AddModelError(string.Empty, "При согласовании приказов произошла(и) ошибка(и).Не все приказы были согласованы.");
            return View(model);
        }
        protected bool ValidateModel(MissionOrderListModel model)
        {
            if (model.BeginDate.HasValue && model.EndDate.HasValue &&
                model.BeginDate.Value > model.EndDate.Value)
                ModelState.AddModelError("BeginDate", "Дата в поле <Период с> не может быть больше даты в поле <по>.");
            if (model.IsApproveClick && (model.Documents == null || model.Documents.Count == 0
                       || model.Documents.Where(x => x.IsChecked).Count() == 0 ))
                ModelState.AddModelError(string.Empty, "Не выбрано ни одного приказа для согласования.");
            return ModelState.IsValid;
        }

        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.Director)]
        public ActionResult CreateMissionOrderRequest()
        {
            CreateMissionOrderModel model = RequestBl.GetCreateMissionOrderModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult CreateMissionOrderRequest(CreateMissionOrderModel model)
        {
            return RedirectToAction("MissionOrderEdit",
                                             new RouteValueDictionary {
                                                                        {"id", 0}, 
                                                                        {"userId", model.UserId}
                                                                       });
        }

        [HttpGet]
        public ActionResult MissionOrderEdit(int id,int? userId)
        {
            MissionOrderEditModel model = RequestBl.GetMissionOrderEditModel(id,userId);
            return View(model);
        }

        [HttpPost]
        public ActionResult MissionOrderEdit(MissionOrderEditModel model)
        {
            CorrectCheckboxes(model);
            CorrectDropdowns(model);
            if (!ValidateMissionOrderEditModel(model))
            {
                //model.IsApproved = false;
                //model.IsApprovedForAll = false;
                RequestBl.ReloadDictionaries(model);
                return View(model);
            }

            string error;
            if (!RequestBl.SaveMissionOrderEditModel(model, out error))
            {
                if (model.ReloadPage)
                {
                    ModelState.Clear();
                    if (!string.IsNullOrEmpty(error))
                        ModelState.AddModelError("", error);
                    return View(RequestBl.GetMissionOrderEditModel(model.Id, model.UserId));
                }
                if (!string.IsNullOrEmpty(error))
                    ModelState.AddModelError("", error);
            }
            return View(model);
        }
        protected bool ValidateMissionOrderEditModel(MissionOrderEditModel model)
        {
            //return false;
            return ModelState.IsValid;
        }
        protected void CorrectDropdowns(MissionOrderEditModel model)
        {
             if (!model.IsEditable)
             {
                 model.TypeId = model.TypeIdHidden;
                 model.Kind = model.KindHidden;
                 model.GoalId = model.GoalIdHidden;
             }
             if(!model.IsSecritaryEditable)
             {
                 model.AirTicketType = model.AirTicketTypeHidden;
                 model.TrainTicketType = model.TrainTicketTypeHidden;
             }
        }
        protected void CorrectCheckboxes(MissionOrderEditModel model)
        {
            if (!model.IsChiefApproveAvailable /*&& model.IsChiefApprovedHidden.Value*/)
            {
                if (ModelState.ContainsKey("IsChiefApproved"))
                    ModelState.Remove("IsChiefApproved");
                model.IsChiefApproved = model.IsChiefApprovedHidden;
            }
            if (!model.IsManagerApproveAvailable /*&& model.IsManagerApprovedHidden.Value*/)
            {
                if (ModelState.ContainsKey("IsManagerApproved"))
                    ModelState.Remove("IsManagerApproved");
                model.IsManagerApproved = model.IsManagerApprovedHidden;
            }
            if (!model.IsUserApprovedAvailable && model.IsUserApprovedHidden)
            {
                if (ModelState.ContainsKey("IsUserApproved"))
                    ModelState.Remove("IsUserApproved");
                model.IsUserApproved = model.IsUserApprovedHidden;
            }
            if (ModelState.ContainsKey("IsChiefApproveNeed"))
                ModelState.Remove("IsChiefApproveNeed");
            model.IsChiefApproveNeed = model.IsChiefApproveNeedHidden;

            if (model.IsManagerApproveAvailable && model.IsManagerApproved.HasValue
                && !model.IsManagerApproved.Value)
            {
                if (ModelState.ContainsKey("IsManagerApproved"))
                    ModelState.Remove("IsManagerApproved");
            }
            if (model.IsChiefApproveAvailable && model.IsChiefApproved.HasValue
                && !model.IsChiefApproved.Value)
            {
                if (ModelState.ContainsKey("IsChiefApproved"))
                    ModelState.Remove("IsChiefApproved");
                if (ModelState.ContainsKey("IsManagerApproved"))
                    ModelState.Remove("IsManagerApproved");
            }
            if (!model.IsEditable)
            {
                if (model.IsResidencePaidHidden)
                {
                    if (ModelState.ContainsKey("IsResidencePaid"))
                        ModelState.Remove("IsResidencePaid");
                    model.IsResidencePaid = model.IsResidencePaidHidden;
                }
                if (model.IsAirTicketsPaidHidden)
                {
                    if (ModelState.ContainsKey("IsAirTicketsPaid"))
                        ModelState.Remove("IsAirTicketsPaid");
                    model.IsAirTicketsPaid = model.IsAirTicketsPaidHidden;
                }
                if (model.IsTrainTicketsPaidHidden)
                {
                    if (ModelState.ContainsKey("IsTrainTicketsPaid"))
                        ModelState.Remove("IsTrainTicketsPaid");
                    model.IsTrainTicketsPaid = model.IsTrainTicketsPaidHidden;
                }
            }
        }
        
        [HttpGet]
        //[ReportAuthorize(UserRole.Manager)]
        public ActionResult EditTargetDialog(int id,string json)
        {
            try
            {
                MissionOrderEditTargetModel model = new MissionOrderEditTargetModel { TargetId = id };
                if(!string.IsNullOrEmpty(json))
                {
                    JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
                    MissionOrderTargetModel target = jsonSerializer.Deserialize<MissionOrderTargetModel>(json);
                    model.AirTicketTypeId = target.AirTicketTypeId;
                    model.AllDays = target.AllDaysCount;
                    model.BeginDate = target.DateFrom;
                    model.City = target.City;
                    model.CountryId = target.CountryId;
                    model.DailyAllowanceId = target.DailyAllowanceId;
                    model.EndDate = target.DateTo;
                    model.Organization = target.Organization;
                    model.RealDays = target.TargetDaysCount;
                    model.ResidenceId = target.ResidenceId;
                    model.TrainTicketTypeId = target.TrainTicketTypeId;

                }
                RequestBl.SetMissionOrderEditTargetModel(model);
                return PartialView(model);
            }
            catch (Exception ex)
            {
                Log.Error("Exception", ex);
                string error = "Ошибка при загрузке данных: " + ex.GetBaseException().Message;
                return PartialView("EditTargetDialogError", new DialogErrorModel { Error = error });
            }
        }
        #region Print
        [HttpGet]
        public ActionResult PrintOrder(int id)
        {
            PrintMissionOrderViewModel model = RequestBl.GetPrintMissionOrderModel(id);

            return View(model);
        }
        [HttpGet]
        public ActionResult PrintOrderDocument(int id)
        {
            UserInfoModel model = RequestBl.GetPrintMissionOrderDocumentModel(id);
            return View(model);
        }
        [HttpGet]
        public ActionResult PrintPathList(int id)
        {
            UserInfoModel model = RequestBl.GetPrintMissionOrderDocumentModel(id);
            return View(model);
        }
        [HttpGet]
        public ActionResult GetOrderPrintForm(int id)
        {
            return GetPrintForm(id, "PrintOrder");
        }
        [HttpGet]
        public ActionResult GetDocumentPrintForm(int id)
        {
            return GetPrintForm(id, "PrintOrderDocument");
        }
        [HttpGet]
        public ActionResult GetPathListPrintForm(int id)
        {
            return GetPrintForm(id, "PrintPathList");
        }
        [HttpGet]
        public ActionResult GetPrintForm(int id,string actionName)
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
                    GetConverterCommandParam(id,actionName)
                    , cookieName, authCookie.Value);
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
                return GetFile(Response,Request,Server,filePath, fileName, @"application/pdf");
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
        public static ActionResult GetFile(HttpResponseBase Response, HttpRequestBase Request,HttpServerUtilityBase Server,
            string filePath, string fileName, string contentType)
        {
            byte[] value;
            using (FileStream stream = System.IO.File.Open(filePath, FileMode.Open))
            {
                value = new byte[stream.Length];
                stream.Read(value, 0, (int)stream.Length);
            }
            const string userFileName = "MissionOrder.pdf";
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
        protected string GetConverterCommandParam(int id, string actionName)
        {
            var localhostUrl = ConfigurationManager.AppSettings["localhost"];
            string urlTemplate = string.Format("MissionOrder/{0}",actionName);
            string args = @"/"+id;
            return !string.IsNullOrEmpty(localhostUrl)
                       ? string.Format(@"{0}/{1}{2}", localhostUrl, urlTemplate, args)
                       : Url.Content(string.Format(@"{0}{1}", urlTemplate, args));
        }
        #endregion

        [HttpGet]
        public ActionResult GradeList()
        {
            GradeListViewModel model = RequestBl.GetGradeListModel();
            return View(model);
        }
        [HttpGet]
        public ActionResult Instructions()
        {
            return View(new InstructionsViewModel());
        }

        [HttpGet]
        [ReportAuthorize(UserRole.Employee | UserRole.Manager | UserRole.Accountant | UserRole.OutsourcingManager | UserRole.Findep)]
        public ActionResult MissionReportsList()
        {
            var model = RequestBl.GetMissionReportsListModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult MissionReportsList(MissionReportsListModel model)
        {
            //ModelState.Clear();
            RequestBl.SetMissionReportsListModel(model, !ValidateModel(model));
            //if (model.HasErrors)
            //    ModelState.AddModelError(string.Empty, "При согласовании приказов произошла(и) ошибка(и).Не все приказы были согласованы.");
            return View(model);
        }
        protected bool ValidateModel(MissionReportsListModel model)
        {
            if (model.BeginDate.HasValue && model.EndDate.HasValue &&
                model.BeginDate.Value > model.EndDate.Value)
                ModelState.AddModelError("BeginDate", "Дата в поле <Период с> не может быть больше даты в поле <по>.");
            //if (model.IsApproveClick && (model.Documents == null || model.Documents.Count == 0
            //           || model.Documents.Where(x => x.IsChecked).Count() == 0))
            //    ModelState.AddModelError(string.Empty, "Не выбрано ни одного приказа для согласования.");
            return ModelState.IsValid;
        }

        [ReportAuthorize(UserRole.Employee | UserRole.Manager | UserRole.Accountant | UserRole.OutsourcingManager | UserRole.Findep)]
        [HttpGet]
        public ActionResult MissionReportEdit(int id/*, int? userId*/)
        {
            MissionReportEditModel model = RequestBl.GetMissionReportEditModel(id/*, userId*/);
            return View(model);
        }
        [HttpPost]
        public ActionResult MissionReportEdit(MissionReportEditModel model)
        {
            CorrectCheckboxes(model);
            //CorrectDropdowns(model);
            //if (!ValidateMissionOrderEditModel(model))
            //{
            //    RequestBl.ReloadDictionaries(model);
            //    return View(model);
            //}

            string error;
            if (!RequestBl.SaveMissionReportEditModel(model, out error))
            {
                if (model.ReloadPage)
                {
                    ModelState.Clear();
                    if (!string.IsNullOrEmpty(error))
                        ModelState.AddModelError("", error);
                    return View(RequestBl.GetMissionReportEditModel(model.Id));
                }
                if (!string.IsNullOrEmpty(error))
                    ModelState.AddModelError("", error);
            }
            return View(model);
        }
        protected void CorrectCheckboxes(MissionReportEditModel model)
        {
            if (!model.IsUserApprovedAvailable && model.IsUserApprovedHidden)
            {
                if (ModelState.ContainsKey("IsUserApproved"))
                    ModelState.Remove("IsUserApproved");
                model.IsUserApproved = model.IsUserApprovedHidden;
            }
            if (!model.IsManagerApproveAvailable && model.IsManagerApprovedHidden)
            {
                if (ModelState.ContainsKey("IsManagerApproved"))
                    ModelState.Remove("IsManagerApproved");
                model.IsManagerApproved = model.IsManagerApprovedHidden;
            }
            if (!model.IsAccountantApproveAvailable && model.IsAccountantApprovedHidden)
            {
                if (ModelState.ContainsKey("IsAccountantApproved"))
                    ModelState.Remove("IsAccountantApproved");
                model.IsAccountantApproved = model.IsAccountantApprovedHidden;
            }
            if(model.IsManagerRejectAvailable && model.IsManagerReject)
            {
                if (ModelState.ContainsKey("IsUserApproved"))
                    ModelState.Remove("IsUserApproved");
                if (ModelState.ContainsKey("IsManagerApproved"))
                    ModelState.Remove("IsManagerApproved");
            }
            if (model.IsAccountantRejectAvailable && model.IsAccountantReject)
            {
                if (ModelState.ContainsKey("IsAccountantApproved"))
                    ModelState.Remove("IsAccountantApproved");
            }

            /*if (ModelState.ContainsKey("IsChiefApproveNeed"))
                ModelState.Remove("IsChiefApproveNeed");
            model.IsChiefApproveNeed = model.IsChiefApproveNeedHidden;

            if (model.IsManagerApproveAvailable && model.IsManagerApproved.HasValue
                && !model.IsManagerApproved.Value)
            {
                if (ModelState.ContainsKey("IsManagerApproved"))
                    ModelState.Remove("IsManagerApproved");
            }
            if (model.IsChiefApproveAvailable && model.IsChiefApproved.HasValue
                && !model.IsChiefApproved.Value)
            {
                if (ModelState.ContainsKey("IsChiefApproved"))
                    ModelState.Remove("IsChiefApproved");
                if (ModelState.ContainsKey("IsManagerApproved"))
                    ModelState.Remove("IsManagerApproved");
            }
            if (!model.IsEditable)
            {
                if (model.IsResidencePaidHidden)
                {
                    if (ModelState.ContainsKey("IsResidencePaid"))
                        ModelState.Remove("IsResidencePaid");
                    model.IsResidencePaid = model.IsResidencePaidHidden;
                }
                if (model.IsAirTicketsPaidHidden)
                {
                    if (ModelState.ContainsKey("IsAirTicketsPaid"))
                        ModelState.Remove("IsAirTicketsPaid");
                    model.IsAirTicketsPaid = model.IsAirTicketsPaidHidden;
                }
                if (model.IsTrainTicketsPaidHidden)
                {
                    if (ModelState.ContainsKey("IsTrainTicketsPaid"))
                        ModelState.Remove("IsTrainTicketsPaid");
                    model.IsTrainTicketsPaid = model.IsTrainTicketsPaidHidden;
                }
            }*/
        }
        [HttpGet]
        //[ReportAuthorize(UserRole.Manager)]
        public ActionResult EditCostDialog(int id, string json)
        {
            try
            {
                MissionReportEditCostModel model = new MissionReportEditCostModel { CostId = id };
                if (!string.IsNullOrEmpty(json))
                {
                    JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
                    CostDto dto = jsonSerializer.Deserialize<CostDto>(json);
                    model.AccountantSum = dto.AccountantSum;
                    model.CostTypeId = dto.CostTypeId;
                    //model.Count = dto.Count;
                    model.GradeSum = dto.GradeSum;
                    model.PurchaseBookSum = dto.PurchaseBookSum;
                    model.UserSum = dto.UserSum;
                    

                }
                RequestBl.SetMissionReportEditCostModel(model);
                return PartialView(model);
            }
            catch (Exception ex)
            {
                Log.Error("Exception", ex);
                string error = "Ошибка при загрузке данных: " + ex.GetBaseException().Message;
                return PartialView("EditCostDialogError", new DialogErrorModel { Error = error });
            }
        }
        [HttpGet]
        public ActionResult EditTranDialog(int id, int costId, string json)
        {
            try
            {
                MissionReportEditTranModel model = new MissionReportEditTranModel { TranId = id,CostId = costId };
                if (!string.IsNullOrEmpty(json))
                {
                    JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
                    TransactionDto dto = jsonSerializer.Deserialize<TransactionDto>(json);
                    model.DebitAccountId = dto.DebitId;
                    model.CreditAccountId = dto.CreditId;
                    model.Sum = dto.Sum;
                }
                RequestBl.SetMissionReportEditTranModel(model);
                return PartialView(model);
            }
            catch (Exception ex)
            {
                Log.Error("Exception", ex);
                string error = "Ошибка при загрузке данных: " + ex.GetBaseException().Message;
                return PartialView("EditTranDialogError", new DialogErrorModel { Error = error });
            }
        }

        [HttpGet]
        public FileContentResult ViewAttachment(int id)
        {
            try
            {
                AttachmentModel model = RequestBl.GetFileContext(id);
                return File(model.Context, model.ContextType, model.FileName);
            }
            catch (Exception ex)
            {
                Log.Error("Error on ViewAttachment:", ex);
                throw;
            }
        }
        [HttpGet]
        public ActionResult RenderAttachments(int id, int typeId)
        {
            //IContractRequest bo = Ioc.Resolve<IContractRequest>();
            RequestAttachmentsModel model = RequestBl.GetMrAttachmentsModel(id, (RequestAttachmentTypeEnum)typeId);
            return PartialView("RequestAttachmentsPartial", model);
        }
        [HttpGet]
        public ActionResult AddAttachmentDialog(int id, int typeId, string name)
        {
            try
            {
                AddAttachmentModel model = new AddAttachmentModel
                                               {
                                                   DocumentId = id,
                                                   Description = name,
                                                   IsDescriptionDisabled = !string.IsNullOrEmpty(name)
                                               };
                return PartialView(model);
            }
            catch (Exception ex)
            {
                Log.Error("Exception", ex);
                string error = "Ошибка при загрузке данных: " + ex.GetBaseException().Message;
                return PartialView("AttachmentDialogError", new DialogErrorModel { Error = error });
            }
        }
        protected UploadFileDto GetFileContext()
        {
            if (Request.Files.Count == 0)
                return null;
            string file = Request.Files.GetKey(0);
            return GetFileContext(file);
        }
        protected UploadFileDto GetFileContext(string file)
        {
            //if (Request.Files.Count == 0)
            //    return null;
            //string file = Request.Files.GetKey(0);
            HttpPostedFileBase hpf = Request.Files[file];
            if ((hpf == null) || (hpf.ContentLength == 0))
                return null;
            if (hpf.ContentLength > MaxFileSize)
            {
                ModelState.AddModelError("", string.Format("Размер прикрепленного файла не может превышать {0} Мб.", MaxFileSize / (1024 * 1024)));
                return null;
            }
            byte[] context = GetFileData(hpf);
            return new UploadFileDto
            {
                Context = context,
                ContextType = hpf.ContentType,
                FileName = Path.GetFileName(hpf.FileName),
            };
        }
        protected byte[] GetFileData(HttpPostedFileBase file)
        {
            var length = file.ContentLength;
            var fileContent = new byte[length];
            file.InputStream.Read(fileContent, 0, length);
            return fileContent;
        }
        [HttpPost]
        public ContentResult SaveAttachment(int id, string description, string qqFile)
        {
            bool saveResult;
            string error;
            try
            {
                var length = Request.ContentLength;
                var bytes = new byte[length];
                Request.InputStream.Read(bytes, 0, length);

                saveResult = true;
                if (length > MaxFileSize)
                {
                    error = string.Format("Размер прикрепленного файла > {0} байт.", MaxFileSize);
                }
                else if (description == null || string.IsNullOrEmpty(description.Trim()))
                {
                    error = "Описание - обязательное поле";
                }
                else if (description.Trim().Length > MaxCommentLength)
                {
                    error = string.Format("Длина поля 'Описание' не может превышать {0} символов.", MaxCommentLength);
                }
                else
                {
                    var model = new SaveAttacmentModel
                    {
                        EntityId = id,
                        EntityTypeId = RequestAttachmentTypeEnum.MissionReport,
                        Description = description.Trim(),
                        FileDto = new UploadFileDto
                        {
                            Context = bytes,
                            FileName = qqFile,
                            //ContextType = Request.Content,
                        }
                    };
                    saveResult = RequestBl.SaveAttachment(model);
                    error = model.Error;
                }
            }
            catch (Exception ex)
            {
                Log.Error("Exception on SaveAttachment:", ex);
                error = ex.GetBaseException().Message;
                saveResult = false;
            }
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            var jsonString = jsonSerializer.Serialize(new SaveTypeResult { Error = error, Result = saveResult });
            return Content(jsonString);
        }
        [HttpGet]
        public ContentResult DeleteAttachment(int id)
        {
            bool saveResult;
            string error;
            try
            {
                DeleteAttacmentModel model = new DeleteAttacmentModel { Id = id };
                saveResult = RequestBl.DeleteAttachment(model);
                error = model.Error;

            }
            catch (Exception ex)
            {
                Log.Error("Exception on DeleteAttachment:", ex);
                error = ex.GetBaseException().Message;
                saveResult = false;
            }
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            var jsonString = jsonSerializer.Serialize(new SaveTypeResult { Error = error, Result = saveResult });
            return Content(jsonString);
        }


        [HttpGet]
        [ReportAuthorize(UserRole.Accountant | UserRole.OutsourcingManager)]
        public ActionResult MissionPurchaseBookDocList()
        {
            var model = RequestBl.GetMissionPurchaseBookDocsListModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult MissionPurchaseBookDocList(MissionPurchaseBookDocListModel model)
        {
            RequestBl.SetMissionPurchaseBookDocsModel(model, !ValidateModel(model));
            return View(model);
        }
        protected bool ValidateModel(MissionPurchaseBookDocListModel model)
        {
            if (model.BeginDate.HasValue && model.EndDate.HasValue &&
                model.BeginDate.Value > model.EndDate.Value)
                ModelState.AddModelError("BeginDate", "Дата в поле <Период с> не может быть больше даты в поле <по>.");
            return ModelState.IsValid;
        }

        [HttpGet]
        [ReportAuthorize(UserRole.Accountant | UserRole.OutsourcingManager)]
        public ActionResult EditMissionPbDocument(int id)
        {
            var model = RequestBl.GetEditMissionPbDocumentModel(id);
            return View(model);
        }

        public ContentResult GetContractorAccount(int id)
        {
            ContractorAccountDto model;
            try
            {
                model = RequestBl.GetContractorAccount(id);
            }
            catch (Exception ex)
            {
                Log.Error("Exception on GetContractorAccount:", ex);
                string error = ex.GetBaseException().Message;
                model = new ContractorAccountDto { Error = string.Format("Ошибка: {0}", error) };
            }
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            var jsonString = jsonSerializer.Serialize(model);
            return Content(jsonString);
        }

        [HttpPost]
        [ReportAuthorize(UserRole.Accountant)]
        public ActionResult EditMissionPbDocument(EditMissionPbDocumentModel model)
        {
            if (!ValidateModel(model))
            {
                RequestBl.ReloadDictionaries(model);
                return View(model);
            }
            string error;
            if (!RequestBl.SaveMissionPbDocumentEditModel(model, out error))
            {
                if (model.ReloadPage)
                {
                    ModelState.Clear();
                    if (!string.IsNullOrEmpty(error))
                        ModelState.AddModelError("", error);
                    return View(RequestBl.GetEditMissionPbDocumentModel(model.Id));
                }
                if (!string.IsNullOrEmpty(error))
                    ModelState.AddModelError("", error);
            }
            return View(model);
        }
        protected bool ValidateModel(EditMissionPbDocumentModel model)
        {
            //if(string.IsNullOrEmpty(model.Number))
            //    ModelState.AddModelError("Number", "СФ (Акт) номер - обязательное поле");
            //if (!model.DocumentDate.HasValue)
            //    ModelState.AddModelError("DocumentDate", "СФ (Акт) дата - неправильная дата");
            return ModelState.IsValid;
        }

        [HttpGet]
        public ActionResult RenderRecords(int id)
        {
            EditMissionPbRecordsModel model = RequestBl.GetPbRecordsModel(id);
            return PartialView("MissionPdRecordPartial", model);
        }

        public ActionResult EditRecordDialog(int id, int documentId)
        {
            try
            {
                MissionPbEditRecordModel model = new MissionPbEditRecordModel { RecordId = id,DocumentId = documentId};
                RequestBl.SetMissionReportEditRecordModel(model);
                return PartialView(model);
            }
            catch (Exception ex)
            {
                Log.Error("Exception", ex);
                string error = "Ошибка при загрузке данных: " + ex.GetBaseException().Message;
                return PartialView("EditRecordDialogError", new DialogErrorModel { Error = error });
            }
        }

       

        [HttpGet]
        public ContentResult GetCostTypes(int reportId,bool isNew)
        {
            PbRecordCostTypesDto model;
            try
            {
                model = RequestBl.GetCostTypes(reportId,isNew);
            }
            catch (Exception ex)
            {
                Log.Error("Exception on GetCostTypes:", ex);
                string error = ex.GetBaseException().Message;
                model = new PbRecordCostTypesDto
                            {
                                Error = string.Format("Ошибка: {0}", error),
                                Children = new List<IdNameDto>()
                            };
            }
            var jsonSerializer = new JavaScriptSerializer();
            string jsonString = jsonSerializer.Serialize(model);
            return Content(jsonString);
        }

        [HttpGet]
        public ContentResult GetRequestNumberForCostType(int reportId,int costTypeId)
        {
            ContractorAccountDto model;
            try
            {
                model = RequestBl.GetRequestNumberForCostType(reportId,costTypeId);
            }
            catch (Exception ex)
            {
                Log.Error("Exception on GetRequestNumberForCostType:", ex);
                string error = ex.GetBaseException().Message;
                model = new ContractorAccountDto
                {
                    Error = string.Format("Ошибка: {0}", error)
                };
            }
            var jsonSerializer = new JavaScriptSerializer();
            string jsonString = jsonSerializer.Serialize(model);
            return Content(jsonString);
        }
        [HttpGet]
        public ContentResult GetReports(int userId)
        {
            PbRecordCostTypesDto model;
            try
            {
                model = RequestBl.GetReportsForPbUserId(userId);
            }
            catch (Exception ex)
            {
                Log.Error("Exception on GetReports:", ex);
                string error = ex.GetBaseException().Message;
                model = new PbRecordCostTypesDto
                {
                    Error = string.Format("Ошибка: {0}", error),
                    Children = new List<IdNameDto>()
                };
            }
            var jsonSerializer = new JavaScriptSerializer();
            string jsonString = jsonSerializer.Serialize(model);
            return Content(jsonString);
        }
        [HttpPost]
        public ContentResult SavePbRecord(SavePbRecordModel model)
        {
            DialogErrorModel result;
            try
            {
                result = new DialogErrorModel{Error = "Test error"};//RequestBl.GetReportsForPbUserId(userId);
            }
            catch (Exception ex)
            {
                Log.Error("Exception on SavePbRecord:", ex);
                string error = ex.GetBaseException().Message;
                result = new DialogErrorModel
                {
                    Error = string.Format("Ошибка: {0}", error)
                };
            }
            var jsonSerializer = new JavaScriptSerializer();
            string jsonString = jsonSerializer.Serialize(result);
            return Content(jsonString);
        }
    }
}