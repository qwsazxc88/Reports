create table Certification (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  CertificationDate DATETIME null,
  CertificateNumber NVARCHAR(20) null,
  CertificateDateOfIssue DATETIME null,
  InitiatingOrder NVARCHAR(20) null,
  EducationId INT null,
  constraint PK_Certification  primary key (Id)
)
create table Family (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  SpouseId INT null,
  FatherId INT null,
  MotherId INT null,
  Cohabitants NVARCHAR(250) null,
  constraint PK_Family  primary key (Id)
)
create table DocumentType (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  Code NVARCHAR(10) null,
  Name NVARCHAR(128) null,
  constraint PK_DocumentType  primary key (Id)
)
create table PersonnelManagers (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  EmploymentOrderDate DATETIME null,
  EmploymentOrderNumber NVARCHAR(10) null,
  EmploymentDate DATETIME null,
  ContractDate DATETIME null,
  ContractNumber NVARCHAR(10) null,
  NorthernAreaAddition DECIMAL(19, 2) null,
  AreaMultiplier DECIMAL(19, 2) null,
  AreaAddition DECIMAL(19, 2) null,
  TravelRelatedAddition DECIMAL(19, 2) null,
  CompetenceAddition DECIMAL(19, 2) null,
  FrontOfficeExperienceAddition DECIMAL(19, 2) null,
  Grade INT null,
  InsurableExperienceYears INT null,
  InsurableExperienceMonths INT null,
  InsurableExperienceDays INT null,
  NorthExperienceYears INT null,
  NorthExperienceMonths INT null,
  NorthExperienceDays INT null,
  PersonalAccount NVARCHAR(30) null,
  PreviousIncome DECIMAL(19, 2) null,
  IsNonResident BIT null,
  constraint PK_PersonnelManagers  primary key (Id)
)
create table OnsiteTrainingType (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  Code NVARCHAR(10) null,
  Name NVARCHAR(128) null,
  constraint PK_OnsiteTrainingType  primary key (Id)
)
create table Experience (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  WorkBookSeries NVARCHAR(10) null,
  WorkBookNumber NVARCHAR(10) null,
  WorkBookDateOfIssue DATETIME null,
  WorkBookSupplementSeries NVARCHAR(10) null,
  WorkBookSupplementNumber NVARCHAR(10) null,
  WorkBookSupplementDateOfIssue DATETIME null,
  constraint PK_Experience  primary key (Id)
)
create table Candidate (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  UserId INT null,
  GeneralInfoId INT null,
  PassportId INT null,
  EducationId INT null,
  FamilyId INT null,
  MilitaryServiceId INT null,
  ExperienceId INT null,
  ContactsId INT null,
  BackgroundCheckId INT null,
  OnsiteTrainingId INT null,
  ManagersId INT null,
  PersonnelManagersId INT null,
  constraint PK_Candidate  primary key (Id)
)
create table Contacts (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  ZipCode NVARCHAR(6) null,
  Region NVARCHAR(50) null,
  District NVARCHAR(50) null,
  City NVARCHAR(50) null,
  Street NVARCHAR(50) null,
  StreetNumber NVARCHAR(10) null,
  Building NVARCHAR(10) null,
  Appartment NVARCHAR(10) null,
  WorkPhone NVARCHAR(10) null,
  HomePhone NVARCHAR(10) null,
  Mobile NVARCHAR(10) null,
  Email NVARCHAR(10) null,
  constraint PK_Contacts  primary key (Id)
)
create table TaxpayerStatus (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  Code NVARCHAR(10) null,
  Name NVARCHAR(128) null,
  constraint PK_TaxpayerStatus  primary key (Id)
)
create table InsuredPersonType (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  Code NVARCHAR(10) null,
  Name NVARCHAR(128) null,
  constraint PK_InsuredPersonType  primary key (Id)
)
create table Reference (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  LastName NVARCHAR(50) null,
  FirstName NVARCHAR(50) null,
  Patronymic NVARCHAR(50) null,
  WorksAt NVARCHAR(50) null,
  Position NVARCHAR(50) null,
  Phone NVARCHAR(10) null,
  Relation NVARCHAR(50) null,
  BackgroundCheckId INT null,
  constraint PK_Reference  primary key (Id)
)
create table PostGraduateEducationDiploma (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  EducationId INT null,
  constraint PK_PostGraduateEducationDiploma  primary key (Id)
)
create table MilitaryService (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  IsLiableForMilitaryService BIT null,
  MilitaryCardNumber NVARCHAR(20) null,
  MilitaryCardDate DATETIME null,
  ReserveCategoryId INT null,
  RankId INT null,
  SpecialityCategoryId INT null,
  MilitarySpecialityCode NVARCHAR(6) null,
  CombatFitness NVARCHAR(50) null,
  Commissariat NVARCHAR(100) null,
  constraint PK_MilitaryService  primary key (Id)
)
create table HigherEducationDiploma (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  IssuedBy NVARCHAR(150) null,
  Series NVARCHAR(10) null,
  Number NVARCHAR(10) null,
  AdmissionYear NVARCHAR(4) null,
  GraduationYear NVARCHAR(4) null,
  Qualification NVARCHAR(50) null,
  Speciality NVARCHAR(50) null,
  Profession NVARCHAR(50) null,
  Department NVARCHAR(50) null,
  EducationId INT null,
  constraint PK_HigherEducationDiploma  primary key (Id)
)
create table OnsiteTraining (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  OnsiteTrainingTypeId INT null,
  Description NVARCHAR(250) null,
  BeginningDate DATETIME null,
  EndDate DATETIME null,
  IsComplete BIT null,
  ReasonsForIncompleteTraining NVARCHAR(250) null,
  Results NVARCHAR(250) null,
  constraint PK_OnsiteTraining  primary key (Id)
)
create table MilitarySpecialityCategory (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  Code NVARCHAR(10) null,
  Name NVARCHAR(128) null,
  constraint PK_MilitarySpecialityCategory  primary key (Id)
)
create table Disability (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  CertificateSeries NVARCHAR(10) null,
  CertificateNumber NVARCHAR(10) null,
  CertificateDateOfIssue DATETIME null,
  DisabilityDegree NVARCHAR(2) null,
  CertificateExpirationDate DATETIME null,
  GeneralInfoId INT null,
  constraint PK_Disability  primary key (Id)
)
create table Automobile (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  Make NVARCHAR(100) null,
  LicensePlateNumber NVARCHAR(10) null,
  BackgroundCheckId INT null,
  constraint PK_Automobile  primary key (Id)
)
create table Education (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  constraint PK_Education  primary key (Id)
)
create table MilitaryRank (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  Code NVARCHAR(10) null,
  Name NVARCHAR(128) null,
  constraint PK_MilitaryRank  primary key (Id)
)
create table Country (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  Code NVARCHAR(10) null,
  Name NVARCHAR(128) null,
  constraint PK_Country  primary key (Id)
)
create table FamilyMember (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  Name NVARCHAR(50) null,
  PassportData NVARCHAR(150) null,
  DateOfBirth DATETIME null,
  PlaceOfBirth NVARCHAR(150) null,
  WorksAt NVARCHAR(50) null,
  Contacts NVARCHAR(100) null,
  FamilyId INT null,
  constraint PK_FamilyMember  primary key (Id)
)
create table ExperienceItem (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  BeginningDate DATETIME null,
  EndDate DATETIME null,
  Company NVARCHAR(50) null,
  Position NVARCHAR(50) null,
  CompanyContacts NVARCHAR(250) null,
  ExperienceId INT null,
  constraint PK_ExperienceItem  primary key (Id)
)
create table GeneralInfo (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  LastName NVARCHAR(50) null,
  FirstName NVARCHAR(50) null,
  Patronymic NVARCHAR(50) null,
  IsMale BIT null,
  CitizenshipId INT null,
  InsuredPersonTypeId INT null,
  DateOfBirth DATETIME null,
  RegionOfBirth NVARCHAR(50) null,
  DistrictOfBirth NVARCHAR(50) null,
  CityOfBirth NVARCHAR(50) null,
  INN NVARCHAR(12) null,
  SNILS NVARCHAR(14) null,
  TaxpayerStatusId INT null,
  AgreedToPersonalDataProcessing BIT null,
  constraint PK_GeneralInfo  primary key (Id)
)
create table FinancialLiability (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  Code NVARCHAR(10) null,
  Name NVARCHAR(128) null,
  BackgroundCheckId INT null,
  constraint PK_FinancialLiability  primary key (Id)
)
create table NameChange (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  PreviousName NVARCHAR(50) null,
  Date DATETIME null,
  Place NVARCHAR(50) null,
  Reason NVARCHAR(50) null,
  GeneralInfoId INT null,
  constraint PK_NameChange  primary key (Id)
)
create table ForeignLanguage (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  LanguageName NVARCHAR(50) null,
  Level NVARCHAR(20) null,
  GeneralInfoId INT null,
  constraint PK_ForeignLanguage  primary key (Id)
)
create table Managers (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  PositionId INT null,
  DirectorateId INT null,
  DepartmentId INT null,
  EmploymentConditions NVARCHAR(250) null,
  Schedule NVARCHAR(255) null,
  ProbationaryPeriod NVARCHAR(255) null,
  SalaryBasis DECIMAL(19, 2) null,
  WorkCity NVARCHAR(255) null,
  PersonalAddition DECIMAL(19, 2) null,
  PositionAddition DECIMAL(19, 2) null,
  IsFront BIT null,
  Bonus DECIMAL(19, 2) null,
  IsLiable BIT null,
  RequestNumber NVARCHAR(10) null,
  constraint PK_Managers  primary key (Id)
)
create table Passport (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  DocumentTypeId INT null,
  InternalPassportSeries NVARCHAR(4) null,
  InternalPassportNumber NVARCHAR(6) null,
  InternalPassportDateOfIssue DATETIME null,
  InternalPassportIssuedBy NVARCHAR(150) null,
  InternalPassportSubdivisionCode NVARCHAR(7) null,
  RegistrationDate DATETIME null,
  ZipCode NVARCHAR(6) null,
  Region NVARCHAR(50) null,
  District NVARCHAR(50) null,
  City NVARCHAR(50) null,
  Street NVARCHAR(50) null,
  StreetNumber NVARCHAR(6) null,
  Building NVARCHAR(3) null,
  Appartment NVARCHAR(5) null,
  InternationalPassportSeries NVARCHAR(10) null,
  InternationalPassportNumber NVARCHAR(10) null,
  InternationalPassportDateOfIssue DATETIME null,
  InternationalPassportIssuedBy NVARCHAR(150) null,
  constraint PK_Passport  primary key (Id)
)
create table Training (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  CertificateIssuedBy NVARCHAR(50) null,
  Series NVARCHAR(10) null,
  Number NVARCHAR(10) null,
  BeginningDate DATETIME null,
  EndDate DATETIME null,
  Speciality NVARCHAR(50) null,
  EducationId INT null,
  constraint PK_Training  primary key (Id)
)
create table MilitaryReserveCategory (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  Code NVARCHAR(10) null,
  Name NVARCHAR(128) null,
  constraint PK_MilitaryReserveCategory  primary key (Id)
)
create table BackgroundCheck (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  AverageSalary DECIMAL(19, 2) null,
  PreviousDismissalReason NVARCHAR(250) null,
  PreviousSuperior NVARCHAR(250) null,
  PositionSoughtId INT null,
  MilitaryOperationsExperience NVARCHAR(10) null,
  DriversLicenseDateOfIssue DATETIME null,
  DriversLicenseCategories INT null,
  DrivingExperience INT null,
  IsReadyForBusinessTrips BIT null,
  Sports NVARCHAR(250) null,
  Hobbies NVARCHAR(250) null,
  ImportantEvents NVARCHAR(250) null,
  ChronicalDiseases NVARCHAR(250) null,
  constraint PK_BackgroundCheck  primary key (Id)
)

