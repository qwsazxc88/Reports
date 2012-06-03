using System.Collections.Generic;
using Reports.Core.Dto;

namespace Reports.Presenters.UI.ViewModel
{
    public class DocumentTypeListModel
    {
        public IList<IdNameDto> Types { get; set; }
    }
}