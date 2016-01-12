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
    /// Справочник бизнес-групп
    /// </summary>
    public class StaffDepartmentBusinessGroupDao : DefaultDao<StaffDepartmentBusinessGroup>, IStaffDepartmentBusinessGroupDao
    {
        public StaffDepartmentBusinessGroupDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        /// </summary>
        /// Бизнс-группы.
        /// </summary>
        /// <param name="AdminFilterId">Id управления.</param>
        /// <param name="ManagementFilterId">Id дирекции</param>
        /// <param name="BranchFilterId">Id филиала</param>
        /// <returns></returns>
        public IList<StaffDepartmentBusinessGroupDto> GetDepartmentBusinessGroups(int AdminFilterId, int ManagementFilterId, int BranchFilterId)
        {
            IQuery query = Session.CreateSQLQuery(@"SELECT A.Id as bId, A.Code as bCode, A.Name as bName, A.AdminId, B.Name as AdminName, A.DepartmentId as bDepartmentId, C.Name as DepName, D.Name as ManagementName, E.Name as BranchName, F.Name as DepManager
                                                    FROM StaffDepartmentBusinessGroup as A
                                                    LEFT JOIN StaffDepartmentAdministration as B ON B.Id = A.AdminId
                                                    LEFT JOIN Department as C ON C.Id = A.DepartmentId
                                                    LEFT JOIN StaffDepartmentManagement as D ON D.Id = B.ManagementId
                                                    LEFT JOIN StaffDepartmentBranch as E ON E.Id = D.BranchId " + SqlWhere(AdminFilterId, ManagementFilterId, BranchFilterId) + @"
                                                    LEFT JOIN vwStaffListDepartmentManagers as F ON F.DepartmentId = A.DepartmentId and F.IsMainManager = 1 and isnull(F.IsPregnant, 0) = 0")
                .AddScalar("bId", NHibernateUtil.Int32)
                .AddScalar("bCode", NHibernateUtil.String)
                .AddScalar("bName", NHibernateUtil.String)
                .AddScalar("AdminId", NHibernateUtil.Int32)
                .AddScalar("AdminName", NHibernateUtil.String)
                .AddScalar("bDepartmentId", NHibernateUtil.Int32)
                .AddScalar("DepName", NHibernateUtil.String)
                .AddScalar("ManagementName", NHibernateUtil.String)
                .AddScalar("BranchName", NHibernateUtil.String)
                .AddScalar("DepManager", NHibernateUtil.String);

            return query.SetResultTransformer(Transformers.AliasToBean<StaffDepartmentBusinessGroupDto>()).List<StaffDepartmentBusinessGroupDto>();
        }
        
        /// <summary>
        /// Собираем условие для запроса.
        /// </summary>
        /// <param name="AdminFilterId">Id управления.</param>
        /// <param name="ManagementFilterId">Id дирекции</param>
        /// <param name="BranchFilterId">Id филиала</param>
        /// <returns></returns>
        protected string SqlWhere(int AdminFilterId, int ManagementFilterId, int BranchFilterId)
        {
            string Where = string.Empty;

            if (AdminFilterId != 0)
                Where += "B.Id = " + AdminFilterId.ToString();

            if (ManagementFilterId != 0)
                Where += (Where.Length != 0 ? " and " : "") + "D.Id = " + ManagementFilterId.ToString();

            if (BranchFilterId != 0)
                Where += (Where.Length != 0 ? " and " : "") + "E.Id = " + BranchFilterId.ToString();

            return (Where.Length != 0 ? "WHERE " + Where : "");
        }
        /// <summary>
        /// Проверка на доступность удаления данной строки.
        /// </summary>
        /// <param name="Id">Id удаляемой строки</param>
        /// <returns></returns>
        public bool IsEnableDelete(int Id)
        {
            IQuery query = Session.CreateSQLQuery("SELECT cast(case when count(*) > 0 then 0 else 1 end as bit) IsExists FROM dbo.StaffDepartmentRPLink WHERE BGId = " + Id.ToString())
            .AddScalar("IsExists", NHibernateUtil.Boolean);
            return query.UniqueResult<bool>();
        }

        /// <summary>
        /// Достаем запись из справочника кодировок бизнес-групп по подразделению из СКД
        /// </summary>
        /// <param name="Dep">Подразделение 5 уровня</param>
        /// <returns></returns>
        public StaffDepartmentBusinessGroup GetDepartmentBusinessGroupByDeparment(Department Dep)
        {
            return Session.Query<StaffDepartmentBusinessGroup>().
                Where(x => x.Department == Dep)
                .FirstOrDefault();
        }

        /// <summary>
        /// Формируем новый код для бизнес-группы.
        /// </summary>
        /// <param name="Administration">Управление</param>
        /// <returns></returns>
        public string GetNewBusinessGroupCode(StaffDepartmentAdministration Administration)
        {
            IList<StaffDepartmentBusinessGroup> bg = Session.Query<StaffDepartmentBusinessGroup>().Where(x => x.DepartmentAdministration == Administration).ToList();

            string Code = bg.Count() == 0 || bg.Where(x => x.Code.StartsWith(Administration.Code)).Count() == 0 ? "0" :
                bg.Where(x => x.Code.StartsWith(Administration.Code))
                .OrderByDescending(x => x.Code.Substring(8))
                .FirstOrDefault().Code.Substring(8);

            //предпологаем, что код содержит только цифры с разделителями, увеличиваем на 1
            Code = (Convert.ToInt32(Code) + 1).ToString();
            Code = Administration.Code + "-" + (Code.Length == 2 ? "0" + Code : ((Code.Length == 1 ? "00" + Code : Code)));

            return Code;
        }
    }
}
