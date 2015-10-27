using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;

namespace Reports.Presenters.UI.ViewModel.StaffList
{
    /// <summary>
    /// Модель связей групп и операций.
    /// </summary>
    public class StaffDepartmentOperationLinksModel
    {
        [Display(Name = "Группа операций")]
        public int OperationGroupId { get; set; }
        public IList<StaffDepartmentOperationGroupsDto> OperationGroups { get; set; }
        [Display(Name = "Операция")]
        public IList<StaffDepartmentOperationLinksDto> OperationList { get; set; }
    }
}
