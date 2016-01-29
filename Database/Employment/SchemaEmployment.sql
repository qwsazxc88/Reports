--на данный момент времени уже не соответствует действительности
--скрипт создания объектов базы данных для системы приема и занесения первичных данных в справочники
USE WebAppSKB
GO
--структура базы данных раздела "ПРИЕМ"
--EmploymentCandidate
	--EmploymentCandidateDocNeeded
	--GeneralInfo
		--NameChange
		--Country	 (есть данные) (убрать Франция, Израиль, Туркменистан, Узбекистан)
		--ForeignLanguage
		--DisabilityDegree (есть данные)
	--Passport
		--DocumentType (есть данные)
	--Education
		--Certification
		--HigherEducationDiploma
			--EmploymentEducationType	 (есть данные) (убрать Дошкольное образование, неполное высшее образование)
		--PostGraduateEducationDiploma
		--Training
	--Family
		--FamilyMember
		--FamilyStatus (есть данные)
	--MilitaryService
		--MilitaryRanks (есть данные)
		--MilitaryRelationAccount (есть данные)
		--MilitarySpecialityCategory (есть данные)
		--MilitaryValidityCategory (есть данные)
	--Experience
		--ExperienceItem
	--Contacts
	--BackgroundCheck
		--Reference
	--OnsiteTraining
	--Managers
	--PersonnelManagers
		--AccessGroup (есть данные)
		--InsuredPersonType (есть данные)
		--PersonalAccountContractor (есть данные)
		--Signer
		--Schedule (есть данные)
		--SupplementaryAgreement

--1. УДАЛЕНИЕ ССЫЛОК
	IF OBJECT_ID ('FK_EmploymentCandidate_TKMarkUser', 'F') IS NOT NULL
		ALTER TABLE [dbo].[EmploymentCandidate] DROP CONSTRAINT [FK_EmploymentCandidate_TKMarkUser]
	GO

	IF OBJECT_ID ('FK_EmploymentCandidate_TDMarkUser', 'F') IS NOT NULL
		ALTER TABLE [dbo].[EmploymentCandidate] DROP CONSTRAINT [FK_EmploymentCandidate_TDMarkUser]
	GO

	IF OBJECT_ID ('FK_EmploymentCandidate_Users', 'F') IS NOT NULL
		ALTER TABLE [dbo].[EmploymentCandidate] DROP CONSTRAINT [FK_EmploymentCandidate_Users]
	GO

	IF OBJECT_ID ('FK_EmploymentCandidate_AppointmentReport', 'F') IS NOT NULL
		ALTER TABLE [dbo].[EmploymentCandidate] DROP CONSTRAINT [FK_EmploymentCandidate_AppointmentReport]
	GO

	IF OBJECT_ID ('FK_EmploymentCandidate_Appointment', 'F') IS NOT NULL
		ALTER TABLE [dbo].[EmploymentCandidate] DROP CONSTRAINT [FK_EmploymentCandidate_Appointment]
	GO

	IF OBJECT_ID ('FK_Candidate_User', 'F') IS NOT NULL
		ALTER TABLE [dbo].[EmploymentCandidate] DROP CONSTRAINT [FK_Candidate_User]
	GO

	IF OBJECT_ID ('FK_Candidate_PersonnelManagers', 'F') IS NOT NULL
		ALTER TABLE [dbo].[EmploymentCandidate] DROP CONSTRAINT [FK_Candidate_PersonnelManagers]
	GO

	IF OBJECT_ID ('FK_Candidate_Passport', 'F') IS NOT NULL
		ALTER TABLE [dbo].[EmploymentCandidate] DROP CONSTRAINT [FK_Candidate_Passport]
	GO

	IF OBJECT_ID ('FK_Candidate_OnsiteTraining', 'F') IS NOT NULL
		ALTER TABLE [dbo].[EmploymentCandidate] DROP CONSTRAINT [FK_Candidate_OnsiteTraining]
	GO

	IF OBJECT_ID ('FK_Candidate_MilitaryService', 'F') IS NOT NULL
		ALTER TABLE [dbo].[EmploymentCandidate] DROP CONSTRAINT [FK_Candidate_MilitaryService]
	GO

	IF OBJECT_ID ('FK_Candidate_Managers', 'F') IS NOT NULL
		ALTER TABLE [dbo].[EmploymentCandidate] DROP CONSTRAINT [FK_Candidate_Managers]
	GO

	IF OBJECT_ID ('FK_Candidate_GeneralInfo', 'F') IS NOT NULL
		ALTER TABLE [dbo].[EmploymentCandidate] DROP CONSTRAINT [FK_Candidate_GeneralInfo]
	GO

	IF OBJECT_ID ('FK_Candidate_Family', 'F') IS NOT NULL
		ALTER TABLE [dbo].[EmploymentCandidate] DROP CONSTRAINT [FK_Candidate_Family]
	GO

	IF OBJECT_ID ('FK_Candidate_Experience', 'F') IS NOT NULL
		ALTER TABLE [dbo].[EmploymentCandidate] DROP CONSTRAINT [FK_Candidate_Experience]
	GO

	IF OBJECT_ID ('FK_Candidate_Education', 'F') IS NOT NULL
		ALTER TABLE [dbo].[EmploymentCandidate] DROP CONSTRAINT [FK_Candidate_Education]
	GO

	IF OBJECT_ID ('FK_Candidate_Contacts', 'F') IS NOT NULL
		ALTER TABLE [dbo].[EmploymentCandidate] DROP CONSTRAINT [FK_Candidate_Contacts]
	GO

	IF OBJECT_ID ('FK_Candidate_BackgroundCheck', 'F') IS NOT NULL
		ALTER TABLE [dbo].[EmploymentCandidate] DROP CONSTRAINT [FK_Candidate_BackgroundCheck]
	GO

	IF OBJECT_ID ('FK_Candidate_AppointmentCreator', 'F') IS NOT NULL
		ALTER TABLE [dbo].[EmploymentCandidate] DROP CONSTRAINT [FK_Candidate_AppointmentCreator]
	GO

	IF OBJECT_ID ('FK_EmploymentCandidateDocNeeded_Users1', 'F') IS NOT NULL
		ALTER TABLE [dbo].[EmploymentCandidateDocNeeded] DROP CONSTRAINT [FK_EmploymentCandidateDocNeeded_Users1]
	GO

	IF OBJECT_ID ('FK_EmploymentCandidateDocNeeded_Users', 'F') IS NOT NULL
		ALTER TABLE [dbo].[EmploymentCandidateDocNeeded] DROP CONSTRAINT [FK_EmploymentCandidateDocNeeded_Users]
	GO

	IF OBJECT_ID ('FK_EmploymentCandidateDocNeeded_EmploymentCandidate', 'F') IS NOT NULL
		ALTER TABLE [dbo].[EmploymentCandidateDocNeeded] DROP CONSTRAINT [FK_EmploymentCandidateDocNeeded_EmploymentCandidate]
	GO

	IF OBJECT_ID ('FK_GeneralInfo_DisabilityDegree', 'F') IS NOT NULL
		ALTER TABLE [dbo].[GeneralInfo] DROP CONSTRAINT [FK_GeneralInfo_DisabilityDegree]
	GO

	IF OBJECT_ID ('FK_GeneralInfo_Country', 'F') IS NOT NULL
		ALTER TABLE [dbo].[GeneralInfo] DROP CONSTRAINT [FK_GeneralInfo_Country]
	GO

	IF OBJECT_ID ('FK_GeneralInfo_Citizenship', 'F') IS NOT NULL
		ALTER TABLE [dbo].[GeneralInfo] DROP CONSTRAINT [FK_GeneralInfo_Citizenship]
	GO

	IF OBJECT_ID ('FK_GeneralInfo_Candidate', 'F') IS NOT NULL
		ALTER TABLE [dbo].[GeneralInfo] DROP CONSTRAINT [FK_GeneralInfo_Candidate]
	GO

	IF OBJECT_ID ('FK_NameChange_GeneralInfo', 'F') IS NOT NULL
		ALTER TABLE [dbo].[NameChange] DROP CONSTRAINT [FK_NameChange_GeneralInfo]
	GO

	IF OBJECT_ID ('FK_ForeignLanguage_GeneralInfo', 'F') IS NOT NULL
		ALTER TABLE [dbo].[ForeignLanguage] DROP CONSTRAINT [FK_ForeignLanguage_GeneralInfo]
	GO

	IF OBJECT_ID ('FK_Passport_DocumentType', 'F') IS NOT NULL
		ALTER TABLE [dbo].[Passport] DROP CONSTRAINT [FK_Passport_DocumentType]
	GO

	IF OBJECT_ID ('FK_Passport_Candidate', 'F') IS NOT NULL
		ALTER TABLE [dbo].[Passport] DROP CONSTRAINT [FK_Passport_Candidate]
	GO

	IF OBJECT_ID ('FK_Education_Candidate', 'F') IS NOT NULL
		ALTER TABLE [dbo].[Education] DROP CONSTRAINT [FK_Education_Candidate]
	GO

	IF OBJECT_ID ('FK_Certification_Education', 'F') IS NOT NULL
		ALTER TABLE [dbo].[Certification] DROP CONSTRAINT [FK_Certification_Education]
	GO

	IF OBJECT_ID ('FK_HigherEducationDiploma_EmploymentEducationType', 'F') IS NOT NULL
		ALTER TABLE [dbo].[HigherEducationDiploma] DROP CONSTRAINT [FK_HigherEducationDiploma_EmploymentEducationType]
	GO

	IF OBJECT_ID ('FK_HigherEducationDiploma_Education', 'F') IS NOT NULL
		ALTER TABLE [dbo].[HigherEducationDiploma] DROP CONSTRAINT [FK_HigherEducationDiploma_Education]
	GO

	IF OBJECT_ID ('FK_PostGraduateEducationDiploma_Education', 'F') IS NOT NULL
		ALTER TABLE [dbo].[PostGraduateEducationDiploma] DROP CONSTRAINT [FK_PostGraduateEducationDiploma_Education]
	GO

	IF OBJECT_ID ('FK_Training_Education', 'F') IS NOT NULL
		ALTER TABLE [dbo].[Training] DROP CONSTRAINT [FK_Training_Education]
	GO

	IF OBJECT_ID ('FK_Family_FamilyStatus', 'F') IS NOT NULL
		ALTER TABLE [dbo].[Family] DROP CONSTRAINT [FK_Family_FamilyStatus]
	GO

	IF OBJECT_ID ('FK_Family_Candidate', 'F') IS NOT NULL
		ALTER TABLE [dbo].[Family] DROP CONSTRAINT [FK_Family_Candidate]
	GO

	IF OBJECT_ID ('FK_FamilyMember_Family', 'F') IS NOT NULL
		ALTER TABLE [dbo].[FamilyMember] DROP CONSTRAINT [FK_FamilyMember_Family]
	GO

	IF OBJECT_ID ('FK_MilitaryService_MilitaryValidityCategory', 'F') IS NOT NULL
		ALTER TABLE [dbo].[MilitaryService] DROP CONSTRAINT [FK_MilitaryService_MilitaryValidityCategory]
	GO

	IF OBJECT_ID ('FK_MilitaryService_MilitarySpecialityCategory', 'F') IS NOT NULL
		ALTER TABLE [dbo].[MilitaryService] DROP CONSTRAINT [FK_MilitaryService_MilitarySpecialityCategory]
	GO

	IF OBJECT_ID ('FK_MilitaryService_MilitaryRelationAccount', 'F') IS NOT NULL
		ALTER TABLE [dbo].[MilitaryService] DROP CONSTRAINT [FK_MilitaryService_MilitaryRelationAccount]
	GO

	IF OBJECT_ID ('FK_MilitaryService_MilitaryRanks', 'F') IS NOT NULL
		ALTER TABLE [dbo].[MilitaryService] DROP CONSTRAINT [FK_MilitaryService_MilitaryRanks]
	GO

	IF OBJECT_ID ('FK_MilitaryService_Candidate', 'F') IS NOT NULL
		ALTER TABLE [dbo].[MilitaryService] DROP CONSTRAINT [FK_MilitaryService_Candidate]
	GO

	IF OBJECT_ID ('FK_Experience_Candidate', 'F') IS NOT NULL
		ALTER TABLE [dbo].[Experience] DROP CONSTRAINT [FK_Experience_Candidate]
	GO

	IF OBJECT_ID ('FK_ExperienceItem_Experience', 'F') IS NOT NULL
		ALTER TABLE [dbo].[ExperienceItem] DROP CONSTRAINT [FK_ExperienceItem_Experience]
	GO

	IF OBJECT_ID ('FK_Contacts_Candidate', 'F') IS NOT NULL
		ALTER TABLE [dbo].[Contacts] DROP CONSTRAINT [FK_Contacts_Candidate]
	GO

	IF OBJECT_ID ('FK_BackgroundCheck_PrevApprover', 'F') IS NOT NULL
		ALTER TABLE [dbo].[BackgroundCheck] DROP CONSTRAINT [FK_BackgroundCheck_PrevApprover]
	GO

	IF OBJECT_ID ('FK_BackgroundCheck_PrevApprover', 'F') IS NOT NULL
		ALTER TABLE [dbo].[BackgroundCheck] DROP CONSTRAINT [FK_BackgroundCheck_PrevApprover]
	GO

	IF OBJECT_ID ('FK_BackgroundCheck_CancelReject', 'F') IS NOT NULL
		ALTER TABLE [dbo].[BackgroundCheck] DROP CONSTRAINT [FK_BackgroundCheck_CancelReject]
	GO

	IF OBJECT_ID ('FK_BackgroundCheck_Approver', 'F') IS NOT NULL
		ALTER TABLE [dbo].[BackgroundCheck] DROP CONSTRAINT [FK_BackgroundCheck_Approver]
	GO

	IF OBJECT_ID ('FK_Reference_BackgroundCheck', 'F') IS NOT NULL
		ALTER TABLE [dbo].[Reference] DROP CONSTRAINT [FK_Reference_BackgroundCheck]
	GO

	IF OBJECT_ID ('FK_OnsiteTraining_Candidate', 'F') IS NOT NULL
		ALTER TABLE [dbo].[OnsiteTraining] DROP CONSTRAINT [FK_OnsiteTraining_Candidate]
	GO

	IF OBJECT_ID ('FK_OnsiteTraining_Approver', 'F') IS NOT NULL
		ALTER TABLE [dbo].[OnsiteTraining] DROP CONSTRAINT [FK_OnsiteTraining_Approver]
	GO

	IF OBJECT_ID ('FK_Managers_RejectingChief', 'F') IS NOT NULL
		ALTER TABLE [dbo].[Managers] DROP CONSTRAINT [FK_Managers_RejectingChief]
	GO

	IF OBJECT_ID ('FK_Managers_Position', 'F') IS NOT NULL
		ALTER TABLE [dbo].[Managers] DROP CONSTRAINT [FK_Managers_Position]
	GO

	IF OBJECT_ID ('FK_Managers_Department', 'F') IS NOT NULL
		ALTER TABLE [dbo].[Managers] DROP CONSTRAINT [FK_Managers_Department]
	GO

	IF OBJECT_ID ('FK_Managers_Candidate', 'F') IS NOT NULL
		ALTER TABLE [dbo].[Managers] DROP CONSTRAINT [FK_Managers_Candidate]
	GO

	IF OBJECT_ID ('FK_Managers_CancelRejectUser', 'F') IS NOT NULL
		ALTER TABLE [dbo].[Managers] DROP CONSTRAINT [FK_Managers_CancelRejectUser]
	GO

	IF OBJECT_ID ('FK_Managers_CancelRejectHigherUser', 'F') IS NOT NULL
		ALTER TABLE [dbo].[Managers] DROP CONSTRAINT [FK_Managers_CancelRejectHigherUser]
	GO

	IF OBJECT_ID ('FK_Managers_ApprovingManager', 'F') IS NOT NULL
		ALTER TABLE [dbo].[Managers] DROP CONSTRAINT [FK_Managers_ApprovingManager]
	GO

	IF OBJECT_ID ('FK_Managers_ApprovingHigherManager', 'F') IS NOT NULL
		ALTER TABLE [dbo].[Managers] DROP CONSTRAINT [FK_Managers_ApprovingHigherManager]
	GO

	IF OBJECT_ID ('FK_PersonnelManagers_Users', 'F') IS NOT NULL
		ALTER TABLE [dbo].[PersonnelManagers] DROP CONSTRAINT [FK_PersonnelManagers_Users]
	GO

	IF OBJECT_ID ('FK_PersonnelManagers_Signer', 'F') IS NOT NULL
		ALTER TABLE [dbo].[PersonnelManagers] DROP CONSTRAINT [FK_PersonnelManagers_Signer]
	GO

	IF OBJECT_ID ('FK_PersonnelManagers_Schedule', 'F') IS NOT NULL
		ALTER TABLE [dbo].[PersonnelManagers] DROP CONSTRAINT [FK_PersonnelManagers_Schedule]
	GO

	IF OBJECT_ID ('FK_PersonnelManagers_PersonnelOrderExtraCharges', 'F') IS NOT NULL
		ALTER TABLE [dbo].[PersonnelManagers] DROP CONSTRAINT [FK_PersonnelManagers_PersonnelOrderExtraCharges]
	GO

	IF OBJECT_ID ('FK_PersonnelManagers_PersonalAccountContractor', 'F') IS NOT NULL
		ALTER TABLE [dbo].[PersonnelManagers] DROP CONSTRAINT [FK_PersonnelManagers_PersonalAccountContractor]
	GO

	IF OBJECT_ID ('FK_PersonnelManagers_InsuredPersonType', 'F') IS NOT NULL
		ALTER TABLE [dbo].[PersonnelManagers] DROP CONSTRAINT [FK_PersonnelManagers_InsuredPersonType]
	GO

	IF OBJECT_ID ('FK_PersonnelManagers_Candidate', 'F') IS NOT NULL
		ALTER TABLE [dbo].[PersonnelManagers] DROP CONSTRAINT [FK_PersonnelManagers_Candidate]
	GO

	IF OBJECT_ID ('FK_PersonnelManagers_ApprovedByPersonnelManagerUser', 'F') IS NOT NULL
		ALTER TABLE [dbo].[PersonnelManagers] DROP CONSTRAINT [FK_PersonnelManagers_ApprovedByPersonnelManagerUser]
	GO

	IF OBJECT_ID ('FK_PersonnelManagers_AccessGroup', 'F') IS NOT NULL
		ALTER TABLE [dbo].[PersonnelManagers] DROP CONSTRAINT [FK_PersonnelManagers_AccessGroup]
	GO

	IF OBJECT_ID ('FK_SupplementaryAgreement_PersonnelManagers', 'F') IS NOT NULL
		ALTER TABLE [dbo].[SupplementaryAgreement] DROP CONSTRAINT [FK_SupplementaryAgreement_PersonnelManagers]
	GO


