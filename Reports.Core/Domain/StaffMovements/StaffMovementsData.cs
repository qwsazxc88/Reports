using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Core.Domain
{
    public class StaffMovementsData: AbstractEntity
    {
        //<!--Данные оклада и регионального коэфф.-->
        /// <summary>
        /// Тип оклада
        /// </summary>
        public virtual bool CasingType { get; set; }
        /// <summary>
        /// Тип расчёта оклада
        /// </summary>
        public virtual bool TargetCasingType { get; set; }
        /// <summary>
        /// Оклад
        /// </summary>
        public virtual decimal Casing { get; set; }
        /// <summary>
        /// Оклад
        /// </summary>
        public virtual decimal Salary { get; set; }
        /// <summary>
        /// Региональный коэффициент
        /// </summary>
        public virtual decimal RegionCoefficient { get; set; }
        /// <summary>
        /// Грейд
        /// </summary>
        public virtual int Grade { get; set; }
        /// <summary>
        /// Северный стаж
        /// </summary>
        public virtual int NorthFactor { get; set; }
        public virtual int NorthFactorOrder { get; set; }
        public virtual int NorthFactorYear { get; set; }
        public virtual int NorthFactorMonth { get; set; }
        public virtual int NorthFactorDay { get; set; }
        public virtual decimal NorthFactorAddition { get; set; }
        public virtual int NorthFactorAdditionAction { get; set; }

        /// <summary>
        /// Причина перевода
        /// </summary>
        public virtual string MovementReason { get; set; }
        /// <summary>
        /// Условия перевода
        /// </summary>
        public virtual string MovementCondition { get; set; }
        /// <summary>
        /// Группа доступа
        /// </summary>
        public virtual AccessGroup AccessGroup { get; set; }
        /// <summary>
        /// Совместительство
        /// </summary>
        public virtual bool Conjunction { get; set; }
        /// <summary>
        /// График работы
        /// </summary>
        public virtual Schedule HoursType { get; set; }
        /// <summary>
        /// Pyrus
        /// </summary>
        public virtual string PyrusLink { get; set; }
    }
}
