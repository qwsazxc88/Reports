alter table Family add CandidateId INT not null

--

alter table PersonnelManagers add CandidateId INT not null
alter table PersonnelManagers alter column EmploymentOrderDate DATETIME not null
alter table PersonnelManagers alter column EmploymentOrderNumber NVARCHAR(20) not null
alter table PersonnelManagers alter column EmploymentDate DATETIME not null
alter table PersonnelManagers alter column ContractDate DATETIME not null
alter table PersonnelManagers alter column ContractNumber NVARCHAR(20) not null
alter table PersonnelManagers drop column Grade
alter table PersonnelManagers add OverallExperienceYears INT not null
alter table PersonnelManagers add OverallExperienceMonths INT not null
alter table PersonnelManagers add OverallExperienceDays INT not null
alter table PersonnelManagers alter column InsurableExperienceYears INT not null
alter table PersonnelManagers alter column InsurableExperienceMonths INT not null
alter table PersonnelManagers alter column InsurableExperienceDays INT not null
alter table PersonnelManagers drop column NorthExperienceYears
alter table PersonnelManagers drop column NorthExperienceMonths
alter table PersonnelManagers drop column NorthExperienceDays
alter table PersonnelManagers alter column PersonalAccount NVARCHAR(50) not null
alter table PersonnelManagers add PersonalAccountContractor NVARCHAR(50) not null
alter table PersonnelManagers add ApprovedByPersonnelManagerId INT null
alter table PersonnelManagers drop column PreviousIncome
alter table PersonnelManagers drop column IsNonResident

--

alter table OnsiteTraining drop constraint FK_OnsiteTraining_OnsiteTrainingType
drop table OnsiteTrainingType

--

alter table Experience add CandidateId INT not null
alter table Experience alter column WorkBookSeries NVARCHAR(20) null
alter table Experience alter column WorkBookNumber NVARCHAR(20) null
alter table Experience alter column WorkBookSupplementSeries NVARCHAR(20) null
alter table Experience alter column WorkBookSupplementNumber NVARCHAR(20) null

--

drop table Candidate

--

create table MilitaryPersonnelType (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  Code NVARCHAR(10) null,
  Name NVARCHAR(128) null,
  constraint PK_MilitaryPersonnelType  primary key (Id)
)

--

alter table Contacts add CandidateId INT not null
alter table Contacts drop column Appartment
alter table Contacts add Apartment NVARCHAR(10) null
alter table Contacts alter column Email NVARCHAR(50) null

--

alter table GeneralInfo drop constraint FK_GeneralInfo_TaxpayerStatus
drop table TaxpayerStatus

--

--create table FamilyRelationship (
-- Id INT IDENTITY NOT NULL,
--  Version INT not null,
--  Code NVARCHAR(10) null,
--  Name NVARCHAR(128) null,
--  constraint PK_FamilyRelationship  primary key (Id)
--)

--

alter table MilitaryService add CandidateId INT not null
alter table MilitaryService alter column IsLiableForMilitaryService BIT not null
alter table MilitaryService drop constraint FK_MilitaryService_ReserveCategory
drop index MilitaryService_ReserveCategory on MilitaryService
alter table MilitaryService drop column ReserveCategoryId
alter table MilitaryService add ReserveCategory INT null
alter table MilitaryService drop constraint FK_MilitaryService_SpecialityCategory
drop index MilitaryService_SpecialityCategory on MilitaryService
alter table MilitaryService drop column SpecialityCategoryId
alter table MilitaryService add SpecialityCategory NVARCHAR(50) null
alter table MilitaryService alter column MilitarySpecialityCode NVARCHAR(7) null
alter table MilitaryService alter column CombatFitness NVARCHAR(1) null
alter table MilitaryService add MilitaryServiceRegistrationInfo NVARCHAR(250) null
alter table MilitaryService add RegistrationExpirationId INT null
alter table MilitaryService add PersonnelCategoryId INT null
alter table MilitaryService add PersonnelTypeId INT null
alter table MilitaryService add IsAssigned BIT not null
alter table MilitaryService add ConscriptionStatusId INT null

--

drop index OnsiteTraining_OnsiteTrainingType on OnsiteTraining
alter table OnsiteTraining drop column OnsiteTrainingTypeId
alter table OnsiteTraining add CandidateId INT not null
alter table OnsiteTraining add Type NVARCHAR(200) not null
alter table OnsiteTraining alter column Description NVARCHAR(200) not null
alter table OnsiteTraining alter column BeginningDate DATETIME not null
alter table OnsiteTraining alter column EndDate DATETIME not null
alter table OnsiteTraining alter column IsComplete BIT not null
alter table OnsiteTraining alter column ReasonsForIncompleteTraining NVARCHAR(200) null
alter table OnsiteTraining alter column Results NVARCHAR(200) null
alter table OnsiteTraining add IsConfirmed BIT not null
alter table OnsiteTraining add Comments NVARCHAR(200) null

