if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_HelpServiceRequest_HelpServiceType]') AND parent_object_id = OBJECT_ID('HelpServiceRequest'))
alter table HelpServiceRequest  drop constraint FK_HelpServiceRequest_HelpServiceType

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_HelpServiceRequest_HelpServiceProductionTime]') AND parent_object_id = OBJECT_ID('HelpServiceRequest'))
alter table HelpServiceRequest  drop constraint FK_HelpServiceRequest_HelpServiceProductionTime

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_HelpServiceRequest_HelpServiceTransferMethod]') AND parent_object_id = OBJECT_ID('HelpServiceRequest'))
alter table HelpServiceRequest  drop constraint FK_HelpServiceRequest_HelpServiceTransferMethod

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_HelpServiceRequest_HelpServicePeriod]') AND parent_object_id = OBJECT_ID('HelpServiceRequest'))
alter table HelpServiceRequest  drop constraint FK_HelpServiceRequest_HelpServicePeriod

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_HelpServiceRequest_User]') AND parent_object_id = OBJECT_ID('HelpServiceRequest'))
alter table HelpServiceRequest  drop constraint FK_HelpServiceRequest_User

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_HelpServiceRequest_CreatorUser]') AND parent_object_id = OBJECT_ID('HelpServiceRequest'))
alter table HelpServiceRequest  drop constraint FK_HelpServiceRequest_CreatorUser

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_HelpServiceRequest_Consultant]') AND parent_object_id = OBJECT_ID('HelpServiceRequest'))
alter table HelpServiceRequest  drop constraint FK_HelpServiceRequest_Consultant

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_HelpServiceRequestComment_User]') AND parent_object_id = OBJECT_ID('HelpServiceRequestComment'))
alter table HelpServiceRequestComment  drop constraint FK_HelpServiceRequestComment_User

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_HelpServiceRequestComment_HelpServiceRequest]') AND parent_object_id = OBJECT_ID('HelpServiceRequestComment'))
alter table HelpServiceRequestComment  drop constraint FK_HelpServiceRequestComment_HelpServiceRequest

if exists (select * from dbo.sysobjects where id = object_id(N'HelpServiceRequest') and OBJECTPROPERTY(id, N'IsUserTable') = 1) 
	drop table HelpServiceRequest
if exists (select * from dbo.sysobjects where id = object_id(N'HelpServiceRequestComment') and OBJECTPROPERTY(id, N'IsUserTable') = 1) 
	drop table HelpServiceRequestComment
GO

create table HelpServiceRequest (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  CreateDate DATETIME not null,
  EditDate DATETIME not null,
  SendDate DATETIME null,
  BeginWorkDate DATETIME null,
  EndWorkDate DATETIME null,
  Number INT not null,
  TypeId INT not null,
  ProductionTimeId INT not null,
  TransferMethodId INT not null,
  PeriodId INT null,
  Requirements NVARCHAR(256) null,
  UserId INT not null,
  CreatorId INT not null,
  ConsultantId INT null,
  constraint PK_HelpServiceRequest  primary key (Id)
)
create table HelpServiceRequestComment (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  UserId INT not null,
  HelpServiceRequestId INT not null,
  DateCreated DATETIME not null,
  Comment NVARCHAR(256) not null,
  constraint PK_HelpServiceRequestComment  primary key (Id)
)

create index HelpServiceRequest_HelpServiceType on HelpServiceRequest (TypeId)
create index HelpServiceRequest_HelpServiceProductionTime on HelpServiceRequest (ProductionTimeId)
create index HelpServiceRequest_HelpServiceTransferMethod on HelpServiceRequest (TransferMethodId)
create index HelpServiceRequest_HelpServicePeriod on HelpServiceRequest (PeriodId)
create index IX_HelpServiceRequest_User_Id on HelpServiceRequest (UserId)
create index IX_HelpServiceRequest_CreatorUser_Id on HelpServiceRequest (CreatorId)
create index IX_HelpServiceRequest_Consultant_Id on HelpServiceRequest (ConsultantId)
alter table HelpServiceRequest add constraint FK_HelpServiceRequest_HelpServiceType foreign key (TypeId) references HelpServiceType
alter table HelpServiceRequest add constraint FK_HelpServiceRequest_HelpServiceProductionTime foreign key (ProductionTimeId) references HelpServiceProductionTime
alter table HelpServiceRequest add constraint FK_HelpServiceRequest_HelpServiceTransferMethod foreign key (TransferMethodId) references HelpServiceTransferMethod
alter table HelpServiceRequest add constraint FK_HelpServiceRequest_HelpServicePeriod foreign key (PeriodId) references HelpServicePeriod
alter table HelpServiceRequest add constraint FK_HelpServiceRequest_User foreign key (UserId) references [Users]
alter table HelpServiceRequest add constraint FK_HelpServiceRequest_CreatorUser foreign key (CreatorId) references [Users]
alter table HelpServiceRequest add constraint FK_HelpServiceRequest_Consultant foreign key (ConsultantId) references [Users]
create index IX_HelpServiceRequestComment_User on HelpServiceRequestComment (UserId)
create index IX_HelpServiceRequestComment_HelpServiceRequest on HelpServiceRequestComment (HelpServiceRequestId)
alter table HelpServiceRequestComment add constraint FK_HelpServiceRequestComment_User foreign key (UserId) references [Users]
alter table HelpServiceRequestComment add constraint FK_HelpServiceRequestComment_HelpServiceRequest foreign key (HelpServiceRequestId) references HelpServiceRequest
GO