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
using Reports.Core.Dao;
using Reports.Core.Domain;
using Reports.Core.Dto;
using Reports.Core.Enum;
using Reports.Presenters.UI.Bl;
using Reports.Presenters.UI.ViewModel;
using WebMvc.Attributes;
using System.Web.Routing;

namespace WebMvc.Controllers
{
    /// <summary>
    /// Контролер для ГПД.
    /// </summary>
    [PreventSpamAttribute]
    [ReportAuthorize(UserRole.Accountant | UserRole.OutsourcingManager)]

    public class GPDController : BaseController
    {
        protected IGpdBl gpdBl;
        public IGpdBl GpdBl
        {
            get
            {
                gpdBl = Ioc.Resolve<IGpdBl>();
                return Validate.Dependency(gpdBl);
            }
        }
        /// <summary>
        /// Просмотр договоров.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            GpdContractModel model = new GpdContractModel();
            GpdBl.SetGpdContractView(model);
            return View(model);
        }
        /// <summary>
        /// Просмотр договоров с возможными сортировками в таблице.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(GpdContractModel model)
        {
            GpdBl.SetGpdContractView(model);
            return View(model);
        }
        /// <summary>
        /// Вызов страницы создания/редактирования договора.
        /// </summary>
        /// <param name="Id">ID записи.</param>
        /// <param name="PersonID">ID физического лица</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GpdContractEdit(int Id, int PersonID, string Msg)
        {
            GpdContractEditModel model = GpdBl.SetGpdContractEdit(Id, PersonID, 0, null);
            ModelState.Clear();
            if (Msg != null && Msg.Trim().Length != 0)
                ModelState.AddModelError("errorMessage", Msg);
            if (model.hasErrors)
                ModelState.AddModelError("errorMessage", "Произошла ошибка при загрузке страницы!");
            return View(model);
        }
        /// <summary>
        /// Сохраняем договор.
        /// </summary>
        /// <param name="model">Обрабатываемая модель.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GpdContractEdit(GpdContractEditModel model)
        {
            ModelState.Clear();
            int StatusID = model.StatusID;
            if (model.Operation == 1)
            {
                model = GpdBl.SetGpdContractEdit(model);
                return View(model);
            }

            if (model.StatusID == 2 || model.StatusID == 4)
                GpdBl.CheckFillFieldsForGpdContract(model, ModelState);

            if (ModelState.Count != 0)
            {
                model = GpdBl.SetGpdContractEdit(model);//чтобы не пропадали данные
                model.StatusID = 4;
                return View(model);
            }
            string error;
            //сохранение договора
            if (GpdBl.SaveGpdContract(model, out error))
            {
                //model = GpdBl.SetGpdContractEdit(model.Id, model.PersonID, 0, null);
                string Message = StatusID == 4 ? "Черновик вашего документа сохранен!" : (StatusID == 2 ? "Ваш документ успешно сохранен!" : "Занесение вашего документа отменено!");
                //ModelState.AddModelError("errorMessage", Message);
                //Response.Redirect(Url.Action("GpdContractEdit", "GPD", new { Id = model.Id, PersonID = 0 }));//чтобы не срабатывало нажатие на F5 после сохранения и не множились записи
                //Session["GpdContractEdit_Post"] = true;
                return RedirectToAction("GpdContractEdit", "GPD", new { Id = model.Id, PersonID = 0, Msg = Message });//чтобы не срабатывало нажатие на F5 после сохранения и не множились записи
                //return View(model);
            }
            else
            {
                if (!string.IsNullOrEmpty(error))
                    ModelState.AddModelError("errorMessage", error);
                model = GpdBl.SetGpdContractEdit(model.Id, model.PersonID, 0, null);
                return View(model);
            }
        }
        
