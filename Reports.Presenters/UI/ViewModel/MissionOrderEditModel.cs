using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;

namespace Reports.Presenters.UI.ViewModel
{
    public class MissionOrderEditModel : UserInfoModel, IContainId
    {
        public int Version { get; set; }

        #region IContainId Members

        public int Id { get; set; }
        public int UserId { get; set; }
        #endregion















        public string Targets { get; set; }


        public RequestCommentsModel CommentsModel { get; set; }
    }

    public class MissionOrderTargetModel
    {
        public int TargetId { get; set; }
        public int CountryId { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Organization { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public string AllDaysCount { get; set; }
        public string TargetDaysCount { get; set; }
        public int DailyAllowanceId { get; set; }
        public string DailyAllowanceName { get; set; }
        public int ResidenceId { get; set; }
        public string ResidenceName { get; set; }
        public int AirTicketTypeId { get; set; }
        public string AirTicketTypeName { get; set; }
        public int TrainTicketTypeId { get; set; }
        public string TrainTicketTypeName { get; set; }
    }
    public class JsonList
    {
        public MissionOrderTargetModel[] List { get; set; }
    }
    public class MissionOrderEditTargetModel
    {
        public int Id { get; set; }
        //public string Day { get; set; }
        //public int UserId { get; set; }

        [Display(Name = "Страна")]
        public int CountryId { get; set; }
        public List<IdNameDto> Countries { get; set; }

        [Display(Name = "Город")]
        public string City { get; set; }

        [Display(Name = "Организация")]
        public string Organization { get; set; }

        [Display(Name = "Дата начала")]
        public string BeginDate { get; set; }

        [Display(Name = "Дата окончания")]
        public string EndDate { get; set; }

        [Display(Name = "Всего календарных дней")]
        public string AllDays { get; set; }

        [Display(Name = "Дней, не считая время в пути")]
        public string RealDays { get; set; }

        [Display(Name = "Суточные")]
        public int DailyAllowanceId { get; set; }
        public List<IdNameDto> DailyAllowances { get; set; }

        [Display(Name = "Проживание")]
        public int ResidenceId { get; set; }
        public List<IdNameDto> Residences { get; set; }

        [Display(Name = "Авиа билеты")]
        public int AirTicketTypeId { get; set; }
        public List<IdNameDto> AirTicketTypes { get; set; }

        [Display(Name = "Железнодорожные билеты")]
        public int TrainTicketTypeId { get; set; }
        public List<IdNameDto> TrainTicketTypes { get; set; }
    }


}