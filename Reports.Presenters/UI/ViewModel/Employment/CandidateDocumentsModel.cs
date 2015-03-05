using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto.Employment2;
using System.Web;
using System.Web.Mvc;

namespace Reports.Presenters.UI.ViewModel.Employment2
{
    public class CandidateDocumentsModel : AbstractEmploymentModel
    {
        //состояние кандидата
        //public IList<CandidateStateDto> CandidateState { get; set; }
        public CandidateStateModel CandidateStateModel { get; set; }

        //трудовой договор
        public HttpPostedFileBase EmploymentContractFile { get; set; }
        public string EmploymentContractFileName { get; set; }
        public int EmploymentContractFileId { get; set; }

        //приказ о приеме
        public HttpPostedFileBase OrderOnReceptionFile { get; set; }
        public string OrderOnReceptionFileName { get; set; }
        public int OrderOnReceptionFileId { get; set; }

        //Т2
        public HttpPostedFileBase T2File { get; set; }
        public string T2FileName { get; set; }
        public int T2FileId { get; set; }

        //договор о мат. ответственности
        public HttpPostedFileBase ContractMatResponsibleFile { get; set; }
        public string ContractMatResponsibleFileName { get; set; }
        public int ContractMatResponsibleFileId { get; set; }

        //ДС персональные данные
        public HttpPostedFileBase PersonalDataFile { get; set; }
        public string PersonalDataFileName { get; set; }
        public int PersonalDataFileId { get; set; }

        //обязательство конфиденциальность
        public HttpPostedFileBase DataObligationFile { get; set; }
        public string DataObligationFileName { get; set; }
        public int DataObligationFileId { get; set; }

        //личный листок по учету кадров
        public HttpPostedFileBase EmploymentFile { get; set; }
        public string EmploymentFileName { get; set; }
        public int EmploymentFileId { get; set; }

        //реестр личного дела
        public HttpPostedFileBase RegisterPersonalRecordFile { get; set; }
        public string RegisterPersonalRecordFileName { get; set; }
        public int RegisterPersonalRecordFileId { get; set; }

        //для удаления сканов
        public int DeleteAttachmentId { get; set; }
    }
}
