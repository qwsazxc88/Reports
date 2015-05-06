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
            model.Regions = KladrDao.GetRegions();
            model.Areas = KladrDao.GetAreas(null);
            model.Cityes = KladrDao.GetCityes(null, null);
            model.Settlements = KladrDao.GetSettlements(null, null, null);
            return model;
        }
        /// <summary>
        /// Загружаем список районов.
        /// </summary>
        /// <param name="RegionCode">Код региона.</param>
        /// <returns></returns>
        public IList<KladrDto> GetAreas(string RegionCode)
        {
            KladrDto row = KladrDao.GetKladrByCode(RegionCode).Single();
            return KladrDao.GetAreas(row.RegionCode);
        }
    }
}
