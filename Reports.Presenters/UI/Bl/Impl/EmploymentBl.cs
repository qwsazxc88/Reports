﻿using System;
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
using Reports.Presenters.UI.ViewModel.Employment2;
using System.ComponentModel.DataAnnotations;


namespace Reports.Presenters.UI.Bl.Impl
{
    public class EmploymentBl : BaseBl, IEmploymentBl
    {
        public const string StrIncorrectManagerLevel = "Неправильный уровень {0} руководителя (id {1}) в базе данных.";
        public const string StrNoDepartmentForManager = "Не указано структурное подраздаление для руководителя (id {0}).";

        public const int MinManagerLevel = 2;
        public const int MaxManagerLevel = 6;

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

        protected IPositionDao positionDao;
        public IPositionDao PositionDao
        {
            get { return Validate.Dependency(positionDao); }
            set { positionDao = value; }
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

        #endregion

        #region Get Model

        public GeneralInfoModel GetGeneralInfoModel(int? userId = null)
        {
            // TODO: EMPL доработать реализацию
            //UserRole role = AuthenticationService.CurrentUser.UserRole;
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
                model.CitizenshipId = entity.Citizenship.Id;
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
                model.StatusId = entity.Status;
                model.Version = entity.Version;
                model.IsDraft = true;
                model.IsFinal = entity.IsFinal;
            }            
            return model;
        }

        public PassportModel GetPassportModel(int? userId = null)
        {
            // TODO: EMPL доработать реализацию
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
                model.DocumentTypeId = entity.DocumentType.Id;
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
            // TODO: EMPL заменить реализацией
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
            // TODO: EMPL заменить реализацией
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
                if (model.Father == null)
                {
                    model.Father = new FamilyMemberDto();
                }

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
                if (model.Mother == null)
                {
                    model.Mother = new FamilyMemberDto();
                }

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
                    model.Spouse = new FamilyMemberDto();
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
            // TODO: EMPL доработать реализацию
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
            // TODO: EMPL заменить реализацией
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
            // TODO: EMPL заменить реализацией
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
            // TODO: EMPL заменить реализацией
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

                model.ApproverName = entity.Approver.Name;
                model.ApprovalStatus = entity.ApprovalStatus;
                model.IsApproveBySecurityAvailable = (entity.Candidate.Status == EmploymentStatus.PENDING_APPROVAL_BY_SECURITY)
                    && ((AuthenticationService.CurrentUser.UserRole & UserRole.Security) == UserRole.Security);
            }
            LoadDictionaries(model);
            return model;
        }

