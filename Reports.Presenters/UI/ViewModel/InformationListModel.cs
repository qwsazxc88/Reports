using System.Collections.Generic;

namespace Reports.Presenters.UI.ViewModel
{
    public class InformationListModel
    {
        public IList<InformationModelDto> Items { get; set; }
    }
    public class InformationModelDto
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}
