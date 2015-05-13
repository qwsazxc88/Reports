--������ �������� �������� ���� ������ ��� ������� ������ � ��������� ��������� ������ � �����������
USE WebAppSKB
GO
--��������� ���� ������ ������� "�����"
--EmploymentCandidate
	--EmploymentCandidateDocNeeded
	--GeneralInfo
		--NameChange
		--Country	 (���� ������) (������ �������, �������, ������������, ����������)
		--ForeignLanguage
		--DisabilityDegree (���� ������)
	--Passport
		--DocumentType (���� ������)
	--Education
		--Certification
		--HigherEducationDiploma
			--EmploymentEducationType	 (���� ������) (������ ���������� �����������, �������� ������ �����������)
		--PostGraduateEducationDiploma
		--Training
	--Family
		--FamilyMember
		--FamilyStatus (���� ������)
	--MilitaryService
		--MilitaryRanks (���� ������)
		--MilitaryRelationAccount (���� ������)
		--MilitarySpecialityCategory (���� ������)
		--MilitaryValidityCategory (���� ������)
	--Experience
		--ExperienceItem
	--Contacts
	--BackgroundCheck
		--Reference
	--OnsiteTraining
	--Managers
	--PersonnelManagers
		--AccessGroup (���� ������)
		--InsuredPersonType (���� ������)
		--PersonalAccountContractor (���� ������)
		--Signer
		--Schedule (���� ������)
		--SupplementaryAgreement

--������� ������

	--������ ����� - ������
		IF OBJECT_ID ('SupplementaryAgreement', 'U') IS NOT NULL
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


		IF OBJECT_ID ('FK_Candidate_PersonnelManagers', 'F') IS NOT NULL
		ALTER TABLE [dbo].[EmploymentCandidate] DROP CONSTRAINT [FK_Candidate_PersonnelManagers]
		GO

		IF OBJECT_ID ('PersonnelManagers', 'U') IS NOT NULL
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



		CREATE TABLE [dbo].[PersonnelOrderExtraCharges](
			[Id] [int] IDENTITY(1,1) NOT NULL,
			[Version] [int] NOT NULL,
			[Code1C] [nvarchar](5) NULL,
			[Name] [nvarchar](50) NULL,
		 CONSTRAINT [PK_PersonnelOrderExtraCharges] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
		) ON [PRIMARY]

		GO


		IF OBJECT_ID ('Schedule', 'U') IS NOT NULL
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



		IF OBJECT_ID ('PersonalAccountContractor', 'U') IS NOT NULL
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



		IF OBJECT_ID ('InsuredPersonType', 'U') IS NOT NULL
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



		IF OBJECT_ID ('AccessGroup', 'U') IS NOT NULL
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



		IF OBJECT_ID ('Signer', 'U') IS NOT NULL
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

	--������ ����� - �����
	
		
	--�����������
		IF OBJECT_ID ('FK_Candidate_Managers', 'F') IS NOT NULL
		ALTER TABLE [dbo].[EmploymentCandidate] DROP CONSTRAINT [FK_Candidate_Managers]
		GO

		IF OBJECT_ID ('Managers', 'U') IS NOT NULL
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
		 CONSTRAINT [PK_Managers] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
		) ON [PRIMARY]

		GO



		--������
		IF OBJECT_ID ('FK_Candidate_OnsiteTraining', 'F') IS NOT NULL
		ALTER TABLE [dbo].[EmploymentCandidate] DROP CONSTRAINT [FK_Candidate_OnsiteTraining]
		GO


		IF OBJECT_ID ('OnsiteTraining', 'U') IS NOT NULL
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
			
	--�� - ������
		IF OBJECT_ID ('Reference', 'U') IS NOT NULL
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



		IF OBJECT_ID ('FK_Candidate_BackgroundCheck', 'F') IS NOT NULL
		ALTER TABLE [dbo].[EmploymentCandidate] DROP CONSTRAINT [FK_Candidate_BackgroundCheck]
		GO

		IF OBJECT_ID ('BackgroundCheck', 'U') IS NOT NULL
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
			[ApprovalStatus] [bit] NULL,
			[ApproverId] [int] NULL,
			[ApprovalDate] [datetime] NULL,
			[IsFinal] [bit] NOT NULL CONSTRAINT [DF__Backgroun__IsFin__39794BAC]  DEFAULT ((0)),
			[IsApprovalSkipped] [bit] NOT NULL CONSTRAINT [DF_BackgroundCheck_IsApprovalSkipped]  DEFAULT ((0)),
			[IsValidate] [bit] NULL CONSTRAINT [DF_BackgroundCheck_IsValidate]  DEFAULT ((0)),
			[PyrusRef] [nvarchar](150) NULL,
		 CONSTRAINT [PK_BackgroundCheck] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
		) ON [PRIMARY]

		GO
	--�� - �����

	--�������� - ������
		IF OBJECT_ID ('FK_Candidate_Contacts', 'F') IS NOT NULL
		ALTER TABLE [dbo].[EmploymentCandidate] DROP CONSTRAINT [FK_Candidate_Contacts]
		GO


		IF OBJECT_ID ('Contacts', 'U') IS NOT NULL
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
	--�������� - �����

	--�������� ������������ - ������
		IF OBJECT_ID ('ExperienceItem', 'U') IS NOT NULL
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


		IF OBJECT_ID ('FK_Candidate_Experience', 'F') IS NOT NULL
		ALTER TABLE [dbo].[EmploymentCandidate] DROP CONSTRAINT [FK_Candidate_Experience]
		GO

		IF OBJECT_ID ('Experience', 'U') IS NOT NULL
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
			[IsFinal] [bit] NOT NULL DEFAULT ((0)),
			[IsValidate] [bit] NULL CONSTRAINT [DF_Experience_IsValidate]  DEFAULT ((0)),
		 CONSTRAINT [PK_Experience] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
		) ON [PRIMARY]

		GO
	--�������� ������������ - �����

	--�������� ���� - ������
		IF OBJECT_ID ('FK_Candidate_MilitaryService', 'F') IS NOT NULL
		ALTER TABLE [dbo].[EmploymentCandidate] DROP CONSTRAINT [FK_Candidate_MilitaryService]
		GO


		IF OBJECT_ID ('MilitaryService', 'U') IS NOT NULL
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



		IF OBJECT_ID ('MilitaryValidityCategory', 'U') IS NOT NULL
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



		IF OBJECT_ID ('MilitarySpecialityCategory', 'U') IS NOT NULL
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



		IF OBJECT_ID ('MilitaryRelationAccount', 'U') IS NOT NULL
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




		IF OBJECT_ID ('MilitaryRanks', 'U') IS NOT NULL
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
	--�������� ���� - �����

	--�������� ��������� - ������
		IF OBJECT_ID ('FamilyMember', 'U') IS NOT NULL
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



		IF OBJECT_ID ('FK_Candidate_Family', 'F') IS NOT NULL
		ALTER TABLE [dbo].[EmploymentCandidate] DROP CONSTRAINT [FK_Candidate_Family]
		GO


		IF OBJECT_ID ('Family', 'U') IS NOT NULL
		DROP TABLE [dbo].[Family]
		GO

		CREATE TABLE [dbo].[Family](
			[Id] [int] IDENTITY(1,1) NOT NULL,
			[Version] [int] NOT NULL,
			[Cohabitants] [nvarchar](250) NULL,
			[CandidateId] [int] NOT NULL,
			[IsFinal] [bit] NOT NULL DEFAULT ((0)),
			[IsValidate] [bit] NULL CONSTRAINT [DF_Family_IsValidate]  DEFAULT ((0)),
			[FamilyStatusId] [int] NULL,
		 CONSTRAINT [PK_Family] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
		) ON [PRIMARY]

		GO



		IF OBJECT_ID ('FamilyStatus', 'U') IS NOT NULL
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
	--�������� ��������� - �����
	
	--����������� - ������
		IF OBJECT_ID ('Training', 'U') IS NOT NULL
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



		IF OBJECT_ID ('PostGraduateEducationDiploma', 'U') IS NOT NULL
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



		IF OBJECT_ID ('HigherEducationDiploma', 'U') IS NOT NULL
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



		IF OBJECT_ID ('EmploymentEducationType', 'U') IS NOT NULL
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



		IF OBJECT_ID ('Certification', 'U') IS NOT NULL
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



		IF OBJECT_ID ('FK_Candidate_Education', 'F') IS NOT NULL
		ALTER TABLE [dbo].[EmploymentCandidate] DROP CONSTRAINT [FK_Candidate_Education]
		GO


		IF OBJECT_ID ('Education', 'U') IS NOT NULL
		DROP TABLE [dbo].[Education]
		GO

		CREATE TABLE [dbo].[Education](
			[Id] [int] IDENTITY(1,1) NOT NULL,
			[Version] [int] NOT NULL,
			[CandidateId] [int] NOT NULL,
			[IsFinal] [bit] NOT NULL DEFAULT ((0)),
			[IsValidate] [bit] NULL CONSTRAINT [DF_Education_IsValidate]  DEFAULT ((0)),
		 CONSTRAINT [PK_Education] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
		) ON [PRIMARY]

		GO
	--����������� - �����

	--������� - ������
		IF OBJECT_ID ('FK_Candidate_Passport', 'F') IS NOT NULL
		ALTER TABLE [dbo].[EmploymentCandidate] DROP CONSTRAINT [FK_Candidate_Passport]
		GO


		IF OBJECT_ID ('Passport', 'U') IS NOT NULL
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


		IF OBJECT_ID ('DocumentType', 'U') IS NOT NULL
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
	--������� - �����

	--�������� ���������� - ������
		IF OBJECT_ID ('ForeignLanguage', 'U') IS NOT NULL
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



		IF OBJECT_ID ('NameChange', 'U') IS NOT NULL
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




		IF OBJECT_ID ('FK_Candidate_GeneralInfo', 'F') IS NOT NULL
		ALTER TABLE [dbo].[EmploymentCandidate] DROP CONSTRAINT [FK_Candidate_GeneralInfo]
		GO



		IF OBJECT_ID ('GeneralInfo', 'U') IS NOT NULL
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



		IF OBJECT_ID ('DisabilityDegree', 'U') IS NOT NULL
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



		IF OBJECT_ID ('Country', 'U') IS NOT NULL
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
	--�������� ���������� - �����
	--�������� ��� ������ - ������
		IF OBJECT_ID ('EmploymentCandidateDocNeeded', 'U') IS NOT NULL
		DROP TABLE [dbo].[EmploymentCandidateDocNeeded]
		GO
	--�������� ��� ������ - �����
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
	--��������
		IF OBJECT_ID ('EmploymentCandidate', 'U') IS NOT NULL
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
			[IsPersonnelManagerToManagerSendEmail] [bit] NULL CONSTRAINT [DF_EmploymentCandidate_IsPersonnelManagerToManagerSendEmail]  DEFAULT ((0)),
			[PersonnelManagerToManagerSendEmailDate] [datetime] NULL,
			[AppointmentReportId] [int] NULL,
			[AppointmentId] [int] NULL,
			[IsTechDissmiss] [bit] NULL CONSTRAINT [DF_EmploymentCandidate_IsTtechDissmiss]  DEFAULT ((0)),
		 CONSTRAINT [PK_EmploymentCandidate] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
		) ON [PRIMARY]

		GO


	--����������� � ������
		IF OBJECT_ID ('EmploymentCandidateComments', 'U') IS NOT NULL
		DROP TABLE [dbo].[EmploymentCandidateComments]
		GO

		CREATE TABLE [dbo].[EmploymentCandidateComments](
			[Id] [int] IDENTITY(1,1) NOT NULL,
			[Version] [int] NOT NULL,
			[UserId] [int] NULL,
			[CandidateId] [int] NULL,
			[CommentTypeId] [int] NULL,
			[CreatedDate] [datetime] NULL CONSTRAINT [DF_EmploymentCandidateComments_CreatedDate]  DEFAULT (getdate()),
			[Comment] [nvarchar](256) NULL,
		 CONSTRAINT [PK_EmploymentCandidateComments] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
		) ON [PRIMARY]

		GO

	
