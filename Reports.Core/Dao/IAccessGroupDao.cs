using System.Collections.Generic;
using Reports.Core.Domain;
using Reports.Core.Dto;

namespace Reports.Core.Dao
{
    public interface IAccessGroupDao : IDao<AccessGroup>
    {
        //IList<Position> LoadAllSorted();
        IList<AccessGroup> GetAccessGroups();
        /// <summary>
        /// Функция возвращает список сотрудников по группе доступа
        /// </summary>
        /// <param name="depFromFilter">Подразделение</param>
        /// <param name="AccessGroupCode">Код группы доступа</param>
        /// <param name="userName">ФИО сотрудника</param>
        /// <param name="Manager6">ФИО руководителя 6 уровня</param>
        /// <param name="Manager5">ФИО руководителя 5 уровня</param>
        /// <param name="Manager4">ФИО руководителя 4 уровня</param>
        /// <param name="IsManagerShow">Показать руководителей</param>
        /// <param name="sortBy">Id колонки для сортировки</param>
        /// <param name="sortDescending">Тип сортировки</param>
        /// <returns></returns>
        IList<AccessGroupListDto> GetAccessGroupList(
                User user,
                Department depFromFilter,
                string AccessGroupCode,
                string userName,
                string Manager6,
                string Manager5,
                string Manager4,
                bool IsManagerShow,
                int sortBy,
                bool? sortDescending);
    }
}