using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Reports.Core.Dto.Employment2
{
    public class DisabilityDto
    {
        [Display(Name = "Серия справки")]
        public string CertificateSeries { get; set; }

        [Display(Name = "Номер справки")]
        public string CertificateNumber { get; set; }

        [Display(Name = "Дата выдачи")]
        public DateTime DateOfIssue { get; set; }

        [Display(Name = "Группа инвалидности")]
        public string DisabilityDegree { get; set; }

        [Display(Name = "Срок действия справки")]
        public DateTime CerificateExpirationDate { get; set; }
    }
}
