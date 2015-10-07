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
        ///// <summary>
        ///// Операция: 1 - создание новой группы, 2 - редактирование группы, 3 - сохранение связей, 4 - создание новой строки ПО, 5 - редактирование строки ПО
        ///// </summary>
        //public int SwitchOperation { get; set; }
        public int TabIndex { get; set; }   //для позиционирования на вкладке
        //public bool IsError { get; set; }
        //public bool IsModal { get; set; }   //модальная/обычная загрузка
        #endregion
    }
}
