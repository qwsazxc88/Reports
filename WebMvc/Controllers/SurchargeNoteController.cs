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
        public ActionResult SurchargeNoteList()
        {
            SurchargeNoteListModel model = RequestBl.GetSurchargeNoteListModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult SurchargeNoteList(SurchargeNoteListModel model)
        {
            RequestBl.SetDocumentsToModel(model);
            return View(model);
        }
        [HttpGet]
        public ActionResult SurchargeNoteEdit(int id=0)
        {
            var model = RequestBl.GetSurchargeNoteEditModel(id);
            return View("SurchargeNoteEdit",model);
        }
        [HttpPost]
        public ActionResult SurchargeNoteEdit(SurchargeNoteEditModel model)
        {
            RequestBl.SaveSurchargeNote(model);
            var file = GetFileContext(Request,ModelState);
            string attach = "";
            var attid = RequestBl.SaveAttachment(model.Id, model.AttachmentId, file, RequestAttachmentTypeEnum.SurchargeNoteAttachment,out attach);
            if (attid.HasValue)
            {
                model.AttachmentName = attach;
                model.AttachmentId = attid.Value;
            }
            model.NoteType = 1;
            model.CreateDate = DateTime.Now;
            return View("SurchargeNoteEdit",model);
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