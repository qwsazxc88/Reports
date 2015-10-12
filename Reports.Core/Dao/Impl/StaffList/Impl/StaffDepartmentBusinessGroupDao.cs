using System.Collections.Generic;
using Reports.Core.Dto;
using Reports.Core.Domain;
using Reports.Core.Services;
using NHibernate.Transform;
using NHibernate;
using NHibernate.Criterion;

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
            IQuery query = Session.CreateSQLQuery(@"SELECT A.Id as bId, A.Code as bCode, A.Name as bName, A.AdminId, B.Name as AdminName, A.DepartmentId as bDepartmentId, C.Name as DepName, D.Name as ManagementName, E.Name as BranchName
                                                    FROM StaffDepartmentBusinessGroup as A
                                                    LEFT JOIN StaffDepartmentAdministration as B ON B.Id = A.AdminId
                                                    LEFT JOIN Department as C ON C.Id = A.DepartmentId
                                                    LEFT JOIN StaffDepartmentManagement as D ON D.Id = B.ManagementId
                                                    LEFT JOIN StaffDepartmentBranch as E ON E.Id = D.BranchId " + SqlWhere(AdminFilterId, ManagementFilterId, BranchFilterId))
                .AddScalar("bId", NHibernateUtil.Int32)
                .AddScalar("bCode", NHibernateUtil.String)
                .AddScalar("bName", NHibernateUtil.String)
                .AddScalar("AdminId", NHibernateUtil.Int32)
                .AddScalar("AdminName", NHibernateUtil.String)
                .AddScalar("bDepartmentId", NHibernateUtil.Int32)
                .AddScalar("DepName", NHibernateUtil.String)
                .AddScalar("ManagementName", NHibernateUtil.String)
                .AddScalar("BranchName", NHibernateUtil.String);

            return query.SetResultTransformer(Transformers.AliasToBean<StaffDepartmentBusinessGroupDto>()).List<StaffDepartmentBusinessGroupDto>();
        }
        
        /// <summary>
        /// Собираем условие для запроса.
        /// </summary>
        /// <param name="AdminFilterId">Id управления.</param>
        /// <param name="ManagementFilterId">Id дирекции</param>
        /// <param name="BranchFilterId">Id филиала</param>
        /// <returns></returns>
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
    }
}
