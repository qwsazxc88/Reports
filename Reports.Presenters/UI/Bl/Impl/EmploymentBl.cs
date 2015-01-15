using System;
using System.Reflection;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using Reports.Core;
using Reports.Core.Enum;
using Reports.Core.Dao;
using Reports.Core.Dto;
using Reports.Core.Dto.Employment2;
using Reports.Core.Domain;
using Reports.Presenters.Services;
using Reports.Presenters.UI.ViewModel;
using Reports.Presenters.UI.ViewModel.Employment2;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.IO;


namespace Reports.Presenters.UI.Bl.Impl
{
    public class EmploymentBl : RequestBl, IEmploymentBl
    {
        #region Constants

        public const string StrIncorrectManagerLevel = "Неправильный уровень {0} руководителя (id {1}) в базе данных.";
        public const string StrNoDepartmentForManager = "Не указано структурное подраздаление для руководителя (id {0}).";
        public const string StrNotAgreedToPersonalDataProcessing = "Сохранение невозможно: отсутствует согласие на обработку персональных данных.";

        public const int MinManagerLevel = 2;
        public const int MaxManagerLevel = 6;

        public int RUSSIAN_FEDERATION = 643;

        #endregion

        #region Dependencies

        protected IEmploymentCandidateDao employmentCandidateDao;
        public IEmploymentCandidateDao EmploymentCandidateDao
        {
            get { return Validate.Dependency(employmentCandidateDao); }
            set { employmentCandidateDao = value; }
        }

        protected IEmploymentCommonDao employmentCommonDao;
        public IEmploymentCommonDao EmploymentCommonDao
        {
            get { return Validate.Dependency(employmentCommonDao); }
            set { employmentCommonDao = value; }
        }

        protected IEmploymentGeneralInfoDao employmentGeneralInfoDao;
        public IEmploymentGeneralInfoDao EmploymentGeneralInfoDao
        {
            get { return Validate.Dependency(employmentGeneralInfoDao); }
            set { employmentGeneralInfoDao = value; }
        }

        protected IInsuredPersonTypeDao insuredPersonTypeDao;
        public IInsuredPersonTypeDao InsuredPersonTypeDao
        {
            get { return Validate.Dependency(insuredPersonTypeDao); }
            set { insuredPersonTypeDao = value; }
        }

        protected IEmploymentPassportDao employmentPassportDao;
        public IEmploymentPassportDao EmploymentPassportDao
        {
            get { return Validate.Dependency(employmentPassportDao); }
            set { employmentPassportDao = value; }
        }

        protected IDocumentTypeDao documentTypeDao;
        public IDocumentTypeDao DocumentTypeDao
        {
            get { return Validate.Dependency(documentTypeDao); }
            set { documentTypeDao = value; }
        }

        protected IEmploymentEducationDao employmentEducationDao;
        public IEmploymentEducationDao EmploymentEducationDao
        {
            get { return Validate.Dependency(employmentEducationDao); }
            set { employmentEducationDao = value; }
        }

        protected IEmploymentFamilyDao employmentFamilyDao;
        public IEmploymentFamilyDao EmploymentFamilyDao
        {
            get { return Validate.Dependency(employmentFamilyDao); }
            set { employmentFamilyDao = value; }
        }

        protected IEmploymentMilitaryServiceDao employmentMilitaryServiceDao;
        public IEmploymentMilitaryServiceDao EmploymentMilitaryServiceDao
        {
            get { return Validate.Dependency(employmentMilitaryServiceDao); }
            set { employmentMilitaryServiceDao = value; }
        }

        protected IEmploymentExperienceDao employmentExperienceDao;
        public IEmploymentExperienceDao EmploymentExperienceDao
        {
            get { return Validate.Dependency(employmentExperienceDao); }
            set { employmentExperienceDao = value; }
        }

        protected IEmploymentContactsDao employmentContactsDao;
        public IEmploymentContactsDao EmploymentContactsDao
        {
            get { return Validate.Dependency(employmentContactsDao); }
            set { employmentContactsDao = value; }
        }

        protected IEmploymentBackgroundCheckDao employmentBackgroundCheckDao;
        public IEmploymentBackgroundCheckDao EmploymentBackgroundCheckDao
        {
            get { return Validate.Dependency(employmentBackgroundCheckDao); }
            set { employmentBackgroundCheckDao = value; }
        }

        protected IEmploymentOnsiteTrainingDao employmentOnsiteTrainingDao;
        public IEmploymentOnsiteTrainingDao EmploymentOnsiteTrainingDao
        {
            get { return Validate.Dependency(employmentOnsiteTrainingDao); }
            set { employmentOnsiteTrainingDao = value; }
        }

        protected IEmploymentManagersDao employmentManagersDao;
        public IEmploymentManagersDao EmploymentManagersDao
        {
            get { return Validate.Dependency(employmentManagersDao); }
            set { employmentManagersDao = value; }
        }

        protected IEmploymentPersonnelManagersDao employmentPersonnelManagersDao;
        public IEmploymentPersonnelManagersDao EmploymentPersonnelManagersDao
        {
            get { return Validate.Dependency(employmentPersonnelManagersDao); }
            set { employmentPersonnelManagersDao = value; }
        }

        protected ICountryDao countryDao;
        public ICountryDao CountryDao
        {
            get { return Validate.Dependency(countryDao); }
            set { countryDao = value; }
        }

        protected IAccessGroupDao accessGroupDao;
        public IAccessGroupDao AccessGroupDao
        {
            get { return Validate.Dependency(accessGroupDao); }
            set { accessGroupDao = value; }
        }

        protected IDisabilityDegreeDao disabilityDegreeDao;
        public IDisabilityDegreeDao DisabilityDegreeDao
        {
            get { return Validate.Dependency(disabilityDegreeDao); }
            set { disabilityDegreeDao = value; }
        }

        protected IPersonalAccountContractorDao personalAccountContractorDao;
        public IPersonalAccountContractorDao PersonalAccountContractorDao
        {
            get { return Validate.Dependency(personalAccountContractorDao); }
            set { personalAccountContractorDao = value; }
        }

        protected IAppointmentDao appointmentDao;
        public IAppointmentDao AppointmentDao
        {
            get { return Validate.Dependency(appointmentDao); }
            set { appointmentDao = value; }
        }

        protected IScheduleDao scheduleDao;
        public IScheduleDao ScheduleDao
        {
            get { return Validate.Dependency(scheduleDao); }
            set { scheduleDao = value; }
        }

        protected IManualRoleRecordDao missionOrderRoleRecordDao;
        public IManualRoleRecordDao MissionOrderRoleRecordDao
        {
            get { return Validate.Dependency(missionOrderRoleRecordDao); }
            set { missionOrderRoleRecordDao = value; }
        }

        protected IEmploymentSignersDao employmentSignersDao;
        public IEmploymentSignersDao EmploymentSignersDao
        {
            get { return Validate.Dependency(employmentSignersDao); }
            set { employmentSignersDao = value; }
        }

        #endregion

        #region Get Model

        public GeneralInfoModel GetGeneralInfoModel(int? userId = null)
        {
            userId = userId ?? AuthenticationService.CurrentUser.Id;
            GeneralInfoModel model = new GeneralInfoModel { UserId = userId.Value };
            LoadDictionaries(model);
            GeneralInfo entity = null;
            int? id = EmploymentCommonDao.GetDocumentId<GeneralInfo>(userId.Value);
            if (id.HasValue)
            {
                entity = EmploymentGeneralInfoDao.Get(id.Value);
            }
            if (entity != null)
            {
                model.AgreedToPersonalDataProcessing = entity.AgreedToPersonalDataProcessing;
                model.CitizenshipId = entity.Citizenship != null ? entity.Citizenship.Id : RUSSIAN_FEDERATION;
                model.CityOfBirth = entity.CityOfBirth;
                model.DateOfBirth = entity.DateOfBirth;

                model.DisabilityCertificateDateOfIssue = entity.DisabilityCertificateDateOfIssue;
                model.DisabilityCertificateExpirationDate = entity.DisabilityCertificateExpirationDate;
                model.DisabilityCertificateNumber = entity.DisabilityCertificateNumber;
                model.DisabilityCertificateSeries = entity.DisabilityCertificateSeries;
                model.DisabilityDegreeId =
                    entity.DisabilityDegree != null
                    ? (int?)entity.DisabilityDegree.Id
                    : null;

                model.DistrictOfBirth = entity.DistrictOfBirth;
                model.FirstName = entity.FirstName;

                foreach (var item in entity.ForeignLanguages)
                {
                    model.ForeignLanguages.Add(new ForeignLanguageDto { LanguageName = item.LanguageName, Level = item.Level });
                }

                model.INN = entity.INN;
                model.InsuredPersonTypeId = entity.InsuredPersonType != null ? (int?)entity.InsuredPersonType.Id : null;
                model.InsuredPersonTypeSelectedName =
                    model.InsuredPersonTypeId.HasValue
                    ? model.InsuredPersonTypeItems.Where(x => x.Value == model.InsuredPersonTypeId.ToString()).FirstOrDefault().Text
                    : string.Empty;
                model.IsMale = entity.IsMale;
                model.IsPatronymicAbsent = entity.IsPatronymicAbsent;
                model.LastName = entity.LastName;

                foreach (var item in entity.NameChanges)
                {
                    model.NameChanges.Add(new NameChangeDto { Date = item.Date, Place = item.Place, PreviousName = item.PreviousName, Reason = item.Reason });
                }

                model.Patronymic = entity.Patronymic;
                model.RegionOfBirth = entity.RegionOfBirth;
                model.SNILS = entity.SNILS;
                model.StatusId = entity.Status ?? 0;
                model.Version = entity.Version;
                model.IsDraft = true;
                model.IsFinal = entity.IsFinal;

                int attachmentId = 0;
                string attachmentFilename = string.Empty;
                GetAttachmentData(ref attachmentId, ref attachmentFilename, entity.Candidate.Id, RequestAttachmentTypeEnum.INNScan);
                model.INNScanAttachmentId = attachmentId;
                model.INNScanAttachmentFilename = attachmentFilename;
            }            
            return model;
        }

        protected void GetAttachmentData(ref int attachmentId, ref string attachmentFilename, int candidateId, RequestAttachmentTypeEnum type)
        {
            if (candidateId == 0)
                return;
            RequestAttachment attach = RequestAttachmentDao.FindByRequestIdAndTypeId(candidateId, type);
            if (attach == null)
                return;
            attachmentId = attach.Id;
            attachmentFilename = attach.FileName;
        }

        public PassportModel GetPassportModel(int? userId = null)
        {
            userId = userId ?? AuthenticationService.CurrentUser.Id;
            PassportModel model = new PassportModel { UserId = userId.Value };
            Passport entity = null;
            int? id = EmploymentCommonDao.GetDocumentId<Passport>(userId.Value);
            if (id.HasValue)
            {
                entity = EmploymentPassportDao.Get(id.Value);
            }
            if (entity != null)
            {
                model.Apartment = entity.Apartment;
                model.Building = entity.Building;
                model.City = entity.City;
                model.District = entity.District;
                model.DocumentTypeId = entity.DocumentType != null ? entity.DocumentType.Id : 0;
                model.InternalPassportDateOfIssue = entity.InternalPassportDateOfIssue;
                model.InternalPassportIssuedBy = entity.InternalPassportIssuedBy;
                model.InternalPassportNumber = entity.InternalPassportNumber;
                model.InternalPassportSeries = entity.InternalPassportSeries;
                model.InternalPassportSubdivisionCode = entity.InternalPassportSubdivisionCode;
                model.InternationalPassportDateOfIssue = entity.InternationalPassportDateOfIssue;
                model.InternationalPassportIssuedBy = entity.InternationalPassportIssuedBy;
                model.InternationalPassportNumber = entity.InternationalPassportNumber;
                model.InternationalPassportSeries = entity.InternationalPassportSeries;
                model.Region = entity.Region;
                model.RegistrationDate = entity.RegistrationDate;
                model.Street = entity.Street;
                model.StreetNumber = entity.StreetNumber;
                model.ZipCode = entity.ZipCode;
                model.IsDraft = true;
                model.IsFinal = entity.IsFinal;
            }
            LoadDictionaries(model);
            return model;
        }

        public EducationModel GetEducationModel(int? userId = null)
        {
            userId = userId ?? AuthenticationService.CurrentUser.Id;
            EducationModel model = new EducationModel { UserId = userId.Value };
            Education entity = null;
            int? id = EmploymentCommonDao.GetDocumentId<Education>(userId.Value);
            if (id.HasValue)
            {
                entity = EmploymentEducationDao.Get(id.Value);
            }
            if (entity != null)
            {
                foreach (var item in entity.Certifications)
                {
                    model.Certifications.Add(new CertificationDto {
                        CertificateDateOfIssue = item.CertificateDateOfIssue,
                        CertificateNumber = item.CertificateNumber,
                        CertificationDate = item.CertificationDate,
                        InitiatingOrder = item.InitiatingOrder
                    });
                }
                foreach (var item in entity.HigherEducationDiplomas)
                {
                    model.HigherEducationDiplomas.Add(new HigherEducationDiplomaDto
                    {
                        AdmissionYear = item.AdmissionYear,
                        Department = item.Department,
                        GraduationYear = item.GraduationYear,
                        IssuedBy = item.IssuedBy,
                        Number = item.Number,
                        Profession = item.Profession,
                        Qualification = item.Qualification,
                        Series = item.Series,
                        Speciality = item.Speciality
                    });
                }
                foreach (var item in entity.PostGraduateEducationDiplomas)
                {
                    model.PostGraduateEducationDiplomas.Add(new PostGraduateEducationDiplomaDto
                    {
                        AdmissionYear = item.AdmissionYear,
                        GraduationYear = item.GraduationYear,
                        IssuedBy = item.IssuedBy,
                        Number = item.Number,
                        Series = item.Series,
                        Speciality = item.Speciality
                    });
                }
                foreach (var item in entity.Training)
                {
                    model.Training.Add(new TrainingDto
                    {
                        BeginningDate = item.BeginningDate,
                        CertificateIssuedBy = item.CertificateIssuedBy,
                        EndDate = item.EndDate,
                        Number = item.Number,
                        Series = item.Series,
                        Speciality = item.Speciality
                    });
                }
                model.IsDraft = true;
                model.IsFinal = entity.IsFinal;
            }
            LoadDictionaries(model);
            return model;
        }

