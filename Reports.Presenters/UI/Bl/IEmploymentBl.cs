﻿using System;
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

        bool ApproveBackgroundCheck (int userId, bool IsApprovalSkipped, bool? approvalStatus, out string error);
        bool SaveOnsiteTrainingReport (OnsiteTrainingModel viewModel, out string error);
        bool ApproveCandidateByManager(ManagersModel viewModel, out string error);
        bool ApproveCandidateByHigherManager(int userId, bool? approvalStatus, out string error);
        bool SavePersonnelManagersReport(PersonnelManagersModel viewModel, out string error);
        bool SaveApprovals(IList<CandidateApprovalDto> roster, out string error);
        bool SaveContractChangesToIndefinite(IList<CandidateChangeContractToIndefiniteDto> roster, out string error);
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
        void SaveCandidateDocumentsAttachments(CandidateDocumentsModel model);
    }
}