--2. ТАБЛИЦЫ
	if OBJECT_ID (N'EmploymentCandidate', 'U') is not null
		DROP TABLE [dbo].[EmploymentCandidate]
	GO
	CREATE TABLE [dbo].[EmploymentCandidate](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[Version] [int] NOT NULL,
		[UserId] [int] NULL,
		[GeneralInfoId] [int] NULL,
		[PassportId] [int] NULL,
		[EducationId] [int] NULL,
		[FamilyId] [int] NULL,
		[MilitaryServiceId] [int] NULL,
		[ExperienceId] [int] NULL,
		[ContactsId] [int] NULL,
		[BackgroundCheckId] [int] NULL,
		[OnsiteTrainingId] [int] NULL,
		[ManagersId] [int] NULL,
		[PersonnelManagersId] [int] NULL,
		[Status] [int] NULL,
		[ContractNumber1C] [nvarchar](20) NULL,
		[SendTo1C] [datetime] NULL,
		[QuestionnaireDate] [datetime] NULL,
		[AppointmentCreatorId] [int] NULL,
		[PersonnelId] [int] NULL,
		[IsTrainingNeeded] [bit] NULL CONSTRAINT [DF_EmploymentCandidate_IsTrainingNeeded]  DEFAULT ((0)),
		[IsBeforEmployment] [bit] NULL CONSTRAINT [DF_EmploymentCandidate_IsBeforEmployment]  DEFAULT ((0)),
		[IsCandidateToBackgroundPrevSendEmail] [bit] NULL CONSTRAINT [DF_EmploymentCandidate_IsCandidateToBackgroundSendEmail1]  DEFAULT ((0)),
		[CandidateToBackgroundPrevSendEmailDate] [datetime] NULL,
		[IsCandidateToBackgroundSendEmail] [bit] NULL CONSTRAINT [DF_EmploymentCandidate_IsCandidateToBackground]  DEFAULT ((0)),
		[CandidateToBackgroundSendEmailDate] [datetime] NULL,
		[IsCandidateToManagerSendEmail] [bit] NULL CONSTRAINT [DF_EmploymentCandidate_IsCandidateToManagerSendEmail]  DEFAULT ((0)),
		[CandidateToManagerSendEmailDate] [datetime] NULL,
		[IsBackgroundToManagerSendEmail] [bit] NULL CONSTRAINT [DF_EmploymentCandidate_IsBackgroundToManagerSendEmail]  DEFAULT ((0)),
		[BackgroundToManagerSendEmailDate] [datetime] NULL,
		[IsManagerToTrainingSendEmail] [bit] NULL CONSTRAINT [DF_EmploymentCandidate_IsManagerToTrainingSendEmail]  DEFAULT ((0)),
		[ManagerToTrainingSendEmailDate] [datetime] NULL,
		[IsManagerToHigherManagerSendEmail] [bit] NULL CONSTRAINT [DF_EmploymentCandidate_IsManagerToHigherManagerSendEmail]  DEFAULT ((0)),
		[ManagerToHigherManagerSendEmailDate] [datetime] NULL,
		[IsPersonnelManagerToManagerSendEmail] [bit] NULL CONSTRAINT [DF_EmploymentCandidate_IsPersonnelManagerToManagerSendEmail_1]  DEFAULT ((0)),
		[PersonnelManagerToManagerSendEmailDate] [datetime] NULL,
		[AppointmentReportId] [int] NULL,
		[AppointmentId] [int] NULL,
		[IsTechDissmiss] [bit] NULL CONSTRAINT [DF_EmploymentCandidate_IsTechDissmiss]  DEFAULT ((0)),
		[IsScanFinal] [bit] NULL CONSTRAINT [DF_EmploymentCandidate_IsScanFinal]  DEFAULT ((0)),
		[IsTKReceived] [bit] NULL CONSTRAINT [DF_EmploymentCandidate_IsTKReceived]  DEFAULT ((0)),
		[TKReceivedDate] [datetime] NULL,
		[TKMarkUserId] [int] NULL,
		[IsTDReceived] [bit] NULL CONSTRAINT [DF_EmploymentCandidate_IsTDReceived]  DEFAULT ((0)),
		[TDReceivedDate] [datetime] NULL,
		[TDMarkUserId] [int] NULL,
	 CONSTRAINT [PK_EmploymentCandidate] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]

	GO


	if OBJECT_ID (N'EmploymentCandidateDocNeeded', 'U') is not null
		DROP TABLE [dbo].[EmploymentCandidateDocNeeded]
	GO
	CREATE TABLE [dbo].[EmploymentCandidateDocNeeded](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[Version] [int] NOT NULL CONSTRAINT [DF_EmploymentCandidateDocNeeded_Version]  DEFAULT ((1)),
		[CandidateId] [int] NULL,
		[DocTypeId] [int] NULL,
		[IsNeeded] [bit] NULL CONSTRAINT [DF_EmploymentCandidateDocNeeded_IsNeeded]  DEFAULT ((0)),
		[DateCreate] [datetime] NULL,
		[CreatorId] [int] NULL,
		[DateEdit] [datetime] NULL,
		[EditorId] [int] NULL,
	 CONSTRAINT [PK_EmploymentCandidateDocNeeded] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]

	GO


	if OBJECT_ID (N'GeneralInfo', 'U') is not null
		DROP TABLE [dbo].[GeneralInfo]
	GO
	CREATE TABLE [dbo].[GeneralInfo](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[Version] [int] NOT NULL,
		[LastName] [nvarchar](50) NULL,
		[FirstName] [nvarchar](50) NULL,
		[Patronymic] [nvarchar](50) NULL,
		[IsMale] [bit] NULL,
		[CitizenshipId] [int] NULL,
		[DateOfBirth] [datetime] NULL,
		[RegionOfBirth] [nvarchar](50) NULL,
		[DistrictOfBirth] [nvarchar](50) NULL,
		[CityOfBirth] [nvarchar](50) NULL,
		[INN] [nvarchar](12) NULL,
		[SNILS] [nvarchar](14) NULL,
		[AgreedToPersonalDataProcessing] [bit] NOT NULL,
		[CandidateId] [int] NOT NULL,
		[IsPatronymicAbsent] [bit] NOT NULL CONSTRAINT [DF__GeneralIn__IsPat__2B2B2C55]  DEFAULT ((0)),
		[DisabilityCertificateSeries] [nvarchar](20) NULL,
		[DisabilityCertificateNumber] [nvarchar](20) NULL,
		[DisabilityCertificateDateOfIssue] [datetime] NULL,
		[DisabilityCertificateExpirationDate] [datetime] NULL,
		[IsDisabilityTermLess] [bit] NULL CONSTRAINT [DF_GeneralInfo_IsDisabilityTermLess]  DEFAULT ((0)),
		[IsFinal] [bit] NOT NULL CONSTRAINT [DF__GeneralIn__IsFin__3791033A]  DEFAULT ((0)),
		[DisabilityDegreeId] [int] NULL,
		[IsValidate] [bit] NULL CONSTRAINT [DF_GeneralInfo_IsValidate]  DEFAULT ((0)),
		[CountryBirthId] [int] NULL,
	 CONSTRAINT [PK_GeneralInfo] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]

	GO


	if OBJECT_ID (N'NameChange', 'U') is not null
		DROP TABLE [dbo].[NameChange]
	GO
	CREATE TABLE [dbo].[NameChange](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[Version] [int] NOT NULL,
		[PreviousName] [nvarchar](50) NULL,
		[Date] [datetime] NULL,
		[Place] [nvarchar](50) NULL,
		[Reason] [nvarchar](50) NULL,
		[GeneralInfoId] [int] NULL,
	 CONSTRAINT [PK_NameChange] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]

	GO


	if OBJECT_ID (N'Country', 'U') is not null
		DROP TABLE [dbo].[Country]
	GO
	CREATE TABLE [dbo].[Country](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[Version] [int] NOT NULL,
		[Code] [nvarchar](10) NULL,
		[Name] [nvarchar](128) NULL,
	 CONSTRAINT [PK_Country] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]

	GO


	if OBJECT_ID (N'ForeignLanguage', 'U') is not null
		DROP TABLE [dbo].[ForeignLanguage]
	GO
	CREATE TABLE [dbo].[ForeignLanguage](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[Version] [int] NOT NULL,
		[LanguageName] [nvarchar](50) NULL,
		[Level] [nvarchar](20) NULL,
		[GeneralInfoId] [int] NULL,
	 CONSTRAINT [PK_ForeignLanguage] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]

	GO


	if OBJECT_ID (N'DisabilityDegree', 'U') is not null
		DROP TABLE [dbo].[DisabilityDegree]
	GO
	CREATE TABLE [dbo].[DisabilityDegree](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[Version] [int] NOT NULL,
		[Code] [nvarchar](10) NULL,
		[Name] [nvarchar](128) NULL,
	 CONSTRAINT [PK_DisabilityDegree] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]

	GO


	if OBJECT_ID (N'Passport', 'U') is not null
		DROP TABLE [dbo].[Passport]
	GO
	CREATE TABLE [dbo].[Passport](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[Version] [int] NOT NULL,
		[DocumentTypeId] [int] NULL,
		[InternalPassportSeries] [nvarchar](5) NULL,
		[InternalPassportNumber] [nvarchar](6) NULL,
		[InternalPassportDateOfIssue] [datetime] NULL,
		[InternalPassportIssuedBy] [nvarchar](250) NULL,
		[InternalPassportSubdivisionCode] [nvarchar](15) NULL,
		[RegistrationDate] [datetime] NULL,
		[ZipCode] [nvarchar](6) NULL,
		[Region] [nvarchar](50) NULL,
		[District] [nvarchar](50) NULL,
		[City] [nvarchar](50) NULL,
		[Street] [nvarchar](50) NULL,
		[StreetNumber] [nvarchar](6) NULL,
		[Building] [nvarchar](3) NULL,
		[InternationalPassportSeries] [nvarchar](10) NULL,
		[InternationalPassportNumber] [nvarchar](10) NULL,
		[InternationalPassportDateOfIssue] [datetime] NULL,
		[InternationalPassportIssuedBy] [nvarchar](150) NULL,
		[CandidateId] [int] NOT NULL,
		[Apartment] [nvarchar](5) NULL,
		[IsFinal] [bit] NOT NULL CONSTRAINT [DF__Passport__IsFina__38852773]  DEFAULT ((0)),
		[IsValidate] [bit] NULL CONSTRAINT [DF_Passport_IsValidate]  DEFAULT ((0)),
	 CONSTRAINT [PK_Passport] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]

	GO


	if OBJECT_ID (N'DocumentType', 'U') is not null
		DROP TABLE [dbo].[DocumentType]
	GO
	CREATE TABLE [dbo].[DocumentType](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[Version] [int] NOT NULL,
		[Code] [nvarchar](10) NULL,
		[Name] [nvarchar](128) NULL,
	 CONSTRAINT [PK_DocumentType] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]

	GO


	if OBJECT_ID (N'Education', 'U') is not null
		DROP TABLE [dbo].[Education]
	GO
	CREATE TABLE [dbo].[Education](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[Version] [int] NOT NULL,
		[CandidateId] [int] NOT NULL,
		[IsFinal] [bit] NOT NULL CONSTRAINT [DF__Education__IsFin__5D227A9C]  DEFAULT ((0)),
		[IsValidate] [bit] NULL CONSTRAINT [DF_Education_IsValidate]  DEFAULT ((0)),
	 CONSTRAINT [PK_Education] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]

	GO


	if OBJECT_ID (N'Certification', 'U') is not null
		DROP TABLE [dbo].[Certification]
	GO
	CREATE TABLE [dbo].[Certification](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[Version] [int] NOT NULL,
		[CertificationDate] [datetime] NULL,
		[CertificateNumber] [nvarchar](20) NULL,
		[CertificateDateOfIssue] [datetime] NULL,
		[InitiatingOrder] [nvarchar](20) NULL,
		[EducationId] [int] NULL,
		[LocationEI] [nvarchar](150) NULL,
	 CONSTRAINT [PK_Certification] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]

	GO


	if OBJECT_ID (N'HigherEducationDiploma', 'U') is not null
		DROP TABLE [dbo].[HigherEducationDiploma]
	GO
	CREATE TABLE [dbo].[HigherEducationDiploma](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[Version] [int] NOT NULL,
		[EducationTypeId] [int] NULL,
		[IssuedBy] [nvarchar](150) NULL,
		[Series] [nvarchar](10) NULL,
		[Number] [nvarchar](10) NULL,
		[AdmissionYear] [nvarchar](4) NULL,
		[GraduationYear] [nvarchar](4) NULL,
		[Qualification] [nvarchar](50) NULL,
		[Speciality] [nvarchar](50) NULL,
		[Profession] [nvarchar](50) NULL,
		[Department] [nvarchar](50) NULL,
		[EducationId] [int] NULL,
		[LocationEI] [nvarchar](150) NULL,
	 CONSTRAINT [PK_HigherEducationDiploma] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]

	GO


	if OBJECT_ID (N'EmploymentEducationType', 'U') is not null
		DROP TABLE [dbo].[EmploymentEducationType]
	GO
	CREATE TABLE [dbo].[EmploymentEducationType](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[Version] [int] NULL,
		[Code] [nvarchar](10) NULL,
		[Name] [nvarchar](50) NULL,
		[Priority] [int] NOT NULL,
	 CONSTRAINT [PK_EmploymentEducationType] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]

	GO


	if OBJECT_ID (N'PostGraduateEducationDiploma', 'U') is not null
		DROP TABLE [dbo].[PostGraduateEducationDiploma]
	GO
	CREATE TABLE [dbo].[PostGraduateEducationDiploma](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[Version] [int] NOT NULL,
		[EducationId] [int] NULL,
		[IssuedBy] [nvarchar](150) NULL,
		[Series] [nvarchar](10) NULL,
		[Number] [nvarchar](10) NULL,
		[AdmissionYear] [nvarchar](4) NULL,
		[GraduationYear] [nvarchar](4) NULL,
		[Speciality] [nvarchar](50) NULL,
		[LocationEI] [nvarchar](150) NULL,
	 CONSTRAINT [PK_PostGraduateEducationDiploma] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]

	GO


	if OBJECT_ID (N'Training', 'U') is not null
		DROP TABLE [dbo].[Training]
	GO
	CREATE TABLE [dbo].[Training](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[Version] [int] NOT NULL,
		[CertificateIssuedBy] [nvarchar](50) NULL,
		[Series] [nvarchar](10) NULL,
		[Number] [nvarchar](10) NULL,
		[BeginningDate] [datetime] NULL,
		[EndDate] [datetime] NULL,
		[Speciality] [nvarchar](50) NULL,
		[EducationId] [int] NULL,
		[LocationEI] [nvarchar](150) NULL,
	 CONSTRAINT [PK_Training] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]

	GO


	if OBJECT_ID (N'Family', 'U') is not null
		DROP TABLE [dbo].[Family]
	GO
	CREATE TABLE [dbo].[Family](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[Version] [int] NOT NULL,
		[Cohabitants] [nvarchar](250) NULL,
		[CandidateId] [int] NOT NULL,
		[IsFinal] [bit] NOT NULL CONSTRAINT [DF__Family__IsFinal__4DE0370C]  DEFAULT ((0)),
		[IsValidate] [bit] NULL CONSTRAINT [DF_Family_IsValidate]  DEFAULT ((0)),
		[FamilyStatusId] [int] NULL,
	 CONSTRAINT [PK_Family] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]

	GO


	if OBJECT_ID (N'FamilyMember', 'U') is not null
		DROP TABLE [dbo].[FamilyMember]
	GO
	CREATE TABLE [dbo].[FamilyMember](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[Version] [int] NOT NULL,
		[RelationshipId] [int] NULL,
		[Name] [nvarchar](50) NULL,
		[PassportData] [nvarchar](150) NULL,
		[DateOfBirth] [datetime] NULL,
		[PlaceOfBirth] [nvarchar](150) NULL,
		[WorksAt] [nvarchar](50) NULL,
		[Contacts] [nvarchar](100) NULL,
		[FamilyId] [int] NULL,
	 CONSTRAINT [PK_FamilyMember] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]

	GO


	if OBJECT_ID (N'FamilyStatus', 'U') is not null
		DROP TABLE [dbo].[FamilyStatus]
	GO
	CREATE TABLE [dbo].[FamilyStatus](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[Version] [int] NULL,
		[Code] [nvarchar](10) NULL,
		[Name] [nvarchar](50) NULL,
	 CONSTRAINT [PK_FamilyStatus] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]

	GO


	if OBJECT_ID (N'MilitaryService', 'U') is not null
		DROP TABLE [dbo].[MilitaryService]
	GO
	CREATE TABLE [dbo].[MilitaryService](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[Version] [int] NOT NULL,
		[IsLiableForMilitaryService] [bit] NOT NULL,
		[MilitaryCardNumber] [nvarchar](20) NULL,
		[MilitaryCardDate] [datetime] NULL,
		[RankId] [int] NULL,
		[MilitarySpecialityCode] [nvarchar](7) NULL,
		[MilitaryValidityCategoryId] [int] NULL,
		[Commissariat] [nvarchar](100) NULL,
		[CandidateId] [int] NOT NULL,
		[ReserveCategory] [nvarchar](20) NULL,
		[SpecialityCategoryId] [int] NULL,
		[MilitaryRelationAccountId] [int] NULL,
		[RegistrationExpirationId] [int] NULL,
		[PersonnelCategoryId] [int] NULL,
		[PersonnelTypeId] [int] NULL,
		[IsAssigned] [bit] NOT NULL,
		[ConscriptionStatusId] [int] NULL,
		[CommonMilitaryServiceRegistrationInfo] [nvarchar](250) NULL,
		[SpecialMilitaryServiceRegistrationInfo] [nvarchar](250) NULL,
		[IsReserved] [bit] NOT NULL CONSTRAINT [DF__MilitaryS__IsRes__2C1F508E]  DEFAULT ((0)),
		[MobilizationTicketNumber] [nvarchar](20) NULL,
		[IsFinal] [bit] NOT NULL CONSTRAINT [DF__MilitaryS__IsFin__35A8BAC8]  DEFAULT ((0)),
		[IsValidate] [bit] NULL CONSTRAINT [DF_MilitaryService_IsValidate]  DEFAULT ((0)),
	 CONSTRAINT [PK_MilitaryService] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]

	GO


	if OBJECT_ID (N'MilitaryRanks', 'U') is not null
		DROP TABLE [dbo].[MilitaryRanks]
	GO
	CREATE TABLE [dbo].[MilitaryRanks](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[Version] [int] NOT NULL CONSTRAINT [DF_MilitaryRanks_Version]  DEFAULT ((1)),
		[Code] [nvarchar](10) NULL,
		[Name] [nvarchar](50) NULL,
	 CONSTRAINT [PK_MilitaryRanks] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]

	GO


	if OBJECT_ID (N'MilitaryRelationAccount', 'U') is not null
		DROP TABLE [dbo].[MilitaryRelationAccount]
	GO
	CREATE TABLE [dbo].[MilitaryRelationAccount](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[Version] [int] NOT NULL CONSTRAINT [DF_MilitaryRelationAccount_Version]  DEFAULT ((1)),
		[Code] [nvarchar](10) NULL,
		[Name] [nvarchar](50) NULL,
	 CONSTRAINT [PK_MilitaryRelationAccount] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]

	GO


	if OBJECT_ID (N'MilitarySpecialityCategory', 'U') is not null
		DROP TABLE [dbo].[MilitarySpecialityCategory]
	GO
	CREATE TABLE [dbo].[MilitarySpecialityCategory](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[Version] [int] NOT NULL,
		[Code] [nvarchar](10) NULL,
		[Name] [nvarchar](128) NULL,
	 CONSTRAINT [PK_MilitarySpecialityCategory] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]

	GO


	if OBJECT_ID (N'MilitaryValidityCategory', 'U') is not null
		DROP TABLE [dbo].[MilitaryValidityCategory]
	GO
	CREATE TABLE [dbo].[MilitaryValidityCategory](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[Version] [int] NOT NULL CONSTRAINT [DF_MilitaryValidityCategory_Version]  DEFAULT ((1)),
		[Code] [nvarchar](10) NULL,
		[Name] [nvarchar](100) NULL,
	 CONSTRAINT [PK_MilitaryValidityCategory] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]

	GO


	if OBJECT_ID (N'Experience', 'U') is not null
		DROP TABLE [dbo].[Experience]
	GO
	CREATE TABLE [dbo].[Experience](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[Version] [int] NOT NULL,
		[WorkBookSeries] [nvarchar](20) NULL,
		[WorkBookNumber] [nvarchar](20) NULL,
		[WorkBookDateOfIssue] [datetime] NULL,
		[WorkBookSupplementSeries] [nvarchar](20) NULL,
		[WorkBookSupplementNumber] [nvarchar](20) NULL,
		[WorkBookSupplementDateOfIssue] [datetime] NULL,
		[CandidateId] [int] NOT NULL,
		[IsFinal] [bit] NOT NULL CONSTRAINT [DF__Experienc__IsFin__38E51A26]  DEFAULT ((0)),
		[IsValidate] [bit] NULL CONSTRAINT [DF_Experience_IsValidate]  DEFAULT ((0)),
	 CONSTRAINT [PK_Experience] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]

	GO


	if OBJECT_ID (N'ExperienceItem', 'U') is not null
		DROP TABLE [dbo].[ExperienceItem]
	GO
	CREATE TABLE [dbo].[ExperienceItem](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[Version] [int] NOT NULL,
		[BeginningDate] [datetime] NULL,
		[EndDate] [datetime] NULL,
		[Company] [nvarchar](50) NULL,
		[Position] [nvarchar](50) NULL,
		[CompanyContacts] [nvarchar](250) NULL,
		[ExperienceId] [int] NULL,
	 CONSTRAINT [PK_ExperienceItem] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]

	GO


	if OBJECT_ID (N'Contacts', 'U') is not null
		DROP TABLE [dbo].[Contacts]
	GO
	CREATE TABLE [dbo].[Contacts](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[Version] [int] NOT NULL,
		[Country] [nvarchar](50) NULL,
		[ZipCode] [nvarchar](6) NULL,
		[Republic] [nvarchar](50) NULL,
		[Region] [nvarchar](50) NULL,
		[District] [nvarchar](50) NULL,
		[City] [nvarchar](50) NULL,
		[Street] [nvarchar](50) NULL,
		[StreetNumber] [nvarchar](10) NULL,
		[Building] [nvarchar](10) NULL,
		[WorkPhone] [nvarchar](20) NULL,
		[HomePhone] [nvarchar](20) NULL,
		[Mobile] [nvarchar](20) NULL,
		[Email] [nvarchar](50) NULL,
		[CandidateId] [int] NOT NULL,
		[Apartment] [nvarchar](10) NULL,
		[IsFinal] [bit] NOT NULL CONSTRAINT [DF__Contacts__IsFina__34B4968F]  DEFAULT ((0)),
		[IsValidate] [bit] NULL CONSTRAINT [DF_Contacts_IsValidate]  DEFAULT ((0)),
	 CONSTRAINT [PK_Contacts] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]

	GO


	if OBJECT_ID (N'BackgroundCheck', 'U') is not null
		DROP TABLE [dbo].[BackgroundCheck]
	GO
	CREATE TABLE [dbo].[BackgroundCheck](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[Version] [int] NOT NULL,
		[AverageSalary] [decimal](19, 2) NULL,
		[PreviousDismissalReason] [nvarchar](250) NULL,
		[PreviousSuperior] [nvarchar](250) NULL,
		[MilitaryOperationsExperience] [nvarchar](50) NULL,
		[HasDriversLicense] [bit] NULL CONSTRAINT [DF_BackgroundCheck_HasDriversLicense]  DEFAULT ((0)),
		[DriversLicenseNumber] [nvarchar](12) NULL,
		[DriversLicenseDateOfIssue] [datetime] NULL,
		[DriversLicenseCategories] [nvarchar](20) NULL,
		[DrivingExperience] [int] NULL,
		[IsReadyForBusinessTrips] [bit] NOT NULL,
		[Sports] [nvarchar](250) NULL,
		[Hobbies] [nvarchar](250) NULL,
		[ImportantEvents] [nvarchar](250) NULL,
		[ChronicalDiseases] [nvarchar](250) NULL,
		[CandidateId] [int] NOT NULL,
		[Liabilities] [nvarchar](250) NULL,
		[PositionSought] [nvarchar](50) NULL,
		[HasAutomobile] [bit] NULL CONSTRAINT [DF_BackgroundCheck_HasAutomobile]  DEFAULT ((0)),
		[AutomobileMake] [nvarchar](50) NULL,
		[AutomobileLicensePlateNumber] [nvarchar](15) NULL,
		[Penalties] [nvarchar](250) NULL,
		[PsychiatricAndAddictionTreatment] [nvarchar](250) NULL,
		[Smoking] [nvarchar](250) NULL,
		[Drinking] [nvarchar](250) NULL,
		[OwnerOfShares] [nvarchar](250) NULL,
		[PositionInGoverningBodies] [nvarchar](250) NULL,
		[ApprovalStatus] [bit] NULL,
		[ApproverId] [int] NULL,
		[ApprovalDate] [datetime] NULL,
		[IsFinal] [bit] NOT NULL CONSTRAINT [DF__Backgroun__IsFin__39794BAC]  DEFAULT ((0)),
		[IsApprovalSkipped] [bit] NOT NULL CONSTRAINT [DF_BackgroundCheck_IsApprovalSkipped]  DEFAULT ((0)),
		[IsValidate] [bit] NULL CONSTRAINT [DF_BackgroundCheck_IsValidate]  DEFAULT ((0)),
		[PyrusRef] [nvarchar](150) NULL,
		[CancelRejectUserId] [int] NULL,
		[CancelRejectDate] [datetime] NULL,
		[PrevApprovalStatus] [bit] NULL,
		[PrevApproverId] [int] NULL,
		[PrevApprovalDate] [datetime] NULL,
	 CONSTRAINT [PK_BackgroundCheck] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]

	GO


	if OBJECT_ID (N'Reference', 'U') is not null
		DROP TABLE [dbo].[Reference]
	GO
	CREATE TABLE [dbo].[Reference](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[Version] [int] NOT NULL,
		[LastName] [nvarchar](50) NULL,
		[FirstName] [nvarchar](50) NULL,
		[Patronymic] [nvarchar](50) NULL,
		[WorksAt] [nvarchar](50) NULL,
		[Position] [nvarchar](50) NULL,
		[Phone] [nvarchar](10) NULL,
		[Relation] [nvarchar](50) NULL,
		[BackgroundCheckId] [int] NULL,
	 CONSTRAINT [PK_Reference] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]

	GO


	if OBJECT_ID (N'OnsiteTraining', 'U') is not null
		DROP TABLE [dbo].[OnsiteTraining]
	GO
	CREATE TABLE [dbo].[OnsiteTraining](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[Version] [int] NOT NULL,
		[Description] [nvarchar](200) NULL,
		[BeginningDate] [datetime] NULL,
		[EndDate] [datetime] NULL,
		[IsComplete] [bit] NULL,
		[ReasonsForIncompleteTraining] [nvarchar](200) NULL,
		[Results] [nvarchar](200) NULL,
		[CandidateId] [int] NOT NULL,
		[Type] [nvarchar](200) NULL,
		[Comments] [nvarchar](200) NULL,
		[ApproverId] [int] NULL,
		[IsFinal] [bit] NOT NULL CONSTRAINT [DF_OnsiteTraining_IsFinal]  DEFAULT ((0)),
	 CONSTRAINT [PK_OnsiteTraining] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]

	GO


	if OBJECT_ID (N'Managers', 'U') is not null
		DROP TABLE [dbo].[Managers]
	GO
	CREATE TABLE [dbo].[Managers](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[Version] [int] NOT NULL,
		[PositionId] [int] NULL,
		[DepartmentId] [int] NULL,
		[EmploymentConditions] [nvarchar](250) NULL,
		[ProbationaryPeriod] [nvarchar](3) NULL,
		[WorkCity] [nvarchar](100) NULL,
		[IsFront] [bit] NOT NULL,
		[Bonus] [decimal](19, 2) NULL,
		[IsLiable] [bit] NOT NULL,
		[RequestNumber] [nvarchar](50) NULL,
		[CandidateId] [int] NOT NULL,
		[SalaryMultiplier] [decimal](3, 2) NULL,
		[ManagerApprovalStatus] [bit] NULL,
		[ApprovingManagerId] [int] NULL,
		[ManagerRejectionReason] [nvarchar](50) NULL,
		[HigherManagerApprovalStatus] [bit] NULL,
		[ApprovingHigherManagerId] [int] NULL,
		[HigherManagerRejectionReason] [nvarchar](50) NULL,
		[RejectingChiefId] [int] NULL,
		[ChiefRejectionReason] [nvarchar](50) NULL,
		[ManagerApprovalDate] [datetime] NULL,
		[HigherManagerApprovalDate] [datetime] NULL,
		[IsSecondaryJob] [bit] NOT NULL CONSTRAINT [DF_Managers_IsSecondaryJob]  DEFAULT ((0)),
		[IsExternalPTWorker] [bit] NULL CONSTRAINT [DF_Managers_IsExternalPTWorker]  DEFAULT ((0)),
		[SalaryBasis] [decimal](15, 2) NULL,
		[RegistrationDate] [datetime] NULL,
		[PlanRegistrationDate] [datetime] NULL,
		[CancelRejectUserId] [int] NULL,
		[CancelRejectDate] [datetime] NULL,
		[CancelRejectHigherUserId] [int] NULL,
		[CancelRejectHigherDate] [datetime] NULL,
		[MentorName] [nvarchar](100) NULL,
	 CONSTRAINT [PK_Managers] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]

	GO


	if OBJECT_ID (N'PersonnelManagers', 'U') is not null
		DROP TABLE [dbo].[PersonnelManagers]
	GO
	CREATE TABLE [dbo].[PersonnelManagers](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[Version] [int] NOT NULL,
		[EmploymentOrderDate] [datetime] NULL,
		[EmploymentOrderNumber] [nvarchar](20) NULL,
		[EmploymentDate] [datetime] NULL,
		[ContractDate] [datetime] NULL,
		[ContractNumber] [nvarchar](20) NULL,
		[BasicSalary] [decimal](15, 2) NULL CONSTRAINT [DF_PersonnelManagers_BaseSalary]  DEFAULT ((0)),
		[NorthernAreaAddition] [decimal](19, 2) NULL,
		[AreaMultiplier] [decimal](19, 2) NULL,
		[AreaAddition] [decimal](19, 2) NULL,
		[TravelRelatedAddition] [decimal](15, 2) NULL,
		[CompetenceAddition] [decimal](15, 2) NULL,
		[FrontOfficeExperienceAddition] [decimal](15, 2) NULL,
		[InsurableExperienceYears] [int] NULL,
		[InsurableExperienceMonths] [int] NULL,
		[InsurableExperienceDays] [int] NULL,
		[PersonalAccount] [nvarchar](23) NULL,
		[CandidateId] [int] NOT NULL,
		[OverallExperienceYears] [int] NULL,
		[OverallExperienceMonths] [int] NULL,
		[OverallExperienceDays] [int] NULL,
		[ApprovedByPersonnelManagerId] [int] NULL,
		[AccessGroupId] [int] NULL,
		[PersonalAccountContractorId] [int] NULL,
		[ContractEndDate] [datetime] NULL,
		[SignerId] [int] NULL,
		[IsHourlySalaryBasis] [bit] NOT NULL CONSTRAINT [DF_PersonnelManagers_IsHourlySalaryBasis]  DEFAULT ((0)),
		[InsuredPersonTypeId] [int] NULL,
		[Status] [int] NULL,
		[ContractPoint_1_Id] [int] NULL,
		[ContractPoint_2_Id] [int] NULL,
		[ContractPoint_3_Id] [int] NULL,
		[ContractPointsFio] [nvarchar](100) NULL,
		[ContractPointsAddress] [nvarchar](150) NULL,
		[ScheduleId] [int] NULL,
		[CompleteDate] [datetime] NULL,
		[PersonalAddition] [decimal](19, 2) NULL,
		[PositionAddition] [decimal](19, 2) NULL,
		[RejectDate] [datetime] NULL,
		[RejectUserId] [int] NULL,
		[NorthExperienceYears] [int] NULL,
		[NorthExperienceMonths] [int] NULL,
		[NorthExperienceDays] [int] NULL,
		[NorthExperienceType] [int] NULL,
		[ExtraChargesId] [int] NULL,
	 CONSTRAINT [PK_PersonnelManagers] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]

	GO


	if OBJECT_ID (N'AccessGroup', 'U') is not null
		DROP TABLE [dbo].[AccessGroup]
	GO
	CREATE TABLE [dbo].[AccessGroup](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[Version] [int] NOT NULL,
		[Code] [nvarchar](10) NULL,
		[Name] [nvarchar](128) NULL,
	 CONSTRAINT [PK_AccessGroup] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]

	GO


	if OBJECT_ID (N'InsuredPersonType', 'U') is not null
		DROP TABLE [dbo].[InsuredPersonType]
	GO
	CREATE TABLE [dbo].[InsuredPersonType](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[Version] [int] NOT NULL,
		[Code] [nvarchar](10) NULL,
		[Name] [nvarchar](128) NULL,
	 CONSTRAINT [PK_InsuredPersonType] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]

	GO


	if OBJECT_ID (N'PersonalAccountContractor', 'U') is not null
		DROP TABLE [dbo].[PersonalAccountContractor]
	GO
	CREATE TABLE [dbo].[PersonalAccountContractor](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[Version] [int] NOT NULL,
		[Code] [nvarchar](10) NULL,
		[Name] [nvarchar](128) NULL,
	 CONSTRAINT [PK_PersonalAccountContractor] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]

	GO


	if OBJECT_ID (N'Signer', 'U') is not null
		DROP TABLE [dbo].[Signer]
	GO
	CREATE TABLE [dbo].[Signer](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[Version] [int] NOT NULL,
		[Name] [nvarchar](50) NULL,
		[PreamblePartyTemplate] [nvarchar](500) NULL,
		[Position] [nvarchar](250) NULL,
	 CONSTRAINT [PK_Signer] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]

	GO


	if OBJECT_ID (N'Schedule', 'U') is not null
		DROP TABLE [dbo].[Schedule]
	GO
	CREATE TABLE [dbo].[Schedule](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[Version] [int] NOT NULL,
		[Code] [nvarchar](40) NULL,
		[Name] [nvarchar](128) NULL,
	 CONSTRAINT [PK_Schedule] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]

	GO


	if OBJECT_ID (N'SupplementaryAgreement', 'U') is not null
		DROP TABLE [dbo].[SupplementaryAgreement]
	GO
	CREATE TABLE [dbo].[SupplementaryAgreement](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[CreateDate] [datetime] NULL,
		[Number] [int] NULL,
		[OrderCreateDate] [datetime] NULL,
		[OrderNumber] [int] NULL,
		[PersonnelManagersId] [int] NOT NULL,
	 CONSTRAINT [PK_SupplementaryAgreement] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]

	GO