--

drop table Automobile

--

alter table Education add CandidateId INT not null

--

create table ConscriptionStatus (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  Code NVARCHAR(10) null,
  Name NVARCHAR(128) null,
  constraint PK_ConscriptionStatus  primary key (Id)
)

--

--alter table FamilyMember add RelationshipId INT null

--

alter table GeneralInfo add CandidateId INT not null
alter table GeneralInfo alter column LastName NVARCHAR(50) not null
alter table GeneralInfo alter column FirstName NVARCHAR(50) not null
alter table GeneralInfo alter column DateOfBirth DATETIME not null
alter table GeneralInfo alter column CityOfBirth NVARCHAR(50) not null
drop index GeneralInfo_TaxpayerStatus on GeneralInfo
alter table GeneralInfo drop column TaxpayerStatusId
alter table GeneralInfo add Status INT not null
alter table GeneralInfo alter column AgreedToPersonalDataProcessing BIT not null

--

drop table FinancialLiability

--

create table MilitaryServiceRegistrationExpiration (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  Code NVARCHAR(10) null,
  Name NVARCHAR(128) null,
  constraint PK_MilitaryServiceRegistrationExpiration  primary key (Id)
)

--

alter table Managers add CandidateId INT not null
alter table Managers alter column EmploymentConditions NVARCHAR(100) null
alter table Managers alter column Schedule NVARCHAR(50) null
alter table Managers alter column ProbationaryPeriod NVARCHAR(50) null
alter table Managers drop column SalaryBasis
alter table Managers alter column WorkCity NVARCHAR(50) null
alter table Managers alter column IsFront BIT not null
alter table Managers alter column IsLiable BIT not null
alter table Managers alter column RequestNumber NVARCHAR(50) null

--

alter table Passport add CandidateId INT not null
drop index Passport_DocumentType on Passport
alter table Passport alter column DocumentTypeId INT not null
create index Passport_DocumentType on Passport (DocumentTypeId)
alter table Passport alter column InternalPassportSeries NVARCHAR(4) not null
alter table Passport alter column InternalPassportNumber NVARCHAR(6) not null
alter table Passport alter column InternalPassportDateOfIssue DATETIME not null
alter table Passport alter column InternalPassportIssuedBy NVARCHAR(150) not null
alter table Passport alter column InternalPassportSubdivisionCode NVARCHAR(7) not null
alter table Passport alter column RegistrationDate DATETIME not null
alter table Passport alter column ZipCode NVARCHAR(6) not null
alter table Passport alter column City NVARCHAR(50) not null
alter table Passport drop column Appartment
alter table Passport add Apartment NVARCHAR(5) null

--

drop table MilitaryReserveCategory

--

create table MilitaryPersonnelCategory (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  Code NVARCHAR(10) null,
  Name NVARCHAR(128) null,
  constraint PK_MilitaryPersonnelCategory  primary key (Id)
)

--

alter table BackgroundCheck add CandidateId INT not null
alter table BackgroundCheck add Liabilities NVARCHAR(250) null
alter table BackgroundCheck drop constraint FK_BackgroundCheck_PositionSought
drop index BackgroundCheck_PositionSought on BackgroundCheck
alter table BackgroundCheck drop column PositionSoughtId
alter table BackgroundCheck add PositionSought NVARCHAR(50) null
alter table BackgroundCheck alter column MilitaryOperationsExperience NVARCHAR(50) null
alter table BackgroundCheck add DriversLicenseNumber NVARCHAR(12) null
alter table BackgroundCheck add AutomobileMake NVARCHAR(50) null
alter table BackgroundCheck add AutomobileLicensePlateNumber NVARCHAR(15) null
alter table BackgroundCheck alter column IsReadyForBusinessTrips BIT not null

--

create table EmploymentCandidate (
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
  constraint PK_EmploymentCandidate  primary key (Id)
)

--