--������ ������
	ALTER TABLE [dbo].[PersonnelManagers]  WITH CHECK ADD  CONSTRAINT [FK_PersonnelManagers_Users] FOREIGN KEY([RejectUserId])
	REFERENCES [dbo].[Users] ([Id])
	GO

	ALTER TABLE [dbo].[PersonnelManagers] CHECK CONSTRAINT [FK_PersonnelManagers_Users]
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

	ALTER TABLE [dbo].[EmploymentCandidateComments]  WITH CHECK ADD  CONSTRAINT [FK_EmploymentCandidateComments_Users] FOREIGN KEY([UserId])
	REFERENCES [dbo].[Users] ([Id])
	GO

	ALTER TABLE [dbo].[EmploymentCandidateComments] CHECK CONSTRAINT [FK_EmploymentCandidateComments_Users]
	GO

	ALTER TABLE [dbo].[EmploymentCandidateComments]  WITH CHECK ADD  CONSTRAINT [FK_EmploymentCandidateComments_Users1] FOREIGN KEY([CandidateId])
	REFERENCES [dbo].[Users] ([Id])
	GO

	ALTER TABLE [dbo].[EmploymentCandidateComments] CHECK CONSTRAINT [FK_EmploymentCandidateComments_Users1]
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

	ALTER TABLE [dbo].[EmploymentCandidate]  WITH CHECK ADD  CONSTRAINT [FK_EmploymentCandidate_Users] FOREIGN KEY([PersonnelId])
	REFERENCES [dbo].[Users] ([Id])
	GO

	ALTER TABLE [dbo].[EmploymentCandidate] CHECK CONSTRAINT [FK_EmploymentCandidate_Users]
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

	ALTER TABLE [dbo].[BackgroundCheck]  WITH CHECK ADD  CONSTRAINT [FK_BackgroundCheck_Candidate] FOREIGN KEY([CandidateId])
	REFERENCES [dbo].[EmploymentCandidate] ([Id])
	GO

	ALTER TABLE [dbo].[BackgroundCheck] CHECK CONSTRAINT [FK_BackgroundCheck_Candidate]
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

	ALTER TABLE [dbo].[SupplementaryAgreement]  WITH CHECK ADD  CONSTRAINT [FK_SupplementaryAgreement_PersonnelManagers] FOREIGN KEY([PersonnelManagersId])
	REFERENCES [dbo].[PersonnelManagers] ([Id])
	GO

	ALTER TABLE [dbo].[SupplementaryAgreement] CHECK CONSTRAINT [FK_SupplementaryAgreement_PersonnelManagers]
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

	ALTER TABLE [dbo].[PersonnelManagers]  WITH CHECK ADD  CONSTRAINT [FK_PersonnelManagers_PersonnelOrderExtraCharges] FOREIGN KEY([ExtraChargesId])
	REFERENCES [dbo].[PersonnelOrderExtraCharges] ([Id])
	GO

	ALTER TABLE [dbo].[PersonnelManagers] CHECK CONSTRAINT [FK_PersonnelManagers_PersonnelOrderExtraCharges]
	GO

--������ �����

