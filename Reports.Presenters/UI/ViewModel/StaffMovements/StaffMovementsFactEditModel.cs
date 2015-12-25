using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;
using System.Web;
namespace Reports.Presenters.UI.ViewModel
{
    public class StaffMovementsFactEditModel : StandartEditModel
    {
        public StaffMovementsFactEditModel()
        {
            this.User = new StandartUserDto();
            this.StatusId = 1;
        }
        public IList<AdditionsDto> ActiveAdditions { get; set; }
        public bool IsAgreementAvailable { get; set; }
        public bool IsAgreementAdditionAvailable { get; set; }
        public bool IsOrderAvailable { get; set; }
        public decimal RegionCoefficient {get;set;}
        public decimal Salary {get;set;}
        public decimal Casing { get; set; }
        public bool IsDocsAddAvailable { get; set; }
        public UploadFileDto MaterialLiabilityDocDto { get; set; }
        public UploadFileDto RequirementsOrderDocDto { get; set; }
        public UploadFileDto ServiceOrderDocDto { get; set; }
        public UploadFileDto AgreementDocDto { get; set; }
        public UploadFileDto AgreementAdditionalDocDto { get; set; }
        public UploadFileDto OrderDocDto { get; set; }
        public int MaterialLiabilityDocAttachmentId { get; set; }
        public int ServiceOrderDocAttachmentId { get; set; }
        public int RequirementsOrderDocAttachmentId { get; set; }
        public int AgreementDocId { get; set; }
        public int AgreementAdditionalDocId { get; set; }
        public int OrderDocId { get; set; }
        public bool IsDocsReceived { get; set; }
        public bool IsCheckByPersonelAvailable { get; set; }
        public bool IsSaveAvailable { get; set; }
        public int SignerId { get; set; }
        public List<IdNameDto> Signers { get; set; }
        public decimal TotalSalary { get; set; }
    }
}
