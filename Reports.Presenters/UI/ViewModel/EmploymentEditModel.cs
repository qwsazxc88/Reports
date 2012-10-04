using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;

namespace Reports.Presenters.UI.ViewModel
{
    public class EmploymentEditModel : UserInfoModel, ICheckBoxes
    {
        public int Id { get; set; }
        public int Version { get; set; }
        public int UserId { get; set; }


        [Display(Name = "принять  на работу")]
        public int TypeId { get; set; }
        public int TypeIdHidden { get; set; }
        public IList<IdNameDto> Types;
        public bool IsTypeEditable { get; set; }

        [Display(Name = "на (ставка)")]
        public int GraphicTypeId { get; set; }
        public int GraphicTypeIdHidden { get; set; }
        public IList<IdNameDto> GraphicTypes;
      
        [Display(Name = "на  должность")]
        public int PositionId { get; set; }
        public int PositionIdHidden { get; set; }
        public IList<IdNameDto> Positions;


        [Display(Name = "Дата начала Т Д")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}",ApplyFormatInEditMode = true)]
        [Required(ErrorMessageResourceName = "EmploymentEditModel_BeginDate_Required",
         ErrorMessageResourceType = typeof(Resources))]
        [LocalizationDisplayName("EmploymentEditModel_BeginDate_Required", typeof(Resources))]
        public DateTime? BeginDate { get; set; }
        //[Display(Name = "Дата окончания Т Д")]
        //[DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        ////[Required(ErrorMessageResourceName = "EmploymentEditModel_EndDate_Required",
        ////ErrorMessageResourceType = typeof(Resources))]
        ////[LocalizationDisplayName("EmploymentEditModel_EndDate_Required", typeof(Resources))]
        //public DateTime? EndDate { get; set; }


        [Display(Name = "Оклад (тарифная ставка)")]
        [Required(ErrorMessageResourceName = "EmploymentEditModel_Salary_Required",
        ErrorMessageResourceType = typeof(Resources))]
        [LocalizationDisplayName("EmploymentEditModel_Salary_Required", typeof(Resources))]
        public string Salary { get; set; }

        [Display(Name = "Испытательный срок")]
        public string Probaion { get; set; }

        [Display(Name = "Районный коэффициент")]
        public string RegionFactor { get; set; }
        [Display(Name = "Северный коэффициент")]
        public string NorthFactor { get; set; }
         [Display(Name = "Надбавка территориальная")]
        public string RegionAddition { get; set; }
         [Display(Name = "Надбавка персональная")]
        public string PersonalAddition { get; set; }
         [Display(Name = "Надбавка за разъездной характер работы")]
        public string TravelWorkAddition { get; set; }
         [Display(Name = "Надбавка за квалификацию")]
        public string SkillAddition { get; set; }
         [Display(Name = "Надбавка за выслугу лет рабочим и служащим")]
        public string LongWorkAddition { get; set; }


        //[Display(Name = "Процент оплаты заработка")]
        //public int PaymentPercentTypeId { get; set; }
        //public int PaymentPercentTypeIdHidden { get; set; }
        //public IList<IdNameDtoSort> PaymentPercentTypes;


        [Display(Name = "Надбавка")]
        public int AdditionId { get; set; }
        public int AdditionIdHidden { get; set; }
        public IList<IdNameDto> Additions;
        //public bool IsPaymentRestrictTypeEditable { get; set; }

        [Display(Name = "Основание")]
        public string Reason { get; set; }
       
        [Display(Name = "Заполнение табеля")]
        public int TimesheetStatusId { get; set; }
        public int TimesheetStatusIdHidden { get; set; }
        public IList<IdNameDto> TimesheetStatuses;
        public bool IsTimesheetStatusEditable { get; set; }


        [Display(Name = "Согласен Сотрудник")]
        public bool IsApprovedByUser { get; set; }
        public bool IsApprovedByUserHidden { get; set; }
        public bool IsApprovedByUserEnable { get; set; }
        [Display(Name = "Согласен Руководитель")]
        public bool IsApprovedByManager { get; set; }
        public bool IsApprovedByManagerHidden { get; set; }
        public bool IsApprovedByManagerEnable { get; set; }
        [Display(Name = "Согласен Кадровик")]
        public bool IsApprovedByPersonnelManager { get; set; }
        public bool IsApprovedByPersonnelManagerHidden { get; set; }
        public bool IsApprovedByPersonnelManagerEnable { get; set; }
        [Display(Name = "Выгружен в 1с8")]
        public bool IsPostedTo1C { get; set; }
        public bool IsPostedTo1CHidden { get; set; }
        public bool IsPostedTo1CEnable { get; set; }

        [Display(Name = "Автор")]
        public string CreatorLogin { get; set; }

        //[Display(Name = "Скан паспорта")]
        //public string Attachment { get; set; }
        //public int AttachmentId { get; set; }

        //[Display(Name = "Скан пенсионного")]
        //public string PensAttachment { get; set; }
        //public int PensAttachmentId { get; set; }

        //[Display(Name = "Скан ИНН")]
        //public string InnAttachment { get; set; }
        //public int InnAttachmentId { get; set; }

        //[Display(Name = "Скан 2НДФЛ")]
        //public string NdflAttachment { get; set; }
        //public int NdflAttachmentId { get; set; }
        //public int AttachmentTypeId { get; set; }

        public bool IsSaveAvailable { get; set; }
        public bool IsDeleteAvailable { get; set; }
        public bool IsDelete { get; set; }
        public bool IsDeleted { get; set; }
        public RequestCommentsModel CommentsModel { get; set; }
        public RequestAttachmentsModel AttachmentsModel { get; set; }

        public bool ReloadPage { get; set; }
    }
}
