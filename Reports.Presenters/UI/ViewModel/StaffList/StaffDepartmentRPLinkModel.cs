using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;
using Reports.Core.Domain;

namespace Reports.Presenters.UI.ViewModel.StaffList
{
    /// <summary>
    /// Модель справочника РП-привязок.
    /// </summary>
    public class StaffDepartmentRPLinkModel
    {
        public IList<StaffDepartmentRPLinkDto> RPLinks { get; set; }

        #region Поля для модального окна
        public int Id { get; set; }

        [Display(Name = "Название РП-привязки"),
            StringLength(150, ErrorMessage = "Не более 150 знаков."),
            Required(ErrorMessage = "*")]
        public string Name { get; set; }

        [Display(Name = "Код РП-привязки"),
            StringLength(7, ErrorMessage = "Не более 7 знаков."),
            Required(ErrorMessage = "*")]
        public string Code { get; set; }

        [Display(Name = "Бизнес-группа")]
        public int BGId { get; set; }
        public IList<StaffDepartmentBusinessGroupDto> BusinessGroups { get; set; }

        [Display(Name = "Подразделение в СКД")]
        public int DepartmentId { get; set; }
        public IList<Department> SixLevelDeps { get; set; }


        #endregion

        #region Служебные поля.
        public string MessageStr { get; set; }
        #endregion
    }
}
