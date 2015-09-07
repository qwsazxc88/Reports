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
            this.StatusId = 1;
        }
        #region Служебное
        public bool IsDepartmentEditable { get; set; }
        public bool IsPositionEditable { get; set; }

        public bool IsManagerEditable { get; set; }
        public bool IsPersonnelManagerEditable { get; set; }
        public bool IsSourceManagerAcceptAvailable { get; set; }
        public bool IsTargetManagerAcceptAvailable { get; set; }
        public bool IsPersonnelManagerAcceptAvailable { get; set; }
        public bool IsPersonnelManagerBankAcceptAvailable { get; set; }
        public bool IsChiefAcceptAvailable { get; set; }
        public bool IsUserAcceptAvailable { get; set; }
        public bool ISRejectAvailable { get; set; }
        public bool IsDocsEditable { get; set; }
        public bool IsDocsAddAvailable { get; set; }
        public bool IsConfirmButtonAvailable { get; set; }
        public bool IsStopButtonAvailable { get; set; }

        public bool IsRejectButtonPressed { get; set; }
        public bool IsAcceptButtonPressed { get; set; }
        public bool IsConfirmButtonPressed { get; set; }
        public bool IsCancelButtonPressed { get; set; }
        public bool IsStopButtonPressed { get; set; }

        public bool IsManagerVisible { get; set; }
        public bool IsDocsVisible { get; set; }
        public bool IsPersonnelVisible { get; set; }
        #endregion
        #region Общее
        public IList<IdNameDto> RequestTypes { get; set; }
        [Display(Name = "Дата перевода")]
        public DateTime? MovementDate { get; set; }
        [Display(Name = "Дата временного перевода до")]
        public DateTime? MovementTempTo { get; set; }
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
        #region Согласования
        [Display(Name="Согласовано сотрудником")]
        public DateTime? SendDate { get; set; }
        [Display(Name = "Согласовано отпускающим руководителем")]
        public DateTime? SourceManagerAccept { get; set; }
        [Display(Name = "Согласовано принимающим руководителем")]
        public DateTime? TargetManagerAccept { get; set; }
        [Display(Name = "Согласовано кадровиком банка")]
        public DateTime? PersonnelManagerBankAccept { get; set; }
        public string PersonnelManagerBank { get; set; }
        [Display(Name = "Согласовано вышестоящим руководителем")]
        public DateTime? ChiefAccept { get; set; }
        public string Chief { get; set; }
        [Display(Name = "Согласовано кадровиком")]
        public DateTime? PersonnelManagerAccept { get; set; }
        public string PersonnelManager { get; set; }
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
        [Display(Name = "Дата окончания временной надбавки")]
        public DateTime? AdditionPositionTo {get;set;}
        [Display(Name = "Персональная надбавка")]
        public decimal AdditionPersonnel { get; set; }
        [Display(Name="Дата окончания временной надбавки")]
        public DateTime? AdditionPersonnelTo { get; set; }
        [Display(Name = "Надбавка за квалификацию")]
        public decimal AdditionQuality { get; set; }
        [Display(Name = "Дата окончания временной надбавки")]
        public DateTime? AdditionQualityTo { get; set; }
        public bool IsTempMoving { get; set; }
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
        [Display(Name = "Дата окончания временной надбавки")]
        public DateTime? AdditionFrontTo { get; set; }
        [Display(Name = "Грейд")]
        public int Grade { get; set; }
        [Display(Name = "График работы")]
        public int HoursType { get; set; }
        public IList<IdNameDto> HoursTypes { get; set; }
        [Display(Name = "Северный стаж")]
        public int NorthFactor { get; set; }
        [Display(Name="Порядок начисления надбавок")]
        public int NorthFactorOrder { get; set; }
        [Display(Name = "лет")]
        public int NorthFactorYear { get; set; }
        [Display(Name = "месяцев")]
        public int NorthFactorMonth { get; set; }
        [Display(Name = "дней")]
        public int NorthFactorDay { get; set; }
        [Display(Name="Надбавка")]
        public decimal NorthFactorAddition { get; set; }
        public IList<IdNameDto> NorthFactorOrders { get; set; }
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
        public bool MovementNoteIsRequired { get; set; }
        public int MovementNoteAttachmentId { get; set; }
        public UploadFileDto MovementNoteDto { get; set; }

        public HttpPostedFileBase AdditionalAgreementDoc { get; set; }
        public bool AdditionalAgreementDocIsRequired { get; set; }
        public int AdditionalAgreementDocAttachmentId { get; set; }
        public UploadFileDto AdditionalAgreementDocDto { get; set; }

        public HttpPostedFileBase MovementOrderDoc { get; set; }
        public bool MovementOrderDocIsRequired { get; set; }
        public int MovementOrderDocAttachmentId { get; set; }
        public UploadFileDto MovementOrderDocDto { get; set; }

        public HttpPostedFileBase MaterialLiabilityDoc { get; set; }
        public bool MaterialLiabilityDocIsRequired { get; set; }
        public int MaterialLiabilityDocAttachmentId { get; set; }
        public UploadFileDto MaterialLiabilityDocDto { get; set; }

        public HttpPostedFileBase RequirementsOrderDoc { get; set; }
        public bool RequirementsOrderDocIsRequired { get; set; }
        public int RequirementsOrderDocAttachmentId { get; set; }
        public UploadFileDto RequirementsOrderDocDto { get; set; }

        public HttpPostedFileBase ServiceOrderDoc { get; set; }
        public bool ServiceOrderDocIsRequired { get; set; }
        public int ServiceOrderDocAttachmentId { get; set; }
        public UploadFileDto ServiceOrderDocDto { get; set; }
        #endregion
    }
}
