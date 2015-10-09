using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;
using Reports.Core.Domain;

namespace Reports.Presenters.UI.ViewModel.StaffList
{
    /// <summary>
    /// Модель справочника бизнес-групп.
    /// </summary>
    public class StaffDepartmentBusinessGroupModel
    {
        public IList<StaffDepartmentBusinessGroupDto> BusinessGroups { get; set; }

        #region Поля для модального окна
        public int bId { get; set; }

        [Display(Name = "Название бизнесс-группы"),
            StringLength(150, ErrorMessage = "Не более 150 знаков."),
            Required(ErrorMessage = "*")]
        public string bName { get; set; }

        [Display(Name = "Код бизнесс-группы"),
            StringLength(11, ErrorMessage = "Не более 11 знаков."),
            Required(ErrorMessage = "*")]
        public string bCode { get; set; }

        [Display(Name = "Управление")]
        public int AdminId { get; set; }
        public IList<StaffDepartmentAdministrationDto> Administrations { get; set; }

        [Display(Name = "Подразделение в СКД")]
        public int bDepartmentId { get; set; }
        public IList<Department> FiveLevelDeps { get; set; }


        #endregion

        #region Служебные поля.
        public string MessageStr { get; set; }
        #endregion
    }
}
