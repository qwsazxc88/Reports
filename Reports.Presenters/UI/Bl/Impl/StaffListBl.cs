using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reports.Presenters.UI.ViewModel.StaffList;
using Reports.Core;
using Reports.Core.Domain;
using Reports.Core.Dao;
using Reports.Core.Dto;
using System.Web.Mvc;

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
        /// Загружаем структуру по заданному коду подразделения
        /// </summary>
        /// <param name="DepId">Код родительского подразделения</param>
        /// <returns></returns>
        public TreeGridAjaxModel GetDepartmentStructure(string DepId)
        {
            TreeGridAjaxModel model = new TreeGridAjaxModel();
            //если на входе код подразделения 7 уровня, то надо достать должности и сотрудников
            if (DepartmentDao.LoadAll().Where(x => x.Code1C == Convert.ToInt32(DepId)).Single().ItemLevel != 7)
                model.Departments = GetDepartmentListByParent(DepId);
            else
            {
                //таким способом сотрудники загружаются долго, если сделать функцию или представление, то скорость загрузки увеличится в разы
                IList<User> Users = UserDao.LoadAll().Where(x => x.Department != null && x.Department.Code1C == Convert.ToInt32(DepId) && x.IsActive == true && (x.RoleId & 2) > 0).ToList();
                IList<UsersListItemDto> ul = new List<UsersListItemDto>();
                foreach (var item in Users)
                {
                    ul.Add(new UsersListItemDto(item.Id, item.Name, item.Department.Path, item.Department.Name, item.Position.Name, item.Login));
                }
                model.UserPositions = ul;
            }
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
            model.HouseTypes = GetAddressDictionary(1);
            model.BuildTypes = GetAddressDictionary(2);
            model.FlatTypes = GetAddressDictionary(3);
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
        /// <summary>
        /// Заполняем выпадающие списки.
        /// </summary>
        /// <param name="FillType">Переключатель: 1 - дом/владение, 2 - корпус/строение, 3 - квартира/офис.</param>
        /// <returns></returns>
        public IList<IdNameDto> GetAddressDictionary(int FillType)
        {
            IList<IdNameDto> ht = new List<IdNameDto>();

            switch (FillType)
            {
                case 1:
                    //ht.Add(new IdNameDto { Id = 0, Name = "" });
                    ht.Add(new IdNameDto { Id = 1, Name = "дом"});
                    ht.Add(new IdNameDto { Id = 2, Name = "владение" });
                    break;
                case 2:
                    //ht.Add(new IdNameDto { Id = 0, Name = "" });
                    ht.Add(new IdNameDto { Id = 1, Name = "корпус" });
                    ht.Add(new IdNameDto { Id = 2, Name = "строение" });
                    break;
                case 3:
                    //ht.Add(new IdNameDto { Id = 0, Name = "" });
                    ht.Add(new IdNameDto { Id = 1, Name = "кв." });
                    ht.Add(new IdNameDto { Id = 2, Name = "офис" });
                    break;
            }
            

            return ht;
        }
    }
}
