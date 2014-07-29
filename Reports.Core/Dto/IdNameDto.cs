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
        public List<decimal> userStats { get; set; }
        public List<int> userStatsDays { get; set; }
    }
    public class IdNameAddressDto : IdNameDto
    {
        public string Address { get; set; }
    }
    public class IdContextDto
    {
        public int Id { get; set; }
        public byte[] Context { get; set; }
    }
    
}