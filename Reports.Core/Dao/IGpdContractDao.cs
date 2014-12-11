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
        /// <param name="DepartmentId"></param>
        /// <param name="CTID"></param>
        /// <param name="DateBegin"></param>
        /// <param name="DateEnd"></param>
        /// <param name="IsDraft"></param>
        /// <param name="Surname"></param>
        /// <param name="IsFind"></param>
        /// <param name="SortBy"></param>
        /// <param name="SortDescending"></param>
        /// <returns></returns>
        IList<GpdContractDto> GetContracts(UserRole role,
                                            int Id,
                                            //int CreatorID,
                                            int DepartmentId,
                                            //string DepartmentName,
                                            //int PersonID,
                                            int CTID,
                                            //int StatusID,
                                            //string NumContract,
                                            //string NameContract,
                                            DateTime? DateBegin,
                                            DateTime? DateEnd,
                                            //DateTime? DateP,
                                            //DateTime? DatePOld,
                                            //int PayeeID,
                                            //int PayerID,
                                            //string GPDID,
                                            //string PurposePayment,
                                            bool IsDraft,
                                            //string CreatorName,
                                            //DateTime CreateDate,
                                            string Surname,
                                            //string CTName,
                                            //string StatusName,
                                            //string Autor,
                                            //string DepLevel3Name,
                                            //string DepLevel7Name,
                                            bool IsFind,
                                            int SortBy,
                                            bool? SortDescending);
        /// <summary>
        /// Уровень подразделения.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        int GetDepLevel(int Id);
    }
}
