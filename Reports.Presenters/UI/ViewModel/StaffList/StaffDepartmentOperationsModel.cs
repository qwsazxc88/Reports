using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;


namespace Reports.Presenters.UI.ViewModel.StaffList
{
    /// <summary>
    /// Модель справочника операций.
    /// </summary>
    public class StaffDepartmentOperationsModel
    {
        public IList<StaffDepartmentOperationsDto> Operations { get; set; }

        #region Поля для модального окна
        public int oId { get; set; }

        [Display(Name = "Название операции"),
            StringLength(250, ErrorMessage = "Не более 250 знаков."),
            Required(ErrorMessage = "*")]
        public string oName { get; set; }

        [Display(Name = "Используется")]
        public bool oIsUsed { get; set; }

        #endregion
    }
}
