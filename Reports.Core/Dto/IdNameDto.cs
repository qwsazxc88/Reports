using System;
using System.Collections.Generic;

namespace Reports.Core.Dto
{
    public class IdNameDto
    {
        public IdNameDto()
        {
        }

        public IdNameDto(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class IdNameReadonlyDto:IdNameDto
    {
        public bool IsReadOnly { get; set; }
    }
    public class IdNameDtoSort:IdNameDto
    {
        public int SortOrder { get; set; }
    }
    public class IdNameDtoWithDates : IdNameDto
    {
        public DateTime? DateAccept { get; set; }
        public DateTime? DateRelease { get; set; }
        public List<int> userStats { get; set; }
        public List<int> userStatsDays { get; set; }
    }
    
}