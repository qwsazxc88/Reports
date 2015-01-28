using System;
using System.Collections.Generic;
using Reports.Core.Domain;
using Reports.Core.Dto;

namespace Reports.Core.Dao
{
    public interface IGpdRefDetailDao : IDao<GpdRefDetail>
    {
        /// <summary>
        /// Типы реквизитов.
        /// </summary>
        /// <param name="role"></param>
        /// <param name="Id"></param>
        /// <param name="Name"></param>
        /// <returns></returns>
        IList<GpdRefDetailDto> GetDetailTypes(UserRole role,
                                                int Id,
                                                string Name);
        /// <summary>
        /// Справочник реквизитов.
        /// </summary>
        /// <param name="role"></param>
        /// <param name="Id">ID реквизита</param>
        /// <param name="Name">Название реквизита</param>
        /// <param name="ContractorName">Название контрагента.</param>
        /// <param name="SortBy">Номер поля для сортировки</param>
        /// <param name="SortDescending">Направление сортировки</param>
        /// <returns></returns>
        IList<GpdDetailDto> GetRefDetail(UserRole role, int Id, string Name, string ContractorName, int SortBy, bool? SortDescending);
        /// <summary>
        /// Права.
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        IList<GpdPermissionDto> GetPermission(UserRole role);
        /// <summary>
        /// Запрос для наборов реквизитов.
        /// </summary>
        /// <param name="ID">ID набора.</param>
        /// <param name="Name">Название набора реквизитов</param>
        /// <param name="Surname">ФИО физического лица</param>
        /// <param name="PayerName">Плательщик</param>
        /// <param name="PayeeName">Получатель</param>
        /// <param name="flgView">Признак просмотра списка</param>
        /// <param name="SortBy">Номер поля для сортировки</param>
        /// <param name="SortDescending">Направление сортировки</param>
        /// <returns></returns>
        IList<GpdDetailSetsListDto> GetDetailSetList(int ID,
            string Name,
            string Surname,
            string PayerName,
            string PayeeName,
            bool flgView,
            int SortBy,
            bool? SortDescending);
        /// <summary>
        /// Запрос для физических лиц.
        /// </summary>
        /// <returns></returns>
        IList<GpdContractSurnameDto> GetPersons(int Id);
        /// <summary>
        /// Автозаполнение для физиков
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="PersonID"></param>
        /// <returns></returns>
        IList<GpdContractSurnameDto> GetAutocompletePersons(string Name, int PersonID);
    }
}
