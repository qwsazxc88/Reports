﻿using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Reports.Core;
using Reports.Core.Dto;
using Reports.Presenters.Services;
using Reports.Presenters.UI.Bl;
using Reports.Presenters.UI.ViewModel;
using WebMvc.Models;

namespace WebMvc.Controllers
{
    [Authorize]
    public class HomeController : BaseController
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

        public ActionResult Index(int? menuId)
        {
            //ViewBag.Message = "Welcome to ASP.NET MVC!";
            var service = Ioc.Resolve<IAuthenticationService>();
            IUser user = service.CurrentUser;
            return View(new HomeModel {menuId = menuId.HasValue ? menuId.Value : 0});
        }

        public ActionResult About()
        {
            return View();
        }

        [HttpGet]
        public ActionResult DepartmentDialog(int id /*, int typeId*/)
        {
            try
            {
                //DepartmentTreeModel model = new DepartmentTreeModel { DepartmentID = id };
                DepartmentTreeModel model = RequestBl.GetDepartmentTreeModel(id);
                return PartialView(model);
            }
            catch (Exception ex)
            {
                Log.Error("Exception", ex);
                string error = "Ошибка при загрузке данных: " + ex.GetBaseException().Message;
                return PartialView("DialogError", new DialogErrorModel {Error = error});
            }
        }
       
        [HttpGet]
        public ContentResult GetChildren(int parentId, int level)
        {
            DepartmentChildrenDto model;
            try
            {
                model = RequestBl.GetChildren(parentId, level);
            }
            catch (Exception ex)
            {
                Log.Error("Exception on GetChildren:", ex);
                string error = ex.GetBaseException().Message;
                model = new DepartmentChildrenDto
                            {
                                Error = string.Format("Ошибка: {0}", error),
                                Children = new List<IdNameDto>()
                            };
            }
            var jsonSerializer = new JavaScriptSerializer();
            string jsonString = jsonSerializer.Serialize(model);
            return Content(jsonString);
        }

        [HttpGet]
        public ActionResult SetShortNameDialog()
        {
            try
            {
                //DepartmentTreeModel model = new DepartmentTreeModel { DepartmentID = id };
                TerraGraphicsSetShortNameModel model = RequestBl.SetShortNameModel();
                return PartialView(model);
            }
            catch (Exception ex)
            {
                Log.Error("Exception", ex);
                string error = "Ошибка при загрузке данных: " + ex.GetBaseException().Message;
                return PartialView("TpDialogError", new DialogErrorModel { Error = error });
            }
        }

        [HttpGet]
        public ContentResult GetTerraPointChildren(int parentId, int level)
        {
            TerraPointChildrenDto model;
            try
            {
                model = RequestBl.GetTerraPointChildren(parentId, level);
            }
            catch (Exception ex)
            {
                Log.Error("Exception on GetTerraPointChildren:", ex);
                string error = ex.GetBaseException().Message;
                model = new TerraPointChildrenDto
                {
                    Error = string.Format("Ошибка: {0}", error),
                    Children = new List<IdNameDto>()
                };
            }
            var jsonSerializer = new JavaScriptSerializer();
            string jsonString = jsonSerializer.Serialize(model);
            return Content(jsonString);
        }
        [HttpGet]
        public ContentResult GetTerraPointShortName(int pointId)
        {
            TerraPointShortNameDto model;
            try
            {
                model = RequestBl.GetTerraPointShortName(pointId);
            }
            catch (Exception ex)
            {
                Log.Error("Exception on GetTerraPointShortName:", ex);
                string error = ex.GetBaseException().Message;
                model = new TerraPointShortNameDto
                {
                    Error = string.Format("Ошибка: {0}", error),
                    ShortName = string.Empty,
                };
            }
            var jsonSerializer = new JavaScriptSerializer();
            string jsonString = jsonSerializer.Serialize(model);
            return Content(jsonString);
        }
        [HttpGet]
        public ContentResult SaveTerraPointShortName(int pointId, string shortName)
        {
            TerraPointShortNameDto model;
            try
            {
                model = RequestBl.SaveTerraPointShortName(pointId,shortName);
            }
            catch (Exception ex)
            {
                Log.Error("Exception on SaveTerraPointShortName:", ex);
                string error = ex.GetBaseException().Message;
                model = new TerraPointShortNameDto
                {
                    Error = string.Format("Ошибка: {0}", error),
                    ShortName = string.Empty,
                };
            }
            var jsonSerializer = new JavaScriptSerializer();
            string jsonString = jsonSerializer.Serialize(model);
            return Content(jsonString);
        }
    }
}