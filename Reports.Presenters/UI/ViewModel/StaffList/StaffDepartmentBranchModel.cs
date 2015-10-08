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
        

        #region Поля для модального окна
        public int Id { get; set; }

        [Display(Name = "Название филиала"),
            StringLength(50, ErrorMessage = "Не более 50 знаков."),
            Required(ErrorMessage = "*")]
        public string Name { get; set; }

        [Display(Name = "Код филиала"),
            StringLength(2, ErrorMessage = "Не более 2 знаков."),
            Required(ErrorMessage = "*")]
        public string Code { get; set; }

        [Display(Name = "Подразделение в СКД")]
        public int DepartmentId { get; set; }
        public IList<Department> TwoLevelDeps { get; set; }
        

        #endregion

        #region Служебные поля.
        public string MessageStr { get; set; }
        //public int SwitchOperation { get; set; }
        //public int TabIndex { get; set; }   //для позиционирования на вкладке
        //public bool IsError { get; set; }
        //public bool IsModal { get; set; }   //модальная/обычная загрузка
        #endregion
    }
}
