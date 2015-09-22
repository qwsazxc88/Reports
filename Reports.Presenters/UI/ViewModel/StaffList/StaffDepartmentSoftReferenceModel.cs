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
        [Display(Name = "Id группы ПО")]
        public int? GroupId { get; set; }

        [Display(Name = "Наименование группы ПО")]
        public string SoftGroupName { get; set; }

        [Display(Name = "Группы ПО")]
        public IList<IdNameWithOldNameDto> GroupList { get; set; }
        #endregion

        #region Связи ПО с группами
        
        [Display(Name = "Группы ПО")]
        public int SoftGroupId { get; set; }

        [Display(Name = "Связи ПО с группами")]
        public int? SoftGroupLinkId { get; set; }
        public IList<SoftGroupLinkDto> SoftGroupLink { get; set; }
        #endregion

        #region Установленное ПО
        [Display(Name = "Id ПО")]
        public int? SoftId { get; set; }

        [Display(Name = "Наименование ПО")]
        public string SoftName { get; set; }

        [Display(Name = "Установленное ПО")]
        public IList<IdNameWithOldNameDto> SoftList { get; set; }
        #endregion

        #region Служебные поля.
        public string MessageStr { get; set; }
        /// <summary>
        /// Операция: 1 - создание новой группы, 2 - редактирование группы, 3 - сохранение связей, 4 - создание новой строки ПО, 5 - редактирование строки ПО
        /// </summary>
        public int SwitchOperation { get; set; }
        public int TabIndex { get; set; }   //для позиционирования на вкладке
        public bool IsError { get; set; }   
        #endregion
    }
}
