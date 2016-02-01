using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Core.Dto
{
    public class UserDocsDto
    {
        public int Id { get; set; }
        public string ShortLink { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public int RoleId { get; set; }
    }
}
