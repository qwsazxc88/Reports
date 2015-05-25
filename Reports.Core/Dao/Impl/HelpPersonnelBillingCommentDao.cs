using System.Collections.Generic;
using Reports.Core.Domain;
using Reports.Core.Services;
using NHibernate;
using NHibernate.Transform;
using Reports.Core.Dto;

namespace Reports.Core.Dao.Impl
{
    public class HelpPersonnelBillingCommentDao : DefaultDao<HelpPersonnelBillingComments>, IHelpPersonnelBillingCommentDao
    {
        public HelpPersonnelBillingCommentDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        /// <summary>
        /// Достаем переписку по заданной задаче внутреннего биллинга.
        /// </summary>
        /// <param name="HelpBillingId">Id задачи внутреннего биллинга</param>
        /// <returns></returns>
        public IList<HelpPersonnelBillingCommentsDto> GetComments(int HelpBillingId)
        {
            string sqlWhere = "";
            string sqlQuery = @"SELECT * FROM [dbo].[vwHelpBillingComments] ";
            sqlWhere = "HelpBillingId = " + HelpBillingId.ToString();
            sqlQuery += (sqlWhere.Length != 0 ? " WHERE " + sqlWhere : "");
            sqlQuery += " ORDER BY CreatedDate desc";

            IQuery query = CreateCommentQuery(sqlQuery);
            IList<HelpPersonnelBillingCommentsDto> documentList = query.SetResultTransformer(Transformers.AliasToBean(typeof(HelpPersonnelBillingCommentsDto))).List<HelpPersonnelBillingCommentsDto>();
            return documentList;
        }
        public IQuery CreateCommentQuery(string sqlQuery)
        {
            return Session.CreateSQLQuery(sqlQuery).
                AddScalar("Creator", NHibernateUtil.String).
                AddScalar("CreatedDate", NHibernateUtil.DateTime).
                AddScalar("Comment", NHibernateUtil.String).
                AddScalar("IsQuestion", NHibernateUtil.Boolean);
        }
    }
}