        public FamilyModel GetFamilyModel(int? userId = null)
        {
            userId = userId ?? AuthenticationService.CurrentUser.Id;
            FamilyModel model = new FamilyModel { UserId = userId.Value };
            Family entity = null;
            int? id = EmploymentCommonDao.GetDocumentId<Family>(userId.Value);
            if (id.HasValue)
            {
                entity = EmploymentFamilyDao.Get(id.Value);
            }
            if (entity != null)
            {
                model.Children = entity.FamilyMembers.Where<FamilyMember>(x => x.RelationshipId == FamilyRelationship.CHILD)
                    .ToList<FamilyMember>()
                    .ConvertAll<FamilyMemberDto>(x => new FamilyMemberDto
                    {
                        Contacts = x.Contacts,
                        DateOfBirth = x.DateOfBirth,
                        Name = x.Name,
                        PassportData = x.PassportData,
                        PlaceOfBirth = x.PlaceOfBirth,
                        WorksAt = x.WorksAt
                    });

                model.Cohabitants = entity.Cohabitants;

                model.Father = entity.FamilyMembers.Where<FamilyMember>(x => x.RelationshipId == FamilyRelationship.FATHER)
                    .ToList<FamilyMember>()
                    .ConvertAll<FamilyMemberDto>(x => new FamilyMemberDto
                    {
                        Contacts = x.Contacts,
                        DateOfBirth = x.DateOfBirth,
                        Name = x.Name,
                        PassportData = x.PassportData,
                        PlaceOfBirth = x.PlaceOfBirth,
                        WorksAt = x.WorksAt
                    })
                    .FirstOrDefault<FamilyMemberDto>();
                /*if (model.Father == null)
                {
                    model.Father = new FamilyMemberDto();
                }*/

                model.Mother = entity.FamilyMembers.Where<FamilyMember>(x => x.RelationshipId == FamilyRelationship.MOTHER)
                    .ToList<FamilyMember>()
                    .ConvertAll<FamilyMemberDto>(x => new FamilyMemberDto
                    {
                        Contacts = x.Contacts,
                        DateOfBirth = x.DateOfBirth,
                        Name = x.Name,
                        PassportData = x.PassportData,
                        PlaceOfBirth = x.PlaceOfBirth,
                        WorksAt = x.WorksAt
                    })
                    .FirstOrDefault<FamilyMemberDto>();
                /*if (model.Mother == null)
                {
                    model.Mother = new FamilyMemberDto();
                }*/

                model.Spouse = entity.FamilyMembers.Where<FamilyMember>(x => x.RelationshipId == FamilyRelationship.SPOUSE)
                    .ToList<FamilyMember>()
                    .ConvertAll<FamilyMemberDto>(x => new FamilyMemberDto
                    {
                        Contacts = x.Contacts,
                        DateOfBirth = x.DateOfBirth,
                        Name = x.Name,
                        PassportData = x.PassportData,
                        PlaceOfBirth = x.PlaceOfBirth,
                        WorksAt = x.WorksAt
                    })
                    .FirstOrDefault<FamilyMemberDto>();
                if (model.Spouse == null)
                {
                    model.IsMarried = false;
                    //model.Spouse = new FamilyMemberDto();
                }
                else
                {
                    model.IsMarried = true;
                }
                model.IsDraft = true;
                model.IsFinal = entity.IsFinal;
            }
            LoadDictionaries(model);
            return model;
        }

        public MilitaryServiceModel GetMilitaryServiceModel(int? userId = null)
        {
            userId = userId ?? AuthenticationService.CurrentUser.Id;
            MilitaryServiceModel model = new MilitaryServiceModel { UserId = userId.Value };
            MilitaryService entity = null;
            int? id = EmploymentCommonDao.GetDocumentId<MilitaryService>(userId.Value);
            if (id.HasValue)
            {
                entity = EmploymentMilitaryServiceDao.Get(id.Value);
            }
            if (entity != null)
            {
                model.CombatFitness = entity.CombatFitness;
                model.Commissariat = entity.Commissariat;
                model.CommonMilitaryServiceRegistrationInfo = entity.CommonMilitaryServiceRegistrationInfo;
                model.ConscriptionStatus = entity.ConscriptionStatus;
                model.IsAssigned = entity.IsAssigned;
                model.IsLiableForMilitaryService = entity.IsLiableForMilitaryService;
                model.IsReserved = entity.IsReserved;
                model.MilitaryCardDate = entity.MilitaryCardDate;
                model.MilitaryCardNumber = entity.MilitaryCardNumber;
                model.MilitaryServiceRegistrationInfo = entity.MilitaryServiceRegistrationInfo;
                model.MilitarySpecialityCode = entity.MilitarySpecialityCode;
                model.MobilizationTicketNumber = entity.MobilizationTicketNumber;
                model.PersonnelCategory = entity.PersonnelCategory;
                model.PersonnelType = entity.PersonnelType;
                model.Rank = entity.Rank;
                model.RegistrationExpiration = entity.RegistrationExpiration;
                model.ReserveCategory = entity.ReserveCategory;
                model.SpecialityCategory = entity.SpecialityCategory;
                model.SpecialMilitaryServiceRegistrationInfo = entity.SpecialMilitaryServiceRegistrationInfo;
                model.IsDraft = true;
                model.IsFinal = entity.IsFinal;
            }
            LoadDictionaries(model);
            return model;
        }

        public ExperienceModel GetExperienceModel(int? userId = null)
        {
            userId = userId ?? AuthenticationService.CurrentUser.Id;
            ExperienceModel model = new ExperienceModel { UserId = userId.Value };
            Experience entity = null;
            int? id = EmploymentCommonDao.GetDocumentId<Experience>(userId.Value);
            if (id.HasValue)
            {
                entity = EmploymentExperienceDao.Get(id.Value);
            }
            if (entity != null)
            {
                foreach (var item in entity.ExperienceItems)
                {
                    model.ExperienceItems.Add(new ExperienceItemDto
                    {
                        BeginningDate = item.BeginningDate,
                        Company = item.Company,
                        CompanyContacts = item.CompanyContacts,
                        EndDate = item.EndDate,
                        Position = item.Position
                    });
                }
                model.WorkBookDateOfIssue = entity.WorkBookDateOfIssue;
                model.WorkBookNumber = entity.WorkBookNumber;
                model.WorkBookSeries = entity.WorkBookSeries;
                model.WorkBookSupplementDateOfIssue = entity.WorkBookSupplementDateOfIssue;
                model.WorkBookSupplementNumber = entity.WorkBookSupplementNumber;
                model.WorkBookSupplementSeries = entity.WorkBookSupplementSeries;
                model.IsDraft = true;
                model.IsFinal = entity.IsFinal;
            }
            LoadDictionaries(model);
            return model;
        }

        public ContactsModel GetContactsModel(int? userId = null)
        {
            userId = userId ?? AuthenticationService.CurrentUser.Id;
            ContactsModel model = new ContactsModel { UserId = userId.Value };
            Contacts entity = null;
            int? id = EmploymentCommonDao.GetDocumentId<Contacts>(userId.Value);
            if (id.HasValue)
            {
                entity = EmploymentContactsDao.Get(id.Value);
            }
            if (entity != null)
            {
                model.Apartment = entity.Apartment;
                model.Building = entity.Building;
                model.City = entity.City;
                model.District = entity.District;
                model.Email = entity.Email;
                model.HomePhone = entity.HomePhone;
                model.Mobile = entity.Mobile;
                model.Region = entity.Region;
                model.Street = entity.Street;
                model.StreetNumber = entity.StreetNumber;
                model.WorkPhone = entity.WorkPhone;
                model.ZipCode = entity.ZipCode;
                model.IsDraft = true;
                model.IsFinal = entity.IsFinal;
            }
            LoadDictionaries(model);
            return model;
        }

        public BackgroundCheckModel GetBackgroundCheckModel(int? userId = null)
        {
            userId = userId ?? AuthenticationService.CurrentUser.Id;
            BackgroundCheckModel model = new BackgroundCheckModel { UserId = userId.Value };
            BackgroundCheck entity = null;
            int? id = EmploymentCommonDao.GetDocumentId<BackgroundCheck>(userId.Value);
            if (id.HasValue)
            {
                entity = EmploymentBackgroundCheckDao.Get(id.Value);
            }
            if (entity != null)
            {
                model.AutomobileLicensePlateNumber = entity.AutomobileLicensePlateNumber;
                model.AutomobileMake = entity.AutomobileMake;
                model.AverageSalary = entity.AverageSalary;
                model.ChronicalDiseases = entity.ChronicalDiseases;
                model.Drinking = entity.Drinking;
                model.DriversLicenseCategories = entity.DriversLicenseCategories;
                model.DriversLicenseDateOfIssue = entity.DriversLicenseDateOfIssue;
                model.DriversLicenseNumber = entity.DriversLicenseNumber;
                model.DrivingExperience = entity.DrivingExperience;
                model.HasAutomobile = entity.AutomobileMake != null && entity.AutomobileMake.Length > 0;
                model.HasDriversLicense = entity.DriversLicenseDateOfIssue.HasValue;
                model.Hobbies = entity.Hobbies;
                model.ImportantEvents = entity.ImportantEvents;
                model.IsReadyForBusinessTrips = entity.IsReadyForBusinessTrips;
                model.Liabilities = entity.Liabilities;
                model.MilitaryOperationsExperience = entity.MilitaryOperationsExperience;
                model.Penalties = entity.Penalties;
                model.PositionSought = entity.PositionSought;
                model.PreviousDismissalReason = entity.PreviousDismissalReason;
                model.PreviousSuperior = entity.PreviousSuperior;
                model.PsychiatricAndAddictionTreatment = entity.PsychiatricAndAddictionTreatment;
                foreach (var item in entity.References)
                {
                    model.References.Add(new ReferenceDto
                    {
                        FirstName = item.FirstName,
                        LastName = item.LastName,
                        Patronymic = item.Patronymic,
                        Phone = item.Phone,
                        Position = item.Position,
                        Relation = item.Relation,
                        WorksAt = item.WorksAt                        
                    });
                }
                model.Smoking = entity.Smoking;
                model.Sports = entity.Sports;
                                
                model.IsDraft = true;
                model.IsFinal = entity.IsFinal;

                model.IsApprovalSkipped = entity.IsApprovalSkipped;
                model.ApproverName = entity.Approver == null ? string.Empty : entity.Approver.Name;
                model.ApprovalStatus = entity.ApprovalStatus;
                model.IsApproveBySecurityAvailable = (entity.Candidate.Status == EmploymentStatus.PENDING_APPROVAL_BY_SECURITY)
                    && ((AuthenticationService.CurrentUser.UserRole & UserRole.Security) == UserRole.Security);
            }
            LoadDictionaries(model);
            return model;
        }

        public OnsiteTrainingModel GetOnsiteTrainingModel(int? userId = null)
        {
            userId = userId ?? AuthenticationService.CurrentUser.Id;
            OnsiteTrainingModel model = new OnsiteTrainingModel { UserId = userId.Value };
            OnsiteTraining entity = null;
            int? id = EmploymentCommonDao.GetDocumentId<OnsiteTraining>(userId.Value);
            if (id.HasValue)
            {
                entity = EmploymentOnsiteTrainingDao.Get(id.Value);
            }
            if (entity != null)
            {
                model.BeginningDate = entity.BeginningDate;
                model.Comments = entity.Comments;
                model.Description = entity.Description;
                model.EndDate = entity.EndDate;
                model.IsComplete = entity.IsComplete;
                model.ReasonsForIncompleteTraining = entity.ReasonsForIncompleteTraining;
                model.Results = entity.Results;
                model.Type = entity.Type;

                model.ApproverName = entity.Approver != null ? entity.Approver.Name : string.Empty;
                model.IsApproveByTrainerAvailable = ((AuthenticationService.CurrentUser.UserRole & UserRole.Trainer) == UserRole.Trainer
                    && !entity.IsFinal);

                model.IsDraft = true;
                model.IsFinal = entity.IsFinal;
            }
            else
            {
                model.IsDraft = true;
                model.IsFinal = false;
                model.IsApproveByTrainerAvailable = true;
            }

            LoadDictionaries(model);
            return model;
        }

        public ApplicationLetterModel GetApplicationLetterModel(int? userId = null)
        {
            userId = userId ?? AuthenticationService.CurrentUser.Id;
            EmploymentCandidate candidate = GetCandidate(userId.Value);
            ApplicationLetterModel model = new ApplicationLetterModel { UserId = userId.Value };

            int attachmentId = 0;
            string attachmentFilename = string.Empty;
            GetAttachmentData(ref attachmentId, ref attachmentFilename, candidate.Id, RequestAttachmentTypeEnum.ApplicationLetterScan);
            model.ApplicationLetterScanAttachmentId = attachmentId;
            model.ApplicationLetterScanAttachmentFilename = attachmentFilename;
            model.IsApplicationLetterUploadAvailable = candidate.Status == EmploymentStatus.PENDING_APPLICATION_LETTER && !(model.ApplicationLetterScanAttachmentId > 0);

            return model;
        }

