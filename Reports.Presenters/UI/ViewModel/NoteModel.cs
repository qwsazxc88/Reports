using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Presenters.UI.ViewModel
{
    public class NoteModel
    {
        public string To { get; set; }
        public string From { get; set; }
        public string Theme { get; set; }
        public DateTime Date { get; set; }
        public string Reason { get; set; }
        public IList<string> Departments { get; set; }
        public string Position { get; set; }
        public int PositionsCount { get; set; }
        public decimal Salary { get; set; }
        public decimal Premium { get; set; }
    }
}
