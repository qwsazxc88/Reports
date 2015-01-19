using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;

namespace Reports.Presenters.UI.ViewModel
{
    /// <summary>
    /// Модель для редактирования актов ГПД.
    /// </summary>
    public class GpdActEditModel
    {
        public int Id { get; set; }

        [Display(Name = "Дата акта")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime? ActDate { get; set; }

        [Display(Name = "№ акта")]
        public string ActNumber { get; set; }

        [Display(Name = "ФИО")]
        public string Surname { get; set; }

        [Display(Name = "Наименование договора")]
        public string NameContract { get; set; }

        [Display(Name = "Номер и срок действия договора")]
        public string NumContract { get; set; }

        [Display(Name = "Подразделение 3 ур.")]
        public string DepLevel3Name { get; set; }

        [Display(Name = "Месяц начисления")]
        [DisplayFormat(DataFormatString = "{0:MM.yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime? ChargingDate { get; set; }

        [Display(Name = "Оплата за период с ")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime? DateBegin { get; set; }

        [Display(Name = " по ")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime? DateEnd { get; set; }

        [Display(Name = "Сумма начисленная")]
        [DisplayFormat(DataFormatString = "{0:### ### ### ###.##}", ApplyFormatInEditMode = true)]
        public System.Decimal Amount { get; set; }

        [Display(Name = "Сумма к выплате")]
        [DisplayFormat(DataFormatString = "{0:### ### ### ###.##}", ApplyFormatInEditMode = true)]
        public decimal AmountPayment { get; set; }

        [Display(Name = "Дата платежного поручения")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime? POrderDate { get; set; }

        [Display(Name = "Назначение платежа")]
        public string PurposePayment { get; set; }

        [Display(Name = "№ заявки в ЭССС")]
        public string ESSSNum { get; set; }

        [Display(Name = "Автор")]
        public string Autor { get; set; }

        [Display(Name = "Состояние")]
        public string StatusName { get; set; }
        public int StatusID { get; set; }

        [Display(Name = "Ошибка")]
        public string errorMessage { get; set; }
        public bool hasErrors { get; set; }
        public System.DateTime? ContractBeginDate { get; set; }
        public System.DateTime? ContractEndDate { get; set; }
        public int GCID { get; set; }
        public int CreatorID { get; set; }
        public bool IsCancel { get; set; }

        public IList<GpdActCommentDto> Comments { get; set; }
        public string CommentStr { get; set; }

        //права
        public IList<GpdPermissionDto> Permissions { get; set; }
    }
}