        public ManagersModel GetManagersModel(int? userId = null)
        {
            userId = userId ?? AuthenticationService.CurrentUser.Id;
            ManagersModel model = new ManagersModel { UserId = userId.Value };
            Managers entity = null;
            int? id = EmploymentCommonDao.GetDocumentId<Managers>(userId.Value);
            if (id.HasValue)
            {
                entity = EmploymentManagersDao.Get(id.Value);
            }

            if (entity != null)
            {
                model.Bonus = entity.Bonus;                

                // Если подразделение еще не заполнено,
                // пытаемся подтянуть его из временной учетной записи соответствующего пользователя
                model.DepartmentId = entity.Department != null
                    ? entity.Department.Id
                    : (entity.Candidate.User.Department != null
                        ? entity.Candidate.User.Department.Id
                        : 0);
                model.DepartmentName = entity.Department != null
                    ? entity.Department.Name
                    : (entity.Candidate.User.Department != null
                        ? entity.Candidate.User.Department.Name
                        : string.Empty);

                model.EmploymentConditions = entity.EmploymentConditions;
                model.IsSecondaryJob = entity.IsSecondaryJob;
                model.IsFront = entity.IsFront;
                model.IsLiable = entity.IsLiable;
                model.PersonalAddition = entity.PersonalAddition;
                model.PositionAddition = entity.PositionAddition;
                model.PositionId = entity.Position != null ? entity.Position.Id : 0;
                model.ProbationaryPeriod = entity.ProbationaryPeriod;
                model.RequestNumber = entity.RequestNumber;
                model.SalaryBasis = entity.SalaryBasis;
                model.SalaryMultiplier = entity.SalaryMultiplier;
                model.ScheduleId = entity.Schedule != null ? (int?)entity.Schedule.Id : null;
                model.WorkCity = entity.WorkCity;

                model.ApprovingManagerName = entity.ApprovingManager != null ? entity.ApprovingManager.Name : string.Empty;
                model.ApprovingHigherManagerName = entity.ApprovingHigherManager != null ? entity.ApprovingHigherManager.Name : string.Empty;
                model.ManagerApprovalDate = entity.ManagerApprovalDate;
                model.ManagerRejectionReason = entity.ManagerRejectionReason;

                model.ManagerApprovalStatus = entity.ManagerApprovalStatus;
                model.HigherManagerApprovalStatus = entity.HigherManagerApprovalStatus;
                model.HigherManagerApprovalDate = entity.HigherManagerApprovalDate;
                model.HigherManagerRejectionReason = entity.HigherManagerRejectionReason;
            }

            EmploymentCandidate candidate = GetCandidate(userId.Value);

            model.IsApproveByManagerAvailable = (candidate.Status == EmploymentStatus.PENDING_APPROVAL_BY_MANAGER)
                && ((AuthenticationService.CurrentUser.UserRole & UserRole.Manager) == UserRole.Manager);

            model.IsApproveByHigherManagerAvailable = (candidate.Status == EmploymentStatus.PENDING_APPROVAL_BY_HIGHER_MANAGER)
                && ((AuthenticationService.CurrentUser.UserRole & UserRole.Manager) == UserRole.Manager);

            LoadDictionaries(model);
            return model;
        }

        public PersonnelManagersModel GetPersonnelManagersModel(int? userId = null)
        {
            userId = userId ?? AuthenticationService.CurrentUser.Id;
            PersonnelManagersModel model = new PersonnelManagersModel { UserId = userId.Value };
            PersonnelManagers entity = null;
            int? id = EmploymentCommonDao.GetDocumentId<PersonnelManagers>(userId.Value);
            if (id.HasValue)
            {
                entity = EmploymentPersonnelManagersDao.Get(id.Value);
            }

            if (entity != null)
            {
                model.AccessGroupId = entity.AccessGroup != null ? entity.AccessGroup.Id : 0;
                //model.ApprovedByPersonnelManager = entity.ApprovedByPersonnelManager;
                model.AreaAddition = entity.AreaAddition;
                model.AreaMultiplier = entity.AreaMultiplier;
                model.CompetenceAddition = entity.CompetenceAddition;
                model.ContractDate = entity.ContractDate;
                model.ContractEndDate = entity.ContractEndDate;
                model.ContractNumber = entity.ContractNumber;
                model.EmploymentDate = entity.EmploymentDate;
                model.EmploymentOrderDate = entity.EmploymentOrderDate;
                model.EmploymentOrderNumber = entity.EmploymentOrderNumber;
                model.FrontOfficeExperienceAddition = entity.FrontOfficeExperienceAddition;
                model.Grade = entity.Candidate.User.Grade;
                model.InsurableExperienceDays = entity.InsurableExperienceDays;
                model.InsurableExperienceMonths = entity.InsurableExperienceMonths;
                model.InsurableExperienceYears = entity.InsurableExperienceYears;

                model.IsFixedTermContract = entity.Candidate.User.IsFixedTermContract;
                model.IsHourlySalaryBasis = entity.IsHourlySalaryBasis;
                model.IsContractChangedToIndefinite = entity.SupplementaryAgreements != null && entity.SupplementaryAgreements.Count > 0;
                if (entity.SupplementaryAgreements != null && entity.SupplementaryAgreements.Count > 0)
                {
                    model.SupplementaryAgreementCreateDate = entity.SupplementaryAgreements[0].CreateDate;
                    model.SupplementaryAgreementNumber = entity.SupplementaryAgreements[0].Number;
                    model.ChangeContractToIndefiniteOrderCreateDate = entity.SupplementaryAgreements[0].OrderCreateDate;
                    model.ChangeContractToIndefiniteOrderNumber = entity.SupplementaryAgreements[0].OrderNumber;
                }

                model.Level = entity.Candidate.User.Level;
                model.NorthernAreaAddition = entity.NorthernAreaAddition;
                model.OverallExperienceDays = entity.OverallExperienceDays;
                model.OverallExperienceMonths = entity.OverallExperienceMonths;
                model.OverallExperienceYears = entity.OverallExperienceYears;
                model.PersonalAccount = entity.PersonalAccount;
                model.PersonalAccountContractorId = entity.PersonalAccountContractor != null ? entity.PersonalAccountContractor.Id : 0;
                model.SignerId = entity.Signer != null ? entity.Signer.Id : 0;
                model.TravelRelatedAddition = entity.TravelRelatedAddition;
            }

            LoadDictionaries(model);
            return model;
        }

        public RosterModel GetRosterModel(RosterFiltersModel filters)
        {
            User current = UserDao.Load(AuthenticationService.CurrentUser.Id);
            RosterModel model = new RosterModel();

            model.IsCandidateInfoAvailable = (current.UserRole & (UserRole.Chief
                | UserRole.Director
                | UserRole.Manager
                | UserRole.OutsourcingManager
                | UserRole.PersonnelManager
                | UserRole.Security
                | UserRole.StaffManager)) > 0;
            model.IsBackgroundCheckAvailable = (current.UserRole & (UserRole.Security
                | UserRole.Chief
                | UserRole.Director
                | UserRole.Manager
                | UserRole.OutsourcingManager
                | UserRole.PersonnelManager
                | UserRole.StaffManager)) > 0;
            model.IsManagersAvailable = (current.UserRole & (UserRole.Chief
                | UserRole.Director
                | UserRole.Manager
                | UserRole.PersonnelManager
                | UserRole.OutsourcingManager)) > 0;
            model.IsPersonalManagersAvailable = (current.UserRole & (UserRole.Chief
                | UserRole.Director
                | UserRole.Manager
                | UserRole.OutsourcingManager
                | UserRole.PersonnelManager
                | UserRole.StaffManager)) > 0;

            if (filters == null)
            {                
                model.Roster = new List<CandidateDto>();
            }
            else
            {
                model.Roster = EmploymentCandidateDao.GetCandidates(current.Id,
                    current.UserRole,
                    filters != null ? filters.DepartmentId : 0,
                    filters != null ? (filters.StatusId.HasValue ? filters.StatusId.Value : 0) : 0,
                    filters != null ? filters.BeginDate : null,
                    filters != null ? filters.EndDate : null,
                    filters != null ? filters.UserName : null,
                    filters.SortBy,
                    filters.SortDescending);

                model.SortBy = filters.SortBy;
                model.SortDescending = filters.SortDescending;
            }

            LoadDictionaries(model);

            model.IsBulkChangeContractToIndefiniteAvailable = model.Roster.Any(x => x.IsChangeContractToIndefiniteAvailable);
            model.IsBulkApproveByManagerAvailable = model.Roster.Any(x => x.IsApproveByManagerAvailable);
            model.IsBulkApproveByHigherManagerAvailable = model.Roster.Any(x => x.IsApproveByHigherManagerAvailable);
                                    
            return model;
        }

        public CreateCandidateModel GetCreateCandidateModel()
        {
            var model = new CreateCandidateModel();
            model.IsOnBehalfOfManagerAvailable = (AuthenticationService.CurrentUser.UserRole & (UserRole.Manager | UserRole.Chief | UserRole.Director )) == 0 ? true : false;
            return model;
        }

        public PrintCreatedCandidateModel GetPrintCreatedCandidateModel(int id, out string error)
        {
            error = string.Empty;
            var user = userDao.Load(id);

            return new PrintCreatedCandidateModel { Login = user.Login, Password = user.Password };
        }

        public SignersModel GetSignersModel()
        {
            var model = new SignersModel
            {
                SignerToAddOrEdit = new SignerDto { Id = 0 },
                Signers = EmploymentSignersDao.LoadAllSorted().ToList()
                    .ConvertAll(x => new SignerDto { Id = x.Id, Name = x.Name, PreamblePartyTemplate = x.PreamblePartyTemplate })
                    .OrderBy(x => x.Name)
                    .ToList()
            };

            return model;
        }

        public PrintContractFormModel GetPrintContractFormModel(int userId)
        {
            EmploymentCandidate candidate = GetCandidate(userId);
            PrintContractFormModel model = new PrintContractFormModel();

            if (candidate.GeneralInfo != null)
            {
                model.EmployeeName = candidate.GeneralInfo.LastName + " " + candidate.GeneralInfo.FirstName + " " + candidate.GeneralInfo.Patronymic ?? string.Empty;
                model.EmployeeNameShortened = candidate.GeneralInfo.LastName + " " + candidate.GeneralInfo.FirstName[0] + ". "
                    + (string.IsNullOrEmpty(candidate.GeneralInfo.Patronymic) ? string.Empty : candidate.GeneralInfo.Patronymic[0].ToString() + ".");
            }

            if (candidate.Passport != null)
            {
                model.EmployeeAddress = candidate.Passport.ZipCode
                    + (!string.IsNullOrEmpty(candidate.Passport.Region) ? ", " + candidate.Passport.Region : string.Empty)
                    + (!string.IsNullOrEmpty(candidate.Passport.District) ? ", " + candidate.Passport.District : string.Empty)
                    + (!string.IsNullOrEmpty(candidate.Passport.City) ? ", " + candidate.Passport.City : string.Empty)
                    + (!string.IsNullOrEmpty(candidate.Passport.Street) ? ", " + candidate.Passport.Street : string.Empty)
                    + (!string.IsNullOrEmpty(candidate.Passport.StreetNumber) ? ", " + candidate.Passport.StreetNumber : string.Empty)
                    + (!string.IsNullOrEmpty(candidate.Passport.Building) ? " " + candidate.Passport.Building : string.Empty)
                    + (!string.IsNullOrEmpty(candidate.Passport.Apartment) ? ", кв. " + candidate.Passport.Apartment : string.Empty);
                model.PassportDateOfIssue = candidate.Passport.InternalPassportDateOfIssue;
                model.PassportIssuedBy = candidate.Passport.InternalPassportIssuedBy;
                model.PassportSeriesNumber = candidate.Passport.InternalPassportSeries + " " + candidate.Passport.InternalPassportNumber;
            }

            if (candidate.Managers != null)
            {
                model.Department = candidate.Managers.Department != null ? candidate.Managers.Department.Name : string.Empty;
                model.Position = candidate.Managers.Position != null ? candidate.Managers.Position.Name : string.Empty;
                model.ProbationaryPeriod = candidate.Managers.ProbationaryPeriod;
                model.WorkCity = candidate.Managers.WorkCity;
                model.IsSecondaryJob = candidate.Managers.IsSecondaryJob;
                model.Schedule = candidate.Managers.Schedule != null ? candidate.Managers.Schedule.Name : string.Empty;
                model.SalaryBasis = candidate.Managers.SalaryBasis;
            }

            if (candidate.PersonnelManagers != null)
            {
                model.ContractDate = candidate.PersonnelManagers.ContractDate;
                model.ContractEndDate = candidate.PersonnelManagers.ContractEndDate;
                model.EmploymentDate = candidate.PersonnelManagers.EmploymentDate;
                model.Number = candidate.PersonnelManagers.ContractNumber;
                if (candidate.PersonnelManagers.Signer != null)
                {
                    if (!string.IsNullOrEmpty(candidate.PersonnelManagers.Signer.Name))
                    {
                        string[] employerRepresentativeNameParts = candidate.PersonnelManagers.Signer.Name.Split(' ');
                        if (employerRepresentativeNameParts.Length >= 2)
                        {
                            model.EmployerRepresentativeNameShortened = employerRepresentativeNameParts[0];
                            for (int i = 1; i < employerRepresentativeNameParts.Length; i++)
                            {
                                model.EmployerRepresentativeNameShortened += string.Format(" {0}.", employerRepresentativeNameParts[i][0]);
                            }
                        }
                    }
                    
                    model.EmployerRepresentativeTemplate = 
                        !string.IsNullOrEmpty(candidate.PersonnelManagers.Signer.PreamblePartyTemplate)
                        ? candidate.PersonnelManagers.Signer.PreamblePartyTemplate
                        : string.Empty;
                }                
            }

            model.IsFixedTermContract = candidate.User.IsFixedTermContract;
            
            return model;
        }

        public PrintEmploymentOrderModel GetPrintEmploymentOrderModel(int userId)
        {
            EmploymentCandidate candidate = GetCandidate(userId);
            PrintEmploymentOrderModel model = new PrintEmploymentOrderModel();

            if (candidate.GeneralInfo != null)
            {
                model.EmployeeName = candidate.GeneralInfo.LastName + " " + candidate.GeneralInfo.FirstName + " " + candidate.GeneralInfo.Patronymic ?? string.Empty;

            }

            if (candidate.Managers != null)
            {
                model.PersonalAddition = candidate.Managers.PersonalAddition;
                model.PositionAddition = candidate.Managers.PositionAddition;
                model.Conditions = candidate.Managers.EmploymentConditions;
                model.Department = candidate.Managers.Department != null ? candidate.Managers.Department.Name : string.Empty;
                model.Position = candidate.Managers.Position != null ? candidate.Managers.Position.Name : string.Empty;
                model.ProbationaryPeriod = candidate.Managers.ProbationaryPeriod;
                model.SalaryBasis = candidate.Managers.SalaryBasis;
            }

            if (candidate.PersonnelManagers != null)
            {
                model.AreaAddition = candidate.PersonnelManagers.AreaAddition;
                model.CompetenceAddition = candidate.PersonnelManagers.CompetenceAddition;
                model.ContractDate = candidate.PersonnelManagers.ContractDate;
                model.ContractNumber = candidate.PersonnelManagers.ContractNumber;
                model.EmploymentDate = candidate.PersonnelManagers.EmploymentDate;
                model.FrontOfficeExperienceAddition = candidate.PersonnelManagers.FrontOfficeExperienceAddition;
                model.NorthernAreaAddition = candidate.PersonnelManagers.NorthernAreaAddition;
                model.OrderDate = candidate.PersonnelManagers.EmploymentOrderDate;
                model.OrderNumber = candidate.PersonnelManagers.EmploymentOrderNumber;
                model.TravelRelatedAddition = candidate.PersonnelManagers.TravelRelatedAddition;
            }

            return model;
        }

