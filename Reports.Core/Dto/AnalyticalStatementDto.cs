using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Core.Dto
{
    /// <summary>
    /// Аналитическая ведомость
    /// </summary>
    public class AnalyticalStatementDto
    {
        /// <summary>
        /// Id пользователя
        /// </summary>
        public int userId { get; set; }
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Должность
        /// </summary>
        public string Position { get; set; }
        /// <summary>
        /// Подразделение 3 уровня
        /// </summary>
        public string Dep3Name { get; set; }
        /// <summary>
        /// Подразделение 7 уровня
        /// </summary>
        public string Dep7Name { get; set; }
        /// <summary>
        /// Остаток на начало периода
        /// </summary>
        public float SaldoBegin { get; set; }
        /// <summary>
        /// Сумма, выданная в подотчет
        /// </summary>
        public float Ordered { get; set; }
        /// <summary>
        /// Сумма, принятая бухгалтерией
        /// </summary>
        public float ReportedBefore { get; set; }
        /// <summary>
        /// Сумма, выданная в подотчет до
        /// </summary>
        public float OrderedBefore { get; set; }
        /// <summary>
        /// Сумма, принятая бухгалтерией  до
        /// </summary>
        public float Reported { get; set; }
        /// <summary>
        /// Остаток конечный
        /// </summary>
        public float SaldoEnd { get; set; }
        /// <summary>
        /// Сумма оплаченная организацией
        /// </summary>
        public float PurchaseBookAllSum { get; set; }
            
    }
}
