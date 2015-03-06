using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Core.Dto
{
    public class AnalyticalStatementDetailsDto
    {
        /// <summary>
        /// Id документа
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Тип документа (Приказ на командировку, Авансовый отчёт ...)
        /// </summary>
        public int DocumentType { get; set; }
        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Дата 
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// Сальдо начальное
        /// </summary>
        public float SaldoStart { get; set; }
        /// <summary>
        /// Выдано
        /// </summary>
        public float Ordered { get; set; }
        /// <summary>
        /// Принято
        /// </summary>
        public float Reported { get; set; }
        /// <summary>
        /// Сальдо конечное
        /// </summary>
        public float SaldoEnd { get; set; }
    }
}