        public IList<CandidateDto> GetPrintRosterModel(RosterFiltersModel filters, int? sortBy, bool? sortDescending)
        {
            User current = UserDao.Load(AuthenticationService.CurrentUser.Id);
            IList<CandidateDto> model = null;

            if (filters == null)
            {
                model = new List<CandidateDto>();
            }
            else
            {
                model = EmploymentCandidateDao.GetCandidates(current.Id,
                    current.UserRole,
                    filters != null ? filters.DepartmentId : 0,
                    filters != null ? (filters.StatusId.HasValue ? filters.StatusId.Value : 0) : 0,
                    filters != null ? filters.BeginDate : null,
                    filters != null ? filters.EndDate : null,
                    filters != null ? filters.UserName : null,
                    filters.SortBy,
                    filters.SortDescending);
            }

            return model;
        }

        #endregion        

        #region LoadDictionaries

        public void LoadDictionaries(GeneralInfoModel model)
        {
            model.CitizenshipItems = GetCountries();
            model.InsuredPersonTypeItems = GetInsuredPersonTypes();
            model.DisabilityDegrees = GetDisabilityDegrees();
            model.StatusItems = GetStatuses();
        }
        public void LoadDictionaries(PassportModel model)
        {
            model.DocumentTypeItems = GetDocumentTypes();
        }
        public void LoadDictionaries(EducationModel model)
        {

        }
        public void LoadDictionaries(FamilyModel model)
        {

        }
        public void LoadDictionaries(MilitaryServiceModel model)
        {
            model.RankItems = GetRanks();
            model.RegistrationExpirationItems = GetRegistrationExpirations();
            model.PersonnelCategoryItems = GetPersonnelCategories();
            model.PersonnelTypeItems = GetPersonnelTypes();
            model.ConscriptionStatusItems = GetConscriptionStatuses();
        }
        public void LoadDictionaries(ExperienceModel model)
        {

        }
        public void LoadDictionaries(ContactsModel model)
        {

        }
        public void LoadDictionaries(BackgroundCheckModel model)
        {
            model.ApprovalStatuses = GetApprovalStatuses();
        }
        public void LoadDictionaries(OnsiteTrainingModel model)
        {
            //
        }
        public void LoadDictionaries(ManagersModel model)
        {
            model.PositionItems = GetPositions();
            model.Schedules = GetSchedules();

            model.ApprovalStatuses = GetApprovalStatuses();
        }
        public void LoadDictionaries(PersonnelManagersModel model)
        {
            model.PersonalAccountContractors = GetPersonalAccountContractors();
            model.AccessGroups = GetAccessGroups();
            model.Signers = GetSigners();
        }
        public void LoadDictionaries(RosterModel model)
        {
            model.Statuses = GetEmploymentStatuses();
        }
        public void LoadDictionaries(SignersModel model)
        {

        }

        public IEnumerable<SelectListItem> GetCountries()
        {
            return CountryDao.LoadAllSorted().ToList().ConvertAll(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).OrderBy(x => x.Value);
        }

        public IEnumerable<SelectListItem> GetInsuredPersonTypes()
        {
            return InsuredPersonTypeDao.LoadAllSorted().ToList().ConvertAll(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).OrderBy(x => Int32.Parse(x.Value));
        }

        public IEnumerable<SelectListItem> GetDisabilityDegrees()
        {
            return DisabilityDegreeDao.LoadAllSorted().ToList().ConvertAll(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).OrderBy(x => Int32.Parse(x.Value));
        }

        public IEnumerable<SelectListItem> GetStatuses()
        {
            return new List<SelectListItem>
            {
                new SelectListItem {Text = "Резидент", Value = "0"},
                new SelectListItem {Text = "Нерезидент", Value = "1"},
                new SelectListItem {Text = "Высококвалифицированный иностранный специалист", Value = "2"},
                new SelectListItem {Text = "Участник программы по переселению соотечественников", Value = "3"},
                new SelectListItem {Text = "Член экипажа судна под флагом РФ", Value = "4"}
            };
        }

        public IEnumerable<SelectListItem> GetDocumentTypes()
        {
            return DocumentTypeDao.LoadAllSorted().ToList().ConvertAll(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).OrderBy(x => x.Value);
        }

        public IEnumerable<SelectListItem> GetRanks()
        {
            return new List<SelectListItem>
            {
                new SelectListItem {Text = "Подлежит призыву", Value = "0"},
                new SelectListItem {Text = "Рядовой", Value = "1"},
                new SelectListItem {Text = "Матрос", Value = "2"},
                new SelectListItem {Text = "Ефрейтор", Value = "3"},
                new SelectListItem {Text = "Ст. матрос", Value = "4"},
                new SelectListItem {Text = "Мл. сержант", Value = "5"},
                new SelectListItem {Text = "Старшина 2-й статьи", Value = "6"},
                new SelectListItem {Text = "Сержант", Value = "7"},
                new SelectListItem {Text = "Старшина 1-й статьи", Value = "8"},
                new SelectListItem {Text = "Ст. сержант", Value = "9"},
                new SelectListItem {Text = "Гл. старшина", Value = "10"},
                new SelectListItem {Text = "Старшина", Value = "11"},
                new SelectListItem {Text = "Гл. корабельный старшина", Value = "12"},
                new SelectListItem {Text = "Прапорщик", Value = "13"},
                new SelectListItem {Text = "Мичман", Value = "14"},
                new SelectListItem {Text = "Ст. прапорщик", Value = "15"},
                new SelectListItem {Text = "Ст. мичман", Value = "16"},
                new SelectListItem {Text = "Мл. лейтенант", Value = "17"},
                new SelectListItem {Text = "Лейтенант", Value = "18"},
                new SelectListItem {Text = "Ст. лейтенант", Value = "19"},
                new SelectListItem {Text = "Капитан", Value = "20"},
                new SelectListItem {Text = "Капитан-лейтенант", Value = "21"},
                new SelectListItem {Text = "Майор", Value = "22"},
                new SelectListItem {Text = "Капитан 3 ранга", Value = "23"},
                new SelectListItem {Text = "Подполковник", Value = "24"},
                new SelectListItem {Text = "Капитан 2 ранга", Value = "25"},
                new SelectListItem {Text = "Полковник", Value = "26"},
                new SelectListItem {Text = "Капитан 1 ранга", Value = "27"},
                new SelectListItem {Text = "Генерал-майор", Value = "28"},
                new SelectListItem {Text = "Контр-адмирал", Value = "29"},
                new SelectListItem {Text = "Генерал-лейтенант", Value = "30"},
                new SelectListItem {Text = "Вице-адмирал", Value = "31"},
                new SelectListItem {Text = "Генерал-полковник", Value = "32"},
                new SelectListItem {Text = "Адмирал", Value = "33"},
                new SelectListItem {Text = "Генерал армии", Value = "34"},
                new SelectListItem {Text = "Адмирал флота", Value = "35"},
                new SelectListItem {Text = "Маршал РФ", Value = "36"}
            };
        }

        public IEnumerable<SelectListItem> GetRegistrationExpirations()
        {
            return new List<SelectListItem>
            {
                new SelectListItem {Text = "-", Value = "0"},
                new SelectListItem {Text = "Снят с воинского учета по возрасту", Value = "1"},
                new SelectListItem {Text = "Снят с воинского учета по состоянию здоровья", Value = "2"}
            };
        }

        public IEnumerable<SelectListItem> GetPersonnelCategories()
        {
            return new List<SelectListItem>
            {
                new SelectListItem {Text = "", Value = "0"},
                new SelectListItem {Text = "Руководители", Value = "1"},
                new SelectListItem {Text = "Специальности", Value = "2"},
                new SelectListItem {Text = "Другие служащие", Value = "3"},
                new SelectListItem {Text = "Рабочие", Value = "4"}
            };
        }

        public IEnumerable<SelectListItem> GetPersonnelTypes()
        {
            return new List<SelectListItem>
            {
                new SelectListItem {Text = "", Value = "0"},
                new SelectListItem {Text = "Офицеры", Value = "1"},
                new SelectListItem {Text = "Прочие (прапорщики, солдаты, мичманы, сержанты, матросы...)", Value = "2"}
            };
        }

        public IEnumerable<SelectListItem> GetConscriptionStatuses()
        {
            return new List<SelectListItem>
            {
                new SelectListItem {Text = "Не подлежит", Value = "1"},
                new SelectListItem {Text = "Подлежит", Value = "2"},
                new SelectListItem {Text = "Ограниченно годен", Value = "3"}
            };
        }

        public IEnumerable<SelectListItem> GetPositions()
        {
            return PositionDao.LoadAllSorted().ToList().ConvertAll(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).OrderBy(x => x.Value);
        }

        public IEnumerable<SelectListItem> GetSchedules()
        {
            return ScheduleDao.LoadAllSorted().ToList().ConvertAll(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).OrderBy(x => Int32.Parse(x.Value));
        }

        public IEnumerable<SelectListItem> GetPersonalAccountContractors()
        {
            return PersonalAccountContractorDao.LoadAllSorted().ToList().ConvertAll(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).OrderBy(x => x.Value);
        }

        public IEnumerable<SelectListItem> GetAccessGroups()
        {
            return AccessGroupDao.LoadAllSorted().ToList().ConvertAll(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).OrderBy(x => x.Value);
        }

        public IEnumerable<SelectListItem> GetSigners()
        {
            return EmploymentSignersDao.LoadAllSorted().ToList().ConvertAll(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).OrderBy(x => x.Text);
        }        

        public IEnumerable<SelectListItem> GetEmploymentStatuses()
        {
            return new List<SelectListItem>
            {
                new SelectListItem {Text = "Ожидает согласование СБ", Value = "1"},
                new SelectListItem {Text = "Обучение", Value = "2"},
                new SelectListItem {Text = "Ожидает согласование руководителем", Value = "3"},
                new SelectListItem {Text = "Ожидает согласование вышестоящим руководителем", Value = "4"},
                new SelectListItem {Text = "Оформление Кадры", Value = "6"},
                new SelectListItem {Text = "Завершено", Value = "7"},
                new SelectListItem {Text = "Выгружено в 1С", Value = "8"}
            };
        }

        public IEnumerable<SelectListItem> GetApprovalStatuses()
        {
            return new List<SelectListItem>
            {
                new SelectListItem {Text = "Согласен на прием", Value = "true"},
                new SelectListItem {Text = "Отклонить прием", Value = "false"}
            };
        }

        #endregion

        #region Process Saving

        public int? CreateCandidate(CreateCandidateModel model, out string error)
        {
            error = string.Empty;
            IUser current = AuthenticationService.CurrentUser;
            User currentUser = UserDao.Load(current.Id);
            User onBehalfOfManager = model.OnBehalfOfManagerId.HasValue ? UserDao.Load(model.OnBehalfOfManagerId.Value) : null;
            Department department = DepartmentDao.Load(model.DepartmentId);

            if ((currentUser.UserRole & (UserRole.Manager | UserRole.Chief | UserRole.Director)) == 0 && onBehalfOfManager == null)
            {
                error = "Необходимо выбрать руководителя, от имени которого Вы добавляете кандидата.";
                return null;
            }

            // Проверка прав руководителя на подразделение
            if (!IsUserManagerForDepartment(department, onBehalfOfManager == null ? currentUser : onBehalfOfManager))
            {
                error = "Отсутствуют права на выбранное подразделение.";
                return null;
            }
            
            User newUser = new User
            {
                Login = string.Empty,
                Password = AppointmentBl.CreatePassword(AppointmentBl.PasswordLength),
                IsFirstTimeLogin = true,
                IsActive = true,
                IsNew = true,
                Name = string.Empty,
                RoleId = (int)UserRole.Candidate,
                Department = department,
                GivesCredit = false,
                IsMainManager = false,
                IsFixedTermContract = model.IsFixedTermContract
            };

            EmploymentCandidate candidate = new EmploymentCandidate
            {
                User = newUser,
                AppointmentCreator = onBehalfOfManager != null ? onBehalfOfManager : currentUser,
                QuestionnaireDate = DateTime.Now                
            };
            
            EmploymentCommonDao.SaveAndFlush(candidate);

            candidate.User.Login = "c" + candidate.Id.ToString();
            candidate.User.Name = candidate.User.Login;

            // Create blank employment pages
            candidate.GeneralInfo = new GeneralInfo
            {
                AgreedToPersonalDataProcessing = false,
                Candidate = candidate,
                IsPatronymicAbsent = false,
                IsFinal = false
            };
            candidate.Passport = new Passport
            {
                Candidate = candidate,
                IsFinal = false
            };
            candidate.Education = new Education
            {
                Candidate = candidate,
                IsFinal = false
            };
            candidate.Family = new Family
            {
                Candidate = candidate,
                IsFinal = false
            };
            candidate.MilitaryService = new MilitaryService
            {
                Candidate = candidate,
                IsAssigned = false,
                IsLiableForMilitaryService = false,
                IsReserved = false,
                IsFinal = false
            };

            candidate.Experience = new Experience
            {
                Candidate = candidate,
                IsFinal = false
            };

            candidate.Contacts = new Contacts
            {
                Candidate = candidate,
                IsFinal = false
            };

            candidate.BackgroundCheck = new BackgroundCheck
            {
                Candidate = candidate,
                IsReadyForBusinessTrips = false,
                IsFinal = false,
                IsApprovalSkipped = false
            };

            candidate.OnsiteTraining = new OnsiteTraining
            {
                Candidate = candidate,
                IsFinal = false
            };

            candidate.Managers = new Managers
            {
                Candidate = candidate,
                IsFront = false,
                IsLiable = false
            };

            candidate.PersonnelManagers = new PersonnelManagers
            {
                Candidate = candidate
            };

            EmploymentCommonDao.SaveAndFlush(candidate);

            return candidate.User.Id;
        }
                
