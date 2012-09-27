if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_DismissalComment_User]') AND parent_object_id = OBJECT_ID('DismissalComment'))
alter table DismissalComment  drop constraint FK_DismissalComment_User

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_DismissalComment_Dismissal]') AND parent_object_id = OBJECT_ID('DismissalComment'))
alter table DismissalComment  drop constraint FK_DismissalComment_Dismissal

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Absence_AbsenceType]') AND parent_object_id = OBJECT_ID('Absence'))
alter table Absence  drop constraint FK_Absence_AbsenceType

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Absence_User]') AND parent_object_id = OBJECT_ID('Absence'))
alter table Absence  drop constraint FK_Absence_User

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Absence_CreatorUser]') AND parent_object_id = OBJECT_ID('Absence'))
alter table Absence  drop constraint FK_Absence_CreatorUser

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Absence_TimesheetStatus]') AND parent_object_id = OBJECT_ID('Absence'))
alter table Absence  drop constraint FK_Absence_TimesheetStatus

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Employment_EmploymentType]') AND parent_object_id = OBJECT_ID('Employment'))
alter table Employment  drop constraint FK_Employment_EmploymentType

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Employment_EmploymentHoursType]') AND parent_object_id = OBJECT_ID('Employment'))
alter table Employment  drop constraint FK_Employment_EmploymentHoursType

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Employment_Addition]') AND parent_object_id = OBJECT_ID('Employment'))
alter table Employment  drop constraint FK_Employment_Addition

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Employment_Position]') AND parent_object_id = OBJECT_ID('Employment'))
alter table Employment  drop constraint FK_Employment_Position

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Employment_User]') AND parent_object_id = OBJECT_ID('Employment'))
alter table Employment  drop constraint FK_Employment_User

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Employment_CreatorUser]') AND parent_object_id = OBJECT_ID('Employment'))
alter table Employment  drop constraint FK_Employment_CreatorUser

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Employment_TimesheetStatus]') AND parent_object_id = OBJECT_ID('Employment'))
alter table Employment  drop constraint FK_Employment_TimesheetStatus

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Document_EmployeeDocumentType]') AND parent_object_id = OBJECT_ID('Document'))
alter table Document  drop constraint FK_Document_EmployeeDocumentType

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Document_EmployeeDocumentSubType]') AND parent_object_id = OBJECT_ID('Document'))
alter table Document  drop constraint FK_Document_EmployeeDocumentSubType

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Document_User]') AND parent_object_id = OBJECT_ID('Document'))
alter table Document  drop constraint FK_Document_User

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_TimesheetCorrectionComment_User]') AND parent_object_id = OBJECT_ID('TimesheetCorrectionComment'))
alter table TimesheetCorrectionComment  drop constraint FK_TimesheetCorrectionComment_User

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_TimesheetCorrectionComment_TimesheetCorrection]') AND parent_object_id = OBJECT_ID('TimesheetCorrectionComment'))
alter table TimesheetCorrectionComment  drop constraint FK_TimesheetCorrectionComment_TimesheetCorrection

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_USER_ROLE]') AND parent_object_id = OBJECT_ID('[Users]'))
alter table [Users]  drop constraint FK_USER_ROLE

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_User_UserManager]') AND parent_object_id = OBJECT_ID('[Users]'))
alter table [Users]  drop constraint FK_User_UserManager

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_User_PersonnelManager]') AND parent_object_id = OBJECT_ID('[Users]'))
alter table [Users]  drop constraint FK_User_PersonnelManager

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_User_Organization]') AND parent_object_id = OBJECT_ID('[Users]'))
alter table [Users]  drop constraint FK_User_Organization

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_User_Position]') AND parent_object_id = OBJECT_ID('[Users]'))
alter table [Users]  drop constraint FK_User_Position

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_User_Department]') AND parent_object_id = OBJECT_ID('[Users]'))
alter table [Users]  drop constraint FK_User_Department

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Sicklist_SicklistType]') AND parent_object_id = OBJECT_ID('Sicklist'))
alter table Sicklist  drop constraint FK_Sicklist_SicklistType

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Sicklist_BabyMindingType]') AND parent_object_id = OBJECT_ID('Sicklist'))
alter table Sicklist  drop constraint FK_Sicklist_BabyMindingType

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Sicklist_SicklistPaymentPercent]') AND parent_object_id = OBJECT_ID('Sicklist'))
alter table Sicklist  drop constraint FK_Sicklist_SicklistPaymentPercent

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Sicklist_SicklistPaymentRestrictType]') AND parent_object_id = OBJECT_ID('Sicklist'))
alter table Sicklist  drop constraint FK_Sicklist_SicklistPaymentRestrictType

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Sicklist_User]') AND parent_object_id = OBJECT_ID('Sicklist'))
alter table Sicklist  drop constraint FK_Sicklist_User

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Sicklist_CreatorUser]') AND parent_object_id = OBJECT_ID('Sicklist'))
alter table Sicklist  drop constraint FK_Sicklist_CreatorUser

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Sicklist_TimesheetStatus]') AND parent_object_id = OBJECT_ID('Sicklist'))
alter table Sicklist  drop constraint FK_Sicklist_TimesheetStatus

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_EmploymentComment_User]') AND parent_object_id = OBJECT_ID('EmploymentComment'))
alter table EmploymentComment  drop constraint FK_EmploymentComment_User

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_EmploymentComment_Employment]') AND parent_object_id = OBJECT_ID('EmploymentComment'))
alter table EmploymentComment  drop constraint FK_EmploymentComment_Employment

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_DocumentComment_User]') AND parent_object_id = OBJECT_ID('DocumentComment'))
alter table DocumentComment  drop constraint FK_DocumentComment_User

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_DocumentComment_Document]') AND parent_object_id = OBJECT_ID('DocumentComment'))
alter table DocumentComment  drop constraint FK_DocumentComment_Document

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_TimesheetDay_Status]') AND parent_object_id = OBJECT_ID('TimesheetDay'))
alter table TimesheetDay  drop constraint FK_TimesheetDay_Status

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_TimesheetDay_Timesheet]') AND parent_object_id = OBJECT_ID('TimesheetDay'))
alter table TimesheetDay  drop constraint FK_TimesheetDay_Timesheet

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_DocumentSubType_EmployeeDocumentType]') AND parent_object_id = OBJECT_ID('EmployeeDocumentSubType'))
alter table EmployeeDocumentSubType  drop constraint FK_DocumentSubType_EmployeeDocumentType

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Attachment_Document]') AND parent_object_id = OBJECT_ID('Attachment'))
alter table Attachment  drop constraint FK_Attachment_Document

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_SicklistComment_User]') AND parent_object_id = OBJECT_ID('SicklistComment'))
alter table SicklistComment  drop constraint FK_SicklistComment_User

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_SicklistComment_Sicklist]') AND parent_object_id = OBJECT_ID('SicklistComment'))
alter table SicklistComment  drop constraint FK_SicklistComment_Sicklist

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_AbsenceComment_User]') AND parent_object_id = OBJECT_ID('AbsenceComment'))
alter table AbsenceComment  drop constraint FK_AbsenceComment_User

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_AbsenceComment_Absence]') AND parent_object_id = OBJECT_ID('AbsenceComment'))
alter table AbsenceComment  drop constraint FK_AbsenceComment_Absence

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Mission_MissionType]') AND parent_object_id = OBJECT_ID('Mission'))
alter table Mission  drop constraint FK_Mission_MissionType

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Mission_User]') AND parent_object_id = OBJECT_ID('Mission'))
alter table Mission  drop constraint FK_Mission_User

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Mission_CreatorUser]') AND parent_object_id = OBJECT_ID('Mission'))
alter table Mission  drop constraint FK_Mission_CreatorUser

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Mission_TimesheetStatus]') AND parent_object_id = OBJECT_ID('Mission'))
alter table Mission  drop constraint FK_Mission_TimesheetStatus

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_TimesheetCorrection_TimesheetCorrectionType]') AND parent_object_id = OBJECT_ID('TimesheetCorrection'))
alter table TimesheetCorrection  drop constraint FK_TimesheetCorrection_TimesheetCorrectionType

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_TimesheetCorrection_User]') AND parent_object_id = OBJECT_ID('TimesheetCorrection'))
alter table TimesheetCorrection  drop constraint FK_TimesheetCorrection_User

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_TimesheetCorrection_CreatorUser]') AND parent_object_id = OBJECT_ID('TimesheetCorrection'))
alter table TimesheetCorrection  drop constraint FK_TimesheetCorrection_CreatorUser

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_TimesheetCorrection_TimesheetStatus]') AND parent_object_id = OBJECT_ID('TimesheetCorrection'))
alter table TimesheetCorrection  drop constraint FK_TimesheetCorrection_TimesheetStatus

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Timesheet_User]') AND parent_object_id = OBJECT_ID('Timesheet'))
alter table Timesheet  drop constraint FK_Timesheet_User

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_UserLogin_User]') AND parent_object_id = OBJECT_ID('UserLogin'))
alter table UserLogin  drop constraint FK_UserLogin_User

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_HolidayWorkComment_User]') AND parent_object_id = OBJECT_ID('HolidayWorkComment'))
alter table HolidayWorkComment  drop constraint FK_HolidayWorkComment_User

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_HolidayWorkComment_HolidayWork]') AND parent_object_id = OBJECT_ID('HolidayWorkComment'))
alter table HolidayWorkComment  drop constraint FK_HolidayWorkComment_HolidayWork

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Dismissal_DismissalType]') AND parent_object_id = OBJECT_ID('Dismissal'))
alter table Dismissal  drop constraint FK_Dismissal_DismissalType

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Dismissal_User]') AND parent_object_id = OBJECT_ID('Dismissal'))
alter table Dismissal  drop constraint FK_Dismissal_User

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Dismissal_CreatorUser]') AND parent_object_id = OBJECT_ID('Dismissal'))
alter table Dismissal  drop constraint FK_Dismissal_CreatorUser

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Dismissal_TimesheetStatus]') AND parent_object_id = OBJECT_ID('Dismissal'))
alter table Dismissal  drop constraint FK_Dismissal_TimesheetStatus

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MissionComment_User]') AND parent_object_id = OBJECT_ID('MissionComment'))
alter table MissionComment  drop constraint FK_MissionComment_User

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MissionComment_Mission]') AND parent_object_id = OBJECT_ID('MissionComment'))
alter table MissionComment  drop constraint FK_MissionComment_Mission

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_ATTACHMENT_USER_ROLE]') AND parent_object_id = OBJECT_ID('RequestAttachment'))
alter table RequestAttachment  drop constraint FK_ATTACHMENT_USER_ROLE

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Vacation_VacationType]') AND parent_object_id = OBJECT_ID('Vacation'))
alter table Vacation  drop constraint FK_Vacation_VacationType

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Vacation_User]') AND parent_object_id = OBJECT_ID('Vacation'))
alter table Vacation  drop constraint FK_Vacation_User

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Vacation_CreatorUser]') AND parent_object_id = OBJECT_ID('Vacation'))
alter table Vacation  drop constraint FK_Vacation_CreatorUser

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Vacation_TimesheetStatus]') AND parent_object_id = OBJECT_ID('Vacation'))
alter table Vacation  drop constraint FK_Vacation_TimesheetStatus

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_HolidayWork_HolidayWorkType]') AND parent_object_id = OBJECT_ID('HolidayWork'))
alter table HolidayWork  drop constraint FK_HolidayWork_HolidayWorkType

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_HolidayWork_User]') AND parent_object_id = OBJECT_ID('HolidayWork'))
alter table HolidayWork  drop constraint FK_HolidayWork_User

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_HolidayWork_CreatorUser]') AND parent_object_id = OBJECT_ID('HolidayWork'))
alter table HolidayWork  drop constraint FK_HolidayWork_CreatorUser

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_HolidayWork_TimesheetStatus]') AND parent_object_id = OBJECT_ID('HolidayWork'))
alter table HolidayWork  drop constraint FK_HolidayWork_TimesheetStatus

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_VacationComment_User]') AND parent_object_id = OBJECT_ID('VacationComment'))
alter table VacationComment  drop constraint FK_VacationComment_User

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_VacationComment_Vacation]') AND parent_object_id = OBJECT_ID('VacationComment'))
alter table VacationComment  drop constraint FK_VacationComment_Vacation

