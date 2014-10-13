using System;

namespace Reports.Core.Dto
{
    public class HelpVersionDto
    {
        public int Id { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Comment { get; set; }
    }
    public class HelpFaqDto
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}