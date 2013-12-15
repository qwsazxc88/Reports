using System.Collections.Generic;

namespace Reports.Presenters.UI.ViewModel
{
    public class GradeListViewModel
    {
        public TableDto Daily { get; set; }
        public TableDto Residence { get; set; }
        public TableDto AirTicket { get; set; }
        public TableDto TrainTicket { get; set; }
    }
    public class TableDto
    {
        public List<RowDto> rows;
    }
    public class RowDto
    {
        public List<string> Values { get; set; }
    }
}