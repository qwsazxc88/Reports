using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Reports.Presenters.UI.ViewModel;
namespace WebMvc.Controllers
{
    public class SurchargeNoteController : Controller
    {
        //
        // GET: /SurchargeNote/
        [HttpGet]
        public ActionResult HolidayNoteList()
        {
            return View();
        }
        [HttpGet]
        public ActionResult HolidayNoteEdit()
        {
            SurchargeNoteEditModel model = new SurchargeNoteEditModel();
            model.NoteType = 1;
            model.CreateDate = DateTime.Now;
            return View(model);
        }

    }
}
