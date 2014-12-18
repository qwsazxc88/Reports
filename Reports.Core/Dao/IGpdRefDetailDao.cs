using System;
using System.Collections.Generic;
using Reports.Core.Domain;
using Reports.Core.Dto;

namespace Reports.Core.Dao
{
    public interface IGpdRefDetailDao : IDao<GpdRefDetail>
    {
        /// <summary>
        /// Типы реквизитов.
        /// </summary>
        /// <param name="role"></param>
        /// <param name="Id"></param>
        /// <param name="Name"></param>
        /// <returns></returns>
        IList<GpdRefDetailDto> GetDetailTypes(UserRole role,
                                                int Id,
                                                string Name);
        /// <summary>
        /// Справочник реквизитов.
        /// </summary>
        /// <param name="role"></param>
        /// <param name="Id"></param>
        /// <param name="Name"></param>
        /// <param name="DTID"></param>
        /// <param name="INN"></param>
        /// <param name="KPP"></param>
        /// <param name="Account"></param>
        /// <param name="BankName"></param>
        /// <param name="BankBIK"></param>
        /// <param name="CorrAccount"></param>
        /// <param name="CreatorID"></param>
        /// <returns></returns>
        IList<GpdRefDetailFullDto> GetRefDetail(UserRole role,
                                                int Id,
                                                string Name,
                                                int DTID,
                                                string INN,
                                                string KPP,
                                                string Account,
                                                string BankName,
                                                string BankBIK,
                                                string CorrAccount,
                                                int CreatorID,
                                                string Code);
        /// <summary>
        /// Права.
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        IList<GpdPermissionDto> GetPermission(UserRole role);
    }
}
