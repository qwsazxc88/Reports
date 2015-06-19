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

        #region Штатные единицы
        /// <summary>
        /// Загрузка структуры подразделений в соответствии с правами текущего пользователя.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.Director | UserRole.Findep | UserRole.PersonnelManager | UserRole.Accountant | UserRole.OutsourcingManager)]
        public ActionResult StaffEstablishedPostRequest()
        {
            StaffEstablishedPostRequestModel model = new StaffEstablishedPostRequestModel();
            model.Departments = StaffListBl.GetDepartmentListByParent("9900424");
            return View(model);
        }
        /// <summary>
        /// Загрузка структуры подразделений в соответствии с правами текущего пользователя.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ReportAuthorize(UserRole.Manager | UserRole.Director | UserRole.Findep | UserRole.PersonnelManager | UserRole.Accountant | UserRole.OutsourcingManager)]
        public ActionResult StaffEstablishedPostRequest(string DepId)
        {
            var jsonSerializer = new JavaScriptSerializer();
            StaffEstablishedPostRequestModel model = StaffListBl.GetDepartmentStructureWithStaffPost(DepId);
            string jsonString = jsonSerializer.Serialize(model);
            return Content(jsonString);
        }
        #endregion

        #region Для тестов
        /// <summary>
        /// Рекурсивное построение дерева.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        //[ReportAuthorize(UserRole.Manager | UserRole.Chief | UserRole.Director | UserRole.Security | UserRole.Trainer | UserRole.PersonnelManager | UserRole.OutsourcingManager | UserRole.Candidate)]
        public ActionResult TreeView()
        {
            TreeViewModel model = StaffListBl.GetDepartmentList();
            return View(model);
        }
        /// <summary>
        /// Загрузка второго уровня подразделений из структуры банка для построения дерева.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult TreeViewAjax()
        {
            TreeViewAjaxModel model = new TreeViewAjaxModel();//StaffListBl.GetDepartmentList();
            model.Departments = StaffListBl.GetDepartmentListByParent("9900424");
            return View(model);
        }
        /// <summary>
        /// Загрузка списка подразделений/должностей (для 7 уровня подразделений) по заданному коду родительского подразделения.
        /// </summary>
        /// <param name="DepId">Код подразделения</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult TreeViewAjax(string DepId)
        {
            TreeViewAjaxModel model = new TreeViewAjaxModel();
            model.Departments = StaffListBl.GetDepartmentListByParent(DepId);
            return Json(model.Departments);
        }
        /// <summary>
        /// Загрузка второго уровня подразделений из структуры банка для построения дерева в таблице.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult TreeGridAjax()
        {
            TreeGridAjaxModel model = StaffListBl.GetDepartmentStructure("9900424");
            return View(model);
        }
        /// <summary>
        /// Загрузка списка подразделений/должностей (для 7 уровня подразделений) по заданному коду родительского подразделения.
        /// </summary>
        /// <param name="DepId">Код подразделения</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult TreeGridAjax(string DepId)
        {
            var jsonSerializer = new JavaScriptSerializer();
            TreeGridAjaxModel model = StaffListBl.GetDepartmentStructure(DepId);

            string jsonString = jsonSerializer.Serialize(model);
            return Content(jsonString);
        }
        /// <summary>
        /// Начальная загрузка формы для построения адресов.
        /// </summary>
        /// <param name="Id">Id адреса</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Address(int? Id)
        {
            AddressModel model = StaffListBl.GetAddress(Id.HasValue ? Id.Value : 0);
            return PartialView(model);
        }
        /// <summary>
        /// Сохраняем составленный адрес.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Address(AddressModel model)
        {
            model = StaffListBl.GetAddress(1);
            return View(model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Code"></param>
        /// <param name="AddressType"></param>
        /// <param name="RegionCode"></param>
        /// <param name="AreaCode"></param>
        /// <param name="CityCode"></param>
        /// <param name="SettlementCode"></param>
        /// <returns></returns>
        [HttpGet]
        public ContentResult GetKladr(string Code, int AddressType)
        {
            var jsonSerializer = new JavaScriptSerializer();
            string jsonString = jsonSerializer.Serialize(StaffListBl.GetKladr(Code, AddressType, null, null, null, null));
            return Content(jsonString);
        }
        /// <summary>
        /// Загрузка страницы структуры подразделений с привязкой к точкам Фиграда.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DepStructureFingradPoints()
        {
            DepStructureFingradPointsModel model = StaffListBl.GetDepartmentStructureWithFingradPoins("9900424");
            return View(model);
        }
        /// <summary>
        /// Загрузка структуры подразделений с привязкой к точкам Фиграда по коду родительского подразделения.
        /// </summary>
        /// <param name="DepId">Код подразделения</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DepStructureFingradPoints(string DepId)
        {
            var jsonSerializer = new JavaScriptSerializer();
            DepStructureFingradPointsModel model = StaffListBl.GetDepartmentStructureWithFingradPoins(DepId);

            string jsonString = jsonSerializer.Serialize(model);
            return Content(jsonString);
        }
        #endregion
        
    }
}