        public bool ProcessSaving<TVM, TE>(TVM model, out string error)
            where TVM : AbstractEmploymentModel
            where TE : new()
        {
            error = string.Empty;
            User user = null;
            IUser current = AuthenticationService.CurrentUser;

            int id = EmploymentCommonDao.GetDocumentId<TE>(model.UserId);
            TE entity = EmploymentCommonDao.GetEntityById<TE>(id);

            if (entity == null)
            {
                entity = new TE();
            }

            try
            {
                user = UserDao.Load(model.UserId);

                if (!SetEntity<TVM, TE>(entity, model, out error))
                {
                    return false;
                }
                
                EmploymentCommonDao.SaveOrUpdateDocument<TE>(entity);
                SaveAttachments<TVM>(model);
            }
            catch (Exception)
            {

            }
            finally
            {

            }

            return false;
        }

        public bool ProcessSaving(ApplicationLetterModel model, out string error)
        {            
            SaveAttachments<ApplicationLetterModel>(model);
            int userId = model.UserId != 0 ? model.UserId : AuthenticationService.CurrentUser.Id;

            EmploymentCandidate candidate = GetCandidate(userId);
            candidate.Status = EmploymentStatus.PENDING_APPROVAL_BY_MANAGER;
            EmploymentCommonDao.SaveOrUpdateDocument<EmploymentCandidate>(candidate);

            error = string.Empty;
            return true;
        }

        public bool ProcessSaving(SignerDto itemToSave, out string error)
        {
            error = string.Empty;

            Signer entity = EmploymentSignersDao.Get(itemToSave.Id) ?? new Signer();
            entity.Name = itemToSave.Name;
            entity.PreamblePartyTemplate = itemToSave.PreamblePartyTemplate;

            EmploymentSignersDao.SaveAndFlush(entity);

            return true;
        }

        #region SaveAttachments
        public void SaveAttachments<TVM>(TVM viewModel)
            where TVM : AbstractEmploymentModel
        {
            int candidateId = GetCandidate(viewModel.UserId != 0 ? viewModel.UserId : AuthenticationService.CurrentUser.Id).Id;

            switch (viewModel.GetType().Name)
            {
                case "GeneralInfoModel":
                    SaveGeneralInfoAttachments(viewModel as GeneralInfoModel, candidateId);
                    break;
                case "PassportModel":
                    SavePassportAttachments(viewModel as PassportModel, candidateId);
                    break;

                case "FamilyModel":
                    SaveFamilyAttachments(viewModel as FamilyModel, candidateId);
                    break;
                case "MilitaryServiceModel":
                    SaveMilitaryServiceAttachments(viewModel as MilitaryServiceModel, candidateId);
                    break;
                case "ExperienceModel":
                    SaveExperienceAttachments(viewModel as ExperienceModel, candidateId);
                    break;
                case "BackgroundCheckModel":
                    SaveBackgroundCheckAttachments(viewModel as BackgroundCheckModel, candidateId);
                    break;
                case "ApplicationLetterModel":
                    SaveApplicationLetterAttachments(viewModel as ApplicationLetterModel, candidateId);
                    break;
                default:
                    break;
            }
        }

        protected void SaveGeneralInfoAttachments(GeneralInfoModel model, int candidateId)
        {
            if (model.PhotoFile != null)
            {
                UploadFileDto fileDto = GetFileContext(model.PhotoFile);
                string fileName = string.Empty;
                SaveAttachment(candidateId, model.PhotoAttachmentId, fileDto, RequestAttachmentTypeEnum.Photo, out fileName);
            }
            if (model.INNScanFile != null)
            {
                UploadFileDto fileDto = GetFileContext(model.INNScanFile);
                string fileName = string.Empty;
                //int? attachmentId = 
                SaveAttachment(candidateId, model.INNScanAttachmentId, fileDto, RequestAttachmentTypeEnum.INNScan, out fileName);
            }
            if (model.SNILSScanFile != null)
            {
                UploadFileDto fileDto = GetFileContext(model.INNScanFile);
                string fileName = string.Empty;
                SaveAttachment(candidateId, model.SNILSScanAttachmentId, fileDto, RequestAttachmentTypeEnum.SNILSScan, out fileName);
            }
            if (model.DisabilityCertificateScanFile != null)
            {
                UploadFileDto fileDto = GetFileContext(model.DisabilityCertificateScanFile);
                string fileName = string.Empty;
                SaveAttachment(candidateId, model.DisabilityCertificateScanAttachmentId, fileDto, RequestAttachmentTypeEnum.DisabilityCertificateScan, out fileName);
            }
        }

        protected void SavePassportAttachments(PassportModel model, int candidateId)
        {
            if (model.InternalPassportScanFile != null)
            {
                UploadFileDto fileDto = GetFileContext(model.InternalPassportScanFile);
                string fileName = string.Empty;
                SaveAttachment(candidateId, model.InternalPassportScanAttachmentId, fileDto, RequestAttachmentTypeEnum.InternalPassportScan, out fileName);
            }
        }

        // TODO: EMPL SaveEducationAttachments

        protected void SaveFamilyAttachments(FamilyModel model, int candidateId)
        {
            if (model.MarriageCertificateScanFile != null)
            {
                UploadFileDto fileDto = GetFileContext(model.MarriageCertificateScanFile);
                string fileName = string.Empty;
                SaveAttachment(candidateId, model.MarriageCertificateScanAttachmentId, fileDto, RequestAttachmentTypeEnum.MarriageCertificateScan, out fileName);
            }
            // TODO: EMPL Загрузка сканов свидетельств о рождении
        }

        protected void SaveMilitaryServiceAttachments(MilitaryServiceModel model, int candidateId)
        {
            if (model.MilitaryCardScanFile != null)
            {
                UploadFileDto fileDto = GetFileContext(model.MilitaryCardScanFile);
                string fileName = string.Empty;
                SaveAttachment(candidateId, model.MilitaryCardScanAttachmentId, fileDto, RequestAttachmentTypeEnum.MilitaryCardScan, out fileName);
            }
            if (model.MobilizationTicketScanFile != null)
            {
                UploadFileDto fileDto = GetFileContext(model.MobilizationTicketScanFile);
                string fileName = string.Empty;
                SaveAttachment(candidateId, model.MobilizationTicketScanAttachmentId, fileDto, RequestAttachmentTypeEnum.MobilizationTicketScan, out fileName);
            }
        }

        protected void SaveExperienceAttachments(ExperienceModel model, int candidateId)
        {
            if (model.WorkBookScanFile != null)
            {
                UploadFileDto fileDto = GetFileContext(model.WorkBookScanFile);
                string fileName = string.Empty;
                SaveAttachment(candidateId, model.WorkBookScanAttachmentId, fileDto, RequestAttachmentTypeEnum.WorkbookScan, out fileName);
            }
            if (model.WorkBookSupplementScanFile != null)
            {
                UploadFileDto fileDto = GetFileContext(model.WorkBookSupplementScanFile);
                string fileName = string.Empty;
                SaveAttachment(candidateId, model.WorkBookSupplementScanAttachmentId, fileDto, RequestAttachmentTypeEnum.WorkbookSupplementScan, out fileName);
            }
        }

        protected void SaveBackgroundCheckAttachments(BackgroundCheckModel model, int candidateId)
        {
            if (model.PersonalDataProcessingScanFile != null)
            {
                UploadFileDto fileDto = GetFileContext(model.PersonalDataProcessingScanFile);
                string fileName = string.Empty;
                SaveAttachment(candidateId, model.PersonalDataProcessingScanAttachmentId, fileDto, RequestAttachmentTypeEnum.PersonalDataProcessingScan, out fileName);
            }
            if (model.InfoValidityScanFile != null)
            {
                UploadFileDto fileDto = GetFileContext(model.InfoValidityScanFile);
                string fileName = string.Empty;
                SaveAttachment(candidateId, model.InfoValidityScanAttachmentId, fileDto, RequestAttachmentTypeEnum.InfoValidityScan, out fileName);
            }
        }

        protected void SaveApplicationLetterAttachments(ApplicationLetterModel model, int candidateId)
        {
            if (model.ApplicationLetterScanFile != null)
            {
                UploadFileDto fileDto = GetFileContext(model.ApplicationLetterScanFile);
                string fileName = string.Empty;
                SaveAttachment(candidateId, model.ApplicationLetterScanAttachmentId, fileDto, RequestAttachmentTypeEnum.ApplicationLetterScan, out fileName);
            }
        } 
        #endregion

        #endregion        

        #region SetEntity

        protected bool SetEntity<TVM, TE>(TE entity, TVM viewModel, out string error)
        {
            switch (entity.GetType().Name)
            {
                case "GeneralInfo": return SetGeneralInfoEntity(entity as GeneralInfo, viewModel as GeneralInfoModel, out error);
                case "Passport": return SetPassportEntity(entity as Passport, viewModel as PassportModel, out error);
                case "Education": return SetEducationEntity(entity as Education, viewModel as EducationModel, out error);
                case "Family": return SetFamilyEntity(entity as Family, viewModel as FamilyModel, out error);
                case "MilitaryService": return SetMilitaryServiceEntity(entity as MilitaryService, viewModel as MilitaryServiceModel, out error);
                case "Experience": return SetExperienceEntity(entity as Experience, viewModel as ExperienceModel, out error);
                case "Contacts": return SetContactsEntity(entity as Contacts, viewModel as ContactsModel, out error);
                case "BackgroundCheck": return SetBackgroundCheckEntity(entity as BackgroundCheck, viewModel as BackgroundCheckModel, out error);
                //case "OnsiteTraining": return SetOnsiteTrainingEntity(entity as OnsiteTraining, viewModel as OnsiteTrainingModel, out error);
                case "Managers": return SetManagersEntity(entity as Managers, viewModel as ManagersModel, out error);
                case "PersonnelManagers": return SetPersonnelManagersEntity(entity as PersonnelManagers, viewModel as PersonnelManagersModel, out error);
                default:
                    error = "Неизвестный тип документа";
                    return false;
            }            
        }

        protected bool SetGeneralInfoEntity(GeneralInfo entity, GeneralInfoModel viewModel, out string error)
        {
            error = string.Empty;

            if (viewModel.AgreedToPersonalDataProcessing)
            {
                entity.AgreedToPersonalDataProcessing = viewModel.AgreedToPersonalDataProcessing;
            }
            else
            {
                error = StrNotAgreedToPersonalDataProcessing;
                return false;
            }

            #region SetEntityProps
            entity.Candidate = GetCandidate(viewModel.UserId);
            entity.Candidate.GeneralInfo = entity;
            entity.Citizenship = CountryDao.Load(viewModel.CitizenshipId);
            entity.CityOfBirth = viewModel.CityOfBirth;
            entity.DateOfBirth = viewModel.DateOfBirth;

            entity.DisabilityCertificateDateOfIssue = viewModel.DisabilityCertificateDateOfIssue;
            entity.DisabilityCertificateExpirationDate = viewModel.DisabilityCertificateExpirationDate;
            entity.DisabilityCertificateNumber = viewModel.DisabilityCertificateNumber;
            entity.DisabilityCertificateSeries = viewModel.DisabilityCertificateSeries;
            entity.DisabilityDegree = viewModel.DisabilityDegreeId.HasValue ? DisabilityDegreeDao.Load(viewModel.DisabilityDegreeId.Value) : null;

            entity.DistrictOfBirth = viewModel.DistrictOfBirth;
            entity.FirstName = viewModel.FirstName;

            if (entity.ForeignLanguages == null)
            {
                entity.ForeignLanguages = new List<ForeignLanguage>();
            }
            if (viewModel.ForeignLanguages != null && viewModel.ForeignLanguages.Count > entity.ForeignLanguages.Count)
            {
                int lastIndex = viewModel.ForeignLanguages.Count - 1;
                entity.ForeignLanguages.Add(new ForeignLanguage
                {
                    LanguageName = viewModel.ForeignLanguages[lastIndex].LanguageName,
                    Level = viewModel.ForeignLanguages[lastIndex].Level
                });
            }

            entity.INN = viewModel.INN;
            entity.InsuredPersonType = viewModel.InsuredPersonTypeId.HasValue ? InsuredPersonTypeDao.Load(viewModel.InsuredPersonTypeId.Value) : null;
            entity.IsFinal = !viewModel.IsDraft;
            entity.IsMale = viewModel.IsMale;
            entity.IsPatronymicAbsent = viewModel.IsPatronymicAbsent;
            entity.LastName = viewModel.LastName;

            if (entity.NameChanges == null)
            {
                entity.NameChanges = new List<NameChange>();
            }
            if (viewModel.NameChanges != null && viewModel.NameChanges.Count > entity.NameChanges.Count)
            {
                int lastIndex = viewModel.NameChanges.Count - 1;
                entity.NameChanges.Add(new NameChange
                {
                    Date = viewModel.NameChanges[lastIndex].Date,
                    Place = viewModel.NameChanges[lastIndex].Place,
                    PreviousName = viewModel.NameChanges[lastIndex].PreviousName,
                    Reason = viewModel.NameChanges[lastIndex].Reason
                });
            }
            entity.Patronymic = viewModel.IsPatronymicAbsent ? String.Empty : viewModel.Patronymic;
            entity.RegionOfBirth = viewModel.RegionOfBirth;
            entity.SNILS = viewModel.SNILS;
            entity.Status = viewModel.StatusId;
            #endregion

            return true;
        }

