using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Reports.Core.Dto;

namespace Reports.Presenters.UI.ViewModel
{
    public class EditMissionPbDocumentModel : IContainId
    {
        public int Version { get; set; }

        #region IContainId Members

        public int Id { get; set; }
        public int UserId { get; set; }
        #endregion

        [Display(Name = "СФ (Акт) номер")]
        [Required(ErrorMessageResourceName = "EditMissionPbDocumentModel_Number_Required", ErrorMessageResourceType = typeof(Resources))]
        [LocalizationDisplayName("EditMissionPbDocumentModel_Number_Required", typeof(Resources))]
        public string Number { get; set; }

        [Display(Name = "СФ (Акт) дата")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessageResourceName = "EditMissionPbDocumentModel_Date_Required", ErrorMessageResourceType = typeof(Resources))]
        [LocalizationDisplayName("EditMissionPbDocumentModel_Date_Required", typeof(Resources))]
        public DateTime? DocumentDate { get; set; }

        [Display(Name = "Контрагент")]
        public int ContractorId { get; set; }
        public int ContractorIdHidden { get; set; }
        public IList<IdNameDto> Contractors;
        public string ContractorAccount { get; set; }

        public bool IsEditable { get; set; }

        public bool ReloadPage { get; set; }
    }
}
