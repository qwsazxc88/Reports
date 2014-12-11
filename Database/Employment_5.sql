alter table GeneralInfo add IsPatronymicAbsent bit not null default 0

if exists(select * from sys.columns where Name = N'InternalPassportDateOfIssue' and Object_ID = Object_ID(N'Passport'))
	alter table Passport alter column InternalPassportDateOfIssue datetime null
if exists(select * from sys.columns where Name = N'RegistrationDate' and Object_ID = Object_ID(N'Passport'))
	alter table Passport alter column RegistrationDate datetime null

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MilitaryService_Rank]') AND parent_object_id = OBJECT_ID('MilitaryService'))
	alter table MilitaryService  drop constraint FK_MilitaryService_Rank
if exists (select * from dbo.sysobjects where id = object_id(N'MilitaryRank') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table MilitaryRank

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MilitaryService_RegistrationExpiration]') AND parent_object_id = OBJECT_ID('MilitaryService'))
	alter table MilitaryService  drop constraint FK_MilitaryService_RegistrationExpiration
if exists (select * from dbo.sysobjects where id = object_id(N'MilitaryServiceRegistrationExpiration') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table MilitaryServiceRegistrationExpiration

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MilitaryService_PersonnelCategory]') AND parent_object_id = OBJECT_ID('MilitaryService'))
	alter table MilitaryService  drop constraint FK_MilitaryService_PersonnelCategory
if exists (select * from dbo.sysobjects where id = object_id(N'MilitaryPersonnelCategory') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table MilitaryPersonnelCategory

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MilitaryService_PersonnelType]') AND parent_object_id = OBJECT_ID('MilitaryService'))
	alter table MilitaryService  drop constraint FK_MilitaryService_PersonnelType
if exists (select * from dbo.sysobjects where id = object_id(N'MilitaryPersonnelType') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table MilitaryPersonnelType

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MilitaryService_ConscriptionStatus]') AND parent_object_id = OBJECT_ID('MilitaryService'))
	alter table MilitaryService  drop constraint FK_MilitaryService_ConscriptionStatus
if exists (select * from dbo.sysobjects where id = object_id(N'ConscriptionStatus') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table ConscriptionStatus

if exists(select * from sys.columns where Name = N'ReserveCategory' and Object_ID = Object_ID(N'MilitaryService'))
	alter table MilitaryService alter column ReserveCategory nvarchar(20) null
if exists(select * from sys.columns where Name = N'CombatFitness' and Object_ID = Object_ID(N'MilitaryService'))
	alter table MilitaryService alter column CombatFitness nvarchar(20) null

alter table MilitaryService add CommonMilitaryServiceRegistrationInfo nvarchar(250) null
alter table MilitaryService add SpecialMilitaryServiceRegistrationInfo nvarchar(250) null
alter table MilitaryService add IsReserved bit not null default 0
alter table MilitaryService add MobilizationTicketNumber nvarchar(20) null

if exists(select * from sys.columns where Name = N'DriversLicenseCategories' and Object_ID = Object_ID(N'BackgroundCheck'))
	alter table BackgroundCheck alter column DriversLicenseCategories nvarchar(20) null

alter table BackgroundCheck add Penalties nvarchar(250) null
alter table BackgroundCheck add PsychiatricAndAddictionTreatment nvarchar(250) null
alter table BackgroundCheck add Smoking nvarchar(250) null
alter table BackgroundCheck add Drinking nvarchar(250) null

if exists(select * from sys.columns where Name = N'BeginningDate' and Object_ID = Object_ID(N'OnsiteTraining'))
	alter table OnsiteTraining alter column BeginningDate datetime null
if exists(select * from sys.columns where Name = N'EndDate' and Object_ID = Object_ID(N'OnsiteTraining'))
	alter table OnsiteTraining alter column EndDate datetime null

alter table Managers add DailySalaryBasis DECIMAL(19, 2) null
alter table Managers add HourlySalaryBasis DECIMAL(19, 2) null
alter table Managers add SalaryMultiplier DECIMAL(3, 2) null

if exists(select * from sys.columns where Name = N'EmploymentOrderDate' and Object_ID = Object_ID(N'PersonnelManagers'))
	alter table PersonnelManagers alter column EmploymentOrderDate datetime null
if exists(select * from sys.columns where Name = N'EmploymentDate' and Object_ID = Object_ID(N'PersonnelManagers'))
	alter table PersonnelManagers alter column EmploymentDate datetime null
if exists(select * from sys.columns where Name = N'ContractDate' and Object_ID = Object_ID(N'PersonnelManagers'))
	alter table PersonnelManagers alter column ContractDate datetime null

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Disability_GeneralInfo]') AND parent_object_id = OBJECT_ID('Disability'))
	alter table Disability  drop constraint FK_Disability_GeneralInfo
if exists (select * from dbo.sysobjects where id = object_id(N'Disability') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
	drop table Disability

alter table GeneralInfo add DisabilityCertificateSeries NVARCHAR(50) null
alter table GeneralInfo add DisabilityCertificateNumber NVARCHAR(50) null
alter table GeneralInfo add DisabilityCertificateDateOfIssue DATETIME null
alter table GeneralInfo add DisabilityDegree NVARCHAR(5) null
alter table GeneralInfo add DisabilityCertificateExpirationDate DATETIME null

alter table PostGraduateEducationDiploma add IssuedBy [nvarchar](150) NULL
alter table PostGraduateEducationDiploma add Series [nvarchar](10) NULL
alter table PostGraduateEducationDiploma add Number [nvarchar](10) NULL
alter table PostGraduateEducationDiploma add AdmissionYear [nvarchar](4) NULL
alter table PostGraduateEducationDiploma add GraduationYear [nvarchar](4) NULL
alter table PostGraduateEducationDiploma add Speciality [nvarchar](50) NULL

-----------------------------------------------------------------------------------------------------------------------------------

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Family_Spouse]') AND parent_object_id = OBJECT_ID('Family'))
alter table Family  drop constraint FK_Family_Spouse

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Family_Father]') AND parent_object_id = OBJECT_ID('Family'))
alter table Family  drop constraint FK_Family_Father

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Family_Mother]') AND parent_object_id = OBJECT_ID('Family'))
alter table Family  drop constraint FK_Family_Mother

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Children_Family]') AND parent_object_id = OBJECT_ID('FamilyMember'))
alter table FamilyMember  drop constraint FK_Children_Family

alter table FamilyMember add constraint FK_FamilyMember_Family foreign key (FamilyId) references Family

if exists (select 1 from sysindexes where name = 'Family_Spouse') drop index Family_Spouse on Family
if exists (select 1 from sysindexes where name = 'Family_Father') drop index Family_Father on Family
if exists (select 1 from sysindexes where name = 'Family_Mother') drop index Family_Mother on Family

if exists(select * from sys.columns where Name = N'SpouseId' and Object_ID = Object_ID(N'Family'))
	alter table Family drop column SpouseId
if exists(select * from sys.columns where Name = N'FatherId' and Object_ID = Object_ID(N'Family'))
	alter table Family drop column FatherId
if exists(select * from sys.columns where Name = N'MotherId' and Object_ID = Object_ID(N'Family'))
	alter table Family drop column MotherId

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_FamilyMember_Relationship]') AND parent_object_id = OBJECT_ID('FamilyMember'))
	alter table FamilyMember  drop constraint FK_FamilyMember_Relationship

if exists (select * from dbo.sysobjects where id = object_id(N'FamilyRelationship') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
	drop table FamilyRelationship

if exists (select 1 from sysindexes where name = 'FamilyMember_Relationship') drop index FamilyMember_Relationship on FamilyMember