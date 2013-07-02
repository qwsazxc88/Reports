using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;

namespace Reports.Presenters.UI.ViewModel
{
    public class AbsenceEditModel : UserInfoModel, ICheckBoxes,ICheckForEntity
    {
        public int Id { get; set; }
        public int Version { get; set; }
        public int UserId { get; set; }


        [Display(Name = "Вид невыхода")]
        public int AbsenceTypeId { get; set; }
        public int AbsenceTypeIdHidden { get; set; }
        public IList<IdNameDto> AbsenceTypes;
        public bool IsAbsenceTypeEditable { get; set; }
        [Display(Name = "Дата начала")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}",ApplyFormatInEditMode = true)]
        [Required(ErrorMessageResourceName = "AbsenceEditModel_BeginDate_Required",
         ErrorMessageResourceType = typeof(Resources))]
        [LocalizationDisplayName("AbsenceEditModel_BeginDate_Required", typeof(Resources))]
        //[DataType("DateTimeDto")]
        public DateTime? BeginDate { get; set; }
        [Display(Name = "Дата окончания")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessageResourceName = "AbsenceEditModel_EndDate_Required",
        ErrorMessageResourceType = typeof(Resources))]
        [LocalizationDisplayName("AbsenceEditModel_EndDate_Required", typeof(Resources))]
        public DateTime? EndDate { get; set; }
        //[Required(ErrorMessageResourceName = "AbsenceEditModel_DaysCount_Required",
        //ErrorMessageResourceType = typeof(Resources))]
        //[LocalizationDisplayName("AbsenceEditModel_DaysCount_Required", typeof(Resources))]
        [Display(Name = "Количество календарных дней")]
        public int? DaysCount { get; set; }
        public int? DaysCountHidden { get; set; }

        [Display(Name = "Заполнение табеля")]
        public int TimesheetStatusId { get; set; }
        public int TimesheetStatusIdHidden { get; set; }
        public IList<IdNameDto> TimesheetStatuses;
        public bool IsTimesheetStatusEditable { get; set; }

        [Display(Name = "Сотрудник cогласен")]
        public bool IsApprovedByUser { get; set; }
        public bool IsApprovedByUserHidden { get; set; }
        public bool IsApprovedByUserEnable { get; set; }
        //public bool IsApprovedByUserChecked { get; set; }
        [Display(Name = "Руководитель cогласен")]
        public bool IsApprovedByManager { get; set; }
        public bool IsApprovedByManagerHidden { get; set; }
        public bool IsApprovedByManagerEnable { get; set; }
        [Display(Name = "Кадровик cогласен")]
        public bool IsApprovedByPersonnelManager { get; set; }
        public bool IsApprovedByPersonnelManagerHidden { get; set; }
        public bool IsApprovedByPersonnelManagerEnable { get; set; }
        [Display(Name = "Выгружен в 1С")]
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

        public bool IsApproved { get; set; }
        public bool IsApprovedEnable { get; set; }
    }
}