--3.СОЗДАНИЕ ССЫЛОК
	ALTER TABLE [dbo].[EmploymentCandidate]  WITH CHECK ADD  CONSTRAINT [FK_EmploymentCandidate_TDMarkUser] FOREIGN KEY([TDMarkUserId])
	REFERENCES [dbo].[Users] ([Id])
	GO

	ALTER TABLE [dbo].[EmploymentCandidate] CHECK CONSTRAINT [FK_EmploymentCandidate_TDMarkUser]
	GO

	ALTER TABLE [dbo].[EmploymentCandidate]  WITH CHECK ADD  CONSTRAINT [FK_EmploymentCandidate_TKMarkUser] FOREIGN KEY([TKMarkUserId])
	REFERENCES [dbo].[Users] ([Id])
	GO

	ALTER TABLE [dbo].[EmploymentCandidate] CHECK CONSTRAINT [FK_EmploymentCandidate_TKMarkUser]
	GO

	ALTER TABLE [dbo].[EmploymentCandidate]  WITH CHECK ADD  CONSTRAINT [FK_Candidate_AppointmentCreator] FOREIGN KEY([AppointmentCreatorId])
	REFERENCES [dbo].[Users] ([Id])
	GO

	ALTER TABLE [dbo].[EmploymentCandidate] CHECK CONSTRAINT [FK_Candidate_AppointmentCreator]
	GO

	ALTER TABLE [dbo].[EmploymentCandidate]  WITH CHECK ADD  CONSTRAINT [FK_Candidate_BackgroundCheck] FOREIGN KEY([BackgroundCheckId])
	REFERENCES [dbo].[BackgroundCheck] ([Id])
	GO

	ALTER TABLE [dbo].[EmploymentCandidate] CHECK CONSTRAINT [FK_Candidate_BackgroundCheck]
	GO

	ALTER TABLE [dbo].[EmploymentCandidate]  WITH CHECK ADD  CONSTRAINT [FK_Candidate_Contacts] FOREIGN KEY([ContactsId])
	REFERENCES [dbo].[Contacts] ([Id])
	GO

	ALTER TABLE [dbo].[EmploymentCandidate] CHECK CONSTRAINT [FK_Candidate_Contacts]
	GO

	ALTER TABLE [dbo].[EmploymentCandidate]  WITH CHECK ADD  CONSTRAINT [FK_Candidate_Education] FOREIGN KEY([EducationId])
	REFERENCES [dbo].[Education] ([Id])
	GO

	ALTER TABLE [dbo].[EmploymentCandidate] CHECK CONSTRAINT [FK_Candidate_Education]
	GO

	ALTER TABLE [dbo].[EmploymentCandidate]  WITH CHECK ADD  CONSTRAINT [FK_Candidate_Experience] FOREIGN KEY([ExperienceId])
	REFERENCES [dbo].[Experience] ([Id])
	GO

	ALTER TABLE [dbo].[EmploymentCandidate] CHECK CONSTRAINT [FK_Candidate_Experience]
	GO

	ALTER TABLE [dbo].[EmploymentCandidate]  WITH CHECK ADD  CONSTRAINT [FK_Candidate_Family] FOREIGN KEY([FamilyId])
	REFERENCES [dbo].[Family] ([Id])
	GO

	ALTER TABLE [dbo].[EmploymentCandidate] CHECK CONSTRAINT [FK_Candidate_Family]
	GO

	ALTER TABLE [dbo].[EmploymentCandidate]  WITH CHECK ADD  CONSTRAINT [FK_Candidate_GeneralInfo] FOREIGN KEY([GeneralInfoId])
	REFERENCES [dbo].[GeneralInfo] ([Id])
	GO

	ALTER TABLE [dbo].[EmploymentCandidate] CHECK CONSTRAINT [FK_Candidate_GeneralInfo]
	GO

	ALTER TABLE [dbo].[EmploymentCandidate]  WITH CHECK ADD  CONSTRAINT [FK_Candidate_Managers] FOREIGN KEY([ManagersId])
	REFERENCES [dbo].[Managers] ([Id])
	GO

	ALTER TABLE [dbo].[EmploymentCandidate] CHECK CONSTRAINT [FK_Candidate_Managers]
	GO

	ALTER TABLE [dbo].[EmploymentCandidate]  WITH CHECK ADD  CONSTRAINT [FK_Candidate_MilitaryService] FOREIGN KEY([MilitaryServiceId])
	REFERENCES [dbo].[MilitaryService] ([Id])
	GO

	ALTER TABLE [dbo].[EmploymentCandidate] CHECK CONSTRAINT [FK_Candidate_MilitaryService]
	GO

	ALTER TABLE [dbo].[EmploymentCandidate]  WITH CHECK ADD  CONSTRAINT [FK_Candidate_OnsiteTraining] FOREIGN KEY([OnsiteTrainingId])
	REFERENCES [dbo].[OnsiteTraining] ([Id])
	GO

	ALTER TABLE [dbo].[EmploymentCandidate] CHECK CONSTRAINT [FK_Candidate_OnsiteTraining]
	GO

	ALTER TABLE [dbo].[EmploymentCandidate]  WITH CHECK ADD  CONSTRAINT [FK_Candidate_Passport] FOREIGN KEY([PassportId])
	REFERENCES [dbo].[Passport] ([Id])
	GO

	ALTER TABLE [dbo].[EmploymentCandidate] CHECK CONSTRAINT [FK_Candidate_Passport]
	GO

	ALTER TABLE [dbo].[EmploymentCandidate]  WITH CHECK ADD  CONSTRAINT [FK_Candidate_PersonnelManagers] FOREIGN KEY([PersonnelManagersId])
	REFERENCES [dbo].[PersonnelManagers] ([Id])
	GO

	ALTER TABLE [dbo].[EmploymentCandidate] CHECK CONSTRAINT [FK_Candidate_PersonnelManagers]
	GO

	ALTER TABLE [dbo].[EmploymentCandidate]  WITH CHECK ADD  CONSTRAINT [FK_Candidate_User] FOREIGN KEY([UserId])
	REFERENCES [dbo].[Users] ([Id])
	GO

	ALTER TABLE [dbo].[EmploymentCandidate] CHECK CONSTRAINT [FK_Candidate_User]
	GO

	ALTER TABLE [dbo].[EmploymentCandidate]  WITH CHECK ADD  CONSTRAINT [FK_EmploymentCandidate_Appointment] FOREIGN KEY([AppointmentId])
	REFERENCES [dbo].[Appointment] ([Id])
	GO

	ALTER TABLE [dbo].[EmploymentCandidate] CHECK CONSTRAINT [FK_EmploymentCandidate_Appointment]
	GO

	ALTER TABLE [dbo].[EmploymentCandidate]  WITH CHECK ADD  CONSTRAINT [FK_EmploymentCandidate_AppointmentReport] FOREIGN KEY([AppointmentReportId])
	REFERENCES [dbo].[AppointmentReport] ([Id])
	GO

	ALTER TABLE [dbo].[EmploymentCandidate] CHECK CONSTRAINT [FK_EmploymentCandidate_AppointmentReport]
	GO

	ALTER TABLE [dbo].[EmploymentCandidate]  WITH CHECK ADD  CONSTRAINT [FK_EmploymentCandidate_Users] FOREIGN KEY([PersonnelId])
	REFERENCES [dbo].[Users] ([Id])
	GO

	ALTER TABLE [dbo].[EmploymentCandidate] CHECK CONSTRAINT [FK_EmploymentCandidate_Users]
	GO

	ALTER TABLE [dbo].[EmploymentCandidateDocNeeded]  WITH CHECK ADD  CONSTRAINT [FK_EmploymentCandidateDocNeeded_EmploymentCandidate] FOREIGN KEY([CandidateId])
	REFERENCES [dbo].[EmploymentCandidate] ([Id])
	GO

	ALTER TABLE [dbo].[EmploymentCandidateDocNeeded] CHECK CONSTRAINT [FK_EmploymentCandidateDocNeeded_EmploymentCandidate]
	GO

	ALTER TABLE [dbo].[EmploymentCandidateDocNeeded]  WITH CHECK ADD  CONSTRAINT [FK_EmploymentCandidateDocNeeded_Users] FOREIGN KEY([CreatorId])
	REFERENCES [dbo].[Users] ([Id])
	GO

	ALTER TABLE [dbo].[EmploymentCandidateDocNeeded] CHECK CONSTRAINT [FK_EmploymentCandidateDocNeeded_Users]
	GO

	ALTER TABLE [dbo].[EmploymentCandidateDocNeeded]  WITH CHECK ADD  CONSTRAINT [FK_EmploymentCandidateDocNeeded_Users1] FOREIGN KEY([EditorId])
	REFERENCES [dbo].[Users] ([Id])
	GO

	ALTER TABLE [dbo].[EmploymentCandidateDocNeeded] CHECK CONSTRAINT [FK_EmploymentCandidateDocNeeded_Users1]
	GO

	ALTER TABLE [dbo].[GeneralInfo]  WITH CHECK ADD  CONSTRAINT [FK_GeneralInfo_Candidate] FOREIGN KEY([CandidateId])
	REFERENCES [dbo].[EmploymentCandidate] ([Id])
	GO

	ALTER TABLE [dbo].[GeneralInfo] CHECK CONSTRAINT [FK_GeneralInfo_Candidate]
	GO

	ALTER TABLE [dbo].[GeneralInfo]  WITH CHECK ADD  CONSTRAINT [FK_GeneralInfo_Citizenship] FOREIGN KEY([CitizenshipId])
	REFERENCES [dbo].[Country] ([Id])
	GO

	ALTER TABLE [dbo].[GeneralInfo] CHECK CONSTRAINT [FK_GeneralInfo_Citizenship]
	GO

	ALTER TABLE [dbo].[GeneralInfo]  WITH CHECK ADD  CONSTRAINT [FK_GeneralInfo_Country] FOREIGN KEY([CountryBirthId])
	REFERENCES [dbo].[Country] ([Id])
	GO

	ALTER TABLE [dbo].[GeneralInfo] CHECK CONSTRAINT [FK_GeneralInfo_Country]
	GO

	ALTER TABLE [dbo].[GeneralInfo]  WITH CHECK ADD  CONSTRAINT [FK_GeneralInfo_DisabilityDegree] FOREIGN KEY([DisabilityDegreeId])
	REFERENCES [dbo].[DisabilityDegree] ([Id])
	GO

	ALTER TABLE [dbo].[GeneralInfo] CHECK CONSTRAINT [FK_GeneralInfo_DisabilityDegree]
	GO

	ALTER TABLE [dbo].[NameChange]  WITH CHECK ADD  CONSTRAINT [FK_NameChange_GeneralInfo] FOREIGN KEY([GeneralInfoId])
	REFERENCES [dbo].[GeneralInfo] ([Id])
	GO

	ALTER TABLE [dbo].[NameChange] CHECK CONSTRAINT [FK_NameChange_GeneralInfo]
	GO

	ALTER TABLE [dbo].[ForeignLanguage]  WITH CHECK ADD  CONSTRAINT [FK_ForeignLanguage_GeneralInfo] FOREIGN KEY([GeneralInfoId])
	REFERENCES [dbo].[GeneralInfo] ([Id])
	GO

	ALTER TABLE [dbo].[ForeignLanguage] CHECK CONSTRAINT [FK_ForeignLanguage_GeneralInfo]
	GO

	ALTER TABLE [dbo].[Passport]  WITH CHECK ADD  CONSTRAINT [FK_Passport_Candidate] FOREIGN KEY([CandidateId])
	REFERENCES [dbo].[EmploymentCandidate] ([Id])
	GO

	ALTER TABLE [dbo].[Passport] CHECK CONSTRAINT [FK_Passport_Candidate]
	GO

	ALTER TABLE [dbo].[Passport]  WITH CHECK ADD  CONSTRAINT [FK_Passport_DocumentType] FOREIGN KEY([DocumentTypeId])
	REFERENCES [dbo].[DocumentType] ([Id])
	GO

	ALTER TABLE [dbo].[Passport] CHECK CONSTRAINT [FK_Passport_DocumentType]
	GO

	ALTER TABLE [dbo].[Education]  WITH CHECK ADD  CONSTRAINT [FK_Education_Candidate] FOREIGN KEY([CandidateId])
	REFERENCES [dbo].[EmploymentCandidate] ([Id])
	GO

	ALTER TABLE [dbo].[Education] CHECK CONSTRAINT [FK_Education_Candidate]
	GO

	ALTER TABLE [dbo].[Certification]  WITH CHECK ADD  CONSTRAINT [FK_Certification_Education] FOREIGN KEY([EducationId])
	REFERENCES [dbo].[Education] ([Id])
	GO

	ALTER TABLE [dbo].[Certification] CHECK CONSTRAINT [FK_Certification_Education]
	GO

	ALTER TABLE [dbo].[HigherEducationDiploma]  WITH CHECK ADD  CONSTRAINT [FK_HigherEducationDiploma_Education] FOREIGN KEY([EducationId])
	REFERENCES [dbo].[Education] ([Id])
	GO

	ALTER TABLE [dbo].[HigherEducationDiploma] CHECK CONSTRAINT [FK_HigherEducationDiploma_Education]
	GO

	ALTER TABLE [dbo].[HigherEducationDiploma]  WITH CHECK ADD  CONSTRAINT [FK_HigherEducationDiploma_EmploymentEducationType] FOREIGN KEY([EducationTypeId])
	REFERENCES [dbo].[EmploymentEducationType] ([Id])
	GO

	ALTER TABLE [dbo].[HigherEducationDiploma] CHECK CONSTRAINT [FK_HigherEducationDiploma_EmploymentEducationType]
	GO

	ALTER TABLE [dbo].[PostGraduateEducationDiploma]  WITH CHECK ADD  CONSTRAINT [FK_PostGraduateEducationDiploma_Education] FOREIGN KEY([EducationId])
	REFERENCES [dbo].[Education] ([Id])
	GO

	ALTER TABLE [dbo].[PostGraduateEducationDiploma] CHECK CONSTRAINT [FK_PostGraduateEducationDiploma_Education]
	GO

	ALTER TABLE [dbo].[Training]  WITH CHECK ADD  CONSTRAINT [FK_Training_Education] FOREIGN KEY([EducationId])
	REFERENCES [dbo].[Education] ([Id])
	GO

	ALTER TABLE [dbo].[Training] CHECK CONSTRAINT [FK_Training_Education]
	GO

	ALTER TABLE [dbo].[Family]  WITH CHECK ADD  CONSTRAINT [FK_Family_Candidate] FOREIGN KEY([CandidateId])
	REFERENCES [dbo].[EmploymentCandidate] ([Id])
	GO

	ALTER TABLE [dbo].[Family] CHECK CONSTRAINT [FK_Family_Candidate]
	GO

	ALTER TABLE [dbo].[Family]  WITH CHECK ADD  CONSTRAINT [FK_Family_FamilyStatus] FOREIGN KEY([FamilyStatusId])
	REFERENCES [dbo].[FamilyStatus] ([Id])
	GO

	ALTER TABLE [dbo].[Family] CHECK CONSTRAINT [FK_Family_FamilyStatus]
	GO

	ALTER TABLE [dbo].[FamilyMember]  WITH CHECK ADD  CONSTRAINT [FK_FamilyMember_Family] FOREIGN KEY([FamilyId])
	REFERENCES [dbo].[Family] ([Id])
	GO

	ALTER TABLE [dbo].[FamilyMember] CHECK CONSTRAINT [FK_FamilyMember_Family]
	GO

	ALTER TABLE [dbo].[MilitaryService]  WITH CHECK ADD  CONSTRAINT [FK_MilitaryService_Candidate] FOREIGN KEY([CandidateId])
	REFERENCES [dbo].[EmploymentCandidate] ([Id])
	GO

	ALTER TABLE [dbo].[MilitaryService] CHECK CONSTRAINT [FK_MilitaryService_Candidate]
	GO

	ALTER TABLE [dbo].[MilitaryService]  WITH CHECK ADD  CONSTRAINT [FK_MilitaryService_MilitaryRanks] FOREIGN KEY([RankId])
	REFERENCES [dbo].[MilitaryRanks] ([Id])
	GO

	ALTER TABLE [dbo].[MilitaryService] CHECK CONSTRAINT [FK_MilitaryService_MilitaryRanks]
	GO

	ALTER TABLE [dbo].[MilitaryService]  WITH CHECK ADD  CONSTRAINT [FK_MilitaryService_MilitaryRelationAccount] FOREIGN KEY([MilitaryRelationAccountId])
	REFERENCES [dbo].[MilitaryRelationAccount] ([Id])
	GO

	ALTER TABLE [dbo].[MilitaryService] CHECK CONSTRAINT [FK_MilitaryService_MilitaryRelationAccount]
	GO

	ALTER TABLE [dbo].[MilitaryService]  WITH CHECK ADD  CONSTRAINT [FK_MilitaryService_MilitarySpecialityCategory] FOREIGN KEY([SpecialityCategoryId])
	REFERENCES [dbo].[MilitarySpecialityCategory] ([Id])
	GO

	ALTER TABLE [dbo].[MilitaryService] CHECK CONSTRAINT [FK_MilitaryService_MilitarySpecialityCategory]
	GO

	ALTER TABLE [dbo].[MilitaryService]  WITH CHECK ADD  CONSTRAINT [FK_MilitaryService_MilitaryValidityCategory] FOREIGN KEY([MilitaryValidityCategoryId])
	REFERENCES [dbo].[MilitaryValidityCategory] ([Id])
	GO

	ALTER TABLE [dbo].[MilitaryService] CHECK CONSTRAINT [FK_MilitaryService_MilitaryValidityCategory]
	GO

	ALTER TABLE [dbo].[Experience]  WITH CHECK ADD  CONSTRAINT [FK_Experience_Candidate] FOREIGN KEY([CandidateId])
	REFERENCES [dbo].[EmploymentCandidate] ([Id])
	GO

	ALTER TABLE [dbo].[Experience] CHECK CONSTRAINT [FK_Experience_Candidate]
	GO

	ALTER TABLE [dbo].[ExperienceItem]  WITH CHECK ADD  CONSTRAINT [FK_ExperienceItem_Experience] FOREIGN KEY([ExperienceId])
	REFERENCES [dbo].[Experience] ([Id])
	GO

	ALTER TABLE [dbo].[ExperienceItem] CHECK CONSTRAINT [FK_ExperienceItem_Experience]
	GO

	ALTER TABLE [dbo].[Contacts]  WITH CHECK ADD  CONSTRAINT [FK_Contacts_Candidate] FOREIGN KEY([CandidateId])
	REFERENCES [dbo].[EmploymentCandidate] ([Id])
	GO

	ALTER TABLE [dbo].[Contacts] CHECK CONSTRAINT [FK_Contacts_Candidate]
	GO

	ALTER TABLE [dbo].[BackgroundCheck]  WITH CHECK ADD  CONSTRAINT [FK_BackgroundCheck_Approver] FOREIGN KEY([ApproverId])
	REFERENCES [dbo].[Users] ([Id])
	GO

	ALTER TABLE [dbo].[BackgroundCheck] CHECK CONSTRAINT [FK_BackgroundCheck_Approver]
	GO

	ALTER TABLE [dbo].[BackgroundCheck]  WITH CHECK ADD  CONSTRAINT [FK_BackgroundCheck_CancelReject] FOREIGN KEY([CancelRejectUserId])
	REFERENCES [dbo].[Users] ([Id])
	GO

	ALTER TABLE [dbo].[BackgroundCheck] CHECK CONSTRAINT [FK_BackgroundCheck_CancelReject]
	GO

	ALTER TABLE [dbo].[BackgroundCheck]  WITH CHECK ADD  CONSTRAINT [FK_BackgroundCheck_Candidate] FOREIGN KEY([CandidateId])
	REFERENCES [dbo].[EmploymentCandidate] ([Id])
	GO

	ALTER TABLE [dbo].[BackgroundCheck] CHECK CONSTRAINT [FK_BackgroundCheck_Candidate]
	GO

	ALTER TABLE [dbo].[BackgroundCheck]  WITH CHECK ADD  CONSTRAINT [FK_BackgroundCheck_PrevApprover] FOREIGN KEY([PrevApproverId])
	REFERENCES [dbo].[Users] ([Id])
	GO

	ALTER TABLE [dbo].[BackgroundCheck] CHECK CONSTRAINT [FK_BackgroundCheck_PrevApprover]
	GO

	ALTER TABLE [dbo].[Reference]  WITH CHECK ADD  CONSTRAINT [FK_Reference_BackgroundCheck] FOREIGN KEY([BackgroundCheckId])
	REFERENCES [dbo].[BackgroundCheck] ([Id])
	GO

	ALTER TABLE [dbo].[Reference] CHECK CONSTRAINT [FK_Reference_BackgroundCheck]
	GO

	ALTER TABLE [dbo].[OnsiteTraining]  WITH CHECK ADD  CONSTRAINT [FK_OnsiteTraining_Approver] FOREIGN KEY([ApproverId])
	REFERENCES [dbo].[Users] ([Id])
	GO

	ALTER TABLE [dbo].[OnsiteTraining] CHECK CONSTRAINT [FK_OnsiteTraining_Approver]
	GO

	ALTER TABLE [dbo].[OnsiteTraining]  WITH CHECK ADD  CONSTRAINT [FK_OnsiteTraining_Candidate] FOREIGN KEY([CandidateId])
	REFERENCES [dbo].[EmploymentCandidate] ([Id])
	GO

	ALTER TABLE [dbo].[OnsiteTraining] CHECK CONSTRAINT [FK_OnsiteTraining_Candidate]
	GO

	ALTER TABLE [dbo].[Managers]  WITH CHECK ADD  CONSTRAINT [FK_Managers_ApprovingHigherManager] FOREIGN KEY([ApprovingHigherManagerId])
	REFERENCES [dbo].[Users] ([Id])
	GO

	ALTER TABLE [dbo].[Managers] CHECK CONSTRAINT [FK_Managers_ApprovingHigherManager]
	GO

	ALTER TABLE [dbo].[Managers]  WITH CHECK ADD  CONSTRAINT [FK_Managers_ApprovingManager] FOREIGN KEY([ApprovingManagerId])
	REFERENCES [dbo].[Users] ([Id])
	GO

	ALTER TABLE [dbo].[Managers] CHECK CONSTRAINT [FK_Managers_ApprovingManager]
	GO

	ALTER TABLE [dbo].[Managers]  WITH CHECK ADD  CONSTRAINT [FK_Managers_CancelRejectHigherUser] FOREIGN KEY([CancelRejectHigherUserId])
	REFERENCES [dbo].[Users] ([Id])
	GO

	ALTER TABLE [dbo].[Managers] CHECK CONSTRAINT [FK_Managers_CancelRejectHigherUser]
	GO

	ALTER TABLE [dbo].[Managers]  WITH CHECK ADD  CONSTRAINT [FK_Managers_CancelRejectUser] FOREIGN KEY([CancelRejectUserId])
	REFERENCES [dbo].[Users] ([Id])
	GO

	ALTER TABLE [dbo].[Managers] CHECK CONSTRAINT [FK_Managers_CancelRejectUser]
	GO

	ALTER TABLE [dbo].[Managers]  WITH CHECK ADD  CONSTRAINT [FK_Managers_Candidate] FOREIGN KEY([CandidateId])
	REFERENCES [dbo].[EmploymentCandidate] ([Id])
	GO

	ALTER TABLE [dbo].[Managers] CHECK CONSTRAINT [FK_Managers_Candidate]
	GO

	ALTER TABLE [dbo].[Managers]  WITH CHECK ADD  CONSTRAINT [FK_Managers_Department] FOREIGN KEY([DepartmentId])
	REFERENCES [dbo].[Department] ([Id])
	GO

	ALTER TABLE [dbo].[Managers] CHECK CONSTRAINT [FK_Managers_Department]
	GO

	ALTER TABLE [dbo].[Managers]  WITH CHECK ADD  CONSTRAINT [FK_Managers_Position] FOREIGN KEY([PositionId])
	REFERENCES [dbo].[Position] ([Id])
	GO

	ALTER TABLE [dbo].[Managers] CHECK CONSTRAINT [FK_Managers_Position]
	GO

	ALTER TABLE [dbo].[Managers]  WITH CHECK ADD  CONSTRAINT [FK_Managers_RejectingChief] FOREIGN KEY([RejectingChiefId])
	REFERENCES [dbo].[Users] ([Id])
	GO

	ALTER TABLE [dbo].[Managers] CHECK CONSTRAINT [FK_Managers_RejectingChief]
	GO

	ALTER TABLE [dbo].[PersonnelManagers]  WITH CHECK ADD  CONSTRAINT [FK_PersonnelManagers_AccessGroup] FOREIGN KEY([AccessGroupId])
	REFERENCES [dbo].[AccessGroup] ([Id])
	GO

	ALTER TABLE [dbo].[PersonnelManagers] CHECK CONSTRAINT [FK_PersonnelManagers_AccessGroup]
	GO

	ALTER TABLE [dbo].[PersonnelManagers]  WITH CHECK ADD  CONSTRAINT [FK_PersonnelManagers_ApprovedByPersonnelManagerUser] FOREIGN KEY([ApprovedByPersonnelManagerId])
	REFERENCES [dbo].[Users] ([Id])
	GO

	ALTER TABLE [dbo].[PersonnelManagers] CHECK CONSTRAINT [FK_PersonnelManagers_ApprovedByPersonnelManagerUser]
	GO

	ALTER TABLE [dbo].[PersonnelManagers]  WITH CHECK ADD  CONSTRAINT [FK_PersonnelManagers_Candidate] FOREIGN KEY([CandidateId])
	REFERENCES [dbo].[EmploymentCandidate] ([Id])
	GO

	ALTER TABLE [dbo].[PersonnelManagers] CHECK CONSTRAINT [FK_PersonnelManagers_Candidate]
	GO

	ALTER TABLE [dbo].[PersonnelManagers]  WITH CHECK ADD  CONSTRAINT [FK_PersonnelManagers_InsuredPersonType] FOREIGN KEY([InsuredPersonTypeId])
	REFERENCES [dbo].[InsuredPersonType] ([Id])
	GO

	ALTER TABLE [dbo].[PersonnelManagers] CHECK CONSTRAINT [FK_PersonnelManagers_InsuredPersonType]
	GO

	ALTER TABLE [dbo].[PersonnelManagers]  WITH CHECK ADD  CONSTRAINT [FK_PersonnelManagers_PersonalAccountContractor] FOREIGN KEY([PersonalAccountContractorId])
	REFERENCES [dbo].[PersonalAccountContractor] ([Id])
	GO

	ALTER TABLE [dbo].[PersonnelManagers] CHECK CONSTRAINT [FK_PersonnelManagers_PersonalAccountContractor]
	GO

	ALTER TABLE [dbo].[PersonnelManagers]  WITH CHECK ADD  CONSTRAINT [FK_PersonnelManagers_PersonnelOrderExtraCharges] FOREIGN KEY([ExtraChargesId])
	REFERENCES [dbo].[PersonnelOrderExtraCharges] ([Id])
	GO

	ALTER TABLE [dbo].[PersonnelManagers] CHECK CONSTRAINT [FK_PersonnelManagers_PersonnelOrderExtraCharges]
	GO

	ALTER TABLE [dbo].[PersonnelManagers]  WITH CHECK ADD  CONSTRAINT [FK_PersonnelManagers_Schedule] FOREIGN KEY([ScheduleId])
	REFERENCES [dbo].[Schedule] ([Id])
	GO

	ALTER TABLE [dbo].[PersonnelManagers] CHECK CONSTRAINT [FK_PersonnelManagers_Schedule]
	GO

	ALTER TABLE [dbo].[PersonnelManagers]  WITH CHECK ADD  CONSTRAINT [FK_PersonnelManagers_Signer] FOREIGN KEY([SignerId])
	REFERENCES [dbo].[Signer] ([Id])
	GO

	ALTER TABLE [dbo].[PersonnelManagers] CHECK CONSTRAINT [FK_PersonnelManagers_Signer]
	GO

	ALTER TABLE [dbo].[PersonnelManagers]  WITH CHECK ADD  CONSTRAINT [FK_PersonnelManagers_Users] FOREIGN KEY([RejectUserId])
	REFERENCES [dbo].[Users] ([Id])
	GO

	ALTER TABLE [dbo].[PersonnelManagers] CHECK CONSTRAINT [FK_PersonnelManagers_Users]
	GO

	ALTER TABLE [dbo].[SupplementaryAgreement]  WITH CHECK ADD  CONSTRAINT [FK_SupplementaryAgreement_PersonnelManagers] FOREIGN KEY([PersonnelManagersId])
	REFERENCES [dbo].[PersonnelManagers] ([Id])
	GO

	ALTER TABLE [dbo].[SupplementaryAgreement] CHECK CONSTRAINT [FK_SupplementaryAgreement_PersonnelManagers]
	GO


