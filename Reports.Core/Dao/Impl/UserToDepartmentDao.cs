using System.Collections.Generic;
using NHibernate;
using NHibernate.Transform;
using Reports.Core.Domain;
using Reports.Core.Dto;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    /*public class UserToDepartmentDao : DefaultDao<UserToDepartment>, IUserToDepartmentDao
    {
        public UserToDepartmentDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region IUserToDepartmentDao Members

        public IList<IdNameDto> GetByUserId(int userId)
        {
            string sqlQuery =
                @"select d.Id as Id,d.Name as Name from UserToDepartment ud
            inner join Department d on d.Id = ud.DepartmentId 
            where ud.UserId = :userId ";
            IQuery query = Session.CreateSQLQuery(sqlQuery).
                AddScalar("Id", NHibernateUtil.Int32).
                AddScalar("Name", NHibernateUtil.String);
            query.SetInt32("userId", userId);
            return query.SetResultTransformer(Transformers.AliasToBean(typeof (IdNameDto))).List<IdNameDto>();
        }

        #endregion
    }*/
}