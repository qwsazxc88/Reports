using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;
using Reports.Core.Domain;

namespace Reports.Presenters.UI.ViewModel.StaffList
{
    /// <summary>
    /// Модель справочника дирекций.
    /// </summary>
    public class StaffDepartmentManagementModel
    {
        public IList<StaffDepartmentManagementDto> Managements { get; set; }

        #region Поля для модального окна
        public int mId { get; set; }

        [Display(Name = "Название дирекции"),
            StringLength(50, ErrorMessage = "Не более 50 знаков."),
            Required(ErrorMessage = "*")]
        public string mName { get; set; }

        [Display(Name = "Код дирекции"),
            StringLength(3, ErrorMessage = "Не более 3 знаков."),
            Required(ErrorMessage = "*")]
        public string mCode { get; set; }

        [Display(Name = "Филиал")]
        public int BranchId { get; set; }
        public IList<StaffDepartmentBranchDto> Branches { get; set; }

        [Display(Name = "Подразделение в СКД")]
        public int mDepartmentId { get; set; }
        public IList<Department> ThreeLevelDeps { get; set; }


        #endregion

        #region Служебные поля.
        public string MessageStr { get; set; }
        #endregion
    }
}