        protected bool SetPassportEntity(Passport entity, PassportModel viewModel, out string error)
        {
            error = string.Empty;

            if (entity.Candidate.GeneralInfo == null || !entity.Candidate.GeneralInfo.AgreedToPersonalDataProcessing)
            {
                error = StrNotAgreedToPersonalDataProcessing;
                return false;
            }

            #region SetEntityProps
            entity.Apartment = viewModel.Apartment;
            entity.Building = viewModel.Building;
            entity.Candidate = GetCandidate(viewModel.UserId);
            entity.Candidate.Passport = entity;
            entity.City = viewModel.City;
            entity.District = viewModel.District;
            entity.DocumentType = DocumentTypeDao.Load(viewModel.DocumentTypeId);
            entity.InternalPassportDateOfIssue = viewModel.InternalPassportDateOfIssue;
            entity.InternalPassportIssuedBy = viewModel.InternalPassportIssuedBy;
            entity.InternalPassportNumber = viewModel.InternalPassportNumber;
            entity.InternalPassportSeries = viewModel.InternalPassportSeries;
            entity.InternalPassportSubdivisionCode = viewModel.InternalPassportSubdivisionCode;
            entity.InternationalPassportDateOfIssue = viewModel.InternationalPassportDateOfIssue;
            entity.InternationalPassportIssuedBy = viewModel.InternationalPassportIssuedBy;
            entity.InternationalPassportNumber = viewModel.InternationalPassportNumber;
            entity.InternationalPassportSeries = viewModel.InternationalPassportSeries;
            entity.IsFinal = !viewModel.IsDraft;
            entity.Region = viewModel.Region;
            entity.RegistrationDate = viewModel.RegistrationDate;
            entity.Street = viewModel.Street;
            entity.StreetNumber = viewModel.StreetNumber;
            entity.ZipCode = viewModel.ZipCode;
            #endregion

            return true;
        }

        protected bool SetEducationEntity(Education entity, EducationModel viewModel, out string error)
        {
            error = string.Empty;

            if (entity.Candidate.GeneralInfo == null || !entity.Candidate.GeneralInfo.AgreedToPersonalDataProcessing)
            {
                error = StrNotAgreedToPersonalDataProcessing;
                return false;
            }

            #region SetEntityProps
            entity.Candidate = GetCandidate(viewModel.UserId);
            entity.Candidate.Education = entity;

            if (entity.Certifications == null)
            {
                entity.Certifications = new List<Certification>();
            }
            if (viewModel.Certifications != null && viewModel.Certifications.Count > entity.Certifications.Count)
            {
                int lastIndex = viewModel.Certifications.Count - 1;
                entity.Certifications.Add(new Certification
                {
                    CertificateDateOfIssue = viewModel.Certifications[lastIndex].CertificateDateOfIssue,
                    CertificateNumber = viewModel.Certifications[lastIndex].CertificateNumber,
                    CertificationDate = viewModel.Certifications[lastIndex].CertificationDate,
                    InitiatingOrder = viewModel.Certifications[lastIndex].InitiatingOrder
                });
            }

            if (entity.HigherEducationDiplomas == null)
            {
                entity.HigherEducationDiplomas = new List<HigherEducationDiploma>();
            }
            if (viewModel.HigherEducationDiplomas != null && viewModel.HigherEducationDiplomas.Count > entity.HigherEducationDiplomas.Count)
            {
                int lastIndex = viewModel.HigherEducationDiplomas.Count - 1;
                entity.HigherEducationDiplomas.Add(new HigherEducationDiploma
                {
                    AdmissionYear = viewModel.HigherEducationDiplomas[lastIndex].AdmissionYear,
                    Department = viewModel.HigherEducationDiplomas[lastIndex].Department,
                    GraduationYear = viewModel.HigherEducationDiplomas[lastIndex].GraduationYear,
                    IssuedBy = viewModel.HigherEducationDiplomas[lastIndex].IssuedBy,
                    Number = viewModel.HigherEducationDiplomas[lastIndex].Number,
                    Profession = viewModel.HigherEducationDiplomas[lastIndex].Profession,
                    Qualification = viewModel.HigherEducationDiplomas[lastIndex].Qualification,
                    Series = viewModel.HigherEducationDiplomas[lastIndex].Series,
                    Speciality = viewModel.HigherEducationDiplomas[lastIndex].Speciality
                });
            }

            if (entity.PostGraduateEducationDiplomas == null)
            {
                entity.PostGraduateEducationDiplomas = new List<PostGraduateEducationDiploma>();
            }
            if (viewModel.PostGraduateEducationDiplomas != null && viewModel.PostGraduateEducationDiplomas.Count > entity.PostGraduateEducationDiplomas.Count)
            {
                int lastIndex = viewModel.PostGraduateEducationDiplomas.Count - 1;
                entity.PostGraduateEducationDiplomas.Add(new PostGraduateEducationDiploma
                {
                    AdmissionYear = viewModel.PostGraduateEducationDiplomas[lastIndex].AdmissionYear,
                    GraduationYear = viewModel.PostGraduateEducationDiplomas[lastIndex].GraduationYear,
                    IssuedBy = viewModel.PostGraduateEducationDiplomas[lastIndex].IssuedBy,
                    Number = viewModel.PostGraduateEducationDiplomas[lastIndex].Number,
                    Series = viewModel.PostGraduateEducationDiplomas[lastIndex].Series,
                    Speciality = viewModel.PostGraduateEducationDiplomas[lastIndex].Speciality
                });
            }

            if (entity.Training == null)
            {
                entity.Training = new List<Training>();
            }
            if (viewModel.Training != null && viewModel.Training.Count > entity.Training.Count)
            {
                int lastIndex = viewModel.Training.Count - 1;
                entity.Training.Add(new Training
                {
                    BeginningDate = viewModel.Training[lastIndex].BeginningDate,
                    CertificateIssuedBy = viewModel.Training[lastIndex].CertificateIssuedBy,
                    EndDate = viewModel.Training[lastIndex].EndDate,
                    Number = viewModel.Training[lastIndex].Number,
                    Series = viewModel.Training[lastIndex].Series,
                    Speciality = viewModel.Training[lastIndex].Speciality
                });
            }

            entity.IsFinal = !viewModel.IsDraft;
            #endregion

            return true;
        }

        protected bool SetFamilyEntity(Family entity, FamilyModel viewModel, out string error)
        {
            error = string.Empty;

            if (entity.Candidate.GeneralInfo == null || !entity.Candidate.GeneralInfo.AgreedToPersonalDataProcessing)
            {
                error = StrNotAgreedToPersonalDataProcessing;
                return false;
            }

            #region SetEntityProps
            entity.Candidate = GetCandidate(viewModel.UserId);
            entity.Candidate.Family = entity;

            entity.Cohabitants = viewModel.Cohabitants;

            if (entity.FamilyMembers == null)
            {
                entity.FamilyMembers = new List<FamilyMember>();
            }

            // Если информация об отце заносится в БД впервые
            if (viewModel.Father != null && !entity.FamilyMembers.Any<FamilyMember>(x => x.RelationshipId == FamilyRelationship.FATHER))
            {
                FamilyMember father = SetFamilyMember(new FamilyMember(), FamilyRelationship.FATHER, viewModel.Father);
                entity.FamilyMembers.Add(father);
            }
            // Если требуется обновление информации об отце
            else if (viewModel.Father != null)
            {
                FamilyMember father = GetFamilyMemberByRelationship(entity.FamilyMembers, FamilyRelationship.FATHER);
                SetFamilyMember(father, FamilyRelationship.FATHER, viewModel.Father);
            }

            if (viewModel.Mother != null && !entity.FamilyMembers.Any<FamilyMember>(x => x.RelationshipId == FamilyRelationship.MOTHER))
            {
                FamilyMember mother = SetFamilyMember(new FamilyMember(), FamilyRelationship.MOTHER, viewModel.Mother);
                entity.FamilyMembers.Add(mother);
            }
            else if (viewModel.Mother != null)
            {
                FamilyMember mother = GetFamilyMemberByRelationship(entity.FamilyMembers, FamilyRelationship.MOTHER);
                SetFamilyMember(mother, FamilyRelationship.MOTHER, viewModel.Mother);
            }

            if (viewModel.IsMarried && !entity.FamilyMembers.Any<FamilyMember>(x => x.RelationshipId == FamilyRelationship.SPOUSE))
            {
                FamilyMember spouse = SetFamilyMember(new FamilyMember(), FamilyRelationship.SPOUSE, viewModel.Spouse);
                entity.FamilyMembers.Add(spouse);
            }
            else if (viewModel.Spouse != null)
            {
                FamilyMember spouse = GetFamilyMemberByRelationship(entity.FamilyMembers, FamilyRelationship.SPOUSE);
                SetFamilyMember(spouse, FamilyRelationship.SPOUSE, viewModel.Spouse);
            }

            if (viewModel.Children != null && viewModel.Children.Count > entity.FamilyMembers.Where<FamilyMember>(x => x.RelationshipId == FamilyRelationship.CHILD).Count())
            {
                int lastIndex = viewModel.Children.Count - 1;
                entity.FamilyMembers.Add(SetFamilyMember(new FamilyMember(), FamilyRelationship.CHILD, viewModel.Children[lastIndex]));
            }

            entity.IsFinal = !viewModel.IsDraft; 
            #endregion

            return true;
        }
        
        protected bool SetMilitaryServiceEntity(MilitaryService entity, MilitaryServiceModel viewModel, out string error)
        {
            error = string.Empty;

            if (entity.Candidate.GeneralInfo == null || !entity.Candidate.GeneralInfo.AgreedToPersonalDataProcessing)
            {
                error = StrNotAgreedToPersonalDataProcessing;
                return false;
            }

            #region SetEntityProps
            entity.Candidate = GetCandidate(viewModel.UserId);
            entity.Candidate.MilitaryService = entity;
            entity.CombatFitness = viewModel.CombatFitness;
            entity.Commissariat = viewModel.Commissariat;
            entity.CommonMilitaryServiceRegistrationInfo = viewModel.CommonMilitaryServiceRegistrationInfo;
            entity.ConscriptionStatus = viewModel.ConscriptionStatus;
            entity.IsAssigned = viewModel.IsAssigned;
            entity.IsFinal = !viewModel.IsDraft;
            entity.IsLiableForMilitaryService = viewModel.IsLiableForMilitaryService;
            entity.IsReserved = viewModel.IsReserved;
            entity.MilitaryCardDate = viewModel.MilitaryCardDate;
            entity.MilitaryCardNumber = viewModel.MilitaryCardNumber;
            entity.MilitaryServiceRegistrationInfo = viewModel.MilitaryServiceRegistrationInfo;
            entity.MilitarySpecialityCode = viewModel.MilitarySpecialityCode;
            entity.MobilizationTicketNumber = viewModel.MobilizationTicketNumber;
            entity.PersonnelCategory = viewModel.PersonnelCategory;
            entity.PersonnelType = viewModel.PersonnelType;
            entity.Rank = viewModel.Rank;
            entity.RegistrationExpiration = viewModel.RegistrationExpiration;
            entity.ReserveCategory = viewModel.ReserveCategory;
            entity.SpecialityCategory = viewModel.SpecialityCategory;
            entity.SpecialMilitaryServiceRegistrationInfo = viewModel.SpecialMilitaryServiceRegistrationInfo; 
            #endregion

            return true;
        }

        protected bool SetExperienceEntity(Experience entity, ExperienceModel viewModel, out string error)
        {
            error = string.Empty;

            if (entity.Candidate.GeneralInfo == null || !entity.Candidate.GeneralInfo.AgreedToPersonalDataProcessing)
            {
                error = StrNotAgreedToPersonalDataProcessing;
                return false;
            }

            #region SetEntityProps
            entity.Candidate = GetCandidate(viewModel.UserId);
            entity.Candidate.Experience = entity;

            if (entity.ExperienceItems == null)
            {
                entity.ExperienceItems = new List<ExperienceItem>();
            }
            if (viewModel.ExperienceItems != null && viewModel.ExperienceItems.Count > entity.ExperienceItems.Count)
            {
                int lastIndex = viewModel.ExperienceItems.Count - 1;
                entity.ExperienceItems.Add(new ExperienceItem
                {
                    BeginningDate = viewModel.ExperienceItems[lastIndex].BeginningDate,
                    Company = viewModel.ExperienceItems[lastIndex].Company,
                    CompanyContacts = viewModel.ExperienceItems[lastIndex].CompanyContacts,
                    EndDate = viewModel.ExperienceItems[lastIndex].EndDate,
                    Position = viewModel.ExperienceItems[lastIndex].Position
                });
            }
            entity.IsFinal = !viewModel.IsDraft;
            entity.WorkBookDateOfIssue = viewModel.WorkBookDateOfIssue;
            entity.WorkBookNumber = viewModel.WorkBookNumber;
            entity.WorkBookSeries = viewModel.WorkBookSeries;
            entity.WorkBookSupplementDateOfIssue = viewModel.WorkBookSupplementDateOfIssue;
            entity.WorkBookSupplementNumber = viewModel.WorkBookSupplementNumber;
            entity.WorkBookSupplementSeries = viewModel.WorkBookSupplementSeries; 
            #endregion

            return true;
        }

        protected bool SetContactsEntity(Contacts entity, ContactsModel viewModel, out string error)
        {
            error = string.Empty;

            if (entity.Candidate.GeneralInfo == null || !entity.Candidate.GeneralInfo.AgreedToPersonalDataProcessing)
            {
                error = StrNotAgreedToPersonalDataProcessing;
                return false;
            }

            #region SetEntityProps
            entity.Apartment = viewModel.Apartment;
            entity.Building = viewModel.Building;
            entity.Candidate = GetCandidate(viewModel.UserId);
            entity.Candidate.Contacts = entity;
            entity.City = viewModel.City;
            entity.District = viewModel.District;
            entity.Email = viewModel.Email;
            entity.HomePhone = viewModel.HomePhone;
            entity.IsFinal = !viewModel.IsDraft;
            entity.Mobile = viewModel.Mobile;
            entity.Region = viewModel.Region;
            entity.Street = viewModel.Street;
            entity.StreetNumber = viewModel.StreetNumber;
            entity.WorkPhone = viewModel.WorkPhone;
            entity.ZipCode = viewModel.ZipCode; 
            #endregion

            return true;
        }