--4. СОЗДАНИЕ ОПИСАНИЙ К ТАБЛИЦАМ/ПОЛЯМ
	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Признак получения оригинала трудовой книжки кадровиком' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmploymentCandidate', @level2type=N'COLUMN',@level2name=N'IsTKReceived'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Дата получения кадровиком оригинала трудовой книжки кандидата' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmploymentCandidate', @level2type=N'COLUMN',@level2name=N'TKReceivedDate'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id пользователя сделавшего отметку о ТК' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmploymentCandidate', @level2type=N'COLUMN',@level2name=N'TKMarkUserId'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Признак получения оригинала трудовой договора кадровиком' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmploymentCandidate', @level2type=N'COLUMN',@level2name=N'IsTDReceived'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Дата получения кадровиком оригинала трудового договора кандидата' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmploymentCandidate', @level2type=N'COLUMN',@level2name=N'TDReceivedDate'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id пользователя сделавшего отметку о ТД' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmploymentCandidate', @level2type=N'COLUMN',@level2name=N'TDMarkUserId'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Номер документа приема на работу из 1С' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmploymentCandidate', @level2type=N'COLUMN',@level2name=N'ContractNumber1C'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Дата выгрузки в 1С' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmploymentCandidate', @level2type=N'COLUMN',@level2name=N'SendTo1C'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID кадровика' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmploymentCandidate', @level2type=N'COLUMN',@level2name=N'PersonnelId'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Нужно обучение' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmploymentCandidate', @level2type=N'COLUMN',@level2name=N'IsTrainingNeeded'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Обучение до принятия' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmploymentCandidate', @level2type=N'COLUMN',@level2name=N'IsBeforEmployment'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Сообщение от кандидата послано в ДП для предварительного согласования' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmploymentCandidate', @level2type=N'COLUMN',@level2name=N'IsCandidateToBackgroundPrevSendEmail'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Дата сообщения от кандидата в ДП для предварительного согласования' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmploymentCandidate', @level2type=N'COLUMN',@level2name=N'CandidateToBackgroundPrevSendEmailDate'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Сообщение от кандидата послано в ДП' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmploymentCandidate', @level2type=N'COLUMN',@level2name=N'IsCandidateToBackgroundSendEmail'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Дата сообщения от кандидата в ДП' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmploymentCandidate', @level2type=N'COLUMN',@level2name=N'CandidateToBackgroundSendEmailDate'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Сообщение послано руководителю о заявлении' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmploymentCandidate', @level2type=N'COLUMN',@level2name=N'IsCandidateToManagerSendEmail'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Дата сообщения руководителю о заявлении' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmploymentCandidate', @level2type=N'COLUMN',@level2name=N'CandidateToManagerSendEmailDate'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Сообщение от Дп послано руководителю' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmploymentCandidate', @level2type=N'COLUMN',@level2name=N'IsBackgroundToManagerSendEmail'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Дата сообщения от ДП руководителю' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmploymentCandidate', @level2type=N'COLUMN',@level2name=N'BackgroundToManagerSendEmailDate'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Сообщение тренеру послано' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmploymentCandidate', @level2type=N'COLUMN',@level2name=N'IsManagerToTrainingSendEmail'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Дата сообщения тренеру' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmploymentCandidate', @level2type=N'COLUMN',@level2name=N'ManagerToTrainingSendEmailDate'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Сообщение высшему руководству послано' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmploymentCandidate', @level2type=N'COLUMN',@level2name=N'IsManagerToHigherManagerSendEmail'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Дата сообщения высшему руководству' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmploymentCandidate', @level2type=N'COLUMN',@level2name=N'ManagerToHigherManagerSendEmailDate'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Сообщение руководству от кадровика послано' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmploymentCandidate', @level2type=N'COLUMN',@level2name=N'IsPersonnelManagerToManagerSendEmail'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Дата сообщения руководству от кадровика' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmploymentCandidate', @level2type=N'COLUMN',@level2name=N'PersonnelManagerToManagerSendEmailDate'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Признак технического увольнения' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmploymentCandidate', @level2type=N'COLUMN',@level2name=N'IsTechDissmiss'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Подтверждение достоверности сканов анкеты' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmploymentCandidate', @level2type=N'COLUMN',@level2name=N'IsScanFinal'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id записи' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmploymentCandidateDocNeeded', @level2type=N'COLUMN',@level2name=N'Id'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Версия записи' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmploymentCandidateDocNeeded', @level2type=N'COLUMN',@level2name=N'Version'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id кандидата' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmploymentCandidateDocNeeded', @level2type=N'COLUMN',@level2name=N'CandidateId'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id документа' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmploymentCandidateDocNeeded', @level2type=N'COLUMN',@level2name=N'DocTypeId'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Документ нужен для подписи кандидатом' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmploymentCandidateDocNeeded', @level2type=N'COLUMN',@level2name=N'IsNeeded'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Дата создания записи' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmploymentCandidateDocNeeded', @level2type=N'COLUMN',@level2name=N'DateCreate'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id создателя' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmploymentCandidateDocNeeded', @level2type=N'COLUMN',@level2name=N'CreatorId'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Дата редактирования' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmploymentCandidateDocNeeded', @level2type=N'COLUMN',@level2name=N'DateEdit'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id редактора' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmploymentCandidateDocNeeded', @level2type=N'COLUMN',@level2name=N'EditorId'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Список необходимых документов для подписи' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmploymentCandidateDocNeeded'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Признак бессрочной справки об инвалидности' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GeneralInfo', @level2type=N'COLUMN',@level2name=N'IsDisabilityTermLess'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Подтверждение кандидатом правильность внесенных данных' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GeneralInfo', @level2type=N'COLUMN',@level2name=N'IsValidate'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Страна рождения' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GeneralInfo', @level2type=N'COLUMN',@level2name=N'CountryBirthId'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Подтверждение кандидатом правильность внесенных данных' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Passport', @level2type=N'COLUMN',@level2name=N'IsValidate'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Подтверждение кандидатом правильность внесенных данных' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Education', @level2type=N'COLUMN',@level2name=N'IsValidate'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Место нахождения учебного заведения' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Certification', @level2type=N'COLUMN',@level2name=N'LocationEI'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id виды образования' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HigherEducationDiploma', @level2type=N'COLUMN',@level2name=N'EducationTypeId'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Место нахождения учебного заведения' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HigherEducationDiploma', @level2type=N'COLUMN',@level2name=N'LocationEI'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID записи' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmploymentEducationType', @level2type=N'COLUMN',@level2name=N'Id'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Версия' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmploymentEducationType', @level2type=N'COLUMN',@level2name=N'Version'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Код' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmploymentEducationType', @level2type=N'COLUMN',@level2name=N'Code'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Наименование' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmploymentEducationType', @level2type=N'COLUMN',@level2name=N'Name'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Приоритет (сортировка)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmploymentEducationType', @level2type=N'COLUMN',@level2name=N'Priority'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Виды образования' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmploymentEducationType'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Место нахождения учебного заведения' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PostGraduateEducationDiploma', @level2type=N'COLUMN',@level2name=N'LocationEI'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Место нахождения учебного заведения' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Training', @level2type=N'COLUMN',@level2name=N'LocationEI'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Подтверждение кандидатом правильность внесенных данных' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Family', @level2type=N'COLUMN',@level2name=N'IsValidate'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id записи' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FamilyStatus', @level2type=N'COLUMN',@level2name=N'Id'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Справочник смейных статусов' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FamilyStatus'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Категория пригодности к военной службе' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MilitaryService', @level2type=N'COLUMN',@level2name=N'MilitaryValidityCategoryId'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Состав (профиль)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MilitaryService', @level2type=N'COLUMN',@level2name=N'SpecialityCategoryId'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Отношение к воинмкому учету' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MilitaryService', @level2type=N'COLUMN',@level2name=N'MilitaryRelationAccountId'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Подтверждение кандидатом правильность внесенных данных' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MilitaryService', @level2type=N'COLUMN',@level2name=N'IsValidate'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id записи' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MilitaryRanks', @level2type=N'COLUMN',@level2name=N'Id'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Версия' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MilitaryRanks', @level2type=N'COLUMN',@level2name=N'Version'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Код' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MilitaryRanks', @level2type=N'COLUMN',@level2name=N'Code'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Название' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MilitaryRanks', @level2type=N'COLUMN',@level2name=N'Name'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Справочник воинских званий' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MilitaryRanks'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id записи' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MilitaryRelationAccount', @level2type=N'COLUMN',@level2name=N'Id'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Версия' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MilitaryRelationAccount', @level2type=N'COLUMN',@level2name=N'Version'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Название' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MilitaryRelationAccount', @level2type=N'COLUMN',@level2name=N'Name'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Справочник отношений к воинскому учету' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MilitaryRelationAccount'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id записи' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MilitaryValidityCategory', @level2type=N'COLUMN',@level2name=N'Id'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Версия' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MilitaryValidityCategory', @level2type=N'COLUMN',@level2name=N'Version'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Код' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MilitaryValidityCategory', @level2type=N'COLUMN',@level2name=N'Code'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Наименование' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MilitaryValidityCategory', @level2type=N'COLUMN',@level2name=N'Name'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Справочник категорий годности к военной службе' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MilitaryValidityCategory'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Подтверждение кандидатом правильность внесенных данных' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Experience', @level2type=N'COLUMN',@level2name=N'IsValidate'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Страна' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Contacts', @level2type=N'COLUMN',@level2name=N'Country'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Республика/край' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Contacts', @level2type=N'COLUMN',@level2name=N'Republic'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Подтверждение кандидатом правильность внесенных данных' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Contacts', @level2type=N'COLUMN',@level2name=N'IsValidate'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Наличие водительских прав' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BackgroundCheck', @level2type=N'COLUMN',@level2name=N'HasDriversLicense'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Наличие автомобиля' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BackgroundCheck', @level2type=N'COLUMN',@level2name=N'HasAutomobile'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Родственники владельцы долей предприятий' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BackgroundCheck', @level2type=N'COLUMN',@level2name=N'OwnerOfShares'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'родственники в органах управления иных юр. лиц' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BackgroundCheck', @level2type=N'COLUMN',@level2name=N'PositionInGoverningBodies'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Дата согласования' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BackgroundCheck', @level2type=N'COLUMN',@level2name=N'ApprovalDate'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Подтверждение кандидатом правильность внесенных данных' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BackgroundCheck', @level2type=N'COLUMN',@level2name=N'IsValidate'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Ссылка на задачу в системе Pyrus' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BackgroundCheck', @level2type=N'COLUMN',@level2name=N'PyrusRef'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Пользователь отменивший отклонение' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BackgroundCheck', @level2type=N'COLUMN',@level2name=N'CancelRejectUserId'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Дата отмены отклонения' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BackgroundCheck', @level2type=N'COLUMN',@level2name=N'CancelRejectDate'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Статус предварительного согласования' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BackgroundCheck', @level2type=N'COLUMN',@level2name=N'PrevApprovalStatus'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Дата предварительного согласования' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BackgroundCheck', @level2type=N'COLUMN',@level2name=N'PrevApprovalDate'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Внешний совместитель' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Managers', @level2type=N'COLUMN',@level2name=N'IsExternalPTWorker'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Дата оформления' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Managers', @level2type=N'COLUMN',@level2name=N'RegistrationDate'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Планируемая дата приема' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Managers', @level2type=N'COLUMN',@level2name=N'PlanRegistrationDate'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Пользователь отменивший отклонение' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Managers', @level2type=N'COLUMN',@level2name=N'CancelRejectUserId'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Дата отмены отклонения' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Managers', @level2type=N'COLUMN',@level2name=N'CancelRejectDate'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Пользователь отменивший отклонение высшего руководителя' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Managers', @level2type=N'COLUMN',@level2name=N'CancelRejectHigherUserId'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Дата отмены отклонения высшего руководителя' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Managers', @level2type=N'COLUMN',@level2name=N'CancelRejectHigherDate'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ФИО наставника' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Managers', @level2type=N'COLUMN',@level2name=N'MentorName'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Базовый оклад по штатнму распсанию' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PersonnelManagers', @level2type=N'COLUMN',@level2name=N'BasicSalary'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id варианта пунтка 1.5 для договора' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PersonnelManagers', @level2type=N'COLUMN',@level2name=N'ContractPoint_1_Id'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id варианта пунтка 1.6 для договора' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PersonnelManagers', @level2type=N'COLUMN',@level2name=N'ContractPoint_2_Id'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id варианта пунтка 5.1 для договора' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PersonnelManagers', @level2type=N'COLUMN',@level2name=N'ContractPoint_3_Id'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ФИО для пунктов договора' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PersonnelManagers', @level2type=N'COLUMN',@level2name=N'ContractPointsFio'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ContractPointsAddress' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PersonnelManagers', @level2type=N'COLUMN',@level2name=N'ContractPointsAddress'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'График работы' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PersonnelManagers', @level2type=N'COLUMN',@level2name=N'ScheduleId'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Дата готовности кандидата к выгрузке в 1С' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PersonnelManagers', @level2type=N'COLUMN',@level2name=N'CompleteDate'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Дата отклонения' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PersonnelManagers', @level2type=N'COLUMN',@level2name=N'RejectDate'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id сотрудника, который провел отклонение' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PersonnelManagers', @level2type=N'COLUMN',@level2name=N'RejectUserId'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Год севернного стажа' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PersonnelManagers', @level2type=N'COLUMN',@level2name=N'NorthExperienceYears'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Месяц севернного стажа' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PersonnelManagers', @level2type=N'COLUMN',@level2name=N'NorthExperienceMonths'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'День севернного стажа' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PersonnelManagers', @level2type=N'COLUMN',@level2name=N'NorthExperienceDays'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Тип северного стажа' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PersonnelManagers', @level2type=N'COLUMN',@level2name=N'NorthExperienceType'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Порядок начисления надбавок' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PersonnelManagers', @level2type=N'COLUMN',@level2name=N'ExtraChargesId'
	GO





