using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;
using Reports.Core.Domain;

namespace Reports.Presenters.UI.ViewModel.StaffList
{
    /// <summary>
    /// Модель справочника связей банковского ПО
    /// </summary>
    public class StaffDepartmentSoftGroupLinksModel
    {
        [Display(Name = "Группы ПО")]
        public int SoftGroupId { get; set; }
        public IList<StaffDepartmentSoftGroupDto> GroupList { get; set; }

        [Display(Name = "Связи ПО с группами")]
        public IList<StaffDepartmentSoftGroupLinksDto> SoftGroupLink { get; set; }
    }
}
