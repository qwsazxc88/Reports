using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Reports.Core.Dto;

namespace Reports.Presenters.UI.ViewModel
{
    public class DeductionEditModel : DeductionUserInfoModel,IContainId
    {
        public int Id { get; set; }
        public int Version { get; set; }


        [Display(Name = "ФИО")]
        public int UserId { get; set; }
        public int UserIdHidden { get; set; }
        public IList<IdNameDto> Users;

        public bool IsEditable { get; set; }

        [Display(Name = "Вид удержания")]
        public int KindId { get; set; }
        public int KindIdHidden { get; set; }
        public IList<IdNameDto> Kindes;

        [Display(Name = "Период")]
        public int MonthId { get; set; }
        public int MonthIdHidden { get; set; }
        public IList<IdNameDto> Monthes;

        public string Sum { get; set; }

        [Display(Name = "Тип заявки")]
        public int TypeId { get; set; }
        public int TypeIdHidden { get; set; }
        public IList<IdNameDto> Types;

        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата увольнения")]
        public DateTime? DismissalDate { get; set; }

        [Display(Name = "Срочное увольнение")]
        public bool IsFastDismissal { get; set; }
        public bool IsFastDismissalHidden { get; set; }

        [Display(Name = "Автор")]
        public string Editor { get; set; }

        [Display(Name = "Состояние")]
        public string Status { get; set; }

        public string IsUserEmailMessageSend {get; set; }

        public bool IsSaveAvailable { get; set; }
        public bool IsDeleteAvailable { get; set; }
        public bool IsDelete { get; set; }
        public bool IsDeleted { get; set; }

        public bool ReloadPage { get; set; }
    }
}
