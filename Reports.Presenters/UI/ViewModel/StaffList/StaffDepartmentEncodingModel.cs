using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;
using Reports.Core.Domain;

namespace Reports.Presenters.UI.ViewModel.StaffList
{
    /// <summary>
    /// Модель для справочника кодирования.
    /// </summary>
    public class StaffDepartmentEncodingModel
    {
        #region Служебные поля.
        public string MessageStr { get; set; }
        public int TabIndex { get; set; }   //для позиционирования на вкладке
        #endregion
    }
}
