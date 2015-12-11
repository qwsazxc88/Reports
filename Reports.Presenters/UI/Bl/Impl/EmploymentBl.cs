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
//using Reports.Core.Domain.Employment2;
using Reports.Presenters.Services;
using Reports.Presenters.UI.ViewModel;
using Reports.Presenters.UI.ViewModel.Employment2;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.IO;
using System.Net.Mail;



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

        //public int RUSSIAN_FEDERATION = 643;
        public int RUSSIAN_FEDERATION = 3;

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

        //protected IAccessGroupDao accessGroupDao;
        //public IAccessGroupDao AccessGroupDao
        //{
        //    get { return Validate.Dependency(accessGroupDao); }
        //    set { accessGroupDao = value; }
        //}

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
        protected IAppointmentReportDao appointmentReportDao;
        public IAppointmentReportDao AppointmentReportDao
        {
            get { return Validate.Dependency(appointmentReportDao); }
            set { appointmentReportDao = value; }
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

        protected IEmploymentEducationTypeDao employmentEducationTypeDao;
        public IEmploymentEducationTypeDao EmploymentEducationTypeDao
        {
            get { return Validate.Dependency(employmentEducationTypeDao); }
            set { employmentEducationTypeDao = value; }
        }

        protected IEmploymentHigherEducationDiplomaDao employmentHigherEducationDiplomaDao;
        public IEmploymentHigherEducationDiplomaDao EmploymentHigherEducationDiplomaDao
        {
            get { return Validate.Dependency(employmentHigherEducationDiplomaDao); }
            set { employmentHigherEducationDiplomaDao = value; }
        }

        protected IEmploymentCandidateCommentDao employmentCandidateCommentDao;
        public IEmploymentCandidateCommentDao EmploymentCandidateCommentDao
        {
            get { return Validate.Dependency(employmentCandidateCommentDao); }
            set { employmentCandidateCommentDao = value; }
        }

        protected IEmploymentCandidateDocNeededDao employmentCandidateDocNeededDao;
        public IEmploymentCandidateDocNeededDao EmploymentCandidateDocNeededDao
        {
            get { return Validate.Dependency(employmentCandidateDocNeededDao); }
            set { employmentCandidateDocNeededDao = value; }
        }

        protected IExtraChargesDao extraChargesDao;
        public IExtraChargesDao ExtraChargesDao
        {
            get { return Validate.Dependency(extraChargesDao); }
            set { extraChargesDao = value; }
        }

        protected IStaffEstablishedPostDao staffestablishedPostDao;
        public IStaffEstablishedPostDao StaffEstablishedPostDao
        {
            get { return Validate.Dependency(staffestablishedPostDao); }
            set { staffestablishedPostDao = value; }
        }

        protected IStaffEstablishedPostUserLinksDao staffestablishedPostUserLinksDao;
        public IStaffEstablishedPostUserLinksDao StaffEstablishedPostUserLinksDao
        {
            get { return Validate.Dependency(staffestablishedPostUserLinksDao); }
            set { staffestablishedPostUserLinksDao = value; }
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
                model.CountryBirthId = entity.CountryBirth != null ? entity.CountryBirth.Id : 0;
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
                model.IsDisabilityTermLess = entity.IsDisabilityTermLess;

                model.DistrictOfBirth = entity.DistrictOfBirth;
                model.FirstName = entity.FirstName;

                foreach (var item in entity.ForeignLanguages)
                {
                    model.ForeignLanguages.Add(new ForeignLanguageDto { Id = item.Id, LanguageName = item.LanguageName, Level = item.Level });
                }

                model.INN = entity.INN;
                model.IsMale = entity.IsMale;
                model.IsPatronymicAbsent = entity.IsPatronymicAbsent;
                model.LastName = entity.LastName;

                foreach (var item in entity.NameChanges.OrderBy(x => x.Date))
                {
                    model.NameChanges.Add(new NameChangeDto { Id = item.Id, Date = item.Date, Place = item.Place, PreviousName = item.PreviousName, Reason = item.Reason });
                }

                model.Patronymic = entity.Patronymic;
                model.RegionOfBirth = entity.RegionOfBirth;
                model.SNILS = entity.SNILS;
                model.Version = entity.Version;
                model.IsDraft = !entity.IsFinal;
                model.IsFinal = entity.IsFinal;
                model.IsValidate = entity.IsValidate;

                int attachmentId = 0;
                string attachmentFilename = string.Empty;
                //скан фото
                GetAttachmentData(ref attachmentId, ref attachmentFilename, entity.Candidate.Id, RequestAttachmentTypeEnum.Photo);
                model.PhotoAttachmentId = attachmentId;
                model.PhotoAttachmentFilename = attachmentFilename;

                //скан инн
                GetAttachmentData(ref attachmentId, ref attachmentFilename, entity.Candidate.Id, RequestAttachmentTypeEnum.INNScan);
                model.INNScanAttachmentId = attachmentId;
                model.INNScanAttachmentFilename = attachmentFilename;

                //скан снилс
                GetAttachmentData(ref attachmentId, ref attachmentFilename, entity.Candidate.Id, RequestAttachmentTypeEnum.SNILSScan);
                model.SNILSScanAttachmentId = attachmentId;
                model.SNILSScanAttachmentFilename = attachmentFilename;

                //скан справик об  инвалидности
                GetAttachmentData(ref attachmentId, ref attachmentFilename, entity.Candidate.Id, RequestAttachmentTypeEnum.DisabilityCertificateScan);
                model.DisabilityCertificateScanAttachmentId = attachmentId;
                model.DisabilityCertificateScanAttachmentFilename = attachmentFilename;
            }

            //состояние кандидата
            model.CandidateStateModel = new CandidateStateModel();
            model.CandidateStateModel.CandidateState = EmploymentCandidateDao.GetCandidateState(entity == null ? -1 : entity.Candidate.Id);

            return model;
        }
        /// <summary>
        /// Заполняем списками, если не прошла форма проверку
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public GeneralInfoModel GetGeneralInfoModel(GeneralInfoModel model)
        {
            //userId = userId ?? AuthenticationService.CurrentUser.Id;
            //GeneralInfoModel model = new GeneralInfoModel { UserId = userId.Value };
            LoadDictionaries(model);
            GeneralInfo entity = null;
            int? id = EmploymentCommonDao.GetDocumentId<GeneralInfo>(model.UserId);
            if (id.HasValue)
            {
                entity = EmploymentGeneralInfoDao.Get(id.Value);
            }
            if (entity != null)
            {
                
                foreach (var item in entity.ForeignLanguages)
                {
                    model.ForeignLanguages.Add(new ForeignLanguageDto { Id = item.Id, LanguageName = item.LanguageName, Level = item.Level });
                }


                foreach (var item in entity.NameChanges.OrderBy(x => x.Date))
                {
                    model.NameChanges.Add(new NameChangeDto { Id = item.Id, Date = item.Date, Place = item.Place, PreviousName = item.PreviousName, Reason = item.Reason });
                }

                

                int attachmentId = 0;
                string attachmentFilename = string.Empty;
                //скан фото
                GetAttachmentData(ref attachmentId, ref attachmentFilename, entity.Candidate.Id, RequestAttachmentTypeEnum.Photo);
                model.PhotoAttachmentId = attachmentId;
                model.PhotoAttachmentFilename = attachmentFilename;

                //скан инн
                GetAttachmentData(ref attachmentId, ref attachmentFilename, entity.Candidate.Id, RequestAttachmentTypeEnum.INNScan);
                model.INNScanAttachmentId = attachmentId;
                model.INNScanAttachmentFilename = attachmentFilename;

                //скан снилс
                GetAttachmentData(ref attachmentId, ref attachmentFilename, entity.Candidate.Id, RequestAttachmentTypeEnum.SNILSScan);
                model.SNILSScanAttachmentId = attachmentId;
                model.SNILSScanAttachmentFilename = attachmentFilename;

                //скан справик об  инвалидности
                GetAttachmentData(ref attachmentId, ref attachmentFilename, entity.Candidate.Id, RequestAttachmentTypeEnum.DisabilityCertificateScan);
                model.DisabilityCertificateScanAttachmentId = attachmentId;
                model.DisabilityCertificateScanAttachmentFilename = attachmentFilename;

                model.IsDraft = !entity.IsFinal;
                model.IsFinal = entity.IsFinal;
                model.IsValidate = entity.IsValidate;
            }

            //состояние кандидата
            model.CandidateStateModel = new CandidateStateModel();
            model.CandidateStateModel.CandidateState = EmploymentCandidateDao.GetCandidateState(entity == null ? -1 : entity.Candidate.Id);

            return model;
        }

        protected void GetAttachmentData(ref int attachmentId, ref string attachmentFilename, int candidateId, RequestAttachmentTypeEnum type)
        {
            if (candidateId == 0)
                return;
            RequestAttachment attach = RequestAttachmentDao.FindByRequestIdAndTypeId(candidateId, type);
            if (attach == null)
            {
                attachmentId = 0;
                attachmentFilename = null;
                return;
            }
            attachmentId = attach.Id;
            attachmentFilename = attach.FileName;
        }
        /// <summary>
        /// Достаем информацию по скану документа с информацией по операции добавления скана.
        /// </summary>
        /// <param name="attachmentId">Id скана</param>
        /// <param name="attachmentFilename">Имя файла</param>
        /// <param name="candidateId">Id документа</param>
        /// <param name="type">Вид документа</param>
        /// <param name="Surname">ФИО пользователя, который добавил файл</param>
        /// <param name="OperationDate">Дата добавления скана.</param>
        protected void GetAttachmentDataWithOperationInfo(ref int attachmentId, ref string attachmentFilename, int candidateId, RequestAttachmentTypeEnum type, ref string Surname, ref DateTime? OperationDate)
        {
            if (candidateId == 0)
                return;
            RequestAttachment attach = RequestAttachmentDao.FindByRequestIdAndTypeId(candidateId, type);
            if (attach == null)
            {
                attachmentId = 0;
                attachmentFilename = null;
                return;
            }
            attachmentId = attach.Id;
            attachmentFilename = attach.FileName;
            if (attach.Creator != null)
                Surname = UserDao.Get(attach.Creator.Id).Name;
            OperationDate = attach.DateCreated;
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
                model.IsDraft = !entity.IsFinal;
                model.IsFinal = entity.IsFinal;
                model.IsValidate = entity.IsValidate;
                //скан
                int attachmentId = 0;
                string attachmentFilename = string.Empty;
                GetAttachmentData(ref attachmentId, ref attachmentFilename, entity.Candidate.Id, RequestAttachmentTypeEnum.InternalPassportScan);
                model.InternalPassportScanAttachmentId = attachmentId;
                model.InternalPassportScanAttachmentFilename = attachmentFilename;
            }
            LoadDictionaries(model);
            //состояние кандидата
            model.CandidateStateModel = new CandidateStateModel();
            model.CandidateStateModel.CandidateState = EmploymentCandidateDao.GetCandidateState(entity == null ? -1 : entity.Candidate.Id);
            return model;
        }

        public PassportModel GetPassportModel(PassportModel model)
        {
            Passport entity = null;
            int? id = EmploymentCommonDao.GetDocumentId<Passport>(model.UserId);
            if (id.HasValue)
            {
                entity = EmploymentPassportDao.Get(id.Value);
            }
            if (entity != null)
            {
                //скан
                int attachmentId = 0;
                string attachmentFilename = string.Empty;
                GetAttachmentData(ref attachmentId, ref attachmentFilename, entity.Candidate.Id, RequestAttachmentTypeEnum.InternalPassportScan);
                model.InternalPassportScanAttachmentId = attachmentId;
                model.InternalPassportScanAttachmentFilename = attachmentFilename;

                model.IsDraft = !entity.IsFinal;
                model.IsFinal = entity.IsFinal;
                model.IsValidate = entity.IsValidate;
            }
            LoadDictionaries(model);
            //состояние кандидата
            model.CandidateStateModel = new CandidateStateModel();
            model.CandidateStateModel.CandidateState = EmploymentCandidateDao.GetCandidateState(entity == null ? -1 : entity.Candidate.Id);
            return model;
        }

        public EducationModel GetEducationModel(int? userId = null)
        {
            userId = userId ?? AuthenticationService.CurrentUser.Id;
            EducationModel model = new EducationModel { UserId = userId.Value };
            Education entity = null;
            int attachmentId = 0;
            string attachmentFilename = string.Empty;

            int? id = EmploymentCommonDao.GetDocumentId<Education>(userId.Value);
            if (id.HasValue)
            {
                entity = EmploymentEducationDao.Get(id.Value);
            }
            if (entity != null)
            {
                foreach (var item in entity.Certifications.OrderBy(x => x.CertificationDate))
                {
                    model.Certifications.Add(new CertificationDto {
                        Id = item.Id,
                        CertificateDateOfIssue = item.CertificateDateOfIssue,
                        CertificateNumber = item.CertificateNumber,
                        CertificationDate = item.CertificationDate,
                        InitiatingOrder = item.InitiatingOrder,
                        LocationEI = item.LocationEI
                    });
                }

                GetAttachmentData(ref attachmentId, ref attachmentFilename, entity.Candidate.Id, RequestAttachmentTypeEnum.CertificationScan);
                model.CertificationScanId = attachmentId;
                model.CertificationScanFileName = attachmentFilename;
                model.HigherEducationDiplomas = EmploymentHigherEducationDiplomaDao.GetHighEducationTypes(entity.Id);
                //foreach (var item in entity.HigherEducationDiplomas)
                //{
                //    model.HigherEducationDiplomas.Add(new HigherEducationDiplomaDto
                //    {
                //        Id = item.Id,
                //        EducationTypeId = item.EducationTypeId,
                //        AdmissionYear = item.AdmissionYear,
                //        Department = item.Department,
                //        GraduationYear = item.GraduationYear,
                //        IssuedBy = item.IssuedBy,
                //        Number = item.Number,
                //        Profession = item.Profession,
                //        Qualification = item.Qualification,
                //        Series = item.Series,
                //        Speciality = item.Speciality
                //    });
                //}

                GetAttachmentData(ref attachmentId, ref attachmentFilename, entity.Candidate.Id, RequestAttachmentTypeEnum.HigherEducationDiplomaScan);
                model.HigherEducationDiplomaScanId = attachmentId;
                model.HigherEducationDiplomaScanFileName = attachmentFilename;

                foreach (var item in entity.PostGraduateEducationDiplomas.OrderBy(x => x.AdmissionYear))
                {
                    model.PostGraduateEducationDiplomas.Add(new PostGraduateEducationDiplomaDto
                    {
                        Id = item.Id,
                        AdmissionYear = item.AdmissionYear,
                        GraduationYear = item.GraduationYear,
                        IssuedBy = item.IssuedBy,
                        Number = item.Number,
                        Series = item.Series,
                        Speciality = item.Speciality,
                        LocationEI = item.LocationEI
                    });
                }

                GetAttachmentData(ref attachmentId, ref attachmentFilename, entity.Candidate.Id, RequestAttachmentTypeEnum.PostGraduateEducationDiplomaScan);
                model.PostGraduateEducationDiplomaScanId = attachmentId;
                model.PostGraduateEducationDiplomaScanFileName = attachmentFilename;

                foreach (var item in entity.Training.OrderBy(x => x.BeginningDate))
                {
                    model.Training.Add(new TrainingDto
                    {
                        Id = item.Id,
                        BeginningDate = item.BeginningDate,
                        CertificateIssuedBy = item.CertificateIssuedBy,
                        EndDate = item.EndDate,
                        Number = item.Number,
                        Series = item.Series,
                        Speciality = item.Speciality,
                        LocationEI = item.LocationEI
                    });
                }

                GetAttachmentData(ref attachmentId, ref attachmentFilename, entity.Candidate.Id, RequestAttachmentTypeEnum.Training);
                model.TrainingScanId = attachmentId;
                model.TrainingScanFileName = attachmentFilename;

                model.IsDraft = !entity.IsFinal;
                model.IsFinal = entity.IsFinal;
                model.IsValidate = entity.IsValidate;
            }
            LoadDictionaries(model);
            //состояние кандидата
            model.CandidateStateModel = new CandidateStateModel();
            model.CandidateStateModel.CandidateState = EmploymentCandidateDao.GetCandidateState(entity == null ? -1 : entity.Candidate.Id);
            return model;
        }

        public FamilyModel GetFamilyModel(int? userId = null)
        {
            userId = userId ?? AuthenticationService.CurrentUser.Id;
            FamilyModel model = new FamilyModel { UserId = userId.Value };
            Family entity = null;
            int attachmentId = 0;
            string attachmentFilename = string.Empty;

            int? id = EmploymentCommonDao.GetDocumentId<Family>(userId.Value);
            if (id.HasValue)
            {
                entity = EmploymentFamilyDao.Get(id.Value);
            }
            if (entity != null)
            {
                model.FamillyStatuses = EmploymentFamilyDao.GetFamilyStatuses();
                model.FamilyStatusId = entity.FamilyStatusId;
                model.Children = entity.FamilyMembers.Where<FamilyMember>(x => x.RelationshipId == FamilyRelationship.CHILD)
                    .OrderBy(x => x.DateOfBirth)
                    .ToList<FamilyMember>()
                    .ConvertAll<FamilyMemberDto>(x => new FamilyMemberDto
                    {
                        Id = x.Id,
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
                        Id = x.Id,
                        Contacts = x.Contacts,
                        DateOfBirth = x.DateOfBirth,
                        Name = x.Name,
                        PassportData = x.PassportData,
                        PlaceOfBirth = x.PlaceOfBirth,
                        WorksAt = x.WorksAt
                    })
                    .FirstOrDefault<FamilyMemberDto>();

                model.Mother = entity.FamilyMembers.Where<FamilyMember>(x => x.RelationshipId == FamilyRelationship.MOTHER)
                    .ToList<FamilyMember>()
                    .ConvertAll<FamilyMemberDto>(x => new FamilyMemberDto
                    {
                        Id = x.Id,
                        Contacts = x.Contacts,
                        DateOfBirth = x.DateOfBirth,
                        Name = x.Name,
                        PassportData = x.PassportData,
                        PlaceOfBirth = x.PlaceOfBirth,
                        WorksAt = x.WorksAt
                    })
                    .FirstOrDefault<FamilyMemberDto>();

                model.Spouse = entity.FamilyMembers.Where<FamilyMember>(x => x.RelationshipId == FamilyRelationship.SPOUSE)
                    .ToList<FamilyMember>()
                    .ConvertAll<FamilyMemberDto>(x => new FamilyMemberDto
                    {
                        Id = x.Id,
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
                }
                else
                {
                    model.IsMarried = true;
                }

                //сканы
                GetAttachmentData(ref attachmentId, ref attachmentFilename, entity.Candidate.Id, RequestAttachmentTypeEnum.MarriageCertificateScan);
                model.MarriageCertificateScanAttachmentId = attachmentId;
                model.MarriageCertificateScanAttachmentFilename = attachmentFilename;

                GetAttachmentData(ref attachmentId, ref attachmentFilename, entity.Candidate.Id, RequestAttachmentTypeEnum.ChildBirthCertificateScan);
                model.ChildBirthCertificateScanAttachmentId = attachmentId;
                model.ChildBirthCertificateScanAttachmentFilename = attachmentFilename;

                model.IsDraft = !entity.IsFinal;
                model.IsFinal = entity.IsFinal;
                model.IsValidate = entity.IsValidate;
            }
            LoadDictionaries(model);
            //состояние кандидата
            model.CandidateStateModel = new CandidateStateModel();
            model.CandidateStateModel.CandidateState = EmploymentCandidateDao.GetCandidateState(entity == null ? -1 : entity.Candidate.Id);
            return model;
        }

        public FamilyModel GetFamilyModel(FamilyModel model)
        {
            //userId = userId ?? AuthenticationService.CurrentUser.Id;
            Family entity = null;
            int attachmentId = 0;
            string attachmentFilename = string.Empty;

            int? id = EmploymentCommonDao.GetDocumentId<Family>(model.UserId);
            if (id.HasValue)
            {
                entity = EmploymentFamilyDao.Get(id.Value);
            }
            if (entity != null)
            {
                model.FamillyStatuses = EmploymentFamilyDao.GetFamilyStatuses();
                model.FamilyStatusId = entity.FamilyStatusId;
                
                model.Cohabitants = entity.Cohabitants;

                if (model.Spouse == null)
                {
                    model.IsMarried = false;
                }
                else
                {
                    model.IsMarried = true;
                }

                //сканы
                GetAttachmentData(ref attachmentId, ref attachmentFilename, entity.Candidate.Id, RequestAttachmentTypeEnum.MarriageCertificateScan);
                model.MarriageCertificateScanAttachmentId = attachmentId;
                model.MarriageCertificateScanAttachmentFilename = attachmentFilename;

                GetAttachmentData(ref attachmentId, ref attachmentFilename, entity.Candidate.Id, RequestAttachmentTypeEnum.ChildBirthCertificateScan);
                model.ChildBirthCertificateScanAttachmentId = attachmentId;
                model.ChildBirthCertificateScanAttachmentFilename = attachmentFilename;

                model.IsDraft = !entity.IsFinal;
                model.IsFinal = entity.IsFinal;
                model.IsValidate = entity.IsValidate;
            }
            LoadDictionaries(model);
            //состояние кандидата
            model.CandidateStateModel = new CandidateStateModel();
            model.CandidateStateModel.CandidateState = EmploymentCandidateDao.GetCandidateState(entity == null ? -1 : entity.Candidate.Id);
            return model;
        }

        public MilitaryServiceModel GetMilitaryServiceModel(int? userId = null)
        {
            userId = userId ?? AuthenticationService.CurrentUser.Id;
            MilitaryServiceModel model = new MilitaryServiceModel { UserId = userId.Value };
            MilitaryService entity = null;
            int attachmentId = 0;
            string attachmentFilename = string.Empty;

            int? id = EmploymentCommonDao.GetDocumentId<MilitaryService>(userId.Value);
            if (id.HasValue)
            {
                entity = EmploymentMilitaryServiceDao.Get(id.Value);
            }
            if (entity != null)
            {
                model.MilitaryValidityCategoryId = entity.MilitaryValidityCategoryId;
                model.Commissariat = entity.Commissariat;
                model.CommonMilitaryServiceRegistrationInfo = entity.CommonMilitaryServiceRegistrationInfo;
                model.ConscriptionStatus = entity.ConscriptionStatus;
                model.IsAssigned = entity.IsAssigned;
                model.IsLiableForMilitaryService = entity.IsLiableForMilitaryService;
                model.IsReserved = entity.IsReserved;
                model.MilitaryCardDate = entity.MilitaryCardDate;
                model.MilitaryCardNumber = entity.MilitaryCardNumber;
                model.MilitaryRelationAccountId = entity.MilitaryRelationAccountId;
                model.MilitarySpecialityCode = entity.MilitarySpecialityCode;
                model.MobilizationTicketNumber = entity.MobilizationTicketNumber;
                model.PersonnelCategory = entity.PersonnelCategory;
                model.PersonnelType = entity.PersonnelType;
                model.RankId = entity.RankId;
                model.RegistrationExpiration = entity.RegistrationExpiration;
                model.ReserveCategory = entity.ReserveCategory;
                model.SpecialityCategoryId = entity.SpecialityCategoryId;
                model.SpecialMilitaryServiceRegistrationInfo = entity.SpecialMilitaryServiceRegistrationInfo;

                //MilitaryRelationAccount

                model.IsDraft = !entity.IsFinal;
                model.IsFinal = entity.IsFinal;
                model.IsValidate = entity.IsValidate;

                //сканы
                GetAttachmentData(ref attachmentId, ref attachmentFilename, entity.Candidate.Id, RequestAttachmentTypeEnum.MilitaryCardScan);
                model.MilitaryCardScanAttachmentId = attachmentId;
                model.MilitaryCardScanAttachmentFilename = attachmentFilename;

                GetAttachmentData(ref attachmentId, ref attachmentFilename, entity.Candidate.Id, RequestAttachmentTypeEnum.MobilizationTicketScan);
                model.MobilizationTicketScanAttachmentId = attachmentId;
                model.MobilizationTicketScanAttachmentFilename = attachmentFilename;
                //model.ReserveCategoryId = entity.ReserveCategoryId != null ? entity.ReserveCategoryId.Value : 0;
                model.ReserveCategoryId = entity.ReserveCategoryId;
            }
            LoadDictionaries(model);
            //состояние кандидата
            model.CandidateStateModel = new CandidateStateModel();
            model.CandidateStateModel.CandidateState = EmploymentCandidateDao.GetCandidateState(entity == null ? -1 : entity.Candidate.Id);
            return model;
        }

        public MilitaryServiceModel GetMilitaryServiceModel(MilitaryServiceModel model)
        {

            MilitaryService entity = null;
            int attachmentId = 0;
            string attachmentFilename = string.Empty;

            int? id = EmploymentCommonDao.GetDocumentId<MilitaryService>(model.UserId);
            if (id.HasValue)
            {
                entity = EmploymentMilitaryServiceDao.Get(id.Value);
            }
            if (entity != null)
            {

                //сканы
                GetAttachmentData(ref attachmentId, ref attachmentFilename, entity.Candidate.Id, RequestAttachmentTypeEnum.MilitaryCardScan);
                model.MilitaryCardScanAttachmentId = attachmentId;
                model.MilitaryCardScanAttachmentFilename = attachmentFilename;

                GetAttachmentData(ref attachmentId, ref attachmentFilename, entity.Candidate.Id, RequestAttachmentTypeEnum.MobilizationTicketScan);
                model.MobilizationTicketScanAttachmentId = attachmentId;
                model.MobilizationTicketScanAttachmentFilename = attachmentFilename;

                model.IsDraft = !entity.IsFinal;
                model.IsFinal = entity.IsFinal;
                model.IsValidate = entity.IsValidate;
            }

            LoadDictionaries(model);
            //состояние кандидата
            model.CandidateStateModel = new CandidateStateModel();
            model.CandidateStateModel.CandidateState = EmploymentCandidateDao.GetCandidateState(entity == null ? -1 : entity.Candidate.Id);
            return model;
        }

        public ExperienceModel GetExperienceModel(int? userId = null)
        {
            userId = userId ?? AuthenticationService.CurrentUser.Id;
            ExperienceModel model = new ExperienceModel { UserId = userId.Value };
            Experience entity = null;
            int attachmentId = 0;
            string attachmentFilename = string.Empty;

            int? id = EmploymentCommonDao.GetDocumentId<Experience>(userId.Value);
            if (id.HasValue)
            {
                entity = EmploymentExperienceDao.Get(id.Value);
            }
            if (entity != null)
            {
                foreach (var item in entity.ExperienceItems.OrderBy(x => x.BeginningDate))
                {
                    model.ExperienceItems.Add(new ExperienceItemDto
                    {
                        Id = item.Id,
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
                model.IsDraft = !entity.IsFinal;
                model.IsFinal = entity.IsFinal;
                model.IsValidate = entity.IsValidate;

                //сканы
                GetAttachmentData(ref attachmentId, ref attachmentFilename, entity.Candidate.Id, RequestAttachmentTypeEnum.WorkbookScan);
                model.WorkBookScanAttachmentId = attachmentId;
                model.WorkBookScanAttachmentFilename = attachmentFilename;

                GetAttachmentData(ref attachmentId, ref attachmentFilename, entity.Candidate.Id, RequestAttachmentTypeEnum.WorkbookSupplementScan);
                model.WorkBookSupplementScanAttachmentId = attachmentId;
                model.WorkBookSupplementScanAttachmentFilename = attachmentFilename;
            }
            LoadDictionaries(model);
            //состояние кандидата
            model.CandidateStateModel = new CandidateStateModel();
            model.CandidateStateModel.CandidateState = EmploymentCandidateDao.GetCandidateState(entity == null ? -1 : entity.Candidate.Id);
            return model;
        }

        public ExperienceModel GetExperienceModel(ExperienceModel model)
        {

            Experience entity = null;
            int attachmentId = 0;
            string attachmentFilename = string.Empty;

            int? id = EmploymentCommonDao.GetDocumentId<Experience>(model.UserId);
            if (id.HasValue)
            {
                entity = EmploymentExperienceDao.Get(id.Value);
            }
            if (entity != null)
            {
                foreach (var item in entity.ExperienceItems.OrderBy(x => x.BeginningDate))
                {
                    model.ExperienceItems.Add(new ExperienceItemDto
                    {
                        Id = item.Id,
                        BeginningDate = item.BeginningDate,
                        Company = item.Company,
                        CompanyContacts = item.CompanyContacts,
                        EndDate = item.EndDate,
                        Position = item.Position
                    });
                }

                model.IsDraft = !entity.IsFinal;
                model.IsFinal = entity.IsFinal;
                model.IsValidate = entity.IsValidate;

                //сканы
                GetAttachmentData(ref attachmentId, ref attachmentFilename, entity.Candidate.Id, RequestAttachmentTypeEnum.WorkbookScan);
                model.WorkBookScanAttachmentId = attachmentId;
                model.WorkBookScanAttachmentFilename = attachmentFilename;

                GetAttachmentData(ref attachmentId, ref attachmentFilename, entity.Candidate.Id, RequestAttachmentTypeEnum.WorkbookSupplementScan);
                model.WorkBookSupplementScanAttachmentId = attachmentId;
                model.WorkBookSupplementScanAttachmentFilename = attachmentFilename;
            }
            LoadDictionaries(model);
            //состояние кандидата
            model.CandidateStateModel = new CandidateStateModel();
            model.CandidateStateModel.CandidateState = EmploymentCandidateDao.GetCandidateState(entity == null ? -1 : entity.Candidate.Id);
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
                model.Country = entity.Country;
                model.Republic = entity.Republic;
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
                model.IsDraft = !entity.IsFinal;
                model.IsFinal = entity.IsFinal;
                model.IsValidate = entity.IsValidate;
            }
            LoadDictionaries(model);
            //состояние кандидата
            model.CandidateStateModel = new CandidateStateModel();
            model.CandidateStateModel.CandidateState = EmploymentCandidateDao.GetCandidateState(entity == null ? -1 : entity.Candidate.Id);
            return model;
        }

        public ContactsModel GetContactsModel(ContactsModel model)
        {
            Contacts entity = null;
            int? id = EmploymentCommonDao.GetDocumentId<Contacts>(model.UserId);
            if (id.HasValue)
            {
                entity = EmploymentContactsDao.Get(id.Value);
            }
            if (entity != null)
            {
                model.IsDraft = !entity.IsFinal;
                model.IsFinal = entity.IsFinal;
                model.IsValidate = entity.IsValidate;
            }
            LoadDictionaries(model);
            //состояние кандидата
            model.CandidateStateModel = new CandidateStateModel();
            model.CandidateStateModel.CandidateState = EmploymentCandidateDao.GetCandidateState(entity == null ? -1 : entity.Candidate.Id);
            return model;
        }

        public BackgroundCheckModel GetBackgroundCheckModel(int? userId = null)
        {
            userId = userId ?? AuthenticationService.CurrentUser.Id;
            BackgroundCheckModel model = new BackgroundCheckModel { UserId = userId.Value };
            BackgroundCheck entity = null;
            int attachmentId = 0;
            string attachmentFilename = string.Empty;

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
                model.HasAutomobile = entity.HasAutomobile;//entity.AutomobileMake != null && entity.AutomobileMake.Length > 0;
                model.HasDriversLicense = entity.HasDriversLicense;//entity.DriversLicenseDateOfIssue.HasValue;
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
                model.OwnerOfShares = entity.OwnerOfShares;
                model.PositionInGoverningBodies = entity.PositionInGoverningBodies;
                foreach (var item in entity.References)
                {
                    model.References.Add(new ReferenceDto
                    {
                        Id = item.Id,
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

                model.IsDraft = !entity.IsFinal;
                model.IsFinal = entity.IsFinal;
                model.IsValidate = entity.IsValidate;

                //сылка на Pyrus
                model.PyrusRef = entity.PyrusRef;

                //сканы
                GetAttachmentData(ref attachmentId, ref attachmentFilename, entity.Candidate.Id, RequestAttachmentTypeEnum.PersonalDataProcessingScan);
                model.PersonalDataProcessingScanAttachmentId = attachmentId;
                model.PersonalDataProcessingScanAttachmentFilename = attachmentFilename;

                GetAttachmentData(ref attachmentId, ref attachmentFilename, entity.Candidate.Id, RequestAttachmentTypeEnum.InfoValidityScan);
                model.InfoValidityScanAttachmentId = attachmentId;
                model.InfoValidityScanAttachmentFilename = attachmentFilename;

                model.IsApprovalSkipped = entity.IsApprovalSkipped;
                model.ApproverName = entity.Approver == null ? string.Empty : entity.Approver.Name;
                model.ApprovalStatus = entity.ApprovalStatus;
                model.ApprovalDate = entity.ApprovalDate;

                model.IsApproveBySecurityAvailable = (entity.Candidate.Status == EmploymentStatus.PENDING_APPROVAL_BY_SECURITY)
                        && ((AuthenticationService.CurrentUser.UserRole & UserRole.Security) == UserRole.Security);

                model.Comments = EmploymentCandidateCommentDao.GetComments(entity.Candidate.User.Id, (int)EmploymentCommentTypeEnum.BackgroundCheck);
                model.IsAddCommentAvailable = (AuthenticationService.CurrentUser.UserRole & UserRole.Security) > 0 ||
                    (AuthenticationService.CurrentUser.UserRole & UserRole.PersonnelManager) > 0 ? true : false;

                model.SendTo1C = entity.Candidate.SendTo1C;
                //если была выгрузка в 1С, то закрываем эту кнопку для кандидата
                model.IsPrintButtonAvailable = model.SendTo1C.HasValue ? false : true;
                //кадровикам удаление доступно всегда
                model.IsDeleteScanButtonAvailable = (AuthenticationService.CurrentUser.UserRole & UserRole.OutsourcingManager) > 0 ? true : (entity.ApprovalDate.HasValue ? false : true);
                //кадровикам удаление доступно всегда
                model.IsDeleteScanButtonAvailable = (AuthenticationService.CurrentUser.UserRole & UserRole.PersonnelManager) > 0 && !entity.IsFinal ? true : model.IsDeleteScanButtonAvailable;
                GetAttachmentData(ref attachmentId, ref attachmentFilename, entity.Candidate.Id, RequestAttachmentTypeEnum.EmploymentFileScan);
                model.EmploymentFileId = attachmentId;
                model.EmploymentFileName = attachmentFilename;

                //для консультантов даем возможность отменить отклонение
                if (AuthenticationService.CurrentUser.UserRole == UserRole.ConsultantOutsourcing)
                {
                    if (entity.Candidate.Status == EmploymentStatus.REJECTED && entity.ApprovalStatus.HasValue && !entity.ApprovalStatus.Value)
                        model.IsCancelApproveAvailale = true;
                }

                //определяем признак кандидата из Экспресс-Волги
                Department ParentDep = DepartmentDao.Get(11923);
                IList<IdNameDto> volgadeps = ParentDep != null ? DepartmentDao.LoadAll().Where(x => x.Path.StartsWith(ParentDep.Path)).ToList().ConvertAll(x => new IdNameDto { Id = x.Id, Name = x.Name }) : null;
                model.IsVolga = volgadeps != null && volgadeps.Where(x => x.Id == entity.Candidate.User.Department.Id).Count() != 0 ? true : false;
            }
            LoadDictionaries(model);
            //состояние кандидата
            model.CandidateStateModel = new CandidateStateModel();
            model.CandidateStateModel.CandidateState = EmploymentCandidateDao.GetCandidateState(entity == null ? -1 : entity.Candidate.Id);
            return model;
        }

        public BackgroundCheckModel GetBackgroundCheckModel(BackgroundCheckModel model)
        {
            BackgroundCheck entity = null;
            int attachmentId = 0;
            string attachmentFilename = string.Empty;

            int? id = EmploymentCommonDao.GetDocumentId<BackgroundCheck>(model.UserId);
            if (id.HasValue)
            {
                entity = EmploymentBackgroundCheckDao.Get(id.Value);
            }
            if (entity != null)
            {
                foreach (var item in entity.References)
                {
                    model.References.Add(new ReferenceDto
                    {
                        Id = item.Id,
                        FirstName = item.FirstName,
                        LastName = item.LastName,
                        Patronymic = item.Patronymic,
                        Phone = item.Phone,
                        Position = item.Position,
                        Relation = item.Relation,
                        WorksAt = item.WorksAt
                    });
                }

                model.IsDraft = !entity.IsFinal;
                model.IsFinal = entity.IsFinal;
                model.IsValidate = entity.IsValidate;

                //сканы
                GetAttachmentData(ref attachmentId, ref attachmentFilename, entity.Candidate.Id, RequestAttachmentTypeEnum.PersonalDataProcessingScan);
                model.PersonalDataProcessingScanAttachmentId = attachmentId;
                model.PersonalDataProcessingScanAttachmentFilename = attachmentFilename;

                GetAttachmentData(ref attachmentId, ref attachmentFilename, entity.Candidate.Id, RequestAttachmentTypeEnum.InfoValidityScan);
                model.InfoValidityScanAttachmentId = attachmentId;
                model.InfoValidityScanAttachmentFilename = attachmentFilename;

                model.IsApprovalSkipped = entity.IsApprovalSkipped;
                model.ApproverName = entity.Approver == null ? string.Empty : entity.Approver.Name;
                model.ApprovalStatus = entity.ApprovalStatus;
                model.ApprovalDate = entity.ApprovalDate;

                model.IsApproveBySecurityAvailable = (entity.Candidate.Status == EmploymentStatus.PENDING_APPROVAL_BY_SECURITY)
                        && ((AuthenticationService.CurrentUser.UserRole & UserRole.Security) == UserRole.Security);

                //для консультантов даем возможность отменить отклонение
                if (AuthenticationService.CurrentUser.UserRole == UserRole.ConsultantOutsourcing)
                {
                    if (entity.Candidate.Status == EmploymentStatus.REJECTED && entity.ApprovalStatus.HasValue && !entity.ApprovalStatus.Value)
                        model.IsCancelApproveAvailale = true;
                }

                //определяем признак кандидата из Экспресс-Волги
                Department ParentDep = DepartmentDao.Get(11923);
                IList<IdNameDto> volgadeps = ParentDep != null ? DepartmentDao.LoadAll().Where(x => x.Path.StartsWith(ParentDep.Path)).ToList().ConvertAll(x => new IdNameDto { Id = x.Id, Name = x.Name }) : null;
                model.IsVolga = volgadeps != null && volgadeps.Where(x => x.Id == entity.Candidate.User.Department.Id).Count() != 0 ? true : false;
            }
            LoadDictionaries(model);
            //состояние кандидата
            model.CandidateStateModel = new CandidateStateModel();
            model.CandidateStateModel.CandidateState = EmploymentCandidateDao.GetCandidateState(entity == null ? -1 : entity.Candidate.Id);
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

                model.IsDraft = !entity.IsFinal;
                model.IsFinal = entity.IsFinal;
            }
            else
            {
                model.IsDraft = true;
                model.IsFinal = false;
                model.IsApproveByTrainerAvailable = true;
            }

            LoadDictionaries(model);
            //состояние кандидата
            model.CandidateStateModel = new CandidateStateModel();
            model.CandidateStateModel.CandidateState = EmploymentCandidateDao.GetCandidateState(entity == null ? -1 : entity.Candidate.Id);
            return model;
        }

        public ApplicationLetterModel GetApplicationLetterModel(int? userId = null)
        {
            userId = userId ?? AuthenticationService.CurrentUser.Id;
            EmploymentCandidate candidate = GetCandidate(userId.Value);
            ApplicationLetterModel model = new ApplicationLetterModel { UserId = userId.Value };

            //int attachmentId = 0;
            //string attachmentFilename = string.Empty;
            //GetAttachmentData(ref attachmentId, ref attachmentFilename, candidate.Id, RequestAttachmentTypeEnum.ApplicationLetterScan);
            //model.ApplicationLetterScanAttachmentId = attachmentId;
            //model.ApplicationLetterScanAttachmentFilename = attachmentFilename;
            //model.IsApplicationLetterUploadAvailable = candidate.Status == EmploymentStatus.PENDING_APPLICATION_LETTER && !(model.ApplicationLetterScanAttachmentId > 0);

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
                model.IsExternalPTWorker = entity.IsExternalPTWorker;
                model.IsFront = entity.IsFront;
                model.IsLiable = entity.IsLiable;
                model.PositionId = entity.Position != null ? entity.Position.Id : 0;
                model.PositionName = model.PositionItems != null && model.PositionId != 0 ? model.PositionItems.Where(x => x.Value == model.PositionId.ToString()).Single().Text : "";
                model.ProbationaryPeriod = entity.ProbationaryPeriod;
                model.RequestNumber = entity.RequestNumber;
                //оклад и надбавки
                model.SalaryBasis = entity.SalaryBasis;
                model.SalaryMultiplier = entity.SalaryMultiplier;
                model.AreaMultiplier = entity.Candidate.PersonnelManagers.AreaMultiplier;
                model.PersonalAddition = entity.Candidate.PersonnelManagers.PersonalAddition;
                model.PositionAddition = entity.Candidate.PersonnelManagers.PositionAddition;
                model.AreaAddition = entity.Candidate.PersonnelManagers.AreaAddition;
                model.TravelRelatedAddition = entity.Candidate.PersonnelManagers.TravelRelatedAddition;
                model.CompetenceAddition = entity.Candidate.PersonnelManagers.CompetenceAddition;
                model.FrontOfficeExperienceAddition = entity.Candidate.PersonnelManagers.FrontOfficeExperienceAddition;

                model.WorkCity = entity.WorkCity;
                model.RegistrationDate = entity.RegistrationDate;
                
                model.ApprovingManagerName = entity.ApprovingManager != null ? entity.ApprovingManager.Name : string.Empty;
                model.ApprovingHigherManagerName = entity.ApprovingHigherManager != null ? entity.ApprovingHigherManager.Name : string.Empty;
                model.ManagerApprovalDate = entity.ManagerApprovalDate;
                model.ManagerRejectionReason = entity.ManagerRejectionReason;

                model.ManagerApprovalStatus = entity.ManagerApprovalStatus;
                model.HigherManagerApprovalStatus = entity.HigherManagerApprovalStatus;
                model.HigherManagerApprovalDate = entity.HigherManagerApprovalDate;
                model.HigherManagerRejectionReason = entity.HigherManagerRejectionReason;
                model.SendTo1C = entity.Candidate.SendTo1C;
                model.MentorName = entity.MentorName;
                model.PyrusNumber = entity.PyrusNumber;

                model.Comments = EmploymentCandidateCommentDao.GetComments(entity.Candidate.User.Id, (int)EmploymentCommentTypeEnum.Managers);
                model.IsAddCommentAvailable = (AuthenticationService.CurrentUser.UserRole & UserRole.Manager) > 0 ||
                    (AuthenticationService.CurrentUser.UserRole & UserRole.PersonnelManager) > 0 ? true : false;

                
                //определяем признак кандидата из Экспресс-Волги
                Department ParentDep = DepartmentDao.Get(11923);
                IList<IdNameDto> volgadeps = ParentDep != null ? DepartmentDao.LoadAll().Where(x => x.Path.StartsWith(ParentDep.Path)).ToList().ConvertAll(x => new IdNameDto { Id = x.Id, Name = x.Name }) : null;
                model.IsVolga = volgadeps != null && volgadeps.Where(x => x.Id == entity.Candidate.User.Department.Id).Count() != 0 ? true : false;

                //для консультантов даем возможность отменить отклонение
                if (AuthenticationService.CurrentUser.UserRole == UserRole.ConsultantOutsourcing)
                {
                    if (entity.Candidate.Status == EmploymentStatus.REJECTED && (entity.ManagerApprovalStatus.HasValue && !entity.ManagerApprovalStatus.Value) && entity.ApprovingManager != null)
                        model.IsCancelApproveAvailale = true;
                    if (entity.Candidate.Status == EmploymentStatus.REJECTED && (entity.HigherManagerApprovalStatus.HasValue && !entity.HigherManagerApprovalStatus.Value) && entity.ApprovingHigherManager != null)
                        model.IsCancelApproveHigherAvailale = true;
                }
            }

            EmploymentCandidate candidate = GetCandidate(userId.Value);
            
            //решили, что согласовать кандидата может не только руководитель-инициатор, но и его зам
            //по текущему пользователю определяем, находится ли руководитель-инициатор в его подразделении
            //для отображения списка
            IList<User> managers = DepartmentDao.GetDepartmentManagers(candidate.AppointmentCreator.Department.Id, false)
                        .Where<User>(x => x.Level == candidate.AppointmentCreator.Level && x.RoleId == (int)UserRole.Manager /*&& x.Id == candidate.AppointmentCreator.Id*/)
                        .ToList<User>();

            User currentUser = UserDao.Load(AuthenticationService.CurrentUser.Id);
            if (currentUser.UserRole == UserRole.Manager)
            {
                IList<User> managersApproval = DepartmentDao.GetDepartmentManagers(currentUser.Department.Id, false)
                        .Where<User>(x => x.Level == currentUser.Level && x.RoleId == (int)UserRole.Manager && x.Id == candidate.AppointmentCreator.Id)
                        .ToList<User>();
                //согласовывает руководитель-инициатор
                model.IsApproveByManagerAvailable = (candidate.Status == EmploymentStatus.PENDING_APPROVAL_BY_MANAGER)
                    && ((AuthenticationService.CurrentUser.UserRole & UserRole.Manager) == UserRole.Manager)
                    && (candidate.AppointmentCreator.Id == AuthenticationService.CurrentUser.Id || managersApproval.Count != 0);
            }
            else
                model.IsApproveByManagerAvailable = false;


            //список руководителей
            foreach (var item in managers)
            {
                model.ManagerApprovalList += (string.IsNullOrEmpty(model.ManagerApprovalList) ? "" : ", ") + item.Name + "(" + item.Position.Name + ")";
            }

            
            //утверждать кандидата может руководитель выше уровнем, чем руководитель-инициатор
            //автоматическая привязка утверждающего
            IList<User> HighManagers = DepartmentDao.GetDepartmentManagers(candidate.AppointmentCreator.Department.Id, true)
                .Where<User>(x => x.Level < candidate.AppointmentCreator.Level /*&& x.Level != candidate.AppointmentCreator.Level*/ && x.Level >= (candidate.AppointmentCreator.Level > 3 ? 3 : 2))
                .OrderByDescending<User, int?>(manager => manager.Level)
                .ToList<User>();
            //ручная привязка утверждающего
            IList<User> manualRoleManagers = ManualRoleRecordDao.GetManualRoleHoldersForUser(candidate.AppointmentCreator.Id, UserManualRole.ApprovesEmployment);

            
            model.IsApproveByHigherManagerAvailable = (candidate.Status == EmploymentStatus.PENDING_APPROVAL_BY_HIGHER_MANAGER)
                && ((AuthenticationService.CurrentUser.UserRole & UserRole.Manager) == UserRole.Manager)
                && (HighManagers.Where<User>(x => x.Id == AuthenticationService.CurrentUser.Id).ToList<User>().Count != 0 ||
                    manualRoleManagers.Where<User>(x => x.Id == AuthenticationService.CurrentUser.Id).ToList<User>().Count != 0);

            //список 
            foreach (var item in HighManagers)
            {
                model.HigherManagerApprovalList += (string.IsNullOrEmpty(model.HigherManagerApprovalList) ? "" : ", ") + item.Name + "(" + item.Position.Name + ")";
            }

            foreach (var item in manualRoleManagers)
            {
                model.HigherManagerApprovalList += (string.IsNullOrEmpty(model.HigherManagerApprovalList) ? "" : ", ") + item.Name + "(" + item.Position.Name + ")";
            }

            //состояние кандидата
            model.CandidateStateModel = new CandidateStateModel();
            model.CandidateStateModel.CandidateState = EmploymentCandidateDao.GetCandidateState(entity == null ? -1 : entity.Candidate.Id);

            model.IsPyrusDialogVisible = AuthenticationService.CurrentUser.UserRole == UserRole.OutsourcingManager || AuthenticationService.CurrentUser.UserRole == UserRole.Manager ? true : false;


            StaffEstablishedPostUserLinks PostUserLink = StaffEstablishedPostUserLinksDao.GetPostUserLinkByDocId(entity.Candidate.Id, (int)StaffReserveTypeEnum.Employment);

            if (PostUserLink != null)
                model.UserLinkId = PostUserLink.Id;

            model.IsConsultant = AuthenticationService.CurrentUser.UserRole == UserRole.ConsultantOutsourcing ? true : false;
            model.SalaryTotal = CalculateCandidateSalary(entity).ToString("C", System.Globalization.CultureInfo.CurrentCulture);
            
            LoadDictionaries(model);

            return model;
        }

        public ManagersModel GetManagersModel(ManagersModel model)
        {
            Managers entity = null;
            int? id = EmploymentCommonDao.GetDocumentId<Managers>(model.UserId);
            if (id.HasValue)
            {
                entity = EmploymentManagersDao.Get(id.Value);
            }

            if (entity != null)
            {
                //для консультантов даем возможность отменить отклонение
                if (AuthenticationService.CurrentUser.UserRole == UserRole.ConsultantOutsourcing)
                {
                    if (entity.Candidate.Status == EmploymentStatus.REJECTED && (entity.ManagerApprovalStatus.HasValue && !entity.ManagerApprovalStatus.Value) && entity.ApprovingManager != null)
                        model.IsCancelApproveAvailale = true;
                    if (entity.Candidate.Status == EmploymentStatus.REJECTED && (entity.HigherManagerApprovalStatus.HasValue && !entity.HigherManagerApprovalStatus.Value) && entity.ApprovingHigherManager != null)
                        model.IsCancelApproveHigherAvailale = true;
                }

                //определяем признак кандидата из Экспресс-Волги
                Department ParentDep = DepartmentDao.Get(11923);
                IList<IdNameDto> volgadeps = ParentDep != null ? DepartmentDao.LoadAll().Where(x => x.Path.StartsWith(ParentDep.Path)).ToList().ConvertAll(x => new IdNameDto { Id = x.Id, Name = x.Name }) : null;
                model.IsVolga = volgadeps != null && volgadeps.Where(x => x.Id == entity.Candidate.User.Department.Id).Count() != 0 ? true : false;
            }

            EmploymentCandidate candidate = GetCandidate(model.UserId);

            //решили, что согласовать кандидата может не только руководитель-инициатор, но и его зам
            //по текущему пользователю определяем, находится ли руководитель-инициатор в его подразделении
            //для отображения списка
            IList<User> managers = DepartmentDao.GetDepartmentManagers(candidate.AppointmentCreator.Department.Id, false)
                        .Where<User>(x => x.Level == candidate.AppointmentCreator.Level && x.RoleId == (int)UserRole.Manager /*&& x.Id == candidate.AppointmentCreator.Id*/)
                        .ToList<User>();

            User currentUser = UserDao.Load(AuthenticationService.CurrentUser.Id);
            if (currentUser.UserRole == UserRole.Manager)
            {
                IList<User> managersApproval = DepartmentDao.GetDepartmentManagers(currentUser.Department.Id, false)
                        .Where<User>(x => x.Level == currentUser.Level && x.RoleId == (int)UserRole.Manager && x.Id == candidate.AppointmentCreator.Id)
                        .ToList<User>();
                //согласовывает руководитель-инициатор
                model.IsApproveByManagerAvailable = (candidate.Status == EmploymentStatus.PENDING_APPROVAL_BY_MANAGER)
                    && ((AuthenticationService.CurrentUser.UserRole & UserRole.Manager) == UserRole.Manager)
                    && (candidate.AppointmentCreator.Id == AuthenticationService.CurrentUser.Id || managersApproval.Count != 0);
            }
            else
                model.IsApproveByManagerAvailable = false;


            //список руководителей
            foreach (var item in managers)
            {
                model.ManagerApprovalList += (string.IsNullOrEmpty(model.ManagerApprovalList) ? "" : ", ") + item.Name;
            }


            //утверждать кандидата может руководитель выше уровнем, чем руководитель-инициатор
            //автоматическая привязка утверждающего
            IList<User> HighManagers = DepartmentDao.GetDepartmentManagers(candidate.AppointmentCreator.Department.Id, true)
                .Where<User>(x => x.Level < candidate.AppointmentCreator.Level /*&& x.Level != candidate.AppointmentCreator.Level*/ && x.Level >= (candidate.AppointmentCreator.Level > 3 ? 3 : 2))
                .OrderByDescending<User, int?>(manager => manager.Level)
                .ToList<User>();
            //ручная привязка утверждающего
            IList<User> manualRoleManagers = ManualRoleRecordDao.GetManualRoleHoldersForUser(candidate.AppointmentCreator.Id, UserManualRole.ApprovesEmployment);


            model.IsApproveByHigherManagerAvailable = (candidate.Status == EmploymentStatus.PENDING_APPROVAL_BY_HIGHER_MANAGER)
                && ((AuthenticationService.CurrentUser.UserRole & UserRole.Manager) == UserRole.Manager)
                && (HighManagers.Where<User>(x => x.Id == AuthenticationService.CurrentUser.Id).ToList<User>().Count != 0 ||
                    manualRoleManagers.Where<User>(x => x.Id == AuthenticationService.CurrentUser.Id).ToList<User>().Count != 0);

            //список 
            foreach (var item in HighManagers)
            {
                model.HigherManagerApprovalList += (string.IsNullOrEmpty(model.HigherManagerApprovalList) ? "" : ", ") + item.Name;
            }

            foreach (var item in manualRoleManagers)
            {
                model.HigherManagerApprovalList += (string.IsNullOrEmpty(model.HigherManagerApprovalList) ? "" : ", ") + item.Name;
            }

            model.IsConsultant = AuthenticationService.CurrentUser.UserRole == UserRole.ConsultantOutsourcing ? true : false;
            model.SalaryTotal = CalculateCandidateSalary(entity).ToString("C", System.Globalization.CultureInfo.CurrentCulture);

            LoadDictionaries(model);
            //состояние кандидата
            model.CandidateStateModel = new CandidateStateModel();
            model.CandidateStateModel.CandidateState = EmploymentCandidateDao.GetCandidateState(entity == null ? -1 : entity.Candidate.Id);
            return model;
        }

        public IList<IdNameDto> GetPositionAutocomplete(string Name)
        {
            return PositionDao.GetPositions(Name);
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

            LoadDictionaries(model);

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
                model.BasicSalary = entity.BasicSalary;
                model.IsContractChangedToIndefinite = entity.SupplementaryAgreements != null && entity.SupplementaryAgreements.Count > 0;
                if (entity.SupplementaryAgreements != null && entity.SupplementaryAgreements.Count > 0)
                {
                    model.SupplementaryAgreementCreateDate = entity.SupplementaryAgreements[0].CreateDate;
                    model.SupplementaryAgreementNumber = entity.SupplementaryAgreements[0].Number;
                    model.ChangeContractToIndefiniteOrderCreateDate = entity.SupplementaryAgreements[0].OrderCreateDate;
                    model.ChangeContractToIndefiniteOrderNumber = entity.SupplementaryAgreements[0].OrderNumber;
                }

                //model.Level = entity.Candidate.User.Level;
                model.NorthernAreaAddition = entity.NorthernAreaAddition;
                model.OverallExperienceDays = entity.OverallExperienceDays;
                model.OverallExperienceMonths = entity.OverallExperienceMonths;
                model.OverallExperienceYears = entity.OverallExperienceYears;
                model.PersonalAccount = entity.PersonalAccount;
                model.PersonalAccountContractorId = entity.PersonalAccountContractor != null ? entity.PersonalAccountContractor.Id : 0;
                model.SignerId = entity.Signer != null ? entity.Signer.Id : 0;
                model.TravelRelatedAddition = entity.TravelRelatedAddition;
                model.InsuredPersonTypeId = entity.InsuredPersonType != null ? (int?)entity.InsuredPersonType.Id : null;
                model.InsuredPersonTypeSelectedName =
                    model.InsuredPersonTypeId.HasValue
                    ? model.InsuredPersonTypeItems.Where(x => x.Value == model.InsuredPersonTypeId.ToString()).FirstOrDefault().Text
                    : string.Empty;
                model.StatusId = entity.Status ?? 0;

                model.ScheduleId = entity.Schedule != null ? (int?)entity.Schedule.Id : null;

                model.ContractPoint_1_Id = entity.ContractPoint_1_Id;
                model.ContractPoint_2_Id = entity.ContractPoint_2_Id;
                model.ContractPoint_3_Id = entity.ContractPoint_3_Id;
                model.ContractPointsFio = entity.ContractPointsFio;
                model.ContractPointsAddress = entity.ContractPointsAddress;

                model.NorthExperienceYears = entity.NorthExperienceYears;
                model.NorthExperienceMonths = entity.NorthExperienceMonths;
                model.NorthExperienceDays = entity.NorthExperienceDays;
                model.NorthExperienceType = entity.NorthExperienceType;
                model.ExtraChargesId = entity.ExtraCharges == null ? 0 : entity.ExtraCharges.Id;

                model.PersonalAddition = entity.PersonalAddition;
                model.PositionAddition = entity.PositionAddition;

                model.Comments = EmploymentCandidateCommentDao.GetComments(entity.Candidate.User.Id, (int)EmploymentCommentTypeEnum.PersonnelManagers);
                model.IsAddCommentAvailable = (AuthenticationService.CurrentUser.UserRole & UserRole.PersonnelManager) > 0 ? true : false;
                model.SendTo1C = entity.Candidate.SendTo1C;
            }

            
            //состояние кандидата
            model.CandidateStateModel = new CandidateStateModel();
            model.CandidateStateModel.CandidateState = EmploymentCandidateDao.GetCandidateState(entity == null ? -1 : entity.Candidate.Id);
            return model;
        }

        public PersonnelManagersModel GetPersonnelManagersModel(PersonnelManagersModel model)
        {
            PersonnelManagers entity = null;
            int? id = EmploymentCommonDao.GetDocumentId<PersonnelManagers>(model.UserId);
            if (id.HasValue)
            {
                entity = EmploymentPersonnelManagersDao.Get(id.Value);
            }


            LoadDictionaries(model);
            //состояние кандидата
            model.CandidateStateModel = new CandidateStateModel();
            model.CandidateStateModel.CandidateState = EmploymentCandidateDao.GetCandidateState(entity == null ? -1 : entity.Candidate.Id);
            return model;
        }

        public CandidateDocumentsModel GetCandidateDocumentsModel(int? userId = null)
        {
            userId = userId ?? AuthenticationService.CurrentUser.Id;
            CandidateDocumentsModel model = new CandidateDocumentsModel { UserId = userId.Value };
            
            
            GeneralInfo entity = null;
            int? id = EmploymentCommonDao.GetDocumentId<GeneralInfo>(userId.Value);
            if (id.HasValue)
            {
                entity = EmploymentGeneralInfoDao.Get(id.Value);
            }

            
            if (entity != null)
            {
                int attachmentId = 0;
                string attachmentFilename = string.Empty;

                model.SendTo1C = entity.Candidate.SendTo1C;
                model.CandidateStatus = (int)entity.Candidate.Status;

                //заявление о приеме
                GetAttachmentData(ref attachmentId, ref attachmentFilename, entity.Candidate.Id, RequestAttachmentTypeEnum.ApplicationLetterScan);
                model.ApplicationLetterScanAttachmentId = attachmentId;
                model.ApplicationLetterScanAttachmentFilename = attachmentFilename;
                //model.IsApplicationLetterUploadAvailable = candidate.Status == EmploymentStatus.PENDING_APPLICATION_LETTER && !(model.ApplicationLetterScanAttachmentId > 0);

                //трудовой договор
                GetAttachmentData(ref attachmentId, ref attachmentFilename, entity.Candidate.Id, RequestAttachmentTypeEnum.EmploymentContractScan);
                model.EmploymentContractFileId = attachmentId;
                model.EmploymentContractFileName = attachmentFilename;

                //приказ о приеме
                GetAttachmentData(ref attachmentId, ref attachmentFilename, entity.Candidate.Id, RequestAttachmentTypeEnum.OrderOnReceptionScan);
                model.OrderOnReceptionFileId = attachmentId;
                model.OrderOnReceptionFileName = attachmentFilename;

                //Т2
                GetAttachmentData(ref attachmentId, ref attachmentFilename, entity.Candidate.Id, RequestAttachmentTypeEnum.CandidateT2Scan);
                model.T2FileId = attachmentId;
                model.T2FileName = attachmentFilename;

                //договор о мат. ответственности
                GetAttachmentData(ref attachmentId, ref attachmentFilename, entity.Candidate.Id, RequestAttachmentTypeEnum.ContractMatResponsibleScan);
                model.ContractMatResponsibleFileId = attachmentId;
                model.ContractMatResponsibleFileName = attachmentFilename;

                //ДС персональные данные
                GetAttachmentData(ref attachmentId, ref attachmentFilename, entity.Candidate.Id, RequestAttachmentTypeEnum.PersonalDataScan);
                model.PersonalDataFileId = attachmentId;
                model.PersonalDataFileName = attachmentFilename;

                //обязательство конфиденциальность
                GetAttachmentData(ref attachmentId, ref attachmentFilename, entity.Candidate.Id, RequestAttachmentTypeEnum.DataObligationScan);
                model.DataObligationFileId = attachmentId;
                model.DataObligationFileName = attachmentFilename;

                //личный листок по учету кадров
                GetAttachmentData(ref attachmentId, ref attachmentFilename, entity.Candidate.Id, RequestAttachmentTypeEnum.EmploymentFileScan);
                model.EmploymentFileId = attachmentId;
                model.EmploymentFileName = attachmentFilename;

                //реестр личного дела
                GetAttachmentData(ref attachmentId, ref attachmentFilename, entity.Candidate.Id, RequestAttachmentTypeEnum.RegisterPersonalRecordScan);
                model.RegisterPersonalRecordFileId = attachmentId;
                model.RegisterPersonalRecordFileName = attachmentFilename;

                //памятка сотруднику о сохранении коммерческой, банковской и служебной тайны
                GetAttachmentData(ref attachmentId, ref attachmentFilename, entity.Candidate.Id, RequestAttachmentTypeEnum.InstructionOfSecretScan);
                model.InstructionOfSecretFileId = attachmentId;
                model.InstructionOfSecretFileName = attachmentFilename;

                //Инструкция по обеспечению сохранности сведений, составляющих коммерческую, и служебную тайну
                GetAttachmentData(ref attachmentId, ref attachmentFilename, entity.Candidate.Id, RequestAttachmentTypeEnum.InstructionEnsuringSafetyScan);
                model.InstructionEnsuringSafetyFileId = attachmentId;
                model.InstructionEnsuringSafetyFileName = attachmentFilename;

                //Согласие физического лица на проверку персональных данных (Приложение №3)
                GetAttachmentData(ref attachmentId, ref attachmentFilename, entity.Candidate.Id, RequestAttachmentTypeEnum.AgreePersonForCheckingScan);
                model.AgreePersonForCheckingFileId = attachmentId;
                model.AgreePersonForCheckingFileName = attachmentFilename;

                //Порядок по исполнению требований при организации кассовой работы сотрудниками ВСП (Приложение 1)
                GetAttachmentData(ref attachmentId, ref attachmentFilename, entity.Candidate.Id, RequestAttachmentTypeEnum.CashWorkAddition1Scan);
                model.CashWorkAddition1FileId = attachmentId;
                model.CashWorkAddition1FileName = attachmentFilename;

                //Порядок по обслуживанию клиентов в кассе сотрудниками ВСП (Приложение 2)
                GetAttachmentData(ref attachmentId, ref attachmentFilename, entity.Candidate.Id, RequestAttachmentTypeEnum.CashWorkAddition2Scan);
                model.CashWorkAddition2FileId = attachmentId;
                model.CashWorkAddition2FileName = attachmentFilename;

                //Обязательство о неразглашении коммерческой и служебной тайны
                GetAttachmentData(ref attachmentId, ref attachmentFilename, entity.Candidate.Id, RequestAttachmentTypeEnum.ObligationTradeSecretScan);
                model.ObligationTradeSecretFileId = attachmentId;
                model.ObligationTradeSecretFileName = attachmentFilename;

                //Справка 182-Н от предыдущего работодателя
                GetAttachmentData(ref attachmentId, ref attachmentFilename, entity.Candidate.Id, RequestAttachmentTypeEnum.Certificate182HScan);
                model.Certificate182HFileId = attachmentId;
                model.Certificate182HFileName = attachmentFilename;

                //Справка 2-НДФЛ от предыдущего работодателя
                GetAttachmentData(ref attachmentId, ref attachmentFilename, entity.Candidate.Id, RequestAttachmentTypeEnum.Certificate2NDFLScan);
                model.Certificate2NDFLFileId = attachmentId;
                model.Certificate2NDFLFileName = attachmentFilename;

                //достаем метки для документов
                IList<AttachmentNeedListDto> adl = EmploymentCandidateDocNeededDao.GetCandidateDocListNeeded(entity.Candidate.Id);
                if (adl != null && adl.Count != 0)
                {
                    model.ApplicationLetterScanFileNeeded = adl.Where(x => x.DocTypeId == (int)RequestAttachmentTypeEnum.ApplicationLetterScan).Single().IsNeeded;
                    model.EmploymentContractFileNeeded = adl.Where(x => x.DocTypeId == (int)RequestAttachmentTypeEnum.EmploymentContractScan).Single().IsNeeded;
                    model.OrderOnReceptionFileNeeded = adl.Where(x => x.DocTypeId == (int)RequestAttachmentTypeEnum.OrderOnReceptionScan).Single().IsNeeded;
                    model.T2FileNeeded = adl.Where(x => x.DocTypeId == (int)RequestAttachmentTypeEnum.CandidateT2Scan).Single().IsNeeded;
                    model.ContractMatResponsibleFileNeeded = adl.Where(x => x.DocTypeId == (int)RequestAttachmentTypeEnum.ContractMatResponsibleScan).Single().IsNeeded;
                    model.PersonalDataFileNeeded = adl.Where(x => x.DocTypeId == (int)RequestAttachmentTypeEnum.PersonalDataScan).Single().IsNeeded;
                    model.DataObligationFileNeeded = adl.Where(x => x.DocTypeId == (int)RequestAttachmentTypeEnum.DataObligationScan).Single().IsNeeded;
                    model.EmploymentFileNeeded = adl.Where(x => x.DocTypeId == (int)RequestAttachmentTypeEnum.EmploymentFileScan).Single().IsNeeded;
                    model.RegisterPersonalRecordFileNeeded = adl.Where(x => x.DocTypeId == (int)RequestAttachmentTypeEnum.RegisterPersonalRecordScan).Single().IsNeeded;
                    model.InstructionOfSecretFileNeeded = adl.Where(x => x.DocTypeId == (int)RequestAttachmentTypeEnum.InstructionOfSecretScan).Single().IsNeeded;
                    model.InstructionEnsuringSafetyFileNeeded = adl.Where(x => x.DocTypeId == (int)RequestAttachmentTypeEnum.InstructionEnsuringSafetyScan).Single().IsNeeded;
                    model.AgreePersonForCheckingFileNeeded = adl.Where(x => x.DocTypeId == (int)RequestAttachmentTypeEnum.AgreePersonForCheckingScan).Single().IsNeeded;
                    model.CashWorkAddition1FileNeeded = adl.Where(x => x.DocTypeId == (int)RequestAttachmentTypeEnum.CashWorkAddition1Scan).Single().IsNeeded;
                    model.CashWorkAddition2FileNeeded = adl.Where(x => x.DocTypeId == (int)RequestAttachmentTypeEnum.CashWorkAddition2Scan).Single().IsNeeded;
                    model.ObligationTradeSecretFileNeeded = adl.Where(x => x.DocTypeId == (int)RequestAttachmentTypeEnum.ObligationTradeSecretScan).Single().IsNeeded;
                    //для уже существующего списка
                    if (model.Certificate182HFileNeeded = adl.Where(x => x.DocTypeId == (int)RequestAttachmentTypeEnum.Certificate182HScan).Count() != 0)
                        model.Certificate182HFileNeeded = adl.Where(x => x.DocTypeId == (int)RequestAttachmentTypeEnum.Certificate182HScan).Single().IsNeeded;
                    if (model.Certificate2NDFLFileNeeded = adl.Where(x => x.DocTypeId == (int)RequestAttachmentTypeEnum.Certificate2NDFLScan).Count() != 0)
                        model.Certificate2NDFLFileNeeded = adl.Where(x => x.DocTypeId == (int)RequestAttachmentTypeEnum.Certificate2NDFLScan).Single().IsNeeded;
                }
                else
                {
                    model.ApplicationLetterScanFileNeeded = true;
                    model.EmploymentContractFileNeeded = true;
                    model.OrderOnReceptionFileNeeded = true;
                }

                //если нет списка, то не показаываем кнопки к документам 2, 3, 4, 5 позиций
                model.IsDocListAvailable = EmploymentCandidateDocNeededDao.GetCandidateDocNeeded(entity.Candidate.Id).Count() == 0 ? false : true;
                //признак видимости для кнопок печати (показа шаблона документов)
                //если была выгрузка в 1С, то закрываем эту кнопку для кандидата
                model.IsPrintButtonAvailable = model.SendTo1C.HasValue ? (AuthenticationService.CurrentUser.UserRole == UserRole.Candidate ? false : true) : true;
                //после выгрузки в 1С кнопки удаления прикрепленных сканов доступны только кадровикам
                model.IsDeleteScanButtonAvailable = model.SendTo1C.HasValue ? (AuthenticationService.CurrentUser.UserRole == UserRole.PersonnelManager ? true : false) : true;
            }

            //состояние кандидата
            model.CandidateStateModel = new CandidateStateModel();
            model.CandidateStateModel.CandidateState = EmploymentCandidateDao.GetCandidateState(entity == null ? -1 : entity.Candidate.Id);
            model.IsSave = false;

            
            return model;
        }

        public ScanOriginalDocumentsModel GetScanOriginalDocumentsModel(int? userId = null)
        {
            userId = userId ?? AuthenticationService.CurrentUser.Id;
            ScanOriginalDocumentsModel model = new ScanOriginalDocumentsModel { UserId = userId.Value };
            EmploymentCandidate entity = GetCandidate(model.UserId);

            if (entity != null)
            {
                IList<EmploymentAttachmentDto> attach = EmploymentCandidateDao.GetCandidateQuestAttachmentList(entity.Id);

                model.AgreedToPersonalDataProcessing = entity.GeneralInfo.AgreedToPersonalDataProcessing;
                model.IsScanFinal = entity.SendTo1C.HasValue ? true : entity.IsScanFinal;
                model.SendTo1C = entity.SendTo1C;
                model.PrevApproverName = entity.BackgroundCheck.PrevApprover == null ? string.Empty : entity.BackgroundCheck.PrevApprover.Name;
                model.PrevApprovalStatus = entity.BackgroundCheck.PrevApprovalStatus;
                model.PrevApprovalDate = entity.BackgroundCheck.PrevApprovalDate;
                model.PrevApprovalStatuses = GetApprovalStatuses();
                model.IsPrevApproveBySecurityAvailable = entity.IsScanFinal && AuthenticationService.CurrentUser.UserRole == UserRole.Security && !entity.BackgroundCheck.PrevApprovalStatus.HasValue ? true : false;
                if (AuthenticationService.CurrentUser.UserRole == UserRole.PersonnelManager)
                    model.IsAgreeAvailable = !entity.IsScanFinal;
                else
                    model.IsAgreeAvailable = entity.Status == 0 && !entity.BackgroundCheck.PrevApprovalDate.HasValue ? true : false;
                //для консультантов даем возможность отменить отклонение
                if (AuthenticationService.CurrentUser.UserRole == UserRole.ConsultantOutsourcing)
                {
                    if (entity.Status == EmploymentStatus.REJECTED && entity.BackgroundCheck.PrevApprovalStatus.HasValue && !entity.BackgroundCheck.PrevApprovalStatus.Value)
                        model.IsCancelApproveAvailale = true;
                }

                //определяем признак кандидата из Экспресс-Волги
                Department ParentDep = DepartmentDao.Get(11923);
                IList<IdNameDto> volgadeps = ParentDep != null ? DepartmentDao.LoadAll().Where(x => x.Path.StartsWith(ParentDep.Path)).ToList().ConvertAll(x => new IdNameDto { Id = x.Id, Name = x.Name }) : null;
                model.IsVolga = volgadeps!=  null && volgadeps.Where(x => x.Id == entity.User.Department.Id).Count() != 0 ? true : false;


                if (attach != null && attach.Count != 0)
                {
                    model.AttachmentList = attach;

                    foreach (EmploymentAttachmentDto item in attach)
                    {
                        if (item.RequestType == 213)//снилс
                        {
                            model.SNILSScanAttachmentId = item.Id;
                            model.SNILSScanAttachmentFilename = item.FileName;
                        }

                        if (item.RequestType == 212)//инн
                        {
                            model.INNScanAttachmentId = item.Id;
                            model.INNScanAttachmentFilename = item.FileName;
                        }

                        if (item.RequestType == 214)//инвалидность
                        {
                            model.DisabilityCertificateScanAttachmentId = item.Id;
                            model.DisabilityCertificateScanAttachmentFilename = item.FileName;
                        }

                        if (item.RequestType == 211)//паспорт
                        {
                            model.InternalPassportScanAttachmentId = item.Id;
                            model.InternalPassportScanAttachmentFilename = item.FileName;
                        }

                        if (item.RequestType == 221)//образование
                        {
                            model.HigherEducationDiplomaScanId = item.Id;
                            model.HigherEducationDiplomaScanFileName = item.FileName;
                        }

                        if (item.RequestType == 222)//послевузовское образование
                        {
                            model.PostGraduateEducationDiplomaScanId = item.Id;
                            model.PostGraduateEducationDiplomaScanFileName = item.FileName;
                        }

                        if (item.RequestType == 223)//дополнительное образование
                        {
                            model.CertificationScanId = item.Id;
                            model.CertificationScanFileName = item.FileName;
                        }

                        if (item.RequestType == 224)//повышение квалификации
                        {
                            model.TrainingScanId = item.Id;
                            model.TrainingScanFileName = item.FileName;
                        }

                        if (item.RequestType == 231)//брак
                        {
                            model.MarriageCertificateScanAttachmentId = item.Id;
                            model.MarriageCertificateScanAttachmentFilename = item.FileName;
                        }

                        if (item.RequestType == 232)//дети
                        {
                            model.ChildBirthCertificateScanAttachmentId = item.Id;
                            model.ChildBirthCertificateScanAttachmentFilename = item.FileName;
                        }

                        if (item.RequestType == 241)//военник
                        {
                            model.MilitaryCardScanAttachmentId = item.Id;
                            model.MilitaryCardScanAttachmentFilename = item.FileName;
                        }

                        if (item.RequestType == 242)//моб. талон
                        {
                            model.MobilizationTicketScanAttachmentId = item.Id;
                            model.MobilizationTicketScanAttachmentFilename = item.FileName;
                        }

                        if (item.RequestType == 215)//трудовая книжка
                        {
                            model.WorkBookScanAttachmentId = item.Id;
                            model.WorkBookScanAttachmentFilename = item.FileName;
                        }

                        if (item.RequestType == 216)//вкладыш в трудовую книжку
                        {
                            model.WorkBookSupplementScanAttachmentId = item.Id;
                            model.WorkBookSupplementScanAttachmentFilename = item.FileName;
                        }

                        if (item.RequestType == 261)//Скан согласия на обработку персональных данных
                        {
                            model.PersonalDataProcessingScanAttachmentId = item.Id;
                            model.PersonalDataProcessingScanAttachmentFilename = item.FileName;
                        }

                        if (item.RequestType == 262)//Скан текста о достоверности сведений
                        {
                            model.InfoValidityScanAttachmentId = item.Id;
                            model.InfoValidityScanAttachmentFilename = item.FileName;
                        }
                    }
                }
            }

           
            //состояние кандидата
            model.CandidateStateModel = new CandidateStateModel();
            model.CandidateStateModel.CandidateState = EmploymentCandidateDao.GetCandidateState(entity == null ? -1 : entity.Id);


            return model;
        }

        public PersonnelInfoModel GetPersonnelInfoModel(PersonnelInfoModel model)
        {
            EmploymentCandidate entity = GetCandidate(model.CandidateID);
            User currentUser = UserDao.Get(AuthenticationService.CurrentUser.Id);

            model.CandidateName = entity.User.Name;//.GeneralInfo.LastName + " " + entity.GeneralInfo.FirstName + " " + entity.GeneralInfo.Patronymic;
            model.EmailMessage = "Заявка на прием №" + entity.Id.ToString() + "\nКандидат: " + entity.User.Name + " \n";
            model.UsersTo = GetCandidateProccedRegistration(model.CandidateID).ToList().ConvertAll(x => new IdNameDto { Id = x.Id, Name = x.Name + " - " + (x.Position == null ? "Кадровик РК" : x.Position.Name) });
            if ((currentUser.UserRole & UserRole.ConsultantOutsourcing) > 0 || (currentUser.UserRole & UserRole.Manager) > 0 || (currentUser.UserRole & UserRole.Security) > 0 || (currentUser.UserRole & UserRole.PersonnelManager) > 0)
                model.IsSendAvailable = true;
            else
                model.IsSendAvailable = false;
            return model;
        }

        public RosterModel GetRosterModel(RosterFiltersModel filters)
        {
            User current = UserDao.Load(AuthenticationService.CurrentUser.Id);
            RosterModel model = new RosterModel();

            model.IsCandidateInfoAvailable = (current.UserRole & (UserRole.Chief
                | UserRole.Director
                | UserRole.Manager
                | UserRole.Estimator
                | UserRole.OutsourcingManager
                | UserRole.PersonnelManager
                | UserRole.Security
                | UserRole.StaffManager)) > 0;
            model.IsBackgroundCheckAvailable = (current.UserRole & (UserRole.Security
                | UserRole.Chief
                | UserRole.Director
                | UserRole.Manager
                | UserRole.OutsourcingManager
                | UserRole.Estimator
                | UserRole.PersonnelManager
                | UserRole.StaffManager)) > 0;
            model.IsManagersAvailable = (current.UserRole & (UserRole.Chief
                | UserRole.Director
                | UserRole.Manager
                | UserRole.PersonnelManager
                | UserRole.Estimator
                | UserRole.OutsourcingManager)) > 0;
            model.IsPersonalManagersAvailable = (current.UserRole & (UserRole.Chief
                | UserRole.Director
                | UserRole.Manager
                | UserRole.OutsourcingManager
                | UserRole.Estimator
                | UserRole.PersonnelManager
                | UserRole.StaffManager)) > 0;

            if (filters == null)
            {                
                model.Roster = new List<CandidateDto>();
                model.BeginDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            }
            else
            {
                model.Roster = EmploymentCandidateDao.GetCandidates(current.Id,
                    current.UserRole,
                    filters != null ? filters.DepartmentId : 0,
                    filters != null ? (filters.StatusId.HasValue ? filters.StatusId.Value : -2) : 0,
                    filters != null ? filters.BeginDate : null,
                    filters != null ? filters.EndDate : null,
                    filters != null ? filters.CompleteDate : null,
                    filters != null ? filters.EmploymentDateBegin : null,
                    filters != null ? filters.EmploymentDateEnd : null,
                    filters != null ? filters.UserName : null,
                    filters != null ? filters.ContractNumber1C : null,
                    filters != null ? (filters.CandidateId.HasValue ? filters.CandidateId.Value : 0) : 0,
                    filters != null ? filters.AppointmentReportNumber : null,
                    filters != null ? (filters.AppointmentNumber.HasValue ? filters.AppointmentNumber.Value : 0) : 0,
                    filters != null ? filters.PersonnelId : 0,
                    filters.SortBy,
                    filters.SortDescending);

                model.SortBy = filters.SortBy;
                model.SortDescending = filters.SortDescending;
                model.DepartmentId = filters.DepartmentId;
                model.DepartmentName = filters.DepartmentId != 0 ? DepartmentDao.Load(filters.DepartmentId).Name : string.Empty;
                model.BeginDate = filters.BeginDate;
                model.EndDate = filters.EndDate;
                model.CompleteDate = filters.CompleteDate;
                model.EmploymentDateBegin = filters.EmploymentDateBegin;
                model.EmploymentDateEnd = filters.EmploymentDateEnd;
                model.UserName = filters.UserName;
                model.ContractNumber1C = filters.ContractNumber1C;
                model.CandidateId = filters.CandidateId;
            }

            LoadDictionaries(model);

            

            model.IsBulkChangeContractToIndefiniteAvailable = model.Roster.Any(x => x.IsChangeContractToIndefiniteAvailable);
            model.IsBulkApproveByManagerAvailable = model.Roster.Any(x => x.IsApproveByManagerAvailable);
            model.IsBulkApproveByHigherManagerAvailable = model.Roster.Any(x => x.IsApproveByHigherManagerAvailable);
            if (AuthenticationService.CurrentUser.UserRole == UserRole.Manager || AuthenticationService.CurrentUser.UserRole == UserRole.OutsourcingManager
                || AuthenticationService.CurrentUser.UserRole == UserRole.ConsultantOutsourcing
                || AuthenticationService.CurrentUser.UserRole == UserRole.PersonnelManager)
                model.IsMarkDocOriginal = true;
            else
                model.IsMarkDocOriginal = false;
                                    
            return model;
        }

        public CreateCandidateModel GetCreateCandidateModel()
        {
            var model = new CreateCandidateModel();
            model.IsOnBehalfOfManagerAvailable = (AuthenticationService.CurrentUser.UserRole & (UserRole.Manager | UserRole.Chief | UserRole.Director )) == 0 ? true : false;
            model.Personnels = EmploymentCandidateDao.GetPersonnels();
            model.PostUserLinks = new List<IdNameDto>();
            return model;
        }

        public CreateCandidateModel GetCreateCandidateModel(CreateCandidateModel model)
        {
            model.IsOnBehalfOfManagerAvailable = (AuthenticationService.CurrentUser.UserRole & (UserRole.Manager | UserRole.Chief | UserRole.Director)) == 0 ? true : false;
            model.Personnels = EmploymentCandidateDao.GetPersonnels();
            model.PostUserLinks = StaffEstablishedPostDao.GetStaffEstablishedArrangements(model.DepartmentId)
                .Where(x => x.IsVacation)
                .ToList()
                .ConvertAll(x => new IdNameDto { Id = x.Id, Name = x.PositionName + (x.IsSTD ? " - СТД" : "") + (x.ReplacedId != 0 ? " - " + x.ReplacedName : "") });
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
                    .ConvertAll(x => new SignerDto { Id = x.Id, Name = x.Name, Position = x.Position, PreamblePartyTemplate = x.PreamblePartyTemplate })
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
                
                model.EmployeeNameShortened = (!string.IsNullOrEmpty(candidate.GeneralInfo.LastName) ? candidate.GeneralInfo.LastName : string.Empty) + " " + 
                    (!string.IsNullOrEmpty(candidate.GeneralInfo.FirstName) ? candidate.GeneralInfo.FirstName[0].ToString() : string.Empty) + ". "
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
                //model.Department = candidate.Managers.Department != null ? candidate.Managers.Department.Name : string.Empty;
                model.Department = candidate.Managers.Department != null ? DepartmentDao.LoadAll().Where(x => candidate.Managers.Department.Path.StartsWith(x.Path) && x.ItemLevel == 6).Single().Name : string.Empty;
                model.City = candidate.Managers.Department != null ? (candidate.Managers.Department.Path.StartsWith("9900424.9901038.9901164.") ? "Владивосток" : "Кострома") : string.Empty;
                model.Position = candidate.Managers.Position != null ? candidate.Managers.Position.Name : string.Empty;
                model.ProbationaryPeriod = GetProbationaryPeriodString(candidate.Managers.ProbationaryPeriod);
                //model.WorkCity = candidate.Managers.WorkCity;
                model.IsSecondaryJob = candidate.Managers.IsSecondaryJob;
                //model.Schedule = candidate.Managers.Schedule != null ? candidate.Managers.Schedule.Name : string.Empty;
                model.SalaryBasis = candidate.Managers.SalaryBasis;
                model.SalaryMultiplier = candidate.Managers.SalaryMultiplier;   
            }

            if (candidate.PersonnelManagers != null)
            {
                model.ContractDate = candidate.PersonnelManagers.ContractDate;
                model.ContractEndDate = candidate.PersonnelManagers.ContractEndDate;
                model.ContractNumber = candidate.PersonnelManagers.ContractNumber;
                model.EmploymentDate = candidate.PersonnelManagers.EmploymentDate;
                model.PersonalAddition = candidate.PersonnelManagers.PersonalAddition;
                model.PositionAddition = candidate.PersonnelManagers.PositionAddition;
                model.AreaAddition = candidate.PersonnelManagers.AreaAddition;
                model.TravelRelatedAddition = candidate.PersonnelManagers.TravelRelatedAddition;
                model.CompetenceAddition = candidate.PersonnelManagers.CompetenceAddition;
                model.FrontOfficeExperienceAddition = candidate.PersonnelManagers.FrontOfficeExperienceAddition;
                model.NorthernAreaAddition = candidate.PersonnelManagers.NorthernAreaAddition;
                model.AreaMultiplier = candidate.PersonnelManagers.AreaMultiplier;

                model.ContractPoint_1_Id = candidate.PersonnelManagers.ContractPoint_1_Id;
                model.ContractPoint_2_Id = candidate.PersonnelManagers.ContractPoint_2_Id;
                model.ContractPoint_3_Id = candidate.PersonnelManagers.ContractPoint_3_Id;
                model.ContractPointsFio = candidate.PersonnelManagers.ContractPointsFio;
                model.ContractPointsAddress = candidate.PersonnelManagers.ContractPointsAddress;
                if (candidate.PersonnelManagers.Signer != null)
                {
                    model.EmployerRepresentativeName = candidate.PersonnelManagers.Signer.Name;
                    model.EmployerRepresentativePosition = candidate.PersonnelManagers.Signer.Position;
                    model.EmployerRepresentativePreamblePartyTemplate = candidate.PersonnelManagers.Signer.PreamblePartyTemplate;
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

                    //model.EmployerRepresentativeTemplate = candidate.PersonnelManagers.Signer.PreamblePartyTemplate;
                }                
            }

            //model.IsFixedTermContract = candidate.User.IsFixedTermContract;
            
            return model;
        }

        public PrintEmploymentOrderModel GetPrintEmploymentOrderModel(int userId)
        {
            EmploymentCandidate candidate = GetCandidate(userId);
            PrintEmploymentOrderModel model = new PrintEmploymentOrderModel();

            //табельный номер
            model.UserCode = candidate.User.Code == null ? string.Empty : candidate.User.Code;

            if (candidate.GeneralInfo != null)
            {
                model.EmployeeName = candidate.GeneralInfo.LastName + " " + candidate.GeneralInfo.FirstName + " " + candidate.GeneralInfo.Patronymic ?? string.Empty;

            }

            if (candidate.Managers != null)
            {
                model.PersonalAddition = candidate.PersonnelManagers.PersonalAddition;
                model.PositionAddition = candidate.PersonnelManagers.PositionAddition;
                model.Conditions = candidate.Managers.EmploymentConditions;
                //model.Department = candidate.Managers.Department != null ? candidate.Managers.Department.Name : string.Empty;
                model.Department = candidate.Managers.Department != null ? DepartmentDao.LoadAll().Where(x => candidate.Managers.Department.Path.StartsWith(x.Path) && x.ItemLevel == 6).Single().Name : string.Empty;
                model.IsSecondaryJob = candidate.Managers.IsSecondaryJob;
                model.Position = candidate.Managers.Position != null ? candidate.Managers.Position.Name : string.Empty;
                model.ProbationaryPeriod = GetProbationaryPeriodString(candidate.Managers.ProbationaryPeriod);
                model.SalaryBasis = candidate.Managers.SalaryBasis;
            }

            if (candidate.PersonnelManagers != null)
            {
                model.AreaAddition = candidate.PersonnelManagers.AreaAddition;
                model.CompetenceAddition = candidate.PersonnelManagers.CompetenceAddition;
                model.ContractDate = candidate.PersonnelManagers.ContractDate;
                model.ContractEndDate = candidate.PersonnelManagers.ContractEndDate;
                model.ContractNumber = candidate.PersonnelManagers.ContractNumber;                
                model.EmploymentDate = candidate.PersonnelManagers.EmploymentDate;                
                model.FrontOfficeExperienceAddition = candidate.PersonnelManagers.FrontOfficeExperienceAddition;
                model.NorthernAreaAddition = candidate.PersonnelManagers.NorthernAreaAddition;
                model.OrderDate = candidate.PersonnelManagers.EmploymentOrderDate;
                model.OrderNumber = candidate.PersonnelManagers.EmploymentOrderNumber;
                model.TravelRelatedAddition = candidate.PersonnelManagers.TravelRelatedAddition;
                if (candidate.PersonnelManagers.ContractPoint_1_Id.HasValue)
                {
                    if (candidate.PersonnelManagers.ContractPoint_1_Id.Value == 2)
                        model.ContractCondition = "Договор заключается временно, на период отсутствия основного работника " + candidate.PersonnelManagers.ContractPointsFio;
                    else if (candidate.PersonnelManagers.ContractPoint_1_Id.Value == 1)
                        model.ContractCondition = "Договор заключается временно для выполнения работ, непосредственно связанных со стажировкой и профессиональным обучением работников";
                    else
                    {
                        model.ContractCondition = (candidate.Managers.IsSecondaryJob ? "Работа по совместительству" : "Основная работа");
                    }
                }

                if (candidate.Managers.SalaryMultiplier.HasValue && candidate.Managers.SalaryMultiplier.Value < 1)
                    model.ContractCondition += ", сотрудник принимается на " + candidate.Managers.SalaryMultiplier.Value.ToString() + " ставки";


                if (candidate.PersonnelManagers.Signer != null)
                {
                    model.EmployerRepresentativeNameShortened = candidate.PersonnelManagers.Signer.Name;
                    model.EmployerRepresentativePosition = candidate.PersonnelManagers.Signer.Position;

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
                }
            }
            
            return model;
        }

        public PrintT2Model GetPrintT2Model(int userId)
        {
            //временно не работает, по причине нехватки первичных данных анкеты для заполнения справки
            EmploymentCandidate candidate = GetCandidate(userId);
            PrintT2Model model = new PrintT2Model();

            if (candidate.GeneralInfo != null)
            {
                model.Code = candidate.User.Code;
                model.SNILS = candidate.User.Cnilc;
                model.Inn = candidate.GeneralInfo.INN;
                model.IsSecondaryJob = candidate.Managers.IsSecondaryJob;
                model.IsMale = candidate.GeneralInfo.IsMale;
                model.LastName = candidate.GeneralInfo.LastName;
                model.FirstName = candidate.GeneralInfo.FirstName;
                model.Patronymic = candidate.GeneralInfo.Patronymic;
                model.ContractDate = candidate.PersonnelManagers.ContractDate;
                model.ContractNumber = candidate.PersonnelManagers.ContractNumber;
                model.DateOfBirth = candidate.GeneralInfo.DateOfBirth;
                model.RegionOfBirth = candidate.GeneralInfo.RegionOfBirth;
                model.CityOfBirth = candidate.GeneralInfo.CityOfBirth;
                model.CitizenshipName = candidate.GeneralInfo.Citizenship.Name;
                model.ForeignLanguages = candidate.GeneralInfo.ForeignLanguages;
                model.HigherEducations = EmploymentHigherEducationDiplomaDao.GetHighEducationTypes(candidate.Education.Id);
                model.PostGraduateEducations = candidate.Education.PostGraduateEducationDiplomas;
                model.OverallExperienceYears = candidate.PersonnelManagers.OverallExperienceYears;
                model.OverallExperienceMonths = candidate.PersonnelManagers.OverallExperienceMonths;
                model.OverallExperienceDays = candidate.PersonnelManagers.OverallExperienceDays;
                model.InsurableExperienceYears = candidate.PersonnelManagers.InsurableExperienceYears;
                model.InsurableExperienceMonths = candidate.PersonnelManagers.InsurableExperienceMonths;
                model.InsurableExperienceDays = candidate.PersonnelManagers.InsurableExperienceDays;
                if (candidate.Family.FamilyStatusId.HasValue && candidate.Family.FamilyStatusId.Value != 0)
                    model.FamilyStatusName = EmploymentFamilyDao.GetFamilyStatuses().Where(x => x.Id == candidate.Family.FamilyStatusId.Value).ToList()[0].Name;
                model.FamilyMembers = candidate.Family.FamilyMembers;
            }

            //if (candidate.Passport != null)
            //{
            //    model.EmployeeAddress = candidate.Passport.ZipCode
            //        + (!string.IsNullOrEmpty(candidate.Passport.Region) ? ", " + candidate.Passport.Region : string.Empty)
            //        + (!string.IsNullOrEmpty(candidate.Passport.District) ? ", " + candidate.Passport.District : string.Empty)
            //        + (!string.IsNullOrEmpty(candidate.Passport.City) ? ", " + candidate.Passport.City : string.Empty)
            //        + (!string.IsNullOrEmpty(candidate.Passport.Street) ? ", " + candidate.Passport.Street : string.Empty)
            //        + (!string.IsNullOrEmpty(candidate.Passport.StreetNumber) ? ", " + candidate.Passport.StreetNumber : string.Empty)
            //        + (!string.IsNullOrEmpty(candidate.Passport.Building) ? " " + candidate.Passport.Building : string.Empty)
            //        + (!string.IsNullOrEmpty(candidate.Passport.Apartment) ? ", кв. " + candidate.Passport.Apartment : string.Empty);
            //    model.PassportDateOfIssue = candidate.Passport.InternalPassportDateOfIssue;
            //    model.PassportIssuedBy = candidate.Passport.InternalPassportIssuedBy;
            //    model.PassportSeriesNumber = candidate.Passport.InternalPassportSeries + " " + candidate.Passport.InternalPassportNumber;
            //}

            //if (candidate.Managers != null)
            //{
            //    model.Department = candidate.Managers.Department != null ? candidate.Managers.Department.Name : string.Empty;
            //    model.Position = candidate.Managers.Position != null ? candidate.Managers.Position.Name : string.Empty;
            //    model.ProbationaryPeriod = candidate.Managers.ProbationaryPeriod;
            //    model.IsSecondaryJob = candidate.Managers.IsSecondaryJob;
            //    model.SalaryBasis = candidate.Managers.SalaryBasis;
            //}

            //if (candidate.PersonnelManagers != null)
            //{
            //    model.ContractDate = candidate.PersonnelManagers.ContractDate;
            //    model.ContractNumber = candidate.PersonnelManagers.ContractNumber;
            //    model.AreaAddition = candidate.PersonnelManagers.AreaAddition;
            //    model.ContractPoint_1_Id = candidate.PersonnelManagers.ContractPoint_1_Id;
            //    model.ContractPoint_2_Id = candidate.PersonnelManagers.ContractPoint_2_Id;
            //    model.ContractPoint_3_Id = candidate.PersonnelManagers.ContractPoint_3_Id;
            //    model.ContractPointsFio = candidate.PersonnelManagers.ContractPointsFio;
            //    model.ContractPointsAddress = candidate.PersonnelManagers.ContractPointsAddress;
            //    if (candidate.PersonnelManagers.Signer != null)
            //    {
            //        model.EmployerRepresentativeName = candidate.PersonnelManagers.Signer.Name;
            //        model.EmployerRepresentativePosition = candidate.PersonnelManagers.Signer.Position;
            //        if (!string.IsNullOrEmpty(candidate.PersonnelManagers.Signer.Name))
            //        {
            //            string[] employerRepresentativeNameParts = candidate.PersonnelManagers.Signer.Name.Split(' ');
            //            if (employerRepresentativeNameParts.Length >= 2)
            //            {
            //                model.EmployerRepresentativeNameShortened = employerRepresentativeNameParts[0];
            //                for (int i = 1; i < employerRepresentativeNameParts.Length; i++)
            //                {
            //                    model.EmployerRepresentativeNameShortened += string.Format(" {0}.", employerRepresentativeNameParts[i][0]);
            //                }
            //            }
            //        }

            //    }
            //}


            return model;
        }

        public PrintLiabilityContractModel GetPrintLiabilityContractModel(int userId)
        {
            EmploymentCandidate candidate = GetCandidate(userId);
            PrintLiabilityContractModel model = new PrintLiabilityContractModel();

            

            if (candidate.GeneralInfo != null)
            {
                model.EmployeeName = candidate.GeneralInfo.LastName + " " + candidate.GeneralInfo.FirstName + " " + candidate.GeneralInfo.Patronymic ?? string.Empty;
                model.EmployeeNameShortened = candidate.GeneralInfo.LastName + " " +
                    (!string.IsNullOrEmpty(candidate.GeneralInfo.FirstName) ? candidate.GeneralInfo.FirstName[0] + "." : string.Empty) +
                    (!string.IsNullOrEmpty(candidate.GeneralInfo.Patronymic) ? candidate.GeneralInfo.Patronymic[0] + "." : string.Empty);                
            }

            if (candidate.Contacts != null)
            {
                model.EmployeePhone = candidate.Contacts.Mobile;
            }

            if (candidate.Passport != null)
            {
                model.EmployeePassportSeriesNumber = candidate.Passport.InternalPassportSeries + " " + candidate.Passport.InternalPassportNumber;
                model.EmployeePassportDateOfIssue = candidate.Passport.InternalPassportDateOfIssue;
                model.EmployeePassportIssuedBy = candidate.Passport.InternalPassportIssuedBy;
                model.EmployeeAddress = candidate.Passport.ZipCode
                    + (!string.IsNullOrEmpty(candidate.Passport.Region) ? ", " + candidate.Passport.Region : string.Empty)
                    + (!string.IsNullOrEmpty(candidate.Passport.District) ? ", " + candidate.Passport.District : string.Empty)
                    + (!string.IsNullOrEmpty(candidate.Passport.City) ? ", " + candidate.Passport.City : string.Empty)
                    + (!string.IsNullOrEmpty(candidate.Passport.Street) ? ", " + candidate.Passport.Street : string.Empty)
                    + (!string.IsNullOrEmpty(candidate.Passport.StreetNumber) ? ", " + candidate.Passport.StreetNumber : string.Empty)
                    + (!string.IsNullOrEmpty(candidate.Passport.Building) ? " " + candidate.Passport.Building : string.Empty)
                    + (!string.IsNullOrEmpty(candidate.Passport.Apartment) ? ", кв. " + candidate.Passport.Apartment : string.Empty);
            }

            if (candidate.Managers != null)
            {
                model.EmployeePosition = candidate.Managers.Position != null ? candidate.Managers.Position.Name : string.Empty;
                model.EmployeeDepartment = candidate.Managers.Department != null ? candidate.Managers.Department.Name : string.Empty;
                model.EmployeeDepartment = candidate.Managers.Department != null ? DepartmentDao.LoadAll().Where(x => candidate.Managers.Department.Path.StartsWith(x.Path) && x.ItemLevel == 6).Single().Name : string.Empty;
                model.City = candidate.Managers.Department != null ? (candidate.Managers.Department.Path.StartsWith("9900424.9901038.9901164.") ? "Владивосток" : "Кострома") : string.Empty;
            }

            if (candidate.PersonnelManagers != null)
            {
                if (candidate.PersonnelManagers.Signer != null)
                {
                    model.EmployerRepresentativePosition = candidate.PersonnelManagers.Signer.Position;

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

                    model.EmployerRepresentativeTemplate = candidate.PersonnelManagers.Signer.PreamblePartyTemplate;
                }
            }

            model.ContractDate = candidate.PersonnelManagers.ContractDate.HasValue ? candidate.PersonnelManagers.ContractDate.Value : DateTime.Now;
            model.ContractNumber = candidate.PersonnelManagers.ContractNumber;                

            return model;
        }

        public PrintPersonalDataAgreementModel GetPrintPersonalDataAgreementModel(int userId)
        {
            EmploymentCandidate candidate = GetCandidate(userId);
            PrintPersonalDataAgreementModel model = new PrintPersonalDataAgreementModel();

            if (candidate.GeneralInfo != null)
            {
                model.EmployeeName = candidate.GeneralInfo.LastName + " " + candidate.GeneralInfo.FirstName + " " + candidate.GeneralInfo.Patronymic ?? string.Empty;
                model.EmployeeNameShortened = candidate.GeneralInfo.LastName + " " +
                    (!string.IsNullOrEmpty(candidate.GeneralInfo.FirstName) ? candidate.GeneralInfo.FirstName[0] + "." : string.Empty) +
                    (!string.IsNullOrEmpty(candidate.GeneralInfo.Patronymic) ? candidate.GeneralInfo.Patronymic[0] + "." : string.Empty);  
            }

            if (candidate.Passport != null)
            {
                model.EmployeePassportSeriesNumber = candidate.Passport.InternalPassportSeries + " " + candidate.Passport.InternalPassportNumber;
                model.EmployeePassportDateOfIssue = candidate.Passport.InternalPassportDateOfIssue;
                model.EmployeePassportIssuedBy = candidate.Passport.InternalPassportIssuedBy;
                model.EmployeeAddress = candidate.Passport.ZipCode
                    + (!string.IsNullOrEmpty(candidate.Passport.Region) ? ", " + candidate.Passport.Region : string.Empty)
                    + (!string.IsNullOrEmpty(candidate.Passport.District) ? ", " + candidate.Passport.District : string.Empty)
                    + (!string.IsNullOrEmpty(candidate.Passport.City) ? ", " + candidate.Passport.City : string.Empty)
                    + (!string.IsNullOrEmpty(candidate.Passport.Street) ? ", " + candidate.Passport.Street : string.Empty)
                    + (!string.IsNullOrEmpty(candidate.Passport.StreetNumber) ? ", " + candidate.Passport.StreetNumber : string.Empty)
                    + (!string.IsNullOrEmpty(candidate.Passport.Building) ? " " + candidate.Passport.Building : string.Empty)
                    + (!string.IsNullOrEmpty(candidate.Passport.Apartment) ? ", кв. " + candidate.Passport.Apartment : string.Empty);
            }

            if (candidate.PersonnelManagers != null)
            {
                model.EmploymentContractDate = candidate.PersonnelManagers.ContractDate;
                model.EmploymentContractNumber = candidate.PersonnelManagers.ContractNumber;

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

                    model.EmployerRepresentativeTemplate = candidate.PersonnelManagers.Signer.PreamblePartyTemplate;
                }
            }


            model.AgreementDate = candidate.PersonnelManagers.ContractDate.HasValue ? candidate.PersonnelManagers.ContractDate.Value : DateTime.Now;

            return model;
        }

        public PrintPersonalDataObligationModel GetPrintPersonalDataObligationModel(int userId)
        {
            EmploymentCandidate candidate = GetCandidate(userId);
            PrintPersonalDataObligationModel model = new PrintPersonalDataObligationModel();

            if (candidate.GeneralInfo != null)
            {
                model.EmployeeName = candidate.GeneralInfo.LastName + " " + candidate.GeneralInfo.FirstName + " " + candidate.GeneralInfo.Patronymic ?? string.Empty;
                model.EmployeeNameShortened = candidate.GeneralInfo.LastName + " " +
                    (!string.IsNullOrEmpty(candidate.GeneralInfo.FirstName) ? candidate.GeneralInfo.FirstName[0] + "." : string.Empty) +
                    (!string.IsNullOrEmpty(candidate.GeneralInfo.Patronymic) ? candidate.GeneralInfo.Patronymic[0] + "." : string.Empty);
            }

            return model;
        }

        public PrintEmploymentFileModel GetPrintEmploymentFileModel(int userId)
        {
            EmploymentCandidate candidate = GetCandidate(userId);
            PrintEmploymentFileModel model = new PrintEmploymentFileModel();

            #region BackgroundCheck
            if (candidate.BackgroundCheck != null)
            {
                model.AutomobileLicensePlateNumber = candidate.BackgroundCheck.AutomobileLicensePlateNumber;
                model.AutomobileMake = candidate.BackgroundCheck.AutomobileMake;
                model.AverageSalary = candidate.BackgroundCheck.AverageSalary;
                model.DriversLicenseCategories = candidate.BackgroundCheck.DriversLicenseCategories;
                model.DriversLicenseDateOfIssue = candidate.BackgroundCheck.DriversLicenseDateOfIssue;
                model.DriversLicenseNumber = candidate.BackgroundCheck.DriversLicenseNumber;
                model.DrivingExperience = candidate.BackgroundCheck.DrivingExperience;
                model.Hobbies = candidate.BackgroundCheck.Hobbies;
                model.ImportantEvents = candidate.BackgroundCheck.ImportantEvents;
                model.IsReadyForBusinessTrips = candidate.BackgroundCheck.IsReadyForBusinessTrips;
                model.Liabilities = candidate.BackgroundCheck.Liabilities;
                model.MilitaryOperationsExperience = candidate.BackgroundCheck.MilitaryOperationsExperience;
                model.PositionSought = candidate.BackgroundCheck.PositionSought;
                model.PreviousDismissalReason = candidate.BackgroundCheck.PreviousDismissalReason;
                model.PreviousSuperior = candidate.BackgroundCheck.PreviousSuperior;
                model.ChronicalDiseases = candidate.BackgroundCheck.ChronicalDiseases;
                model.Penalties = candidate.BackgroundCheck.Penalties;
                model.PsychiatricAndAddictionTreatment = candidate.BackgroundCheck.PsychiatricAndAddictionTreatment;
                model.Drinking = candidate.BackgroundCheck.Drinking;
                model.Smoking = candidate.BackgroundCheck.Smoking;
                model.OwnerOfShares = candidate.BackgroundCheck.OwnerOfShares;
                model.PositionInGoverningBodies = candidate.BackgroundCheck.PositionInGoverningBodies;
                
                if (candidate.BackgroundCheck.References != null)
                {
                    foreach (var item in candidate.BackgroundCheck.References)
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
                }
                model.Sports = candidate.BackgroundCheck.Sports;
            } 
            #endregion

            #region Contacts
            if (candidate.Contacts != null)
            {
                model.ActualAddress = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}",
                    !string.IsNullOrEmpty(candidate.Contacts.ZipCode) ? (candidate.Contacts.ZipCode + ", ") : string.Empty,
                    !string.IsNullOrEmpty(candidate.Contacts.Region) ? (candidate.Contacts.Region + ", ") : string.Empty,
                    !string.IsNullOrEmpty(candidate.Contacts.District) ? (candidate.Contacts.District + ", ") : string.Empty,
                    !string.IsNullOrEmpty(candidate.Contacts.City) ? (candidate.Contacts.City + ", ") : string.Empty,
                    !string.IsNullOrEmpty(candidate.Contacts.Street) ? (candidate.Contacts.Street + ", ") : string.Empty,
                    !string.IsNullOrEmpty(candidate.Contacts.StreetNumber) ? (candidate.Contacts.StreetNumber + ", ") : string.Empty,
                    !string.IsNullOrEmpty(candidate.Contacts.Building) ? (candidate.Contacts.Building + ", ") : string.Empty,
                    !string.IsNullOrEmpty(candidate.Contacts.Apartment) ? (candidate.Contacts.Apartment) : string.Empty
                );
                model.PhoneNumbers = string.Format("{0}, {1}", candidate.Contacts.HomePhone, candidate.Contacts.Mobile);

            } 
            #endregion

            #region Education
            if (candidate.Education != null)
            {
                if (candidate.Education.Training != null)
                {
                    foreach (var item in candidate.Education.Training)
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
                }
                if (candidate.Education.HigherEducationDiplomas != null)
                {
                    foreach (var item in candidate.Education.HigherEducationDiplomas)
                    {
                        model.Educations += (string.IsNullOrEmpty(model.Educations) ? "" : ", ") + item.EducationTypes.Name;
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
                }

            } 
            #endregion

            #region Experience
            if (candidate.Experience != null)
            {
                if (candidate.Experience.ExperienceItems != null)
                {
                    foreach (var item in candidate.Experience.ExperienceItems)
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
                }
                //foreach (var item in candidate.Experience.ExperienceItems)
                //{
                //    model.ExperienceItems.Add(new ExperienceItemDto
                //    {
                //        BeginningDate = item.BeginningDate,
                //        Company = item.Company,
                //        CompanyContacts = item.CompanyContacts,
                //        EndDate = item.EndDate,
                //        Position = item.Position
                //    });
                //}

            } 
            #endregion

            #region Family
            if (candidate.Family != null)
            {
                model.Cohabitants = candidate.Family.Cohabitants;
                
                if (candidate.Family.FamilyMembers != null)
                {
                    model.Father = candidate.Family.FamilyMembers.Where<FamilyMember>(x => x.RelationshipId == FamilyRelationship.FATHER)
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

                    model.Mother = candidate.Family.FamilyMembers.Where<FamilyMember>(x => x.RelationshipId == FamilyRelationship.MOTHER)
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

                    model.Spouse = candidate.Family.FamilyMembers.Where<FamilyMember>(x => x.RelationshipId == FamilyRelationship.SPOUSE)
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

                    model.Children = candidate.Family.FamilyMembers.Where<FamilyMember>(x => x.RelationshipId == FamilyRelationship.CHILD)
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
                }
                model.IsMarried = candidate.Family.FamilyMembers.Any(fm => fm.RelationshipId == FamilyRelationship.SPOUSE);
                if (EmploymentFamilyDao.GetFamilyStatuses() != null && EmploymentFamilyDao.GetFamilyStatuses().ToList().Count != 0)
                    model.FamilyStatusName = EmploymentFamilyDao.GetFamilyStatuses().Where(x => x.Id == candidate.Family.FamilyStatusId).Single().Name;

            } 
            #endregion

            #region GeneralInfo
            if (candidate.GeneralInfo != null)
            {
                model.DateOfBirth = candidate.GeneralInfo.DateOfBirth;
                model.FirstName = candidate.GeneralInfo.FirstName;
                if (candidate.GeneralInfo.ForeignLanguages != null)
                {
                    foreach (var item in candidate.GeneralInfo.ForeignLanguages)
                    {
                        model.ForeignLanguages.Add(new ForeignLanguageDto
                        {
                            LanguageName = item.LanguageName,
                            Level = item.Level
                        });
                    }
                }
                if (candidate.GeneralInfo.NameChanges != null)
                {
                    foreach (var item in candidate.GeneralInfo.NameChanges)
                    {
                        model.NameChanges.Add(new NameChangeDto
                        {
                            Date = item.Date,
                            Place = item.Place,
                            Reason = item.Reason
                        });
                    }
                }
                model.IsMale = candidate.GeneralInfo.IsMale;
                model.LastName = candidate.GeneralInfo.LastName;
                model.Patronymic = candidate.GeneralInfo.Patronymic;
                model.PlaceOfBirth = string.Format("{0}, {1}, {2}", candidate.GeneralInfo.RegionOfBirth, candidate.GeneralInfo.DistrictOfBirth, candidate.GeneralInfo.CityOfBirth);

            } 
            #endregion

            #region Passport
            if (candidate.Passport != null)
            {
                model.InternalPassportDateOfIssue = candidate.Passport.InternalPassportDateOfIssue;
                model.InternalPassportIssuedBy = candidate.Passport.InternalPassportIssuedBy;
                model.InternalPassportNumber = candidate.Passport.InternalPassportNumber;
                model.InternalPassportSeries = candidate.Passport.InternalPassportSeries;
                model.InternalPassportSubdivisionCode = candidate.Passport.InternalPassportSubdivisionCode;

                model.InternationalPassportDateOfIssue = candidate.Passport.InternationalPassportDateOfIssue;
                model.InternationalPassportIssuedBy = candidate.Passport.InternationalPassportIssuedBy;
                model.InternationalPassportNumber = candidate.Passport.InternationalPassportNumber;
                model.InternationalPassportSeries = candidate.Passport.InternationalPassportSeries;

                model.RegistrationZipCode = candidate.Contacts.ZipCode;

                model.RegistrationAddress = string.Format("{0}{1}{2}{3}{4}{5}{6}",
                    (!string.IsNullOrEmpty(candidate.Passport.Region) ? (candidate.Contacts.Region + ", ") : string.Empty),
                    (!string.IsNullOrEmpty(candidate.Passport.District) ? (candidate.Contacts.District + ", ") : string.Empty),
                    (!string.IsNullOrEmpty(candidate.Passport.City) ? (candidate.Contacts.City + ", ") : string.Empty),
                    (!string.IsNullOrEmpty(candidate.Passport.Street) ? (candidate.Contacts.Street + ", ") : string.Empty),
                    (!string.IsNullOrEmpty(candidate.Passport.StreetNumber) ? (candidate.Contacts.StreetNumber + ", ") : string.Empty),
                    (!string.IsNullOrEmpty(candidate.Passport.Building) ? (candidate.Contacts.Building + ", ") : string.Empty),
                    (!string.IsNullOrEmpty(candidate.Passport.Apartment) ? (candidate.Contacts.Apartment) : string.Empty)
                );

            } 
            #endregion

            return model;
        }

        public PrintRegisterPersonalRecordModel GetPrintRegisterPersonalRecordModel(int userId)
        {
            EmploymentCandidate candidate = GetCandidate(userId);
            PrintRegisterPersonalRecordModel model = new PrintRegisterPersonalRecordModel();

            //model.EmploymentDate = candidate.PersonnelManagers.EmploymentDate;
            model.EmployeeName = candidate.User.Name;
            model.Attachments = EmploymentCandidateDao.GetCandidateAttachmentList(candidate.Id);
            //model.PositionName = candidate.Managers.Position.Name;
            //model.DepartmentName = candidate.Managers.Department.Name;

            return model;
        }

        public PrintInstructionOfSecretModel GetPrintInstructionOfSecretModel(int userId)
        {
            EmploymentCandidate candidate = GetCandidate(userId);
            PrintInstructionOfSecretModel model = new PrintInstructionOfSecretModel();

            model.EmploymentDate = candidate.PersonnelManagers.EmploymentDate;
            model.EmployeeName = candidate.User.Name;
            model.PositionName = candidate.Managers.Position != null ? candidate.Managers.Position.Name : string.Empty;
            model.DepartmentName = candidate.Managers.Department != null ? DepartmentDao.LoadAll().Where(x => candidate.Managers.Department.Path.StartsWith(x.Path) && x.ItemLevel == 6).Single().Name : string.Empty;

            if (candidate.PersonnelManagers != null)
            {
                if (candidate.PersonnelManagers.Signer != null)
                {
                    //model.EmployerRepresentativeName = candidate.PersonnelManagers.Signer.Name;
                    model.EmployerRepresentativePosition = candidate.PersonnelManagers.Signer.Position;
                    //model.EmployerRepresentativePreamblePartyTemplate = candidate.PersonnelManagers.Signer.PreamblePartyTemplate;
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
                }
            }

            return model;
        }

        public PrintInstructionEnsuringSafetyModel GetPrintInstructionEnsuringSafetyModel(int userId)
        {
            EmploymentCandidate candidate = GetCandidate(userId);
            PrintInstructionEnsuringSafetyModel model = new PrintInstructionEnsuringSafetyModel();

            model.EmploymentDate = candidate.PersonnelManagers.EmploymentDate;
            model.EmployeeName = candidate.User.Name;
            //model.PositionName = candidate.Managers.Position.Name;
            //model.DepartmentName = candidate.Managers.Department.Name;

            return model;
        }

        public PrintAgreePersonForCheckingModel GetPrintAgreePersonForCheckingModel(int userId)
        {
            EmploymentCandidate candidate = GetCandidate(userId);
            PrintAgreePersonForCheckingModel model = new PrintAgreePersonForCheckingModel();

            model.EmploymentDate = candidate.PersonnelManagers.EmploymentDate;
            model.EmployeeName = candidate.User.Name;
            //model.PositionName = candidate.Managers.Position.Name;
            //model.DepartmentName = candidate.Managers.Department.Name;

            return model;
        }

        public PrintCashWorkAddition1Model GetPrintCashWorkAddition1Model(int userId)
        {
            EmploymentCandidate candidate = GetCandidate(userId);
            PrintCashWorkAddition1Model model = new PrintCashWorkAddition1Model();

            model.EmploymentDate = candidate.PersonnelManagers.EmploymentDate;
            model.EmployeeName = candidate.User.Name;
            //model.PositionName = candidate.Managers.Position.Name;
            //model.DepartmentName = candidate.Managers.Department.Name;

            return model;
        }

        public PrintCashWorkAddition2Model GetPrintCashWorkAddition2Model(int userId)
        {
            EmploymentCandidate candidate = GetCandidate(userId);
            PrintCashWorkAddition2Model model = new PrintCashWorkAddition2Model();

            model.EmploymentDate = candidate.PersonnelManagers.EmploymentDate;
            model.EmployeeName = candidate.User.Name;
            //model.PositionName = candidate.Managers.Position.Name;
            //model.DepartmentName = candidate.Managers.Department.Name;

            return model;
        }

        public PrintObligationTradeSecretModel GetPrintObligationTradeSecretModel(int userId)
        {
            EmploymentCandidate candidate = GetCandidate(userId);
            PrintObligationTradeSecretModel model = new PrintObligationTradeSecretModel();

            model.EmploymentDate = candidate.PersonnelManagers.EmploymentDate;
            model.EmployeeName = candidate.User.Name;
            model.PositionName = candidate.Managers.Position == null ? string.Empty : candidate.Managers.Position.Name;
            model.EmployeeNameShortened = (!string.IsNullOrEmpty(candidate.GeneralInfo.LastName) ? candidate.GeneralInfo.LastName : string.Empty) + " " +
                    (!string.IsNullOrEmpty(candidate.GeneralInfo.FirstName) ? candidate.GeneralInfo.FirstName[0].ToString() : string.Empty) + ". "
                    + (string.IsNullOrEmpty(candidate.GeneralInfo.Patronymic) ? string.Empty : candidate.GeneralInfo.Patronymic[0].ToString() + ".");
            //model.DepartmentName = candidate.Managers.Department.Name;

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
                    filters != null ? (filters.StatusId.HasValue ? filters.StatusId.Value : -2) : 0,
                    filters != null ? filters.BeginDate : null,
                    filters != null ? filters.EndDate : null,
                    filters != null ? filters.CompleteDate : null,
                    filters != null ? filters.EmploymentDateBegin : null,
                    filters != null ? filters.EmploymentDateEnd : null,
                    filters != null ? filters.UserName : null,
                    filters != null ? filters.ContractNumber1C : null,
                    filters != null ? (filters.CandidateId.HasValue ? filters.CandidateId.Value : 0) : 0,
                    filters != null ? filters.AppointmentReportNumber : null,
                    filters != null ? (filters.AppointmentNumber.HasValue ? filters.AppointmentNumber.Value : 0) : 0,
                    filters != null ? filters.PersonnelId : 0,
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
            model.CountryBirthItems = GetCountries();
            model.DisabilityDegrees = GetDisabilityDegrees();
        }
        public void LoadDictionaries(PassportModel model)
        {
            model.DocumentTypeItems = GetDocumentTypes();
        }
        public void LoadDictionaries(EducationModel model)
        {
            model.EducationTypes = GetHighEducatonTypes();
        }
        public void LoadDictionaries(FamilyModel model)
        {

        }
        public void LoadDictionaries(MilitaryServiceModel model)
        {
            model.RankItems = EmploymentMilitaryServiceDao.GetMilitaryRanks();
            model.RegistrationExpirationItems = GetRegistrationExpirations();
            model.PersonnelCategoryItems = GetPersonnelCategories();
            model.PersonnelTypeItems = GetPersonnelTypes();
            model.ConscriptionStatusItems = GetConscriptionStatuses();
            model.MilitaryValidityCategoryes = EmploymentMilitaryServiceDao.GetMilitaryValidityCategoryes();
            model.MilitaryRelationAccounts = EmploymentMilitaryServiceDao.GetMilitaryRelationAccounts();
            model.SpecialityCategoryes = EmploymentMilitaryServiceDao.GetMilitarySpecialityCategoryes();
            model.ReserveCategoryes = GetReserveCategoryes();
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
            model.ApprovalStatuses = GetApprovalStatuses();
            model.PostUserLinks = StaffEstablishedPostDao.GetStaffEstablishedArrangements(model.DepartmentId)
                .Where(x => x.IsVacation || (x.IsReserve && x.Id == model.UserLinkId))
                .ToList()
                .ConvertAll(x => new IdNameDto { Id = x.Id, Name = x.PositionName + (x.IsSTD ? " - СТД" : "") + (x.ReplacedId != 0 ? " - " + x.ReplacedName : "") });
            model.PostUserLinks.Insert(0, new IdNameDto { Id = 0, Name = "" });
           
        }
        public void LoadDictionaries(PersonnelManagersModel model)
        {
            model.PersonalAccountContractors = GetPersonalAccountContractors();
            model.AccessGroups = GetAccessGroups();
            model.Signers = GetSigners();
            model.InsuredPersonTypeItems = GetInsuredPersonTypes();
            model.StatusItems = GetStatuses();
            model.Schedules = GetSchedules();
            model.ContractPoint1_Items = GetContractPointVariants().Where(x => x.PointTypeId == 1).OrderBy(x => x.PointId).ToList();
            model.ContractPoint2_Items = GetContractPointVariants().Where(x => x.PointTypeId == 2).OrderBy(x => x.PointId).ToList();
            model.ContractPoint3_Items = GetContractPointVariants().Where(x => x.PointTypeId == 3).ToList();
            model.NorthExperienceTypes = GetNorthExperienceTypes();
            model.ExtraCharges = ExtraChargesDao.LoadAll();
        }
        public void LoadDictionaries(RosterModel model)
        {
            model.Statuses = GetEmploymentStatuses();
            model.Personnels = EmploymentCandidateDao.GetPersonnels();
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
                new SelectListItem {Text = "Член экипажа судна под флагом РФ", Value = "4"},
                new SelectListItem {Text = "Беженец", Value = "5"}
            };
        }

        public IList<EmploymentEducationType> GetHighEducatonTypes()
        {
            IList<EmploymentEducationType> EducationTypes = EmploymentEducationTypeDao.LoadAllSorted();
            return EducationTypes;
            //return new List<SelectListItem>
            //{
            //    new SelectListItem {Text = "Дошкольное образование", Value = "01"},
            //    new SelectListItem {Text = "Начальное (общее) образование", Value = "02"},
            //    new SelectListItem {Text = "Основное общее образование", Value = "03"},
            //    new SelectListItem {Text = "Среднее (полное) общее образование", Value = "07"},
            //    new SelectListItem {Text = "Начальное профессиональное образование", Value = "10"},
            //    new SelectListItem {Text = "Среднее профессиональное образование", Value = "11"},
            //    new SelectListItem {Text = "Неполное высшее образование", Value = "15"},
            //    new SelectListItem {Text = "Высшее образование", Value = "18"},
            //    new SelectListItem {Text = "Послевузовское образование", Value = "19"}
            //};
        }

        public IEnumerable<SelectListItem> GetDocumentTypes()
        {
            return DocumentTypeDao.LoadAllSorted().ToList().ConvertAll(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).OrderBy(x => x.Value);
        }

        public IEnumerable<SelectListItem> GetRegistrationExpirations()
        {
            return new List<SelectListItem>
            {
                //new SelectListItem {Text = "-", Value = "0"},
                new SelectListItem {Text = "Военнообязанный", Value = "1"},
                new SelectListItem {Text = "Не военнообязанный", Value = "2"},
                new SelectListItem {Text = "Призывник", Value = "3"}
            };
        }

        public IEnumerable<SelectListItem> GetReserveCategoryes()
        {
            return new List<SelectListItem>
            {
                new SelectListItem {Text = "", Value = ""},
                new SelectListItem {Text = "1", Value = "1"},
                new SelectListItem {Text = "2", Value = "2"}
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
            return PositionDao.LoadAllSorted().ToList().ConvertAll(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).OrderBy(x => x.Text);
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
                new SelectListItem {Text = "Анкета в стадии заполнения", Value = "0"},
                new SelectListItem {Text = "Ожидает предварительного согласования ДБ", Value = "10"},
                new SelectListItem {Text = "Ожидает согласование ДБ", Value = "1"},
                new SelectListItem {Text = "Обучение", Value = "2"},
                new SelectListItem {Text = "Ожидается заявление о приеме", Value = "3"},
                new SelectListItem {Text = "Ожидает согласование руководителем", Value = "4"},
                new SelectListItem {Text = "Ожидает согласование вышестоящим руководителем", Value = "5"},
                new SelectListItem {Text = "Оформление Кадры", Value = "6"},
                new SelectListItem {Text = "Контроль руководителя - пакет документов на подпись", Value = "11"},
                new SelectListItem {Text = "Документы подписаны кандидатом", Value = "12"},
                new SelectListItem {Text = "Оформлен", Value = "7"},
                new SelectListItem {Text = "Выгружено в 1С", Value = "8"},
                new SelectListItem {Text = "Отклонен", Value = "9"},
                new SelectListItem {Text = "Временно заблокирован", Value = "-1"}
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

        public IList<ContractPointDto> GetContractPointVariants()
        {
            //поля с частями текста договора вбил в html и тут пока не используются, возможно и не будут
            //общий списк бъется на 3 части для отображения в трех комбобоксах
            IList<ContractPointDto> cpv = new List<ContractPointDto> { };
            cpv.Add(new ContractPointDto { PointId = 1, PointTypeId = 1, PointTypeName = "Вариант 1", PointNamePart_1 = "Настоящий Договор заключается временно на срок с даты начала по дату окончания ТД для выполнения работ, непосредственно связанных со стажировкой и профессиональным обучением работников и вступает в силу со дня подписания сторонами." });
            cpv.Add(new ContractPointDto { PointId = 2, PointTypeId = 1, PointTypeName = "Вариант 2", PointNamePart_1 = "Настоящий Договор является срочным и заключается на период отсутствия основного работника ", PointNamePart_2 = ", за которым сохраняется место работы." });
            cpv.Add(new ContractPointDto { PointId = 3, PointTypeId = 1, PointTypeName = "Вариант 3", PointNamePart_1 = "Настоящий Договор заключается на неопределенный срок и вступает в силу со дня подписания сторонами." });
            cpv.Add(new ContractPointDto { PointId = 13, PointTypeId = 1, PointTypeName = "Вариант 4", PointNamePart_1 = "Настоящий Договор заключается временно для выполнения работ, связанных с временным расширением объема оказываемых услуг, и вступает в силу со дня подписания сторонами. Настоящий Договор заключается на срок с даты начала (проставляется дата начала ТД) по дату окончания ТД (проставляется дата окончания ТД)." });
            cpv.Add(new ContractPointDto { PointId = 4, PointTypeId = 2, PointTypeName = "Вариант 1", PointNamePart_1 = "Фактическое место работы Работника:" });
            cpv.Add(new ContractPointDto { PointId = 5, PointTypeId = 3, PointTypeName = "Вариант 1", PointNamePart_1 = "РАБОТНИКУ устанавливается следующий режим рабочего времени: пятидневная рабочая неделя с двумя выходными днями, продолжительность ежедневной работы 8 часов." });
            cpv.Add(new ContractPointDto { PointId = 6, PointTypeId = 3, PointTypeName = "Вариант 2", PointNamePart_1 = "РАБОТНИКУ устанавливается следующий режим рабочего времени: продолжительность ежедневной работы для совместителей не выше 4 часов." });
            cpv.Add(new ContractPointDto { PointId = 7, PointTypeId = 3, PointTypeName = "Вариант 3", PointNamePart_1 = "РАБОТНИКУ устанавливается следующий режим рабочего времени: ненормированный рабочий день." });
            //побили на две части закомментаренный пункт
            //cpv.Add(new ContractPointDto { PointId = 8, PointTypeId = 3, PointTypeName = "Вариант 4", PointNamePart_1 = "РАБОТНИКУ устанавливается следующий режим рабочего времени: рабочая неделя с предоставлением выходных дней по скользящему графику с суммированным учетом рабочего времени за учетный период (учетный период - квартал, 1 год)." });
            cpv.Add(new ContractPointDto { PointId = 8, PointTypeId = 3, PointTypeName = "Вариант 4", PointNamePart_1 = "РАБОТНИКУ устанавливается следующий режим рабочего времени: рабочая неделя с предоставлением выходных дней по скользящему графику с суммированным учетом рабочего времени за учетный период квартал." });
            cpv.Add(new ContractPointDto { PointId = 12, PointTypeId = 3, PointTypeName = "Вариант 5", PointNamePart_1 = " РАБОТНИКУ устанавливается следующий режим рабочего времени: рабочая неделя с предоставлением выходных дней по скользящему графику с суммированным учетом рабочего времени за учетный период 1 календарный год." });
            cpv.Add(new ContractPointDto { PointId = 9, PointTypeId = 3, PointTypeName = "Вариант 6", PointNamePart_1 = "РАБОТНИКУ устанавливается следующий режим рабочего времени: суммированный учет рабочего времени (по графику)." });
            cpv.Add(new ContractPointDto { PointId = 10, PointTypeId = 3, PointTypeName = "Вариант 7", PointNamePart_1 = "РАБОТНИКУ устанавливается следующий режим рабочего времени: (сокращенная продолжительность рабочего времени, неполное рабочее время, другой режим)." });
            cpv.Add(new ContractPointDto { PointId = 11, PointTypeId = 3, PointTypeName = "Вариант 8", PointNamePart_1 = "РАБОТНИКУ устанавливается следующий режим рабочего времени: пятидневная рабочая неделя с двумя выходными днями, продолжительность ежедневной работы 4 часа." });

            return cpv;
        }

        public IList<IdNameDto> GetNorthExperienceTypes()
        {
            IList<IdNameDto> inDto = new List<IdNameDto> { };

            inDto.Add(new IdNameDto { Id = 1, Name = "Сотруднику не полагается северная надбавка" });
            inDto.Add(new IdNameDto { Id = 2, Name = "Северный стаж сотрудника отсутствует, начать начисление стажа с даты приема" });
            inDto.Add(new IdNameDto { Id = 3, Name = "Северный стаж у сотрудника имеется, указать количество северного стажа" });

            return inDto;
        }

        #endregion

        #region Process Saving

        public int? CreateCandidate(CreateCandidateModel model, out string error)
        {
            error = string.Empty;
            IUser current = AuthenticationService.CurrentUser;
            User currentUser = UserDao.Load(current.Id);
            User onBehalfOfManager = model.OnBehalfOfManagerId.HasValue ? UserDao.Load(model.OnBehalfOfManagerId.Value) : null;
            User PersonnelUser = UserDao.Load(model.PersonnelId);
            Department department = DepartmentDao.Load(model.DepartmentId);
            StaffEstablishedPostUserLinks PostUserLink = StaffEstablishedPostUserLinksDao.Get(model.UserLinkId.Value);

            if(department.ItemLevel != 7)
            {
                error = "Укажите подразделение 7 уровня!";
                return null;
            }

            if ((currentUser.UserRole & (UserRole.Manager | UserRole.Chief | UserRole.Director)) == 0 && onBehalfOfManager == null)
            {
                error = "Необходимо выбрать руководителя, от имени которого Вы добавляете кандидата.";
                return null;
            }

            //проверяем, чтобы не спали при создании кандидата
            if (StaffEstablishedPostDao.GetStaffEstablishedArrangements(model.DepartmentId)
                .Where(x => x.IsVacation).Count() == 0)
            {
                error = "В данном подразделении нет свободных вакансий!";
                return null;
            }
            else
            {
                StaffEstablishedPostDto Vacation = StaffEstablishedPostDao.GetStaffEstablishedArrangements(model.DepartmentId)
                .Where(x => x.IsVacation && x.Id == model.UserLinkId)
                .FirstOrDefault();
                if (Vacation.IsReserve)
                {
                    error = "На данную вакансию нельзя принять сотрудника, так как данная вакансия зарезервирована " + (Vacation.ReserveType == 1 ? "штатным перемещением" : "другим кандидатом") + "!";
                    return null;
                }

                //проверяем соответствие ТД
                if (Vacation.IsSTD && !model.IsFixedTermContract)
                {
                    error = "На данную вакансию можно принять сотрудника только по срочному трудовому договору!";
                    return null;
                }
            }


            //// временная проверка на создание кандидата для дальневосточной и московской дирекции
            //if (!department.Path.StartsWith("9900424.9900920.9904119.") && !department.Path.StartsWith("9900424.9901038.9901164.") && !department.Path.StartsWith("9900424.9900426."))
            //{
            //    error = "Раздел 'Прием' пока работает в тестовом режиме для дирекций: Московской, Дальневосточной и ГО АУП!.";
            //    return null;
            //}

            // Проверка прав руководителя на подразделение
            if (!IsUserManagerForDepartment(department, onBehalfOfManager == null ? currentUser : onBehalfOfManager))
            {
                error = "Отсутствуют права на выбранное подразделение.";
                return null;
            }

            if (string.IsNullOrEmpty(model.Surname) || string.IsNullOrWhiteSpace(model.Surname))
            {
                error = "Заполните ФИО кандидата!";
                return null;
            }
            else
            {
                if (model.Surname.Trim().Length == 0)
                {
                    error = "Заполните ФИО кандидата!";
                    return null;
                }
            }

            User newUser = new User
            {
                Login = string.Empty,
                Password = AppointmentBl.CreatePassword(AppointmentBl.PasswordLength),
                IsFirstTimeLogin = true,
                IsActive = true,
                IsNew = true,
                Name = model.Surname,//string.Empty,
                RoleId = (int)UserRole.Candidate,
                Department = department,
                GivesCredit = false,
                IsMainManager = false,
                IsFixedTermContract = model.IsFixedTermContract,
                Cnilc = model.SNILS
            };

            EmploymentCandidate candidate = new EmploymentCandidate
            {
                User = newUser,
                AppointmentCreator = onBehalfOfManager != null ? onBehalfOfManager : currentUser,
                QuestionnaireDate = DateTime.Now,
                Personnels = PersonnelUser,
                IsTrainingNeeded = model.IsTrainingNeeded,
                IsBeforEmployment = model.IsTrainingNeeded ? model.IsBeforEmployment : false,
                Appointment = (model.AppointmentId!=0)?AppointmentDao.Load(model.AppointmentId):null,
                AppointmentReport =(model.AppointmentReportId!=0)?AppointmentReportDao.Load(model.AppointmentReportId):null
            };
            
            EmploymentCommonDao.SaveAndFlush(candidate);

            candidate.User.Login = "c" + candidate.User.Id.ToString();
            candidate.User.Name = model.Surname;//candidate.User.Login;

            // Create blank employment pages
            candidate.GeneralInfo = new GeneralInfo
            {
                AgreedToPersonalDataProcessing = false,
                Candidate = candidate,
                IsPatronymicAbsent = false,
                IsFinal = false,
                SNILS = model.SNILS
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
                Department = department,
                PlanRegistrationDate = model.PlanRegistrationDate,
                RegistrationDate = model.PlanRegistrationDate,
                IsFront = false,
                IsLiable = false
            };

            candidate.PersonnelManagers = new PersonnelManagers
            {
                Candidate = candidate
            };

            EmploymentCommonDao.SaveAndFlush(candidate);

            //резервируем место в штатной расстановке
            PostUserLink.DocId = candidate.Id;
            PostUserLink.ReserveType = (int)StaffReserveTypeEnum.Employment;
            PostUserLink.Editor = currentUser;
            PostUserLink.EditDate = DateTime.Now;

            StaffEstablishedPostUserLinksDao.SaveAndFlush(PostUserLink);
            //сообщение тренеру
            //EmploymentSendEmail(candidate.User.Id, 4, false);

            return candidate.User.Id;
        }
                
        public bool ProcessSaving<TVM, TE>(TVM model, out string error)
            where TVM : AbstractEmploymentModel
            where TE : new()
        {
            error = string.Empty;
            User user = null;
            User currentUser = UserDao.Get(AuthenticationService.CurrentUser.Id);

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


                ////для вкладки руководителей сохраняем резервирование
                //if (model.GetType().Name == "ManagersModel")
                //{
                //    ManagersModel m = model as ManagersModel;
                //    Managers e = entity as Managers;

                //    //убираем резервирование
                //    if (!RemoveStaffPostReserve(e.Candidate.Id, currentUser))
                //        return false;
    
                //    //резервируем
                //    if (m.UserLinkId != 0)
                //    {
                //        StaffEstablishedPostUserLinks PostUserLink = StaffEstablishedPostUserLinksDao.Get(m.UserLinkId);
                //        //резервируем место в штатной расстановке
                //        PostUserLink.DocId = e.Candidate.Id;
                //        PostUserLink.ReserveType = (int)StaffReserveTypeEnum.Employment;
                //        PostUserLink.Editor = currentUser;
                //        PostUserLink.EditDate = DateTime.Now;

                //        //для вкладки руководителя
                //        e.Position = PostUserLink.StaffEstablishedPost.Position;
                //        e.Department = PostUserLink.StaffEstablishedPost.Department;

                //        StaffEstablishedPostUserLinksDao.SaveAndFlush(PostUserLink);
                //    }
                //}

                EmploymentCommonDao.SaveOrUpdateDocument<TE>(entity);

                
                //сканы добавляются теперь в другом месте, тут можно только прицепить фотографию кандидата
                SaveAttachments<TVM>(model);


                //сообщение в ДП
                //если идет сохранение черновика руководителя или кадров, то не делать рассылку
                if (model.GetType().Name != "ManagersModel" && model.GetType().Name != "PersonnelManagersModel")
                {
                    EmploymentSendEmail(user.Id, 1, false);
                }
                    
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

            //сообщение руководителю 
            EmploymentSendEmail(userId, 3, false);

            error = string.Empty;
            return true;
        }

        public bool ProcessSaving(SignerDto itemToSave, out string error)
        {
            error = string.Empty;

            Signer entity = EmploymentSignersDao.Get(itemToSave.Id) ?? new Signer();
            entity.Name = itemToSave.Name;
            entity.Position = itemToSave.Position;
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
                case "EducationModel":
                    SaveEducationAttachments(viewModel as EducationModel, candidateId);
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
                UploadFileDto fileDto = GetFileContext(model.SNILSScanFile);
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

        protected void SaveEducationAttachments(EducationModel model, int candidateId)
        {
            if (model.HigherEducationDiplomaScanFile != null)
            {
                UploadFileDto fileDto = GetFileContext(model.HigherEducationDiplomaScanFile);
                string fileName = string.Empty;
                SaveAttachment(candidateId, model.HigherEducationDiplomaScanId, fileDto, RequestAttachmentTypeEnum.HigherEducationDiplomaScan, out fileName);
            }
            if (model.PostGraduateEducationDiplomaScanFile != null)
            {
                UploadFileDto fileDto = GetFileContext(model.PostGraduateEducationDiplomaScanFile);
                string fileName = string.Empty;
                //int? attachmentId = 
                SaveAttachment(candidateId, model.PostGraduateEducationDiplomaScanId, fileDto, RequestAttachmentTypeEnum.PostGraduateEducationDiplomaScan, out fileName);
            }
            if (model.CertificationScanFile != null)
            {
                UploadFileDto fileDto = GetFileContext(model.CertificationScanFile);
                string fileName = string.Empty;
                SaveAttachment(candidateId, model.CertificationScanId, fileDto, RequestAttachmentTypeEnum.CertificationScan, out fileName);
            }
            if (model.TrainingScanFile != null)
            {
                UploadFileDto fileDto = GetFileContext(model.TrainingScanFile);
                string fileName = string.Empty;
                SaveAttachment(candidateId, model.TrainingScanId, fileDto, RequestAttachmentTypeEnum.Training, out fileName);
            }
        }

        protected void SaveFamilyAttachments(FamilyModel model, int candidateId)
        {
            if (model.MarriageCertificateScanFile != null)
            {
                UploadFileDto fileDto = GetFileContext(model.MarriageCertificateScanFile);
                string fileName = string.Empty;
                SaveAttachment(candidateId, model.MarriageCertificateScanAttachmentId, fileDto, RequestAttachmentTypeEnum.MarriageCertificateScan, out fileName);
            }

            if (model.ChildBirthCertificateScanFile != null)
            {
                UploadFileDto fileDto = GetFileContext(model.ChildBirthCertificateScanFile);
                string fileName = string.Empty;
                SaveAttachment(candidateId, model.ChildBirthCertificateScanAttachmentId, fileDto, RequestAttachmentTypeEnum.ChildBirthCertificateScan, out fileName);
            }
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

            if (model.EmploymentFile != null)
            {
                UploadFileDto fileDto = GetFileContext(model.EmploymentFile);
                string fileName = string.Empty;
                SaveAttachment(candidateId, model.EmploymentFileId, fileDto, RequestAttachmentTypeEnum.EmploymentFileScan, out fileName);
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

        public void SaveCandidateDocumentsAttachments(CandidateDocumentsModel model, out string error)
        {
            error = string.Empty;

            EmploymentCandidate candidate = GetCandidate(model.UserId);
            int candidateId = candidate.Id;

            if (AuthenticationService.CurrentUser.UserRole != UserRole.PersonnelManager)
            {
                if (candidate.SendTo1C.HasValue)
                {
                    error = "Кандидат выгружен в 1С! Выполнение операции прервано!";
                    return;
                }
            }
            
            //сохраняем отметки документов обязательных для приема и отсылаем сообщение руководителю и замам
            IList<AttachmentNeedListDto> DocNeeded = new List<AttachmentNeedListDto> { };

            //оправка сообщения
            if (model.IsSentEmail)
            {
                EmploymentSendEmail(candidate.User.Id, 8, false);//сообщение 
                error = "Сообщение руководителю отправлено!";
                return;
            }

            //сохраняем сканы
            if (model.ApplicationLetterScanFile != null)
            {
                if ((int)candidate.Status < (int)EmploymentStatus.PENDING_APPLICATION_LETTER || (int)candidate.Status == (int)EmploymentStatus.PENDING_PREV_APPROVAL_BY_SECURITY)
                {
                    error = "Нельзя добавить скан заявления без проверки департамента безопасности!";
                    return;
                }

                UploadFileDto fileDto = GetFileContext(model.ApplicationLetterScanFile);
                string fileName = string.Empty;
                SaveAttachment(candidateId, model.ApplicationLetterScanAttachmentId, fileDto, RequestAttachmentTypeEnum.ApplicationLetterScan, out fileName);

                //после скана заявления о приеме, меняем статус
                if (candidate.Status == EmploymentStatus.PENDING_APPLICATION_LETTER)
                {
                    candidate.Status = EmploymentStatus.PENDING_APPROVAL_BY_MANAGER;
                    EmploymentCommonDao.SaveOrUpdateDocument<EmploymentCandidate>(candidate);
                    error = "Заявление о приеме загружено!";
                    return;
                }
            }
            else
            {
                if (AuthenticationService.CurrentUser.UserRole == UserRole.PersonnelManager && (int)candidate.Status < (int)EmploymentStatus.PENDING_FINALIZATION_BY_PERSONNEL_MANAGER)
                {
                    error = "Кандидат еще не согласован! Сформировать список документов невозможно!";
                    return;
                }

                if (candidate.Status != EmploymentStatus.DOCUMENTS_SENT_TO_SIGNATURE_TO_CANDIDATE && AuthenticationService.CurrentUser.UserRole != UserRole.PersonnelManager)
                {
                    //error = "Кандидат не согласован вышестоящим руководством! Все операции с документами недоступны!";
                    error = "Пакет документов для приема еще не сформирован! Все операции с документами недоступны!";
                    return;
                }
            }
            DocNeeded.Add(new AttachmentNeedListDto { DocTypeId = (int)RequestAttachmentTypeEnum.ApplicationLetterScan, IsNeeded = model.ApplicationLetterScanFileNeeded });

            

            if (model.EmploymentContractFile != null)
            {
                UploadFileDto fileDto = GetFileContext(model.EmploymentContractFile);
                string fileName = string.Empty;
                SaveAttachment(candidateId, model.EmploymentContractFileId, fileDto, RequestAttachmentTypeEnum.EmploymentContractScan, out fileName);
            }
            DocNeeded.Add(new AttachmentNeedListDto { DocTypeId = (int)RequestAttachmentTypeEnum.EmploymentContractScan, IsNeeded = model.EmploymentContractFileNeeded });

            if (model.OrderOnReceptionFile != null)
            {
                UploadFileDto fileDto = GetFileContext(model.OrderOnReceptionFile);
                string fileName = string.Empty;
                SaveAttachment(candidateId, model.OrderOnReceptionFileId, fileDto, RequestAttachmentTypeEnum.OrderOnReceptionScan, out fileName);
            }
            DocNeeded.Add(new AttachmentNeedListDto { DocTypeId = (int)RequestAttachmentTypeEnum.OrderOnReceptionScan, IsNeeded = model.OrderOnReceptionFileNeeded });

            if (model.T2File != null)
            {
                UploadFileDto fileDto = GetFileContext(model.T2File);
                string fileName = string.Empty;
                SaveAttachment(candidateId, model.T2FileId, fileDto, RequestAttachmentTypeEnum.CandidateT2Scan, out fileName);
            }
            DocNeeded.Add(new AttachmentNeedListDto { DocTypeId = (int)RequestAttachmentTypeEnum.CandidateT2Scan, IsNeeded = model.T2FileNeeded });

            if (model.ContractMatResponsibleFile != null)
            {
                UploadFileDto fileDto = GetFileContext(model.ContractMatResponsibleFile);
                string fileName = string.Empty;
                SaveAttachment(candidateId, model.ContractMatResponsibleFileId, fileDto, RequestAttachmentTypeEnum.ContractMatResponsibleScan, out fileName);
            }
            DocNeeded.Add(new AttachmentNeedListDto { DocTypeId = (int)RequestAttachmentTypeEnum.ContractMatResponsibleScan, IsNeeded = model.ContractMatResponsibleFileNeeded });

            if (model.PersonalDataFile != null)
            {
                UploadFileDto fileDto = GetFileContext(model.PersonalDataFile);
                string fileName = string.Empty;
                SaveAttachment(candidateId, model.PersonalDataFileId, fileDto, RequestAttachmentTypeEnum.PersonalDataScan, out fileName);
            }
            DocNeeded.Add(new AttachmentNeedListDto { DocTypeId = (int)RequestAttachmentTypeEnum.PersonalDataScan, IsNeeded = model.PersonalDataFileNeeded });

            if (model.DataObligationFile != null)
            {
                UploadFileDto fileDto = GetFileContext(model.DataObligationFile);
                string fileName = string.Empty;
                SaveAttachment(candidateId, model.DataObligationFileId, fileDto, RequestAttachmentTypeEnum.DataObligationScan, out fileName);
            }
            DocNeeded.Add(new AttachmentNeedListDto { DocTypeId = (int)RequestAttachmentTypeEnum.DataObligationScan, IsNeeded = model.DataObligationFileNeeded });

            if (model.EmploymentFile != null)
            {
                UploadFileDto fileDto = GetFileContext(model.EmploymentFile);
                string fileName = string.Empty;
                SaveAttachment(candidateId, model.EmploymentFileId, fileDto, RequestAttachmentTypeEnum.EmploymentFileScan, out fileName);
            }
            DocNeeded.Add(new AttachmentNeedListDto { DocTypeId = (int)RequestAttachmentTypeEnum.EmploymentFileScan, IsNeeded = model.EmploymentFileNeeded });

            if (model.RegisterPersonalRecordFile != null)
            {
                UploadFileDto fileDto = GetFileContext(model.RegisterPersonalRecordFile);
                string fileName = string.Empty;
                SaveAttachment(candidateId, model.RegisterPersonalRecordFileId, fileDto, RequestAttachmentTypeEnum.RegisterPersonalRecordScan, out fileName);
            }
            DocNeeded.Add(new AttachmentNeedListDto { DocTypeId = (int)RequestAttachmentTypeEnum.RegisterPersonalRecordScan, IsNeeded = model.RegisterPersonalRecordFileNeeded });

            if (model.InstructionOfSecretFile != null)
            {
                UploadFileDto fileDto = GetFileContext(model.InstructionOfSecretFile);
                string fileName = string.Empty;
                SaveAttachment(candidateId, model.InstructionOfSecretFileId, fileDto, RequestAttachmentTypeEnum.InstructionOfSecretScan, out fileName);
            }
            DocNeeded.Add(new AttachmentNeedListDto { DocTypeId = (int)RequestAttachmentTypeEnum.InstructionOfSecretScan, IsNeeded = model.InstructionOfSecretFileNeeded });

            if (model.InstructionEnsuringSafetyFile != null)
            {
                UploadFileDto fileDto = GetFileContext(model.InstructionEnsuringSafetyFile);
                string fileName = string.Empty;
                SaveAttachment(candidateId, model.InstructionEnsuringSafetyFileId, fileDto, RequestAttachmentTypeEnum.InstructionEnsuringSafetyScan, out fileName);
            }
            DocNeeded.Add(new AttachmentNeedListDto { DocTypeId = (int)RequestAttachmentTypeEnum.InstructionEnsuringSafetyScan, IsNeeded = model.InstructionEnsuringSafetyFileNeeded });

            if (model.AgreePersonForCheckingFile != null)
            {
                UploadFileDto fileDto = GetFileContext(model.AgreePersonForCheckingFile);
                string fileName = string.Empty;
                SaveAttachment(candidateId, model.AgreePersonForCheckingFileId, fileDto, RequestAttachmentTypeEnum.AgreePersonForCheckingScan, out fileName);
            }
            DocNeeded.Add(new AttachmentNeedListDto { DocTypeId = (int)RequestAttachmentTypeEnum.AgreePersonForCheckingScan, IsNeeded = model.AgreePersonForCheckingFileNeeded });

            if (model.CashWorkAddition1File != null)
            {
                UploadFileDto fileDto = GetFileContext(model.CashWorkAddition1File);
                string fileName = string.Empty;
                SaveAttachment(candidateId, model.CashWorkAddition1FileId, fileDto, RequestAttachmentTypeEnum.CashWorkAddition1Scan, out fileName);
            }
            DocNeeded.Add(new AttachmentNeedListDto { DocTypeId = (int)RequestAttachmentTypeEnum.CashWorkAddition1Scan, IsNeeded = model.CashWorkAddition1FileNeeded });

            if (model.CashWorkAddition2File != null)
            {
                UploadFileDto fileDto = GetFileContext(model.CashWorkAddition2File);
                string fileName = string.Empty;
                SaveAttachment(candidateId, model.CashWorkAddition2FileId, fileDto, RequestAttachmentTypeEnum.CashWorkAddition2Scan, out fileName);
            }
            DocNeeded.Add(new AttachmentNeedListDto { DocTypeId = (int)RequestAttachmentTypeEnum.CashWorkAddition2Scan, IsNeeded = model.CashWorkAddition2FileNeeded });

            if (model.ObligationTradeSecretFile != null)
            {
                UploadFileDto fileDto = GetFileContext(model.ObligationTradeSecretFile);
                string fileName = string.Empty;
                SaveAttachment(candidateId, model.ObligationTradeSecretFileId, fileDto, RequestAttachmentTypeEnum.ObligationTradeSecretScan, out fileName);
            }
            DocNeeded.Add(new AttachmentNeedListDto { DocTypeId = (int)RequestAttachmentTypeEnum.ObligationTradeSecretScan, IsNeeded = model.ObligationTradeSecretFileNeeded });

            if (model.Certificate182HFile != null)
            {
                UploadFileDto fileDto = GetFileContext(model.Certificate182HFile);
                string fileName = string.Empty;
                SaveAttachment(candidateId, model.Certificate182HFileId, fileDto, RequestAttachmentTypeEnum.Certificate182HScan, out fileName);
            }
            DocNeeded.Add(new AttachmentNeedListDto { DocTypeId = (int)RequestAttachmentTypeEnum.Certificate182HScan, IsNeeded = model.Certificate182HFileNeeded });

            if (model.Certificate2NDFLFile != null)
            {
                UploadFileDto fileDto = GetFileContext(model.Certificate2NDFLFile);
                string fileName = string.Empty;
                SaveAttachment(candidateId, model.Certificate2NDFLFileId, fileDto, RequestAttachmentTypeEnum.Certificate2NDFLScan, out fileName);
            }
            DocNeeded.Add(new AttachmentNeedListDto { DocTypeId = (int)RequestAttachmentTypeEnum.Certificate2NDFLScan, IsNeeded = model.Certificate2NDFLFileNeeded });

            if (model.IsSave)
            {
                try
                {
                    string NewEmploymentContractNumber = null;
                    if (candidate.PersonnelManagers.ContractDate.HasValue)
                        NewEmploymentContractNumber = string.IsNullOrEmpty(candidate.PersonnelManagers.ContractNumber) ? EmploymentPersonnelManagersDao.GetNewEmploymentContractNumber(candidate.PersonnelManagers.ContractDate.Value) : candidate.PersonnelManagers.ContractNumber;
                    if (string.IsNullOrEmpty(candidate.PersonnelManagers.ContractNumber))
                    {
                        candidate.PersonnelManagers.ContractNumber = NewEmploymentContractNumber;// viewModel.ContractNumber;
                        candidate.PersonnelManagers.EmploymentOrderNumber = NewEmploymentContractNumber;//viewModel.EmploymentOrderNumber;
                        EmploymentCandidateDao.SaveAndFlush(candidate);
                    }
                }
                catch
                {
                }

                User CreatorEditor = UserDao.Load(AuthenticationService.CurrentUser.Id);
                try
                {
                    IList<EmploymentCandidateDocNeededDto> dnList = EmploymentCandidateDocNeededDao.GetCandidateDocNeeded(candidate.Id);
                    EmploymentCandidateDocNeeded dn = null;

                    foreach (var item in DocNeeded)
                    {
                        if (dnList != null && dnList.Where(x => x.DocTypeId == item.DocTypeId).ToList<EmploymentCandidateDocNeededDto>().Count != 0)
                        {
                            dn = EmploymentCandidateDocNeededDao.Load(dnList.Where(x => x.DocTypeId == item.DocTypeId).Single().Id);
                        }
                        
                        if (dn == null)
                        {
                            dn = new EmploymentCandidateDocNeeded
                            {
                                Candidate = candidate,
                                Creator = CreatorEditor,
                                DateCreate = DateTime.Now,
                                DocTypeId = item.DocTypeId,
                                IsNeeded = (item.DocTypeId == (int)RequestAttachmentTypeEnum.ApplicationLetterScan || item.DocTypeId == (int)RequestAttachmentTypeEnum.EmploymentContractScan
                                || item.DocTypeId == (int)RequestAttachmentTypeEnum.OrderOnReceptionScan ? true : item.IsNeeded)//первые три документа обязательны для всех
                            };
                        }
                        else
                        {
                            dn.Candidate = candidate;
                            dn.Editor = CreatorEditor;
                            dn.DateEdit = DateTime.Now;
                            dn.DocTypeId = item.DocTypeId;
                            dn.IsNeeded = (item.DocTypeId == (int)RequestAttachmentTypeEnum.ApplicationLetterScan || item.DocTypeId == (int)RequestAttachmentTypeEnum.EmploymentContractScan
                                || item.DocTypeId == (int)RequestAttachmentTypeEnum.OrderOnReceptionScan ? true : item.IsNeeded);//первые три документа обязательны для всех
                        }

                        EmploymentCandidateDocNeededDao.SaveAndFlush(dn);
                        dn = null;
                    }

                    EmploymentCandidateDocNeededDao.CommitTran();


                    EmploymentSendEmail(candidate.User.Id, 6, CheckChangesInDocList(dnList, DocNeeded));//сообщение 
                }
                catch 
                {
                    EmploymentCandidateDocNeededDao.RollbackTran();
                    error = "Произошла ошибка при сохранении данных!";
                    return;
                }
            }

            //после всех телодвижений устанавливаем статус
            //если прицеплен весь указанный перечень, меняем статус у кандидата
            if (EmploymentCandidateDocNeededDao.CheckCandidateSignDocExists(candidate.Id))
            {
                //если не было выгрузки в 1С меняем статус (кадровики могут подгружать документы после выгрузки)
                if(!candidate.SendTo1C.HasValue)
                    candidate.Status = EmploymentStatus.DOCUMENTS_SIGNATURE_CANDIDATE_COMPLETE;
            }
            else
            {
                if ((int)candidate.Status == (int)EmploymentStatus.PENDING_FINALIZATION_BY_PERSONNEL_MANAGER && !candidate.SendTo1C.HasValue)
                    candidate.Status = EmploymentStatus.DOCUMENTS_SENT_TO_SIGNATURE_TO_CANDIDATE;
            }

            try
            {
                EmploymentCandidateDao.SaveAndFlush(candidate);
            }
            catch
            {
                EmploymentCandidateDao.RollbackTran();
                error = "Произошла ошибка при сохранении данных!";
                return;
            }

        }
        /// <summary>
        /// Удаляем скан с вкладки документы.
        /// </summary>
        /// <param name="model">Модель страницы</param>
        /// <param name="error">Сообщение об ошибке</param>
        public void DeleteCandidateDocument(CandidateDocumentsModel model, out string error)
        {
            error = string.Empty;
            EmploymentCandidate candidate = GetCandidate(model.UserId);

            //если удаляет файл кандидат, когда он уже этого делать не должен
            if (AuthenticationService.CurrentUser.UserRole == UserRole.Candidate)
            {
                if (candidate.Status == EmploymentStatus.DOCUMENTS_SIGNATURE_CANDIDATE_COMPLETE && EmploymentCandidateDocNeededDao.CheckCandidateSignDocExists(candidate.Id))
                {
                    error = "Загружен весь перечень документов, удаление файлов кандидатом запрещено! Если Вами был загружен файл/ы с некорректной информацией, то перешлите его/их кадровику!";
                    return;
                }
            }

            DeleteAttacmentModel modelDel = new DeleteAttacmentModel { Id = model.DeleteAttachmentId };
            if (DeleteAttachment(modelDel))
            {
                //при удалении скана кадровиком до выгрузки меняется статус анкеты
                if (AuthenticationService.CurrentUser.UserRole == UserRole.PersonnelManager)
                {
                    if (!EmploymentCandidateDocNeededDao.CheckCandidateSignDocExists(candidate.Id))
                    {
                        //если не было выгрузки в 1С меняем статус (кадровики могут подгружать документы после выгрузки)
                        if (!candidate.SendTo1C.HasValue && candidate.Status == EmploymentStatus.DOCUMENTS_SIGNATURE_CANDIDATE_COMPLETE)
                        {
                            candidate.Status = EmploymentStatus.DOCUMENTS_SENT_TO_SIGNATURE_TO_CANDIDATE;
                            error = "Файл удален! Теперь кандидат при необходимости может сам загрузить его повторно!";
                            return;
                        }
                    }
                }
            }

            error = "Файл удален!";
        }

        public void SaveScanOriginalDocumentsModelAttachments(ScanOriginalDocumentsModel model, out string error)
        {
            error = string.Empty;

            EmploymentCandidate candidate = GetCandidate(model.UserId);
            int candidateId = candidate.Id;

            if (candidate.SendTo1C.HasValue && AuthenticationService.CurrentUser.UserRole != UserRole.PersonnelManager)
            {
                error = "Данный документ выгружен в 1С! Выполнение операции невозможно!";
                return;
            }

            //ТУТ ДЕЛАЕМ ЗАПРЕТ НА ДОБАВЛЕНИЕ/УДАЛЕНИЕ СКАНОВ ПОСЛЕ СОГЛАСОВАНИЯ И ОТПРАВКИ НА СОГЛАСОВАНИЕ
            if (!model.IsScanODDraft)
            {
                if (candidate.IsScanFinal)
                {
                    error = "Документ был отправлен на предварительное согласование, добавление/удаление файлов невозможно!";
                    return;
                }
            }

            GeneralInfoModel gim = new GeneralInfoModel();
            gim.SNILSScanFile = model.SNILSScanFile;
            gim.INNScanFile = model.INNScanFile;
            gim.DisabilityCertificateScanFile = model.DisabilityCertificateScanFile;
            SaveGeneralInfoAttachments(gim, candidateId);

            PassportModel pm = new PassportModel();
            pm.InternalPassportScanFile = model.InternalPassportScanFile;
            SavePassportAttachments(pm, candidateId);

            EducationModel em = new EducationModel();
            em.HigherEducationDiplomaScanFile = model.HigherEducationDiplomaScanFile;
            em.PostGraduateEducationDiplomaScanFile = model.PostGraduateEducationDiplomaScanFile;
            em.CertificationScanFile = model.CertificationScanFile;
            em.TrainingScanFile = model.TrainingScanFile;
            SaveEducationAttachments(em, candidateId);

            FamilyModel fm = new FamilyModel();
            fm.MarriageCertificateScanFile = model.MarriageCertificateScanFile;
            fm.ChildBirthCertificateScanFile = model.ChildBirthCertificateScanFile;
            SaveFamilyAttachments(fm, candidateId);

            MilitaryServiceModel msm = new MilitaryServiceModel();
            msm.MilitaryCardScanFile = model.MilitaryCardScanFile;
            msm.MobilizationTicketScanFile = model.MobilizationTicketScanFile;
            SaveMilitaryServiceAttachments(msm, candidateId);

            ExperienceModel exm = new ExperienceModel();
            exm.WorkBookScanFile = model.WorkBookScanFile;
            exm.WorkBookSupplementScanFile = model.WorkBookSupplementScanFile;
            SaveExperienceAttachments(exm, candidateId);

            BackgroundCheckModel bcm = new BackgroundCheckModel();
            bcm.PersonalDataProcessingScanFile = model.PersonalDataProcessingScanFile;
            bcm.InfoValidityScanFile = model.InfoValidityScanFile;
            SaveBackgroundCheckAttachments(bcm, candidateId);


            candidate.GeneralInfo.AgreedToPersonalDataProcessing = model.AgreedToPersonalDataProcessing;
            if (model.IsAgree)
            {
                candidate.IsScanFinal = model.IsScanFinal;
                if (candidate.Status == 0)//статус меняем только стадии заполнения анкеты
                {
                    if (!candidate.BackgroundCheck.PrevApprovalDate.HasValue && !candidate.BackgroundCheck.ApprovalDate.HasValue)//если кандидат не проходил предварительную и обычную проверку
                        candidate.Status = EmploymentStatus.PENDING_PREV_APPROVAL_BY_SECURITY;
                }
                else
                {
                    if (model.IsScanFinal)
                    {
                        if (candidate.GeneralInfo.IsFinal && candidate.Passport.IsFinal && candidate.Education.IsFinal && candidate.Family.IsFinal
                                && candidate.MilitaryService.IsFinal && candidate.Experience.IsFinal && candidate.Contacts.IsFinal && candidate.BackgroundCheck.IsFinal &&
                                candidate.BackgroundCheck.PrevApprovalDate.HasValue && !candidate.BackgroundCheck.ApprovalDate.HasValue)
                        {
                            candidate.Status = EmploymentStatus.PENDING_APPROVAL_BY_SECURITY;
                        }
                    }
                }
            }
            else if (model.IsScanODDraft && AuthenticationService.CurrentUser.UserRole == UserRole.PersonnelManager)//отменить согласование может только кадровик
            {
                candidate.IsScanFinal = false;
                error = "Данные сохранены!";
                //статус не меняем, потому что эту операцию делает только кадровик и может это делать на любом этапе оформления кандидата
                //candidate.Status = 0;
            }


            try
            {
                EmploymentCandidateDao.SaveAndFlush(candidate);
                //если в данный момент времени был проставлен необходимый статус, то делаем рассылку
                if (candidate.Status == EmploymentStatus.PENDING_PREV_APPROVAL_BY_SECURITY)
                {
                    //сообщение тренеру
                    EmploymentSendEmail(candidate.User.Id, 4, false);
                    //сообщение в ДБ для предварительного согласования
                    EmploymentSendEmail(candidate.User.Id, 7, false);
                }
            }
            catch
            {
                error = "Произошла ошибка при сохранении данных!";
                EmploymentCandidateDao.RollbackTran();
            }


        }

        
        /// <summary>
        /// Проверяем наличие изменений в списке документов для подписи кандидатом
        /// </summary>
        /// <param name="DocList">Состояние документов из базы данных</param>
        /// <param name="CurrentDocState">Состояние с клиента</param>
        /// <returns></returns>
        protected bool CheckChangesInDocList(IList<EmploymentCandidateDocNeededDto> DocList, IList<AttachmentNeedListDto> CurrentDocState)
        {
            if (DocList.Count == 0) return true;//списка в базе нет, то есть пакет документов формировался впервые
            if (DocList.Count != CurrentDocState.Count) return false;//списки  не соотвествуют друг другу (ошибка)

            foreach (var item in DocList)
            {
                if (item.DocTypeId != (int)RequestAttachmentTypeEnum.ApplicationLetterScan &&
                    item.DocTypeId != (int)RequestAttachmentTypeEnum.EmploymentContractScan &&
                    item.DocTypeId != (int)RequestAttachmentTypeEnum.OrderOnReceptionScan)
                {
                    if (item.IsNeeded != CurrentDocState.Where(x => x.DocTypeId == item.DocTypeId).Single().IsNeeded) return true;
                }
            }
            return false;
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
        /// <summary>
        /// Удаление строки из раздела об изменении фамили
        /// </summary>
        /// <param name="model"></param>
        /// <param name="NameID"></param>
        public void DeleteNameChange(GeneralInfoModel model, int NameID)
        {
            int id = EmploymentCommonDao.GetDocumentId<GeneralInfo>(model.UserId);
            GeneralInfo entity = EmploymentCommonDao.GetEntityById<GeneralInfo>(id);
            foreach (var nc in entity.NameChanges)
            {
                if (nc.Id == NameID)
                {
                    entity.NameChanges.Remove(nc);
                    break;
                }
            }
            EmploymentCommonDao.SaveOrUpdateDocument<GeneralInfo>(entity);
        }
        /// <summary>
        /// Удаление строки из раздела о владении языками
        /// </summary>
        /// <param name="model"></param>
        /// <param name="NameID"></param>
        public void DeleteLanguage(GeneralInfoModel model, int LanguageID)
        {
            int id = EmploymentCommonDao.GetDocumentId<GeneralInfo>(model.UserId);
            GeneralInfo entity = EmploymentCommonDao.GetEntityById<GeneralInfo>(id);
            foreach (var fl in entity.ForeignLanguages)
            {
                if (fl.Id == LanguageID)
                {
                    entity.ForeignLanguages.Remove(fl);
                    break;
                }
            }
            EmploymentCommonDao.SaveOrUpdateDocument<GeneralInfo>(entity);
        }
        
        protected bool SetGeneralInfoEntity(GeneralInfo entity, GeneralInfoModel viewModel, out string error)
        {
            error = string.Empty;
            //даем сохранять смену фамилии и владение языками в режиме черновика, без требования поставить птицу о соглашении на обоработку своих личных данных
            if (!viewModel.AgreedToPersonalDataProcessing)
            {
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

                return true;
            }



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

            entity.Citizenship = viewModel.CitizenshipId != 0 ? CountryDao.Load(viewModel.CitizenshipId) : null;
            entity.CountryBirth = viewModel.CountryBirthId != 0 ? CountryDao.Load(viewModel.CountryBirthId) : null;

            entity.CityOfBirth = viewModel.CityOfBirth;
            entity.DateOfBirth = viewModel.DateOfBirth;

            entity.DisabilityCertificateDateOfIssue = viewModel.DisabilityCertificateDateOfIssue;
            entity.DisabilityCertificateExpirationDate = viewModel.DisabilityCertificateExpirationDate;
            entity.DisabilityCertificateNumber = viewModel.DisabilityCertificateNumber;
            entity.DisabilityCertificateSeries = viewModel.DisabilityCertificateSeries;
            entity.DisabilityDegree = viewModel.DisabilityDegreeId.HasValue ? DisabilityDegreeDao.Load(viewModel.DisabilityDegreeId.Value) : null;
            entity.IsDisabilityTermLess = viewModel.IsDisabilityTermLess;
            entity.DistrictOfBirth = viewModel.DistrictOfBirth;
            

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
            entity.IsFinal = !viewModel.IsDraft;
            entity.IsValidate = viewModel.IsValidate;
            entity.IsMale = viewModel.IsMale;
            entity.IsPatronymicAbsent = viewModel.IsPatronymicAbsent;
            entity.FirstName = viewModel.FirstName;
            entity.LastName = viewModel.LastName;
            entity.Candidate.User.Name = viewModel.LastName + " " + viewModel.FirstName + " " + viewModel.Patronymic;

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

            // все вкладки кандидата заполнены и сообщения в ДП не было, то проставляем статус для ДП
            if (entity.Candidate.IsScanFinal)
            {
                if (entity.IsFinal && entity.Candidate.Passport.IsFinal && entity.Candidate.Education.IsFinal && entity.Candidate.Family.IsFinal
                            && entity.Candidate.MilitaryService.IsFinal && entity.Candidate.Experience.IsFinal && entity.Candidate.Contacts.IsFinal && entity.Candidate.BackgroundCheck.IsFinal &&
                            entity.Candidate.BackgroundCheck.PrevApprovalDate.HasValue && !entity.Candidate.BackgroundCheck.ApprovalDate.HasValue)
                {
                    entity.Candidate.Status = EmploymentStatus.PENDING_APPROVAL_BY_SECURITY;
                }
            }
            else
            {
                error = "Данные сохранены, но не отправлены на согласование! Данную часть анкеты можно отправить на согласование, только после отправки на согласование сканов для анкеты!";
                //return false;
            }
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
            entity.IsValidate = viewModel.IsValidate;
            entity.Region = viewModel.Region;
            entity.RegistrationDate = viewModel.RegistrationDate;
            entity.Street = viewModel.Street;
            entity.StreetNumber = viewModel.StreetNumber;
            entity.ZipCode = viewModel.ZipCode;

            // все вкладки кандидата заполнены и сообщения в ДП не было, то проставляем статус для ДП
            if (entity.Candidate.IsScanFinal)
            {
                if (entity.Candidate.IsScanFinal && entity.Candidate.GeneralInfo.IsFinal && entity.IsFinal && entity.Candidate.Education.IsFinal && entity.Candidate.Family.IsFinal
                        && entity.Candidate.MilitaryService.IsFinal && entity.Candidate.Experience.IsFinal && entity.Candidate.Contacts.IsFinal && entity.Candidate.BackgroundCheck.IsFinal &&
                        entity.Candidate.BackgroundCheck.PrevApprovalDate.HasValue && !entity.Candidate.BackgroundCheck.ApprovalDate.HasValue)
                {
                    entity.Candidate.Status = EmploymentStatus.PENDING_APPROVAL_BY_SECURITY;
                }
            }
            else
            {
                error = "Данные сохранены, но не отправлены на согласование! Данную часть анкеты можно отправить на согласование, только после отправки на согласование сканов для анкеты!";
                //return false;
            }
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
                    InitiatingOrder = viewModel.Certifications[lastIndex].InitiatingOrder,
                    LocationEI = viewModel.Certifications[lastIndex].LocationEI
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
                    EducationTypes = EmploymentEducationTypeDao.Load(viewModel.HigherEducationDiplomas[lastIndex].EducationTypeId),
                    AdmissionYear = viewModel.HigherEducationDiplomas[lastIndex].AdmissionYear,
                    Department = viewModel.HigherEducationDiplomas[lastIndex].Department,
                    GraduationYear = viewModel.HigherEducationDiplomas[lastIndex].GraduationYear,
                    IssuedBy = viewModel.HigherEducationDiplomas[lastIndex].IssuedBy,
                    Number = viewModel.HigherEducationDiplomas[lastIndex].Number,
                    Profession = viewModel.HigherEducationDiplomas[lastIndex].Profession,
                    Qualification = viewModel.HigherEducationDiplomas[lastIndex].Qualification,
                    Series = viewModel.HigherEducationDiplomas[lastIndex].Series,
                    Speciality = viewModel.HigherEducationDiplomas[lastIndex].Speciality,
                    LocationEI = viewModel.HigherEducationDiplomas[lastIndex].LocationEI
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
                    Speciality = viewModel.PostGraduateEducationDiplomas[lastIndex].Speciality,
                    LocationEI = viewModel.PostGraduateEducationDiplomas[lastIndex].LocationEI
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
                    Speciality = viewModel.Training[lastIndex].Speciality,
                    LocationEI = viewModel.Training[lastIndex].LocationEI
                });
            }

            entity.IsFinal = !viewModel.IsDraft;
            entity.IsValidate = viewModel.IsValidate;

            // все вкладки кандидата заполнены и сообщения в ДП не было, то проставляем статус для ДП
            if (entity.Candidate.IsScanFinal)
            {
                if (entity.Candidate.IsScanFinal && entity.Candidate.GeneralInfo.IsFinal && entity.Candidate.Passport.IsFinal && entity.IsFinal && entity.Candidate.Family.IsFinal
                        && entity.Candidate.MilitaryService.IsFinal && entity.Candidate.Experience.IsFinal && entity.Candidate.Contacts.IsFinal && entity.Candidate.BackgroundCheck.IsFinal &&
                        entity.Candidate.BackgroundCheck.PrevApprovalDate.HasValue && !entity.Candidate.BackgroundCheck.ApprovalDate.HasValue)
                {
                    entity.Candidate.Status = EmploymentStatus.PENDING_APPROVAL_BY_SECURITY;
                }
            }
            else
            {
                error = "Данные сохранены, но не отправлены на согласование! Данную часть анкеты можно отправить на согласование, только после отправки на согласование сканов для анкеты!";
                //return false;
            }
            
            #endregion

            return true;
        }
        /// <summary>
        /// Удаляем строки на странице Образования
        /// </summary>
        /// <param name="model"></param>
        public void DeleteEducationRow(EducationModel model)
        {
            Education entity = null;
            int? id = EmploymentCommonDao.GetDocumentId<Education>(model.UserId);
            if (id.HasValue)
            {
                entity = EmploymentEducationDao.Get(id.Value);
            }

            switch (model.Operation)
            {
                case 1:
                    foreach (var item in entity.HigherEducationDiplomas)
                    {
                        if (item.Id == model.RowID)
                        {
                            entity.HigherEducationDiplomas.Remove(item);
                            break;
                        }
                    }
                    break;
                case 2:
                    foreach (var item in entity.PostGraduateEducationDiplomas)
                    {
                        if (item.Id == model.RowID)
                        {
                            entity.PostGraduateEducationDiplomas.Remove(item);
                            break;
                        }
                    }
                    break;
                case 3:
                    foreach (var item in entity.Certifications)
                    {
                        if (item.Id == model.RowID)
                        {
                            entity.Certifications.Remove(item);
                            break;
                        }
                    }
                    break;
                case 4:
                    foreach (var item in entity.Training)
                    {
                        if (item.Id == model.RowID)
                        {
                            entity.Training.Remove(item);
                            break;
                        }
                    }
                    break;
            }
            EmploymentCommonDao.SaveOrUpdateDocument<Education>(entity);
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
            entity.FamilyStatusId = viewModel.FamilyStatusId;

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
            else if (viewModel.IsMarried && viewModel.Spouse != null)
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
            entity.IsValidate = viewModel.IsValidate;

            // все вкладки кандидата заполнены и сообщения в ДП не было, то проставляем статус для ДП
            if (entity.Candidate.IsScanFinal)
            {
                if (entity.Candidate.IsScanFinal && entity.Candidate.GeneralInfo.IsFinal && entity.Candidate.Passport.IsFinal && entity.Candidate.Education.IsFinal && entity.IsFinal
                        && entity.Candidate.MilitaryService.IsFinal && entity.Candidate.Experience.IsFinal && entity.Candidate.Contacts.IsFinal && entity.Candidate.BackgroundCheck.IsFinal &&
                        entity.Candidate.BackgroundCheck.PrevApprovalDate.HasValue && !entity.Candidate.BackgroundCheck.ApprovalDate.HasValue)
                {
                    entity.Candidate.Status = EmploymentStatus.PENDING_APPROVAL_BY_SECURITY;
                }
            }
            else
            {
                error = "Данные сохранены, но не отправлены на согласование! Данную часть анкеты можно отправить на согласование, только после отправки на согласование сканов для анкеты!";
                //return false;
            }
            #endregion

            return true;
        }

        /// <summary>
        /// Удаление строки из раздела о семейном положении (пока дети удаляются)
        /// </summary>
        /// <param name="model"></param>
        public void DeleteFamilyMember(FamilyModel model)
        {
            int id = EmploymentCommonDao.GetDocumentId<Family>(model.UserId);
            Family entity = EmploymentCommonDao.GetEntityById<Family>(id);
            foreach (var item in entity.FamilyMembers)
            {
                if (item.Id == model.RowID)
                {
                    entity.FamilyMembers.Remove(item);
                    break;
                }
            }
            EmploymentCommonDao.SaveOrUpdateDocument<Family>(entity);
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
            entity.MilitaryValidityCategoryId = viewModel.MilitaryValidityCategoryId;
            entity.Commissariat = viewModel.Commissariat;
            entity.CommonMilitaryServiceRegistrationInfo = viewModel.CommonMilitaryServiceRegistrationInfo;
            entity.ConscriptionStatus = viewModel.ConscriptionStatus;
            entity.IsAssigned = viewModel.IsAssigned;
            entity.IsFinal = !viewModel.IsDraft;
            entity.IsLiableForMilitaryService = viewModel.IsLiableForMilitaryService;
            entity.IsReserved = viewModel.IsReserved;
            entity.MilitaryCardDate = viewModel.MilitaryCardDate;
            entity.MilitaryCardNumber = viewModel.MilitaryCardNumber;
            entity.MilitaryRelationAccountId = viewModel.MilitaryRelationAccountId;
            entity.MilitarySpecialityCode = viewModel.MilitarySpecialityCode;
            entity.MobilizationTicketNumber = viewModel.MobilizationTicketNumber;
            entity.PersonnelCategory = viewModel.PersonnelCategory;
            entity.PersonnelType = viewModel.PersonnelType;
            entity.RankId = viewModel.RankId;
            entity.RegistrationExpiration = viewModel.RegistrationExpiration;
            entity.ReserveCategory = viewModel.ReserveCategory;
            entity.SpecialityCategoryId = viewModel.SpecialityCategoryId;
            entity.SpecialMilitaryServiceRegistrationInfo = viewModel.SpecialMilitaryServiceRegistrationInfo;
            entity.ReserveCategoryId = viewModel.ReserveCategoryId;
            entity.IsValidate = viewModel.IsValidate;

            // все вкладки кандидата заполнены и сообщения в ДП не было, то проставляем статус для ДП
            if (entity.Candidate.IsScanFinal)
            {
                if (entity.Candidate.IsScanFinal && entity.Candidate.GeneralInfo.IsFinal && entity.Candidate.Passport.IsFinal && entity.Candidate.Education.IsFinal && entity.Candidate.Family.IsFinal
                        && entity.IsFinal && entity.Candidate.Experience.IsFinal && entity.Candidate.Contacts.IsFinal && entity.Candidate.BackgroundCheck.IsFinal &&
                        entity.Candidate.BackgroundCheck.PrevApprovalDate.HasValue && !entity.Candidate.BackgroundCheck.ApprovalDate.HasValue)
                {
                    entity.Candidate.Status = EmploymentStatus.PENDING_APPROVAL_BY_SECURITY;
                }
            }
            else
            {
                error = "Данные сохранены, но не отправлены на согласование! Данную часть анкеты можно отправить на согласование, только после отправки на согласование сканов для анкеты!";
                //return false;
            }
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
            entity.IsValidate = viewModel.IsValidate;

            // все вкладки кандидата заполнены и сообщения в ДП не было, то проставляем статус для ДП
            if (entity.Candidate.IsScanFinal)
            {
                if (entity.Candidate.IsScanFinal && entity.Candidate.GeneralInfo.IsFinal && entity.Candidate.Passport.IsFinal && entity.Candidate.Education.IsFinal && entity.Candidate.Family.IsFinal
                        && entity.Candidate.MilitaryService.IsFinal && entity.IsFinal && entity.Candidate.Contacts.IsFinal && entity.Candidate.BackgroundCheck.IsFinal &&
                        entity.Candidate.BackgroundCheck.PrevApprovalDate.HasValue && !entity.Candidate.BackgroundCheck.ApprovalDate.HasValue)
                {
                    entity.Candidate.Status = EmploymentStatus.PENDING_APPROVAL_BY_SECURITY;
                }
            }
            else
            {
                error = "Данные сохранены, но не отправлены на согласование! Данную часть анкеты можно отправить на согласование, только после отправки на согласование сканов для анкеты!";
                //return false;
            }
            #endregion

            return true;
        }
        /// <summary>
        /// Удаляем строку из таблицы на странице с трудовым опытом
        /// </summary>
        /// <param name="model"></param>
        public void DeleteExperiensRow(ExperienceModel model)
        {
            int id = EmploymentCommonDao.GetDocumentId<Experience>(model.UserId);
            Experience entity = EmploymentCommonDao.GetEntityById<Experience>(id);
            foreach (var item in entity.ExperienceItems)
            {
                if (item.Id == model.RowID)
                {
                    entity.ExperienceItems.Remove(item);
                    break;
                }
            }
            EmploymentCommonDao.SaveOrUpdateDocument<Experience>(entity);
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
            entity.Country = viewModel.Country;
            entity.Republic = viewModel.Republic;
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
            entity.IsValidate = viewModel.IsValidate;

            // все вкладки кандидата заполнены и сообщения в ДП не было, то проставляем статус для ДП
            if (entity.Candidate.IsScanFinal && entity.Candidate.GeneralInfo.IsFinal && entity.Candidate.Passport.IsFinal && entity.Candidate.Education.IsFinal && entity.Candidate.Family.IsFinal
                    && entity.Candidate.MilitaryService.IsFinal && entity.Candidate.Experience.IsFinal && entity.IsFinal && entity.Candidate.BackgroundCheck.IsFinal &&
                    entity.Candidate.BackgroundCheck.PrevApprovalDate.HasValue && !entity.Candidate.BackgroundCheck.ApprovalDate.HasValue)
            {
                entity.Candidate.Status = EmploymentStatus.PENDING_APPROVAL_BY_SECURITY;
            }
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
            entity.HasAutomobile = viewModel.HasAutomobile;
            entity.AutomobileLicensePlateNumber = viewModel.AutomobileLicensePlateNumber;
            entity.AutomobileMake = viewModel.AutomobileMake;
            entity.AverageSalary = viewModel.AverageSalary;
            entity.Candidate = GetCandidate(viewModel.UserId);
            entity.Candidate.BackgroundCheck = entity;
            entity.ChronicalDiseases = viewModel.ChronicalDiseases;
            entity.Drinking = viewModel.Drinking;
            entity.HasDriversLicense = viewModel.HasDriversLicense;
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
            entity.OwnerOfShares = viewModel.OwnerOfShares;
            entity.PositionInGoverningBodies = viewModel.PositionInGoverningBodies;
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
            entity.IsValidate = viewModel.IsValidate;


            // все вкладки кандидата заполнены и сообщения в ДП не было, то проставляем статус для ДП
            if (entity.Candidate.IsScanFinal)
            {
                if (entity.Candidate.IsScanFinal && entity.Candidate.GeneralInfo.IsFinal && entity.Candidate.Passport.IsFinal && entity.Candidate.Education.IsFinal && entity.Candidate.Family.IsFinal
                            && entity.Candidate.MilitaryService.IsFinal && entity.Candidate.Experience.IsFinal && entity.Candidate.Contacts.IsFinal && entity.IsFinal &&
                            entity.Candidate.BackgroundCheck.PrevApprovalDate.HasValue && !entity.Candidate.BackgroundCheck.ApprovalDate.HasValue)
                {
                    entity.Candidate.Status = EmploymentStatus.PENDING_APPROVAL_BY_SECURITY;
                }
            }
            else
            {
                error = "Данные сохранены, но не отправлены на согласование! Данную часть анкеты можно отправить на согласование, только после отправки на согласование сканов для анкеты!";
                //return false;
            }
            
            #endregion

            return true;
        }

        /// <summary>
        /// Удаляем строку из таблицы на странице службы безопасности
        /// </summary>
        /// <param name="model"></param>
        public void DeleteBackgroundRow(BackgroundCheckModel model)
        {
            int id = EmploymentCommonDao.GetDocumentId<BackgroundCheck>(model.UserId);
            BackgroundCheck entity = EmploymentCommonDao.GetEntityById<BackgroundCheck>(id);
            foreach (var item in entity.References)
            {
                if (item.Id == model.RowID)
                {
                    entity.References.Remove(item);
                    break;
                }
            }
            EmploymentCommonDao.SaveOrUpdateDocument<BackgroundCheck>(entity);
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

            StaffEstablishedPostDto Vacation = StaffEstablishedPostDao.GetStaffEstablishedArrangements(viewModel.DepartmentId)
                .Where(x => x.Id == viewModel.UserLinkId)
                .FirstOrDefault();
            if (Vacation == null)
            {
                error = "Выберите доступную вакансию!";
                return false;
            }

            //проверяем соответствие ТД
            if (Vacation.IsSTD && entity.Candidate.User.IsFixedTermContract.HasValue && !entity.Candidate.User.IsFixedTermContract.Value)
            {
                error = "На данную вакансию можно принять сотрудника только по срочному трудовому договору!";
                return false;
            }

            // Проверка прав руководителя на подразделение
            if (currentUser.UserRole == UserRole.Manager)
            {
                if (!IsUserManagerForDepartment(department, currentUser))
                {
                    error = "Отсутствуют права на выбранное подразделение.";
                    return false;
                }
            }

            if (entity.Department.ItemLevel != 7)
            {
                error = "Укажите подразделение 7 уровня!";
                return false;
            }

            entity.Bonus = viewModel.Bonus;
            entity.Candidate = GetCandidate(viewModel.UserId);
            entity.Candidate.Managers = entity;
            entity.Department = DepartmentDao.Load(viewModel.DepartmentId);
            //entity.Candidate.User.Department = DepartmentDao.Load(viewModel.DepartmentId);
            entity.EmploymentConditions = viewModel.EmploymentConditions;            
            entity.IsFront = viewModel.IsFront;
            entity.IsLiable = viewModel.IsLiable;
            entity.IsSecondaryJob = viewModel.IsSecondaryJob;
            entity.IsExternalPTWorker = !viewModel.IsSecondaryJob ? false : viewModel.IsExternalPTWorker;
            //entity.Position = PositionDao.Load(viewModel.PositionId);
            entity.ProbationaryPeriod = viewModel.ProbationaryPeriod;
            entity.RequestNumber = viewModel.RequestNumber;
            entity.SalaryBasis = viewModel.SalaryBasis;
            entity.SalaryMultiplier = viewModel.SalaryMultiplier;
            entity.WorkCity = viewModel.WorkCity;
            entity.MentorName = viewModel.MentorName;

            //надбавки
            entity.Candidate.PersonnelManagers.AreaMultiplier = viewModel.AreaMultiplier;
            entity.Candidate.PersonnelManagers.PersonalAddition = viewModel.PersonalAddition;
            entity.Candidate.PersonnelManagers.PositionAddition = viewModel.PositionAddition;
            entity.Candidate.PersonnelManagers.AreaAddition = viewModel.AreaAddition;
            entity.Candidate.PersonnelManagers.TravelRelatedAddition = viewModel.TravelRelatedAddition;
            entity.Candidate.PersonnelManagers.CompetenceAddition = viewModel.CompetenceAddition;
            entity.Candidate.PersonnelManagers.FrontOfficeExperienceAddition = viewModel.FrontOfficeExperienceAddition;
            entity.PyrusNumber = viewModel.PyrusNumber;

            if (!entity.Candidate.SendTo1C.HasValue && !viewModel.SendTo1C.HasValue)
            {
                entity.RegistrationDate = viewModel.RegistrationDate;
                entity.Candidate.PersonnelManagers.EmploymentDate = viewModel.RegistrationDate;
            }


            //убираем резервирование
            if (!RemoveStaffPostReserve(entity.Candidate.Id, currentUser))
                return false;

            //резервируем
            if (viewModel.UserLinkId != 0)
            {
                StaffEstablishedPostUserLinks PostUserLink = StaffEstablishedPostUserLinksDao.Get(viewModel.UserLinkId);    
                //резервируем место в штатной расстановке
                PostUserLink.DocId = entity.Candidate.Id;
                PostUserLink.ReserveType = (int)StaffReserveTypeEnum.Employment;
                PostUserLink.Editor = currentUser;
                PostUserLink.EditDate = DateTime.Now;

                //для вкладки руководителя
                entity.Position = PostUserLink.StaffEstablishedPost.Position;
                entity.Department = PostUserLink.StaffEstablishedPost.Department;

                StaffEstablishedPostUserLinksDao.SaveAndFlush(PostUserLink);
            }

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
            //entity.Candidate.User.Level = viewModel.Level;
            entity.CompetenceAddition = viewModel.CompetenceAddition;
            if (!entity.Candidate.SendTo1C.HasValue && !viewModel.SendTo1C.HasValue)
            {
                entity.ContractDate = viewModel.ContractDate;
                entity.ContractNumber = viewModel.ContractNumber;
                entity.EmploymentDate = viewModel.EmploymentDate;
                entity.Candidate.Managers.RegistrationDate = viewModel.EmploymentDate;
                entity.EmploymentOrderDate = viewModel.EmploymentOrderDate;
                entity.EmploymentOrderNumber = viewModel.EmploymentOrderNumber;
                entity.ContractEndDate = viewModel.ContractEndDate;
            }
            entity.FrontOfficeExperienceAddition = viewModel.FrontOfficeExperienceAddition;
            entity.InsurableExperienceDays = viewModel.InsurableExperienceDays;
            entity.InsurableExperienceMonths = viewModel.InsurableExperienceMonths;
            entity.InsurableExperienceYears = viewModel.InsurableExperienceYears;
            entity.IsHourlySalaryBasis = viewModel.IsHourlySalaryBasis;
            entity.BasicSalary = viewModel.BasicSalary;
            entity.OverallExperienceDays = viewModel.OverallExperienceDays;
            entity.OverallExperienceMonths = viewModel.OverallExperienceMonths;
            entity.OverallExperienceYears = viewModel.OverallExperienceYears;
            entity.PersonalAccount = viewModel.PersonalAccount;
            entity.PersonalAccountContractor = PersonalAccountContractorDao.Load(viewModel.PersonalAccountContractorId);
            entity.Signer = EmploymentSignersDao.Load(viewModel.SignerId);
            entity.TravelRelatedAddition = viewModel.TravelRelatedAddition;
            entity.InsuredPersonType = viewModel.InsuredPersonTypeId.HasValue ? InsuredPersonTypeDao.Load(viewModel.InsuredPersonTypeId.Value) : null;
            entity.Status = viewModel.StatusId;
            entity.PositionAddition = viewModel.PositionAddition;
            entity.PersonalAddition = viewModel.PersonalAddition;
            entity.ContractPoint_1_Id = viewModel.ContractPoint_1_Id;
            entity.ContractPoint_2_Id = viewModel.ContractPoint_2_Id;
            entity.ContractPoint_3_Id = viewModel.ContractPoint_3_Id;
            entity.ContractPointsFio = viewModel.ContractPoint_1_Id == 2 ? viewModel.ContractPointsFio : null;
            entity.ContractPointsAddress = viewModel.ContractPoint_2_Id == 4 ? viewModel.ContractPointsAddress : null;
            entity.Schedule = viewModel.ScheduleId.HasValue ? ScheduleDao.Load(viewModel.ScheduleId.Value) : null;

            entity.NorthExperienceYears = viewModel.NorthExperienceType != 2 ? viewModel.NorthExperienceYears : 0;
            entity.NorthExperienceMonths = viewModel.NorthExperienceType != 2 ? viewModel.NorthExperienceMonths : 0;
            entity.NorthExperienceDays = viewModel.NorthExperienceType != 2 ? viewModel.NorthExperienceDays : 0;
            entity.NorthernAreaAddition = viewModel.NorthExperienceType != 2 ? viewModel.NorthernAreaAddition : 0;
            entity.NorthExperienceType = viewModel.NorthExperienceType;
            entity.ExtraCharges = viewModel.NorthExperienceType == 2 || viewModel.NorthExperienceType == 3 ? ExtraChargesDao.Load(viewModel.ExtraChargesId.Value) : null;

            if (viewModel.ScheduleId.HasValue)
            {
                entity.Schedule = ScheduleDao.Load(viewModel.ScheduleId.Value);
            }

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
        /// <summary>
        /// Добавляем комментарий
        /// </summary>
        /// <param name="CandidateId">Id кандидата</param>
        /// <param name="CommentTypeId">Вид журнала, к которому относится комментаий</param>
        /// <param name="Comment">Текст комментария</param>
        /// <param name="error">сообщение об ошибке</param>
        /// <returns></returns>
        public bool SaveComments(int CandidateId, int CommentTypeId, string Comment, out string error)
        {
            EmploymentCandidateComment entity = new EmploymentCandidateComment { 
                UserId = AuthenticationService.CurrentUser.Id,
                CandidateId = CandidateId,
                CommentTypeId = CommentTypeId,
                Comment = Comment,
                CreatedDate = DateTime.Now
            };
            try
            {
                EmploymentCandidateCommentDao.SaveAndFlush(entity);
                //EmploymentCandidateCommentDao.CommitTran();
                error = "Комментарий добавлен!";
                return true;
            }
            catch (Exception ex)
            {
                EmploymentCandidateCommentDao.RollbackTran();
                error = string.Format("Исключение:{0}", ex.GetBaseException().Message);
                return false;
            }
        }
        /// <summary>
        /// сохраняем признак технического увольнения из реестра
        /// </summary>
        /// <param name="CandidateId">Id кандидата.</param>
        /// <param name="IsDT">ghbpyfr</param>
        /// <returns></returns>
        public bool SaveCandidateTechDissmiss(IList<CandidateTechDissmissDto> roster)
        {
            try
            {
                foreach (var item in roster)
                {
                    EmploymentCandidate entity = EmploymentCommonDao.Load(item.Id);
                    if (entity.IsTechDissmiss != item.IsTechDissmiss)
                    {
                        entity.IsTechDissmiss = item.IsTechDissmiss;
                        EmploymentCommonDao.SaveOrUpdateDocument<EmploymentCandidate>(entity);
                    }
                }
                
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        /// <summary>
        /// Сохраняем признаки получения оригиналов ТК/ТД
        /// </summary>
        /// <param name="roster">Обрабатываемый список</param>
        /// <param name="IsTK">Переключатель типа документов.</param>
        /// <returns></returns>
        public bool SaveCandidateDocRecieved(IList<CandidateDocRecievedDto> roster, bool IsTK)
        {
            try
            {
                User curUser = UserDao.Get(AuthenticationService.CurrentUser.Id);
                foreach (var item in roster)
                {
                    EmploymentCandidate entity = EmploymentCommonDao.Load(item.Id);

                    if (IsTK)
                    {
                        if (entity.IsTKReceived != item.IsTKReceived)
                        {
                            entity.IsTKReceived = item.IsTKReceived;

                            if (entity.IsTKReceived)
                                entity.TKReceivedDate = DateTime.Now;
                            else
                                entity.TKReceivedDate = null;

                            item.ReceivedDate = entity.TKReceivedDate.HasValue ? entity.TKReceivedDate.Value.ToShortDateString() : "";
                            entity.TKMarkUser = curUser;
                        }
                    }
                    else
                    {
                        if (entity.IsTDReceived != item.IsTDReceived)
                        {
                            entity.IsTDReceived = item.IsTDReceived;
                            if (entity.IsTDReceived)
                                entity.TDReceivedDate = DateTime.Now;
                            else
                                entity.TDReceivedDate = null;

                            item.ReceivedDate = entity.TDReceivedDate.HasValue ? entity.TDReceivedDate.Value.ToShortDateString() : "";
                            entity.TDMarkUser = curUser;
                        }
                    }

                    EmploymentCommonDao.SaveOrUpdateDocument<EmploymentCandidate>(entity);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }

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
        /// <summary>
        /// Предварительное согласование кандидата
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="PrevApprovalStatus"></param>
        /// <param name="IsCancelApproveAvailale"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public bool PrevApproveBackgroundCheck(int userId, bool? PrevApprovalStatus, bool IsCancelApproveAvailale, out string error)
        {
            error = string.Empty;

            IUser current = AuthenticationService.CurrentUser;
            User CurUser = UserDao.Load(current.Id);
            if ((current.UserRole & UserRole.Security) == UserRole.Security || (IsCancelApproveAvailale && (current.UserRole & UserRole.ConsultantOutsourcing) == UserRole.ConsultantOutsourcing))
            {
                BackgroundCheck entity = null;
                int? id = EmploymentCommonDao.GetDocumentId<BackgroundCheck>(userId);
                if (id.HasValue)
                {
                    entity = EmploymentBackgroundCheckDao.Get(id.Value);
                }
                if (entity != null)
                {
                    if (CheckCandidateIsBlocked(entity.Candidate.User.Id))
                    {
                        error = "Данный кандидат временно заблокирован! Согласование невозможно!.";
                        return false;
                    }


                    if (IsCancelApproveAvailale)//отмена отклонения
                    {
                        entity.PrevApprovalStatus = null;
                        entity.PrevApprover = null;
                        //entity.PyrusRef = PyrusRef;
                        //entity.IsApprovalSkipped = false;
                        entity.PrevApprovalDate = null;
                        entity.Candidate.Status = 0;// EmploymentStatus.PENDING_APPROVAL_BY_SECURITY;
                        entity.Candidate.PersonnelManagers.RejectDate = null;
                        entity.Candidate.PersonnelManagers.RejectUser = null;
                        entity.Candidate.User.IsActive = true;
                        entity.CancelRejectUser = CurUser;
                        entity.CancelRejectDate = DateTime.Now;

                        if (!EmploymentCommonDao.SaveOrUpdateDocument<BackgroundCheck>(entity))
                        {
                            error = "Ошибка согласования.";
                            return false;
                        }
                        else
                        {
                            error = "Отклонение кандидата отменено!";
                            return true;
                        }
                    }


                    if (entity.Candidate.Status == EmploymentStatus.PENDING_PREV_APPROVAL_BY_SECURITY && entity.Candidate.IsScanFinal && entity.Candidate.GeneralInfo.AgreedToPersonalDataProcessing)
                    {

                        entity.PrevApprovalStatus = PrevApprovalStatus;
                        entity.PrevApprover = UserDao.Get(current.Id);
                        entity.PrevApprovalDate = DateTime.Now;
                        if (PrevApprovalStatus == true)
                        {
                            //entity.Candidate.Status = EmploymentStatus.PENDING_APPLICATION_LETTER;
                            //если все вкладки кандидата заполнены до предварительного согласования и сообщения в ДП не было, то проставляем статус для ДП и шлем письмо
                            if (entity.Candidate.IsScanFinal)
                            {
                                if (entity.IsFinal && entity.Candidate.GeneralInfo.IsFinal && entity.Candidate.Passport.IsFinal && entity.Candidate.Education.IsFinal && entity.Candidate.Family.IsFinal
                                        && entity.Candidate.MilitaryService.IsFinal && entity.Candidate.Experience.IsFinal && entity.Candidate.Contacts.IsFinal && 
                                        entity.Candidate.BackgroundCheck.PrevApprovalDate.HasValue && !entity.Candidate.BackgroundCheck.ApprovalDate.HasValue)
                                {
                                    entity.Candidate.Status = EmploymentStatus.PENDING_APPROVAL_BY_SECURITY;
                                }
                                else
                                    entity.Candidate.Status = 0;
                            }
                            else
                                entity.Candidate.Status = 0;
                        }

                        if (PrevApprovalStatus == false)
                        {
                            entity.Candidate.Status = EmploymentStatus.REJECTED;
                            entity.Candidate.PersonnelManagers.RejectDate = DateTime.Now;
                            entity.Candidate.PersonnelManagers.RejectUser = CurUser;
                            entity.Candidate.User.IsActive = false;

                            if (!RemoveStaffPostReserve(entity.Candidate.Id, CurUser))
                            {
                                error = "Ошибка согласования.";
                                return false;
                            }
                        }

                        error = PrevApprovalStatus == false ? "Кандидат отклонен." : "Кандидат прошел предварительное согласование ДБ!";

                        if (!EmploymentCommonDao.SaveOrUpdateDocument<BackgroundCheck>(entity))
                        {
                            error = "Ошибка согласования.";
                            return false;
                        }
                        //сообщение тренеру из ДП
                        EmploymentSendEmail(entity.Candidate.User.Id, 4, false);
                        //если до предварительного согласования анкета была заполнена и поменялся статус на вторую проверку ДБ, шлем письмо.
                        if(entity.Candidate.Status == EmploymentStatus.PENDING_APPROVAL_BY_SECURITY)
                            EmploymentSendEmail(entity.Candidate.User.Id, 1, false);
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
                if (IsCancelApproveAvailale && (current.UserRole & UserRole.ConsultantOutsourcing) == UserRole.ConsultantOutsourcing)
                    error = "Отменить отклонение может только консультант аутсорсинга.";
                else
                    error = "Документ может согласовать только сотрудник ДБ.";
            }

            return false;
        }

        public bool ApproveBackgroundCheck(BackgroundCheckModel model, out string error)
        {
            error = string.Empty;

            IUser current = AuthenticationService.CurrentUser;
            User CurUser = UserDao.Load(current.Id);
            if ((current.UserRole & UserRole.Security) == UserRole.Security || (model.IsCancelApproveAvailale && (current.UserRole & UserRole.ConsultantOutsourcing) == UserRole.ConsultantOutsourcing))
            {
                BackgroundCheck entity = null;
                int? id = EmploymentCommonDao.GetDocumentId<BackgroundCheck>(model.UserId);
                if (id.HasValue)
                {
                    entity = EmploymentBackgroundCheckDao.Get(id.Value);
                }
                if (entity != null)
                {
                    if (CheckCandidateIsBlocked(entity.Candidate.User.Id))
                    {
                        error = "Данный кандидат временно заблокирован! Согласование невозможно!.";
                        return false;
                    }

                    if (model.IsCancelApproveAvailale)//отмена отклонения
                    {
                        entity.ApprovalStatus = null;
                        entity.Approver = null;
                        //entity.PyrusRef = PyrusRef;
                        entity.IsApprovalSkipped = false;
                        entity.ApprovalDate = null;
                        entity.Candidate.Status = EmploymentStatus.PENDING_APPROVAL_BY_SECURITY;
                        entity.Candidate.PersonnelManagers.RejectDate = null;
                        entity.Candidate.PersonnelManagers.RejectUser = null;
                        entity.Candidate.User.IsActive = true;
                        entity.CancelRejectUser = CurUser;
                        entity.CancelRejectDate = DateTime.Now;

                        if (!EmploymentCommonDao.SaveOrUpdateDocument<BackgroundCheck>(entity))
                        {
                            error = "Ошибка согласования.";
                            return false;
                        }
                        else
                        {
                            error = "Отклонение кандидата отменено!";
                            return true;
                        }
                    }


                    ////определяем признак кандидата из Экспресс-Волги
                    //Department ParentDep = DepartmentDao.Get(11923);
                    //IList<IdNameDto> volgadeps = ParentDep != null ? DepartmentDao.LoadAll().Where(x => x.Path.StartsWith(ParentDep.Path)).ToList().ConvertAll(x => new IdNameDto { Id = x.Id, Name = x.Name }) : null;
                    //model.IsVolga = volgadeps != null && volgadeps.Where(x => x.Id == entity.User.Department.Id).Count() != 0 ? true : false;


                    //сохраняем файл личного дела
                    if (entity.Candidate.Status == EmploymentStatus.PENDING_APPROVAL_BY_SECURITY)
                        SaveBackgroundCheckAttachments(model, entity.Candidate.Id);

                    if (entity.Candidate.Status == EmploymentStatus.PENDING_APPROVAL_BY_SECURITY && !model.IsApprovalSkipped)
                    {
                        entity.ApprovalStatus = model.ApprovalStatus;
                        entity.Approver = UserDao.Get(current.Id);
                        entity.PyrusRef = model.PyrusRef;
                        entity.IsApprovalSkipped = model.IsApprovalSkipped;
                        entity.ApprovalDate = DateTime.Now;
                        if (model.ApprovalStatus == true)
                        {
                            entity.Candidate.Status = EmploymentStatus.PENDING_APPLICATION_LETTER;
                        }
                        else if (model.ApprovalStatus == false)
                        {
                            entity.Candidate.Status = EmploymentStatus.REJECTED;
                            entity.Candidate.PersonnelManagers.RejectDate = DateTime.Now;
                            entity.Candidate.PersonnelManagers.RejectUser = CurUser;
                            entity.Candidate.User.IsActive = false;

                            if (!RemoveStaffPostReserve(entity.Candidate.Id, CurUser))
                            {
                                error = "Ошибка согласования.";
                                return false;
                            }

                            error = "Кандидат отклонен!";
                        }

                        if (!EmploymentCommonDao.SaveOrUpdateDocument<BackgroundCheck>(entity))
                        {
                            error = "Ошибка согласования.";
                            return false;
                        }
                        //сообщение руководителю из ДП
                        EmploymentSendEmail(entity.Candidate.User.Id, 2, false);
                        return true;
                    }
                    else if (entity.Candidate.Status == EmploymentStatus.PENDING_APPROVAL_BY_SECURITY && model.IsApprovalSkipped)
                    {
                        entity.ApprovalStatus = true;
                        entity.Approver = UserDao.Get(current.Id);
                        entity.PyrusRef = model.PyrusRef;
                        entity.Candidate.Status = EmploymentStatus.PENDING_APPLICATION_LETTER;
                        entity.IsApprovalSkipped = model.IsApprovalSkipped;
                        entity.ApprovalDate = DateTime.Now;
                        if (!EmploymentCommonDao.SaveOrUpdateDocument<BackgroundCheck>(entity))
                        {
                            error = "Ошибка изменения статуса.";
                            return false;
                        }
                        //сообщение руководителю из ДП
                        EmploymentSendEmail(entity.Candidate.User.Id, 2, false);
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
                if (model.IsCancelApproveAvailale && (current.UserRole & UserRole.ConsultantOutsourcing) == UserRole.ConsultantOutsourcing)
                    error = "Отменить отклонение может только консультант аутсорсинга.";
                else
                    error = "Документ может согласовать только сотрудник ДБ.";

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
            User CurUser = UserDao.Load(current.Id);
            if ((current.UserRole & UserRole.Manager) == UserRole.Manager || (viewModel.IsCancelApproveAvailale && (current.UserRole & UserRole.ConsultantOutsourcing) == UserRole.ConsultantOutsourcing))
            {
                Managers entity = null;
                int? id = EmploymentCommonDao.GetDocumentId<Managers>(viewModel.UserId);
                if (id.HasValue)
                {
                    entity = EmploymentManagersDao.Get(id.Value);
                }
                if (entity != null)
                {
                    //if (entity.Candidate.AppointmentCreator.Id != current.Id)
                    //{
                    //    error = "Кандидата может согласовать только руководитель, создавший соответствующую заявку на подбор персонала.";
                    //    return false;
                    //}
                    if (CheckCandidateIsBlocked(entity.Candidate.User.Id))
                    {
                        error = "Данный кандидат временно заблокирован! Согласование невозможно!.";
                        return false;
                    }

                    //отмена отклонения
                    if (viewModel.IsCancelApproveAvailale && AuthenticationService.CurrentUser.UserRole == UserRole.ConsultantOutsourcing)
                    {
                        entity.Candidate.Status = EmploymentStatus.PENDING_APPROVAL_BY_MANAGER;
                        entity.ManagerApprovalStatus = null;
                        entity.Candidate.PersonnelManagers.RejectDate = null;
                        entity.Candidate.PersonnelManagers.RejectUser = null;
                        entity.Candidate.User.IsActive = true;
                        entity.ApprovingManager = null;
                        entity.ManagerApprovalDate = null;
                        entity.CancelRejectUser = CurUser;
                        entity.CancelRejectDate = DateTime.Now;

                        if (!EmploymentCommonDao.SaveOrUpdateDocument<Managers>(entity))
                        {
                            error = "Ошибка сохранения.";
                            return false;
                        }
                        else
                        {
                            error = "Отклонение кандидата отменено!";
                            return true;
                        }
                    }


                    
                    
                    if (entity.Candidate.AppointmentReport != null && entity.Candidate.Appointment != null)
                    {
                        //проверка по обучению в найме
                        if (entity.Candidate.AppointmentReport.Type.Id == 1 && entity.Candidate.Appointment.Recruter != 2)
                        {
                            if (entity.Candidate.AppointmentReport.TestingResult < 3 || entity.Candidate.AppointmentReport.IsEducationExists == false)
                            {
                                error = "Обучение кандидата в отчете по подбору не пройдено!";
                                return false;
                            }
                        }

                        //проверка на увольнение сотрудника при приеме кандидата на его должность
                        if (entity.Candidate.Appointment.Reason.Id == 5)
                        {
                            if (entity.Candidate.Appointment.ReasonPositionUser != null)
                            {
                                User DismissUser = UserDao.Load(entity.Candidate.Appointment.ReasonPositionUser.Id);
                                IList<Dismissal> dml = DismissalDao.LoadAll().Where(x => x.User == DismissUser && x.SendTo1C.HasValue).ToList();
                                if (dml.Count == 0)
                                {
                                    error = "Данная ставка еще не освобождена! Согласовать кандидата на данный момент невозможно!";
                                    return false;
                                }
                            }
                        }
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
                        entity.Position = PositionDao.Load(viewModel.PositionId);
                        entity.ProbationaryPeriod = viewModel.ProbationaryPeriod;
                        entity.RequestNumber = viewModel.RequestNumber;
                        entity.SalaryBasis = viewModel.SalaryBasis;
                        entity.SalaryMultiplier = viewModel.SalaryMultiplier;
                        entity.WorkCity = viewModel.WorkCity;
                        entity.MentorName = viewModel.MentorName;
                        entity.IsSecondaryJob = viewModel.IsSecondaryJob;
                        entity.IsExternalPTWorker = !viewModel.IsSecondaryJob ? false : viewModel.IsExternalPTWorker;

                        //надбавки
                        entity.Candidate.PersonnelManagers.AreaMultiplier = viewModel.AreaMultiplier;
                        entity.Candidate.PersonnelManagers.PersonalAddition = viewModel.PersonalAddition;
                        entity.Candidate.PersonnelManagers.PositionAddition = viewModel.PositionAddition;
                        entity.Candidate.PersonnelManagers.AreaAddition = viewModel.AreaAddition;
                        entity.Candidate.PersonnelManagers.TravelRelatedAddition = viewModel.TravelRelatedAddition;
                        entity.Candidate.PersonnelManagers.CompetenceAddition = viewModel.CompetenceAddition;
                        entity.Candidate.PersonnelManagers.FrontOfficeExperienceAddition = viewModel.FrontOfficeExperienceAddition;
                        entity.PyrusNumber = viewModel.PyrusNumber;

                        if (!entity.Candidate.SendTo1C.HasValue && !viewModel.SendTo1C.HasValue)
                        {
                            entity.RegistrationDate = viewModel.RegistrationDate;
                            entity.Candidate.PersonnelManagers.EmploymentDate = viewModel.RegistrationDate;
                        }

                        //entity.Approver = UserDao.Get(current.Id);
                        if (viewModel.ManagerApprovalStatus == true)
                        {
                            entity.Candidate.Status = EmploymentStatus.PENDING_APPROVAL_BY_HIGHER_MANAGER;
                            entity.ManagerApprovalStatus = true;
                        }
                        else if (viewModel.ManagerApprovalStatus == false)
                        {
                            entity.Candidate.Status = EmploymentStatus.REJECTED;
                            entity.Candidate.PersonnelManagers.RejectDate = DateTime.Now;
                            entity.Candidate.PersonnelManagers.RejectUser = CurUser;
                            entity.Candidate.User.IsActive = false;
                            entity.ManagerApprovalStatus = false;

                            if (!RemoveStaffPostReserve(entity.Candidate.Id, CurUser))
                            {
                                error = "Ошибка согласования.";
                                return false;
                            }
                        }

                        //решили что за инициатора может согласовать его зам
                        User currentUser = UserDao.Load(AuthenticationService.CurrentUser.Id);
                        IList<User> managers = DepartmentDao.GetDepartmentManagers(currentUser.Department.Id, false)
                                .Where<User>(x => x.Level == currentUser.Level && x.RoleId == (int)UserRole.Manager && x.Id == entity.Candidate.AppointmentCreator.Id)
                                .ToList<User>();

                        if (managers.Count == 0)
                        {
                            error = "Для текущего пользователя не определены права для согласования данного кандидата!";
                            return false;
                        }

                        entity.ApprovingManager = managers.Count != 0 ? currentUser : entity.Candidate.AppointmentCreator;
                        entity.ManagerApprovalDate = DateTime.Now;
                        if (!EmploymentCommonDao.SaveOrUpdateDocument<Managers>(entity))
                        {
                            error = "Ошибка сохранения.";
                            return false;
                        }
                        else
                            EmploymentSendEmail(viewModel.UserId, 5, false);
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
                if (viewModel.IsCancelApproveAvailale && (current.UserRole & UserRole.Manager) == UserRole.Manager)
                    error = "Отменить отклонение кандидата может только консультант аутсорсинга!";
                else
                    error = "Кандидата может согласовать только руководитель.";
            }

            return false;
        }

        public bool ApproveCandidateByHigherManager(int userId, bool? approvalStatus, bool IsCancel, out string error)
        {
            error = string.Empty;

            User current = UserDao.Get(AuthenticationService.CurrentUser.Id);
            if ((current.UserRole & UserRole.Manager) == UserRole.Manager || (IsCancel && (current.UserRole & UserRole.ConsultantOutsourcing) == UserRole.ConsultantOutsourcing))
            {
                Managers entity = null;
                int? id = EmploymentCommonDao.GetDocumentId<Managers>(userId);
                if (id.HasValue)
                {
                    entity = EmploymentManagersDao.Get(id.Value);
                }
                if (entity != null)
                {
                    if (CheckCandidateIsBlocked(entity.Candidate.User.Id))
                    {
                        error = "Данный кандидат временно заблокирован! Согласование невозможно!.";
                        return false;
                    }


                    if (IsCancel)//отмена отклонения
                    {
                        entity.ApprovingHigherManager = null;
                        entity.HigherManagerApprovalDate = null;
                        entity.Candidate.Status = EmploymentStatus.PENDING_APPROVAL_BY_HIGHER_MANAGER;
                        entity.HigherManagerApprovalStatus = null;
                        entity.Candidate.PersonnelManagers.RejectDate = null;
                        entity.Candidate.PersonnelManagers.RejectUser = null;
                        entity.Candidate.User.IsActive = true;
                        entity.CancelRejectHigherUser = current;
                        entity.CancelRejectHigherDate = DateTime.Now;

                        if (!EmploymentCommonDao.SaveOrUpdateDocument<Managers>(entity))
                        {
                            error = "Ошибка сохранения.";
                            return false;
                        }
                        else
                        {
                            error = "Отклонение кандидата отменено!";
                            return false;
                        }
                    }


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
                            entity.Candidate.PersonnelManagers.RejectDate = DateTime.Now;
                            entity.Candidate.PersonnelManagers.RejectUser = current;
                            entity.Candidate.User.IsActive = false;
                            entity.HigherManagerApprovalStatus = false;

                            if (!RemoveStaffPostReserve(entity.Candidate.Id, current))
                            {
                                error = "Ошибка согласования.";
                                return false;
                            }
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
                if (IsCancel && (current.UserRole & UserRole.ConsultantOutsourcing) == UserRole.ConsultantOutsourcing)
                    error = "Отменить отклонение кандидата может только консультант аутсорсинга!";
                else
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

                if (CheckCandidateIsBlocked(entity.Candidate.User.Id))
                {
                    error = "Данный кандидат временно заблокирован! Согласование невозможно!.";
                    return false;
                }

                EmploymentStatus candidateStatus = candidate.Status;

                if (//candidateStatus == EmploymentStatus.PENDING_FINALIZATION_BY_PERSONNEL_MANAGER
                    candidateStatus == EmploymentStatus.DOCUMENTS_SIGNATURE_CANDIDATE_COMPLETE /* || candidateStatus == EmploymentStatus.COMPLETE*/)
                {
                    if (!IsUnlimitedEditAvailable())
                    {
                        error = "У вас нет прав для редактирования данных!";
                        return false;
                    }

                    //нет сканов необходимых документов
                    if (!EmploymentCandidateDao.GetCandidateState(candidate.Id).Single().CandidateApp)
                    {
                        error = "Нет сканов документов для приема!";
                        return false;
                    }
                    //формирование номера ТД  и приказа о приеме перенес в сохранение кадровиком списка документов для подписи кандидатом
                    //string NewEmploymentContractNumber = null;
                    //if (viewModel.ContractDate.HasValue)
                    //    NewEmploymentContractNumber = string.IsNullOrEmpty(viewModel.ContractNumber) ? EmploymentPersonnelManagersDao.GetNewEmploymentContractNumber(viewModel.ContractDate.Value) : viewModel.ContractNumber;
                    //entity.ContractNumber = NewEmploymentContractNumber;// viewModel.ContractNumber;

                    entity.AccessGroup = AccessGroupDao.Load(viewModel.AccessGroupId);
                    //entity.ApprovedByPersonnelManager = viewModel.ApprovedByPersonnelManager;
                    entity.AreaAddition = viewModel.AreaAddition;
                    entity.AreaMultiplier = viewModel.AreaMultiplier;
                    entity.Candidate = GetCandidate(viewModel.UserId);
                    entity.Candidate.PersonnelManagers = entity;
                    entity.Candidate.User.Grade = viewModel.Grade;
                    //entity.Candidate.User.Level = viewModel.Level;
                    entity.CompetenceAddition = viewModel.CompetenceAddition;
                    if (!entity.Candidate.SendTo1C.HasValue && !viewModel.SendTo1C.HasValue)
                    {
                        entity.ContractDate = viewModel.ContractDate;
                        entity.ContractEndDate = viewModel.ContractEndDate;
                        entity.EmploymentDate = viewModel.EmploymentDate;
                        entity.Candidate.Managers.RegistrationDate = viewModel.EmploymentDate;
                        entity.EmploymentOrderDate = viewModel.EmploymentOrderDate;
                    }
                    entity.FrontOfficeExperienceAddition = viewModel.FrontOfficeExperienceAddition;
                    entity.InsurableExperienceDays = viewModel.InsurableExperienceDays;
                    entity.InsurableExperienceMonths = viewModel.InsurableExperienceMonths;
                    entity.InsurableExperienceYears = viewModel.InsurableExperienceYears;
                    entity.IsHourlySalaryBasis = viewModel.IsHourlySalaryBasis;
                    entity.OverallExperienceDays = viewModel.OverallExperienceDays;
                    entity.OverallExperienceMonths = viewModel.OverallExperienceMonths;
                    entity.OverallExperienceYears = viewModel.OverallExperienceYears;
                    entity.PersonalAccount = viewModel.PersonalAccount;
                    entity.PersonalAccountContractor = PersonalAccountContractorDao.Load(viewModel.PersonalAccountContractorId);
                    entity.Signer = EmploymentSignersDao.Load(viewModel.SignerId);
                    entity.TravelRelatedAddition = viewModel.TravelRelatedAddition;
                    entity.InsuredPersonType = viewModel.InsuredPersonTypeId.HasValue ? InsuredPersonTypeDao.Load(viewModel.InsuredPersonTypeId.Value) : null;
                    entity.Status = viewModel.StatusId;
                    entity.ContractPoint_1_Id = viewModel.ContractPoint_1_Id;
                    entity.ContractPoint_2_Id = viewModel.ContractPoint_2_Id;
                    entity.ContractPoint_3_Id = viewModel.ContractPoint_3_Id;
                    entity.ContractPointsFio = viewModel.ContractPoint_1_Id == 2 ? viewModel.ContractPointsFio : null;
                    entity.ContractPointsAddress = viewModel.ContractPoint_2_Id == 4 ? viewModel.ContractPointsAddress : null;
                    entity.Schedule = viewModel.ScheduleId.HasValue ? ScheduleDao.Load(viewModel.ScheduleId.Value) : null;
                    entity.PositionAddition = viewModel.PositionAddition;
                    entity.PersonalAddition = viewModel.PersonalAddition;

                    entity.NorthExperienceYears = viewModel.NorthExperienceType == 3 ? viewModel.NorthExperienceYears : 0;
                    entity.NorthExperienceMonths = viewModel.NorthExperienceType == 3 ? viewModel.NorthExperienceMonths : 0;
                    entity.NorthExperienceDays = viewModel.NorthExperienceType == 3 ? viewModel.NorthExperienceDays : 0;
                    entity.NorthernAreaAddition = viewModel.NorthExperienceType == 3 ? viewModel.NorthernAreaAddition : 0;
                    entity.NorthExperienceType = viewModel.NorthExperienceType;
                    entity.ExtraCharges = viewModel.NorthExperienceType == 2 || viewModel.NorthExperienceType == 3 ? ExtraChargesDao.Load(viewModel.ExtraChargesId.Value) : null;

                    if (entity.SupplementaryAgreements != null && entity.SupplementaryAgreements.Count > 0)
                    {
                        entity.SupplementaryAgreements[0].CreateDate = viewModel.SupplementaryAgreementCreateDate;
                        entity.SupplementaryAgreements[0].Number = viewModel.SupplementaryAgreementNumber;
                        entity.SupplementaryAgreements[0].OrderCreateDate = viewModel.ChangeContractToIndefiniteOrderCreateDate;
                        entity.SupplementaryAgreements[0].OrderNumber = viewModel.ChangeContractToIndefiniteOrderNumber;
                    }

                    //entity.Approver = UserDao.Get(current.Id);
                    entity.CompleteDate = DateTime.Now;
                    entity.Candidate.Status = EmploymentStatus.COMPLETE;
                    entity.Candidate.Personnels = UserDao.Load(CurrentUser.Id);
                    if (!EmploymentCommonDao.SaveOrUpdateDocument<PersonnelManagers>(entity))
                    {
                        error = "Ошибка сохранения.";
                        return false;
                    }
                    else
                    {
                        //SendEmailToUser();
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
        /// <summary>
        /// Отклонение приема кадровиком
        /// </summary>
        /// <param name="viewModel"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public bool SavePersonnelManagersRejecting(PersonnelManagersModel viewModel, out string error)
        {
            error = string.Empty;

            IUser current = AuthenticationService.CurrentUser;
            User curUser = UserDao.Load(current.Id);
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

                //if (entity.Candidate.GeneralInfo == null || !entity.Candidate.GeneralInfo.AgreedToPersonalDataProcessing)
                //{
                //    error = StrNotAgreedToPersonalDataProcessing;
                //    return false;
                //}

                if (entity.Candidate.SendTo1C.HasValue)
                {
                    error = "Нельзя отклонить данного кандидата, так как его данные выгружены в 1С!";
                    return false;
                }

                EmploymentStatus candidateStatus = candidate.Status;

                if (candidateStatus != EmploymentStatus.REJECTED)
                {
                    if (!IsUnlimitedEditAvailable())
                    {
                        error = "У вас нет прав для редактирования данных!";
                        return false;
                    }

                    entity.Candidate.Status = EmploymentStatus.REJECTED;
                    entity.Candidate.User.IsActive = false;
                    entity.Candidate.Personnels = UserDao.Load(CurrentUser.Id);
                    entity.RejectDate = DateTime.Now;
                    entity.RejectUser = curUser;

                    if (!RemoveStaffPostReserve(entity.Candidate.Id, UserDao.Load(CurrentUser.Id)))
                    {
                        error = "Ошибка согласования.";
                        return false;
                    }

                    if (!EmploymentCommonDao.SaveOrUpdateDocument<PersonnelManagers>(entity))
                    {
                        error = "Ошибка сохранения.";
                        return false;
                    }
                    return true;
                }
                else
                {
                    error = "Кандидат уже был отклонен ранее!";
                }

            }
            else
            {
                error = "Отклонить кандидата может только сотрудник отдела кадров!";
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
        /// <summary>
        /// Проверка на доступность кандидата для согласования.
        /// </summary>
        /// <param name="UserId">Id учетной записи кандидата.</param>
        /// <returns></returns>
        public bool CheckCandidateIsBlocked(int UserId)
        {
            //проверяем по количеству доступных вакансий в найме и статусам кандидатов в приеме
            EmploymentCandidate entity = GetCandidate(UserId);
            //если кандидат не из найма, то не проверяем
            if (entity.Appointment == null) return false;

            IList<EmploymentCandidate> candidate = EmploymentCandidateDao.LoadAll().Where(x => x.Appointment != null && x.Appointment.Id == entity.Appointment.Id).ToList();
            //проверка работает при различных согласованиях, если кандидат пришел из найма
            //ДБ
            //руководитель
            //высшее руководство
            //кадровик
            if ((candidate.Where(x => x.Status == EmploymentStatus.PENDING_APPROVAL_BY_HIGHER_MANAGER || x.Status == EmploymentStatus.PENDING_FINALIZATION_BY_PERSONNEL_MANAGER ||
                x.Status == EmploymentStatus.COMPLETE || x.Status == EmploymentStatus.SENT_TO_1C).Count() == entity.Appointment.BankAccountantAcceptCount) &&
                (entity.Status != EmploymentStatus.PENDING_APPROVAL_BY_HIGHER_MANAGER && entity.Status != EmploymentStatus.PENDING_FINALIZATION_BY_PERSONNEL_MANAGER &&
                entity.Status != EmploymentStatus.COMPLETE && entity.Status != EmploymentStatus.SENT_TO_1C))
                return true;
            else
                return false;
        }
        /// <summary>
        /// Удаление резервирования в штатной расстановке.
        /// </summary>
        /// <param name="CandidateId">Id заявки на прием</param>
        /// <param name="CurUser">Текущий пользователь</param>
        /// <returns></returns>
        protected bool RemoveStaffPostReserve(int CandidateId, User CurUser)
        {
            StaffEstablishedPostUserLinks PostUserLink = StaffEstablishedPostUserLinksDao.GetPostUserLinkByDocId(CandidateId, (int)StaffReserveTypeEnum.Employment);

            if (PostUserLink == null) return true;

            PostUserLink.DocId = 0;
            PostUserLink.ReserveType = 0;
            PostUserLink.Editor = CurUser;
            PostUserLink.EditDate = DateTime.Now;

            try
            {
                StaffEstablishedPostUserLinksDao.SaveAndFlush(PostUserLink);
            }
            catch
            {
                return false;
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
            bool IsValid = false;
            // Контроль уровня вышестоящего руководителя
            if (!IsManagerLevelValid(current))
            {
                IsValid = false;
                throw new ValidationException(string.Format(StrIncorrectManagerLevel,
                        current.Level.HasValue ? current.Level.Value.ToString() : "<не указан>", current.Id));
            }
            else
                IsValid = true;
            // Контроль уровня руководителя-инициатора
            if (!IsManagerLevelValid(creator))
            {
                IsValid = false;
                throw new ValidationException(string.Format(StrIncorrectManagerLevel,
                        creator.Level.HasValue ? creator.Level.Value.ToString() : "<не указан>", creator.Id));
            }
            else
                IsValid = true;


            switch (current.Level)
            {
                case 2:
                case 3:
                    // Для руководителей 3 уровня получаем список ручных привязок к подразделениям, если первые две проверки по автоматическим правам не прошли.
                    return IsValid ? IsValid :  MissionOrderRoleRecordDao.GetRoleRecords(user: current, roleCode: "000000058")
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
                //для кадровикв банка Ибрагимова, Рогозина, Тиханова, Чеснова, Букова (осн) только режим просмотра
                if (AuthenticationService.CurrentUser.Id == 1002 || AuthenticationService.CurrentUser.Id == 998 || AuthenticationService.CurrentUser.Id == 991 || AuthenticationService.CurrentUser.Id == 1006 || AuthenticationService.CurrentUser.Id == 990)
                    return false;
                else
                    return true;
            }
            else
            {
                return false;
            }
        }

        public int CheckExistsEducationRecord(int UserId, int Type)
        {
            return EmploymentEducationDao.CheckExistsEducationRecord(UserId, Type);
        }

        /// <summary>
        /// К испытательному сроку дописываем прописью дни и месяцы.
        /// </summary>
        /// <param name="ProbationaryPeriod"></param>
        /// <returns></returns>
        protected string GetProbationaryPeriodString(string ProbationaryPeriod)
        {
            if (string.IsNullOrEmpty(ProbationaryPeriod)) return "";
            //если пользователи уже успели что-то внести строковые символы
            try { Convert.ToInt32(ProbationaryPeriod); }
            catch {return ProbationaryPeriod;}

            int i = Convert.ToInt32(ProbationaryPeriod);
            string str = string.Empty;

            if (i == 1) return ProbationaryPeriod + " месяц";
            if (i > 1 && i < 5) return ProbationaryPeriod + " месяца";
            if (i >= 5 && i <= 12) return ProbationaryPeriod + " месяцев";
            if (i > 12 && i <= 20) str = " дней";
            else if (Convert.ToInt32(ProbationaryPeriod.Substring(ProbationaryPeriod.Length - 1, 1)) == 1) str = " день";
            else if (Convert.ToInt32(ProbationaryPeriod.Substring(ProbationaryPeriod.Length - 1, 1)) > 1 && Convert.ToInt32(ProbationaryPeriod.Substring(ProbationaryPeriod.Length - 1, 1)) < 5) str = " дня";
            else str = " дней";

            return ProbationaryPeriod + str;
        }

        public string GetStartView()
        {
            UserRole currentUserRole = AuthenticationService.CurrentUser.UserRole;
            if ((currentUserRole & UserRole.Candidate) == UserRole.Candidate)
            {
                //return "GeneralInfo";
                return "ScanOriginalDocuments";
            }
            else
            {
                return "Roster";
            }
        }
        /// <summary>
        /// Считаем итоговую зарплату кандидата.
        /// </summary>
        /// <param name="entity">Данные руководителя.</param>
        /// <returns></returns>
        protected decimal CalculateCandidateSalary(Managers entity)
        {
            decimal SalaryBasis = (entity.SalaryBasis.HasValue ? entity.SalaryBasis.Value : 0);//оклад
            decimal SalaryMultiplier = (entity.SalaryMultiplier.HasValue ? entity.SalaryMultiplier.Value : 0);//ставка
            decimal AreaMultiplier = (entity.Candidate.PersonnelManagers.AreaMultiplier.HasValue ? entity.Candidate.PersonnelManagers.AreaMultiplier.Value : 0);//районный коэффициент %
            decimal PersonalAddition = (entity.Candidate.PersonnelManagers.PersonalAddition.HasValue ? entity.Candidate.PersonnelManagers.PersonalAddition.Value : 0);//Персональная надбавка (руб)
            decimal AreaAddition = (entity.Candidate.PersonnelManagers.AreaAddition.HasValue ? entity.Candidate.PersonnelManagers.AreaAddition.Value : 0);//Территориальная надбавка (руб)
            decimal TravelRelatedAddition = (entity.Candidate.PersonnelManagers.TravelRelatedAddition.HasValue ? entity.Candidate.PersonnelManagers.TravelRelatedAddition.Value : 0);//Надбавка за разъездной характер работы (руб)
            decimal CompetenceAddition = (entity.Candidate.PersonnelManagers.CompetenceAddition.HasValue ? entity.Candidate.PersonnelManagers.CompetenceAddition.Value : 0);//Надбавка за квалификацию (руб)
            decimal FrontOfficeExperienceAddition = (entity.Candidate.PersonnelManagers.FrontOfficeExperienceAddition.HasValue ? entity.Candidate.PersonnelManagers.FrontOfficeExperienceAddition.Value : 0);//Надбавка за стаж работы специалистом фронт-офиса (руб)
            decimal NorthernAreaAddition = (entity.Candidate.PersonnelManagers.NorthernAreaAddition.HasValue ? entity.Candidate.PersonnelManagers.NorthernAreaAddition.Value : 0);//северная %

            return (SalaryBasis * SalaryMultiplier + PersonalAddition + AreaAddition + FrontOfficeExperienceAddition + TravelRelatedAddition + CompetenceAddition)
                     + ((SalaryBasis * SalaryMultiplier + PersonalAddition + AreaAddition + FrontOfficeExperienceAddition + TravelRelatedAddition + CompetenceAddition) * (AreaMultiplier / 100))
                     + ((SalaryBasis * SalaryMultiplier + PersonalAddition + AreaAddition + FrontOfficeExperienceAddition + TravelRelatedAddition + CompetenceAddition) * (NorthernAreaAddition / 100)); 
        }

        public bool IsFixedTermContract(int userId)
        {
            User user = UserDao.Get(userId);
            return user != null && user.IsFixedTermContract.HasValue && user.IsFixedTermContract.Value;
        }
        /// <summary>
        /// Процедура формирования и отсылки сообщений в процессе приема кандидата на работу.
        /// </summary>
        /// <param name="UserId">Id кандидата</param>
        /// <param name="EmailType">Тип сообщения.</param>
        /// <param name="IsChangeDocList">Наличие изменений в списке кадровых документов на подпись кандидату.</param>
        protected void EmploymentSendEmail(int UserId, int EmailType, bool IsChangeDocList)
        {
            //EmailType - 1 - при заполнении анкеты в ДП, 2 - ДБ руководителю, 3 - руководителю о заявлении, 4 - тренеру при создании кандидата, 5 - вышестоящему руководству, 6 - руководителю и замам о готовности документов на прием,
            //7 - при отправки сканов на предварительное согласование ДБ, 8 - руководителю от кадровика (страшилка)
            EmploymentCandidate entity = GetCandidate(UserId);

            User user = UserDao.Load(entity.AppointmentCreator.Id);
            //User user = UserDao.Load(18458);    //для теста учетка Жени
            IList<User> managers = null;
            int CurrentLevel = 0;

            //если кандидат отклонен
            if (entity.Status == EmploymentStatus.REJECTED) return;

            //все вкладки анкеты кандидата должны быть посланы на утверждение
            //даже если будет последующая редакция кадровиком посланных на утверждение анкет кандидата, почта уйдет 1 раз
            if (!entity.GeneralInfo.IsFinal || !entity.Passport.IsFinal || !entity.Education.IsFinal || !entity.Family.IsFinal || !entity.MilitaryService.IsFinal || !entity.Experience.IsFinal || !entity.Contacts.IsFinal || !entity.BackgroundCheck.IsFinal) return;

            //проверка на наличие адреса в базе для руководителя
            if (EmailType == 2 || EmailType == 3)
            {
                if (string.IsNullOrEmpty(user.Email))
                {
                    Log.ErrorFormat("E-mail is empty for user {0}", user.Id);
                    return;
                }
            }

            string defaultEmail = null;
            string to = null;
            string Emailaddress = user.Email;
            string body = null;
            string Subject = null;

            //для проверки на необходимость отправки сообщения
            IList<CandidateStateDto> CandidateState = EmploymentCandidateDao.GetCandidateState(entity == null ? -1 : entity.Id);

            string Dep3Name = DepartmentDao.GetParentDepartmentWithLevel(entity.Managers.Department, 3) != null ? " - " + DepartmentDao.GetParentDepartmentWithLevel(entity.Managers.Department, 3).Name : "";

            switch (EmailType)
            {
                case 1: //в ДБ
                    if (entity.Status != EmploymentStatus.PENDING_APPROVAL_BY_SECURITY) return;
                    if (entity.IsCandidateToBackgroundSendEmail && entity.CandidateToBackgroundSendEmailDate.HasValue) return;  //сообщение было послано ранее
                    //проверка на необходимость отправки сообщения
                    if (CandidateState == null || !CandidateState.Single().CandidateReady) return;
                    defaultEmail = ConfigurationService.EmploymentCandidateToBackgroundCheckEmail;
                    Emailaddress = "list-priem-bezopas@sovcombank.ru";
                    //Emailaddress = "loseva@ruscount.ru";
                    //Emailaddress = "zagryazkin@ruscount.ru";
                    to = string.IsNullOrEmpty(defaultEmail) ? Emailaddress : defaultEmail;
                    Subject = "Оформлена заявка на прием" + Dep3Name;
                    body = @"Оформлена заявка на прием " + entity.User.Name + ". Необходимо согласование сотрудника Департамента безопасности.";
                    entity.IsCandidateToBackgroundSendEmail = true;
                    entity.CandidateToBackgroundSendEmailDate = DateTime.Now;
                    break;
                case 2: //из ДБ руководителю
                    if (entity.IsBackgroundToManagerSendEmail && entity.BackgroundToManagerSendEmailDate.HasValue) return;  //сообщение было послано ранее
                    defaultEmail = ConfigurationService.EmploymentBackgroundCheckToManagerEmail;
                    to = string.IsNullOrEmpty(defaultEmail) ? Emailaddress : defaultEmail;
                    if (!entity.BackgroundCheck.ApprovalStatus.HasValue && !entity.BackgroundCheck.IsApprovalSkipped) return;    //ошибка, ДБ не проверяли кандидата

                    if (entity.BackgroundCheck.ApprovalStatus.Value)
                    {
                        Subject = "Сотрудником Департамента безопасности согласован прием кандидата";
                        body = @"Сотрудником Департамента безопасности согласован прием кандидата " + entity.User.Name + ". Ожидается заявление о приеме от кандидата.";
                    }
                    else
                    {
                        Subject = "Сотрудником Департамента безопасности отклонен прием кандидата";
                        body = @"Сотрудником Департамента безопасности отклонен прием кандидата " + entity.User.Name + ".";
                    }
                    entity.IsBackgroundToManagerSendEmail = true;
                    entity.BackgroundToManagerSendEmailDate = DateTime.Now;
                    break;
                case 3: //руководителю на счет заявления
                    if (entity.IsCandidateToManagerSendEmail && entity.CandidateToManagerSendEmailDate.HasValue) return;  //сообщение было послано ранее
                    defaultEmail = ConfigurationService.EmploymentCandidateToManagerEmail;
                    to = string.IsNullOrEmpty(defaultEmail) ? Emailaddress : defaultEmail;
                    Subject = "Кандидатом выложено заявление о приеме";
                    body = @"Кандидатом " + entity.User.Name + " выложено заявление о приеме. Необходимо согласование руководителя.";
                    entity.IsCandidateToManagerSendEmail = true;
                    entity.CandidateToManagerSendEmailDate = DateTime.Now;
                    break;
                case 4: //тренеру
                    if (!entity.IsTrainingNeeded) return;//обучение не требуется
                    if (entity.IsManagerToTrainingSendEmail && entity.ManagerToTrainingSendEmailDate.HasValue) return;  //сообщение было послано ранее
                    defaultEmail = ConfigurationService.EmploymentManagerToTrainingEmail;
                    Emailaddress = "list-priem-obuch@sovcombank.ru";
                    to = string.IsNullOrEmpty(defaultEmail) ? Emailaddress : defaultEmail;
                    if (!entity.IsTrainingNeeded) return;   //обучение не требуется
                    Subject = "Оформлена заявка на прием кандидата. Требуется обучение";
                    if (entity.IsTrainingNeeded && entity.IsBeforEmployment)
                    {
                        body = @"Оформлена заявка на прием кандидата " + entity.User.Name + ". Кандидат проходит предварительную проверку департаментом безопасности. Требуется обучение тренера до приема кандидата.";
                    }
                    else
                    {
                        body = @"Оформлена заявка на прием кандидата " + entity.User.Name + ". Кандидат проходит предварительную проверку департаментом безопасности. Требуется обучение тренера после приема кандидата.";
                    }
                    entity.IsManagerToTrainingSendEmail = true;
                    entity.ManagerToTrainingSendEmailDate = DateTime.Now;
                    break;
                case 5: //вышестоящему руководству
                    if (!entity.Managers.ManagerApprovalStatus.HasValue) return;    //непосредственный руководитель еще не согласовал
                    if (entity.IsManagerToHigherManagerSendEmail && entity.ManagerToHigherManagerSendEmailDate.HasValue) return;    //письмо высшему руководству уже было

                    Emailaddress = null;
                    //IList<User> managers = null;

                    //так как в данном случае нужно послать сообщение нескольким сотрудникам, то определяем руководителей и подмастерье выше уровнем, собираем ихние адреса в строку

                    CurrentLevel = entity.AppointmentCreator.Level.Value;
                    //как показала практика, бывет, что инициатор 2 уровня, он же будет вышестоящим руководством
                    if (CurrentLevel == 2)
                    {
                        managers = DepartmentDao.GetDepartmentManagers(entity.AppointmentCreator.Department.Id, true).ToList<User>();
                        foreach (User mu in managers)
                        {
                            if (!string.IsNullOrEmpty(mu.Email))
                            {
                                //Emailaddress += (string.IsNullOrEmpty(Emailaddress) ? "" : ", ") + user.Email;//для теста
                                Emailaddress += (string.IsNullOrEmpty(Emailaddress) ? "" : ", ") + mu.Email;//рабочая строка
                            }
                        }
                    }
                    else  //для смертных
                    {
                        //может быть так, что выше уровнем нет никого, по этому нужно идти вверх до 3 уровня (автоматическая привязка), пока не найдем живых
                        while (CurrentLevel > 3)
                        {
                            managers = DepartmentDao.GetDepartmentManagers(entity.AppointmentCreator.Department.Id, true)
                            .Where<User>(x => x.Level == CurrentLevel - 1)
                            .ToList<User>();
                            foreach (User mu in managers)
                            {
                                if (!string.IsNullOrEmpty(mu.Email))
                                {
                                    //Emailaddress += (string.IsNullOrEmpty(Emailaddress) ? "" : ", ") + user.Email;//для теста
                                    Emailaddress += (string.IsNullOrEmpty(Emailaddress) ? "" : ", ") + mu.Email;//рабочая строка
                                }
                            }
                            if (managers.Count != 0) break;
                            CurrentLevel -= 1;
                        }

                    }

                    //ручная привязка утверждающего, если нет руководства в автомате и руководитель 3 уровня
                    if (managers.Count == 0)
                    {
                        IList<User> manualRoleManagers = ManualRoleRecordDao.GetManualRoleHoldersForUser(entity.AppointmentCreator.Id, UserManualRole.ApprovesEmployment)
                            .Where(x => x.Level == 2)
                            .ToList<User>();
                        foreach (User mu in manualRoleManagers)
                        {
                            if (!string.IsNullOrEmpty(mu.Email))
                                //Emailaddress += (string.IsNullOrEmpty(Emailaddress) ? "" : ", ") + user.Email;//для теста
                                Emailaddress += (string.IsNullOrEmpty(Emailaddress) ? "" : ", ") + mu.Email;//рабочая строка
                        }
                    }

                    if (string.IsNullOrEmpty(Emailaddress)) return;

                    defaultEmail = ConfigurationService.EmploymentManagerToHighManagerEmail;
                    to = string.IsNullOrEmpty(defaultEmail) ? Emailaddress : defaultEmail;
                    Subject = "Требуется согласование кандидата";
                    body = @"Кандидат " + entity.User.Name + " согласован непосредственным руководителем. Необходимо согласование кандидата высшим руководством.";
                    entity.IsManagerToHigherManagerSendEmail = true;
                    entity.ManagerToHigherManagerSendEmailDate = DateTime.Now;
                    break;
                case 6: //руководству и замам от кадровика
                    if (!entity.Managers.HigherManagerApprovalStatus.HasValue) return;    //Вышестоящий руководитель еще не согласовал

                    Emailaddress = null;//рабочая строка
                    //Emailaddress = "zagryazkin@ruscount.ru";//для теста
                    //IList<User> managers = null;

                    //так как в данном случае нужно послать сообщение нескольким сотрудникам, то определяем руководителей и подмастерье текущего уровня, собираем их адреса в строку
                    CurrentLevel = entity.AppointmentCreator.Level.Value;
                    managers = DepartmentDao.GetDepartmentManagers(entity.AppointmentCreator.Department.Id, false)
                        .Where<User>(x => x.Level == CurrentLevel && x.RoleId == (int)UserRole.Manager)
                        .ToList<User>();
                        foreach (User mu in managers)
                        {
                            if (!string.IsNullOrEmpty(mu.Email))
                            {
                                //Emailaddress += (string.IsNullOrEmpty(Emailaddress) ? "" : ", ") + "zagryazkin@ruscount.ru";//для теста
                                Emailaddress += (string.IsNullOrEmpty(Emailaddress) ? "" : ", ") + mu.Email;//рабочая строка
                            }
                        }



                    //ручная привязка утверждающего, если нет руководства в автомате и руководитель 3 уровня
                    if (managers.Count == 0)
                    {
                        IList<User> manualRoleManagers = ManualRoleRecordDao.GetManualRoleHoldersForUser(entity.AppointmentCreator.Id, UserManualRole.ApprovesEmployment)
                            .Where(x => x.Level == 2)
                            .ToList<User>();
                        foreach (User mu in manualRoleManagers)
                        {
                            if (!string.IsNullOrEmpty(mu.Email))
                                //Emailaddress += (string.IsNullOrEmpty(Emailaddress) ? "" : ", ") + Emailaddress;//для теста
                                Emailaddress += (string.IsNullOrEmpty(Emailaddress) ? "" : ", ") + mu.Email;//рабочая строка
                        }
                    }
                    
                    if (string.IsNullOrEmpty(Emailaddress)) return;


                    defaultEmail = ConfigurationService.EmploymentPersonnelManagerToManagerEmail;
                    to = string.IsNullOrEmpty(defaultEmail) ? Emailaddress : defaultEmail;

                    body = @"Документы на прием для подписи Кандидатом " + entity.User.Name + " сформированы в заявке на прием №." + entity.Id.ToString() + @" 
                             Необходимо распечатать, подписать у кандидата и прикрепить подписанные сканы на страницу 'Документы' в заявку на прием №." + entity.Id.ToString();

                    if (!entity.IsPersonnelManagerToManagerSendEmail && !entity.PersonnelManagerToManagerSendEmailDate.HasValue)
                    {    //письмо руководству уже было
                        Subject = "Сформирован пакет кадровых документов для подписи кандидатом";
                        //body = @"Кадровые документы для подписи кандидатом " + entity.User.Name + " готовы!";
                    }
                    else
                    {
                        if (IsChangeDocList)//если не ошибочное нажатие без изменений
                        {
                            Subject = "Пакет кадровых документов для подписи кандидатом изменен";
                            //body = @"Кадровые документы для подписи кандидатом " + entity.User.Name + " готовы!";
                        }
                        else
                            return;
                    }
                    entity.IsPersonnelManagerToManagerSendEmail = true;
                    entity.PersonnelManagerToManagerSendEmailDate = DateTime.Now;
                    break;
                case 7: //в ДБ
                    if (entity.IsCandidateToBackgroundPrevSendEmail && entity.CandidateToBackgroundPrevSendEmailDate.HasValue) return;  //сообщение было послано ранее
                    //проверка на необходимость отправки сообщения
                    if (CandidateState == null || !CandidateState.Single().CandidateReady) return;
                    //string Dep3Name = DepartmentDao.GetParentDepartmentWithLevel(entity.Managers.Department, 3) != null ? " - " + DepartmentDao.GetParentDepartmentWithLevel(entity.Managers.Department, 3).Name : "";
                    //DepartmentDao.GetParentDepartmentWithLevel(entity.Department, 3).Name
                    defaultEmail = ConfigurationService.EmploymentCandidateToBackgroundCheckEmail;
                    Emailaddress = "list-priem-bezopas@sovcombank.ru";
                    //Emailaddress = "loseva@ruscount.ru";
                    //Emailaddress = "zagryazkin@ruscount.ru";
                    to = string.IsNullOrEmpty(defaultEmail) ? Emailaddress : defaultEmail;
                    Subject = "Оформлена заявка на прием (предварительное согласование)" + Dep3Name;
                    body = @"Оформлена заявка на прием " + entity.User.Name + ". Необходимо предварительное согласование согласование сотрудника Департамента безопасности.";
                    entity.IsCandidateToBackgroundPrevSendEmail = true;
                    entity.CandidateToBackgroundPrevSendEmailDate = DateTime.Now;
                    break;
                case 8: //руководству и замам от кадровика (страшилка)
                    Emailaddress = null;//рабочая строка
                    //Emailaddress = "zagryazkin@ruscount.ru";//для теста
                    //IList<User> managers = null;

                    //так как в данном случае нужно послать сообщение нескольким сотрудникам, то определяем руководителей и подмастерье текущего уровня, собираем их адреса в строку
                    CurrentLevel = entity.AppointmentCreator.Level.Value;
                    managers = DepartmentDao.GetDepartmentManagers(entity.AppointmentCreator.Department.Id, false)
                        .Where<User>(x => x.Level == CurrentLevel && x.RoleId == (int)UserRole.Manager)
                        .ToList<User>();
                    foreach (User mu in managers)
                    {
                        if (!string.IsNullOrEmpty(mu.Email))
                        {
                            //Emailaddress += (string.IsNullOrEmpty(Emailaddress) ? "" : ", ") + "zagryazkin@ruscount.ru";//для теста
                            Emailaddress += (string.IsNullOrEmpty(Emailaddress) ? "" : ", ") + mu.Email;//рабочая строка
                        }
                    }

                    //ручная привязка утверждающего, если нет руководства в автомате и руководитель 3 уровня
                    if (managers.Count == 0)
                    {
                        IList<User> manualRoleManagers = ManualRoleRecordDao.GetManualRoleHoldersForUser(entity.AppointmentCreator.Id, UserManualRole.ApprovesEmployment)
                            .Where(x => x.Level == 2)
                            .ToList<User>();
                        foreach (User mu in manualRoleManagers)
                        {
                            if (!string.IsNullOrEmpty(mu.Email))
                                //Emailaddress += (string.IsNullOrEmpty(Emailaddress) ? "" : ", ") + Emailaddress;//для теста
                                Emailaddress += (string.IsNullOrEmpty(Emailaddress) ? "" : ", ") + mu.Email;//рабочая строка
                        }
                    }

                    if (string.IsNullOrEmpty(Emailaddress)) return;


                    defaultEmail = ConfigurationService.EmploymentPersonnelManagerToManagerEmail;
                    to = string.IsNullOrEmpty(defaultEmail) ? Emailaddress : defaultEmail;
                    Subject = "Просьба ускорить процесс подписания документов!";
                    body = @"Дата приема кандидата " + entity.User.Name + " " + entity.Managers.RegistrationDate.Value.ToShortDateString() + @".  
                             По состоянию на " + DateTime.Now.ToShortDateString() + " нет подписанных сканов документов от кандидата. Просьба ускорить процесс подписания документов!";

                    
                    break;
            }

            


            EmailDto dto = SendEmail(to, Subject, body);
            if (string.IsNullOrEmpty(dto.Error))
            {
                try
                {
                    EmploymentCommonDao.SaveOrUpdateDocument<EmploymentCandidate>(entity);
                }
                catch (Exception)
                {
                }
                finally
                {
                }
            }
            else
                Log.ErrorFormat("Cannot send email to user {0}(email {1}) about deduction {2} : {3}",
                    user.Id, to, 18458, dto.Error);
        }
        /// <summary>
        /// Собираем список участников процесса приема данного кандидата.
        /// </summary>
        /// <param name="CandidateId">Id заявки на прием</param>
        protected IList<User> GetCandidateProccedRegistration(int CandidateId)
        {
            EmploymentCandidate entity = GetCandidate(CandidateId);
            User currentUser = UserDao.Load(AuthenticationService.CurrentUser.Id);
            //руководство - инициаторы
            IList<User> managersApproval = DepartmentDao.GetDepartmentManagers(entity.AppointmentCreator.Department.Id, false)
                        //.Where<User>(x => x.Level == currentUser.Level /*&& x.RoleId == (int)UserRole.Manager && x.Id == entity.AppointmentCreator.Id*/)
                        .ToList<User>();
            //автоматическая привязка утверждающего
            IList<User> HighManagers = DepartmentDao.GetDepartmentManagers(entity.AppointmentCreator.Department.Id, true)
                .Where<User>(x => x.Level < entity.AppointmentCreator.Level /*&& x.Level != candidate.AppointmentCreator.Level*/ && x.Level >= (entity.AppointmentCreator.Level > 3 ? 3 : 2))
                .OrderByDescending<User, int?>(manager => manager.Level)
                .ToList<User>();

            
            //ручная привязка утверждающего
            IList<User> manualRoleManagers = ManualRoleRecordDao.GetManualRoleHoldersForUser(entity.AppointmentCreator.Id, UserManualRole.ApprovesEmployment);

            //объединяем всех участников в один список
            IList<User> RegUsersList = new List<User>().Union(managersApproval).Union(HighManagers).Union(manualRoleManagers).ToList();

            //департамент безопасности
            if (entity.BackgroundCheck.PrevApprover != null)
                RegUsersList.Add(entity.BackgroundCheck.PrevApprover);
            if (entity.BackgroundCheck.Approver != null && entity.BackgroundCheck.Approver != entity.BackgroundCheck.PrevApprover)
                RegUsersList.Add(entity.BackgroundCheck.Approver);

            //кадровики РК
            RegUsersList.Add(entity.Personnels);

            //исключаем текущего пользователя из списка, чтобы не спамил сам себя
            if (RegUsersList.Contains(currentUser))
                RegUsersList.Remove(currentUser);


            return RegUsersList;// UserDao.LoadAll().Where(x => x.Id == 1246).ToList();
        }
        /// <summary>
        /// Отправка сообщения участника процесса приема другому участнику.
        /// </summary>
        /// <param name="model">Обрабатываемая модель.</param>
        /// <param name="error">Сообщение.</param>
        /// <returns></returns>
        public bool EmploymentProccedRegistrationSendEmail(PersonnelInfoModel model, out string error)
        {
            error = string.Empty;

            User currentUser = UserDao.Load(AuthenticationService.CurrentUser.Id);

            User UserTo = UserDao.Get(model.ToUserId);


            string to = string.Empty;
            string body = string.Empty;
            string Subject = string.Empty;
            string from = string.Empty;


            if (string.IsNullOrEmpty(UserTo.Email))
            {
                error = "Отправка сообщения невозможна, так как в базе данных нет данных об почтовом адресе оппонента!";
                return false;
            }
            else
                to = UserTo.Email;

            //to = "zagryazkin@ruscount.ru";
            //from = "zagryazkin@ruscount.ru";

            Subject = model.Subject;
            body = model.EmailMessage;

            EmailDto dto = new EmailDto();
            Settings settings = SettingsDao.LoadFirst();
            dto.SmtpServer = settings.NotificationSmtp;
            //dto.SmtpServer = "mail.ruscount.ru";
            dto.SmtpPort = settings.NotificationPort;
            dto.UserName = settings.NotificationLogin;
            dto.Password = settings.NotificationPassword;
            //dto.From = from;
            dto.From = string.IsNullOrEmpty(currentUser.Email) ? settings.NotificationEmail : currentUser.Email;
            dto.To = to;
            dto.Subject = Subject;
            dto.Body = body;

            MailMessage mailMessage = null;
            try
            {
                mailMessage = new MailMessage
                {
                    From = new MailAddress(dto.From, currentUser.Name)
                };
                string[] toAddresses = dto.To.Split(';');
                foreach (string address in toAddresses)
                    mailMessage.To.Add(new MailAddress(address, address));
                //тавим в копию отправителя
                string[] CCAddresses = dto.From.Split(';');
                foreach (string address in CCAddresses)
                    mailMessage.CC.Add(new MailAddress(address, address));

                mailMessage.Subject = dto.Subject;
                mailMessage.Body = "<html>" + dto.Body + "</html>";
                mailMessage.IsBodyHtml = true;
                //mailMessage.Body = dto.Body;
                //mailMessage.IsBodyHtml = false;
                var smtpClient = new SmtpClient
                {
                    Host = dto.SmtpServer,
                    Port = dto.SmtpPort,
                    UseDefaultCredentials = false,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new BlockGSSAPINTLMCredential(dto.UserName, dto.Password)
                };
                smtpClient.Send(mailMessage);

                

                Log.DebugFormat("Отправлено письмо на {0}, тема {1}, текст {2}"
                        , dto.To, dto.Subject, dto.Body);

                error = "Сообщение отправлено!";
                return true;
            }
            catch (Exception ex)
            {
                Log.Error("Exception:", ex);
                dto.Error = "Исключение: " + ex.GetBaseException().Message;
                error = "По техническим причинам сообщение не отправлено! (Исключение: " + ex.GetBaseException().Message + ")";
                return false;
            }
            finally
            {
                if (mailMessage != null)
                    mailMessage.Dispose();
            }
        }
        public void GetStaffEstablishmentPostDetails(ManagersModel model)
        {
            //достаем список штатных доступных единиц
            if (model.IsSP)
            {
                model.PostUserLinks = StaffEstablishedPostDao.GetStaffEstablishedArrangements(model.DepartmentId)
                .Where(x => x.IsVacation || (x.IsReserve && x.Id == model.UserLinkId))
                .ToList()
                .ConvertAll(x => new IdNameDto { Id = x.Id, Name = x.PositionName + (x.IsSTD ? " - СТД" : "") + (x.ReplacedId != 0 ? " - " + x.ReplacedName : "")  });
                model.PostUserLinks.Insert(0, new IdNameDto { Id = 0, Name = "" });
            }
            else //по Id строки штатной расстановки достаем данные по вакансии
            {
                StaffEstablishedPostUserLinks PostUserLink = StaffEstablishedPostUserLinksDao.Get(model.UserLinkId);
                //оклад штатной единицы
                model.SalaryBasis = PostUserLink.StaffEstablishedPost.Salary;
                //районный коэффициент
                if (PostUserLink.StaffEstablishedPost.PostChargeLinks.Where(x => x.ExtraCharges.GUID == "66f08438-f006-44e8-b9ee-32a8dcf557ba").Count() != 0)
                    model.AreaMultiplier = PostUserLink.StaffEstablishedPost.PostChargeLinks.Where(x => x.ExtraCharges.GUID == "66f08438-f006-44e8-b9ee-32a8dcf557ba").Single().Amount;
                else
                    model.AreaMultiplier = 0;
            }
        }
    }
}