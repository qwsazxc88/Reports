using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;
namespace Reports.Presenters.UI.ViewModel
{
    public class StandartEditModel
    {
        #region Служебные поля
        public int Id { get; set; }
        #endregion
        #region Состояние
        [Display(Name = "Статус")]
        public string Status { get; set; }
        public int StatusId { get; set; }
        public bool IsEditable { get; set; }
        public bool IsRejected { get; set; }
        public int RequestType { get; set; }
        #endregion
        #region Стандартные поля
        [Display(Name = "Дата создания")]
        public DateTime CreateDate { get; set; }
        [Display(Name = "Номер заявки")]
        public string Number { get; set; }
        #endregion
        #region Создатель
        public StandartUserDto Creator { get; set; }
        #endregion
        #region Сотрудник
        public StandartUserDto User { get; set; }

        #endregion
        #region Кнопки
        public bool IsCancelAvailable { get; set; }
        public bool IsSaveAvailable { get; set; }
        #endregion
        
    }
}
