using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Presenters.UI.ViewModel.StaffList
{
    /// <summary>
    /// Модель справочника операций подразделений.
    /// </summary>
    public class StaffDepartmentOperationReferenceModel
    {
        #region Служебные поля.
        public bool IsModal { get; set; }   //для модального вызова
        public int TabIndex { get; set; }   //для позиционирования на вкладке
        #endregion
    }
}
