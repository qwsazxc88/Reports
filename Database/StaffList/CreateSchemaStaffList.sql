--������ ������� ��������� ���� ������ ��� ������� �������� ����������, ������� ������� ���� � ��������� ����� ����������� ���������� �������
--������ �� ������ ��������� ��� ������������ ������������, ������ ����������� ����� ������� ����������� ��� �������� ����������
--RETURN
use WebAppTest
go

--1. �������� ������
--��� ��� ����������� ���������� ������������� ���������, ������ ����� ��� ������ ������ 
IF OBJECT_ID ('FK_StaffMovements_StaffEstablishedPostRequest', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffMovements] DROP CONSTRAINT [FK_StaffMovements_StaffEstablishedPostRequest]
GO
IF OBJECT_ID ('FK_StaffMovements_StaffEstablishedPost', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffMovements] DROP CONSTRAINT [FK_StaffMovements_StaffEstablishedPost]
GO


--��� ������� ������������
UPDATE Users SET SEPId = null
IF OBJECT_ID ('FK_Users_StaffEstablishedPost', 'F') IS NOT NULL
	ALTER TABLE [dbo].[Users] DROP CONSTRAINT [FK_Users_StaffEstablishedPost]
GO



IF OBJECT_ID ('FK_RefAddresses_Editors', 'F') IS NOT NULL
	ALTER TABLE [dbo].[RefAddresses] DROP CONSTRAINT [FK_RefAddresses_Editors]
GO

IF OBJECT_ID ('FK_RefAddresses_Creators', 'F') IS NOT NULL
	ALTER TABLE [dbo].[RefAddresses] DROP CONSTRAINT [FK_RefAddresses_Creators]
GO

IF OBJECT_ID ('FK_StaffProgramCodes_StaffProgramReference', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffProgramCodes] DROP CONSTRAINT [FK_StaffProgramCodes_StaffProgramReference]
GO

IF OBJECT_ID ('FK_StaffProgramCodes_StaffDepartmentManagerDetails', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffProgramCodes] DROP CONSTRAINT [FK_StaffProgramCodes_StaffDepartmentManagerDetails]
GO

IF OBJECT_ID ('FK_StaffProgramCodes_EditorUser', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffProgramCodes] DROP CONSTRAINT [FK_StaffProgramCodes_EditorUser]
GO

IF OBJECT_ID ('FK_StaffProgramCodes_CreatorUser', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffProgramCodes] DROP CONSTRAINT [FK_StaffProgramCodes_CreatorUser]
GO

IF OBJECT_ID ('FK_StaffEstablishedPostRequest_StaffEstablishedPostRequestTypes', 'F') IS NOT NULL	
	ALTER TABLE [dbo].[StaffEstablishedPostRequest] DROP CONSTRAINT [FK_StaffEstablishedPostRequest_StaffEstablishedPostRequestTypes]
GO

IF OBJECT_ID ('FK_StaffEstablishedPostRequest_StaffEstablishedPost', 'F') IS NOT NULL	
	ALTER TABLE [dbo].[StaffEstablishedPostRequest] DROP CONSTRAINT [FK_StaffEstablishedPostRequest_StaffEstablishedPost]
GO

IF OBJECT_ID ('FK_StaffEstablishedPostRequest_Position', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffEstablishedPostRequest] DROP CONSTRAINT [FK_StaffEstablishedPostRequest_Position]
GO

IF OBJECT_ID ('FK_StaffEstablishedPostRequest_EditorUsers', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffEstablishedPostRequest] DROP CONSTRAINT [FK_StaffEstablishedPostRequest_EditorUsers]
GO

IF OBJECT_ID ('FK_StaffEstablishedPostRequest_Department', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffEstablishedPostRequest] DROP CONSTRAINT [FK_StaffEstablishedPostRequest_Department]
GO

IF OBJECT_ID ('FK_StaffEstablishedPostRequest_CreatorUser', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffEstablishedPostRequest] DROP CONSTRAINT [FK_StaffEstablishedPostRequest_CreatorUser]
GO

IF OBJECT_ID ('FK_StaffEstablishedPostRequest_AppointmentReason', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffEstablishedPostRequest] DROP CONSTRAINT [FK_StaffEstablishedPostRequest_AppointmentReason]
GO

IF OBJECT_ID ('FK_StaffEstablishedPostArchive_StaffEstablishedPost', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffEstablishedPostArchive] DROP CONSTRAINT [FK_StaffEstablishedPostArchive_StaffEstablishedPost]
GO

IF OBJECT_ID ('FK_StaffEstablishedPostArchive_Position', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffEstablishedPostArchive] DROP CONSTRAINT [FK_StaffEstablishedPostArchive_Position]
GO

IF OBJECT_ID ('FK_StaffEstablishedPostArchive_Department', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffEstablishedPostArchive] DROP CONSTRAINT [FK_StaffEstablishedPostArchive_Department]
GO

IF OBJECT_ID ('FK_StaffEstablishedPostArchive_CreatorUser', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffEstablishedPostArchive] DROP CONSTRAINT [FK_StaffEstablishedPostArchive_CreatorUser]
GO

IF OBJECT_ID ('FK_StaffEstablishedPost_Position', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffEstablishedPost] DROP CONSTRAINT [FK_StaffEstablishedPost_Position]
GO

IF OBJECT_ID ('FK_StaffEstablishedPost_EditorUser', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffEstablishedPost] DROP CONSTRAINT [FK_StaffEstablishedPost_EditorUser]
GO

IF OBJECT_ID ('FK_StaffEstablishedPost_Department', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffEstablishedPost] DROP CONSTRAINT [FK_StaffEstablishedPost_Department]
GO

IF OBJECT_ID ('FK_StaffEstablishedPost_CreatorUser', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffEstablishedPost] DROP CONSTRAINT [FK_StaffEstablishedPost_CreatorUser]
GO

IF OBJECT_ID ('FK_StaffDepartmentRequest_StaffDepartmentRequestTypes', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffDepartmentRequest] DROP CONSTRAINT [FK_StaffDepartmentRequest_StaffDepartmentRequestTypes]
GO

IF OBJECT_ID ('FK_StaffDepartmentRequest_RefAddresses', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffDepartmentRequest] DROP CONSTRAINT [FK_StaffDepartmentRequest_RefAddresses]
GO

IF OBJECT_ID ('FK_StaffDepartmentRequest_RefAddresses', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffDepartmentRequest] DROP CONSTRAINT [FK_StaffDepartmentRequest_RefAddresses]
GO

IF OBJECT_ID ('FK_StaffDepartmentRequest_DepartmentParent', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffDepartmentRequest] DROP CONSTRAINT [FK_StaffDepartmentRequest_DepartmentParent]
GO

IF OBJECT_ID ('FK_StaffDepartmentRequest_EditorUser', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffDepartmentRequest] DROP CONSTRAINT [FK_StaffDepartmentRequest_EditorUser]
GO

IF OBJECT_ID ('FK_StaffDepartmentRequest_DepartmentNext', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffDepartmentRequest] DROP CONSTRAINT [FK_StaffDepartmentRequest_DepartmentNext]
GO

IF OBJECT_ID ('FK_StaffDepartmentRequest_Department', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffDepartmentRequest] DROP CONSTRAINT [FK_StaffDepartmentRequest_Department]
GO

IF OBJECT_ID ('FK_StaffDepartmentRequest_CreatorUser', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffDepartmentRequest] DROP CONSTRAINT [FK_StaffDepartmentRequest_CreatorUser]
GO

IF OBJECT_ID ('FK_StaffDepartmentOperationLinks_StaffDepartmentOperations', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffDepartmentOperationLinks] DROP CONSTRAINT [FK_StaffDepartmentOperationLinks_StaffDepartmentOperations]
GO

IF OBJECT_ID ('FK_StaffDepartmentOperationLinks_StaffDepartmentManagerDetails', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffDepartmentOperationLinks] DROP CONSTRAINT [FK_StaffDepartmentOperationLinks_StaffDepartmentManagerDetails]
GO

IF OBJECT_ID ('FK_StaffDepartmentOperationLinks_EditorUser', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffDepartmentOperationLinks] DROP CONSTRAINT [FK_StaffDepartmentOperationLinks_EditorUser]
GO

IF OBJECT_ID ('FK_StaffDepartmentOperationLinks_CreatorUser', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffDepartmentOperationLinks] DROP CONSTRAINT [FK_StaffDepartmentOperationLinks_CreatorUser]
GO

IF OBJECT_ID ('FK_StaffDepartmentManagerDetails_StaffDepartmentTypes', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffDepartmentManagerDetails] DROP CONSTRAINT [FK_StaffDepartmentManagerDetails_StaffDepartmentTypes]
GO

IF OBJECT_ID ('FK_StaffDepartmentManagerDetails_StaffNetShopIdentification', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffDepartmentManagerDetails] DROP CONSTRAINT [FK_StaffDepartmentManagerDetails_StaffNetShopIdentification]
GO

IF OBJECT_ID ('FK_StaffDepartmentManagerDetails_StaffDepartmentReasons', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffDepartmentManagerDetails] DROP CONSTRAINT [FK_StaffDepartmentManagerDetails_StaffDepartmentReasons]
GO

IF OBJECT_ID ('FK_StaffDepartmentManagerDetails_StaffDepartmentRequest', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffDepartmentManagerDetails] DROP CONSTRAINT [FK_StaffDepartmentManagerDetails_StaffDepartmentRequest]
GO

IF OBJECT_ID ('FK_StaffDepartmentManagerDetails_RefAddresses', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffDepartmentManagerDetails] DROP CONSTRAINT [FK_StaffDepartmentManagerDetails_RefAddresses]
GO

IF OBJECT_ID ('FK_StaffDepartmentManagerDetails_EditorUser', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffDepartmentManagerDetails] DROP CONSTRAINT [FK_StaffDepartmentManagerDetails_EditorUser]
GO

IF OBJECT_ID ('FK_StaffDepartmentManagerDetails_CreatorUser', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffDepartmentManagerDetails] DROP CONSTRAINT [FK_StaffDepartmentManagerDetails_CreatorUser]
GO

IF OBJECT_ID ('FK_StaffDepartmentLandmarks_StaffLandmarkTypes', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffDepartmentLandmarks] DROP CONSTRAINT [FK_StaffDepartmentLandmarks_StaffLandmarkTypes]
GO

IF OBJECT_ID ('FK_StaffDepartmentLandmarks_StaffDepartmentManagerDetails', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffDepartmentLandmarks] DROP CONSTRAINT [FK_StaffDepartmentLandmarks_StaffDepartmentManagerDetails]
GO

IF OBJECT_ID ('FK_StaffDepartmentLandmarks_EditorUser', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffDepartmentLandmarks] DROP CONSTRAINT [FK_StaffDepartmentLandmarks_EditorUser]
GO

IF OBJECT_ID ('FK_StaffDepartmentLandmarks_CreatorUser', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffDepartmentLandmarks] DROP CONSTRAINT [FK_StaffDepartmentLandmarks_CreatorUser]
GO

IF OBJECT_ID ('FK_StaffDepartmentCBDetails_StaffDepartmentRequest', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffDepartmentCBDetails] DROP CONSTRAINT [FK_StaffDepartmentCBDetails_StaffDepartmentRequest]
GO

IF OBJECT_ID ('FK_StaffDepartmentCBDetails_EditorUser', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffDepartmentCBDetails] DROP CONSTRAINT [FK_StaffDepartmentCBDetails_EditorUser]
GO

IF OBJECT_ID ('FK_StaffDepartmentCBDetails_CreatorUser', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffDepartmentCBDetails] DROP CONSTRAINT [FK_StaffDepartmentCBDetails_CreatorUser]
GO

IF OBJECT_ID ('FK_StaffDepartmentCBDetails_DepCashin', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffDepartmentCBDetails] DROP CONSTRAINT [FK_StaffDepartmentCBDetails_DepCashin]
GO

IF OBJECT_ID ('FK_StaffDepartmentCBDetails_DepATM', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffDepartmentCBDetails] DROP CONSTRAINT [FK_StaffDepartmentCBDetails_DepATM]
GO

IF OBJECT_ID ('FK_DepartmentArchive_Department', 'F') IS NOT NULL
	ALTER TABLE [dbo].[DepartmentArchive] DROP CONSTRAINT [FK_DepartmentArchive_Department]
GO

