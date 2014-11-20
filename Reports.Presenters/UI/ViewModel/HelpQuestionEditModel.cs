using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;

namespace Reports.Presenters.UI.ViewModel
{
    public class HelpQuestionEditModel :HelpUserInfoModel, IContainId
    {
        public int Version { get; set; }

        #region IContainId Members

        public int Id { get; set; }
        public int UserId { get; set; }

        #endregion

        [Display(Name = "Вид вопроса")]
        public int TypeId { get; set; }
        public int TypeIdHidden { get; set; }
        public IList<IdNameDto> Types;

        [Display(Name = "Тип вопроса")]
        public int SubtypeId { get; set; }
        public int SubtypeIdHidden { get; set; }
        public IList<IdNameDto> Subtypes;

        [Display(Name = "Вопрос")]
        public string Question { get; set; }
        [Display(Name = "Ответ")]
        public string Answer { get; set; }
        
        public bool IsTypeEditable { get; set; }
        public bool IsQuestionEditable { get; set; }
        public bool IsAnswerEditable { get; set; }

        public bool IsSaveAvailable { get; set; }
        public bool IsSendAvailable { get; set; }
        public bool IsEndAvailable { get; set; }
        public bool IsBeginWorkAvailable { get; set; }
        //public bool IsConsultantEditable { get; set; }
        public bool IsEndWorkAvailable { get; set; }
        public bool IsRedirectAvailable { get; set; }

        public int Operation { get; set; }
        public int RedirectRoleId { get; set; }

        public bool ReloadPage { get; set; }


    }
}