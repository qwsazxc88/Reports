using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;
using Reports.Core.Domain;

namespace Reports.Presenters.UI.ViewModel.StaffList
{
    /// <summary>
    /// Модель справочника филиалов.
    /// </summary>
    public class StaffDepartmentBranchModel
    {
        public IList<StaffDepartmentBranchDto> Branches { get; set; }
        public IList<Department> TwoLevelDeps { get; set; }
        #region Служебные поля.
        public string MessageStr { get; set; }
        //public int SwitchOperation { get; set; }
        //public int TabIndex { get; set; }   //для позиционирования на вкладке
        //public bool IsError { get; set; }
        //public bool IsModal { get; set; }   //модальная/обычная загрузка
        #endregion
    }
}
