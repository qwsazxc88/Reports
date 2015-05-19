using Reports.Core.Domain;
using Reports.Core.Services;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;

namespace Reports.Core.Dao.Impl
{
    public class EmploymentEducationDao : DefaultDao<Education>, IEmploymentEducationDao
    {
        public EmploymentEducationDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        /// <summary>
        /// Определяем наличие сведений об образовании по количеству строк.
        /// </summary>
        /// <param name="UserId">Id кандидата.</param>
        /// <param name="Type">Тип образования: 1 - Сведения об образовании, 2 - Послевузовское образование, 3 - Аттестация, 4 - Повышение квалификации.</param>
        /// <returns></returns>
        public int CheckExistsEducationRecord(int UserId, int Type)
        {
            string TypeStr = string.Empty;
            switch (Type)
            {
                case 1:
                    TypeStr = "INNER JOIN HigherEducationDiploma as C ON C.EducationId = B.Id";
                    break;
                case 2:
                    TypeStr = "INNER JOIN PostGraduateEducationDiploma as C ON C.EducationId = B.Id";
                    break;
                case 3:
                    TypeStr = "INNER JOIN Certification as C ON C.EducationId = B.Id";
                    break;
                case 4:
                    TypeStr = "INNER JOIN Training as C ON C.EducationId = B.Id";
                    break;
            }

            string QueryStr = string.Format(@"SELECT count(*) as RowCnt FROM dbo.EmploymentCandidate as A 
                                              INNER JOIN Education as B ON B.CandidateId = A.Id
                                              {0}
                                              WHERE A.UserId = " + UserId.ToString(), TypeStr);

            IQuery query = CreateDLQuery(QueryStr);
            return query.UniqueResult<int>();
        }
        /// <summary>
        /// Создание подзапроса.
        /// </summary>
        /// <param name="sqlQuery"></param>
        /// <returns></returns>
        public virtual IQuery CreateDLQuery(string sqlQuery)
        {
            return Session.CreateSQLQuery(sqlQuery).AddScalar("RowCnt", NHibernateUtil.Int32);
        }
    }
}