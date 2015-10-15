using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;
using Reports.Core.Domain;

namespace Reports.Presenters.UI.ViewModel.StaffList
{
    /// <summary>
    /// Модель справочника ПО.
    /// </summary>
    public class StaffDepartmentSoftReferenceModel
    {
        #region Служебные поля.
        public int TabIndex { get; set; }   //для позиционирования на вкладке
        #endregion
    }
}