--�������� � �������� � ����� - ������ (����������� �� �����)
	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� ������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BackgroundCheck', @level2type=N'COLUMN',@level2name=N'ApprovalDate'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������� ������������ ����������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmploymentCandidate', @level2type=N'COLUMN',@level2name=N'IsTechDissmiss'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PersonnelOrderExtraCharges', @level2type=N'COLUMN',@level2name=N'Version'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������� ���������� ��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PersonnelManagers', @level2type=N'COLUMN',@level2name=N'ExtraChargesId'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PersonnelOrderExtraCharges', @level2type=N'COLUMN',@level2name=N'Id'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��� ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PersonnelOrderExtraCharges', @level2type=N'COLUMN',@level2name=N'Code1C'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PersonnelOrderExtraCharges', @level2type=N'COLUMN',@level2name=N'Name'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���������� ���������� ��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PersonnelOrderExtraCharges'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��� ���������� �����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PersonnelManagers', @level2type=N'COLUMN',@level2name=N'NorthExperienceYears'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����� ���������� �����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PersonnelManagers', @level2type=N'COLUMN',@level2name=N'NorthExperienceMonths'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� ���������� �����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PersonnelManagers', @level2type=N'COLUMN',@level2name=N'NorthExperienceDays'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��� ��������� �����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PersonnelManagers', @level2type=N'COLUMN',@level2name=N'NorthExperienceType'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� ����������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PersonnelManagers', @level2type=N'COLUMN',@level2name=N'RejectDate'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ����������, ������� ������ ����������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PersonnelManagers', @level2type=N'COLUMN',@level2name=N'RejectUserId'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������ �� ������ � ������� Pyrus' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BackgroundCheck', @level2type=N'COLUMN',@level2name=N'PyrusRef'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������� ����������� �� ��������� �������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmploymentCandidate', @level2type=N'COLUMN',@level2name=N'IsPersonnelManagerToManagerSendEmail'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� ��������� ����������� �� ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmploymentCandidate', @level2type=N'COLUMN',@level2name=N'PersonnelManagerToManagerSendEmailDate'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmploymentCandidateDocNeeded', @level2type=N'COLUMN',@level2name=N'Id'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������ ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmploymentCandidateDocNeeded', @level2type=N'COLUMN',@level2name=N'Version'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmploymentCandidateDocNeeded', @level2type=N'COLUMN',@level2name=N'CandidateId'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmploymentCandidateDocNeeded', @level2type=N'COLUMN',@level2name=N'DocTypeId'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�������� ����� ��� ������� ����������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmploymentCandidateDocNeeded', @level2type=N'COLUMN',@level2name=N'IsNeeded'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� �������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmploymentCandidateDocNeeded', @level2type=N'COLUMN',@level2name=N'DateCreate'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmploymentCandidateDocNeeded', @level2type=N'COLUMN',@level2name=N'CreatorId'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� ��������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmploymentCandidateDocNeeded', @level2type=N'COLUMN',@level2name=N'DateEdit'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmploymentCandidateDocNeeded', @level2type=N'COLUMN',@level2name=N'EditorId'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������ ����������� ���������� ��� �������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmploymentCandidateDocNeeded'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� ���������� ��������� � �������� � 1�' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PersonnelManagers', @level2type=N'COLUMN',@level2name=N'CompleteDate'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� �������� � 1�' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmploymentCandidate', @level2type=N'COLUMN',@level2name=N'SendTo1C'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����� ��������� ������ �� ������ �� 1�' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmploymentCandidate', @level2type=N'COLUMN',@level2name=N'ContractNumber1C'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmploymentCandidateComments', @level2type=N'COLUMN',@level2name=N'Id'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmploymentCandidateComments', @level2type=N'COLUMN',@level2name=N'Version'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmploymentCandidateComments', @level2type=N'COLUMN',@level2name=N'UserId'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmploymentCandidateComments', @level2type=N'COLUMN',@level2name=N'CandidateId'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�������� � �������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmploymentCandidateComments', @level2type=N'COLUMN',@level2name=N'CommentTypeId'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� �������� ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmploymentCandidateComments', @level2type=N'COLUMN',@level2name=N'CreatedDate'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����� ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmploymentCandidateComments', @level2type=N'COLUMN',@level2name=N'Comment'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����������� � �������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmploymentCandidateComments'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmploymentCandidate', @level2type=N'COLUMN',@level2name=N'PersonnelId'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����� ��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmploymentCandidate', @level2type=N'COLUMN',@level2name=N'IsTrainingNeeded'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�������� �� ��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmploymentCandidate', @level2type=N'COLUMN',@level2name=N'IsBeforEmployment'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������� �� ��������� ������� � ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmploymentCandidate', @level2type=N'COLUMN',@level2name=N'IsCandidateToBackgroundSendEmail'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� ��������� �� ��������� � ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmploymentCandidate', @level2type=N'COLUMN',@level2name=N'CandidateToBackgroundSendEmailDate'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������� ������� ������������ � ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmploymentCandidate', @level2type=N'COLUMN',@level2name=N'IsCandidateToManagerSendEmail'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� ��������� ������������ � ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmploymentCandidate', @level2type=N'COLUMN',@level2name=N'CandidateToManagerSendEmailDate'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������� �� �� ������� ������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmploymentCandidate', @level2type=N'COLUMN',@level2name=N'IsBackgroundToManagerSendEmail'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� ��������� �� �� ������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmploymentCandidate', @level2type=N'COLUMN',@level2name=N'BackgroundToManagerSendEmailDate'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������� ������� �������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmploymentCandidate', @level2type=N'COLUMN',@level2name=N'IsManagerToTrainingSendEmail'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� ��������� �������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmploymentCandidate', @level2type=N'COLUMN',@level2name=N'ManagerToTrainingSendEmailDate'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������� ������� ����������� �������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmploymentCandidate', @level2type=N'COLUMN',@level2name=N'IsManagerToHigherManagerSendEmail'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� ��������� ������� �����������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmploymentCandidate', @level2type=N'COLUMN',@level2name=N'ManagerToHigherManagerSendEmailDate'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������� ���������� ������� �� ������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GeneralInfo', @level2type=N'COLUMN',@level2name=N'IsDisabilityTermLess'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������������� ���������� ������������ ��������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GeneralInfo', @level2type=N'COLUMN',@level2name=N'IsValidate'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������ ��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GeneralInfo', @level2type=N'COLUMN',@level2name=N'CountryBirthId'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������������� ���������� ������������ ��������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Passport', @level2type=N'COLUMN',@level2name=N'IsValidate'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������������� ���������� ������������ ��������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Education', @level2type=N'COLUMN',@level2name=N'IsValidate'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����� ���������� �������� ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Certification', @level2type=N'COLUMN',@level2name=N'LocationEI'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ���� �����������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HigherEducationDiploma', @level2type=N'COLUMN',@level2name=N'EducationTypeId'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����� ���������� �������� ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HigherEducationDiploma', @level2type=N'COLUMN',@level2name=N'LocationEI'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmploymentEducationType', @level2type=N'COLUMN',@level2name=N'Id'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmploymentEducationType', @level2type=N'COLUMN',@level2name=N'Version'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmploymentEducationType', @level2type=N'COLUMN',@level2name=N'Code'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmploymentEducationType', @level2type=N'COLUMN',@level2name=N'Name'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������� (����������)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmploymentEducationType', @level2type=N'COLUMN',@level2name=N'Priority'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� �����������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmploymentEducationType'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����� ���������� �������� ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PostGraduateEducationDiploma', @level2type=N'COLUMN',@level2name=N'LocationEI'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����� ���������� �������� ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Training', @level2type=N'COLUMN',@level2name=N'LocationEI'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������������� ���������� ������������ ��������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Family', @level2type=N'COLUMN',@level2name=N'IsValidate'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FamilyStatus', @level2type=N'COLUMN',@level2name=N'Id'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���������� ������� ��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FamilyStatus'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������� ����������� � ������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MilitaryService', @level2type=N'COLUMN',@level2name=N'MilitaryValidityCategoryId'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������ (�������)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MilitaryService', @level2type=N'COLUMN',@level2name=N'SpecialityCategoryId'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������� � ��������� �����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MilitaryService', @level2type=N'COLUMN',@level2name=N'MilitaryRelationAccountId'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������������� ���������� ������������ ��������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MilitaryService', @level2type=N'COLUMN',@level2name=N'IsValidate'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MilitaryRanks', @level2type=N'COLUMN',@level2name=N'Id'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MilitaryRanks', @level2type=N'COLUMN',@level2name=N'Version'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MilitaryRanks', @level2type=N'COLUMN',@level2name=N'Code'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MilitaryRanks', @level2type=N'COLUMN',@level2name=N'Name'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���������� �������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MilitaryRanks'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MilitaryRelationAccount', @level2type=N'COLUMN',@level2name=N'Id'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MilitaryRelationAccount', @level2type=N'COLUMN',@level2name=N'Version'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MilitaryRelationAccount', @level2type=N'COLUMN',@level2name=N'Name'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���������� ��������� � ��������� �����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MilitaryRelationAccount'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MilitaryValidityCategory', @level2type=N'COLUMN',@level2name=N'Id'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MilitaryValidityCategory', @level2type=N'COLUMN',@level2name=N'Version'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MilitaryValidityCategory', @level2type=N'COLUMN',@level2name=N'Code'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MilitaryValidityCategory', @level2type=N'COLUMN',@level2name=N'Name'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���������� ��������� �������� � ������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MilitaryValidityCategory'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������������� ���������� ������������ ��������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Experience', @level2type=N'COLUMN',@level2name=N'IsValidate'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Contacts', @level2type=N'COLUMN',@level2name=N'Country'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����������/����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Contacts', @level2type=N'COLUMN',@level2name=N'Republic'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������������� ���������� ������������ ��������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Contacts', @level2type=N'COLUMN',@level2name=N'IsValidate'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������� ������������ ����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BackgroundCheck', @level2type=N'COLUMN',@level2name=N'HasDriversLicense'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������� ����������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BackgroundCheck', @level2type=N'COLUMN',@level2name=N'HasAutomobile'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������������� ���������� ������������ ��������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BackgroundCheck', @level2type=N'COLUMN',@level2name=N'IsValidate'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������� ������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Managers', @level2type=N'COLUMN',@level2name=N'IsExternalPTWorker'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� ����������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Managers', @level2type=N'COLUMN',@level2name=N'RegistrationDate'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������� ����� �� ������� ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PersonnelManagers', @level2type=N'COLUMN',@level2name=N'BasicSalary'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id �������� ������ 1.5 ��� ��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PersonnelManagers', @level2type=N'COLUMN',@level2name=N'ContractPoint_1_Id'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id �������� ������ 1.6 ��� ��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PersonnelManagers', @level2type=N'COLUMN',@level2name=N'ContractPoint_2_Id'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id �������� ������ 5.1 ��� ��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PersonnelManagers', @level2type=N'COLUMN',@level2name=N'ContractPoint_3_Id'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��� ��� ������� ��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PersonnelManagers', @level2type=N'COLUMN',@level2name=N'ContractPointsFio'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ContractPointsAddress' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PersonnelManagers', @level2type=N'COLUMN',@level2name=N'ContractPointsAddress'
	GO

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������ ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PersonnelManagers', @level2type=N'COLUMN',@level2name=N'ScheduleId'
	GO
