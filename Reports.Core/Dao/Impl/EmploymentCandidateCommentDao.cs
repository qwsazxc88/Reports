using System.Collections.Generic;
using Reports.Core.Domain;
using Reports.Core.Services;
using NHibernate;
using NHibernate.Transform;
using Reports.Core.Dto.Employment2;

namespace Reports.Core.Dao.Impl
{
    public class EmploymentCandidateCommentDao : DefaultDao<EmploymentCandidateComment>, IEmploymentCandidateCommentDao
    {
        public EmploymentCandidateCommentDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        public IList<EmploymentCandidateCommentDto> GetComments(int CandidateId, int CommentTypeId)
        {
            string sqlWhere = "";
            string sqlQuery = @"SELECT * FROM [dbo].[vwEmploymentComments] ";
            sqlWhere = "CandidateId = " + CandidateId.ToString() + " and CommentTypeId = " + CommentTypeId.ToString();
            sqlQuery += (sqlWhere.Length != 0 ? " WHERE " + sqlWhere : "");
            sqlQuery += " ORDER BY CreatedDate desc";

            IQuery query = CreateQuery(sqlQuery);
            IList<EmploymentCandidateCommentDto> documentList = query.SetResultTransformer(Transformers.AliasToBean(typeof(EmploymentCandidateCommentDto))).List<EmploymentCandidateCommentDto>();
            return documentList;
        }
        public override IQuery CreateQuery(string sqlQuery)
        {
            return Session.CreateSQLQuery(sqlQuery).
                AddScalar("Creator", NHibernateUtil.String).
                AddScalar("CreatorPosition", NHibernateUtil.String).
                AddScalar("CreatedDate", NHibernateUtil.DateTime).
                AddScalar("Comment", NHibernateUtil.String).
                AddScalar("CommentTypeId", NHibernateUtil.Int32);
        }
    }
}