        protected bool SetBackgroundCheckEntity(BackgroundCheck entity, BackgroundCheckModel viewModel, out string error)
        {
            error = string.Empty;

            if (entity.Candidate.GeneralInfo == null || !entity.Candidate.GeneralInfo.AgreedToPersonalDataProcessing)
            {
                error = StrNotAgreedToPersonalDataProcessing;
                return false;
            }

            #region SetEntityProps
            entity.AutomobileLicensePlateNumber = viewModel.AutomobileLicensePlateNumber;
            entity.AutomobileMake = viewModel.AutomobileMake;
            entity.AverageSalary = viewModel.AverageSalary;
            entity.Candidate = GetCandidate(viewModel.UserId);
            entity.Candidate.BackgroundCheck = entity;
            entity.ChronicalDiseases = viewModel.ChronicalDiseases;
            entity.Drinking = viewModel.Drinking;
            entity.DriversLicenseCategories = viewModel.DriversLicenseCategories;
            entity.DriversLicenseDateOfIssue = viewModel.DriversLicenseDateOfIssue;
            entity.DriversLicenseNumber = viewModel.DriversLicenseNumber;
            entity.DrivingExperience = viewModel.DrivingExperience;
            entity.Hobbies = viewModel.Hobbies;
            entity.ImportantEvents = viewModel.ImportantEvents;
            entity.IsFinal = !viewModel.IsDraft;
            entity.IsReadyForBusinessTrips = viewModel.IsReadyForBusinessTrips;
            entity.Liabilities = viewModel.Liabilities;
            entity.MilitaryOperationsExperience = viewModel.MilitaryOperationsExperience;
            entity.Penalties = viewModel.Penalties;
            entity.PositionSought = viewModel.PositionSought;
            entity.PreviousDismissalReason = viewModel.PreviousDismissalReason;
            entity.PreviousSuperior = viewModel.PreviousSuperior;
            entity.PsychiatricAndAddictionTreatment = viewModel.PsychiatricAndAddictionTreatment;
            //entity.References = 
            if (entity.References == null)
            {
                entity.References = new List<Reference>();
            }
            if (viewModel.References != null && viewModel.References.Count > entity.References.Count)
            {
                int lastIndex = viewModel.References.Count - 1;
                entity.References.Add(new Reference
                {
                    FirstName = viewModel.References[lastIndex].FirstName,
                    LastName = viewModel.References[lastIndex].LastName,
                    Patronymic = viewModel.References[lastIndex].Patronymic,
                    Phone = viewModel.References[lastIndex].Phone,
                    Position = viewModel.References[lastIndex].Position,
                    Relation = viewModel.References[lastIndex].Relation,
                    WorksAt = viewModel.References[lastIndex].WorksAt
                });
            }

            entity.Smoking = viewModel.Smoking;
            entity.Sports = viewModel.Sports;
            // TODO: EMPL Добавить проверку завершенности всех предыдущих документов
            if (entity.IsFinal)
            {
                entity.Candidate.Status = EmploymentStatus.PENDING_APPROVAL_BY_SECURITY;
            } 
            #endregion

            return true;
        }

        #region Deleted
        /*
        protected void SetOnsiteTrainingEntity(OnsiteTraining entity, OnsiteTrainingModel viewModel)
        {
            entity.BeginningDate = viewModel.BeginningDate;
            entity.Candidate = GetCandidate(viewModel.UserId);
            entity.Candidate.OnsiteTraining = entity;
            entity.Comments = viewModel.Comments;
            entity.Description = viewModel.Description;
            entity.EndDate = viewModel.EndDate;
            entity.IsComplete = viewModel.IsComplete;
            entity.ReasonsForIncompleteTraining = viewModel.ReasonsForIncompleteTraining;
            entity.Results = viewModel.Results;
            entity.Type = viewModel.Type;
        }
        */
        #endregion

        protected bool SetManagersEntity(Managers entity, ManagersModel viewModel, out string error)
        {
            error = string.Empty;

            User currentUser = UserDao.Get(AuthenticationService.CurrentUser.Id);
            Department department = DepartmentDao.Get(viewModel.DepartmentId);            

            // Проверка прав руководителя на подразделение
            if (!IsUserManagerForDepartment(department, currentUser))
            {
                error = "Отсутствуют права на выбранное подразделение.";
                return false;
            }

            entity.Bonus = viewModel.Bonus;
            entity.Candidate = GetCandidate(viewModel.UserId);
            entity.Candidate.Managers = entity;
            entity.Department = DepartmentDao.Load(viewModel.DepartmentId);
            entity.EmploymentConditions = viewModel.EmploymentConditions;            
            entity.IsFront = viewModel.IsFront;
            entity.IsLiable = viewModel.IsLiable;
            entity.IsSecondaryJob = viewModel.IsSecondaryJob;
            entity.PersonalAddition = viewModel.PersonalAddition;
            entity.Position = PositionDao.Load(viewModel.PositionId);
            entity.PositionAddition = viewModel.PositionAddition;
            entity.ProbationaryPeriod = viewModel.ProbationaryPeriod;
            entity.RequestNumber = viewModel.RequestNumber;
            entity.SalaryBasis = viewModel.SalaryBasis;
            entity.SalaryMultiplier = viewModel.SalaryMultiplier;
            if (viewModel.ScheduleId.HasValue)
            {
                entity.Schedule = ScheduleDao.Load(viewModel.ScheduleId.Value);
            }
            entity.WorkCity = viewModel.WorkCity;
            
            return true;
        }

        protected bool SetPersonnelManagersEntity(PersonnelManagers entity, PersonnelManagersModel viewModel, out string error)
        {
            error = string.Empty;

            if (entity.Candidate.GeneralInfo == null || !entity.Candidate.GeneralInfo.AgreedToPersonalDataProcessing)
            {
                error = StrNotAgreedToPersonalDataProcessing;
                return false;
            }

            #region SetEntityProps
            entity.AccessGroup = AccessGroupDao.Load(viewModel.AccessGroupId);
            //entity.ApprovedByPersonnelManager = viewModel.ApprovedByPersonnelManager;
            entity.AreaAddition = viewModel.AreaAddition;
            entity.AreaMultiplier = viewModel.AreaMultiplier;
            entity.Candidate = GetCandidate(viewModel.UserId);
            entity.Candidate.PersonnelManagers = entity;
            entity.Candidate.User.Grade = viewModel.Grade;
            entity.Candidate.User.Level = viewModel.Level;
            entity.CompetenceAddition = viewModel.CompetenceAddition;
            entity.ContractDate = viewModel.ContractDate;
            entity.ContractNumber = viewModel.ContractNumber;
            entity.EmploymentDate = viewModel.EmploymentDate;
            entity.EmploymentOrderDate = viewModel.EmploymentOrderDate;
            entity.EmploymentOrderNumber = viewModel.EmploymentOrderNumber;
            entity.FrontOfficeExperienceAddition = viewModel.FrontOfficeExperienceAddition;
            entity.InsurableExperienceDays = viewModel.InsurableExperienceDays;
            entity.InsurableExperienceMonths = viewModel.InsurableExperienceMonths;
            entity.InsurableExperienceYears = viewModel.InsurableExperienceYears;
            entity.IsHourlySalaryBasis = viewModel.IsHourlySalaryBasis;
            entity.NorthernAreaAddition = viewModel.NorthernAreaAddition;
            entity.OverallExperienceDays = viewModel.OverallExperienceDays;
            entity.OverallExperienceMonths = viewModel.OverallExperienceMonths;
            entity.OverallExperienceYears = viewModel.OverallExperienceYears;
            entity.PersonalAccount = viewModel.PersonalAccount;
            entity.PersonalAccountContractor = PersonalAccountContractorDao.Load(viewModel.PersonalAccountContractorId);
            entity.Signer = EmploymentSignersDao.Load(viewModel.SignerId);
            entity.TravelRelatedAddition = viewModel.TravelRelatedAddition;
            if (entity.SupplementaryAgreements != null && entity.SupplementaryAgreements.Count > 0)
            {
                entity.SupplementaryAgreements[0].CreateDate = viewModel.SupplementaryAgreementCreateDate;
                entity.SupplementaryAgreements[0].Number = viewModel.SupplementaryAgreementNumber;
                entity.SupplementaryAgreements[0].OrderCreateDate = viewModel.ChangeContractToIndefiniteOrderCreateDate;
                entity.SupplementaryAgreements[0].OrderNumber = viewModel.ChangeContractToIndefiniteOrderNumber;
            }
            #endregion

            return true;
        }

        #endregion

        #region SetEntityHelpers

        protected FamilyMember GetFamilyMemberByRelationship(IList<FamilyMember> familyMembers, FamilyRelationship relationship)
        {
            FamilyMember result = null;
            for (var i = 0; i < familyMembers.Count; i++)
            {
                if (familyMembers[i].RelationshipId == relationship)
                {
                    result = familyMembers[i];
                    break;
                }
            }
            return result;
        }

        protected FamilyMember SetFamilyMember(FamilyMember familyMember, FamilyRelationship relationship, FamilyMemberDto data)
        {
            familyMember.Contacts = data.Contacts;
            familyMember.DateOfBirth = data.DateOfBirth;
            familyMember.Name = data.Name;
            familyMember.PassportData = data.PassportData;
            familyMember.PlaceOfBirth = data.PlaceOfBirth;
            familyMember.WorksAt = data.WorksAt;
            familyMember.RelationshipId = relationship;
            return familyMember;
        }

        protected EmploymentCandidate GetCandidate(int userId)
        {
            return EmploymentCommonDao.GetCandidateByUserId(userId);
        }

        protected UploadFileDto GetFileContext(HttpPostedFileBase postedFile)
        {
            if ((postedFile == null) || (postedFile.ContentLength == 0))
                return null;
            byte[] context = GetFileData(postedFile);
            return new UploadFileDto
            {
                Context = context,
                ContextType = postedFile.ContentType,
                FileName = Path.GetFileName(postedFile.FileName)
            };
        }

        public new AttachmentModel GetFileContext(int id)
        {
            RequestAttachment attachment = RequestAttachmentDao.Load(id);
            return new AttachmentModel
            {
                Context = attachment.UncompressContext,
                FileName = attachment.FileName,
                ContextType = attachment.ContextType
            };
        }

        protected byte[] GetFileData(HttpPostedFileBase file)
        {
            var length = file.ContentLength;
            var fileContent = new byte[length];
            file.InputStream.Read(fileContent, 0, length);
            return fileContent;
        }

        #endregion

        #region Approve

        public bool ApproveBackgroundCheck(int userId, bool IsApprovalSkipped, bool? approvalStatus, out string error)
        {
            error = string.Empty;

            IUser current = AuthenticationService.CurrentUser;
            if ((current.UserRole & UserRole.Security) == UserRole.Security)
            {
                BackgroundCheck entity = null;
                int? id = EmploymentCommonDao.GetDocumentId<BackgroundCheck>(userId);
                if (id.HasValue)
                {
                    entity = EmploymentBackgroundCheckDao.Get(id.Value);
                }
                if (entity != null)
                {
                    if (entity.Candidate.Status == EmploymentStatus.PENDING_APPROVAL_BY_SECURITY && !IsApprovalSkipped)
                    {                            
                        entity.ApprovalStatus = approvalStatus;
                        entity.Approver = UserDao.Get(current.Id);
                        if (approvalStatus == true)
                        {
                            entity.Candidate.Status = EmploymentStatus.PENDING_APPLICATION_LETTER;
                        }
                        else if (approvalStatus == false)
                        {
                            entity.Candidate.Status = EmploymentStatus.REJECTED;
                        }
                        if (!EmploymentCommonDao.SaveOrUpdateDocument<BackgroundCheck>(entity))
                        {
                            error = "Ошибка согласования.";
                            return false;
                        }
                        return true;
                    }
                    else if (entity.Candidate.Status == EmploymentStatus.PENDING_APPROVAL_BY_SECURITY && IsApprovalSkipped)
                    {
                        entity.ApprovalStatus = approvalStatus;
                        entity.Approver = UserDao.Get(current.Id);
                        entity.Candidate.Status = EmploymentStatus.PENDING_APPLICATION_LETTER;
                        if (!EmploymentCommonDao.SaveOrUpdateDocument<BackgroundCheck>(entity))
                        {
                            error = "Ошибка изменения статуса.";
                            return false;
                        }
                        return true;
                    }
                    else
                    {
                        error = "Невозможно согласовать документ на данном этапе.";
                    }
                }
                else
                {
                    error = "Документ для согласования не найден.";
                }
            }
            else
            {
                error = "Документ может согласовать только сотрудник СБ.";
            }

            return false;
        }

        public bool SaveOnsiteTrainingReport(OnsiteTrainingModel viewModel, out string error)
        {
            error = string.Empty;

            IUser current = AuthenticationService.CurrentUser;
            if ((current.UserRole & UserRole.Trainer) == UserRole.Trainer)
            {
                OnsiteTraining entity = null;
                int? id = EmploymentCommonDao.GetDocumentId<OnsiteTraining>(viewModel.UserId);
                if (id.HasValue)
                {
                    entity = EmploymentOnsiteTrainingDao.Get(id.Value);
                }
                if (entity == null)
                {
                    entity = new OnsiteTraining();
                    entity.Candidate = GetCandidate(viewModel.UserId);
                }

                entity.BeginningDate = viewModel.BeginningDate;
                entity.Candidate.OnsiteTraining = entity;
                entity.Comments = viewModel.Comments;
                entity.Description = viewModel.Description;
                entity.EndDate = viewModel.EndDate;
                entity.IsComplete = viewModel.IsComplete;
                entity.ReasonsForIncompleteTraining = viewModel.ReasonsForIncompleteTraining;
                entity.Results = viewModel.Results;
                entity.Type = viewModel.Type;
                entity.IsFinal = !viewModel.IsDraft;

                entity.Approver = UserDao.Get(current.Id);

                #region Удалено. Причина: согласование тренером больше не является обязательным этапом приема и не влияет на последовательность смены статусов
                /*
                    if (viewModel.IsComplete == true)
                    {
                        entity.Candidate.Status = EmploymentStatus.PENDING_APPLICATION_LETTER;
                    }
                    else if (viewModel.IsComplete == false)
                    {
                        entity.Candidate.Status = EmploymentStatus.REJECTED;
                    }*/

                #endregion

                if (!EmploymentCommonDao.SaveOrUpdateDocument<OnsiteTraining>(entity))
                {
                    error = "Ошибка сохранения.";
                    return false;
                }
                return true;
            }
            else
            {
                error = "Документ может сохранить только тренер.";
            }

            return false;
        }

