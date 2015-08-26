using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Reports.Core;
using Reports.Presenters.UI.Bl;
using Reports.Presenters.UI.ViewModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
namespace WebMvc.Controllers
{
    public class StaffMovementsController : Controller
    {
        protected IStaffMovementsBl staffMovementsBl;
        public IStaffMovementsBl StaffMovementsBl
        {
            get
            {
                staffMovementsBl = Ioc.Resolve<IStaffMovementsBl>();
                return Validate.Dependency(staffMovementsBl);
            }
        }
        [HttpGet]
        public ActionResult Index()
        {
            var model = StaffMovementsBl.GetListModel();
            return View("StaffMovementsList", model);
        }
        public ContentResult Index(StaffMovementsListModel model)
        {
            var docs=StaffMovementsBl.GetDocuments(model.DepartmentId, model.UserName, model.Number, model.Status);
            return Content(Newtonsoft.Json.JsonConvert.SerializeObject(docs));
        }
        [HttpGet]
        public ActionResult Create()
        {
            var model = StaffMovementsBl.GetEditModel(0);
            return View("StaffMovementsCreate",model);
        }
        
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = StaffMovementsBl.GetEditModel(id);
            return View("StaffMovementsEdit",model);
        }
        [HttpPost]
        public ActionResult Edit(StaffMovementsEditModel model)
        {
            if ((model.Creator == null || model.Creator.Id == 0) && model.Id == 0)
            {
                StaffMovementsBl.SetModel(model);
            }
            else
            {
                StaffMovementsBl.SaveModel(model);
                StaffMovementsBl.SetModel(model);
            }
            return View("StaffMovementsEdit", model);

        }

        public ContentResult GetPositionsForDepartment(int id)
        {
            return Content(Newtonsoft.Json.JsonConvert.SerializeObject(StaffMovementsBl.GetPositionsForDepartment(id)));
        }
    }
}
