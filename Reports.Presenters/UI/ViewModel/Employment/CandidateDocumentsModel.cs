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

        public DateTime? SendTo1C { get; set; }

        //заявление о приеме
        public HttpPostedFileBase ApplicationLetterScanFile { get; set; }
        public string ApplicationLetterScanAttachmentFilename { get; set; }
        public int ApplicationLetterScanAttachmentId { get; set; }
        public bool ApplicationLetterScanFileNeeded { get; set; }//требуется скан

        //трудовой договор
        public HttpPostedFileBase EmploymentContractFile { get; set; }
        public string EmploymentContractFileName { get; set; }
        public int EmploymentContractFileId { get; set; }
        public bool EmploymentContractFileNeeded { get; set; }//требуется скан

        //приказ о приеме
        public HttpPostedFileBase OrderOnReceptionFile { get; set; }
        public string OrderOnReceptionFileName { get; set; }
        public int OrderOnReceptionFileId { get; set; }
        public bool OrderOnReceptionFileNeeded { get; set; }//требуется скан

        //Т2
        public HttpPostedFileBase T2File { get; set; }
        public string T2FileName { get; set; }
        public int T2FileId { get; set; }
        public bool T2FileNeeded { get; set; }//требуется скан

        //договор о мат. ответственности
        public HttpPostedFileBase ContractMatResponsibleFile { get; set; }
        public string ContractMatResponsibleFileName { get; set; }
        public int ContractMatResponsibleFileId { get; set; }
        public bool ContractMatResponsibleFileNeeded { get; set; }//требуется скан

        //ДС персональные данные
        public HttpPostedFileBase PersonalDataFile { get; set; }
        public string PersonalDataFileName { get; set; }
        public int PersonalDataFileId { get; set; }
        public bool PersonalDataFileNeeded { get; set; }//требуется скан

        //обязательство конфиденциальность
        public HttpPostedFileBase DataObligationFile { get; set; }
        public string DataObligationFileName { get; set; }
        public int DataObligationFileId { get; set; }
        public bool DataObligationFileNeeded { get; set; }//требуется скан

        //личный листок по учету кадров
        public HttpPostedFileBase EmploymentFile { get; set; }
        public string EmploymentFileName { get; set; }
        public int EmploymentFileId { get; set; }
        public bool EmploymentFileNeeded { get; set; }//требуется скан

        //реестр личного дела
        public HttpPostedFileBase RegisterPersonalRecordFile { get; set; }
        public string RegisterPersonalRecordFileName { get; set; }
        public int RegisterPersonalRecordFileId { get; set; }
        public bool RegisterPersonalRecordFileNeeded { get; set; }//требуется скан

        //памятка сотруднику о сохранении коммерческой, банковской и служебной тайны
        public HttpPostedFileBase InstructionOfSecretFile { get; set; }
        public string InstructionOfSecretFileName { get; set; }
        public int InstructionOfSecretFileId { get; set; }
        public bool InstructionOfSecretFileNeeded { get; set; }//требуется скан

        //Инструкция по обеспечению сохранности сведений, составляющих коммерческую, и служебную тайну
        public HttpPostedFileBase InstructionEnsuringSafetyFile { get; set; }
        public string InstructionEnsuringSafetyFileName { get; set; }
        public int InstructionEnsuringSafetyFileId { get; set; }
        public bool InstructionEnsuringSafetyFileNeeded { get; set; }//требуется скан

        //Согласие физического лица на проверку персональных данных (Приложение №3)
        public HttpPostedFileBase AgreePersonForCheckingFile { get; set; }
        public string AgreePersonForCheckingFileName { get; set; }
        public int AgreePersonForCheckingFileId { get; set; }
        public bool AgreePersonForCheckingFileNeeded { get; set; }//требуется скан

        //Порядок по исполнению требований при организации кассовой работы сотрудниками ВСП (Приложение 1)
        public HttpPostedFileBase CashWorkAddition1File { get; set; }
        public string CashWorkAddition1FileName { get; set; }
        public int CashWorkAddition1FileId { get; set; }
        public bool CashWorkAddition1FileNeeded { get; set; }//требуется скан

        //Порядок по обслуживанию клиентов в кассе сотрудниками ВСП (Приложение 2)
        public HttpPostedFileBase CashWorkAddition2File { get; set; }
        public string CashWorkAddition2FileName { get; set; }
        public int CashWorkAddition2FileId { get; set; }
        public bool CashWorkAddition2FileNeeded { get; set; }//требуется скан

        //Обязательство о неразглашении коммерческой и служебной тайны
        public HttpPostedFileBase ObligationTradeSecretFile { get; set; }
        public string ObligationTradeSecretFileName { get; set; }
        public int ObligationTradeSecretFileId { get; set; }
        public bool ObligationTradeSecretFileNeeded { get; set; }//требуется скан

        //Справка 182-Н от предыдущего работодателя
        public HttpPostedFileBase Certificate182HFile { get; set; }
        public string Certificate182HFileName { get; set; }
        public int Certificate182HFileId { get; set; }
        public bool Certificate182HFileNeeded { get; set; }//требуется скан

        //Справка 2-НДФЛ от предыдущего работодателя
        public HttpPostedFileBase Certificate2NDFLFile { get; set; }
        public string Certificate2NDFLFileName { get; set; }
        public int Certificate2NDFLFileId { get; set; }
        public bool Certificate2NDFLFileNeeded { get; set; }//требуется скан


        //для удаления сканов
        public int DeleteAttachmentId { get; set; }
        //для сохранение выбранного перечня документов
        public bool IsSave { get; set; }
        //признак наличия сформированного списка документов для кандидата
        public bool IsDocListAvailable { get; set; }
        //признак видимости для кнопок печати (показа шаблона документов)
        public bool IsPrintButtonAvailable { get; set; }
        //признак видимости кнопки удаления скана
        public bool IsDeleteScanButtonAvailable { get; set; }
    }
}