IF OBJECT_ID ('FK_DepartmentArchive_CreatorUser', 'F') IS NOT NULL
	ALTER TABLE [dbo].[DepartmentArchive] DROP CONSTRAINT [FK_DepartmentArchive_CreatorUser]
GO

IF OBJECT_ID ('FK_StaffPostChargeLinks_StaffExtraCharges', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffPostChargeLinks] DROP CONSTRAINT [FK_StaffPostChargeLinks_StaffExtraCharges]
GO

IF OBJECT_ID ('FK_StaffPostChargeLinks_Staff', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffPostChargeLinks] DROP CONSTRAINT [FK_StaffPostChargeLinks_Staff]
GO

IF OBJECT_ID ('FK_StaffPostChargeLinks_EditorUser', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffPostChargeLinks] DROP CONSTRAINT [FK_StaffPostChargeLinks_EditorUser]
GO

IF OBJECT_ID ('FK_StaffPostChargeLinks_CreatorUser', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffPostChargeLinks] DROP CONSTRAINT [FK_StaffPostChargeLinks_CreatorUser]
GO

IF OBJECT_ID ('FK_StaffDepartmentOperationModes_StaffDepartmentManagerDetails', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffDepartmentOperationModes] DROP CONSTRAINT [FK_StaffDepartmentOperationModes_StaffDepartmentManagerDetails]
GO

IF OBJECT_ID ('FK_StaffDepartmentOperationModes_EditorUser', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffDepartmentOperationModes] DROP CONSTRAINT [FK_StaffDepartmentOperationModes_EditorUser]
GO

IF OBJECT_ID ('FK_StaffDepartmentOperationModes_CreatorUser', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffDepartmentOperationModes] DROP CONSTRAINT [FK_StaffDepartmentOperationModes_CreatorUser]
GO

IF OBJECT_ID ('FK_StaffEstablishedPostChargeLinks_StaffExtraCharges', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffEstablishedPostChargeLinks] DROP CONSTRAINT [FK_StaffEstablishedPostChargeLinks_StaffExtraCharges]
GO

IF OBJECT_ID ('FK_StaffEstablishedPostChargeLinks_StaffEstablishedPostRequest', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffEstablishedPostChargeLinks] DROP CONSTRAINT [FK_StaffEstablishedPostChargeLinks_StaffEstablishedPostRequest]
GO

IF OBJECT_ID ('FK_StaffEstablishedPostChargeLinks_StaffEstablishedPost', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffEstablishedPostChargeLinks] DROP CONSTRAINT [FK_StaffEstablishedPostChargeLinks_StaffEstablishedPost]
GO

IF OBJECT_ID ('FK_StaffEstablishedPostChargeLinks_EditorUser', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffEstablishedPostChargeLinks] DROP CONSTRAINT [FK_StaffEstablishedPostChargeLinks_EditorUser]
GO

IF OBJECT_ID ('FK_StaffEstablishedPostChargeLinks_CreatorUser', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffEstablishedPostChargeLinks] DROP CONSTRAINT [FK_StaffEstablishedPostChargeLinks_CreatorUser]
GO


--2. �������� ������
if OBJECT_ID (N'Kladr', 'U') is not null
	DROP TABLE [dbo].[Kladr]
GO
CREATE TABLE [dbo].[Kladr](
	[Name] [nvarchar](52) NULL,
	[ShortName] [nvarchar](12) NULL,
	[Index] [nvarchar](6) NULL,
	[AltName] [nvarchar](50) NULL,
	[AddressType] [nvarchar](1) NULL,
	[RegionCode] [nvarchar](2) NULL,
	[AreaCode] [nvarchar](3) NULL,
	[CityCode] [nvarchar](3) NULL,
	[SettlementCode] [nvarchar](3) NULL,
	[StreetCode] [nvarchar](4) NULL,
	[Code] [nvarchar](50) NULL
) ON [PRIMARY]

GO


if OBJECT_ID (N'RefAddresses', 'U') is not null
	DROP TABLE [dbo].[RefAddresses]
