﻿using System;
using Reports.Core.Domain;
using Reports.Core.Services;
using Reports.Core.Dto;
using System.Collections.Generic;
using NHibernate.Transform;
using NHibernate;
using NHibernate.Criterion;

namespace Reports.Core.Dao
{
    /// <summary>
    /// Заявки по штатным единицам.
    /// </summary>
    public interface IStaffEstablishedPostRequestDao : IDao<StaffEstablishedPostRequest>
    {
        /// <summary>
        /// Список заявок для штатных единиц.
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
        IList<EstablishedPostRequestDto> GetEstablishedPostRequestList(User curUser, int DepartmentId, int Id, string Surname, DateTime? DateBegin, DateTime? DateEnd, int StatusId, int SortBy, bool? SortDescending);
        /// <summary>
        /// Достаем Id действующей заявки для данной штатной единицы.
        /// </summary>
        /// <param name="Id">Id штатной единицы.</param>
        /// <returns></returns>
        int GetCurrentRequestId(int SEPId);
    }
}
