using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;
using System.Web;
namespace Reports.Presenters.UI.ViewModel
{
    public class StaffMovementsEditModel:StandartEditModel
    {
        public StaffMovementsEditModel()
        {
            this.User = new StandartUserDto();
            this.Creator = new StandartUserDto();
        }
        #region Общее
        public IList<IdNameDto> RequestTypes { get; set; }
        [Display(Name = "Дата перевода")]
        public DateTime? MovementDate { get; set; }
        [Display(Name = "В подразделение")]
        public string TargetDepartmentName { get; set; }
        public int TargetDepartmentId { get; set; }
        [Display(Name = "На должность")]
        public string TargetPositionName { get; set; }
        public int TargetPositionId { get; set; }
        public IList<IdNameDto> TargetPositions { get; set; }
        public StandartUserDto SourceManager { get; set; }
        public StandartUserDto TargetManager { get; set; }
        #endregion
        #region Для Руководителя      
        [Display(Name = "Ставка")]
        public decimal TargetSalary { get; set; }
        [Display(Name = "Вид расчёта оклада")]
        public int TargetSalaryType { get; set; }
        [Display(Name = "Оклад")]
        public decimal TargetCasing { get; set; }
        [Display(Name="Условия перевода")]
        public string MovementCondition { get; set; }
        [Display(Name="Должностная надбавка")]
        public decimal AdditionPosition {get;set;}
        public DateTime? AdditionPositionTo {get;set;}
        [Display(Name = "Персональная надбавка")]
        public decimal AdditionPersonnel { get; set; }
        public DateTime? AdditionPersonnelTo { get; set; }
        [Display(Name = "Надбавка за стаж работы")]
        public decimal AdditionQuality { get; set; }
        public DateTime? AdditionQualityTo { get; set; }
        #endregion
        #region Для кадровиков
        public bool IsHourly { get; set; }
        [Display(Name="Дата приказа о переводе")]
        public DateTime? OrderDate { get; set; }
        [Display(Name="Дата ДС")]
        public DateTime? AdditionalAgreementDate { get; set; }
        [Display(Name="Номер приказа о переводе")]
        public string OrderNumber { get; set; }
        [Display(Name="Номер ДС")]
        public string AdditionalAgreementNumber { get; set; }
        [Display(Name="Районный коэффициент")]
        public decimal RegionCoefficient {get;set;}
        [Display(Name="Территориальная надбавка (руб.)")]
        public decimal AdditionTerritory {get;set;}
        [Display(Name="Надбавка за разъездной характер работы")]
        public decimal AdditionTraveling { get; set; }
        [Display(Name = "Надбавка за стаж работы специалистом фронт-офиса (руб.)")]
        public decimal AdditionFront { get; set; }
        public DateTime? AdditionFrontTo { get; set; }
        [Display(Name = "Грейд")]
        public int Grade { get; set; }
        [Display(Name = "График работы")]
        public int HoursType { get; set; }
        public IList<IdNameDto> HoursTypes { get; set; }
        [Display(Name = "Северный стаж")]
        public int NorthFactor { get; set; }
        public IList<IdNameDto> NorthFactors { get; set; }
        [Display(Name = "Пункты Дополнительного соглашения")]
        public string AdditionalAgreementEnties { get; set; }
        [Display(Name="Внести изменения в ТД №")]
        public string ChangesToAgreement { get; set; }
        [Display(Name = "Внести изменения в пункты ТД")]
        public string ChangesToAgreementEnties { get; set; }
        [Display(Name = "Дата ТД, в который вносятся изменения")]
        public DateTime? AgreementDate { get; set; }
        [Display(Name="Основание для приказа о переводе")]
        public string MovementReason { get; set; }
        [Display(Name="Группа доступа")]
        public int AccessGroup { get; set; }
        public IList<IdNameDto> AccessGroupsList { get; set; }
        [Display(Name="Подписант")]
        public string SignatoryName { get; set; }
        public int SignatoryId { get; set; }
        public IList<IdNameDto> SignatoryList { get; set; }
        #endregion
        #region Files
        public HttpPostedFileBase MovementNote { get; set; }
        public int MovementNoteAttachmentId { get; set; }
        public UploadFileDto MovementNoteDto { get; set; }

        public HttpPostedFileBase AdditionalAgreementDoc { get; set; }
        public int AdditionalAgreementDocAttachmentId { get; set; }
        public UploadFileDto AdditionalAgreementDocDto { get; set; }

        public HttpPostedFileBase MovementOrderDoc { get; set; }
        public int MovementOrderDocAttachmentId { get; set; }
        public UploadFileDto MovementOrderDocDto { get; set; }

        public HttpPostedFileBase MaterialLiabilityDoc { get; set; }
        public int MaterialLiabilityDocAttachmentId { get; set; }
        public UploadFileDto MaterialLiabilityDocDto { get; set; }

        public HttpPostedFileBase RequirementsOrderDoc { get; set; }
        public int RequirementsOrderDocAttachmentId { get; set; }
        public UploadFileDto RequirementsOrderDocDto { get; set; }

        public HttpPostedFileBase ServiceOrderDoc { get; set; }
        public int ServiceOrderDocAttachmentId { get; set; }
        public UploadFileDto ServiceOrderDocDto { get; set; }
        #endregion
    }
}
