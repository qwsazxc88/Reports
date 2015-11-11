using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Reports.Presenters.UI.Bl;
using Reports.Core;
using Reports.Presenters.UI.ViewModel;
using Newtonsoft.Json;
namespace WebMvc.Controllers
{
    [Authorize]
    public class DocumentMovementsController : Controller
    {
        //
        // GET: /DocumentMovements/
        protected IDocumentMovementsBl documentMovementsBl;
        public IDocumentMovementsBl DocumentMovementsBl
        {
            get
            {
                documentMovementsBl = Ioc.Resolve<IDocumentMovementsBl>();
                return Validate.Dependency(documentMovementsBl);
            }
        }

        public ActionResult Index()
        {
            return RedirectToAction("DocumentMovementsList");
        }
        [HttpGet]
        public ActionResult DocumentMovementsList()
        {
            var model = DocumentMovementsBl.GetListModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult DocumentMovementsList(DocumentMovementsListModel model)
        {
            model.Documents = DocumentMovementsBl.GetDocuments(model);
            return View(model);
        }
        [HttpPost]
        public ContentResult DocumentMovementsListJson(DocumentMovementsListModel model)
        {
            model.Documents = DocumentMovementsBl.GetDocuments(model);            
            return Content(Newtonsoft.Json.JsonConvert.SerializeObject( model.Documents));
        }
        [HttpGet]
        public ActionResult DocumentMovementsEdit(int id)
        {
            var model = DocumentMovementsBl.GetEditModel(id);
            return View(model);
        }
        [HttpPost]
        public ActionResult DocumentMovementsEdit(DocumentMovementsEditModel model)
        {
            model = DocumentMovementsBl.SaveModel(model);
            return View(model);
        }
        [HttpPost]
        public ContentResult GetRuscountUsers(string name)
        {
            return Content(JsonConvert.SerializeObject(DocumentMovementsBl.GetRuscountUsers(name)));
        }
    }
}
