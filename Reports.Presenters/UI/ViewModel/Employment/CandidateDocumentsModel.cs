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

        //заявление о приеме
        public HttpPostedFileBase ApplicationLetterScanFile { get; set; }
        public string ApplicationLetterScanAttachmentFilename { get; set; }
        public int ApplicationLetterScanAttachmentId { get; set; }

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

        //памятка сотруднику о сохранении коммерческой, банковской и служебной тайны
        public HttpPostedFileBase InstructionOfSecretFile { get; set; }
        public string InstructionOfSecretFileName { get; set; }
        public int InstructionOfSecretFileId { get; set; }

        //Инструкция по обеспечению сохранности сведений, составляющих коммерческую, и служебную тайну
        public HttpPostedFileBase InstructionEnsuringSafetyFile { get; set; }
        public string InstructionEnsuringSafetyFileName { get; set; }
        public int InstructionEnsuringSafetyFileId { get; set; }

        //Согласие физического лица на проверку персональных данных (Приложение №3)
        public HttpPostedFileBase AgreePersonForCheckingFile { get; set; }
        public string AgreePersonForCheckingFileName { get; set; }
        public int AgreePersonForCheckingFileId { get; set; }

        //Порядок по исполнению требований при организации кассовой работы сотрудниками ВСП (Приложение 1)
        public HttpPostedFileBase CashWorkAddition1File { get; set; }
        public string CashWorkAddition1FileName { get; set; }
        public int CashWorkAddition1FileId { get; set; }

        //Порядок по обслуживанию клиентов в кассе сотрудниками ВСП (Приложение 2)
        public HttpPostedFileBase CashWorkAddition2File { get; set; }
        public string CashWorkAddition2FileName { get; set; }
        public int CashWorkAddition2FileId { get; set; }

        //Обязательство о неразглашении коммерческой и служебной тайны
        public HttpPostedFileBase ObligationTradeSecretFile { get; set; }
        public string ObligationTradeSecretFileName { get; set; }
        public int ObligationTradeSecretFileId { get; set; }

        //для удаления сканов
        public int DeleteAttachmentId { get; set; }
    }
}
