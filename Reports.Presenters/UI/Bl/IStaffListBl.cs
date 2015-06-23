using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reports.Presenters.UI.ViewModel.StaffList;
using Reports.Core.Domain;
using Reports.Core.Dto;

namespace Reports.Presenters.UI.Bl
{
    public interface IStaffListBl : IBaseBl
    {
        #region Штатные расписание.
        /// <summary>
        /// Загружаем структуру по заданному коду подразделения и штатные единицы
        /// </summary>
        /// <param name="DepId">Код родительского подразделения</param>
        /// <returns></returns>
        StaffListModel GetDepartmentStructureWithStaffPost(string DepId);

        #region Заявки для подразделений
        /// <summary>
        /// Заполняем модель заявки на создание подразделения.
        /// </summary>
        /// <param name="model">Модель заявки.</param>
        /// <returns></returns>
        StaffDepartmentRequestModel GetNewDepartmentRequest(StaffDepartmentRequestModel model);
        #endregion
        
        #endregion

        #region Для теста
        TreeViewModel GetDepartmentList();
        /// <summary>
        /// Загружаем структуру по заданному коду подразделения
        /// </summary>
        /// <param name="DepId">Код родительского подразделения</param>
        /// <returns></returns>
        TreeGridAjaxModel GetDepartmentStructure(string DepId);
        IList<Department> GetDepartmentListByParent(string DepId);
        /// <summary>
        /// Загружаем модель для составления Российских адресов.
        /// </summary>
        /// <param name="Id">Id адреса</param>
        /// <returns></returns>
        AddressModel GetAddress(int Id);
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
        KladrWithPostIndex GetKladr(string Code, int AddressType, string RegionCode, string AreaCode, string CityCode, string SettlementCode);
        /// <summary>
        /// Загружаем структуру по заданному коду подразделения с привязками к точкам Финграда
        /// </summary>
        /// <param name="DepId">Код родительского подразделения</param>
        /// <returns></returns>
        DepStructureFingradPointsModel GetDepartmentStructureWithFingradPoins(string DepId);
        #endregion
    }
}
