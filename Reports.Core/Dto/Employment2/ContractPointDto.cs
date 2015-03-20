using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Core.Dto.Employment2
{   
    /// <summary>
    /// Список возможных вариантов некоторых пунктов в трудовом договоре
    /// </summary>
    public class ContractPointDto
    {
        public int PointId { get; set; }                //id пункта
        public int PointTypeId { get; set; }            //id типа
        public string PointTypeName { get; set; }       //название типа
        public string PointNamePart_1 { get; set; }     //первая часть текста пункта
        public string PointNamePart_2 { get; set; }     //вторая часть текста пункта
    }
}