--�������� � �������� � ����� - �����


--������������� ������
	IF OBJECT_ID ('vwEmploymentComments', 'V') IS NOT NULL
		DROP VIEW [dbo].[vwEmploymentComments]
	GO

	--������� ����������� � �������� ������
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
	
	--��������� ������ ���������� �� ID ������ �� ���
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

	--��������� ���������� �����
	CREATE VIEW [dbo].[vwEmploymentHighEducations]
	AS
	SELECT A.*, B.Name as EducationTypeName
	FROM HigherEducationDiploma as A
	LEFT JOIN EmploymentEducationType as B ON B.Id = A.EducationTypeId
	GO



IF OBJECT_ID ('vwEmploymentFillState', 'V') IS NOT NULL
	DROP VIEW [dbo].[vwEmploymentFillState]
GO

--��������� ���������� �����
CREATE VIEW [dbo].[vwEmploymentFillState]
AS
SELECT -1 as Id, cast(0 as bit) as GeneralFinal, cast(0 as bit) as PassportFinal, cast(0 as bit) as EducationFinal, cast(0 as bit) as FamilyFinal, cast(0 as bit) as MilitaryFinal, cast(0 as bit) as ExperienceFinal,
			 cast(0 as bit) as ContactFinal, cast(0 as bit) as BackgroundFinal, cast(0 as bit) as CandidateApp,
			 --�������� ��������� �������� ������
			 cast(0 as bit) as CandidateReady,
			 --������������
			 cast(0 as bit) as BackgroundApproval, cast(0 as bit) as TrainingApproval, cast(0 as bit) as ManagerApproval, cast(0 as bit) as PersonnelManagerApproval
UNION ALL
SELECT A.Id, B.IsFinal as GeneralFinal, C.IsFinal as PassportFinal, D.IsFinal as EducationFinal, E.IsFinal as FamilyFinal, F.IsFinal as MilitaryFinal, G.IsFinal as ExperienceFinal,
			 H.IsFinal as ContactFinal, I.IsFinal as BackgroundFinal, 
			 --cast(case when L.cnt = 8 then 1 else 0 end as bit) as CandidateApp,
			 cast(case when L.cnt is null or (isnull(L.cnt, 0) <> 0)  then 0 else 1 end as bit) as CandidateApp,
			 --�������� ��������� �������� ������
			 cast(case when B.IsFinal = 1 and C.IsFinal = 1 and D.IsFinal = 1 and E.IsFinal = 1 and F.IsFinal = 1 and G.IsFinal = 1 and H.IsFinal = 1 and I.IsFinal = 1 then 1 else 0 end as bit) as CandidateReady,
			 --������������
			 I.ApprovalStatus as BackgroundApproval, J.IsComplete as TrainingApproval, K.HigherManagerApprovalStatus as ManagerApproval, 
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



--������� ����������� � �������� ������
CREATE VIEW [dbo].[vwEmploymentPositions]
AS
SELECT Id, Name + case when IsMarkedToRemoval = 1 then ' (�� ������������)' else '' end as Name
FROM dbo.Position



GO




--������������� �����

--��������� ������ ��� ������������ - ������

		--Country	 (���� ������) (������ �������, �������, ������������, ����������)
		INSERT INTO dbo.Country(Version, Code, Name) VALUES(1, N'112', N'��������')
		INSERT INTO dbo.Country(Version, Code, Name) VALUES(1, N'398', N'���������')
		INSERT INTO dbo.Country(Version, Code, Name) VALUES(1, N'643', N'������')
		INSERT INTO dbo.Country(Version, Code, Name) VALUES(1, N'804', N'�������')

		--DisabilityDegree (���� ������)
		INSERT INTO dbo.DisabilityDegree(Version, Name) VALUES(1, N'I')
		INSERT INTO dbo.DisabilityDegree(Version, Name) VALUES(1, N'II')
		INSERT INTO dbo.DisabilityDegree(Version, Name) VALUES(1, N'III')

		--DocumentType (���� ������)
		INSERT INTO dbo.DocumentType(Version, Code, Name) VALUES(1, N'21', N'������� ���������� ���������� ���������')
		INSERT INTO dbo.DocumentType(Version, Code, Name) VALUES(1, N'12', N'��� �� ����������')
		INSERT INTO dbo.DocumentType(Version, Code, Name) VALUES(1, N'27', N'������� ����� ������� ������')
		INSERT INTO dbo.DocumentType(Version, Code, Name) VALUES(1, N'7', N'������� ����� ������� (�������, ��������, ��������)')
		INSERT INTO dbo.DocumentType(Version, Code, Name) VALUES(1, N'14', N'��������� ������������� �������� ���������� ���������� ���������')
		INSERT INTO dbo.DocumentType(Version, Code, Name) VALUES(1, N'9', N'��������������� ������� ���������� ���������� ���������')
		INSERT INTO dbo.DocumentType(Version, Code, Name) VALUES(1, N'22', N'������������� ���������� ���������� ���������')
		INSERT INTO dbo.DocumentType(Version, Code, Name) VALUES(1, N'2', N'������������� ���������� ����')
		INSERT INTO dbo.DocumentType(Version, Code, Name) VALUES(1, N'91', N'���� ���������')
		INSERT INTO dbo.DocumentType(Version, Code, Name) VALUES(1, N'1', N'������� ���������� ����')
		INSERT INTO dbo.DocumentType(Version, Code, Name) VALUES(1, N'10', N'������� ������������ ����������')
		INSERT INTO dbo.DocumentType(Version, Code, Name) VALUES(1, N'6', N'������� �����������')
		INSERT INTO dbo.DocumentType(Version, Code, Name) VALUES(1, N'26', N'������� ������')
		INSERT INTO dbo.DocumentType(Version, Code, Name) VALUES(1, N'15', N'���������� �� ��������� ���������� � ���������� ���������')
		INSERT INTO dbo.DocumentType(Version, Code, Name) VALUES(1, N'11', N'������������� � ������������ ����������� � ��������� �������� �� ���������� ���������� ���������')
		INSERT INTO dbo.DocumentType(Version, Code, Name) VALUES(1, N'3', N'������������� � ��������')
		INSERT INTO dbo.DocumentType(Version, Code, Name) VALUES(1, N'23', N'������������� � ��������, �������� �������������� ������� ������������ �����������')
		INSERT INTO dbo.DocumentType(Version, Code, Name) VALUES(1, N'5', N'������� �� ������������ ��  ����� ������� �������')
		INSERT INTO dbo.DocumentType(Version, Code, Name) VALUES(1, N'13', N'������������� ������� � ���������� ���������')
		INSERT INTO dbo.DocumentType(Version, Code, Name) VALUES(1, N'4', N'������������� �������� �������')


		--EmploymentEducationType	 (���� ������) (������ ���������� �����������, �������� ������ �����������)
		INSERT INTO dbo.EmploymentEducationType(Version, Code, Name, Priority) VALUES(1, N'02', N'��������� (�����) �����������', 1)
		INSERT INTO dbo.EmploymentEducationType(Version, Code, Name, Priority) VALUES(1, N'03', N'�������� ����� �����������', 2)
		INSERT INTO dbo.EmploymentEducationType(Version, Code, Name, Priority) VALUES(1, N'07', N'������� (������) ����� �����������', 3)
		INSERT INTO dbo.EmploymentEducationType(Version, Code, Name, Priority) VALUES(1, N'10', N'��������� ���������������� �����������', 4)
		INSERT INTO dbo.EmploymentEducationType(Version, Code, Name, Priority) VALUES(1, N'11', N'������� ���������������� �����������', 5)
		INSERT INTO dbo.EmploymentEducationType(Version, Code, Name, Priority) VALUES(1, N'18', N'������ �����������', 6)
		INSERT INTO dbo.EmploymentEducationType(Version, Code, Name, Priority) VALUES(1, N'19', N'�������������� �����������', 7)

		--FamilyStatus (���� ������)
		INSERT INTO dbo.FamilyStatus(Version, Code, Name) VALUES(1, N'4', N'������ (�����)')
		INSERT INTO dbo.FamilyStatus(Version, Code, Name) VALUES(1, N'1', N'������� �� ������� (�� �������� � �����)')
		INSERT INTO dbo.FamilyStatus(Version, Code, Name) VALUES(1, N'5', N'�������� (���������)')
		INSERT INTO dbo.FamilyStatus(Version, Code, Name) VALUES(1, N'6', N'��������� (���������)')
		INSERT INTO dbo.FamilyStatus(Version, Code, Name) VALUES(1, N'7', N'������� � ������������������ �����')
		INSERT INTO dbo.FamilyStatus(Version, Code, Name) VALUES(1, N'3', N'������� � �������������������� �����')

		--MilitaryRanks (���� ������)
		INSERT INTO dbo.MilitaryRanks(Version, Name) VALUES(1, N'�������')
		INSERT INTO dbo.MilitaryRanks(Version, Name) VALUES(1, N'������')
		INSERT INTO dbo.MilitaryRanks(Version, Name) VALUES(1, N'��������')
		INSERT INTO dbo.MilitaryRanks(Version, Name) VALUES(1, N'������� ������')
		INSERT INTO dbo.MilitaryRanks(Version, Name) VALUES(1, N'������� �������')
		INSERT INTO dbo.MilitaryRanks(Version, Name) VALUES(1, N'�������� 2-� ������')
		INSERT INTO dbo.MilitaryRanks(Version, Name) VALUES(1, N'�������')
		INSERT INTO dbo.MilitaryRanks(Version, Name) VALUES(1, N'�������� 1-� ������')
		INSERT INTO dbo.MilitaryRanks(Version, Name) VALUES(1, N'������� �������')
		INSERT INTO dbo.MilitaryRanks(Version, Name) VALUES(1, N'������� ��������')
		INSERT INTO dbo.MilitaryRanks(Version, Name) VALUES(1, N'��������')
		INSERT INTO dbo.MilitaryRanks(Version, Name) VALUES(1, N'������� ����������� ��������')
		INSERT INTO dbo.MilitaryRanks(Version, Name) VALUES(1, N'���������')
		INSERT INTO dbo.MilitaryRanks(Version, Name) VALUES(1, N'������')
		INSERT INTO dbo.MilitaryRanks(Version, Name) VALUES(1, N'������� ���������')
		INSERT INTO dbo.MilitaryRanks(Version, Name) VALUES(1, N'������� ������')
		INSERT INTO dbo.MilitaryRanks(Version, Name) VALUES(1, N'������� ���������')
		INSERT INTO dbo.MilitaryRanks(Version, Name) VALUES(1, N'���������')
		INSERT INTO dbo.MilitaryRanks(Version, Name) VALUES(1, N'������� ���������')
		INSERT INTO dbo.MilitaryRanks(Version, Name) VALUES(1, N'�������')
		INSERT INTO dbo.MilitaryRanks(Version, Name) VALUES(1, N'�������-���������')
		INSERT INTO dbo.MilitaryRanks(Version, Name) VALUES(1, N'�����')
		INSERT INTO dbo.MilitaryRanks(Version, Name) VALUES(1, N'������� 3 �����')
		INSERT INTO dbo.MilitaryRanks(Version, Name) VALUES(1, N'������������')
		INSERT INTO dbo.MilitaryRanks(Version, Name) VALUES(1, N'������� 2 �����')
		INSERT INTO dbo.MilitaryRanks(Version, Name) VALUES(1, N'���������')
		INSERT INTO dbo.MilitaryRanks(Version, Name) VALUES(1, N'������� 1 �����')
		INSERT INTO dbo.MilitaryRanks(Version, Name) VALUES(1, N'�������-�����')
		INSERT INTO dbo.MilitaryRanks(Version, Name) VALUES(1, N'�����-�������')
		INSERT INTO dbo.MilitaryRanks(Version, Name) VALUES(1, N'�������-���������')
		INSERT INTO dbo.MilitaryRanks(Version, Name) VALUES(1, N'����-�������')
		INSERT INTO dbo.MilitaryRanks(Version, Name) VALUES(1, N'�������-���������')
		INSERT INTO dbo.MilitaryRanks(Version, Name) VALUES(1, N'�������')
		INSERT INTO dbo.MilitaryRanks(Version, Name) VALUES(1, N'������� �����')
		INSERT INTO dbo.MilitaryRanks(Version, Name) VALUES(1, N'������� �����')
		INSERT INTO dbo.MilitaryRanks(Version, Name) VALUES(1, N'������ ��')

		--MilitaryRelationAccount (���� ������)
		INSERT INTO dbo.MilitaryRelationAccount(Version, Name) VALUES(1, N'������� �� �������� �����')
		INSERT INTO dbo.MilitaryRelationAccount(Version, Name) VALUES(1, N'������ �� �������� ����')
		INSERT INTO dbo.MilitaryRelationAccount(Version, Name) VALUES(1, N'�� ������� �� �������� ����� (�� ������)')
		INSERT INTO dbo.MilitaryRelationAccount(Version, Name) VALUES(1, N'���� � ��������� ����� �� ��������')
		INSERT INTO dbo.MilitaryRelationAccount(Version, Name) VALUES(1, N'���� � ��������� ����� �� ��������� ��������')

		--MilitarySpecialityCategory (���� ������)
		INSERT INTO dbo.MilitarySpecialityCategory(Version, Name) VALUES(1, N'���������')
		INSERT INTO dbo.MilitarySpecialityCategory(Version, Name) VALUES(1, N'���������-�����������')
		INSERT INTO dbo.MilitarySpecialityCategory(Version, Name) VALUES(1, N'������-������������')
		INSERT INTO dbo.MilitarySpecialityCategory(Version, Name) VALUES(1, N'��������������')
		INSERT INTO dbo.MilitarySpecialityCategory(Version, Name) VALUES(1, N'�����������')
		INSERT INTO dbo.MilitarySpecialityCategory(Version, Name) VALUES(1, N'�����������')
		INSERT INTO dbo.MilitarySpecialityCategory(Version, Name) VALUES(1, N'������������')
		INSERT INTO dbo.MilitarySpecialityCategory(Version, Name) VALUES(1, N'������������� (����������)')
		INSERT INTO dbo.MilitarySpecialityCategory(Version, Name) VALUES(1, N'������� � �������')
		INSERT INTO dbo.MilitarySpecialityCategory(Version, Name) VALUES(1, N'�������� � ��������')
		INSERT INTO dbo.MilitarySpecialityCategory(Version, Name) VALUES(1, N'���������� � �������')
		INSERT INTO dbo.MilitarySpecialityCategory(Version, Name) VALUES(1, N'������� �������')
		INSERT INTO dbo.MilitarySpecialityCategory(Version, Name) VALUES(1, N'������� �������')
		INSERT INTO dbo.MilitarySpecialityCategory(Version, Name) VALUES(1, N'������ �������')

		--MilitaryValidityCategory (���� ������)
		INSERT INTO dbo.MilitaryValidityCategory(Version, Name) VALUES(1, N'� - ����� � ������� ������')
		INSERT INTO dbo.MilitaryValidityCategory(Version, Name) VALUES(1, N'� - ����� � ������� ������ � ��������������� �������������')
		INSERT INTO dbo.MilitaryValidityCategory(Version, Name) VALUES(1, N'� - ����������� ����� � ������� ������')
		INSERT INTO dbo.MilitaryValidityCategory(Version, Name) VALUES(1, N'� - �������� �� ����� � ������� ������')
		INSERT INTO dbo.MilitaryValidityCategory(Version, Name) VALUES(1, N'� - �� ����� � ������� ������')

		--AccessGroup (���� ������)
		INSERT INTO dbo.AccessGroup(Version, Code, Name) VALUES(1, N'000052', N'GE (734)')
		INSERT INTO dbo.AccessGroup(Version, Code, Name) VALUES(1, N'000017', N'�� �������� ���. (163)')
		INSERT INTO dbo.AccessGroup(Version, Code, Name) VALUES(1, N'000022', N'�� ��������� ���������� ���. (17)')
		INSERT INTO dbo.AccessGroup(Version, Code, Name) VALUES(1, N'000025', N'�� ���������� ���� (42)')
		INSERT INTO dbo.AccessGroup(Version, Code, Name) VALUES(1, N'000060', N'�� ����������� ���.')
		INSERT INTO dbo.AccessGroup(Version, Code, Name) VALUES(1, N'000035', N'�� ���������� ���� (456)')
		INSERT INTO dbo.AccessGroup(Version, Code, Name) VALUES(1, N'000061', N'�� ���� (������)')
		INSERT INTO dbo.AccessGroup(Version, Code, Name) VALUES(1, N'000039', N'�� ����������� ���. (54)')
		INSERT INTO dbo.AccessGroup(Version, Code, Name) VALUES(1, N'000043', N'�� ����������� ���� (180) ')
		INSERT INTO dbo.AccessGroup(Version, Code, Name) VALUES(1, N'000024', N'�� ��������� ���. (400)')
		INSERT INTO dbo.AccessGroup(Version, Code, Name) VALUES(1, N'000029', N'�� ������������ ���� (409)')
		INSERT INTO dbo.AccessGroup(Version, Code, Name) VALUES(1, N'000037', N'�� ���������� ������� (100)')
		INSERT INTO dbo.AccessGroup(Version, Code, Name) VALUES(1, N'000042', N'�� ����-��������� ��������� ���������� ����� (1)')
		INSERT INTO dbo.AccessGroup(Version, Code, Name) VALUES(1, N'000018', N'�� ������������ ���. (132)')
		INSERT INTO dbo.AccessGroup(Version, Code, Name) VALUES(1, N'000019', N'�� ����������� ���. (65)')
		INSERT INTO dbo.AccessGroup(Version, Code, Name) VALUES(1, N'000020', N'�� ����������� ���. (127)')
		INSERT INTO dbo.AccessGroup(Version, Code, Name) VALUES(1, N'000051', N'�� �. ������ (72)')
		INSERT INTO dbo.AccessGroup(Version, Code, Name) VALUES(1, N'000023', N'�� ���������� ���. (125)')
		INSERT INTO dbo.AccessGroup(Version, Code, Name) VALUES(1, N'000027', N'�� ����������� ���. (400)')
		INSERT INTO dbo.AccessGroup(Version, Code, Name) VALUES(1, N'000028', N'�� ������������� ���� (517)')
		INSERT INTO dbo.AccessGroup(Version, Code, Name) VALUES(1, N'000046', N'�� �������� ���. (48)')
		INSERT INTO dbo.AccessGroup(Version, Code, Name) VALUES(1, N'000031', N'�� ������������� ���. (44)')
		INSERT INTO dbo.AccessGroup(Version, Code, Name) VALUES(1, N'000047', N'�� ���������� ������������ (11)')
		INSERT INTO dbo.AccessGroup(Version, Code, Name) VALUES(1, N'000054', N'�� ���������� ��������� (3)')
		INSERT INTO dbo.AccessGroup(Version, Code, Name) VALUES(1, N'000038', N'�� ��������� ���. (91)')
		INSERT INTO dbo.AccessGroup(Version, Code, Name) VALUES(1, N'000048', N'�� �������� ���. (12)')
		INSERT INTO dbo.AccessGroup(Version, Code, Name) VALUES(1, N'000049', N'�� ����������� ���. (7)')
		INSERT INTO dbo.AccessGroup(Version, Code, Name) VALUES(1, N'000045', N'�� ����������� ���. (95)')
		INSERT INTO dbo.AccessGroup(Version, Code, Name) VALUES(1, N'000021', N'��� �. ������ (563)')
		INSERT INTO dbo.AccessGroup(Version, Code, Name) VALUES(1, N'000053', N'��� �. �����-��������� (17)')
		INSERT INTO dbo.AccessGroup(Version, Code, Name) VALUES(1, N'000062', N'��� ��������� �������')
		INSERT INTO dbo.AccessGroup(Version, Code, Name) VALUES(1, N'000030', N'��� ���������� ���. (51)')
		INSERT INTO dbo.AccessGroup(Version, Code, Name) VALUES(1, N'000016', N'�� ��������� ���� (446)')
		INSERT INTO dbo.AccessGroup(Version, Code, Name) VALUES(1, N'000026', N'�� ����������� ���. (432)')
		INSERT INTO dbo.AccessGroup(Version, Code, Name) VALUES(1, N'000032', N'�� ������������� ���. (1057)')
		INSERT INTO dbo.AccessGroup(Version, Code, Name) VALUES(1, N'000033', N'�� ������ ���. (237)')
		INSERT INTO dbo.AccessGroup(Version, Code, Name) VALUES(1, N'000034', N'�� ������������ ���. (258)')
		INSERT INTO dbo.AccessGroup(Version, Code, Name) VALUES(1, N'000036', N'�� ���������� ����� (23)')
		INSERT INTO dbo.AccessGroup(Version, Code, Name) VALUES(1, N'000050', N'�� ������������ ���. (3)')
		INSERT INTO dbo.AccessGroup(Version, Code, Name) VALUES(1, N'000040', N'�� ������� ���. (171)')
		INSERT INTO dbo.AccessGroup(Version, Code, Name) VALUES(1, N'000041', N'�� ��������� ���. (104)')
		INSERT INTO dbo.AccessGroup(Version, Code, Name) VALUES(1, N'000044', N'�� ����������� ���. (295)')

		--InsuredPersonType (���� ������)
		INSERT INTO dbo.InsuredPersonType(Version, Code, Name) VALUES(1, N'1', N'����������')
		INSERT INTO dbo.InsuredPersonType(Version, Code, Name) VALUES(1, N'2', N'����������� ��������, ��������� ����������� �� ���������� ��')
		INSERT INTO dbo.InsuredPersonType(Version, Code, Name) VALUES(1, N'3', N'����������� ��������, �������� ����������� �� ���������� ��')
		INSERT INTO dbo.InsuredPersonType(Version, Code, Name) VALUES(1, N'4', N'����������� ��������, �������� ����������� �� ���������� ��, ��� ������������ �������� ���������')
		INSERT INTO dbo.InsuredPersonType(Version, Code, Name) VALUES(1, N'5', N'���������� ��������� ����������� ��������, �������� ����������� �� ���������� ��, ��� ������������ �������� ���������')
		INSERT INTO dbo.InsuredPersonType(Version, Code, Name) VALUES(1, N'6', N'����������� ��������, �������� ����������� �� ���������� ��, � �������� ��������� ������������ �������� ��������')
		INSERT INTO dbo.InsuredPersonType(Version, Code, Name) VALUES(1, N'7', N'����������������������� ����������� ����������� � ����� �� �����, ��������� ����������� �� ���������� ��')
		INSERT INTO dbo.InsuredPersonType(Version, Code, Name) VALUES(1, N'8', N'����������������������� ����������� ����������� � ����� �� �����, �������� ����������� �� ���������� ��')
		INSERT INTO dbo.InsuredPersonType(Version, Code, Name) VALUES(1, N'9', N'����������� ��������, �������� ����������� �� ���������� ��, �� ���������� �����������')

		--PersonalAccountContractor (���� ������)
		INSERT INTO dbo.PersonalAccountContractor(Version, Code, Name) VALUES(1, N'00150', N'���������� �� #3')
		INSERT INTO dbo.PersonalAccountContractor(Version, Code, Name) VALUES(1, N'00151', N'���������� �� #4')
		INSERT INTO dbo.PersonalAccountContractor(Version, Code, Name) VALUES(1, N'000000001', N'�� #1')
		INSERT INTO dbo.PersonalAccountContractor(Version, Code, Name) VALUES(1, N'000000011', N'�� #2')

		--Schedule (���� ������)
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'f99856a6-b750-11df-2d9b-003048ba0539', N'0.8 ')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'fc58af06-9eb1-11df-5d87-00304861d218', N'16,8 � (0,42 ������)')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'2ac389d7-9921-11e3-80c7-002590d1e727', N'0,075')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'ada03614-b659-11e3-80c8-002590d1e727', N'0,025')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'73f04627-cece-11e3-80c8-002590d1e727', N'0,22 ������')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'f4d4171a-1a2c-11e4-80c8-002590d1e727', N'0,975 ������')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'ae226b18-52e0-11e4-80c8-002590d1e727', N'0,045 ������')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'66d0308d-6a5f-11e4-80d1-002590d1e727', N'6,0�����(0.15��)')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'e46671f8-140d-11e1-81fb-003048ba0538', N'0,7 ������ (28 �����)')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'f6bd90b2-03d6-11e3-8233-003048ba0538', N'0.63 ������')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'1583436d-8c6e-11de-839b-00304861d218', N'12 � (0,3������)')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'febaac53-e21c-11dd-8574-00304861d218', N'������ 2 ����� 2 �� 11 ����� 1�����')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'febaac54-e21c-11dd-8574-00304861d218', N'������ 2 ����� 2 2�����')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'8c9e6dc5-6129-11de-86d4-00304861d218', N'1 ��� ')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'd906b1b2-61f0-11de-86d4-00304861d218', N'0,16')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'b042393a-c242-11e0-87d1-003048ba0538', N'0,2 ������')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'3162fd27-c820-11dd-87ea-00304861d218', N'���������� 20 �����')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'71d159a1-5a0d-11e2-8a08-003048ba0538', N'0,45 ������')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'f7aa8f0c-bf2f-11e1-8aef-003048ba0538', N'0,05')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'd0b82d2e-ce8d-434d-8c81-1c3913800dc7', N'35 ����� (0,875��.)')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'521ce3a4-d4c2-11de-8ca7-00304861d219', N'14� (0,35 ������)')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'63c22fd6-eced-4d28-8da9-069188cdf26c', N' ����������   0,2 ������')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'acb3070b-6bf0-11e1-8ec0-003048ba0538', N'0,55 ������')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'b26e39cc-bc37-11e0-8fd0-003048ba0538', N'0,01')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'756a6d55-2f03-11de-9079-00304861d218', N'��������')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'5008e5fa-7283-11de-947f-00304861d218', N'39.6 ���� ( 0.99��.)')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'f73ab4b8-1c39-11df-9641-00304861d219', N'10 ����� (0,25)')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'ec155a9c-6cb3-11e1-96a3-003048ba0538', N'0,68 ������ (27,2 ����)')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'61c4a9ce-518a-11de-9784-00304861d218', N'32 ���� ( 0.8��.)')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'61c4a9d6-518a-11de-9784-00304861d218', N'29,2 ���� (0.73 ��.)')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'c0dded9a-56f9-11de-9784-00304861d218', N'36,4 ���� ( 0,91��.)')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'c0dded9b-56f9-11de-9784-00304861d218', N'34,8 ���� ( 0,87 ��.)')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'c0dded9c-56f9-11de-9784-00304861d218', N'36.8 ����  (0,92��.)')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'c0ddeda2-56f9-11de-9784-00304861d218', N'33.6 ���� ( 0.84��.)')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'bce58c8b-5973-11de-9784-00304861d218', N'30 (0,75��)')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'05006ae7-5af8-11de-9784-00304861d218', N'38,4 ���� (0,96��)')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'fbcb3980-a8aa-11de-995b-00304861d218', N'������������� ����')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'cfa8292d-53ab-11dd-9b24-0010dcc22269', N'�������� ������ 40����� ������')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'de51bdda-7bea-11de-9c3b-00304861d218', N'24 �(0,6��)')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'b9b91cc4-44e3-11de-9cd9-00304861d218', N'0,8 �����')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'c8f5409c-4512-11de-9cd9-00304861d218', N'4 ���� (0,1 ��)')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'7e4d4f62-0cb9-11e0-9fcb-003048ba0538', N'���������� 0.4��')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'1dd16838-5a44-11de-a159-003048359abd', N'���������� 20 �����')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'ce213082-5fe4-11e2-a43a-003048ba0538', N'�����������')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'ffdfa5b5-6e88-11dd-a46b-00304861d218', N'�������� ������ 20 �����')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'cd0cecb3-6f55-11dd-a46b-00304861d218', N'�������� ������ 20����� ������ (�������)')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'50398604-8878-11de-a46e-003048359abd', N'���������� 32 ����')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'99320ebb-5484-11dd-a7f9-0008a1a11604', N'�������� ������')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'99320edd-5484-11dd-a7f9-0008a1a11604', N'���. ���� ���. �������')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'799797be-dcfd-11df-ab02-003048ba0538', N'���������� 30 �����')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'99b7f7c2-7b6b-11de-ad67-003048359abd', N'10-�������')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'1692bfc9-8b8a-11e2-aec5-003048ba0538', N'36 ����� (0,9 ��.)')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'b149c25e-ae9a-11df-af89-003048ba0539', N'���������� (30 �����) ')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'8856fcc0-d13e-11dd-b086-00308d000000', N'�������� ������ (���������)')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'd74d4e81-f80c-11e2-b1b0-003048ba0538', N'0,44 ������')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'b640afc7-f4b7-11e0-b3a0-003048ba0538', N'0,1')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'e3acbc3a-ded4-11de-b424-00304861d219', N'20 ����� (0,5 ��.)')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'b88e0281-4fec-11e3-b49b-003048ba0538', N'15 ����� (0,38 ��.)')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'7777d5c2-75cd-11de-b4c1-00304861d218', N'25� (0,625)')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'6f224468-7670-11de-b4c1-00304861d218', N'0,01')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'bfab25e3-7825-11de-b4c1-00304861d218', N'0,001')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'b2783d7a-d503-11de-b4ed-003048359abd', N'���������� 10����� (�������)')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'60f2719a-e959-11de-b4ed-003048359abd', N'����������� ���� �� 7 ����� (���������)')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'0033bf3b-eb11-11de-b4ed-003048359abd', N'���������� 35 ����� (�������)')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'14cdc40f-eb7f-11dd-b538-00304861d218', N'������ 15�/� (���������)')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'14cdc410-eb7f-11dd-b538-00304861d218', N'������ 25�/� (���������)')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'e45f5464-c4be-4957-b6e9-5d878aacce67', N'37,5 ����� (0,94 ��)')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'bfbed33a-a80e-11de-b733-003048359abd', N'���������� 37,5')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'91f99117-c9d5-11de-b733-003048359abd', N'���������� 35')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'7dd7f374-e861-11e2-b868-003048ba0538', N'0,86������')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'9e15309f-3397-11dd-b8e4-00304861d218', N'�������')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'7b0b10df-a356-11de-b935-00304861d218', N'16 � (0,4������)')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'e1f76452-2b01-11dd-bc32-00304861d218', N'����������')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'13e773f9-4129-11de-bf01-00304861d218', N'20 �����')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'f1bfede7-4423-11de-bf01-00304861d218', N'35,6 ���� (0,89��.)')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'9bf8e0a9-a1b5-11de-bff9-00304861d218', N'6,4 ���� (0,16 ��)')
		INSERT INTO dbo.Schedule(Version, Code, Name) VALUES(1, N'9bf8e0aa-a1b5-11de-bff9-00304861d218', N'6,8 ���� (0,17 ��)')

		--Signer
		INSERT INTO Signer(Version, Name, PreamblePartyTemplate,Position)
		VALUES (1, N'������������ ������ �������', N'������������� ��������� ��������������� �������� ������� "�����������" ��� "����������" ������������� ������ ��������, ������������ �� ��������� ������������ � 6/�� �� 16.09.2014�.', N'������������ �������� ��������������� �������� ������� "�����������" ��� "����������"')

		INSERT INTO Signer(Version, Name, PreamblePartyTemplate,Position)
		VALUES (1, N'���������� ����� ����������', N'����������� ���������� ��������� ���������������� � ����� ��� "����������" ����������� ����� ����������, ����������� �� ��������� ������������ � 8 �� 15.01.2015 �.', N'���������� ���������� ��������� ���������������� � ����� ��� "����������"')


		--PersonnelOrderExtraCharges
		INSERT INTO PersonnelOrderExtraCharges(Version, Code1C, Name)	VALUES(1, N'1', N'������ 1 �������')
		INSERT INTO PersonnelOrderExtraCharges(Version, Code1C, Name)	VALUES(1, N'2', N'������ 2 �������')
		INSERT INTO PersonnelOrderExtraCharges(Version, Code1C, Name)	VALUES(1, N'3', N'������ 3 �������')
		INSERT INTO PersonnelOrderExtraCharges(Version, Code1C, Name)	VALUES(1, N'4', N'������ 4 �������')
		INSERT INTO PersonnelOrderExtraCharges(Version, Code1C, Name)	VALUES(1, N'5', N'������ 1 ��������')
		INSERT INTO PersonnelOrderExtraCharges(Version, Code1C, Name)	VALUES(1, N'6', N'������ 2 ��������')
		INSERT INTO PersonnelOrderExtraCharges(Version, Code1C, Name)	VALUES(1, N'7', N'������ 3 ��������')
		INSERT INTO PersonnelOrderExtraCharges(Version, Code1C, Name)	VALUES(1, N'8', N'������ 4 ��������')
		

