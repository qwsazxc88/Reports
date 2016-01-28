using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;
using Reports.Core.Domain;
using System.Web.Mvc;

namespace Reports.Presenters.UI.ViewModel.StaffList
{
    /// <summary>
    /// Модель заявки на создание временной вакансии при длительном отсутствии сотрудника.
    /// </summary>
    public class StaffTemporaryReleaseVacancyRequestModel
    {
        #region Шапка заявки
        [Display(Name = "Дата создания заявки")]
        public DateTime? CreateDate { get; set; }

        [Display(Name = "Номер заявки")]
        public int Id { get; set; }

        [Display(Name = "Инициатор")]
        public string RequestInitiator { get; set; } //фио + должность
        public int UserId { get; set; }

        [Display(Name = "Код штатной единицы")]
        public int SEPId { get; set; }

        [Display(Name = "Назание структурного подразделения")]
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }

        [Display(Name = "Принадлежность подразделения")]
        public string AccessoryName { get; set; }

        [Display(Name = "Руководители")]
        public string Managers { get; set; }

        [Display(Name = "Должность")]
        public int PositionId { get; set; }
        public string PositionName { get; set; }

        [Display(Name = "ФИО сотрудника")]
        public string Surname { get; set; }
        #endregion


        #region Редактируемые поля
        #endregion
        [Display(Name = "Виды отсутствий")]
        public int AbsencesTypeId { get; set; }
        public IList<IdNameDto> AbsencesTypes { get; set; }

        [Display(Name = "Начало периода")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateBegin { get; set; }

        [Display(Name = "Конец периода")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateEnd { get; set; }

        [Display(Name = "Примечание")]
        public string Note { get; set; }

        

        #region Служебные переменные
        //public bool IsNew { get; set; } //признак новой заявки
        public bool IsUsed { get; set; }    //признак использования
        public string MessageStr { get; set; }  //для сообщений

        #endregion
    }
}