        public bool ApproveCandidateByManager(ManagersModel viewModel, out string error)
        {
            error = string.Empty;

            IUser current = AuthenticationService.CurrentUser;
            if ((current.UserRole & UserRole.Manager) == UserRole.Manager)
            {
                Managers entity = null;
                int? id = EmploymentCommonDao.GetDocumentId<Managers>(viewModel.UserId);
                if (id.HasValue)
                {
                    entity = EmploymentManagersDao.Get(id.Value);
                }
                if (entity != null)
                {
                    if (entity.Candidate.AppointmentCreator.Id != current.Id)
                    {
                        error = "Кандидата может согласовать только руководитель, создавший соответствующую заявку на подбор персонала.";
                        return false;
                    }
                    if (entity.Candidate.Status == EmploymentStatus.PENDING_APPROVAL_BY_MANAGER)
                    {
                        entity.Bonus = viewModel.Bonus;
                        entity.Candidate = GetCandidate(viewModel.UserId);
                        entity.Candidate.Managers = entity;
                        entity.Department = DepartmentDao.Load(viewModel.DepartmentId);
                        entity.EmploymentConditions = viewModel.EmploymentConditions;
                        entity.IsFront = viewModel.IsFront;
                        entity.IsLiable = viewModel.IsLiable;
                        entity.PersonalAddition = viewModel.PersonalAddition;
                        entity.Position = PositionDao.Load(viewModel.PositionId);
                        entity.PositionAddition = viewModel.PositionAddition;
                        entity.ProbationaryPeriod = viewModel.ProbationaryPeriod;
                        entity.RequestNumber = viewModel.RequestNumber;
                        entity.SalaryBasis = viewModel.SalaryBasis;
                        entity.SalaryMultiplier = viewModel.SalaryMultiplier;
                        entity.Schedule = viewModel.ScheduleId.HasValue ? ScheduleDao.Load(viewModel.ScheduleId.Value) : null;
                        entity.WorkCity = viewModel.WorkCity;

                        //entity.Approver = UserDao.Get(current.Id);
                        if (viewModel.ManagerApprovalStatus == true)
                        {
                            entity.Candidate.Status = EmploymentStatus.PENDING_APPROVAL_BY_HIGHER_MANAGER;
                            entity.ManagerApprovalStatus = true;
                        }
                        else if (viewModel.ManagerApprovalStatus == false)
                        {
                            entity.Candidate.Status = EmploymentStatus.REJECTED;
                            entity.ManagerApprovalStatus = false;
                        }
                        entity.ApprovingManager = entity.Candidate.AppointmentCreator;
                        entity.ManagerApprovalDate = DateTime.Now;
                        if (!EmploymentCommonDao.SaveOrUpdateDocument<Managers>(entity))
                        {
                            error = "Ошибка сохранения.";
                            return false;
                        }
                        return true;
                    }
                    else
                    {
                        error = "Невозможно сохранить документ на данном этапе.";
                    }
                }
                else
                {
                    error = "Документ не найден.";
                }
            }
            else
            {
                error = "Кандидата может согласовать только руководитель.";
            }

            return false;
        }

        public bool ApproveCandidateByHigherManager(int userId, bool? approvalStatus, out string error)
        {
            error = string.Empty;

            User current = UserDao.Get(AuthenticationService.CurrentUser.Id);
            if ((current.UserRole & UserRole.Manager) == UserRole.Manager)
            {
                Managers entity = null;
                int? id = EmploymentCommonDao.GetDocumentId<Managers>(userId);
                if (id.HasValue)
                {
                    entity = EmploymentManagersDao.Get(id.Value);
                }
                if (entity != null)
                {
                    if (!IsCurrentUserChiefForCreator(current, entity.Candidate.AppointmentCreator))
                    {
                        error = "Кандидата может согласовать только руководитель, являющийся вышестоящим для создателя заявки на подбор персонала.";
                        return false;
                    }

                    if (entity.Candidate.Status == EmploymentStatus.PENDING_APPROVAL_BY_HIGHER_MANAGER)
                    {                        
                        entity.ApprovingHigherManager = current;
                        entity.HigherManagerApprovalDate = DateTime.Now;
                        if (approvalStatus == true)
                        {
                            entity.Candidate.Status = EmploymentStatus.PENDING_FINALIZATION_BY_PERSONNEL_MANAGER;
                            entity.HigherManagerApprovalStatus = true;
                        }
                        else if (approvalStatus == false)
                        {
                            entity.Candidate.Status = EmploymentStatus.REJECTED;
                            entity.HigherManagerApprovalStatus = false;
                        }
                        if (!EmploymentCommonDao.SaveOrUpdateDocument<Managers>(entity))
                        {
                            error = "Ошибка сохранения.";
                            return false;
                        }
                        return true;
                    }
                    else
                    {
                        error = "Невозможно сохранить документ на данном этапе.";
                    }
                }
                else
                {
                    error = "Документ не найден.";
                }
            }
            else
            {
                error = "Кандидата может согласовать только руководитель.";
            }

            return false;
        }

        public bool SavePersonnelManagersReport(PersonnelManagersModel viewModel, out string error)
        {
            error = string.Empty;
            
            IUser current = AuthenticationService.CurrentUser;
            if ((current.UserRole & UserRole.PersonnelManager) == UserRole.PersonnelManager)
            {
                PersonnelManagers entity = null;
                EmploymentCandidate candidate = GetCandidate(viewModel.UserId);

                int? id = EmploymentCommonDao.GetDocumentId<PersonnelManagers>(viewModel.UserId);
                if (id.HasValue)
                {
                    entity = EmploymentPersonnelManagersDao.Get(id.Value);
                }
                if (entity == null)
                {
                    entity = new PersonnelManagers { Candidate = candidate };
                }

                if (entity.Candidate.GeneralInfo == null || !entity.Candidate.GeneralInfo.AgreedToPersonalDataProcessing)
                {
                    error = StrNotAgreedToPersonalDataProcessing;
                    return false;
                }

                EmploymentStatus candidateStatus = candidate.Status;

                if (candidateStatus == EmploymentStatus.PENDING_FINALIZATION_BY_PERSONNEL_MANAGER
                    || candidateStatus == EmploymentStatus.COMPLETE)
                {
                    entity.AccessGroup = AccessGroupDao.Load(viewModel.AccessGroupId);
                    //entity.ApprovedByPersonnelManager = viewModel.ApprovedByPersonnelManager;
                    entity.AreaAddition = viewModel.AreaAddition;
                    entity.AreaMultiplier = viewModel.AreaMultiplier;
                    entity.Candidate = GetCandidate(viewModel.UserId);
                    entity.Candidate.PersonnelManagers = entity;
                    entity.Candidate.User.Grade = viewModel.Grade;
                    entity.Candidate.User.Level = viewModel.Level;
                    entity.CompetenceAddition = viewModel.CompetenceAddition;
                    entity.ContractDate = viewModel.ContractDate;
                    entity.ContractEndDate = viewModel.ContractEndDate;
                    entity.ContractNumber = viewModel.ContractNumber;
                    entity.EmploymentDate = viewModel.EmploymentDate;
                    entity.EmploymentOrderDate = viewModel.EmploymentOrderDate;
                    entity.EmploymentOrderNumber = viewModel.EmploymentOrderNumber;
                    entity.FrontOfficeExperienceAddition = viewModel.FrontOfficeExperienceAddition;
                    entity.InsurableExperienceDays = viewModel.InsurableExperienceDays;
                    entity.InsurableExperienceMonths = viewModel.InsurableExperienceMonths;
                    entity.InsurableExperienceYears = viewModel.InsurableExperienceYears;
                    entity.IsHourlySalaryBasis = viewModel.IsHourlySalaryBasis;
                    entity.NorthernAreaAddition = viewModel.NorthernAreaAddition;
                    entity.OverallExperienceDays = viewModel.OverallExperienceDays;
                    entity.OverallExperienceMonths = viewModel.OverallExperienceMonths;
                    entity.OverallExperienceYears = viewModel.OverallExperienceYears;
                    entity.PersonalAccount = viewModel.PersonalAccount;
                    entity.PersonalAccountContractor = PersonalAccountContractorDao.Load(viewModel.PersonalAccountContractorId);
                    entity.Signer = EmploymentSignersDao.Load(viewModel.SignerId);
                    entity.TravelRelatedAddition = viewModel.TravelRelatedAddition;

                    if (entity.SupplementaryAgreements != null && entity.SupplementaryAgreements.Count > 0)
                    {
                        entity.SupplementaryAgreements[0].CreateDate = viewModel.SupplementaryAgreementCreateDate;
                        entity.SupplementaryAgreements[0].Number = viewModel.SupplementaryAgreementNumber;
                        entity.SupplementaryAgreements[0].OrderCreateDate = viewModel.ChangeContractToIndefiniteOrderCreateDate;
                        entity.SupplementaryAgreements[0].OrderNumber = viewModel.ChangeContractToIndefiniteOrderNumber;
                    }

                    //entity.Approver = UserDao.Get(current.Id);
                    entity.Candidate.Status = EmploymentStatus.COMPLETE;
                    if (!EmploymentCommonDao.SaveOrUpdateDocument<PersonnelManagers>(entity))
                    {
                        error = "Ошибка сохранения.";
                        return false;
                    }
                    return true;
                }
                else
                {
                    error = "Невозможно сохранить документ на данном этапе.";
                }

            }
            else
            {
                error = "Документ может сохранить только сотрудник отдела кадров.";
            }

            return false;
        }

        public bool SaveApprovals(IList<CandidateApprovalDto> roster, out string error)
        {
            error = string.Empty;

            IList<int> idsToApproveByManager;
            IList<int> idsToApproveByHigherManager;

            User current = UserDao.Load(AuthenticationService.CurrentUser.Id);

            if (roster != null && ((current.UserRole & UserRole.Manager) == UserRole.Manager ||
                                          (current.UserRole & UserRole.Chief) == UserRole.Chief ||
                                          (current.UserRole & UserRole.Director) == UserRole.Director))
            {
                idsToApproveByManager = roster.Where(x => x.IsApprovedByManager == true).Select(x => x.Id).ToList();
                idsToApproveByHigherManager = roster.Where(x => x.IsApprovedByHigherManager == true).Select(x => x.Id).ToList();

                List<EmploymentCandidate> entities = EmploymentCandidateDao.LoadForIdsList(idsToApproveByManager).ToList();
                foreach (EmploymentCandidate entity in entities)
                {
                    entity.Status = EmploymentStatus.PENDING_APPROVAL_BY_HIGHER_MANAGER;
                    entity.Managers.ApprovingManager = current;
                    entity.Managers.ManagerApprovalStatus = true;
                    EmploymentCandidateDao.Save(entity);
                }

                entities = EmploymentCandidateDao.LoadForIdsList(idsToApproveByHigherManager).ToList();
                foreach (EmploymentCandidate entity in entities)
                {
                    entity.Status = EmploymentStatus.PENDING_FINALIZATION_BY_PERSONNEL_MANAGER;
                    entity.Managers.ApprovingHigherManager = current;
                    entity.Managers.HigherManagerApprovalStatus = true;
                    EmploymentCandidateDao.Save(entity);
                }
                EmploymentCandidateDao.Flush();
            }

            

            return true;
        }

        #endregion

        public bool SaveContractChangesToIndefinite(IList<CandidateChangeContractToIndefiniteDto> roster, out string error)
        {
            error = string.Empty;

            IList<int> idsToChangeContractToIndefinite;

            User current = UserDao.Load(AuthenticationService.CurrentUser.Id);

            if (roster != null && ((current.UserRole & UserRole.Manager) == UserRole.Manager ||
                                          (current.UserRole & UserRole.Chief) == UserRole.Chief ||
                                          (current.UserRole & UserRole.Director) == UserRole.Director))
            {
                idsToChangeContractToIndefinite = roster.Where(x => x.IsContractChangedToIndefinite == true).Select(x => x.Id).ToList();

                List<EmploymentCandidate> entities = EmploymentCandidateDao.LoadForIdsList(idsToChangeContractToIndefinite).ToList();
                foreach (EmploymentCandidate entity in entities)
                {
                    if (entity.PersonnelManagers == null)
                    {
                        entity.PersonnelManagers = new PersonnelManagers { Candidate = entity };
                    }

                    if (entity.PersonnelManagers.SupplementaryAgreements.Count == 0)
                    {
                        entity.PersonnelManagers.SupplementaryAgreements.Add(new SupplementaryAgreement { PersonnelManagers = entity.PersonnelManagers });
                        EmploymentCandidateDao.Save(entity);
                    }

                    entity.User.IsFixedTermContract = true;
                    entity.PersonnelManagers.ContractEndDate = null;
                }
                EmploymentCandidateDao.Flush();
            }

            return true;
        }

        public bool IsCurrentUserChiefForCreator(User current, User creator)
        {
            // Контроль уровня вышестоящего руководителя
            if (!IsManagerLevelValid(current))
            {
                throw new ValidationException(string.Format(StrIncorrectManagerLevel,
                        current.Level.HasValue ? current.Level.Value.ToString() : "<не указан>", current.Id));
            }
            // Контроль уровня руководителя-инициатора
            if (!IsManagerLevelValid(creator))
            {
                throw new ValidationException(string.Format(StrIncorrectManagerLevel,
                        creator.Level.HasValue ? creator.Level.Value.ToString() : "<не указан>", creator.Id));
            }

            switch (current.Level)
            {
                case 3:
                    // Для руководителей 3 уровня получаем список ручных привязок к подразделениям
                    return MissionOrderRoleRecordDao.GetRoleRecords(user: current, roleCode: "000000037")
                        .Any(roleRecord => (roleRecord.TargetDepartment != null && creator.Department.Path.StartsWith(roleRecord.TargetDepartment.Path)));
                case 4:
                case 5:
                    return creator.Department.Path.StartsWith(current.Department.Path)
                        && creator.Department.Path.Length > current.Department.Path.Length;
                default:
                    return false;
            }
        }

        protected bool IsManagerLevelValid(User manager)
        {
            return (manager.Level.HasValue && manager.Level >= MinManagerLevel && manager.Level <= MaxManagerLevel);
        }

        public bool IsUnlimitedEditAvailable()
        {
            if ((AuthenticationService.CurrentUser.UserRole & UserRole.PersonnelManager) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string GetStartView()
        {
            UserRole currentUserRole = AuthenticationService.CurrentUser.UserRole;
            if ((currentUserRole & UserRole.Candidate) == UserRole.Candidate)
            {
                return "GeneralInfo";
            }
            else
            {
                return "Roster";
            }
        }

        public bool IsFixedTermContract(int userId)
        {
            User user = UserDao.Get(userId);
            return user != null && user.IsFixedTermContract.HasValue && user.IsFixedTermContract.Value;
        }
    }
}