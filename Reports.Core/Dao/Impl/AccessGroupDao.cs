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

        /// <summary>
        /// Функция возвращает список сотрудников по группе доступа
        /// </summary>
        /// <param name="depFromFilter">Подразделение</param>
        /// <param name="AccessGroupCode">Код группы доступа</param>
        /// <param name="userName">ФИО сотрудника</param>
        /// <param name="Manager6">ФИО руководителя 6 уровня</param>
        /// <param name="Manager5">ФИО руководителя 5 уровня</param>
        /// <param name="Manager4">ФИО руководителя 4 уровня</param>
        /// <param name="IsManagerShow">Показать руководителей</param>
        /// <param name="sortBy">Id колонки для сортировки</param>
        /// <param name="sortDescending">Тип сортировки</param>
        /// <returns></returns>
        public IList<AccessGroupListDto> GetAccessGroupList(
                Department depFromFilter,
                string AccessGroupCode,
                string userName,
                string Manager6,
                string Manager5,
                string Manager4,
                bool IsManagerShow,
                int sortBy,
                bool? sortDescending)
        {
            string sqlQuery = "SELECT * FROM dbo." + (!IsManagerShow ? "vwAccessGroupListWithoutManagers ag " : "vwAccessGroupList ag");
            sqlQuery += " LEFT JOIN (	SELECT  userId,SaldoPrimary,SaldoAdditional FROM VacationSaldo vs2	INNER JOIN (SELECT MAX(vs1.Id) as id FROM VacationSaldo vs1 GROUP BY UserID) p ON p.id=vs2.id) vs ON ag.UserId=vs.UserId";
            string whereString = GetDepartmentWhere(depFromFilter);
            whereString = GetAccessGroupCodeWhere(whereString, AccessGroupCode);
            whereString = GetUserNameWhere(whereString, userName);
            whereString = GetManagersWhere(whereString, Manager6, Manager5, Manager4);
            sqlQuery = GetSqlQueryOrdered(sqlQuery, whereString, sortBy, sortDescending);

            IQuery query = CreateQueryForList(sqlQuery);

            //AddDatesToQuery(query, beginDate, endDate, userName);
            return query.SetResultTransformer(Transformers.AliasToBean<AccessGroupListDto>()).List<AccessGroupListDto>();
        }
        public string GetDepartmentWhere(Department depFromFilter)
        {
            string whereString = "";
            if (depFromFilter != null)
            {
                whereString = string.Format(@"{0} UserDepPath like '{1}%'", (whereString.Length > 0 ? whereString + @" and" : string.Empty), depFromFilter.Path);
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

        public string GetManagersWhere(string whereString, string manager6, string manager5, string manager4)
        {
            if (!string.IsNullOrEmpty(manager6))
            {
                whereString = string.Format(@"{0} manager6 like '%{1}%'", (whereString.Length > 0 ? whereString + @" and" : string.Empty), manager6);
            }

            if (!string.IsNullOrEmpty(manager5))
            {
                whereString = string.Format(@"{0} manager5 like '%{1}%'", (whereString.Length > 0 ? whereString + @" and" : string.Empty), manager5);
            }

            if (!string.IsNullOrEmpty(manager4))
            {
                whereString = string.Format(@"{0} manager4 like '%{1}%'", (whereString.Length > 0 ? whereString + @" and" : string.Empty), manager4);
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
                .AddScalar("Email", NHibernateUtil.String)
                .AddScalar("EndDate", NHibernateUtil.DateTime)
                .AddScalar("Manager6", NHibernateUtil.String)
                .AddScalar("Manager5", NHibernateUtil.String)
                .AddScalar("Manager4", NHibernateUtil.String)
                .AddScalar("SaldoPrimary",NHibernateUtil.Decimal)
                .AddScalar("SaldoAdditional",NHibernateUtil.Decimal)
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
                case 6:
                    orderBy = "Email";
                    break;
                case 7:
                    orderBy = "EndDate";
                    break;
                case 8:
                    orderBy = "Manager6";
                    break;
                case 9:
                    orderBy = "Manager5";
                    break;
                case 10:
                    orderBy = "Manager4";
                    break;
                case 11:
                    orderBy = "SaldoPrimary";
                    break;
                case 12:
                    orderBy = "SaldoAdditional";
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