using System;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Reports.Core;
using Reports.Presenters.UI.Bl;
using Reports.Presenters.UI.ViewModel;
using WebMvc.Attributes;

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
        public ActionResult MissionOrderEdit(int id,int? userId)
        {
            MissionOrderEditModel model = RequestBl.GetMissionOrderEditModel(id,userId);
            return View(model);
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