GO	
CREATE TABLE [dbo].[RefAddresses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Version] [int] NOT NULL,
	[Address] [nvarchar](400) NULL,
	[PostIndex] [nvarchar](6) NULL,
	[RegionCode] [nvarchar](30) NULL,
	[RegionName] [nvarchar](50) NULL,
	[AreaCode] [nvarchar](30) NULL,
	[AreaName] [nvarchar](50) NULL,
	[CityCode] [nvarchar](30) NULL,
	[CityName] [nvarchar](50) NULL,
	[SettlementCode] [nvarchar](30) NULL,
	[SettlementName] [nvarchar](50) NULL,
	[StreetCode] [nvarchar](30) NULL,
	[StreetName] [nvarchar](50) NULL,
	[HouseType] [int] NULL,
	[HouseNumber] [nvarchar](10) NULL,
	[BuildType] [int] NULL,
	[BuildNumber] [nvarchar](10) NULL,
	[FlatType] [int] NULL,
	[FlatNumber] [nvarchar](5) NULL,
	[IsUsed] [bit] NULL,
	[CreatorId] [int] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[EditorId] [int] NULL,
	[EditDate] [datetime] NULL,
 CONSTRAINT [PK_RefAddresses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO



if OBJECT_ID (N'StaffProgramReference', 'U') is not null
	DROP TABLE [dbo].[StaffProgramReference]
GO	
CREATE TABLE [dbo].[StaffProgramReference](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[CreateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_StaffProgramReference] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO



if OBJECT_ID (N'StaffProgramCodes', 'U') is not null
	DROP TABLE [dbo].[StaffProgramCodes]
GO	
CREATE TABLE [dbo].[StaffProgramCodes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Version] [int] NOT NULL,
	[DMDetailId] [int] NULL,
	[ProgramId] [int] NULL,
	[Code] [nvarchar](20) NULL,
	[CreatorId] [int] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[EditorId] [int] NULL,
	[EditDate] [datetime] NULL,
 CONSTRAINT [PK_StaffProgramCodes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


if OBJECT_ID (N'StaffLandmarkTypes', 'U') is not null
	DROP TABLE [dbo].[StaffLandmarkTypes]
GO	
CREATE TABLE [dbo].[StaffLandmarkTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[CreateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_StaffLandmarkTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


if OBJECT_ID (N'StaffExtraCharges', 'U') is not null
	DROP TABLE [dbo].[StaffExtraCharges]
GO	
CREATE TABLE [dbo].[StaffExtraCharges](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GUID] [nvarchar](40) NULL,
	[Name] [nvarchar](100) NULL,
 CONSTRAINT [PK_StaffExtraCharges] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


if OBJECT_ID (N'StaffEstablishedPostRequest', 'U') is not null
	DROP TABLE [dbo].[StaffEstablishedPostRequest]
GO	
CREATE TABLE [dbo].[StaffEstablishedPostRequest](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Version] [int] NOT NULL,
	[DateRequest] [datetime] NULL,
	[RequestTypeId] [int] NOT NULL,
	[SEPId] [int] NULL,
	[PositionId] [int] NULL,
	[DepartmentId] [int] NULL,
	[Quantity] [int] NULL,
	[Salary] [numeric](18, 2) NULL,
	[IsUsed] [bit] NULL,
	[IsDraft] [bit] NULL,
	[DateSendToApprove] [datetime] NULL,
	[BeginAccountDate] [datetime] NULL,
	[ReasonId] [int] NULL,
	[CreatorID] [int] NULL,
	[CreateDate] [datetime] NULL,
	[EditorID] [int] NULL,
	[EditDate] [datetime] NULL,
 CONSTRAINT [PK_StaffEstablishedPostRequest] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


if OBJECT_ID (N'StaffEstablishedPostArchive', 'U') is not null
	DROP TABLE [dbo].[StaffEstablishedPostArchive]
GO	
CREATE TABLE [dbo].[StaffEstablishedPostArchive](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SEPId] [int] NOT NULL,
	[PositionId] [int] NULL,
	[DepartmentId] [int] NULL,
	[Quantity] [int] NULL,
	[Salary] [numeric](18, 2) NULL,
	[IsUsed] [bit] NULL,
	[BeginAccountDate] [datetime] NULL,
	[Priority] [int] NULL,
	[CreatorID] [int] NULL,
	[CreateDate] [datetime] NULL,
 CONSTRAINT [PK_StaffEstablishedPostArchive] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


if OBJECT_ID (N'StaffEstablishedPost', 'U') is not null
	DROP TABLE [dbo].[StaffEstablishedPost]
GO
CREATE TABLE [dbo].[StaffEstablishedPost](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Version] [int] NOT NULL,
	[PositionId] [int] NULL,
	[DepartmentId] [int] NULL,
	[Quantity] [int] NULL,
	[Salary] [numeric](18, 2) NULL,
	[IsUsed] [bit] NULL,
	[BeginAccountDate] [datetime] NULL,
	[Priority] [int] NULL,
	[CreatorID] [int] NULL,
	[CreateDate] [datetime] NULL,
	[EditorID] [int] NULL,
	[EditDate] [datetime] NULL,
 CONSTRAINT [PK_StaffEstablishedPost] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


if OBJECT_ID (N'StaffDepartmentTypes', 'U') is not null
	DROP TABLE [dbo].[StaffDepartmentTypes]
GO
CREATE TABLE [dbo].[StaffDepartmentTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Version] [int] NOT NULL,
	[Name] [nvarchar](50) NULL,
	[CreateDate] [datetime] NULL,
 CONSTRAINT [PK_StaffDepartmentTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


if OBJECT_ID (N'StaffDepartmentTaxDetails', 'U') is not null
	DROP TABLE [dbo].[StaffDepartmentTaxDetails]
GO
CREATE TABLE [dbo].[StaffDepartmentTaxDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DepartmentId] [int] NULL,
	[KPP] [nvarchar](9) NULL,
	[OKTMO] [nvarchar](11) NULL,
	[OKATO] [nvarchar](11) NULL,
	[RegionCode] [nvarchar](2) NULL,
	[TaxAdminCode] [nvarchar](10) NULL,
	[TaxAdminName] [nvarchar](100) NULL,
	[PostAddress] [nvarchar](250) NULL,
 CONSTRAINT [PK_StaffDepartmentTaxDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


if OBJECT_ID (N'StaffDepartmentRequest', 'U') is not null
	DROP TABLE [dbo].[StaffDepartmentRequest]
GO
CREATE TABLE [dbo].[StaffDepartmentRequest](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Version] [int] NOT NULL,
	[DateRequest] [datetime] NULL,
	--[Number] [int] NULL,
	[RequestTypeId] [int] NOT NULL,
	[DepartmentId] [int] NULL,
	[ItemLevel] [int] NULL,
	[ParentId] [int] NULL,
	[Name] [nvarchar](128) NULL,
	[IsBack] [bit] NULL,
	[OrderNumber] [nvarchar](200) NULL,
	[OrderDate] [datetime] NULL,
	[LegalAddressId] [int] NULL,
	[IsTaxAdminAccount] [bit] NULL,
	[IsEmployeAvailable] [bit] NULL,
	[DepNextId] [int] NULL,
	[IsPlan] [bit] NULL,
	[IsUsed] [bit] NULL,
	[IsDraft] [bit] NULL,
	[DateSendToApprove] [datetime] NULL,
	[BeginAccountDate] [datetime] NULL,
	[DateState] [datetime] NULL,
	[CreatorID] [int] NULL,
	[CreateDate] [datetime] NULL,
	[EditorID] [int] NULL,
	[EditDate] [datetime] NULL,
 CONSTRAINT [PK_StaffDepartmentRequest] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


if OBJECT_ID (N'StaffDepartmentOperations', 'U') is not null
	DROP TABLE [dbo].[StaffDepartmentOperations]
GO
CREATE TABLE [dbo].[StaffDepartmentOperations](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Version] [int] NOT NULL,
	[Name] [nvarchar](400) NULL,
	[CreateDate] [datetime] NULL,
 CONSTRAINT [PK_StaffDepartmentOperations] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


if OBJECT_ID (N'StaffDepartmentOperationLinks', 'U') is not null
	DROP TABLE [dbo].[StaffDepartmentOperationLinks]
GO
CREATE TABLE [dbo].[StaffDepartmentOperationLinks](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Version] [int] NOT NULL,
	[DMDetailId] [int] NULL,
	[OperationId] [int] NULL,
	[CreatorID] [int] NULL,
	[CreateDate] [datetime] NULL,
	[EditorId] [int] NULL,
	[EditDate] [datetime] NULL,
 CONSTRAINT [PK_StaffDepartmentOperationLinks] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


if OBJECT_ID (N'StaffDepartmentManagerDetails', 'U') is not null
	DROP TABLE [dbo].[StaffDepartmentManagerDetails]
GO
CREATE TABLE [dbo].[StaffDepartmentManagerDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Version] [int] NOT NULL,
	[DepRequestId] [int] NULL,
	[DepCode] [nvarchar](20) NULL,
	[NameShort] [nvarchar](100) NULL,
	[NameComment] [nvarchar](50) NULL,
	[ReasonId] [int] NULL,
	[PrevDepCode] [nvarchar](20) NULL,
	[FactAddressId] [int] NULL,
	[DepStatus] [nvarchar](50) NULL,
	[DepTypeId] [int] NULL,
	[OpenDate] [datetime] NULL,
	[CloseDate] [datetime] NULL,
	[OperationMode] [nvarchar](400) NULL,
	[BeginIdleDate] [datetime] NULL,
	[EndIdleDate] [datetime] NULL,
	[IsRentPlace] [bit] NULL,
	[AgreementDetails] [nvarchar](250) NULL,
	[DivisionArea] [numeric](18, 2) NULL,
	[AmountPayment] [numeric](18, 2) NULL,
	[Phone] [nvarchar](200) NULL,
	[IsBlocked] [bit] NULL,
	[NetShopId] [int] NULL,
	[IsAvailableCash] [bit] NULL,
	[IsLegalEntity] [bit] NULL,
	[PlanEPCount] [int] NULL,
	[PlanSalaryFund] [numeric](18, 2) NULL,
	[Note] [nvarchar](250) NULL,
	[CreatorId] [int] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[EditorId] [int] NULL,
	[EditDate] [datetime] NULL,
 CONSTRAINT [PK_StaffDepartmentManagerDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


if OBJECT_ID (N'StaffDepartmentLandmarks', 'U') is not null
	DROP TABLE [dbo].[StaffDepartmentLandmarks]
GO
CREATE TABLE [dbo].[StaffDepartmentLandmarks](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Version] [int] NOT NULL,
	[DMDetailId] [int] NULL,
	[LandmarkId] [int] NULL,
	[Description] [nvarchar](300) NULL,
	[CreatorId] [int] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[EditorId] [int] NULL,
	[EditDate] [datetime] NULL,
 CONSTRAINT [PK_StaffDepartmentLandmarks] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


if OBJECT_ID (N'StaffDepartmentCBDetails', 'U') is not null
	DROP TABLE [dbo].[StaffDepartmentCBDetails]
GO
CREATE TABLE [dbo].[StaffDepartmentCBDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Version] [int] NOT NULL,
	[DepRequestId] [int] NULL,
	[ATMCountTotal] [int] NULL,
	[ATMCashInCount] [int] NULL,
	[ATMCount] [int] NULL,
	[ATMCashInStarted] [int] NULL,
	[DepCachinId] [int] NULL,
	[DepATMId] [int] NULL,
	[CashInStartedDate] [datetime] NULL,
	[ATMStartedDate] [datetime] NULL,
	[CreatorId] [int] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[EditorId] [int] NULL,
	[EditDate] [datetime] NULL,
 CONSTRAINT [PK_StaffDepartmentCBDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


if OBJECT_ID (N'DepartmentArchive', 'U') is not null
	DROP TABLE [dbo].[DepartmentArchive]
GO
CREATE TABLE [dbo].[DepartmentArchive](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DepartmentId] [int] NULL,
	[Code] [nvarchar](10) NULL,
	[Name] [nvarchar](128) NULL,
	[Code1C] [int] NULL,
	[ParentId] [int] NULL,
	[Path] [nvarchar](128) NULL,
	[ItemLevel] [int] NULL,
	[IsUsed] [bit] NULL,
	[CreatorID] [int] NULL,
	[CreateDate] [datetime] NULL,
 CONSTRAINT [PK_DepartmentArchive] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


if OBJECT_ID (N'StaffPostChargeLinks', 'U') is not null
	DROP TABLE [dbo].[StaffPostChargeLinks]
GO
CREATE TABLE [dbo].[StaffPostChargeLinks](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[StaffExtraChargeId] [int] NULL,
	[Salary] [numeric](18, 2) NULL,
	[CreatorID] [int] NULL,
	[CreateDate] [datetime] NULL,
	[EditorID] [int] NULL,
	[EditDate] [datetime] NULL,
 CONSTRAINT [PK_StaffPostChargeLinks] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


if OBJECT_ID (N'StaffDepartmentRequestTypes', 'U') is not null
	DROP TABLE [dbo].[StaffDepartmentRequestTypes]
GO
CREATE TABLE [dbo].[StaffDepartmentRequestTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Version] [int] NOT NULL,
	[Name] [nvarchar](50) NULL,
	[CreateDate] [datetime] NULL,
 CONSTRAINT [PK_StaffDepartmentRequestTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


if OBJECT_ID (N'StaffDepartmentOperationModes', 'U') is not null
	DROP TABLE [dbo].[StaffDepartmentOperationModes]
GO
CREATE TABLE [dbo].[StaffDepartmentOperationModes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Version] [int] NOT NULL,
	[DMDetailId] [int] NULL,
	[WeekDay] [int] NOT NULL,
	[WorkBegin] [nvarchar](5) NULL,
	[WorkEnd] [nvarchar](5) NULL,
	[BreakBegin] [nvarchar](5) NULL,
	[BreakEnd] [nvarchar](5) NULL,
	[IsWorkDay] [bit] NULL,
	[CreatorId] [int] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[EditorId] [int] NULL,
	[EditDate] [datetime] NULL,
 CONSTRAINT [PK_StaffDepartmentOperationModes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


if OBJECT_ID (N'StaffEstablishedPostRequestTypes', 'U') is not null
	DROP TABLE [dbo].[StaffEstablishedPostRequestTypes]
GO
CREATE TABLE [dbo].[StaffEstablishedPostRequestTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Version] [int] NOT NULL,
	[Name] [nvarchar](50) NULL,
	[CreateDate] [datetime] NULL,
 CONSTRAINT [PK_StaffEstablishedPostRequestTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


if OBJECT_ID (N'StaffEstablishedPostChargeLinks', 'U') is not null
	DROP TABLE [dbo].[StaffEstablishedPostChargeLinks]
GO
CREATE TABLE [dbo].[StaffEstablishedPostChargeLinks](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Version] [int] NOT NULL,
	[SEPRequestId] [int] NULL,
	[SEPId] [int] NULL,
	[StaffExtraChargeId] [int] NULL,
	[Amount] [numeric](18, 2) NULL,
	[AmountProc] [numeric](18, 2) NULL,
	[CreatorID] [int] NULL,
	[CreateDate] [datetime] NULL,
	[EditorID] [int] NULL,
	[EditDate] [datetime] NULL,
 CONSTRAINT [PK_StaffEstablishedPostChargeLinks] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


if OBJECT_ID (N'StaffDepartmentReasons', 'U') is not null
	DROP TABLE [dbo].[StaffDepartmentReasons]
GO
CREATE TABLE [dbo].[StaffDepartmentReasons](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[CreateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_StaffDepartmentReasons] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO



if OBJECT_ID (N'StaffNetShopIdentification', 'U') is not null
	DROP TABLE [dbo].[StaffNetShopIdentification]
GO
CREATE TABLE [dbo].[StaffNetShopIdentification](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[CreateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_StaffNetShopIdentification] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO



--3. �������� ������ � �����������
ALTER TABLE [dbo].[StaffNetShopIdentification] ADD  CONSTRAINT [DF_StaffNetShopIdentification_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO

ALTER TABLE [dbo].[StaffDepartmentManagerDetails]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentManagerDetails_StaffNetShopIdentification] FOREIGN KEY([NetShopId])
REFERENCES [dbo].[StaffNetShopIdentification] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentManagerDetails] CHECK CONSTRAINT [FK_StaffDepartmentManagerDetails_StaffNetShopIdentification]
GO

ALTER TABLE [dbo].[StaffDepartmentReasons] ADD  CONSTRAINT [DF_StaffDepartmentReasons_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO

ALTER TABLE [dbo].[StaffDepartmentManagerDetails]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentManagerDetails_StaffDepartmentReasons] FOREIGN KEY([ReasonId])
REFERENCES [dbo].[StaffDepartmentReasons] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentManagerDetails] CHECK CONSTRAINT [FK_StaffDepartmentManagerDetails_StaffDepartmentReasons]
GO

ALTER TABLE [dbo].[StaffEstablishedPostChargeLinks] ADD  CONSTRAINT [DF_StaffEstablishedPostChargeLinks_Salary]  DEFAULT ((0)) FOR [Amount]
GO

ALTER TABLE [dbo].[StaffEstablishedPostChargeLinks] ADD  CONSTRAINT [DF_StaffEstablishedPostChargeLinks_AmountProc]  DEFAULT ((0)) FOR [AmountProc]
GO

ALTER TABLE [dbo].[StaffEstablishedPostChargeLinks] ADD  CONSTRAINT [DF_StaffEstablishedPostChargeLinks_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO

ALTER TABLE [dbo].[StaffEstablishedPostChargeLinks]  WITH CHECK ADD  CONSTRAINT [FK_StaffEstablishedPostChargeLinks_CreatorUser] FOREIGN KEY([CreatorID])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[StaffEstablishedPostChargeLinks] CHECK CONSTRAINT [FK_StaffEstablishedPostChargeLinks_CreatorUser]
GO

ALTER TABLE [dbo].[StaffEstablishedPostChargeLinks]  WITH CHECK ADD  CONSTRAINT [FK_StaffEstablishedPostChargeLinks_EditorUser] FOREIGN KEY([EditorID])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[StaffEstablishedPostChargeLinks] CHECK CONSTRAINT [FK_StaffEstablishedPostChargeLinks_EditorUser]
GO

ALTER TABLE [dbo].[StaffEstablishedPostChargeLinks]  WITH CHECK ADD  CONSTRAINT [FK_StaffEstablishedPostChargeLinks_StaffEstablishedPost] FOREIGN KEY([SEPId])
REFERENCES [dbo].[StaffEstablishedPost] ([Id])
GO

ALTER TABLE [dbo].[StaffEstablishedPostChargeLinks] CHECK CONSTRAINT [FK_StaffEstablishedPostChargeLinks_StaffEstablishedPost]
GO

ALTER TABLE [dbo].[StaffEstablishedPostChargeLinks]  WITH CHECK ADD  CONSTRAINT [FK_StaffEstablishedPostChargeLinks_StaffEstablishedPostRequest] FOREIGN KEY([SEPRequestId])
REFERENCES [dbo].[StaffEstablishedPostRequest] ([Id])
GO

ALTER TABLE [dbo].[StaffEstablishedPostChargeLinks] CHECK CONSTRAINT [FK_StaffEstablishedPostChargeLinks_StaffEstablishedPostRequest]
GO

ALTER TABLE [dbo].[StaffEstablishedPostChargeLinks]  WITH CHECK ADD  CONSTRAINT [FK_StaffEstablishedPostChargeLinks_StaffExtraCharges] FOREIGN KEY([StaffExtraChargeId])
REFERENCES [dbo].[StaffExtraCharges] ([Id])
GO

ALTER TABLE [dbo].[StaffEstablishedPostChargeLinks] CHECK CONSTRAINT [FK_StaffEstablishedPostChargeLinks_StaffExtraCharges]
GO

ALTER TABLE [dbo].[StaffEstablishedPostRequestTypes] ADD  CONSTRAINT [DF_StaffEstablishedPostRequestTypes_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO

ALTER TABLE [dbo].[StaffDepartmentRequestTypes] ADD  CONSTRAINT [DF_StaffDepartmentRequestTypes_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO

ALTER TABLE [dbo].[StaffDepartmentRequest]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentRequest_DepartmentParent] FOREIGN KEY([ParentId])
REFERENCES [dbo].[Department] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentRequest] CHECK CONSTRAINT [FK_StaffDepartmentRequest_DepartmentParent]
GO

ALTER TABLE [dbo].[StaffPostChargeLinks] ADD  CONSTRAINT [DF_StaffPostChargeLinks_Salary]  DEFAULT ((0)) FOR [Salary]
GO

ALTER TABLE [dbo].[StaffPostChargeLinks] ADD  CONSTRAINT [DF_StaffPostChargeLinks_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO

ALTER TABLE [dbo].[StaffPostChargeLinks]  WITH CHECK ADD  CONSTRAINT [FK_StaffPostChargeLinks_CreatorUser] FOREIGN KEY([CreatorID])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[StaffPostChargeLinks] CHECK CONSTRAINT [FK_StaffPostChargeLinks_CreatorUser]
GO

ALTER TABLE [dbo].[StaffPostChargeLinks]  WITH CHECK ADD  CONSTRAINT [FK_StaffPostChargeLinks_EditorUser] FOREIGN KEY([EditorID])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[StaffPostChargeLinks] CHECK CONSTRAINT [FK_StaffPostChargeLinks_EditorUser]
GO

ALTER TABLE [dbo].[StaffPostChargeLinks]  WITH CHECK ADD  CONSTRAINT [FK_StaffPostChargeLinks_Staff] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[StaffPostChargeLinks] CHECK CONSTRAINT [FK_StaffPostChargeLinks_Staff]
GO

ALTER TABLE [dbo].[StaffPostChargeLinks]  WITH CHECK ADD  CONSTRAINT [FK_StaffPostChargeLinks_StaffExtraCharges] FOREIGN KEY([StaffExtraChargeId])
REFERENCES [dbo].[StaffExtraCharges] ([Id])
GO

ALTER TABLE [dbo].[StaffPostChargeLinks] CHECK CONSTRAINT [FK_StaffPostChargeLinks_StaffExtraCharges]
GO

ALTER TABLE [dbo].[DepartmentArchive] ADD  CONSTRAINT [DF_DepartmentArchive_IsUsed]  DEFAULT ((1)) FOR [IsUsed]
GO

ALTER TABLE [dbo].[DepartmentArchive] ADD  CONSTRAINT [DF_DepartmentArchive_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO

ALTER TABLE [dbo].[DepartmentArchive]  WITH CHECK ADD  CONSTRAINT [FK_DepartmentArchive_CreatorUser] FOREIGN KEY([CreatorID])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[DepartmentArchive] CHECK CONSTRAINT [FK_DepartmentArchive_CreatorUser]
GO

ALTER TABLE [dbo].[DepartmentArchive]  WITH CHECK ADD  CONSTRAINT [FK_DepartmentArchive_Department] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[Department] ([Id])
GO

ALTER TABLE [dbo].[DepartmentArchive] CHECK CONSTRAINT [FK_DepartmentArchive_Department]
GO

ALTER TABLE [dbo].[StaffDepartmentCBDetails] ADD  CONSTRAINT [DF_StaffDepartmentCBDetails_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO

ALTER TABLE [dbo].[StaffDepartmentCBDetails]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentCBDetails_CreatorUser] FOREIGN KEY([CreatorId])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentCBDetails] CHECK CONSTRAINT [FK_StaffDepartmentCBDetails_CreatorUser]
GO

ALTER TABLE [dbo].[StaffDepartmentCBDetails]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentCBDetails_EditorUser] FOREIGN KEY([EditorId])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentCBDetails] CHECK CONSTRAINT [FK_StaffDepartmentCBDetails_EditorUser]
GO

ALTER TABLE [dbo].[StaffDepartmentCBDetails]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentCBDetails_StaffDepartmentRequest] FOREIGN KEY([DepRequestId])
REFERENCES [dbo].[StaffDepartmentRequest] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentCBDetails] CHECK CONSTRAINT [FK_StaffDepartmentCBDetails_StaffDepartmentRequest]
GO

ALTER TABLE [dbo].[StaffDepartmentCBDetails]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentCBDetails_DepATM] FOREIGN KEY([DepATMId])
REFERENCES [dbo].[Department] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentCBDetails] CHECK CONSTRAINT [FK_StaffDepartmentCBDetails_DepATM]
GO

ALTER TABLE [dbo].[StaffDepartmentCBDetails]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentCBDetails_DepCashin] FOREIGN KEY([DepCachinId])
REFERENCES [dbo].[Department] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentCBDetails] CHECK CONSTRAINT [FK_StaffDepartmentCBDetails_DepCashin]
GO

ALTER TABLE [dbo].[StaffDepartmentLandmarks] ADD  CONSTRAINT [DF_StaffDepartmentLandmarks_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO

ALTER TABLE [dbo].[StaffDepartmentLandmarks]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentLandmarks_CreatorUser] FOREIGN KEY([CreatorId])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentLandmarks] CHECK CONSTRAINT [FK_StaffDepartmentLandmarks_CreatorUser]
GO

ALTER TABLE [dbo].[StaffDepartmentLandmarks]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentLandmarks_EditorUser] FOREIGN KEY([EditorId])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentLandmarks] CHECK CONSTRAINT [FK_StaffDepartmentLandmarks_EditorUser]
GO

ALTER TABLE [dbo].[StaffDepartmentLandmarks]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentLandmarks_StaffDepartmentManagerDetails] FOREIGN KEY([DMDetailId])
REFERENCES [dbo].[StaffDepartmentManagerDetails] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentLandmarks] CHECK CONSTRAINT [FK_StaffDepartmentLandmarks_StaffDepartmentManagerDetails]
GO

ALTER TABLE [dbo].[StaffDepartmentLandmarks]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentLandmarks_StaffLandmarkTypes] FOREIGN KEY([LandmarkId])
REFERENCES [dbo].[StaffLandmarkTypes] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentLandmarks] CHECK CONSTRAINT [FK_StaffDepartmentLandmarks_StaffLandmarkTypes]
GO

