using System;
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
        /// <param name="role">Роль текущего пользователя.</param>
        /// <param name="DepartmentId">Id подразделения.</param>
        /// <param name="Id">Номер заявки</param>
        /// <param name="Surname">ФИО инициатора</param>
        /// <param name="DateBegin">Дата начала периода создания заявки</param>
        /// <param name="DateEnd">Дата конца периода создания заявки</param>
        /// <param name="StatusId">Id статуса заявки.</param>
        /// <param name="SortBy">Номер колонки для сортировки</param>
        /// <param name="SortDescending">Признак направления сортировки.</param>
        /// <param name="RequestTypeId">Id вида заявки</param>
        /// <param name="BFGId">Id принадлежности подразделения</param>
        /// <returns></returns>
        IList<DepartmentRequestListDto> GetDepartmentRequestList(User curUser, UserRole role, int DepartmentId, int Id, string Surname, DateTime? DateBegin, DateTime? DateEnd, int StatusId, int SortBy, bool? SortDescending, int RequestTypeId, int BFGId);
        /// <summary>
        /// Достаем Id действующей заявки для данного подразделения.
        /// </summary>
        /// <param name="Id">Id подразделения</param>
        /// <returns></returns>
        int GetCurrentRequestId(int DepartmentId);
        /// <summary>
        /// Проверка на возможность создать код Финграда для создаваемого подразделения.
        /// </summary>
        /// <param name="Id">Id родительского подразделения</param>
        /// <returns></returns>
        bool IsEnableCreateCode(int Id);
        /// <summary>
        /// Формируем новый код для подразделения 7 уровня.
        /// </summary>
        /// <param name="br">Филиал</param>
        /// <param name="mn">Дирекция</param>
        /// <param name="rp">РП-привязка</param>
        /// <returns></returns>
        string GetNewFinDepCode(StaffDepartmentBranch br, StaffDepartmentManagement mn, StaffDepartmentRPLink rp);
        /// <summary>
        /// Проверка на возможность закрыть подразделение.
        /// </summary>
        /// <param name="Id">Id подразделения</param>
        /// <returns></returns>
        bool IsEnableCloseDepartment(int Id);
    }
}
