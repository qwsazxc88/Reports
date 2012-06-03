using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;

namespace Reports.Presenters.UI.ViewModel
{
    public class EmployeeDocumentListModel : UserModel
    {
        [Display(Name = "Документы")]
        public IList<DocumentDtoModel> Documents { get; set; }

        public bool ViewHeader { get; set; }
        public bool ShowNew { get; set; }
        public bool AddVisible { get; set; }
//        public int DocumentTypeId { get; set; }
        [Display(Name = "Тип заявки")]
        public int DocumentSubTypeId { get; set; }
        public IList<IdNameDto> DocumentTypesAndSubtypes;
        //public IList<IdNameDto> DocumentSubTypes;
        public bool TypeFilterVisible { get; set; }
    }
    public class DocumentDtoModel
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Date { get; set; }
        public bool IsApproved { get; set; }
        public int OwnerId { get; set; }
    }

}