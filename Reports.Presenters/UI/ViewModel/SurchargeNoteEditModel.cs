﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reports.Core.Dto;
using System.Web;
namespace Reports.Presenters.UI.ViewModel
{
    public class SurchargeNoteEditModel: SurchargeNoteDto
    {
        public HttpPostedFileBase File { get; set; }
        public IList<IdNameDto> Users { get; set; }
        public IList<IdNameDto> PersonnelsApproved { get; set; }
        public IList<IdNameDto> PayTypes { get; set; }
        public IList<IdNameDto> MonthTypes { get; set; }
        public bool IsEditable { get; set; }
        public bool IsDelete { get; set; }
        public bool CountantAccept { get; set; }
        public bool PersonnelManagerBankAccept { get; set; }
        public bool PersonnelAccept { get; set; }
        public int DepartmentRequiredLevel {get;set;}
        public string Chiefs { get; set; }
        public string Personnels { get; set; }
    }
}
