using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;


namespace Reports.Presenters.UI.ViewModel.StaffList
{
    /// <summary>
    /// Модель справочника групп опервций.
    /// </summary>
    public class StaffDepartmentOperationGroupsModel
    {
        public IList<StaffDepartmentOperationGroupsDto> OperationGroups { get; set; }

        #region Поля для модального окна
        public int gId { get; set; }

        [Display(Name = "Название группы"),
            StringLength(50, ErrorMessage = "Не более 50 знаков."),
            Required(ErrorMessage = "*")]
        public string gName { get; set; }

        [Display(Name = "Используется")]
        public bool gIsUsed { get; set; }

        #endregion

    }
}
