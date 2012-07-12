if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_TimesheetDay_Status]') AND parent_object_id = OBJECT_ID('TimesheetDay'))
alter table TimesheetDay  drop constraint FK_TimesheetDay_Status

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_TimesheetDay_Timesheet]') AND parent_object_id = OBJECT_ID('TimesheetDay'))
alter table TimesheetDay  drop constraint FK_TimesheetDay_Timesheet

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

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Attachment_Document]') AND parent_object_id = OBJECT_ID('Attachment'))
alter table Attachment  drop constraint FK_Attachment_Document

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_UserToDepartment_User]') AND parent_object_id = OBJECT_ID('[UserToDepartment]'))
alter table [UserToDepartment]  drop constraint FK_UserToDepartment_User

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_UserToDepartment_Department]') AND parent_object_id = OBJECT_ID('[UserToDepartment]'))
alter table [UserToDepartment]  drop constraint FK_UserToDepartment_Department

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_DocumentComment_User]') AND parent_object_id = OBJECT_ID('DocumentComment'))
alter table DocumentComment  drop constraint FK_DocumentComment_User

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_DocumentComment_Document]') AND parent_object_id = OBJECT_ID('DocumentComment'))
alter table DocumentComment  drop constraint FK_DocumentComment_Document

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Vacation_VacationType]') AND parent_object_id = OBJECT_ID('Vacation'))
alter table Vacation  drop constraint FK_Vacation_VacationType

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Vacation_RequestStatus]') AND parent_object_id = OBJECT_ID('Vacation'))
alter table Vacation  drop constraint FK_Vacation_RequestStatus

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Vacation_User]') AND parent_object_id = OBJECT_ID('Vacation'))
alter table Vacation  drop constraint FK_Vacation_User

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Vacation_CreatorUser]') AND parent_object_id = OBJECT_ID('Vacation'))
alter table Vacation  drop constraint FK_Vacation_CreatorUser

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Vacation_TimesheetStatus]') AND parent_object_id = OBJECT_ID('Vacation'))
alter table Vacation  drop constraint FK_Vacation_TimesheetStatus

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_DocumentSubType_EmployeeDocumentType]') AND parent_object_id = OBJECT_ID('EmployeeDocumentSubType'))
alter table EmployeeDocumentSubType  drop constraint FK_DocumentSubType_EmployeeDocumentType

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Document_EmployeeDocumentType]') AND parent_object_id = OBJECT_ID('Document'))
alter table Document  drop constraint FK_Document_EmployeeDocumentType

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Document_EmployeeDocumentSubType]') AND parent_object_id = OBJECT_ID('Document'))
alter table Document  drop constraint FK_Document_EmployeeDocumentSubType

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Document_User]') AND parent_object_id = OBJECT_ID('Document'))
alter table Document  drop constraint FK_Document_User

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Timesheet_User]') AND parent_object_id = OBJECT_ID('Timesheet'))
alter table Timesheet  drop constraint FK_Timesheet_User

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_UserLogin_User]') AND parent_object_id = OBJECT_ID('UserLogin'))
alter table UserLogin  drop constraint FK_UserLogin_User

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_VacationComment_User]') AND parent_object_id = OBJECT_ID('VacationComment'))
alter table VacationComment  drop constraint FK_VacationComment_User

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_VacationComment_Vacation]') AND parent_object_id = OBJECT_ID('VacationComment'))
alter table VacationComment  drop constraint FK_VacationComment_Vacation

