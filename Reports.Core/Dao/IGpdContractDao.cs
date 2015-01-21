using System;
using System.Collections.Generic;
using Reports.Core.Domain;
using Reports.Core.Dto;

namespace Reports.Core.Dao
{
    public interface IGpdContractDao : IDao<GpdContract>
    {
        /// <summary>
        /// Статусы договоров.
        /// </summary>
        /// <param name="role"></param>
        /// <param name="Id"></param>
        /// <param name="Name"></param>
        /// <returns></returns>
        IList<GpdContractStatusesDto> GetStatuses(UserRole role,
                                                    int Id,
                                                    string Name);
        /// <summary>
        /// Список сроков оплаты.
        /// </summary>
        /// <returns></returns>
        IList<GpdContractStatusesDto> GetPaymentPeriods();
        /// <summary>
        /// Виды начисления.
        /// </summary>
        /// <param name="role"></param>
        /// <param name="Id"></param>
        /// <param name="Name"></param>
        /// <returns></returns>
        IList<GpdContractChargingTypesDto> GetChargingTypes(UserRole role,
                                                    int Id,
                                                    string Name);
        /// <summary>
        /// Физические лица.
        /// </summary>
        /// <param name="role"></param>
        /// <param name="Id"></param>
        /// <param name="Name"></param>
        /// <returns></returns>
        IList<GpdContractSurnameDto> GetPersons(UserRole role,
                                                    int Id,
                                                    string Name);
        /// <summary>
        /// Список физических лиц с набором реквизитов.
        /// </summary>
        /// <param name="Name">ФИО физ лица</param>
        /// <param name="PersonID">ID физ лица</param>
        /// <returns></returns>
        IList<GpdContractSurnameDto> GetAutocompletePersons(string Name, int PersonID);

        /// <summary>
        /// Договора.
        /// </summary>
        /// <param name="role"></param>
        /// <param name="Id"></param>
        /// <param name="DepartmentId"></param>
        /// <param name="CTID"></param>
        /// <param name="DateBegin"></param>
        /// <param name="DateEnd"></param>
        /// <param name="Surname"></param>
        /// <param name="IsFind"></param>
        /// <param name="SortBy"></param>
        /// <param name="SortDescending"></param>
        /// <returns></returns>
        IList<GpdContractDto> GetContracts(UserRole role,
                                            int? Id,
                                            int DepartmentId,
                                            int CTID,
                                            DateTime? DateBegin,
                                            DateTime? DateEnd,
                                            string Surname,
                                            string NumContract,
                                            bool IsFind,
                                            int SortBy,
                                            bool? SortDescending);
        /// <summary>
        /// Уровень подразделения.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        int GetDepLevel(int Id);
        /// <summary>
        /// Права.
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        IList<GpdPermissionDto> GetPermission(UserRole role);
    }
}
