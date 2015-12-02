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
    public class StaffMovementsController : BaseController
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
            var model = StaffMovementsBl.GetListModel();
            return View("StaffMovementsList", model);
        }
        public ContentResult Index(StaffMovementsListModel model)
        {
            var docs=StaffMovementsBl.GetDocuments(model.DepartmentId, model.UserName, model.Number.HasValue?model.Number.Value:0, model.Status);
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
            ModelState.Clear();
            if ((model.Creator == null || model.Creator.Id == 0) && model.Id == 0)
            {
                StaffMovementsBl.SetModel(model);
            }
            else
            {
                //model.AdditionalAgreementDocDto = GetFileContext(Request, ModelState, "AdditionalAgreementDoc");
                model.MaterialLiabilityDocDto = GetFileContext(Request, ModelState, "MaterialLiabilityDoc");
                model.MovementNoteDto = GetFileContext(Request, ModelState, "MovementNote");
                //model.MovementOrderDocDto = GetFileContext(Request, ModelState, "MovementOrderDoc");
                model.RequirementsOrderDocDto = GetFileContext(Request, ModelState, "RequirementsOrderDoc");
                model.ServiceOrderDocDto = GetFileContext(Request, ModelState, "ServiceOrderDoc");
                StaffMovementsBl.SaveModel(model);
                StaffMovementsBl.SetModel(model);
            }
            return View("StaffMovementsEdit", model);

        }
        /*public ActionResult GetPrintModel(int id, int type)
        {
            StaffMovementsPrintModel model=StaffMovementsBl.GetPrintModel(id);
            string view = "";
            switch (type)
            {
                case 1:
                    view = "~/Views/StaffMovements/Templates/MaterialLiabilityDoc.cshtml";
                    break;
                case 2:
                    view = "~/Views/StaffMovements/Templates/Addition.cshtml";
                    break;
                case 3:
                    view = "~/Views/StaffMovements/Templates/Addition2.cshtml";
                    break;
                case 4:
                    view = "~/Views/StaffMovements/Templates/Prilozhenie1.cshtml";
                    break;
                case 5:
                    view = "~/Views/StaffMovements/Templates/prilozhenie2.cshtml";
                    break;
            }
            return View(view, model);
        }*/
        public ContentResult GetPositionsForDepartment(int id)
        {
            var positions = StaffMovementsBl.GetPositionsForDepartment(id);
            return Content(Newtonsoft.Json.JsonConvert.SerializeObject(positions));
        }

        public ContentResult CheckMovementDate(DateTime date, int UserId, int id)
        {
            if ( StaffMovementsBl.CheckMovementsExist(date, UserId,id)) return Content("Error");
            else return Content("Ok");
        }
        public ContentResult SaveDocs(StaffMovementsEditModel model)
        {
            StaffMovementsBl.SaveDocsModel(model);
            return Content("Ok");
        }

        public FileResult GetAgreementDoc(string AgreementNumber, string AgreementDate ,string UserName, string SignerName, string TargetPosition, string TargetDepartment, string SignerShortName, string SignerPositionWithDepartment, string UserShortName)
        {
            var data = NoteDocumentCreator.StaffMovementsDocsCreator.CreateAgreementDoc(Server.MapPath("~/Files"), AgreementNumber, AgreementDate, UserName, SignerName, TargetPosition, TargetDepartment, SignerShortName, SignerPositionWithDepartment, UserShortName);
            return File(data, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "dogovor.docx");
        }
    }
}
