﻿using System;
using System.Collections.Generic;
using Reports.Core.Domain;
using Reports.Core.Dto;

namespace Reports.Core.Dao
{
    public interface IGpdActDao : IDao<GpdAct>
    {
        /// <summary>
        /// Запрос для создания нового акта ГПД.
        /// </summary>
        /// <param name="role">Текущая роль пользователя.</param>
        /// <param name="GCID">ID договора.</param>
        /// <returns></returns>
        IList<GpdActDto> GetNewAct(UserRole role, int GCID);

        /// <summary>
        /// Запрос для просмотра/редактирования акта ГПД.
        /// </summary>
        /// <param name="role">Текущая роль пользователя.</param>
        /// <param name="ID">ID акта.</param>
        /// <param name="IsFind">Признак для составления условия запроса.</param>
        /// <param name="DateBegin">Начало периода для даты акта.</param>
        /// <param name="DateEnd">Конец периода для даты акта.</param>
        /// <param name="DepartmentId">ID подразделения в договоре.</param>
        /// <param name="Surname">ФИО физического лица.</param>
        /// <param name="StatusID">ID статуса акта.</param>
        /// /// <param name="ActNumber">№ акта.</param>
        /// <param name="SortBy">Переменная определяет поле сортировки.</param>
        /// <param name="SortDescending">Признак направления сортировки.</param>
        /// <returns></returns>
        IList<GpdActDto> GetAct(UserRole role,
                                        int? ID,
                                        bool IsFind,
                                        DateTime? DateBegin,
                                        DateTime? DateEnd,
                                        int DepartmentId,
                                        string Surname,
                                        int StatusID,
                                        string ActNumber,
                                        int SortBy,
                                        bool? SortDescending);
        /// <summary>
        /// Список статусов актов.
        /// </summary>
        /// <param name="role"></param>
        /// <param name="Id"></param>
        /// <param name="Name"></param>
        /// <returns></returns>
        IList<GpdActStatusesDto> GetStatuses();
        /// <summary>
        /// Комментарии к акту ГПД.
        /// </summary>
        /// <param name="Id">Id акта ГПД.</param>
        /// <returns></returns>
        IList<GpdActCommentDto> GetComments(int Id);
        /// <summary>
        /// Права.
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        IList<GpdPermissionDto> GetPermission(UserRole role);
    }
}
