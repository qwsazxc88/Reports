using Reports.Core.Dto.Employment2;
using Reports.Core.Domain;
using Reports.Core.Services;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;
using System.Collections.Generic;

namespace Reports.Core.Dao.Impl
{
    public class EmploymentHigherEducationDiplomaDao : DefaultDao<HigherEducationDiploma>, IEmploymentHigherEducationDiplomaDao
    {
        public EmploymentHigherEducationDiplomaDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        /// <summary>
        /// Седения оо образовании.
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public IList<HigherEducationDiplomaDto> GetHighEducationTypes(int EducationId)
        {
            string sqlQuery = @"SELECT * FROM [dbo].[vwEmploymentHighEducations] WHERE EducationId = " + EducationId.ToString();
            IQuery query = CreateQuery(sqlQuery);
            IList<HigherEducationDiplomaDto> documentList = query.SetResultTransformer(Transformers.AliasToBean(typeof(HigherEducationDiplomaDto))).List<HigherEducationDiplomaDto>();
            return documentList;
        }
        public override IQuery CreateQuery(string sqlQuery)
        {
            return Session.CreateSQLQuery(sqlQuery).
                AddScalar("Id", NHibernateUtil.Int32).
                AddScalar("EducationTypeName", NHibernateUtil.String).
                AddScalar("IssuedBy", NHibernateUtil.String).
                AddScalar("Series", NHibernateUtil.String).
                AddScalar("Number", NHibernateUtil.String).
                AddScalar("AdmissionYear", NHibernateUtil.String).
                AddScalar("GraduationYear", NHibernateUtil.String).
                AddScalar("Qualification", NHibernateUtil.String).
                AddScalar("Speciality", NHibernateUtil.String).
                AddScalar("Profession", NHibernateUtil.String).
                AddScalar("Department", NHibernateUtil.String).
                AddScalar("LocationEI", NHibernateUtil.String);
        }
    }
}
