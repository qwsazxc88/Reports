using System;
using System.Collections.Generic;

namespace Reports.Core.Dto
{
    public class HelpPersonnelBillingRequestDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Urgency { get; set; }
        public DateTime CreateDate { get; set; }
        public string ForUserName { get; set; }
        public string Dep3Name { get; set; }
        public string Dep7Name { get; set; }
        public int RequestNumber { get; set; }
        public string CreatorName { get; set; }
        //public string RepicientName { get; set; }
        public int StatusNumber { get; set; }
        public string Status { get; set; }
        public int Number { get; set; }
    }

    public class HelpPersonnelBillingCommentsDto
    {
        public string Creator { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Comment { get; set; }
        public bool IsQuestion { get; set; }
    }
    /// <summary>
    /// Список сотрудников, которые получают задачи из биллинга
    /// </summary>
    public class HelpPersonnelBillingRecipientDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsRecipient { get; set; }
    }
    /// <summary>
    /// Список групп сотрудников, которые получают задачи в биллинге
    /// </summary>
    public class HelpPersonnelBillingRecipientGroupsDto
    {
        public int RoleId { get; set; }
        public string Description { get; set; }
        public bool IsRecipient { get; set; }
        public bool IsRecipientOld { get; set; }
    }

    public class HelpPersonnelBillingExecutorsDto
    {
        public IList<HelpPersonnelBillingRecipientDto> RecipientList { get; set; }
        public IList<HelpPersonnelBillingRecipientGroupsDto> RecipientGroups { get; set; }
    }
}