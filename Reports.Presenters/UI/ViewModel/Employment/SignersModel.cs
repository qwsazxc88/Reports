﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto.Employment2;

namespace Reports.Presenters.UI.ViewModel.Employment2
{
    public class SignersModel
    {
        public IList<SignerDto> Signers { get; set; }
        public SignerDto SignerToAddOrEdit { get; set; }
        
        public SignersModel()
        {
            Signers = new List<SignerDto>();
        }
    }
}