if exists (select * from dbo.sysobjects where id = object_id(N'TimesheetDay') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table TimesheetDay
if exists (select * from dbo.sysobjects where id = object_id(N'TimesheetStatus') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table TimesheetStatus
if exists (select * from dbo.sysobjects where id = object_id(N'[Users]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [Users]
if exists (select * from dbo.sysobjects where id = object_id(N'Attachment') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Attachment
if exists (select * from dbo.sysobjects where id = object_id(N'[UserToDepartment]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [UserToDepartment]
if exists (select * from dbo.sysobjects where id = object_id(N'Organization') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Organization
if exists (select * from dbo.sysobjects where id = object_id(N'Role') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Role
if exists (select * from dbo.sysobjects where id = object_id(N'RequestStatus') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table RequestStatus
if exists (select * from dbo.sysobjects where id = object_id(N'Information') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Information
if exists (select * from dbo.sysobjects where id = object_id(N'Department') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Department
if exists (select * from dbo.sysobjects where id = object_id(N'Position') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Position
if exists (select * from dbo.sysobjects where id = object_id(N'DocumentComment') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table DocumentComment
if exists (select * from dbo.sysobjects where id = object_id(N'ExportImportAction') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table ExportImportAction
if exists (select * from dbo.sysobjects where id = object_id(N'DBVersion') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table DBVersion
if exists (select * from dbo.sysobjects where id = object_id(N'Vacation') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Vacation
if exists (select * from dbo.sysobjects where id = object_id(N'EmployeeDocumentType') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table EmployeeDocumentType
if exists (select * from dbo.sysobjects where id = object_id(N'EmployeeDocumentSubType') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table EmployeeDocumentSubType
if exists (select * from dbo.sysobjects where id = object_id(N'Document') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Document
if exists (select * from dbo.sysobjects where id = object_id(N'Settings') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Settings
if exists (select * from dbo.sysobjects where id = object_id(N'Timesheet') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Timesheet
if exists (select * from dbo.sysobjects where id = object_id(N'UserLogin') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table UserLogin
if exists (select * from dbo.sysobjects where id = object_id(N'VacationType') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table VacationType
if exists (select * from dbo.sysobjects where id = object_id(N'VacationComment') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table VacationComment

create table TimesheetDay (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  Number INT not null,
  Hours REAL not null,
  StatusId INT not null,
  TimesheetId INT not null,
  constraint PK_TimesheetDay  primary key (Id)
)
create table TimesheetStatus (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  ShortName NVARCHAR(2) not null,
  Name NVARCHAR(255) not null,
  constraint PK_TimesheetStatus  primary key (Id)
)
create table [Users] (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  IsFirstTimeLogin BIT not null,
  IsActive BIT not null,
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
  constraint PK_Users primary key (Id)
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
create table [UserToDepartment] (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  UserId INT null,
  DepartmentId INT null,
  constraint PK_UserToDepartment primary key (Id)
)
create table Organization (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  Code INT null,
  Name NVARCHAR(128) null,
  constraint PK_Organization  primary key (Id)
)
create table Role (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  Name NVARCHAR(100) not null,
  constraint PK_Role  primary key (Id)
)
create table RequestStatus (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  Name NVARCHAR(128) null,
  constraint PK_RequestStatus  primary key (Id)
)
create table Information (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  Subject NVARCHAR(1024) not null,
  Message NVARCHAR(MAX) not null,
  constraint PK_Information  primary key (Id)
)
create table Department (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  Code INT null,
  Name NVARCHAR(128) null,
  constraint PK_Department  primary key (Id)
)
create table Position (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  Code INT null,
  Name NVARCHAR(128) null,
  constraint PK_Position  primary key (Id)
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
create table ExportImportAction (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  Type INT not null,
  Date DATETIME not null,
  Month DATETIME null,
  constraint PK_ExportImportAction  primary key (Id)
)
create table DBVersion (
 Id INT IDENTITY NOT NULL,
  Version NVARCHAR(20) not null,
  constraint PK_DBVersion  primary key (Id)
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
  StatusId INT not null,
  UserId INT not null,
  CreatorId INT not null,
  ManagerDateAccept DATETIME null,
  PersonnelManagerDateAccept DATETIME null,
  TimesheetStatusId INT null,
  constraint PK_Vacation  primary key (Id)
)
create table EmployeeDocumentType (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  Name NVARCHAR(100) not null,
  constraint PK_EmployeeDocumentType  primary key (Id)
)
create table EmployeeDocumentSubType (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  Name NVARCHAR(100) not null,
  TypeId INT not null,
  constraint PK_EmployeeDocumentSubType  primary key (Id)
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
create table VacationType (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  Code INT null,
  Name NVARCHAR(128) null,
  constraint PK_VacationType  primary key (Id)
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
create index IX_TimesheetDay_Status_Id on TimesheetDay (StatusId)
create index IX_TimesheetDay_Timesheet_Id on TimesheetDay (TimesheetId)
alter table TimesheetDay add constraint FK_TimesheetDay_Status foreign key (StatusId) references TimesheetStatus
alter table TimesheetDay add constraint FK_TimesheetDay_Timesheet foreign key (TimesheetId) references Timesheet
create index IX_USER_ROLE_ID on [Users] (RoleId)
create index IX_User_UserManager_Id on [Users] (ManagerId)
create index IX_User_PersonnelManager_Id on [Users] (PersonnelManagerId)
create index IX_User_Organization_Id on [Users] (OrganizationId)
create index IX_User_Position_Id on [Users] (PositionId)
alter table [Users] add constraint FK_USER_ROLE foreign key (RoleId) references Role
alter table [Users] add constraint FK_User_UserManager foreign key (ManagerId) references [Users]
alter table [Users] add constraint FK_User_PersonnelManager foreign key (PersonnelManagerId) references [Users]
alter table [Users] add constraint FK_User_Organization foreign key (OrganizationId) references Organization
alter table [Users] add constraint FK_User_Position foreign key (PositionId) references Position
create index IX_Attachment_Document_Id on Attachment (DocumentId)
alter table Attachment add constraint FK_Attachment_Document foreign key (DocumentId) references Document
create index IX_UserToDepartment_User_Id on [UserToDepartment] (UserId)
create index IX_UserToDepartment_Department_Id on [UserToDepartment] (DepartmentId)
alter table [UserToDepartment] add constraint FK_UserToDepartment_User foreign key (UserId) references [Users]
alter table [UserToDepartment] add constraint FK_UserToDepartment_Department foreign key (DepartmentId) references Department
create index IX_DocumentComment_User_Id on DocumentComment (UserId)
create index IX_DocumentComment_Document_Id on DocumentComment (DocumentId)
alter table DocumentComment add constraint FK_DocumentComment_User foreign key (UserId) references [Users]
alter table DocumentComment add constraint FK_DocumentComment_Document foreign key (DocumentId) references Document
create index Vacation_VacationType on Vacation (TypeId)
create index Vacation_RequestStatus on Vacation (StatusId)
create index IX_Vacation_User_Id on Vacation (UserId)
create index IX_Vacation_CreatorUser_Id on Vacation (CreatorId)
create index Vacation_TimesheetStatus on Vacation (TimesheetStatusId)
alter table Vacation add constraint FK_Vacation_VacationType foreign key (TypeId) references VacationType
alter table Vacation add constraint FK_Vacation_RequestStatus foreign key (StatusId) references RequestStatus
alter table Vacation add constraint FK_Vacation_User foreign key (UserId) references [Users]
alter table Vacation add constraint FK_Vacation_CreatorUser foreign key (CreatorId) references [Users]
alter table Vacation add constraint FK_Vacation_TimesheetStatus foreign key (TimesheetStatusId) references TimesheetStatus
create index IX_DocumentSubType_EmployeeDocumentType_Id on EmployeeDocumentSubType (TypeId)
alter table EmployeeDocumentSubType add constraint FK_DocumentSubType_EmployeeDocumentType foreign key (TypeId) references EmployeeDocumentType
create index IX_Document_EmployeeDocumentType_Id on Document (TypeId)
create index IX_Document_EmployeeDocumentSubType_Id on Document (SubTypeId)
create index IX_Document_User_Id on Document (UserId)
alter table Document add constraint FK_Document_EmployeeDocumentType foreign key (TypeId) references EmployeeDocumentType
alter table Document add constraint FK_Document_EmployeeDocumentSubType foreign key (SubTypeId) references EmployeeDocumentSubType
alter table Document add constraint FK_Document_User foreign key (UserId) references [Users]
create index IX_Timesheet_User_Id on Timesheet (UserId)
alter table Timesheet add constraint FK_Timesheet_User foreign key (UserId) references [Users]
alter table UserLogin add constraint FK_UserLogin_User foreign key (UserId) references [Users]
create index IX_VacationComment_User_Id on VacationComment (UserId)
create index IX_VacationComment_Vacation_Id on VacationComment (VacationId)
alter table VacationComment add constraint FK_VacationComment_User foreign key (UserId) references [Users]
alter table VacationComment add constraint FK_VacationComment_Vacation foreign key (VacationId) references Vacation

set identity_insert  [Role] on
INSERT INTO [Role] (Id,[Name],Version) values (1,'Администратор',1) 
INSERT INTO [Role] (Id,[Name],Version) values (2,'Сотрудник',1) 
INSERT INTO [Role] (Id,[Name],Version) values (3,'Руководитель',1) 
INSERT INTO [Role] (Id,[Name],Version) values (4,'Кадровик',1) 
INSERT INTO [Role] (Id,[Name],Version) values (5,'Бюджет',1) 
INSERT INTO [Role] (Id,[Name],Version) values (6,'Отусорсинг',1)
set identity_insert  [Role] off 

set identity_insert  [RequestStatus] on
INSERT INTO [RequestStatus] (Id,[Name],Version) values (1,'Не одобрен сотрудником',1) 
INSERT INTO [RequestStatus] (Id,[Name],Version) values (2,'Одобрен сотрудником',1) 
INSERT INTO [RequestStatus] (Id,[Name],Version) values (3,'Одобрен руководителем',1) 
INSERT INTO [RequestStatus] (Id,[Name],Version) values (4,'Одобрен кадровиком',1)
INSERT INTO [RequestStatus] (Id,[Name],Version) values (5,'Выгружен в 1С',1)
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
INSERT INTO [Users] (IsActive,IsFirstTimeLogin, Login ,Password,DateAccept                ,       Name,                         Version,  [DateRelease]    , [RoleId],      [Code]			 ) 
VALUES			   (1,       		0               ,'admin','adminadmin' ,	GetDate(),      N'Администратор',             1,		  null ,          1,           'АА0000000001' )
declare @managerId int
INSERT INTO [Users] (IsActive,IsFirstTimeLogin, Login ,Password,DateAccept                ,       Name,                         Version,  [DateRelease]    , [RoleId],      [Code] ) 
VALUES			   (1,       	1              ,'manager' ,'manager'  ,	'2008-12-01 15:13:25:000',  N'Руководитель',              1,         null								, 3,		   'АВ0000000001')
set @managerId = @@Identity
INSERT INTO UserToDepartment (UserId,DepartmentId,Version) values (@managerId,@DepartmentId,1)
INSERT INTO UserToDepartment (UserId,DepartmentId,Version) values (@managerId,@Department1Id,1)
declare @personnelId int
INSERT INTO [Users] (IsActive,IsFirstTimeLogin, Login ,Password,DateAccept                ,               Name,                          Version,  [DateRelease]    , [RoleId],      [Code] ) 
VALUES			   (1,       	1              ,'personnel' ,'personnel'  ,	'2008-12-01 15:13:25:000',    N'Кадровик',                    1,         null								, 4,		   'АГ0000000001')
set @personnelId = @@Identity
INSERT INTO UserToDepartment (UserId,DepartmentId,Version) values (@personnelId,@DepartmentId,1)
INSERT INTO UserToDepartment (UserId,DepartmentId,Version) values (@personnelId,@Department1Id,1)
declare @budgetId int
INSERT INTO [Users] (IsActive,IsFirstTimeLogin, Login ,Password,DateAccept                ,              Name,               Version,  [DateRelease]    , [RoleId],      [Code] ) 
VALUES			   (1,       	0              ,'budget' ,'budget'  ,	'2008-12-01 15:13:25:000',       N'Бюджет',           1,         null								        , 5,		   'АГ0000000001')
set @budgetId = @@Identity
declare @outsorsingId int
INSERT INTO [Users] (IsActive,IsFirstTimeLogin, Login ,Password,DateAccept                ,                       Name,                           Version,  [DateRelease]    , [RoleId],      [Code] ) 
VALUES			   (1,       	0              ,'outsorsing' ,'outsorsing'  ,	'2008-12-01 15:13:25:000',       N'Аутсорсинг',                        1,         null,              6,		       'АД0000000001')
set @outsorsingId = @@Identity
declare @userId int
INSERT INTO [Users] (IsActive,IsFirstTimeLogin, Login ,Password,DateAccept                ,       Name,                        Version,  [DateRelease]    , [RoleId],      [Code] ,             ManagerId,         PersonnelManagerId, OrganizationId,/*DepartmentId,*/PositionId) 
VALUES			   (1,       	1              ,'user' ,'user'  ,	'2008-12-01 15:13:25:000',    N'Пользователь',                   1,         null            , 2,		   'АБ0000000001' ,  @managerId,       @personnelId,       @OrganizationId,/*@DepartmentId,*/@PositionId)
set @userId = @@Identity
INSERT INTO UserToDepartment (UserId,DepartmentId,Version) values (@userId,@DepartmentId,1)

declare @user1Id int
INSERT INTO [Users] (IsActive,IsFirstTimeLogin, Login ,Password,DateAccept                ,       Name,                           Version,  [DateRelease]    , [RoleId],      [Code] , ManagerId,PersonnelManagerId,            OrganizationId,/*DepartmentId,*/PositionId) 
VALUES			   (1,       	1              ,'ivanov' ,'ivanov'  ,	'2008-12-01 15:13:25:000',N'Иванов Иван Иванович',            1,         null            , 2,		   'АЕ0000000001' ,  @managerId,       @personnelId,@Organization1Id,/*@Department1Id,*/@Position1Id)
set @user1Id = @@Identity
INSERT INTO UserToDepartment (UserId,DepartmentId,Version) values (@user1Id,@Department1Id,1)

INSERT INTO [Users] (IsActive,IsFirstTimeLogin, Login ,Password,DateAccept                ,            Name,                         Version,  [DateRelease]    , [RoleId],      [Code] , ManagerId,PersonnelManagerId) 
VALUES			   (1,       	1              ,'petrov' ,'petrov'  ,	'2008-12-01 15:13:25:000',      N'Петров Петр Петрович',         1,         null            , 2,		   'АЖ0000000001' ,  @managerId,       @personnelId)

 
INSERT INTO DBVERSION (Version) VALUES('1.0.0.1')


--insert into dbo.Document
--([Version],LastModifiedDate, Comment, TypeId,          SubtypeId,     UserId, SendEmailToBilling)
--values (1,'2011-10-22',     'Тестовый',  @firstTypeId, @firstSubTypeId, @userId, 0)

