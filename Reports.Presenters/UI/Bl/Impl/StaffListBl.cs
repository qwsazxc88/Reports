﻿using System;
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
        /// <param name="Id">Id адреса</param>
        /// <returns></returns>
        public AddressModel GetAddress(int Id)
        {
            AddressModel model = new AddressModel();
            if (Id == 0)    //загрузка без иденификатора
            {
                model.Regions = KladrDao.GetKladr(1, null, null, null, null);
                model.Areas = KladrDao.GetKladr(2, null, null, null, null);
                model.Cityes = KladrDao.GetKladr(3, null, null, null, null);
                model.Settlements = KladrDao.GetKladr(4, null, null, null, null);
                model.Streets = KladrDao.GetKladr(5, null, null, null, null);
                model.HouseTypes = GetAddressDictionary(1);
                model.BuildTypes = GetAddressDictionary(2);
                model.FlatTypes = GetAddressDictionary(3);
            }
            else
            {
                //тут по Id записи достаем строку с адресом и строим форму
                model.Regions = KladrDao.GetKladr(1, null, null, null, null);
                model.RegionCode = "770000000000000";

                model.Areas = KladrDao.GetKladr(2, !string.IsNullOrEmpty(model.RegionCode) ? model.Regions.Where(x => x.Code == model.RegionCode).Single().RegionCode : null, null, null, null);
                model.AreaCode = string.Empty; ;

                model.Cityes = KladrDao.GetKladr(3, !string.IsNullOrEmpty(model.RegionCode) ? model.Regions.Where(x => x.Code == model.RegionCode).Single().RegionCode : null,
                    !string.IsNullOrEmpty(model.AreaCode) ? model.Areas.Where(x => x.Code == model.AreaCode).Single().AreaCode : null, null, null);
                model.CityCode = "770000020000000";

                model.Settlements = KladrDao.GetKladr(4, !string.IsNullOrEmpty(model.RegionCode) ? model.Regions.Where(x => x.Code == model.RegionCode).Single().RegionCode : null,
                    !string.IsNullOrEmpty(model.AreaCode) ? model.Areas.Where(x => x.Code == model.AreaCode).Single().AreaCode : null,
                    !string.IsNullOrEmpty(model.CityCode) ? model.Cityes.Where(x => x.Code == model.CityCode).Single().CityCode : null, null);
                model.SettlementCode = string.Empty;

                model.Streets = KladrDao.GetKladr(5, !string.IsNullOrEmpty(model.RegionCode) ? model.Regions.Where(x => x.Code == model.RegionCode).Single().RegionCode : null,
                    !string.IsNullOrEmpty(model.AreaCode) ? model.Areas.Where(x => x.Code == model.AreaCode).Single().AreaCode : null,
                    !string.IsNullOrEmpty(model.CityCode) ? model.Cityes.Where(x => x.Code == model.CityCode).Single().CityCode : null,
                    !string.IsNullOrEmpty(model.SettlementCode) ? model.Settlements.Where(x => x.Code == model.SettlementCode).Single().SettlementCode : null);
                model.StreetCode = "770000020004549";

                model.HouseTypes = GetAddressDictionary(1);
                model.HouseType = 1;
                model.HouseNumber = string.Empty;
                model.BuildTypes = GetAddressDictionary(2);
                model.BuildType = 1;
                model.BuildNumber = "1133";
                model.FlatTypes = GetAddressDictionary(3);
                model.FlatType = 1;
                model.FlatNumber = "7";
                model.PostIndex = "124460";
                model.Address = GetAddressStr(model);
            }
            return model;
        }
        /// <summary>
        /// По заполненной модели строим строку адреса.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        protected string GetAddressStr(AddressModel model)
        {
            string AddressStr = "";

            if (!string.IsNullOrEmpty(model.PostIndex))
                AddressStr = model.PostIndex + ", ";

            if(!string.IsNullOrEmpty(model.RegionCode))
            {
                AddressStr += model.Regions.Where(x => x.Code == model.RegionCode).Single().Name;
            }

            if(!string.IsNullOrEmpty(model.AreaCode))
            {
                AddressStr += ", " +  model.Areas.Where(x => x.Code == model.AreaCode).Single().Name;
            }

            if(!string.IsNullOrEmpty(model.CityCode))
            {
                AddressStr += ", " +  model.Cityes.Where(x => x.Code == model.CityCode).Single().Name;
            }

            if(!string.IsNullOrEmpty(model.SettlementCode))
            {
                AddressStr += ", " +  model.Settlements.Where(x => x.Code == model.SettlementCode).Single().Name;
            }

            if(!string.IsNullOrEmpty(model.StreetCode))
            {
                AddressStr += ", " +  model.Streets.Where(x => x.Code == model.StreetCode).Single().Name;
            }

            
            if(!string.IsNullOrEmpty(model.HouseNumber)){
                AddressStr += (AddressStr == "" ? "" :  ", ") + model.HouseTypes.Where(x => x.Id == model.HouseType).Single().Name;
                AddressStr += (AddressStr == "" ? "" :  " №") + model.HouseNumber;
            }


            if(!string.IsNullOrEmpty(model.BuildNumber)){
                AddressStr += (AddressStr == "" ? "" :  ", ") + model.BuildTypes.Where(x => x.Id == model.BuildType).Single().Name;
                AddressStr += (AddressStr == "" ? "" :  " ") + model.BuildNumber;
            }


            if (!string.IsNullOrEmpty(model.FlatNumber))
            {
                AddressStr += (AddressStr == "" ? "" : ", ") + model.FlatTypes.Where(x => x.Id == model.FlatType).Single().Name;
                AddressStr += (AddressStr == "" ? "" : " ") + model.FlatNumber;
            }

            return AddressStr;
        }
        /// <summary>
        /// Загружаем список объектов частей адресов.
        /// </summary>
        /// <param name="Code">Код объекта.</param>
        /// <param name="AddressType">Тип объекта.</param>
        /// <param name="RegionCode">Код региона.</param>
        /// <param name="AreaCode">Код района.</param>
        /// <param name="CityCode">Код города.</param>
        /// <param name="SettlementCode">Код населенного пункта.</param>
        /// <returns></returns>
        public KladrWithPostIndex GetKladr(string Code, int AddressType, string RegionCode, string AreaCode, string CityCode, string SettlementCode)
        {
            //по коду объекта адреса достаем запись и уже по даннм из
            KladrWithPostIndex k = new KladrWithPostIndex();
            if (string.IsNullOrEmpty(Code))
                k.Kladr = KladrDao.GetKladr(AddressType, RegionCode, AreaCode, CityCode, SettlementCode);
            else
            {
                //по коду выбранного обекта достаем строку и берем из нее параметры для поиска подчиненных списков объектов
                KladrDto row = KladrDao.GetKladrByCode(Code).Single();  
                if (AddressType < 6)
                    k.Kladr = KladrDao.GetKladr(AddressType, row.RegionCode, row.AreaCode, row.CityCode, row.SettlementCode);
                k.PostIndex = row.Index;    //индекс берем из записи выбранного объекта
            }
            return k;
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