using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;

namespace Reports.Presenters.UI.ViewModel
{
    public class EditMissionPbDocumentModel : IContainId
    {
        public IList<IdNameDto> Contractors;
        public int Version { get; set; }

        [Display(Name = "Сч/ф номер")]
        public string CfNumber { get; set; }

        [Display(Name = "Сч/ф дата")]
        //[DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
            public string CfDate { get; set; }

        [Display(Name = "Акт номер")]
        public string Number { get; set; }

        [Display(Name = "Акт дата")]
        //[DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
            public string DocumentDate { get; set; }

        [Display(Name = "Контрагент")]
        public int ContractorId { get; set; }

        public int ContractorIdHidden { get; set; }
        public string ContractorAccount { get; set; }

        public bool IsEditable { get; set; }

        public bool ReloadPage { get; set; }

        public EditMissionPbRecordsModel RecordsModel { get; set; }

        #region IContainId Members

        public int Id { get; set; }
        public int UserId { get; set; }

        #endregion
    }

    public class EditMissionPbRecordsModel
    {
        public int DocumentId { get; set; }
        public decimal Sum { get; set; }
        public decimal SumNds { get; set; }
        public decimal AllSum { get; set; }
        public List<MissionPurchaseBookRecordDto> Records { get; set; }
    }
}