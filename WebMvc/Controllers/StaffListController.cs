using System;
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
        public ActionResult Address()
        {
            AddressModel model = StaffListBl.GetAddress();
            return View(model);
        }

        [HttpGet]
        public ContentResult GetArea(string RegionCode)
        {
            //DepartmentChildrenDto model;
            //try
            //{
            //    model = RequestBl.GetChildren(parentId, level);
            //}
            //catch (Exception ex)
            //{
            //    Log.Error("Exception on GetChildren:", ex);
            //    string error = ex.GetBaseException().Message;
            //    model = new DepartmentChildrenDto
            //    {
            //        Error = string.Format("Ошибка: {0}", error),
            //        Children = new List<IdNameDto>()
            //    };
            //}
            var jsonSerializer = new JavaScriptSerializer();
            string jsonString = jsonSerializer.Serialize(StaffListBl.GetAreas(RegionCode));
            return Content(jsonString);
        }
    }
}
