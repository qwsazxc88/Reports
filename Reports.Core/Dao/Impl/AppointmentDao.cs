using System.Collections.Generic;
using NHibernate;
using NHibernate.Transform;
using Reports.Core.Domain;
using Reports.Core.Dto;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class AppointmentDao : DefaultDao<Appointment>, IAppointmentDao
    {
        public AppointmentDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
        public virtual IList<DepartmentDto> GetDepartmentsForManager23(int managerId, int level)
        {
            string sqlQuery = string.Format(@" select d.Id,d.Name,d.Path,d.ItemLevel from [dbo].[AppointmentManager23ToDepartment3] mtod
                                        inner join [dbo].[Department] d on d.Id = mtod.[DepartmentId]
                                        where mtod.managerId = {0}",managerId);
            if(level == 2)
                sqlQuery += string.Format(@" union
                            select d.Id,d.Name,d.Path,d.ItemLevel  from [dbo].[AppointmentManager2ToManager3] mtom
                            inner join [dbo].[AppointmentManager23ToDepartment3] mtod on  mtom.[Manager3Id] = mtod.[ManagerId]
                            inner join [dbo].[Department] d on d.Id = mtod.[DepartmentId]
                            where mtom.[Manager2Id] = {0}", managerId);

            IQuery query = Session.CreateSQLQuery(sqlQuery).
                AddScalar("Id", NHibernateUtil.Int32).
                AddScalar("Name", NHibernateUtil.String).
                AddScalar("Path", NHibernateUtil.String).
                AddScalar("ItemLevel", NHibernateUtil.Int32);
            return query.SetResultTransformer(Transformers.AliasToBean(typeof(DepartmentDto))).List<DepartmentDto>();
        }
        public virtual IList<int> GetManager3ForManager2(int managerId)
        {
            string sqlQuery = string.Format(@" select [Manager3Id]  as Id from [dbo].[AppointmentManager2ToManager3]
                                            where [Manager2Id] = {0}", managerId);
            IQuery query = Session.CreateSQLQuery(sqlQuery).
                AddScalar("Id", NHibernateUtil.Int32);
            return query./*SetResultTransformer(Transformers.AliasToBean(typeof(int))).*/List<int>();
        }
    }
}