if exists (select * from dbo.sysobjects where id = object_id(N'DismissalComment') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table DismissalComment
if exists (select * from dbo.sysobjects where id = object_id(N'Absence') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Absence
if exists (select * from dbo.sysobjects where id = object_id(N'Employment') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Employment
if exists (select * from dbo.sysobjects where id = object_id(N'SicklistType') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table SicklistType
if exists (select * from dbo.sysobjects where id = object_id(N'Document') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Document
if exists (select * from dbo.sysobjects where id = object_id(N'TimesheetCorrectionComment') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table TimesheetCorrectionComment
if exists (select * from dbo.sysobjects where id = object_id(N'EmploymentHoursType') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table EmploymentHoursType
if exists (select * from dbo.sysobjects where id = object_id(N'EmploymentType') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table EmploymentType
if exists (select * from dbo.sysobjects where id = object_id(N'Information') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Information
if exists (select * from dbo.sysobjects where id = object_id(N'[Users]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [Users]
if exists (select * from dbo.sysobjects where id = object_id(N'Sicklist') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Sicklist
if exists (select * from dbo.sysobjects where id = object_id(N'Settings') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Settings
if exists (select * from dbo.sysobjects where id = object_id(N'DBVersion') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table DBVersion
if exists (select * from dbo.sysobjects where id = object_id(N'DismissalType') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table DismissalType
if exists (select * from dbo.sysobjects where id = object_id(N'Role') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Role
if exists (select * from dbo.sysobjects where id = object_id(N'VacationType') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table VacationType
if exists (select * from dbo.sysobjects where id = object_id(N'SicklistPaymentPercent') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table SicklistPaymentPercent
if exists (select * from dbo.sysobjects where id = object_id(N'EmploymentComment') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table EmploymentComment
if exists (select * from dbo.sysobjects where id = object_id(N'HolidayWorkType') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table HolidayWorkType
if exists (select * from dbo.sysobjects where id = object_id(N'DocumentComment') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table DocumentComment
if exists (select * from dbo.sysobjects where id = object_id(N'TimesheetDay') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table TimesheetDay
if exists (select * from dbo.sysobjects where id = object_id(N'EmployeeDocumentSubType') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table EmployeeDocumentSubType
if exists (select * from dbo.sysobjects where id = object_id(N'Attachment') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Attachment
if exists (select * from dbo.sysobjects where id = object_id(N'SicklistComment') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table SicklistComment
if exists (select * from dbo.sysobjects where id = object_id(N'AbsenceComment') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table AbsenceComment
if exists (select * from dbo.sysobjects where id = object_id(N'Mission') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Mission
if exists (select * from dbo.sysobjects where id = object_id(N'SicklistPaymentRestrictType') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table SicklistPaymentRestrictType
if exists (select * from dbo.sysobjects where id = object_id(N'EmployeeDocumentType') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table EmployeeDocumentType
if exists (select * from dbo.sysobjects where id = object_id(N'TimesheetCorrection') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table TimesheetCorrection
if exists (select * from dbo.sysobjects where id = object_id(N'AbsenceType') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table AbsenceType
if exists (select * from dbo.sysobjects where id = object_id(N'Organization') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Organization
if exists (select * from dbo.sysobjects where id = object_id(N'Timesheet') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Timesheet
if exists (select * from dbo.sysobjects where id = object_id(N'UserLogin') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table UserLogin
if exists (select * from dbo.sysobjects where id = object_id(N'Department') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Department
if exists (select * from dbo.sysobjects where id = object_id(N'EmploymentAddition') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table EmploymentAddition
if exists (select * from dbo.sysobjects where id = object_id(N'MissionType') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table MissionType
if exists (select * from dbo.sysobjects where id = object_id(N'HolidayWorkComment') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table HolidayWorkComment
if exists (select * from dbo.sysobjects where id = object_id(N'RequestStatus') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table RequestStatus
if exists (select * from dbo.sysobjects where id = object_id(N'ExportImportAction') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table ExportImportAction
if exists (select * from dbo.sysobjects where id = object_id(N'RequestPrintForm') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table RequestPrintForm
if exists (select * from dbo.sysobjects where id = object_id(N'Dismissal') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Dismissal
if exists (select * from dbo.sysobjects where id = object_id(N'MissionComment') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table MissionComment
if exists (select * from dbo.sysobjects where id = object_id(N'Position') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Position
if exists (select * from dbo.sysobjects where id = object_id(N'RequestAttachment') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table RequestAttachment
if exists (select * from dbo.sysobjects where id = object_id(N'Vacation') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Vacation
if exists (select * from dbo.sysobjects where id = object_id(N'TimesheetCorrectionType') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table TimesheetCorrectionType
if exists (select * from dbo.sysobjects where id = object_id(N'HolidayWork') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table HolidayWork
if exists (select * from dbo.sysobjects where id = object_id(N'RequestNextNumber') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table RequestNextNumber
if exists (select * from dbo.sysobjects where id = object_id(N'VacationComment') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table VacationComment
if exists (select * from dbo.sysobjects where id = object_id(N'TimesheetStatus') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table TimesheetStatus
if exists (select * from dbo.sysobjects where id = object_id(N'SicklistBabyMindingType') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table SicklistBabyMindingType

create table DismissalComment (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  UserId INT not null,
  DismissalId INT not null,
  DateCreated DATETIME not null,
  Comment NVARCHAR(256) not null,
  constraint PK_DismissalComment  primary key (Id)
)
create table Absence (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  CreateDate DATETIME not null,
  BeginDate DATETIME not null,
  EndDate DATETIME not null,
  DaysCount INT not null,
  Number INT not null,
  TypeId INT not null,
  UserId INT not null,
  CreatorId INT not null,
  UserDateAccept DATETIME null,
  ManagerDateAccept DATETIME null,
  PersonnelManagerDateAccept DATETIME null,
  SendTo1C DATETIME null,
  DeleteDate DATETIME null,
  TimesheetStatusId INT null,
  constraint PK_Absence  primary key (Id)
)
create table Employment (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  CreateDate DATETIME not null,
  BeginDate DATETIME not null,
  Number INT not null,
  TypeId INT not null,
  HoursTypeId INT not null,
  AdditionId INT null,
  PositionId INT not null,
  Salary DECIMAL(19, 2) not null,
  Probaion INT null,
  Reason NVARCHAR(256) null,
  UserId INT not null,
  CreatorId INT not null,
  UserDateAccept DATETIME null,
  ManagerDateAccept DATETIME null,
  PersonnelManagerDateAccept DATETIME null,
  SendTo1C DATETIME null,
  DeleteDate DATETIME null,
  TimesheetStatusId INT null,
  constraint PK_Employment  primary key (Id)
)
create table SicklistType (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  Code INT null,
  Name NVARCHAR(128) null,
  constraint PK_SicklistType  primary key (Id)
)
create table Document (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  LastModifiedDate DATETIME not null,
  Comment NVARCHAR(MAX) null,
  TypeId INT not null,
  SubTypeId INT null,
  UserId INT not null,
  ManagerDateAccept DATETIME null,
  PersonnelManagerDateAccept DATETIME null,
  BudgetManagerDateAccept DATETIME null,
  OutsourcingManagerDateAccept DATETIME null,
  SendEmailToBilling BIT not null,
  constraint PK_Document  primary key (Id)
)
create table TimesheetCorrectionComment (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  UserId INT not null,
  TimesheetCorrectionId INT not null,
  DateCreated DATETIME not null,
  Comment NVARCHAR(256) not null,
  constraint PK_TimesheetCorrectionComment  primary key (Id)
)
create table EmploymentHoursType (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  Name NVARCHAR(128) not null,
  constraint PK_EmploymentHoursType  primary key (Id)
)
create table EmploymentType (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  Name NVARCHAR(128) not null,
  constraint PK_EmploymentType  primary key (Id)
)
create table Information (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  Subject NVARCHAR(1024) not null,
  Message NVARCHAR(MAX) not null,
  constraint PK_Information  primary key (Id)
)
create table [Users] (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  IsFirstTimeLogin BIT not null,
  IsActive BIT not null,
  IsNew BIT not null,
  Login NVARCHAR(64) not null unique,
  Password NVARCHAR(64) null,
  Code NVARCHAR(32) null,
  DateAccept DATETIME null,
  Name NVARCHAR(512) not null,
  Email NVARCHAR(512) null,
  DateRelease DATETIME null,
  Comment NVARCHAR(512) null,
  RoleId INT not null,
  ManagerId INT null,
  PersonnelManagerId INT null,
  OrganizationId INT null,
  PositionId INT null,
  DepartmentId INT null,
  constraint PK_Users primary key (Id)
)
create table Sicklist (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  CreateDate DATETIME not null,
  BeginDate DATETIME not null,
  EndDate DATETIME not null,
  DaysCount INT not null,
  Number INT not null,
  TypeId INT not null,
  BabyMindingTypeId INT null,
  PaymentPercentId INT null,
  PaymentRestrictTypeId INT null,
  PaymentBeginDate DATETIME null,
  ExperienceYears INT null,
  ExperienceMonthes INT null,
  PaymentDecreaseDate DATETIME null,
  IsPreviousPaymentCounted BIT not null,
  Is2010Calculate BIT not null,
  IsAddToFullPayment BIT not null,
  UserId INT not null,
  CreatorId INT not null,
  UserDateAccept DATETIME null,
  ManagerDateAccept DATETIME null,
  PersonnelManagerDateAccept DATETIME null,
  SendTo1C DATETIME null,
  DeleteDate DATETIME null,
  TimesheetStatusId INT null,
  constraint PK_Sicklist  primary key (Id)
)
create table Settings (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  DownloadDictionaryFilePath NVARCHAR(512) not null,
  UploadTimesheetFilePath NVARCHAR(512) not null,
  ReleaseEmployeeDeleteDate DATETIME not null,
  ExportImportEmail NVARCHAR(128) not null,
  SendEmailToManagerAboutNew BIT not null,
  NotificationEmail NVARCHAR(128) not null,
  NotificationSmtp NVARCHAR(128) not null,
  NotificationPort INT not null,
  NotificationLogin NVARCHAR(32) not null,
  NotificationPassword NVARCHAR(32) not null,
  BillingEmail NVARCHAR(128) not null,
  BillingSmtp NVARCHAR(128) not null,
  BillingPort INT not null,
  BillingLogin NVARCHAR(32) not null,
  BillingPassword NVARCHAR(32) not null,
  constraint PK_Settings  primary key (Id)
)
create table DBVersion (
 Id INT IDENTITY NOT NULL,
  Version NVARCHAR(20) not null,
  constraint PK_DBVersion  primary key (Id)
)
create table DismissalType (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  Name NVARCHAR(128) not null,
  Reason NVARCHAR(512) not null,
  constraint PK_DismissalType  primary key (Id)
)
create table Role (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  Name NVARCHAR(100) not null,
  constraint PK_Role  primary key (Id)
)
create table VacationType (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  Code INT null,
  Name NVARCHAR(128) null,
  constraint PK_VacationType  primary key (Id)
)
create table SicklistPaymentPercent (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  SicklistPercent INT null,
  SortOrder INT null,
  constraint PK_SicklistPaymentPercent  primary key (Id)
)
create table EmploymentComment (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  UserId INT not null,
  EmploymentId INT not null,
  DateCreated DATETIME not null,
  Comment NVARCHAR(256) not null,
  constraint PK_EmploymentComment  primary key (Id)
)
create table HolidayWorkType (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  Code INT null,
  Name NVARCHAR(128) not null,
  constraint PK_HolidayWorkType  primary key (Id)
)
create table DocumentComment (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  UserId INT not null,
  DocumentId INT not null,
  DateCreated DATETIME not null,
  Comment NVARCHAR(256) not null,
  constraint PK_DocumentComment  primary key (Id)
)
create table TimesheetDay (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  Number INT not null,
  Hours REAL not null,
  StatusId INT not null,
  TimesheetId INT not null,
  constraint PK_TimesheetDay  primary key (Id)
)
create table EmployeeDocumentSubType (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  Name NVARCHAR(100) not null,
  TypeId INT not null,
  constraint PK_EmployeeDocumentSubType  primary key (Id)
)
create table Attachment (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  FileName NVARCHAR(64) not null,
  ContextType NVARCHAR(64) not null,
  Context VARBINARY(MAX) not null,
  DocumentId INT not null,
  constraint PK_Attachment  primary key (Id)
)
create table SicklistComment (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  UserId INT not null,
  SicklistId INT not null,
  DateCreated DATETIME not null,
  Comment NVARCHAR(256) not null,
  constraint PK_SicklistComment  primary key (Id)
)
create table AbsenceComment (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  UserId INT not null,
  AbsenceId INT not null,
  DateCreated DATETIME not null,
  Comment NVARCHAR(256) not null,
  constraint PK_AbsenceComment  primary key (Id)
)
create table Mission (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  CreateDate DATETIME not null,
  BeginDate DATETIME not null,
  EndDate DATETIME not null,
  DaysCount INT not null,
  Number INT not null,
  TypeId INT not null,
  Country NVARCHAR(255) not null,
  Organization NVARCHAR(255) not null,
  Goal NVARCHAR(255) not null,
  FinancesSource NVARCHAR(255) not null,
  Reason NVARCHAR(255) null,
  UserId INT not null,
  CreatorId INT not null,
  UserDateAccept DATETIME null,
  ManagerDateAccept DATETIME null,
  PersonnelManagerDateAccept DATETIME null,
  SendTo1C DATETIME null,
  DeleteDate DATETIME null,
  TimesheetStatusId INT null,
  constraint PK_Mission  primary key (Id)
)
create table SicklistPaymentRestrictType (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  Name NVARCHAR(128) null,
  constraint PK_SicklistPaymentRestrictType  primary key (Id)
)
create table EmployeeDocumentType (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  Name NVARCHAR(100) not null,
  constraint PK_EmployeeDocumentType  primary key (Id)
)
create table TimesheetCorrection (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  CreateDate DATETIME not null,
  EventDate DATETIME not null,
  Number INT not null,
  TypeId INT not null,
  Hours INT null,
  UserId INT not null,
  CreatorId INT not null,
  UserDateAccept DATETIME null,
  ManagerDateAccept DATETIME null,
  PersonnelManagerDateAccept DATETIME null,
  SendTo1C DATETIME null,
  DeleteDate DATETIME null,
  TimesheetStatusId INT null,
  constraint PK_TimesheetCorrection  primary key (Id)
)
create table AbsenceType (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  Code INT null,
  Name NVARCHAR(128) null,
  constraint PK_AbsenceType  primary key (Id)
)
create table Organization (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  Code NVARCHAR(10) null,
  Name NVARCHAR(256) null,
  constraint PK_Organization  primary key (Id)
)
create table Timesheet (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  Month DATETIME not null,
  UserNotAcceptDate DATETIME null,
  PersonnelNotAcceptDate DATETIME null,
  UserId INT not null,
  constraint PK_Timesheet  primary key (Id)
)
create table UserLogin (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  UserId INT null,
  Date DATETIME not null,
  constraint PK_UserLogin  primary key (Id)
)
create table Department (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  Code NVARCHAR(10) null,
  Name NVARCHAR(128) null,
  constraint PK_Department  primary key (Id)
)
create table EmploymentAddition (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  Code NVARCHAR(255) null,
  Name NVARCHAR(128) not null,
  CalculationMethod NVARCHAR(255) null,
  constraint PK_EmploymentAddition  primary key (Id)
)
create table MissionType (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  Name NVARCHAR(128) null,
  constraint PK_MissionType  primary key (Id)
)
create table HolidayWorkComment (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  UserId INT not null,
  HolidayWorkId INT not null,
  DateCreated DATETIME not null,
  Comment NVARCHAR(256) not null,
  constraint PK_HolidayWorkComment  primary key (Id)
)
create table RequestStatus (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  Name NVARCHAR(128) null,
  constraint PK_RequestStatus  primary key (Id)
)
create table ExportImportAction (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  Type INT not null,
  Date DATETIME not null,
  Month DATETIME null,
  constraint PK_ExportImportAction  primary key (Id)
)
create table RequestPrintForm (
 Id INT IDENTITY NOT NULL,
  Context VARBINARY(MAX) not null,
  RequestId INT not null,
  RequestTypeId INT not null,
  constraint PK_RequestPrintForm  primary key (Id)
)
create table Dismissal (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  CreateDate DATETIME not null,
  EndDate DATETIME not null,
  Number INT not null,
  TypeId INT not null,
  Compensation DECIMAL(19, 2) null,
  Reason NVARCHAR(256) not null,
  UserId INT not null,
  CreatorId INT not null,
  UserDateAccept DATETIME null,
  ManagerDateAccept DATETIME null,
  PersonnelManagerDateAccept DATETIME null,
  SendTo1C DATETIME null,
  DeleteDate DATETIME null,
  TimesheetStatusId INT null,
  constraint PK_Dismissal  primary key (Id)
)
create table MissionComment (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  UserId INT not null,
  MissionId INT not null,
  DateCreated DATETIME not null,
  Comment NVARCHAR(256) not null,
  constraint PK_MissionComment  primary key (Id)
)
create table Position (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  Code NVARCHAR(10) null,
  Name NVARCHAR(128) null,
  constraint PK_Position  primary key (Id)
)
create table RequestAttachment (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  FileName NVARCHAR(64) not null,
  ContextType NVARCHAR(64) not null,
  Context VARBINARY(MAX) not null,
  RequestId INT not null,
  RequestType INT not null,
  DateCreated DATETIME not null,
  Description NVARCHAR(256) null,
  CreatorRoleId INT not null,
  constraint PK_RequestAttachment  primary key (Id)
)
create table Vacation (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  CreateDate DATETIME not null,
  BeginDate DATETIME not null,
  EndDate DATETIME not null,
  DaysCount INT not null,
  Number INT not null,
  TypeId INT not null,
  UserId INT not null,
  CreatorId INT not null,
  UserDateAccept DATETIME null,
  ManagerDateAccept DATETIME null,
  PersonnelManagerDateAccept DATETIME null,
  SendTo1C DATETIME null,
  DeleteDate DATETIME null,
  TimesheetStatusId INT null,
  constraint PK_Vacation  primary key (Id)
)
create table TimesheetCorrectionType (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  Code INT not null,
  Name NVARCHAR(128) not null,
  Reason NVARCHAR(512) not null,
  constraint PK_TimesheetCorrectionType  primary key (Id)
)
create table HolidayWork (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  CreateDate DATETIME not null,
  Number INT not null,
  TypeId INT not null,
  WorkDate DATETIME null,
  Hours INT not null,
  UserId INT not null,
  CreatorId INT not null,
  UserDateAccept DATETIME null,
  ManagerDateAccept DATETIME null,
  PersonnelManagerDateAccept DATETIME null,
  SendTo1C DATETIME null,
  DeleteDate DATETIME null,
  TimesheetStatusId INT null,
  constraint PK_HolidayWork  primary key (Id)
)
create table RequestNextNumber (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  RequestTypeId INT not null,
  NextNumber INT not null,
  constraint PK_RequestNextNumber  primary key (Id)
)
create table VacationComment (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  UserId INT not null,
  VacationId INT not null,
  DateCreated DATETIME not null,
  Comment NVARCHAR(256) not null,
  constraint PK_VacationComment  primary key (Id)
)
create table TimesheetStatus (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  ShortName NVARCHAR(2) not null,
  Name NVARCHAR(255) not null,
  constraint PK_TimesheetStatus  primary key (Id)
)
create table SicklistBabyMindingType (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  Code INT null,
  Name NVARCHAR(128) null,
  constraint PK_SicklistBabyMindingType  primary key (Id)
)
create index IX_DismissalComment_User_Id on DismissalComment (UserId)
create index IX_DismissalComment_Dismissal_Id on DismissalComment (DismissalId)
alter table DismissalComment add constraint FK_DismissalComment_User foreign key (UserId) references [Users]
alter table DismissalComment add constraint FK_DismissalComment_Dismissal foreign key (DismissalId) references Dismissal
create index Absence_AbsenceType on Absence (TypeId)
create index IX_Absence_User_Id on Absence (UserId)
create index IX_Absence_CreatorUser_Id on Absence (CreatorId)
create index Absence_TimesheetStatus on Absence (TimesheetStatusId)
alter table Absence add constraint FK_Absence_AbsenceType foreign key (TypeId) references AbsenceType
alter table Absence add constraint FK_Absence_User foreign key (UserId) references [Users]
alter table Absence add constraint FK_Absence_CreatorUser foreign key (CreatorId) references [Users]
alter table Absence add constraint FK_Absence_TimesheetStatus foreign key (TimesheetStatusId) references TimesheetStatus
create index Employment_EmploymentType on Employment (TypeId)
create index Employment_EmploymentHoursType on Employment (HoursTypeId)
create index Employment_Addition on Employment (AdditionId)
create index Employment_Position on Employment (PositionId)
create index IX_Employment_User_Id on Employment (UserId)
create index IX_Employment_CreatorUser_Id on Employment (CreatorId)
create index Employment_TimesheetStatus on Employment (TimesheetStatusId)
alter table Employment add constraint FK_Employment_EmploymentType foreign key (TypeId) references EmploymentType
alter table Employment add constraint FK_Employment_EmploymentHoursType foreign key (HoursTypeId) references EmploymentHoursType
alter table Employment add constraint FK_Employment_Addition foreign key (AdditionId) references EmploymentAddition
alter table Employment add constraint FK_Employment_Position foreign key (PositionId) references Position
alter table Employment add constraint FK_Employment_User foreign key (UserId) references [Users]
alter table Employment add constraint FK_Employment_CreatorUser foreign key (CreatorId) references [Users]
alter table Employment add constraint FK_Employment_TimesheetStatus foreign key (TimesheetStatusId) references TimesheetStatus
create index IX_Document_EmployeeDocumentType_Id on Document (TypeId)
create index IX_Document_EmployeeDocumentSubType_Id on Document (SubTypeId)
create index IX_Document_User_Id on Document (UserId)
alter table Document add constraint FK_Document_EmployeeDocumentType foreign key (TypeId) references EmployeeDocumentType
alter table Document add constraint FK_Document_EmployeeDocumentSubType foreign key (SubTypeId) references EmployeeDocumentSubType
alter table Document add constraint FK_Document_User foreign key (UserId) references [Users]
create index IX_TimesheetCorrectionComment_User_Id on TimesheetCorrectionComment (UserId)
create index IX_TimesheetCorrectionComment_TimesheetCorrection_Id on TimesheetCorrectionComment (TimesheetCorrectionId)
alter table TimesheetCorrectionComment add constraint FK_TimesheetCorrectionComment_User foreign key (UserId) references [Users]
alter table TimesheetCorrectionComment add constraint FK_TimesheetCorrectionComment_TimesheetCorrection foreign key (TimesheetCorrectionId) references TimesheetCorrection
create index IX_USER_ROLE_ID on [Users] (RoleId)
create index IX_User_UserManager_Id on [Users] (ManagerId)
create index IX_User_PersonnelManager_Id on [Users] (PersonnelManagerId)
create index IX_User_Organization_Id on [Users] (OrganizationId)
create index IX_User_Position_Id on [Users] (PositionId)
create index IX_User_Department_Id on [Users] (DepartmentId)
alter table [Users] add constraint FK_USER_ROLE foreign key (RoleId) references Role
alter table [Users] add constraint FK_User_UserManager foreign key (ManagerId) references [Users]
alter table [Users] add constraint FK_User_PersonnelManager foreign key (PersonnelManagerId) references [Users]
alter table [Users] add constraint FK_User_Organization foreign key (OrganizationId) references Organization
alter table [Users] add constraint FK_User_Position foreign key (PositionId) references Position
alter table [Users] add constraint FK_User_Department foreign key (DepartmentId) references Department
create index Sicklist_SicklistType on Sicklist (TypeId)
create index Sicklist_BabyMindingType on Sicklist (BabyMindingTypeId)
create index Sicklist_SicklistPaymentPercent on Sicklist (PaymentPercentId)
create index Sicklist_SicklistPaymentRestrictType on Sicklist (PaymentRestrictTypeId)
create index IX_Sicklist_User_Id on Sicklist (UserId)
create index IX_Sicklist_CreatorUser_Id on Sicklist (CreatorId)
create index Sicklist_TimesheetStatus on Sicklist (TimesheetStatusId)
alter table Sicklist add constraint FK_Sicklist_SicklistType foreign key (TypeId) references SicklistType
alter table Sicklist add constraint FK_Sicklist_BabyMindingType foreign key (BabyMindingTypeId) references SicklistBabyMindingType
alter table Sicklist add constraint FK_Sicklist_SicklistPaymentPercent foreign key (PaymentPercentId) references SicklistPaymentPercent
alter table Sicklist add constraint FK_Sicklist_SicklistPaymentRestrictType foreign key (PaymentRestrictTypeId) references SicklistPaymentRestrictType
alter table Sicklist add constraint FK_Sicklist_User foreign key (UserId) references [Users]
alter table Sicklist add constraint FK_Sicklist_CreatorUser foreign key (CreatorId) references [Users]
alter table Sicklist add constraint FK_Sicklist_TimesheetStatus foreign key (TimesheetStatusId) references TimesheetStatus
create index IX_EmploymentComment_User_Id on EmploymentComment (UserId)
create index IX_EmploymentComment_Employment_Id on EmploymentComment (EmploymentId)
alter table EmploymentComment add constraint FK_EmploymentComment_User foreign key (UserId) references [Users]
alter table EmploymentComment add constraint FK_EmploymentComment_Employment foreign key (EmploymentId) references Employment
create index IX_DocumentComment_User_Id on DocumentComment (UserId)
create index IX_DocumentComment_Document_Id on DocumentComment (DocumentId)
alter table DocumentComment add constraint FK_DocumentComment_User foreign key (UserId) references [Users]
alter table DocumentComment add constraint FK_DocumentComment_Document foreign key (DocumentId) references Document
create index IX_TimesheetDay_Status_Id on TimesheetDay (StatusId)
create index IX_TimesheetDay_Timesheet_Id on TimesheetDay (TimesheetId)
alter table TimesheetDay add constraint FK_TimesheetDay_Status foreign key (StatusId) references TimesheetStatus
alter table TimesheetDay add constraint FK_TimesheetDay_Timesheet foreign key (TimesheetId) references Timesheet
create index IX_DocumentSubType_EmployeeDocumentType_Id on EmployeeDocumentSubType (TypeId)
alter table EmployeeDocumentSubType add constraint FK_DocumentSubType_EmployeeDocumentType foreign key (TypeId) references EmployeeDocumentType
create index IX_Attachment_Document_Id on Attachment (DocumentId)
alter table Attachment add constraint FK_Attachment_Document foreign key (DocumentId) references Document
create index IX_SicklistComment_User_Id on SicklistComment (UserId)
create index IX_SicklistComment_Sicklist_Id on SicklistComment (SicklistId)
alter table SicklistComment add constraint FK_SicklistComment_User foreign key (UserId) references [Users]
alter table SicklistComment add constraint FK_SicklistComment_Sicklist foreign key (SicklistId) references Sicklist
create index IX_AbsenceComment_User_Id on AbsenceComment (UserId)
create index IX_AbsenceComment_Absence_Id on AbsenceComment (AbsenceId)
alter table AbsenceComment add constraint FK_AbsenceComment_User foreign key (UserId) references [Users]
alter table AbsenceComment add constraint FK_AbsenceComment_Absence foreign key (AbsenceId) references Absence
create index Mission_MissionType on Mission (TypeId)
create index IX_Mission_User_Id on Mission (UserId)
create index IX_Mission_CreatorUser_Id on Mission (CreatorId)
create index Mission_TimesheetStatus on Mission (TimesheetStatusId)
alter table Mission add constraint FK_Mission_MissionType foreign key (TypeId) references MissionType
alter table Mission add constraint FK_Mission_User foreign key (UserId) references [Users]
alter table Mission add constraint FK_Mission_CreatorUser foreign key (CreatorId) references [Users]
alter table Mission add constraint FK_Mission_TimesheetStatus foreign key (TimesheetStatusId) references TimesheetStatus
create index TimesheetCorrection_TimesheetCorrectionType on TimesheetCorrection (TypeId)
create index IX_TimesheetCorrection_User_Id on TimesheetCorrection (UserId)
create index IX_TimesheetCorrection_CreatorUser_Id on TimesheetCorrection (CreatorId)
create index TimesheetCorrection_TimesheetStatus on TimesheetCorrection (TimesheetStatusId)
alter table TimesheetCorrection add constraint FK_TimesheetCorrection_TimesheetCorrectionType foreign key (TypeId) references TimesheetCorrectionType
alter table TimesheetCorrection add constraint FK_TimesheetCorrection_User foreign key (UserId) references [Users]
alter table TimesheetCorrection add constraint FK_TimesheetCorrection_CreatorUser foreign key (CreatorId) references [Users]
alter table TimesheetCorrection add constraint FK_TimesheetCorrection_TimesheetStatus foreign key (TimesheetStatusId) references TimesheetStatus
create index IX_Timesheet_User_Id on Timesheet (UserId)
alter table Timesheet add constraint FK_Timesheet_User foreign key (UserId) references [Users]
alter table UserLogin add constraint FK_UserLogin_User foreign key (UserId) references [Users]
create index IX_HolidayWorkComment_User_Id on HolidayWorkComment (UserId)
create index IX_HolidayWorkComment_HolidayWork_Id on HolidayWorkComment (HolidayWorkId)
alter table HolidayWorkComment add constraint FK_HolidayWorkComment_User foreign key (UserId) references [Users]
alter table HolidayWorkComment add constraint FK_HolidayWorkComment_HolidayWork foreign key (HolidayWorkId) references HolidayWork
create index Dismissal_DismissalType on Dismissal (TypeId)
create index IX_Dismissal_User_Id on Dismissal (UserId)
create index IX_Dismissal_CreatorUser_Id on Dismissal (CreatorId)
create index Dismissal_TimesheetStatus on Dismissal (TimesheetStatusId)
alter table Dismissal add constraint FK_Dismissal_DismissalType foreign key (TypeId) references DismissalType
alter table Dismissal add constraint FK_Dismissal_User foreign key (UserId) references [Users]
alter table Dismissal add constraint FK_Dismissal_CreatorUser foreign key (CreatorId) references [Users]
alter table Dismissal add constraint FK_Dismissal_TimesheetStatus foreign key (TimesheetStatusId) references TimesheetStatus
create index IX_MissionComment_User_Id on MissionComment (UserId)
create index IX_MissionComment_Mission_Id on MissionComment (MissionId)
alter table MissionComment add constraint FK_MissionComment_User foreign key (UserId) references [Users]
alter table MissionComment add constraint FK_MissionComment_Mission foreign key (MissionId) references Mission
create index IX_ATTACHMENT_USER_ROLE_ID on RequestAttachment (CreatorRoleId)
alter table RequestAttachment add constraint FK_ATTACHMENT_USER_ROLE foreign key (CreatorRoleId) references Role
create index Vacation_VacationType on Vacation (TypeId)
create index IX_Vacation_User_Id on Vacation (UserId)
create index IX_Vacation_CreatorUser_Id on Vacation (CreatorId)
create index Vacation_TimesheetStatus on Vacation (TimesheetStatusId)
alter table Vacation add constraint FK_Vacation_VacationType foreign key (TypeId) references VacationType
alter table Vacation add constraint FK_Vacation_User foreign key (UserId) references [Users]
alter table Vacation add constraint FK_Vacation_CreatorUser foreign key (CreatorId) references [Users]
alter table Vacation add constraint FK_Vacation_TimesheetStatus foreign key (TimesheetStatusId) references TimesheetStatus
create index HolidayWork_HolidayWorkType on HolidayWork (TypeId)
create index IX_HolidayWork_User_Id on HolidayWork (UserId)
create index IX_HolidayWork_CreatorUser_Id on HolidayWork (CreatorId)
create index HolidayWork_TimesheetStatus on HolidayWork (TimesheetStatusId)
alter table HolidayWork add constraint FK_HolidayWork_HolidayWorkType foreign key (TypeId) references HolidayWorkType
alter table HolidayWork add constraint FK_HolidayWork_User foreign key (UserId) references [Users]
alter table HolidayWork add constraint FK_HolidayWork_CreatorUser foreign key (CreatorId) references [Users]
alter table HolidayWork add constraint FK_HolidayWork_TimesheetStatus foreign key (TimesheetStatusId) references TimesheetStatus
create index IX_VacationComment_User_Id on VacationComment (UserId)
create index IX_VacationComment_Vacation_Id on VacationComment (VacationId)
alter table VacationComment add constraint FK_VacationComment_User foreign key (UserId) references [Users]
alter table VacationComment add constraint FK_VacationComment_Vacation foreign key (VacationId) references Vacation

set identity_insert  [Role] on
INSERT INTO [Role] (Id,[Name],Version) values (1,'Администратор',1) 
INSERT INTO [Role] (Id,[Name],Version) values (2,'Сотрудник',1) 
INSERT INTO [Role] (Id,[Name],Version) values (4,'Руководитель',1) 
INSERT INTO [Role] (Id,[Name],Version) values (8,'Кадровик',1) 
INSERT INTO [Role] (Id,[Name],Version) values (16,'Бюджет',1) 
INSERT INTO [Role] (Id,[Name],Version) values (32,'Отусорсинг',1)
set identity_insert  [Role] off 

set identity_insert  [RequestStatus] on
INSERT INTO [RequestStatus] (Id,[Name],Version) values (1,'Не одобрена сотрудником',1) 
INSERT INTO [RequestStatus] (Id,[Name],Version) values (2,'Одобрена сотрудником',1) 
INSERT INTO [RequestStatus] (Id,[Name],Version) values (3,'Одобрена руководителем',1) 
INSERT INTO [RequestStatus] (Id,[Name],Version) values (4,'Одобрена кадровиком',1)
INSERT INTO [RequestStatus] (Id,[Name],Version) values (5,'Выгружена в 1С',1)
set identity_insert  [RequestStatus] off 

declare @OrganizationId int
declare @Organization1Id int
INSERT INTO [dbo].[Organization]  ([Code],[Name],Version) values (1,'Тестовая организация',1)	
set @OrganizationId = @@Identity
INSERT INTO [dbo].[Organization]  ([Code],[Name],Version) values (2,'Тестовая организация 1',1)	
set @Organization1Id = @@Identity	
	

declare @DepartmentId int
declare @Department1Id int
INSERT INTO [dbo].[Department]  ([Code],[Name],Version) values (1,'Тестовый департамент',1)		
set @DepartmentId = @@Identity	
INSERT INTO [dbo].[Department]  ([Code],[Name],Version) values (2,'Тестовый департамент 1',1)		
set @Department1Id = @@Identity	


declare @PositionId int
declare @Position1Id int
INSERT INTO [dbo].[Position]  ([Code],[Name],Version) values (1,'Тестовая должность',1)		
set @PositionId = @@Identity
INSERT INTO [dbo].[Position]  ([Code],[Name],Version) values (2,'Тестовая должность 1',1)		
set @Position1Id = @@Identity
declare @ManPositionId int
INSERT INTO [dbo].[Position]  ([Code],[Name],Version) values (3,'Руководитель',1)		
set @ManPositionId = @@Identity	
declare @PerPositionId int
INSERT INTO [dbo].[Position]  ([Code],[Name],Version) values (4,'Кадровик',1)		
set @PerPositionId = @@Identity	
	

INSERT INTO [dbo].[VacationType]  ([Code],[Name],Version) values (51,'Дополнительный учебный отпуск без оплаты #1203',1)			
INSERT INTO [dbo].[VacationType]  ([Code],[Name],Version) values (31,'Оплата дня сдачи крови и доп. дня отдыха донорам #1125',1)			
INSERT INTO [dbo].[VacationType]  ([Code],[Name],Version) values (41,'Оплата дополнительного отпуска по календарным дням #1207',1)			
INSERT INTO [dbo].[VacationType]  ([Code],[Name],Version) values (32,'Оплата дополнительных выходных дней по уходу за детьми - инвалидами #1504',1)			
INSERT INTO [dbo].[VacationType]  ([Code],[Name],Version) values (41,'Оплата отпуска по календарным дням #1201',1)			
INSERT INTO [dbo].[VacationType]  ([Code],[Name],Version) values (42,'Оплата отпуска по шестидневке #1202',1)			
INSERT INTO [dbo].[VacationType]  ([Code],[Name],Version) values (41,'Оплата учебного отпуска по календарным дням #1204',1)			
INSERT INTO [dbo].[VacationType]  ([Code],[Name],Version) values (53,'Отпуск без оплаты согласно ТК РФ #1205',1)			
INSERT INTO [dbo].[VacationType]  ([Code],[Name],Version) values (54,'Отпуск за свой счет #1206',1)			
INSERT INTO [dbo].[VacationType]  ([Code],[Name],Version) values (23,'Отпуск по беременности и родам #1501',1)			
INSERT INTO [dbo].[VacationType]  ([Code],[Name],Version) values (52,'Отпуск по уходу за ребенком без оплаты #1802',1)		


INSERT INTO [dbo].[AbsenceType]  ([Code],[Name],Version) values (51,'Дополнительный учебный отпуск без оплаты #1203',1)	
INSERT INTO [dbo].[AbsenceType]  ([Code],[Name],Version) values (53,'Отпуск без оплаты согласно ТК РФ #1205',1)	
INSERT INTO [dbo].[AbsenceType]  ([Code],[Name],Version) values (54,'Отпуск за свой счет#1206',1)	
INSERT INTO [dbo].[AbsenceType]  ([Code],[Name],Version) values (10024,'Отсутствие по болезни (по беременности и родам) #1804',1)	
INSERT INTO [dbo].[AbsenceType]  ([Code],[Name],Version) values (10023,'Отсутствие по болезни #1803',1)	
INSERT INTO [dbo].[AbsenceType]  ([Code],[Name],Version) values (55,'Отсутствие по невыясненной причине #1806',1)	
INSERT INTO [dbo].[AbsenceType]  ([Code],[Name],Version) values (56,'Прогул, простой по вине работника #1807',1)	

INSERT INTO [dbo].[SicklistType]  ([Code],[Name],Version) values (25,'БЛ по травме в быту (не оплачивается) #1805',1)
INSERT INTO [dbo].[SicklistType]  ([Code],[Name],Version) values (26,'Доплата по больничным листам #1402',1)
INSERT INTO [dbo].[SicklistType]  ([Code],[Name],Version) values (22,'Оплата БЛ по травме на производстве #1403',1)
INSERT INTO [dbo].[SicklistType]  ([Code],[Name],Version) values (25,'Оплата больничных листов за счет работодателя #1426',1)
INSERT INTO [dbo].[SicklistType]  ([Code],[Name],Version) values (21,'Оплата больничных листов #1469',1)
INSERT INTO [dbo].[SicklistType]  ([Code],[Name],Version) values (10024,'Отсутствие по болезни (по беременности и родам) #1804',1)
INSERT INTO [dbo].[SicklistType]  ([Code],[Name],Version) values (10023,'Отсутствие по болезни #1803',1)
INSERT INTO [dbo].[SicklistType]  ([Code],[Name],Version) values (55,'Отсутствие по невыясненной причине #1806',1)
set identity_insert  [dbo].[SicklistType] on
INSERT INTO [dbo].[SicklistType]  (Id,[Code],[Name],Version) values (9,71,'Пособие по уходу за ребёнком до 1.5 лет #1502',1)
INSERT INTO [dbo].[SicklistType]  (Id,[Code],[Name],Version) values (10,72,'Пособие по уходу за ребёнком до 3 лет #1503',1)
set identity_insert [dbo].[SicklistType] off 

INSERT INTO [dbo].[SicklistBabyMindingType]  ([Code],[Name],Version) values (73,'Тестовый уход за ребенком 1',1)
INSERT INTO [dbo].[SicklistBabyMindingType]  ([Code],[Name],Version) values (74,'Тестовый уход за ребенком 2',1)

INSERT INTO [dbo].[HolidayWorkType]  ([Code],[Name],Version) values (13,'Доплата за работу в праздники и выходные #1107',1)
INSERT INTO [dbo].[HolidayWorkType]  ([Code],[Name],Version) values (12,'Оплата праздничных и выходных дней #1106',1)
INSERT INTO [dbo].[HolidayWorkType]  ([Code],[Name],Version) values (null,'Отгул',1)

INSERT INTO [dbo].[MissionType]  ([Name],Version) values ('Командировку по России',1)
INSERT INTO [dbo].[MissionType]  ([Name],Version) values ('Командировку по СНГ',1)
INSERT INTO [dbo].[MissionType]  ([Name],Version) values ('Командировку  за рубеж',1)

INSERT INTO [dbo].[EmploymentType]  ([Name],Version) values ('По совместительству',1)
INSERT INTO [dbo].[EmploymentType]  ([Name],Version) values ('На основное место работы',1)
INSERT INTO [dbo].[EmploymentType]  ([Name],Version) values ('По срочному трудовому договору',1)
INSERT INTO [dbo].[EmploymentType]  ([Name],Version) values ('Переводом',1)

INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('Пятидневка   0,2 ставки',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('0,001',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('0,01',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('0,01',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('0,1',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('0,16',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('0,2 ставки',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('0,55 ставки',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('0,68 ставки (27,2 часа)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('0,7 ставки (28 часов)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('0,8 часов',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('0.8',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('1 час',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('10 часов (0,25)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('10-часовая',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('12 ч (0,3ставки)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('14ч (0,35 ставки)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('16 ч (0,4ставки)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('16,8 ч (0,42 ставки)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('20 часов',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('20 часов (0,5 ст.)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('24 ч(0,6ст)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('25 (0,625)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('29,2 часа (0.73 ст.)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('30 (0,75ст)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('32 часа ( 0.8ст.)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('33.6 часа ( 0.84ст.)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('34,8 часа ( 0,87 ст.)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('35 часов (0,87 ст.)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('35,6 часа (0,89ст.)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('36,4 часа ( 0,91ст.)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('36.8 часа  (0,92ст.)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('37,5 часов (0,94 ст)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('38,4 часа (0,96ст)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('39.6 часа ( 0.99ст.)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('4 часа (0,1 ст)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('6,4 часа (0,16 ст)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('6,8 часа (0,17 ст)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('График 15ч/н (Ярославль)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('график 2 через 2 2смена',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('график 2 через 2 по 11 часов 1смена',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('График 25ч/н (Ярославль)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('Основной',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('Основной график',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('Основной график (Ярославль)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('Основной график 20 часов',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('Основной график 20часов неделя (Иваново)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('Основной график 40часов неделя',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('Пятидневка',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('Пятидневка (30 часов)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('Пятидневка 0.4ст',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('пятидневка 10часов (Иваново)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('Пятидневка 20 часов',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('Пятидневка 20 часов',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('Пятидневка 30 часов',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('Пятидневка 32 часа',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('Пятидневка 35',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('Пятидневка 35 часов (Иваново)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('Пятидневка 37,5',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('Сменный',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('Сокращенный день по 7 часов (Ярославль)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('Сум. учет раб. времени',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('Суммированный учет',1)

INSERT INTO [dbo].[EmploymentAddition]  ([Code],[Name],[CalculationMethod],Version) values ('00002','Надбавка за выслугу лет рабочим и служащим #1114','По месячной тарифной ставке по часам',1)
INSERT INTO [dbo].[EmploymentAddition]  ([Code],[Name],[CalculationMethod],Version) values ('00002','Надбавка за квалификацию #1115','По месячной тарифной ставке по часам',1)
INSERT INTO [dbo].[EmploymentAddition]  ([Code],[Name],[CalculationMethod],Version) values ('00002','Надбавка за разъездной характер работы #1116','По месячной тарифной ставке по часам',1)
INSERT INTO [dbo].[EmploymentAddition]  ([Code],[Name],[CalculationMethod],Version) values ('00002','Надбавка Персональная #1117','По месячной тарифной ставке по часам',1)
INSERT INTO [dbo].[EmploymentAddition]  ([Code],[Name],[CalculationMethod],Version) values ('00002','Надбавка территориальная #1123','По месячной тарифной ставке по часам',1)
INSERT INTO [dbo].[EmploymentAddition]  ([Code],[Name],[CalculationMethod],Version) values ('00070','Районный коэффициент #1301','Процентом Зависимое второго уровня',1)
INSERT INTO [dbo].[EmploymentAddition]  ([Code],[Name],[CalculationMethod],Version) values (null,'Расширение зоны обслуживания #1120','По месячной тарифной ставке по часам Первичное',1)
INSERT INTO [dbo].[EmploymentAddition]  ([Code],[Name],[CalculationMethod],Version) values ('00070','Северная надбавка 1 #1302','Процентом Зависимое второго уровня',1)

--INSERT INTO [dbo].[DismissalType]  ([Name],[Reason],Version) values ('Заявление работника',1)
--INSERT INTO [dbo].[DismissalType]  ([Name],[Reason],Version) values ('Служебная записка',1)
--INSERT INTO [dbo].[DismissalType]  ([Name],[Reason],Version) values ('Распоряжение руководителя',1)
--INSERT INTO [dbo].[DismissalType]  ([Name],[Reason],Version) values ('Медицинское заключение',1)
--INSERT INTO [dbo].[DismissalType]  ([Name],[Reason],Version) values ('Приказ руководителя',1)
--INSERT INTO [dbo].[DismissalType]  ([Name],[Reason],Version) values ('Справка о смерти',1)

INSERT INTO [dbo].[DismissalType]  ([Name],[Reason],Version) values ('п. 1 ст. 81 ТК','Ликвидация',1)
INSERT INTO [dbo].[DismissalType]  ([Name],[Reason],Version) values ('п. 1 части 1 ст. 77 ТК','Соглашение сторон.',1)
INSERT INTO [dbo].[DismissalType]  ([Name],[Reason],Version) values ('п. 2 ст. 77 ТК','Истечение срока трудового договора.',1)
INSERT INTO [dbo].[DismissalType]  ([Name],[Reason],Version) values ('п. 2 части 1 ст. 81 ТК','Сокращение численности штата',1)
INSERT INTO [dbo].[DismissalType]  ([Name],[Reason],Version) values ('п. 3 ст. 77 ТК','Расторжение трудового договора по инициативе работника.',1)
INSERT INTO [dbo].[DismissalType]  ([Name],[Reason],Version) values ('п. 4 ст. 77 ТК','Расторжение трудового договора по инициативе работодателя',1)
INSERT INTO [dbo].[DismissalType]  ([Name],[Reason],Version) values ('п. 5 ст. 77 ТК','Перевод работника на другую работу к другому работодателю',1)
INSERT INTO [dbo].[DismissalType]  ([Name],[Reason],Version) values ('п. 5 части первой ст. 77','Перевод работника на выборную должность',1)
INSERT INTO [dbo].[DismissalType]  ([Name],[Reason],Version) values ('п. 6 ст. 81 ТК','Расторжение трудового договора по инициативе работодателя за прогул.',1)
INSERT INTO [dbo].[DismissalType]  ([Name],[Reason],Version) values ('п. 6 ст.83 ТК','Смерть  работника  либо работодателя - физ. лица, а также признание судом работника либо работодателя - физ. лица умершим',1)
INSERT INTO [dbo].[DismissalType]  ([Name],[Reason],Version) values ('п. 7 ч. 1 ст.81 ТК РФ','Трудовой договор расторгнут в связи с совершением виновных действий работником',1)
INSERT INTO [dbo].[DismissalType]  ([Name],[Reason],Version) values ('п. 7 части 1 ст. 77','Отказ работника от продолжения работы в связи с изменением определенных сторонами условий ТД',1)
INSERT INTO [dbo].[DismissalType]  ([Name],[Reason],Version) values ('п.3 части 1 ст. 77 ТК РФ','Собственное желание',1)
INSERT INTO [dbo].[DismissalType]  ([Name],[Reason],Version) values ('п.5 части первой ст. 83 ТК РФ','Трудовой договор прекращен в связи с признанием работника полностью неспособным к трудовой деятельности в соответствии с медицинским заключением',1)
INSERT INTO [dbo].[DismissalType]  ([Name],[Reason],Version) values ('п.6 ч.1 ст. 83','Трудовой договор прекращен в связи со смертью работника',1)

INSERT INTO [dbo].[TimesheetCorrectionType]  ([Code],[Name],[Reason],Version) values (2,'Оклад по часам #1102','По месячной тарифной ставке по часам',1)
INSERT INTO [dbo].[TimesheetCorrectionType]  ([Code],[Name],[Reason],Version) values (3,'Оплата по часовому тарифу #1103','По часовой тарифной ставке',1)
INSERT INTO [dbo].[TimesheetCorrectionType]  ([Code],[Name],[Reason],Version) values (66,'Оплата почасового простоя от оклада по часам #1707','По месячной тарифной ставке по часам',1)
INSERT INTO [dbo].[TimesheetCorrectionType]  ([Code],[Name],[Reason],Version) values (67,'Оплата почасового простоя по часовому тарифу# 1708','По часовой тарифной ставке',1)
INSERT INTO [dbo].[TimesheetCorrectionType]  ([Code],[Name],[Reason],Version) values (68,'Почасовой простой по вине работодателя #1702','По среднему заработку',1)



--INSERT INTO [dbo].[DismissalCompensationType]  ([Name],Version) values ('Тест 1',1)
--INSERT INTO [dbo].[DismissalCompensationType]  ([Name],Version) values ('Тест 2',1)


INSERT INTO [dbo].[SicklistPaymentRestrictType] ([Name],Version) values ('В размере ММОТ',1)
INSERT INTO [dbo].[SicklistPaymentRestrictType] ([Name],Version) values ('Общее, в соответствии с Законом о бюджете ФСС',1)

--INSERT INTO [dbo].[SicklistPaymentType]  ([Code],[Name],[PaymentMethod],Version) values (25,'БЛ по травме в быту (не оплачивается) #1805','Нулевая сумма',1)
--INSERT INTO [dbo].[SicklistPaymentType]  ([Code],[Name],[PaymentMethod],Version) values (26,'Доплата по больничным листам #1402','Доплата до среднего заработка ФСС',1)
--INSERT INTO [dbo].[SicklistPaymentType]  ([Code],[Name],[PaymentMethod],Version) values (22,'Оплата БЛ по травме на производстве #1403','По среднему заработку ФСС',1)
--INSERT INTO [dbo].[SicklistPaymentType]  ([Code],[Name],[PaymentMethod],Version) values (25,'Оплата больничных листов за счет работодателя #1426','По среднему заработку ФСС',1)
--INSERT INTO [dbo].[SicklistPaymentType]  ([Code],[Name],[PaymentMethod],Version) values (21,'Оплата больничных листов #1469','По среднему заработку ФСС',1)
--INSERT INTO [dbo].[SicklistPaymentType]  ([Code],[Name],[PaymentMethod],Version) values (10024,'Отсутствие по болезни (по беременности и родам) #1804','Нулевая сумма',1)
--INSERT INTO [dbo].[SicklistPaymentType]  ([Code],[Name],[PaymentMethod],Version) values (10023,'Отсутствие по болезни #1803','Нулевая сумма',1)
--INSERT INTO [dbo].[SicklistPaymentType]  ([Code],[Name],[PaymentMethod],Version) values (55,'Отсутствие по невыясненной причине #1806','Нулевая сумма',1)
--INSERT INTO [dbo].[SicklistPaymentType]  ([Code],[Name],[PaymentMethod],Version) values (71,'Пособие по уходу за ребёнком до 1.5 лет #1502','Пособие по уходу за ребенком до 1.5 лет',1)
--INSERT INTO [dbo].[SicklistPaymentType]  ([Code],[Name],[PaymentMethod],Version) values (72,'Пособие по уходу за ребёнком до 3 лет #1503','Пособие по уходу за ребенком до 3 лет',1)

INSERT INTO [dbo].[SicklistPaymentPercent]  ([SicklistPercent],SortOrder,Version) values (60,3,1)
INSERT INTO [dbo].[SicklistPaymentPercent]  ([SicklistPercent],SortOrder,Version) values (80,2,1)
INSERT INTO [dbo].[SicklistPaymentPercent]  ([SicklistPercent],SortOrder,Version) values (100,1,1)



set identity_insert  [TimesheetStatus] on
INSERT INTO [TimesheetStatus] (Id,Version,ShortName,Name) values (1,1,'Я','Явка')
INSERT INTO [TimesheetStatus] (Id,Version,ShortName,Name) values (2,1,'Б','Временная нетрудоспособность с назначением пособия согласно законодательству')
INSERT INTO [TimesheetStatus] (Id,Version,ShortName,Name) values (3,1,'Т','Временная нетрудоспособность без назнач. пособия в случаях, предусм. законодательством')
INSERT INTO [TimesheetStatus] (Id,Version,ShortName,Name) values (4,1,'ВЧ','Вечерние часы')
INSERT INTO [TimesheetStatus] (Id,Version,ShortName,Name) values (5,1,'Н','Ночные часы')
INSERT INTO [TimesheetStatus] (Id,Version,ShortName,Name) values (6,1,'В','Выходные и нерабочие дни')
INSERT INTO [TimesheetStatus] (Id,Version,ShortName,Name) values (7,1,'К','Командировка')
INSERT INTO [TimesheetStatus] (Id,Version,ShortName,Name) values (8,1,'ОТ','Отпуск')
INSERT INTO [TimesheetStatus] (Id,Version,ShortName,Name) values (9,1,'ОЗ','Отпуск без сохранения заработной платы в случаях, предусмотренных законодательством')
INSERT INTO [TimesheetStatus] (Id,Version,ShortName,Name) values (10,1,'ДО','Отпуск без сохранения заработной платы, предоставляемый сотр. по разреш. работодателя')
INSERT INTO [TimesheetStatus] (Id,Version,ShortName,Name) values (11,1,'Р','Отпуск по беременности и родам (отпуск в связи с усыновлением новорожд. ребенка)')
INSERT INTO [TimesheetStatus] (Id,Version,ShortName,Name) values (12,1,'ОЖ','Отпуск по уходу за ребенком до достижения им возраста трех лет')
INSERT INTO [TimesheetStatus] (Id,Version,ShortName,Name) values (13,1,'РВ','Продолжительность работы в выходные и нерабочие, праздничные дни')
INSERT INTO [TimesheetStatus] (Id,Version,ShortName,Name) values (14,1,'С','Продолжительность сверхурочной работы')
INSERT INTO [TimesheetStatus] (Id,Version,ShortName,Name) values (15,1,'ПР','Прогулы (отсутствие на рабочем месте без уваж. причин в теч. времени, уст. законодательством)')
INSERT INTO [TimesheetStatus] (Id,Version,ShortName,Name) values (16,1,'НН','Неявки по невыясненным причинам')
INSERT INTO [TimesheetStatus] (Id,Version,ShortName,Name) values (17,1,'ВП','Простои по вине сотрудника')
INSERT INTO [TimesheetStatus] (Id,Version,ShortName,Name) values (18,1,'РП','Время простоя по вине работодателя')
set identity_insert  [TimesheetStatus] off


declare @typeId int
declare @firstTypeId int
declare @firstSubTypeId int
INSERT INTO [EmployeeDocumentType] ([Name],Version) values ('Отпуск',1) 
set @typeId = @@Identity
set @firstTypeId = @typeId
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('Ежегодный',1,@typeId) 
set @firstSubTypeId = @@Identity
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('Дополнительный',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('Учебный',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('По уходу за ребенком',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('Без сохранения ЗП',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('Донорские дни',1,@typeId) 

INSERT INTO [EmployeeDocumentType] ([Name],Version) values ('Командировка',1) 
set @typeId = @@Identity
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('По РФ',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('По СНГ',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('За рубеж',1,@typeId) 

INSERT INTO [EmployeeDocumentType] ([Name],Version) values ('Больничный',1) 
set @typeId = @@Identity
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('По нетрудоспособности',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('По уходу за ребенком',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('Травма на производстве',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('Санитарно — курортный',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('По беременности и родам',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('По уходу за взрослым',1,@typeId) 


INSERT INTO [EmployeeDocumentType] ([Name],Version) values ('Отсутствие на рабочем месте',1) 
set @typeId = @@Identity
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('Прогул',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('По невыясненной причине',1,@typeId) 

INSERT INTO [EmployeeDocumentType] ([Name],Version) values ('Увольнение',1) 
set @typeId = @@Identity
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('По собственному желанию',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('По сокращению штата',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('По соглашению сторон',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('По инициативе работодателя',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('В связи с переводом',1,@typeId) 

INSERT INTO [EmployeeDocumentType] ([Name],Version) values ('Перемещение',1) 
set @typeId = @@Identity
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('Изменение оплаты труда',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('места работы',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('должности',1,@typeId) 

INSERT INTO [EmployeeDocumentType] ([Name],Version) values ('Прочее',1) 
set @typeId = @@Identity
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('Простой по вине работодателя',1,@typeId) 

INSERT INTO [EmployeeDocumentType] ([Name],Version) values ('Мат. помощь',1) 
set @typeId = @@Identity
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('Погребение',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('Регистрация брака (прочее)',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('Рождение, усыновление',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('Пособие по уходу за реб. 3х лет',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('Пособие по уходу за реб. 1,5х лет',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('Сохраняемый заработок на время трудоустройства',1,@typeId) 

INSERT INTO [EmployeeDocumentType] ([Name],Version) values ('Выплата за счет ФСС',1) 
set @typeId = @@Identity
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('Пособие ФСС в связи со смертью',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('Пособие ФСС при постановке на учет в ранние сроки беременности',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('Пособие ФСС при рождении ребенка',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('Пособие ФСС при усыновлении ребенка',1,@typeId) 

INSERT INTO [EmployeeDocumentType] ([Name],Version) values ('Удержание',1) 
set @typeId = @@Identity
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('Сотовая связь',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('Исполнительный лист',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('Обслуживание авто',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('Налоговый вычет',1,@typeId) 


-- User
INSERT INTO [Users] (IsActive,IsFirstTimeLogin, Login ,Password,DateAccept                ,       Name,                         Version,  [DateRelease]    , [RoleId],      [Code]		, [IsNew]	 ) 
VALUES			   (1,       		0               ,'admin','adminadmin' ,	GetDate(),      N'Администратор',             1,		  null ,          1,           'АА0000000001' , 0)
declare @managerId int
INSERT INTO [Users] (IsActive,IsFirstTimeLogin, Login ,Password,DateAccept                ,       Name,                         Version,  [DateRelease]    , [RoleId],      [Code]  , [IsNew], PositionId) 
VALUES			   (1,       	0              ,'manager' ,'manager'  ,	'2008-12-01 15:13:25:000',  N'Руководитель',              1,         null								, 4,		   'АВ0000000001' , 0, @ManPositionId)
set @managerId = @@Identity
--INSERT INTO UserToDepartment (UserId,DepartmentId,Version) values (@managerId,@DepartmentId,1)
--INSERT INTO UserToDepartment (UserId,DepartmentId,Version) values (@managerId,@Department1Id,1)
declare @personnelId int
INSERT INTO [Users] (IsActive,IsFirstTimeLogin, Login ,Password,DateAccept                ,               Name,                          Version,  [DateRelease]    , [RoleId],      [Code]  , [IsNew], PositionId) 
VALUES			   (1,       	0              ,'personnel' ,'personnel'  ,	'2008-12-01 15:13:25:000',    N'Кадровик',                    1,         null								, 8,		   'АГ0000000001' , 0, @PerPositionId)
set @personnelId = @@Identity
--INSERT INTO UserToDepartment (UserId,DepartmentId,Version) values (@personnelId,@DepartmentId,1)
--INSERT INTO UserToDepartment (UserId,DepartmentId,Version) values (@personnelId,@Department1Id,1)
declare @budgetId int
INSERT INTO [Users] (IsActive,IsFirstTimeLogin, Login ,Password,DateAccept                ,              Name,               Version,  [DateRelease]    , [RoleId],      [Code] , [IsNew]) 
VALUES			   (1,       	0              ,'budget' ,'budget'  ,	'2008-12-01 15:13:25:000',       N'Бюджет',           1,         null								        , 16,		   'АГ0000000001' , 0)
set @budgetId = @@Identity
declare @outsorsingId int
INSERT INTO [Users] (IsActive,IsFirstTimeLogin, Login ,Password,DateAccept                ,                       Name,                           Version,  [DateRelease]    , [RoleId],      [Code] , [IsNew] ) 
VALUES			   (1,       	0              ,'outsorsing' ,'outsorsing'  ,	'2008-12-01 15:13:25:000',       N'Аутсорсинг',                        1,         null,              32,		       'АД0000000001' , 0)
set @outsorsingId = @@Identity
declare @userId int
INSERT INTO [Users] (IsActive,IsFirstTimeLogin, Login ,Password,DateAccept                ,       Name,                        Version,  [DateRelease]    , [RoleId],      [Code] ,             ManagerId,         PersonnelManagerId, OrganizationId,DepartmentId,PositionId , [IsNew]) 
VALUES			   (1,       	0              ,'user' ,'user'  ,	'2008-12-01 15:13:25:000',    N'Пользователь',                   1,         null            , 2,		   'АБ0000000001' ,  @managerId,       @personnelId,       @OrganizationId,@DepartmentId,@PositionId , 0)
set @userId = @@Identity
--INSERT INTO UserToDepartment (UserId,DepartmentId,Version) values (@userId,@DepartmentId,1)

declare @user1Id int
INSERT INTO [Users] (IsActive,IsFirstTimeLogin, Login ,Password,DateAccept                ,       Name,                           Version,  [DateRelease]    , [RoleId],      [Code] , ManagerId,PersonnelManagerId,            OrganizationId,DepartmentId,PositionId , [IsNew]) 
VALUES			   (1,       	0              ,'ivanov' ,'ivanov'  ,	'2008-12-01 15:13:25:000',N'Иванов Иван Иванович',            1,         null            , 2,		   'АЕ0000000001' ,  @managerId,       @personnelId,@Organization1Id,@Department1Id,@Position1Id , 0)
set @user1Id = @@Identity
--INSERT INTO UserToDepartment (UserId,DepartmentId,Version) values (@user1Id,@Department1Id,1)

INSERT INTO [Users] (IsActive,IsFirstTimeLogin, Login ,Password,DateAccept                ,            Name,                         Version,  [DateRelease]    , [RoleId],      [Code] , ManagerId,PersonnelManagerId , [IsNew], OrganizationId,DepartmentId,PositionId) 
VALUES			   (1,       	0              ,'petrov' ,'petrov'  ,	'2008-12-01 15:13:25:000',      N'Петров Петр Петрович',         1,         null            , 2,		   'АЖ0000000001' ,  @managerId,       @personnelId , 0 ,  @OrganizationId,@DepartmentId, @PositionId )

 
INSERT INTO DBVERSION (Version) VALUES('1.0.0.1')


--insert into dbo.Document
--([Version],LastModifiedDate, Comment, TypeId,          SubtypeId,     UserId, SendEmailToBilling)
--values (1,'2011-10-22',     'Тестовый',  @firstTypeId, @firstSubTypeId, @userId, 0)

