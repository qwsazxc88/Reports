using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;
using Reports.Core.Domain;

namespace Reports.Presenters.UI.ViewModel.StaffList
{
    /// <summary>
    /// Модель справочника управлений
    /// </summary>
    public class StaffDepartmentAdministrationModel
    {
        public IList<StaffDepartmentAdministrationDto> Administrations { get; set; }

        #region Справочники для фильтров
        [Display(Name = "Фильтр по дирекции")]
        public int ManagementFilterId { get; set; }

        [Display(Name = "Фильтр по филиалу")]
        public int BranchFilterId { get; set; }
        public IList<StaffDepartmentBranchDto> Branches { get; set; }
        #endregion

        #region Поля для модального окна
        public int aId { get; set; }

        [Display(Name = "Название управления"),
            StringLength(150, ErrorMessage = "Не более 150 знаков."),
            Required(ErrorMessage = "*")]
        public string aName { get; set; }

        [Display(Name = "Код управления"),
            StringLength(7, ErrorMessage = "Не более 7 знаков."),
            Required(ErrorMessage = "*")]
        public string aCode { get; set; }

        [Display(Name = "Дирекция")]
        public int ManagementId { get; set; }
        public IList<StaffDepartmentManagementDto> Managements { get; set; }

        [Display(Name = "Подразделение в СКД")]
        public int aDepartmentId { get; set; }
        public IList<Department> FourLevelDeps { get; set; }


        #endregion

        #region Служебные поля.
        public string MessageStr { get; set; }
        #endregion
    }
}
