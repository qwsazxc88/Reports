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
        /// <returns></returns>
        AddressModel GetAddress();
        /// <summary>
        /// Загружаем список районов.
        /// </summary>
        /// <param name="Code">Код записи.</param>
        /// <param name="AddressType">Тип записи.</param>
        /// <returns></returns>
        IList<KladrDto> GetKladr(string Code, int AddressType, string RegionCode, string AreaCode, string CityCode, string SettlementCode);
        KladrWithPostIndex GetKladrNew(string Code, int AddressType, string RegionCode, string AreaCode, string CityCode, string SettlementCode);
    }
}
