using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;
using Reports.Core.Domain;
using Reports.Core.Services;
using Reports.Core.Dto;

namespace Reports.Core.Dao.Impl
{
    public class AccessGroupDao : DefaultDao<AccessGroup>, IAccessGroupDao
    {
        public AccessGroupDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        /// <summary>
        /// Список групп доступа
        /// </summary>
        /// <returns></returns>
        public IList<AccessGroup> GetAccessGroups()
        {

            IQuery query = CreateQuery(@"SELECT null as Code, null as Name
                                         UNION ALL
                                         SELECT Code, Name FROM dbo.AccessGroup
                                         ORDER BY Name");

            return query.SetResultTransformer(Transformers.AliasToBean<AccessGroup>()).List<AccessGroup>();
        }
        public override IQuery CreateQuery(string sqlQuery)
        {
            IQuery query = Session.CreateSQLQuery(sqlQuery)
                .AddScalar("Code", NHibernateUtil.String)
                .AddScalar("Name", NHibernateUtil.String)
                ;
            return query;
        }


        public IList<AccessGroupListDto> GetAccessGroupList(
                int departmentId,
                string AccessGroupCode,
                string userName,
                int sortBy,
                bool? sortDescending)
        {
            string sqlQuery = "SELECT * FROM dbo.vwAccessGroupList";
            string whereString = GetDepartmentWhere(departmentId);
            whereString = GetAccessGroupCodeWhere(whereString, AccessGroupCode);
            whereString = GetUserNameWhere(whereString, userName);
            sqlQuery = GetSqlQueryOrdered(sqlQuery, whereString, sortBy, sortDescending);

            IQuery query = CreateQueryForList(sqlQuery);

            //AddDatesToQuery(query, beginDate, endDate, userName);
            return query.SetResultTransformer(Transformers.AliasToBean<AccessGroupListDto>()).List<AccessGroupListDto>();
        }
        public string GetDepartmentWhere(int DepartmentId)
        {
            string whereString = "";
            if (DepartmentId > 0)
            {
                whereString = string.Format(@"{0} DepartmentId = {1}", (whereString.Length > 0 ? whereString + @" and" : string.Empty), DepartmentId);
            }

            return whereString;
        }

        public string GetAccessGroupCodeWhere(string whereString, string AccessGroupCode)
        {
            if (!string.IsNullOrEmpty(AccessGroupCode))
            {
                whereString = string.Format(@"{0} AccessGroupCode = {1}", (whereString.Length > 0 ? whereString + @" and" : string.Empty), AccessGroupCode);
            }

            return whereString;
        }

        public override string GetUserNameWhere(string whereString, string userName)
        {
            if (!string.IsNullOrEmpty(userName))
            {
                whereString = string.Format(@"{0} userName like '{1}%'", (whereString.Length > 0 ? whereString + @" and" : string.Empty), userName);
            }

            return whereString;
        }

        public IQuery CreateQueryForList(string sqlQuery)
        {
            IQuery query = Session.CreateSQLQuery(sqlQuery)
                .AddScalar("UserId", NHibernateUtil.Int32)
                .AddScalar("UserName", NHibernateUtil.String)
                .AddScalar("PositionName", NHibernateUtil.String)
                .AddScalar("Dep3Name", NHibernateUtil.String)
                .AddScalar("Dep7Name", NHibernateUtil.String)
                //.AddScalar("DepartmentId", NHibernateUtil.Int32)
                .AddScalar("AccessGroupCode", NHibernateUtil.String)
                .AddScalar("AccessGroupName", NHibernateUtil.String)
                ;
            return query;
        }

        public override string GetSqlQueryOrdered(string sqlQuery, string whereString,
                    int sortedBy,
                    bool? sortDescending)
        {
            string orderBy = string.Empty;
            if (!string.IsNullOrEmpty(whereString))
            {
                sqlQuery += @" where " + whereString;
            }

            switch (sortedBy)
            {
                case 1:
                    orderBy = "UserName";
                    break;
                case 2:
                    orderBy = "PositionName";
                    break;
                case 3:
                    orderBy = "Dep3Name";
                    break;
                case 4:
                    orderBy = "Dep7Name";
                    break;
                case 5:
                    orderBy = "AccessGroupName";
                    break;
                default:
                    orderBy = "UserName";
                    break;
            }

            orderBy = string.Format(@" order by {0} {1} ", orderBy, (orderBy.Length > 0 && sortDescending.HasValue && sortDescending.Value) ? "desc" : string.Empty);

            sqlQuery += orderBy;

            return sqlQuery;
        }
    }
}