ALTER TABLE [dbo].[StaffDepartmentManagerDetails] ADD  CONSTRAINT [DF_StaffDepartmentManagerDetails_DivisionArea]  DEFAULT ((0)) FOR [DivisionArea]
GO

ALTER TABLE [dbo].[StaffDepartmentManagerDetails] ADD  CONSTRAINT [DF_StaffDepartmentManagerDetails_AmountPayment]  DEFAULT ((0)) FOR [AmountPayment]
GO

ALTER TABLE [dbo].[StaffDepartmentManagerDetails] ADD  CONSTRAINT [DF_StaffDepartmentManagerDetails_IsBlocked]  DEFAULT ((0)) FOR [IsBlocked]
GO

ALTER TABLE [dbo].[StaffDepartmentManagerDetails] ADD  CONSTRAINT [DF_StaffDepartmentManagerDetails_PlanSalaryFund]  DEFAULT ((0)) FOR [PlanSalaryFund]
GO

ALTER TABLE [dbo].[StaffDepartmentManagerDetails] ADD  CONSTRAINT [DF_StaffDepartmentManagerDetails_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO

ALTER TABLE [dbo].[StaffDepartmentManagerDetails]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentManagerDetails_CreatorUser] FOREIGN KEY([CreatorId])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentManagerDetails] CHECK CONSTRAINT [FK_StaffDepartmentManagerDetails_CreatorUser]
GO

ALTER TABLE [dbo].[StaffDepartmentManagerDetails]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentManagerDetails_EditorUser] FOREIGN KEY([EditorId])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentManagerDetails] CHECK CONSTRAINT [FK_StaffDepartmentManagerDetails_EditorUser]
GO

ALTER TABLE [dbo].[StaffDepartmentManagerDetails]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentManagerDetails_RefAddresses] FOREIGN KEY([FactAddressId])
REFERENCES [dbo].[RefAddresses] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentManagerDetails] CHECK CONSTRAINT [FK_StaffDepartmentManagerDetails_RefAddresses]
GO

ALTER TABLE [dbo].[StaffDepartmentManagerDetails]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentManagerDetails_StaffDepartmentRequest] FOREIGN KEY([DepRequestId])
REFERENCES [dbo].[StaffDepartmentRequest] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentManagerDetails] CHECK CONSTRAINT [FK_StaffDepartmentManagerDetails_StaffDepartmentRequest]
GO

ALTER TABLE [dbo].[StaffDepartmentManagerDetails]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentManagerDetails_StaffDepartmentTypes] FOREIGN KEY([DepTypeId])
REFERENCES [dbo].[StaffDepartmentTypes] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentManagerDetails] CHECK CONSTRAINT [FK_StaffDepartmentManagerDetails_StaffDepartmentTypes]
GO

ALTER TABLE [dbo].[StaffDepartmentOperationLinks] ADD  CONSTRAINT [DF_StaffDepartmentOperationLinks_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO

ALTER TABLE [dbo].[StaffDepartmentOperationLinks]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentOperationLinks_CreatorUser] FOREIGN KEY([CreatorID])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentOperationLinks] CHECK CONSTRAINT [FK_StaffDepartmentOperationLinks_CreatorUser]
GO

ALTER TABLE [dbo].[StaffDepartmentOperationLinks]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentOperationLinks_EditorUser] FOREIGN KEY([EditorId])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentOperationLinks] CHECK CONSTRAINT [FK_StaffDepartmentOperationLinks_EditorUser]
GO

ALTER TABLE [dbo].[StaffDepartmentOperationLinks]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentOperationLinks_StaffDepartmentManagerDetails] FOREIGN KEY([DMDetailId])
REFERENCES [dbo].[StaffDepartmentManagerDetails] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentOperationLinks] CHECK CONSTRAINT [FK_StaffDepartmentOperationLinks_StaffDepartmentManagerDetails]
GO

ALTER TABLE [dbo].[StaffDepartmentOperationLinks]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentOperationLinks_StaffDepartmentOperations] FOREIGN KEY([OperationId])
REFERENCES [dbo].[StaffDepartmentOperations] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentOperationLinks] CHECK CONSTRAINT [FK_StaffDepartmentOperationLinks_StaffDepartmentOperations]
GO

ALTER TABLE [dbo].[StaffDepartmentOperations] ADD  CONSTRAINT [DF_StaffDepartmentOperations_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO

ALTER TABLE [dbo].[StaffDepartmentRequest] ADD  CONSTRAINT [DF_StaffDepartmentRequest_IsBack]  DEFAULT ((0)) FOR [IsBack]
GO

ALTER TABLE [dbo].[StaffDepartmentRequest] ADD  CONSTRAINT [DF_StaffDepartmentRequest_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO

ALTER TABLE [dbo].[StaffDepartmentRequest]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentRequest_CreatorUser] FOREIGN KEY([CreatorID])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentRequest] CHECK CONSTRAINT [FK_StaffDepartmentRequest_CreatorUser]
GO

ALTER TABLE [dbo].[StaffDepartmentRequest]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentRequest_Department] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[Department] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentRequest] CHECK CONSTRAINT [FK_StaffDepartmentRequest_Department]
GO

ALTER TABLE [dbo].[StaffDepartmentRequest]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentRequest_DepartmentNext] FOREIGN KEY([DepNextId])
REFERENCES [dbo].[Department] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentRequest] CHECK CONSTRAINT [FK_StaffDepartmentRequest_DepartmentNext]
GO

ALTER TABLE [dbo].[StaffDepartmentRequest]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentRequest_EditorUser] FOREIGN KEY([EditorID])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentRequest] CHECK CONSTRAINT [FK_StaffDepartmentRequest_EditorUser]
GO

ALTER TABLE [dbo].[StaffDepartmentRequest]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentRequest_RefAddresses] FOREIGN KEY([LegalAddressId])
REFERENCES [dbo].[RefAddresses] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentRequest] CHECK CONSTRAINT [FK_StaffDepartmentRequest_RefAddresses]
GO

ALTER TABLE [dbo].[StaffDepartmentRequest]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentRequest_StaffDepartmentRequestTypes] FOREIGN KEY([RequestTypeId])
REFERENCES [dbo].[StaffDepartmentRequestTypes] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentRequest] CHECK CONSTRAINT [FK_StaffDepartmentRequest_StaffDepartmentRequestTypes]
GO

ALTER TABLE [dbo].[StaffDepartmentTaxDetails]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentTaxDetails_Department] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[Department] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentTaxDetails] CHECK CONSTRAINT [FK_StaffDepartmentTaxDetails_Department]
GO

