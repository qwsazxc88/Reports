using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;

namespace Reports.Presenters.UI.ViewModel
{
    public class ClearanceChecklistEditModel : UserInfoModel
    {
        public int UserId { get; set; }

        public int Id { get; set; }

        public IList<ClearanceChecklistApprovalDto> ClearanceChecklistApprovals { get; set; }

        // Props assigned by outsourcing
        // Номер реестра
        public int? RegistryNumber { get; set; }
        // Сумма НДФЛ
        public decimal? PersonalIncomeTax { get; set; }
        // ОКТМО
        public string OKTMO { get; set; }

        public bool IsBottomEnabled { get; set; }

        public DateTime EndDate { get; set; }

        public ClearanceChecklistEditModel()
        {
            this.ClearanceChecklistApprovals = new List<ClearanceChecklistApprovalDto>();
        }
    }
}
