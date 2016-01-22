using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Core.Dto
{
    public class UserPersonnelDataDto
    {
        public int UserId { get; set; }
        public int PeopleId  { get; set; }
        public string UserName { get; set; }
        public string FIO { get; set; }
        public string Position { get; set; }
        public string Dep3Name { get; set; }
        public string Dep7Name { get; set; }
        public string Snils { get; set; }
        public string INN { get; set; }
        public DateTime? DateAccept { get; set; }
        public DateTime? DateRelease { get; set; }
        public bool GPDFlag { get; set; }
        public bool UserFlag { get; set; }
        public int DepId { get; set; }
    }
}
