using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;

namespace Reports.Presenters.UI.ViewModel
{
    public class HolidayWorkEditModel : UserInfoModel, ICheckBoxes
    {
        public int Id { get; set; }
        public int Version { get; set; }
        public int UserId { get; set; }


        [Display(Name = @"Вид оплаты\доплаты")]
        public int TypeId { get; set; }
        public int TypeIdHidden { get; set; }
        public IList<IdNameDto> Types;
        public bool IsTypeEditable { get; set; }

        [Display(Name = "Работа в выходной /праздничный день")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}",ApplyFormatInEditMode = true)]
        [Required(ErrorMessageResourceName = "HolidayWorkEditModel_Date_Required",ErrorMessageResourceType = typeof(Resources))]
        [LocalizationDisplayName("HolidayWorkEditModel_Date_Required", typeof(Resources))]
        public DateTime? Date { get; set; }
       
        //[Display(Name = "Часовая тарифная ставка")]
        //[Required(ErrorMessageResourceName = "HolidayWorkEditModel_Rate_Required", ErrorMessageResourceType = typeof(Resources))]
        //[LocalizationDisplayName("HolidayWorkEditModel_Rate_Required", typeof(Resources))]
        //public string Rate { get; set; }
        [Display(Name = "Кол-во отработанных часов")]
        [Required(ErrorMessageResourceName = "HolidayWorkEditModel_Hours_Required", ErrorMessageResourceType = typeof(Resources))]
        [LocalizationDisplayName("HolidayWorkEditModel_Hours_Required", typeof(Resources))]
        public string Hours { get; set; }

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

        public bool IsSaveAvailable { get; set; }
        public bool IsDeleteAvailable { get; set; }
        public bool IsDelete { get; set; }
        public bool IsDeleted { get; set; }
        public RequestCommentsModel CommentsModel { get; set; }

        public bool ReloadPage { get; set; }
    }
}