--ПРЕДСТАВЛЕНИЯ НАЧАЛО
	IF OBJECT_ID ('vwEmploymentComments', 'V') IS NOT NULL
		DROP VIEW [dbo].[vwEmploymentComments]
	GO

	--достаем комментарии к разделам приема
	CREATE VIEW [dbo].[vwEmploymentComments]
	AS
	SELECT B.Name as Creator, isnull(D.Name, '') as CreatorPosition, A.CreatedDate, A.Comment, CommentTypeId, A.CandidateId, A.UserId
	FROM EmploymentCandidateComments as A
	INNER JOIN Users as B ON B.Id = A.UserId
	INNER JOIN Users as C ON C.Id = A.CandidateId
	LEFT JOIN Position as D ON D.Id = B.PositionId
	GO



	IF OBJECT_ID ('vwEmploymentPersonnels', 'V') IS NOT NULL
		DROP VIEW [dbo].[vwEmploymentPersonnels]
	GO
	
	--опредялем доступ кадровиков по ID одного из них
	CREATE VIEW [dbo].[vwEmploymentPersonnels]
	AS
	SELECT A.Id as UserId, D.Id as PersonnelId
	FROM Users as A
	INNER JOIN UserAccessGroup as B ON B.UserCode = A.Code
	INNER JOIN UserAccessGroup as C ON C.AccessGroupCode = B.AccessGroupCode
	INNER JOIN Users as D ON D.Code = C.UserCode
	GO



	IF OBJECT_ID ('vwEmploymentHighEducations', 'V') IS NOT NULL
	DROP VIEW [dbo].[vwEmploymentHighEducations]
	GO

	--состояние заполнения анкет
	CREATE VIEW [dbo].[vwEmploymentHighEducations]
	AS
	SELECT A.*, B.Name as EducationTypeName
	FROM HigherEducationDiploma as A
	LEFT JOIN EmploymentEducationType as B ON B.Id = A.EducationTypeId
	GO



