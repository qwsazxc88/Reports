using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;
using System;
using Reports.Core.Domain;
namespace Reports.Presenters.UI.ViewModel
{
    public class HelpServiceRequestEditModel : HelpUserInfoModel, IContainId, IHelpServiceDictionariesStates
    {
        #region IContainId Members
        public int Id { get; set; }
        public int UserId { get; set; }
        #endregion
        public int Version { get; set; }

        [Display(Name = "Вид услуги")]
        public int TypeId { get; set; }
        public int TypeIdHidden { get; set; }
        public IList<IdNameDto> Types;

        [Display(Name = "Скан образца")]
        public string Attachment { get; set; }
        public int AttachmentId { get; set; }
        public bool IsAttachmentVisible { get; set; }

        [Display(Name = "Требования к документам")]
        public string Requirements { get; set; }
        public bool IsRequirementsVisible { get; set; }

        [Display(Name = "Период")]
        public int? PeriodId { get; set; }
        public int? PeriodIdHidden { get; set; }
        public IList<IdNameDto> Periods { get; set; }
        public bool IsPeriodVisible { get; set; }

        [Display(Name = "Срок изготовления")]
        public int ProductionTimeTypeId { get; set; }
        public int ProductionTimeTypeIdHidden { get; set; }
        public IList<IdNameDto> ProductionTimeTypes;

        [Display(Name = "Способ передачи")]
        public int TransferMethodTypeId { get; set; }
        public int TransferMethodTypeIdHidden { get; set; }
        public IList<IdNameDto> TransferMethodTypes;

        [Display(Name = "Скан")]
        public string ServiceAttachment { get; set; }
        public int ServiceAttachmentId { get; set; }
        //public bool IsServiceAttachmentVisible { get; set; }

        public CommentsModel CommentsModel { get; set; }

        public bool IsSaveAvailable { get; set; }
        public bool IsEditable { get; set; }
        public bool IsSendAvailable { get; set; }
        public bool IsEndAvailable { get; set; }
        public bool IsBeginWorkAvailable { get; set; }
        public bool IsConsultantOutsourcingEditable { get; set; }
        public bool IsEndWorkAvailable { get; set; }
        public bool IsNotEndWorkAvailable { get; set; }

        public int Operation { get; set; }

        public bool ReloadPage { get; set; }

        public string Address { get; set; }

        #region Поля для уволенных сотрудников
        [Display(Name = "ФИО уволенного сотрудника")]
        public string FiredUserName { get; set; }
        public string FiredUserSurname { get; set; }
        public string FiredUserPatronymic { get; set; }

        [Display(Name = "Дата рождения")]
        public string UserBirthDate { get; set; }

        [Display(Name = "Примечание")]
        public int Note { get; set; }
        public IList<NoteTypeDto> NoteList { get; set; }
        #endregion
    }
}
