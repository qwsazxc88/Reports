using System.Collections.Generic;
using Reports.Core.Domain;
using Reports.Core.Services;
using NHibernate;
using NHibernate.Transform;
using Reports.Core.Dto;

namespace Reports.Core.Dao
{
    public interface IHelpPersonnelBillingCommentDao : IDao<HelpPersonnelBillingComments>
    {
        /// <summary>
        /// Достаем переписку по заданной задаче внутреннего биллинга.
        /// </summary>
        /// <param name="HelpBillingId">Id задачи внутреннего биллинга</param>
        /// <returns></returns>
        IList<HelpPersonnelBillingCommentsDto> GetComments(int HelpBillingId);
    }
}