IF OBJECT_ID ('vwEmploymentFillState', 'V') IS NOT NULL
	DROP VIEW [dbo].[vwEmploymentFillState]
GO

--состояние заполнения анкет
CREATE VIEW [dbo].[vwEmploymentFillState]
AS
SELECT -1 as Id, cast(0 as bit) as ScanFinal, cast(0 as bit) as GeneralFinal, cast(0 as bit) as PassportFinal, cast(0 as bit) as EducationFinal, cast(0 as bit) as FamilyFinal, cast(0 as bit) as MilitaryFinal, 
			 cast(0 as bit) as ExperienceFinal, cast(0 as bit) as ContactFinal, cast(0 as bit) as BackgroundFinal, cast(0 as bit) as CandidateApp,
			 --кандидат полностью заполнил анкету
			 cast(0 as bit) as CandidateReady,
			 --согласование
			 cast(0 as bit) as BackgroundApproval, cast(0 as bit) as TrainingApproval, cast(0 as bit) as ManagerApproval, cast(0 as bit) as PrevBackgroundApproval, cast(0 as bit) as PersonnelManagerApproval
UNION ALL
SELECT A.Id, isnull(A.IsScanFinal, 0) as ScanFinal, B.IsFinal as GeneralFinal, C.IsFinal as PassportFinal, D.IsFinal as EducationFinal, E.IsFinal as FamilyFinal, F.IsFinal as MilitaryFinal, G.IsFinal as ExperienceFinal,
			 H.IsFinal as ContactFinal, I.IsFinal as BackgroundFinal, 
			 --cast(case when L.cnt = 8 then 1 else 0 end as bit) as CandidateApp,
			 cast(case when L.cnt is null or (isnull(L.cnt, 0) <> 0)  then 0 else 1 end as bit) as CandidateApp,
			 --кандидат полностью заполнил анкету
			 cast(case when B.IsFinal = 1 and C.IsFinal = 1 and D.IsFinal = 1 and E.IsFinal = 1 and F.IsFinal = 1 and G.IsFinal = 1 and H.IsFinal = 1 and I.IsFinal = 1 and A.IsScanFinal = 1 then 1 else 0 end as bit) as CandidateReady,
			 --согласование
			 I.ApprovalStatus as BackgroundApproval, J.IsComplete as TrainingApproval, K.HigherManagerApprovalStatus as ManagerApproval, 
			 I.PrevApprovalStatus as PrevBackgroundApproval,
			 cast(case when A.Status = 7 or A.Status = 8 then 1 else 0 end as bit) as PersonnelManagerApproval
FROM EmploymentCandidate as A
INNER JOIN GeneralInfo as B ON B.Id = A.GeneralInfoId
INNER JOIN Passport as C ON C.Id = A.PassportId
INNER JOIN Education as D ON D.Id = A.EducationId
INNER JOIN Family as E ON E.Id = A.FamilyId
INNER JOIN MilitaryService as F ON F.Id = A.MilitaryServiceId
INNER JOIN Experience as G ON G.Id = A.ExperienceId
INNER JOIN Contacts as H ON H.Id = A.ContactsId
INNER JOIN BackgroundCheck as I ON I.Id = A.BackgroundCheckId
INNER JOIN OnsiteTraining as J ON J.Id = A.OnsiteTrainingId
INNER JOIN Managers as K ON K.Id = A.ManagersId
LEFT JOIN (SELECT A.CandidateId, A.cnt - isnull(B.cnt, 0) as cnt 
					FROM (SELECT CandidateId, count(CandidateId) as cnt FROM EmploymentCandidateDocNeeded WHERE IsNeeded = 1 GROUP BY CandidateId) as A
					LEFT JOIN (SELECT B.CandidateId, count(B.CandidateId) as cnt
										FROM RequestAttachment as A
										INNER JOIN EmploymentCandidateDocNeeded as B ON B.CandidateId = A.RequestId and B.DocTypeId = A.RequestType and B.IsNeeded = 1
										GROUP BY B.CandidateId) as B ON B.CandidateId = A.CandidateId) as L ON L.CandidateId = A.Id	
GO


IF OBJECT_ID ('vwEmploymentPositions', 'V') IS NOT NULL
	DROP VIEW [dbo].[vwEmploymentPositions]
GO



--достаем комментарии к разделам приема
CREATE VIEW [dbo].[vwEmploymentPositions]
AS
SELECT Id, Name + case when IsMarkedToRemoval = 1 then ' (не использовать)' else '' end as Name
FROM dbo.Position



GO




--ПРЕДСТАВЛЕНИЯ КОНЕЦ

