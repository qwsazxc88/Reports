using System;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Reports.Core;
using Reports.Presenters.UI.Bl;
using Reports.Presenters.UI.ViewModel;
using WebMvc.Attributes;
using System.Web.Routing;

namespace WebMvc.Controllers
{
    [ReportAuthorize(UserRole.Employee | UserRole.Manager | UserRole.Accountant  | UserRole.OutsourcingManager)]
    public class MissionOrderController : BaseController
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
        [HttpGet]
        public ActionResult Index()
        {
            var model = RequestBl.GetMissionOrderListModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(MissionOrderListModel model)
        {
            RequestBl.SetMissionOrderListModel(model, !ValidateModel(model));
            return View(model);
        }
        protected bool ValidateModel(MissionOrderListModel model)
        {
            if (model.BeginDate.HasValue && model.EndDate.HasValue &&
                model.BeginDate.Value > model.EndDate.Value)
                ModelState.AddModelError("BeginDate", "Дата в поле <Период с> не может быть больше даты в поле <по>.");
            return ModelState.IsValid;
        }

        [HttpGet]
        [ReportAuthorize(UserRole.Manager)]
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
                RequestBl.LoadDictionaries(model);
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
            return ModelState.IsValid;
        }
        protected void CorrectDropdowns(MissionOrderEditModel model)
        {
             if (!model.IsEditable)
             {
                 model.TypeId = model.TypeIdHidden;
                 model.GoalId = model.GoalIdHidden;
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

    }
}