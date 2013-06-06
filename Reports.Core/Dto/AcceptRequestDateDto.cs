using System;

namespace Reports.Core.Dto
{
    public class AcceptRequestDateDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public DateTime DateAccept { get; set; }
        //public DateTime DateCreate { get; set; }
    }
}