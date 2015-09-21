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
        #region Группы ПО
        [Display(Name = "Группы ПО")]
        public IList<IdNameDto> SoftGroupList { get; set; }
        #endregion

        #region Связи ПО с группами
        [Display(Name = "Группы ПО")]
        public int? SoftGroupId { get; set; }

        [Display(Name = "Установленное ПО")]
        public int? SoftId { get; set; }

        [Display(Name = "Связи ПО с группами")]
        public int? SoftGroupLinkId { get; set; }
        public IList<SoftGroupLinkDto> SoftGroupLink { get; set; }
        #endregion

        #region Установленное ПО
        [Display(Name = "Установленное ПО")]
        public IList<IdNameDto> SoftList { get; set; }
        #endregion

        #region Служебные поля.
        public string MessageStr { get; set; }
        #endregion
    }
}
