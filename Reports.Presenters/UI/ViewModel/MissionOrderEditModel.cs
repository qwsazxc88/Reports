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
        public string BeginDate { get; set; }

        [Display(Name = "Дата окончания командировки")]
        public string EndDate { get; set; }

        [Display(Name = "Цель командировки")]
        public int GoalId { get; set; }
        public int GoalIdHidden { get; set; }
        public IList<IdNameDto> Goals;

        [Display(Name = "Тип командировки")]
        public int TypeId { get; set; }
        public int TypeIdHidden { get; set; }
        public IList<IdNameDto> Types;

        [Display(Name = "Грейд G (1,2,3,4)")]
        public string Grade { get; set; }
       

        public RequestCommentsModel CommentsModel { get; set; }

        public bool IsEditable { get; set; }
        public bool IsSaveAvailable { get; set; }
        public bool IsDeleteAvailable { get; set; }
        public bool IsDelete { get; set; }
        public bool IsDeleted { get; set; }
       
    }
}