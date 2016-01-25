using System;
using System.Collections.Generic;
using Reports.Core.Domain;
using Reports.Core.Dto;


namespace Reports.Core.Dao
{
    /// <summary>
    /// Заявки на временно освобождение вакансии.
    /// </summary>
    public interface IStaffTemporaryReleaseVacancyRequestDao : IDao<StaffTemporaryReleaseVacancyRequest>
    {
        /// <summary>
        /// Список заявок на создание временных вакансий.
        /// </summary>
        /// <param name="curUser">Текущий пользователь.</param>
        /// <param name="role">Роль пользователя (из-за байт-кода дополнительный параметр).</param>
        /// <param name="DepartmentId">Id подразделения.</param>
        /// <param name="Id">Номер заявки</param>
        /// <param name="SEPId">Id штатной единицы</param>
        /// <param name="Surname">ФИО инициатора</param>
        /// <param name="DateBegin">Дата начала периода действия заявки</param>
        /// <param name="DateEnd">Дата конца периода действия заявки</param>
        /// <param name="AbsencesTypeId">Id вида отсутствия.</param>
        /// <param name="SortBy">Номер колонки для сортировки</param>
        /// <param name="SortDescending">Признак направления сортировки.</param>
        /// <returns></returns>
        IList<StaffTemporaryReleaseVacancyDto> GetTemporaryReleaseVacancyList(User curUser, UserRole role, int DepartmentId, int Id, int SEPId, string Surname, DateTime? DateBegin, DateTime? DateEnd, int AbsencesTypeId, int SortBy, bool? SortDescending);
    }
}