--ПЕРВИЧНЫЕ ДАННЫЕ ДЛЯ СПРАВОЧНИКОВ - НАЧАЛО

		--Country	 (есть данные) (убрать Франция, Израиль, Туркменистан, Узбекистан)
		INSERT INTO dbo.Country(Version, Code, Name) VALUES(1, N'112', N'БЕЛАРУСЬ')
		INSERT INTO dbo.Country(Version, Code, Name) VALUES(1, N'398', N'КАЗАХСТАН')
		INSERT INTO dbo.Country(Version, Code, Name) VALUES(1, N'643', N'РОССИЯ')
		INSERT INTO dbo.Country(Version, Code, Name) VALUES(1, N'804', N'УКРАИНА')

		--DisabilityDegree (есть данные)
		INSERT INTO dbo.DisabilityDegree(Version, Name) VALUES(1, N'I')
		INSERT INTO dbo.DisabilityDegree(Version, Name) VALUES(1, N'II')
		INSERT INTO dbo.DisabilityDegree(Version, Name) VALUES(1, N'III')

		--DocumentType (есть данные)
		INSERT INTO dbo.DocumentType(Version, Code, Name) VALUES(1, N'21', N'Паспорт гражданина Российской Федерации')
		INSERT INTO dbo.DocumentType(Version, Code, Name) VALUES(1, N'12', N'Вид на жительство')
		INSERT INTO dbo.DocumentType(Version, Code, Name) VALUES(1, N'27', N'Военный билет офицера запаса')
		INSERT INTO dbo.DocumentType(Version, Code, Name) VALUES(1, N'7', N'Военный билет солдата (матроса, сержанта, старшины)')
		INSERT INTO dbo.DocumentType(Version, Code, Name) VALUES(1, N'14', N'Временное удостоверение личности гражданина Российской Федерации')
		INSERT INTO dbo.DocumentType(Version, Code, Name) VALUES(1, N'9', N'Дипломатический паспорт гражданина Российской Федерации')
		INSERT INTO dbo.DocumentType(Version, Code, Name) VALUES(1, N'22', N'Загранпаспорт гражданина Российской Федерации')
		INSERT INTO dbo.DocumentType(Version, Code, Name) VALUES(1, N'2', N'Загранпаспорт гражданина СССР')
		INSERT INTO dbo.DocumentType(Version, Code, Name) VALUES(1, N'91', N'Иные документы')
		INSERT INTO dbo.DocumentType(Version, Code, Name) VALUES(1, N'1', N'Паспорт гражданина СССР')
		INSERT INTO dbo.DocumentType(Version, Code, Name) VALUES(1, N'10', N'Паспорт иностранного гражданина')
		INSERT INTO dbo.DocumentType(Version, Code, Name) VALUES(1, N'6', N'Паспорт Минморфлота')
		INSERT INTO dbo.DocumentType(Version, Code, Name) VALUES(1, N'26', N'Паспорт моряка')
		INSERT INTO dbo.DocumentType(Version, Code, Name) VALUES(1, N'15', N'Разрешение на временное проживание в Российской Федерации')
		INSERT INTO dbo.DocumentType(Version, Code, Name) VALUES(1, N'11', N'Свидетельство о рассмотрении ходатайства о признании беженцем на территории Российской Федерации')
		INSERT INTO dbo.DocumentType(Version, Code, Name) VALUES(1, N'3', N'Свидетельство о рождении')
		INSERT INTO dbo.DocumentType(Version, Code, Name) VALUES(1, N'23', N'Свидетельство о рождении, выданное уполномоченным органом иностранного государства')
		INSERT INTO dbo.DocumentType(Version, Code, Name) VALUES(1, N'5', N'Справка об освобождении из  места лишения свободы')
		INSERT INTO dbo.DocumentType(Version, Code, Name) VALUES(1, N'13', N'Удостоверение беженца в Российской Федерации')
		INSERT INTO dbo.DocumentType(Version, Code, Name) VALUES(1, N'4', N'Удостоверение личности офицера')


		--EmploymentEducationType	 (есть данные) (убрать Дошкольное образование, неполное высшее образование)
		INSERT INTO dbo.EmploymentEducationType(Version, Code, Name, Priority) VALUES(1, N'02', N'Начальное (общее) образование', 1)
		INSERT INTO dbo.EmploymentEducationType(Version, Code, Name, Priority) VALUES(1, N'03', N'Основное общее образование', 2)
		INSERT INTO dbo.EmploymentEducationType(Version, Code, Name, Priority) VALUES(1, N'07', N'Среднее (полное) общее образование', 3)
		INSERT INTO dbo.EmploymentEducationType(Version, Code, Name, Priority) VALUES(1, N'10', N'Начальное профессиональное образование', 4)
		INSERT INTO dbo.EmploymentEducationType(Version, Code, Name, Priority) VALUES(1, N'11', N'Среднее профессиональное образование', 5)
		INSERT INTO dbo.EmploymentEducationType(Version, Code, Name, Priority) VALUES(1, N'18', N'Высшее образование', 6)
		INSERT INTO dbo.EmploymentEducationType(Version, Code, Name, Priority) VALUES(1, N'19', N'Послевузовское образование', 7)

		--FamilyStatus (есть данные)
		INSERT INTO dbo.FamilyStatus(Version, Code, Name) VALUES(1, N'4', N'Вдовец (вдова)')
		INSERT INTO dbo.FamilyStatus(Version, Code, Name) VALUES(1, N'1', N'Никогда не состоял (не состояла в браке)')
		INSERT INTO dbo.FamilyStatus(Version, Code, Name) VALUES(1, N'5', N'Разведен (разведена)')
		INSERT INTO dbo.FamilyStatus(Version, Code, Name) VALUES(1, N'6', N'Разошелся (разошлась)')
		INSERT INTO dbo.FamilyStatus(Version, Code, Name) VALUES(1, N'7', N'Состоит в зарегистрированном браке')
		INSERT INTO dbo.FamilyStatus(Version, Code, Name) VALUES(1, N'3', N'Состоит в незарегистрированном браке')

		--MilitaryRanks (есть данные)
		INSERT INTO dbo.MilitaryRanks(Version, Name) VALUES(1, N'Рядовой')
		INSERT INTO dbo.MilitaryRanks(Version, Name) VALUES(1, N'Матрос')
		INSERT INTO dbo.MilitaryRanks(Version, Name) VALUES(1, N'Ефрейтор')
		INSERT INTO dbo.MilitaryRanks(Version, Name) VALUES(1, N'Старший матрос')
		INSERT INTO dbo.MilitaryRanks(Version, Name) VALUES(1, N'Младший сержант')
		INSERT INTO dbo.MilitaryRanks(Version, Name) VALUES(1, N'Старшина 2-й статьи')
		INSERT INTO dbo.MilitaryRanks(Version, Name) VALUES(1, N'Сержант')
		INSERT INTO dbo.MilitaryRanks(Version, Name) VALUES(1, N'Старшина 1-й статьи')
		INSERT INTO dbo.MilitaryRanks(Version, Name) VALUES(1, N'Старший сержант')
		INSERT INTO dbo.MilitaryRanks(Version, Name) VALUES(1, N'Главный старшина')
		INSERT INTO dbo.MilitaryRanks(Version, Name) VALUES(1, N'Старшина')
		INSERT INTO dbo.MilitaryRanks(Version, Name) VALUES(1, N'Главный корабельный старшина')
		INSERT INTO dbo.MilitaryRanks(Version, Name) VALUES(1, N'Прапорщик')
		INSERT INTO dbo.MilitaryRanks(Version, Name) VALUES(1, N'Мичман')
		INSERT INTO dbo.MilitaryRanks(Version, Name) VALUES(1, N'Старший прапорщик')
		INSERT INTO dbo.MilitaryRanks(Version, Name) VALUES(1, N'Старший мичман')
		INSERT INTO dbo.MilitaryRanks(Version, Name) VALUES(1, N'Младший лейтенант')
		INSERT INTO dbo.MilitaryRanks(Version, Name) VALUES(1, N'Лейтенант')
		INSERT INTO dbo.MilitaryRanks(Version, Name) VALUES(1, N'Старший лейтенант')
		INSERT INTO dbo.MilitaryRanks(Version, Name) VALUES(1, N'Капитан')
		INSERT INTO dbo.MilitaryRanks(Version, Name) VALUES(1, N'Капитан-лейтенант')
		INSERT INTO dbo.MilitaryRanks(Version, Name) VALUES(1, N'Майор')
		INSERT INTO dbo.MilitaryRanks(Version, Name) VALUES(1, N'Капитан 3 ранга')
		INSERT INTO dbo.MilitaryRanks(Version, Name) VALUES(1, N'Подполковник')
		INSERT INTO dbo.MilitaryRanks(Version, Name) VALUES(1, N'Капитан 2 ранга')
		INSERT INTO dbo.MilitaryRanks(Version, Name) VALUES(1, N'Полковник')
		INSERT INTO dbo.MilitaryRanks(Version, Name) VALUES(1, N'Капитан 1 ранга')
		INSERT INTO dbo.MilitaryRanks(Version, Name) VALUES(1, N'Генерал-майор')
		INSERT INTO dbo.MilitaryRanks(Version, Name) VALUES(1, N'Контр-адмирал')
		INSERT INTO dbo.MilitaryRanks(Version, Name) VALUES(1, N'Генерал-лейтенант')
		INSERT INTO dbo.MilitaryRanks(Version, Name) VALUES(1, N'Вице-адмирал')
		INSERT INTO dbo.MilitaryRanks(Version, Name) VALUES(1, N'Генерал-полковник')
		INSERT INTO dbo.MilitaryRanks(Version, Name) VALUES(1, N'Адмирал')
		INSERT INTO dbo.MilitaryRanks(Version, Name) VALUES(1, N'Генерал армии')
		INSERT INTO dbo.MilitaryRanks(Version, Name) VALUES(1, N'Адмирал флота')
		INSERT INTO dbo.MilitaryRanks(Version, Name) VALUES(1, N'Маршал РФ')

		--MilitaryRelationAccount (есть данные)
		INSERT INTO dbo.MilitaryRelationAccount(Version, Name) VALUES(1, N'Состоит на воинском учете')
		INSERT INTO dbo.MilitaryRelationAccount(Version, Name) VALUES(1, N'Встает на воинский учет')
		INSERT INTO dbo.MilitaryRelationAccount(Version, Name) VALUES(1, N'Не состоит на воинском учете (но должен)')
		INSERT INTO dbo.MilitaryRelationAccount(Version, Name) VALUES(1, N'Снят с воинского учета по возрасту')
		INSERT INTO dbo.MilitaryRelationAccount(Version, Name) VALUES(1, N'Снят с воинского учета по состоянию здоровья')

		--MilitarySpecialityCategory (есть данные)
		INSERT INTO dbo.MilitarySpecialityCategory(Version, Name) VALUES(1, N'командный')
		INSERT INTO dbo.MilitarySpecialityCategory(Version, Name) VALUES(1, N'инженерно-технический')
		INSERT INTO dbo.MilitarySpecialityCategory(Version, Name) VALUES(1, N'военно-гуманитарный')
		INSERT INTO dbo.MilitarySpecialityCategory(Version, Name) VALUES(1, N'педагогический')
		INSERT INTO dbo.MilitarySpecialityCategory(Version, Name) VALUES(1, N'юридический')
		INSERT INTO dbo.MilitarySpecialityCategory(Version, Name) VALUES(1, N'медицинский')
		INSERT INTO dbo.MilitarySpecialityCategory(Version, Name) VALUES(1, N'ветеринарный')
		INSERT INTO dbo.MilitarySpecialityCategory(Version, Name) VALUES(1, N'экономический (финансовый)')
		INSERT INTO dbo.MilitarySpecialityCategory(Version, Name) VALUES(1, N'солдаты и матросы')
		INSERT INTO dbo.MilitarySpecialityCategory(Version, Name) VALUES(1, N'сержанты и старшины')
		INSERT INTO dbo.MilitarySpecialityCategory(Version, Name) VALUES(1, N'прапорщики и мичманы')
		INSERT INTO dbo.MilitarySpecialityCategory(Version, Name) VALUES(1, N'младшие офицеры')
		INSERT INTO dbo.MilitarySpecialityCategory(Version, Name) VALUES(1, N'старшие офицеры')
		INSERT INTO dbo.MilitarySpecialityCategory(Version, Name) VALUES(1, N'высшие офицеры')

		--MilitaryValidityCategory (есть данные)
		INSERT INTO dbo.MilitaryValidityCategory(Version, Name) VALUES(1, N'А - годен к военной службе')
		INSERT INTO dbo.MilitaryValidityCategory(Version, Name) VALUES(1, N'Б - годен к военной службе с незначительными ограничениями')
		INSERT INTO dbo.MilitaryValidityCategory(Version, Name) VALUES(1, N'В - ограниченно годен к военной службе')
		INSERT INTO dbo.MilitaryValidityCategory(Version, Name) VALUES(1, N'Г - временно не годен к военной службе')
		INSERT INTO dbo.MilitaryValidityCategory(Version, Name) VALUES(1, N'Д - не годен к военной службе')

		--AccessGroup (есть данные)
		INSERT INTO dbo.AccessGroup(Version, Code, Name) VALUES(1, N'000052', N'GE (734)')
		INSERT INTO dbo.AccessGroup(Version, Code, Name) VALUES(1, N'000017', N'Дв Амурская обл. (163)')
		INSERT INTO dbo.AccessGroup(Version, Code, Name) VALUES(1, N'000022', N'Дв Еврейская автономная обл. (17)')
		INSERT INTO dbo.AccessGroup(Version, Code, Name) VALUES(1, N'000025', N'Дв Камчатский край (42)')
		INSERT INTO dbo.AccessGroup(Version, Code, Name) VALUES(1, N'000060', N'ДВ Магаданской обл.')
		INSERT INTO dbo.AccessGroup(Version, Code, Name) VALUES(1, N'000035', N'Дв Приморский край (456)')
		INSERT INTO dbo.AccessGroup(Version, Code, Name) VALUES(1, N'000061', N'ДВ Саха (Якутия)')
		INSERT INTO dbo.AccessGroup(Version, Code, Name) VALUES(1, N'000039', N'Дв Сахалинская обл. (54)')
		INSERT INTO dbo.AccessGroup(Version, Code, Name) VALUES(1, N'000043', N'Дв Хабаровский край (180) ')
		INSERT INTO dbo.AccessGroup(Version, Code, Name) VALUES(1, N'000024', N'Кр Иркутская обл. (400)')
		INSERT INTO dbo.AccessGroup(Version, Code, Name) VALUES(1, N'000029', N'Кр Красноярский край (409)')
		INSERT INTO dbo.AccessGroup(Version, Code, Name) VALUES(1, N'000037', N'Кр Республика Хакасия (100)')
		INSERT INTO dbo.AccessGroup(Version, Code, Name) VALUES(1, N'000042', N'Кр Усть-Ордынский Бурятский Автономный Округ (1)')
		INSERT INTO dbo.AccessGroup(Version, Code, Name) VALUES(1, N'000018', N'Кс Владимирская обл. (132)')
		INSERT INTO dbo.AccessGroup(Version, Code, Name) VALUES(1, N'000019', N'Кс Вологодская обл. (65)')
		INSERT INTO dbo.AccessGroup(Version, Code, Name) VALUES(1, N'000020', N'Кс Воронежская обл. (127)')
		INSERT INTO dbo.AccessGroup(Version, Code, Name) VALUES(1, N'000051', N'Кс г. Казань (72)')
		INSERT INTO dbo.AccessGroup(Version, Code, Name) VALUES(1, N'000023', N'Кс Ивановская обл. (125)')
		INSERT INTO dbo.AccessGroup(Version, Code, Name) VALUES(1, N'000027', N'Кс Костромская обл. (400)')
		INSERT INTO dbo.AccessGroup(Version, Code, Name) VALUES(1, N'000028', N'Кс Краснодарский край (517)')
		INSERT INTO dbo.AccessGroup(Version, Code, Name) VALUES(1, N'000046', N'Кс Липецкая обл. (48)')
		INSERT INTO dbo.AccessGroup(Version, Code, Name) VALUES(1, N'000031', N'Кс Нижегородская обл. (44)')
		INSERT INTO dbo.AccessGroup(Version, Code, Name) VALUES(1, N'000047', N'Кс Республика Башкортостан (11)')
		INSERT INTO dbo.AccessGroup(Version, Code, Name) VALUES(1, N'000054', N'Кс Республика Татарстан (3)')
		INSERT INTO dbo.AccessGroup(Version, Code, Name) VALUES(1, N'000038', N'Кс Самарская обл. (91)')
		INSERT INTO dbo.AccessGroup(Version, Code, Name) VALUES(1, N'000048', N'Кс Тульская обл. (12)')
		INSERT INTO dbo.AccessGroup(Version, Code, Name) VALUES(1, N'000049', N'Кс Ульяновская обл. (7)')
		INSERT INTO dbo.AccessGroup(Version, Code, Name) VALUES(1, N'000045', N'Кс Ярославская обл. (95)')
		INSERT INTO dbo.AccessGroup(Version, Code, Name) VALUES(1, N'000021', N'Мск г. Москва (563)')
		INSERT INTO dbo.AccessGroup(Version, Code, Name) VALUES(1, N'000053', N'Мск г. Санкт-Петербург (17)')
		INSERT INTO dbo.AccessGroup(Version, Code, Name) VALUES(1, N'000062', N'Мск Калужская область')
		INSERT INTO dbo.AccessGroup(Version, Code, Name) VALUES(1, N'000030', N'Мск Московская обл. (51)')
		INSERT INTO dbo.AccessGroup(Version, Code, Name) VALUES(1, N'000016', N'Нс Алтайский край (446)')
		INSERT INTO dbo.AccessGroup(Version, Code, Name) VALUES(1, N'000026', N'Нс Кемеровская обл. (432)')
		INSERT INTO dbo.AccessGroup(Version, Code, Name) VALUES(1, N'000032', N'Нс Новосибирская обл. (1057)')
		INSERT INTO dbo.AccessGroup(Version, Code, Name) VALUES(1, N'000033', N'Нс Омская обл. (237)')
		INSERT INTO dbo.AccessGroup(Version, Code, Name) VALUES(1, N'000034', N'Нс Оренбургская обл. (258)')
		INSERT INTO dbo.AccessGroup(Version, Code, Name) VALUES(1, N'000036', N'Нс Республика Алтай (23)')
		INSERT INTO dbo.AccessGroup(Version, Code, Name) VALUES(1, N'000050', N'Нс Свердловская обл. (3)')
		INSERT INTO dbo.AccessGroup(Version, Code, Name) VALUES(1, N'000040', N'Нс Томская обл. (171)')
		INSERT INTO dbo.AccessGroup(Version, Code, Name) VALUES(1, N'000041', N'Нс Тюменская обл. (104)')
		INSERT INTO dbo.AccessGroup(Version, Code, Name) VALUES(1, N'000044', N'Нс Челябинская обл. (295)')

		--InsuredPersonType (есть данные)
		INSERT INTO dbo.InsuredPersonType(Version, Code, Name) VALUES(1, N'1', N'ГражданеРФ')
		INSERT INTO dbo.InsuredPersonType(Version, Code, Name) VALUES(1, N'2', N'Иностранные граждане, постоянно проживающие на территории РФ')
		INSERT INTO dbo.InsuredPersonType(Version, Code, Name) VALUES(1, N'3', N'Иностранные граждане, временно проживающие на территории РФ')
		INSERT INTO dbo.InsuredPersonType(Version, Code, Name) VALUES(1, N'4', N'Иностранные граждане, временно пребывающие на территории РФ, без долгосрочных трудовых договоров')
		INSERT INTO dbo.InsuredPersonType(Version, Code, Name) VALUES(1, N'5', N'Признанные беженцами иностранные граждане, временно пребывающие на территории РФ, без долгосрочных трудовых договоров')
		INSERT INTO dbo.InsuredPersonType(Version, Code, Name) VALUES(1, N'6', N'Иностранные граждане, временно пребывающие на территории РФ, с которыми заключены долгосрочные трудовые договоры')
		INSERT INTO dbo.InsuredPersonType(Version, Code, Name) VALUES(1, N'7', N'Высококвалифицированные иностранные специалисты и члены их семьи, постоянно проживающие на территории РФ')
		INSERT INTO dbo.InsuredPersonType(Version, Code, Name) VALUES(1, N'8', N'Высококвалифицированные иностранные специалисты и члены их семьи, временно проживающие на территории РФ')
		INSERT INTO dbo.InsuredPersonType(Version, Code, Name) VALUES(1, N'9', N'Иностранные граждане, временно пребывающие на территории РФ, не подлежащие страхованию')

		--PersonalAccountContractor (есть данные)
		INSERT INTO dbo.PersonalAccountContractor(Version, Code, Name) VALUES(1, N'00150', N'Нерезидент МФ #3')
		INSERT INTO dbo.PersonalAccountContractor(Version, Code, Name) VALUES(1, N'00151', N'Нерезидент ЦФ #4')
		INSERT INTO dbo.PersonalAccountContractor(Version, Code, Name) VALUES(1, N'000000001', N'МФ #1')
		INSERT INTO dbo.PersonalAccountContractor(Version, Code, Name) VALUES(1, N'000000011', N'ЦФ #2')

		--Schedule (есть данные)
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'f99856a6-b750-11df-2d9b-003048ba0539', N'0.8 ')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'fc58af06-9eb1-11df-5d87-00304861d218', N'16,8 ч (0,42 ставки)')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'2ac389d7-9921-11e3-80c7-002590d1e727', N'0,075')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'ada03614-b659-11e3-80c8-002590d1e727', N'0,025')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'73f04627-cece-11e3-80c8-002590d1e727', N'0,22 ставки')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'f4d4171a-1a2c-11e4-80c8-002590d1e727', N'0,975 ставки')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'ae226b18-52e0-11e4-80c8-002590d1e727', N'0,045 ставки')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'66d0308d-6a5f-11e4-80d1-002590d1e727', N'6,0часов(0.15ст)')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'e46671f8-140d-11e1-81fb-003048ba0538', N'0,7 ставки (28 часов)')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'f6bd90b2-03d6-11e3-8233-003048ba0538', N'0.63 ставки')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'1583436d-8c6e-11de-839b-00304861d218', N'12 ч (0,3ставки)')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'febaac53-e21c-11dd-8574-00304861d218', N'график 2 через 2 по 11 часов 1смена')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'febaac54-e21c-11dd-8574-00304861d218', N'график 2 через 2 2смена')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'8c9e6dc5-6129-11de-86d4-00304861d218', N'1 час ')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'd906b1b2-61f0-11de-86d4-00304861d218', N'0,16')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'b042393a-c242-11e0-87d1-003048ba0538', N'0,2 ставки')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'3162fd27-c820-11dd-87ea-00304861d218', N'Пятидневка 20 часов')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'71d159a1-5a0d-11e2-8a08-003048ba0538', N'0,45 ставки')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'f7aa8f0c-bf2f-11e1-8aef-003048ba0538', N'0,05')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'd0b82d2e-ce8d-434d-8c81-1c3913800dc7', N'35 часов (0,875ст.)')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'521ce3a4-d4c2-11de-8ca7-00304861d219', N'14ч (0,35 ставки)')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'63c22fd6-eced-4d28-8da9-069188cdf26c', N' Пятидневка   0,2 ставки')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'acb3070b-6bf0-11e1-8ec0-003048ba0538', N'0,55 ставки')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'b26e39cc-bc37-11e0-8fd0-003048ba0538', N'0,01')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'756a6d55-2f03-11de-9079-00304861d218', N'Основной')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'5008e5fa-7283-11de-947f-00304861d218', N'39.6 часа ( 0.99ст.)')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'f73ab4b8-1c39-11df-9641-00304861d219', N'10 часов (0,25)')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'ec155a9c-6cb3-11e1-96a3-003048ba0538', N'0,68 ставки (27,2 часа)')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'61c4a9ce-518a-11de-9784-00304861d218', N'32 часа ( 0.8ст.)')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'61c4a9d6-518a-11de-9784-00304861d218', N'29,2 часа (0.73 ст.)')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'c0dded9a-56f9-11de-9784-00304861d218', N'36,4 часа ( 0,91ст.)')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'c0dded9b-56f9-11de-9784-00304861d218', N'34,8 часа ( 0,87 ст.)')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'c0dded9c-56f9-11de-9784-00304861d218', N'36.8 часа  (0,92ст.)')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'c0ddeda2-56f9-11de-9784-00304861d218', N'33.6 часа ( 0.84ст.)')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'bce58c8b-5973-11de-9784-00304861d218', N'30 (0,75ст)')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'05006ae7-5af8-11de-9784-00304861d218', N'38,4 часа (0,96ст)')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'fbcb3980-a8aa-11de-995b-00304861d218', N'Суммированный учет')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'cfa8292d-53ab-11dd-9b24-0010dcc22269', N'Основной график 40часов неделя')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'de51bdda-7bea-11de-9c3b-00304861d218', N'24 ч(0,6ст)')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'b9b91cc4-44e3-11de-9cd9-00304861d218', N'0,8 часов')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'c8f5409c-4512-11de-9cd9-00304861d218', N'4 часа (0,1 ст)')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'7e4d4f62-0cb9-11e0-9fcb-003048ba0538', N'Пятидневка 0.4ст')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'1dd16838-5a44-11de-a159-003048359abd', N'Пятидневка 20 часов')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'ce213082-5fe4-11e2-a43a-003048ba0538', N'Шестидневка')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'ffdfa5b5-6e88-11dd-a46b-00304861d218', N'Основной график 20 часов')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'cd0cecb3-6f55-11dd-a46b-00304861d218', N'Основной график 20часов неделя (Иваново)')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'50398604-8878-11de-a46e-003048359abd', N'Пятидневка 32 часа')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'99320ebb-5484-11dd-a7f9-0008a1a11604', N'Основной график')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'99320edd-5484-11dd-a7f9-0008a1a11604', N'Сум. учет раб. времени')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'799797be-dcfd-11df-ab02-003048ba0538', N'Пятидневка 30 часов')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'99b7f7c2-7b6b-11de-ad67-003048359abd', N'10-часовая')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'1692bfc9-8b8a-11e2-aec5-003048ba0538', N'36 часов (0,9 ст.)')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'b149c25e-ae9a-11df-af89-003048ba0539', N'Пятидневка (30 часов) ')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'8856fcc0-d13e-11dd-b086-00308d000000', N'Основной график (Ярославль)')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'd74d4e81-f80c-11e2-b1b0-003048ba0538', N'0,44 ставки')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'b640afc7-f4b7-11e0-b3a0-003048ba0538', N'0,1')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'e3acbc3a-ded4-11de-b424-00304861d219', N'20 часов (0,5 ст.)')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'b88e0281-4fec-11e3-b49b-003048ba0538', N'15 часов (0,38 ст.)')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'7777d5c2-75cd-11de-b4c1-00304861d218', N'25ч (0,625)')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'6f224468-7670-11de-b4c1-00304861d218', N'0,01')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'bfab25e3-7825-11de-b4c1-00304861d218', N'0,001')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'b2783d7a-d503-11de-b4ed-003048359abd', N'пятидневка 10часов (Иваново)')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'60f2719a-e959-11de-b4ed-003048359abd', N'Сокращенный день по 7 часов (Ярославль)')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'0033bf3b-eb11-11de-b4ed-003048359abd', N'Пятидневка 35 часов (Иваново)')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'14cdc40f-eb7f-11dd-b538-00304861d218', N'График 15ч/н (Ярославль)')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'14cdc410-eb7f-11dd-b538-00304861d218', N'График 25ч/н (Ярославль)')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'e45f5464-c4be-4957-b6e9-5d878aacce67', N'37,5 часов (0,94 ст)')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'bfbed33a-a80e-11de-b733-003048359abd', N'Пятидневка 37,5')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'91f99117-c9d5-11de-b733-003048359abd', N'Пятидневка 35')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'7dd7f374-e861-11e2-b868-003048ba0538', N'0,86ставки')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'9e15309f-3397-11dd-b8e4-00304861d218', N'Сменный')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'7b0b10df-a356-11de-b935-00304861d218', N'16 ч (0,4ставки)')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'e1f76452-2b01-11dd-bc32-00304861d218', N'Пятидневка')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'13e773f9-4129-11de-bf01-00304861d218', N'20 часов')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'f1bfede7-4423-11de-bf01-00304861d218', N'35,6 часа (0,89ст.)')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'9bf8e0a9-a1b5-11de-bff9-00304861d218', N'6,4 часа (0,16 ст)')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'9bf8e0aa-a1b5-11de-bff9-00304861d218', N'6,8 часа (0,17 ст)')

		--Signer
		INSERT INTO Signer(Version, Name, PreamblePartyTemplate,Position)
		VALUES (1, N'Баграновский Сергей Юрьевич', N'Регионального директора Дальневосточной дирекции Филиала "Центральный" ПАО "СОВКОМБАНК" Баграновского Сергея Юрьевича, действующего на основании Доверенности № 6/ФЦ от 16.09.2014г.', N'Региональный директор Дальневосточной дирекции Филиала "Центральный" ПАО "СОВКОМБАНК"')

		INSERT INTO Signer(Version, Name, PreamblePartyTemplate,Position)
		VALUES (1, N'Милованова Елена Николаевна', N'специалиста управления кадрового делопроизводства и учета ПАО "СОВКОМБАНК" Миловановой Елены Николаевны, действующей на основании Доверенности № 8 от 15.01.2015 г.', N'Специалист управления кадрового делопроизводства и учета ПАО "СОВКОМБАНК"')


		--PersonnelOrderExtraCharges
		INSERT INTO PersonnelOrderExtraCharges(Version, Code1C, Name)	VALUES(1, N'1', N'Группа 1 обычная')
		INSERT INTO PersonnelOrderExtraCharges(Version, Code1C, Name)	VALUES(1, N'2', N'Группа 2 обычная')
		INSERT INTO PersonnelOrderExtraCharges(Version, Code1C, Name)	VALUES(1, N'3', N'Группа 3 обычная')
		INSERT INTO PersonnelOrderExtraCharges(Version, Code1C, Name)	VALUES(1, N'4', N'Группа 4 обычная')
		INSERT INTO PersonnelOrderExtraCharges(Version, Code1C, Name)	VALUES(1, N'5', N'Группа 1 льготная')
		INSERT INTO PersonnelOrderExtraCharges(Version, Code1C, Name)	VALUES(1, N'6', N'Группа 2 льготная')
		INSERT INTO PersonnelOrderExtraCharges(Version, Code1C, Name)	VALUES(1, N'7', N'Группа 3 льготная')
		INSERT INTO PersonnelOrderExtraCharges(Version, Code1C, Name)	VALUES(1, N'8', N'Группа 4 льготная')
		

