﻿using System;
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

    [ReportAuthorize(UserRole.Employee | UserRole.Manager | UserRole.Accountant | UserRole.OutsourcingManager |
        UserRole.Director | UserRole.Secretary | UserRole.Findep | UserRole.Archivist)]

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
            bool hasError = false;
            GpdBl.SetGpdContractView(model, hasError);
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
            //GpdContractModel model = new GpdContractModel();
            bool hasError = false;
            GpdBl.SetGpdContractView(model, hasError);
            return View(model);
        }
        /// <summary>
        /// Вызов страницы создания/редактирования договора.
        /// </summary>
        /// <param name="Id">ID записи.</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GpdContractEdit(int Id)
        {
            bool hasError = false;
            GpdContractEditModel model = GpdBl.SetGpdContractEdit(Id, hasError);
            ModelState.Clear();
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
            bool hasError = false;
            ModelState.Clear();

            model = GpdBl.EditDetailsFromContract(model, ModelState);
            return View(model);
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
            bool hasError = false;
            GpdRefDetailEditModel model = GpdBl.SetRefDetailEditModel(Id, Id == 0 ? 4 : 2, hasError);
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
            bool hasError = false;
            ModelState.Clear();
            if (model.StatusID != 2)
            {
                model = GpdBl.SetRefDetailEditModel(model.Id, model.StatusID, hasError);
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
                        //думал, что после сохранения нужно возвращаться к списку
                        //return RedirectToAction("GpdRefDetail");
                        model = GpdBl.SetRefDetailEditModel(model.Id, model.StatusID, hasError);
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
        public ActionResult GpdActEdit(int Id, int GCID)
        {
            bool hasError = false;
            GpdActEditModel model = GpdBl.SetActEditModel(Id, GCID, hasError);
            ModelState.Clear();
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
            if (!model.IsCancel)
                GpdBl.CheckFillFieldsForGpdAct(model, ModelState);
            if (ModelState.Count != 0)
                return View(model);
            else
            {
                string error;
                if (GpdBl.SaveGpdAct(model, out error))
                {
                    model = GpdBl.SetActEditModel(model.Id, model.GCID, hasError);
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
}
