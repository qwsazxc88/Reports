using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;

namespace Reports.Presenters.UI.ViewModel
{
    public class DocumentSubtypeListModel
    {
        public IList<IdNameDto> DocumentTypes;

        [Display(Name = "Тип заявки")]
        public int DocumentTypeId { get; set; }

        public IList<IdNameDto> Subtypes { get; set; }
    }
}