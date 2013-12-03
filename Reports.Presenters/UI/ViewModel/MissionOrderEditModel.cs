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

        [Display(Name = "Дата начала командировки")]
        public string BeginMissionDate { get; set; }

        [Display(Name = "Дата окончания командировки")]
        public string EndMissionDate { get; set; }

        [Display(Name = "Цель командировки")]
        public int GoalId { get; set; }
        public int GoalIdHidden { get; set; }
        public IList<IdNameDto> Goals;

        [Display(Name = "Тип командировки")]
        public int TypeId { get; set; }
        public int TypeIdHidden { get; set; }
        public IList<IdNameDto> Types;

        public string DailyAllowanceGrades { get; set; }
        public string ResidenceGrades { get; set; }
        public string AirTicketTypeGrades { get; set; }
        public string TrainTicketTypeGrades { get; set; }
      

        public string Targets { get; set; }

        [Display(Name = "Грейд G (1,2,3,4)")]
        public string Grade { get; set; }

        public decimal AllSumDaily { get; set; }
        public string  UserAllSumDaily { get; set; }
        public decimal AllSumResidence { get; set; }
        public string  UserAllSumResidence { get; set; }
        public decimal AllSumAir { get; set; }
        public string  UserAllSumAir { get; set; }
        public decimal AllSumTrain { get; set; }
        public string  UserAllSumTrain { get; set; }
        public decimal AllSum { get; set; }
        public decimal UserAllSum { get; set; }

        [Display(Name = "Сумма наличных в подотчет")]
        public string UserSumCash { get; set; }
        [Display(Name = "Сумма безналичных в подотчет")]
        public string UserSumNotCash { get; set; }

        [Display(Name = "Настоящим подтверждаю свое согласие на удержание из заработной платы неизрасходованных подотчетных сумм. С условиями настоящего Приказа ознакомлен (а)")]
        public bool IsUserApproved { get; set; }
        public bool IsUserApprovedAvailable { get; set; }
        public bool IsUserApprovedHidden { get; set; }

        [Display(Name = "Командировка приходится на выходные/праздничные дни или  её продолжительность более семи дней")]
        public bool IsChiefApproveNeed { get; set; }
        public bool IsChiefApproveNeedHidden { get; set; }

        public bool? IsManagerApproved { get; set; }
        public bool IsManagerApproveAvailable { get; set; }
        public bool? IsManagerApprovedHidden { get; set; }

        public bool? IsChiefApproved { get; set; }
        public bool IsChiefApproveAvailable { get; set; }
        public bool? IsChiefApprovedHidden { get; set; }
        //public bool IsManagerNotApproved { get; set; }

        public RequestCommentsModel CommentsModel { get; set; }

        public bool IsEditable { get; set; }
       
        public bool IsSaveAvailable { get; set; }
        public bool IsDeleteAvailable { get; set; }
        public bool IsDelete { get; set; }
        public bool IsDeleted { get; set; }

        public bool ReloadPage { get; set; }
       
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
        public int TargetId { get; set; }
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