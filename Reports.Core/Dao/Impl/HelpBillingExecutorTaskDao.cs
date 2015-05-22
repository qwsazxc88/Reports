using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Transform;
using Reports.Core.Domain;
using Reports.Core.Dto;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class HelpBillingExecutorTaskDao : DefaultDao<HelpBillingExecutorTasks>, IHelpBillingExecutorTaskDao
    {
        public HelpBillingExecutorTaskDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        /// <summary>
        /// Список сотрудников получателей задачи.
        /// </summary>
        /// <param name="HelpBillingId">Id задачи биллинга</param>
        /// <param name="IsNew">Признак, создается задача или отправлена на выполнение</param>
        /// <returns></returns>
        public IList<HelpPersonnelBillingRecipientDto> GetHelpBillingRecipients(int HelpBillingId, bool IsNew)
        {
            //string sqlWhere = "";
            string sqlQuery = string.Format(@"SELECT * FROM dbo.fnGetHelpBillingExecutorForTask({0}, {1}) ", HelpBillingId.ToString(), IsNew ? "1" : "0");
            sqlQuery += " ORDER BY Name";

            IQuery query = CreateRecipientQuery(sqlQuery);
            IList<HelpPersonnelBillingRecipientDto> documentList = query.SetResultTransformer(Transformers.AliasToBean(typeof(HelpPersonnelBillingRecipientDto))).List<HelpPersonnelBillingRecipientDto>();
            return documentList;
        }
        public IQuery CreateRecipientQuery(string sqlQuery)
        {
            return Session.CreateSQLQuery(sqlQuery).
                AddScalar("Id", NHibernateUtil.Int32).
                AddScalar("UserId", NHibernateUtil.Int32).
                AddScalar("RoleId", NHibernateUtil.Int32).
                AddScalar("Name", NHibernateUtil.String).
                AddScalar("Description", NHibernateUtil.String).
                AddScalar("IsRecipient", NHibernateUtil.Boolean);
        }
        /// <summary>
        /// Список групп сотрудников получателей задачи.
        /// </summary>
        /// <param name="HelpBillingId">Id задачи биллинга</param>
        /// <returns></returns>
        public IList<HelpPersonnelBillingRecipientGroupsDto> GetHelpBillingRecipientGroups(int HelpBillingId)
        {
            //string sqlWhere = "";
            string sqlQuery = string.Format(@"SELECT * FROM dbo.fnGetHelpBillingExecutorGroupForTask({0})", HelpBillingId.ToString());
            sqlQuery += " ORDER BY [Description]";

            IQuery query = CreateRecipientGroupsQuery(sqlQuery);
            IList<HelpPersonnelBillingRecipientGroupsDto> documentList = query.SetResultTransformer(Transformers.AliasToBean(typeof(HelpPersonnelBillingRecipientGroupsDto))).List<HelpPersonnelBillingRecipientGroupsDto>();
            return documentList;
        }
        public IQuery CreateRecipientGroupsQuery(string sqlQuery)
        {
            return Session.CreateSQLQuery(sqlQuery).
                AddScalar("RoleId", NHibernateUtil.Int32).
                AddScalar("Description", NHibernateUtil.String).
                AddScalar("IsRecipient", NHibernateUtil.Boolean).
                AddScalar("IsRecipientOld", NHibernateUtil.Boolean);
        }
    }
}
