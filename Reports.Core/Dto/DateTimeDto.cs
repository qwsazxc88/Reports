using System;

namespace Reports.Core.Dto
{
    public class DateTimeDto
    {
        public DateTimeDto()
        {
        }

        public DateTimeDto(DateTime? date)
        {
            Date = date;
        }

        public DateTimeDto(DateTime? date, bool isReadOnly)
        {
            Date = date;
            IsReadOnly = isReadOnly;
        }

        public DateTime? Date { get; set; }
        public bool IsReadOnly { get; set; }
    }
}