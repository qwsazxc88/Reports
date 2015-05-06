using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reports.Presenters.UI.ViewModel.StaffList;
using Reports.Core;
using Reports.Core.Domain;
using Reports.Core.Dao;
using Reports.Core.Dto;

namespace Reports.Presenters.UI.Bl.Impl
{
    public class StaffListBl : RequestBl, IStaffListBl
    {
        #region Dependencies
        protected IKladrDao kladrDao;
        public IKladrDao KladrDao
        {
            get { return Validate.Dependency(kladrDao); }
            set { kladrDao = value; }
        }
        #endregion
        /// <summary>
        /// собираем полное дерево
        /// </summary>
        /// <returns></returns>
        public TreeViewModel GetDepartmentList()
        {
            User currentUser = UserDao.Load(AuthenticationService.CurrentUser.Id);
            TreeViewModel model = new TreeViewModel();
            //model.Departments = DepartmentDao.LoadAll().Where(x => x.Path.StartsWith(currentUser.Department.Path)).ToList();
            //model.Departments = DepartmentDao.LoadAll().Where(x => x.Id == currentUser.Department.Id).ToList();
            //model.ParentId = currentUser.Department.ParentId.ToString();
            //model.DepId = currentUser.Department.Code;
            model.Departments = DepartmentDao.LoadAll();
            return model;
        }
        /// <summary>
        /// подгружаем только подчиненые ветки на один уровень ниже
        /// </summary>
        /// <param name="DepId"></param>
        /// <returns></returns>
        public IList<Department> GetDepartmentListByParent(string DepId)
        {
            if (string.IsNullOrEmpty(DepId))
                return DepartmentDao.LoadAll().Where(x => x.ItemLevel == 2).ToList();
            else
                return DepartmentDao.LoadAll().Where(x => x.ParentId.ToString() == DepId).ToList();
        }

        /// <summary>
        /// Загружаем модель для составления Российских адресов.
        /// </summary>
        /// <returns></returns>
        public AddressModel GetAddress()
        {
            AddressModel model = new AddressModel();
            model.Regions = KladrDao.GetKladr(1, null, null, null, null);
            model.Areas = KladrDao.GetKladr(2, null, null, null, null);
            model.Cityes = KladrDao.GetKladr(3, null, null, null, null);
            model.Settlements = KladrDao.GetKladr(4, null, null, null, null);
            model.Streets = KladrDao.GetKladr(5, null, null, null, null);
            return model;
        }
        /// <summary>
        /// Загружаем список районов.
        /// </summary>
        /// <param name="Code">Код записи.</param>
        /// <param name="AddressType">Тип записи.</param>
        /// <returns></returns>
        public IList<KladrDto> GetKladr(string Code, int AddressType)
        {
            KladrDto row = KladrDao.GetKladrByCode(Code).Single();
            return KladrDao.GetKladr(AddressType, row.RegionCode, row.AreaCode, row.CityCode, row.SettlementCode);
        }
    }
}
