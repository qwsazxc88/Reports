using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;
using System.Web;
using System.Web.Mvc;
namespace Reports.Presenters.UI.ViewModel
{
    public class VacationReturnViewModel: StandartEditModel
    {
        [Display(Name="Обоснование отзыва")]
        [Required(ErrorMessage = "Обязательное поле")]
        public string ReturnReason { get; set; }

        [Display(Name="Дата отзыва с ")]
        [DisplayFormat(ApplyFormatInEditMode=true, DataFormatString="dd.MM.yyyy")]
        [Required(ErrorMessage = "Обязательное поле")]
        public DateTime? ReturnDate { get; set; }

        [Display(Name = "Дата отзыва по ")]
        [Required(ErrorMessage = "Обязательное поле")]
        public DateTime? ContinueDate { get; set; }

        [Display(Name = "Количество неиспользованных дней отпуска в связи с отзывом")]
        public int DaysCount { get; set; }

        [Display(Name = "Руководитель")]
        public ApprovalViewModel Manager { get; set; }
        [Display(Name = "Вышестоящий руководитель")]
        public ApprovalViewModel Chief { get; set; }
        [Display(Name = "Кадровик")]
        public ApprovalViewModel PersonnelManager { get; set; }

        [Display(Name="Вид отзыва")]
        public int ReturnType { get; set; }        
        public List<IdNameDto> ReturnTypes { get; set; }

        [Display(Name="Дата начала отпуска")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "dd.MM.yyyy")]
        public DateTime VacationStartDate { get; set; }

        [Display(Name = "Дата завершения отпуска")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "dd.MM.yyyy")]
        public DateTime VacationEndDate { get; set; }
        
        [Display(Name="Скан согласия сотрудника:")]
        public string FileName { get; set; }
        [Display(Name = "Скан согласия сотрудника:")]
        
        public HttpPostedFileBase File { get; set; }
        public UploadFileDto FileDto { get; set; }
        public int FileId { get; set; }
        #region Flags
        public bool ScanAddAvailable { get; set; }
        public bool IsScanVisible { get; set; }
        #endregion
        public VacationReturnViewModel()
        {
            this.User = new StandartUserDto();
            this.Creator = new StandartUserDto();
            this.Manager = new ApprovalViewModel();
            this.Chief = new ApprovalViewModel();
            this.PersonnelManager = new ApprovalViewModel();
        }
    }
}
