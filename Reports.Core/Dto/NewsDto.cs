using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Core.Dto
{
    public class NewsDto
    {
        public int id { get; set; }
        public string Header { get; set; }
        public string PostDate { get; set; }
        public string Text { get; set; }

    }
}