alter table Certification add constraint FK_Certification_Education foreign key (EducationId) references Education
create index Family_Spouse on Family (SpouseId)
create index Family_Father on Family (FatherId)
create index Family_Mother on Family (MotherId)
alter table Family add constraint FK_Family_Spouse foreign key (SpouseId) references FamilyMember
alter table Family add constraint FK_Family_Father foreign key (FatherId) references FamilyMember
alter table Family add constraint FK_Family_Mother foreign key (MotherId) references FamilyMember
create index Candidate_User on Candidate (UserId)
create index Candidate_GeneralInfo on Candidate (GeneralInfoId)
create index Candidate_Passport on Candidate (PassportId)
create index Candidate_Education on Candidate (EducationId)
create index Candidate_Family on Candidate (FamilyId)
create index Candidate_MilitaryService on Candidate (MilitaryServiceId)
create index Candidate_Experience on Candidate (ExperienceId)
create index Candidate_Contacts on Candidate (ContactsId)
create index Candidate_BackgroundCheck on Candidate (BackgroundCheckId)
create index Candidate_OnsiteTraining on Candidate (OnsiteTrainingId)
create index Candidate_Managers on Candidate (ManagersId)
create index Candidate_PersonnelManagers on Candidate (PersonnelManagersId)
alter table Candidate add constraint FK_Candidate_User foreign key (UserId) references [Users]
alter table Candidate add constraint FK_Candidate_GeneralInfo foreign key (GeneralInfoId) references GeneralInfo
alter table Candidate add constraint FK_Candidate_Passport foreign key (PassportId) references Passport
alter table Candidate add constraint FK_Candidate_Education foreign key (EducationId) references Education
alter table Candidate add constraint FK_Candidate_Family foreign key (FamilyId) references Family
alter table Candidate add constraint FK_Candidate_MilitaryService foreign key (MilitaryServiceId) references MilitaryService
alter table Candidate add constraint FK_Candidate_Experience foreign key (ExperienceId) references Experience
alter table Candidate add constraint FK_Candidate_Contacts foreign key (ContactsId) references Contacts
alter table Candidate add constraint FK_Candidate_BackgroundCheck foreign key (BackgroundCheckId) references BackgroundCheck
alter table Candidate add constraint FK_Candidate_OnsiteTraining foreign key (OnsiteTrainingId) references OnsiteTraining
alter table Candidate add constraint FK_Candidate_Managers foreign key (ManagersId) references Managers
alter table Candidate add constraint FK_Candidate_PersonnelManagers foreign key (PersonnelManagersId) references PersonnelManagers
alter table Reference add constraint FK_Reference_BackgroundCheck foreign key (BackgroundCheckId) references BackgroundCheck
alter table PostGraduateEducationDiploma add constraint FK_PostGraduateEducationDiploma_Education foreign key (EducationId) references Education
create index MilitaryService_ReserveCategory on MilitaryService (ReserveCategoryId)
create index MilitaryService_Rank on MilitaryService (RankId)
create index MilitaryService_SpecialityCategory on MilitaryService (SpecialityCategoryId)
alter table MilitaryService add constraint FK_MilitaryService_ReserveCategory foreign key (ReserveCategoryId) references MilitaryReserveCategory
alter table MilitaryService add constraint FK_MilitaryService_Rank foreign key (RankId) references MilitaryRank
alter table MilitaryService add constraint FK_MilitaryService_SpecialityCategory foreign key (SpecialityCategoryId) references MilitarySpecialityCategory
alter table HigherEducationDiploma add constraint FK_HigherEducationDiploma_Education foreign key (EducationId) references Education
create index OnsiteTraining_OnsiteTrainingType on OnsiteTraining (OnsiteTrainingTypeId)
alter table OnsiteTraining add constraint FK_OnsiteTraining_OnsiteTrainingType foreign key (OnsiteTrainingTypeId) references OnsiteTrainingType
alter table Disability add constraint FK_Disability_GeneralInfo foreign key (GeneralInfoId) references GeneralInfo
alter table Automobile add constraint FK_Automobile_BackgroundCheck foreign key (BackgroundCheckId) references BackgroundCheck
alter table FamilyMember add constraint FK_Children_Family foreign key (FamilyId) references Family
alter table ExperienceItem add constraint FK_ExperienceItem_Experience foreign key (ExperienceId) references Experience
create index GeneralInfo_Citizenship on GeneralInfo (CitizenshipId)
create index GeneralInfo_InsuredPersonType on GeneralInfo (InsuredPersonTypeId)
create index GeneralInfo_TaxpayerStatus on GeneralInfo (TaxpayerStatusId)
alter table GeneralInfo add constraint FK_GeneralInfo_Citizenship foreign key (CitizenshipId) references Country
alter table GeneralInfo add constraint FK_GeneralInfo_InsuredPersonType foreign key (InsuredPersonTypeId) references InsuredPersonType
alter table GeneralInfo add constraint FK_GeneralInfo_TaxpayerStatus foreign key (TaxpayerStatusId) references TaxpayerStatus
alter table FinancialLiability add constraint FK_Liability_BackgroundCheck foreign key (BackgroundCheckId) references BackgroundCheck
alter table NameChange add constraint FK_NameChange_GeneralInfo foreign key (GeneralInfoId) references GeneralInfo
alter table ForeignLanguage add constraint FK_ForeignLanguage_GeneralInfo foreign key (GeneralInfoId) references GeneralInfo
create index Managers_Position on Managers (PositionId)
create index Managers_Directorate on Managers (DirectorateId)
create index Managers_Department on Managers (DepartmentId)
alter table Managers add constraint FK_Managers_Position foreign key (PositionId) references Position
alter table Managers add constraint FK_Managers_Directorate foreign key (DirectorateId) references Department
alter table Managers add constraint FK_Managers_Department foreign key (DepartmentId) references Department
create index Passport_DocumentType on Passport (DocumentTypeId)
alter table Passport add constraint FK_Passport_DocumentType foreign key (DocumentTypeId) references DocumentType
alter table Training add constraint FK_Training_Education foreign key (EducationId) references Education
create index BackgroundCheck_PositionSought on BackgroundCheck (PositionSoughtId)
alter table BackgroundCheck add constraint FK_BackgroundCheck_PositionSought foreign key (PositionSoughtId) references Position
