using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;
namespace Reports.Presenters.UI.ViewModel
{
    public class VacationReturnViewModel: StandartEditModel
    {
        [Display(Name="Обоснование отзыва")]
        public FieldViewModel<string> ReturnReason { get; set; }

        [Display(Name="Дата отзыва с ")]
        [DisplayFormat(ApplyFormatInEditMode=true, DataFormatString="dd.MM.yyyy")]
        public DateFieldViewModel ReturnDate { get; set; }

        [Display(Name = "Дата отзыва по ")]
        public DateFieldViewModel ContinueDate { get; set; }

        [Display(Name = "Кол-во неиспользованных дней отпуска в связи с отзывом")]
        public int DaysCount { get; set; }

        [Display(Name = "Руководитель")]
        public string ManagerName { get; set; }
        public DateTime ManagerDateAccept { get; set; }
        public string ChiefName { get; set; }
        public DateTime ChiefDateAccept { get; set; }
        public string PersonnelManagerName { get; set; }
        public DateTime PersonnelManagerDateAccept { get; set; }
        public int ReturnType { get; set; }
        public List<IdNameDto> ReturnTypes { get; set; }
        public DateTime VacationStartDate { get; set; }
        public DateTime VacationEndDate { get; set; }
        public bool ScanAddAvailable { get; set; }
        public bool IsScanVisible { get; set; }
        public bool PersonnelManagerAccept { get; set; }
        public bool ManagerAccept { get; set; }
        public bool ChiefAccept { get; set; }
        public bool PersonnelManagerAcceptAvailable { get; set; }
        public bool ManagerAcceptAvailable { get; set; }
        public bool ChiefAcceptAvailable { get; set; }
        [Display(Name="Скан согласия сотрудника:")]
        public string FileName { get; set; }

        public VacationReturnViewModel()
        {
            this.User = new StandartUserDto();
            this.Creator = new StandartUserDto();
            this.ReturnReason = new FieldViewModel<string>();
            this.ReturnDate = new DateFieldViewModel();
            this.ContinueDate = new DateFieldViewModel();
        }
    }
}