        /// <summary>
        /// Просмотр справочника реквизитов.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GpdRefDetail()
        {
            GpdRefDetailModel model = new GpdRefDetailModel();
            ModelState.Clear();
            bool hasError = false;
            GpdBl.SetGpdRefDetailView(model, hasError);
            if (model.hasErrors)
                ModelState.AddModelError("errorMessage", "Произошла ошибка при загрузке страницы!");
            return View(model);
        }
        /// <summary>
        /// Показываем результаты поиска.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GpdRefDetail(GpdRefDetailModel model)
        {
            ModelState.Clear();
            bool hasError = false;
            GpdBl.SetGpdRefDetailFind(model, hasError);
            if (model.hasErrors)
                ModelState.AddModelError("errorMessage", "Произошла ошибка при загрузке страницы!");
            return View(model);
        }
        /// <summary>
        /// Вызываем форму создания/редактирования записи в справочнике реквизитов.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GpdRefDetailEdit(int Id)
        {
            GpdRefDetailEditModel model = GpdBl.SetRefDetailEditModel(Id, Id == 0 ? 4 : 2, 0, false, 1, 0, 0, 0, 0);
            ModelState.Clear();
            if (model.hasErrors)
                ModelState.AddModelError("errorMessage", "Произошла ошибка при загрузке страницы!");
            return View(model);
        }
        /// <summary>
        /// Сохраняем данные в справочнике реквизитов.
        /// </summary>
        /// <param name="model">Модель редактирования.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GpdRefDetailEdit(GpdRefDetailEditModel model)
        {
            ModelState.Clear();
            if (model.StatusID != 2)
            {
                int PersonID = model.PersonID;
                model = GpdBl.SetRefDetailEditModel(model.Id, model.StatusID, model.Operation, false, model.DTID, model.PayerID, model.PayeeID, model.DetailId, model.PersonID);
                model.PersonID = PersonID;
                return View(model);
            }
            else
            {
                GpdBl.CheckFillFieldsForGpdRefDetail(model, ModelState, false);
                if (ModelState.Count != 0)
                    return View(model);
                else
                {
                    string error;
                    if (GpdBl.SaveGpdRefDetail(model, out error))
                    {
                        model = GpdBl.SetRefDetailEditModel(model.Id, model.StatusID, 0, false, model.DTID, model.PayerID, model.PayeeID, model.DetailId, model.PersonID);
                        if (model.StatusID == 2)
                            model.errorMessage = "Запись сохранена!";
                        return View(model);
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(error))
                            ModelState.AddModelError("errorMessage", error);
                        return View(model);
                    }
                }
            }
        }
        /// <summary>
        /// Вызываем форму просмотра актов ГПД.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GpdActList()
        {
            bool hasError = false;
            GpdActListModel model = new GpdActListModel();
            GpdBl.SetGpdActFind(model, hasError);
            if (model.hasErrors)
                ModelState.AddModelError("errorMessage", "Произошла ошибка при загрузке страницы!");
            return View(model);
        }
        /// <summary>
        /// Просмотр актов ГПД с возможными сортировками в таблице.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GpdActList(GpdActListModel model)
        {
            //GpdContractModel model = new GpdContractModel();
            bool hasError = false;
            GpdBl.SetGpdActView(model, hasError);
            return View(model);
        }
        /// <summary>
        /// Вызываем форму создания/редактирования акта ГПД.
        /// </summary>
        /// <param name="Id">ID акта.</param>
        /// <param name="GCID">ID договора.</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GpdActEdit(int Id, int GCID, string Msg)
        {
            bool hasError = false;
            GpdActEditModel model = GpdBl.SetActEditModel(Id, GCID, hasError);

            ModelState.Clear();
            if (Msg != null && Msg.Trim().Length != 0)
                ModelState.AddModelError("errorMessage", Msg);

            if (model.hasErrors)
                ModelState.AddModelError("errorMessage", "Произошла ошибка при загрузке страницы!");
            return View(model);
        }
        /// <summary>
        /// Различные телодвижения в составлении/редактировании акта ГПД..
        /// </summary>
        /// <param name="model">Модель создания/редактирования акта ГПД.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GpdActEdit(GpdActEditModel model)
        {
            bool hasError = false;
            ModelState.Clear();
            int StatusID = model.StatusID;
            if (model.Operation == 1)
            {
                //model = GpdBl.SetGpdContractEdit(model.Id, model.PersonID, model.DepartmentId, model.DepartmentName);
                model = GpdBl.SetActEditModel(model);
                return View(model);
            }

            if (!model.IsCancel)
            {
                GpdBl.CheckFillFieldsForGpdAct(model, ModelState);
                model = GpdBl.SetActEditModel(model);
            }
            if (ModelState.Count != 0)
            {
                model.StatusID = 4;
                return View(model);
            }
            else
            {
                string error;
                if (GpdBl.SaveGpdAct(model, out error))
                {
                    //model = GpdBl.SetActEditModel(model.Id, model.GCID, hasError);
                    string Message = StatusID == 4 ? "Черновик вашего документа сохранен!" : (StatusID == 2 ? "Ваш документ успешно сохранен!" : "Занесение вашего документа отменено!");
                    //ModelState.AddModelError("errorMessage", Message);
                    return RedirectToAction("GpdActEdit", "GPD", new { Id = model.Id, GCID = 0, Msg = Message });//View(model);
                    //return View(model);
                }
                else
                {
                    if (!string.IsNullOrEmpty(error))
                        ModelState.AddModelError("errorMessage", error);
                    model = GpdBl.SetActEditModel(model.Id, model.GCID, hasError);
                    return View(model);
                }
            }
        }
        /// <summary>
        /// Автозаполнение реквизитов плательщика.
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        public ActionResult AutocompleteDetailPayer(string term)
        {
            IList<GpdContractDetailDto> Details = GpdBl.GetDetailsAutocomplete(term, 0);
            var DetailsList = Details.ToList().Select(a => new { label = a.LongName, PayerID = a.Id }).Distinct();

            return Json(DetailsList, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Автозаполнение реквизитов получателя.
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        public ActionResult AutocompleteDetailPayeer(string term)
        {
            IList<GpdContractDetailDto> Details = GpdBl.GetDetailsAutocomplete(term, 0);
            var DetailsList = Details.ToList().Select(a => new { label = a.LongName, PayeeID = a.Id }).Distinct();

            return Json(DetailsList, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Автозаполнение лицевого счета получателя.
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        public ActionResult AutocompleteDetailPAccount(string term)
        {
            IList<GpdContractDetailDto> Details = GpdBl.GetDetailsAutocomplete(term, 0);
            var DetailsList = Details.ToList().Select(a => new { label = a.LongName, PAccountID = a.Id }).Distinct();

            return Json(DetailsList, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Автозаполнение фио в создании набора реквизитов.
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        public ActionResult AutocompletePersonSearch(string term)
        {
            IList<GpdContractSurnameDto> Persons = GpdBl.GetPersonAutocomplete(term, 0);
            //var PersonList = Persons.Where(a => a.Name.Contains(term)).ToList().Select(a => new { label = a.Name, snils = a.SNILS, PersonID = a.Id }).Distinct();
            var PersonList = Persons.ToList().Select(a => new { label = a.LongName, PersonID = a.Id }).Distinct();

            return Json(PersonList, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Автозаполнение для выбора набора в акте.
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        public ActionResult AutocompleteDetSetSearch(string term)
        {
            IList<GpdContractSurnameDto> Persons = GpdBl.GetPersonDSAutocomplete(term, 0);
            //var PersonList = Persons.ToList().Select(a => new { label = a.LongName, PayerName = a.PayerName, PayeeName = a.PayeeName, BankName = a.BankName, Account = a.Account, PersonID = a.PersonID, DSID = a.Id }).Distinct();
            var PersonList = Persons.ToList().Select(a => new { label = a.Name, DSID = a.Id }).Distinct();

            return Json(PersonList, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Всплывающее окно для создания/редактирования реквизита.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GpdRefDetailDialog(int id)
        {
            try
            {
                int DetailType = Convert.ToInt32(id.ToString().Substring(0, 1));
                id = Convert.ToInt32(id.ToString().Substring(1));
                GpdRefDetailDialogModel model = GpdBl.SetDetailDialog(id);
                model.DetailType = DetailType;
                return PartialView(model);
            }
            catch (Exception ex)
            {
                Log.Error("Exception", ex);
                string error = "Ошибка при загрузке данных: " + ex.GetBaseException().Message;
                return PartialView("DialogError", new DialogErrorModel { Error = error });
            }
        }
        /// <summary>
        /// Редактируем реквизит из диалогового окна.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GpdRefDetailDialog(GpdRefDetailDialogModel model)
        {
            //модальное окно посылает и принимает форму через ajax
            ModelState.Clear();
            GpdBl.CheckFillFieldsForGpdRefDetailDialog(model, ModelState);
            int DetailType = model.DetailType;
            if (ModelState.Count != 0)
            {
                return PartialView(model);
            }
            else
            {
                string error;
                if (GpdBl.SaveGpdRefDetailDialog(model, out error))
                {
                    model = GpdBl.SetDetailDialog(model.Id);
                    model.DetailType = DetailType;
                    ModelState.AddModelError("errorMessage", "Реквизит успешно сохранен!");
                    return PartialView(model);
                }
                else
                {
                    if (!string.IsNullOrEmpty(error))
                        ModelState.AddModelError("errorMessage", error);
                    return PartialView(model);
                }
            }
        }
    }
}
