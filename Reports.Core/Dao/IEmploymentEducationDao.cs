using System;
using System.Collections.Generic;
using Reports.Core.Domain;
using Reports.Core.Dto;

namespace Reports.Core.Dao
{
    public interface IEmploymentEducationDao : IDao<Education>
    {
        /// <summary>
        /// Определяем наличие сведений об образовании по количеству строк.
        /// </summary>
        /// <param name="UserId">Id кандидата.</param>
        /// <param name="Type">Тип образования: 1 - Сведения об образовании, 2 - Послевузовское образование, 3 - Аттестация, 4 - Повышение квалификации.</param>
        /// <returns></returns>
        int CheckExistsEducationRecord(int UserId, int Type);
    }
}