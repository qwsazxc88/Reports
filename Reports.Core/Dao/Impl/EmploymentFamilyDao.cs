using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;
using Reports.Core.Domain;
using Reports.Core.Services;
using Reports.Core.Dto.Employment2;

namespace Reports.Core.Dao.Impl
{
    public class EmploymentFamilyDao : DefaultDao<Family>, IEmploymentFamilyDao
    {
        public EmploymentFamilyDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        /// <summary>
        /// Седения оо образовании.
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public IList<FamilyStatusDto> GetFamilyStatuses()
        {
            string sqlQuery = @"SELECT * FROM [dbo].[FamilyStatus]";
            IQuery query = CreateQuery(sqlQuery);
            IList<FamilyStatusDto> documentList = query.SetResultTransformer(Transformers.AliasToBean(typeof(FamilyStatusDto))).List<FamilyStatusDto>();
            return documentList;
        }
        public override IQuery CreateQuery(string sqlQuery)
        {
            return Session.CreateSQLQuery(sqlQuery).
                AddScalar("Id", NHibernateUtil.Int32).
                AddScalar("Name", NHibernateUtil.String);
        }
    }
}