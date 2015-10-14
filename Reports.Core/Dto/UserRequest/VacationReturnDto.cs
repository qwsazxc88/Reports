using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Core.Dto
{
    public class VacationReturnDto
    {
        public int NPP { get; set; }
        public int Id { get; set; }
        public string Type { get; set; }
        public string UserName { get; set; }
        public string Position { get; set; }
        public string Dep3Name { get; set; }
        public string Dep7Name { get; set; }
        public string Manager { get; set; }
        public DateTime CreateDate { get; set; }
        public string Status { get; set; }
        public string ReturnType { get; set; }
        public string Vacation { get; set; }
        public DateTime ReturnDate { get; set; }
        public DateTime ContinueDate { get; set; }
    }
}