create index Family_Candidate on Family (CandidateId)
alter table Family add constraint FK_Family_Candidate foreign key (CandidateId) references EmploymentCandidate
create index PersonnelManagers_Candidate on PersonnelManagers (CandidateId)
create index IX_PersonnelManagers_ApprovedByPersonnelManagerUser_Id on PersonnelManagers (ApprovedByPersonnelManagerId)
alter table PersonnelManagers add constraint FK_PersonnelManagers_Candidate foreign key (CandidateId) references EmploymentCandidate
alter table PersonnelManagers add constraint FK_PersonnelManagers_ApprovedByPersonnelManagerUser foreign key (ApprovedByPersonnelManagerId) references [Users]
create index Experience_Candidate on Experience (CandidateId)
alter table Experience add constraint FK_Experience_Candidate foreign key (CandidateId) references EmploymentCandidate
create index Contacts_Candidate on Contacts (CandidateId)
alter table Contacts add constraint FK_Contacts_Candidate foreign key (CandidateId) references EmploymentCandidate
create index MilitaryService_Candidate on MilitaryService (CandidateId)
create index MilitaryService_RegistrationExpiration on MilitaryService (RegistrationExpirationId)
create index MilitaryService_PersonnelCategory on MilitaryService (PersonnelCategoryId)
create index MilitaryService_PersonnelType on MilitaryService (PersonnelTypeId)
create index MilitaryService_ConscriptionStatus on MilitaryService (ConscriptionStatusId)
alter table MilitaryService add constraint FK_MilitaryService_Candidate foreign key (CandidateId) references EmploymentCandidate
alter table MilitaryService add constraint FK_MilitaryService_RegistrationExpiration foreign key (RegistrationExpirationId) references MilitaryServiceRegistrationExpiration
alter table MilitaryService add constraint FK_MilitaryService_PersonnelCategory foreign key (PersonnelCategoryId) references MilitaryPersonnelCategory
alter table MilitaryService add constraint FK_MilitaryService_PersonnelType foreign key (PersonnelTypeId) references MilitaryPersonnelType
alter table MilitaryService add constraint FK_MilitaryService_ConscriptionStatus foreign key (ConscriptionStatusId) references ConscriptionStatus
create index OnsiteTraining_Candidate on OnsiteTraining (CandidateId)
alter table OnsiteTraining add constraint FK_OnsiteTraining_Candidate foreign key (CandidateId) references EmploymentCandidate
--create index Education_Candidate on Education (CandidateId)
alter table Education add constraint FK_Education_Candidate foreign key (CandidateId) references EmploymentCandidate
--create index FamilyMember_Relationship on FamilyMember (RelationshipId)
--alter table FamilyMember add constraint FK_FamilyMember_Relationship foreign key (RelationshipId) references FamilyRelationship
create index GeneralInfo_Candidate on GeneralInfo (CandidateId)
alter table GeneralInfo add constraint FK_GeneralInfo_Candidate foreign key (CandidateId) references EmploymentCandidate
create index Managers_Candidate on Managers (CandidateId)
alter table Managers add constraint FK_Managers_Candidate foreign key (CandidateId) references EmploymentCandidate
create index Passport_Candidate on Passport (CandidateId)
alter table Passport add constraint FK_Passport_Candidate foreign key (CandidateId) references EmploymentCandidate
create index BackgroundCheck_Candidate on BackgroundCheck (CandidateId)
alter table BackgroundCheck add constraint FK_BackgroundCheck_Candidate foreign key (CandidateId) references EmploymentCandidate
create index Candidate_User on EmploymentCandidate (UserId)
create index Candidate_GeneralInfo on EmploymentCandidate (GeneralInfoId)
create index Candidate_Passport on EmploymentCandidate (PassportId)
create index Candidate_Education on EmploymentCandidate (EducationId)
create index Candidate_Family on EmploymentCandidate (FamilyId)
create index Candidate_MilitaryService on EmploymentCandidate (MilitaryServiceId)
create index Candidate_Experience on EmploymentCandidate (ExperienceId)
create index Candidate_Contacts on EmploymentCandidate (ContactsId)
create index Candidate_BackgroundCheck on EmploymentCandidate (BackgroundCheckId)
create index Candidate_OnsiteTraining on EmploymentCandidate (OnsiteTrainingId)
create index Candidate_Managers on EmploymentCandidate (ManagersId)
create index Candidate_PersonnelManagers on EmploymentCandidate (PersonnelManagersId)
alter table EmploymentCandidate add constraint FK_Candidate_User foreign key (UserId) references [Users]
alter table EmploymentCandidate add constraint FK_Candidate_GeneralInfo foreign key (GeneralInfoId) references GeneralInfo
alter table EmploymentCandidate add constraint FK_Candidate_Passport foreign key (PassportId) references Passport
alter table EmploymentCandidate add constraint FK_Candidate_Education foreign key (EducationId) references Education
alter table EmploymentCandidate add constraint FK_Candidate_Family foreign key (FamilyId) references Family
alter table EmploymentCandidate add constraint FK_Candidate_MilitaryService foreign key (MilitaryServiceId) references MilitaryService
alter table EmploymentCandidate add constraint FK_Candidate_Experience foreign key (ExperienceId) references Experience
alter table EmploymentCandidate add constraint FK_Candidate_Contacts foreign key (ContactsId) references Contacts
alter table EmploymentCandidate add constraint FK_Candidate_BackgroundCheck foreign key (BackgroundCheckId) references BackgroundCheck
alter table EmploymentCandidate add constraint FK_Candidate_OnsiteTraining foreign key (OnsiteTrainingId) references OnsiteTraining
alter table EmploymentCandidate add constraint FK_Candidate_Managers foreign key (ManagersId) references Managers
alter table EmploymentCandidate add constraint FK_Candidate_PersonnelManagers foreign key (PersonnelManagersId) references PersonnelManagers
