using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Reports.Presenters.UI.ViewModel;
using Reports.Presenters.UI.ViewModel.Employment2;
using Reports.Core.Domain;
using Reports.Core.Dto.Employment2;

namespace Reports.Presenters.UI.Bl
{
    public interface IEmploymentBl : IBaseBl
    {
        GeneralInfoModel GetGeneralInfoModel(int? userId = null);
        PassportModel GetPassportModel(int? userId = null);
        EducationModel GetEducationModel(int? userId = null);
        FamilyModel GetFamilyModel(int? userId = null);
        MilitaryServiceModel GetMilitaryServiceModel(int? userId = null);
        ExperienceModel GetExperienceModel(int? userId = null);
        ContactsModel GetContactsModel(int? userId = null);
        BackgroundCheckModel GetBackgroundCheckModel(int? userId = null);
        OnsiteTrainingModel GetOnsiteTrainingModel(int? userId = null);
        ManagersModel GetManagersModel(int? userId = null);
        PersonnelManagersModel GetPersonnelManagersModel(int? userId = null);
        RosterModel GetRosterModel(RosterFiltersModel filters);
        SignersModel GetSignersModel();

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
        IEnumerable<SelectListItem> GetRanks();
        IEnumerable<SelectListItem> GetRegistrationExpirations();
        IEnumerable<SelectListItem> GetPersonnelCategories();
        IEnumerable<SelectListItem> GetPersonnelTypes();
        IEnumerable<SelectListItem> GetConscriptionStatuses();
        IEnumerable<SelectListItem> GetPositions();
        IEnumerable<SelectListItem> GetDirectorates();
        IEnumerable<SelectListItem> GetDepartments();
        IEnumerable<SelectListItem> GetSchedules();
        IEnumerable<SelectListItem> GetPersonalAccountContractors();
        IEnumerable<SelectListItem> GetAccessGroups();
        IEnumerable<SelectListItem> GetEmploymentStatuses();
        IEnumerable<SelectListItem> GetApprovalStatuses();
        IEnumerable<SelectListItem> GetOnsiteTrainingStatuses();

        bool ProcessSaving<TVM, TE>(TVM model, out string error)
            where TVM : AbstractEmploymentModel
            where TE : new();

        bool ApproveBackgroundCheck (int userId, out string error);
        bool SaveOnsiteTrainingReport (OnsiteTrainingModel viewModel, out string error);
        bool ApproveCandidateByManager(ManagersModel viewModel, out string error);
        bool ApproveCandidateByHigherManager(int userId, out string error);
        bool SavePersonnelManagersReport(PersonnelManagersModel viewModel, out string error);
        bool SaveApprovals(IList<CandidateApprovalDto> roster, out string error);

        bool IsCurrentUserChiefForCreator(User current, User creator);
        string GetStartView();
        AttachmentModel GetFileContext(int id);
        bool DeleteAttachment(DeleteAttacmentModel model);
    }
}
