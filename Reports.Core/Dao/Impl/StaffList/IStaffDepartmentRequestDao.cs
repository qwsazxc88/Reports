﻿using System;
using System.Collections.Generic;
using Reports.Core.Domain;
using Reports.Core.Dto;

namespace Reports.Core.Dao
{
    /// <summary>
    /// Заявки на изменение структуры подразделений.
    /// </summary>
    public interface IStaffDepartmentRequestDao : IDao<StaffDepartmentRequest>
    {
        /// <summary>
        /// Список заявок для подразделений.
        /// </summary>
        /// <param name="curUser">Текущий пользователь.</param>
        /// <param name="DepartmentId">Id подразделения.</param>
        /// <param name="Id">Номер заявки</param>
        /// <param name="Surname">ФИО инициатора</param>
        /// <param name="DateBegin">Дата начала периода создания заявки</param>
        /// <param name="DateEnd">Дата конца периода создания заявки</param>
        /// <param name="StatusId">Id статуса заявки.</param>
        /// <param name="SortBy">Номер колонки для сортировки</param>
        /// <param name="SortDescending">Признак направления сортировки.</param>
        /// <returns></returns>
        IList<DepartmentRequestListDto> GetDepartmentRequestList(User curUser, int DepartmentId, int Id, string Surname, DateTime? DateBegin, DateTime? DateEnd, int StatusId, int SortBy, bool? SortDescending);
        /// <summary>
        /// Достаем Id действующей заявки для данного подразделения.
        /// </summary>
        /// <param name="Id">Id подразделения</param>
        /// <returns></returns>
        int GetCurrentRequestId(int DepartmentId);
    }
}