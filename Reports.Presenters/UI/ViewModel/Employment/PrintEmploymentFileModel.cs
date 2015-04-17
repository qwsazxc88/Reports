using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto.Employment2;

namespace Reports.Presenters.UI.ViewModel.Employment2
{
    public class PrintEmploymentFileModel : AbstractEmploymentModel
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Patronymic { get; set; }
        public IList<NameChangeDto> NameChanges { get; set; }
        public bool IsMale { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string PlaceOfBirth { get; set; }
        public bool IsMarried { get; set; }
        //public string EducationLevel { get; set; }
        public IList<HigherEducationDiplomaDto> HigherEducationDiplomas { get; set; }
        public IList<TrainingDto> Training { get; set; }
        public string RegistrationZipCode { get; set; }
        public string RegistrationAddress { get; set; }
        public string ActualAddress { get; set; }
        public string PhoneNumbers { get; set; }
        public string Cohabitants { get; set; }
        public IList<ForeignLanguageDto> ForeignLanguages { get; set; }
        public IList<ExperienceItemDto> ExperienceItems { get; set; }
        public decimal? AverageSalary { get; set; }
        public string Liabilities { get; set; }
        public string PreviousDismissalReason { get; set; }
        public string PreviousSuperior { get; set; }
        public string PositionSought { get; set; }
        public FamilyMemberDto Spouse { get; set; }
        public FamilyMemberDto Father { get; set; }
        public FamilyMemberDto Mother { get; set; }
        public IList<FamilyMemberDto> Children { get; set; }
        public string MilitaryOperationsExperience { get; set; }
        public string DriversLicenseNumber { get; set; }
        public string DriversLicenseCategories { get; set; }
        public DateTime? DriversLicenseDateOfIssue { get; set; }
        public int? DrivingExperience { get; set; }
        public string AutomobileMake { get; set; }
        public string AutomobileLicensePlateNumber { get; set; }
        public bool IsReadyForBusinessTrips { get; set; }
        public string Sports { get; set; }
        public string Hobbies { get; set; }
        public string ImportantEvents { get; set; }
        public IList<ReferenceDto> References { get; set; }

        public string InternalPassportSeries { get; set; }
        public string InternalPassportNumber { get; set; }
        public string InternalPassportSubdivisionCode { get; set; }
        public DateTime? InternalPassportDateOfIssue { get; set; }
        public string InternalPassportIssuedBy { get; set; }

        public string InternationalPassportSeries { get; set; }
        public string InternationalPassportNumber { get; set; }
        public DateTime? InternationalPassportDateOfIssue { get; set; }
        public string InternationalPassportIssuedBy { get; set; }
        public string FamilyStatusName { get; set; }
        public string Educations { get; set; }
        public string ChronicalDiseases { get; set; }
        public string Penalties { get; set; }
        public string PsychiatricAndAddictionTreatment { get; set; }
        public string Drinking { get; set; }
        public string Smoking { get; set; }

        public PrintEmploymentFileModel()
        {
            NameChanges = new List<NameChangeDto>();
            HigherEducationDiplomas = new List<HigherEducationDiplomaDto>();
            Training = new List<TrainingDto>();
            ForeignLanguages = new List<ForeignLanguageDto>();
            ExperienceItems = new List<ExperienceItemDto>();
            Children = new List<FamilyMemberDto>();
            References = new List<ReferenceDto>();
        }
    }
}