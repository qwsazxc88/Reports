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
    /// Справочник филиалов.
    /// </summary>
    public class StaffDepartmentBranchDao : DefaultDao<StaffDepartmentBranch>, IStaffDepartmentBranchDao
    {
        public StaffDepartmentBranchDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
        /// </summary>
        /// Филиалы.
        /// </summary>
        /// <returns></returns>
        public IList<StaffDepartmentBranchDto> GetDepartmentBranches()
        {
            IQuery query = Session.CreateSQLQuery(@"SELECT A.Id, A.Code, A.Name, A.DepartmentId, B.Name as DepName
                                                   FROM StaffDepartmentBranch as A
                                                   LEFT JOIN Department as B ON B.Id = A.DepartmentId")
                .AddScalar("Id", NHibernateUtil.Int32)
                .AddScalar("Code", NHibernateUtil.String)
                .AddScalar("Name", NHibernateUtil.String)
                .AddScalar("DepartmentId", NHibernateUtil.Int32)
                .AddScalar("DepName", NHibernateUtil.String);

            return query.SetResultTransformer(Transformers.AliasToBean<StaffDepartmentBranchDto>()).List<StaffDepartmentBranchDto>();
        }

        /// <summary>
        /// Проверка на доступность удаления данной строки.
        /// </summary>
        /// <param name="Id">Id удаляемой строки</param>
        /// <returns></returns>
        public bool IsEnableDelete(int Id)
        {
            IQuery query = Session.CreateSQLQuery("SELECT cast(case when count(*) > 0 then 0 else 1 end as bit) IsExists FROM dbo.StaffDepartmentManagement WHERE BranchId = " + Id.ToString())
            .AddScalar("IsExists", NHibernateUtil.Boolean);
            return query.UniqueResult<bool>();
        }
        /// <summary>
        /// Достаем запись из справочника кодировок филиалов по подразделению из СКД
        /// </summary>
        /// <param name="Dep">Подразделение ниже 2 уровня.</param>
        /// <returns></returns>
        public StaffDepartmentBranch GetDepartmentBranchByDeparment(Department Dep)
        {
            Department dep2level = Session.Query<Department>().Where(x => Dep.Path.StartsWith(x.Path) && x.ItemLevel == 2).FirstOrDefault();
            return Session.Query<StaffDepartmentBranch>().
                Where(x => x.Department == dep2level)
                .FirstOrDefault();
        }

        /// <summary>
        /// Формируем новый код для филиала.
        /// </summary>
        /// <returns></returns>
        public string GetNewBranchCode()
        {
            string Code = Session.Query<StaffDepartmentBranch>().Max(x => x.Code);
            //предпологаем, что код содержит только цифры
            Code = (Convert.ToInt32(Code) + 1).ToString();

            Code = (Code.Length == 1 ? "0" + Code : Code);

            return Code;
        }
    }
}