ALTER TABLE [dbo].[StaffDepartmentTypes] ADD  CONSTRAINT [DF_StaffDepartmentTypes_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO

ALTER TABLE [dbo].[StaffEstablishedPost] ADD  CONSTRAINT [DF_StaffEstablishedPost_Quantity]  DEFAULT ((1)) FOR [Quantity]
GO

ALTER TABLE [dbo].[StaffEstablishedPost] ADD  CONSTRAINT [DF_StaffEstablishedPost_Salary]  DEFAULT ((0)) FOR [Salary]
GO

ALTER TABLE [dbo].[StaffEstablishedPost] ADD  CONSTRAINT [DF_StaffEstablishedPost_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO

ALTER TABLE [dbo].[StaffEstablishedPost]  WITH CHECK ADD  CONSTRAINT [FK_StaffEstablishedPost_CreatorUser] FOREIGN KEY([CreatorID])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[StaffEstablishedPost] CHECK CONSTRAINT [FK_StaffEstablishedPost_CreatorUser]
GO

ALTER TABLE [dbo].[StaffEstablishedPost]  WITH CHECK ADD  CONSTRAINT [FK_StaffEstablishedPost_Department] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[Department] ([Id])
GO

ALTER TABLE [dbo].[StaffEstablishedPost] CHECK CONSTRAINT [FK_StaffEstablishedPost_Department]
GO

ALTER TABLE [dbo].[StaffEstablishedPost]  WITH CHECK ADD  CONSTRAINT [FK_StaffEstablishedPost_EditorUser] FOREIGN KEY([EditorID])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[StaffEstablishedPost] CHECK CONSTRAINT [FK_StaffEstablishedPost_EditorUser]
GO

ALTER TABLE [dbo].[StaffEstablishedPost]  WITH CHECK ADD  CONSTRAINT [FK_StaffEstablishedPost_Position] FOREIGN KEY([PositionId])
REFERENCES [dbo].[Position] ([Id])
GO

ALTER TABLE [dbo].[StaffEstablishedPost] CHECK CONSTRAINT [FK_StaffEstablishedPost_Position]
GO

ALTER TABLE [dbo].[StaffEstablishedPostArchive] ADD  CONSTRAINT [DF_StaffEstablishedPostArchive_Quantity]  DEFAULT ((1)) FOR [Quantity]
GO

ALTER TABLE [dbo].[StaffEstablishedPostArchive] ADD  CONSTRAINT [DF_StaffEstablishedPostArchive_Salary]  DEFAULT ((0)) FOR [Salary]
GO

ALTER TABLE [dbo].[StaffEstablishedPostArchive] ADD  CONSTRAINT [DF_StaffEstablishedPostArchive_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO

ALTER TABLE [dbo].[StaffEstablishedPostArchive]  WITH CHECK ADD  CONSTRAINT [FK_StaffEstablishedPostArchive_CreatorUser] FOREIGN KEY([CreatorID])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[StaffEstablishedPostArchive] CHECK CONSTRAINT [FK_StaffEstablishedPostArchive_CreatorUser]
GO

ALTER TABLE [dbo].[StaffEstablishedPostArchive]  WITH CHECK ADD  CONSTRAINT [FK_StaffEstablishedPostArchive_Department] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[Department] ([Id])
GO

ALTER TABLE [dbo].[StaffEstablishedPostArchive] CHECK CONSTRAINT [FK_StaffEstablishedPostArchive_Department]
GO

ALTER TABLE [dbo].[StaffEstablishedPostArchive]  WITH CHECK ADD  CONSTRAINT [FK_StaffEstablishedPostArchive_Position] FOREIGN KEY([PositionId])
REFERENCES [dbo].[Position] ([Id])
GO

ALTER TABLE [dbo].[StaffEstablishedPostArchive] CHECK CONSTRAINT [FK_StaffEstablishedPostArchive_Position]
GO

ALTER TABLE [dbo].[StaffEstablishedPostArchive]  WITH CHECK ADD  CONSTRAINT [FK_StaffEstablishedPostArchive_StaffEstablishedPost] FOREIGN KEY([SEPId])
REFERENCES [dbo].[StaffEstablishedPost] ([Id])
GO

ALTER TABLE [dbo].[StaffEstablishedPostArchive] CHECK CONSTRAINT [FK_StaffEstablishedPostArchive_StaffEstablishedPost]
GO

ALTER TABLE [dbo].[StaffEstablishedPostRequest] ADD  CONSTRAINT [DF_StaffEstablishedPostRequest_Quantity]  DEFAULT ((1)) FOR [Quantity]
GO

ALTER TABLE [dbo].[StaffEstablishedPostRequest] ADD  CONSTRAINT [DF_StaffEstablishedPostRequest_Salary]  DEFAULT ((0)) FOR [Salary]
GO

ALTER TABLE [dbo].[StaffEstablishedPostRequest] ADD  CONSTRAINT [DF_StaffEstablishedPostRequest_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO

ALTER TABLE [dbo].[StaffEstablishedPostRequest]  WITH CHECK ADD  CONSTRAINT [FK_StaffEstablishedPostRequest_AppointmentReason] FOREIGN KEY([ReasonId])
REFERENCES [dbo].[AppointmentReason] ([Id])
GO

ALTER TABLE [dbo].[StaffEstablishedPostRequest] CHECK CONSTRAINT [FK_StaffEstablishedPostRequest_AppointmentReason]
GO

ALTER TABLE [dbo].[StaffEstablishedPostRequest]  WITH CHECK ADD  CONSTRAINT [FK_StaffEstablishedPostRequest_CreatorUser] FOREIGN KEY([CreatorID])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[StaffEstablishedPostRequest] CHECK CONSTRAINT [FK_StaffEstablishedPostRequest_CreatorUser]
GO

ALTER TABLE [dbo].[StaffEstablishedPostRequest]  WITH CHECK ADD  CONSTRAINT [FK_StaffEstablishedPostRequest_Department] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[Department] ([Id])
GO

ALTER TABLE [dbo].[StaffEstablishedPostRequest] CHECK CONSTRAINT [FK_StaffEstablishedPostRequest_Department]
GO

ALTER TABLE [dbo].[StaffEstablishedPostRequest]  WITH CHECK ADD  CONSTRAINT [FK_StaffEstablishedPostRequest_EditorUsers] FOREIGN KEY([EditorID])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[StaffEstablishedPostRequest] CHECK CONSTRAINT [FK_StaffEstablishedPostRequest_EditorUsers]
GO

ALTER TABLE [dbo].[StaffEstablishedPostRequest]  WITH CHECK ADD  CONSTRAINT [FK_StaffEstablishedPostRequest_Position] FOREIGN KEY([PositionId])
REFERENCES [dbo].[Position] ([Id])
GO

ALTER TABLE [dbo].[StaffEstablishedPostRequest] CHECK CONSTRAINT [FK_StaffEstablishedPostRequest_Position]
GO

ALTER TABLE [dbo].[StaffEstablishedPostRequest]  WITH CHECK ADD  CONSTRAINT [FK_StaffEstablishedPostRequest_StaffEstablishedPost] FOREIGN KEY([SEPId])
REFERENCES [dbo].[StaffEstablishedPost] ([Id])
GO

ALTER TABLE [dbo].[StaffEstablishedPostRequest] CHECK CONSTRAINT [FK_StaffEstablishedPostRequest_StaffEstablishedPost]
GO

ALTER TABLE [dbo].[StaffEstablishedPostRequest]  WITH CHECK ADD  CONSTRAINT [FK_StaffEstablishedPostRequest_StaffEstablishedPostRequestTypes] FOREIGN KEY([RequestTypeId])
REFERENCES [dbo].[StaffEstablishedPostRequestTypes] ([Id])
GO

ALTER TABLE [dbo].[StaffEstablishedPostRequest] CHECK CONSTRAINT [FK_StaffEstablishedPostRequest_StaffEstablishedPostRequestTypes]
GO

ALTER TABLE [dbo].[StaffLandmarkTypes] ADD  CONSTRAINT [DF_StaffLandmarkTypes_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO

ALTER TABLE [dbo].[StaffProgramCodes] ADD  CONSTRAINT [DF_StaffProgramCodes_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO

ALTER TABLE [dbo].[StaffProgramCodes]  WITH CHECK ADD  CONSTRAINT [FK_StaffProgramCodes_CreatorUser] FOREIGN KEY([CreatorId])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[StaffProgramCodes] CHECK CONSTRAINT [FK_StaffProgramCodes_CreatorUser]
GO

ALTER TABLE [dbo].[StaffProgramCodes]  WITH CHECK ADD  CONSTRAINT [FK_StaffProgramCodes_EditorUser] FOREIGN KEY([EditorId])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[StaffProgramCodes] CHECK CONSTRAINT [FK_StaffProgramCodes_EditorUser]
GO

ALTER TABLE [dbo].[StaffProgramCodes]  WITH CHECK ADD  CONSTRAINT [FK_StaffProgramCodes_StaffDepartmentManagerDetails] FOREIGN KEY([DMDetailId])
REFERENCES [dbo].[StaffDepartmentManagerDetails] ([Id])
GO

ALTER TABLE [dbo].[StaffProgramCodes] CHECK CONSTRAINT [FK_StaffProgramCodes_StaffDepartmentManagerDetails]
GO

ALTER TABLE [dbo].[StaffProgramCodes]  WITH CHECK ADD  CONSTRAINT [FK_StaffProgramCodes_StaffProgramReference] FOREIGN KEY([ProgramId])
REFERENCES [dbo].[StaffProgramReference] ([Id])
GO

ALTER TABLE [dbo].[StaffProgramCodes] CHECK CONSTRAINT [FK_StaffProgramCodes_StaffProgramReference]
GO

ALTER TABLE [dbo].[StaffProgramReference] ADD  CONSTRAINT [DF_StaffProgramReference_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO

ALTER TABLE [dbo].[RefAddresses] ADD  CONSTRAINT [DF_RefAddresses_IsUsed]  DEFAULT ((0)) FOR [IsUsed]
GO

ALTER TABLE [dbo].[RefAddresses] ADD  CONSTRAINT [DF_RefAddresses_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO

ALTER TABLE [dbo].[RefAddresses]  WITH CHECK ADD  CONSTRAINT [FK_RefAddresses_Creators] FOREIGN KEY([CreatorId])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[RefAddresses] CHECK CONSTRAINT [FK_RefAddresses_Creators]
GO

ALTER TABLE [dbo].[RefAddresses]  WITH CHECK ADD  CONSTRAINT [FK_RefAddresses_Editors] FOREIGN KEY([EditorId])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[RefAddresses] CHECK CONSTRAINT [FK_RefAddresses_Editors]
GO

ALTER TABLE [dbo].[StaffDepartmentOperationModes] ADD  CONSTRAINT [DF_StaffDepartmentOperationModes_IsWorkDay]  DEFAULT ((0)) FOR [IsWorkDay]
GO

ALTER TABLE [dbo].[StaffDepartmentOperationModes] ADD  CONSTRAINT [DF_StaffDepartmentOperationModes_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO

ALTER TABLE [dbo].[StaffDepartmentOperationModes]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentOperationModes_CreatorUser] FOREIGN KEY([CreatorId])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentOperationModes] CHECK CONSTRAINT [FK_StaffDepartmentOperationModes_CreatorUser]
GO

ALTER TABLE [dbo].[StaffDepartmentOperationModes]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentOperationModes_EditorUser] FOREIGN KEY([EditorId])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentOperationModes] CHECK CONSTRAINT [FK_StaffDepartmentOperationModes_EditorUser]
GO

ALTER TABLE [dbo].[StaffDepartmentOperationModes]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentOperationModes_StaffDepartmentManagerDetails] FOREIGN KEY([DMDetailId])
REFERENCES [dbo].[StaffDepartmentManagerDetails] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentOperationModes] CHECK CONSTRAINT [FK_StaffDepartmentOperationModes_StaffDepartmentManagerDetails]
GO

ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_StaffEstablishedPost] FOREIGN KEY([SEPId])
REFERENCES [dbo].[StaffEstablishedPost] ([Id])
GO

ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_StaffEstablishedPost]
GO


--4. �������� ��������
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffNetShopIdentification', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffNetShopIdentification', @level2type=N'COLUMN',@level2name=N'Name'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� �������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffNetShopIdentification', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���������� ����� ������������� ������� ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffNetShopIdentification'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ���� ������������� �������� ��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentManagerDetails', @level2type=N'COLUMN',@level2name=N'NetShopId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentReasons', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentReasons', @level2type=N'COLUMN',@level2name=N'Name'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� �������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentReasons', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���������� ������ ��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentReasons'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostChargeLinks', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������ ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostChargeLinks', @level2type=N'COLUMN',@level2name=N'Version'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������ ��� ������� ������� �������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostChargeLinks', @level2type=N'COLUMN',@level2name=N'SEPRequestId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������� �������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostChargeLinks', @level2type=N'COLUMN',@level2name=N'SEPId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostChargeLinks', @level2type=N'COLUMN',@level2name=N'StaffExtraChargeId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������ ��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostChargeLinks', @level2type=N'COLUMN',@level2name=N'Amount'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������ �������� � ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostChargeLinks', @level2type=N'COLUMN',@level2name=N'AmountProc'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostChargeLinks', @level2type=N'COLUMN',@level2name=N'CreatorID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� �������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostChargeLinks', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostChargeLinks', @level2type=N'COLUMN',@level2name=N'EditorID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� ���������� �������������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostChargeLinks', @level2type=N'COLUMN',@level2name=N'EditDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����� �������� � �������� ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostChargeLinks'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostRequestTypes', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������ ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostRequestTypes', @level2type=N'COLUMN',@level2name=N'Version'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostRequestTypes', @level2type=N'COLUMN',@level2name=N'Name'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� �������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostRequestTypes', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���������� ����� ������ ��� ������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostRequestTypes'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentRequestTypes', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������ ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentRequestTypes', @level2type=N'COLUMN',@level2name=N'Version'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentRequestTypes', @level2type=N'COLUMN',@level2name=N'Name'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� �������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentRequestTypes', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���������� ����� ������ ��� �������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentRequestTypes'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� ����������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentRequest', @level2type=N'COLUMN',@level2name=N'DateState'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������������� �������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentRequest', @level2type=N'COLUMN',@level2name=N'ParentId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���������� � �������� �������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPost', @level2type=N'COLUMN',@level2name=N'Priority'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffPostChargeLinks', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ����������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffPostChargeLinks', @level2type=N'COLUMN',@level2name=N'UserId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffPostChargeLinks', @level2type=N'COLUMN',@level2name=N'StaffExtraChargeId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffPostChargeLinks', @level2type=N'COLUMN',@level2name=N'Salary'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffPostChargeLinks', @level2type=N'COLUMN',@level2name=N'CreatorID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� �������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffPostChargeLinks', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffPostChargeLinks', @level2type=N'COLUMN',@level2name=N'EditorID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� ���������� �������������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffPostChargeLinks', @level2type=N'COLUMN',@level2name=N'EditDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����� �������� � ������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffPostChargeLinks'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DepartmentArchive', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id �������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DepartmentArchive', @level2type=N'COLUMN',@level2name=N'DepartmentId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������� �������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DepartmentArchive', @level2type=N'COLUMN',@level2name=N'IsUsed'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DepartmentArchive', @level2type=N'COLUMN',@level2name=N'CreatorID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� �������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DepartmentArchive', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����� ����������� �������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DepartmentArchive'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentCBDetails', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������ ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentCBDetails', @level2type=N'COLUMN',@level2name=N'Version'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentCBDetails', @level2type=N'COLUMN',@level2name=N'DepRequestId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���������� ���������� �����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentCBDetails', @level2type=N'COLUMN',@level2name=N'ATMCountTotal'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���������� ���������� � �������� �����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentCBDetails', @level2type=N'COLUMN',@level2name=N'ATMCashInCount'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���������� ���������� � �������� ����������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentCBDetails', @level2type=N'COLUMN',@level2name=N'ATMCount'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���������� ���������� ���������� � �������� �����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentCBDetails', @level2type=N'COLUMN',@level2name=N'ATMCashInStarted'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������������� ������������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentCBDetails', @level2type=N'COLUMN',@level2name=N'DepCachinId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������������� ������������� ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentCBDetails', @level2type=N'COLUMN',@level2name=N'DepATMId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� ������� ������ (������)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentCBDetails', @level2type=N'COLUMN',@level2name=N'CashInStartedDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� ������� ��������� (������)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentCBDetails', @level2type=N'COLUMN',@level2name=N'ATMStartedDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������ ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentCBDetails', @level2type=N'COLUMN',@level2name=N'CreatorId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� �������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentCBDetails', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentCBDetails', @level2type=N'COLUMN',@level2name=N'EditorId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� ���������� ��������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentCBDetails', @level2type=N'COLUMN',@level2name=N'EditDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�� ��������� �������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentCBDetails'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentLandmarks', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������ ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentLandmarks', @level2type=N'COLUMN',@level2name=N'Version'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id �������������� ����������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentLandmarks', @level2type=N'COLUMN',@level2name=N'DMDetailId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ���� ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentLandmarks', @level2type=N'COLUMN',@level2name=N'LandmarkId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentLandmarks', @level2type=N'COLUMN',@level2name=N'Description'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������ ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentLandmarks', @level2type=N'COLUMN',@level2name=N'CreatorId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� �������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentLandmarks', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentLandmarks', @level2type=N'COLUMN',@level2name=N'EditorId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� ���������� ��������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentLandmarks', @level2type=N'COLUMN',@level2name=N'EditDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������� �������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentLandmarks'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentManagerDetails', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������ ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentManagerDetails', @level2type=N'COLUMN',@level2name=N'Version'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentManagerDetails', @level2type=N'COLUMN',@level2name=N'DepRequestId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��� ������������� ��� ��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentManagerDetails', @level2type=N'COLUMN',@level2name=N'DepCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������� ��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentManagerDetails', @level2type=N'COLUMN',@level2name=N'NameShort'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���������� � �������� �������� ��� �������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentManagerDetails', @level2type=N'COLUMN',@level2name=N'NameComment'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������� ��� ������������� ��� ��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentManagerDetails', @level2type=N'COLUMN',@level2name=N'PrevDepCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����������� �����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentManagerDetails', @level2type=N'COLUMN',@level2name=N'FactAddressId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������ �������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentManagerDetails', @level2type=N'COLUMN',@level2name=N'DepStatus'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��� �������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentManagerDetails', @level2type=N'COLUMN',@level2name=N'DepTypeId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� ������������ �������� �����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentManagerDetails', @level2type=N'COLUMN',@level2name=N'OpenDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� �������� �����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentManagerDetails', @level2type=N'COLUMN',@level2name=N'CloseDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������� �������� � ����������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentManagerDetails', @level2type=N'COLUMN',@level2name=N'ReasonId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����� ������ �������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentManagerDetails', @level2type=N'COLUMN',@level2name=N'OperationMode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� ������ �������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentManagerDetails', @level2type=N'COLUMN',@level2name=N'BeginIdleDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� ����� �������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentManagerDetails', @level2type=N'COLUMN',@level2name=N'EndIdleDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������� ������������� ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentManagerDetails', @level2type=N'COLUMN',@level2name=N'IsRentPlace'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������� ��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentManagerDetails', @level2type=N'COLUMN',@level2name=N'AgreementDetails'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������� �������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentManagerDetails', @level2type=N'COLUMN',@level2name=N'DivisionArea'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����� ������������ �������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentManagerDetails', @level2type=N'COLUMN',@level2name=N'AmountPayment'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����� ��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentManagerDetails', @level2type=N'COLUMN',@level2name=N'Phone'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������� ���������� �������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentManagerDetails', @level2type=N'COLUMN',@level2name=N'IsBlocked'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������� ������� �����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentManagerDetails', @level2type=N'COLUMN',@level2name=N'IsAvailableCash'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������� ������������ ����������� ���' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentManagerDetails', @level2type=N'COLUMN',@level2name=N'IsLegalEntity'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����������� ���������� ������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentManagerDetails', @level2type=N'COLUMN',@level2name=N'PlanEPCount'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����������� ���' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentManagerDetails', @level2type=N'COLUMN',@level2name=N'PlanSalaryFund'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentManagerDetails', @level2type=N'COLUMN',@level2name=N'Note'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������ ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentManagerDetails', @level2type=N'COLUMN',@level2name=N'CreatorId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� �������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentManagerDetails', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentManagerDetails', @level2type=N'COLUMN',@level2name=N'EditorId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� ���������� ��������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentManagerDetails', @level2type=N'COLUMN',@level2name=N'EditDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�������������� ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentManagerDetails'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentOperationLinks', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������ ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentOperationLinks', @level2type=N'COLUMN',@level2name=N'Version'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id �������������� ����������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentOperationLinks', @level2type=N'COLUMN',@level2name=N'DMDetailId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentOperationLinks', @level2type=N'COLUMN',@level2name=N'OperationId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentOperationLinks', @level2type=N'COLUMN',@level2name=N'CreatorID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� �������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentOperationLinks', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentOperationLinks', @level2type=N'COLUMN',@level2name=N'EditorId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� ���������� ��������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentOperationLinks', @level2type=N'COLUMN',@level2name=N'EditDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�������� ���������� ��������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentOperationLinks'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentOperations', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������ ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentOperations', @level2type=N'COLUMN',@level2name=N'Version'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentOperations', @level2type=N'COLUMN',@level2name=N'Name'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� �������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentOperations', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���������� �������� �������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentOperations'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentRequest', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������ ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentRequest', @level2type=N'COLUMN',@level2name=N'Version'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentRequest', @level2type=N'COLUMN',@level2name=N'DateRequest'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentRequest', @level2type=N'COLUMN',@level2name=N'RequestTypeId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id �������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentRequest', @level2type=N'COLUMN',@level2name=N'DepartmentId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������� �������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentRequest', @level2type=N'COLUMN',@level2name=N'ItemLevel'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������������ �������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentRequest', @level2type=N'COLUMN',@level2name=N'Name'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������� ���/�����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentRequest', @level2type=N'COLUMN',@level2name=N'IsBack'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����� �������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentRequest', @level2type=N'COLUMN',@level2name=N'OrderNumber'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� �������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentRequest', @level2type=N'COLUMN',@level2name=N'OrderDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����������� �����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentRequest', @level2type=N'COLUMN',@level2name=N'LegalAddressId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������� ���������� �� ���� � ����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentRequest', @level2type=N'COLUMN',@level2name=N'IsTaxAdminAccount'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������� ������� ��������� � �������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentRequest', @level2type=N'COLUMN',@level2name=N'IsEmployeAvailable'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ��������� �������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentRequest', @level2type=N'COLUMN',@level2name=N'DepNextId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������� �������� ��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentRequest', @level2type=N'COLUMN',@level2name=N'IsPlan'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������� �������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentRequest', @level2type=N'COLUMN',@level2name=N'IsUsed'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������� ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentRequest', @level2type=N'COLUMN',@level2name=N'IsDraft'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� �������� �� ������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentRequest', @level2type=N'COLUMN',@level2name=N'DateSendToApprove'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� ������ ����� � �������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentRequest', @level2type=N'COLUMN',@level2name=N'BeginAccountDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentRequest', @level2type=N'COLUMN',@level2name=N'CreatorID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� �������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentRequest', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentRequest', @level2type=N'COLUMN',@level2name=N'EditorID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� ���������� �������������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentRequest', @level2type=N'COLUMN',@level2name=N'EditDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������ �� ��������� ��������� �������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentRequest'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentTaxDetails', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id �������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentTaxDetails', @level2type=N'COLUMN',@level2name=N'DepartmentId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentTaxDetails', @level2type=N'COLUMN',@level2name=N'KPP'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentTaxDetails', @level2type=N'COLUMN',@level2name=N'OKTMO'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentTaxDetails', @level2type=N'COLUMN',@level2name=N'OKATO'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��� �������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentTaxDetails', @level2type=N'COLUMN',@level2name=N'RegionCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��� ��������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentTaxDetails', @level2type=N'COLUMN',@level2name=N'TaxAdminCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�������� ��������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentTaxDetails', @level2type=N'COLUMN',@level2name=N'TaxAdminName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�������� �����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentTaxDetails', @level2type=N'COLUMN',@level2name=N'PostAddress'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������� ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentTaxDetails'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentTypes', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������ ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentTypes', @level2type=N'COLUMN',@level2name=N'Version'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentTypes', @level2type=N'COLUMN',@level2name=N'Name'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� �������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentTypes', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���������� ����� �������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentTypes'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPost', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������ ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPost', @level2type=N'COLUMN',@level2name=N'Version'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPost', @level2type=N'COLUMN',@level2name=N'PositionId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id �������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPost', @level2type=N'COLUMN',@level2name=N'DepartmentId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPost', @level2type=N'COLUMN',@level2name=N'Quantity'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPost', @level2type=N'COLUMN',@level2name=N'Salary'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������� �������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPost', @level2type=N'COLUMN',@level2name=N'IsUsed'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� ������ ����� � �������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPost', @level2type=N'COLUMN',@level2name=N'BeginAccountDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPost', @level2type=N'COLUMN',@level2name=N'CreatorID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� �������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPost', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPost', @level2type=N'COLUMN',@level2name=N'EditorID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� ���������� �������������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPost', @level2type=N'COLUMN',@level2name=N'EditDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���������� ������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPost'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostArchive', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������� �������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostArchive', @level2type=N'COLUMN',@level2name=N'SEPId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostArchive', @level2type=N'COLUMN',@level2name=N'PositionId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id �������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostArchive', @level2type=N'COLUMN',@level2name=N'DepartmentId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostArchive', @level2type=N'COLUMN',@level2name=N'Quantity'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostArchive', @level2type=N'COLUMN',@level2name=N'Salary'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������� �������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostArchive', @level2type=N'COLUMN',@level2name=N'IsUsed'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� ������ ����� � �������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostArchive', @level2type=N'COLUMN',@level2name=N'BeginAccountDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���������� � �������� �������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostArchive', @level2type=N'COLUMN',@level2name=N'Priority'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID ���������/���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostArchive', @level2type=N'COLUMN',@level2name=N'CreatorID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� ��������/�������������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostArchive', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����� ����������� ������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostArchive'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostRequest', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������ ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostRequest', @level2type=N'COLUMN',@level2name=N'Version'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostRequest', @level2type=N'COLUMN',@level2name=N'DateRequest'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostRequest', @level2type=N'COLUMN',@level2name=N'RequestTypeId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������� �������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostRequest', @level2type=N'COLUMN',@level2name=N'SEPId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostRequest', @level2type=N'COLUMN',@level2name=N'PositionId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id �������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostRequest', @level2type=N'COLUMN',@level2name=N'DepartmentId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostRequest', @level2type=N'COLUMN',@level2name=N'Quantity'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostRequest', @level2type=N'COLUMN',@level2name=N'Salary'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������� �������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostRequest', @level2type=N'COLUMN',@level2name=N'IsUsed'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������� ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostRequest', @level2type=N'COLUMN',@level2name=N'IsDraft'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� �������� �� ������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostRequest', @level2type=N'COLUMN',@level2name=N'DateSendToApprove'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� ������ ����� � �������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostRequest', @level2type=N'COLUMN',@level2name=N'BeginAccountDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������� �������� ������� �������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostRequest', @level2type=N'COLUMN',@level2name=N'ReasonId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostRequest', @level2type=N'COLUMN',@level2name=N'CreatorID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� �������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostRequest', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostRequest', @level2type=N'COLUMN',@level2name=N'EditorID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� ���������� �������������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostRequest', @level2type=N'COLUMN',@level2name=N'EditDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������ �� ������� ��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostRequest'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffExtraCharges', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������������� ������ ��� 1�' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffExtraCharges', @level2type=N'COLUMN',@level2name=N'GUID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffExtraCharges', @level2type=N'COLUMN',@level2name=N'Name'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���������� ��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffExtraCharges'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffLandmarkTypes', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffLandmarkTypes', @level2type=N'COLUMN',@level2name=N'Name'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� �������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffLandmarkTypes', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���������� ����� ����������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffLandmarkTypes'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffProgramCodes', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������ ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffProgramCodes', @level2type=N'COLUMN',@level2name=N'Version'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id �������������� ����������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffProgramCodes', @level2type=N'COLUMN',@level2name=N'DMDetailId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ����������� ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffProgramCodes', @level2type=N'COLUMN',@level2name=N'ProgramId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��� (�������������?)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffProgramCodes', @level2type=N'COLUMN',@level2name=N'Code'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������ ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffProgramCodes', @level2type=N'COLUMN',@level2name=N'CreatorId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� �������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffProgramCodes', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffProgramCodes', @level2type=N'COLUMN',@level2name=N'EditorId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� ���������� ��������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffProgramCodes', @level2type=N'COLUMN',@level2name=N'EditDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���������� ����� ����������� ��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffProgramCodes'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffProgramReference', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�������� ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffProgramReference', @level2type=N'COLUMN',@level2name=N'Name'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� �������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffProgramReference', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���������� ����������� ��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffProgramReference'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������ ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'Version'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'Address'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'PostIndex'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��� �������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'RegionCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�������� �������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'RegionName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'AreaCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'AreaName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'CityCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'CityName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��� ����������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'SettlementCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�������� ����������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'SettlementName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��� �����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'StreetCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�������� �����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'StreetName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��� ���/��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'HouseType'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����� ����/��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'HouseNumber'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��� ������/��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'BuildType'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����� �������/��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'BuildNumber'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��� ��������/����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'FlatType'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����� ��������/�����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'FlatNumber'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������� �������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'IsUsed'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������ ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'CreatorId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� �������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'EditorId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� ���������� ��������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'EditDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���������� �������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�������� �������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Kladr', @level2type=N'COLUMN',@level2name=N'Name'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������� �������� ���� �������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Kladr', @level2type=N'COLUMN',@level2name=N'ShortName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�������� ������ �������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Kladr', @level2type=N'COLUMN',@level2name=N'Index'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�������������� �������� �������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Kladr', @level2type=N'COLUMN',@level2name=N'AltName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��� �������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Kladr', @level2type=N'COLUMN',@level2name=N'AddressType'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��� �������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Kladr', @level2type=N'COLUMN',@level2name=N'RegionCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Kladr', @level2type=N'COLUMN',@level2name=N'AreaCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Kladr', @level2type=N'COLUMN',@level2name=N'CityCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��� ����������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Kladr', @level2type=N'COLUMN',@level2name=N'SettlementCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��� �����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Kladr', @level2type=N'COLUMN',@level2name=N'StreetCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��� �������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Kladr', @level2type=N'COLUMN',@level2name=N'Code'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������������� �������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Kladr'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentOperationModes', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������ ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentOperationModes', @level2type=N'COLUMN',@level2name=N'Version'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id �������������� ����������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentOperationModes', @level2type=N'COLUMN',@level2name=N'DMDetailId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����� ��� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentOperationModes', @level2type=N'COLUMN',@level2name=N'WeekDay'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����� ������ �������� ���' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentOperationModes', @level2type=N'COLUMN',@level2name=N'WorkBegin'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����� ��������� �������� ���' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentOperationModes', @level2type=N'COLUMN',@level2name=N'WorkEnd'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������ ���������� ��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentOperationModes', @level2type=N'COLUMN',@level2name=N'BreakBegin'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����� ���������� ��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentOperationModes', @level2type=N'COLUMN',@level2name=N'BreakEnd'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������� �������� ��� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentOperationModes', @level2type=N'COLUMN',@level2name=N'IsWorkDay'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������ ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentOperationModes', @level2type=N'COLUMN',@level2name=N'CreatorId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� �������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentOperationModes', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentOperationModes', @level2type=N'COLUMN',@level2name=N'EditorId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� ���������� ��������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentOperationModes', @level2type=N'COLUMN',@level2name=N'EditDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����� ������ �������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentOperationModes'
GO



--5. �������� �������������
IF OBJECT_ID ('vwKladr_1', 'V') IS NOT NULL
	DROP VIEW [dbo].[vwKladr_1]
GO
CREATE VIEW [dbo].[vwKladr_1]
AS
SELECT     NULL AS Name, NULL AS ShortName, NULL AS [Index], NULL AS AltName, NULL AS AddressType, NULL AS RegionCode, NULL AS AreaCode, NULL AS CityCode, NULL
                       AS SettlementCode, NULL AS StreetCode, NULL AS Code
UNION ALL
SELECT     Name + ' ' + ShortName AS Name, ShortName, [Index], AltName, AddressType, RegionCode, AreaCode, CityCode, SettlementCode, StreetCode, Code
FROM         dbo.Kladr
WHERE     (AddressType = 1)

GO



IF OBJECT_ID ('vwKladr_2', 'V') IS NOT NULL
	DROP VIEW [dbo].[vwKladr_2]
GO
CREATE VIEW [dbo].[vwKladr_2]
AS
SELECT     NULL AS Name, NULL AS ShortName, NULL AS [Index], NULL AS AltName, NULL AS AddressType, NULL AS RegionCode, NULL AS AreaCode, NULL AS CityCode, NULL
                       AS SettlementCode, NULL AS StreetCode, NULL AS Code
UNION ALL
SELECT     Name + ' ' + ShortName AS Name, ShortName, [Index], AltName, AddressType, RegionCode, AreaCode, CityCode, SettlementCode, StreetCode, Code
FROM         dbo.Kladr
WHERE     (AddressType = 2)
GO


IF OBJECT_ID ('vwKladr_3', 'V') IS NOT NULL
	DROP VIEW [dbo].[vwKladr_3]
GO
CREATE VIEW [dbo].[vwKladr_3]
AS
SELECT     NULL AS Name, NULL AS ShortName, NULL AS [Index], NULL AS AltName, NULL AS AddressType, NULL AS RegionCode, NULL AS AreaCode, NULL AS CityCode, NULL
                       AS SettlementCode, NULL AS StreetCode, NULL AS Code
UNION ALL
SELECT     Name + ' ' + ShortName AS Name, ShortName, [Index], AltName, AddressType, RegionCode, AreaCode, CityCode, SettlementCode, StreetCode, Code
FROM         dbo.Kladr
WHERE     (AddressType = 3)
GO


IF OBJECT_ID ('vwKladr_4', 'V') IS NOT NULL
	DROP VIEW [dbo].[vwKladr_4]
GO
CREATE VIEW [dbo].[vwKladr_4]
AS
SELECT     NULL AS Name, NULL AS ShortName, NULL AS [Index], NULL AS AltName, NULL AS AddressType, NULL AS RegionCode, NULL AS AreaCode, NULL AS CityCode, NULL
                       AS SettlementCode, NULL AS StreetCode, NULL AS Code
UNION ALL
SELECT     Name + ' ' + ShortName AS Name, ShortName, [Index], AltName, AddressType, RegionCode, AreaCode, CityCode, SettlementCode, StreetCode, Code
FROM         dbo.Kladr
WHERE     (AddressType = 4)
GO


IF OBJECT_ID ('vwKladr_5', 'V') IS NOT NULL
	DROP VIEW [dbo].[vwKladr_5]
GO
CREATE VIEW [dbo].[vwKladr_5]
AS
SELECT     NULL AS Name, NULL AS ShortName, NULL AS [Index], NULL AS AltName, NULL AS AddressType, NULL AS RegionCode, NULL AS AreaCode, NULL AS CityCode, NULL
                       AS SettlementCode, NULL AS StreetCode, NULL AS Code
UNION ALL
SELECT     Name + ' ' + ShortName AS Name, ShortName, [Index], AltName, AddressType, RegionCode, AreaCode, CityCode, SettlementCode, StreetCode, Code
FROM         dbo.Kladr
WHERE     (AddressType = 5)
GO


IF OBJECT_ID ('vwStaffListDepartment', 'V') IS NOT NULL
	DROP VIEW [dbo].[vwStaffListDepartment]
GO

--��������� ���������� �����
CREATE VIEW [dbo].[vwStaffListDepartment]
AS
SELECT A.Id, A.Code, A.Name, A.Code1C, A.ParentId, A.Path, A.ItemLevel, A.CodeSKD, A.Priority, C.NameShort as DepFingradName, C.NameComment as DepFingradNameComment, C.DepCode as FinDepPointCode
FROM Department as A
LEFT JOIN StaffDepartmentRequest as B ON B.DepartmentId = A.Id and B.IsUsed = 1 
LEFT JOIN StaffDepartmentManagerDetails as C ON C.DepRequestId = B.Id
--INNER JOIN Department as C ON C.id = A.DepartmentId
GO


--6. ���������� ������������ �������
--StaffExtraCharges
INSERT INTO StaffExtraCharges([GUID], Name) VALUES(N'4f4a4697-cc10-11dd-87ea-00304861d218', N'�������� �� ������� ��� ������� � ��������#1114')
INSERT INTO StaffExtraCharges([GUID], Name) VALUES(N'4f4a4696-cc10-11dd-87ea-00304861d218', N'�������� �� ������������#1115')
INSERT INTO StaffExtraCharges([GUID], Name) VALUES(N'd9cd6dfe-b4b0-11de-b733-003048359abd', N'�������� �� ���������� �������� ������#1116')
INSERT INTO StaffExtraCharges([GUID], Name) VALUES(N'784efe28-3634-11dd-b8e4-00304861d218', N'�������� ������������#1117')
INSERT INTO StaffExtraCharges([GUID], Name) VALUES(N'c693b11a-ec98-11df-aabb-003048ba0538', N'�������� ���������������#1123')
INSERT INTO StaffExtraCharges([GUID], Name) VALUES(N'66f08438-f006-44e8-b9ee-32a8dcf557ba', N'�������� �����������#1301')
INSERT INTO StaffExtraCharges([GUID], Name) VALUES(N'1f076cf3-1ebb-11e4-80c8-002590d1e727', N'�������� �������� (�������) 1#1302')
INSERT INTO StaffExtraCharges([GUID], Name) VALUES(N'a5ceb324-a745-11de-b733-003048359abd', N'�������� �������� (���.) 1#1302')


--StaffLandmarkTypes
INSERT INTO StaffLandmarkTypes(Name) VALUES(N'������� �����')
INSERT INTO StaffLandmarkTypes(Name) VALUES(N'��������� ����������')
INSERT INTO StaffLandmarkTypes(Name) VALUES(N'�������� �������')
INSERT INTO StaffLandmarkTypes(Name) VALUES(N'�������� ������')
INSERT INTO StaffLandmarkTypes(Name) VALUES(N'����� ������')

--StaffProgramReference
INSERT INTO StaffProgramReference(Name) VALUES(N'��������')
INSERT INTO StaffProgramReference(Name) VALUES(N'���')
INSERT INTO StaffProgramReference(Name) VALUES(N'��������')
INSERT INTO StaffProgramReference(Name) VALUES(N'��')
INSERT INTO StaffProgramReference(Name) VALUES(N'���������')
INSERT INTO StaffProgramReference(Name) VALUES(N'���')
INSERT INTO StaffProgramReference(Name) VALUES(N'���/GE')

--StaffDepartmentRequestTypes
INSERT INTO StaffDepartmentRequestTypes(Version, Name) VALUES(1, N'�������� ��')
INSERT INTO StaffDepartmentRequestTypes(Version, Name) VALUES(1, N'��������� ���������� ��')
INSERT INTO StaffDepartmentRequestTypes(Version, Name) VALUES(1, N'�������� ��')

--StaffEstablishedPostRequestTypes
INSERT INTO StaffEstablishedPostRequestTypes(Version, Name) VALUES(1, N'�������� ��')
INSERT INTO StaffEstablishedPostRequestTypes(Version, Name) VALUES(1, N'��������� ��')
INSERT INTO StaffEstablishedPostRequestTypes(Version, Name) VALUES(1, N'���������� ��')

--DepartmentArchive
INSERT INTO DepartmentArchive(DepartmentId, Code, Name, Code1C, ParentId, Path, ItemLevel, IsUsed)
SELECT Id, Code, Name, Code1C, ParentId, Path, ItemLevel, 1 FROM Department

--StaffDepartmentReasons
INSERT INTO StaffDepartmentReasons(Name) VALUES (N'-')
INSERT INTO StaffDepartmentReasons(Name) VALUES (N'��� �����')
INSERT INTO StaffDepartmentReasons(Name) VALUES (N'��� ����� ��')
INSERT INTO StaffDepartmentReasons(Name) VALUES (N'������������')
INSERT INTO StaffDepartmentReasons(Name) VALUES (N'�������')
INSERT INTO StaffDepartmentReasons(Name) VALUES (N'�� ������� - ��������������')
INSERT INTO StaffDepartmentReasons(Name) VALUES (N'������� ����')
INSERT INTO StaffDepartmentReasons(Name) VALUES (N'������� ������ ���������')
INSERT INTO StaffDepartmentReasons(Name) VALUES (N'������� �����������')
INSERT INTO StaffDepartmentReasons(Name) VALUES (N'������� ��������')
INSERT INTO StaffDepartmentReasons(Name) VALUES (N'������� �������')
INSERT INTO StaffDepartmentReasons(Name) VALUES (N'������� �����������')
INSERT INTO StaffDepartmentReasons(Name) VALUES (N'�� ����� ��')
INSERT INTO StaffDepartmentReasons(Name) VALUES (N'��������� �����')
INSERT INTO StaffDepartmentReasons(Name) VALUES (N'��������� �������')

--StaffNetShopIdentification
INSERT INTO StaffNetShopIdentification(Name) VALUES (N'-')
INSERT INTO StaffNetShopIdentification(Name) VALUES (N'�� ���������')
INSERT INTO StaffNetShopIdentification(Name) VALUES (N'�� �������')
INSERT INTO StaffNetShopIdentification(Name) VALUES (N'�������')

--StaffDepartmentTypes
INSERT INTO StaffDepartmentTypes(Version, Name) VALUES(1, N'-')
INSERT INTO StaffDepartmentTypes(Version, Name) VALUES(1, N'Cash-in')
INSERT INTO StaffDepartmentTypes(Version, Name) VALUES(1, N'����')
INSERT INTO StaffDepartmentTypes(Version, Name) VALUES(1, N'��������')
INSERT INTO StaffDepartmentTypes(Version, Name) VALUES(1, N'��������, Cash-in')
INSERT INTO StaffDepartmentTypes(Version, Name) VALUES(1, N'��������')
INSERT INTO StaffDepartmentTypes(Version, Name) VALUES(1, N'��')
INSERT INTO StaffDepartmentTypes(Version, Name) VALUES(1, N'���')
INSERT INTO StaffDepartmentTypes(Version, Name) VALUES(1, N'��')
INSERT INTO StaffDepartmentTypes(Version, Name) VALUES(1, N'��1')
INSERT INTO StaffDepartmentTypes(Version, Name) VALUES(1, N'��2')
INSERT INTO StaffDepartmentTypes(Version, Name) VALUES(1, N'���')
INSERT INTO StaffDepartmentTypes(Version, Name) VALUES(1, N'�����')
INSERT INTO StaffDepartmentTypes(Version, Name) VALUES(1, N'��')
INSERT INTO StaffDepartmentTypes(Version, Name) VALUES(1, N'��������� � ����������')
INSERT INTO StaffDepartmentTypes(Version, Name) VALUES(1, N'�����������������')
INSERT INTO StaffDepartmentTypes(Version, Name) VALUES(1, N'�����')
INSERT INTO StaffDepartmentTypes(Version, Name) VALUES(1, N'���')
INSERT INTO StaffDepartmentTypes(Version, Name) VALUES(1, N'����1')
INSERT INTO StaffDepartmentTypes(Version, Name) VALUES(1, N'����2')
INSERT INTO StaffDepartmentTypes(Version, Name) VALUES(1, N'�����')
INSERT INTO StaffDepartmentTypes(Version, Name) VALUES(1, N'������')
INSERT INTO StaffDepartmentTypes(Version, Name) VALUES(1, N'���1')
INSERT INTO StaffDepartmentTypes(Version, Name) VALUES(1, N'���2')
INSERT INTO StaffDepartmentTypes(Version, Name) VALUES(1, N'����')
INSERT INTO StaffDepartmentTypes(Version, Name) VALUES(1, N'���')
INSERT INTO StaffDepartmentTypes(Version, Name) VALUES(1, N'������')


IF DB_NAME() = 'WebAppTest'
BEGIN
	INSERT INTO Kladr 
	SELECT * FROM WebAppSKB.dbo.Kladr

	UPDATE sysdiagrams SET definition = B.definition
	FROM sysdiagrams as A
	INNER JOIN WebAppSKB.dbo.sysdiagrams as B ON B.diagram_id = A.diagram_id 

END


DELETE FROM Messages WHERE CommentPlaceType in (2, 3)

--7. �������� �������

IF OBJECT_ID ('fnGetDepartmentOperationModes', 'TF') IS NOT NULL
	DROP FUNCTION [dbo].[fnGetDepartmentOperationModes]
GO

--������� ������� ����� ������ ��� ������������� �� Id �������������� ���������� ������
CREATE FUNCTION [dbo].[fnGetDepartmentOperationModes]
(
	@DMDetailId int
)
RETURNS 
@ReturnTable TABLE 
(
	 Id int 
	,DMDetailId int
	,WeekDay int
	,WorkBegin nvarchar(5)
	,WorkEnd nvarchar(5)
	,BreakBegin nvarchar(5)
	,BreakEnd nvarchar(5)
	,IsWorkDay bit
)
AS
BEGIN

	INSERT INTO @ReturnTable
	SELECT Id, DMDetailId, WeekDay, WorkBegin, WorkEnd, BreakBegin, BreakEnd, IsWorkDay 
	FROM StaffDepartmentOperationModes
	WHERE DMDetailId = @DMDetailId

	IF NOT EXISTS (SELECT * FROM @ReturnTable)
	BEGIN
		INSERT INTO @ReturnTable
		SELECT Id, DMDetailId, WeekDay, WorkBegin, WorkEnd, BreakBegin, BreakEnd, IsWorkDay FROM StaffDepartmentOperationModes WHERE DMDetailId = -1
		UNION ALL
		SELECT null, null, 1, null, null, null, null, 0
		UNION ALL
		SELECT null, null, 2, null, null, null, null, 0
		UNION ALL
		SELECT null, null, 3, null, null, null, null, 0
		UNION ALL
		SELECT null, null, 4, null, null, null, null, 0
		UNION ALL
		SELECT null, null, 5, null, null, null, null, 0
		UNION ALL
		SELECT null, null, 6, null, null, null, null, 0
		UNION ALL
		SELECT null, null, 7, null, null, null, null, 0
	END


	RETURN 
END

GO




IF OBJECT_ID ('fnGetStaffEstablishedPostCountByDepartment', 'FN') IS NOT NULL
	DROP FUNCTION [dbo].[fnGetStaffEstablishedPostCountByDepartment]
GO


--������� ���������� ���������� ������� ������ ������������� �������� ���������� ��������� ������������� �� 7 ������ ������������
CREATE FUNCTION dbo.fnGetStaffEstablishedPostCountByDepartment
(
	@DepartmentId int	--Id �������������
)
RETURNS int
AS
BEGIN
	DECLARE @SEPCount int, @DepId int
	DECLARE @tbl table (DepartmentId int)

	--������������ �� �������� ������������� ���������� ������� ������
	IF (SELECT ItemLevel FROM Department WHERE Id = @DepartmentId) = 1
	BEGIN
		SELECT @SEPCount = isnull(sum(Quantity), 0) FROM StaffEstablishedPost WHERE IsUsed = 1
		RETURN @SEPCount
	END
	ELSE
		--SELECT @SEPCount = isnull(sum(Quantity), 0) FROM StaffEstablishedPost WHERE DepartmentId = @DepartmentId and IsUsed = 1
		SELECT @SEPCount = isnull(sum(C.Quantity), 0) 
		FROM Department as A
		INNER JOIN Department as B ON B.Path like A.Path + N'%'
		INNER JOIN StaffEstablishedPost as C ON C.DepartmentId = B.Id and C.IsUsed = 1
		WHERE A.Id = @DepartmentId 
/*
	--���� ���� ����������� �������������, �� ������� � ���
	IF EXISTS (SELECT * FROM Department as A
						 INNER JOIN Department as B ON B.ParentId = A.Code1C and B.ItemLevel <= 7
						 WHERE A.Id = @DepartmentId)
	BEGIN
		INSERT INTO @tbl 
		SELECT B.Id FROM Department as A
		INNER JOIN Department as B ON B.ParentId = A.Code1C and B.ItemLevel <= 7
		WHERE A.Id = @DepartmentId

		WHILE EXISTS (SELECT * FROM @tbl)
		BEGIN
			SELECT top 1 @DepId = DepartmentId FROM @tbl

			SET @SEPCount += (SELECT dbo.fnGetStaffEstablishedPostCountByDepartment(@DepId))

			DELETE FROM @tbl WHERE DepartmentId = @DepId
		END
	END
*/
	
	RETURN @SEPCount
--SELECT dbo.fnGetStaffEstablishedPostCountByDepartment(4128) as SEPCount
--SELECT dbo.fnGetStaffEstablishedPostCountByDepartment(4129) as SEPCount
--SELECT dbo.fnGetStaffEstablishedPostCountByDepartment(4130) as SEPCount
--SELECT dbo.fnGetStaffEstablishedPostCountByDepartment(4131) as SEPCount
--SELECT dbo.fnGetStaffEstablishedPostCountByDepartment(4132) as SEPCount
--SELECT dbo.fnGetStaffEstablishedPostCountByDepartment(4205) as SEPCount
--SELECT dbo.fnGetStaffEstablishedPostCountByDepartment(8010) as SEPCount
END
GO