--��������� ������ ��� ������������ - �����
--������� ������
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
		SELECT '���� ���������', '��' FROM RequestAttachment WHERE RequestType = 201 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� ���������', '���' 
	END

	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 202 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� ���', '��' FROM RequestAttachment WHERE RequestType = 202 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� ���', '���' 
	END
	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 203 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� �����', '��' FROM RequestAttachment WHERE RequestType = 203 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� �����', '���' 
	END
	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 204 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� ������� �� ������������', '��' FROM RequestAttachment WHERE RequestType = 204 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� ������� �� ������������', '���' 
	END

	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 211 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� ��������', '��' FROM RequestAttachment WHERE RequestType = 211 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� ��������', '���' 
	END

	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 221 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� ��������� �� �����������', '��' FROM RequestAttachment WHERE RequestType = 221 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� ��������� �� �����������', '���' 
	END

	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 222 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� ��������� � �������������� �����������', '��' FROM RequestAttachment WHERE RequestType = 222 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� ��������� � �������������� �����������', '���' 
	END

	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 223 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� ��������� � �������������� �����������', '��' FROM RequestAttachment WHERE RequestType = 223 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� ��������� � �������������� �����������', '���' 
	END
	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 224 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� ��������� � ��������� ������������', '��' FROM RequestAttachment WHERE RequestType = 224 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� ��������� � ��������� ������������', '���' 
	END
	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 231 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� ������������� � �����', '��' FROM RequestAttachment WHERE RequestType = 231 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� ������������� � �����', '���' 
	END
	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 232 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� ������������ � �������� �����', '��' FROM RequestAttachment WHERE RequestType = 232 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� ������������ � �������� �����', '���' 
	END
	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 241 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� �������� ������', '��' FROM RequestAttachment WHERE RequestType = 241 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� �������� ������', '���' 
	END
	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 242 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� ���������������� ������', '��' FROM RequestAttachment WHERE RequestType = 242 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� ���������������� ������', '���' 
	END
	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 251 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� �������� ������', '��' FROM RequestAttachment WHERE RequestType = 251 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� �������� ������', '���' 
	END
	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 252 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� �������� � �������� ������', '��' FROM RequestAttachment WHERE RequestType = 252 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� �������� � �������� ������', '���' 
	END
	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 261 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� ����������� ������ �������� �� ��������� ������������ ������', '��' FROM RequestAttachment WHERE RequestType = 261 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� ����������� ������ �������� �� ��������� ������������ ������', '���' 
	END
	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 262 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� ����������� ������ � ������������� ��������', '��' FROM RequestAttachment WHERE RequestType = 262 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� ����������� ������ � ������������� ��������', '���' 
	END
	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 271 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� ��������� � ������ �� ������', '��' FROM RequestAttachment WHERE RequestType = 271 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� ��������� � ������ �� ������', '���' 
	END
	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 272 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� ��������� ��������', '��' FROM RequestAttachment WHERE RequestType = 272 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� ��������� ��������', '���' 
	END
	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 273 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� ������� � ������', '��' FROM RequestAttachment WHERE RequestType = 273 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� ������� � ������', '���' 
	END
	/*
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 274 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� �2', '��' FROM RequestAttachment WHERE RequestType = 274 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� �2', '���' 
	END
	*/
	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 275 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� �������� � ������������ ���������������', '��' FROM RequestAttachment WHERE RequestType = 275 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� �������� � ������������ ���������������', '���' 
	END
	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 276 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� �� ������������ ������', '��' FROM RequestAttachment WHERE RequestType = 276 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� �� ������������ ������', '���' 
	END
	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 277 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� ������������� ������������������', '��' FROM RequestAttachment WHERE RequestType = 277 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� ������������� ������������������', '���' 
	END
	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 278 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� ������ �� ����� ������', '��' FROM RequestAttachment WHERE RequestType = 278 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� ������ �� ����� ������', '���' 
	END
	/*
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 279 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� ������� ������� ����', '��' FROM RequestAttachment WHERE RequestType = 279 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� ������� ������� ����', '���' 
	END
	*/
	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 280 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� ������� ���������� � ���������� ������������, ���������� � ��������� �����', '��' FROM RequestAttachment WHERE RequestType = 280 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� ������� ���������� � ���������� ������������, ���������� � ��������� �����', '���' 
	END
	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 281 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� ���������� �� ����������� ����������� ��������, ������������ ������������, � ��������� �����', '��' FROM RequestAttachment WHERE RequestType = 281 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� ���������� �� ����������� ����������� ��������, ������������ ������������, � ��������� �����', '���' 
	END
	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 282 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� �������� ����������� ���� �� �������� ������������ ������ (���������� �3)', '��' FROM RequestAttachment WHERE RequestType = 282 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� �������� ����������� ���� �� �������� ������������ ������ (���������� �3)', '���' 
	END
	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 283 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� ������� �� ���������� ���������� ��� ����������� �������� ������ ������������ ��� (���������� 1)', '��' FROM RequestAttachment WHERE RequestType = 283 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� ������� �� ���������� ���������� ��� ����������� �������� ������ ������������ ��� (���������� 1)', '���' 
	END
	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 284 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� ������� �� ������������ �������� � ����� ������������ ��� (���������� 2)', '��' FROM RequestAttachment WHERE RequestType = 284 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� ������� �� ������������ �������� � ����� ������������ ��� (���������� 2)', '���' 
	END
	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 285 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� ������������� � ������������� ������������ � ��������� �����', '��' FROM RequestAttachment WHERE RequestType = 285 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� ������������� � ������������� ������������ � ��������� �����', '���' 
	END
	
--select * from dbo.fnGetEmploymentAttachmentList(36) order by AttachmentTypeName

	RETURN 
END

GO
--������� �����