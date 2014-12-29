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
        /// <param name="Id"></param>
        /// <param name="Name"></param>
        /// <param name="DTID"></param>
        /// <param name="SortBy">Номер поля по которому будет проводится сортировка.</param>
        /// <param name="SortDescending">Направление сортировки.</param>
        /// <returns></returns>
        IList<GpdRefDetailFullDto> GetRefDetail(UserRole role,
            int Id,
            string Name,
            int DTID,
            int SortBy,
            bool? SortDescending
            );
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
        /// <param name="SortBy">Номер поля для сортировки</param>
        /// <param name="SortDescending">Направление сортировки</param>
        /// <returns></returns>
        IList<GpdDetailSetsListDto> GetDetailSetList(int ID,
            string Name,
            string Surname,
            string PayerName,
            string PayeeName,
            int SortBy,
            bool? SortDescending);
    }
}
