﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMvc.Attributes;
using Reports.Presenters.UI.Bl;
using Reports.Core;
using Reports.Core.Domain;
using Reports.Presenters.UI.ViewModel.StaffList;
using System.Web.Script.Serialization;

namespace WebMvc.Controllers
{
    public class StaffListController : Controller
    {
        #region Dependencies
        protected IStaffListBl stafflistBl;

        public IStaffListBl StaffListBl
        {
            get
            {
                stafflistBl = Ioc.Resolve<IStaffListBl>();
                return Validate.Dependency(stafflistBl);
            }
        }
        #endregion

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        //[ReportAuthorize(UserRole.Manager | UserRole.Chief | UserRole.Director | UserRole.Security | UserRole.Trainer | UserRole.PersonnelManager | UserRole.OutsourcingManager | UserRole.Candidate)]
        public ActionResult TreeView()
        {
            TreeViewModel model = StaffListBl.GetDepartmentList();
            return View(model);
        }

        [HttpGet]
        public ActionResult TreeViewAjax()
        {
            TreeViewAjaxModel model = new TreeViewAjaxModel();//StaffListBl.GetDepartmentList();
            model.Departments = StaffListBl.GetDepartmentListByParent("9900424");
            return View(model);
        }

        [HttpPost]
        public ActionResult TreeViewAjax(string DepId)
        {
            TreeViewAjaxModel model = new TreeViewAjaxModel(); 
            model.Departments = StaffListBl.GetDepartmentListByParent(DepId);
            return Json(model.Departments);
        }

        [HttpGet]
        public ActionResult TreeGridAjax()
        {
            TreeGridAjaxModel model = StaffListBl.GetDepartmentStructure("9900424");
            return View(model);
        }

        [HttpPost]
        public ActionResult TreeGridAjax(string DepId)
        {
            var jsonSerializer = new JavaScriptSerializer();
            TreeGridAjaxModel model = StaffListBl.GetDepartmentStructure(DepId);
            
            string jsonString = jsonSerializer.Serialize(model);
            return Content(jsonString);
        }

        [HttpGet]
        public ActionResult Address()
        {
            AddressModel model = StaffListBl.GetAddress();
            return View(model);
        }

        [HttpPost]
        public ActionResult Address(AddressModel model)
        {
            model = StaffListBl.GetAddress(model);
            return View(model);
        }

        [HttpGet]
        public ContentResult GetKladr(string Code, int AddressType, string RegionCode, string AreaCode, string CityCode, string SettlementCode)
        {
            var jsonSerializer = new JavaScriptSerializer();
            string jsonString = jsonSerializer.Serialize(StaffListBl.GetKladrNew(Code, AddressType, RegionCode, AreaCode, CityCode, SettlementCode));
            return Content(jsonString);
        }
    }
}
