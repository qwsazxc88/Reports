using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;

namespace Reports.Presenters.UI.ViewModel
{
    public class EditPersonnelBillingRequestViewModel : BillingRequestInfoViewModel, IContainId
    {
        public int Version { get; set; }
        #region IContainId Members

        public int Id { get; set; }
        public int UserId { get; set; }

        #endregion

        [Display(Name = "Тема")]
        public int TitleId { get; set; }
        public int TitleIdHidden { get; set; }
        public IList<IdNameDto> Titles;

        [Display(Name = "Структурное подразделение")]
        public string DepartmentName { get; set; }
        public int DepartmentId { get; set; }

        [Display(Name = "На сотрудника")]
        public string UserName { get; set; }

        [Display(Name = "Срочность")]
        public int UrgencyId { get; set; }
        public int UrgencyIdHidden { get; set; }
        public IList<IdNameDto> Urgencies;

        [Display(Name = "Получатель(и)")]
        public int RecipientId { get; set; }
        public int RecipientIdHidden { get; set; }
        public IList<IdNameDto> Recipients;

        [Display(Name = "Вопрос")]
        public string Question { get; set; }
        [Display(Name = "Ответ")]
        public string Answer { get; set; }

        [Display(Name = "Запрос прочитан")]
        public bool IsWorkBegin { get; set; }
        public bool IsWorkBeginAvailable { get; set; }
        public bool IsWorkBeginHidden { get; set; }

        public bool IsEditable { get; set; }
        public bool IsSaveAvailable { get; set; }
        public bool IsAnswerEditable { get; set; }

        public bool IsSendAvailable { get; set; }
       
        public bool ReloadPage { get; set; }

        public RequestAttachmentsModel AttachmentsModel { get; set; }
    }
    public class BillingRequestInfoViewModel : PreventDCModel
    {
        [Display(Name = "Отправитель")]
        public string CreatorName { get; set; }

        [Display(Name = "Номер документа")]
        public string DocumentNumber { get; set; }

        [Display(Name = "Дата создания")]
        public string DateCreated { get; set; }

        [Display(Name = "Стр. подразделение (3 уровень)")]
        public string Department3Name { get; set; }
       
        [Display(Name = "Дата отправки")]
        public string DateSended { get; set; }

        [Display(Name = "Получатель")]
        public string RecipientName { get; set; }

        [Display(Name = "Дата пррочтения")]
        public string DateBeginWork { get; set; }

        [Display(Name = "Дата обработки")]
        public string DateEndWork { get; set; }
    }

    public enum AllPersonnelBillingRecipientEnum
    {
        AllEstimators = -1,
        AllConsultantOutsorsingManager = -2
    }
}
