using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;
using Reports.Core.Domain;

namespace Reports.Presenters.UI.ViewModel.StaffList
{
    /// <summary>
    /// Модель справочника групп ПО
    /// </summary>
    public class StaffDepartmentSoftGroupModel
    {
        [Display(Name = "Группы ПО")]
        public IList<StaffDepartmentSoftGroupDto> GroupList { get; set; }

        #region Поля для модального окна
        [Display(Name = "Id группы ПО")]
        public int? gId { get; set; }

        [Display(Name = "Наименование группы ПО"),
            StringLength(50, ErrorMessage = "Не более 50 знаков."),
            Required(ErrorMessage = "*")]
        public string gName { get; set; }
        #endregion

    }
}
