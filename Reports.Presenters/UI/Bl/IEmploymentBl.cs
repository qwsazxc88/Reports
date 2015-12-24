using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Reports.Presenters.UI.ViewModel;
using Reports.Presenters.UI.ViewModel.Employment2;
using Reports.Core.Domain;
using Reports.Core.Dto.Employment2;
using Reports.Core.Dto;

namespace Reports.Presenters.UI.Bl
{
    public interface IEmploymentBl : IBaseBl
    {
        GeneralInfoModel GetGeneralInfoModel(int? userId = null);
        GeneralInfoModel GetGeneralInfoModel(GeneralInfoModel model);

        PassportModel GetPassportModel(int? userId = null);
        PassportModel GetPassportModel(PassportModel model);

        EducationModel GetEducationModel(int? userId = null);

        FamilyModel GetFamilyModel(int? userId = null);
        FamilyModel GetFamilyModel(FamilyModel model);

        MilitaryServiceModel GetMilitaryServiceModel(int? userId = null);
        MilitaryServiceModel GetMilitaryServiceModel(MilitaryServiceModel model);

        ExperienceModel GetExperienceModel(int? userId = null);
        ExperienceModel GetExperienceModel(ExperienceModel model);

        ContactsModel GetContactsModel(int? userId = null);
        ContactsModel GetContactsModel(ContactsModel model);

        BackgroundCheckModel GetBackgroundCheckModel(int? userId = null);
        BackgroundCheckModel GetBackgroundCheckModel(BackgroundCheckModel model);

        OnsiteTrainingModel GetOnsiteTrainingModel(int? userId = null);
        ApplicationLetterModel GetApplicationLetterModel(int? userId = null);

        ManagersModel GetManagersModel(int? userId = null);
        ManagersModel GetManagersModel(ManagersModel model);
        IList<IdNameDto> GetPositionAutocomplete(string Name);

        PersonnelManagersModel GetPersonnelManagersModel(int? userId = null);
        PersonnelManagersModel GetPersonnelManagersModel(PersonnelManagersModel model);

        CandidateDocumentsModel GetCandidateDocumentsModel(int? userId = null);
        ScanOriginalDocumentsModel GetScanOriginalDocumentsModel(int? userId = null);
        PersonnelInfoModel GetPersonnelInfoModel(PersonnelInfoModel model);
        RosterModel GetRosterModel(RosterFiltersModel filters);
        CreateCandidateModel GetCreateCandidateModel();
        CreateCandidateModel GetCreateCandidateModel(CreateCandidateModel model);
        PrintCreatedCandidateModel GetPrintCreatedCandidateModel(int id, out string error);
        SignersModel GetSignersModel();
        PrintContractFormModel GetPrintContractFormModel(int userId);
        PrintEmploymentOrderModel GetPrintEmploymentOrderModel(int userId);
        PrintT2Model GetPrintT2Model(int userId);
        PrintLiabilityContractModel GetPrintLiabilityContractModel(int userId);
        PrintPersonalDataAgreementModel GetPrintPersonalDataAgreementModel(int userId);
        PrintPersonalDataObligationModel GetPrintPersonalDataObligationModel(int userId);
        PrintEmploymentFileModel GetPrintEmploymentFileModel(int userId);
        PrintRegisterPersonalRecordModel GetPrintRegisterPersonalRecordModel(int userId);
        PrintInstructionOfSecretModel GetPrintInstructionOfSecretModel(int userId);
        PrintInstructionEnsuringSafetyModel GetPrintInstructionEnsuringSafetyModel(int userId);
        PrintAgreePersonForCheckingModel GetPrintAgreePersonForCheckingModel(int userId);
        PrintCashWorkAddition1Model GetPrintCashWorkAddition1Model(int userId);
        PrintCashWorkAddition2Model GetPrintCashWorkAddition2Model(int userId);
        PrintObligationTradeSecretModel GetPrintObligationTradeSecretModel(int userId);
        IList<CandidateDto> GetPrintRosterModel(RosterFiltersModel filters, int? sortBy, bool? sortDescending);
        
        void LoadDictionaries(GeneralInfoModel model);
        void LoadDictionaries(PassportModel model);
        void LoadDictionaries(EducationModel model);
        void LoadDictionaries(FamilyModel model);
        void LoadDictionaries(MilitaryServiceModel model);
        void LoadDictionaries(ExperienceModel model);
        void LoadDictionaries(ContactsModel model);
        void LoadDictionaries(BackgroundCheckModel model);
        void LoadDictionaries(OnsiteTrainingModel model);
        void LoadDictionaries(ManagersModel model);
        void LoadDictionaries(PersonnelManagersModel model);
        void LoadDictionaries(RosterModel model);
        void LoadDictionaries(SignersModel model);

