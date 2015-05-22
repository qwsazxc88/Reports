using System;
using System.Collections.Generic;
using Reports.Core.Domain;
using Reports.Core.Dto;

namespace Reports.Core.Dao
{
    public interface IHelpBillingExecutorTaskDao : IDao<HelpBillingExecutorTasks>
    {
        /// <summary>
        /// Список сотрудников получателей задачи.
        /// </summary>
        /// <param name="HelpBillingId">Id задачи биллинга</param>
        /// <param name="IsNew">Признак, создается задача или отправлена на выполнение</param>
        /// <returns></returns>
        IList<HelpPersonnelBillingRecipientDto> GetHelpBillingRecipients(int HelpBillingId, bool IsNew);
        /// <summary>
        /// Список групп сотрудников получателей задачи.
        /// </summary>
        /// <param name="HelpBillingId">Id задачи биллинга</param>
        /// <returns></returns>
        IList<HelpPersonnelBillingRecipientGroupsDto> GetHelpBillingRecipientGroups(int HelpBillingId);
    }
}
