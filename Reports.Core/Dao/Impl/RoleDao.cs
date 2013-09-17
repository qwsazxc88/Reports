using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Transform;
using Reports.Core.Domain;
using Reports.Core.Dto;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class RoleDao : DefaultDao<Role>, IRoleDao
    {
        public RoleDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
        //public IList<Role> LoadAllSorted()
        //{
        //    ICriteria criteria = Session.CreateCriteria(typeof(Role));
        //    criteria.AddOrder(new Order("Name", true));
        //    return criteria.List<Role>();
        //}
        public IList<IdNameDto> LoadRolesForList(List<UserRole> list)
        {
            //string idsList = list.Aggregate(string.Empty, (current, x) => current + ((int)x).ToString() + ",");
            //idsList = idsList.Substring(0, idsList.Length - 1);
            string sqlQuery =
              string.Format(@"select Id,Name from  [dbo].[Role]
                        where Id in (:roleIdList)
                        order by Name");

            IQuery query = Session.CreateSQLQuery(sqlQuery).
               AddScalar("Id", NHibernateUtil.Int32).
               AddScalar("Name", NHibernateUtil.String).
               SetParameterList("roleIdList", list.ConvertAll(x => (int)x).ToList());
            return query.SetResultTransformer(Transformers.AliasToBean(typeof(IdNameDto))).List<IdNameDto>();
        }
    }
}