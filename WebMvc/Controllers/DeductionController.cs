using System.Web.Mvc;
using Reports.Core;
using Reports.Presenters.UI.Bl;
using Reports.Presenters.UI.Bl.Impl;
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
            //int? userId = new int?();
            DeductionEditModel model = RequestBl.GetDeductionEditModel(id);
            return View(model);
        }
    }
}