        IEnumerable<SelectListItem> GetCountries();
        IEnumerable<SelectListItem> GetInsuredPersonTypes();
        IEnumerable<SelectListItem> GetDisabilityDegrees();
        IEnumerable<SelectListItem> GetStatuses();
        IEnumerable<SelectListItem> GetDocumentTypes();
        IEnumerable<SelectListItem> GetRegistrationExpirations();
        IEnumerable<SelectListItem> GetPersonnelCategories();
        IEnumerable<SelectListItem> GetPersonnelTypes();
        IEnumerable<SelectListItem> GetConscriptionStatuses();
        IEnumerable<SelectListItem> GetPositions();
        IEnumerable<SelectListItem> GetSchedules();
        IEnumerable<SelectListItem> GetPersonalAccountContractors();
        IEnumerable<SelectListItem> GetAccessGroups();
        IEnumerable<SelectListItem> GetEmploymentStatuses();
        IEnumerable<SelectListItem> GetApprovalStatuses();
        IList<ContractPointDto> GetContractPointVariants();

        int? CreateCandidate(CreateCandidateModel model, out string error);
        bool ProcessSaving<TVM, TE>(TVM model, out string error)
            where TVM : AbstractEmploymentModel
            where TE : new();
        bool ProcessSaving(ApplicationLetterModel model, out string error);
        bool ProcessSaving(SignerDto itemToSave, out string error);
        void SaveAttachments<TVM>(TVM viewModel)
            where TVM : AbstractEmploymentModel;

        bool ApproveBackgroundCheck(BackgroundCheckModel model, out string error);
        bool PrevApproveBackgroundCheck(int userId, bool? PrevApprovalStatus, bool IsCancelApproveAvailale, out string error);
        bool SaveOnsiteTrainingReport (OnsiteTrainingModel viewModel, out string error);
        bool ApproveCandidateByManager(ManagersModel viewModel, out string error);
        bool ApproveCandidateByHigherManager(int userId, bool? approvalStatus, bool IsCancel, out string error);
        bool SavePersonnelManagersReport(PersonnelManagersModel viewModel, out string error);
        bool SavePersonnelManagersRejecting(PersonnelManagersModel viewModel, out string error);
        bool SaveApprovals(IList<CandidateApprovalDto> roster, out string error);
        bool SaveContractChangesToIndefinite(IList<CandidateChangeContractToIndefiniteDto> roster, out string error);
        /// <summary>
        /// Сохраняем признаки получения оригиналов ТК/ТД
        /// </summary>
        /// <param name="roster">Обрабатываемый список</param>
        /// <param name="IsTK">Переключатель типа документов.</param>
        /// <returns></returns>
        bool SaveCandidateDocRecieved(IList<CandidateDocRecievedDto> roster, bool IsTK);
        /// <summary>
        /// Добавляем комментарий
        /// </summary>
        /// <param name="CandidateId">Id кандидата</param>
        /// <param name="CommentTypeId">Вид журнала, к которому относится комментаий</param>
        /// <param name="Comment">Текст комментария</param>
        /// <param name="error">сообщение об ошибке</param>
        /// <returns></returns>
        bool SaveComments(int CandidateId, int CommentTypeId, string Comment, out string error);

        bool IsCurrentUserChiefForCreator(User current, User creator);
        bool IsUnlimitedEditAvailable();

        string GetStartView();
        AttachmentModel GetFileContext(int id);
        bool DeleteAttachment(DeleteAttacmentModel model);

        bool IsFixedTermContract(int userId);
        void DeleteNameChange(GeneralInfoModel model, int NameID);
        void DeleteLanguage(GeneralInfoModel model, int LanguageID);
        void DeleteEducationRow(EducationModel model);
        void DeleteExperiensRow(ExperienceModel model);
        void DeleteBackgroundRow(BackgroundCheckModel model);
        void DeleteFamilyMember(FamilyModel model);
        void SaveCandidateDocumentsAttachments(CandidateDocumentsModel model, out string error);
        /// <summary>
        /// Удаляем скан с вкладки документы.
        /// </summary>
        /// <param name="model">Модель страницы</param>
        /// <param name="error">Сообщение об ошибке</param>
        void DeleteCandidateDocument(CandidateDocumentsModel model, out string error);
        void SaveScanOriginalDocumentsModelAttachments(ScanOriginalDocumentsModel model, out string error);

        /// <summary>
        /// сохраняем признак технического увольнения из реестра
        /// </summary>
        /// <param name="CandidateId">Id кандидата.</param>
        /// <param name="IsDT">ghbpyfr</param>
        /// <returns></returns>
        bool SaveCandidateTechDissmiss(IList<CandidateTechDissmissDto> roster);
        /// <summary>
        /// Определяем наличие сведений об образовании по количеству строк.
        /// </summary>
        /// <param name="UserId">Id кандидата.</param>
        /// <param name="Type">Тип образования: 1 - Сведения об образовании, 2 - Послевузовское образование, 3 - Аттестация, 4 - Повышение квалификации.</param>
        /// <returns></returns>
        int CheckExistsEducationRecord(int UserId, int Type);
        /// <summary>
        /// Отправка сообщения участника процесса приема другому участнику.
        /// </summary>
        /// <param name="model">Обрабатываемая модель.</param>
        /// <param name="error">Сообщение.</param>
        /// <returns></returns>
        bool EmploymentProccedRegistrationSendEmail(PersonnelInfoModel model, out string error);
        void GetStaffEstablishmentPostDetails(ManagersModel model);
    }
}
