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
        public int UserLinkId { get; set; }
        public IList<IdNameDto> UserLinks { get; set; }
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

        public bool IsForTemporaryVacancy { get; set; }
        #endregion
        #region Общее
        public IList<IdNameDto> RequestTypes { get; set; }
        [Display(Name = "Дата перевода")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}",
               ApplyFormatInEditMode = true)]
        public DateTime? MovementDate { get; set; }
        [Display(Name = "Дата окончания временного перевода")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}",
               ApplyFormatInEditMode = true)]
        public DateTime? MovementTempTo { get; set; }
        [Display(Name = "В подразделение")]
        public string TargetDepartmentName { get; set; }
        public int TargetDepartmentId { get; set; }
        [Display(Name = "На должность")]
        public string TargetPositionName { get; set; }
        public int TargetPositionId { get; set; }
        public string SourcePositionName { get; set; }
        public string SourceDepartment { get; set; }
        public IList<IdNameDto> TargetPositions { get; set; }
        public StandartUserDto SourceManager { get; set; }
        public IList<IdNameDto> AdditionActions { get; set; }
        public StandartUserDto TargetManager { get; set; }
        #endregion
        #region Согласования
        [Display(Name="Согласовано сотрудником")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}",
               ApplyFormatInEditMode = true)]
        public DateTime? SendDate { get; set; }
        [Display(Name = "Согласовано отпускающим руководителем")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}",
               ApplyFormatInEditMode = true)]
        public DateTime? SourceManagerAccept { get; set; }
        [Display(Name = "Согласовано принимающим руководителем")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}",
               ApplyFormatInEditMode = true)]
        public DateTime? TargetManagerAccept { get; set; }
        [Display(Name = "Согласовано кадровиком банка")]
        public DateTime? PersonnelManagerBankAccept { get; set; }
        public string PersonnelManagerBank { get; set; }
        [Display(Name = "Согласовано вышестоящим руководителем")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}",
               ApplyFormatInEditMode = true)]
        public DateTime? ChiefAccept { get; set; }
        public string Chief { get; set; }
        [Display(Name = "Согласовано кадровиком")]
        public DateTime? PersonnelManagerAccept { get; set; }
        public string PersonnelManager { get; set; }
        #endregion
        #region Для Руководителя
        [Display(Name="Оклад")]
        public Decimal TargetCasing { get; set; }
        [Display(Name = "Региональный коэффициент")]
        public Decimal TargetRegion { get; set; }
        [Display(Name = "Ставка")]
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        public decimal Salary { get; set; }
        [Display(Name = "Ставка")]
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        public decimal TargetSalaryCount { get; set; }
        [Display(Name = "Вид расчёта оклада")]
        public string CasingType { get; set; }
        [Display(Name = "Оклад")]
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        public decimal Casing { get; set; }
        [Display(Name="Условия перевода")]
        public string MovementCondition { get; set; }
        [Display(Name="Задача в Pyrus")]
        public string PyrusLink { get; set; }
        [Display(Name = "Итого")]
        public decimal TotalSalary { get; set; }
        public IList<AdditionsDto> ActiveAdditions { get; set; }
        public IList<AdditionsDto> AdditionsToEdit { get; set; }
        /// <summary>
        /// Временно
        /// </summary>
        public bool IsTempMoving { get; set; }
        /// <summary>
        /// Совместительство
        /// </summary>
        public bool Conjunction { get; set; }
        #endregion
        #region Для кадровиков
        /// <summary>
        /// Вид расчета оклада для редактирования
        /// </summary>
        public bool TargetCasingType { get; set; }
        
        [Display(Name="Районный коэффициент")]
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        public decimal RegionCoefficient {get;set;}        
       
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
        [Display(Name="Действие")]
        public int NorthFactorAdditionAction { get; set; }
        public IList<IdNameDto> NorthFactorOrders { get; set; }
        public IList<IdNameDto> NorthFactors { get; set; }        
        [Display(Name = "Основание для перевода")]
        public string MovementReason { get; set; }
        [Display(Name="Группа доступа")]
        public int AccessGroup { get; set; }
        public IList<IdNameDto> AccessGroupsList { get; set; }   
    
        #endregion
        #region Files
        public HttpPostedFileBase MovementNote { get; set; }
        public bool MovementNoteIsRequired { get { return true; } set {  } }
        public int MovementNoteAttachmentId { get; set; }
        public UploadFileDto MovementNoteDto { get; set; }
        #endregion
    }
}
