using Reports.Core.Domain;
using Reports.Core.Services;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;
using System;

namespace Reports.Core.Dao.Impl
{
    public class EmploymentPersonnelManagersDao : DefaultDao<PersonnelManagers>, IEmploymentPersonnelManagersDao
    {
        public EmploymentPersonnelManagersDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        /// <summary>
        /// Формируем номер приказа о приеме и трудового договора.
        /// </summary>
        /// <param name="ContractDate">Дата договора</param>
        /// <returns></returns>
        public string GetNewEmploymentContractNumber(DateTime ContractDate)
        {
            string NumberPart = ContractDate.Year.ToString().Substring(2) + "-" + ContractDate.DayOfYear.ToString() + "/";
            string SqlQuery = @"SELECT '" + NumberPart + @"' + cast(count(*) + 1 as nvarchar) as newContractNumber
                                FROM PersonnelManagers
                                WHERE EmploymentOrderNumber like '" + NumberPart + "%'";

            IQuery query = CreateEAQuery(SqlQuery);
            return query.UniqueResult<string>();
        }
        /// <summary>
        /// Создание подзапроса.
        /// </summary>
        /// <param name="sqlQuery"></param>
        /// <returns></returns>
        public virtual IQuery CreateEAQuery(string sqlQuery)
        {
            return Session.CreateSQLQuery(sqlQuery).AddScalar("newContractNumber", NHibernateUtil.String);
        }
    }
}