        public OnsiteTrainingModel GetOnsiteTrainingModel(int? userId = null)
        {
            // TODO: EMPL заменить реализацией
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

                model.ApproverName = entity.Approver.Name;
                model.ApprovalStatus = entity.IsComplete;
                model.IsApproveByTrainerAvailable = (entity.Candidate.Status == EmploymentStatus.PENDING_REPORT_BY_TRAINER)
                    && ((AuthenticationService.CurrentUser.UserRole & UserRole.Trainer) == UserRole.Trainer);
            }
            LoadDictionaries(model);
            return model;
        }

        public ManagersModel GetManagersModel(int? userId = null)
        {
            // TODO: EMPL заменить реализацией
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
                model.DirectorateId = entity.Directorate.Id;
                model.DailySalaryBasis = entity.DailySalaryBasis;
                model.DepartmentId = entity.Department.Id;
                model.EmploymentConditions = entity.EmploymentConditions;
                model.HourlySalaryBasis = entity.HourlySalaryBasis;
                model.IsFront = entity.IsFront;
                model.IsLiable = entity.IsLiable;
                model.PersonalAddition = entity.PersonalAddition;
                model.PositionAddition = entity.PositionAddition;
                model.PositionId = entity.Position.Id;
                model.ProbationaryPeriod = entity.ProbationaryPeriod;
                model.RequestNumber = entity.RequestNumber;
                model.SalaryMultiplier = entity.SalaryMultiplier;
                model.Schedule = entity.Schedule;
                model.WorkCity = entity.WorkCity;

                model.ApprovingManagerName = entity.ApprovingManager.Name;
                model.ApprovingHigherManagerName = entity.ApprovingHigherManager.Name;
                model.ManagerRejectionReason = entity.ManagerRejectionReason;

                model.ManagerApprovalStatus = entity.ManagerApprovalStatus;
                model.HigherManagerApprovalStatus = entity.HigherManagerApprovalStatus;
                model.HigherManagerRejectionReason = entity.HigherManagerRejectionReason;

                model.IsApproveByManagerAvailable = (entity.Candidate.Status == EmploymentStatus.PENDING_APPROVAL_BY_MANAGER)
                    && ((AuthenticationService.CurrentUser.UserRole & UserRole.Manager) == UserRole.Manager);

                model.IsApproveByHigherManagerAvailable = (entity.Candidate.Status == EmploymentStatus.PENDING_APPROVAL_BY_HIGHER_MANAGER)
                    && ((AuthenticationService.CurrentUser.UserRole & UserRole.Manager) == UserRole.Manager);
            }
            LoadDictionaries(model);
            return model;
        }

        public PersonnelManagersModel GetPersonnelManagersModel(int? userId = null)
        {
            // TODO: EMPL заменить реализацией
            userId = userId ?? AuthenticationService.CurrentUser.Id;
            PersonnelManagersModel model = new PersonnelManagersModel { UserId = userId.Value };
            PersonnelManagers entity = null;
            int? id = EmploymentCommonDao.GetDocumentId<Managers>(userId.Value);
            if (id.HasValue)
            {
                entity = EmploymentPersonnelManagersDao.Get(id.Value);
            }
            if (entity != null)
            {
                model.AccessGroupId = entity.AccessGroup.Id;
                //model.ApprovedByPersonnelManager = entity.ApprovedByPersonnelManager;
                model.AreaAddition = entity.AreaAddition;
                model.AreaMultiplier = entity.AreaMultiplier;
                model.CompetenceAddition = entity.CompetenceAddition;
                model.ContractDate = entity.ContractDate;
                model.ContractNumber = entity.ContractNumber;
                model.EmploymentDate = entity.EmploymentDate;
                model.EmploymentOrderDate = entity.EmploymentOrderDate;
                model.EmploymentOrderNumber = entity.EmploymentOrderNumber;
                model.FrontOfficeExperienceAddition = entity.FrontOfficeExperienceAddition;
                model.Grade = entity.Candidate.User.Grade;
                model.InsurableExperienceDays = entity.InsurableExperienceDays;
                model.InsurableExperienceMonths = entity.InsurableExperienceMonths;
                model.InsurableExperienceYears = entity.InsurableExperienceYears;
                model.NorthernAreaAddition = entity.NorthernAreaAddition;
                model.OverallExperienceDays = entity.OverallExperienceDays;
                model.OverallExperienceMonths = entity.OverallExperienceMonths;
                model.OverallExperienceYears = entity.OverallExperienceYears;
                model.PersonalAccount = entity.PersonalAccount;
                model.PersonalAccountContractorId = entity.PersonalAccountContractor.Id;
                model.TravelRelatedAddition = entity.TravelRelatedAddition;
            }
            LoadDictionaries(model);
            return model;
        }

        public RosterModel GetRosterModel(RosterFiltersModel filters)
        {
            IList<EmploymentCandidate> candidates;
            RosterModel model = new RosterModel();
            if (filters == null)
            {                
                candidates = new List<EmploymentCandidate>();
            }
            else
            {
                candidates = EmploymentCandidateDao.LoadFiltered(filters.DepartmentId, filters.StatusId, filters.UserName, filters.BeginDate, filters.EndDate);
            }

            LoadDictionaries(model);
            
            SelectListItem tempItem = null;
            model.Roster = candidates.ToList<EmploymentCandidate>().ConvertAll<CandidateDto>(x => new CandidateDto
            {                
                ContractDate = x.PersonnelManagers != null ? x.PersonnelManagers.ContractDate : null,
                ContractNumber = x.PersonnelManagers != null ? x.PersonnelManagers.ContractNumber : String.Empty,
                DateOfBirth = x.GeneralInfo != null ? (DateTime?)x.GeneralInfo.DateOfBirth : null,
                Department = x.Managers != null ? x.Managers.Department.Name : String.Empty,
                Directorate = x.Managers != null ? x.Managers.Directorate.Name : String.Empty,
                Disabilities = x.GeneralInfo != null ? "Справка "
                    + x.GeneralInfo.DisabilityCertificateSeries +
                    " " + x.GeneralInfo.DisabilityCertificateNumber +
                    ", дата выдачи: " + (x.GeneralInfo.DisabilityCertificateDateOfIssue.HasValue ? x.GeneralInfo.DisabilityCertificateDateOfIssue.Value.ToShortDateString() : String.Empty) +
                    ", группа " + (x.GeneralInfo.DisabilityDegree != null ? x.GeneralInfo.DisabilityDegree.Name : "?") +
                    ", срок действия справки: " + (x.GeneralInfo.DisabilityCertificateExpirationDate.HasValue ? x.GeneralInfo.DisabilityCertificateExpirationDate.Value.ToShortDateString() : String.Empty)
                    : String.Empty,
                EmploymentDate = x.PersonnelManagers != null ? x.PersonnelManagers.EmploymentDate : null,
                EmploymentOrderDate = x.PersonnelManagers != null ? x.PersonnelManagers.EmploymentOrderDate : null,
                EmploymentOrderNumber = x.PersonnelManagers != null ? x.PersonnelManagers.EmploymentOrderNumber : String.Empty,
                Grade = x.User != null ? x.User.Grade : null,
                Id = x.User.Id,
                Name = x.GeneralInfo != null ? x.GeneralInfo.LastName + " " + x.GeneralInfo.FirstName + " " + x.GeneralInfo.Patronymic : String.Empty,
                Position = x.Managers != null ? x.Managers.Position.Name : String.Empty,
                ProbationaryPeriod = x.Managers != null ? x.Managers.ProbationaryPeriod : String.Empty,
                Status = (tempItem = GetEmploymentStatuses().Where(status => status.Value == ((int?)x.Status ?? 0).ToString()).FirstOrDefault()) != null ? tempItem.Text : string.Empty,
                Schedule = x.Managers != null ? x.Managers.Schedule : String.Empty,
                WorkCity = x.Managers != null ? x.Managers.WorkCity : String.Empty
            });

            return model;
        }

        public SignersModel GetSignersModel()
        {
            // TODO: EMPL заменить реализацией
            return new SignersModel();
        }

        #endregion

        #region Set Model

        public void SetGeneralInfoModel(GeneralInfoModel model, bool hasError)
        {

        }
        
        public void SetPassportModel(PassportModel model, bool hasError)
        {

        }
        
        public void SetEducationModel(EducationModel model, bool hasError)
        {

        }
        
        public void SetFamilyModel(FamilyModel model, bool hasError)
        {

        }
        
        public void SetMilitaryServiceModel(MilitaryServiceModel model, bool hasError)
        {

        }
        
        public void SetExperienceModel(ExperienceModel model, bool hasError)
        {

        }
        
        public void SetContactsModel(ContactsModel model, bool hasError)
        {

        }
        
        public void SetBackgroundCheckModel(BackgroundCheckModel model, bool hasError)
        {

        }
        
        public void SetOnsiteTrainingModel(OnsiteTrainingModel model, bool hasError)
        {

        }
        
        public void SetManagersModel(ManagersModel model, bool hasError)
        {

        }
        
        public void SetPersonnelManagersModel(PersonnelManagersModel model, bool hasError)
        {

        }
        
        public void SetRosterModel(RosterModel model, bool hasError)
        {

        }
        
        public void SetSignersModel(SignersModel model, bool hasError)
        {

        }

        #endregion

        #region Process Saving

        public bool ProcessSaving<TVM, TE>(TVM model, out string error)
            where TVM: AbstractEmploymentModel
            where TE: new()
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
                if (model.UserId == AuthenticationService.CurrentUser.Id)
                {
                    SetEntity<TVM, TE>(entity, model);
                    EmploymentCommonDao.SaveOrUpdateDocument<TE>(entity);
                    // EmploymentCommonDao.SaveOrUpdateDocument<TE>(entity, model.UserId);
                }
            }
            catch (Exception exc)
            {

            }
            finally
            {

            }

            return false;
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
            model.ApprovalStatuses = GetOnsiteTrainingStatuses();
        }
        public void LoadDictionaries(ManagersModel model)
        {
            model.PositionItems = GetPositions();
            model.DirectorateItems = GetDirectorates();
            model.DepartmentItems = GetDepartments();

            model.ApprovalStatuses = GetApprovalStatuses();
        }
        public void LoadDictionaries(PersonnelManagersModel model)
        {
            model.PersonalAccountContractors = GetPersonalAccountContractors();
            model.AccessGroups = GetAccessGroups();
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

        public IEnumerable<SelectListItem> GetDirectorates()
        {
            return DepartmentDao.LoadAllSorted().Where(item => item.ItemLevel == 3).ToList().ConvertAll(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).OrderBy(x => x.Value);
        }

        public IEnumerable<SelectListItem> GetDepartments()
        {
            return DepartmentDao.LoadAllSorted().ToList().ConvertAll(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).OrderBy(x => x.Value);
        }

        public IEnumerable<SelectListItem> GetPersonalAccountContractors()
        {
            return PersonalAccountContractorDao.LoadAllSorted().ToList().ConvertAll(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).OrderBy(x => x.Value);
        }

        public IEnumerable<SelectListItem> GetAccessGroups()
        {
            return AccessGroupDao.LoadAllSorted().ToList().ConvertAll(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).OrderBy(x => x.Value);
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

        public IEnumerable<SelectListItem> GetOnsiteTrainingStatuses()
        {
            return new List<SelectListItem>
            {
                new SelectListItem {Text = "Обучение пройдено", Value = "true"},
                new SelectListItem {Text = "Обучение не пройдено", Value = "false"}
            };
        }

        #endregion

        #region SetEntity

        protected void SetEntity<TVM, TE>(TE entity, TVM viewModel)
        {
            switch (entity.GetType().Name)
            {
                case "GeneralInfo":
                    SetGeneralInfoEntity(entity as GeneralInfo, viewModel as GeneralInfoModel);
                    break;
                case "Passport":
                    SetPassportEntity(entity as Passport, viewModel as PassportModel);
                    break;
                case "Education":
                    SetEducationEntity(entity as Education, viewModel as EducationModel);
                    break;
                case "Family":
                    SetFamilyEntity(entity as Family, viewModel as FamilyModel);
                    break;
                case "MilitaryService":
                    SetMilitaryServiceEntity(entity as MilitaryService, viewModel as MilitaryServiceModel);
                    break;
                case "Experience":
                    SetExperienceEntity(entity as Experience, viewModel as ExperienceModel);
                    break;
                case "Contacts":
                    SetContactsEntity(entity as Contacts, viewModel as ContactsModel);
                    break;
                case "BackgroundCheck":
                    SetBackgroundCheckEntity(entity as BackgroundCheck, viewModel as BackgroundCheckModel);
                    break;
                //case "OnsiteTraining":
                //    SetOnsiteTrainingEntity(entity as OnsiteTraining, viewModel as OnsiteTrainingModel);
                //    break;
                //case "Managers":
                //    SetManagersEntity(entity as Managers, viewModel as ManagersModel);
                //    break;
                //case "PersonnelManagers":
                //    SetPersonnelManagersEntity(entity as PersonnelManagers, viewModel as PersonnelManagersModel);
                //    break;
                default:
                    break;
            }            
        }

        protected void SetGeneralInfoEntity(GeneralInfo entity, GeneralInfoModel viewModel)
        {
            entity.AgreedToPersonalDataProcessing = viewModel.AgreedToPersonalDataProcessing;
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
            //entity.Candidate = EmploymentCandidateDao.LoadAll().Where(x => x.User.

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
        }

        protected void SetPassportEntity(Passport entity, PassportModel viewModel)
        {
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
        }

        protected void SetEducationEntity(Education entity, EducationModel viewModel)
        {
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
        }

        protected void SetFamilyEntity(Family entity, FamilyModel viewModel)
        {
            entity.Candidate = GetCandidate(viewModel.UserId);
            entity.Candidate.Family = entity;
            
            entity.Cohabitants = viewModel.Cohabitants;

            if (entity.FamilyMembers == null)
            {
                entity.FamilyMembers = new List<FamilyMember>();
            }

            if (viewModel.Father != null && entity.FamilyMembers.Where<FamilyMember>(x => x.RelationshipId == FamilyRelationship.FATHER).Count() == 0)
            {
                entity.FamilyMembers.Add(new FamilyMember
                {
                    RelationshipId = FamilyRelationship.FATHER,
                    Contacts = viewModel.Father.Contacts,
                    DateOfBirth = viewModel.Father.DateOfBirth,
                    Name = viewModel.Father.Name,
                    PassportData = viewModel.Father.PassportData,
                    PlaceOfBirth = viewModel.Father.PlaceOfBirth,
                    WorksAt = viewModel.Father.WorksAt
                });
            }

            if (viewModel.Father != null && entity.FamilyMembers.Where<FamilyMember>(x => x.RelationshipId == FamilyRelationship.MOTHER).Count() == 0)
            {
                entity.FamilyMembers.Add(new FamilyMember
                {
                    RelationshipId = FamilyRelationship.MOTHER,
                    Contacts = viewModel.Mother.Contacts,
                    DateOfBirth = viewModel.Mother.DateOfBirth,
                    Name = viewModel.Mother.Name,
                    PassportData = viewModel.Mother.PassportData,
                    PlaceOfBirth = viewModel.Mother.PlaceOfBirth,
                    WorksAt = viewModel.Mother.WorksAt
                });
            }

            if (viewModel.IsMarried && viewModel.Father != null && entity.FamilyMembers.Where<FamilyMember>(x => x.RelationshipId == FamilyRelationship.SPOUSE).Count() == 0)
            {
                entity.FamilyMembers.Add(new FamilyMember
                {
                    RelationshipId = FamilyRelationship.SPOUSE,
                    Contacts = viewModel.Spouse.Contacts,
                    DateOfBirth = viewModel.Spouse.DateOfBirth,
                    Name = viewModel.Spouse.Name,
                    PassportData = viewModel.Spouse.PassportData,
                    PlaceOfBirth = viewModel.Spouse.PlaceOfBirth,
                    WorksAt = viewModel.Spouse.WorksAt
                });
            }

            if (viewModel.Children != null && viewModel.Children.Count > entity.FamilyMembers.Where<FamilyMember>(x => x.RelationshipId == FamilyRelationship.CHILD).Count())
            {
                int lastIndex = viewModel.Children.Count - 1;
                entity.FamilyMembers.Add(new FamilyMember
                {
                    RelationshipId = FamilyRelationship.CHILD,
                    Contacts = viewModel.Children[lastIndex].Contacts,
                    DateOfBirth = viewModel.Children[lastIndex].DateOfBirth,
                    Name = viewModel.Children[lastIndex].Name,
                    PassportData = viewModel.Children[lastIndex].PassportData,
                    PlaceOfBirth = viewModel.Children[lastIndex].PlaceOfBirth,
                    WorksAt = viewModel.Children[lastIndex].WorksAt
                });
            }

            entity.IsFinal = !viewModel.IsDraft;
        }

        protected void SetMilitaryServiceEntity(MilitaryService entity, MilitaryServiceModel viewModel)
        {
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
        }

        protected void SetExperienceEntity(Experience entity, ExperienceModel viewModel)
        {
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
        }

        protected void SetContactsEntity(Contacts entity, ContactsModel viewModel)
        {
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
        }

        protected void SetBackgroundCheckEntity(BackgroundCheck entity, BackgroundCheckModel viewModel)
        {
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
            // TODO: Добавить проверку завершенности всех предыдущих документов
            entity.Candidate.Status = EmploymentStatus.PENDING_APPROVAL_BY_SECURITY;
        }

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

        /*
        protected void SetManagersEntity(Managers entity, ManagersModel viewModel)
        {
            entity.Bonus = viewModel.Bonus;
            entity.Candidate = GetCandidate(viewModel.UserId);
            entity.Candidate.Managers = entity;
            entity.DailySalaryBasis = viewModel.DailySalaryBasis;
            entity.Department = DepartmentDao.Load(viewModel.DepartmentId);
            entity.Directorate = DepartmentDao.Load(viewModel.DirectorateId);
            entity.EmploymentConditions = viewModel.EmploymentConditions;            
            entity.HourlySalaryBasis = viewModel.HourlySalaryBasis;
            entity.IsFront = viewModel.IsFront;
            entity.IsLiable = viewModel.IsLiable;
            entity.PersonalAddition = viewModel.PersonalAddition;
            entity.Position = PositionDao.Load(viewModel.PositionId);
            entity.PositionAddition = viewModel.PositionAddition;
            entity.ProbationaryPeriod = viewModel.ProbationaryPeriod;
            entity.RequestNumber = viewModel.RequestNumber;
            entity.SalaryMultiplier = viewModel.SalaryMultiplier;
            entity.Schedule = viewModel.Schedule;
            entity.WorkCity = viewModel.WorkCity;
        }
        */

        /*
        protected void SetPersonnelManagersEntity(PersonnelManagers entity, PersonnelManagersModel viewModel)
        {
            entity.AccessGroup = AccessGroupDao.Load(viewModel.AccessGroupId);
            //entity.ApprovedByPersonnelManager = viewModel.ApprovedByPersonnelManager;
            entity.AreaAddition = viewModel.AreaAddition;
            entity.AreaMultiplier = viewModel.AreaMultiplier;
            entity.Candidate = GetCandidate(viewModel.UserId);
            entity.Candidate.PersonnelManagers = entity;
            entity.Candidate.User.Grade = viewModel.Grade;
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
            entity.NorthernAreaAddition = viewModel.NorthernAreaAddition;
            entity.OverallExperienceDays = viewModel.OverallExperienceDays;
            entity.OverallExperienceMonths = viewModel.OverallExperienceMonths;
            entity.OverallExperienceYears = viewModel.OverallExperienceYears;
            entity.PersonalAccount = viewModel.PersonalAccount;
            entity.PersonalAccountContractor = PersonalAccountContractorDao.Load(viewModel.PersonalAccountContractorId);
            entity.TravelRelatedAddition = viewModel.TravelRelatedAddition;
        }
        */

        protected EmploymentCandidate GetCandidate(int userId)
        {
            EmploymentCandidate candidate = EmploymentCommonDao.GetCandidateByUserId(userId);
            if (candidate == null)
            {
                EmploymentCommonDao.SaveOrUpdateDocument<EmploymentCandidate>(new EmploymentCandidate { User = UserDao.Get(userId) });
                candidate = EmploymentCommonDao.GetCandidateByUserId(userId);
            }
            return candidate;
        }

        #endregion

        #region Approve

        public bool ApproveBackgroundCheck(int userId, out string error)
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
                    if (entity.Candidate.Status == EmploymentStatus.PENDING_APPROVAL_BY_SECURITY)
                    {                        
                        entity.ApprovalStatus = true;
                        entity.Approver = UserDao.Get(current.Id);
                        entity.Candidate.Status = EmploymentStatus.PENDING_REPORT_BY_TRAINER;
                        if (!EmploymentCommonDao.SaveOrUpdateDocument<BackgroundCheck>(entity))
                        {
                            error = "Ошибка согласования.";
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
                if (entity != null)
                {
                    if (entity.Candidate.Status == EmploymentStatus.PENDING_REPORT_BY_TRAINER)
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

                        entity.Approver = UserDao.Get(current.Id);
                        entity.Candidate.Status = EmploymentStatus.PENDING_REPORT_BY_TRAINER;
                        if (!EmploymentCommonDao.SaveOrUpdateDocument<OnsiteTraining>(entity))
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
                error = "Документ может сохранить только тренер.";
            }

            return false;
        }

        public bool ApproveCandidateByManager(ManagersModel viewModel, out string error)
        {
            error = string.Empty;

            // TODO: Добавить реализацию
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
                        entity.DailySalaryBasis = viewModel.DailySalaryBasis;
                        entity.Department = DepartmentDao.Load(viewModel.DepartmentId);
                        entity.Directorate = DepartmentDao.Load(viewModel.DirectorateId);
                        entity.EmploymentConditions = viewModel.EmploymentConditions;
                        entity.HourlySalaryBasis = viewModel.HourlySalaryBasis;
                        entity.IsFront = viewModel.IsFront;
                        entity.IsLiable = viewModel.IsLiable;
                        entity.PersonalAddition = viewModel.PersonalAddition;
                        entity.Position = PositionDao.Load(viewModel.PositionId);
                        entity.PositionAddition = viewModel.PositionAddition;
                        entity.ProbationaryPeriod = viewModel.ProbationaryPeriod;
                        entity.RequestNumber = viewModel.RequestNumber;
                        entity.SalaryMultiplier = viewModel.SalaryMultiplier;
                        entity.Schedule = viewModel.Schedule;
                        entity.WorkCity = viewModel.WorkCity;

                        //entity.Approver = UserDao.Get(current.Id);
                        entity.Candidate.Status = EmploymentStatus.PENDING_APPROVAL_BY_HIGHER_MANAGER;
                        entity.ApprovingManager = entity.Candidate.AppointmentCreator;
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

        public bool ApproveCandidateByHigherManager(int userId, out string error)
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
                        entity.Candidate.Status = EmploymentStatus.COMPLETE;
                        entity.ApprovingHigherManager = current;
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
                int? id = EmploymentCommonDao.GetDocumentId<PersonnelManagers>(viewModel.UserId);
                if (id.HasValue)
                {
                    entity = EmploymentPersonnelManagersDao.Get(id.Value);
                }
                if (entity != null)
                {
                    if (entity.Candidate.Status == EmploymentStatus.PENDING_FINALIZATION_BY_PERSONNEL_MANAGER)
                    {
                        entity.AccessGroup = AccessGroupDao.Load(viewModel.AccessGroupId);
                        //entity.ApprovedByPersonnelManager = viewModel.ApprovedByPersonnelManager;
                        entity.AreaAddition = viewModel.AreaAddition;
                        entity.AreaMultiplier = viewModel.AreaMultiplier;
                        entity.Candidate = GetCandidate(viewModel.UserId);
                        entity.Candidate.PersonnelManagers = entity;
                        entity.Candidate.User.Grade = viewModel.Grade;
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
                        entity.NorthernAreaAddition = viewModel.NorthernAreaAddition;
                        entity.OverallExperienceDays = viewModel.OverallExperienceDays;
                        entity.OverallExperienceMonths = viewModel.OverallExperienceMonths;
                        entity.OverallExperienceYears = viewModel.OverallExperienceYears;
                        entity.PersonalAccount = viewModel.PersonalAccount;
                        entity.PersonalAccountContractor = PersonalAccountContractorDao.Load(viewModel.PersonalAccountContractorId);
                        entity.TravelRelatedAddition = viewModel.TravelRelatedAddition;

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
                    error = "Документ не найден.";
                }
            }
            else
            {
                error = "Документ может сохранить только сотрудник отдела кадров.";
            }

            return false;
        }

        #endregion

        protected bool IsCurrentUserChiefForCreator(User current, User creator)
        {

            if (!current.Level.HasValue || current.Level < MinManagerLevel || current.Level > MaxManagerLevel)
                throw new ValidationException(string.Format(StrIncorrectManagerLevel,
                        current.Level.HasValue ? current.Level.Value.ToString() : "<не указан>", current.Id));
            if (!creator.Level.HasValue || creator.Level < MinManagerLevel || creator.Level > MaxManagerLevel)
                throw new ValidationException(string.Format(StrIncorrectManagerLevel,
                        creator.Level.HasValue ? creator.Level.Value.ToString() : "<не указан>", creator.Id));
            List<DepartmentDto> departments;
            switch (current.Level)
            {
                case 2:
                    IList<int> managers2 = AppointmentDao.GetChildrenManager2ForManager2(current.Id);
                    if (managers2.Any(x => x == creator.Id && creator.Level.Value == 2))
                        return true;
                    IList<int> managers = AppointmentDao.GetManager3ForManager2(current.Id);
                    if (managers.Any(x => x == creator.Id && creator.Level.Value == 3))
                        return true;
                    departments = AppointmentDao.GetDepartmentsForManager23(current.Id, 2, true).ToList();
                    return departments.Any(x => creator.Department.Path.StartsWith(x.Path) && creator.Level == 4);
                case 3:
                    if (creator.Level != 4)
                        return false;
                    departments = AppointmentDao.GetDepartmentsForManager23(current.Id, 3, false).ToList();
                    return departments.Any(x => creator.Department.Path.StartsWith(x.Path));
                default:
                    return false;
            }
        }
    }
}