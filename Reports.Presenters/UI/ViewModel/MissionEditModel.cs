using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;

namespace Reports.Presenters.UI.ViewModel
{
    public class MissionEditModel : UserInfoModel, ICheckBoxes
    {
        public int Id { get; set; }
        public int Version { get; set; }
        public int UserId { get; set; }


        [Display(Name = "Вид командировки")]
        public int TypeId { get; set; }
        public int TypeIdHidden { get; set; }
        public IList<IdNameDto> Types;
        public bool IsTypeEditable { get; set; }

        [Display(Name = "Дата начала")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}",ApplyFormatInEditMode = true)]
        [Required(ErrorMessageResourceName = "MissionEditModel_BeginDate_Required",
         ErrorMessageResourceType = typeof(Resources))]
        [LocalizationDisplayName("MissionEditModel_BeginDate_Required", typeof(Resources))]
        public DateTime? BeginDate { get; set; }

        [Display(Name = "Дата окончания")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessageResourceName = "MissionEditModel_EndDate_Required",
        ErrorMessageResourceType = typeof(Resources))]
        [LocalizationDisplayName("MissionEditModel_EndDate_Required", typeof(Resources))]
        public DateTime? EndDate { get; set; }
        
         [Display(Name = "Страна, город командировки")]
        [Required(ErrorMessageResourceName = "MissionEditModel_Country_Required",
        ErrorMessageResourceType = typeof(Resources))]
        [LocalizationDisplayName("MissionEditModel_Country_Required", typeof(Resources))]
        public string Country { get; set; }

        [Display(Name = "Организация, куда командировка")]
        [Required(ErrorMessageResourceName = "MissionEditModel_Organization_Required",
        ErrorMessageResourceType = typeof(Resources))]
        [LocalizationDisplayName("MissionEditModel_Organization_Required", typeof(Resources))]
        public string MissionOrganization { get; set; }

         [Display(Name = "Цель командировки")]
        [Required(ErrorMessageResourceName = "MissionEditModel_Goal_Required",
         ErrorMessageResourceType = typeof(Resources))]
        [LocalizationDisplayName("MissionEditModel_Goal_Required", typeof(Resources))]
        public string Goal { get; set; }

         [Display(Name = "Источник финансирования командировки")]
        [Required(ErrorMessageResourceName = "MissionEditModel_FinancesSource_Required",
         ErrorMessageResourceType = typeof(Resources))]
        [LocalizationDisplayName("MissionEditModel_FinancesSource_Required", typeof(Resources))]
        public string FinancesSource { get; set; }

         [Display(Name = "Основание командировки")]
        //[Required(ErrorMessageResourceName = "MissionEditModel_Reason_Required",
        //ErrorMessageResourceType = typeof(Resources))]
        //[LocalizationDisplayName("MissionEditModel_Reason_Required", typeof(Resources))]
        public string Reason { get; set; }
        public bool IsReasonEditable { get; set; }

        [Display(Name = "Количество календарных дней")]
        public int? DaysCount { get; set; }
        public int? DaysCountHidden { get; set; }

        [Display(Name = "Заполнение табеля")]
        public int TimesheetStatusId { get; set; }
        public int TimesheetStatusIdHidden { get; set; }
        public IList<IdNameDto> TimesheetStatuses;
        public bool IsTimesheetStatusEditable { get; set; }

        [Display(Name = "Согласен Сотрудник")]
        public bool IsApprovedByUser { get; set; }
        public bool IsApprovedByUserHidden { get; set; }
        public bool IsApprovedByUserEnable { get; set; }
        //public bool IsApprovedByUserChecked { get; set; }
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

        public bool IsSaveAvailable { get; set; }
        public bool IsDeleteAvailable { get; set; }
        public bool IsDelete { get; set; }
        public bool IsDeleted { get; set; }
        public RequestCommentsModel CommentsModel { get; set; }

        public bool ReloadPage { get; set; }

        public bool IsPrintOrderAvailable { get; set; }
        public bool IsPrintCertificateAvailable { get; set; }

        public bool IsApproved { get; set; }
        public bool IsApprovedEnable { get; set; }
    }
}
