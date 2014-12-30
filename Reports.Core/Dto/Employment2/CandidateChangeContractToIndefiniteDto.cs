using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Reports.Core.Dto.Employment2
{
    public class CandidateChangeContractToIndefiniteDto
    {
        public int Id { get; set; }

        public bool IsContractChangedToIndefinite { get; set; }
    }
}
