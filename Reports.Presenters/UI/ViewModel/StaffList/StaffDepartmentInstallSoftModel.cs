using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;
using Reports.Core.Domain;

namespace Reports.Presenters.UI.ViewModel.StaffList
{
    /// <summary>
    /// Модель справочника банковского ПО
    /// </summary>
    public class StaffDepartmentInstallSoftModel
    {
        [Display(Name = "Установленное ПО")]
        public IList<StaffDepartmentInstallSoftDto> SoftList { get; set; }

        #region Поля для модального окна
        [Display(Name = "Id ПО")]
        public int sId { get; set; }

        [Display(Name = "Наименование ПО")]
        public string sName { get; set; }
        #endregion
    }
}
