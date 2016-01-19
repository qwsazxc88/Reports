using System.Collections.Generic;
using Reports.Core.Dto;
using Reports.Core.Domain;
using Reports.Core.Services;
using NHibernate.Transform;
using NHibernate;
using NHibernate.Criterion;
using System;
using System.Linq;
using NHibernate.Linq;

namespace Reports.Core.Dao.Impl
{
    /// <summary>
    /// справочник РП-привязок
    /// </summary>
    public class StaffDepartmentRPLinkDao : DefaultDao<StaffDepartmentRPLink>, IStaffDepartmentRPLinkDao
    {
        public StaffDepartmentRPLinkDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        /// </summary>
        /// РП-привязки.
        /// </summary>
        /// <param name="BGFilterId">Id бизнес-группы.</param>
        /// <param name="AdminFilterId">Id управления.</param>
        /// <param name="ManagementFilterId">Id дирекции</param>
        /// <param name="BranchFilterId">Id филиала</param>
        /// <returns></returns>
        public IList<StaffDepartmentRPLinkDto> GetDepartmentRPLinks(int BGFilterId, int AdminFilterId, int ManagementFilterId, int BranchFilterId)
        {
            IQuery query = Session.CreateSQLQuery(@"SELECT A.Id as rId, A.Code as rCode, A.Name as rName, A.BGId, B.Name as BGName, A.DepartmentId as rDepartmentId, C.Name as DepName, D.Name as AdminName, E.Name as ManagementName, F.Name as BranchName
                                                    FROM StaffDepartmentRPLink as A
                                                    LEFT JOIN StaffDepartmentBusinessGroup as B ON B.Id = A.BGId
                                                    LEFT JOIN Department as C ON C.Id = A.DepartmentId
                                                    LEFT JOIN StaffDepartmentAdministration as D ON D.Id = B.AdminId
												    LEFT JOIN StaffDepartmentManagement as E ON E.Id = D.ManagementId
                                                    LEFT JOIN StaffDepartmentBranch as F ON F.Id = E.BranchId " + SqlWhere(BGFilterId, AdminFilterId, ManagementFilterId, BranchFilterId))
                .AddScalar("rId", NHibernateUtil.Int32)
                .AddScalar("rCode", NHibernateUtil.String)
                .AddScalar("rName", NHibernateUtil.String)
                .AddScalar("BGId", NHibernateUtil.Int32)
                .AddScalar("BGName", NHibernateUtil.String)
                .AddScalar("rDepartmentId", NHibernateUtil.Int32)
                .AddScalar("DepName", NHibernateUtil.String)
                .AddScalar("AdminName", NHibernateUtil.String)
                .AddScalar("ManagementName", NHibernateUtil.String)
                .AddScalar("BranchName", NHibernateUtil.String);

            return query.SetResultTransformer(Transformers.AliasToBean<StaffDepartmentRPLinkDto>()).List<StaffDepartmentRPLinkDto>();
        }

        /// <summary>
        /// Собираем условие для запроса.
        /// </summary>
        /// <param name="BGFilterId">Id бизнес-группы.</param>
        /// <param name="AdminFilterId">Id управления.</param>
        /// <param name="ManagementFilterId">Id дирекции</param>
        /// <param name="BranchFilterId">Id филиала</param>
        /// <returns></returns>
        /// <returns></returns>
        protected string SqlWhere(int BGFilterId, int AdminFilterId, int ManagementFilterId, int BranchFilterId)
        {
            string Where = string.Empty;

            if (BGFilterId != 0)
                Where += "B.Id = " + BGFilterId.ToString();

            if (AdminFilterId != 0)
                Where += (Where.Length != 0 ? " and " : "") + "D.Id = " + AdminFilterId.ToString();

            if (ManagementFilterId != 0)
                Where += (Where.Length != 0 ? " and " : "") + "E.Id = " + ManagementFilterId.ToString();

            if (BranchFilterId != 0)
                Where += (Where.Length != 0 ? " and " : "") + "F.Id = " + BranchFilterId.ToString();

            return (Where.Length != 0 ? "WHERE " + Where : "");
        }

        /// <summary>
        /// Проверка на доступность удаления данной строки.
        /// </summary>
        /// <param name="Id">Id удаляемой строки</param>
        /// <returns></returns>
        public bool IsEnableDelete(int Id)
        {
            IQuery query = Session.CreateSQLQuery(@"SELECT cast(case when count(*) > 0 then 0 else 1 end as bit) IsExists FROM dbo.StaffDepartmentRPLink WHERE DepartmentId is not null and Id = " + Id.ToString())
            .AddScalar("IsExists", NHibernateUtil.Boolean);
            return query.UniqueResult<bool>();
        }

        /// <summary>
        /// Достаем запись из справочника кодировок РП-привязок по подразделению из СКД
        /// </summary>
        /// <param name="Dep">Подразделение 6 уровня</param>
        /// <returns></returns>
        public StaffDepartmentRPLink GetDepartmentRPLinkByDeparment(Department Dep)
        {
            return Session.Query<StaffDepartmentRPLink>().
                Where(x => x.Department == Dep)
                .FirstOrDefault();
        }

        /// <summary>
        /// Формируем новый код для РП-привязки.
        /// </summary>
        /// <param name="BusinessGroup">Бизнес-группа</param>
        /// <returns></returns>
        public string GetNewRPLinkCode(StaffDepartmentBusinessGroup BusinessGroup)
        {
            IList<StaffDepartmentRPLink> rp = Session.Query<StaffDepartmentRPLink>().Where(x => x.DepartmentBG == BusinessGroup && x.Code.StartsWith(BusinessGroup.Code.Substring(0, 6))).ToList();
            string Code = rp.Count() == 0 || rp.Where(x => x.Code.StartsWith(BusinessGroup.Code.Substring(0, 6))).Count() == 0 ? "0" : 
                rp.Where(x => x.Code.StartsWith(BusinessGroup.Code.Substring(0, 6)))
                .OrderByDescending(x => x.Code.Substring(6, 2))
                .FirstOrDefault().Code.Substring(6, 2);

            //предпологаем, что код содержит только цифры с разделителями, увеличиваем на 1
            Code = (Convert.ToInt32(Code) + 1).ToString();
            Code = BusinessGroup.Code.Substring(0, 6) + (Code.Length == 1 ? "0" + Code : Code) + "-000";

            return Code;
        }

        /// <summary>
        /// Достаем структуру финграда для нашего подразделения
        /// </summary>
        /// <param name="DepartmentId">Id подразделения 6 уровня</param>
        /// <returns></returns>
        public StaffDepartmentFingradStructureDto GetFingradStructureForDeparment(int DepartmentId)
        {
            IQuery query = Session.CreateSQLQuery(@"SELECT Id, RPLinkCode, RPLinkName, RPLinkNameSKD, BGCode, BGName, BGNameSKD, AdminCode, AdminName, AdminNameSKD, ManagementCode, ManagementName, ManagementNameSKD
                                                    FROM dbo.fnGetFingradStructureForDeparment(:DepartmentId)")
                 .AddScalar("Id", NHibernateUtil.Int32)
                 .AddScalar("RPLinkCode", NHibernateUtil.String)
                 .AddScalar("RPLinkName", NHibernateUtil.String)
                 .AddScalar("RPLinkNameSKD", NHibernateUtil.String)
                 .AddScalar("BGCode", NHibernateUtil.String)
                 .AddScalar("BGName", NHibernateUtil.String)
                 .AddScalar("BGNameSKD", NHibernateUtil.String)
                 .AddScalar("AdminCode", NHibernateUtil.String)
                 .AddScalar("AdminName", NHibernateUtil.String)
                 .AddScalar("AdminNameSKD", NHibernateUtil.String)
                 .AddScalar("ManagementCode", NHibernateUtil.String)
                 .AddScalar("ManagementName", NHibernateUtil.String)
                 .AddScalar("ManagementNameSKD", NHibernateUtil.String)
                 .SetInt32("DepartmentId", DepartmentId);

            return query.SetResultTransformer(Transformers.AliasToBean<StaffDepartmentFingradStructureDto>()).UniqueResult<StaffDepartmentFingradStructureDto>();
        }
    }
}
