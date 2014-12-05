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
        /// Плательщики/получатели, определяем по полю DTID
        /// </summary>
        /// <param name="role"></param>
        /// <param name="DTID">ID типа реквизитов.</param>
        /// <param name="Id"></param>
        /// <param name="Name"></param>
        /// <returns></returns>
        IList<GpdContractDetailDto> GetDetails(UserRole role,
                                                    int DTID,
                                                    int Id,
                                                    string Name);
        /// <summary>
        /// Договора.
        /// </summary>
        /// <param name="role"></param>
        /// <param name="Id"></param>
        /// <param name="CreatorID"></param>
        /// <param name="DepartmentId"></param>
        /// <param name="PersonID"></param>
        /// <param name="CTID"></param>
        /// <param name="StatusID"></param>
        /// <param name="NumContract"></param>
        /// <param name="NameContract"></param>
        /// <param name="DateBegin"></param>
        /// <param name="DateEnd"></param>
        /// <param name="PayeeID"></param>
        /// <param name="PayerID"></param>
        /// <param name="GPDID"></param>
        /// <param name="PurposePayment"></param>
        /// <returns></returns>
        IList<GpdContractDto> GetContracts(UserRole role,
                                            int Id,
                                            int CreatorID,
                                            int DepartmentId,
                                            string DepartmentName,
                                            int PersonID,
                                            int CTID,
                                            int StatusID,
                                            string NumContract,
                                            string NameContract,
                                            DateTime? DateBegin,
                                            DateTime? DateEnd,
                                            DateTime? DateP,
                                            DateTime? DatePOld,
                                            int PayeeID,
                                            int PayerID,
                                            string GPDID,
                                            string PurposePayment,
                                            bool IsDraft,
                                            string CreatorName,
                                            DateTime CreateDate,
                                            string Surname,
                                            string CTName,
                                            string StatusName,
                                            string Autor,
                                            bool IsFind);
        /// <summary>
        /// Уровень подразделения.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        int GetDepLevel(int Id);
    }
}
