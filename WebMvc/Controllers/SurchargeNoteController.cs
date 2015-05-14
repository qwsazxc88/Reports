using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Reports.Presenters.UI.ViewModel;
using Reports.Presenters.UI.Bl;
using Reports.Core;
using Reports.Core.Enum;
namespace WebMvc.Controllers
{
    public class SurchargeNoteController : BaseController
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
        //
        // GET: /SurchargeNote/
        [HttpGet]
        public ActionResult SurchargeNoteList(int type=0)
        {
            SurchargeNoteListModel model = RequestBl.GetSurchargeNoteListModel();
            model.NoteType = type;
            return View(model);
        }
        [HttpPost]
        public ActionResult SurchargeNoteList(SurchargeNoteListModel model)
        {
            RequestBl.SetDocumentsToModel(model);
            return View(model);
        }
        [HttpGet]
        public ActionResult SurchargeNoteEdit(int id=0,int type=0)
        {
            var model = RequestBl.GetSurchargeNoteEditModel(id);
            if (model.Id == 0)
                model.NoteType = type;
            return View("SurchargeNoteEdit",model);
        }
        [HttpPost]
        public ActionResult SurchargeNoteEdit(SurchargeNoteEditModel model)
        {
            if (!ValidateEditModel(model))
            {
                RequestBl.GetDictionaries(model);
                return View(model);
            }
            RequestBl.SaveSurchargeNote(model);
            if (!model.CountantDateAccept.HasValue && !model.PersonnelDateAccept.HasValue)
            {
                var file = GetFileContext(Request, ModelState);
                if (file != null)
                {
                    string attach = "";
                    var attid = RequestBl.SaveAttachment(model.Id, model.AttachmentId, file, RequestAttachmentTypeEnum.SurchargeNoteAttachment, out attach);
                    if (attid.HasValue)
                    {
                        model.AttachmentName = attach;
                        model.AttachmentId = attid.Value;
                    }
                }
            }
            
            return View("SurchargeNoteEdit",model);
        }

        private bool ValidateEditModel(SurchargeNoteEditModel model)
        {
            if (model.Id == 0)
            {
                if (model.File == null || model.File.ContentLength == 0)
                    ModelState.AddModelError("File", "Файл не выбран");
            }
            if (model.DepartmentId == 0)
                ModelState.AddModelError("DepartmentId", "Не выбран департамент");
            else
            {
                if(!RequestBl.CheckDepartmentLevel(model.DepartmentId, 7)) ModelState.AddModelError("DepartmentId","Нужно выбрать департамен 7 уровня");
            }
            if (model.PayDay < DateTime.Parse("01.01.1970"))
                ModelState.AddModelError("PayDay", "Нужно выбрать дату");
            return ModelState.IsValid;
        }
        public FileContentResult ViewAttachment(int id/*,int type*/)
        {
            try
            {
                AttachmentModel model = RequestBl.GetFileContext(id/*,type*/);
                return File(model.Context, model.ContextType, model.FileName);
            }
            catch (Exception ex)
            {
                Log.Error("Error on ViewAttachment:", ex);
                throw;
            }
        

    }
}
}