--ПЕРВИЧНЫЕ ДАННЫЕ ДЛЯ СПРАВОЧНИКОВ - КОНЕЦ
--ФУНКЦИИ НАЧАЛО
IF OBJECT_ID ('fnGetEmploymentAttachmentList', 'TF') IS NOT NULL
	DROP FUNCTION [dbo].[fnGetEmploymentAttachmentList]
GO



CREATE FUNCTION [dbo].[fnGetEmploymentAttachmentList]
(
	@CandidateId int
)
RETURNS 
@ReturnTable TABLE 
(
	AttachmentTypeName nvarchar(150),
	AtachmentAvalable nvarchar(10)
)
AS
BEGIN
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 201 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Фото кандидата', 'да' FROM RequestAttachment WHERE RequestType = 201 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Фото кандидата', 'нет' 
	END

	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 202 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан ИНН', 'да' FROM RequestAttachment WHERE RequestType = 202 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан ИНН', 'нет' 
	END
	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 203 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан СНИЛС', 'да' FROM RequestAttachment WHERE RequestType = 203 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан СНИЛС', 'нет' 
	END
	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 204 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан справки об инвалидности', 'да' FROM RequestAttachment WHERE RequestType = 204 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан справки об инвалидности', 'нет' 
	END

	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 211 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан паспорта', 'да' FROM RequestAttachment WHERE RequestType = 211 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан паспорта', 'нет' 
	END

	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 221 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан документа об образовании', 'да' FROM RequestAttachment WHERE RequestType = 221 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан документа об образовании', 'нет' 
	END

	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 222 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан документа о послевузовском образовании', 'да' FROM RequestAttachment WHERE RequestType = 222 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан документа о послевузовском образовании', 'нет' 
	END

	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 223 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан документа о дополнительном образовании', 'да' FROM RequestAttachment WHERE RequestType = 223 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан документа о дополнительном образовании', 'нет' 
	END
	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 224 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан документа о повышении квалификации', 'да' FROM RequestAttachment WHERE RequestType = 224 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан документа о повышении квалификации', 'нет' 
	END
	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 231 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан свидетельства о браке', 'да' FROM RequestAttachment WHERE RequestType = 231 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан свидетельства о браке', 'нет' 
	END
	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 232 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан свидетельств о рождении детей', 'да' FROM RequestAttachment WHERE RequestType = 232 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан свидетельств о рождении детей', 'нет' 
	END
	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 241 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан военного билета', 'да' FROM RequestAttachment WHERE RequestType = 241 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан военного билета', 'нет' 
	END
	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 242 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан мобилизационного талона', 'да' FROM RequestAttachment WHERE RequestType = 242 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан мобилизационного талона', 'нет' 
	END
	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 251 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан трудовой книжки', 'да' FROM RequestAttachment WHERE RequestType = 251 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан трудовой книжки', 'нет' 
	END
	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 252 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан вкладыша в трудовую книжку', 'да' FROM RequestAttachment WHERE RequestType = 252 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан вкладыша в трудовую книжку', 'нет' 
	END
	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 261 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан рукописного текста согласия на обработку персональных данных', 'да' FROM RequestAttachment WHERE RequestType = 261 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан рукописного текста согласия на обработку персональных данных', 'нет' 
	END
	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 262 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан рукописного текста о достоверности сведений', 'да' FROM RequestAttachment WHERE RequestType = 262 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан рукописного текста о достоверности сведений', 'нет' 
	END
	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 271 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан заявления о приеме на работу', 'да' FROM RequestAttachment WHERE RequestType = 271 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан заявления о приеме на работу', 'нет' 
	END
	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 272 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан трудового договора', 'да' FROM RequestAttachment WHERE RequestType = 272 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан трудового договора', 'нет' 
	END
	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 273 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан приказа о приеме', 'да' FROM RequestAttachment WHERE RequestType = 273 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан приказа о приеме', 'нет' 
	END
	/*
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 274 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан Т2', 'да' FROM RequestAttachment WHERE RequestType = 274 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан Т2', 'нет' 
	END
	*/
	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 275 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан договора о материальной ответственности', 'да' FROM RequestAttachment WHERE RequestType = 275 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан договора о материальной ответственности', 'нет' 
	END
	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 276 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан ДС персональные данные', 'да' FROM RequestAttachment WHERE RequestType = 276 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан ДС персональные данные', 'нет' 
	END
	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 277 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан обязательства конфеденциальности', 'да' FROM RequestAttachment WHERE RequestType = 277 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан обязательства конфеденциальности', 'нет' 
	END
	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 278 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан листка по учету кадров', 'да' FROM RequestAttachment WHERE RequestType = 278 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан листка по учету кадров', 'нет' 
	END
	/*
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 279 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан реестра личного дела', 'да' FROM RequestAttachment WHERE RequestType = 279 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан реестра личного дела', 'нет' 
	END
	*/
	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 280 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан памятки сотруднику о сохранении коммерческой, банковской и служебной тайны', 'да' FROM RequestAttachment WHERE RequestType = 280 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан памятки сотруднику о сохранении коммерческой, банковской и служебной тайны', 'нет' 
	END
	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 281 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан инструкции по обеспечению сохранности сведений, составляющих коммерческую, и служебную тайну', 'да' FROM RequestAttachment WHERE RequestType = 281 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан инструкции по обеспечению сохранности сведений, составляющих коммерческую, и служебную тайну', 'нет' 
	END
	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 282 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан согласия физического лица на проверку персональных данных (Приложение №3)', 'да' FROM RequestAttachment WHERE RequestType = 282 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан согласия физического лица на проверку персональных данных (Приложение №3)', 'нет' 
	END
	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 283 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан порядка по исполнению требований при организации кассовой работы сотрудниками ВСП (Приложение 1)', 'да' FROM RequestAttachment WHERE RequestType = 283 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан порядка по исполнению требований при организации кассовой работы сотрудниками ВСП (Приложение 1)', 'нет' 
	END
	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 284 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан порядка по обслуживанию клиентов в кассе сотрудниками ВСП (Приложение 2)', 'да' FROM RequestAttachment WHERE RequestType = 284 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан порядка по обслуживанию клиентов в кассе сотрудниками ВСП (Приложение 2)', 'нет' 
	END
	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 285 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан обязательства о неразглашении коммерческой и служебной тайны', 'да' FROM RequestAttachment WHERE RequestType = 285 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан обязательства о неразглашении коммерческой и служебной тайны', 'нет' 
	END
	
--select * from dbo.fnGetEmploymentAttachmentList(36) order by AttachmentTypeName

	RETURN 
END

GO


IF OBJECT_ID ('fnCheckCandidateSignDocExists', 'FN') IS NOT NULL
	DROP FUNCTION [dbo].[fnCheckCandidateSignDocExists]
GO


--функция проверяет наличие всех подписанных документов кандидатом
CREATE FUNCTION dbo.fnCheckCandidateSignDocExists
(
	@CandidateId int	
)
RETURNS bit
AS
BEGIN
	DECLARE @IsExists bit
	
	IF NOT EXISTS(SELECT * FROM EmploymentCandidateDocNeeded WHERE CandidateId = @CandidateId)
		SET @IsExists = 0
	ELSE
		SELECT @IsExists = cast(case when count(*) = 0 then 1 else 0 end as bit) --as DocExists
		FROM EmploymentCandidateDocNeeded as A
		LEFT JOIN RequestAttachment as B ON B.RequestId = A.CandidateId and B.RequestType = A.DocTypeId
		WHERE A.CandidateId = -1 and A.IsNeeded = 1 and B.Id is null
	
	RETURN @IsExists
--SELECT dbo.fnCheckCandidateSignDocExists(-1) as SEPCount

END
GO


--ФУНКЦИИ КОНЕЦ